using System.Net.Sockets;

namespace MioussBot
{
    internal class Filter
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
              bool isClient = true
          )
        {
            int i = 0;
            Packet packet;
            packet.id = 0;
            string status = isClient ? "CLIENT" : "SERVER";
            //Form1.AddText($"{Environment.NewLine} {Environment.NewLine} ------- NEW PACKET {status}-------");
            try
            {
                while (i < buffer.Length)
                {

                    packet.idAndLengthType = (short)((buffer[i] << 8) + buffer[i + 1]);

                    packet.id = (short)(packet.idAndLengthType >> 2);
                    packet.lengthType = (short)(packet.idAndLengthType & 3);

                    i += 2;

                    if (isClient)
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

                    TreatPacket(packet, status);

                    i += packet.length + packet.lengthType;
                }
            }
            catch (Exception e)
            {
                Form1.AddText($"\tImpossible to decode packet [{packet.id}] : {e.Message}");
            }
        }

        static void TreatPacket(Packet packet, string status)
        {
            BigEndianReader reader = new(packet.content);

            PacketDictionnary.LogPacketName(packet.id, status);

            PacketReader.reader = reader;

            switch (packet.id)
            {
                case 3145:
                    PacketReader.CurrentMapMessage();
                break;
                case 6295:
                    PacketReader.InteractiveUseRequestMessage();
                    break;
                case 6639:
                    PacketReader.GameMapMovementMessage();
                 break;
            }

        }
    }

}
