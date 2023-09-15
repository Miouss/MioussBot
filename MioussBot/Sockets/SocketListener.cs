namespace MioussBot
{
    internal class SocketListener
    {
        static public SocketCom? client;
        static public SocketCom? ankamaServer;

        static public void Start()
        {
            try
            {
                client = new SocketCom(true);
                ankamaServer = new SocketCom(false);

                SocketCom.Initialize();

                client.StartFiltering(ankamaServer.Socket);
                ankamaServer.StartFiltering(client.Socket);
            }
            catch (Exception e)
            {
                Form1.Log(e.ToString());
            }
        }
    }

}