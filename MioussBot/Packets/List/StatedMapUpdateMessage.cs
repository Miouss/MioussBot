using System.Xml.Linq;

namespace MioussBot.Packets.List
{
    internal class StatedMapUpdateMessage
    {
        static public readonly short id = 8533;
        static public readonly string name = "StatedMapUpdateMessage";

        static public readonly bool isFiltered = false;
        static public readonly bool isIgnored = false;

        static public void Decrypt(BigEndianReader reader)
        {
            uint statedElementsLen = (uint)reader.ReadVarShort();
            for (int i = 0; i < statedElementsLen; i++)
         {
                uint elementId = (uint)reader.ReadInt();
                uint elementCellId = (uint)reader.ReadVarShort();
                uint elementState = (uint)reader.ReadVarInt();
                bool onCurrentMap = reader.ReadBoolean();

                int loc2 = (int)Math.Floor((double)elementCellId / 14);
                int loc3 = (int)Math.Floor((double)(loc2 + 1) / 2);
                int loc4 = loc2 - loc3;
                int loc5 = (int)(elementCellId - loc2 * 14);

                Form1.LogPacketMessage($"Element ID : {elementId} | Element Cell ID : {elementCellId} [{loc3 + loc5}, {loc5 - loc4}] | Element State : {elementState} | On Current Map : {onCurrentMap}");
            }
        }
    }
}
