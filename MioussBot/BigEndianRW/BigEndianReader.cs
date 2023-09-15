using System.Text;

namespace MioussBot
{
    internal class BigEndianReader
    {
        private readonly int INT_SIZE = 32;
        private readonly int SHORT_SIZE = 16;
        private readonly int CHUNCK_BIT_SIZE = 7;
        private readonly int MASK_10 = 128;
        private readonly int MASK_01 = 127;
        private readonly int UNSIGNED_SHORT_MAX_VALUE = 65536;

        public readonly byte[] content;
        private int position = 0;

        public BigEndianReader(byte[] content)
        {
            this.content = content;
        }

        public byte ReadByte()
        {
            if (position >= content.Length)
                throw new Exception("No more (READ BYTE) byte to read");

            return content[position++];
        }

        public byte[] ReadBytes(int length)
        {
            if (position + length > content.Length)
                throw new Exception("No more byte to read");

            byte[] bytes = new byte[length];

            Array.Copy(content, position, bytes, 0, length);

            position += length;

            return bytes;
        }

        public int ReadInt()
        {
            return (ReadByte() << 24) + (ReadByte() << 16) + (ReadByte() << 8) + ReadByte();
        }

        public bool ReadBoolean()
        {
            return ReadByte() == 01;
        }

        public short ReadShort()
        {
            return (short)((short)(ReadByte() << 8) + ReadByte());
        }

        public int ReadIntOn16Bits()
        {
            return (ReadByte() << 8) + ReadByte();
        }

        public string ReadString()
        {
            int length = ReadIntOn16Bits();
            return Encoding.UTF8.GetString(ReadBytes(length));
        }

        public int ReadVarInt()
        {
            int value = 0;
            int offset = 0;

            while (offset < INT_SIZE)
            {
                int b = ReadByte();
                bool hasNext = (b & MASK_10) == MASK_10;
                if (offset > 0)
                    value += (b & MASK_01) << offset;
                else
                    value += b & MASK_01;

                offset += CHUNCK_BIT_SIZE;

                if (!hasNext)
                    return value;
            }

            throw new Exception("Too much data");
        }

        public short ReadVarShort()
        {
            int value = 0;
            int offset = 0;

            while (offset < SHORT_SIZE)
            {
                int b = ReadByte();
                bool hasNext = (b & MASK_10) == MASK_10;
                if (offset > 0)
                    value += (b & MASK_01) << offset;
                else
                    value += b & MASK_01;

                offset += CHUNCK_BIT_SIZE;

                if (!hasNext)
                {
                    if (value > short.MaxValue)
                        value -= UNSIGNED_SHORT_MAX_VALUE;

                    return (short)value;
                }
            }

            throw new Exception("Too much data");
        }

        public double ReadDouble()
        {
            long longBits =
                ((long)ReadByte() << 56) +
                ((long)ReadByte() << 48) +
                ((long)ReadByte() << 40) +
                ((long)ReadByte() << 32) +
                ((long)ReadByte() << 24) +
                ((long)ReadByte() << 16) +
                ((long)ReadByte() << 8) |
                ReadByte();

            return BitConverter.Int64BitsToDouble(longBits);
        }
    }
}