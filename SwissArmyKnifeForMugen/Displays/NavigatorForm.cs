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

namespace SwissArmyKnifeForMugen.Displays
{
    /// <summary>
    /// Form to help with navigating the application.
    /// </summary>
    public class NavigatorForm : Form
    {
        private int locX;
        private int locY;
        private Form _owner;
        private TextBox textBox1;
        private PictureBox pictureBox1;
        private Button okButton;

        public NavigatorForm(Form owner)
        {
            InitializeComponent();
            _owner = owner;
        }

        public void SetLoc(int x, int y)
        {
            locX = x;
            locY = y;
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            Close();
            if (_owner == null)
                return;
            _owner.Activate();
        }

        private void NavigatorForm_Load(object sender, EventArgs e) => SetDesktopLocation(locX, locY);

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(NavigatorForm));
            textBox1 = new TextBox();
            okButton = new Button();
            pictureBox1 = new PictureBox();
            ((ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            textBox1.BorderStyle = BorderStyle.None;
            textBox1.Cursor = Cursors.Default;
            textBox1.Font = new Font("MS UI Gothic", 11f, FontStyle.Regular, GraphicsUnit.Point, 128);
            textBox1.ImeMode = ImeMode.Disable;
            textBox1.Location = new Point(145, 13);
            textBox1.Multiline = true;
            textBox1.Name = "textBox1";
            textBox1.ReadOnly = true;
            textBox1.Size = new Size(330, 175);
            textBox1.TabIndex = 6;
            textBox1.TabStop = false;
            textBox1.Text = "Next, make sure to register some profiles to be used for running mugen (it is possible to register up to 8 profiles.)\r\n\r\n You can create a new profile by double clicking one of the 'New Profile' items in the window to the left. \r\n\r\n Feel free to click the Help button for more information.";
            okButton.Location = new Point(343, 194);
            okButton.Name = "okButton";
            okButton.Size = new Size(116, 28);
            okButton.TabIndex = 8;
            okButton.Text = "OK";
            okButton.UseVisualStyleBackColor = true;
            okButton.Click += new EventHandler(okButton_Click);
            pictureBox1.BackgroundImageLayout = ImageLayout.None;
            pictureBox1.Image = Resources.arror1;
            pictureBox1.InitialImage = null;
            pictureBox1.Location = new Point(8, 36);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(130, 88);
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox1.TabIndex = 7;
            pictureBox1.TabStop = false;
            AutoScaleDimensions = new SizeF(6f, 12f);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(490, 234);
            ControlBox = false;
            Controls.Add(okButton);
            Controls.Add(pictureBox1);
            Controls.Add(textBox1);
            FormBorderStyle = FormBorderStyle.None;
            Icon = (Icon)componentResourceManager.GetObject("$this.Icon");
            Name = nameof(NavigatorForm);
            Opacity = 0.9;
            StartPosition = FormStartPosition.Manual;
            Text = "Information";
            Load += new EventHandler(NavigatorForm_Load);
            ((ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }
    }
}
