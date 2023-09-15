namespace MioussBot.Packets
{
    internal class Trajet
    {
        static public bool IsRecording = false;
        static public bool IsUsingTrajet = false;

        static public bool IsMoving = false;
        static public bool IsInteracting = false;

        static public List<object> trajets = new();

        static public void AddCollectAction(uint id, uint cellId)
        {
            trajets.Add(new CollectAction(id, cellId));
        }

        static public void AddMapMove(int mapId, uint[] keyMoves)
        {
            trajets.Add(new MapMove(mapId, keyMoves));
        }

        static public void StartTrajet()
        {
            new Task(async () =>
            {
                IsUsingTrajet = true;
                Form1.Log("Trajet started");
                int i = 0;

                while (trajets.Any())
                {
                    Form1.Log($"Moving : {IsMoving} | Interacting : {IsInteracting}");

                    if (!IsMoving && !IsInteracting)
                    {
                        var trajet = trajets[i];

                        if (trajet is MapMove)
                        {
                            MapMove move = (MapMove)trajet;
                            IsMoving = true;
                            GameMapMovementRequestMessage.Send(move.mapId, move.keyMoves);
                            i++;
                        }
                        else
                        {
                            CollectAction collect = (CollectAction)trajet;
                            IsInteracting = true;
                            InteractiveUseRequestMessage.Send((int)collect.id, (int)collect.cellId);

                            i++;
                        }

                        
                        if(i == trajets.Count)
                        {
                            Form1.Log("Trajet done, looping");

                            i = 0;
                        }
                    }

                    await Task.Delay(2000);
                }

                IsUsingTrajet = false;
                Form1.Log("Trajet done");
            }).Start();

        }

        static public void RecordTrajet()
        {
            IsRecording = !IsRecording;
            if(IsRecording)
                Form1.Log("Trajet recording");
            else
                Form1.Log("Trajet recording stopped");
        }
        static public void ClearTrajet()
        {
            trajets.Clear();
        }

    }

    struct CollectAction
    {
        public uint id;
        public uint cellId;

        public CollectAction(uint id, uint cellId)
        {
            this.id = id;
            this.cellId = cellId;
        }
    }

    struct MapMove
    {
        public int mapId;
        public short[] keyMoves;

        public MapMove(int mapId, uint[] keyMoves)
        {
            this.mapId = mapId;
            this.keyMoves = keyMoves.Select(x => (short)x).ToArray();
        }
    }

}
