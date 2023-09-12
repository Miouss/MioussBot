using MioussBot.BigEndianRW;
using MioussBot.Dofus.Assets;
using System.Diagnostics;

namespace MioussBot.Packets
{
    internal class PacketSerializeAndSend
    {
        static public List<Trajet> trajets = new();
        static public bool hasDoneMoved = false;
        static public bool hasServerResponse = false;

        static public void test()
        {
            int mapId = 189792776;

            trajets = new()
            {
                new Trajet(mapId, new short[] { 12535, 12576 }),
                new Trajet(mapId, new short[] { 20768, 20724 }),
                new Trajet(mapId, new short[] { 28916, 28876 }),
                new Trajet(mapId, new short[] { 4300, 4343 })
            };

            new Thread(() =>
            {
                while (trajets.Any())
                {
                    Form1.Log($"Moved : {hasDoneMoved} | Response : {hasServerResponse}");

                    if (hasDoneMoved && hasServerResponse)
                    {
                        Form1.Log("Sending next movement");
                        Trajet trajet = trajets[0];
                        trajets.RemoveAt(0);
                        hasDoneMoved = false;
                        hasServerResponse = false;
                        GameMapMovementMessage(trajet.keyMoves, trajet.mapId);
                    }

                    Thread.Sleep(1000);
                }
                Form1.Log("Trajet done");
            }).Start();
        }

        static public void ZaapAstrub()
        {
            InteractUseRequest(Zaap.Astrub.id, Zaap.Astrub.skillInstanceId);

        }

        static public void ZaapTainela()
        {
            InteractUseRequest(Zaap.Tainela.id, Zaap.Tainela.skillInstanceId);
        }

        static private void InteractUseRequest(int elemId, int skillInstanceId)
        {
            BigEndianWriter writer = new();
            writer.WriteVarInt(elemId);
            writer.WriteVarInt(skillInstanceId);
            writer.Pack(6295, 11);

            writer.SendPacket();
        }

        static private void MapInformationsRequestMessage(int mapId)
        {
            BigEndianWriter writer = new();

            writer.WriteDouble(mapId);
            writer.Pack(9293, 11);

            writer.SendPacket();
        }

        static public void GameMapMovementMessage(short[] keyMoves, int mapId)
        {
            BigEndianWriter writer = new();

            short keyMoveLen = (short)keyMoves.Length;

            writer.WriteShort(keyMoveLen);

            for (int i = 0; i < keyMoveLen; i++)
            {
                writer.WriteShort(keyMoves[i]);
            }

            writer.WriteDouble(mapId);

            writer.Pack(6639, 11);
            writer.SendPacket();

        }
    }
}


struct Trajet
{
    public int mapId;
    public short[] keyMoves;

    public Trajet(int mapId, short[] keyMoves)
    {
        this.mapId = mapId;
        this.keyMoves = keyMoves;
    }
}