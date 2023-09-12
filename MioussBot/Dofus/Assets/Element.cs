namespace MioussBot.Dofus.Assets
{
    readonly struct Element
    {
        public readonly int id;
        public readonly int skillInstanceId;

        public Element(int id, int skillInstanceId)
        {
            this.id = id;
            this.skillInstanceId = skillInstanceId;
        }
    }
}
