using MioussBot.Packets;
using System.Xml.Linq;

namespace MioussBot
{
    internal class CurrentMapMessage
    {
        static public readonly short id = 3145;
        static public readonly string name = "CurrentMapMessage";

        static public readonly bool isFiltered = false;
        static public readonly bool isIgnored = false;

        static public void Decrypt(BigEndianReader reader)
        {
            double mapId = reader.ReadDouble();

            Form1.LogPacketMessage($"MapId : {mapId}");
        }
    }
}
