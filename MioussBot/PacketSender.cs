namespace MioussBot
{
    internal class PacketSender
    {
        static public void ZaapAstrub()
        {
            InteractUseRequest(Zaap.Astrub.id, Zaap.Astrub.cellId);

        }

        static public void ZaapTainela()
        {
            InteractUseRequest(Zaap.Tainela.id, Zaap.Tainela.cellId);
        }

        static public void OrtieTainela()
        {
            InteractUseRequest(Ressources.Ortie.id, Ressources.Ortie.cellId);
        }
        static private void InteractUseRequest(int elemId, int cellId)
        {
            BigEndianWriter writer = new();

            writer.WriteVarInt(elemId);
            writer.WriteVarInt(cellId);
            writer.Pack(6295, 11);
            writer.SendPacket();
        }
    }
}
