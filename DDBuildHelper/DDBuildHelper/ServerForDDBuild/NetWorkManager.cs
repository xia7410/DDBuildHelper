using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace UnityModule
{
    public class ServerForUnity
    {

        //单例
        private static ServerForUnity instance;
        public static ServerForUnity Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new ServerForUnity();
                }
                return instance;
            }
        }

        private static string ipAddress = "127.0.0.1";
        private static int port = 7899;

        Thread th;

        public void Start()
        {
            try
            {
                th = new Thread(new ThreadStart(startServer));
                th.Start();
            }
            catch (Exception err)
            {
                Debug.Print("ddBuildHelper服务器开启失败" + err.ToString());
            }

        }

        void startServer()
        {
            Socket tcpServer = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            tcpServer.Bind(new IPEndPoint(IPAddress.Parse(ipAddress), port));
            tcpServer.Listen(1);//最大连接数
            Debug.Print("ddBuildHelper服务器开启");
            while (true)
            {
                clientSocket = tcpServer.Accept();
                Debug.Print("客户端已连接");
                ReceiveMessage();
                // clientSocket = new UnityClient(clientSocket);
            }
        }
        private Socket clientSocket;

        static int maxBufferSize = 1024;
        private byte[] data = new byte[maxBufferSize];//存放客户端发来的数据

        private void ReceiveMessage()
        {
            while (true)
            {
                try
                {
                    int length = clientSocket.Receive(data);
                    if (length >= maxBufferSize)
                    {
                        Debug.Print("收到超长数据");
                    }
                    string message = Encoding.UTF8.GetString(data, 0, length);
                    SocketModel model = Coding<SocketModel>.decode(message);
                    onMessage(model);
                }
                catch (Exception)
                {
                    Debug.Print("unity断开");
                   // UnityManager.Instance.onUnityClosed();
                    break;
                }

            }
        }

        public void SendMessage(int type, int area, int command, string message)
        {
            SocketModel model = new SocketModel();
            model.Type = type;
            model.Area = area;
            model.Command = command;
            model.Message = message;

            string message2 = Coding<SocketModel>.encode(model);
            byte[] data = Encoding.UTF8.GetBytes(message2);
            try
            {
                clientSocket.Send(data);
            }
            catch (Exception err)
            {
                Debug.Print("向Unity发送消息失败！" + err.ToString());
               // UnityManager.Instance.onUnityClosed();              
            }

        }

        void onMessage(SocketModel model)
        {
            // Debug.Print("收到：" + model.Message);
            switch (model.Type)
            {
                //case Protocol.SELF_INFO:
                //    Debug.Print("unity请求个人信息");
                //    if (model.Command == 0)
                //    {
                //        Debug.Print("编辑器模式");
                //        UnityManager.Instance.unityMode = 0;
                //    }
                //    if (model.Command == 1)
                //    {
                //        Debug.Print("exe模式");
                //        //if (UnityManager.Instance.isUnityShow == false)
                //        //{//还没开启unity，unity就来了
                //        //    SendMessage(UnityProtocol.CLOSE_UNITY, 0, 0, "");
                //        //    Debug.Print("非法请求，还没开始，unity自己来了，关闭unity");
                //        //    return;
                //        //}
                //        UnityManager.Instance.unityMode = 1;
                //        try
                //        {
                //            Debug.Print("修改Unity标题" + model.Message);
                //            Process[] ps = Process.GetProcessesByName("叮叮鸟");
                //            foreach (Process p in ps)//遍历进程
                //            {
                //                SetWindowText(p.MainWindowHandle, "叮叮鸟------虚拟家装设计------免费共享平台------powered by H+ technology" + "      V" + model.Message);
                //            }
                //        }
                //        catch (Exception err)
                //        {
                //            Debug.Print("设置Unity Text错误：" + err.ToString());
                //        }
                //    }
                //    //返回个人信息
                //    Debug.Print("返回的个人信息是" + AppInfo.USER_NAME + "网络模式" + UnityManager.Instance.netMode);
                //    SendMessage(UnityProtocol.SELF_INFO, 0, UnityManager.Instance.netMode, AppInfo.USER_NAME);
                //    break;
                //case UnityProtocol.UNITY_QUIT:
                //    Debug.Print("unity自己退出了...");
                //    UnityManager.Instance.onUnityClosed();

                //    break;
                default:
                    Debug.Print("未知unity协议...");
                    break;
            }
        }

    }
}
