using Microsoft.VisualBasic.ApplicationServices;
using System.Net;
using System.Net.Sockets;

namespace MioussBot
{
    internal class SocketCom
    {
        static CancellationTokenSource Cts;
        static bool IsRunning = false;

        public Socket Socket;
        readonly bool IsClient;

        public SocketCom(bool isClient)
        {
            IsClient = isClient;

            if (IsClient)
                StartClient();
            else
                StartServer();
        }

        static public void Initialize()
        {
            Cts = new();
            IsRunning = true;
        }

        static public void StopAll()
        {
            if (IsRunning)
            {
                Cts?.Cancel();

                Cts?.Dispose();

                IsRunning = false;
            }
        }

        public void StartFiltering(Socket targetSocket)
        {
            new Task(() =>
            {
                try
                {
                    while (!Cts.Token.IsCancellationRequested && Socket.Connected)
                    {
                        PacketDecoder.Handler(Socket, targetSocket, IsClient);
                    }
                    StopCom();
                }
                catch (Exception e)
                {
                    Form1.Log(e.ToString());
                }
            }).Start();
        }

        public void StopCom()
        {
            Socket?.Shutdown(SocketShutdown.Both);
            Socket?.Close();

            string origin = IsClient ? "Client" : "Server";

            Form1.Log($"{origin} connection closed");
        }

        void StartClient()
        {
            IPAddress ipAddress = IPAddress.Parse("127.0.0.1");
            IPEndPoint localEndPoint = new(ipAddress, 443);

            Socket listener = new(ipAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
            listener.Bind(localEndPoint);
            listener.Listen(10);

            Form1.Log("Waiting for a connection...");

            Socket = listener.Accept();

            Form1.Log($"Connected to Dofus Client by {Socket.RemoteEndPoint}");

        }

        void StartServer()
        {
            Socket = new(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            Socket.Connect("172.65.220.96", 5555);

            Form1.Log($"Connected to Ankama Server by {Socket.RemoteEndPoint}");
        }
    }
}
