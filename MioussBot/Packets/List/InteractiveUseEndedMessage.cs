using MioussBot.Dofus.Assets;
using System.Xml.Linq;

namespace MioussBot.Packets.List
{
    internal class InteractiveUseEndedMessage
    {
        static public readonly short id = 5459;
        static public readonly string name = "InteractiveUseEndedMessage";

        static public readonly bool isFiltered = false;
        static public readonly bool isIgnored = false;

        static public void Decrypt()
        {
            Form1.LogPacketMessage("Collect done");

            if (Trajet.IsUsingTrajet)
                Trajet.IsInteracting = false;
        }
    }
}
