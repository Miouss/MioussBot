using System.Xml.Linq;

namespace MioussBot.Packets.List
{
    internal class GameMapMovementConfirmMessage
    {
        static public readonly short id = 302;
        static public readonly string name = "GameMapMovementConfirmMessage";

        static public readonly bool isFiltered = false;
        static public readonly bool isIgnored = false;

        static public void Decrypt()
        {
            Form1.LogPacketMessage("Movement done");

            if (Trajet.IsUsingTrajet)
                Trajet.IsMoving = false;
        }
    }
}
