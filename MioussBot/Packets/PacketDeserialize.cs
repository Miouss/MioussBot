using MioussBot.Packets;
using MioussBot.Packets.List;
using System.Reflection;

namespace MioussBot
{
    internal class PacketDeserialize
    {
        public static BigEndianReader reader;

        static public readonly Dictionary<int, Action> packetDictionnary = new();

        static PacketDeserialize()
        {
            AddAsDeserialize(typeof(CurrentMapMessage));
            AddAsDeserialize(typeof(StatedElementUpdatedMessage));
            AddAsDeserialize(typeof(InteractiveUseRequestMessage));
            AddAsDeserialize(typeof(GameMapMovementRequestMessage));
            AddAsDeserialize(typeof(ObjectQuantityMessage));
            AddAsDeserialize(typeof(InteractiveUseEndedMessage));
            AddAsDeserialize(typeof(InteractiveUseErrorMessage));
            AddAsDeserialize(typeof(StatedMapUpdateMessage));
            AddAsDeserialize(typeof(MapComplementaryInformationsDataMessage));
            AddAsDeserialize(typeof(GameMapMovementConfirmMessage));
            AddAsDeserialize(typeof(ObjectItemAdded));

        }
        public static void Handle(int id)
        {
            if (packetDictionnary.ContainsKey(id) && PacketDictionnary.filter.Contains(id))
                packetDictionnary[id]();
        }

        private static void AddAsDeserialize(Type packetEntity)
        {
            try
            {

                MethodInfo decrypt = packetEntity.GetMethod("Decrypt", BindingFlags.Static | BindingFlags.Public);

                short id = (short)packetEntity.GetField("id").GetValue(packetEntity);
                string name = (string)packetEntity.GetField("name").GetValue(packetEntity);

                bool hasParam = decrypt.GetParameters().Length > 0;

                if (hasParam)
                    packetDictionnary[id] = () => decrypt.Invoke(null, new object[] { reader });
                else
                    packetDictionnary[id] = () => decrypt.Invoke(null, null);
            }
            catch (Exception e)
            {
                Form1.LogPacketMessage($"Error while adding {packetEntity.Name} to the deserialize dictionnary : {e.Message}");
            }
        }
    }
}