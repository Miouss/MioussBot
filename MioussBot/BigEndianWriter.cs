using System.Net.Sockets;
using System.Text;

namespace MioussBot
{
    internal class BigEndianWriter
    {
        private readonly int INT_SIZE = 32;
        private readonly int CHUNCK_BIT_SIZE = 7;
        private readonly int MASK_10 = 128;
        private readonly int MASK_01 = 127;

        private readonly List<byte> buffer = new();
        private Packet packet = new();

        public BigEndianWriter() { }

        public void SendPacket()
        {
            packet.content = buffer.ToArray();

            if (packet.length == 0)
                throw new Exception("Packet vide");

            SocketListener.ankamaServer.Socket.Send(packet.content);
            string packetHexa = BitConverter.ToString(packet.content).Replace("-", " ");

            Form1.AddText($"\n[SEND] ID : {packet.id}\n");

            buffer.Clear();
            packet = new();
        }

        private int GetLengthType()
        {
            int length = buffer.Count;
            packet.length = length;
            byte[] bytes = BitConverter.GetBytes(length);

            if (length > short.MaxValue)
            {
                WriteByteAtBegin(bytes[0]);
                WriteByteAtBegin(bytes[1]);
                WriteByteAtBegin(bytes[2]);

                return 3;
            }
            else if (length > byte.MaxValue)
            {
                WriteByteAtBegin(bytes[0]);
                WriteByteAtBegin(bytes[1]);

                return 2;
            }
            else if (length > 0)
            {
                WriteByteAtBegin(bytes[0]);

                return 1;
            }

            return 0;
        }

        private void WritePacketIdAndLengthType(int id, int lengthType)
        {
            short idAndLengthType = (short)((id << 2) + lengthType);
            byte[] bytes = BitConverter.GetBytes(idAndLengthType);
            WriteByteAtBegin(bytes[0]);
            WriteByteAtBegin(bytes[1]);
        }

        public void Pack(short id, int instanceId)
        {
            packet.id = id;
            int lengthType = GetLengthType();
            foreach (byte b in BitConverter.GetBytes(instanceId))
                WriteByteAtBegin(b);
            WritePacketIdAndLengthType(id, lengthType);
        }

        public void WriteString(string message)
        {
            byte[] messageBytes = Encoding.UTF8.GetBytes(message);
            short length = (short)messageBytes.Length;

            byte[] lengthBytes = BitConverter.GetBytes(length).Reverse().ToArray();

            foreach (byte b in lengthBytes)
                WriteByte(b);

            foreach (byte b in messageBytes)
                WriteByte(b);
        }

        public void WriteShort(short value)
        {
            byte[] bytes = BitConverter.GetBytes(value);
            WriteBytes(bytes);
        }

        public void WriteInt(int value)
        {
            byte[] bytes = BitConverter.GetBytes(value).Reverse().ToArray();

            foreach (byte b in bytes)
                WriteByte(b);
        }

        public void WriteUInt(uint value)
        {
            byte[] bytes = BitConverter.GetBytes(value).Reverse().ToArray();

            foreach (byte b in bytes)
                WriteByte(b);
        }

        public void WriteByte(byte value)
        {
            buffer.Add(value);
        }

        public void WriteByteAtBegin(byte value)
        {
            buffer.Insert(0, value);
        }

        public void WriteBytes(byte[] values)
        {
            for (int i = 0; i < values.Length; i++)
                WriteByte(values[i]);
        }

        public void WriteBoolean(bool value)
        {
            WriteByte((byte)(value ? 1 : 0));
        }

        public static uint SetFlag(uint a, uint pos, bool b)
        {
            switch (pos)
            {
                case 0:
                    if (b)
                    {
                        a |= 1;
                    }
                    else
                    {
                        a &= 255 - 1;
                    }
                    break;
                case 1:
                    if (b)
                    {
                        a |= 2;
                    }
                    else
                    {
                        a &= 255 - 2;
                    }
                    break;
                case 2:
                    if (b)
                    {
                        a |= 4;
                    }
                    else
                    {
                        a &= 255 - 4;
                    }
                    break;
                case 3:
                    if (b)
                    {
                        a |= 8;
                    }
                    else
                    {
                        a &= 255 - 8;
                    }
                    break;
                case 4:
                    if (b)
                    {
                        a |= 16;
                    }
                    else
                    {
                        a &= 255 - 16;
                    }
                    break;
                case 5:
                    if (b)
                    {
                        a |= 32;
                    }
                    else
                    {
                        a &= 255 - 32;
                    }
                    break;
                case 6:
                    if (b)
                    {
                        a |= 64;
                    }
                    else
                    {
                        a &= 255 - 64;
                    }
                    break;
                case 7:
                    if (b)
                    {
                        a |= 128;
                    }
                    else
                    {
                        a &= 255 - 128;
                    }
                    break;
                default:
                    throw new Exception("Bytebox overflow.");
            }
            return a;
        }

        public void WriteVarInt(int value)
        {
            if (value >= 0 && value <= MASK_01)
            {
                WriteInt(value);
                return;
            }

            int c = value;

            while (c != 0)
            {
                int b = c & MASK_01;
                c >>>= CHUNCK_BIT_SIZE;

                if (c > 0)
                    b |= MASK_10;

                WriteByte((byte)b);
            }
        }

        public void WriteVarShort(int value)
        {
            if (value > short.MaxValue || value < short.MinValue)
                throw new Exception("Forbidden value");

            if (value >= 0 && value <= MASK_01)
            {
                WriteShort((short)value);
                return;
            }

            int c = value & 65535;

            if (c >= 0 && c <= MASK_01)
            {
                WriteByte((byte)c);
                return;
            }

            while (c != 0)
            {
                int b = c & MASK_01;
                c >>>= CHUNCK_BIT_SIZE;

                if (c > 0)
                    b |= MASK_10;

                WriteByte((byte)b);
            }
        }   

    }

}
