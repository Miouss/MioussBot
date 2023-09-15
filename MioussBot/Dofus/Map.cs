namespace MioussBot
{
    internal class Map
    {
        private static readonly int MAP_GRID_WIDTH = 14;
        public static Coord getCoordByCellId(int cellId)
        {
            int loc2 = (int)Math.Floor((double)cellId / 14);
            int loc3 = (int)Math.Floor((double)(loc2 + 1) / 2);
            int loc4 = loc2 - loc3;
            int loc5 = (int)(cellId - loc2 * 14);

            int x = loc3 + loc5;
            int y = loc5 - loc4;
            
            return new Coord { x = x, y = y };
        }

        public static double GetCellIdByCoord(double x, double y)
        {
            return Math.Floor((x - y) * 14 + y + (x - y) / 2);
        }

        public static int GetDistance(int cellId1, int cellId2)
        {
            int loc3 = cellId1 / MAP_GRID_WIDTH;
            int loc4 = (loc3 + 1) / 2;
            int loc5 = cellId1 - loc3 * MAP_GRID_WIDTH;
            int loc6 = loc4 + loc5;

            int loc7 = cellId1 / MAP_GRID_WIDTH;
            int loc8 = (loc7 + 1) / 2;
            int loc9 = loc7 - loc8;
            int loc10 = cellId1 - loc7 * MAP_GRID_WIDTH;
            int loc11 = loc10 - loc9;

            int loc12 = cellId2 / MAP_GRID_WIDTH;
            int loc13 = (loc12 + 1) / 2;
            int loc14 = cellId2 - loc12 * MAP_GRID_WIDTH;
            int loc15 = loc13 + loc14;

            int loc16 = cellId2 / MAP_GRID_WIDTH;
            int loc17 = (loc16 + 1) / 2;
            int loc18 = loc16 - loc17;
            int loc19 = cellId2 - loc16 * MAP_GRID_WIDTH;
            int loc20 = loc19 - loc18;

            return (int)Math.Floor((double)(Math.Abs(loc15 - loc6) + Math.Abs(loc20 - loc11)));
        }

        public static List<int> GetCellsIdBetween(int cellId1, int cellId2)
        {
            int loc3 = cellId1 / MAP_GRID_WIDTH;
            int loc4 = (loc3 + 1) / 2;
            int loc5 = cellId1 - loc3 * MAP_GRID_WIDTH;
            int loc6 = loc4 + loc5;

            int loc7 = cellId1 / MAP_GRID_WIDTH;
            int loc8 = (loc7 + 1) / 2;
            int loc9 = loc7 - loc8;
            int loc10 = cellId1 - loc7 * MAP_GRID_WIDTH;
            int loc11 = loc10 - loc9;

            int loc12 = cellId2 / MAP_GRID_WIDTH;
            int loc13 = (loc12 + 1) / 2;
            int loc14 = cellId2 - loc12 * MAP_GRID_WIDTH;
            int loc15 = loc13 + loc14;

            int loc16 = cellId2 / MAP_GRID_WIDTH;
            int loc17 = (loc16 + 1) / 2;
            int loc18 = loc16 - loc17;
            int loc19 = cellId2 - loc16 * MAP_GRID_WIDTH;
            int loc20 = loc19 - loc18;

            int loc21 = loc15 - loc6;
            int loc22 = loc20 - loc11;

            double loc23 = Math.Sqrt(loc21 * loc21 + loc22 * loc22);
            double loc24 = loc21 / loc23;
            double loc25 = loc22 / loc23;

            double loc26 = Math.Abs(1 / loc24);
            double loc27 = Math.Abs(1 / loc25);

            int loc28 = loc24 < 0 ? -1 : 1;
            int loc29 = loc25 < 0 ? -1 : 1;

            double loc30 = 0.5 * loc26;
            double loc31 = 0.5 * loc27;

            List<int> loc32 = new ();

            while (loc6 != loc15 || loc11 != loc20)
            {
                if (Math.Abs(loc30 - loc31) < 0.0001)
                {
                    loc30 += loc26;
                    loc31 += loc27;
                    loc6 += loc28;
                    loc11 += loc29;
                }
                else if (loc30 < loc31)
                {
                    loc30 += loc26;
                    loc6 += loc28;
                }
                else
                {
                    loc31 += loc27;
                    loc11 += loc29;
                }

                loc32.Add((int)GetCellIdByCoord(loc6, loc11));
            }

            return loc32;
        }

    }

    struct Coord
    {
        public int x;
        public int y;
    }
}
