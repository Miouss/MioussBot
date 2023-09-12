using System.Diagnostics;
using System.Net.Sockets;

namespace MioussBot
{
    internal class SocketListener
    {
        static public SocketCom? client;
        static public SocketCom? ankamaServer;


        static public void Start()
        {
            client = new SocketCom(true);

            ankamaServer = new SocketCom(false);

            try
            {
                if (client.Socket.Connected && ankamaServer.Socket.Connected)
                {
                    client.StartFiltering(ankamaServer.Socket);
                    ankamaServer.StartFiltering(client.Socket);
                }
            }
            catch (Exception e)
            {
                Form1.AddText(e.ToString());

            }
        }
        static public void StopFiltering()
        {
            client?.StopFiltering();
            ankamaServer?.StopFiltering();
        }

        static public void Stop()
        {
            client?.Stop();
            ankamaServer?.Stop();
        }
    }

}