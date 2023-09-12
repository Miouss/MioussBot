using System.Diagnostics;

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

                DofusProcess.StartFinding();
            }
            else
            {
                SetCancellingButton();

                SocketListener.StopFiltering();
                DofusProcess.StopFinding();

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
            SocketListener.Stop();
            DofusProcess.Stop();

            Application.Exit();
        }

        private void DebugBox_TextChanged(object sender, EventArgs e)
        {

        }

        static public void AddText(string text)
        {
            DebugBox.AppendText(text + Environment.NewLine);
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            PacketSender.ZaapAstrub();
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            PacketSender.ZaapTainela();
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            PacketSender.OrtieTainela();
        }
    }
}