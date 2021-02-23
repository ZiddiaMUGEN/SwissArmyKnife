// Decompiled with JetBrains decompiler
// Type: SwissArmyKnifeForMugen.NavigatorForm
// Assembly: SAKnifeWM, Version=1.0.8.0, Culture=neutral, PublicKeyToken=null
// MVID: 09478AD8-365C-4BF3-BEA1-B5785151259B
// Assembly location: C:\Users\ziddi\Downloads\Swiss Army Knife 1.1 Conversion\Swiss Army Knife 1.1 Conversion\SAKnifeWM.exe

using SwissArmyKnifeForMugen.Properties;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace SwissArmyKnifeForMugen
{
  public class NavigatorForm : Form
  {
    private int locX;
    private int locY;
    private Form _owner;
    private IContainer components;
    private TextBox textBox1;
    private PictureBox pictureBox1;
    private Button okButton;

    public NavigatorForm(Form owner)
    {
      this.InitializeComponent();
      this._owner = owner;
    }

    public void SetLoc(int x, int y)
    {
      this.locX = x;
      this.locY = y;
    }

    private void okButton_Click(object sender, EventArgs e)
    {
      this.Close();
      if (this._owner == null)
        return;
      this._owner.Activate();
    }

    private void NavigatorForm_Load(object sender, EventArgs e) => this.SetDesktopLocation(this.locX, this.locY);

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (NavigatorForm));
      this.textBox1 = new TextBox();
      this.okButton = new Button();
      this.pictureBox1 = new PictureBox();
      ((ISupportInitialize) this.pictureBox1).BeginInit();
      this.SuspendLayout();
      this.textBox1.BorderStyle = BorderStyle.None;
      this.textBox1.Cursor = Cursors.Default;
      this.textBox1.Font = new Font("MS UI Gothic", 11f, FontStyle.Regular, GraphicsUnit.Point, (byte) 128);
      this.textBox1.ImeMode = ImeMode.Disable;
      this.textBox1.Location = new Point(145, 13);
      this.textBox1.Multiline = true;
      this.textBox1.Name = "textBox1";
      this.textBox1.ReadOnly = true;
      this.textBox1.Size = new Size(330, 175);
      this.textBox1.TabIndex = 6;
      this.textBox1.TabStop = false;
      this.textBox1.Text = "Next, make sure to register some profiles to be used for running mugen (it is possible to register up to 8 profiles.)\r\n\r\n You can create a new profile by double clicking one of the 'New Profile' items in the window to the left. \r\n\r\n Feel free to click the Help button for more information.";
      this.okButton.Location = new Point(343, 194);
      this.okButton.Name = "okButton";
      this.okButton.Size = new Size(116, 28);
      this.okButton.TabIndex = 8;
      this.okButton.Text = "OK";
      this.okButton.UseVisualStyleBackColor = true;
      this.okButton.Click += new EventHandler(this.okButton_Click);
      this.pictureBox1.BackgroundImageLayout = ImageLayout.None;
      this.pictureBox1.Image = (Image) Resources.arror1;
      this.pictureBox1.InitialImage = (Image) null;
      this.pictureBox1.Location = new Point(8, 36);
      this.pictureBox1.Name = "pictureBox1";
      this.pictureBox1.Size = new Size(130, 88);
      this.pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
      this.pictureBox1.TabIndex = 7;
      this.pictureBox1.TabStop = false;
      this.AutoScaleDimensions = new SizeF(6f, 12f);
      this.AutoScaleMode = AutoScaleMode.Font;
      this.ClientSize = new Size(490, 234);
      this.ControlBox = false;
      this.Controls.Add((Control) this.okButton);
      this.Controls.Add((Control) this.pictureBox1);
      this.Controls.Add((Control) this.textBox1);
      this.FormBorderStyle = FormBorderStyle.None;
      this.Icon = (Icon) componentResourceManager.GetObject("$this.Icon");
      this.Name = nameof (NavigatorForm);
      this.Opacity = 0.9;
      this.StartPosition = FormStartPosition.Manual;
      this.Text = "Information";
      this.Load += new EventHandler(this.NavigatorForm_Load);
      ((ISupportInitialize) this.pictureBox1).EndInit();
      this.ResumeLayout(false);
      this.PerformLayout();
    }
  }
}
