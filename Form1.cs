using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Windows.Forms;
using DShowNET;
using DShowNET.Device;
using SampleGrabberNET;
using MathMatrix;

namespace capTv
{
    public partial class Form1 : Form, ISampleGrabberCB
    {
        #region capture Param
        /// <summary> flag to detect first Form appearance </summary>
       // private bool firstActive;
        private bool firstActive;
        /// <summary> base filter of the actually used video devices. </summary>
        private IBaseFilter capFilter;

        /// <summary> graph builder interface. </summary>
        private IGraphBuilder graphBuilder;

        /// <summary> capture graph builder interface. </summary>
        private ICaptureGraphBuilder2 capGraph;
        private ISampleGrabber sampGrabber;

        /// <summary> control interface. </summary>
        private IMediaControl mediaCtrl;

        /// <summary> event interface. </summary>
        private IMediaEventEx mediaEvt;

        /// <summary> video window interface. </summary>
        private IVideoWindow videoWin;

        /// <summary> grabber filter interface. </summary>
        private IBaseFilter baseGrabFlt;

        /// <summary> structure describing the bitmap to grab. </summary>
        private VideoInfoHeader videoInfoHeader;
        private bool captured = true;
        private int bufferedSize;

        /// <summary> buffer for bitmap data. </summary>
        private byte[] savedArray;

        /// <summary> list of installed video devices. </summary>
        private ArrayList capDevices;

        private const int WM_GRAPHNOTIFY = 0x00008001;	// message from graph

        private const int WS_CHILD = 0x40000000;	// attributes for video window
        private const int WS_CLIPCHILDREN = 0x02000000;
        private const int WS_CLIPSIBLINGS = 0x04000000;

        /// <summary> event when callback has finished (ISampleGrabberCB.BufferCB). </summary>
        private delegate void CaptureDone();

#if DEBUG
        private int rotCookie = 0;
#endif
        #endregion
        Improcess calimage = new Improcess();
        Robotcontrol controlRobot = new Robotcontrol();
        Bitmap result;
        bool check = true;
        public Homogenous Hc1, Hc2;
        public double P1, P2, P3;
        public Form1()
        {
            InitializeComponent();
            initTv();
           com.connect();
          //  controlRobot.Robot_connect();
         //   com.connect();
            com.initpolaris();
            com.readdata();
            bt_cal2.Visible = false;
            bt_calculate.Visible = false;
            bt_robotmove.Visible = false;
            //lb_step1.Visible = false;
            lb_step2.Visible = false;
            lb_step3.Visible = false;
            lb_step4.Visible = false;
            lb_step5.Visible = false;
            lb_step6.Visible = false;
            lb_step7.Visible = false;

        }
        #region Capture for C-arm
        private void initTv()
        {
            if (firstActive)
                return;
            firstActive = true;
            if (!DsUtils.IsCorrectDirectXVersion())
            {
                MessageBox.Show(this, "DirectX 8.1 NOT installed!", "DirectShow.NET", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                this.Close(); return;
            }

            if (!DsDev.GetDevicesOfCat(FilterCategory.VideoInputDevice, out capDevices))
            {
                MessageBox.Show(this, "No video capture devices found!", "DirectShow.NET", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                this.Close(); return;
            }

            DsDevice dev = null;
            if (capDevices.Count == 1)
                dev = capDevices[0] as DsDevice;
           
                else
		{
			DeviceSelector selector = new DeviceSelector( capDevices );
			selector.ShowDialog( this );
			dev = selector.SelectedDevice;
		}
            

            if (dev == null)
            {
                this.Close(); return;
            }

            if (!StartupVideo(dev.Mon))
                this.Close();
        }
        /// <summary> capture event, triggered by buffer callback. </summary>
        void OnCaptureDone()
        {
            Trace.WriteLine("!!DLG: OnCaptureDone");
            try
            {
                bt_capture.Enabled = true;
              //  toolBarBtnGrab.Enabled = true;
                int hr;
                if (sampGrabber == null)
                    return;
                hr = sampGrabber.SetCallback(null, 0);

                int w = videoInfoHeader.BmiHeader.Width;
                int h = videoInfoHeader.BmiHeader.Height;
        
                if (((w & 0x03) != 0) || (w < 32) || (w > 4096) || (h < 32) || (h > 4096))
                    return;
                int stride = w * 3;

                GCHandle handle = GCHandle.Alloc(savedArray, GCHandleType.Pinned);
                int scan0 = (int)handle.AddrOfPinnedObject();
                scan0 += (h - 1) * stride;
                Bitmap b = new Bitmap(w, h, -stride, PixelFormat.Format24bppRgb, (IntPtr)scan0);
                handle.Free();
                savedArray = null;
                Image old = pictureBox.Image;
                b.Save("temp.bmp");
                if (check == true)
                {
                    pictureBox.Image = b;
                    
                }
                else
                {
                    pictureBox1.Image = b;
                 //   b.Save("temp.bmp");
                }
                result = b;
                if (old != null)
                    old.Dispose();
                bt_capture.Enabled = true;
            }
            catch (Exception ee)
            {
                MessageBox.Show(this, "Could not grab picture\r\n" + ee.Message, "DirectShow.NET", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }


        /// <summary> start all the interfaces, graphs and preview window. </summary>
        bool StartupVideo(UCOMIMoniker mon)
        {
            int hr;
            try
            {
                if (!CreateCaptureDevice(mon))
                    return false;

                if (!GetInterfaces())
                    return false;

                if (!SetupGraph())
                    return false;

                if (!SetupVideoWindow())
                    return false;

#if DEBUG
                DsROT.AddGraphToRot(graphBuilder, out rotCookie);		// graphBuilder capGraph
#endif

                hr = mediaCtrl.Run();
                if (hr < 0)
                    Marshal.ThrowExceptionForHR(hr);

             //   bool hasTuner = DsUtils.ShowTunerPinDialog(capGraph, capFilter, this.Handle);
             //   toolBarBtnTune.Enabled = hasTuner;

                return true;
            }
            catch (Exception ee)
            {
                MessageBox.Show(this, "Could not start video stream\r\n" + ee.Message, "DirectShow.NET", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return false;
            }
        }

        /// <summary> make the video preview window to show in videoPanel. </summary>
        bool SetupVideoWindow()
        {
            int hr;
            try
            {
                // Set the video window to be a child of the main window
                hr = videoWin.put_Owner(videoPanel.Handle);
                if (hr < 0)
                    Marshal.ThrowExceptionForHR(hr);

                // Set video window style
                hr = videoWin.put_WindowStyle(WS_CHILD | WS_CLIPCHILDREN);
                if (hr < 0)
                    Marshal.ThrowExceptionForHR(hr);

                // Use helper function to position video window in client rect of owner window
                ResizeVideoWindow();

                // Make the video window visible, now that it is properly positioned
                hr = videoWin.put_Visible(DsHlp.OATRUE);
                if (hr < 0)
                    Marshal.ThrowExceptionForHR(hr);

                hr = mediaEvt.SetNotifyWindow(this.Handle, WM_GRAPHNOTIFY, IntPtr.Zero);
                if (hr < 0)
                    Marshal.ThrowExceptionForHR(hr);
                return true;
            }
            catch (Exception ee)
            {
                MessageBox.Show(this, "Could not setup video window\r\n" + ee.Message, "DirectShow.NET", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return false;
            }
        }


        /// <summary> build the capture graph for grabber. </summary>
        bool SetupGraph()
        {
         
               int hr;
		try {
			hr = capGraph.SetFiltergraph( graphBuilder );
			if( hr < 0 )
				Marshal.ThrowExceptionForHR( hr );

			hr = graphBuilder.AddFilter( capFilter, "Ds.NET Video Capture Device" );
			if( hr < 0 )
				Marshal.ThrowExceptionForHR( hr );

			DsUtils.ShowCapPinDialog( capGraph, capFilter, this.Handle );

			AMMediaType media = new AMMediaType();
            media.majorType = MediaType.Video;
            media.subType = MediaSubType.RGB24;
          
			media.formatType = FormatType.VideoInfo;		// ???
			hr = sampGrabber.SetMediaType( media );
			if( hr < 0 )
				Marshal.ThrowExceptionForHR( hr );

			hr = graphBuilder.AddFilter( baseGrabFlt, "Ds.NET Grabber" );
			if( hr < 0 )
				Marshal.ThrowExceptionForHR( hr );

			Guid cat = PinCategory.Preview;
            Guid med = MediaType.Video;
			hr = capGraph.RenderStream( ref cat, ref med, capFilter, null, null ); // baseGrabFlt 
			if( hr < 0 )
				Marshal.ThrowExceptionForHR( hr );

			cat = PinCategory.Capture;
			med = MediaType.Video;
           
			hr = capGraph.RenderStream( ref cat, ref med, capFilter, null, baseGrabFlt ); // baseGrabFlt 
			if( hr < 0 )
				Marshal.ThrowExceptionForHR( hr );

			media = new AMMediaType();
            media.majorType = MediaType.Video;
            media.subType = MediaSubType.RGB24;
            media.formatType = FormatType.VideoInfo;
			hr = sampGrabber.GetConnectedMediaType( media );
			if( hr < 0 )
				Marshal.ThrowExceptionForHR( hr );
			if( (media.formatType != FormatType.VideoInfo) || (media.formatPtr == IntPtr.Zero) )
				throw new NotSupportedException( "Unknown Grabber Media Format" );

			videoInfoHeader = (VideoInfoHeader) Marshal.PtrToStructure( media.formatPtr, typeof(VideoInfoHeader) );
			Marshal.FreeCoTaskMem( media.formatPtr ); media.formatPtr = IntPtr.Zero;

			hr = sampGrabber.SetBufferSamples( false );
			if( hr == 0 )
				hr = sampGrabber.SetOneShot( false );
			if( hr == 0 )
				hr = sampGrabber.SetCallback( null, 0 );
			if( hr < 0 )
				Marshal.ThrowExceptionForHR( hr );

			return true;
            }
            catch (Exception ee)
            {
                MessageBox.Show(this, "Could not setup graph\r\n" + ee.Message, "DirectShow.NET", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return false;
            }
        }


        /// <summary> create the used COM components and get the interfaces. </summary>
        bool GetInterfaces()
        {
            Type comType = null;
            object comObj = null;
            try
            {
                comType = Type.GetTypeFromCLSID(Clsid.FilterGraph);
                if (comType == null)
                    throw new NotImplementedException(@"DirectShow FilterGraph not installed/registered!");
                comObj = Activator.CreateInstance(comType);
                graphBuilder = (IGraphBuilder)comObj; comObj = null;

                Guid clsid = Clsid.CaptureGraphBuilder2;
                Guid riid = typeof(ICaptureGraphBuilder2).GUID;
                comObj = DsBugWO.CreateDsInstance(ref clsid, ref riid);
                capGraph = (ICaptureGraphBuilder2)comObj; comObj = null;

                comType = Type.GetTypeFromCLSID(Clsid.SampleGrabber);
                if (comType == null)
                    throw new NotImplementedException(@"DirectShow SampleGrabber not installed/registered!");
                comObj = Activator.CreateInstance(comType);
                sampGrabber = (ISampleGrabber)comObj; comObj = null;

                mediaCtrl = (IMediaControl)graphBuilder;
                videoWin = (IVideoWindow)graphBuilder;
                mediaEvt = (IMediaEventEx)graphBuilder;
                baseGrabFlt = (IBaseFilter)sampGrabber;
                return true;
            }
            catch (Exception ee)
            {
                MessageBox.Show(this, "Could not get interfaces\r\n" + ee.Message, "DirectShow.NET", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return false;
            }
            finally
            {
                if (comObj != null)
                    Marshal.ReleaseComObject(comObj); comObj = null;
            }
        }

        /// <summary> create the user selected capture device. </summary>
        bool CreateCaptureDevice(UCOMIMoniker mon)
        {
            object capObj = null;
            try
            {
                Guid gbf = typeof(IBaseFilter).GUID;
                mon.BindToObject(null, null, ref gbf, out capObj);
                capFilter = (IBaseFilter)capObj; capObj = null;
                return true;
            }
            catch (Exception ee)
            {
                MessageBox.Show(this, "Could not create capture device\r\n" + ee.Message, "DirectShow.NET", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return false;
            }
            finally
            {
                if (capObj != null)
                    Marshal.ReleaseComObject(capObj); capObj = null;
            }

        }



        /// <summary> do cleanup and release DirectShow. </summary>
        void CloseInterfaces()
        {
            int hr;
            try
            {
#if DEBUG
                if (rotCookie != 0)
                    DsROT.RemoveGraphFromRot(ref rotCookie);
#endif

                if (mediaCtrl != null)
                {
                    hr = mediaCtrl.Stop();
                    mediaCtrl = null;
                }

                if (mediaEvt != null)
                {
                    hr = mediaEvt.SetNotifyWindow(IntPtr.Zero, WM_GRAPHNOTIFY, IntPtr.Zero);
                    mediaEvt = null;
                }

                if (videoWin != null)
                {
                    hr = videoWin.put_Visible(DsHlp.OAFALSE);
                    hr = videoWin.put_Owner(IntPtr.Zero);
                    videoWin = null;
                }

                baseGrabFlt = null;
                if (sampGrabber != null)
                    Marshal.ReleaseComObject(sampGrabber); sampGrabber = null;

                if (capGraph != null)
                    Marshal.ReleaseComObject(capGraph); capGraph = null;

                if (graphBuilder != null)
                    Marshal.ReleaseComObject(graphBuilder); graphBuilder = null;

                if (capFilter != null)
                    Marshal.ReleaseComObject(capFilter); capFilter = null;

                if (capDevices != null)
                {
                    foreach (DsDevice d in capDevices)
                        d.Dispose();
                    capDevices = null;
                }
            }
            catch (Exception)
            { }
        }

        /// <summary> resize preview video window to fill client area. </summary>
        void ResizeVideoWindow()
        {
            if (videoWin != null)
            {
                Rectangle rc = videoPanel.ClientRectangle;
                videoWin.SetWindowPosition(0, 0, rc.Right, rc.Bottom);
            }
        }

        /// <summary> override window fn to handle graph events. </summary>
        protected override void WndProc(ref Message m)
        {
            if (m.Msg == WM_GRAPHNOTIFY)
            {
                if (mediaEvt != null)
                    OnGraphNotify();
                return;
            }
            base.WndProc(ref m);
        }

        /// <summary> graph event (WM_GRAPHNOTIFY) handler. </summary>
        void OnGraphNotify()
        {
            DsEvCode code;
            int p1, p2, hr = 0;
            do
            {
                hr = mediaEvt.GetEvent(out code, out p1, out p2, 0);
                if (hr < 0)
                    break;
                hr = mediaEvt.FreeEventParams(code, p1, p2);
            }
            while (hr == 0);
        }

        /// <summary> sample callback, NOT USED. </summary>
        int ISampleGrabberCB.SampleCB(double SampleTime, IMediaSample pSample)
        {
            Trace.WriteLine("!!CB: ISampleGrabberCB.SampleCB");
            return 0;
        }

        /// <summary> buffer callback, COULD BE FROM FOREIGN THREAD. </summary>
        int ISampleGrabberCB.BufferCB(double SampleTime, IntPtr pBuffer, int BufferLen)
        {
            if (captured || (savedArray == null))
            {
                Trace.WriteLine("!!CB: ISampleGrabberCB.BufferCB");
                return 0;
            }

            captured = true;
            bufferedSize = BufferLen;
            Trace.WriteLine("!!CB: ISampleGrabberCB.BufferCB  !GRAB! size = " + BufferLen.ToString());
            if ((pBuffer != IntPtr.Zero) && (BufferLen > 1000) && (BufferLen <= savedArray.Length))
                Marshal.Copy(pBuffer, savedArray, 0, BufferLen);
            else
                Trace.WriteLine("    !!!GRAB! failed ");
            this.BeginInvoke(new CaptureDone(this.OnCaptureDone));
            return 0;
        }

        #endregion 
        capture com = new capture();
        public Homogenous H1, H2, H3;
        public bool m1, m2, m3;
        public double e1, e2, e3;
        private Matrix Tooltip = new Matrix(1, 4);
        private Matrix Pos = new Matrix(1, 4);
        private void readmarker()
        {
            Tooltip[0, 0] = -18.99;
            Tooltip[0, 1] = 0.89;
            Tooltip[0, 2] = -159.03;
            Tooltip[0, 3] = 1;
          
                try
                {

                    com.readdata();
                    H1 = com.HomoM1;
                    H2 = com.HomoM2;
                    H3 = com.HomoM3;
                    m1 = com.Marker1;
                    m2 = com.Marker2;
                    m3 = com.Marker3;
                    e1 = com.er1;
                    e2 = com.er2;
                    e3 = com.er3;
                    Pos = H3 * Tooltip;
                }
                catch
                {
                    MessageBox.Show("cannot read");
                }
                //   textBox1.Text = H1[3, 0].ToString();
            
        }
        private void bt_capture_Click(object sender, EventArgs e)
        {
           
            int hr;
            if (sampGrabber == null)
                return;
            Trace.WriteLine("!!BTN: toolBarBtnGrab");

            if (savedArray == null)
            {
                int size = videoInfoHeader.BmiHeader.ImageSize;
                if ((size < 1000) || (size > 16000000))
                    return;
                savedArray = new byte[size + 64000];
            }

            //toolBarBtnSave.Enabled = false;
            Image old = pictureBox.Image;
          //  pictureBox.Image = null;
            if (old != null)
                old.Dispose();

            bt_capture.Enabled = false;
            captured = false;
            hr = sampGrabber.SetCallback(this, 1);
            if (check == true)
            {
                bt_calculate.Visible = true;
                lb_step2.Visible = true;
                lb_step3.Visible = true;

            }
            else
            {
                lb_step5.Visible = true;
                lb_step6.Visible = true;
                bt_cal2.Visible = true;
            }
        }

        private void bt_calculate_Click(object sender, EventArgs e)
        {

            Bitmap resultim=calimage.ImageProcess(result);
            pictureBox.Image = resultim;
            readmarker();
         //   if (check == true)
         //   {
                readmarker();
                Hc1 = H1;
                P1 = calimage.Cx1;
                P2 = calimage.Cy1;
                lb_x2.Text = calimage.Cx2.ToString("0.000");
                lb_y2.Text = calimage.Cy2.ToString("0.000");
                lb_ratio2.Text = calimage.Ratio2.ToString("0.00");
                lb_angle2.Text = calimage.Angle2.ToString("0.00");
                lb_x1.Text = calimage.Cx1.ToString("0.000");
                lb_y2.Text = calimage.Cy1.ToString("0.000");
                lb_ratio1.Text = calimage.Ratio1.ToString("0.00");
                lb_angle1.Text = calimage.Angle1.ToString("0.00");
                lb_predict1.Text = calimage.Result1.ToString("0.000");
                lb_predict2.Text = calimage.Result2.ToString("0.000");
                bt_calculate.Text = "calculate 2 shot";
                bt_calculate.Visible = false;
                lb_step1.Visible = false;
                lb_step2.Visible = false;
                lb_step3.Visible = false;
                lb_step4.Visible = true;
                check = false;
               // lb_step5.Visible = true;
           // bt_cal2.Visible = true;
             //   check = false;
        //    }
        //    if (check == false)
           // {
                //Vector ResultP = new Vector();
                //readmarker();
                //Hc2 = H1;
                //P3 = calimage.Cy1;
                //ResultP = calimage.calpostion(H1, H2, P1, P2, P3);
                //lb_x2.Text = calimage.Cx2.ToString("0.000");
                //lb_y2.Text = calimage.Cy2.ToString("0.000");
                //lb_ratio2.Text = calimage.Ratio2.ToString("0.00");
                //lb_angle2.Text = calimage.Angle2.ToString("0.00");
                //lb_x1.Text = calimage.Cx1.ToString("0.000");
                //lb_y2.Text = calimage.Cy1.ToString("0.000");
                //lb_ratio1.Text = calimage.Ratio1.ToString("0.00");
                //lb_angle1.Text = calimage.Angle1.ToString("0.00");
                //lb_predict1.Text = calimage.Result1.ToString("0.000");
                //lb_predict2.Text = calimage.Result2.ToString("0.000");
                //lb_X.Text = ResultP.x.ToString("0.000");
                //lb_Y.Text = ResultP.y.ToString("0.000");
                //lb_Z.Text = ResultP.z.ToString("0.000");
                //bt_calculate.Text = "calculate 1 shot";
                //moveRobot(ResultP);
                //check = true;
            //}
        }
        public void moveRobot(Vector goal)
            {
            Homogenous Hrobot;
               Matrix Robotgoal = new Matrix(1, 4);
         Matrix RobotMove = new Matrix(1, 4);
         Robotgoal[0, 0] = goal.x;
         Robotgoal[0, 1] = goal.y;
         Robotgoal[0, 2] = goal.z;
         Robotgoal[0, 3] = 1;
                
                readmarker();
                Hrobot = H2.Inverse();
                RobotMove = Hrobot * Robotgoal;
                lb_robotx.Text = ((-RobotMove[0, 1]) - 31.822).ToString();
                lb_roboty.Text = (RobotMove[0, 2] - 29.942).ToString();
                
            controlRobot.Robot_Translate((int)double.Parse(lb_robotx.Text), (int)double.Parse(lb_roboty.Text));

            }
        Vector ResultP = new Vector();
        private void bt_cal2_Click(object sender, EventArgs e)
        {
            Bitmap resultim = calimage.ImageProcess(result);
            pictureBox1.Image = resultim;
            readmarker();

       
            readmarker();
            Hc2 = H1;
            P3 = calimage.Cy1;
            ResultP = calimage.calpostion(H1, H2, P1, P2, P3);
            lb_x2.Text = calimage.Cx2.ToString("0.000");
            lb_y2.Text = calimage.Cy2.ToString("0.000");
            lb_ratio2.Text = calimage.Ratio2.ToString("0.00");
            lb_angle2.Text = calimage.Angle2.ToString("0.00");
            lb_x1.Text = calimage.Cx1.ToString("0.000");
            lb_y2.Text = calimage.Cy1.ToString("0.000");
            lb_ratio1.Text = calimage.Ratio1.ToString("0.00");
            lb_angle1.Text = calimage.Angle1.ToString("0.00");
            lb_predict1.Text = calimage.Result1.ToString("0.000");
            lb_predict2.Text = calimage.Result2.ToString("0.000");
            lb_X.Text = ResultP.x.ToString("0.000");
            lb_Y.Text = ResultP.y.ToString("0.000");
            lb_Z.Text = ResultP.z.ToString("0.000");
           // bt_calculate.Text = "calculate 1 shot";
            lb_step1.Visible = false;
            lb_step2.Visible = false;
            lb_step3.Visible = false;
            lb_step4.Visible = false;
            lb_step5.Visible = false;
            lb_step6.Visible = false;
            bt_capture.Visible = false;
            bt_cal2.Visible = false;
           
            bt_robotmove.Visible = true;
            lb_step7.Visible = true;
            check = true;
        }

        private void bt_robotmove_Click(object sender, EventArgs e)
        {
             moveRobot(ResultP);
             lb_step1.Visible = true;
             lb_step7.Visible = false;
             bt_capture.Visible = true;
        }
    }
}
