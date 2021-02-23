// Decompiled with JetBrains decompiler
// Type: SwissArmyKnifeForMugen.ResumeConfirmForm
// Assembly: SAKnifeWM, Version=1.0.8.0, Culture=neutral, PublicKeyToken=null
// MVID: 09478AD8-365C-4BF3-BEA1-B5785151259B
// Assembly location: C:\Users\ziddi\Downloads\Swiss Army Knife 1.1 Conversion\Swiss Army Knife 1.1 Conversion\SAKnifeWM.exe

using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace SwissArmyKnifeForMugen
{
  public class ResumeConfirmForm : Form
  {
    private int _remain = 15;
    private ResumeConfirmForm.Result _result;
    private IContainer components;
    private TextBox textBox1;
    private Button quitAutoModebutton;
    private Button resumeAutoModebutton;
    private Button skipAutoModeButton;
    private Timer resumeConfirmTimer;
    private TextBox textBox2;
    private CheckBox countCheckBox;

    public ResumeConfirmForm() => this.InitializeComponent();

    public ResumeConfirmForm.Result GetResult() => this._result;

    private void quitAutoModebutton_Click(object sender, EventArgs e)
    {
      this._result = ResumeConfirmForm.Result.QUIT;
      this.Close();
    }

    private void resumeAutoModebutton_Click(object sender, EventArgs e)
    {
      this._result = ResumeConfirmForm.Result.RESUME;
      this.Close();
    }

    private void skipAutoModeButton_Click(object sender, EventArgs e)
    {
      this._result = ResumeConfirmForm.Result.RESUME_NEXT;
      this.Close();
    }

    private void resumeConfirmTimer_Tick(object sender, EventArgs e)
    {
      if (!this.countCheckBox.Checked)
        --this._remain;
      this.textBox2.Text = "(Next match will begin in " + (object) this._remain + " seconds.";
      if (this._remain > 0)
        return;
      this._result = ResumeConfirmForm.Result.RESUME_NEXT;
      this.Close();
      this.resumeConfirmTimer.Stop();
    }

    private void ResumeConfirmForm_Shown(object sender, EventArgs e)
    {
      this.skipAutoModeButton.Focus();
      this.resumeConfirmTimer.Start();
    }

    private void countCheckBox_CheckedChanged(object sender, EventArgs e)
    {
      if (!this.countCheckBox.Checked)
        this.textBox2.Enabled = true;
      else
        this.textBox2.Enabled = false;
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
      ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (ResumeConfirmForm));
      this.textBox1 = new TextBox();
      this.quitAutoModebutton = new Button();
      this.resumeAutoModebutton = new Button();
      this.skipAutoModeButton = new Button();
      this.resumeConfirmTimer = new Timer(this.components);
      this.textBox2 = new TextBox();
      this.countCheckBox = new CheckBox();
      this.SuspendLayout();
      this.textBox1.BorderStyle = BorderStyle.None;
      this.textBox1.Cursor = Cursors.Default;
      this.textBox1.Font = new Font("MS UI Gothic", 12f, FontStyle.Regular, GraphicsUnit.Point, (byte) 128);
      this.textBox1.ImeMode = ImeMode.Disable;
      this.textBox1.Location = new Point(36, 18);
      this.textBox1.Multiline = true;
      this.textBox1.Name = "textBox1";
      this.textBox1.ReadOnly = true;
      this.textBox1.Size = new Size(384, 40);
      this.textBox1.TabIndex = 7;
      this.textBox1.TabStop = false;
      this.textBox1.Text = "The match was cancelled. Restart from the next match? \r\n";
      this.quitAutoModebutton.Location = new Point(316, 99);
      this.quitAutoModebutton.Name = "quitAutoModebutton";
      this.quitAutoModebutton.Size = new Size(136, 23);
      this.quitAutoModebutton.TabIndex = 0;
      this.quitAutoModebutton.Text = "Stop continous battle";
      this.quitAutoModebutton.UseVisualStyleBackColor = true;
      this.quitAutoModebutton.Click += new EventHandler(this.quitAutoModebutton_Click);
      this.resumeAutoModebutton.Location = new Point(165, 99);
      this.resumeAutoModebutton.Name = "resumeAutoModebutton";
      this.resumeAutoModebutton.Size = new Size(136, 23);
      this.resumeAutoModebutton.TabIndex = 2;
      this.resumeAutoModebutton.Text = "Restart from cancelled match";
      this.resumeAutoModebutton.UseVisualStyleBackColor = true;
      this.resumeAutoModebutton.Click += new EventHandler(this.resumeAutoModebutton_Click);
      this.skipAutoModeButton.Location = new Point(12, 99);
      this.skipAutoModeButton.Name = "skipAutoModeButton";
      this.skipAutoModeButton.Size = new Size(136, 23);
      this.skipAutoModeButton.TabIndex = 1;
      this.skipAutoModeButton.Text = "Restart from next match";
      this.skipAutoModeButton.UseVisualStyleBackColor = true;
      this.skipAutoModeButton.Click += new EventHandler(this.skipAutoModeButton_Click);
      this.resumeConfirmTimer.Interval = 1000;
      this.resumeConfirmTimer.Tick += new EventHandler(this.resumeConfirmTimer_Tick);
      this.textBox2.BorderStyle = BorderStyle.None;
      this.textBox2.Cursor = Cursors.Default;
      this.textBox2.Font = new Font("MS UI Gothic", 10f, FontStyle.Regular, GraphicsUnit.Point, (byte) 128);
      this.textBox2.ImeMode = ImeMode.Disable;
      this.textBox2.Location = new Point(59, 45);
      this.textBox2.Multiline = true;
      this.textBox2.Name = "textBox2";
      this.textBox2.ReadOnly = true;
      this.textBox2.Size = new Size(349, 20);
      this.textBox2.TabIndex = 8;
      this.textBox2.TabStop = false;
      this.textBox2.Text = "(Next match will begin in 15 seconds)";
      this.countCheckBox.AutoSize = true;
      this.countCheckBox.Location = new Point(145, 75);
      this.countCheckBox.Name = "countCheckBox";
      this.countCheckBox.Size = new Size(179, 16);
      this.countCheckBox.TabIndex = 9;
      this.countCheckBox.Text = "Stop the countdown.";
      this.countCheckBox.UseVisualStyleBackColor = true;
      this.countCheckBox.CheckedChanged += new EventHandler(this.countCheckBox_CheckedChanged);
      this.AutoScaleDimensions = new SizeF(6f, 12f);
      this.AutoScaleMode = AutoScaleMode.Font;
      this.ClientSize = new Size(464, 164);
      this.ControlBox = false;
      this.Controls.Add((Control) this.countCheckBox);
      this.Controls.Add((Control) this.textBox2);
      this.Controls.Add((Control) this.skipAutoModeButton);
      this.Controls.Add((Control) this.resumeAutoModebutton);
      this.Controls.Add((Control) this.quitAutoModebutton);
      this.Controls.Add((Control) this.textBox1);
      this.FormBorderStyle = FormBorderStyle.FixedDialog;
      this.Icon = (Icon) componentResourceManager.GetObject("$this.Icon");
      this.Name = nameof (ResumeConfirmForm);
      this.ShowIcon = false;
      this.StartPosition = FormStartPosition.CenterScreen;
      this.Text = "Swiss Army Knife";
      this.Shown += new EventHandler(this.ResumeConfirmForm_Shown);
      this.ResumeLayout(false);
      this.PerformLayout();
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
