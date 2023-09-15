using MioussBot.Packets;
using System.Xml.Linq;

namespace MioussBot
{
    internal class StatedElementUpdatedMessage
    {
        static public readonly short id = 5787;
        static public readonly string name = "StatedElementUpdatedMessage";

        static public readonly bool isFiltered = false;
        static public readonly bool isIgnored = false;

        static public void Decrypt(BigEndianReader reader)
        {
            Form1.LogPacketMessage($"{BitConverter.ToString(reader.content)}");
            uint elementId = (uint)reader.ReadInt();
            uint elementCellId = (uint)reader.ReadVarShort();
            uint elementState = (uint)reader.ReadVarInt();
            bool onCurrentMap = reader.ReadBoolean();

            Coord coord = Map.getCoordByCellId((int)elementCellId);
            double cellId = Map.GetCellIdByCoord(coord.x, coord.y);

            Form1.LogPacketMessage($"Element ID : {elementId} | Element Cell ID : {elementCellId} [{coord.x}, {coord.y}] [{cellId}] | Element State : {elementState} | On Current Map : {onCurrentMap}");

        }

        static public void AddAsDeserialize()
        {
            PacketDeserialize.packetDictionnary[id] = () => Decrypt(PacketDeserialize.reader);
        }

        static public void AddInDictionnary()
        {
            PacketDictionnary.AddPacket(id, name, isIgnored, isFiltered);
        }
    }
}
