using MioussBot.Packets;

namespace MioussBot
{
    internal class ObjectQuantityMessage
    {
        static public readonly short id = 2962;
        static public readonly string name = "ObjectQuantityMessage";

        static public readonly bool isFiltered = false;
        static public readonly bool isIgnored = false;
        static public void Decrypt(BigEndianReader reader)
        {
            uint objectUID = (uint)reader.ReadVarInt();
            uint quantity = (uint)reader.ReadVarInt();
            uint origin = reader.ReadByte();


            Form1.LogPacketMessage($"ObjectUID : {objectUID} | Quantity : {quantity} | Origin : {origin}");
        }
    }
}
