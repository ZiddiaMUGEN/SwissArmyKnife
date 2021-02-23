// Decompiled with JetBrains decompiler
// Type: SwissArmyKnifeForMugen.TimedMessageBox
// Assembly: SAKnifeWM, Version=1.0.8.0, Culture=neutral, PublicKeyToken=null
// MVID: 09478AD8-365C-4BF3-BEA1-B5785151259B
// Assembly location: C:\Users\ziddi\Downloads\Swiss Army Knife 1.1 Conversion\Swiss Army Knife 1.1 Conversion\SAKnifeWM.exe

using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace SwissArmyKnifeForMugen
{
  public class TimedMessageBox : Form
  {
    private bool _shouldClose;
    private IContainer components;
    private Timer closeTimer;

    private TimedMessageBox() => this.InitializeComponent();

    public static void Show(string msg, bool autoClose) => new TimedMessageBox()?.ShowMsg(msg, autoClose);

    public static void DummyShow(int timeout) => new TimedMessageBox()?.DummyShowMsg(timeout);

    private void ShowMsg(string msg, bool autoClose)
    {
      this.Hide();
      this.closeTimer.Interval = 5000;
      if (autoClose)
      {
        this.closeTimer.Enabled = true;
        this.closeTimer.Start();
      }
      int num = (int) MessageBox.Show((IWin32Window) this, msg, "Error");
      this.Close();
    }

    private void DummyShowMsg(int timeout)
    {
      this.Hide();
      this.closeTimer.Interval = timeout;
      this.closeTimer.Enabled = true;
      this.closeTimer.Start();
      long num = DateTime.Now.Ticks / 10000L;
      for (long index = num; !this._shouldClose && index < num + (long) this.closeTimer.Interval; index = DateTime.Now.Ticks / 10000L)
        Application.DoEvents();
    }

    private void closeTimer_Tick(object sender, EventArgs e)
    {
      this.closeTimer.Stop();
      this._shouldClose = true;
      this.Close();
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      this.components = (IContainer) new Container();
      this.closeTimer = new Timer(this.components);
      this.SuspendLayout();
      this.closeTimer.Interval = 5000;
      this.closeTimer.Tick += new EventHandler(this.closeTimer_Tick);
      this.AutoScaleDimensions = new SizeF(6f, 12f);
      this.AutoScaleMode = AutoScaleMode.Font;
      this.ClientSize = new Size(172, 16);
      this.FormBorderStyle = FormBorderStyle.None;
      this.Location = new Point(-500, -500);
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = nameof (TimedMessageBox);
      this.ShowIcon = false;
      this.ShowInTaskbar = false;
      this.StartPosition = FormStartPosition.Manual;
      this.Text = "dummy";
      this.ResumeLayout(false);
    }
  }
}
