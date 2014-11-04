using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO.Ports;
using System.IO;
using System.Threading;

namespace capTv
{
    class Robotcontrol
    {
        private static SerialPort Serial_T;
        private static SerialPort Serial_R;
        public void Robot_connect()
        {

            Serial_T = new SerialPort("COM23");

            Serial_T.BaudRate = 9600;
            Serial_T.Parity = Parity.None;
            Serial_T.StopBits = StopBits.One;
            Serial_T.DataBits = 8;

            Serial_T.Open();
             Serial_R = new SerialPort("CO24");

            Serial_R.BaudRate = 9600;
            Serial_R.Parity = Parity.None;
            Serial_R.StopBits = StopBits.One;
            Serial_R.DataBits = 8;

            Serial_R.Open();


            System.Diagnostics.Debug.WriteLine("connected");


        }
        public void disconnect()
        {
            Serial_T.Close();
            Serial_R.Close();
            Serial_T.Dispose();
            Serial_R.Dispose();

        }
        public void Robot_Rotate(int r1, int r2)
        {
            string Rotate1;
            string Rotate2;
            Rotate1 = "s1 " + r1.ToString();
            Rotate2 = "s2 " + r2.ToString();
            Serial_R.WriteLine(Rotate1);
            Thread.Sleep(50);
            Serial_R.WriteLine(Rotate2);
        }
        public void Robot_Translate(int t1, int t2)
        {
            string Horizon;
            string Vertical;
          
            Horizon = "Bc90" + t1.ToString() + "j";
            Vertical = "Ac90" + t2.ToString() + "j";
            
            Serial_T.WriteLine(Horizon);
            Thread.Sleep(50);
            Serial_T.WriteLine(Vertical);
            //Thread.Sleep(50);
        
        }
    }
}
