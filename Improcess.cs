using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Emgu.CV;
using Emgu.Util;
using Emgu.CV.Structure;
using Emgu.CV.CvEnum;
using MathMatrix;
using System.Threading;
using System.IO;
namespace capTv
{
    class Improcess
    {
        public struct circledata
        {
            public double width;
            public double hight;
            public double angle;
            public double cx;
            public double cy;
            public double ratio;
         //   public int id;
            public double size;
        };
       // private bool check = true;
        private double a1 = 50;
        private double a2 = -238;
        private double a3 = 392;
        private double a4 = -204;
      //  private double predictdegree = 0;
        private double ratio, x, y, X, Y, Z;
      //  Thread read;
       // public Homogenous H1, H2, H3;
     //   public Homogenous Hc1, Hc2;
        public int count = 0;
       // private Matrix Tooltip = new Matrix(1, 4);
       // private Matrix Pos = new Matrix(1, 4);
       // public bool m1 = false, m2 = false, m3 = false;
     //   public double e1 = 0, e2 = 0, e3 = 0;
      //  capture com = new capture();

        public double Cx1 = 0, Cy1 = 0, Result1 = 0, Cx2 = 0, Cy2 = 0, Result2 = 0, Ratio1 = 0, Ratio2 = 0, Angle1 = 0, Angle2 = 0;

       
        public Vector calpostion(Homogenous Hm1, Homogenous Hm2, double pt1, double pt2, double pt3)
        {
            Homogenous Hr1, Hr2;
            Homogenous Hmr1, Hmr2;
            double a, b, c, d, e, f, g, h, i, j, k, l;
            Hmr1 = Hm1;
            Hmr2 = Hm2;

            Matrix Mmc = new Matrix(4);
            Mmc[0, 0] = 0.3413;
            Mmc[1, 0] = 0.8480;
            Mmc[2, 0] = -0.4061;
            Mmc[3, 0] = 165.3122;
            Mmc[0, 1] = -0.1747;
            Mmc[1, 1] = 0.4817;
            Mmc[2, 1] = 0.8590;
            Mmc[3, 1] = 160.5789;
            Mmc[0, 2] = 0.9238;
            Mmc[1, 2] = -0.2222;
            Mmc[2, 2] = 0.3125;
            Mmc[3, 2] = -37.8094;
            Mmc[0, 3] = 0;
            Mmc[1, 3] = 0;
            Mmc[2, 3] = 0;
            Mmc[3, 3] = 1;
            Homogenous Hmc = new Homogenous(Mmc);
            double fx = 1134.5536;
            double fy = 1151.8598;
            double cx = -21.4693;
            double cy = 678.3443;
            Vector R = new Vector();
            try
            {

                Hr1 = Hmr1 * Hmc;
                Hr1 = Hr1.Inverse();
                Hr2 = Hmr2 * Hmc;
                Hr2 = Hr2.Inverse();
             //   addtext(fh, Hr1[0, 0].ToString() + "\t" + Hr1[1, 0].ToString() + "\t" + Hr1[2, 0].ToString() + "\t" + Hr1[3, 0].ToString() + "\t\r\n" + Hr1[0, 1].ToString() + "\t" + Hr1[1, 1].ToString() + "\t" + Hr1[2, 1].ToString() + "\t" + Hr1[3, 1].ToString() + "\t\r\n" + Hr1[0, 2].ToString() + "\t" + Hr1[1, 2].ToString() + "\t" + Hr1[2, 2].ToString() + "\t" + Hr1[3, 2].ToString() + "\t\r\n" + Hr1[0, 3].ToString() + "\t" + Hr1[1, 3].ToString() + "\t" + Hr1[2, 3].ToString() + "\t" + Hr1[3, 3].ToString() + "\t\r\n" + Hr2[0, 0].ToString() + "\t" + Hr2[1, 0].ToString() + "\t" + Hr2[2, 0].ToString() + "\t" + Hr2[3, 0].ToString() + "\t\r\n" + Hr2[0, 1].ToString() + "\t" + Hr2[1, 1].ToString() + "\t" + Hr2[2, 1].ToString() + "\t" + Hr2[3, 1].ToString() + "\t\r\n" + Hr2[0, 2].ToString() + "\t" + Hr2[1, 2].ToString() + "\t" + Hr2[2, 2].ToString() + "\t" + Hr2[3, 2].ToString() + "\t\r\n" + Hr2[0, 3].ToString() + "\t" + Hr2[1, 3].ToString() + "\t" + Hr2[2, 3].ToString() + "\t" + Hr2[3, 3].ToString() + "\t\r\n");
               // fh.Close();




                a = (fx * Hr1[1, 0]) - (pt1 - cx) * Hr1[1, 2];
                b = (fx * Hr1[2, 0]) - (pt1 - cx) * Hr1[2, 2];
                c = (fx * (Hr1[3, 0])) - ((pt1 - cx) * (Hr1[3, 2]));
                d = (pt1 - cx) * Hr1[0, 2] - (fx * Hr1[0, 0]);



                e = (fy * Hr1[0, 1]) - (pt2 - cy) * Hr1[0, 2];
                f = (fy * Hr1[2, 1]) - (pt2 - cy) * Hr1[2, 2];
                g = (fy * (Hr1[3, 1])) - ((pt2 - cy) * (Hr1[3, 2]));
                h = (pt2 - cy) * Hr1[1, 2] - (fy * Hr1[1, 1]);


                i = (fy * Hr2[0, 1]) - (pt3 - cy) * Hr2[0, 2];
                j = (fy * Hr2[1, 1]) - (pt3 - cy) * Hr2[1, 2];
                k = (fy * (Hr2[3, 1])) - ((pt3 - cy) * (Hr2[3, 2]));
                l = (pt3 - cy) * Hr2[2, 2] - (fy * Hr2[2, 1]);

                X = ((c * (h * l - f * j)) + (a * (g * l + f * k)) + (b * (h * k + g * j))) / ((d * (h * l - f * j)) + (a * (-e * l - i * f)) + (b * (-e * j - i * h)));
                Y = ((d * (g * l + f * k)) + (c * (e * l + i * f)) + (b * (e * k - i * g))) / ((d * (h * l - f * j)) + (a * (-e * l - i * f)) + (b * (-e * j - i * h)));
                Z = ((d * (h * k + g * j)) + a * (i * g - e * k) + (c * (e * j + i * h))) / ((d * (h * l - f * j)) + (a * (-e * l - i * f)) + (b * (-e * j - i * h)));
                R.x = X;
                R.y = Y;
                R.z = Z;
                return R;
            }
            catch
            {
                return R;
            }


        }

        public Bitmap ImageProcess(Bitmap image)
        {

            Image<Bgr, Byte> img = new Image<Bgr, byte>("temp.bmp");
           
            System.Diagnostics.Trace.WriteLine("ImageProcess!!");
            //image = new Image<Bgr, byte>(textBox1.Text);
            Matrix<double> intrinsicPara = new Matrix<double>(3, 3);
            Matrix<double> Distor = new Matrix<double>(4, 1);
            intrinsicPara[0, 0] = 887.55212402;
            intrinsicPara[0, 1] = 0;
            intrinsicPara[0, 2] = 303.03750610;
            intrinsicPara[1, 0] = 0;
            intrinsicPara[1, 1] = 882.22644043;
            intrinsicPara[1, 2] = 217.25865173;
            intrinsicPara[2, 0] = 0;
            intrinsicPara[2, 1] = 0;
            intrinsicPara[2, 2] = 1;
            Distor[0, 0] = 0.49758449;
            Distor[1, 0] = 7.48868513;
            Distor[2, 0] = 0.05677241;
            Distor[3, 0] = -0.02885232;
            IntrinsicCameraParameters camP = new IntrinsicCameraParameters();
            camP.IntrinsicMatrix = intrinsicPara;
            camP.DistortionCoeffs = Distor;
            img = camP.Undistort(img);
            //Vector Ro;
            // double rx, ry, rz;
            circledata[] circledatas = new circledata[100];
           // circledata temp = new circledata();
            //LinkedList<circledata> cdata = new LinkedList<circledata>();
            Image<Gray, Byte> gray = img.Convert<Gray, Byte>();
            //Image<Gray, Byte> bw = gray.ThresholdBinary(new Gray(240), new Gray(255));
            Image<Gray, Byte> cannyEdges = gray.Canny(new Gray(100), new Gray(150));
            // imageBox3.Image = gray;
            int j = 0;
            double size = 0;
       //     double predict2 = 0;
            // double premarkerx = 0;
            Point[] po;

            cannyEdges = cannyEdges.Dilate(1);
            cannyEdges = cannyEdges.Erode(1);

            //imageBox1.Image = cannyEdges;
            Image<Bgr, byte> im;
            im = img;


            using (MemStorage storage = new MemStorage()) //allocate storage for contour approximation
                for (Contour<Point> contours = cannyEdges.FindContours(); contours != null; contours = contours.HNext)
                {

                    if (contours.Area > 50)
                    {
                        po = contours.ToArray();
                        if (po.Length >= 6)
                        {
                            PointF[] p = new PointF[po.Length];
                            for (int i = 0; i < po.Length; i++)
                            {
                                p[i] = po[i];
                            }
                            Ellipse es = EllipseLeastSquareFitting(p);
                            double distance = 0;

                            distance = ((es.MCvBox2D.center.X - 320) * (es.MCvBox2D.center.X - 320)) + ((es.MCvBox2D.center.Y - 240) * (es.MCvBox2D.center.Y - 240));
                            if (es.MCvBox2D.size.Width <= 100 && es.MCvBox2D.size.Height <= 100 && distance < 224 * 224)
                            {

                                size = 3.14 * es.MCvBox2D.size.Height * es.MCvBox2D.size.Width;

                                
                                im.Draw(es, new Bgr(Color.Red), 1);


                                if (es.MCvBox2D.size.Height > es.MCvBox2D.size.Width)
                                {
                                    ratio = es.MCvBox2D.size.Height / es.MCvBox2D.size.Width;
                                    // rotateX = true;
                                    // rotateY = false;
                                }
                                else
                                {
                                    ratio = es.MCvBox2D.size.Width / es.MCvBox2D.size.Height;
                                    // rotateY = true;
                                    // rotateX = false;

                                }


                             //   imageBox2.Image = im;

                                //  predictdegree = c1*(size*size*size*size)+c2*(size*size*size)+c3*(size*size)+c4*size+c5;


                                // tw.WriteLine( + es.MCvBox2D.size.Height  + "\t" + es.MCvBox2D.size.Width + "\t" + size );
                            }
                            if (Math.Abs(x - es.MCvBox2D.center.X) < 20 && Math.Abs(y - es.MCvBox2D.center.Y) < 20 && size < 8000)
                            {

                                circledatas[j].angle = es.MCvBox2D.angle;
                                circledatas[j].cx = es.MCvBox2D.center.X;
                                circledatas[j].cy = es.MCvBox2D.center.Y;
                                circledatas[j].hight = es.MCvBox2D.size.Height;
                                circledatas[j].width = es.MCvBox2D.size.Width;
                                circledatas[j].ratio = ratio;
                                circledatas[j].size = size;



                                // System.Diagnostics.Debug.WriteLine(circledatas[j].size);
                                j++;
                            }
                            x = es.MCvBox2D.center.X;
                            y = es.MCvBox2D.center.Y;
                     
                        }

                    }


                }
            /*   for (int k = 0; k < circledatas.Length; k++)
                  {
                      for (int f = 0; f < circledatas.Length; f++)
                      {
                          if (circledatas[k].size > circledatas[f].size)
                          {
                              temp = circledatas[k];
                              circledatas[k] = circledatas[f];
                              circledatas[f] = temp;
                          }
                      }

                  }*/


            Cx2 = circledatas[1].cx;
            Cy2 = circledatas[1].cy;
            Ratio2 = circledatas[1].ratio;
            Angle2 = circledatas[1].angle;
            Cx1= circledatas[0].cx;
            Cy1 = circledatas[0].cy;
            Ratio1= circledatas[0].ratio;
            Angle1 = circledatas[0].angle;
           
            Result1 = a1 * (circledatas[0].ratio * circledatas[0].ratio * circledatas[0].ratio) + a2 * (circledatas[0].ratio * circledatas[0].ratio) + a3 * circledatas[0].ratio + a4;
            Result2 = a1 * (circledatas[1].ratio * circledatas[1].ratio * circledatas[1].ratio) + a2 * (circledatas[1].ratio * circledatas[1].ratio) + a3 * circledatas[1].ratio + a4;
            // if (circledatas[0].angle < 100)
            //  {
            //      premarker = mx1 * (circledatas[0].ratio * circledatas[0].ratio * circledatas[0].ratio) + mx2 * (circledatas[0].ratio * circledatas[0].ratio) + mx3 * circledatas[0].ratio + mx4;
            //  }

            // lb_cangle.Text = circledatas[1].angle.ToString("0.000");
            //  lb_cratio.Text = circledatas[1].ratio.ToString("0.000");
            return im.Bitmap;
        }
      
        
        public static Ellipse EllipseLeastSquareFitting(PointF[] points)
        {
            IntPtr seq = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(MCvSeq)));
            IntPtr block = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(MCvSeqBlock)));
            GCHandle handle = GCHandle.Alloc(points, GCHandleType.Pinned);
            CvInvoke.cvMakeSeqHeaderForArray(
               CvInvoke.CV_MAKETYPE((int)MAT_DEPTH.CV_32F, 2),
               Marshal.SizeOf(typeof(MCvSeq)),
               Marshal.SizeOf(typeof(PointF)),
               handle.AddrOfPinnedObject(),
               points.Length,
               seq,
               block);

            Ellipse e = new Ellipse(CvInvoke.cvFitEllipse2(seq));
            handle.Free();
            Marshal.FreeHGlobal(seq);
            Marshal.FreeHGlobal(block);

            return e;
        }

    }
}
