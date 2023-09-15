using System.Diagnostics;
using MioussBot.Dofus;
using MioussBot.Packets;

namespace MioussBot
{
    public partial class Form1 : Form
    {
        bool hasStarted = false;

        public Form1()
        {
            InitializeComponent();
        }

        private void Start_Click(object sender, EventArgs e)
        {
            hasStarted = !hasStarted;

            if (hasStarted)
            {
                SetRunningButton();

                ProcessFinder.StartFinding();
            }
            else
            {
                SetCancellingButton();

                SocketCom.StopAll();
                ProcessFinder.Stop();

                SetStartButton();
            }

        }

        private void ChangeButton(string text, Color backColor, Color foreColor)
        {
            Start.Text = text;
            Start.BackColor = backColor;
            Start.ForeColor = foreColor;
        }

        private void SetStartButton()
        {
            ChangeButton("START", Color.Green, Color.White);
        }

        private void SetRunningButton()
        {
            ChangeButton("RUNNING", Color.Yellow, Color.Black);
        }
        private void SetCancellingButton()
        {
            ChangeButton("CANCELLING", Color.Orange, Color.Black);
        }

        private void Stop_Click(object sender, EventArgs e)
        {
            SocketCom.StopAll();
            ProcessFinder.Stop();

            Application.Exit();
        }

        private void DebugBox_TextChanged(object sender, EventArgs e)
        {

        }


        static public void LogPacket(string text)
        {
            Log(text);
        }

        static public void LogPacketMessage(string text)
        {
            Log($"\t{text}");
        }

        static public void Log(string text)
        {
            if (DebugBox.InvokeRequired)
            {
                DebugBox.Invoke((MethodInvoker)(() =>
                {
                    DebugBox.AppendText(text + Environment.NewLine);
                }));
            }
            else
            {
                DebugBox.AppendText(text + Environment.NewLine);
            }
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            InteractiveUseRequestMessage.ZaapAstrub();
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            //GameMapMovementRequestMessage.Send(189793284, 25116, 24752);

            List<int> cells = Map.GetCellsIdBetween(81, 60);

            Log($"Cells : {string.Join(", ", cells)}");

            GameMapMovementRequestMessage.Send(189792261, 81, 67, 80, 66, 79, 65, 78, 64, 77, 63, 76, 62, 75, 61, 74, 60);
        }

        private void StartTrajet_Click(object sender, EventArgs e)
        {
            Trajet.StartTrajet();
        }

        private void RecordTrajet_Click(object sender, EventArgs e)
        {
            Trajet.RecordTrajet();
        }

        private void ClearTrajet_Click(object sender, EventArgs e)
        {
            Trajet.ClearTrajet();
        }
    }
}