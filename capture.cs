using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO.Ports;
using System.Threading;
using System.IO;
using System.Collections;
using MathMatrix;

namespace capTv
{
    class capture
    {
        string filePath = @"d:\\project\\Tool Definition Files\\8700339.rom";
        string filePath2 = @"d:\\project\\Tool Definition Files\\8700338.rom";
        string filePath3 = @"d:\\project\\Tool Definition Files\\8700340.rom";
        SerialPort mySerialPort;
        public Homogenous HomoM1;
        public Homogenous HomoM2;
        public Homogenous HomoM3;
        public double er1 = 0, er2 = 0, er3 = 0;
        public bool Marker1 = false, Marker2 = false, Marker3 = false;

        public void connect()
        {
            mySerialPort = new SerialPort("COM3");

            mySerialPort.BaudRate = 9600;
            mySerialPort.Parity = Parity.None;
            mySerialPort.StopBits = StopBits.One;
            mySerialPort.DataBits = 8;
            mySerialPort.Handshake = Handshake.None;
            mySerialPort.ReadBufferSize = 4094;
            //  mySerialPort.DataReceived += new SerialDataReceivedEventHandler(DataReceivedHandler);
            mySerialPort.ReadTimeout = 5000;
            mySerialPort.Open();

            System.Diagnostics.Debug.WriteLine("connected");


        }
        public void disconnect()
        {
            mySerialPort.Close();
            mySerialPort.Dispose();

        }


        public static byte[] ReadFile(string filePath)
        {
            byte[] buffer;
            FileStream fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
            try
            {
                int length = (int)fileStream.Length;  // get file length
                buffer = new byte[length];            // create buffer
                int count;                            // actual number of bytes read
                int sum = 0;                          // total number of bytes read

                // read until Read method returns 0 (end of the stream has been reached)
                while ((count = fileStream.Read(buffer, sum, length - sum)) > 0)
                    sum += count;  // sum is a buffer offset for next reading
            }
            finally
            {
                fileStream.Close();
            }
            return buffer;
        }
        public void readcom()
        {
            int readcount;
            string indata = "";
            char[] buf = new char[1000];
            readcount = mySerialPort.BytesToRead;
            indata = mySerialPort.ReadExisting();
            //mySerialPort.Read(buf, 0, readcount);
            System.Diagnostics.Debug.WriteLine(indata);
        }


        public void initpolaris()
        {


            System.Diagnostics.Debug.WriteLine("sending");


            mySerialPort.Write("RESET 0\r");
            Thread.Sleep(1000);
            readcom();

            mySerialPort.Write("COMM 50000\r");
            mySerialPort.Close();
            mySerialPort = new SerialPort("COM3");
            mySerialPort.BaudRate = 115200;
            mySerialPort.Open();
            Thread.Sleep(1000);
            readcom();

            mySerialPort.Write("INIT \r");
            Thread.Sleep(1000);
            readcom();

            sendconfig(filePath, 1);

            sendconfig(filePath2, 2);
            sendconfig(filePath3, 3);
            mySerialPort.Write("PHSR 02\r");
            Thread.Sleep(200);
            readcom();
            mySerialPort.Write("PINIT 01\r");
            Thread.Sleep(200);
            readcom();
            mySerialPort.Write("PINIT 02\r");
            Thread.Sleep(200);
            readcom();
            mySerialPort.Write("PINIT 03\r");
            Thread.Sleep(200);
            readcom();
            mySerialPort.Write("PHSR 03\r");
            Thread.Sleep(200);
            readcom();
            mySerialPort.Write("PENA 01D\r");
            Thread.Sleep(200);
            readcom();
            mySerialPort.Write("PENA 02D\r");
            Thread.Sleep(200);
            readcom();
            mySerialPort.Write("PENA 03D\r");
            Thread.Sleep(200);
            readcom();
            mySerialPort.Write("TSTART \r");
            Thread.Sleep(200);
            readcom();
        }
        public void readdata()
        {
            string data = "";
            char[] M1;
            char[] M2;
            char[] M3;
            double m1r0 = 0, m1rx = 0, m1ry = 0, m1rz = 0, m1x = 0, m1y = 0, m1z = 0, m1e = 0,
                M2r0 = 0, M2rx = 0, M2ry = 0, M2rz = 0, M2x = 0, M2y = 0, M2z = 0, M2e = 0,
                M3r0 = 0, M3rx = 0, M3ry = 0, M3rz = 0, M3x = 0, M3y = 0, M3z = 0, M3e = 0;
            mySerialPort.Write("TX 0001\r");
            Thread.Sleep(100);
            data = mySerialPort.ReadExisting();
            System.Diagnostics.Debug.WriteLine(data);
            try
            {
                string[] dataSplit = data.Split('\n');
                M1 = dataSplit[0].ToCharArray();
                M2 = dataSplit[1].ToCharArray();
                M3 = dataSplit[2].ToCharArray();
                #region decode tool 1

                if (M1[4] == '-' || M1[4] == '+')
                {
                    if (M1[4] == '-')
                    {

                        string s = "";
                        for (int k = 1; k < 6; k++)
                        {
                            s = s + M1[4 + k];


                        }

                        int i = Int32.Parse(s);
                        m1r0 = i * (-0.0001);

                    }
                    else
                    {
                        string s = "";
                        for (int k = 1; k < 6; k++)
                        {
                            s = s + M1[4 + k];


                        }

                        int i = Int32.Parse(s);
                        m1r0 = i * (0.0001);

                        //  Marker1.r0 = atoi(&M1[5]) * (0.0001);
                    }
                    if (M1[10] == '-')
                    {
                        string s = "";
                        for (int k = 1; k < 6; k++)
                        {
                            s = s + M1[10 + k];


                        }

                        int i = Int32.Parse(s);
                        m1rx = i * (-0.0001);

                        // Marker1.rx = atoi(&M1[11]) * (-0.0001);
                    }
                    else
                    {
                        string s = "";
                        for (int k = 1; k < 6; k++)
                        {
                            s = s + M1[10 + k];


                        }

                        int i = Int32.Parse(s);
                        m1rx = i * (0.0001);

                        // Marker1.rx = atoi(&M1[11]) * (0.0001);
                    }
                    if (M1[16] == '-')
                    {
                        string s = "";
                        for (int k = 1; k < 6; k++)
                        {
                            s = s + M1[16 + k];


                        }

                        int i = Int32.Parse(s);
                        m1ry = i * (-0.0001);

                        // Marker1.ry = atoi(&M1[17]) * (-0.0001);
                    }
                    else
                    {
                        string s = "";
                        for (int k = 1; k < 6; k++)
                        {
                            s = s + M1[16 + k];


                        }

                        int i = Int32.Parse(s);
                        m1ry = i * (0.0001);

                        // Marker1.ry = atoi(&M1[17]) * (0.0001);
                    }
                    if (M1[22] == '-')
                    {
                        string s = "";
                        for (int k = 1; k < 6; k++)
                        {
                            s = s + M1[22 + k];


                        }

                        int i = Int32.Parse(s);
                        m1rz = i * (-0.0001);

                        // Marker1.rz = atoi(&M1[23]) * (-0.0001);
                    }
                    else
                    {
                        string s = "";
                        for (int k = 1; k < 6; k++)
                        {
                            s = s + M1[22 + k];


                        }

                        int i = Int32.Parse(s);
                        m1rz = i * (0.0001);

                        // Marker1.rz = atoi(&M1[23]) * (0.0001);
                    }


                    if (M1[28] == '-')
                    {
                        string s = "";
                        for (int k = 1; k < 7; k++)
                        {
                            s = s + M1[28 + k];


                        }

                        int i = Int32.Parse(s);
                        m1x = i * (-0.0100);

                        // Marker1.x = atoi(&M1[29]) * (-0.0100);
                    }
                    else
                    {
                        string s = "";
                        for (int k = 1; k < 7; k++)
                        {
                            s = s + M1[28 + k];


                        }

                        int i = Int32.Parse(s);
                        m1x = i * (0.0100);

                        // Marker1.x = atoi(&M1[29]) * (0.0100);
                    }
                    if (M1[35] == '-')
                    {
                        string s = "";
                        for (int k = 1; k < 7; k++)
                        {
                            s = s + M1[35 + k];


                        }

                        int i = Int32.Parse(s);
                        m1y = i * (-0.0100);

                        //   Marker1.y = atoi(&M1[35]) * (-0.0100);
                    }
                    else
                    {
                        string s = "";
                        for (int k = 1; k < 7; k++)
                        {
                            s = s + M1[35 + k];


                        }

                        int i = Int32.Parse(s);
                        m1y = i * (0.0100);

                        //  Marker1.y = atoi(&M1[35]) * (0.0100);
                    }
                    if (M1[42] == '-')
                    {
                        string s = "";
                        for (int k = 1; k < 7; k++)
                        {
                            s = s + M1[42 + k];


                        }

                        int i = Int32.Parse(s);
                        m1z = i * (-0.0100);

                        // Marker1.z = atoi(&M1[42]) * (-0.01000);
                    }
                    else
                    {
                        string s = "";
                        for (int k = 1; k < 7; k++)
                        {
                            s = s + M1[42 + k];


                        }

                        int i = Int32.Parse(s);
                        m1z = i * (0.0100);

                        // Marker1.z = atoi(&M1[42]) * (0.01000);
                    }
                    if (M1[49] == '-')
                    {
                        string s = "";
                        for (int k = 1; k < 6; k++)
                        {
                            s = s + M1[49 + k];


                        }

                        int i = Int32.Parse(s);
                        m1e = i * (-0.0001);
                        er1 = m1e;
                        //  Marker1.error = atoi(&M1[49]) * (-0.0001);
                    }
                    else
                    {
                        string s = "";
                        for (int k = 1; k < 6; k++)
                        {
                            s = s + M1[49 + k];


                        }

                        int i = Int32.Parse(s);
                        m1e = i * (0.0001);
                        er1 = m1e;
                        //  Marker1.error = atoi(&M1[49]) * (0.0001);
                    }
                    // strcpy(Marker1.status, "- OK\0");

                    Marker1 = true;

                }

                else if (M1[4] == 'M')
                {
                    Marker1 = false;
                    //  strcpy(Marker1.status, "MISSING\0");
                }
                else
                {
                    Marker1 = false;
                    //   strcpy(Marker1.status, "DISABLE\0");
                }
                #endregion
                #region decode tool 2
                if (M2[2] == '-' || M2[2] == '+')
                {
                    if (M2[2] == '-')
                    {

                        string s = "";
                        for (int k = 1; k < 6; k++)
                        {
                            s = s + M2[2 + k];


                        }

                        int i = Int32.Parse(s);
                        M2r0 = i * (-0.0001);

                    }
                    else
                    {
                        string s = "";
                        for (int k = 1; k < 6; k++)
                        {
                            s = s + M2[2 + k];


                        }

                        int i = Int32.Parse(s);
                        M2r0 = i * (0.0001);

                        //  Marker1.r0 = atoi(&M2[5]) * (0.0001);
                    }
                    if (M2[8] == '-')
                    {
                        string s = "";
                        for (int k = 1; k < 6; k++)
                        {
                            s = s + M2[8 + k];


                        }

                        int i = Int32.Parse(s);
                        M2rx = i * (-0.0001);

                        // Marker1.rx = atoi(&M2[11]) * (-0.0001);
                    }
                    else
                    {
                        string s = "";
                        for (int k = 1; k < 6; k++)
                        {
                            s = s + M2[8 + k];


                        }

                        int i = Int32.Parse(s);
                        M2rx = i * (0.0001);

                        // Marker1.rx = atoi(&M2[11]) * (0.0001);
                    }
                    if (M2[14] == '-')
                    {
                        string s = "";
                        for (int k = 1; k < 6; k++)
                        {
                            s = s + M2[14 + k];


                        }

                        int i = Int32.Parse(s);
                        M2ry = i * (-0.0001);

                        // Marker1.ry = atoi(&M2[17]) * (-0.0001);
                    }
                    else
                    {
                        string s = "";
                        for (int k = 1; k < 6; k++)
                        {
                            s = s + M2[14 + k];


                        }

                        int i = Int32.Parse(s);
                        M2ry = i * (0.0001);

                        // Marker1.ry = atoi(&M2[17]) * (0.0001);
                    }
                    if (M2[20] == '-')
                    {
                        string s = "";
                        for (int k = 1; k < 6; k++)
                        {
                            s = s + M2[20 + k];


                        }

                        int i = Int32.Parse(s);
                        M2rz = i * (-0.0001);

                        // Marker1.rz = atoi(&M2[23]) * (-0.0001);
                    }
                    else
                    {
                        string s = "";
                        for (int k = 1; k < 6; k++)
                        {
                            s = s + M2[20 + k];


                        }

                        int i = Int32.Parse(s);
                        M2rz = i * (0.0001);

                        // Marker1.rz = atoi(&M2[23]) * (0.0001);
                    }


                    if (M2[26] == '-')
                    {
                        string s = "";
                        for (int k = 1; k < 7; k++)
                        {
                            s = s + M2[26 + k];


                        }

                        int i = Int32.Parse(s);
                        M2x = i * (-0.0100);

                        // Marker1.x = atoi(&M2[29]) * (-0.0100);
                    }
                    else
                    {
                        string s = "";
                        for (int k = 1; k < 7; k++)
                        {
                            s = s + M2[26 + k];


                        }

                        int i = Int32.Parse(s);
                        M2x = i * (0.0100);

                        // Marker1.x = atoi(&M2[29]) * (0.0100);
                    }
                    if (M2[33] == '-')
                    {
                        string s = "";
                        for (int k = 1; k < 7; k++)
                        {
                            s = s + M2[33 + k];


                        }

                        int i = Int32.Parse(s);
                        M2y = i * (-0.0100);

                        //   Marker1.y = atoi(&M2[35]) * (-0.0100);
                    }
                    else
                    {
                        string s = "";
                        for (int k = 1; k < 7; k++)
                        {
                            s = s + M2[33 + k];


                        }

                        int i = Int32.Parse(s);
                        M2y = i * (0.0100);

                        //  Marker1.y = atoi(&M2[35]) * (0.0100);
                    }
                    if (M2[40] == '-')
                    {
                        string s = "";
                        for (int k = 1; k < 7; k++)
                        {
                            s = s + M2[40 + k];


                        }

                        int i = Int32.Parse(s);
                        M2z = i * (-0.0100);

                        // Marker1.z = atoi(&M2[42]) * (-0.01000);
                    }
                    else
                    {
                        string s = "";
                        for (int k = 1; k < 7; k++)
                        {
                            s = s + M2[40 + k];


                        }

                        int i = Int32.Parse(s);
                        M2z = i * (0.0100);

                        // Marker1.z = atoi(&M2[42]) * (0.01000);
                    }
                    if (M2[47] == '-')
                    {
                        string s = "";
                        for (int k = 1; k < 6; k++)
                        {
                            s = s + M2[47 + k];


                        }

                        int i = Int32.Parse(s);
                        M2e = i * (-0.0001);
                        er2 = M2e;
                        //  Marker1.error = atoi(&M2[49]) * (-0.0001);
                    }
                    else
                    {
                        string s = "";
                        for (int k = 1; k < 6; k++)
                        {
                            s = s + M2[47 + k];


                        }

                        int i = Int32.Parse(s);
                        M2e = i * (0.0001);
                        er2 = M2e;
                        //  Marker1.error = atoi(&M2[49]) * (0.0001);
                    }
                    Marker2 = true;   // strcpy(Marker1.status, "- OK\0");



                }

                else if (M2[2] == 'M')
                {
                    Marker2 = false; //  strcpy(Marker1.status, "MISSING\0");
                }
                else
                {
                    Marker2 = false; //   strcpy(Marker1.status, "DISABLE\0");
                }
                #endregion
                #region decode tool 3
                if (M3[2] == '-' || M3[2] == '+')
                {
                    if (M3[2] == '-')
                    {

                        string s = "";
                        for (int k = 1; k < 6; k++)
                        {
                            s = s + M3[2 + k];


                        }

                        int i = Int32.Parse(s);
                        M3r0 = i * (-0.0001);
                    }
                    else
                    {
                        string s = "";
                        for (int k = 1; k < 6; k++)
                        {
                            s = s + M3[2 + k];


                        }

                        int i = Int32.Parse(s);
                        M3r0 = i * (0.0001);
                        //  Marker1.r0 = atoi(&M3[5]) * (0.0001);
                    }
                    if (M3[8] == '-')
                    {
                        string s = "";
                        for (int k = 1; k < 6; k++)
                        {
                            s = s + M3[8 + k];


                        }

                        int i = Int32.Parse(s);
                        M3rx = i * (-0.0001);
                        // Marker1.rx = atoi(&M3[11]) * (-0.0001);
                    }
                    else
                    {
                        string s = "";
                        for (int k = 1; k < 6; k++)
                        {
                            s = s + M3[8 + k];


                        }

                        int i = Int32.Parse(s);
                        M3rx = i * (0.0001);
                        // Marker1.rx = atoi(&M3[11]) * (0.0001);
                    }
                    if (M3[14] == '-')
                    {
                        string s = "";
                        for (int k = 1; k < 6; k++)
                        {
                            s = s + M3[14 + k];


                        }

                        int i = Int32.Parse(s);
                        M3ry = i * (-0.0001);
                        // Marker1.ry = atoi(&M3[17]) * (-0.0001);
                    }
                    else
                    {
                        string s = "";
                        for (int k = 1; k < 6; k++)
                        {
                            s = s + M3[14 + k];


                        }

                        int i = Int32.Parse(s);
                        M3ry = i * (0.0001);
                        // Marker1.ry = atoi(&M3[17]) * (0.0001);
                    }
                    if (M3[20] == '-')
                    {
                        string s = "";
                        for (int k = 1; k < 6; k++)
                        {
                            s = s + M3[20 + k];


                        }

                        int i = Int32.Parse(s);
                        M3rz = i * (-0.0001);
                        // Marker1.rz = atoi(&M3[23]) * (-0.0001);
                    }
                    else
                    {
                        string s = "";
                        for (int k = 1; k < 6; k++)
                        {
                            s = s + M3[20 + k];


                        }

                        int i = Int32.Parse(s);
                        M3rz = i * (0.0001);
                        // Marker1.rz = atoi(&M3[23]) * (0.0001);
                    }


                    if (M3[26] == '-')
                    {
                        string s = "";
                        for (int k = 1; k < 7; k++)
                        {
                            s = s + M3[26 + k];


                        }

                        int i = Int32.Parse(s);
                        M3x = i * (-0.0100);
                        // Marker1.x = atoi(&M3[29]) * (-0.0100);
                    }
                    else
                    {
                        string s = "";
                        for (int k = 1; k < 7; k++)
                        {
                            s = s + M3[26 + k];


                        }

                        int i = Int32.Parse(s);
                        M3x = i * (0.0100);
                        // Marker1.x = atoi(&M3[29]) * (0.0100);
                    }
                    if (M3[33] == '-')
                    {
                        string s = "";
                        for (int k = 1; k < 7; k++)
                        {
                            s = s + M3[33 + k];


                        }

                        int i = Int32.Parse(s);
                        M3y = i * (-0.0100);
                        //   Marker1.y = atoi(&M3[35]) * (-0.0100);
                    }
                    else
                    {
                        string s = "";
                        for (int k = 1; k < 7; k++)
                        {
                            s = s + M3[33 + k];


                        }

                        int i = Int32.Parse(s);
                        M3y = i * (0.0100);
                        //  Marker1.y = atoi(&M3[35]) * (0.0100);
                    }
                    if (M3[40] == '-')
                    {
                        string s = "";
                        for (int k = 1; k < 7; k++)
                        {
                            s = s + M3[40 + k];


                        }

                        int i = Int32.Parse(s);
                        M3z = i * (-0.0100);
                        // Marker1.z = atoi(&M3[42]) * (-0.01000);
                    }
                    else
                    {
                        string s = "";
                        for (int k = 1; k < 7; k++)
                        {
                            s = s + M3[40 + k];


                        }

                        int i = Int32.Parse(s);
                        M3z = i * (0.0100);
                        // Marker1.z = atoi(&M3[42]) * (0.01000);
                    }
                    if (M3[47] == '-')
                    {
                        string s = "";
                        for (int k = 1; k < 6; k++)
                        {
                            s = s + M3[47 + k];


                        }

                        int i = Int32.Parse(s);
                        M3e = i * (-0.0001);
                        er3 = M3e;
                        //  Marker1.error = atoi(&M3[49]) * (-0.0001);
                    }
                    else
                    {
                        string s = "";
                        for (int k = 1; k < 6; k++)
                        {
                            s = s + M3[47 + k];


                        }

                        int i = Int32.Parse(s);
                        M3e = i * (0.0001);
                        er3 = M3e;
                        //  Marker1.error = atoi(&M3[49]) * (0.0001);
                    }
                    // strcpy(Marker1.status, "- OK\0");
                    Marker3 = true;


                }

                else if (M3[2] == 'M')
                {

                    Marker3 = false;//  strcpy(Marker1.status, "MISSING\0");
                }
                else
                {
                    Marker3 = false;//   strcpy(Marker1.status, "DISABLE\0");
                }
                #endregion
                Matrix Rotm1 = CvtQuatToRotationMatrix(m1r0, m1rx, m1ry, m1rz, m1x, m1y, m1z);
                Matrix Rotm2 = CvtQuatToRotationMatrix(M2r0, M2rx, M2ry, M2rz, M2x, M2y, M2z);
                Matrix Rotm3 = CvtQuatToRotationMatrix(M3r0, M3rx, M3ry, M3rz, M3x, M3y, M3z);

                //  Vector P1;
                //   Vector P2 = new Vector(M2x,M2y,M2z);
                //  Vector P3 = new Vector(M3x,M3y,M3z);
                //  double[] E1,E2,E3;
                //     E1= DetermineEuler(Rotm1);
                //     E2 = DetermineEuler(Rotm2);
                //     E3 = DetermineEuler(Rotm3);
                HomoM1 = new Homogenous(Rotm1);
                // P1 = HomoM3.CalDegreeRowPitchYaw();

                HomoM2 = new Homogenous(Rotm2);
                HomoM3 = new Homogenous(Rotm3);
            }
            catch
            {
             //   System.Windows.Forms.MessageBox.Show("error");
                //MessageBox.Show("Update Anime");
            }
        }
        public void sendconfig(string filePath, int id)
        {
            int i = 0;
            byte[] buff = new byte[768];
            char[] sent = new char[200];
            string send = "";
            string sending = "";
            buff = ReadFile(filePath);

            string comand = BitConverter.ToString(buff);
            string[] hexValuesSplit = comand.Split('-');
            string hexOutput = "";
            mySerialPort.Write("PHSR 01\r");
            Thread.Sleep(100);
            readcom();
            mySerialPort.Write("PHRQ ********01****\r");
            Thread.Sleep(100);
            readcom();
            while (i < 768)
            {
                send = "";
                if (id == 1)
                {
                    hexOutput = String.Format("PVWR 01{0:X4}", i);
                }
                else if (id == 2)
                {
                    hexOutput = String.Format("PVWR 02{0:X4}", i);
                }
                else if (id == 3)
                {
                    hexOutput = String.Format("PVWR 03{0:X4}", i);

                }

                for (int j = 0; j < 64; j++)
                {
                    if (i <= 751)
                    {
                        send = send + hexValuesSplit[i];
                        i++;
                    }
                    else { send = send + "00"; i++; }
                }
                sending = hexOutput + send;

                System.Diagnostics.Debug.WriteLine(sending);
                // mySerialPort.WriteLine(sending+"\n");
                mySerialPort.Write(sending + "\r");
                Thread.Sleep(100);
                readcom();
            }

        }
        private Matrix CvtQuatToRotationMatrix(double r0, double rx, double ry, double rz, double x, double y, double z)
        {
            double
                fQ0Q0,
                fQxQx,
                fQyQy,
                fQzQz,
                fQ0Qx,
                fQ0Qy,
                fQ0Qz,
                fQxQy,
                fQxQz,
                fQyQz;

            /*
             * Determine some calculations done more than once.
             */
            fQ0Q0 = r0 * r0;// fQ0Q0 = pdtQuatRot->q0 * pdtQuatRot->q0;
            fQxQx = rx * rx;// fQxQx = pdtQuatRot->qx * pdtQuatRot->qx;
            fQyQy = ry * ry; // fQyQy = pdtQuatRot->qy * pdtQuatRot->qy;
            fQzQz = rz * rz; // pdtQuatRot->qz * pdtQuatRot->qz;
            fQ0Qx = r0 * rx; //pdtQuatRot->q0 * pdtQuatRot->qx;
            fQ0Qy = r0 * ry; //pdtQuatRot->q0 * pdtQuatRot->qy;
            fQ0Qz = r0 * rz; //pdtQuatRot->q0 * pdtQuatRot->qz;
            fQxQy = rx * ry; //pdtQuatRot->qx * pdtQuatRot->qy;
            fQxQz = rx * rz; //pdtQuatRot->qx * pdtQuatRot->qz;
            fQyQz = ry * rz; //pdtQuatRot->qy * pdtQuatRot->qz; 

            /*
             * Determine the rotation matrix elements.
             */
            Matrix Rot = new Matrix(4, false);

            Rot[0, 0] = fQ0Q0 + fQxQx - fQyQy - fQzQz;
            Rot[1, 0] = 2.0 * (-fQ0Qz + fQxQy);
            Rot[2, 0] = 2.0 * (fQ0Qy + fQxQz);
            Rot[0, 1] = 2.0 * (fQ0Qz + fQxQy);
            Rot[1, 1] = fQ0Q0 - fQxQx + fQyQy - fQzQz;
            Rot[2, 1] = 2.0 * (-fQ0Qx + fQyQz);
            Rot[0, 2] = 2.0 * (-fQ0Qy + fQxQz);
            Rot[1, 2] = 2.0 * (fQ0Qx + fQyQz);
            Rot[2, 2] = fQ0Q0 - fQxQx - fQyQy + fQzQz;
            /*printf("%f : %f : %f\n%f : %f : %f\n%f : %f : %f\n",Rot.dtRotMatrix[0][0],Rot.dtRotMatrix[0][1],Rot.dtRotMatrix[0][2],
                                        Rot.dtRotMatrix[1][0],Rot.dtRotMatrix[1][1],Rot.dtRotMatrix[1][2],
                                        Rot.dtRotMatrix[2][0],Rot.dtRotMatrix[2][1],Rot.dtRotMatrix[2][2]);*/
            Rot[0, 3] = 0;
            Rot[1, 3] = 0;
            Rot[2, 3] = 0;
            Rot[3, 3] = 1;
            Rot[3, 0] = x;
            Rot[3, 1] = y;
            Rot[3, 2] = z;
            return Rot;
        } /* CvtQuatToRotationMatrix */
        private double[] DetermineEuler(Matrix Rot)
        {
            double
                fRoll,
                fCosRoll,
                fSinRoll;
            double[] Euler = new double[3];

            fRoll = Math.Atan2(Rot[1, 0], Rot[0, 0]);
            fCosRoll = Math.Cos(fRoll);
            fSinRoll = Math.Sin(fRoll);

            Euler[0] = fRoll;
            Euler[1] = Math.Atan2(-Rot[2, 0],
             (fCosRoll * Rot[0, 0]) + (fSinRoll *
             Rot[1, 0]));
            Euler[2] = Math.Atan2(
             (fSinRoll * Rot[0, 2]) -
             (fCosRoll * Rot[1, 2]),
             (-fSinRoll * Rot[0, 1]) +
             (fCosRoll * Rot[1, 1]));
            Euler[0] = Euler[0] * (180 / Math.PI);
            Euler[1] = Euler[1] * (180 / Math.PI);
            Euler[2] = Euler[2] * (180 / Math.PI);
            return Euler;
        }


    }
}
