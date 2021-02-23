// Decompiled with JetBrains decompiler
// Type: SwissArmyKnifeForMugen.ProfileConfigPanel
// Assembly: SAKnifeWM, Version=1.0.8.0, Culture=neutral, PublicKeyToken=null
// MVID: 09478AD8-365C-4BF3-BEA1-B5785151259B
// Assembly location: C:\Users\ziddi\Downloads\Swiss Army Knife 1.1 Conversion\Swiss Army Knife 1.1 Conversion\SAKnifeWM.exe

using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace SwissArmyKnifeForMugen
{
  public class ProfileConfigPanel : Form
  {
    private bool _isHelpMode;
    private IContainer components;
    private Button cancelButton;
    private Button okButton;
    private Label profileNameLabel;
    private TextBox profileNameTextBox;
    private TabControl tabControl1;
    private TabPage tabPage1;
    private GroupBox groupBox1;
    private Label label5;
    private CheckBox enableSkipModeCheckBox;
    private CheckBox enableSpeedUpCheckBox;
    private CheckBox enableDebugCheckBox;
    private TextBox cmdLineTextBox;
    private Label cmdLineLabel;
    private Button openFileButton2;
    private TextBox selectDefTextBox;
    private Label selectDefLabel;
    private Button openFileButton1;
    private TextBox mugenExeTextBox;
    private Label mugenExeLabel;
    private TabPage tabPage2;
    private GroupBox groupBox2;
    private Button autoButton;
    private Label label13;
    private TextBox colorTextBox;
    private Label label12;
    private TextBox roundTimeTextBox;
    private Label label11;
    private Label colorLabel;
    private TextBox retryTextBox2;
    private Label label9;
    private TextBox roundTextBox;
    private Label roundTimeLabel;
    private Label roundTextLabel;
    private Label retryTextLabel;
    private CheckBox enableAutoModecheckBox;
    private TextBox charListTextBox;
    private TextBox textBox2;
    private OpenFileDialog mugenOpenFileDialog;
    private OpenFileDialog selectOpenFileDialog;
    private ToolTip toolTip1;
    private CheckBox helpCheckBox;
    private ContextMenuStrip charContextMenuStrip;
    private ToolStripMenuItem allSelToolStripMenuItem;
    private ToolStripMenuItem copyToolStripMenuItem;
    private ToolStripMenuItem pasteToolStripMenuItem;
    private RadioButton autoModeRadioButton;
    private RadioButton quickModeRadioButton;
    private RadioButton normalModeRadioButton;
    private Label mugenModeLabel;
    private TextBox textBox1;
    private GroupBox groupBox3;
    private RadioButton bothModeRadioButton;
    private RadioButton p2ModeRadioButton;
    private RadioButton p1ModeRadioButton;
    private Label teamSideLabel;
    private CheckBox roundModeCheckBox;
    private Label modeLabel;
    private Panel panel1;
    private RadioButton allVSModeRadioButton;
    private RadioButton oneVSModeRadioButton;
    private CheckBox enableExperimentalBreakpointsCheckbox;

    public ProfileConfigPanel() => this.InitializeComponent();

    private void LoadProfileConfig()
    {
      MugenProfile currentProfile = ProfileManager.MainObj().GetCurrentProfile();
      if (currentProfile == null)
        return;
      string profileName = currentProfile.GetProfileName();
      if (profileName != null)
        this.profileNameTextBox.Text = profileName;
      string mugenExePath = currentProfile.GetMugenExePath();
      if (mugenExePath != null)
        this.mugenExeTextBox.Text = mugenExePath;
      string mugenSelectCfgPath = currentProfile.GetMugenSelectCfgPath();
      if (mugenSelectCfgPath != null)
        this.selectDefTextBox.Text = mugenSelectCfgPath;
      string commandLineOptionsBase = currentProfile.GetMugenCommandLineOptionsBase();
      if (commandLineOptionsBase != null)
        this.cmdLineTextBox.Text = commandLineOptionsBase;
      this.enableDebugCheckBox.Checked = currentProfile.IsDebugMode();
      this.enableSpeedUpCheckBox.Checked = currentProfile.IsSpeedMode();
      this.enableSkipModeCheckBox.Checked = currentProfile.IsSkipMode();
      this.enableAutoModecheckBox.Checked = currentProfile.IsAutoModeAvailable();
      this.autoModeRadioButton.Enabled = currentProfile.IsAutoModeAvailable();
      this.enableExperimentalBreakpointsCheckbox.Checked = currentProfile.IsExperimentalBreakpoints();
      switch (currentProfile.GetDefaultGameMode())
      {
        case MugenProfile.GameMode.NORMAL:
          this.normalModeRadioButton.Checked = true;
          break;
        case MugenProfile.GameMode.QUICK_VS:
          this.quickModeRadioButton.Checked = true;
          break;
        case MugenProfile.GameMode.AUTO_MODE:
          this.autoModeRadioButton.Checked = true;
          break;
      }
      TextBox roundTextBox = this.roundTextBox;
      int num = currentProfile.GetRoundCount();
      string str1 = num.ToString();
      roundTextBox.Text = str1;
      TextBox roundTimeTextBox = this.roundTimeTextBox;
      num = currentProfile.GetMaxRoundTimeRawData();
      string str2 = num.ToString();
      roundTimeTextBox.Text = str2;
      this.roundModeCheckBox.Checked = currentProfile.IsStrictRoundMode();
      TextBox retryTextBox2 = this.retryTextBox2;
      num = currentProfile.GetErrorRetryCount();
      string str3 = num.ToString();
      retryTextBox2.Text = str3;
      TextBox colorTextBox = this.colorTextBox;
      num = currentProfile.GetDefaultColor();
      string str4 = num.ToString();
      colorTextBox.Text = str4;
      switch (currentProfile.GetMatchMode())
      {
        case MugenProfile.MatchMode.P1vsALL:
          this.oneVSModeRadioButton.Checked = true;
          break;
        case MugenProfile.MatchMode.ALLvsALL:
          this.allVSModeRadioButton.Checked = true;
          break;
      }
      switch (currentProfile.GetTeamSideMode())
      {
        case MugenProfile.TeamSideMode.P1_SIDE:
          this.p1ModeRadioButton.Checked = true;
          break;
        case MugenProfile.TeamSideMode.P2_SIDE:
          this.p2ModeRadioButton.Checked = true;
          break;
        case MugenProfile.TeamSideMode.BOTH_SIDE:
          this.bothModeRadioButton.Checked = true;
          break;
      }
      string charListRawData = currentProfile.GetCharListRawData();
      if (charListRawData == null)
        return;
      this.charListTextBox.Text = charListRawData;
    }

    private bool SaveProfileConfig()
    {
      MugenProfile currentProfile = ProfileManager.MainObj().GetCurrentProfile();
      if (currentProfile == null)
        return false;
      if (this.profileNameTextBox.Text == "")
      {
        int num = (int) MessageBox.Show("Please enter a profile name.", "Swiss Army Knife");
        this.profileNameTextBox.Focus();
        this.profileNameTextBox.SelectAll();
        return false;
      }
      if (this.mugenExeTextBox.Text == "")
      {
        int num = (int) MessageBox.Show("Please specify the mugen executable location.", "Swiss Army Knife");
        this.tabControl1.SelectedIndex = 0;
        this.mugenExeTextBox.Focus();
        this.mugenExeTextBox.SelectAll();
        return false;
      }
      if (!File.Exists(this.mugenExeTextBox.Text))
      {
        int num = (int) MessageBox.Show("The specified mugen executable does not exist.", "Swiss Army Knife");
        this.tabControl1.SelectedIndex = 0;
        this.mugenExeTextBox.Focus();
        this.mugenExeTextBox.SelectAll();
        return false;
      }
      string extension = Path.GetExtension(this.mugenExeTextBox.Text);
      extension.ToLower();
      if (extension != ".exe")
      {
        int num = (int) MessageBox.Show("The specified file is not an executable file", "Swiss Army Knife");
        this.mugenExeTextBox.Focus();
        this.mugenExeTextBox.SelectAll();
        return false;
      }
      FileVersionInfo versionInfo = FileVersionInfo.GetVersionInfo(this.mugenExeTextBox.Text);
      if (versionInfo != null && versionInfo.FileVersion != "1.0.0" && (versionInfo.FileVersion != "1.1.0 Alpha 4" && versionInfo.FileVersion != "1.1.0 Beta 1 P1") && versionInfo.FileVersion != null)
      {
        int num = (int) MessageBox.Show("The specified executable is not mugen.", "Swiss Army Knife");
        this.tabControl1.SelectedIndex = 0;
        this.mugenExeTextBox.Focus();
        this.mugenExeTextBox.SelectAll();
        return false;
      }
      if (this.selectDefTextBox.Text != "")
      {
        if (!File.Exists(this.selectDefTextBox.Text))
        {
          int num = (int) MessageBox.Show("The specified select.def does not exist.", "Swiss Army Knife");
          this.selectDefTextBox.Focus();
          this.selectDefTextBox.SelectAll();
          return false;
        }
        string fileName = Path.GetFileName(this.selectDefTextBox.Text);
        fileName.ToLower();
        if (fileName != "select.def")
        {
          int num = (int) MessageBox.Show("The specified file is not a select.def file.", "Swiss Army Knife");
          this.selectDefTextBox.Focus();
          this.selectDefTextBox.SelectAll();
          return false;
        }
      }
      int result;
      if (!int.TryParse(this.roundTextBox.Text, out result) || result < 1 || result > 99)
      {
        int num = (int) MessageBox.Show("Please specify an integer between 1 and 99 for the number of rounds.", "Swiss Army Knife");
        this.tabControl1.SelectedIndex = 1;
        this.roundTextBox.Focus();
        this.roundTextBox.SelectAll();
        return false;
      }
      if (!int.TryParse(this.roundTimeTextBox.Text, out result) || result < 1 || result > 999)
      {
        int num = (int) MessageBox.Show("Please specify an integer between 1 and 999 for the round time.", "Swiss Army Knife");
        this.tabControl1.SelectedIndex = 1;
        this.roundTimeTextBox.Focus();
        this.roundTimeTextBox.SelectAll();
        return false;
      }
      if (!int.TryParse(this.retryTextBox2.Text, out result) || result < 0 || result > 99)
      {
        int num = (int) MessageBox.Show("Please specify an integer between 1 and 99 for the number of retries after a crash.", "Swiss Army Knife");
        this.tabControl1.SelectedIndex = 1;
        this.retryTextBox2.Focus();
        this.retryTextBox2.SelectAll();
        return false;
      }
      if (!int.TryParse(this.colorTextBox.Text, out result) || result < 1 || result > 12)
      {
        int num = (int) MessageBox.Show("Please specify an integer between 1 and 12 for the default color.", "Swiss Army Knife");
        this.tabControl1.SelectedIndex = 1;
        this.colorTextBox.Focus();
        this.colorTextBox.SelectAll();
        return false;
      }
      currentProfile.MakeBackup();
      string str1 = ";" + Environment.NewLine + "; Filename:" + currentProfile.GetProfileFile() + Environment.NewLine + ";" + Environment.NewLine + Environment.NewLine + "[Mugen]" + Environment.NewLine + "ProfileName = " + this.profileNameTextBox.Text + Environment.NewLine + "MugenExe = " + this.mugenExeTextBox.Text + Environment.NewLine + "SelectDotDef = " + this.selectDefTextBox.Text + Environment.NewLine + "MugenCommandLineOptions = " + this.cmdLineTextBox.Text + Environment.NewLine + "DebugMode = " + (this.enableDebugCheckBox.Checked ? "1" : "0") + Environment.NewLine + "SpeedUp = " + (this.enableSpeedUpCheckBox.Checked ? "1" : "0") + Environment.NewLine + "SkipMode = " + (this.enableSkipModeCheckBox.Checked ? "1" : "0") + Environment.NewLine + "ExperimentalBPS = " + (this.enableExperimentalBreakpointsCheckbox.Checked ? "1" : "0") + Environment.NewLine;
      if (this.normalModeRadioButton.Checked)
        str1 += "DefaultGameMode = 0";
      else if (this.quickModeRadioButton.Checked)
        str1 += "DefaultGameMode = 1";
      else if (this.autoModeRadioButton.Checked)
        str1 += "DefaultGameMode = 2";
      string str2 = str1 + Environment.NewLine + "[AutoMode]" + Environment.NewLine + "AutoMode = " + (this.enableAutoModecheckBox.Checked ? "1" : "0") + Environment.NewLine + "Rounds = " + this.roundTextBox.Text + Environment.NewLine + "MaxRoundTime = " + this.roundTimeTextBox.Text + Environment.NewLine + "StrictRoundMode = " + (this.roundModeCheckBox.Checked ? "1" : "0") + Environment.NewLine + "ErrorRetry = " + this.retryTextBox2.Text + Environment.NewLine + "DefaultColor = " + this.colorTextBox.Text + Environment.NewLine;
      if (this.oneVSModeRadioButton.Checked)
        str2 += "MatchMode = 0";
      else if (this.allVSModeRadioButton.Checked)
        str2 += "MatchMode = 1";
      string str3 = str2 + Environment.NewLine;
      if (this.p1ModeRadioButton.Checked)
        str3 += "TeamSide = 1";
      else if (this.p2ModeRadioButton.Checked)
        str3 += "TeamSide = 2";
      else if (this.bothModeRadioButton.Checked)
        str3 += "TeamSide = 0";
      string configText = str3 + Environment.NewLine + Environment.NewLine + "[Characters]" + Environment.NewLine + this.charListTextBox.Text + Environment.NewLine;
      if (!currentProfile.SaveConfigText(configText))
      {
        int num = (int) MessageBox.Show("Could not write to the configuration file.", "Swiss Army Knife");
        return false;
      }
      currentProfile.ReLoad();
      return true;
    }

    private void okButton_Click(object sender, EventArgs e)
    {
      Control okButton = (Control) this.okButton;
      if (okButton != null && okButton.Cursor == Cursors.Help || !this.SaveProfileConfig())
        return;
      this.Close();
    }

    private void cancelButton_Click(object sender, EventArgs e) => this.Close();

    private void openFileButton1_Click(object sender, EventArgs e)
    {
      if (this.mugenExeTextBox.Text != "")
      {
        if (Path.IsPathRooted(this.mugenExeTextBox.Text))
        {
          if (File.Exists(this.mugenExeTextBox.Text))
            this.mugenOpenFileDialog.InitialDirectory = Path.GetDirectoryName(this.mugenExeTextBox.Text);
          else
            this.mugenOpenFileDialog.InitialDirectory = Environment.SpecialFolder.DesktopDirectory.ToString();
        }
        else
          this.mugenOpenFileDialog.InitialDirectory = Environment.SpecialFolder.DesktopDirectory.ToString();
      }
      else
        this.mugenOpenFileDialog.InitialDirectory = Environment.SpecialFolder.DesktopDirectory.ToString();
      if (this.mugenOpenFileDialog.ShowDialog() != DialogResult.OK)
        return;
      this.mugenExeTextBox.Text = this.mugenOpenFileDialog.FileName;
    }

    private void openFileButton2_Click(object sender, EventArgs e)
    {
      if (this.selectDefTextBox.Text != "")
      {
        if (Path.IsPathRooted(this.selectDefTextBox.Text))
        {
          if (File.Exists(this.selectDefTextBox.Text))
            this.selectOpenFileDialog.InitialDirectory = Path.GetDirectoryName(this.selectDefTextBox.Text);
          else
            this.selectOpenFileDialog.InitialDirectory = Environment.SpecialFolder.DesktopDirectory.ToString();
        }
        else
          this.selectOpenFileDialog.InitialDirectory = Environment.SpecialFolder.DesktopDirectory.ToString();
      }
      else if (this.mugenExeTextBox.Text != "")
      {
        if (Path.IsPathRooted(this.mugenExeTextBox.Text))
        {
          if (File.Exists(this.mugenExeTextBox.Text))
            this.selectOpenFileDialog.InitialDirectory = Path.GetDirectoryName(this.mugenExeTextBox.Text);
          else
            this.selectOpenFileDialog.InitialDirectory = Environment.SpecialFolder.DesktopDirectory.ToString();
        }
        else
          this.selectOpenFileDialog.InitialDirectory = Environment.SpecialFolder.DesktopDirectory.ToString();
      }
      else
        this.selectOpenFileDialog.InitialDirectory = Environment.SpecialFolder.DesktopDirectory.ToString();
      if (this.selectOpenFileDialog.ShowDialog() != DialogResult.OK)
        return;
      this.selectDefTextBox.Text = this.selectOpenFileDialog.FileName;
    }

    private void enableDebugCheckBox_CheckedChanged(object sender, EventArgs e)
    {
      Control enableDebugCheckBox = (Control) this.enableDebugCheckBox;
      if (enableDebugCheckBox == null || !(enableDebugCheckBox.Cursor == Cursors.Help))
        return;
      this.enableDebugCheckBox.CheckedChanged -= new EventHandler(this.enableDebugCheckBox_CheckedChanged);
      this.enableDebugCheckBox.Checked = !this.enableDebugCheckBox.Checked;
      this.enableDebugCheckBox.CheckedChanged += new EventHandler(this.enableDebugCheckBox_CheckedChanged);
      string text = "Enables the debug mode in mugen. This has the same effect as pressing Ctrl + D." + Environment.NewLine;
      this.toolTip1.Hide((IWin32Window) enableDebugCheckBox);
      this.toolTip1.IsBalloon = false;
      this.toolTip1.IsBalloon = true;
      this.toolTip1.SetToolTip(enableDebugCheckBox, " ");
      this.toolTip1.Show(text, (IWin32Window) enableDebugCheckBox, enableDebugCheckBox.Width / 2, enableDebugCheckBox.Height / 2);
    }

    private void enableSpeedUpCheckBox_CheckedChanged(object sender, EventArgs e)
    {
      Control enableSpeedUpCheckBox = (Control) this.enableSpeedUpCheckBox;
      if (enableSpeedUpCheckBox == null || !(enableSpeedUpCheckBox.Cursor == Cursors.Help))
        return;
      this.enableSpeedUpCheckBox.CheckedChanged -= new EventHandler(this.enableSpeedUpCheckBox_CheckedChanged);
      this.enableSpeedUpCheckBox.Checked = !this.enableSpeedUpCheckBox.Checked;
      this.enableSpeedUpCheckBox.CheckedChanged += new EventHandler(this.enableSpeedUpCheckBox_CheckedChanged);
      string text = "Enables the high speed mode in mugen. This has the same effect as pressing Ctrl + S." + Environment.NewLine;
      this.toolTip1.Hide((IWin32Window) enableSpeedUpCheckBox);
      this.toolTip1.IsBalloon = false;
      this.toolTip1.IsBalloon = true;
      this.toolTip1.SetToolTip(enableSpeedUpCheckBox, " ");
      this.toolTip1.Show(text, (IWin32Window) enableSpeedUpCheckBox, enableSpeedUpCheckBox.Width / 2, enableSpeedUpCheckBox.Height / 2);
    }

    private void enableSkipModeCheckBox_CheckedChanged(object sender, EventArgs e)
    {
      Control skipModeCheckBox = (Control) this.enableSkipModeCheckBox;
      if (skipModeCheckBox == null || !(skipModeCheckBox.Cursor == Cursors.Help))
        return;
      this.enableSkipModeCheckBox.CheckedChanged -= new EventHandler(this.enableSkipModeCheckBox_CheckedChanged);
      this.enableSkipModeCheckBox.Checked = !this.enableSkipModeCheckBox.Checked;
      this.enableSkipModeCheckBox.CheckedChanged += new EventHandler(this.enableSkipModeCheckBox_CheckedChanged);
      string text = "Enables the frameskip function in mugen. This will skip the drawing phase of one frame for every drawn one." + Environment.NewLine + "By pairing this setting with the high speed mode, it is possible to make matches last an even shorter time." + Environment.NewLine;
      this.toolTip1.Hide((IWin32Window) skipModeCheckBox);
      this.toolTip1.IsBalloon = false;
      this.toolTip1.IsBalloon = true;
      this.toolTip1.SetToolTip(skipModeCheckBox, " ");
      this.toolTip1.Show(text, (IWin32Window) skipModeCheckBox, skipModeCheckBox.Width / 2, skipModeCheckBox.Height / 2);
    }

    private void EnableAutoModeUI()
    {
      if (this.enableAutoModecheckBox.Checked || this.Cursor == Cursors.Help)
      {
        this.roundTextBox.Enabled = true;
        this.roundTimeTextBox.Enabled = true;
        this.roundModeCheckBox.Enabled = true;
        this.retryTextBox2.Enabled = true;
        this.colorTextBox.Enabled = true;
        this.oneVSModeRadioButton.Enabled = true;
        this.allVSModeRadioButton.Enabled = true;
        this.p1ModeRadioButton.Enabled = true;
        this.p2ModeRadioButton.Enabled = true;
        this.bothModeRadioButton.Enabled = true;
        this.charListTextBox.Enabled = true;
        this.autoButton.Enabled = true;
      }
      else
      {
        this.roundTextBox.Enabled = false;
        this.roundTimeTextBox.Enabled = false;
        this.roundModeCheckBox.Enabled = false;
        this.retryTextBox2.Enabled = false;
        this.colorTextBox.Enabled = false;
        this.oneVSModeRadioButton.Enabled = false;
        this.allVSModeRadioButton.Enabled = false;
        this.p1ModeRadioButton.Enabled = false;
        this.p2ModeRadioButton.Enabled = false;
        this.bothModeRadioButton.Enabled = false;
        this.charListTextBox.Enabled = false;
        this.autoButton.Enabled = false;
      }
    }

    private void enableAutoModecheckBox_CheckedChanged(object sender, EventArgs e)
    {
      Control autoModecheckBox = (Control) this.enableAutoModecheckBox;
      if (autoModecheckBox != null && autoModecheckBox.Cursor == Cursors.Help)
      {
        this.enableAutoModecheckBox.CheckedChanged -= new EventHandler(this.enableAutoModecheckBox_CheckedChanged);
        this.enableAutoModecheckBox.Checked = !this.enableAutoModecheckBox.Checked;
        this.enableAutoModecheckBox.CheckedChanged += new EventHandler(this.enableAutoModecheckBox_CheckedChanged);
        string text = "Enables the continuous battle mode." + Environment.NewLine + Environment.NewLine + "Continuous battle mode is for making one specified character face against others." + Environment.NewLine + "This is similar to survival mode, however, this mode isn't over until all opponents have been fought." + Environment.NewLine + "Typically, this mode would be used to test the functionality of a character before releasing it to the public." + Environment.NewLine + Environment.NewLine + "[Special features of this mode]" + Environment.NewLine + "In this mode, the matches will keep operating regardless of any crashes that may occur in mugen." + Environment.NewLine + "This application has been designed to observe and register the error messages, state numbers, helpers and" + Environment.NewLine + "other information during a crash. Mainly, to be left unattended." + Environment.NewLine + "(This mode can be paired up with video capturing software according to hardware specifications.)" + Environment.NewLine + Environment.NewLine + "Additionally, even if a rare occurence of the timerfreeze controller may appear, the rounds can still be fixed" + Environment.NewLine + "to have a real time limit." + Environment.NewLine + "(Note: If this were to happen, the rounds would be forced to end.)" + Environment.NewLine + Environment.NewLine + "By the same token, this function prevents eternal intros or any other mishaps that may not let the round \nend naturally." + Environment.NewLine + "(Note: Since the rounds are ended forcefully unexpected behavior may occur.)" + Environment.NewLine + Environment.NewLine;
        this.toolTip1.Hide((IWin32Window) autoModecheckBox);
        this.toolTip1.IsBalloon = false;
        this.toolTip1.IsBalloon = true;
        this.toolTip1.SetToolTip(autoModecheckBox, " ");
        this.toolTip1.Show(text, (IWin32Window) autoModecheckBox, autoModecheckBox.Width / 2, autoModecheckBox.Height / 2);
      }
      else
      {
        this.EnableAutoModeUI();
        this.autoModeRadioButton.Enabled = this.enableAutoModecheckBox.Checked;
      }
    }

    private void autoButton_Click(object sender, EventArgs e)
    {
      Control autoButton = (Control) this.autoButton;
      if (autoButton != null && autoButton.Cursor == Cursors.Help)
      {
        string text = "Imports characters from the select.def file specified in the 'Basic settings' tab." + Environment.NewLine + "(Note: When using this settings, any unsaved information on the list will be deleted.)" + Environment.NewLine;
        this.toolTip1.Hide((IWin32Window) autoButton);
        this.toolTip1.IsBalloon = false;
        this.toolTip1.IsBalloon = true;
        this.toolTip1.SetToolTip(autoButton, " ");
        this.toolTip1.Show(text, (IWin32Window) autoButton, autoButton.Width / 2, autoButton.Height / 2);
      }
      else if (this.selectDefTextBox.Text == "" || !File.Exists(this.selectDefTextBox.Text))
      {
        int num = (int) MessageBox.Show("Please specify the location of your select.def file in the 'Basic settings' tab.", "Swiss Army Knife");
      }
      else
      {
        if (MessageBox.Show("This will import characters specified in your select.def file. \nThe character list will be remade and any unsaved information will be deleted, do you wish to proceed?", "Swiss Army Knife", MessageBoxButtons.OKCancel) != DialogResult.OK)
          return;
        MugenProfile currentProfile = ProfileManager.MainObj().GetCurrentProfile();
        if (currentProfile == null)
          return;
        string charListRawData = currentProfile.GetCharListRawData(this.selectDefTextBox.Text);
        if (charListRawData == null || !(charListRawData != ""))
          return;
        this.charListTextBox.Text = "; Specify the characters used for continuous battle mode." + Environment.NewLine + ";" + Environment.NewLine + Environment.NewLine + charListRawData;
      }
    }

    private void ProfileConfigPanel_Load(object sender, EventArgs e)
    {
      this.helpCheckBox.Image = (Image) SystemIcons.Question.ToBitmap();
      this.LoadProfileConfig();
    }

    private void ProfileConfigPanel_FormClosing(object sender, FormClosingEventArgs e)
    {
    }

    private void helpCheckBox_CheckedChanged(object sender, EventArgs e)
    {
      this.toolTip1.Hide((IWin32Window) this);
      this.toolTip1.SetToolTip((Control) this.helpCheckBox, " ");
      this.toolTip1.IsBalloon = false;
      this.toolTip1.IsBalloon = true;
      if (!this._isHelpMode)
      {
        this._isHelpMode = true;
        this.Cursor = Cursors.Help;
        this.tabControl1.Cursor = Cursors.Help;
        this.tabPage1.Cursor = Cursors.Help;
        this.tabPage2.Cursor = Cursors.Help;
        this.profileNameTextBox.Cursor = Cursors.Help;
        this.profileNameLabel.Cursor = Cursors.Help;
        this.mugenExeTextBox.Cursor = Cursors.Help;
        this.mugenExeLabel.Cursor = Cursors.Help;
        this.selectDefTextBox.Cursor = Cursors.Help;
        this.selectDefLabel.Cursor = Cursors.Help;
        this.cmdLineTextBox.Cursor = Cursors.Help;
        this.cmdLineLabel.Cursor = Cursors.Help;
        this.enableDebugCheckBox.Cursor = Cursors.Help;
        this.enableSpeedUpCheckBox.Cursor = Cursors.Help;
        this.enableSkipModeCheckBox.Cursor = Cursors.Help;
        this.enableAutoModecheckBox.Cursor = Cursors.Help;
        this.roundTextBox.Cursor = Cursors.Help;
        this.roundTextLabel.Cursor = Cursors.Help;
        this.roundTimeTextBox.Cursor = Cursors.Help;
        this.roundTimeLabel.Cursor = Cursors.Help;
        this.retryTextBox2.Cursor = Cursors.Help;
        this.retryTextLabel.Cursor = Cursors.Help;
        this.colorTextBox.Cursor = Cursors.Help;
        this.colorLabel.Cursor = Cursors.Help;
        this.modeLabel.Cursor = Cursors.Help;
        this.oneVSModeRadioButton.Cursor = Cursors.Help;
        this.allVSModeRadioButton.Cursor = Cursors.Help;
        this.teamSideLabel.Cursor = Cursors.Help;
        this.p1ModeRadioButton.Cursor = Cursors.Help;
        this.p2ModeRadioButton.Cursor = Cursors.Help;
        this.bothModeRadioButton.Cursor = Cursors.Help;
        this.charListTextBox.Cursor = Cursors.Help;
        this.autoButton.Cursor = Cursors.Help;
        this.normalModeRadioButton.Cursor = Cursors.Help;
        this.quickModeRadioButton.Cursor = Cursors.Help;
        this.autoModeRadioButton.Cursor = Cursors.Help;
        this.autoModeRadioButton.Enabled = true;
        this.okButton.Cursor = Cursors.Help;
        this.okButton.Enabled = false;
        this.EnableAutoModeUI();
        this.toolTip1.Show("Help has been activated, please click on the item you wish to know more information about." + Environment.NewLine + "If the help mode ends unexpectedly, feel free to click the Help button again to use it.", (IWin32Window) this.helpCheckBox, this.helpCheckBox.Width / 2, this.helpCheckBox.Height / 2, 5000);
      }
      else
      {
        this._isHelpMode = false;
        this.Cursor = Cursors.Default;
        this.tabControl1.Cursor = Cursors.Default;
        this.tabPage1.Cursor = Cursors.Default;
        this.tabPage2.Cursor = Cursors.Default;
        this.profileNameTextBox.Cursor = Cursors.Default;
        this.profileNameLabel.Cursor = Cursors.Default;
        this.mugenExeTextBox.Cursor = Cursors.Default;
        this.mugenExeLabel.Cursor = Cursors.Default;
        this.selectDefTextBox.Cursor = Cursors.Default;
        this.selectDefLabel.Cursor = Cursors.Default;
        this.cmdLineTextBox.Cursor = Cursors.Default;
        this.cmdLineLabel.Cursor = Cursors.Default;
        this.enableDebugCheckBox.Cursor = Cursors.Default;
        this.enableSpeedUpCheckBox.Cursor = Cursors.Default;
        this.enableSkipModeCheckBox.Cursor = Cursors.Default;
        this.enableAutoModecheckBox.Cursor = Cursors.Default;
        this.roundTextBox.Cursor = Cursors.Default;
        this.roundTextLabel.Cursor = Cursors.Default;
        this.roundTimeTextBox.Cursor = Cursors.Default;
        this.roundTimeLabel.Cursor = Cursors.Default;
        this.retryTextBox2.Cursor = Cursors.Default;
        this.retryTextLabel.Cursor = Cursors.Default;
        this.colorTextBox.Cursor = Cursors.Default;
        this.colorLabel.Cursor = Cursors.Default;
        this.modeLabel.Cursor = Cursors.Default;
        this.oneVSModeRadioButton.Cursor = Cursors.Default;
        this.allVSModeRadioButton.Cursor = Cursors.Default;
        this.teamSideLabel.Cursor = Cursors.Default;
        this.p1ModeRadioButton.Cursor = Cursors.Default;
        this.p2ModeRadioButton.Cursor = Cursors.Default;
        this.bothModeRadioButton.Cursor = Cursors.Default;
        this.charListTextBox.Cursor = Cursors.Default;
        this.autoButton.Cursor = Cursors.Default;
        this.normalModeRadioButton.Cursor = Cursors.Default;
        this.quickModeRadioButton.Cursor = Cursors.Default;
        this.autoModeRadioButton.Cursor = Cursors.Default;
        if (this.enableAutoModecheckBox.Checked)
          this.autoModeRadioButton.Enabled = true;
        else
          this.autoModeRadioButton.Enabled = false;
        this.okButton.Cursor = Cursors.Default;
        this.okButton.Enabled = true;
        this.EnableAutoModeUI();
        this.toolTip1.Show("The help button.", (IWin32Window) this.helpCheckBox, this.helpCheckBox.Width / 2, this.helpCheckBox.Height / 2, 1000);
      }
    }

    private void ProfileConfigPanel_Deactivate(object sender, EventArgs e)
    {
      if (!(this.Cursor == Cursors.Help))
        return;
      this.toolTip1.Hide((IWin32Window) this);
    }

    private void tabControl1_Click(object sender, EventArgs e)
    {
      Control tabControl1 = (Control) this.tabControl1;
      if (tabControl1 == null || !(tabControl1.Cursor == Cursors.Help))
        return;
      this.toolTip1.Hide((IWin32Window) tabControl1);
      this.toolTip1.IsBalloon = false;
      this.toolTip1.IsBalloon = true;
      this.toolTip1.SetToolTip(tabControl1, " ");
      if (this.tabControl1.SelectedIndex == 0)
        this.toolTip1.Show("The basic settings to run mugen with." + Environment.NewLine + "Specifying the location of the mugen.exe file and a profile name are enough to register a new profile." + Environment.NewLine, (IWin32Window) tabControl1, 10, 10);
      else
        this.toolTip1.Show("A function to help during character creation." + Environment.NewLine + "In this version, this 'Continuous battle' mode has been added." + Environment.NewLine, (IWin32Window) tabControl1, 100, 10);
    }

    private void tabPage1_Click(object sender, EventArgs e)
    {
    }

    private void tabPage2_Click(object sender, EventArgs e)
    {
    }

    private void profileNameLabel_Click(object sender, EventArgs e) => this.profileNameTextBox_Click(sender, e);

    private void profileNameTextBox_Click(object sender, EventArgs e)
    {
      Control profileNameTextBox = (Control) this.profileNameTextBox;
      if (profileNameTextBox == null || !(profileNameTextBox.Cursor == Cursors.Help))
        return;
      string text = "Specifies a name for this profile." + Environment.NewLine + "Example: 'My created characters' or 'Joke characters only'" + Environment.NewLine + "This is required, so you will not be able to register a profile without it." + Environment.NewLine;
      this.toolTip1.Hide((IWin32Window) profileNameTextBox);
      this.toolTip1.IsBalloon = false;
      this.toolTip1.IsBalloon = true;
      this.toolTip1.SetToolTip(profileNameTextBox, " ");
      this.toolTip1.Show(text, (IWin32Window) profileNameTextBox, profileNameTextBox.Width / 2, profileNameTextBox.Height / 2);
    }

    private void mugenExeLabel_Click(object sender, EventArgs e) => this.mugenExeTextBox_Click(sender, e);

    private void mugenExeTextBox_Click(object sender, EventArgs e)
    {
      Control mugenExeTextBox = (Control) this.mugenExeTextBox;
      if (mugenExeTextBox == null || !(mugenExeTextBox.Cursor == Cursors.Help))
        return;
      string text = "Specify the full filepath to your mugen executable file here." + Environment.NewLine + "This is required, so you will not be able to register a profile without it." + Environment.NewLine + "(Note: The use of *.bat batch files is not supported)" + Environment.NewLine;
      this.toolTip1.Hide((IWin32Window) mugenExeTextBox);
      this.toolTip1.IsBalloon = false;
      this.toolTip1.IsBalloon = true;
      this.toolTip1.SetToolTip(mugenExeTextBox, " ");
      this.toolTip1.Show(text, (IWin32Window) mugenExeTextBox, mugenExeTextBox.Width / 2, mugenExeTextBox.Height / 2);
    }

    private void selectDefLabel_Click(object sender, EventArgs e) => this.selectDefTextBox_Click(sender, e);

    private void selectDefTextBox_Click(object sender, EventArgs e)
    {
      Control selectDefTextBox = (Control) this.selectDefTextBox;
      if (selectDefTextBox == null || !(selectDefTextBox.Cursor == Cursors.Help))
        return;
      string text = "Specify the full filepath to your select.def file here." + Environment.NewLine + "This is not required, but certain functions can make use of this file." + Environment.NewLine + "Specifying the location of this file is recommended." + Environment.NewLine;
      this.toolTip1.Hide((IWin32Window) selectDefTextBox);
      this.toolTip1.IsBalloon = false;
      this.toolTip1.IsBalloon = true;
      this.toolTip1.SetToolTip(selectDefTextBox, " ");
      this.toolTip1.Show(text, (IWin32Window) selectDefTextBox, selectDefTextBox.Width / 2, selectDefTextBox.Height / 2);
    }

    private void cmdLineLabel_Click(object sender, EventArgs e) => this.cmdLineTextBox_Click(sender, e);

    private void cmdLineTextBox_Click(object sender, EventArgs e)
    {
      Control cmdLineTextBox = (Control) this.cmdLineTextBox;
      if (cmdLineTextBox == null || !(cmdLineTextBox.Cursor == Cursors.Help))
        return;
      string text = "Specifies command line options for running mugen." + Environment.NewLine + "Example: -r kfm" + Environment.NewLine + "This is not a required setting, but it can be used to specify a screenpack or an addon." + Environment.NewLine;
      this.toolTip1.Hide((IWin32Window) cmdLineTextBox);
      this.toolTip1.IsBalloon = false;
      this.toolTip1.IsBalloon = true;
      this.toolTip1.SetToolTip(cmdLineTextBox, " ");
      this.toolTip1.Show(text, (IWin32Window) cmdLineTextBox, cmdLineTextBox.Width / 2, cmdLineTextBox.Height / 2);
    }

    private void roundTextLabel_Click(object sender, EventArgs e) => this.roundTextBox_Click(sender, e);

    private void roundTextBox_Click(object sender, EventArgs e)
    {
      Control roundTextBox = (Control) this.roundTextBox;
      if (roundTextBox == null || !(roundTextBox.Cursor == Cursors.Help))
        return;
      string text = "Sets the number of rounds for the matches." + Environment.NewLine + "The default setting is 1 round." + Environment.NewLine;
      this.toolTip1.Hide((IWin32Window) roundTextBox);
      this.toolTip1.IsBalloon = false;
      this.toolTip1.IsBalloon = true;
      this.toolTip1.SetToolTip(roundTextBox, " ");
      this.toolTip1.Show(text, (IWin32Window) roundTextBox, roundTextBox.Width / 2, roundTextBox.Height / 2);
    }

    private void roundTimeLabel_Click(object sender, EventArgs e) => this.roundTimeTextBox_Click(sender, e);

    private void roundTimeTextBox_Click(object sender, EventArgs e)
    {
      Control roundTimeTextBox = (Control) this.roundTimeTextBox;
      if (roundTimeTextBox == null || !(roundTimeTextBox.Cursor == Cursors.Help))
        return;
      string text = "Sets the maximum possible time for one round." + Environment.NewLine + "If a round exceeds the maximum alloted time, then it will be forcefully terminated." + Environment.NewLine + "If this is the case, the round will be treated as a draw regardless of life or remaining time." + Environment.NewLine + "The default setting is 10 minutes." + Environment.NewLine + "(Note: The specified time is real time, so regardless of timerfreeze or any similar effects," + Environment.NewLine + "the round will end according to elapsed real time.)" + Environment.NewLine;
      this.toolTip1.Hide((IWin32Window) roundTimeTextBox);
      this.toolTip1.IsBalloon = false;
      this.toolTip1.IsBalloon = true;
      this.toolTip1.SetToolTip(roundTimeTextBox, " ");
      this.toolTip1.Show(text, (IWin32Window) roundTimeTextBox, roundTimeTextBox.Width / 2, roundTimeTextBox.Height / 2);
    }

    private void retryTextLabel_Click(object sender, EventArgs e) => this.retryTextBox2_Click(sender, e);

    private void retryTextBox2_Click(object sender, EventArgs e)
    {
      Control retryTextBox2 = (Control) this.retryTextBox2;
      if (retryTextBox2 == null || !(retryTextBox2.Cursor == Cursors.Help))
        return;
      string text = "Specifies how many times to restart the match in case of a crash." + Environment.NewLine + "If all the attempts result in a crash、" + Environment.NewLine + "the plan will switch to the next scheduled match." + Environment.NewLine + "The default setting is 0 (no retries)." + Environment.NewLine;
      this.toolTip1.Hide((IWin32Window) retryTextBox2);
      this.toolTip1.IsBalloon = false;
      this.toolTip1.IsBalloon = true;
      this.toolTip1.SetToolTip(retryTextBox2, " ");
      this.toolTip1.Show(text, (IWin32Window) retryTextBox2, retryTextBox2.Width / 2, retryTextBox2.Height / 2);
    }

    private void colorLabel_Click(object sender, EventArgs e) => this.colorTextBox_Click(sender, e);

    private void colorTextBox_Click(object sender, EventArgs e)
    {
      Control colorTextBox = (Control) this.colorTextBox;
      if (colorTextBox == null || !(colorTextBox.Cursor == Cursors.Help))
        return;
      string text = "Specifies the default color for opponents." + Environment.NewLine + "If your opponents have a color selectable mode, then it might be \n a good idea to select an appropriate number." + Environment.NewLine + "The default setting is '1', the first color." + Environment.NewLine;
      this.toolTip1.Hide((IWin32Window) colorTextBox);
      this.toolTip1.IsBalloon = false;
      this.toolTip1.IsBalloon = true;
      this.toolTip1.SetToolTip(colorTextBox, " ");
      this.toolTip1.Show(text, (IWin32Window) colorTextBox, colorTextBox.Width / 2, colorTextBox.Height / 2);
    }

    private void charListTextBox_Click(object sender, EventArgs e)
    {
      Control charListTextBox = (Control) this.charListTextBox;
      if (charListTextBox == null || !(charListTextBox.Cursor == Cursors.Help))
        return;
      string text = "Specifies the main character for continuous battle mode." + Environment.NewLine + "Please specify characters the same way you would in your own select.def file." + Environment.NewLine + "(Just like in the def file, you can write down comments using the semicolon ';' character." + Environment.NewLine + "　Example: kfm     ; -- Kung Fu Man" + Environment.NewLine + "         kfm\\Evil_KFM.def     ; -- EVIL Kung Fu Man" + Environment.NewLine + "It is also possible to import characters already specified in your select.def file by using the \n 'Read characters from the select.def file' option." + Environment.NewLine + Environment.NewLine + "The main character is specified in the P1 side and the rest are specified in the P2 side." + Environment.NewLine + "As an example, consider the following setup:" + Environment.NewLine + "    kfm, 1" + Environment.NewLine + "    kfm, 2" + Environment.NewLine + "    kfm, 3" + Environment.NewLine + "    kfm, 4" + Environment.NewLine + "As the order implies, the first character will be the main in P1 side, and the rest  \n will be set in the P2 side" + Environment.NewLine + Environment.NewLine + "Specifying a number with a comma after the character name will allow you to select a palette." + Environment.NewLine + "　Example: kfm, 3     ; -- Kung Fu Man with palette 3" + Environment.NewLine + "　　  kfm\\Evil_KFM.def, 2     ; -- EVIL Kung Fu Man with palette 2" + Environment.NewLine + "If no color is specified, the default color specified above will be used." + Environment.NewLine + Environment.NewLine + "(Warning: this mode makes use of the quick vs. function within mugen, selecting a non-existant \n color will result in a crash.)" + Environment.NewLine + Environment.NewLine;
      this.toolTip1.Hide((IWin32Window) charListTextBox);
      this.toolTip1.IsBalloon = false;
      this.toolTip1.IsBalloon = true;
      this.toolTip1.SetToolTip(charListTextBox, " ");
      this.toolTip1.Show(text, (IWin32Window) charListTextBox, charListTextBox.Width / 2, charListTextBox.Height / 2);
    }

    private void allSelToolStripMenuItem_Click(object sender, EventArgs e)
    {
      if (this.Cursor == Cursors.Help)
        return;
      this.charListTextBox.Focus();
      this.charListTextBox.SelectAll();
    }

    private void copyToolStripMenuItem_Click(object sender, EventArgs e)
    {
      if (this.Cursor == Cursors.Help || this.charListTextBox.SelectedText == null || !(this.charListTextBox.SelectedText != ""))
        return;
      Clipboard.SetText(this.charListTextBox.SelectedText);
    }

    private void pasteToolStripMenuItem_Click(object sender, EventArgs e)
    {
      if (this.Cursor == Cursors.Help)
        return;
      this.charListTextBox.SelectedText = Clipboard.GetText();
    }

    private void helpCheckBox_MouseHover(object sender, EventArgs e)
    {
      if (this.Cursor == Cursors.Help)
        return;
      this.toolTip1.Hide((IWin32Window) this);
      this.toolTip1.IsBalloon = false;
      this.toolTip1.SetToolTip((Control) this, " ");
      this.toolTip1.Show("Help button", (IWin32Window) this, this.Width - this.helpCheckBox.Width, this.helpCheckBox.Height / 2, 1000);
    }

    private void normalModeRadioButton_CheckedChanged(object sender, EventArgs e)
    {
      MugenProfile currentProfile = ProfileManager.MainObj().GetCurrentProfile();
      if (currentProfile == null)
        return;
      Control normalModeRadioButton = (Control) this.normalModeRadioButton;
      if (normalModeRadioButton == null || !(normalModeRadioButton.Cursor == Cursors.Help))
        return;
      switch (currentProfile.GetDefaultGameMode())
      {
        case MugenProfile.GameMode.NORMAL:
          this.normalModeRadioButton.Checked = true;
          break;
        case MugenProfile.GameMode.QUICK_VS:
          this.quickModeRadioButton.Checked = true;
          break;
        case MugenProfile.GameMode.AUTO_MODE:
          this.autoModeRadioButton.Checked = true;
          break;
      }
    }

    private void quickModeRadioButton_CheckedChanged(object sender, EventArgs e)
    {
      MugenProfile currentProfile = ProfileManager.MainObj().GetCurrentProfile();
      if (currentProfile == null)
        return;
      Control quickModeRadioButton = (Control) this.quickModeRadioButton;
      if (quickModeRadioButton == null || !(quickModeRadioButton.Cursor == Cursors.Help))
        return;
      switch (currentProfile.GetDefaultGameMode())
      {
        case MugenProfile.GameMode.NORMAL:
          this.normalModeRadioButton.Checked = true;
          break;
        case MugenProfile.GameMode.QUICK_VS:
          this.quickModeRadioButton.Checked = true;
          break;
        case MugenProfile.GameMode.AUTO_MODE:
          this.autoModeRadioButton.Checked = true;
          break;
      }
    }

    private void autoModeRadioButton_CheckedChanged(object sender, EventArgs e)
    {
      MugenProfile currentProfile = ProfileManager.MainObj().GetCurrentProfile();
      if (currentProfile == null)
        return;
      Control autoModeRadioButton = (Control) this.autoModeRadioButton;
      if (autoModeRadioButton == null || !(autoModeRadioButton.Cursor == Cursors.Help))
        return;
      switch (currentProfile.GetDefaultGameMode())
      {
        case MugenProfile.GameMode.NORMAL:
          this.normalModeRadioButton.Checked = true;
          break;
        case MugenProfile.GameMode.QUICK_VS:
          this.quickModeRadioButton.Checked = true;
          break;
        case MugenProfile.GameMode.AUTO_MODE:
          this.autoModeRadioButton.Checked = true;
          break;
      }
    }

    private void normalModeRadioButton_MouseClick(object sender, MouseEventArgs e)
    {
      Control normalModeRadioButton = (Control) this.normalModeRadioButton;
      if (normalModeRadioButton == null || !(normalModeRadioButton.Cursor == Cursors.Help))
        return;
      string text = "Starts mugen normally." + Environment.NewLine;
      this.toolTip1.Hide((IWin32Window) normalModeRadioButton);
      this.toolTip1.IsBalloon = false;
      this.toolTip1.IsBalloon = true;
      this.toolTip1.SetToolTip(normalModeRadioButton, " ");
      this.toolTip1.Show(text, (IWin32Window) normalModeRadioButton, normalModeRadioButton.Width / 2, normalModeRadioButton.Height / 2);
    }

    private void quickModeRadioButton_MouseClick(object sender, MouseEventArgs e)
    {
      Control quickModeRadioButton = (Control) this.quickModeRadioButton;
      if (quickModeRadioButton == null || !(quickModeRadioButton.Cursor == Cursors.Help))
        return;
      string text = "Starts mugen in quick vs. mode." + Environment.NewLine;
      this.toolTip1.Hide((IWin32Window) quickModeRadioButton);
      this.toolTip1.IsBalloon = false;
      this.toolTip1.IsBalloon = true;
      this.toolTip1.SetToolTip(quickModeRadioButton, " ");
      this.toolTip1.Show(text, (IWin32Window) quickModeRadioButton, quickModeRadioButton.Width / 2, quickModeRadioButton.Height / 2);
    }

    private void autoModeRadioButton_MouseClick(object sender, MouseEventArgs e)
    {
      Control autoModeRadioButton = (Control) this.autoModeRadioButton;
      if (autoModeRadioButton == null || !(autoModeRadioButton.Cursor == Cursors.Help))
        return;
      string text = "Stars mugen in continuous battle mode." + Environment.NewLine;
      this.toolTip1.Hide((IWin32Window) autoModeRadioButton);
      this.toolTip1.IsBalloon = false;
      this.toolTip1.IsBalloon = true;
      this.toolTip1.SetToolTip(autoModeRadioButton, " ");
      this.toolTip1.Show(text, (IWin32Window) autoModeRadioButton, autoModeRadioButton.Width / 2, autoModeRadioButton.Height / 2);
    }

    private void p1ModeRadioButton_CheckedChanged(object sender, EventArgs e)
    {
      Control p1ModeRadioButton = (Control) this.p1ModeRadioButton;
      if (p1ModeRadioButton == null || !(p1ModeRadioButton.Cursor == Cursors.Help))
        return;
      this.p1ModeRadioButton.CheckedChanged -= new EventHandler(this.p1ModeRadioButton_CheckedChanged);
      this.p1ModeRadioButton.Checked = true;
      this.p1ModeRadioButton.CheckedChanged += new EventHandler(this.p1ModeRadioButton_CheckedChanged);
      string text = "Makes the specified character play in the P1 side only." + Environment.NewLine;
      this.toolTip1.Hide((IWin32Window) p1ModeRadioButton);
      this.toolTip1.IsBalloon = false;
      this.toolTip1.IsBalloon = true;
      this.toolTip1.SetToolTip(p1ModeRadioButton, " ");
      this.toolTip1.Show(text, (IWin32Window) p1ModeRadioButton, p1ModeRadioButton.Width / 2, p1ModeRadioButton.Height / 2);
    }

    private void p2ModeRadioButton_CheckedChanged(object sender, EventArgs e)
    {
      Control p2ModeRadioButton = (Control) this.p2ModeRadioButton;
      if (p2ModeRadioButton == null || !(p2ModeRadioButton.Cursor == Cursors.Help))
        return;
      this.p2ModeRadioButton.CheckedChanged -= new EventHandler(this.p2ModeRadioButton_CheckedChanged);
      this.p2ModeRadioButton.Checked = false;
      this.p2ModeRadioButton.CheckedChanged += new EventHandler(this.p2ModeRadioButton_CheckedChanged);
      string text = "Makes the specified character play in the P2 side only." + Environment.NewLine;
      this.toolTip1.Hide((IWin32Window) p2ModeRadioButton);
      this.toolTip1.IsBalloon = false;
      this.toolTip1.IsBalloon = true;
      this.toolTip1.SetToolTip(p2ModeRadioButton, " ");
      this.toolTip1.Show(text, (IWin32Window) p2ModeRadioButton, p2ModeRadioButton.Width / 2, p2ModeRadioButton.Height / 2);
    }

    private void bothModeRadioButton_CheckedChanged(object sender, EventArgs e)
    {
      Control bothModeRadioButton = (Control) this.bothModeRadioButton;
      if (bothModeRadioButton == null || !(bothModeRadioButton.Cursor == Cursors.Help))
        return;
      this.bothModeRadioButton.CheckedChanged -= new EventHandler(this.bothModeRadioButton_CheckedChanged);
      this.bothModeRadioButton.Checked = false;
      this.bothModeRadioButton.CheckedChanged += new EventHandler(this.bothModeRadioButton_CheckedChanged);
      string text = "This option makes the specified character alternate play in both P1 and P2 sides." + Environment.NewLine;
      this.toolTip1.Hide((IWin32Window) bothModeRadioButton);
      this.toolTip1.IsBalloon = false;
      this.toolTip1.IsBalloon = true;
      this.toolTip1.SetToolTip(bothModeRadioButton, " ");
      this.toolTip1.Show(text, (IWin32Window) bothModeRadioButton, bothModeRadioButton.Width / 2, bothModeRadioButton.Height / 2);
    }

    private void teamSideLabel_Click(object sender, EventArgs e)
    {
      Control teamSideLabel = (Control) this.teamSideLabel;
      if (teamSideLabel == null || !(teamSideLabel.Cursor == Cursors.Help))
        return;
      string text = "These options specify which side the selected character will play in." + Environment.NewLine;
      this.toolTip1.Hide((IWin32Window) teamSideLabel);
      this.toolTip1.IsBalloon = false;
      this.toolTip1.IsBalloon = true;
      this.toolTip1.SetToolTip(teamSideLabel, " ");
      this.toolTip1.Show(text, (IWin32Window) teamSideLabel, teamSideLabel.Width / 2, teamSideLabel.Height / 2);
    }

    private void roundModeCheckBox_CheckedChanged(object sender, EventArgs e)
    {
      Control roundModeCheckBox = (Control) this.roundModeCheckBox;
      if (roundModeCheckBox != null && roundModeCheckBox.Cursor == Cursors.Help)
      {
        this.roundModeCheckBox.CheckedChanged -= new EventHandler(this.roundModeCheckBox_CheckedChanged);
        this.roundModeCheckBox.Checked = !this.roundModeCheckBox.Checked;
        this.roundModeCheckBox.CheckedChanged += new EventHandler(this.roundModeCheckBox_CheckedChanged);
        string text = "This changes the winning conditions of one match." + Environment.NewLine + "Normally the game will proceed until one player wins the specified number of rounds but," + Environment.NewLine + "with this option the matches will automatically stop after the specified number of rounds." + Environment.NewLine + "(With this option, a round will be counted even if said round ends in a draw)." + Environment.NewLine;
        this.toolTip1.Hide((IWin32Window) roundModeCheckBox);
        this.toolTip1.IsBalloon = false;
        this.toolTip1.IsBalloon = true;
        this.toolTip1.SetToolTip(roundModeCheckBox, " ");
        this.toolTip1.Show(text, (IWin32Window) roundModeCheckBox, roundModeCheckBox.Width / 2, roundModeCheckBox.Height / 2);
      }
      else if (this.roundModeCheckBox.Checked)
        this.label9.Text = "rounds and end";
      else
        this.label9.Text = "round matches";
    }

    private void modeLabel_Click(object sender, EventArgs e)
    {
      Control modeLabel = (Control) this.modeLabel;
      if (modeLabel == null || !(modeLabel.Cursor == Cursors.Help))
        return;
      string text = "These are the settings for creating the matches." + Environment.NewLine;
      this.toolTip1.Hide((IWin32Window) modeLabel);
      this.toolTip1.IsBalloon = false;
      this.toolTip1.IsBalloon = true;
      this.toolTip1.SetToolTip(modeLabel, " ");
      this.toolTip1.Show(text, (IWin32Window) modeLabel, modeLabel.Width / 2, modeLabel.Height / 2);
    }

    private void oneVSModeRadioButton_CheckedChanged(object sender, EventArgs e)
    {
      Control vsModeRadioButton = (Control) this.oneVSModeRadioButton;
      if (vsModeRadioButton == null || !(vsModeRadioButton.Cursor == Cursors.Help))
        return;
      this.oneVSModeRadioButton.CheckedChanged -= new EventHandler(this.oneVSModeRadioButton_CheckedChanged);
      this.oneVSModeRadioButton.Checked = true;
      this.oneVSModeRadioButton.CheckedChanged += new EventHandler(this.oneVSModeRadioButton_CheckedChanged);
      string text = "This option will match one character against the rest." + Environment.NewLine;
      this.toolTip1.Hide((IWin32Window) vsModeRadioButton);
      this.toolTip1.IsBalloon = false;
      this.toolTip1.IsBalloon = true;
      this.toolTip1.SetToolTip(vsModeRadioButton, " ");
      this.toolTip1.Show(text, (IWin32Window) vsModeRadioButton, vsModeRadioButton.Width / 2, vsModeRadioButton.Height / 2);
    }

    private void allVSModeRadioButton_CheckedChanged(object sender, EventArgs e)
    {
      Control vsModeRadioButton = (Control) this.allVSModeRadioButton;
      if (vsModeRadioButton == null || !(vsModeRadioButton.Cursor == Cursors.Help))
        return;
      this.allVSModeRadioButton.CheckedChanged -= new EventHandler(this.allVSModeRadioButton_CheckedChanged);
      this.allVSModeRadioButton.Checked = false;
      this.allVSModeRadioButton.CheckedChanged += new EventHandler(this.allVSModeRadioButton_CheckedChanged);
      string text = "This option will create matches between all the characters." + Environment.NewLine;
      this.toolTip1.Hide((IWin32Window) vsModeRadioButton);
      this.toolTip1.IsBalloon = false;
      this.toolTip1.IsBalloon = true;
      this.toolTip1.SetToolTip(vsModeRadioButton, " ");
      this.toolTip1.Show(text, (IWin32Window) vsModeRadioButton, vsModeRadioButton.Width / 2, vsModeRadioButton.Height / 2);
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
      ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (ProfileConfigPanel));
      this.cancelButton = new Button();
      this.okButton = new Button();
      this.profileNameLabel = new Label();
      this.profileNameTextBox = new TextBox();
      this.tabControl1 = new TabControl();
      this.tabPage1 = new TabPage();
      this.groupBox1 = new GroupBox();
      this.groupBox3 = new GroupBox();
      this.textBox1 = new TextBox();
      this.autoModeRadioButton = new RadioButton();
      this.quickModeRadioButton = new RadioButton();
      this.normalModeRadioButton = new RadioButton();
      this.mugenModeLabel = new Label();
      this.textBox2 = new TextBox();
      this.label5 = new Label();
      this.enableExperimentalBreakpointsCheckbox = new CheckBox();
      this.enableSkipModeCheckBox = new CheckBox();
      this.enableSpeedUpCheckBox = new CheckBox();
      this.enableDebugCheckBox = new CheckBox();
      this.cmdLineTextBox = new TextBox();
      this.cmdLineLabel = new Label();
      this.openFileButton2 = new Button();
      this.selectDefTextBox = new TextBox();
      this.selectDefLabel = new Label();
      this.openFileButton1 = new Button();
      this.mugenExeTextBox = new TextBox();
      this.mugenExeLabel = new Label();
      this.tabPage2 = new TabPage();
      this.groupBox2 = new GroupBox();
      this.panel1 = new Panel();
      this.allVSModeRadioButton = new RadioButton();
      this.oneVSModeRadioButton = new RadioButton();
      this.modeLabel = new Label();
      this.roundModeCheckBox = new CheckBox();
      this.bothModeRadioButton = new RadioButton();
      this.p2ModeRadioButton = new RadioButton();
      this.p1ModeRadioButton = new RadioButton();
      this.teamSideLabel = new Label();
      this.autoButton = new Button();
      this.label13 = new Label();
      this.colorTextBox = new TextBox();
      this.label12 = new Label();
      this.roundTimeTextBox = new TextBox();
      this.label11 = new Label();
      this.colorLabel = new Label();
      this.retryTextBox2 = new TextBox();
      this.label9 = new Label();
      this.roundTextBox = new TextBox();
      this.roundTimeLabel = new Label();
      this.roundTextLabel = new Label();
      this.retryTextLabel = new Label();
      this.enableAutoModecheckBox = new CheckBox();
      this.charListTextBox = new TextBox();
      this.charContextMenuStrip = new ContextMenuStrip(this.components);
      this.allSelToolStripMenuItem = new ToolStripMenuItem();
      this.copyToolStripMenuItem = new ToolStripMenuItem();
      this.pasteToolStripMenuItem = new ToolStripMenuItem();
      this.mugenOpenFileDialog = new OpenFileDialog();
      this.selectOpenFileDialog = new OpenFileDialog();
      this.toolTip1 = new ToolTip(this.components);
      this.helpCheckBox = new CheckBox();
      this.tabControl1.SuspendLayout();
      this.tabPage1.SuspendLayout();
      this.groupBox1.SuspendLayout();
      this.tabPage2.SuspendLayout();
      this.groupBox2.SuspendLayout();
      this.panel1.SuspendLayout();
      this.charContextMenuStrip.SuspendLayout();
      this.SuspendLayout();
      this.cancelButton.Location = new Point(505, 487);
      this.cancelButton.Name = "cancelButton";
      this.cancelButton.Size = new Size(90, 28);
      this.cancelButton.TabIndex = 53;
      this.cancelButton.Text = "Cancel";
      this.cancelButton.UseVisualStyleBackColor = true;
      this.cancelButton.Click += new EventHandler(this.cancelButton_Click);
      this.okButton.Location = new Point(409, 487);
      this.okButton.Name = "okButton";
      this.okButton.Size = new Size(90, 28);
      this.okButton.TabIndex = 52;
      this.okButton.Text = "Save";
      this.okButton.UseVisualStyleBackColor = true;
      this.okButton.Click += new EventHandler(this.okButton_Click);
      this.profileNameLabel.AutoSize = true;
      this.profileNameLabel.Font = new Font("MS UI Gothic", 10f, FontStyle.Regular, GraphicsUnit.Point, (byte) 128);
      this.profileNameLabel.Location = new Point(16, 11);
      this.profileNameLabel.Name = "profileNameLabel";
      this.profileNameLabel.Size = new Size(108, 14);
      this.profileNameLabel.TabIndex = 4;
      this.profileNameLabel.Text = "Profile name:";
      this.profileNameLabel.Click += new EventHandler(this.profileNameLabel_Click);
      this.profileNameTextBox.Font = new Font("MS UI Gothic", 10f, FontStyle.Regular, GraphicsUnit.Point, (byte) 128);
      this.profileNameTextBox.Location = new Point(136, 8);
      this.profileNameTextBox.MaxLength = 32;
      this.profileNameTextBox.Name = "profileNameTextBox";
      this.profileNameTextBox.Size = new Size(283, 21);
      this.profileNameTextBox.TabIndex = 0;
      this.profileNameTextBox.Click += new EventHandler(this.profileNameTextBox_Click);
      this.tabControl1.Controls.Add((Control) this.tabPage1);
      this.tabControl1.Controls.Add((Control) this.tabPage2);
      this.tabControl1.Location = new Point(4, 35);
      this.tabControl1.Name = "tabControl1";
      this.tabControl1.SelectedIndex = 0;
      this.tabControl1.Size = new Size(616, 442);
      this.tabControl1.TabIndex = 24;
      this.tabControl1.Click += new EventHandler(this.tabControl1_Click);
      this.tabPage1.Controls.Add((Control) this.groupBox1);
      this.tabPage1.Location = new Point(4, 22);
      this.tabPage1.Name = "tabPage1";
      this.tabPage1.Padding = new Padding(3);
      this.tabPage1.Size = new Size(608, 416);
      this.tabPage1.TabIndex = 0;
      this.tabPage1.Text = "Basic settings";
      this.tabPage1.UseVisualStyleBackColor = true;
      this.tabPage1.Click += new EventHandler(this.tabPage1_Click);
      this.groupBox1.Controls.Add((Control) this.groupBox3);
      this.groupBox1.Controls.Add((Control) this.textBox1);
      this.groupBox1.Controls.Add((Control) this.autoModeRadioButton);
      this.groupBox1.Controls.Add((Control) this.quickModeRadioButton);
      this.groupBox1.Controls.Add((Control) this.normalModeRadioButton);
      this.groupBox1.Controls.Add((Control) this.mugenModeLabel);
      this.groupBox1.Controls.Add((Control) this.textBox2);
      this.groupBox1.Controls.Add((Control) this.label5);
      this.groupBox1.Controls.Add((Control) this.enableSkipModeCheckBox);
      this.groupBox1.Controls.Add((Control) this.enableSpeedUpCheckBox);
      this.groupBox1.Controls.Add((Control) this.enableDebugCheckBox);
      this.groupBox1.Controls.Add((Control) this.enableExperimentalBreakpointsCheckbox);
      this.groupBox1.Controls.Add((Control) this.cmdLineTextBox);
      this.groupBox1.Controls.Add((Control) this.cmdLineLabel);
      this.groupBox1.Controls.Add((Control) this.openFileButton2);
      this.groupBox1.Controls.Add((Control) this.selectDefTextBox);
      this.groupBox1.Controls.Add((Control) this.selectDefLabel);
      this.groupBox1.Controls.Add((Control) this.openFileButton1);
      this.groupBox1.Controls.Add((Control) this.mugenExeTextBox);
      this.groupBox1.Controls.Add((Control) this.mugenExeLabel);
      this.groupBox1.Location = new Point(3, 19);
      this.groupBox1.Name = "groupBox1";
      this.groupBox1.Size = new Size(601, 354);
      this.groupBox1.TabIndex = 1;
      this.groupBox1.TabStop = false;
      this.groupBox1.Text = "Mugen settings";
      this.groupBox3.Location = new Point(37, 228);
      this.groupBox3.Margin = new Padding(0);
      this.groupBox3.Name = "groupBox3";
      this.groupBox3.Padding = new Padding(0);
      this.groupBox3.Size = new Size(540, 4);
      this.groupBox3.TabIndex = 2;
      this.groupBox3.TabStop = false;
      this.textBox1.BackColor = SystemColors.Window;
      this.textBox1.BorderStyle = BorderStyle.None;
      this.textBox1.Cursor = Cursors.Default;
      this.textBox1.ImeMode = ImeMode.Disable;
      this.textBox1.Location = new Point(75, 294);
      this.textBox1.Multiline = true;
      this.textBox1.Name = "textBox1";
      this.textBox1.ReadOnly = true;
      this.textBox1.Size = new Size(432, 52);
      this.textBox1.TabIndex = 22;
      this.textBox1.TabStop = false;
      this.textBox1.Text = "The settings will be used when you click the 'Run mugen' button. \r\n\r\n(Note: the rest of these options can still be used manually from the 'More commands' menu";
      this.autoModeRadioButton.AutoSize = true;
      this.autoModeRadioButton.Enabled = false;
      this.autoModeRadioButton.Location = new Point(376, 262);
      this.autoModeRadioButton.Name = "autoModeRadioButton";
      this.autoModeRadioButton.Size = new Size(152, 16);
      this.autoModeRadioButton.TabIndex = 21;
      this.autoModeRadioButton.TabStop = true;
      this.autoModeRadioButton.Text = "Start in continuous battle mode";
      this.autoModeRadioButton.UseVisualStyleBackColor = true;
      this.autoModeRadioButton.CheckedChanged += new EventHandler(this.autoModeRadioButton_CheckedChanged);
      this.autoModeRadioButton.MouseClick += new MouseEventHandler(this.autoModeRadioButton_MouseClick);
      this.quickModeRadioButton.AutoSize = true;
      this.quickModeRadioButton.Location = new Point(208, 262);
      this.quickModeRadioButton.Name = "quickModeRadioButton";
      this.quickModeRadioButton.Size = new Size(151, 16);
      this.quickModeRadioButton.TabIndex = 20;
      this.quickModeRadioButton.TabStop = true;
      this.quickModeRadioButton.Text = "Start in quick vs. mode";
      this.quickModeRadioButton.UseVisualStyleBackColor = true;
      this.quickModeRadioButton.CheckedChanged += new EventHandler(this.quickModeRadioButton_CheckedChanged);
      this.quickModeRadioButton.MouseClick += new MouseEventHandler(this.quickModeRadioButton_MouseClick);
      this.normalModeRadioButton.AutoSize = true;
      this.normalModeRadioButton.Checked = true;
      this.normalModeRadioButton.Location = new Point(61, 262);
      this.normalModeRadioButton.Name = "normalModeRadioButton";
      this.normalModeRadioButton.Size = new Size(132, 16);
      this.normalModeRadioButton.TabIndex = 19;
      this.normalModeRadioButton.TabStop = true;
      this.normalModeRadioButton.Text = "Start normally";
      this.normalModeRadioButton.UseVisualStyleBackColor = true;
      this.normalModeRadioButton.CheckedChanged += new EventHandler(this.normalModeRadioButton_CheckedChanged);
      this.normalModeRadioButton.MouseClick += new MouseEventHandler(this.normalModeRadioButton_MouseClick);
      this.mugenModeLabel.AutoSize = true;
      this.mugenModeLabel.Location = new Point(22, 242);
      this.mugenModeLabel.Name = "mugenModeLabel";
      this.mugenModeLabel.Size = new Size(146, 12);
      this.mugenModeLabel.TabIndex = 18;
      this.mugenModeLabel.Text = "Mugen startup settings:";
      this.textBox2.BackColor = SystemColors.Window;
      this.textBox2.BorderStyle = BorderStyle.None;
      this.textBox2.Cursor = Cursors.Default;
      this.textBox2.ImeMode = ImeMode.Disable;
      this.textBox2.Location = new Point(168, 106);
      this.textBox2.Multiline = true;
      this.textBox2.Name = "textBox2";
      this.textBox2.ReadOnly = true;
      this.textBox2.Size = new Size(392, 72);
      this.textBox2.TabIndex = 17;
      this.textBox2.TabStop = false;
      this.textBox2.Text = "Make sure you specify the location of the real mugen executable. \r\n If you wish to use different lifebars or addons you may do so by specifying command line options. \r\n Example: If you wish to use the kfm motif then use: -r kfm \r\n";
      this.label5.AutoSize = true;
      this.label5.Location = new Point(22, 185);
      this.label5.Name = "label5";
      this.label5.Size = new Size(91, 24);
      this.label5.TabIndex = 16;
      this.label5.Text = "Additional\r\nstartup settings:";
      this.enableSkipModeCheckBox.AutoSize = true;
      this.enableSkipModeCheckBox.Location = new Point(448, 192);
      this.enableSkipModeCheckBox.Name = "enableSkipModeCheckBox";
      this.enableSkipModeCheckBox.Size = new Size(148, 16);
      this.enableSkipModeCheckBox.TabIndex = 15;
      this.enableSkipModeCheckBox.Text = "Enable frameskipping";
      this.enableSkipModeCheckBox.UseVisualStyleBackColor = true;
      this.enableSkipModeCheckBox.CheckedChanged += new EventHandler(this.enableSkipModeCheckBox_CheckedChanged);
      this.enableExperimentalBreakpointsCheckbox.AutoSize = true;
      this.enableExperimentalBreakpointsCheckbox.Location = new Point(136, 211);
      this.enableExperimentalBreakpointsCheckbox.Name = "enableExperimentalBreakpointsCheckbox";
      this.enableExperimentalBreakpointsCheckbox.Size = new Size(151, 16);
      this.enableExperimentalBreakpointsCheckbox.TabIndex = 23;
      this.enableExperimentalBreakpointsCheckbox.Text = "Enable experimental breakpoints";
      this.enableExperimentalBreakpointsCheckbox.UseVisualStyleBackColor = true;
      this.enableExperimentalBreakpointsCheckbox.CheckedChanged += new EventHandler(this.enableSkipModeCheckBox_CheckedChanged);
      this.enableSpeedUpCheckBox.AutoSize = true;
      this.enableSpeedUpCheckBox.Location = new Point(297, 192);
      this.enableSpeedUpCheckBox.Name = "enableSpeedUpCheckBox";
      this.enableSpeedUpCheckBox.Size = new Size(143, 16);
      this.enableSpeedUpCheckBox.TabIndex = 14;
      this.enableSpeedUpCheckBox.Text = "Enable high speed mode";
      this.enableSpeedUpCheckBox.UseVisualStyleBackColor = true;
      this.enableSpeedUpCheckBox.CheckedChanged += new EventHandler(this.enableSpeedUpCheckBox_CheckedChanged);
      this.enableDebugCheckBox.AutoSize = true;
      this.enableDebugCheckBox.Location = new Point(136, 192);
      this.enableDebugCheckBox.Name = "enableDebugCheckBox";
      this.enableDebugCheckBox.Size = new Size(151, 16);
      this.enableDebugCheckBox.TabIndex = 13;
      this.enableDebugCheckBox.Text = "Enable debug mode";
      this.enableDebugCheckBox.UseVisualStyleBackColor = true;
      this.enableDebugCheckBox.CheckedChanged += new EventHandler(this.enableDebugCheckBox_CheckedChanged);
      this.cmdLineTextBox.Font = new Font("MS UI Gothic", 10f, FontStyle.Regular, GraphicsUnit.Point, (byte) 128);
      this.cmdLineTextBox.Location = new Point(155, 79);
      this.cmdLineTextBox.MaxLength = 192;
      this.cmdLineTextBox.Name = "cmdLineTextBox";
      this.cmdLineTextBox.Size = new Size(429, 21);
      this.cmdLineTextBox.TabIndex = 12;
      this.cmdLineTextBox.Click += new EventHandler(this.cmdLineTextBox_Click);
      this.cmdLineLabel.AutoSize = true;
      this.cmdLineLabel.Location = new Point(22, 83);
      this.cmdLineLabel.Name = "cmdLineLabel";
      this.cmdLineLabel.Size = new Size(111, 12);
      this.cmdLineLabel.TabIndex = 11;
      this.cmdLineLabel.Text = "Command line options:";
      this.cmdLineLabel.Click += new EventHandler(this.cmdLineLabel_Click);
      this.openFileButton2.Location = new Point(511, 49);
      this.openFileButton2.Name = "openFileButton2";
      this.openFileButton2.Size = new Size(73, 23);
      this.openFileButton2.TabIndex = 10;
      this.openFileButton2.Text = "Browse...";
      this.openFileButton2.UseVisualStyleBackColor = true;
      this.openFileButton2.Click += new EventHandler(this.openFileButton2_Click);
      this.selectDefTextBox.Font = new Font("MS UI Gothic", 10f, FontStyle.Regular, GraphicsUnit.Point, (byte) 128);
      this.selectDefTextBox.Location = new Point(155, 50);
      this.selectDefTextBox.MaxLength = 250;
      this.selectDefTextBox.Name = "selectDefTextBox";
      this.selectDefTextBox.Size = new Size(350, 21);
      this.selectDefTextBox.TabIndex = 9;
      this.selectDefTextBox.Click += new EventHandler(this.selectDefTextBox_Click);
      this.selectDefLabel.AutoSize = true;
      this.selectDefLabel.Location = new Point(22, 54);
      this.selectDefLabel.Name = "selectDefLabel";
      this.selectDefLabel.Size = new Size(119, 12);
      this.selectDefLabel.TabIndex = 8;
      this.selectDefLabel.Text = "select.def filepath:";
      this.selectDefLabel.Click += new EventHandler(this.selectDefLabel_Click);
      this.openFileButton1.Location = new Point(511, 18);
      this.openFileButton1.Name = "openFileButton1";
      this.openFileButton1.Size = new Size(73, 23);
      this.openFileButton1.TabIndex = 7;
      this.openFileButton1.Text = "Browse...";
      this.openFileButton1.UseVisualStyleBackColor = true;
      this.openFileButton1.Click += new EventHandler(this.openFileButton1_Click);
      this.mugenExeTextBox.Font = new Font("MS UI Gothic", 10f, FontStyle.Regular, GraphicsUnit.Point, (byte) 128);
      this.mugenExeTextBox.Location = new Point(155, 20);
      this.mugenExeTextBox.MaxLength = 250;
      this.mugenExeTextBox.Name = "mugenExeTextBox";
      this.mugenExeTextBox.Size = new Size(350, 21);
      this.mugenExeTextBox.TabIndex = 6;
      this.mugenExeTextBox.Click += new EventHandler(this.mugenExeTextBox_Click);
      this.mugenExeLabel.AutoSize = true;
      this.mugenExeLabel.Location = new Point(22, 24);
      this.mugenExeLabel.Name = "mugenExeLabel";
      this.mugenExeLabel.Size = new Size((int) sbyte.MaxValue, 12);
      this.mugenExeLabel.TabIndex = 5;
      this.mugenExeLabel.Text = "mugen.exe filepath:";
      this.mugenExeLabel.Click += new EventHandler(this.mugenExeLabel_Click);
      this.tabPage2.Controls.Add((Control) this.groupBox2);
      this.tabPage2.Location = new Point(4, 22);
      this.tabPage2.Name = "tabPage2";
      this.tabPage2.Padding = new Padding(3);
      this.tabPage2.Size = new Size(608, 416);
      this.tabPage2.TabIndex = 1;
      this.tabPage2.Text = "Continuous battle mode";
      this.tabPage2.UseVisualStyleBackColor = true;
      this.tabPage2.Click += new EventHandler(this.tabPage2_Click);
      this.groupBox2.Controls.Add((Control) this.panel1);
      this.groupBox2.Controls.Add((Control) this.modeLabel);
      this.groupBox2.Controls.Add((Control) this.roundModeCheckBox);
      this.groupBox2.Controls.Add((Control) this.bothModeRadioButton);
      this.groupBox2.Controls.Add((Control) this.p2ModeRadioButton);
      this.groupBox2.Controls.Add((Control) this.p1ModeRadioButton);
      this.groupBox2.Controls.Add((Control) this.teamSideLabel);
      this.groupBox2.Controls.Add((Control) this.autoButton);
      this.groupBox2.Controls.Add((Control) this.label13);
      this.groupBox2.Controls.Add((Control) this.colorTextBox);
      this.groupBox2.Controls.Add((Control) this.label12);
      this.groupBox2.Controls.Add((Control) this.roundTimeTextBox);
      this.groupBox2.Controls.Add((Control) this.label11);
      this.groupBox2.Controls.Add((Control) this.colorLabel);
      this.groupBox2.Controls.Add((Control) this.retryTextBox2);
      this.groupBox2.Controls.Add((Control) this.label9);
      this.groupBox2.Controls.Add((Control) this.roundTextBox);
      this.groupBox2.Controls.Add((Control) this.roundTimeLabel);
      this.groupBox2.Controls.Add((Control) this.roundTextLabel);
      this.groupBox2.Controls.Add((Control) this.retryTextLabel);
      this.groupBox2.Controls.Add((Control) this.enableAutoModecheckBox);
      this.groupBox2.Controls.Add((Control) this.charListTextBox);
      this.groupBox2.Location = new Point(3, 19);
      this.groupBox2.Name = "groupBox2";
      this.groupBox2.Size = new Size(601, 405);
      this.groupBox2.TabIndex = 2;
      this.groupBox2.TabStop = false;
      this.groupBox2.Text = "Continuous battle mode settings";
      this.panel1.Controls.Add((Control) this.allVSModeRadioButton);
      this.panel1.Controls.Add((Control) this.oneVSModeRadioButton);
      this.panel1.Location = new Point(190, 120);
      this.panel1.Margin = new Padding(0);
      this.panel1.Name = "panel1";
      this.panel1.Size = new Size(285, 25);
      this.panel1.TabIndex = 39;
      this.allVSModeRadioButton.AutoSize = true;
      this.allVSModeRadioButton.Enabled = false;
      this.allVSModeRadioButton.Location = new Point(154, 4);
      this.allVSModeRadioButton.Name = "allVSModeRadioButton";
      this.allVSModeRadioButton.Size = new Size(55, 16);
      this.allVSModeRadioButton.TabIndex = 32;
      this.allVSModeRadioButton.TabStop = true;
      this.allVSModeRadioButton.Text = "Round robin";
      this.allVSModeRadioButton.UseVisualStyleBackColor = true;
      this.allVSModeRadioButton.CheckedChanged += new EventHandler(this.allVSModeRadioButton_CheckedChanged);
      this.oneVSModeRadioButton.AutoSize = true;
      this.oneVSModeRadioButton.Checked = true;
      this.oneVSModeRadioButton.Enabled = false;
      this.oneVSModeRadioButton.Location = new Point(7, 4);
      this.oneVSModeRadioButton.Name = "oneVSModeRadioButton";
      this.oneVSModeRadioButton.Size = new Size(116, 16);
      this.oneVSModeRadioButton.TabIndex = 31;
      this.oneVSModeRadioButton.TabStop = true;
      this.oneVSModeRadioButton.Text = "One character vs. the rest";
      this.oneVSModeRadioButton.UseVisualStyleBackColor = true;
      this.oneVSModeRadioButton.CheckedChanged += new EventHandler(this.oneVSModeRadioButton_CheckedChanged);
      this.modeLabel.AutoSize = true;
      this.modeLabel.Location = new Point(53, 124);
      this.modeLabel.Name = "modeLabel";
      this.modeLabel.Size = new Size(86, 12);
      this.modeLabel.TabIndex = 36;
      this.modeLabel.Text = "Battle pairings:";
      this.modeLabel.Click += new EventHandler(this.modeLabel_Click);
      this.roundModeCheckBox.AutoSize = true;
      this.roundModeCheckBox.Enabled = false;
      this.roundModeCheckBox.Location = new Point(105, 66);
      this.roundModeCheckBox.Name = "roundModeCheckBox";
      this.roundModeCheckBox.Size = new Size(153, 16);
      this.roundModeCheckBox.TabIndex = 19;
      this.roundModeCheckBox.Text = "End the matches after the specified number of rounds.";
      this.roundModeCheckBox.UseVisualStyleBackColor = true;
      this.roundModeCheckBox.CheckedChanged += new EventHandler(this.roundModeCheckBox_CheckedChanged);
      this.bothModeRadioButton.AutoSize = true;
      this.bothModeRadioButton.Enabled = false;
      this.bothModeRadioButton.Location = new Point(388, 145);
      this.bothModeRadioButton.Name = "bothModeRadioButton";
      this.bothModeRadioButton.Size = new Size(88, 16);
      this.bothModeRadioButton.TabIndex = 43;
      this.bothModeRadioButton.Text = "Alternative between P1 and P2 sides";
      this.bothModeRadioButton.UseVisualStyleBackColor = true;
      this.bothModeRadioButton.CheckedChanged += new EventHandler(this.bothModeRadioButton_CheckedChanged);
      this.p2ModeRadioButton.AutoSize = true;
      this.p2ModeRadioButton.Enabled = false;
      this.p2ModeRadioButton.Location = new Point(292, 145);
      this.p2ModeRadioButton.Name = "p2ModeRadioButton";
      this.p2ModeRadioButton.Size = new Size(81, 16);
      this.p2ModeRadioButton.TabIndex = 42;
      this.p2ModeRadioButton.Text = "Always P2 side";
      this.p2ModeRadioButton.UseVisualStyleBackColor = true;
      this.p2ModeRadioButton.CheckedChanged += new EventHandler(this.p2ModeRadioButton_CheckedChanged);
      this.p1ModeRadioButton.AutoSize = true;
      this.p1ModeRadioButton.Checked = true;
      this.p1ModeRadioButton.Enabled = false;
      this.p1ModeRadioButton.Location = new Point(197, 145);
      this.p1ModeRadioButton.Name = "p1ModeRadioButton";
      this.p1ModeRadioButton.Size = new Size(81, 16);
      this.p1ModeRadioButton.TabIndex = 41;
      this.p1ModeRadioButton.TabStop = true;
      this.p1ModeRadioButton.Text = "Always P1 side";
      this.p1ModeRadioButton.UseVisualStyleBackColor = true;
      this.p1ModeRadioButton.CheckedChanged += new EventHandler(this.p1ModeRadioButton_CheckedChanged);
      this.teamSideLabel.AutoSize = true;
      this.teamSideLabel.Location = new Point(53, 147);
      this.teamSideLabel.Name = "teamSideLabel";
      this.teamSideLabel.Size = new Size(63, 12);
      this.teamSideLabel.TabIndex = 26;
      this.teamSideLabel.Text = "Side settings:";
      this.teamSideLabel.Click += new EventHandler(this.teamSideLabel_Click);
      this.autoButton.Enabled = false;
      this.autoButton.Location = new Point(24, 369);
      this.autoButton.Name = "autoButton";
      this.autoButton.Size = new Size(206, 23);
      this.autoButton.TabIndex = 45;
      this.autoButton.Text = "Read characters from the select.def file";
      this.autoButton.UseVisualStyleBackColor = true;
      this.autoButton.Click += new EventHandler(this.autoButton_Click);
      this.label13.AutoSize = true;
      this.label13.Location = new Point(492, 95);
      this.label13.Name = "label13";
      this.label13.Size = new Size(11, 12);
      this.label13.TabIndex = 23;
      this.label13.Text = "p";
      this.colorTextBox.Enabled = false;
      this.colorTextBox.Font = new Font("MS UI Gothic", 10f, FontStyle.Regular, GraphicsUnit.Point, (byte) 128);
      this.colorTextBox.ImeMode = ImeMode.Disable;
      this.colorTextBox.Location = new Point(437, 91);
      this.colorTextBox.MaxLength = 2;
      this.colorTextBox.Name = "colorTextBox";
      this.colorTextBox.Size = new Size(49, 21);
      this.colorTextBox.TabIndex = 30;
      this.colorTextBox.Text = "12";
      this.colorTextBox.TextAlign = HorizontalAlignment.Right;
      this.colorTextBox.Click += new EventHandler(this.colorTextBox_Click);
      this.label12.AutoSize = true;
      this.label12.Location = new Point(492, 47);
      this.label12.Name = "label12";
      this.label12.Size = new Size(36, 12);
      this.label12.TabIndex = 21;
      this.label12.Text = "minutes";
      this.roundTimeTextBox.Enabled = false;
      this.roundTimeTextBox.Font = new Font("MS UI Gothic", 10f, FontStyle.Regular, GraphicsUnit.Point, (byte) 128);
      this.roundTimeTextBox.ImeMode = ImeMode.Disable;
      this.roundTimeTextBox.Location = new Point(437, 43);
      this.roundTimeTextBox.MaxLength = 3;
      this.roundTimeTextBox.Name = "roundTimeTextBox";
      this.roundTimeTextBox.Size = new Size(49, 21);
      this.roundTimeTextBox.TabIndex = 18;
      this.roundTimeTextBox.Text = "10";
      this.roundTimeTextBox.TextAlign = HorizontalAlignment.Right;
      this.roundTimeTextBox.Click += new EventHandler(this.roundTimeTextBox_Click);
      this.label11.AutoSize = true;
      this.label11.Location = new Point(251, 95);
      this.label11.Name = "label11";
      this.label11.Size = new Size(36, 12);
      this.label11.TabIndex = 19;
      this.label11.Text = "times";
      this.colorLabel.AutoSize = true;
      this.colorLabel.Location = new Point(343, 96);
      this.colorLabel.Name = "colorLabel";
      this.colorLabel.Size = new Size(88, 12);
      this.colorLabel.TabIndex = 18;
      this.colorLabel.Text = "Default color:";
      this.colorLabel.Click += new EventHandler(this.colorLabel_Click);
      this.retryTextBox2.Enabled = false;
      this.retryTextBox2.Font = new Font("MS UI Gothic", 10f, FontStyle.Regular, GraphicsUnit.Point, (byte) 128);
      this.retryTextBox2.ImeMode = ImeMode.Disable;
      this.retryTextBox2.Location = new Point(196, 91);
      this.retryTextBox2.MaxLength = 2;
      this.retryTextBox2.Name = "retryTextBox2";
      this.retryTextBox2.Size = new Size(49, 21);
      this.retryTextBox2.TabIndex = 29;
      this.retryTextBox2.Text = "1";
      this.retryTextBox2.TextAlign = HorizontalAlignment.Right;
      this.retryTextBox2.Click += new EventHandler(this.retryTextBox2_Click);
      this.label9.AutoSize = true;
      this.label9.Location = new Point(252, 47);
      this.label9.Name = "label9";
      this.label9.Size = new Size(64, 12);
      this.label9.TabIndex = 16;
      this.label9.Text = "round(s)";
      this.roundTextBox.Enabled = false;
      this.roundTextBox.Font = new Font("MS UI Gothic", 10f, FontStyle.Regular, GraphicsUnit.Point, (byte) 128);
      this.roundTextBox.ImeMode = ImeMode.Disable;
      this.roundTextBox.Location = new Point(196, 43);
      this.roundTextBox.MaxLength = 2;
      this.roundTextBox.Name = "roundTextBox";
      this.roundTextBox.Size = new Size(49, 21);
      this.roundTextBox.TabIndex = 17;
      this.roundTextBox.Text = "1";
      this.roundTextBox.TextAlign = HorizontalAlignment.Right;
      this.roundTextBox.Click += new EventHandler(this.roundTextBox_Click);
      this.roundTimeLabel.AutoSize = true;
      this.roundTimeLabel.Location = new Point(342, 47);
      this.roundTimeLabel.Name = "roundTimeLabel";
      this.roundTimeLabel.Size = new Size(82, 12);
      this.roundTimeLabel.TabIndex = 14;
      this.roundTimeLabel.Text = "Round time:";
      this.roundTimeLabel.Click += new EventHandler(this.roundTimeLabel_Click);
      this.roundTextLabel.AutoSize = true;
      this.roundTextLabel.Location = new Point(53, 47);
      this.roundTextLabel.Name = "roundTextLabel";
      this.roundTextLabel.Size = new Size(66, 12);
      this.roundTextLabel.TabIndex = 13;
      this.roundTextLabel.Text = "Number of rounds:";
      this.roundTextLabel.Click += new EventHandler(this.roundTextLabel_Click);
      this.retryTextLabel.AutoSize = true;
      this.retryTextLabel.Location = new Point(53, 96);
      this.retryTextLabel.Name = "retryTextLabel";
      this.retryTextLabel.Size = new Size(137, 12);
      this.retryTextLabel.TabIndex = 12;
      this.retryTextLabel.Text = "Times to retry after crash:";
      this.retryTextLabel.Click += new EventHandler(this.retryTextLabel_Click);
      this.enableAutoModecheckBox.AutoSize = true;
      this.enableAutoModecheckBox.Location = new Point(30, 21);
      this.enableAutoModecheckBox.Name = "enableAutoModecheckBox";
      this.enableAutoModecheckBox.Size = new Size(152, 16);
      this.enableAutoModecheckBox.TabIndex = 16;
      this.enableAutoModecheckBox.Text = "Enable continuous match mode";
      this.enableAutoModecheckBox.UseVisualStyleBackColor = true;
      this.enableAutoModecheckBox.CheckedChanged += new EventHandler(this.enableAutoModecheckBox_CheckedChanged);
      this.charListTextBox.ContextMenuStrip = this.charContextMenuStrip;
      this.charListTextBox.Enabled = false;
      this.charListTextBox.Font = new Font("ＭＳ ゴシック", 9f, FontStyle.Regular, GraphicsUnit.Point, (byte) 128);
      this.charListTextBox.Location = new Point(17, 168);
      this.charListTextBox.Multiline = true;
      this.charListTextBox.Name = "charListTextBox";
      this.charListTextBox.ScrollBars = ScrollBars.Vertical;
      this.charListTextBox.Size = new Size(567, 195);
      this.charListTextBox.TabIndex = 44;
      this.charListTextBox.Text = "; Specify the characters to use for continuous battle mode. \r\n; \r\n\r\n; --- P1 side ----\r\nkfm, 1\r\n\r\n; Specify the P2 side after this line \r\nkfm, 2\r\nkfm, 3\r\nkfm, 4\r\nkfm, 5\r\nkfm, 6\r\n";
      this.charListTextBox.Click += new EventHandler(this.charListTextBox_Click);
      this.charContextMenuStrip.Items.AddRange(new ToolStripItem[3]
      {
        (ToolStripItem) this.allSelToolStripMenuItem,
        (ToolStripItem) this.copyToolStripMenuItem,
        (ToolStripItem) this.pasteToolStripMenuItem
      });
      this.charContextMenuStrip.Name = "charContextMenuStrip";
      this.charContextMenuStrip.Size = new Size(184, 70);
      this.allSelToolStripMenuItem.Name = "allSelToolStripMenuItem";
      this.allSelToolStripMenuItem.ShortcutKeys = Keys.A | Keys.Control;
      this.allSelToolStripMenuItem.Size = new Size(183, 22);
      this.allSelToolStripMenuItem.Text = "Select all";
      this.allSelToolStripMenuItem.Click += new EventHandler(this.allSelToolStripMenuItem_Click);
      this.copyToolStripMenuItem.Name = "copyToolStripMenuItem";
      this.copyToolStripMenuItem.ShortcutKeys = Keys.C | Keys.Control;
      this.copyToolStripMenuItem.Size = new Size(183, 22);
      this.copyToolStripMenuItem.Text = "Copy";
      this.copyToolStripMenuItem.Click += new EventHandler(this.copyToolStripMenuItem_Click);
      this.pasteToolStripMenuItem.Name = "pasteToolStripMenuItem";
      this.pasteToolStripMenuItem.ShortcutKeys = Keys.V | Keys.Control;
      this.pasteToolStripMenuItem.Size = new Size(183, 22);
      this.pasteToolStripMenuItem.Text = "Paste";
      this.pasteToolStripMenuItem.Click += new EventHandler(this.pasteToolStripMenuItem_Click);
      this.mugenOpenFileDialog.AddExtension = false;
      this.mugenOpenFileDialog.DefaultExt = "exe";
      this.mugenOpenFileDialog.Filter = "mugen.exe |*.exe";
      this.mugenOpenFileDialog.InitialDirectory = "C:\\";
      this.mugenOpenFileDialog.ReadOnlyChecked = true;
      this.mugenOpenFileDialog.RestoreDirectory = true;
      this.mugenOpenFileDialog.SupportMultiDottedExtensions = true;
      this.mugenOpenFileDialog.Title = "Select the mugen executable";
      this.selectOpenFileDialog.AddExtension = false;
      this.selectOpenFileDialog.DefaultExt = "exe";
      this.selectOpenFileDialog.Filter = "select.def file |select.def";
      this.selectOpenFileDialog.InitialDirectory = "C:\\";
      this.selectOpenFileDialog.ReadOnlyChecked = true;
      this.selectOpenFileDialog.RestoreDirectory = true;
      this.selectOpenFileDialog.SupportMultiDottedExtensions = true;
      this.selectOpenFileDialog.Title = "Select the select.def file";
      this.toolTip1.AutomaticDelay = 500000;
      this.toolTip1.AutoPopDelay = 5000000;
      this.toolTip1.InitialDelay = 5000000;
      this.toolTip1.IsBalloon = true;
      this.toolTip1.ReshowDelay = 1000000;
      this.helpCheckBox.Appearance = Appearance.Button;
      this.helpCheckBox.AutoSize = true;
      this.helpCheckBox.BackgroundImageLayout = ImageLayout.None;
      this.helpCheckBox.ImageAlign = ContentAlignment.MiddleLeft;
      this.helpCheckBox.Location = new Point(568, 9);
      this.helpCheckBox.Name = "helpCheckBox";
      this.helpCheckBox.Padding = new Padding(1);
      this.helpCheckBox.Size = new Size(43, 22);
      this.helpCheckBox.TabIndex = 25;
      this.helpCheckBox.TabStop = false;
      this.helpCheckBox.Text = "       ";
      this.helpCheckBox.UseVisualStyleBackColor = true;
      this.helpCheckBox.CheckedChanged += new EventHandler(this.helpCheckBox_CheckedChanged);
      this.helpCheckBox.MouseHover += new EventHandler(this.helpCheckBox_MouseHover);
      this.AutoScaleDimensions = new SizeF(6f, 12f);
      this.AutoScaleMode = AutoScaleMode.Font;
      this.ClientSize = new Size(619, 523);
      this.Controls.Add((Control) this.helpCheckBox);
      this.Controls.Add((Control) this.tabControl1);
      this.Controls.Add((Control) this.profileNameTextBox);
      this.Controls.Add((Control) this.profileNameLabel);
      this.Controls.Add((Control) this.cancelButton);
      this.Controls.Add((Control) this.okButton);
      this.FormBorderStyle = FormBorderStyle.FixedDialog;
      this.Icon = (Icon) componentResourceManager.GetObject("$this.Icon");
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = nameof (ProfileConfigPanel);
      this.ShowInTaskbar = false;
      this.StartPosition = FormStartPosition.CenterScreen;
      this.Text = "Profile settings";
      this.Deactivate += new EventHandler(this.ProfileConfigPanel_Deactivate);
      this.FormClosing += new FormClosingEventHandler(this.ProfileConfigPanel_FormClosing);
      this.Load += new EventHandler(this.ProfileConfigPanel_Load);
      this.tabControl1.ResumeLayout(false);
      this.tabPage1.ResumeLayout(false);
      this.groupBox1.ResumeLayout(false);
      this.groupBox1.PerformLayout();
      this.tabPage2.ResumeLayout(false);
      this.groupBox2.ResumeLayout(false);
      this.groupBox2.PerformLayout();
      this.panel1.ResumeLayout(false);
      this.panel1.PerformLayout();
      this.charContextMenuStrip.ResumeLayout(false);
      this.ResumeLayout(false);
      this.PerformLayout();
    }
  }
}
