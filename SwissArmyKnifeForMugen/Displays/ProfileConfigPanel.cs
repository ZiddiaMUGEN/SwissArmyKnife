// Decompiled with JetBrains decompiler
// Type: SwissArmyKnifeForMugen.ProfileConfigPanel
// Assembly: SAKnifeWM, Version=1.0.8.0, Culture=neutral, PublicKeyToken=null
// MVID: 09478AD8-365C-4BF3-BEA1-B5785151259B
// Assembly location: C:\Users\ziddi\Downloads\Swiss Army Knife 1.1 Conversion\Swiss Army Knife 1.1 Conversion\SAKnifeWM.exe

using SwissArmyKnifeForMugen.Configs;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace SwissArmyKnifeForMugen.Displays
{
    /// <summary>
    /// Form used for configuring and managing profiles, as well as launching MUGEN.
    /// </summary>
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

        public ProfileConfigPanel() => InitializeComponent();

        private void LoadProfileConfig()
        {
            MugenProfile currentProfile = ProfileManager.MainObj().GetCurrentProfile();
            if (currentProfile == null)
                return;
            string profileName = currentProfile.GetProfileName();
            if (profileName != null)
                profileNameTextBox.Text = profileName;
            string mugenExePath = currentProfile.GetMugenExePath();
            if (mugenExePath != null)
                mugenExeTextBox.Text = mugenExePath;
            string mugenSelectCfgPath = currentProfile.GetMugenSelectCfgPath();
            if (mugenSelectCfgPath != null)
                selectDefTextBox.Text = mugenSelectCfgPath;
            string commandLineOptionsBase = currentProfile.GetMugenCommandLineOptionsBase();
            if (commandLineOptionsBase != null)
                cmdLineTextBox.Text = commandLineOptionsBase;
            enableDebugCheckBox.Checked = currentProfile.IsDebugMode();
            enableSpeedUpCheckBox.Checked = currentProfile.IsSpeedMode();
            enableSkipModeCheckBox.Checked = currentProfile.IsSkipMode();
            enableAutoModecheckBox.Checked = currentProfile.IsAutoModeAvailable();
            autoModeRadioButton.Enabled = currentProfile.IsAutoModeAvailable();
            enableExperimentalBreakpointsCheckbox.Checked = currentProfile.IsExperimentalBreakpoints();
            switch (currentProfile.GetDefaultGameMode())
            {
                case MugenProfile.GameMode.NORMAL:
                    normalModeRadioButton.Checked = true;
                    break;
                case MugenProfile.GameMode.QUICK_VS:
                    quickModeRadioButton.Checked = true;
                    break;
                case MugenProfile.GameMode.AUTO_MODE:
                    autoModeRadioButton.Checked = true;
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
            roundModeCheckBox.Checked = currentProfile.IsStrictRoundMode();
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
                    oneVSModeRadioButton.Checked = true;
                    break;
                case MugenProfile.MatchMode.ALLvsALL:
                    allVSModeRadioButton.Checked = true;
                    break;
            }
            switch (currentProfile.GetTeamSideMode())
            {
                case MugenProfile.TeamSideMode.P1_SIDE:
                    p1ModeRadioButton.Checked = true;
                    break;
                case MugenProfile.TeamSideMode.P2_SIDE:
                    p2ModeRadioButton.Checked = true;
                    break;
                case MugenProfile.TeamSideMode.BOTH_SIDE:
                    bothModeRadioButton.Checked = true;
                    break;
            }
            string charListRawData = currentProfile.GetCharListRawData();
            if (charListRawData == null)
                return;
            charListTextBox.Text = charListRawData;
        }

        private bool SaveProfileConfig()
        {
            MugenProfile currentProfile = ProfileManager.MainObj().GetCurrentProfile();
            if (currentProfile == null)
                return false;
            if (profileNameTextBox.Text == "")
            {
                int num = (int)MessageBox.Show("Please enter a profile name.", "Swiss Army Knife");
                profileNameTextBox.Focus();
                profileNameTextBox.SelectAll();
                return false;
            }
            if (mugenExeTextBox.Text == "")
            {
                int num = (int)MessageBox.Show("Please specify the mugen executable location.", "Swiss Army Knife");
                tabControl1.SelectedIndex = 0;
                mugenExeTextBox.Focus();
                mugenExeTextBox.SelectAll();
                return false;
            }
            if (!File.Exists(mugenExeTextBox.Text))
            {
                int num = (int)MessageBox.Show("The specified mugen executable does not exist.", "Swiss Army Knife");
                tabControl1.SelectedIndex = 0;
                mugenExeTextBox.Focus();
                mugenExeTextBox.SelectAll();
                return false;
            }
            string extension = Path.GetExtension(mugenExeTextBox.Text);
            extension.ToLower();
            if (extension != ".exe")
            {
                int num = (int)MessageBox.Show("The specified file is not an executable file", "Swiss Army Knife");
                mugenExeTextBox.Focus();
                mugenExeTextBox.SelectAll();
                return false;
            }
            FileVersionInfo versionInfo = FileVersionInfo.GetVersionInfo(mugenExeTextBox.Text);
            if (versionInfo != null && versionInfo.FileVersion != "1.0.0" && versionInfo.FileVersion != "1.1.0 Alpha 4" && versionInfo.FileVersion != "1.1.0 Beta 1 P1" && versionInfo.FileVersion != null)
            {
                int num = (int)MessageBox.Show("The specified executable is not mugen.", "Swiss Army Knife");
                tabControl1.SelectedIndex = 0;
                mugenExeTextBox.Focus();
                mugenExeTextBox.SelectAll();
                return false;
            }
            if (selectDefTextBox.Text != "")
            {
                if (!File.Exists(selectDefTextBox.Text))
                {
                    int num = (int)MessageBox.Show("The specified select.def does not exist.", "Swiss Army Knife");
                    selectDefTextBox.Focus();
                    selectDefTextBox.SelectAll();
                    return false;
                }
                string fileName = Path.GetFileName(selectDefTextBox.Text);
                fileName.ToLower();
                if (fileName != "select.def")
                {
                    int num = (int)MessageBox.Show("The specified file is not a select.def file.", "Swiss Army Knife");
                    selectDefTextBox.Focus();
                    selectDefTextBox.SelectAll();
                    return false;
                }
            }
            int result;
            if (!int.TryParse(roundTextBox.Text, out result) || result < 1 || result > 99)
            {
                int num = (int)MessageBox.Show("Please specify an integer between 1 and 99 for the number of rounds.", "Swiss Army Knife");
                tabControl1.SelectedIndex = 1;
                roundTextBox.Focus();
                roundTextBox.SelectAll();
                return false;
            }
            if (!int.TryParse(roundTimeTextBox.Text, out result) || result < 1 || result > 999)
            {
                int num = (int)MessageBox.Show("Please specify an integer between 1 and 999 for the round time.", "Swiss Army Knife");
                tabControl1.SelectedIndex = 1;
                roundTimeTextBox.Focus();
                roundTimeTextBox.SelectAll();
                return false;
            }
            if (!int.TryParse(retryTextBox2.Text, out result) || result < 0 || result > 99)
            {
                int num = (int)MessageBox.Show("Please specify an integer between 1 and 99 for the number of retries after a crash.", "Swiss Army Knife");
                tabControl1.SelectedIndex = 1;
                retryTextBox2.Focus();
                retryTextBox2.SelectAll();
                return false;
            }
            if (!int.TryParse(colorTextBox.Text, out result) || result < 1 || result > 12)
            {
                int num = (int)MessageBox.Show("Please specify an integer between 1 and 12 for the default color.", "Swiss Army Knife");
                tabControl1.SelectedIndex = 1;
                colorTextBox.Focus();
                colorTextBox.SelectAll();
                return false;
            }
            currentProfile.MakeBackup();
            string str1 = ";" + Environment.NewLine + "; Filename:" + currentProfile.GetProfileFile() + Environment.NewLine + ";" + Environment.NewLine + Environment.NewLine + "[Mugen]" + Environment.NewLine + "ProfileName = " + profileNameTextBox.Text + Environment.NewLine + "MugenExe = " + mugenExeTextBox.Text + Environment.NewLine + "SelectDotDef = " + selectDefTextBox.Text + Environment.NewLine + "MugenCommandLineOptions = " + cmdLineTextBox.Text + Environment.NewLine + "DebugMode = " + (enableDebugCheckBox.Checked ? "1" : "0") + Environment.NewLine + "SpeedUp = " + (enableSpeedUpCheckBox.Checked ? "1" : "0") + Environment.NewLine + "SkipMode = " + (enableSkipModeCheckBox.Checked ? "1" : "0") + Environment.NewLine + "ExperimentalBPS = " + (enableExperimentalBreakpointsCheckbox.Checked ? "1" : "0") + Environment.NewLine;
            if (normalModeRadioButton.Checked)
                str1 += "DefaultGameMode = 0";
            else if (quickModeRadioButton.Checked)
                str1 += "DefaultGameMode = 1";
            else if (autoModeRadioButton.Checked)
                str1 += "DefaultGameMode = 2";
            string str2 = str1 + Environment.NewLine + "[AutoMode]" + Environment.NewLine + "AutoMode = " + (enableAutoModecheckBox.Checked ? "1" : "0") + Environment.NewLine + "Rounds = " + roundTextBox.Text + Environment.NewLine + "MaxRoundTime = " + roundTimeTextBox.Text + Environment.NewLine + "StrictRoundMode = " + (roundModeCheckBox.Checked ? "1" : "0") + Environment.NewLine + "ErrorRetry = " + retryTextBox2.Text + Environment.NewLine + "DefaultColor = " + colorTextBox.Text + Environment.NewLine;
            if (oneVSModeRadioButton.Checked)
                str2 += "MatchMode = 0";
            else if (allVSModeRadioButton.Checked)
                str2 += "MatchMode = 1";
            string str3 = str2 + Environment.NewLine;
            if (p1ModeRadioButton.Checked)
                str3 += "TeamSide = 1";
            else if (p2ModeRadioButton.Checked)
                str3 += "TeamSide = 2";
            else if (bothModeRadioButton.Checked)
                str3 += "TeamSide = 0";
            string configText = str3 + Environment.NewLine + Environment.NewLine + "[Characters]" + Environment.NewLine + charListTextBox.Text + Environment.NewLine;
            if (!currentProfile.SaveConfigText(configText))
            {
                int num = (int)MessageBox.Show("Could not write to the configuration file.", "Swiss Army Knife");
                return false;
            }
            currentProfile.ReLoad();
            return true;
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            Control okButton = this.okButton;
            if (okButton != null && okButton.Cursor == Cursors.Help || !SaveProfileConfig())
                return;
            Close();
        }

        private void cancelButton_Click(object sender, EventArgs e) => Close();

        private void openFileButton1_Click(object sender, EventArgs e)
        {
            if (mugenExeTextBox.Text != "")
            {
                if (Path.IsPathRooted(mugenExeTextBox.Text))
                {
                    if (File.Exists(mugenExeTextBox.Text))
                        mugenOpenFileDialog.InitialDirectory = Path.GetDirectoryName(mugenExeTextBox.Text);
                    else
                        mugenOpenFileDialog.InitialDirectory = Environment.SpecialFolder.DesktopDirectory.ToString();
                }
                else
                    mugenOpenFileDialog.InitialDirectory = Environment.SpecialFolder.DesktopDirectory.ToString();
            }
            else
                mugenOpenFileDialog.InitialDirectory = Environment.SpecialFolder.DesktopDirectory.ToString();
            if (mugenOpenFileDialog.ShowDialog() != DialogResult.OK)
                return;
            mugenExeTextBox.Text = mugenOpenFileDialog.FileName;
        }

        private void openFileButton2_Click(object sender, EventArgs e)
        {
            if (selectDefTextBox.Text != "")
            {
                if (Path.IsPathRooted(selectDefTextBox.Text))
                {
                    if (File.Exists(selectDefTextBox.Text))
                        selectOpenFileDialog.InitialDirectory = Path.GetDirectoryName(selectDefTextBox.Text);
                    else
                        selectOpenFileDialog.InitialDirectory = Environment.SpecialFolder.DesktopDirectory.ToString();
                }
                else
                    selectOpenFileDialog.InitialDirectory = Environment.SpecialFolder.DesktopDirectory.ToString();
            }
            else if (mugenExeTextBox.Text != "")
            {
                if (Path.IsPathRooted(mugenExeTextBox.Text))
                {
                    if (File.Exists(mugenExeTextBox.Text))
                        selectOpenFileDialog.InitialDirectory = Path.GetDirectoryName(mugenExeTextBox.Text);
                    else
                        selectOpenFileDialog.InitialDirectory = Environment.SpecialFolder.DesktopDirectory.ToString();
                }
                else
                    selectOpenFileDialog.InitialDirectory = Environment.SpecialFolder.DesktopDirectory.ToString();
            }
            else
                selectOpenFileDialog.InitialDirectory = Environment.SpecialFolder.DesktopDirectory.ToString();
            if (selectOpenFileDialog.ShowDialog() != DialogResult.OK)
                return;
            selectDefTextBox.Text = selectOpenFileDialog.FileName;
        }

        private void enableDebugCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            Control enableDebugCheckBox = this.enableDebugCheckBox;
            if (enableDebugCheckBox == null || !(enableDebugCheckBox.Cursor == Cursors.Help))
                return;
            this.enableDebugCheckBox.CheckedChanged -= new EventHandler(enableDebugCheckBox_CheckedChanged);
            this.enableDebugCheckBox.Checked = !this.enableDebugCheckBox.Checked;
            this.enableDebugCheckBox.CheckedChanged += new EventHandler(enableDebugCheckBox_CheckedChanged);
            string text = "Enables the debug mode in mugen. This has the same effect as pressing Ctrl + D." + Environment.NewLine;
            toolTip1.Hide(enableDebugCheckBox);
            toolTip1.IsBalloon = false;
            toolTip1.IsBalloon = true;
            toolTip1.SetToolTip(enableDebugCheckBox, " ");
            toolTip1.Show(text, enableDebugCheckBox, enableDebugCheckBox.Width / 2, enableDebugCheckBox.Height / 2);
        }

        private void enableSpeedUpCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            Control enableSpeedUpCheckBox = this.enableSpeedUpCheckBox;
            if (enableSpeedUpCheckBox == null || !(enableSpeedUpCheckBox.Cursor == Cursors.Help))
                return;
            this.enableSpeedUpCheckBox.CheckedChanged -= new EventHandler(enableSpeedUpCheckBox_CheckedChanged);
            this.enableSpeedUpCheckBox.Checked = !this.enableSpeedUpCheckBox.Checked;
            this.enableSpeedUpCheckBox.CheckedChanged += new EventHandler(enableSpeedUpCheckBox_CheckedChanged);
            string text = "Enables the high speed mode in mugen. This has the same effect as pressing Ctrl + S." + Environment.NewLine;
            toolTip1.Hide(enableSpeedUpCheckBox);
            toolTip1.IsBalloon = false;
            toolTip1.IsBalloon = true;
            toolTip1.SetToolTip(enableSpeedUpCheckBox, " ");
            toolTip1.Show(text, enableSpeedUpCheckBox, enableSpeedUpCheckBox.Width / 2, enableSpeedUpCheckBox.Height / 2);
        }

        private void enableSkipModeCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            Control skipModeCheckBox = enableSkipModeCheckBox;
            if (skipModeCheckBox == null || !(skipModeCheckBox.Cursor == Cursors.Help))
                return;
            enableSkipModeCheckBox.CheckedChanged -= new EventHandler(enableSkipModeCheckBox_CheckedChanged);
            enableSkipModeCheckBox.Checked = !enableSkipModeCheckBox.Checked;
            enableSkipModeCheckBox.CheckedChanged += new EventHandler(enableSkipModeCheckBox_CheckedChanged);
            string text = "Enables the frameskip function in mugen. This will skip the drawing phase of one frame for every drawn one." + Environment.NewLine + "By pairing this setting with the high speed mode, it is possible to make matches last an even shorter time." + Environment.NewLine;
            toolTip1.Hide(skipModeCheckBox);
            toolTip1.IsBalloon = false;
            toolTip1.IsBalloon = true;
            toolTip1.SetToolTip(skipModeCheckBox, " ");
            toolTip1.Show(text, skipModeCheckBox, skipModeCheckBox.Width / 2, skipModeCheckBox.Height / 2);
        }

        private void EnableAutoModeUI()
        {
            if (enableAutoModecheckBox.Checked || Cursor == Cursors.Help)
            {
                roundTextBox.Enabled = true;
                roundTimeTextBox.Enabled = true;
                roundModeCheckBox.Enabled = true;
                retryTextBox2.Enabled = true;
                colorTextBox.Enabled = true;
                oneVSModeRadioButton.Enabled = true;
                allVSModeRadioButton.Enabled = true;
                p1ModeRadioButton.Enabled = true;
                p2ModeRadioButton.Enabled = true;
                bothModeRadioButton.Enabled = true;
                charListTextBox.Enabled = true;
                autoButton.Enabled = true;
            }
            else
            {
                roundTextBox.Enabled = false;
                roundTimeTextBox.Enabled = false;
                roundModeCheckBox.Enabled = false;
                retryTextBox2.Enabled = false;
                colorTextBox.Enabled = false;
                oneVSModeRadioButton.Enabled = false;
                allVSModeRadioButton.Enabled = false;
                p1ModeRadioButton.Enabled = false;
                p2ModeRadioButton.Enabled = false;
                bothModeRadioButton.Enabled = false;
                charListTextBox.Enabled = false;
                autoButton.Enabled = false;
            }
        }

        private void enableAutoModecheckBox_CheckedChanged(object sender, EventArgs e)
        {
            Control autoModecheckBox = enableAutoModecheckBox;
            if (autoModecheckBox != null && autoModecheckBox.Cursor == Cursors.Help)
            {
                enableAutoModecheckBox.CheckedChanged -= new EventHandler(enableAutoModecheckBox_CheckedChanged);
                enableAutoModecheckBox.Checked = !enableAutoModecheckBox.Checked;
                enableAutoModecheckBox.CheckedChanged += new EventHandler(enableAutoModecheckBox_CheckedChanged);
                string text = "Enables the continuous battle mode." + Environment.NewLine + Environment.NewLine + "Continuous battle mode is for making one specified character face against others." + Environment.NewLine + "This is similar to survival mode, however, this mode isn't over until all opponents have been fought." + Environment.NewLine + "Typically, this mode would be used to test the functionality of a character before releasing it to the public." + Environment.NewLine + Environment.NewLine + "[Special features of this mode]" + Environment.NewLine + "In this mode, the matches will keep operating regardless of any crashes that may occur in mugen." + Environment.NewLine + "This application has been designed to observe and register the error messages, state numbers, helpers and" + Environment.NewLine + "other information during a crash. Mainly, to be left unattended." + Environment.NewLine + "(This mode can be paired up with video capturing software according to hardware specifications.)" + Environment.NewLine + Environment.NewLine + "Additionally, even if a rare occurence of the timerfreeze controller may appear, the rounds can still be fixed" + Environment.NewLine + "to have a real time limit." + Environment.NewLine + "(Note: If this were to happen, the rounds would be forced to end.)" + Environment.NewLine + Environment.NewLine + "By the same token, this function prevents eternal intros or any other mishaps that may not let the round \nend naturally." + Environment.NewLine + "(Note: Since the rounds are ended forcefully unexpected behavior may occur.)" + Environment.NewLine + Environment.NewLine;
                toolTip1.Hide(autoModecheckBox);
                toolTip1.IsBalloon = false;
                toolTip1.IsBalloon = true;
                toolTip1.SetToolTip(autoModecheckBox, " ");
                toolTip1.Show(text, autoModecheckBox, autoModecheckBox.Width / 2, autoModecheckBox.Height / 2);
            }
            else
            {
                EnableAutoModeUI();
                autoModeRadioButton.Enabled = enableAutoModecheckBox.Checked;
            }
        }

        private void autoButton_Click(object sender, EventArgs e)
        {
            Control autoButton = this.autoButton;
            if (autoButton != null && autoButton.Cursor == Cursors.Help)
            {
                string text = "Imports characters from the select.def file specified in the 'Basic settings' tab." + Environment.NewLine + "(Note: When using this settings, any unsaved information on the list will be deleted.)" + Environment.NewLine;
                toolTip1.Hide(autoButton);
                toolTip1.IsBalloon = false;
                toolTip1.IsBalloon = true;
                toolTip1.SetToolTip(autoButton, " ");
                toolTip1.Show(text, autoButton, autoButton.Width / 2, autoButton.Height / 2);
            }
            else if (selectDefTextBox.Text == "" || !File.Exists(selectDefTextBox.Text))
            {
                int num = (int)MessageBox.Show("Please specify the location of your select.def file in the 'Basic settings' tab.", "Swiss Army Knife");
            }
            else
            {
                if (MessageBox.Show("This will import characters specified in your select.def file. \nThe character list will be remade and any unsaved information will be deleted, do you wish to proceed?", "Swiss Army Knife", MessageBoxButtons.OKCancel) != DialogResult.OK)
                    return;
                MugenProfile currentProfile = ProfileManager.MainObj().GetCurrentProfile();
                if (currentProfile == null)
                    return;
                string charListRawData = currentProfile.GetCharListRawData(selectDefTextBox.Text);
                if (charListRawData == null || !(charListRawData != ""))
                    return;
                charListTextBox.Text = "; Specify the characters used for continuous battle mode." + Environment.NewLine + ";" + Environment.NewLine + Environment.NewLine + charListRawData;
            }
        }

        private void ProfileConfigPanel_Load(object sender, EventArgs e)
        {
            helpCheckBox.Image = SystemIcons.Question.ToBitmap();
            LoadProfileConfig();
        }

        private void ProfileConfigPanel_FormClosing(object sender, FormClosingEventArgs e)
        {
        }

        private void helpCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            toolTip1.Hide(this);
            toolTip1.SetToolTip(helpCheckBox, " ");
            toolTip1.IsBalloon = false;
            toolTip1.IsBalloon = true;
            if (!_isHelpMode)
            {
                _isHelpMode = true;
                Cursor = Cursors.Help;
                tabControl1.Cursor = Cursors.Help;
                tabPage1.Cursor = Cursors.Help;
                tabPage2.Cursor = Cursors.Help;
                profileNameTextBox.Cursor = Cursors.Help;
                profileNameLabel.Cursor = Cursors.Help;
                mugenExeTextBox.Cursor = Cursors.Help;
                mugenExeLabel.Cursor = Cursors.Help;
                selectDefTextBox.Cursor = Cursors.Help;
                selectDefLabel.Cursor = Cursors.Help;
                cmdLineTextBox.Cursor = Cursors.Help;
                cmdLineLabel.Cursor = Cursors.Help;
                enableDebugCheckBox.Cursor = Cursors.Help;
                enableSpeedUpCheckBox.Cursor = Cursors.Help;
                enableSkipModeCheckBox.Cursor = Cursors.Help;
                enableAutoModecheckBox.Cursor = Cursors.Help;
                roundTextBox.Cursor = Cursors.Help;
                roundTextLabel.Cursor = Cursors.Help;
                roundTimeTextBox.Cursor = Cursors.Help;
                roundTimeLabel.Cursor = Cursors.Help;
                retryTextBox2.Cursor = Cursors.Help;
                retryTextLabel.Cursor = Cursors.Help;
                colorTextBox.Cursor = Cursors.Help;
                colorLabel.Cursor = Cursors.Help;
                modeLabel.Cursor = Cursors.Help;
                oneVSModeRadioButton.Cursor = Cursors.Help;
                allVSModeRadioButton.Cursor = Cursors.Help;
                teamSideLabel.Cursor = Cursors.Help;
                p1ModeRadioButton.Cursor = Cursors.Help;
                p2ModeRadioButton.Cursor = Cursors.Help;
                bothModeRadioButton.Cursor = Cursors.Help;
                charListTextBox.Cursor = Cursors.Help;
                autoButton.Cursor = Cursors.Help;
                normalModeRadioButton.Cursor = Cursors.Help;
                quickModeRadioButton.Cursor = Cursors.Help;
                autoModeRadioButton.Cursor = Cursors.Help;
                autoModeRadioButton.Enabled = true;
                okButton.Cursor = Cursors.Help;
                okButton.Enabled = false;
                EnableAutoModeUI();
                toolTip1.Show("Help has been activated, please click on the item you wish to know more information about." + Environment.NewLine + "If the help mode ends unexpectedly, feel free to click the Help button again to use it.", helpCheckBox, helpCheckBox.Width / 2, helpCheckBox.Height / 2, 5000);
            }
            else
            {
                _isHelpMode = false;
                Cursor = Cursors.Default;
                tabControl1.Cursor = Cursors.Default;
                tabPage1.Cursor = Cursors.Default;
                tabPage2.Cursor = Cursors.Default;
                profileNameTextBox.Cursor = Cursors.Default;
                profileNameLabel.Cursor = Cursors.Default;
                mugenExeTextBox.Cursor = Cursors.Default;
                mugenExeLabel.Cursor = Cursors.Default;
                selectDefTextBox.Cursor = Cursors.Default;
                selectDefLabel.Cursor = Cursors.Default;
                cmdLineTextBox.Cursor = Cursors.Default;
                cmdLineLabel.Cursor = Cursors.Default;
                enableDebugCheckBox.Cursor = Cursors.Default;
                enableSpeedUpCheckBox.Cursor = Cursors.Default;
                enableSkipModeCheckBox.Cursor = Cursors.Default;
                enableAutoModecheckBox.Cursor = Cursors.Default;
                roundTextBox.Cursor = Cursors.Default;
                roundTextLabel.Cursor = Cursors.Default;
                roundTimeTextBox.Cursor = Cursors.Default;
                roundTimeLabel.Cursor = Cursors.Default;
                retryTextBox2.Cursor = Cursors.Default;
                retryTextLabel.Cursor = Cursors.Default;
                colorTextBox.Cursor = Cursors.Default;
                colorLabel.Cursor = Cursors.Default;
                modeLabel.Cursor = Cursors.Default;
                oneVSModeRadioButton.Cursor = Cursors.Default;
                allVSModeRadioButton.Cursor = Cursors.Default;
                teamSideLabel.Cursor = Cursors.Default;
                p1ModeRadioButton.Cursor = Cursors.Default;
                p2ModeRadioButton.Cursor = Cursors.Default;
                bothModeRadioButton.Cursor = Cursors.Default;
                charListTextBox.Cursor = Cursors.Default;
                autoButton.Cursor = Cursors.Default;
                normalModeRadioButton.Cursor = Cursors.Default;
                quickModeRadioButton.Cursor = Cursors.Default;
                autoModeRadioButton.Cursor = Cursors.Default;
                if (enableAutoModecheckBox.Checked)
                    autoModeRadioButton.Enabled = true;
                else
                    autoModeRadioButton.Enabled = false;
                okButton.Cursor = Cursors.Default;
                okButton.Enabled = true;
                EnableAutoModeUI();
                toolTip1.Show("The help button.", helpCheckBox, helpCheckBox.Width / 2, helpCheckBox.Height / 2, 1000);
            }
        }

        private void ProfileConfigPanel_Deactivate(object sender, EventArgs e)
        {
            if (!(Cursor == Cursors.Help))
                return;
            toolTip1.Hide(this);
        }

        private void tabControl1_Click(object sender, EventArgs e)
        {
            Control tabControl1 = this.tabControl1;
            if (tabControl1 == null || !(tabControl1.Cursor == Cursors.Help))
                return;
            toolTip1.Hide(tabControl1);
            toolTip1.IsBalloon = false;
            toolTip1.IsBalloon = true;
            toolTip1.SetToolTip(tabControl1, " ");
            if (this.tabControl1.SelectedIndex == 0)
                toolTip1.Show("The basic settings to run mugen with." + Environment.NewLine + "Specifying the location of the mugen.exe file and a profile name are enough to register a new profile." + Environment.NewLine, tabControl1, 10, 10);
            else
                toolTip1.Show("A function to help during character creation." + Environment.NewLine + "In this version, this 'Continuous battle' mode has been added." + Environment.NewLine, tabControl1, 100, 10);
        }

        private void tabPage1_Click(object sender, EventArgs e)
        {
        }

        private void tabPage2_Click(object sender, EventArgs e)
        {
        }

        private void profileNameLabel_Click(object sender, EventArgs e) => profileNameTextBox_Click(sender, e);

        private void profileNameTextBox_Click(object sender, EventArgs e)
        {
            Control profileNameTextBox = this.profileNameTextBox;
            if (profileNameTextBox == null || !(profileNameTextBox.Cursor == Cursors.Help))
                return;
            string text = "Specifies a name for this profile." + Environment.NewLine + "Example: 'My created characters' or 'Joke characters only'" + Environment.NewLine + "This is required, so you will not be able to register a profile without it." + Environment.NewLine;
            toolTip1.Hide(profileNameTextBox);
            toolTip1.IsBalloon = false;
            toolTip1.IsBalloon = true;
            toolTip1.SetToolTip(profileNameTextBox, " ");
            toolTip1.Show(text, profileNameTextBox, profileNameTextBox.Width / 2, profileNameTextBox.Height / 2);
        }

        private void mugenExeLabel_Click(object sender, EventArgs e) => mugenExeTextBox_Click(sender, e);

        private void mugenExeTextBox_Click(object sender, EventArgs e)
        {
            Control mugenExeTextBox = this.mugenExeTextBox;
            if (mugenExeTextBox == null || !(mugenExeTextBox.Cursor == Cursors.Help))
                return;
            string text = "Specify the full filepath to your mugen executable file here." + Environment.NewLine + "This is required, so you will not be able to register a profile without it." + Environment.NewLine + "(Note: The use of *.bat batch files is not supported)" + Environment.NewLine;
            toolTip1.Hide(mugenExeTextBox);
            toolTip1.IsBalloon = false;
            toolTip1.IsBalloon = true;
            toolTip1.SetToolTip(mugenExeTextBox, " ");
            toolTip1.Show(text, mugenExeTextBox, mugenExeTextBox.Width / 2, mugenExeTextBox.Height / 2);
        }

        private void selectDefLabel_Click(object sender, EventArgs e) => selectDefTextBox_Click(sender, e);

        private void selectDefTextBox_Click(object sender, EventArgs e)
        {
            Control selectDefTextBox = this.selectDefTextBox;
            if (selectDefTextBox == null || !(selectDefTextBox.Cursor == Cursors.Help))
                return;
            string text = "Specify the full filepath to your select.def file here." + Environment.NewLine + "This is not required, but certain functions can make use of this file." + Environment.NewLine + "Specifying the location of this file is recommended." + Environment.NewLine;
            toolTip1.Hide(selectDefTextBox);
            toolTip1.IsBalloon = false;
            toolTip1.IsBalloon = true;
            toolTip1.SetToolTip(selectDefTextBox, " ");
            toolTip1.Show(text, selectDefTextBox, selectDefTextBox.Width / 2, selectDefTextBox.Height / 2);
        }

        private void cmdLineLabel_Click(object sender, EventArgs e) => cmdLineTextBox_Click(sender, e);

        private void cmdLineTextBox_Click(object sender, EventArgs e)
        {
            Control cmdLineTextBox = this.cmdLineTextBox;
            if (cmdLineTextBox == null || !(cmdLineTextBox.Cursor == Cursors.Help))
                return;
            string text = "Specifies command line options for running mugen." + Environment.NewLine + "Example: -r kfm" + Environment.NewLine + "This is not a required setting, but it can be used to specify a screenpack or an addon." + Environment.NewLine;
            toolTip1.Hide(cmdLineTextBox);
            toolTip1.IsBalloon = false;
            toolTip1.IsBalloon = true;
            toolTip1.SetToolTip(cmdLineTextBox, " ");
            toolTip1.Show(text, cmdLineTextBox, cmdLineTextBox.Width / 2, cmdLineTextBox.Height / 2);
        }

        private void roundTextLabel_Click(object sender, EventArgs e) => roundTextBox_Click(sender, e);

        private void roundTextBox_Click(object sender, EventArgs e)
        {
            Control roundTextBox = this.roundTextBox;
            if (roundTextBox == null || !(roundTextBox.Cursor == Cursors.Help))
                return;
            string text = "Sets the number of rounds for the matches." + Environment.NewLine + "The default setting is 1 round." + Environment.NewLine;
            toolTip1.Hide(roundTextBox);
            toolTip1.IsBalloon = false;
            toolTip1.IsBalloon = true;
            toolTip1.SetToolTip(roundTextBox, " ");
            toolTip1.Show(text, roundTextBox, roundTextBox.Width / 2, roundTextBox.Height / 2);
        }

        private void roundTimeLabel_Click(object sender, EventArgs e) => roundTimeTextBox_Click(sender, e);

        private void roundTimeTextBox_Click(object sender, EventArgs e)
        {
            Control roundTimeTextBox = this.roundTimeTextBox;
            if (roundTimeTextBox == null || !(roundTimeTextBox.Cursor == Cursors.Help))
                return;
            string text = "Sets the maximum possible time for one round." + Environment.NewLine + "If a round exceeds the maximum alloted time, then it will be forcefully terminated." + Environment.NewLine + "If this is the case, the round will be treated as a draw regardless of life or remaining time." + Environment.NewLine + "The default setting is 10 minutes." + Environment.NewLine + "(Note: The specified time is real time, so regardless of timerfreeze or any similar effects," + Environment.NewLine + "the round will end according to elapsed real time.)" + Environment.NewLine;
            toolTip1.Hide(roundTimeTextBox);
            toolTip1.IsBalloon = false;
            toolTip1.IsBalloon = true;
            toolTip1.SetToolTip(roundTimeTextBox, " ");
            toolTip1.Show(text, roundTimeTextBox, roundTimeTextBox.Width / 2, roundTimeTextBox.Height / 2);
        }

        private void retryTextLabel_Click(object sender, EventArgs e) => retryTextBox2_Click(sender, e);

        private void retryTextBox2_Click(object sender, EventArgs e)
        {
            Control retryTextBox2 = this.retryTextBox2;
            if (retryTextBox2 == null || !(retryTextBox2.Cursor == Cursors.Help))
                return;
            string text = "Specifies how many times to restart the match in case of a crash." + Environment.NewLine + "If all the attempts result in a crash、" + Environment.NewLine + "the plan will switch to the next scheduled match." + Environment.NewLine + "The default setting is 0 (no retries)." + Environment.NewLine;
            toolTip1.Hide(retryTextBox2);
            toolTip1.IsBalloon = false;
            toolTip1.IsBalloon = true;
            toolTip1.SetToolTip(retryTextBox2, " ");
            toolTip1.Show(text, retryTextBox2, retryTextBox2.Width / 2, retryTextBox2.Height / 2);
        }

        private void colorLabel_Click(object sender, EventArgs e) => colorTextBox_Click(sender, e);

        private void colorTextBox_Click(object sender, EventArgs e)
        {
            Control colorTextBox = this.colorTextBox;
            if (colorTextBox == null || !(colorTextBox.Cursor == Cursors.Help))
                return;
            string text = "Specifies the default color for opponents." + Environment.NewLine + "If your opponents have a color selectable mode, then it might be \n a good idea to select an appropriate number." + Environment.NewLine + "The default setting is '1', the first color." + Environment.NewLine;
            toolTip1.Hide(colorTextBox);
            toolTip1.IsBalloon = false;
            toolTip1.IsBalloon = true;
            toolTip1.SetToolTip(colorTextBox, " ");
            toolTip1.Show(text, colorTextBox, colorTextBox.Width / 2, colorTextBox.Height / 2);
        }

        private void charListTextBox_Click(object sender, EventArgs e)
        {
            Control charListTextBox = this.charListTextBox;
            if (charListTextBox == null || !(charListTextBox.Cursor == Cursors.Help))
                return;
            string text = "Specifies the main character for continuous battle mode." + Environment.NewLine + "Please specify characters the same way you would in your own select.def file." + Environment.NewLine + "(Just like in the def file, you can write down comments using the semicolon ';' character." + Environment.NewLine + "　Example: kfm     ; -- Kung Fu Man" + Environment.NewLine + "         kfm\\Evil_KFM.def     ; -- EVIL Kung Fu Man" + Environment.NewLine + "It is also possible to import characters already specified in your select.def file by using the \n 'Read characters from the select.def file' option." + Environment.NewLine + Environment.NewLine + "The main character is specified in the P1 side and the rest are specified in the P2 side." + Environment.NewLine + "As an example, consider the following setup:" + Environment.NewLine + "    kfm, 1" + Environment.NewLine + "    kfm, 2" + Environment.NewLine + "    kfm, 3" + Environment.NewLine + "    kfm, 4" + Environment.NewLine + "As the order implies, the first character will be the main in P1 side, and the rest  \n will be set in the P2 side" + Environment.NewLine + Environment.NewLine + "Specifying a number with a comma after the character name will allow you to select a palette." + Environment.NewLine + "　Example: kfm, 3     ; -- Kung Fu Man with palette 3" + Environment.NewLine + "　　  kfm\\Evil_KFM.def, 2     ; -- EVIL Kung Fu Man with palette 2" + Environment.NewLine + "If no color is specified, the default color specified above will be used." + Environment.NewLine + Environment.NewLine + "(Warning: this mode makes use of the quick vs. function within mugen, selecting a non-existant \n color will result in a crash.)" + Environment.NewLine + Environment.NewLine;
            toolTip1.Hide(charListTextBox);
            toolTip1.IsBalloon = false;
            toolTip1.IsBalloon = true;
            toolTip1.SetToolTip(charListTextBox, " ");
            toolTip1.Show(text, charListTextBox, charListTextBox.Width / 2, charListTextBox.Height / 2);
        }

        private void allSelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Cursor == Cursors.Help)
                return;
            charListTextBox.Focus();
            charListTextBox.SelectAll();
        }

        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Cursor == Cursors.Help || charListTextBox.SelectedText == null || !(charListTextBox.SelectedText != ""))
                return;
            Clipboard.SetText(charListTextBox.SelectedText);
        }

        private void pasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Cursor == Cursors.Help)
                return;
            charListTextBox.SelectedText = Clipboard.GetText();
        }

        private void helpCheckBox_MouseHover(object sender, EventArgs e)
        {
            if (Cursor == Cursors.Help)
                return;
            toolTip1.Hide(this);
            toolTip1.IsBalloon = false;
            toolTip1.SetToolTip(this, " ");
            toolTip1.Show("Help button", this, Width - helpCheckBox.Width, helpCheckBox.Height / 2, 1000);
        }

        private void normalModeRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            MugenProfile currentProfile = ProfileManager.MainObj().GetCurrentProfile();
            if (currentProfile == null)
                return;
            Control normalModeRadioButton = this.normalModeRadioButton;
            if (normalModeRadioButton == null || !(normalModeRadioButton.Cursor == Cursors.Help))
                return;
            switch (currentProfile.GetDefaultGameMode())
            {
                case MugenProfile.GameMode.NORMAL:
                    this.normalModeRadioButton.Checked = true;
                    break;
                case MugenProfile.GameMode.QUICK_VS:
                    quickModeRadioButton.Checked = true;
                    break;
                case MugenProfile.GameMode.AUTO_MODE:
                    autoModeRadioButton.Checked = true;
                    break;
            }
        }

        private void quickModeRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            MugenProfile currentProfile = ProfileManager.MainObj().GetCurrentProfile();
            if (currentProfile == null)
                return;
            Control quickModeRadioButton = this.quickModeRadioButton;
            if (quickModeRadioButton == null || !(quickModeRadioButton.Cursor == Cursors.Help))
                return;
            switch (currentProfile.GetDefaultGameMode())
            {
                case MugenProfile.GameMode.NORMAL:
                    normalModeRadioButton.Checked = true;
                    break;
                case MugenProfile.GameMode.QUICK_VS:
                    this.quickModeRadioButton.Checked = true;
                    break;
                case MugenProfile.GameMode.AUTO_MODE:
                    autoModeRadioButton.Checked = true;
                    break;
            }
        }

        private void autoModeRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            MugenProfile currentProfile = ProfileManager.MainObj().GetCurrentProfile();
            if (currentProfile == null)
                return;
            Control autoModeRadioButton = this.autoModeRadioButton;
            if (autoModeRadioButton == null || !(autoModeRadioButton.Cursor == Cursors.Help))
                return;
            switch (currentProfile.GetDefaultGameMode())
            {
                case MugenProfile.GameMode.NORMAL:
                    normalModeRadioButton.Checked = true;
                    break;
                case MugenProfile.GameMode.QUICK_VS:
                    quickModeRadioButton.Checked = true;
                    break;
                case MugenProfile.GameMode.AUTO_MODE:
                    this.autoModeRadioButton.Checked = true;
                    break;
            }
        }

        private void normalModeRadioButton_MouseClick(object sender, MouseEventArgs e)
        {
            Control normalModeRadioButton = this.normalModeRadioButton;
            if (normalModeRadioButton == null || !(normalModeRadioButton.Cursor == Cursors.Help))
                return;
            string text = "Starts mugen normally." + Environment.NewLine;
            toolTip1.Hide(normalModeRadioButton);
            toolTip1.IsBalloon = false;
            toolTip1.IsBalloon = true;
            toolTip1.SetToolTip(normalModeRadioButton, " ");
            toolTip1.Show(text, normalModeRadioButton, normalModeRadioButton.Width / 2, normalModeRadioButton.Height / 2);
        }

        private void quickModeRadioButton_MouseClick(object sender, MouseEventArgs e)
        {
            Control quickModeRadioButton = this.quickModeRadioButton;
            if (quickModeRadioButton == null || !(quickModeRadioButton.Cursor == Cursors.Help))
                return;
            string text = "Starts mugen in quick vs. mode." + Environment.NewLine;
            toolTip1.Hide(quickModeRadioButton);
            toolTip1.IsBalloon = false;
            toolTip1.IsBalloon = true;
            toolTip1.SetToolTip(quickModeRadioButton, " ");
            toolTip1.Show(text, quickModeRadioButton, quickModeRadioButton.Width / 2, quickModeRadioButton.Height / 2);
        }

        private void autoModeRadioButton_MouseClick(object sender, MouseEventArgs e)
        {
            Control autoModeRadioButton = this.autoModeRadioButton;
            if (autoModeRadioButton == null || !(autoModeRadioButton.Cursor == Cursors.Help))
                return;
            string text = "Stars mugen in continuous battle mode." + Environment.NewLine;
            toolTip1.Hide(autoModeRadioButton);
            toolTip1.IsBalloon = false;
            toolTip1.IsBalloon = true;
            toolTip1.SetToolTip(autoModeRadioButton, " ");
            toolTip1.Show(text, autoModeRadioButton, autoModeRadioButton.Width / 2, autoModeRadioButton.Height / 2);
        }

        private void p1ModeRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            Control p1ModeRadioButton = this.p1ModeRadioButton;
            if (p1ModeRadioButton == null || !(p1ModeRadioButton.Cursor == Cursors.Help))
                return;
            this.p1ModeRadioButton.CheckedChanged -= new EventHandler(p1ModeRadioButton_CheckedChanged);
            this.p1ModeRadioButton.Checked = true;
            this.p1ModeRadioButton.CheckedChanged += new EventHandler(p1ModeRadioButton_CheckedChanged);
            string text = "Makes the specified character play in the P1 side only." + Environment.NewLine;
            toolTip1.Hide(p1ModeRadioButton);
            toolTip1.IsBalloon = false;
            toolTip1.IsBalloon = true;
            toolTip1.SetToolTip(p1ModeRadioButton, " ");
            toolTip1.Show(text, p1ModeRadioButton, p1ModeRadioButton.Width / 2, p1ModeRadioButton.Height / 2);
        }

        private void p2ModeRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            Control p2ModeRadioButton = this.p2ModeRadioButton;
            if (p2ModeRadioButton == null || !(p2ModeRadioButton.Cursor == Cursors.Help))
                return;
            this.p2ModeRadioButton.CheckedChanged -= new EventHandler(p2ModeRadioButton_CheckedChanged);
            this.p2ModeRadioButton.Checked = false;
            this.p2ModeRadioButton.CheckedChanged += new EventHandler(p2ModeRadioButton_CheckedChanged);
            string text = "Makes the specified character play in the P2 side only." + Environment.NewLine;
            toolTip1.Hide(p2ModeRadioButton);
            toolTip1.IsBalloon = false;
            toolTip1.IsBalloon = true;
            toolTip1.SetToolTip(p2ModeRadioButton, " ");
            toolTip1.Show(text, p2ModeRadioButton, p2ModeRadioButton.Width / 2, p2ModeRadioButton.Height / 2);
        }

        private void bothModeRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            Control bothModeRadioButton = this.bothModeRadioButton;
            if (bothModeRadioButton == null || !(bothModeRadioButton.Cursor == Cursors.Help))
                return;
            this.bothModeRadioButton.CheckedChanged -= new EventHandler(bothModeRadioButton_CheckedChanged);
            this.bothModeRadioButton.Checked = false;
            this.bothModeRadioButton.CheckedChanged += new EventHandler(bothModeRadioButton_CheckedChanged);
            string text = "This option makes the specified character alternate play in both P1 and P2 sides." + Environment.NewLine;
            toolTip1.Hide(bothModeRadioButton);
            toolTip1.IsBalloon = false;
            toolTip1.IsBalloon = true;
            toolTip1.SetToolTip(bothModeRadioButton, " ");
            toolTip1.Show(text, bothModeRadioButton, bothModeRadioButton.Width / 2, bothModeRadioButton.Height / 2);
        }

        private void teamSideLabel_Click(object sender, EventArgs e)
        {
            Control teamSideLabel = this.teamSideLabel;
            if (teamSideLabel == null || !(teamSideLabel.Cursor == Cursors.Help))
                return;
            string text = "These options specify which side the selected character will play in." + Environment.NewLine;
            toolTip1.Hide(teamSideLabel);
            toolTip1.IsBalloon = false;
            toolTip1.IsBalloon = true;
            toolTip1.SetToolTip(teamSideLabel, " ");
            toolTip1.Show(text, teamSideLabel, teamSideLabel.Width / 2, teamSideLabel.Height / 2);
        }

        private void roundModeCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            Control roundModeCheckBox = this.roundModeCheckBox;
            if (roundModeCheckBox != null && roundModeCheckBox.Cursor == Cursors.Help)
            {
                this.roundModeCheckBox.CheckedChanged -= new EventHandler(roundModeCheckBox_CheckedChanged);
                this.roundModeCheckBox.Checked = !this.roundModeCheckBox.Checked;
                this.roundModeCheckBox.CheckedChanged += new EventHandler(roundModeCheckBox_CheckedChanged);
                string text = "This changes the winning conditions of one match." + Environment.NewLine + "Normally the game will proceed until one player wins the specified number of rounds but," + Environment.NewLine + "with this option the matches will automatically stop after the specified number of rounds." + Environment.NewLine + "(With this option, a round will be counted even if said round ends in a draw)." + Environment.NewLine;
                toolTip1.Hide(roundModeCheckBox);
                toolTip1.IsBalloon = false;
                toolTip1.IsBalloon = true;
                toolTip1.SetToolTip(roundModeCheckBox, " ");
                toolTip1.Show(text, roundModeCheckBox, roundModeCheckBox.Width / 2, roundModeCheckBox.Height / 2);
            }
            else if (this.roundModeCheckBox.Checked)
                label9.Text = "rounds and end";
            else
                label9.Text = "round matches";
        }

        private void modeLabel_Click(object sender, EventArgs e)
        {
            Control modeLabel = this.modeLabel;
            if (modeLabel == null || !(modeLabel.Cursor == Cursors.Help))
                return;
            string text = "These are the settings for creating the matches." + Environment.NewLine;
            toolTip1.Hide(modeLabel);
            toolTip1.IsBalloon = false;
            toolTip1.IsBalloon = true;
            toolTip1.SetToolTip(modeLabel, " ");
            toolTip1.Show(text, modeLabel, modeLabel.Width / 2, modeLabel.Height / 2);
        }

        private void oneVSModeRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            Control vsModeRadioButton = oneVSModeRadioButton;
            if (vsModeRadioButton == null || !(vsModeRadioButton.Cursor == Cursors.Help))
                return;
            oneVSModeRadioButton.CheckedChanged -= new EventHandler(oneVSModeRadioButton_CheckedChanged);
            oneVSModeRadioButton.Checked = true;
            oneVSModeRadioButton.CheckedChanged += new EventHandler(oneVSModeRadioButton_CheckedChanged);
            string text = "This option will match one character against the rest." + Environment.NewLine;
            toolTip1.Hide(vsModeRadioButton);
            toolTip1.IsBalloon = false;
            toolTip1.IsBalloon = true;
            toolTip1.SetToolTip(vsModeRadioButton, " ");
            toolTip1.Show(text, vsModeRadioButton, vsModeRadioButton.Width / 2, vsModeRadioButton.Height / 2);
        }

        private void allVSModeRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            Control vsModeRadioButton = allVSModeRadioButton;
            if (vsModeRadioButton == null || !(vsModeRadioButton.Cursor == Cursors.Help))
                return;
            allVSModeRadioButton.CheckedChanged -= new EventHandler(allVSModeRadioButton_CheckedChanged);
            allVSModeRadioButton.Checked = false;
            allVSModeRadioButton.CheckedChanged += new EventHandler(allVSModeRadioButton_CheckedChanged);
            string text = "This option will create matches between all the characters." + Environment.NewLine;
            toolTip1.Hide(vsModeRadioButton);
            toolTip1.IsBalloon = false;
            toolTip1.IsBalloon = true;
            toolTip1.SetToolTip(vsModeRadioButton, " ");
            toolTip1.Show(text, vsModeRadioButton, vsModeRadioButton.Width / 2, vsModeRadioButton.Height / 2);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null)
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            components = new Container();
            ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(ProfileConfigPanel));
            cancelButton = new Button();
            okButton = new Button();
            profileNameLabel = new Label();
            profileNameTextBox = new TextBox();
            tabControl1 = new TabControl();
            tabPage1 = new TabPage();
            groupBox1 = new GroupBox();
            groupBox3 = new GroupBox();
            textBox1 = new TextBox();
            autoModeRadioButton = new RadioButton();
            quickModeRadioButton = new RadioButton();
            normalModeRadioButton = new RadioButton();
            mugenModeLabel = new Label();
            textBox2 = new TextBox();
            label5 = new Label();
            enableExperimentalBreakpointsCheckbox = new CheckBox();
            enableSkipModeCheckBox = new CheckBox();
            enableSpeedUpCheckBox = new CheckBox();
            enableDebugCheckBox = new CheckBox();
            cmdLineTextBox = new TextBox();
            cmdLineLabel = new Label();
            openFileButton2 = new Button();
            selectDefTextBox = new TextBox();
            selectDefLabel = new Label();
            openFileButton1 = new Button();
            mugenExeTextBox = new TextBox();
            mugenExeLabel = new Label();
            tabPage2 = new TabPage();
            groupBox2 = new GroupBox();
            panel1 = new Panel();
            allVSModeRadioButton = new RadioButton();
            oneVSModeRadioButton = new RadioButton();
            modeLabel = new Label();
            roundModeCheckBox = new CheckBox();
            bothModeRadioButton = new RadioButton();
            p2ModeRadioButton = new RadioButton();
            p1ModeRadioButton = new RadioButton();
            teamSideLabel = new Label();
            autoButton = new Button();
            label13 = new Label();
            colorTextBox = new TextBox();
            label12 = new Label();
            roundTimeTextBox = new TextBox();
            label11 = new Label();
            colorLabel = new Label();
            retryTextBox2 = new TextBox();
            label9 = new Label();
            roundTextBox = new TextBox();
            roundTimeLabel = new Label();
            roundTextLabel = new Label();
            retryTextLabel = new Label();
            enableAutoModecheckBox = new CheckBox();
            charListTextBox = new TextBox();
            charContextMenuStrip = new ContextMenuStrip(components);
            allSelToolStripMenuItem = new ToolStripMenuItem();
            copyToolStripMenuItem = new ToolStripMenuItem();
            pasteToolStripMenuItem = new ToolStripMenuItem();
            mugenOpenFileDialog = new OpenFileDialog();
            selectOpenFileDialog = new OpenFileDialog();
            toolTip1 = new ToolTip(components);
            helpCheckBox = new CheckBox();
            tabControl1.SuspendLayout();
            tabPage1.SuspendLayout();
            groupBox1.SuspendLayout();
            tabPage2.SuspendLayout();
            groupBox2.SuspendLayout();
            panel1.SuspendLayout();
            charContextMenuStrip.SuspendLayout();
            SuspendLayout();
            cancelButton.Location = new Point(505, 487);
            cancelButton.Name = "cancelButton";
            cancelButton.Size = new Size(90, 28);
            cancelButton.TabIndex = 53;
            cancelButton.Text = "Cancel";
            cancelButton.UseVisualStyleBackColor = true;
            cancelButton.Click += new EventHandler(cancelButton_Click);
            okButton.Location = new Point(409, 487);
            okButton.Name = "okButton";
            okButton.Size = new Size(90, 28);
            okButton.TabIndex = 52;
            okButton.Text = "Save";
            okButton.UseVisualStyleBackColor = true;
            okButton.Click += new EventHandler(okButton_Click);
            profileNameLabel.AutoSize = true;
            profileNameLabel.Font = new Font("MS UI Gothic", 10f, FontStyle.Regular, GraphicsUnit.Point, 128);
            profileNameLabel.Location = new Point(16, 11);
            profileNameLabel.Name = "profileNameLabel";
            profileNameLabel.Size = new Size(108, 14);
            profileNameLabel.TabIndex = 4;
            profileNameLabel.Text = "Profile name:";
            profileNameLabel.Click += new EventHandler(profileNameLabel_Click);
            profileNameTextBox.Font = new Font("MS UI Gothic", 10f, FontStyle.Regular, GraphicsUnit.Point, 128);
            profileNameTextBox.Location = new Point(136, 8);
            profileNameTextBox.MaxLength = 32;
            profileNameTextBox.Name = "profileNameTextBox";
            profileNameTextBox.Size = new Size(283, 21);
            profileNameTextBox.TabIndex = 0;
            profileNameTextBox.Click += new EventHandler(profileNameTextBox_Click);
            tabControl1.Controls.Add(tabPage1);
            tabControl1.Controls.Add(tabPage2);
            tabControl1.Location = new Point(4, 35);
            tabControl1.Name = "tabControl1";
            tabControl1.SelectedIndex = 0;
            tabControl1.Size = new Size(616, 442);
            tabControl1.TabIndex = 24;
            tabControl1.Click += new EventHandler(tabControl1_Click);
            tabPage1.Controls.Add(groupBox1);
            tabPage1.Location = new Point(4, 22);
            tabPage1.Name = "tabPage1";
            tabPage1.Padding = new Padding(3);
            tabPage1.Size = new Size(608, 416);
            tabPage1.TabIndex = 0;
            tabPage1.Text = "Basic settings";
            tabPage1.UseVisualStyleBackColor = true;
            tabPage1.Click += new EventHandler(tabPage1_Click);
            groupBox1.Controls.Add(groupBox3);
            groupBox1.Controls.Add(textBox1);
            groupBox1.Controls.Add(autoModeRadioButton);
            groupBox1.Controls.Add(quickModeRadioButton);
            groupBox1.Controls.Add(normalModeRadioButton);
            groupBox1.Controls.Add(mugenModeLabel);
            groupBox1.Controls.Add(textBox2);
            groupBox1.Controls.Add(label5);
            groupBox1.Controls.Add(enableSkipModeCheckBox);
            groupBox1.Controls.Add(enableSpeedUpCheckBox);
            groupBox1.Controls.Add(enableDebugCheckBox);
            groupBox1.Controls.Add(enableExperimentalBreakpointsCheckbox);
            groupBox1.Controls.Add(cmdLineTextBox);
            groupBox1.Controls.Add(cmdLineLabel);
            groupBox1.Controls.Add(openFileButton2);
            groupBox1.Controls.Add(selectDefTextBox);
            groupBox1.Controls.Add(selectDefLabel);
            groupBox1.Controls.Add(openFileButton1);
            groupBox1.Controls.Add(mugenExeTextBox);
            groupBox1.Controls.Add(mugenExeLabel);
            groupBox1.Location = new Point(3, 19);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(601, 354);
            groupBox1.TabIndex = 1;
            groupBox1.TabStop = false;
            groupBox1.Text = "Mugen settings";
            groupBox3.Location = new Point(37, 228);
            groupBox3.Margin = new Padding(0);
            groupBox3.Name = "groupBox3";
            groupBox3.Padding = new Padding(0);
            groupBox3.Size = new Size(540, 4);
            groupBox3.TabIndex = 2;
            groupBox3.TabStop = false;
            textBox1.BackColor = SystemColors.Window;
            textBox1.BorderStyle = BorderStyle.None;
            textBox1.Cursor = Cursors.Default;
            textBox1.ImeMode = ImeMode.Disable;
            textBox1.Location = new Point(75, 294);
            textBox1.Multiline = true;
            textBox1.Name = "textBox1";
            textBox1.ReadOnly = true;
            textBox1.Size = new Size(432, 52);
            textBox1.TabIndex = 22;
            textBox1.TabStop = false;
            textBox1.Text = "The settings will be used when you click the 'Run mugen' button. \r\n\r\n(Note: the rest of these options can still be used manually from the 'More commands' menu";
            autoModeRadioButton.AutoSize = true;
            autoModeRadioButton.Enabled = false;
            autoModeRadioButton.Location = new Point(376, 262);
            autoModeRadioButton.Name = "autoModeRadioButton";
            autoModeRadioButton.Size = new Size(152, 16);
            autoModeRadioButton.TabIndex = 21;
            autoModeRadioButton.TabStop = true;
            autoModeRadioButton.Text = "Start in continuous battle mode";
            autoModeRadioButton.UseVisualStyleBackColor = true;
            autoModeRadioButton.CheckedChanged += new EventHandler(autoModeRadioButton_CheckedChanged);
            autoModeRadioButton.MouseClick += new MouseEventHandler(autoModeRadioButton_MouseClick);
            quickModeRadioButton.AutoSize = true;
            quickModeRadioButton.Location = new Point(208, 262);
            quickModeRadioButton.Name = "quickModeRadioButton";
            quickModeRadioButton.Size = new Size(151, 16);
            quickModeRadioButton.TabIndex = 20;
            quickModeRadioButton.TabStop = true;
            quickModeRadioButton.Text = "Start in quick vs. mode";
            quickModeRadioButton.UseVisualStyleBackColor = true;
            quickModeRadioButton.CheckedChanged += new EventHandler(quickModeRadioButton_CheckedChanged);
            quickModeRadioButton.MouseClick += new MouseEventHandler(quickModeRadioButton_MouseClick);
            normalModeRadioButton.AutoSize = true;
            normalModeRadioButton.Checked = true;
            normalModeRadioButton.Location = new Point(61, 262);
            normalModeRadioButton.Name = "normalModeRadioButton";
            normalModeRadioButton.Size = new Size(132, 16);
            normalModeRadioButton.TabIndex = 19;
            normalModeRadioButton.TabStop = true;
            normalModeRadioButton.Text = "Start normally";
            normalModeRadioButton.UseVisualStyleBackColor = true;
            normalModeRadioButton.CheckedChanged += new EventHandler(normalModeRadioButton_CheckedChanged);
            normalModeRadioButton.MouseClick += new MouseEventHandler(normalModeRadioButton_MouseClick);
            mugenModeLabel.AutoSize = true;
            mugenModeLabel.Location = new Point(22, 242);
            mugenModeLabel.Name = "mugenModeLabel";
            mugenModeLabel.Size = new Size(146, 12);
            mugenModeLabel.TabIndex = 18;
            mugenModeLabel.Text = "Mugen startup settings:";
            textBox2.BackColor = SystemColors.Window;
            textBox2.BorderStyle = BorderStyle.None;
            textBox2.Cursor = Cursors.Default;
            textBox2.ImeMode = ImeMode.Disable;
            textBox2.Location = new Point(168, 106);
            textBox2.Multiline = true;
            textBox2.Name = "textBox2";
            textBox2.ReadOnly = true;
            textBox2.Size = new Size(392, 72);
            textBox2.TabIndex = 17;
            textBox2.TabStop = false;
            textBox2.Text = "Make sure you specify the location of the real mugen executable. \r\n If you wish to use different lifebars or addons you may do so by specifying command line options. \r\n Example: If you wish to use the kfm motif then use: -r kfm \r\n";
            label5.AutoSize = true;
            label5.Location = new Point(22, 185);
            label5.Name = "label5";
            label5.Size = new Size(91, 24);
            label5.TabIndex = 16;
            label5.Text = "Additional\r\nstartup settings:";
            enableSkipModeCheckBox.AutoSize = true;
            enableSkipModeCheckBox.Location = new Point(448, 192);
            enableSkipModeCheckBox.Name = "enableSkipModeCheckBox";
            enableSkipModeCheckBox.Size = new Size(148, 16);
            enableSkipModeCheckBox.TabIndex = 15;
            enableSkipModeCheckBox.Text = "Enable frameskipping";
            enableSkipModeCheckBox.UseVisualStyleBackColor = true;
            enableSkipModeCheckBox.CheckedChanged += new EventHandler(enableSkipModeCheckBox_CheckedChanged);
            enableExperimentalBreakpointsCheckbox.AutoSize = true;
            enableExperimentalBreakpointsCheckbox.Location = new Point(136, 211);
            enableExperimentalBreakpointsCheckbox.Name = "enableExperimentalBreakpointsCheckbox";
            enableExperimentalBreakpointsCheckbox.Size = new Size(151, 16);
            enableExperimentalBreakpointsCheckbox.TabIndex = 23;
            enableExperimentalBreakpointsCheckbox.Text = "Enable experimental breakpoints";
            enableExperimentalBreakpointsCheckbox.UseVisualStyleBackColor = true;
            enableExperimentalBreakpointsCheckbox.CheckedChanged += new EventHandler(enableSkipModeCheckBox_CheckedChanged);
            enableSpeedUpCheckBox.AutoSize = true;
            enableSpeedUpCheckBox.Location = new Point(297, 192);
            enableSpeedUpCheckBox.Name = "enableSpeedUpCheckBox";
            enableSpeedUpCheckBox.Size = new Size(143, 16);
            enableSpeedUpCheckBox.TabIndex = 14;
            enableSpeedUpCheckBox.Text = "Enable high speed mode";
            enableSpeedUpCheckBox.UseVisualStyleBackColor = true;
            enableSpeedUpCheckBox.CheckedChanged += new EventHandler(enableSpeedUpCheckBox_CheckedChanged);
            enableDebugCheckBox.AutoSize = true;
            enableDebugCheckBox.Location = new Point(136, 192);
            enableDebugCheckBox.Name = "enableDebugCheckBox";
            enableDebugCheckBox.Size = new Size(151, 16);
            enableDebugCheckBox.TabIndex = 13;
            enableDebugCheckBox.Text = "Enable debug mode";
            enableDebugCheckBox.UseVisualStyleBackColor = true;
            enableDebugCheckBox.CheckedChanged += new EventHandler(enableDebugCheckBox_CheckedChanged);
            cmdLineTextBox.Font = new Font("MS UI Gothic", 10f, FontStyle.Regular, GraphicsUnit.Point, 128);
            cmdLineTextBox.Location = new Point(155, 79);
            cmdLineTextBox.MaxLength = 192;
            cmdLineTextBox.Name = "cmdLineTextBox";
            cmdLineTextBox.Size = new Size(429, 21);
            cmdLineTextBox.TabIndex = 12;
            cmdLineTextBox.Click += new EventHandler(cmdLineTextBox_Click);
            cmdLineLabel.AutoSize = true;
            cmdLineLabel.Location = new Point(22, 83);
            cmdLineLabel.Name = "cmdLineLabel";
            cmdLineLabel.Size = new Size(111, 12);
            cmdLineLabel.TabIndex = 11;
            cmdLineLabel.Text = "Command line options:";
            cmdLineLabel.Click += new EventHandler(cmdLineLabel_Click);
            openFileButton2.Location = new Point(511, 49);
            openFileButton2.Name = "openFileButton2";
            openFileButton2.Size = new Size(73, 23);
            openFileButton2.TabIndex = 10;
            openFileButton2.Text = "Browse...";
            openFileButton2.UseVisualStyleBackColor = true;
            openFileButton2.Click += new EventHandler(openFileButton2_Click);
            selectDefTextBox.Font = new Font("MS UI Gothic", 10f, FontStyle.Regular, GraphicsUnit.Point, 128);
            selectDefTextBox.Location = new Point(155, 50);
            selectDefTextBox.MaxLength = 250;
            selectDefTextBox.Name = "selectDefTextBox";
            selectDefTextBox.Size = new Size(350, 21);
            selectDefTextBox.TabIndex = 9;
            selectDefTextBox.Click += new EventHandler(selectDefTextBox_Click);
            selectDefLabel.AutoSize = true;
            selectDefLabel.Location = new Point(22, 54);
            selectDefLabel.Name = "selectDefLabel";
            selectDefLabel.Size = new Size(119, 12);
            selectDefLabel.TabIndex = 8;
            selectDefLabel.Text = "select.def filepath:";
            selectDefLabel.Click += new EventHandler(selectDefLabel_Click);
            openFileButton1.Location = new Point(511, 18);
            openFileButton1.Name = "openFileButton1";
            openFileButton1.Size = new Size(73, 23);
            openFileButton1.TabIndex = 7;
            openFileButton1.Text = "Browse...";
            openFileButton1.UseVisualStyleBackColor = true;
            openFileButton1.Click += new EventHandler(openFileButton1_Click);
            mugenExeTextBox.Font = new Font("MS UI Gothic", 10f, FontStyle.Regular, GraphicsUnit.Point, 128);
            mugenExeTextBox.Location = new Point(155, 20);
            mugenExeTextBox.MaxLength = 250;
            mugenExeTextBox.Name = "mugenExeTextBox";
            mugenExeTextBox.Size = new Size(350, 21);
            mugenExeTextBox.TabIndex = 6;
            mugenExeTextBox.Click += new EventHandler(mugenExeTextBox_Click);
            mugenExeLabel.AutoSize = true;
            mugenExeLabel.Location = new Point(22, 24);
            mugenExeLabel.Name = "mugenExeLabel";
            mugenExeLabel.Size = new Size(sbyte.MaxValue, 12);
            mugenExeLabel.TabIndex = 5;
            mugenExeLabel.Text = "mugen.exe filepath:";
            mugenExeLabel.Click += new EventHandler(mugenExeLabel_Click);
            tabPage2.Controls.Add(groupBox2);
            tabPage2.Location = new Point(4, 22);
            tabPage2.Name = "tabPage2";
            tabPage2.Padding = new Padding(3);
            tabPage2.Size = new Size(608, 416);
            tabPage2.TabIndex = 1;
            tabPage2.Text = "Continuous battle mode";
            tabPage2.UseVisualStyleBackColor = true;
            tabPage2.Click += new EventHandler(tabPage2_Click);
            groupBox2.Controls.Add(panel1);
            groupBox2.Controls.Add(modeLabel);
            groupBox2.Controls.Add(roundModeCheckBox);
            groupBox2.Controls.Add(bothModeRadioButton);
            groupBox2.Controls.Add(p2ModeRadioButton);
            groupBox2.Controls.Add(p1ModeRadioButton);
            groupBox2.Controls.Add(teamSideLabel);
            groupBox2.Controls.Add(autoButton);
            groupBox2.Controls.Add(label13);
            groupBox2.Controls.Add(colorTextBox);
            groupBox2.Controls.Add(label12);
            groupBox2.Controls.Add(roundTimeTextBox);
            groupBox2.Controls.Add(label11);
            groupBox2.Controls.Add(colorLabel);
            groupBox2.Controls.Add(retryTextBox2);
            groupBox2.Controls.Add(label9);
            groupBox2.Controls.Add(roundTextBox);
            groupBox2.Controls.Add(roundTimeLabel);
            groupBox2.Controls.Add(roundTextLabel);
            groupBox2.Controls.Add(retryTextLabel);
            groupBox2.Controls.Add(enableAutoModecheckBox);
            groupBox2.Controls.Add(charListTextBox);
            groupBox2.Location = new Point(3, 19);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(601, 405);
            groupBox2.TabIndex = 2;
            groupBox2.TabStop = false;
            groupBox2.Text = "Continuous battle mode settings";
            panel1.Controls.Add(allVSModeRadioButton);
            panel1.Controls.Add(oneVSModeRadioButton);
            panel1.Location = new Point(190, 120);
            panel1.Margin = new Padding(0);
            panel1.Name = "panel1";
            panel1.Size = new Size(285, 25);
            panel1.TabIndex = 39;
            allVSModeRadioButton.AutoSize = true;
            allVSModeRadioButton.Enabled = false;
            allVSModeRadioButton.Location = new Point(154, 4);
            allVSModeRadioButton.Name = "allVSModeRadioButton";
            allVSModeRadioButton.Size = new Size(55, 16);
            allVSModeRadioButton.TabIndex = 32;
            allVSModeRadioButton.TabStop = true;
            allVSModeRadioButton.Text = "Round robin";
            allVSModeRadioButton.UseVisualStyleBackColor = true;
            allVSModeRadioButton.CheckedChanged += new EventHandler(allVSModeRadioButton_CheckedChanged);
            oneVSModeRadioButton.AutoSize = true;
            oneVSModeRadioButton.Checked = true;
            oneVSModeRadioButton.Enabled = false;
            oneVSModeRadioButton.Location = new Point(7, 4);
            oneVSModeRadioButton.Name = "oneVSModeRadioButton";
            oneVSModeRadioButton.Size = new Size(116, 16);
            oneVSModeRadioButton.TabIndex = 31;
            oneVSModeRadioButton.TabStop = true;
            oneVSModeRadioButton.Text = "One character vs. the rest";
            oneVSModeRadioButton.UseVisualStyleBackColor = true;
            oneVSModeRadioButton.CheckedChanged += new EventHandler(oneVSModeRadioButton_CheckedChanged);
            modeLabel.AutoSize = true;
            modeLabel.Location = new Point(53, 124);
            modeLabel.Name = "modeLabel";
            modeLabel.Size = new Size(86, 12);
            modeLabel.TabIndex = 36;
            modeLabel.Text = "Battle pairings:";
            modeLabel.Click += new EventHandler(modeLabel_Click);
            roundModeCheckBox.AutoSize = true;
            roundModeCheckBox.Enabled = false;
            roundModeCheckBox.Location = new Point(105, 66);
            roundModeCheckBox.Name = "roundModeCheckBox";
            roundModeCheckBox.Size = new Size(153, 16);
            roundModeCheckBox.TabIndex = 19;
            roundModeCheckBox.Text = "End the matches after the specified number of rounds.";
            roundModeCheckBox.UseVisualStyleBackColor = true;
            roundModeCheckBox.CheckedChanged += new EventHandler(roundModeCheckBox_CheckedChanged);
            bothModeRadioButton.AutoSize = true;
            bothModeRadioButton.Enabled = false;
            bothModeRadioButton.Location = new Point(388, 145);
            bothModeRadioButton.Name = "bothModeRadioButton";
            bothModeRadioButton.Size = new Size(88, 16);
            bothModeRadioButton.TabIndex = 43;
            bothModeRadioButton.Text = "Alternative between P1 and P2 sides";
            bothModeRadioButton.UseVisualStyleBackColor = true;
            bothModeRadioButton.CheckedChanged += new EventHandler(bothModeRadioButton_CheckedChanged);
            p2ModeRadioButton.AutoSize = true;
            p2ModeRadioButton.Enabled = false;
            p2ModeRadioButton.Location = new Point(292, 145);
            p2ModeRadioButton.Name = "p2ModeRadioButton";
            p2ModeRadioButton.Size = new Size(81, 16);
            p2ModeRadioButton.TabIndex = 42;
            p2ModeRadioButton.Text = "Always P2 side";
            p2ModeRadioButton.UseVisualStyleBackColor = true;
            p2ModeRadioButton.CheckedChanged += new EventHandler(p2ModeRadioButton_CheckedChanged);
            p1ModeRadioButton.AutoSize = true;
            p1ModeRadioButton.Checked = true;
            p1ModeRadioButton.Enabled = false;
            p1ModeRadioButton.Location = new Point(197, 145);
            p1ModeRadioButton.Name = "p1ModeRadioButton";
            p1ModeRadioButton.Size = new Size(81, 16);
            p1ModeRadioButton.TabIndex = 41;
            p1ModeRadioButton.TabStop = true;
            p1ModeRadioButton.Text = "Always P1 side";
            p1ModeRadioButton.UseVisualStyleBackColor = true;
            p1ModeRadioButton.CheckedChanged += new EventHandler(p1ModeRadioButton_CheckedChanged);
            teamSideLabel.AutoSize = true;
            teamSideLabel.Location = new Point(53, 147);
            teamSideLabel.Name = "teamSideLabel";
            teamSideLabel.Size = new Size(63, 12);
            teamSideLabel.TabIndex = 26;
            teamSideLabel.Text = "Side settings:";
            teamSideLabel.Click += new EventHandler(teamSideLabel_Click);
            autoButton.Enabled = false;
            autoButton.Location = new Point(24, 369);
            autoButton.Name = "autoButton";
            autoButton.Size = new Size(206, 23);
            autoButton.TabIndex = 45;
            autoButton.Text = "Read characters from the select.def file";
            autoButton.UseVisualStyleBackColor = true;
            autoButton.Click += new EventHandler(autoButton_Click);
            label13.AutoSize = true;
            label13.Location = new Point(492, 95);
            label13.Name = "label13";
            label13.Size = new Size(11, 12);
            label13.TabIndex = 23;
            label13.Text = "p";
            colorTextBox.Enabled = false;
            colorTextBox.Font = new Font("MS UI Gothic", 10f, FontStyle.Regular, GraphicsUnit.Point, 128);
            colorTextBox.ImeMode = ImeMode.Disable;
            colorTextBox.Location = new Point(437, 91);
            colorTextBox.MaxLength = 2;
            colorTextBox.Name = "colorTextBox";
            colorTextBox.Size = new Size(49, 21);
            colorTextBox.TabIndex = 30;
            colorTextBox.Text = "12";
            colorTextBox.TextAlign = HorizontalAlignment.Right;
            colorTextBox.Click += new EventHandler(colorTextBox_Click);
            label12.AutoSize = true;
            label12.Location = new Point(492, 47);
            label12.Name = "label12";
            label12.Size = new Size(36, 12);
            label12.TabIndex = 21;
            label12.Text = "minutes";
            roundTimeTextBox.Enabled = false;
            roundTimeTextBox.Font = new Font("MS UI Gothic", 10f, FontStyle.Regular, GraphicsUnit.Point, 128);
            roundTimeTextBox.ImeMode = ImeMode.Disable;
            roundTimeTextBox.Location = new Point(437, 43);
            roundTimeTextBox.MaxLength = 3;
            roundTimeTextBox.Name = "roundTimeTextBox";
            roundTimeTextBox.Size = new Size(49, 21);
            roundTimeTextBox.TabIndex = 18;
            roundTimeTextBox.Text = "10";
            roundTimeTextBox.TextAlign = HorizontalAlignment.Right;
            roundTimeTextBox.Click += new EventHandler(roundTimeTextBox_Click);
            label11.AutoSize = true;
            label11.Location = new Point(251, 95);
            label11.Name = "label11";
            label11.Size = new Size(36, 12);
            label11.TabIndex = 19;
            label11.Text = "times";
            colorLabel.AutoSize = true;
            colorLabel.Location = new Point(343, 96);
            colorLabel.Name = "colorLabel";
            colorLabel.Size = new Size(88, 12);
            colorLabel.TabIndex = 18;
            colorLabel.Text = "Default color:";
            colorLabel.Click += new EventHandler(colorLabel_Click);
            retryTextBox2.Enabled = false;
            retryTextBox2.Font = new Font("MS UI Gothic", 10f, FontStyle.Regular, GraphicsUnit.Point, 128);
            retryTextBox2.ImeMode = ImeMode.Disable;
            retryTextBox2.Location = new Point(196, 91);
            retryTextBox2.MaxLength = 2;
            retryTextBox2.Name = "retryTextBox2";
            retryTextBox2.Size = new Size(49, 21);
            retryTextBox2.TabIndex = 29;
            retryTextBox2.Text = "1";
            retryTextBox2.TextAlign = HorizontalAlignment.Right;
            retryTextBox2.Click += new EventHandler(retryTextBox2_Click);
            label9.AutoSize = true;
            label9.Location = new Point(252, 47);
            label9.Name = "label9";
            label9.Size = new Size(64, 12);
            label9.TabIndex = 16;
            label9.Text = "round(s)";
            roundTextBox.Enabled = false;
            roundTextBox.Font = new Font("MS UI Gothic", 10f, FontStyle.Regular, GraphicsUnit.Point, 128);
            roundTextBox.ImeMode = ImeMode.Disable;
            roundTextBox.Location = new Point(196, 43);
            roundTextBox.MaxLength = 2;
            roundTextBox.Name = "roundTextBox";
            roundTextBox.Size = new Size(49, 21);
            roundTextBox.TabIndex = 17;
            roundTextBox.Text = "1";
            roundTextBox.TextAlign = HorizontalAlignment.Right;
            roundTextBox.Click += new EventHandler(roundTextBox_Click);
            roundTimeLabel.AutoSize = true;
            roundTimeLabel.Location = new Point(342, 47);
            roundTimeLabel.Name = "roundTimeLabel";
            roundTimeLabel.Size = new Size(82, 12);
            roundTimeLabel.TabIndex = 14;
            roundTimeLabel.Text = "Round time:";
            roundTimeLabel.Click += new EventHandler(roundTimeLabel_Click);
            roundTextLabel.AutoSize = true;
            roundTextLabel.Location = new Point(53, 47);
            roundTextLabel.Name = "roundTextLabel";
            roundTextLabel.Size = new Size(66, 12);
            roundTextLabel.TabIndex = 13;
            roundTextLabel.Text = "Number of rounds:";
            roundTextLabel.Click += new EventHandler(roundTextLabel_Click);
            retryTextLabel.AutoSize = true;
            retryTextLabel.Location = new Point(53, 96);
            retryTextLabel.Name = "retryTextLabel";
            retryTextLabel.Size = new Size(137, 12);
            retryTextLabel.TabIndex = 12;
            retryTextLabel.Text = "Times to retry after crash:";
            retryTextLabel.Click += new EventHandler(retryTextLabel_Click);
            enableAutoModecheckBox.AutoSize = true;
            enableAutoModecheckBox.Location = new Point(30, 21);
            enableAutoModecheckBox.Name = "enableAutoModecheckBox";
            enableAutoModecheckBox.Size = new Size(152, 16);
            enableAutoModecheckBox.TabIndex = 16;
            enableAutoModecheckBox.Text = "Enable continuous match mode";
            enableAutoModecheckBox.UseVisualStyleBackColor = true;
            enableAutoModecheckBox.CheckedChanged += new EventHandler(enableAutoModecheckBox_CheckedChanged);
            charListTextBox.ContextMenuStrip = charContextMenuStrip;
            charListTextBox.Enabled = false;
            charListTextBox.Font = new Font("ＭＳ ゴシック", 9f, FontStyle.Regular, GraphicsUnit.Point, 128);
            charListTextBox.Location = new Point(17, 168);
            charListTextBox.Multiline = true;
            charListTextBox.Name = "charListTextBox";
            charListTextBox.ScrollBars = ScrollBars.Vertical;
            charListTextBox.Size = new Size(567, 195);
            charListTextBox.TabIndex = 44;
            charListTextBox.Text = "; Specify the characters to use for continuous battle mode. \r\n; \r\n\r\n; --- P1 side ----\r\nkfm, 1\r\n\r\n; Specify the P2 side after this line \r\nkfm, 2\r\nkfm, 3\r\nkfm, 4\r\nkfm, 5\r\nkfm, 6\r\n";
            charListTextBox.Click += new EventHandler(charListTextBox_Click);
            charContextMenuStrip.Items.AddRange(new ToolStripItem[3]
            {
         allSelToolStripMenuItem,
         copyToolStripMenuItem,
         pasteToolStripMenuItem
            });
            charContextMenuStrip.Name = "charContextMenuStrip";
            charContextMenuStrip.Size = new Size(184, 70);
            allSelToolStripMenuItem.Name = "allSelToolStripMenuItem";
            allSelToolStripMenuItem.ShortcutKeys = Keys.A | Keys.Control;
            allSelToolStripMenuItem.Size = new Size(183, 22);
            allSelToolStripMenuItem.Text = "Select all";
            allSelToolStripMenuItem.Click += new EventHandler(allSelToolStripMenuItem_Click);
            copyToolStripMenuItem.Name = "copyToolStripMenuItem";
            copyToolStripMenuItem.ShortcutKeys = Keys.C | Keys.Control;
            copyToolStripMenuItem.Size = new Size(183, 22);
            copyToolStripMenuItem.Text = "Copy";
            copyToolStripMenuItem.Click += new EventHandler(copyToolStripMenuItem_Click);
            pasteToolStripMenuItem.Name = "pasteToolStripMenuItem";
            pasteToolStripMenuItem.ShortcutKeys = Keys.V | Keys.Control;
            pasteToolStripMenuItem.Size = new Size(183, 22);
            pasteToolStripMenuItem.Text = "Paste";
            pasteToolStripMenuItem.Click += new EventHandler(pasteToolStripMenuItem_Click);
            mugenOpenFileDialog.AddExtension = false;
            mugenOpenFileDialog.DefaultExt = "exe";
            mugenOpenFileDialog.Filter = "mugen.exe |*.exe";
            mugenOpenFileDialog.InitialDirectory = "C:\\";
            mugenOpenFileDialog.ReadOnlyChecked = true;
            mugenOpenFileDialog.RestoreDirectory = true;
            mugenOpenFileDialog.SupportMultiDottedExtensions = true;
            mugenOpenFileDialog.Title = "Select the mugen executable";
            selectOpenFileDialog.AddExtension = false;
            selectOpenFileDialog.DefaultExt = "exe";
            selectOpenFileDialog.Filter = "select.def file |select.def";
            selectOpenFileDialog.InitialDirectory = "C:\\";
            selectOpenFileDialog.ReadOnlyChecked = true;
            selectOpenFileDialog.RestoreDirectory = true;
            selectOpenFileDialog.SupportMultiDottedExtensions = true;
            selectOpenFileDialog.Title = "Select the select.def file";
            toolTip1.AutomaticDelay = 500000;
            toolTip1.AutoPopDelay = 5000000;
            toolTip1.InitialDelay = 5000000;
            toolTip1.IsBalloon = true;
            toolTip1.ReshowDelay = 1000000;
            helpCheckBox.Appearance = Appearance.Button;
            helpCheckBox.AutoSize = true;
            helpCheckBox.BackgroundImageLayout = ImageLayout.None;
            helpCheckBox.ImageAlign = ContentAlignment.MiddleLeft;
            helpCheckBox.Location = new Point(568, 9);
            helpCheckBox.Name = "helpCheckBox";
            helpCheckBox.Padding = new Padding(1);
            helpCheckBox.Size = new Size(43, 22);
            helpCheckBox.TabIndex = 25;
            helpCheckBox.TabStop = false;
            helpCheckBox.Text = "       ";
            helpCheckBox.UseVisualStyleBackColor = true;
            helpCheckBox.CheckedChanged += new EventHandler(helpCheckBox_CheckedChanged);
            helpCheckBox.MouseHover += new EventHandler(helpCheckBox_MouseHover);
            AutoScaleDimensions = new SizeF(6f, 12f);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(619, 523);
            Controls.Add(helpCheckBox);
            Controls.Add(tabControl1);
            Controls.Add(profileNameTextBox);
            Controls.Add(profileNameLabel);
            Controls.Add(cancelButton);
            Controls.Add(okButton);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            Icon = (Icon)componentResourceManager.GetObject("$this.Icon");
            MaximizeBox = false;
            MinimizeBox = false;
            Name = nameof(ProfileConfigPanel);
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Profile settings";
            Deactivate += new EventHandler(ProfileConfigPanel_Deactivate);
            FormClosing += new FormClosingEventHandler(ProfileConfigPanel_FormClosing);
            Load += new EventHandler(ProfileConfigPanel_Load);
            tabControl1.ResumeLayout(false);
            tabPage1.ResumeLayout(false);
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            tabPage2.ResumeLayout(false);
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            charContextMenuStrip.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }
    }
}
