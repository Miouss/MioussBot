namespace MioussBot
{
    internal struct Packet
    {
        public short idAndLengthType;
        public short id;
        public short lengthType;
        public int length;
        public byte[] content;

        public int instanceId;
    }
}
