using System.Diagnostics;

namespace MioussBot.Dofus
{
    internal class ProcessFinder
    {
        static private Thread? finding;
        static private CancellationTokenSource? tokenSource;
        static public void StartFinding()
        {
            tokenSource = new();
            finding = new Thread(() =>
            {
                Form1.Log("Finding Dofus...");

                bool hasFound = false;

                while (!tokenSource.Token.IsCancellationRequested && !hasFound)
                {
                    Process[] processes = Process.GetProcessesByName("Dofus");

                    hasFound = processes.Length != 0;

                    if (hasFound)
                    {
                        Process DofusProcess = processes[0];
                        Form1.Log($"Dofus found at {DofusProcess.Id}");

                        SocketListener.Start();
                    }
                    else
                    {
                        Thread.Sleep(1000);
                    }
                }
            });

            finding.Start();
        }

        static public void StopFinding()
        {
            tokenSource?.Cancel();
            finding?.Join();
        }

        public static void Stop()
        {
            StopFinding();
            tokenSource?.Dispose();
        }
    }
}
