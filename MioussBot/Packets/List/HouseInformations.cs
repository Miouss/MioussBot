using System.Xml.Linq;

namespace MioussBot.Packets.List
{
    internal class HouseInformations
    {
        static public readonly short id = 5854;
        static public readonly string name = "HouseInformations";

        static public readonly bool isFiltered = false;
        static public readonly bool isIgnored = false;

        public uint houseId = 0;
        public uint modelId = 0;

        public void Decrypt(BigEndianReader reader)
        {
            houseId = (uint)reader.ReadVarInt();
            modelId = (ushort)reader.ReadVarShort();
        }
    }
}
