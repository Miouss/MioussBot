namespace MioussBot
{
    internal struct Zaap
    {
        public static readonly Element Tainela = new(517166, 156623432);
        public static readonly Element Astrub = new(516107, 156634799);
    }

    internal struct Ressources
    {
        public static readonly Element Ortie = new(495687, 156623121);
    }

    readonly struct Element
    {
        public readonly int id;
        public readonly int cellId;

        public Element(int id, int cellId)
        {
            this.id = id;
            this.cellId = cellId;
        }
    }

}
