using MioussBot.Dofus.Assets;
using MioussBot.Packets;
using System.Xml.Linq;

namespace MioussBot
{
    internal class InteractiveUseRequestMessage
    {
        static public readonly short id = 6295;
        static public readonly string name = "InteractiveUseRequestMessage";

        static public readonly bool isFiltered = false;
        static public readonly bool isIgnored = false;

        static public void Decrypt(BigEndianReader reader)
        {
            uint elemId = (uint)reader.ReadVarInt();
            uint skillInstanceId = (uint)reader.ReadVarInt();

            if (Trajet.IsRecording)
            {
                Trajet.AddCollectAction(elemId, skillInstanceId);
                Form1.LogPacketMessage("[RECORDED] Collect action added to the trajet");
            }
            else
            {
                Form1.LogPacketMessage($"Element ID : {elemId} | Skill Instance ID : {skillInstanceId}");
            }
        }
        static public void Send(int elemId, int skillInstanceId)
        {
            BigEndianWriter writer = new();
            writer.WriteVarInt(elemId);
            writer.WriteVarInt(skillInstanceId);
            writer.Pack(id);
        }

        static public void ZaapAstrub()
        {
            Send(Zaap.Astrub.id, Zaap.Astrub.skillInstanceId);

        }

        static public void ZaapTainela()
        {
            Send(Zaap.Tainela.id, Zaap.Tainela.skillInstanceId);
        }

        static public void AddAsDeserialize()
        {
            PacketDeserialize.packetDictionnary[id] = () => Decrypt(PacketDeserialize.reader);
        }

        static public void AddInDictionnary()
        {
            PacketDictionnary.AddPacket(id, name, isIgnored, isFiltered);
        }
    }
}
