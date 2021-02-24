// Decompiled with JetBrains decompiler
// Type: SwissArmyKnifeForMugen.MainConfigPanel
// Assembly: SAKnifeWM, Version=1.0.8.0, Culture=neutral, PublicKeyToken=null
// MVID: 09478AD8-365C-4BF3-BEA1-B5785151259B
// Assembly location: C:\Users\ziddi\Downloads\Swiss Army Knife 1.1 Conversion\Swiss Army Knife 1.1 Conversion\SAKnifeWM.exe

using SwissArmyKnifeForMugen.Configs;
using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace SwissArmyKnifeForMugen.Displays
{
    public class MainConfigPanel : Form
    {
        private bool _disableCancel;
        private Button okButton;
        private Button cancelButton;
        private Label label1;
        private TextBox textBox1;
        private TextBox editorTextBox;
        private Button openFileButton;
        private Label label2;
        private CheckBox displayDebugWinCheckBox;
        private TextBox textBox3;
        private OpenFileDialog openFileDialog1;
        private GroupBox groupBox3;

        public MainConfigPanel() => InitializeComponent();

        public void DisableCancel() => _disableCancel = true;

        /// <summary>
        /// Loads configuration from the primary config file.
        /// </summary>
        private void LoadMainConfig()
        {
            MainConfig mainConfig = ProfileManager.MainObj().GetMainConfig();
            if (mainConfig == null)
                return;
            string str = mainConfig.GetTextEditor();
            if (str == null || str == "")
                str = "notepad.exe";
            editorTextBox.Text = str;
            displayDebugWinCheckBox.Checked = mainConfig.DoesShowDebugWin();
        }

        /// <summary>
        /// Writes out the main config file.
        /// </summary>
        /// <param name="isOK"></param>
        /// <returns></returns>
        private bool SaveMainConfig(bool isOK)
        {
            MainConfig mainConfig = ProfileManager.MainObj().GetMainConfig();
            if (mainConfig == null)
                return true;
            if (!isOK)
            {
                string path = mainConfig.GetTextEditor();
                if (path != null)
                    path = path.ToLower();
                if (path != null && (path.Contains("notepad") || File.Exists(path)))
                    return true;
            }
            string path1 = editorTextBox.Text;
            if (path1 == null || !path1.Contains("notepad") && !File.Exists(path1))
            {
                if (isOK && MessageBox.Show("The specified text editor cannot be found. Would you like to proceed using Notepad instead?", "Swiss Army Knife", MessageBoxButtons.OKCancel) == DialogResult.Cancel)
                    return false;
                path1 = "notepad.exe";
            }
            string configText = "TextEditor = " + path1 + Environment.NewLine + "DisplayDebugWin = " + (displayDebugWinCheckBox.Checked ? "1" : "0");
            if (!mainConfig.SaveConfigText(configText))
            {
                int num = (int)MessageBox.Show("Settings could not be written to the configuration file.", "Swiss Army Knife");
                return false;
            }
            mainConfig.ReLoad();
            return true;
        }

        private void MainConfigPanel_Load(object sender, EventArgs e)
        {
            if (_disableCancel)
                cancelButton.Enabled = false;
            LoadMainConfig();
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            if (SaveMainConfig(true))
                Close();
            else
                cancelButton.Enabled = true;
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            SaveMainConfig(false);
            Close();
        }

        private void openFileButton_Click(object sender, EventArgs e)
        {
            if (editorTextBox.Text != "")
            {
                if (Path.IsPathRooted(editorTextBox.Text))
                {
                    if (File.Exists(editorTextBox.Text))
                        openFileDialog1.InitialDirectory = Path.GetDirectoryName(editorTextBox.Text);
                    else
                        openFileDialog1.InitialDirectory = Environment.SpecialFolder.DesktopDirectory.ToString();
                }
                else
                    openFileDialog1.InitialDirectory = Environment.SpecialFolder.DesktopDirectory.ToString();
            }
            else
                openFileDialog1.InitialDirectory = Environment.SpecialFolder.DesktopDirectory.ToString();
            if (openFileDialog1.ShowDialog() != DialogResult.OK)
                return;
            editorTextBox.Text = openFileDialog1.FileName;
        }

        private void displayDebugWinCheckBox_CheckedChanged(object sender, EventArgs e)
        {
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(MainConfigPanel));
            okButton = new Button();
            cancelButton = new Button();
            label1 = new Label();
            textBox1 = new TextBox();
            editorTextBox = new TextBox();
            openFileButton = new Button();
            label2 = new Label();
            displayDebugWinCheckBox = new CheckBox();
            textBox3 = new TextBox();
            openFileDialog1 = new OpenFileDialog();
            groupBox3 = new GroupBox();
            SuspendLayout();
            okButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            okButton.Location = new Point(265, 283);
            okButton.Name = "okButton";
            okButton.Size = new Size(90, 28);
            okButton.TabIndex = 0;
            okButton.Text = "Confirm";
            okButton.UseVisualStyleBackColor = true;
            okButton.Click += new EventHandler(okButton_Click);
            cancelButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            cancelButton.Location = new Point(361, 283);
            cancelButton.Name = "cancelButton";
            cancelButton.Size = new Size(90, 28);
            cancelButton.TabIndex = 1;
            cancelButton.Text = "Cancel";
            cancelButton.UseVisualStyleBackColor = true;
            cancelButton.Click += new EventHandler(cancelButton_Click);
            label1.AutoSize = true;
            label1.Font = new Font("MS UI Gothic", 10f, FontStyle.Regular, GraphicsUnit.Point, 128);
            label1.Location = new Point(12, 23);
            label1.Name = "label1";
            label1.Size = new Size(132, 14);
            label1.TabIndex = 2;
            label1.Text = "Text editor settings:";
            textBox1.BorderStyle = BorderStyle.None;
            textBox1.Cursor = Cursors.Default;
            textBox1.ImeMode = ImeMode.Disable;
            textBox1.Location = new Point(74, 71);
            textBox1.Multiline = true;
            textBox1.Name = "textBox1";
            textBox1.ReadOnly = true;
            textBox1.Size = new Size(298, 72);
            textBox1.TabIndex = 4;
            textBox1.TabStop = false;
            textBox1.Text = "⇒Please specify the full filepath to your preferred text editor. \r\n\r\n      Example: C:\\Program Files\\Editor\\Editor.exe\r\n\r\nThe default setting is Notepad.\r\n";
            editorTextBox.Font = new Font("MS UI Gothic", 10f, FontStyle.Regular, GraphicsUnit.Point, 128);
            editorTextBox.Location = new Point(40, 44);
            editorTextBox.MaxLength = 250;
            editorTextBox.Name = "editorTextBox";
            editorTextBox.Size = new Size(332, 21);
            editorTextBox.TabIndex = 5;
            editorTextBox.Text = "notepad.exe";
            openFileButton.Location = new Point(378, 43);
            openFileButton.Name = "openFileButton";
            openFileButton.Size = new Size(73, 23);
            openFileButton.TabIndex = 6;
            openFileButton.Text = "Browse...";
            openFileButton.UseVisualStyleBackColor = true;
            openFileButton.Click += new EventHandler(openFileButton_Click);
            label2.AutoSize = true;
            label2.Font = new Font("MS UI Gothic", 10f, FontStyle.Regular, GraphicsUnit.Point, 128);
            label2.Location = new Point(12, 166);
            label2.Name = "label2";
            label2.Size = new Size(91, 14);
            label2.TabIndex = 7;
            label2.Text = "Startup settings:";
            displayDebugWinCheckBox.AutoSize = true;
            displayDebugWinCheckBox.Font = new Font("MS UI Gothic", 10f, FontStyle.Regular, GraphicsUnit.Point, 128);
            displayDebugWinCheckBox.Location = new Point(40, 186);
            displayDebugWinCheckBox.Name = "displayDebugWinCheckBox";
            displayDebugWinCheckBox.Size = new Size(400, 18);
            displayDebugWinCheckBox.TabIndex = 8;
            displayDebugWinCheckBox.Text = "Automatically open the Debug Tools and Variable View forms on startup.";
            displayDebugWinCheckBox.UseVisualStyleBackColor = true;
            displayDebugWinCheckBox.CheckedChanged += new EventHandler(displayDebugWinCheckBox_CheckedChanged);
            textBox3.BorderStyle = BorderStyle.None;
            textBox3.Cursor = Cursors.Default;
            textBox3.Location = new Point(72, 210);
            textBox3.Multiline = true;
            textBox3.Name = "textBox3";
            textBox3.ReadOnly = true;
            textBox3.Size = new Size(310, 51);
            textBox3.TabIndex = 9;
            textBox3.TabStop = false;
            textBox3.Text = "⇒We recommend this setting if the purpose of this tool is to aid in the creation of characters. (Note that they may display offscreen if the screen resolution is less than 1280x1024).";
            openFileDialog1.AddExtension = false;
            openFileDialog1.DefaultExt = "exe";
            openFileDialog1.Filter = "exe file|*.exe";
            openFileDialog1.InitialDirectory = "C:\\";
            openFileDialog1.ReadOnlyChecked = true;
            openFileDialog1.RestoreDirectory = true;
            openFileDialog1.SupportMultiDottedExtensions = true;
            openFileDialog1.Title = "Select a text editor executable file";
            groupBox3.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            groupBox3.Location = new Point(17, 273);
            groupBox3.Margin = new Padding(0);
            groupBox3.Name = "groupBox3";
            groupBox3.Padding = new Padding(0);
            groupBox3.Size = new Size(420, 4);
            groupBox3.TabIndex = 10;
            groupBox3.TabStop = false;
            AutoScaleDimensions = new SizeF(6f, 12f);
            AutoScaleMode = AutoScaleMode.Font;
            AutoSize = true;
            ClientSize = new Size(463, 323);
            Controls.Add(groupBox3);
            Controls.Add(textBox3);
            Controls.Add(displayDebugWinCheckBox);
            Controls.Add(label2);
            Controls.Add(openFileButton);
            Controls.Add(editorTextBox);
            Controls.Add(textBox1);
            Controls.Add(label1);
            Controls.Add(cancelButton);
            Controls.Add(okButton);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            Icon = (Icon)componentResourceManager.GetObject("$this.Icon");
            MaximizeBox = false;
            MinimizeBox = false;
            Name = nameof(MainConfigPanel);
            ShowIcon = false;
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Main settings";
            Load += new EventHandler(MainConfigPanel_Load);
            ResumeLayout(false);
            PerformLayout();
        }
    }
}
