using Microsoft.VisualBasic.Logging;
using MioussBot.BigEndianRW;
using MioussBot.Dofus.Assets;
using MioussBot.Packets;
using System.Reflection.PortableExecutable;

namespace MioussBot.Packets
{
    internal class PacketDeserialize
    {
        public static BigEndianReader reader;

        static readonly Dictionary<int, Action> packetDictionnary = new();

        static PacketDeserialize()
        {
            AddPacket(3145, CurrentMapMessage);
            AddPacket(5787, StatedElementUpdatedMessage);
            AddPacket(6295, InteractiveUseRequestMessage);
            AddPacket(6639, GameMapMovementRequestMessage);
            AddPacket(302, GameMapMovementConfirmMessage);
            AddPacket(4529, HaapiApiKeyMessage);
            AddPacket(3236, FightTeamMemberCharacterInformations);
            AddPacket(2962, ObjectQuantityMessage);
            AddPacket(9325, ObjectItemAdded);
            AddPacket(8309, BasicAckMessage);
        }

        private static void AddPacket(int id, Action action)
        {
            packetDictionnary[id] = action;
        }
        public static void Handle(int id)
        {
            if (packetDictionnary.ContainsKey(id) && PacketDictionnary.filter.Contains(id))
                packetDictionnary[id]();
        }

        static void ObjectItemAdded()
        {
            uint id = 0;
            uint item = 0;

            uint position = (uint)reader.ReadShort();
            uint objectGID = (uint)reader.ReadVarInt();
            uint effectsLen = (uint)reader.ReadShort();
            List<uint> effects = new();

            for (int i = 0; i < effectsLen; i++)
            {
                id = (uint)reader.ReadVarShort();
                effects.Add((uint)reader.ReadVarShort());
            }

            uint objectUID = (uint)reader.ReadVarInt();
            uint quantity = (uint)reader.ReadVarInt();

            Form1.LogPacketMessage($"Position : {position} | Object GID : {objectGID} | Effects : {string.Join(", ", effects)} | Object UID : {objectUID} | Quantity : {quantity}");

        }
        static void ObjectQuantityMessage()
        {
            uint objectUID = (uint)reader.ReadVarInt();
            uint quantity = (uint)reader.ReadVarInt();
            uint origin = reader.ReadByte();


            Form1.LogPacketMessage($"ObjectUID : {objectUID} | Quantity : {quantity} | Origin : {origin}");
        }
        static void FightTeamMemberCharacterInformations()
        {
            string name = reader.ReadString();
            double id = reader.ReadDouble();
            int level = reader.ReadVarShort();


            Form1.LogPacketMessage($"Name : {name} | Level : {level} | ID : {id}");
        }
        static void HaapiApiKeyMessage()
        {
            string apiKey = reader.ReadString();

            Form1.LogPacketMessage($"ApiKey : {apiKey}");
        }
        static void GameMapMovementRequestMessage()
        {
            uint keyMoveLen = (uint)reader.ReadShort();
            List<uint> keyMoves = new();

            for (int i = 0; i < keyMoveLen; i++)
            {
                keyMoves.Add((uint)reader.ReadShort());
            }

            double mapId = reader.ReadDouble();

            Form1.LogPacketMessage($"KeyMoveLen : {keyMoveLen} | keyMoves: {string.Join(", ", keyMoves)} | MapId : {mapId}");

        }


        static void CurrentMapMessage()
        {
            double mapId = reader.ReadDouble();

            Form1.LogPacketMessage($"MapId : {mapId}");
        }

        static void InteractiveUseRequestMessage()
        {
            uint elemId = (uint)reader.ReadVarInt();
            uint skillInstanceId = (uint)reader.ReadVarInt();

            Form1.LogPacketMessage($"Element ID : {elemId} | Skill Instance ID : {skillInstanceId}");

        }

        static void StatedElementUpdatedMessage()
        {
            uint elementId = (uint)reader.ReadInt();
            uint elementCellId = (uint)reader.ReadVarShort();
            uint elementState = (uint)reader.ReadVarInt();
            bool onCurrentMap = reader.ReadBoolean();

            Form1.LogPacketMessage($"Element ID : {elementId} | Element Cell ID : {elementCellId} | Element State : {elementState} | On Current Map : {onCurrentMap}");
        }

        static void GameMapMovementConfirmMessage()
        {
            Form1.LogPacketMessage("Movement done");

            if(PacketSerializeAndSend.trajets.Any())
                PacketSerializeAndSend.hasDoneMoved = true;
        }

        static void BasicAckMessage()
        {
            Form1.LogPacketMessage("Ack");
            if(PacketSerializeAndSend.hasDoneMoved) 
                PacketSerializeAndSend.hasServerResponse = true;
        }


    }
}
