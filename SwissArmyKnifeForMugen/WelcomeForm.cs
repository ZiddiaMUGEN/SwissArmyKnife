// Decompiled with JetBrains decompiler
// Type: SwissArmyKnifeForMugen.WelcomeForm
// Assembly: SAKnifeWM, Version=1.0.8.0, Culture=neutral, PublicKeyToken=null
// MVID: 09478AD8-365C-4BF3-BEA1-B5785151259B
// Assembly location: C:\Users\ziddi\Downloads\Swiss Army Knife 1.1 Conversion\Swiss Army Knife 1.1 Conversion\SAKnifeWM.exe

using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace SwissArmyKnifeForMugen
{
  public class WelcomeForm : Form
  {
    private bool _clickedOK;
    private IContainer components;
    private Button cancelButton;
    private Button okButton;
    private TextBox textBox1;

    public WelcomeForm() => this.InitializeComponent();

    public bool DidClickOK() => this._clickedOK;

    private void okButton_Click(object sender, EventArgs e)
    {
      this._clickedOK = true;
      this.Close();
    }

    private void cancelButton_Click(object sender, EventArgs e)
    {
      this._clickedOK = false;
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
      ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (WelcomeForm));
      this.cancelButton = new Button();
      this.okButton = new Button();
      this.textBox1 = new TextBox();
      this.SuspendLayout();
      this.cancelButton.Location = new Point(394, 200);
      this.cancelButton.Name = "cancelButton";
      this.cancelButton.Size = new Size(90, 28);
      this.cancelButton.TabIndex = 3;
      this.cancelButton.Text = "Cancel";
      this.cancelButton.UseVisualStyleBackColor = true;
      this.cancelButton.Click += new EventHandler(this.cancelButton_Click);
      this.okButton.Location = new Point(298, 200);
      this.okButton.Name = "okButton";
      this.okButton.Size = new Size(90, 28);
      this.okButton.TabIndex = 2;
      this.okButton.Text = "OK";
      this.okButton.UseVisualStyleBackColor = true;
      this.okButton.Click += new EventHandler(this.okButton_Click);
      this.textBox1.BorderStyle = BorderStyle.None;
      this.textBox1.Cursor = Cursors.Default;
      this.textBox1.Font = new Font("MS UI Gothic", 12f, FontStyle.Regular, GraphicsUnit.Point, (byte) 128);
      this.textBox1.ImeMode = ImeMode.Disable;
      this.textBox1.Location = new Point(12, 12);
      this.textBox1.Multiline = true;
      this.textBox1.Name = "textBox1";
      this.textBox1.ReadOnly = true;
      this.textBox1.Size = new Size(472, 180);
      this.textBox1.TabIndex = 5;
      this.textBox1.TabStop = false;
      this.textBox1.Text = componentResourceManager.GetString("textBox1.Text");
      this.AutoScaleDimensions = new SizeF(6f, 12f);
      this.AutoScaleMode = AutoScaleMode.Font;
      this.ClientSize = new Size(496, 240);
      this.Controls.Add((Control) this.textBox1);
      this.Controls.Add((Control) this.cancelButton);
      this.Controls.Add((Control) this.okButton);
      this.FormBorderStyle = FormBorderStyle.FixedDialog;
      this.Icon = (Icon) componentResourceManager.GetObject("$this.Icon");
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = nameof (WelcomeForm);
      this.ShowIcon = false;
      this.StartPosition = FormStartPosition.CenterScreen;
      this.Text = "Welcome!";
      this.ResumeLayout(false);
      this.PerformLayout();
    }
  }
}
