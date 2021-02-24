// Decompiled with JetBrains decompiler
// Type: SwissArmyKnifeForMugen.WelcomeForm
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
    /// form displayed on first startup as a guide/initialization
    /// </summary>
    public class WelcomeForm : Form
    {
        private bool _clickedOK;
        private IContainer components;
        private Button cancelButton;
        private Button okButton;
        private TextBox textBox1;

        public WelcomeForm() => InitializeComponent();

        public bool DidClickOK() => _clickedOK;

        private void okButton_Click(object sender, EventArgs e)
        {
            _clickedOK = true;
            Close();
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            _clickedOK = false;
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
            ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(WelcomeForm));
            cancelButton = new Button();
            okButton = new Button();
            textBox1 = new TextBox();
            SuspendLayout();
            cancelButton.Location = new Point(394, 200);
            cancelButton.Name = "cancelButton";
            cancelButton.Size = new Size(90, 28);
            cancelButton.TabIndex = 3;
            cancelButton.Text = "Cancel";
            cancelButton.UseVisualStyleBackColor = true;
            cancelButton.Click += new EventHandler(cancelButton_Click);
            okButton.Location = new Point(298, 200);
            okButton.Name = "okButton";
            okButton.Size = new Size(90, 28);
            okButton.TabIndex = 2;
            okButton.Text = "OK";
            okButton.UseVisualStyleBackColor = true;
            okButton.Click += new EventHandler(okButton_Click);
            textBox1.BorderStyle = BorderStyle.None;
            textBox1.Cursor = Cursors.Default;
            textBox1.Font = new Font("MS UI Gothic", 12f, FontStyle.Regular, GraphicsUnit.Point, 128);
            textBox1.ImeMode = ImeMode.Disable;
            textBox1.Location = new Point(12, 12);
            textBox1.Multiline = true;
            textBox1.Name = "textBox1";
            textBox1.ReadOnly = true;
            textBox1.Size = new Size(472, 180);
            textBox1.TabIndex = 5;
            textBox1.TabStop = false;
            textBox1.Text = componentResourceManager.GetString("textBox1.Text");
            AutoScaleDimensions = new SizeF(6f, 12f);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(496, 240);
            Controls.Add(textBox1);
            Controls.Add(cancelButton);
            Controls.Add(okButton);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            Icon = (Icon)componentResourceManager.GetObject("$this.Icon");
            MaximizeBox = false;
            MinimizeBox = false;
            Name = nameof(WelcomeForm);
            ShowIcon = false;
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Welcome!";
            ResumeLayout(false);
            PerformLayout();
        }
    }
}
