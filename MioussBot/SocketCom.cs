using System.Net;
using System.Net.Sockets;

namespace MioussBot
{
    internal class SocketCom
    {
        public Socket Socket { get; set; }

        private Thread thread;
        private CancellationTokenSource tokenSource;

        private bool isClient;

        public SocketCom(bool isClient)
        {
            this.isClient = isClient;

            if (isClient)
            {
                StartClient();
            }
            else
            {
                StartServer();
            }
        }

        public void StartFiltering(Socket targetSocket)
        {
            tokenSource = new();
            thread =
                 new(() =>
                 {
                     try
                     {
                         while (!tokenSource.Token.IsCancellationRequested)
                         {
                             Filter.Handler(Socket, targetSocket, isClient);
                         }

                         StopSocket();
                     }
                     catch (Exception e)
                     {
                         Form1.AddText(e.ToString());
                     }
                 });

            thread.Start();
        }

        private void StopSocket()
        {
            Socket?.Shutdown(SocketShutdown.Both);
            Socket?.Close();

            string origin = isClient ? "Client" : "Server";

            Form1.AddText($"{origin} connection closed");
        }


        public void StopFiltering()
        {
            tokenSource?.Cancel();
            thread?.Join();
        }

        public void Stop()
        {
            StopFiltering();
            tokenSource?.Dispose();
        }

        private void StartClient()
        {
            IPAddress ipAddress = IPAddress.Parse("127.0.0.1");
            IPEndPoint localEndPoint = new(ipAddress, 443);

            Socket listener = new(ipAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
            listener.Bind(localEndPoint);
            listener.Listen(10);

            Form1.AddText("Waiting for a connection...");

            Socket = listener.Accept();

            Form1.AddText($"Connected to Dofus Client by {Socket.RemoteEndPoint}");

        }

        private void StartServer()
        {
            Socket = new(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            Socket.Connect("172.65.220.96", 5555);

            Form1.AddText($"Connected to Ankama Server by {Socket.RemoteEndPoint}");
        }
    }
}
