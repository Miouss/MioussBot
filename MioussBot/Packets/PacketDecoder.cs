using System.Net.Sockets;
using MioussBot.BigEndianRW;
using MioussBot.Packets;

namespace MioussBot
{
    internal class PacketDecoder
    {
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

        public static void DecodePacket(
              byte[] buffer,
              bool isClient = true,
              bool isProxy = false
          )
        {
            int i = 0;
            Packet packet;
            packet.id = 0;
            string status = isClient ? "CLIENT" : "SERVER";
            status = isProxy ? "PROXY" : status;

            //Form1.Log($"{Environment.NewLine} {Environment.NewLine} ------- NEW PACKET {status}-------");
            try
            {
                while (i < buffer.Length)
                {
                    packet.header = ((buffer[0] << 8) + buffer[i + 1]);

                    packet.id = (short)(packet.header >> 2);

                    packet.lengthType = (short)(packet.header & 3);

                    i += 2;

                    if (isClient || isProxy)
                    {
                        packet.instanceId =
                            (buffer[i] << 24)
                            + (buffer[i + 1] << 16)
                            + (buffer[i + 2] << 8)
                            + buffer[i + 3];

                        i += 4;
                    }
                    else
                    {
                        packet.instanceId = 0;
                    }

                    packet.length = packet.lengthType switch
                    {
                        1 => buffer[i],
                        2 => (buffer[i] << 8) + buffer[i + 1],
                        3 => (buffer[i] << 16) + (buffer[i + 1] << 8) + buffer[i + 2],
                        _ => 0,
                    };
                    ;

                    packet.content = new byte[packet.length];
                    Array.Copy(buffer, i + packet.lengthType, packet.content, 0, packet.length);

                    TreatPacket(packet.content, packet.id, status);

                    i += (packet.length + packet.lengthType);
                }
            }
            catch (Exception e)
            {
                //Form1.Log($"\tImpossible to decode packet [{packet.id}] : {e.Message}");
            }
        }

        public static void TreatPacket(byte[] content, int id, string status)
        {
            BigEndianReader reader = new(content);

            PacketDictionnary.LogPacketName(id, status);

            PacketDeserialize.reader = reader;
            
            PacketDeserialize.Handle(id);

        }
    }

}
