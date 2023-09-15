using MioussBot.Packets;

namespace MioussBot
{
    internal class ObjectItemAdded
    {

        static public readonly short id = 3025;
        static public readonly string name = "ObjectItemAdded";

        static public readonly bool isFiltered = false;
        static public readonly bool isIgnored = false;

        static public void Decrypt(BigEndianReader reader)
        {
            uint id = 0;
            uint item = 0;

            uint position = (uint)reader.ReadShort();
            uint objectGID = (uint)reader.ReadVarInt();
            uint effectsLen = (uint)reader.ReadShort();
            List<uint> effects = new();

            for (int i = 0; i < effectsLen; i++)
            {
                id = (uint)reader.ReadVarShort();
                effects.Add((uint)reader.ReadVarShort());
            }

            uint objectUID = (uint)reader.ReadVarInt();
            uint quantity = (uint)reader.ReadVarInt();

            Form1.LogPacketMessage($"Position : {position} | Object GID : {objectGID} | Effects : {string.Join(", ", effects)} | Object UID : {objectUID} | Quantity : {quantity}");

        }
    }
}
