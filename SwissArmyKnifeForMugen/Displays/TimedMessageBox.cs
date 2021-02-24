// Decompiled with JetBrains decompiler
// Type: SwissArmyKnifeForMugen.TimedMessageBox
// Assembly: SAKnifeWM, Version=1.0.8.0, Culture=neutral, PublicKeyToken=null
// MVID: 09478AD8-365C-4BF3-BEA1-B5785151259B
// Assembly location: C:\Users\ziddi\Downloads\Swiss Army Knife 1.1 Conversion\Swiss Army Knife 1.1 Conversion\SAKnifeWM.exe

using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace SwissArmyKnifeForMugen.Displays
{
    /// <summary>
    /// Seems like a message box that pops up after a fixed delay.
    /// <br/>Probably used for the MUGEN frozen box and similar.
    /// </summary>
    public class TimedMessageBox : Form
    {
        private bool _shouldClose;
        private IContainer components;
        private Timer closeTimer;

        private TimedMessageBox() => InitializeComponent();

        public static void Show(string msg, bool autoClose) => new TimedMessageBox()?.ShowMsg(msg, autoClose);

        public static void DummyShow(int timeout) => new TimedMessageBox()?.DummyShowMsg(timeout);

        private void ShowMsg(string msg, bool autoClose)
        {
            Hide();
            closeTimer.Interval = 5000;
            if (autoClose)
            {
                closeTimer.Enabled = true;
                closeTimer.Start();
            }
            int num = (int)MessageBox.Show(this, msg, "Error");
            Close();
        }

        private void DummyShowMsg(int timeout)
        {
            Hide();
            closeTimer.Interval = timeout;
            closeTimer.Enabled = true;
            closeTimer.Start();
            long num = DateTime.Now.Ticks / 10000L;
            for (long index = num; !_shouldClose && index < num + closeTimer.Interval; index = DateTime.Now.Ticks / 10000L)
                Application.DoEvents();
        }

        private void closeTimer_Tick(object sender, EventArgs e)
        {
            closeTimer.Stop();
            _shouldClose = true;
            Close();
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
            closeTimer = new Timer(components);
            SuspendLayout();
            closeTimer.Interval = 5000;
            closeTimer.Tick += new EventHandler(closeTimer_Tick);
            AutoScaleDimensions = new SizeF(6f, 12f);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(172, 16);
            FormBorderStyle = FormBorderStyle.None;
            Location = new Point(-500, -500);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = nameof(TimedMessageBox);
            ShowIcon = false;
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.Manual;
            Text = "dummy";
            ResumeLayout(false);
        }
    }
}
