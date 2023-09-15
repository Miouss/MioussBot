using MioussBot.Packets;
using System.Xml.Linq;

namespace MioussBot
{
    internal class MapInformationsRequestMessage
    {
        static public readonly short id = 9293;
        static public readonly string name = "MapInformationsRequestMessage";

        static public readonly bool isFiltered = false;
        static public readonly bool isIgnored = false;

        static public void Send(int mapId)
        {
            BigEndianWriter writer = new();

            writer.WriteDouble(mapId);
            writer.Pack(9293);
        }
    }
}
