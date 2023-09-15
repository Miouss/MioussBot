using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MioussBot.Packets.List
{
    internal class AccountTagInformation
    {
        static public readonly short id = 1732;
        static public readonly string name = "AccountTagInformation";

        static public readonly bool isFiltered = false;
        static public readonly bool isIgnored = false;

        public string nickname = "";
        public string tagNumber = "";

        public void Decrypt(BigEndianReader reader)
        {
            Form1.LogPacketMessage("AccountTagInformation");

            nickname = reader.ReadString();
            tagNumber = reader.ReadString();
        }
    }
}
