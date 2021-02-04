﻿using log4net.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using Util;

namespace Machine
{
    class Program
    {
        //private static LoggingUtility _log = new LoggingUtility("Log_Machine", Level.Error, 30);
        //public static LoggingUtility Log { get { return _log; } }

        //시작 Log.
        //("프로젝트명_클래스명_메서드명 상태")
        //Log.WriteInfo("DAC_Log4NetSampleCode_ErrorSample() 시작");

        //종료 Log.
        //Log.WriteInfo("DAC_Log4NetSampleCode_ErrorSample() 정상종료");

        //로그 오류
        //Log.WriteError("DAC_Log4NetSampleCode_ErrorSample() 오류", err);


        static void Main(string[] args)
        {
            Service1 service = new Service1();
            service.taskID = args[0];
            service.hostIP = args[1];
            service.hostPort = int.Parse(args[2]);
            service.OnStart();

            Console.ReadLine();



        }
    }
    public class Service1
    {
        LoggingUtility loggingUtility;
        TcpListener listener;
        Timer timer1;
        TcpClient tc;
        NetworkStream stream;

        LoggingUtility Log { get { return loggingUtility; } }

        public string taskID { get; set; }
        public string hostIP { get; set; }
        public int hostPort { get; set; }

        public void OnStart()
        {
            loggingUtility = LoggingUtility.GetLoggingUtility(taskID, Level.Debug, 30);

            Console.WriteLine("서비스 시작");
            Log.WriteInfo("서비스 시작");

            if (listener == null)
            {
                listener = new TcpListener(IPAddress.Parse(hostIP), hostPort);
            }

            AsyncEchoServer();
        }

        private async void AsyncEchoServer()
        {
            listener.Start();
            while (true)
            {
                tc = await listener.AcceptTcpClientAsync().ConfigureAwait(false);
                stream = tc.GetStream();

                timer1 = new Timer(3000);
                timer1.Elapsed += Timer1_Elapsed;
                timer1.Enabled = true;
                timer1.AutoReset = true;
                // await Task.Factory.StartNew(AsyncTcpProcess, tc);
            }
        }

        private void Timer1_Elapsed(object sender, ElapsedEventArgs e)
        {
            Random rnd = new Random((int)DateTime.UtcNow.Ticks);

            //50|2|1
            string msg = $"{rnd.Next(1, 77)}|{rnd.Next(3, 5)}|{rnd.Next(0, 2)}";
            byte[] buff = Encoding.Default.GetBytes(msg);

            stream.Write(buff, 0, buff.Length);
            Console.WriteLine(msg);
            Log.WriteInfo("데이터전송: " + msg);
        }

        public void OnStop()
        {
            listener.Stop();
        }
    }
}
