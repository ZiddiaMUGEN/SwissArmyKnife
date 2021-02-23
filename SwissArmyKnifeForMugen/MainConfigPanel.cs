// Decompiled with JetBrains decompiler
// Type: SwissArmyKnifeForMugen.MainConfigPanel
// Assembly: SAKnifeWM, Version=1.0.8.0, Culture=neutral, PublicKeyToken=null
// MVID: 09478AD8-365C-4BF3-BEA1-B5785151259B
// Assembly location: C:\Users\ziddi\Downloads\Swiss Army Knife 1.1 Conversion\Swiss Army Knife 1.1 Conversion\SAKnifeWM.exe

using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace SwissArmyKnifeForMugen
{
  public class MainConfigPanel : Form
  {
    private bool _disableCancel;
    private IContainer components;
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

    public MainConfigPanel() => this.InitializeComponent();

    public void DisableCancel() => this._disableCancel = true;

    private void LoadMainConfig()
    {
      MainConfig mainConfig = ProfileManager.MainObj().GetMainConfig();
      if (mainConfig == null)
        return;
      string str = mainConfig.GetTextEditor();
      if (str == null || str == "")
        str = "notepad.exe";
      this.editorTextBox.Text = str;
      this.displayDebugWinCheckBox.Checked = mainConfig.DoesShowDebugWin();
    }

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
      string path1 = this.editorTextBox.Text;
      if (path1 == null || !path1.Contains("notepad") && !File.Exists(path1))
      {
        if (isOK && MessageBox.Show("The specified text editor cannot be found. Would you like to proceed using Notepad instead?", "Swiss Army Knife", MessageBoxButtons.OKCancel) == DialogResult.Cancel)
          return false;
        path1 = "notepad.exe";
      }
      string configText = "TextEditor = " + path1 + Environment.NewLine + "DisplayDebugWin = " + (this.displayDebugWinCheckBox.Checked ? "1" : "0");
      if (!mainConfig.SaveConfigText(configText))
      {
        int num = (int) MessageBox.Show("Settings could not be written to the configuration file.", "Swiss Army Knife");
        return false;
      }
      mainConfig.ReLoad();
      return true;
    }

    private void MainConfigPanel_Load(object sender, EventArgs e)
    {
      if (this._disableCancel)
        this.cancelButton.Enabled = false;
      this.LoadMainConfig();
    }

    private void okButton_Click(object sender, EventArgs e)
    {
      if (this.SaveMainConfig(true))
        this.Close();
      else
        this.cancelButton.Enabled = true;
    }

    private void cancelButton_Click(object sender, EventArgs e)
    {
      this.SaveMainConfig(false);
      this.Close();
    }

    private void openFileButton_Click(object sender, EventArgs e)
    {
      if (this.editorTextBox.Text != "")
      {
        if (Path.IsPathRooted(this.editorTextBox.Text))
        {
          if (File.Exists(this.editorTextBox.Text))
            this.openFileDialog1.InitialDirectory = Path.GetDirectoryName(this.editorTextBox.Text);
          else
            this.openFileDialog1.InitialDirectory = Environment.SpecialFolder.DesktopDirectory.ToString();
        }
        else
          this.openFileDialog1.InitialDirectory = Environment.SpecialFolder.DesktopDirectory.ToString();
      }
      else
        this.openFileDialog1.InitialDirectory = Environment.SpecialFolder.DesktopDirectory.ToString();
      if (this.openFileDialog1.ShowDialog() != DialogResult.OK)
        return;
      this.editorTextBox.Text = this.openFileDialog1.FileName;
    }

    private void displayDebugWinCheckBox_CheckedChanged(object sender, EventArgs e)
    {
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (MainConfigPanel));
      this.okButton = new Button();
      this.cancelButton = new Button();
      this.label1 = new Label();
      this.textBox1 = new TextBox();
      this.editorTextBox = new TextBox();
      this.openFileButton = new Button();
      this.label2 = new Label();
      this.displayDebugWinCheckBox = new CheckBox();
      this.textBox3 = new TextBox();
      this.openFileDialog1 = new OpenFileDialog();
      this.groupBox3 = new GroupBox();
      this.SuspendLayout();
      this.okButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
      this.okButton.Location = new Point(265, 283);
      this.okButton.Name = "okButton";
      this.okButton.Size = new Size(90, 28);
      this.okButton.TabIndex = 0;
      this.okButton.Text = "Confirm";
      this.okButton.UseVisualStyleBackColor = true;
      this.okButton.Click += new EventHandler(this.okButton_Click);
      this.cancelButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
      this.cancelButton.Location = new Point(361, 283);
      this.cancelButton.Name = "cancelButton";
      this.cancelButton.Size = new Size(90, 28);
      this.cancelButton.TabIndex = 1;
      this.cancelButton.Text = "Cancel";
      this.cancelButton.UseVisualStyleBackColor = true;
      this.cancelButton.Click += new EventHandler(this.cancelButton_Click);
      this.label1.AutoSize = true;
      this.label1.Font = new Font("MS UI Gothic", 10f, FontStyle.Regular, GraphicsUnit.Point, (byte) 128);
      this.label1.Location = new Point(12, 23);
      this.label1.Name = "label1";
      this.label1.Size = new Size(132, 14);
      this.label1.TabIndex = 2;
      this.label1.Text = "Text editor settings:";
      this.textBox1.BorderStyle = BorderStyle.None;
      this.textBox1.Cursor = Cursors.Default;
      this.textBox1.ImeMode = ImeMode.Disable;
      this.textBox1.Location = new Point(74, 71);
      this.textBox1.Multiline = true;
      this.textBox1.Name = "textBox1";
      this.textBox1.ReadOnly = true;
      this.textBox1.Size = new Size(298, 72);
      this.textBox1.TabIndex = 4;
      this.textBox1.TabStop = false;
      this.textBox1.Text = "⇒Please specify the full filepath to your preferred text editor. \r\n\r\n      Example: C:\\Program Files\\Editor\\Editor.exe\r\n\r\nThe default setting is Notepad.\r\n";
      this.editorTextBox.Font = new Font("MS UI Gothic", 10f, FontStyle.Regular, GraphicsUnit.Point, (byte) 128);
      this.editorTextBox.Location = new Point(40, 44);
      this.editorTextBox.MaxLength = 250;
      this.editorTextBox.Name = "editorTextBox";
      this.editorTextBox.Size = new Size(332, 21);
      this.editorTextBox.TabIndex = 5;
      this.editorTextBox.Text = "notepad.exe";
      this.openFileButton.Location = new Point(378, 43);
      this.openFileButton.Name = "openFileButton";
      this.openFileButton.Size = new Size(73, 23);
      this.openFileButton.TabIndex = 6;
      this.openFileButton.Text = "Browse...";
      this.openFileButton.UseVisualStyleBackColor = true;
      this.openFileButton.Click += new EventHandler(this.openFileButton_Click);
      this.label2.AutoSize = true;
      this.label2.Font = new Font("MS UI Gothic", 10f, FontStyle.Regular, GraphicsUnit.Point, (byte) 128);
      this.label2.Location = new Point(12, 166);
      this.label2.Name = "label2";
      this.label2.Size = new Size(91, 14);
      this.label2.TabIndex = 7;
      this.label2.Text = "Startup settings:";
      this.displayDebugWinCheckBox.AutoSize = true;
      this.displayDebugWinCheckBox.Font = new Font("MS UI Gothic", 10f, FontStyle.Regular, GraphicsUnit.Point, (byte) 128);
      this.displayDebugWinCheckBox.Location = new Point(40, 186);
      this.displayDebugWinCheckBox.Name = "displayDebugWinCheckBox";
      this.displayDebugWinCheckBox.Size = new Size(400, 18);
      this.displayDebugWinCheckBox.TabIndex = 8;
      this.displayDebugWinCheckBox.Text = "Automatically open the Debug Tools and Variable View forms on startup.";
      this.displayDebugWinCheckBox.UseVisualStyleBackColor = true;
      this.displayDebugWinCheckBox.CheckedChanged += new EventHandler(this.displayDebugWinCheckBox_CheckedChanged);
      this.textBox3.BorderStyle = BorderStyle.None;
      this.textBox3.Cursor = Cursors.Default;
      this.textBox3.Location = new Point(72, 210);
      this.textBox3.Multiline = true;
      this.textBox3.Name = "textBox3";
      this.textBox3.ReadOnly = true;
      this.textBox3.Size = new Size(310, 51);
      this.textBox3.TabIndex = 9;
      this.textBox3.TabStop = false;
      this.textBox3.Text = "⇒We recommend this setting if the purpose of this tool is to aid in the creation of characters. (Note that they may display offscreen if the screen resolution is less than 1280x1024).";
      this.openFileDialog1.AddExtension = false;
      this.openFileDialog1.DefaultExt = "exe";
      this.openFileDialog1.Filter = "exe file|*.exe";
      this.openFileDialog1.InitialDirectory = "C:\\";
      this.openFileDialog1.ReadOnlyChecked = true;
      this.openFileDialog1.RestoreDirectory = true;
      this.openFileDialog1.SupportMultiDottedExtensions = true;
      this.openFileDialog1.Title = "Select a text editor executable file";
      this.groupBox3.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
      this.groupBox3.Location = new Point(17, 273);
      this.groupBox3.Margin = new Padding(0);
      this.groupBox3.Name = "groupBox3";
      this.groupBox3.Padding = new Padding(0);
      this.groupBox3.Size = new Size(420, 4);
      this.groupBox3.TabIndex = 10;
      this.groupBox3.TabStop = false;
      this.AutoScaleDimensions = new SizeF(6f, 12f);
      this.AutoScaleMode = AutoScaleMode.Font;
      this.AutoSize = true;
      this.ClientSize = new Size(463, 323);
      this.Controls.Add((Control) this.groupBox3);
      this.Controls.Add((Control) this.textBox3);
      this.Controls.Add((Control) this.displayDebugWinCheckBox);
      this.Controls.Add((Control) this.label2);
      this.Controls.Add((Control) this.openFileButton);
      this.Controls.Add((Control) this.editorTextBox);
      this.Controls.Add((Control) this.textBox1);
      this.Controls.Add((Control) this.label1);
      this.Controls.Add((Control) this.cancelButton);
      this.Controls.Add((Control) this.okButton);
      this.FormBorderStyle = FormBorderStyle.FixedDialog;
      this.Icon = (Icon) componentResourceManager.GetObject("$this.Icon");
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = nameof (MainConfigPanel);
      this.ShowIcon = false;
      this.ShowInTaskbar = false;
      this.StartPosition = FormStartPosition.CenterScreen;
      this.Text = "Main settings";
      this.Load += new EventHandler(this.MainConfigPanel_Load);
      this.ResumeLayout(false);
      this.PerformLayout();
    }
  }
}
