namespace MioussBot.Packets
{
    internal struct Packet
    {
        public int header;
        public short id;
        public short lengthType;
        public int length;
        public byte[] content;

        public int instanceId;
    }
}
