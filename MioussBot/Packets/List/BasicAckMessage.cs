using MioussBot.Packets;

namespace MioussBot
{
    internal class BasicAckMessage
    {
        static public readonly short id = 8309;
        static public readonly string name = "BasicAckMessage";

        static public readonly bool isFiltered = false;
        static public readonly bool isIgnored = false;

        static public void Decrypt()
        {
            Form1.LogPacketMessage("BasicAckMessage");
        }
    }
}
