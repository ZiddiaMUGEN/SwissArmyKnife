// Decompiled with JetBrains decompiler
// Type: SwissArmyKnifeForMugen.QuickVSModePanel
// Assembly: SAKnifeWM, Version=1.0.8.0, Culture=neutral, PublicKeyToken=null
// MVID: 09478AD8-365C-4BF3-BEA1-B5785151259B
// Assembly location: C:\Users\ziddi\Downloads\Swiss Army Knife 1.1 Conversion\Swiss Army Knife 1.1 Conversion\SAKnifeWM.exe

using SwissArmyKnifeForMugen.Configs;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;

namespace SwissArmyKnifeForMugen.Displays
{
    /// <summary>
    /// Form displayed while launching Quick VS mode through a Profile.
    /// </summary>
    public class QuickVSModePanel : Form
    {
        private DialogResult _result = DialogResult.Cancel;
        private const short SWP_NOMOVE = 2;
        private const short SWP_NOSIZE = 1;
        private const short SWP_NOZORDER = 4;
        private const int SWP_SHOWWINDOW = 64;
        private const int SWP_HIDEWINDOW = 128;
        private const int SWP_NOACTIVATE = 16;
        private const int SWP_ASYNC = 16384;
        private int locX;
        private int locY;
        private string[] allCharDefs = new string[0];
        private Button cancelButton;
        private Button okButton;
        private GroupBox groupBox1;
        private TextBox textBox3;
        private Button folderButton;
        private GroupBox groupBox2;
        private Label label5;
        private ComboBox roundComboBox;
        private Button swapButton;
        private CheckBox noMusicCheckBox;
        private CheckBox noSoundCheckBox;
        private Label label6;
        private ComboBox stageComboBox;
        private TabControl p1tabControl;
        private TabPage p1tabPage;
        private TabPage p3tabPage;
        private NumericUpDown powerP1NumericUpDown;
        private NumericUpDown lifeP1NumericUpDown;
        private CheckBox powerP1CheckBox;
        private CheckBox lifeP1CheckBox;
        private CheckBox aiP1CheckBox;
        private ComboBox colorP1ComboBox;
        private Label label2;
        private ComboBox defP1ComboBox;
        private Label label1;
        private TabControl p2tabControl;
        private TabPage p2tabPage;
        private TabPage p4tabPage;
        private NumericUpDown powerP2NumericUpDown;
        private NumericUpDown lifeP2NumericUpDown;
        private CheckBox powerP2CheckBox;
        private CheckBox lifeP2CheckBox;
        private CheckBox aiP2CheckBox;
        private ComboBox colorP2ComboBox;
        private Label label3;
        private ComboBox defP2ComboBox;
        private Label label4;
        private NumericUpDown powerP3NumericUpDown;
        private NumericUpDown lifeP3NumericUpDown;
        private CheckBox powerP3CheckBox;
        private CheckBox lifeP3CheckBox;
        private CheckBox aiP3CheckBox;
        private ComboBox colorP3ComboBox;
        private Label label7;
        private ComboBox defP3ComboBox;
        private Label label8;
        private NumericUpDown powerP4NumericUpDown;
        private NumericUpDown lifeP4NumericUpDown;
        private CheckBox powerP4CheckBox;
        private CheckBox lifeP4CheckBox;
        private CheckBox aiP4CheckBox;
        private ComboBox colorP4ComboBox;
        private Label label9;
        private ComboBox defP4ComboBox;
        private Label label10;

        public QuickVSModePanel() => InitializeComponent();

        [DllImport("user32.dll")]
        public static extern IntPtr SetWindowPos(
          IntPtr hWnd,
          int hWndInsertAfter,
          int x,
          int Y,
          int cx,
          int cy,
          int wFlags);

        [DllImport("user32.dll")]
        private static extern IntPtr GetForegroundWindow();

        public void SetLoc(int x, int y)
        {
            locX = x;
            locY = y;
        }

        public DialogResult GetResult() => _result;

        private void Init()
        {
            colorP1ComboBox.Items.Add("1");
            colorP1ComboBox.Items.Add("2");
            colorP1ComboBox.Items.Add("3");
            colorP1ComboBox.Items.Add("4");
            colorP1ComboBox.Items.Add("5");
            colorP1ComboBox.Items.Add("6");
            colorP1ComboBox.Items.Add("7");
            colorP1ComboBox.Items.Add("8");
            colorP1ComboBox.Items.Add("9");
            colorP1ComboBox.Items.Add("10");
            colorP1ComboBox.Items.Add("11");
            colorP1ComboBox.Items.Add("12");
            colorP2ComboBox.Items.Add("1");
            colorP2ComboBox.Items.Add("2");
            colorP2ComboBox.Items.Add("3");
            colorP2ComboBox.Items.Add("4");
            colorP2ComboBox.Items.Add("5");
            colorP2ComboBox.Items.Add("6");
            colorP2ComboBox.Items.Add("7");
            colorP2ComboBox.Items.Add("8");
            colorP2ComboBox.Items.Add("9");
            colorP2ComboBox.Items.Add("10");
            colorP2ComboBox.Items.Add("11");
            colorP2ComboBox.Items.Add("12");
            colorP3ComboBox.Items.Add("1");
            colorP3ComboBox.Items.Add("2");
            colorP3ComboBox.Items.Add("3");
            colorP3ComboBox.Items.Add("4");
            colorP3ComboBox.Items.Add("5");
            colorP3ComboBox.Items.Add("6");
            colorP3ComboBox.Items.Add("7");
            colorP3ComboBox.Items.Add("8");
            colorP3ComboBox.Items.Add("9");
            colorP3ComboBox.Items.Add("10");
            colorP3ComboBox.Items.Add("11");
            colorP3ComboBox.Items.Add("12");
            colorP4ComboBox.Items.Add("1");
            colorP4ComboBox.Items.Add("2");
            colorP4ComboBox.Items.Add("3");
            colorP4ComboBox.Items.Add("4");
            colorP4ComboBox.Items.Add("5");
            colorP4ComboBox.Items.Add("6");
            colorP4ComboBox.Items.Add("7");
            colorP4ComboBox.Items.Add("8");
            colorP4ComboBox.Items.Add("9");
            colorP4ComboBox.Items.Add("10");
            colorP4ComboBox.Items.Add("11");
            colorP4ComboBox.Items.Add("12");
            roundComboBox.Items.Add("1");
            roundComboBox.Items.Add("2");
            roundComboBox.Items.Add("3");
            roundComboBox.Items.Add("4");
            roundComboBox.Items.Add("5");
            stageComboBox.Items.Add("(No setting)");
            MugenProfile currentProfile = ProfileManager.MainObj().GetCurrentProfile();
            if (currentProfile == null)
                return;
            string path = Path.Combine(currentProfile.GetMugenPath(), "stages");
            foreach (string file in Directory.GetFiles(path, "*.def", SearchOption.AllDirectories))
            {
                string str = file.Substring(path.Length + 1);
                stageComboBox.Items.Add(str.Remove(str.Length - 4).Replace('\\', '/'));
            }
        }

        private void GetAllDefFiles()
        {
            List<string> stringList = new List<string>();
            MugenProfile currentProfile = ProfileManager.MainObj().GetCurrentProfile();
            if (currentProfile == null)
                return;
            string path = Path.Combine(currentProfile.GetMugenPath(), "chars");
            foreach (string file in Directory.GetFiles(path, "*.def", SearchOption.AllDirectories))
            {
                string str = file.Substring(path.Length + 1);
                stringList.Add(str);
            }
            allCharDefs = stringList.ToArray();
        }

        private void LoadProfile()
        {
            MugenProfile currentProfile = ProfileManager.MainObj().GetCurrentProfile();
            if (currentProfile == null)
                return;
            QuickVSProfile quickVsProfile = currentProfile.GetQuickVSProfile();
            if (quickVsProfile == null)
                return;
            defP1ComboBox.Items.AddRange(quickVsProfile.GetP1History());
            defP1ComboBox.Items.AddRange(allCharDefs);
            defP1ComboBox.Text = quickVsProfile.GetP1Def();
            colorP1ComboBox.Text = quickVsProfile.GetP1Color().ToString();
            aiP1CheckBox.Checked = quickVsProfile.GetP1AI();
            lifeP1CheckBox.Checked = quickVsProfile.GetP1LifeFlag();
            lifeP1NumericUpDown.Text = quickVsProfile.GetP1LifeValue().ToString();
            powerP1CheckBox.Checked = quickVsProfile.GetP1PowerFlag();
            powerP1NumericUpDown.Text = quickVsProfile.GetP1PowerValue().ToString();
            defP2ComboBox.Items.AddRange(quickVsProfile.GetP2History());
            defP2ComboBox.Items.AddRange(allCharDefs);
            defP2ComboBox.Text = quickVsProfile.GetP2Def();
            colorP2ComboBox.Text = quickVsProfile.GetP2Color().ToString();
            aiP2CheckBox.Checked = quickVsProfile.GetP2AI();
            lifeP2CheckBox.Checked = quickVsProfile.GetP2LifeFlag();
            lifeP2NumericUpDown.Text = quickVsProfile.GetP2LifeValue().ToString();
            powerP2CheckBox.Checked = quickVsProfile.GetP2PowerFlag();
            powerP2NumericUpDown.Text = quickVsProfile.GetP2PowerValue().ToString();
            defP3ComboBox.Items.Add("(No setting)");
            defP3ComboBox.Items.AddRange(quickVsProfile.GetP3History());
            defP3ComboBox.Items.AddRange(allCharDefs);
            defP3ComboBox.Text = quickVsProfile.GetP3Def();
            colorP3ComboBox.Text = quickVsProfile.GetP3Color().ToString();
            aiP3CheckBox.Checked = quickVsProfile.GetP3AI();
            lifeP3CheckBox.Checked = quickVsProfile.GetP3LifeFlag();
            lifeP3NumericUpDown.Text = quickVsProfile.GetP3LifeValue().ToString();
            powerP3CheckBox.Checked = quickVsProfile.GetP3PowerFlag();
            powerP3NumericUpDown.Text = quickVsProfile.GetP3PowerValue().ToString();
            defP4ComboBox.Items.Add("(No setting)");
            defP4ComboBox.Items.AddRange(quickVsProfile.GetP4History());
            defP4ComboBox.Items.AddRange(allCharDefs);
            defP4ComboBox.Text = quickVsProfile.GetP4Def();
            colorP4ComboBox.Text = quickVsProfile.GetP4Color().ToString();
            aiP4CheckBox.Checked = quickVsProfile.GetP4AI();
            lifeP4CheckBox.Checked = quickVsProfile.GetP4LifeFlag();
            lifeP4NumericUpDown.Text = quickVsProfile.GetP4LifeValue().ToString();
            powerP4CheckBox.Checked = quickVsProfile.GetP4PowerFlag();
            powerP4NumericUpDown.Text = quickVsProfile.GetP4PowerValue().ToString();
            roundComboBox.Text = quickVsProfile.GetRounds().ToString();
            noMusicCheckBox.Checked = quickVsProfile.GetNoMusicFlag();
            noSoundCheckBox.Checked = quickVsProfile.GetNoSoundFlag();
            if (quickVsProfile.GetStage() == "")
                stageComboBox.Text = "(No setting)";
            else
                stageComboBox.Text = quickVsProfile.GetStage();
        }

        private void SaveProfile()
        {
            MugenProfile currentProfile = ProfileManager.MainObj().GetCurrentProfile();
            if (currentProfile == null)
                return;
            QuickVSProfile quickVsProfile = currentProfile.GetQuickVSProfile();
            if (quickVsProfile == null)
                return;
            quickVsProfile.SetP1Def(defP1ComboBox.Text);
            int result;
            if (int.TryParse(colorP1ComboBox.Text, out result))
                quickVsProfile.SetP1Color(result);
            quickVsProfile.SetP1AI(aiP1CheckBox.Checked);
            quickVsProfile.SetP1LifeFlag(lifeP1CheckBox.Checked);
            if (int.TryParse(lifeP1NumericUpDown.Text, out result))
                quickVsProfile.SetP1LifeValue(result);
            quickVsProfile.SetP1PowerFlag(powerP1CheckBox.Checked);
            if (int.TryParse(powerP1NumericUpDown.Text, out result))
                quickVsProfile.SetP1PowerValue(result);
            quickVsProfile.SetP2Def(defP2ComboBox.Text);
            if (int.TryParse(colorP2ComboBox.Text, out result))
                quickVsProfile.SetP2Color(result);
            quickVsProfile.SetP2AI(aiP2CheckBox.Checked);
            quickVsProfile.SetP2LifeFlag(lifeP2CheckBox.Checked);
            if (int.TryParse(lifeP2NumericUpDown.Text, out result))
                quickVsProfile.SetP2LifeValue(result);
            quickVsProfile.SetP2PowerFlag(powerP2CheckBox.Checked);
            if (int.TryParse(powerP2NumericUpDown.Text, out result))
                quickVsProfile.SetP2PowerValue(result);
            string text1 = defP3ComboBox.Text;
            if (text1 != null && text1 != "" && text1 != "(No setting)")
            {
                quickVsProfile.SetP3Def(defP3ComboBox.Text);
                if (int.TryParse(colorP3ComboBox.Text, out result))
                    quickVsProfile.SetP3Color(result);
                quickVsProfile.SetP3AI(aiP3CheckBox.Checked);
                quickVsProfile.SetP3LifeFlag(lifeP3CheckBox.Checked);
                if (int.TryParse(lifeP3NumericUpDown.Text, out result))
                    quickVsProfile.SetP3LifeValue(result);
                quickVsProfile.SetP3PowerFlag(powerP3CheckBox.Checked);
                if (int.TryParse(powerP3NumericUpDown.Text, out result))
                    quickVsProfile.SetP3PowerValue(result);
            }
            else
                quickVsProfile.SetP3Def("");
            string text2 = defP4ComboBox.Text;
            if (text2 != null && text2 != "" && text2 != "(No setting)")
            {
                quickVsProfile.SetP4Def(defP4ComboBox.Text);
                if (int.TryParse(colorP4ComboBox.Text, out result))
                    quickVsProfile.SetP4Color(result);
                quickVsProfile.SetP4AI(aiP4CheckBox.Checked);
                quickVsProfile.SetP4LifeFlag(lifeP4CheckBox.Checked);
                if (int.TryParse(lifeP4NumericUpDown.Text, out result))
                    quickVsProfile.SetP4LifeValue(result);
                quickVsProfile.SetP4PowerFlag(powerP4CheckBox.Checked);
                if (int.TryParse(powerP4NumericUpDown.Text, out result))
                    quickVsProfile.SetP4PowerValue(result);
            }
            else
                quickVsProfile.SetP4Def("");
            if (int.TryParse(roundComboBox.Text, out result))
                quickVsProfile.SetRounds(result);
            quickVsProfile.SetNoMusicFlag(noMusicCheckBox.Checked);
            quickVsProfile.SetNoSoundFlag(noSoundCheckBox.Checked);
            if (stageComboBox.Text == null || stageComboBox.Text == "(No setting)")
                quickVsProfile.SetStage("");
            else
                quickVsProfile.SetStage(stageComboBox.Text);
            quickVsProfile.SaveConfigData();
        }

        private bool CheckValues()
        {
            MugenProfile currentProfile = ProfileManager.MainObj().GetCurrentProfile();
            if (currentProfile == null)
            {
                int num = (int)MessageBox.Show("No profile has been selected.", "Swiss Army Knife");
                return false;
            }
            string mugenPath = currentProfile.GetMugenPath();
            if (mugenPath == null)
            {
                int num = (int)MessageBox.Show("The mugen executable has not been specified.", "Swiss Army Knife");
                return false;
            }
            string path1 = Path.Combine(mugenPath, "chars");
            string text1 = defP1ComboBox.Text;
            if (text1 == null || text1 == "" || text1 == "(No setting)")
            {
                int num = (int)MessageBox.Show("No def file has been specified for player 1.", "Swiss Army Knife");
                p1tabControl.SelectedIndex = 0;
                defP1ComboBox.Focus();
                defP1ComboBox.SelectAll();
                return false;
            }
            string str1 = Path.Combine(path1, text1);
            if (Path.GetExtension(text1) == ".def")
            {
                if (!File.Exists(str1))
                {
                    int num = (int)MessageBox.Show("The specified def file for player 1 does not exist. \r\n\r\n" + str1, "Swiss Army Knife");
                    p1tabControl.SelectedIndex = 0;
                    defP1ComboBox.Focus();
                    defP1ComboBox.SelectAll();
                    return false;
                }
            }
            else
            {
                if (!Directory.Exists(str1))
                {
                    int num = (int)MessageBox.Show("The specified folder for player 1 does not exist. \r\n\r\n" + str1, "Swiss Army Knife");
                    p1tabControl.SelectedIndex = 0;
                    defP1ComboBox.Focus();
                    defP1ComboBox.SelectAll();
                    return false;
                }
                string path2 = Path.GetFileName(str1) + ".def";
                string path = Path.Combine(str1, path2);
                if (!File.Exists(path))
                {
                    int num = (int)MessageBox.Show("The specified def file for player 1 does not exist. \r\n\r\n" + path, "Swiss Army Knife");
                    p1tabControl.SelectedIndex = 0;
                    defP1ComboBox.Focus();
                    defP1ComboBox.SelectAll();
                    return false;
                }
            }
            int result;
            if (!int.TryParse(colorP1ComboBox.Text, out result))
            {
                int num = (int)MessageBox.Show("The color for player 1 should be an integer between 1 and 12.", "Swiss Army Knife");
                p1tabControl.SelectedIndex = 0;
                colorP1ComboBox.Focus();
                colorP1ComboBox.SelectAll();
                return false;
            }
            string text2 = defP2ComboBox.Text;
            if (text2 == null || text2 == "" || text2 == "(No setting)")
            {
                int num = (int)MessageBox.Show("No def file has been specified for player 2.", "Swiss Army Knife");
                p2tabControl.SelectedIndex = 0;
                defP2ComboBox.Focus();
                defP2ComboBox.SelectAll();
                return false;
            }
            string str2 = Path.Combine(path1, text2);
            if (Path.GetExtension(text2) == ".def")
            {
                if (!File.Exists(str2))
                {
                    int num = (int)MessageBox.Show("The specified def file for player 2 does not exist. \r\n\r\n" + str2, "Swiss Army Knife");
                    p2tabControl.SelectedIndex = 0;
                    defP2ComboBox.Focus();
                    defP2ComboBox.SelectAll();
                    return false;
                }
            }
            else
            {
                if (!Directory.Exists(str2))
                {
                    int num = (int)MessageBox.Show("The specified folder for player 2 does not exist. \r\n\r\n" + str2, "Swiss Army Knife");
                    p2tabControl.SelectedIndex = 0;
                    defP2ComboBox.Focus();
                    defP2ComboBox.SelectAll();
                    return false;
                }
                string path2 = Path.GetFileName(str2) + ".def";
                string path = Path.Combine(str2, path2);
                if (!File.Exists(path))
                {
                    int num = (int)MessageBox.Show("The specified def file for player 2 does not exist. \r\n\r\n" + path, "Swiss Army Knife");
                    p2tabControl.SelectedIndex = 0;
                    defP2ComboBox.Focus();
                    defP2ComboBox.SelectAll();
                    return false;
                }
            }
            if (!int.TryParse(colorP2ComboBox.Text, out result))
            {
                int num = (int)MessageBox.Show("The color for player 2 should be an integer between 1 and 12.", "Swiss Army Knife");
                p2tabControl.SelectedIndex = 0;
                colorP2ComboBox.Focus();
                colorP2ComboBox.SelectAll();
                return false;
            }
            string text3 = defP3ComboBox.Text;
            if (text3 != null && text3 != "" && text3 != "(No setting)")
            {
                string str3 = Path.Combine(path1, text3);
                if (Path.GetExtension(text3) == ".def")
                {
                    if (!File.Exists(str3))
                    {
                        int num = (int)MessageBox.Show("The specified def file for player 3 does not exist. \r\n\r\n" + str3, "Swiss Army Knife");
                        p1tabControl.SelectedIndex = 1;
                        defP3ComboBox.Focus();
                        defP3ComboBox.SelectAll();
                        return false;
                    }
                }
                else
                {
                    if (!Directory.Exists(str3))
                    {
                        int num = (int)MessageBox.Show("The specified folder for player 3 does not exist. \r\n\r\n" + str3, "Swiss Army Knife");
                        p1tabControl.SelectedIndex = 1;
                        defP3ComboBox.Focus();
                        defP3ComboBox.SelectAll();
                        return false;
                    }
                    string path2 = Path.GetFileName(str3) + ".def";
                    string path = Path.Combine(str3, path2);
                    if (!File.Exists(path))
                    {
                        int num = (int)MessageBox.Show("The specified def file for player 3 does not exist. \r\n\r\n" + path, "Swiss Army Knife");
                        p1tabControl.SelectedIndex = 1;
                        defP3ComboBox.Focus();
                        defP3ComboBox.SelectAll();
                        return false;
                    }
                }
                if (!int.TryParse(colorP3ComboBox.Text, out result))
                {
                    int num = (int)MessageBox.Show("The color for player 3 should be an integer between 1 and 12.", "Swiss Army Knife");
                    p1tabControl.SelectedIndex = 1;
                    colorP3ComboBox.Focus();
                    colorP3ComboBox.SelectAll();
                    return false;
                }
            }
            string text4 = defP4ComboBox.Text;
            if (text4 != null && text4 != "" && text4 != "(No setting)")
            {
                string str3 = Path.Combine(path1, text4);
                if (Path.GetExtension(text4) == ".def")
                {
                    if (!File.Exists(str3))
                    {
                        int num = (int)MessageBox.Show("The specified def file for player 4 does not exist. \r\n\r\n" + str3, "Swiss Army Knife");
                        p2tabControl.SelectedIndex = 1;
                        defP4ComboBox.Focus();
                        defP4ComboBox.SelectAll();
                        return false;
                    }
                }
                else
                {
                    if (!Directory.Exists(str3))
                    {
                        int num = (int)MessageBox.Show("The specified folder for player 4 does not exist. \r\n\r\n" + str3, "Swiss Army Knife");
                        p2tabControl.SelectedIndex = 1;
                        defP4ComboBox.Focus();
                        defP4ComboBox.SelectAll();
                        return false;
                    }
                    string path2 = Path.GetFileName(str3) + ".def";
                    string path = Path.Combine(str3, path2);
                    if (!File.Exists(path))
                    {
                        int num = (int)MessageBox.Show("The specified def file for player 4 does not exist. \r\n\r\n" + path, "Swiss Army Knife");
                        p2tabControl.SelectedIndex = 1;
                        defP4ComboBox.Focus();
                        defP4ComboBox.SelectAll();
                        return false;
                    }
                }
                if (!int.TryParse(colorP4ComboBox.Text, out result))
                {
                    int num = (int)MessageBox.Show("The color for player 4 should be an integer between 1 and 12.", "Swiss Army Knife");
                    p2tabControl.SelectedIndex = 1;
                    colorP4ComboBox.Focus();
                    colorP4ComboBox.SelectAll();
                    return false;
                }
            }
            string text5 = stageComboBox.Text;
            if (text5 != null && text5 != "" && text5 != "(No setting)")
            {
                string path = Path.Combine(Path.Combine(mugenPath, "stages"), text5) + ".def";
                if (!File.Exists(path))
                {
                    int num = (int)MessageBox.Show("The specified def file for the stage does not exist. \r\n\r\n" + path, "Swiss Army Knife");
                    stageComboBox.Focus();
                    stageComboBox.SelectAll();
                    return false;
                }
            }
            if (!int.TryParse(roundComboBox.Text, out result))
            {
                int num = (int)MessageBox.Show("The number of rounds should be an integer between 1 and 99.", "Swiss Army Knife");
                roundComboBox.Focus();
                roundComboBox.SelectAll();
                return false;
            }
            if (result >= 1 && result <= 99)
                return true;
            int num1 = (int)MessageBox.Show("The number of rounds should be an integer between 1 and 99.", "Swiss Army Knife");
            roundComboBox.Focus();
            roundComboBox.SelectAll();
            return false;
        }

        private void QuickVSModePanel_Load(object sender, EventArgs e)
        {
            SetDesktopLocation(locX, locY);
            Init();
            LoadProfile();
        }

        private void folderButton_Click(object sender, EventArgs e)
        {
            MugenProfile currentProfile = ProfileManager.MainObj().GetCurrentProfile();
            if (currentProfile == null)
                return;
            string mugenPath = currentProfile.GetMugenPath();
            if (mugenPath == null)
                return;
            string str = Path.Combine(mugenPath, "chars");
            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.FileName = str;
            startInfo.WorkingDirectory = str;
            startInfo.Arguments = "";
            startInfo.CreateNoWindow = false;
            startInfo.UseShellExecute = true;
            startInfo.Verb = "Open";
            try
            {
                Process.Start(startInfo);
            }
            catch
            {
            }
            IntPtr num1 = IntPtr.Zero;
            int num2 = 20;
            do
            {
                Thread.Sleep(50);
                if (num2-- > 0)
                {
                    num1 = GetForegroundWindow();
                    if (num1 == Handle)
                        num1 = IntPtr.Zero;
                }
                else
                    break;
            }
            while (num1 == IntPtr.Zero);
            if (!(num1 != IntPtr.Zero))
                return;
            IntPtr hWnd = num1;
            Point location = Location;
            int num3 = location.X + Size.Width + 5;
            location = Location;
            int y = location.Y;
            Size size = Size;
            int width = size.Width;
            size = Size;
            int height = size.Height;
            int x = num3;
            int Y = y;
            int cx = width;
            int cy = height;
            SetWindowPos(hWnd, 0, x, Y, cx, cy, 69);
        }

        private void defP1ComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void defP2ComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void colorP1ComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void colorP2ComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void aiP1CheckBox_CheckedChanged(object sender, EventArgs e)
        {
        }

        private void aiP2CheckBox_CheckedChanged(object sender, EventArgs e)
        {
        }

        private void roundComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            if (!CheckValues())
                return;
            _result = DialogResult.OK;
            SaveProfile();
            MainForm.MainObj().ActivateAll(true);
            Close();
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            _result = DialogResult.Cancel;
            Close();
        }

        private void defP1ComboBox_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = !e.Data.GetDataPresent(DataFormats.FileDrop) ? DragDropEffects.None : DragDropEffects.Link;
            Activate();
        }

        private void defP1ComboBox_DragDrop(object sender, DragEventArgs e)
        {
            string[] data = (string[])e.Data.GetData(DataFormats.FileDrop, false);
            if (data == null || data.Length < 1)
                return;
            MugenProfile currentProfile = ProfileManager.MainObj().GetCurrentProfile();
            if (currentProfile == null)
            {
                int num1 = (int)MessageBox.Show("No profile has been selected.", "Swiss Army Knife");
            }
            else
            {
                string mugenPath = currentProfile.GetMugenPath();
                if (mugenPath == null)
                {
                    int num2 = (int)MessageBox.Show("The mugen executable has not been specified.", "Swiss Army Knife");
                }
                else
                {
                    string str1 = null;
                    string str2 = null;
                    if (data.Length == 1)
                        str1 = data[0];
                    else if (data.Length > 1)
                    {
                        str1 = data[data.Length - 1];
                        str2 = data[data.Length - 2];
                    }
                    string str3 = Path.Combine(mugenPath, "chars");
                    if (str1 != null)
                    {
                        if (str1.IndexOf(str3) < 0)
                        {
                            int num3 = (int)MessageBox.Show("The path of the def file and the path of the chars folder do not belong to the same root. This character possibly belongs to a different mugen installation.", "Swiss Army Knife");
                            return;
                        }
                        defP1ComboBox.Text = str1.Substring(str3.Length + 1);
                    }
                    if (str2 == null)
                        return;
                    if (str2.IndexOf(str3) < 0)
                    {
                        int num4 = (int)MessageBox.Show("The path of the def file and the path of the chars folder do not belong to the same root. This character possibly belongs to a different mugen installation.", "Swiss Army Knife");
                    }
                    else
                        defP2ComboBox.Text = str2.Substring(str3.Length + 1);
                }
            }
        }

        private void p1Well_DragEnter(object sender, DragEventArgs e) => defP1ComboBox_DragEnter(sender, e);

        private void p1Well_DragDrop(object sender, DragEventArgs e) => defP1ComboBox_DragDrop(sender, e);

        private void defP2ComboBox_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = !e.Data.GetDataPresent(DataFormats.FileDrop) ? DragDropEffects.None : DragDropEffects.Link;
            Activate();
        }

        private void defP2ComboBox_DragDrop(object sender, DragEventArgs e)
        {
            string[] data = (string[])e.Data.GetData(DataFormats.FileDrop, false);
            if (data == null || data.Length < 1)
                return;
            MugenProfile currentProfile = ProfileManager.MainObj().GetCurrentProfile();
            if (currentProfile == null)
            {
                int num1 = (int)MessageBox.Show("No profile has been selected.", "Swiss Army Knife");
            }
            else
            {
                string mugenPath = currentProfile.GetMugenPath();
                if (mugenPath == null)
                {
                    int num2 = (int)MessageBox.Show("The mugen executable has not been specified.", "Swiss Army Knife");
                }
                else
                {
                    string str1 = null;
                    string str2 = null;
                    if (data.Length == 1)
                        str2 = data[0];
                    else if (data.Length > 1)
                    {
                        str1 = data[data.Length - 1];
                        str2 = data[data.Length - 2];
                    }
                    string str3 = Path.Combine(mugenPath, "chars");
                    if (str1 != null)
                    {
                        if (str1.IndexOf(str3) < 0)
                        {
                            int num3 = (int)MessageBox.Show("The path of the def file and the path of the chars folder do not belong to the same root. This character possibly belongs to a different mugen installation.", "Swiss Army Knife");
                            return;
                        }
                        defP1ComboBox.Text = str1.Substring(str3.Length + 1);
                    }
                    if (str2 == null)
                        return;
                    if (str2.IndexOf(str3) < 0)
                    {
                        int num4 = (int)MessageBox.Show("The path of the def file and the path of the chars folder do not belong to the same root. This character possibly belongs to a different mugen installation.", "Swiss Army Knife");
                    }
                    else
                        defP2ComboBox.Text = str2.Substring(str3.Length + 1);
                }
            }
        }

        private void p2Well_DragEnter(object sender, DragEventArgs e) => defP2ComboBox_DragEnter(sender, e);

        private void p2Well_DragDrop(object sender, DragEventArgs e) => defP2ComboBox_DragDrop(sender, e);

        private void swapButton_Click(object sender, EventArgs e)
        {
            string text1 = defP1ComboBox.Text;
            string text2 = colorP1ComboBox.Text;
            bool flag1 = aiP1CheckBox.Checked;
            bool flag2 = lifeP1CheckBox.Checked;
            string text3 = lifeP1NumericUpDown.Text;
            bool flag3 = powerP1CheckBox.Checked;
            string text4 = powerP1NumericUpDown.Text;
            defP1ComboBox.Text = defP2ComboBox.Text;
            colorP1ComboBox.Text = colorP2ComboBox.Text;
            aiP1CheckBox.Checked = aiP2CheckBox.Checked;
            lifeP1CheckBox.Checked = lifeP2CheckBox.Checked;
            lifeP1NumericUpDown.Text = lifeP2NumericUpDown.Text;
            powerP1CheckBox.Checked = powerP2CheckBox.Checked;
            powerP1NumericUpDown.Text = powerP2NumericUpDown.Text;
            defP2ComboBox.Text = text1;
            colorP2ComboBox.Text = text2;
            aiP2CheckBox.Checked = flag1;
            lifeP2CheckBox.Checked = flag2;
            lifeP2NumericUpDown.Text = text3;
            powerP2CheckBox.Checked = flag3;
            powerP2NumericUpDown.Text = text4;
            string text5 = defP3ComboBox.Text;
            string text6 = colorP3ComboBox.Text;
            bool flag4 = aiP3CheckBox.Checked;
            bool flag5 = lifeP3CheckBox.Checked;
            string text7 = lifeP3NumericUpDown.Text;
            bool flag6 = powerP3CheckBox.Checked;
            string text8 = powerP3NumericUpDown.Text;
            defP3ComboBox.Text = defP4ComboBox.Text;
            colorP3ComboBox.Text = colorP4ComboBox.Text;
            aiP3CheckBox.Checked = aiP4CheckBox.Checked;
            lifeP3CheckBox.Checked = lifeP4CheckBox.Checked;
            lifeP3NumericUpDown.Text = lifeP4NumericUpDown.Text;
            powerP3CheckBox.Checked = powerP4CheckBox.Checked;
            powerP3NumericUpDown.Text = powerP4NumericUpDown.Text;
            defP4ComboBox.Text = text5;
            colorP4ComboBox.Text = text6;
            aiP4CheckBox.Checked = flag4;
            lifeP4CheckBox.Checked = flag5;
            lifeP4NumericUpDown.Text = text7;
            powerP4CheckBox.Checked = flag6;
            powerP4NumericUpDown.Text = text8;
        }

        private void defP3ComboBox_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = !e.Data.GetDataPresent(DataFormats.FileDrop) ? DragDropEffects.None : DragDropEffects.Link;
            Activate();
        }

        private void defP3ComboBox_DragDrop(object sender, DragEventArgs e)
        {
            string[] data = (string[])e.Data.GetData(DataFormats.FileDrop, false);
            if (data == null || data.Length < 1)
                return;
            MugenProfile currentProfile = ProfileManager.MainObj().GetCurrentProfile();
            if (currentProfile == null)
            {
                int num1 = (int)MessageBox.Show("No profile has been selected.", "Swiss Army Knife");
            }
            else
            {
                string mugenPath = currentProfile.GetMugenPath();
                if (mugenPath == null)
                {
                    int num2 = (int)MessageBox.Show("The mugen executable has not been specified.", "Swiss Army Knife");
                }
                else
                {
                    string str1 = null;
                    string str2 = null;
                    if (data.Length == 1)
                        str1 = data[0];
                    else if (data.Length > 1)
                    {
                        str1 = data[data.Length - 1];
                        str2 = data[data.Length - 2];
                    }
                    string str3 = Path.Combine(mugenPath, "chars");
                    if (str1 != null)
                    {
                        if (str1.IndexOf(str3) < 0)
                        {
                            int num3 = (int)MessageBox.Show("The path of the def file and the path of the chars folder do not belong to the same root. This character possibly belongs to a different mugen installation.", "Swiss Army Knife");
                            return;
                        }
                        defP3ComboBox.Text = str1.Substring(str3.Length + 1);
                    }
                    if (str2 == null)
                        return;
                    if (str2.IndexOf(str3) < 0)
                    {
                        int num4 = (int)MessageBox.Show("The path of the def file and the path of the chars folder do not belong to the same root. This character possibly belongs to a different mugen installation.", "Swiss Army Knife");
                    }
                    else
                        defP4ComboBox.Text = str2.Substring(str3.Length + 1);
                }
            }
        }

        private void p3Well_DragEnter(object sender, DragEventArgs e) => defP3ComboBox_DragEnter(sender, e);

        private void p3Well_DragDrop(object sender, DragEventArgs e) => defP3ComboBox_DragDrop(sender, e);

        private void defP4ComboBox_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = !e.Data.GetDataPresent(DataFormats.FileDrop) ? DragDropEffects.None : DragDropEffects.Link;
            Activate();
        }

        private void defP4ComboBox_DragDrop(object sender, DragEventArgs e)
        {
            string[] data = (string[])e.Data.GetData(DataFormats.FileDrop, false);
            if (data == null || data.Length < 1)
                return;
            MugenProfile currentProfile = ProfileManager.MainObj().GetCurrentProfile();
            if (currentProfile == null)
            {
                int num1 = (int)MessageBox.Show("No profile has been selected.", "Swiss Army Knife");
            }
            else
            {
                string mugenPath = currentProfile.GetMugenPath();
                if (mugenPath == null)
                {
                    int num2 = (int)MessageBox.Show("The mugen executable has not been specified.", "Swiss Army Knife");
                }
                else
                {
                    string str1 = null;
                    string str2 = null;
                    if (data.Length == 1)
                        str2 = data[0];
                    else if (data.Length > 1)
                    {
                        str1 = data[data.Length - 1];
                        str2 = data[data.Length - 2];
                    }
                    string str3 = Path.Combine(mugenPath, "chars");
                    if (str1 != null)
                    {
                        if (str1.IndexOf(str3) < 0)
                        {
                            int num3 = (int)MessageBox.Show("The path of the def file and the path of the chars folder do not belong to the same root. This character possibly belongs to a different mugen installation.", "Swiss Army Knife");
                            return;
                        }
                        defP3ComboBox.Text = str1.Substring(str3.Length + 1);
                    }
                    if (str2 == null)
                        return;
                    if (str2.IndexOf(str3) < 0)
                    {
                        int num4 = (int)MessageBox.Show("The path of the def file and the path of the chars folder do not belong to the same root. This character possibly belongs to a different mugen installation.", "Swiss Army Knife");
                    }
                    else
                        defP4ComboBox.Text = str2.Substring(str3.Length + 1);
                }
            }
        }

        private void p4Well_DragEnter(object sender, DragEventArgs e) => defP4ComboBox_DragEnter(sender, e);

        private void p4Well_DragDrop(object sender, DragEventArgs e) => defP4ComboBox_DragDrop(sender, e);

        private void lifeP1CheckBox_CheckedChanged(object sender, EventArgs e) => lifeP1NumericUpDown.Enabled = lifeP1CheckBox.Checked;

        private void powerP1CheckBox_CheckedChanged(object sender, EventArgs e) => powerP1NumericUpDown.Enabled = powerP1CheckBox.Checked;

        private void lifeP2CheckBox_CheckedChanged(object sender, EventArgs e) => lifeP2NumericUpDown.Enabled = lifeP2CheckBox.Checked;

        private void powerP2CheckBox_CheckedChanged(object sender, EventArgs e) => powerP2NumericUpDown.Enabled = powerP2CheckBox.Checked;

        private void lifeP3CheckBox_CheckedChanged(object sender, EventArgs e) => lifeP3NumericUpDown.Enabled = lifeP3CheckBox.Checked;

        private void powerP3CheckBox_CheckedChanged(object sender, EventArgs e) => powerP3NumericUpDown.Enabled = powerP3CheckBox.Checked;

        private void lifeP4CheckBox_CheckedChanged(object sender, EventArgs e) => lifeP4NumericUpDown.Enabled = lifeP4CheckBox.Checked;

        private void powerP4CheckBox_CheckedChanged(object sender, EventArgs e) => powerP4NumericUpDown.Enabled = powerP4CheckBox.Checked;

        private void p1tabControl_SelectedIndexChanged(object sender, EventArgs e) => p2tabControl.SelectedIndex = p1tabControl.SelectedIndex;

        private void p2tabControl_SelectedIndexChanged(object sender, EventArgs e) => p1tabControl.SelectedIndex = p2tabControl.SelectedIndex;

        private void defP1ComboBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Return)
                return;
            defP2ComboBox.Select();
        }

        private void defP2ComboBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Return || !CheckValues())
                return;
            _result = DialogResult.OK;
            SaveProfile();
            MainForm.MainObj().ActivateAll(true);
            Close();
        }

        private void defP3ComboBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Return)
                return;
            defP4ComboBox.Select();
        }

        private void defP4ComboBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Return || !CheckValues())
                return;
            _result = DialogResult.OK;
            SaveProfile();
            MainForm.MainObj().ActivateAll(true);
            Close();
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            cancelButton = new Button();
            okButton = new Button();
            groupBox1 = new GroupBox();
            p1tabControl = new TabControl();
            p1tabPage = new TabPage();
            powerP1NumericUpDown = new NumericUpDown();
            lifeP1NumericUpDown = new NumericUpDown();
            powerP1CheckBox = new CheckBox();
            lifeP1CheckBox = new CheckBox();
            aiP1CheckBox = new CheckBox();
            colorP1ComboBox = new ComboBox();
            label2 = new Label();
            defP1ComboBox = new ComboBox();
            label1 = new Label();
            p3tabPage = new TabPage();
            powerP3NumericUpDown = new NumericUpDown();
            lifeP3NumericUpDown = new NumericUpDown();
            powerP3CheckBox = new CheckBox();
            lifeP3CheckBox = new CheckBox();
            aiP3CheckBox = new CheckBox();
            colorP3ComboBox = new ComboBox();
            label7 = new Label();
            defP3ComboBox = new ComboBox();
            label8 = new Label();
            textBox3 = new TextBox();
            folderButton = new Button();
            groupBox2 = new GroupBox();
            p2tabControl = new TabControl();
            p2tabPage = new TabPage();
            powerP2NumericUpDown = new NumericUpDown();
            lifeP2NumericUpDown = new NumericUpDown();
            powerP2CheckBox = new CheckBox();
            lifeP2CheckBox = new CheckBox();
            aiP2CheckBox = new CheckBox();
            colorP2ComboBox = new ComboBox();
            label3 = new Label();
            defP2ComboBox = new ComboBox();
            label4 = new Label();
            p4tabPage = new TabPage();
            powerP4NumericUpDown = new NumericUpDown();
            lifeP4NumericUpDown = new NumericUpDown();
            powerP4CheckBox = new CheckBox();
            lifeP4CheckBox = new CheckBox();
            aiP4CheckBox = new CheckBox();
            colorP4ComboBox = new ComboBox();
            label9 = new Label();
            defP4ComboBox = new ComboBox();
            label10 = new Label();
            label5 = new Label();
            roundComboBox = new ComboBox();
            swapButton = new Button();
            noMusicCheckBox = new CheckBox();
            noSoundCheckBox = new CheckBox();
            label6 = new Label();
            stageComboBox = new ComboBox();
            groupBox1.SuspendLayout();
            p1tabControl.SuspendLayout();
            p1tabPage.SuspendLayout();
            powerP1NumericUpDown.BeginInit();
            lifeP1NumericUpDown.BeginInit();
            p3tabPage.SuspendLayout();
            powerP3NumericUpDown.BeginInit();
            lifeP3NumericUpDown.BeginInit();
            groupBox2.SuspendLayout();
            p2tabControl.SuspendLayout();
            p2tabPage.SuspendLayout();
            powerP2NumericUpDown.BeginInit();
            lifeP2NumericUpDown.BeginInit();
            p4tabPage.SuspendLayout();
            powerP4NumericUpDown.BeginInit();
            lifeP4NumericUpDown.BeginInit();
            SuspendLayout();
            cancelButton.Location = new Point(361, 442);
            cancelButton.Name = "cancelButton";
            cancelButton.Size = new Size(90, 28);
            cancelButton.TabIndex = 3;
            cancelButton.Text = "Cancel";
            cancelButton.UseVisualStyleBackColor = true;
            cancelButton.Click += new EventHandler(cancelButton_Click);
            okButton.Location = new Point(265, 442);
            okButton.Name = "okButton";
            okButton.Size = new Size(90, 28);
            okButton.TabIndex = 2;
            okButton.Text = "Start";
            okButton.UseVisualStyleBackColor = true;
            okButton.Click += new EventHandler(okButton_Click);
            groupBox1.Controls.Add(p1tabControl);
            groupBox1.Location = new Point(12, 78);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(440, 139);
            groupBox1.TabIndex = 10;
            groupBox1.TabStop = false;
            groupBox1.Text = "Player 1 side";
            p1tabControl.AllowDrop = true;
            p1tabControl.Controls.Add(p1tabPage);
            p1tabControl.Controls.Add(p3tabPage);
            p1tabControl.Location = new Point(6, 18);
            p1tabControl.Name = "p1tabControl";
            p1tabControl.SelectedIndex = 0;
            p1tabControl.Size = new Size(433, 115);
            p1tabControl.TabIndex = 102;
            p1tabControl.SelectedIndexChanged += new EventHandler(p1tabControl_SelectedIndexChanged);
            p1tabPage.AllowDrop = true;
            p1tabPage.Controls.Add(powerP1NumericUpDown);
            p1tabPage.Controls.Add(lifeP1NumericUpDown);
            p1tabPage.Controls.Add(powerP1CheckBox);
            p1tabPage.Controls.Add(lifeP1CheckBox);
            p1tabPage.Controls.Add(aiP1CheckBox);
            p1tabPage.Controls.Add(colorP1ComboBox);
            p1tabPage.Controls.Add(label2);
            p1tabPage.Controls.Add(defP1ComboBox);
            p1tabPage.Controls.Add(label1);
            p1tabPage.Location = new Point(4, 22);
            p1tabPage.Name = "p1tabPage";
            p1tabPage.Padding = new Padding(3);
            p1tabPage.Size = new Size(425, 89);
            p1tabPage.TabIndex = 0;
            p1tabPage.Text = "player1";
            p1tabPage.UseVisualStyleBackColor = true;
            p1tabPage.DragDrop += new DragEventHandler(p1Well_DragDrop);
            p1tabPage.DragEnter += new DragEventHandler(p1Well_DragEnter);
            powerP1NumericUpDown.Enabled = false;
            powerP1NumericUpDown.Location = new Point(350, 62);
            powerP1NumericUpDown.Margin = new Padding(3, 3, 3, 0);
            powerP1NumericUpDown.Maximum = new decimal(new int[4]
            {
        int.MaxValue,
        0,
        0,
        0
            });
            powerP1NumericUpDown.Name = "powerP1NumericUpDown";
            powerP1NumericUpDown.Size = new Size(68, 19);
            powerP1NumericUpDown.TabIndex = 107;
            powerP1NumericUpDown.TextAlign = HorizontalAlignment.Right;
            lifeP1NumericUpDown.Enabled = false;
            lifeP1NumericUpDown.Location = new Point(154, 62);
            lifeP1NumericUpDown.Margin = new Padding(3, 3, 3, 0);
            lifeP1NumericUpDown.Maximum = new decimal(new int[4]
            {
        int.MaxValue,
        0,
        0,
        0
            });
            lifeP1NumericUpDown.Name = "lifeP1NumericUpDown";
            lifeP1NumericUpDown.Size = new Size(68, 19);
            lifeP1NumericUpDown.TabIndex = 105;
            lifeP1NumericUpDown.TextAlign = HorizontalAlignment.Right;
            lifeP1NumericUpDown.Value = new decimal(new int[4]
            {
        1000,
        0,
        0,
        0
            });
            powerP1CheckBox.AutoSize = true;
            powerP1CheckBox.Location = new Point(258, 63);
            powerP1CheckBox.Name = "powerP1CheckBox";
            powerP1CheckBox.Size = new Size(91, 16);
            powerP1CheckBox.TabIndex = 106;
            powerP1CheckBox.Text = "Initial power:";
            powerP1CheckBox.UseVisualStyleBackColor = true;
            powerP1CheckBox.CheckedChanged += new EventHandler(powerP1CheckBox_CheckedChanged);
            lifeP1CheckBox.AutoSize = true;
            lifeP1CheckBox.Location = new Point(66, 63);
            lifeP1CheckBox.Name = "lifeP1CheckBox";
            lifeP1CheckBox.Size = new Size(87, 16);
            lifeP1CheckBox.TabIndex = 104;
            lifeP1CheckBox.Text = "Maximum life:";
            lifeP1CheckBox.UseVisualStyleBackColor = true;
            lifeP1CheckBox.CheckedChanged += new EventHandler(lifeP1CheckBox_CheckedChanged);
            aiP1CheckBox.AutoSize = true;
            aiP1CheckBox.Checked = true;
            aiP1CheckBox.CheckState = CheckState.Checked;
            aiP1CheckBox.Location = new Point(152, 36);
            aiP1CheckBox.Name = "aiP1CheckBox";
            aiP1CheckBox.Size = new Size(90, 16);
            aiP1CheckBox.TabIndex = 103;
            aiP1CheckBox.Text = "Enable AI";
            aiP1CheckBox.UseVisualStyleBackColor = true;
            colorP1ComboBox.AutoCompleteMode = AutoCompleteMode.Append;
            colorP1ComboBox.AutoCompleteSource = AutoCompleteSource.ListItems;
            colorP1ComboBox.FormattingEnabled = true;
            colorP1ComboBox.Location = new Point(65, 34);
            colorP1ComboBox.MaxDropDownItems = 12;
            colorP1ComboBox.MaxLength = 2;
            colorP1ComboBox.Name = "colorP1ComboBox";
            colorP1ComboBox.Size = new Size(45, 20);
            colorP1ComboBox.TabIndex = 102;
            colorP1ComboBox.Text = "1";
            label2.AutoSize = true;
            label2.Location = new Point(5, 37);
            label2.Name = "label2";
            label2.Size = new Size(34, 12);
            label2.TabIndex = 108;
            label2.Text = "Color:";
            defP1ComboBox.AllowDrop = true;
            defP1ComboBox.AutoCompleteMode = AutoCompleteMode.Suggest;
            defP1ComboBox.AutoCompleteSource = AutoCompleteSource.ListItems;
            defP1ComboBox.FormattingEnabled = true;
            defP1ComboBox.Location = new Point(65, 8);
            defP1ComboBox.MaxDropDownItems = 20;
            defP1ComboBox.MaxLength = 64;
            defP1ComboBox.Name = "defP1ComboBox";
            defP1ComboBox.Size = new Size(353, 20);
            defP1ComboBox.TabIndex = 101;
            defP1ComboBox.Text = "(No setting)";
            defP1ComboBox.DragDrop += new DragEventHandler(defP1ComboBox_DragDrop);
            defP1ComboBox.DragEnter += new DragEventHandler(defP1ComboBox_DragEnter);
            defP1ComboBox.KeyDown += new KeyEventHandler(defP1ComboBox_KeyDown);
            label1.AutoSize = true;
            label1.Location = new Point(5, 11);
            label1.Name = "label1";
            label1.Size = new Size(57, 12);
            label1.TabIndex = 109;
            label1.Text = "def file:";
            p3tabPage.Controls.Add(powerP3NumericUpDown);
            p3tabPage.Controls.Add(lifeP3NumericUpDown);
            p3tabPage.Controls.Add(powerP3CheckBox);
            p3tabPage.Controls.Add(lifeP3CheckBox);
            p3tabPage.Controls.Add(aiP3CheckBox);
            p3tabPage.Controls.Add(colorP3ComboBox);
            p3tabPage.Controls.Add(label7);
            p3tabPage.Controls.Add(defP3ComboBox);
            p3tabPage.Controls.Add(label8);
            p3tabPage.Location = new Point(4, 22);
            p3tabPage.Name = "p3tabPage";
            p3tabPage.Padding = new Padding(3);
            p3tabPage.Size = new Size(425, 89);
            p3tabPage.TabIndex = 1;
            p3tabPage.Text = "player3";
            p3tabPage.UseVisualStyleBackColor = true;
            p3tabPage.DragDrop += new DragEventHandler(p3Well_DragDrop);
            p3tabPage.DragEnter += new DragEventHandler(p3Well_DragEnter);
            powerP3NumericUpDown.Enabled = false;
            powerP3NumericUpDown.Location = new Point(350, 62);
            powerP3NumericUpDown.Margin = new Padding(3, 3, 3, 0);
            powerP3NumericUpDown.Maximum = new decimal(new int[4]
            {
        int.MaxValue,
        0,
        0,
        0
            });
            powerP3NumericUpDown.Name = "powerP3NumericUpDown";
            powerP3NumericUpDown.Size = new Size(68, 19);
            powerP3NumericUpDown.TabIndex = 116;
            powerP3NumericUpDown.TextAlign = HorizontalAlignment.Right;
            lifeP3NumericUpDown.Enabled = false;
            lifeP3NumericUpDown.Location = new Point(154, 62);
            lifeP3NumericUpDown.Margin = new Padding(3, 3, 3, 0);
            lifeP3NumericUpDown.Maximum = new decimal(new int[4]
            {
        int.MaxValue,
        0,
        0,
        0
            });
            lifeP3NumericUpDown.Name = "lifeP3NumericUpDown";
            lifeP3NumericUpDown.Size = new Size(68, 19);
            lifeP3NumericUpDown.TabIndex = 114;
            lifeP3NumericUpDown.TextAlign = HorizontalAlignment.Right;
            lifeP3NumericUpDown.Value = new decimal(new int[4]
            {
        1000,
        0,
        0,
        0
            });
            powerP3CheckBox.AutoSize = true;
            powerP3CheckBox.Location = new Point(258, 63);
            powerP3CheckBox.Name = "powerP3CheckBox";
            powerP3CheckBox.Size = new Size(91, 16);
            powerP3CheckBox.TabIndex = 115;
            powerP3CheckBox.Text = "Initial power:";
            powerP3CheckBox.UseVisualStyleBackColor = true;
            powerP3CheckBox.CheckedChanged += new EventHandler(powerP3CheckBox_CheckedChanged);
            lifeP3CheckBox.AutoSize = true;
            lifeP3CheckBox.Location = new Point(66, 63);
            lifeP3CheckBox.Name = "lifeP3CheckBox";
            lifeP3CheckBox.Size = new Size(87, 16);
            lifeP3CheckBox.TabIndex = 113;
            lifeP3CheckBox.Text = "Maximum life:";
            lifeP3CheckBox.UseVisualStyleBackColor = true;
            lifeP3CheckBox.CheckedChanged += new EventHandler(lifeP3CheckBox_CheckedChanged);
            aiP3CheckBox.AutoSize = true;
            aiP3CheckBox.Checked = true;
            aiP3CheckBox.CheckState = CheckState.Checked;
            aiP3CheckBox.Location = new Point(152, 36);
            aiP3CheckBox.Name = "aiP3CheckBox";
            aiP3CheckBox.Size = new Size(90, 16);
            aiP3CheckBox.TabIndex = 112;
            aiP3CheckBox.Text = "Enable AI";
            aiP3CheckBox.UseVisualStyleBackColor = true;
            colorP3ComboBox.AutoCompleteMode = AutoCompleteMode.Append;
            colorP3ComboBox.AutoCompleteSource = AutoCompleteSource.ListItems;
            colorP3ComboBox.FormattingEnabled = true;
            colorP3ComboBox.Location = new Point(65, 34);
            colorP3ComboBox.MaxDropDownItems = 12;
            colorP3ComboBox.MaxLength = 2;
            colorP3ComboBox.Name = "colorP3ComboBox";
            colorP3ComboBox.Size = new Size(45, 20);
            colorP3ComboBox.TabIndex = 111;
            colorP3ComboBox.Text = "1";
            label7.AutoSize = true;
            label7.Location = new Point(5, 37);
            label7.Name = "label7";
            label7.Size = new Size(34, 12);
            label7.TabIndex = 117;
            label7.Text = "Color:";
            defP3ComboBox.AllowDrop = true;
            defP3ComboBox.AutoCompleteMode = AutoCompleteMode.Suggest;
            defP3ComboBox.AutoCompleteSource = AutoCompleteSource.ListItems;
            defP3ComboBox.FormattingEnabled = true;
            defP3ComboBox.Location = new Point(65, 8);
            defP3ComboBox.MaxDropDownItems = 20;
            defP3ComboBox.MaxLength = 64;
            defP3ComboBox.Name = "defP3ComboBox";
            defP3ComboBox.Size = new Size(353, 20);
            defP3ComboBox.TabIndex = 110;
            defP3ComboBox.Text = "(No setting)";
            defP3ComboBox.DragDrop += new DragEventHandler(defP3ComboBox_DragDrop);
            defP3ComboBox.DragEnter += new DragEventHandler(defP3ComboBox_DragEnter);
            defP3ComboBox.KeyDown += new KeyEventHandler(defP3ComboBox_KeyDown);
            label8.AutoSize = true;
            label8.Location = new Point(5, 11);
            label8.Name = "label8";
            label8.Size = new Size(57, 12);
            label8.TabIndex = 118;
            label8.Text = "def file:";
            textBox3.BorderStyle = BorderStyle.None;
            textBox3.Cursor = Cursors.Default;
            textBox3.Location = new Point(35, 9);
            textBox3.Multiline = true;
            textBox3.Name = "textBox3";
            textBox3.ReadOnly = true;
            textBox3.Size = new Size(310, 60);
            textBox3.TabIndex = 100;
            textBox3.TabStop = false;
            textBox3.Text = "Select characters by specifying the name of their def files. \r\n\r\nYou can add characters by typing their names or by dragging and dropping the def files in this window.";
            folderButton.Location = new Point(362, 13);
            folderButton.Name = "folderButton";
            folderButton.Size = new Size(90, 49);
            folderButton.TabIndex = 9;
            folderButton.Text = "Open chars folder in explorer";
            folderButton.UseVisualStyleBackColor = true;
            folderButton.Click += new EventHandler(folderButton_Click);
            groupBox2.Controls.Add(p2tabControl);
            groupBox2.Location = new Point(12, 227);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(440, 139);
            groupBox2.TabIndex = 20;
            groupBox2.TabStop = false;
            groupBox2.Text = "Player 2 side";
            p2tabControl.AllowDrop = true;
            p2tabControl.Controls.Add(p2tabPage);
            p2tabControl.Controls.Add(p4tabPage);
            p2tabControl.Location = new Point(6, 18);
            p2tabControl.Name = "p2tabControl";
            p2tabControl.SelectedIndex = 0;
            p2tabControl.Size = new Size(433, 115);
            p2tabControl.TabIndex = 0;
            p2tabControl.SelectedIndexChanged += new EventHandler(p2tabControl_SelectedIndexChanged);
            p2tabPage.AllowDrop = true;
            p2tabPage.Controls.Add(powerP2NumericUpDown);
            p2tabPage.Controls.Add(lifeP2NumericUpDown);
            p2tabPage.Controls.Add(powerP2CheckBox);
            p2tabPage.Controls.Add(lifeP2CheckBox);
            p2tabPage.Controls.Add(aiP2CheckBox);
            p2tabPage.Controls.Add(colorP2ComboBox);
            p2tabPage.Controls.Add(label3);
            p2tabPage.Controls.Add(defP2ComboBox);
            p2tabPage.Controls.Add(label4);
            p2tabPage.Location = new Point(4, 22);
            p2tabPage.Name = "p2tabPage";
            p2tabPage.Padding = new Padding(3);
            p2tabPage.Size = new Size(425, 89);
            p2tabPage.TabIndex = 0;
            p2tabPage.Text = "player2";
            p2tabPage.UseVisualStyleBackColor = true;
            p2tabPage.DragDrop += new DragEventHandler(p2Well_DragDrop);
            p2tabPage.DragEnter += new DragEventHandler(p2Well_DragEnter);
            powerP2NumericUpDown.Enabled = false;
            powerP2NumericUpDown.Location = new Point(350, 62);
            powerP2NumericUpDown.Margin = new Padding(3, 3, 3, 0);
            powerP2NumericUpDown.Maximum = new decimal(new int[4]
            {
        int.MaxValue,
        0,
        0,
        0
            });
            powerP2NumericUpDown.Name = "powerP2NumericUpDown";
            powerP2NumericUpDown.Size = new Size(68, 19);
            powerP2NumericUpDown.TabIndex = 109;
            powerP2NumericUpDown.TextAlign = HorizontalAlignment.Right;
            lifeP2NumericUpDown.Enabled = false;
            lifeP2NumericUpDown.Location = new Point(154, 62);
            lifeP2NumericUpDown.Margin = new Padding(3, 3, 3, 0);
            lifeP2NumericUpDown.Maximum = new decimal(new int[4]
            {
        int.MaxValue,
        0,
        0,
        0
            });
            lifeP2NumericUpDown.Name = "lifeP2NumericUpDown";
            lifeP2NumericUpDown.Size = new Size(68, 19);
            lifeP2NumericUpDown.TabIndex = 107;
            lifeP2NumericUpDown.TextAlign = HorizontalAlignment.Right;
            lifeP2NumericUpDown.Value = new decimal(new int[4]
            {
        1000,
        0,
        0,
        0
            });
            powerP2CheckBox.AutoSize = true;
            powerP2CheckBox.Location = new Point(258, 63);
            powerP2CheckBox.Name = "powerP2CheckBox";
            powerP2CheckBox.Size = new Size(91, 16);
            powerP2CheckBox.TabIndex = 108;
            powerP2CheckBox.Text = "Initial power:";
            powerP2CheckBox.UseVisualStyleBackColor = true;
            powerP2CheckBox.CheckedChanged += new EventHandler(powerP2CheckBox_CheckedChanged);
            lifeP2CheckBox.AutoSize = true;
            lifeP2CheckBox.Location = new Point(66, 63);
            lifeP2CheckBox.Name = "lifeP2CheckBox";
            lifeP2CheckBox.Size = new Size(87, 16);
            lifeP2CheckBox.TabIndex = 106;
            lifeP2CheckBox.Text = "Maximum life:";
            lifeP2CheckBox.UseVisualStyleBackColor = true;
            lifeP2CheckBox.CheckedChanged += new EventHandler(lifeP2CheckBox_CheckedChanged);
            aiP2CheckBox.AutoSize = true;
            aiP2CheckBox.Checked = true;
            aiP2CheckBox.CheckState = CheckState.Checked;
            aiP2CheckBox.Location = new Point(152, 36);
            aiP2CheckBox.Name = "aiP2CheckBox";
            aiP2CheckBox.Size = new Size(90, 16);
            aiP2CheckBox.TabIndex = 105;
            aiP2CheckBox.Text = "Enable AI";
            aiP2CheckBox.UseVisualStyleBackColor = true;
            colorP2ComboBox.AutoCompleteMode = AutoCompleteMode.Append;
            colorP2ComboBox.AutoCompleteSource = AutoCompleteSource.ListItems;
            colorP2ComboBox.FormattingEnabled = true;
            colorP2ComboBox.Location = new Point(65, 34);
            colorP2ComboBox.MaxDropDownItems = 12;
            colorP2ComboBox.MaxLength = 2;
            colorP2ComboBox.Name = "colorP2ComboBox";
            colorP2ComboBox.Size = new Size(45, 20);
            colorP2ComboBox.TabIndex = 104;
            colorP2ComboBox.Text = "1";
            label3.AutoSize = true;
            label3.Location = new Point(5, 37);
            label3.Name = "label3";
            label3.Size = new Size(34, 12);
            label3.TabIndex = 110;
            label3.Text = "Color:";
            defP2ComboBox.AllowDrop = true;
            defP2ComboBox.AutoCompleteMode = AutoCompleteMode.Suggest;
            defP2ComboBox.AutoCompleteSource = AutoCompleteSource.ListItems;
            defP2ComboBox.FormattingEnabled = true;
            defP2ComboBox.Location = new Point(65, 8);
            defP2ComboBox.MaxDropDownItems = 20;
            defP2ComboBox.MaxLength = 64;
            defP2ComboBox.Name = "defP2ComboBox";
            defP2ComboBox.Size = new Size(353, 20);
            defP2ComboBox.TabIndex = 103;
            defP2ComboBox.Text = "(No setting)";
            defP2ComboBox.DragDrop += new DragEventHandler(defP2ComboBox_DragDrop);
            defP2ComboBox.DragEnter += new DragEventHandler(defP2ComboBox_DragEnter);
            defP2ComboBox.KeyDown += new KeyEventHandler(defP2ComboBox_KeyDown);
            label4.AutoSize = true;
            label4.Location = new Point(5, 11);
            label4.Name = "label4";
            label4.Size = new Size(57, 12);
            label4.TabIndex = 111;
            label4.Text = "def file:";
            p4tabPage.Controls.Add(powerP4NumericUpDown);
            p4tabPage.Controls.Add(lifeP4NumericUpDown);
            p4tabPage.Controls.Add(powerP4CheckBox);
            p4tabPage.Controls.Add(lifeP4CheckBox);
            p4tabPage.Controls.Add(aiP4CheckBox);
            p4tabPage.Controls.Add(colorP4ComboBox);
            p4tabPage.Controls.Add(label9);
            p4tabPage.Controls.Add(defP4ComboBox);
            p4tabPage.Controls.Add(label10);
            p4tabPage.Location = new Point(4, 22);
            p4tabPage.Name = "p4tabPage";
            p4tabPage.Padding = new Padding(3);
            p4tabPage.Size = new Size(425, 89);
            p4tabPage.TabIndex = 1;
            p4tabPage.Text = "player4";
            p4tabPage.UseVisualStyleBackColor = true;
            p4tabPage.DragDrop += new DragEventHandler(p4Well_DragDrop);
            p4tabPage.DragEnter += new DragEventHandler(p4Well_DragEnter);
            powerP4NumericUpDown.Enabled = false;
            powerP4NumericUpDown.Location = new Point(350, 62);
            powerP4NumericUpDown.Margin = new Padding(3, 3, 3, 0);
            powerP4NumericUpDown.Maximum = new decimal(new int[4]
            {
        int.MaxValue,
        0,
        0,
        0
            });
            powerP4NumericUpDown.Name = "powerP4NumericUpDown";
            powerP4NumericUpDown.Size = new Size(68, 19);
            powerP4NumericUpDown.TabIndex = 118;
            powerP4NumericUpDown.TextAlign = HorizontalAlignment.Right;
            lifeP4NumericUpDown.Enabled = false;
            lifeP4NumericUpDown.Location = new Point(154, 62);
            lifeP4NumericUpDown.Margin = new Padding(3, 3, 3, 0);
            lifeP4NumericUpDown.Maximum = new decimal(new int[4]
            {
        int.MaxValue,
        0,
        0,
        0
            });
            lifeP4NumericUpDown.Name = "lifeP4NumericUpDown";
            lifeP4NumericUpDown.Size = new Size(68, 19);
            lifeP4NumericUpDown.TabIndex = 116;
            lifeP4NumericUpDown.TextAlign = HorizontalAlignment.Right;
            lifeP4NumericUpDown.Value = new decimal(new int[4]
            {
        1000,
        0,
        0,
        0
            });
            powerP4CheckBox.AutoSize = true;
            powerP4CheckBox.Location = new Point(258, 63);
            powerP4CheckBox.Name = "powerP4CheckBox";
            powerP4CheckBox.Size = new Size(91, 16);
            powerP4CheckBox.TabIndex = 117;
            powerP4CheckBox.Text = "Initial power:";
            powerP4CheckBox.UseVisualStyleBackColor = true;
            powerP4CheckBox.CheckedChanged += new EventHandler(powerP4CheckBox_CheckedChanged);
            lifeP4CheckBox.AutoSize = true;
            lifeP4CheckBox.Location = new Point(66, 63);
            lifeP4CheckBox.Name = "lifeP4CheckBox";
            lifeP4CheckBox.Size = new Size(87, 16);
            lifeP4CheckBox.TabIndex = 115;
            lifeP4CheckBox.Text = "Maximum life:";
            lifeP4CheckBox.UseVisualStyleBackColor = true;
            lifeP4CheckBox.CheckedChanged += new EventHandler(lifeP4CheckBox_CheckedChanged);
            aiP4CheckBox.AutoSize = true;
            aiP4CheckBox.Checked = true;
            aiP4CheckBox.CheckState = CheckState.Checked;
            aiP4CheckBox.Location = new Point(152, 36);
            aiP4CheckBox.Name = "aiP4CheckBox";
            aiP4CheckBox.Size = new Size(90, 16);
            aiP4CheckBox.TabIndex = 114;
            aiP4CheckBox.Text = "Enable AI";
            aiP4CheckBox.UseVisualStyleBackColor = true;
            colorP4ComboBox.AutoCompleteMode = AutoCompleteMode.Append;
            colorP4ComboBox.AutoCompleteSource = AutoCompleteSource.ListItems;
            colorP4ComboBox.FormattingEnabled = true;
            colorP4ComboBox.Location = new Point(65, 34);
            colorP4ComboBox.MaxDropDownItems = 12;
            colorP4ComboBox.MaxLength = 2;
            colorP4ComboBox.Name = "colorP4ComboBox";
            colorP4ComboBox.Size = new Size(45, 20);
            colorP4ComboBox.TabIndex = 113;
            colorP4ComboBox.Text = "1";
            label9.AutoSize = true;
            label9.Location = new Point(5, 37);
            label9.Name = "label9";
            label9.Size = new Size(34, 12);
            label9.TabIndex = 119;
            label9.Text = "Color:";
            defP4ComboBox.AllowDrop = true;
            defP4ComboBox.AutoCompleteMode = AutoCompleteMode.Suggest;
            defP4ComboBox.AutoCompleteSource = AutoCompleteSource.ListItems;
            defP4ComboBox.FormattingEnabled = true;
            defP4ComboBox.Location = new Point(65, 8);
            defP4ComboBox.MaxDropDownItems = 20;
            defP4ComboBox.MaxLength = 64;
            defP4ComboBox.Name = "defP4ComboBox";
            defP4ComboBox.Size = new Size(353, 20);
            defP4ComboBox.TabIndex = 112;
            defP4ComboBox.Text = "(No setting)";
            defP4ComboBox.DragDrop += new DragEventHandler(defP4ComboBox_DragDrop);
            defP4ComboBox.DragEnter += new DragEventHandler(defP4ComboBox_DragEnter);
            defP4ComboBox.KeyDown += new KeyEventHandler(defP4ComboBox_KeyDown);
            label10.AutoSize = true;
            label10.Location = new Point(5, 11);
            label10.Name = "label10";
            label10.Size = new Size(57, 12);
            label10.TabIndex = 120;
            label10.Text = "def file:";
            label5.AutoSize = true;
            label5.Location = new Point(18, 445);
            label5.Name = "label5";
            label5.Size = new Size(54, 12);
            label5.TabIndex = 100;
            label5.Text = "Rounds:";
            roundComboBox.AutoCompleteMode = AutoCompleteMode.Append;
            roundComboBox.AutoCompleteSource = AutoCompleteSource.ListItems;
            roundComboBox.FormattingEnabled = true;
            roundComboBox.Location = new Point(78, 442);
            roundComboBox.MaxDropDownItems = 5;
            roundComboBox.MaxLength = 2;
            roundComboBox.Name = "roundComboBox";
            roundComboBox.Size = new Size(45, 20);
            roundComboBox.TabIndex = 50;
            roundComboBox.Text = "2";
            roundComboBox.SelectedIndexChanged += new EventHandler(roundComboBox_SelectedIndexChanged);
            swapButton.Location = new Point(138, 442);
            swapButton.Name = "swapButton";
            swapButton.Size = new Size(90, 28);
            swapButton.TabIndex = 51;
            swapButton.Text = "Swap players";
            swapButton.UseVisualStyleBackColor = true;
            swapButton.Click += new EventHandler(swapButton_Click);
            noMusicCheckBox.AutoSize = true;
            noMusicCheckBox.Location = new Point(32, 407);
            noMusicCheckBox.Name = "noMusicCheckBox";
            noMusicCheckBox.Size = new Size(73, 16);
            noMusicCheckBox.TabIndex = 41;
            noMusicCheckBox.Text = "No BGM";
            noMusicCheckBox.UseVisualStyleBackColor = true;
            noSoundCheckBox.AutoSize = true;
            noSoundCheckBox.Location = new Point(111, 407);
            noSoundCheckBox.Name = "noSoundCheckBox";
            noSoundCheckBox.Size = new Size(85, 16);
            noSoundCheckBox.TabIndex = 42;
            noSoundCheckBox.Text = "No sound";
            noSoundCheckBox.UseVisualStyleBackColor = true;
            label6.AutoSize = true;
            label6.Location = new Point(30, 383);
            label6.Name = "label6";
            label6.Size = new Size(45, 12);
            label6.TabIndex = 103;
            label6.Text = "Stage:";
            stageComboBox.AllowDrop = true;
            stageComboBox.AutoCompleteMode = AutoCompleteMode.Suggest;
            stageComboBox.AutoCompleteSource = AutoCompleteSource.ListItems;
            stageComboBox.FormattingEnabled = true;
            stageComboBox.Location = new Point(86, 380);
            stageComboBox.MaxLength = 64;
            stageComboBox.Name = "stageComboBox";
            stageComboBox.Size = new Size(356, 20);
            stageComboBox.TabIndex = 40;
            stageComboBox.Text = "(No setting)";
            AutoScaleDimensions = new SizeF(6f, 12f);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(464, 480);
            Controls.Add(stageComboBox);
            Controls.Add(label6);
            Controls.Add(noSoundCheckBox);
            Controls.Add(noMusicCheckBox);
            Controls.Add(swapButton);
            Controls.Add(roundComboBox);
            Controls.Add(label5);
            Controls.Add(groupBox2);
            Controls.Add(folderButton);
            Controls.Add(textBox3);
            Controls.Add(groupBox1);
            Controls.Add(cancelButton);
            Controls.Add(okButton);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = nameof(QuickVSModePanel);
            ShowIcon = false;
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.Manual;
            Text = "Quick Vs. Mode";
            Load += new EventHandler(QuickVSModePanel_Load);
            groupBox1.ResumeLayout(false);
            p1tabControl.ResumeLayout(false);
            p1tabPage.ResumeLayout(false);
            p1tabPage.PerformLayout();
            powerP1NumericUpDown.EndInit();
            lifeP1NumericUpDown.EndInit();
            p3tabPage.ResumeLayout(false);
            p3tabPage.PerformLayout();
            powerP3NumericUpDown.EndInit();
            lifeP3NumericUpDown.EndInit();
            groupBox2.ResumeLayout(false);
            p2tabControl.ResumeLayout(false);
            p2tabPage.ResumeLayout(false);
            p2tabPage.PerformLayout();
            powerP2NumericUpDown.EndInit();
            lifeP2NumericUpDown.EndInit();
            p4tabPage.ResumeLayout(false);
            p4tabPage.PerformLayout();
            powerP4NumericUpDown.EndInit();
            lifeP4NumericUpDown.EndInit();
            ResumeLayout(false);
            PerformLayout();
        }
    }
}
