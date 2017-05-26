using System;
using System.Net;
using System.Net.Sockets;
using TcpServer;

namespace ServerSocket
{
    class MainClass
    {
        static void Main(string[] args)
        {
            //TcpHelper helper = new TcpHelper ();
            //helper.Run (8080);
            SocketHelper sktHelper = new SocketHelper();
            sktHelper.Port = 8080;
            sktHelper.Start();
        }
    }
}
