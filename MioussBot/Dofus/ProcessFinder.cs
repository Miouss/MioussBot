using System.Diagnostics;


namespace MioussBot.Dofus
{
    internal class ProcessFinder
    {
        static Task? finding;
        static CancellationTokenSource? cts;
        static bool isRunning = false;
        static bool hasFound = false;

        static public void StartFinding()
        {
            cts = new();
            isRunning = true;

            finding = new Task(() =>
            {
                try
                {
                    Form1.Log("Finding Dofus...");

                    hasFound = false;

                    while (!hasFound && !cts.Token.IsCancellationRequested)
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
                            Task.Delay(1000, cts.Token).Wait();
                        }
                    }
                }
                catch (Exception e)
                {
                    if (!cts.Token.IsCancellationRequested)
                        Form1.Log(e.ToString());
                    else
                        Form1.Log("Stop finding Dofus");
                }
            });

            finding.Start();
        }

        static public void Stop()
        {
            if (isRunning && !hasFound)
            {
                cts?.Cancel();
                finding?.Wait();
                cts?.Dispose();

                isRunning = false;
            }
        }
    }
}
