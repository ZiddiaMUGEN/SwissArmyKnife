// Decompiled with JetBrains decompiler
// Type: SwissArmyKnifeForMugen.BackgroundForm
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
    /// TODO
    /// Genuinely no idea if this is used anywhere
    /// </summary>
    public class BackgroundForm : Form
    {
        private static BackgroundForm selfObj;

        private BackgroundForm() => this.InitializeComponent();

        public static BackgroundForm MainObj()
        {
            if (BackgroundForm.selfObj == null)
                BackgroundForm.selfObj = new BackgroundForm();
            return BackgroundForm.selfObj;
        }

        private void BackgroundForm_Load(object sender, EventArgs e)
        {
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(BackgroundForm));
            this.SuspendLayout();
            this.AutoScaleDimensions = new SizeF(6f, 12f);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.ClientSize = new Size(201, 45);
            this.ControlBox = false;
            this.FormBorderStyle = FormBorderStyle.None;
            this.Icon = (Icon)componentResourceManager.GetObject("$this.Icon");
            this.Location = new Point(-500, -5000);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = nameof(BackgroundForm);
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = FormStartPosition.Manual;
            this.Text = "Swiss Army Knife";
            this.Load += new EventHandler(this.BackgroundForm_Load);
            this.ResumeLayout(false);
        }
    }
}
