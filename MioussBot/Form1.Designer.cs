namespace MioussBot
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            Start = new Button();
            Stop = new Button();
            DebugBox = new TextBox();
            button1 = new Button();
            button2 = new Button();
            button3 = new Button();
            Test = new Button();
            SuspendLayout();
            // 
            // Start
            // 
            Start.BackColor = Color.Green;
            Start.ForeColor = Color.White;
            Start.Location = new Point(2, 354);
            Start.Name = "Start";
            Start.Size = new Size(393, 74);
            Start.TabIndex = 0;
            Start.Text = "START";
            Start.UseVisualStyleBackColor = false;
            Start.Click += Start_Click;
            // 
            // Stop
            // 
            Stop.BackColor = Color.Red;
            Stop.ForeColor = Color.White;
            Stop.Location = new Point(413, 354);
            Stop.Name = "Stop";
            Stop.Size = new Size(390, 74);
            Stop.TabIndex = 1;
            Stop.Text = "STOP";
            Stop.UseVisualStyleBackColor = false;
            Stop.Click += Stop_Click;
            // 
            // DebugBox
            // 
            DebugBox.Location = new Point(2, 0);
            DebugBox.Multiline = true;
            DebugBox.Name = "DebugBox";
            DebugBox.ScrollBars = ScrollBars.Vertical;
            DebugBox.Size = new Size(801, 262);
            DebugBox.TabIndex = 2;
            DebugBox.TextChanged += DebugBox_TextChanged;
            // 
            // button1
            // 
            button1.Location = new Point(12, 268);
            button1.Name = "button1";
            button1.Size = new Size(130, 34);
            button1.TabIndex = 3;
            button1.Text = "Interact Zaap Astrub";
            button1.UseVisualStyleBackColor = true;
            button1.Click += Button1_Click;
            // 
            // button2
            // 
            button2.Location = new Point(12, 308);
            button2.Name = "button2";
            button2.Size = new Size(130, 40);
            button2.TabIndex = 4;
            button2.Text = "Interact Zaap Tainela";
            button2.UseVisualStyleBackColor = true;
            button2.Click += Button2_Click;
            // 
            // button3
            // 
            button3.Location = new Point(181, 283);
            button3.Name = "button3";
            button3.Size = new Size(167, 53);
            button3.TabIndex = 5;
            button3.Text = "Collect Ortie";
            button3.UseVisualStyleBackColor = true;
            button3.Click += Button3_Click;
            // 
            // Test
            // 
            Test.Location = new Point(489, 283);
            Test.Name = "Test";
            Test.Size = new Size(247, 56);
            Test.TabIndex = 6;
            Test.Text = "TEST PACKET";
            Test.UseVisualStyleBackColor = true;
            Test.Click += Test_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(Test);
            Controls.Add(button3);
            Controls.Add(button2);
            Controls.Add(button1);
            Controls.Add(DebugBox);
            Controls.Add(Stop);
            Controls.Add(Start);
            Name = "Form1";
            Text = "Form1";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button button1;
        public Button Start;
        public Button Stop;
        private Button button2;
        private Button button3;
        private Button Test;
        static public TextBox DebugBox;
    }
}