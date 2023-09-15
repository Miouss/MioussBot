using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace MioussBot.Packets.List
{
    internal class MapComplementaryInformationsDataMessage
    {
        static public readonly short id = 695;
        static public readonly string name = "MapComplementaryInformationsDataMessage";

        static public readonly bool isFiltered = false;
        static public readonly bool isIgnored = false;

        public static List<HouseInformations> houses = new ();
        public static void Decrypt(BigEndianReader reader)
        {
            Form1.LogPacketMessage("MapComplementaryInformationsDataMessage");
            Form1.LogPacketMessage($"{BitConverter.ToString(reader.content)}");

            uint subAreaId = (uint)reader.ReadVarShort();
            double mapId = reader.ReadDouble();
            Form1.LogPacketMessage($"subAreaId = {subAreaId} | mapId = {mapId}");
            uint housesLen = (ushort)reader.ReadVarShort();

            for (int i = 0; i < housesLen; i++)
            {
                uint id1 = (ushort)reader.ReadVarShort();
                Form1.LogPacketMessage($"HouseId = {id1}");
                HouseInformations item1 = new ();
                item1.Decrypt(reader);
                houses.Add(item1);
            }

            uint actorsLen = (uint)reader.ReadVarShort();
            uint interactiveElementsLen = (uint)reader.ReadVarShort();
            uint statedElementsLen = (uint)reader.ReadVarShort();
            List<StatedElement> statedElements = new ();

            for (int i = 0; i < statedElementsLen; i++)
            {
                uint id2 = (uint)reader.ReadVarShort();
                StatedElement stateEl = new ();
                stateEl.Decrypt(reader);
                Form1.LogPacketMessage($"elementId = {stateEl.elementId} | elCellId = {stateEl.elementCellId} | elState = {stateEl.elementState} | onCurrMap = {stateEl.onCurrentMap}");
                statedElements.Add(stateEl);
            }

            Form1.LogPacketMessage($"statedElementsLen = {statedElementsLen} | interactiveElementsLen = {interactiveElementsLen} | housesLen = {housesLen} | Actors = {actorsLen}");
                //for (int i = 0; i < actorsLen; i++)
                //{
                //    uint id2 = (uint)reader.ReadVarShort();
                //    GameRolePlayActorInformations item2 = new GameRolePlayActorInformations();
                //    item2.Decrypt(reader);
                //}
        }
    }
}
