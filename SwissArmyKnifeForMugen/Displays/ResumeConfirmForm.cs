// Decompiled with JetBrains decompiler
// Type: SwissArmyKnifeForMugen.ResumeConfirmForm
// Assembly: SAKnifeWM, Version=1.0.8.0, Culture=neutral, PublicKeyToken=null
// MVID: 09478AD8-365C-4BF3-BEA1-B5785151259B
// Assembly location: C:\Users\ziddi\Downloads\Swiss Army Knife 1.1 Conversion\Swiss Army Knife 1.1 Conversion\SAKnifeWM.exe

using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace SwissArmyKnifeForMugen.Displays
{
    public class ResumeConfirmForm : Form
    {
        private int _remain = 15;
        private Result _result;
        private IContainer components;
        private TextBox textBox1;
        private Button quitAutoModebutton;
        private Button resumeAutoModebutton;
        private Button skipAutoModeButton;
        private Timer resumeConfirmTimer;
        private TextBox textBox2;
        private CheckBox countCheckBox;

        public ResumeConfirmForm() => InitializeComponent();

        public Result GetResult() => _result;

        private void quitAutoModebutton_Click(object sender, EventArgs e)
        {
            _result = Result.QUIT;
            Close();
        }

        private void resumeAutoModebutton_Click(object sender, EventArgs e)
        {
            _result = Result.RESUME;
            Close();
        }

        private void skipAutoModeButton_Click(object sender, EventArgs e)
        {
            _result = Result.RESUME_NEXT;
            Close();
        }

        private void resumeConfirmTimer_Tick(object sender, EventArgs e)
        {
            if (!countCheckBox.Checked)
                --_remain;
            textBox2.Text = "(Next match will begin in " + _remain + " seconds.";
            if (_remain > 0)
                return;
            _result = Result.RESUME_NEXT;
            Close();
            resumeConfirmTimer.Stop();
        }

        private void ResumeConfirmForm_Shown(object sender, EventArgs e)
        {
            skipAutoModeButton.Focus();
            resumeConfirmTimer.Start();
        }

        private void countCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (!countCheckBox.Checked)
                textBox2.Enabled = true;
            else
                textBox2.Enabled = false;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null)
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            components = new Container();
            ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(ResumeConfirmForm));
            textBox1 = new TextBox();
            quitAutoModebutton = new Button();
            resumeAutoModebutton = new Button();
            skipAutoModeButton = new Button();
            resumeConfirmTimer = new Timer(components);
            textBox2 = new TextBox();
            countCheckBox = new CheckBox();
            SuspendLayout();
            textBox1.BorderStyle = BorderStyle.None;
            textBox1.Cursor = Cursors.Default;
            textBox1.Font = new Font("MS UI Gothic", 12f, FontStyle.Regular, GraphicsUnit.Point, 128);
            textBox1.ImeMode = ImeMode.Disable;
            textBox1.Location = new Point(36, 18);
            textBox1.Multiline = true;
            textBox1.Name = "textBox1";
            textBox1.ReadOnly = true;
            textBox1.Size = new Size(384, 40);
            textBox1.TabIndex = 7;
            textBox1.TabStop = false;
            textBox1.Text = "The match was cancelled. Restart from the next match? \r\n";
            quitAutoModebutton.Location = new Point(316, 99);
            quitAutoModebutton.Name = "quitAutoModebutton";
            quitAutoModebutton.Size = new Size(136, 23);
            quitAutoModebutton.TabIndex = 0;
            quitAutoModebutton.Text = "Stop continous battle";
            quitAutoModebutton.UseVisualStyleBackColor = true;
            quitAutoModebutton.Click += new EventHandler(quitAutoModebutton_Click);
            resumeAutoModebutton.Location = new Point(165, 99);
            resumeAutoModebutton.Name = "resumeAutoModebutton";
            resumeAutoModebutton.Size = new Size(136, 23);
            resumeAutoModebutton.TabIndex = 2;
            resumeAutoModebutton.Text = "Restart from cancelled match";
            resumeAutoModebutton.UseVisualStyleBackColor = true;
            resumeAutoModebutton.Click += new EventHandler(resumeAutoModebutton_Click);
            skipAutoModeButton.Location = new Point(12, 99);
            skipAutoModeButton.Name = "skipAutoModeButton";
            skipAutoModeButton.Size = new Size(136, 23);
            skipAutoModeButton.TabIndex = 1;
            skipAutoModeButton.Text = "Restart from next match";
            skipAutoModeButton.UseVisualStyleBackColor = true;
            skipAutoModeButton.Click += new EventHandler(skipAutoModeButton_Click);
            resumeConfirmTimer.Interval = 1000;
            resumeConfirmTimer.Tick += new EventHandler(resumeConfirmTimer_Tick);
            textBox2.BorderStyle = BorderStyle.None;
            textBox2.Cursor = Cursors.Default;
            textBox2.Font = new Font("MS UI Gothic", 10f, FontStyle.Regular, GraphicsUnit.Point, 128);
            textBox2.ImeMode = ImeMode.Disable;
            textBox2.Location = new Point(59, 45);
            textBox2.Multiline = true;
            textBox2.Name = "textBox2";
            textBox2.ReadOnly = true;
            textBox2.Size = new Size(349, 20);
            textBox2.TabIndex = 8;
            textBox2.TabStop = false;
            textBox2.Text = "(Next match will begin in 15 seconds)";
            countCheckBox.AutoSize = true;
            countCheckBox.Location = new Point(145, 75);
            countCheckBox.Name = "countCheckBox";
            countCheckBox.Size = new Size(179, 16);
            countCheckBox.TabIndex = 9;
            countCheckBox.Text = "Stop the countdown.";
            countCheckBox.UseVisualStyleBackColor = true;
            countCheckBox.CheckedChanged += new EventHandler(countCheckBox_CheckedChanged);
            AutoScaleDimensions = new SizeF(6f, 12f);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(464, 164);
            ControlBox = false;
            Controls.Add(countCheckBox);
            Controls.Add(textBox2);
            Controls.Add(skipAutoModeButton);
            Controls.Add(resumeAutoModebutton);
            Controls.Add(quitAutoModebutton);
            Controls.Add(textBox1);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            Icon = (Icon)componentResourceManager.GetObject("$this.Icon");
            Name = nameof(ResumeConfirmForm);
            ShowIcon = false;
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Swiss Army Knife";
            Shown += new EventHandler(ResumeConfirmForm_Shown);
            ResumeLayout(false);
            PerformLayout();
        }

        public enum Result
        {
            NONE,
            QUIT,
            RESUME,
            RESUME_NEXT,
        }
    }
}
