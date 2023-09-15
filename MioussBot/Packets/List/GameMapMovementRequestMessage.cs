using MioussBot.Packets;
using System.Xml.Linq;

namespace MioussBot
{
    internal class GameMapMovementRequestMessage
    {
        static public readonly short id = 6639;
        static public readonly string name = "GameMapMovementRequestMessage";

        static public readonly bool isFiltered = true;
        static public readonly bool isIgnored = false;

        static public bool hasDoneMoved = false;
        static public bool hasServerResponse = false;

        static public bool IsRecording = false;

        static public void Decrypt(BigEndianReader reader)
        {
            uint keyMoveLen = (uint)reader.ReadShort();
            List<uint> keyMoves = new();

            for (int i = 0; i < keyMoveLen; i++)
            {
                keyMoves.Add((uint)reader.ReadShort());
            }

            double mapId = reader.ReadDouble();

            if (Trajet.IsRecording)
            {
                Trajet.AddMapMove((int)mapId, keyMoves.ToArray());

                Form1.LogPacketMessage($"[RECORDED] Map movement added to the trajet");
            }
            else
            {
                Form1.LogPacketMessage($"Key Moves : {string.Join(", ", keyMoves)} | MapId : {mapId}");
            }
        }
        static public void Send(int mapId, params short[] keyMoves)
        {
            BigEndianWriter writer = new();

            short keyMoveLen = (short)keyMoves.Length;

            writer.WriteShort(keyMoveLen);

            for (int i = 0; i < keyMoveLen; i++)
            {
                writer.WriteShort(keyMoves[i]);
            }

            writer.WriteDouble(mapId);

            writer.Pack(id);
        }
    }
}
