using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace MioussBot.Packets.List
{
    internal class StatedElement
    {
        static public readonly short id = 8253;
        static public readonly string name = "StatedElement";

        static public readonly bool isFiltered = false;
        static public readonly bool isIgnored = false;

        public uint elementId = 0;
        public uint elementCellId = 0;
        public uint elementState = 0;
        public bool onCurrentMap = false;

        public void Decrypt(BigEndianReader reader)
        {
            elementId = (uint)reader.ReadInt();
            elementCellId = (uint)reader.ReadVarShort();
            elementState = (uint)reader.ReadVarInt();
            onCurrentMap = reader.ReadBoolean();
        }
    }
    
}
