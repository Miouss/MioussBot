using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MioussBot
{
    internal class PacketReader
    {
        static public BigEndianReader reader;

        static public void GameMapMovementMessage()
        {
            uint keyMoveLen = (uint)reader.ReadShort();
            List<uint> keyMoves = new();

            for (int i = 0; i < keyMoveLen; i++)
            {
                keyMoves.Add((uint)reader.ReadShort());
            }

            double mapId2 = reader.ReadDouble();

            Form1.AddText($"\tKeyMoveLen : {keyMoveLen} | keyMoves: {string.Join(", ", keyMoves)} | MapId : {mapId2}");

        }

        static public void CurrentMapMessage()
        {
            double mapId = reader.ReadDouble();

            Form1.AddText($"\tMapId : {mapId}");
        }

        static public void InteractiveUseRequestMessage()
        {
            uint elemId = (uint)reader.ReadVarInt();
            uint elemCellId = (uint)reader.ReadVarInt();

            Form1.AddText($"\tElement ID : {elemId} | Element Cell ID : {elemCellId}");

        }
    }
}
