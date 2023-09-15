using System;
using System.Net.Sockets;
using MioussBot.Packets;

namespace MioussBot
{
    internal class PacketDecoder
    {
        static readonly short SHIFT_TWO_BYTES = 2;
        static readonly short SHIFT_FOUR_BYTES = 4;

        public static void Handler(Socket socket, Socket targetSocket, bool isClient = true)
        {

            byte[] buffer = new byte[socket.Available];

            if (buffer.Length != 0)
            {
                socket.Receive(buffer);
                DecodePacket(buffer, isClient);
                targetSocket.Send(buffer);
            }

        }

        static public void DecodePacket(
              byte[] buffer,
              bool isClient = true,
              bool isProxy = false
          )
        {
            short i = 0;
            string clientType = isProxy ? "PROXY" : "CLIENT";
            string status = isClient ? clientType : "SERVER";

            Packet packet = new();

            try
            {
                while (i < buffer.Length)
                {
                    packet.header = GetHeader(buffer, i);
                    packet.id = GetId(packet.header);
                    packet.lengthType = GetLengthType(packet.header);

                    i += SHIFT_TWO_BYTES;

                    if (isClient || isProxy)
                    {
                        packet.instanceId = GetInstanceId(buffer, i);
                        i += SHIFT_FOUR_BYTES;
                    }

                    packet.length = GetLength(buffer, packet.lengthType, i);

                    short shiftToContent = packet.lengthType;

                    i += shiftToContent;

                    CopyContent(ref packet, buffer, i);
                    TreatPacket(packet.content, packet.id, status);

                    short shiftToNextPacket = (short)(packet.length);
                    i += shiftToNextPacket;
                }
            }
            catch (Exception e)
            {
                Form1.Log($"\t[{packet.id}] : {e.Message}");
            }
        }

        static void TreatPacket(byte[] content, int id, string status)
        {
            BigEndianReader reader = new(content);

            PacketDictionnary.LogPacketName(id, status);

            PacketDeserialize.reader = reader;

            PacketDeserialize.Handle(id);

        }

        static short GetHeader(byte[] buffer, int i)
        {
            return (short)((short)(buffer[0] << 8) + buffer[i + 1]);
        }

        static short GetId(int header)
        {
            return (short)(header >> 2);
        }

        static short GetLengthType(int header)
        {
            return (short)(header & 3);
        }

        static int GetLength(byte[] buffer, short lengthType, int i)
        {
            return lengthType switch
            {
                1 => buffer[i],
                2 => (buffer[i] << 8) + buffer[i + 1],
                3 => (buffer[i] << 16) + (buffer[i + 1] << 8) + buffer[i + 2],
                _ => 0,
            };
        }

        static int GetInstanceId(byte[] buffer, int i)
        {
            return (buffer[i] << 24) + (buffer[i + 1] << 16) + (buffer[i + 2] << 8) + buffer[i + 3];
        }

        static void CopyContent(ref Packet packet, byte[] buffer, int startIndex)
        {
            packet.content = new byte[packet.length];
            Array.Copy(buffer, startIndex, packet.content, 0, packet.length);
        }
    }

}
