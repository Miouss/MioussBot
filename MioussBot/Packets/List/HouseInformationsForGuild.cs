using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace MioussBot.Packets.List
{
    internal class HouseInformationsForGuild : HouseInformations
    {
        static public readonly short id = 5625;
        static public readonly string name = "HouseInformationsForGuild";

        static public readonly bool isFiltered = false;
        static public readonly bool isIgnored = false;

        public uint instanceId = 0;
        public bool secondHand = false;
        public AccountTagInformation ownerTag;
        public int worldX = 0;
        public int worldY = 0;
        public double mapId = 0;
        public uint subAreaId = 0;
        public List<int> skillListIds;
        public uint guildshareParams = 0;

        public void Decrypt(BigEndianReader reader)
        {
            Form1.LogPacketMessage("HouseInformationsForGuild");

            base.Decrypt(reader);

            instanceId = (uint)reader.ReadInt();
            secondHand = reader.ReadBoolean();
            ownerTag = new AccountTagInformation();
            ownerTag.Decrypt(reader);
            worldX = reader.ReadShort();
            worldY = reader.ReadShort();
            mapId = reader.ReadDouble();
            subAreaId = (uint)reader.ReadShort();

            uint skillListIdsLen = (uint)reader.ReadShort();

            for (int i = 0; i < skillListIdsLen; i++)
            {
                int val = reader.ReadInt();
                skillListIds.Add(val);
            }

            guildshareParams = (uint)reader.ReadVarInt();
        }
    }
}
