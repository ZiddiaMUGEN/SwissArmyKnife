// Decompiled with JetBrains decompiler
// Type: SwissArmyKnifeForMugen.QuickVSModePanel
// Assembly: SAKnifeWM, Version=1.0.8.0, Culture=neutral, PublicKeyToken=null
// MVID: 09478AD8-365C-4BF3-BEA1-B5785151259B
// Assembly location: C:\Users\ziddi\Downloads\Swiss Army Knife 1.1 Conversion\Swiss Army Knife 1.1 Conversion\SAKnifeWM.exe

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;

namespace SwissArmyKnifeForMugen
{
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
    private IContainer components;
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

    public QuickVSModePanel() => this.InitializeComponent();

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
      this.locX = x;
      this.locY = y;
    }

    public DialogResult GetResult() => this._result;

    private void Init()
    {
      this.colorP1ComboBox.Items.Add((object) "1");
      this.colorP1ComboBox.Items.Add((object) "2");
      this.colorP1ComboBox.Items.Add((object) "3");
      this.colorP1ComboBox.Items.Add((object) "4");
      this.colorP1ComboBox.Items.Add((object) "5");
      this.colorP1ComboBox.Items.Add((object) "6");
      this.colorP1ComboBox.Items.Add((object) "7");
      this.colorP1ComboBox.Items.Add((object) "8");
      this.colorP1ComboBox.Items.Add((object) "9");
      this.colorP1ComboBox.Items.Add((object) "10");
      this.colorP1ComboBox.Items.Add((object) "11");
      this.colorP1ComboBox.Items.Add((object) "12");
      this.colorP2ComboBox.Items.Add((object) "1");
      this.colorP2ComboBox.Items.Add((object) "2");
      this.colorP2ComboBox.Items.Add((object) "3");
      this.colorP2ComboBox.Items.Add((object) "4");
      this.colorP2ComboBox.Items.Add((object) "5");
      this.colorP2ComboBox.Items.Add((object) "6");
      this.colorP2ComboBox.Items.Add((object) "7");
      this.colorP2ComboBox.Items.Add((object) "8");
      this.colorP2ComboBox.Items.Add((object) "9");
      this.colorP2ComboBox.Items.Add((object) "10");
      this.colorP2ComboBox.Items.Add((object) "11");
      this.colorP2ComboBox.Items.Add((object) "12");
      this.colorP3ComboBox.Items.Add((object) "1");
      this.colorP3ComboBox.Items.Add((object) "2");
      this.colorP3ComboBox.Items.Add((object) "3");
      this.colorP3ComboBox.Items.Add((object) "4");
      this.colorP3ComboBox.Items.Add((object) "5");
      this.colorP3ComboBox.Items.Add((object) "6");
      this.colorP3ComboBox.Items.Add((object) "7");
      this.colorP3ComboBox.Items.Add((object) "8");
      this.colorP3ComboBox.Items.Add((object) "9");
      this.colorP3ComboBox.Items.Add((object) "10");
      this.colorP3ComboBox.Items.Add((object) "11");
      this.colorP3ComboBox.Items.Add((object) "12");
      this.colorP4ComboBox.Items.Add((object) "1");
      this.colorP4ComboBox.Items.Add((object) "2");
      this.colorP4ComboBox.Items.Add((object) "3");
      this.colorP4ComboBox.Items.Add((object) "4");
      this.colorP4ComboBox.Items.Add((object) "5");
      this.colorP4ComboBox.Items.Add((object) "6");
      this.colorP4ComboBox.Items.Add((object) "7");
      this.colorP4ComboBox.Items.Add((object) "8");
      this.colorP4ComboBox.Items.Add((object) "9");
      this.colorP4ComboBox.Items.Add((object) "10");
      this.colorP4ComboBox.Items.Add((object) "11");
      this.colorP4ComboBox.Items.Add((object) "12");
      this.roundComboBox.Items.Add((object) "1");
      this.roundComboBox.Items.Add((object) "2");
      this.roundComboBox.Items.Add((object) "3");
      this.roundComboBox.Items.Add((object) "4");
      this.roundComboBox.Items.Add((object) "5");
      this.stageComboBox.Items.Add((object) "(No setting)");
      MugenProfile currentProfile = ProfileManager.MainObj().GetCurrentProfile();
      if (currentProfile == null)
        return;
      string path = Path.Combine(currentProfile.GetMugenPath(), "stages");
      foreach (string file in Directory.GetFiles(path, "*.def", SearchOption.AllDirectories))
      {
        string str = file.Substring(path.Length + 1);
        this.stageComboBox.Items.Add((object) str.Remove(str.Length - 4).Replace('\\', '/'));
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
      this.allCharDefs = stringList.ToArray();
    }

    private void LoadProfile()
    {
      MugenProfile currentProfile = ProfileManager.MainObj().GetCurrentProfile();
      if (currentProfile == null)
        return;
      QuickVSProfile quickVsProfile = currentProfile.GetQuickVSProfile();
      if (quickVsProfile == null)
        return;
      this.defP1ComboBox.Items.AddRange((object[]) quickVsProfile.GetP1History());
      this.defP1ComboBox.Items.AddRange((object[]) this.allCharDefs);
      this.defP1ComboBox.Text = quickVsProfile.GetP1Def();
      this.colorP1ComboBox.Text = quickVsProfile.GetP1Color().ToString();
      this.aiP1CheckBox.Checked = quickVsProfile.GetP1AI();
      this.lifeP1CheckBox.Checked = quickVsProfile.GetP1LifeFlag();
      this.lifeP1NumericUpDown.Text = quickVsProfile.GetP1LifeValue().ToString();
      this.powerP1CheckBox.Checked = quickVsProfile.GetP1PowerFlag();
      this.powerP1NumericUpDown.Text = quickVsProfile.GetP1PowerValue().ToString();
      this.defP2ComboBox.Items.AddRange((object[]) quickVsProfile.GetP2History());
      this.defP2ComboBox.Items.AddRange((object[]) this.allCharDefs);
      this.defP2ComboBox.Text = quickVsProfile.GetP2Def();
      this.colorP2ComboBox.Text = quickVsProfile.GetP2Color().ToString();
      this.aiP2CheckBox.Checked = quickVsProfile.GetP2AI();
      this.lifeP2CheckBox.Checked = quickVsProfile.GetP2LifeFlag();
      this.lifeP2NumericUpDown.Text = quickVsProfile.GetP2LifeValue().ToString();
      this.powerP2CheckBox.Checked = quickVsProfile.GetP2PowerFlag();
      this.powerP2NumericUpDown.Text = quickVsProfile.GetP2PowerValue().ToString();
      this.defP3ComboBox.Items.Add((object) "(No setting)");
      this.defP3ComboBox.Items.AddRange((object[]) quickVsProfile.GetP3History());
      this.defP3ComboBox.Items.AddRange((object[]) this.allCharDefs);
      this.defP3ComboBox.Text = quickVsProfile.GetP3Def();
      this.colorP3ComboBox.Text = quickVsProfile.GetP3Color().ToString();
      this.aiP3CheckBox.Checked = quickVsProfile.GetP3AI();
      this.lifeP3CheckBox.Checked = quickVsProfile.GetP3LifeFlag();
      this.lifeP3NumericUpDown.Text = quickVsProfile.GetP3LifeValue().ToString();
      this.powerP3CheckBox.Checked = quickVsProfile.GetP3PowerFlag();
      this.powerP3NumericUpDown.Text = quickVsProfile.GetP3PowerValue().ToString();
      this.defP4ComboBox.Items.Add((object) "(No setting)");
      this.defP4ComboBox.Items.AddRange((object[]) quickVsProfile.GetP4History());
      this.defP4ComboBox.Items.AddRange((object[]) this.allCharDefs);
      this.defP4ComboBox.Text = quickVsProfile.GetP4Def();
      this.colorP4ComboBox.Text = quickVsProfile.GetP4Color().ToString();
      this.aiP4CheckBox.Checked = quickVsProfile.GetP4AI();
      this.lifeP4CheckBox.Checked = quickVsProfile.GetP4LifeFlag();
      this.lifeP4NumericUpDown.Text = quickVsProfile.GetP4LifeValue().ToString();
      this.powerP4CheckBox.Checked = quickVsProfile.GetP4PowerFlag();
      this.powerP4NumericUpDown.Text = quickVsProfile.GetP4PowerValue().ToString();
      this.roundComboBox.Text = quickVsProfile.GetRounds().ToString();
      this.noMusicCheckBox.Checked = quickVsProfile.GetNoMusicFlag();
      this.noSoundCheckBox.Checked = quickVsProfile.GetNoSoundFlag();
      if (quickVsProfile.GetStage() == "")
        this.stageComboBox.Text = "(No setting)";
      else
        this.stageComboBox.Text = quickVsProfile.GetStage();
    }

    private void SaveProfile()
    {
      MugenProfile currentProfile = ProfileManager.MainObj().GetCurrentProfile();
      if (currentProfile == null)
        return;
      QuickVSProfile quickVsProfile = currentProfile.GetQuickVSProfile();
      if (quickVsProfile == null)
        return;
      quickVsProfile.SetP1Def(this.defP1ComboBox.Text);
      int result;
      if (int.TryParse(this.colorP1ComboBox.Text, out result))
        quickVsProfile.SetP1Color(result);
      quickVsProfile.SetP1AI(this.aiP1CheckBox.Checked);
      quickVsProfile.SetP1LifeFlag(this.lifeP1CheckBox.Checked);
      if (int.TryParse(this.lifeP1NumericUpDown.Text, out result))
        quickVsProfile.SetP1LifeValue(result);
      quickVsProfile.SetP1PowerFlag(this.powerP1CheckBox.Checked);
      if (int.TryParse(this.powerP1NumericUpDown.Text, out result))
        quickVsProfile.SetP1PowerValue(result);
      quickVsProfile.SetP2Def(this.defP2ComboBox.Text);
      if (int.TryParse(this.colorP2ComboBox.Text, out result))
        quickVsProfile.SetP2Color(result);
      quickVsProfile.SetP2AI(this.aiP2CheckBox.Checked);
      quickVsProfile.SetP2LifeFlag(this.lifeP2CheckBox.Checked);
      if (int.TryParse(this.lifeP2NumericUpDown.Text, out result))
        quickVsProfile.SetP2LifeValue(result);
      quickVsProfile.SetP2PowerFlag(this.powerP2CheckBox.Checked);
      if (int.TryParse(this.powerP2NumericUpDown.Text, out result))
        quickVsProfile.SetP2PowerValue(result);
      string text1 = this.defP3ComboBox.Text;
      if (text1 != null && text1 != "" && text1 != "(No setting)")
      {
        quickVsProfile.SetP3Def(this.defP3ComboBox.Text);
        if (int.TryParse(this.colorP3ComboBox.Text, out result))
          quickVsProfile.SetP3Color(result);
        quickVsProfile.SetP3AI(this.aiP3CheckBox.Checked);
        quickVsProfile.SetP3LifeFlag(this.lifeP3CheckBox.Checked);
        if (int.TryParse(this.lifeP3NumericUpDown.Text, out result))
          quickVsProfile.SetP3LifeValue(result);
        quickVsProfile.SetP3PowerFlag(this.powerP3CheckBox.Checked);
        if (int.TryParse(this.powerP3NumericUpDown.Text, out result))
          quickVsProfile.SetP3PowerValue(result);
      }
      else
        quickVsProfile.SetP3Def("");
      string text2 = this.defP4ComboBox.Text;
      if (text2 != null && text2 != "" && text2 != "(No setting)")
      {
        quickVsProfile.SetP4Def(this.defP4ComboBox.Text);
        if (int.TryParse(this.colorP4ComboBox.Text, out result))
          quickVsProfile.SetP4Color(result);
        quickVsProfile.SetP4AI(this.aiP4CheckBox.Checked);
        quickVsProfile.SetP4LifeFlag(this.lifeP4CheckBox.Checked);
        if (int.TryParse(this.lifeP4NumericUpDown.Text, out result))
          quickVsProfile.SetP4LifeValue(result);
        quickVsProfile.SetP4PowerFlag(this.powerP4CheckBox.Checked);
        if (int.TryParse(this.powerP4NumericUpDown.Text, out result))
          quickVsProfile.SetP4PowerValue(result);
      }
      else
        quickVsProfile.SetP4Def("");
      if (int.TryParse(this.roundComboBox.Text, out result))
        quickVsProfile.SetRounds(result);
      quickVsProfile.SetNoMusicFlag(this.noMusicCheckBox.Checked);
      quickVsProfile.SetNoSoundFlag(this.noSoundCheckBox.Checked);
      if (this.stageComboBox.Text == null || this.stageComboBox.Text == "(No setting)")
        quickVsProfile.SetStage("");
      else
        quickVsProfile.SetStage(this.stageComboBox.Text);
      quickVsProfile.SaveConfigData();
    }

    private bool CheckValues()
    {
      MugenProfile currentProfile = ProfileManager.MainObj().GetCurrentProfile();
      if (currentProfile == null)
      {
        int num = (int) MessageBox.Show("No profile has been selected.", "Swiss Army Knife");
        return false;
      }
      string mugenPath = currentProfile.GetMugenPath();
      if (mugenPath == null)
      {
        int num = (int) MessageBox.Show("The mugen executable has not been specified.", "Swiss Army Knife");
        return false;
      }
      string path1 = Path.Combine(mugenPath, "chars");
      string text1 = this.defP1ComboBox.Text;
      if (text1 == null || text1 == "" || text1 == "(No setting)")
      {
        int num = (int) MessageBox.Show("No def file has been specified for player 1.", "Swiss Army Knife");
        this.p1tabControl.SelectedIndex = 0;
        this.defP1ComboBox.Focus();
        this.defP1ComboBox.SelectAll();
        return false;
      }
      string str1 = Path.Combine(path1, text1);
      if (Path.GetExtension(text1) == ".def")
      {
        if (!File.Exists(str1))
        {
          int num = (int) MessageBox.Show("The specified def file for player 1 does not exist. \r\n\r\n" + str1, "Swiss Army Knife");
          this.p1tabControl.SelectedIndex = 0;
          this.defP1ComboBox.Focus();
          this.defP1ComboBox.SelectAll();
          return false;
        }
      }
      else
      {
        if (!Directory.Exists(str1))
        {
          int num = (int) MessageBox.Show("The specified folder for player 1 does not exist. \r\n\r\n" + str1, "Swiss Army Knife");
          this.p1tabControl.SelectedIndex = 0;
          this.defP1ComboBox.Focus();
          this.defP1ComboBox.SelectAll();
          return false;
        }
        string path2 = Path.GetFileName(str1) + ".def";
        string path = Path.Combine(str1, path2);
        if (!File.Exists(path))
        {
          int num = (int) MessageBox.Show("The specified def file for player 1 does not exist. \r\n\r\n" + path, "Swiss Army Knife");
          this.p1tabControl.SelectedIndex = 0;
          this.defP1ComboBox.Focus();
          this.defP1ComboBox.SelectAll();
          return false;
        }
      }
      int result;
      if (!int.TryParse(this.colorP1ComboBox.Text, out result))
      {
        int num = (int) MessageBox.Show("The color for player 1 should be an integer between 1 and 12.", "Swiss Army Knife");
        this.p1tabControl.SelectedIndex = 0;
        this.colorP1ComboBox.Focus();
        this.colorP1ComboBox.SelectAll();
        return false;
      }
      string text2 = this.defP2ComboBox.Text;
      if (text2 == null || text2 == "" || text2 == "(No setting)")
      {
        int num = (int) MessageBox.Show("No def file has been specified for player 2.", "Swiss Army Knife");
        this.p2tabControl.SelectedIndex = 0;
        this.defP2ComboBox.Focus();
        this.defP2ComboBox.SelectAll();
        return false;
      }
      string str2 = Path.Combine(path1, text2);
      if (Path.GetExtension(text2) == ".def")
      {
        if (!File.Exists(str2))
        {
          int num = (int) MessageBox.Show("The specified def file for player 2 does not exist. \r\n\r\n" + str2, "Swiss Army Knife");
          this.p2tabControl.SelectedIndex = 0;
          this.defP2ComboBox.Focus();
          this.defP2ComboBox.SelectAll();
          return false;
        }
      }
      else
      {
        if (!Directory.Exists(str2))
        {
          int num = (int) MessageBox.Show("The specified folder for player 2 does not exist. \r\n\r\n" + str2, "Swiss Army Knife");
          this.p2tabControl.SelectedIndex = 0;
          this.defP2ComboBox.Focus();
          this.defP2ComboBox.SelectAll();
          return false;
        }
        string path2 = Path.GetFileName(str2) + ".def";
        string path = Path.Combine(str2, path2);
        if (!File.Exists(path))
        {
          int num = (int) MessageBox.Show("The specified def file for player 2 does not exist. \r\n\r\n" + path, "Swiss Army Knife");
          this.p2tabControl.SelectedIndex = 0;
          this.defP2ComboBox.Focus();
          this.defP2ComboBox.SelectAll();
          return false;
        }
      }
      if (!int.TryParse(this.colorP2ComboBox.Text, out result))
      {
        int num = (int) MessageBox.Show("The color for player 2 should be an integer between 1 and 12.", "Swiss Army Knife");
        this.p2tabControl.SelectedIndex = 0;
        this.colorP2ComboBox.Focus();
        this.colorP2ComboBox.SelectAll();
        return false;
      }
      string text3 = this.defP3ComboBox.Text;
      if (text3 != null && text3 != "" && text3 != "(No setting)")
      {
        string str3 = Path.Combine(path1, text3);
        if (Path.GetExtension(text3) == ".def")
        {
          if (!File.Exists(str3))
          {
            int num = (int) MessageBox.Show("The specified def file for player 3 does not exist. \r\n\r\n" + str3, "Swiss Army Knife");
            this.p1tabControl.SelectedIndex = 1;
            this.defP3ComboBox.Focus();
            this.defP3ComboBox.SelectAll();
            return false;
          }
        }
        else
        {
          if (!Directory.Exists(str3))
          {
            int num = (int) MessageBox.Show("The specified folder for player 3 does not exist. \r\n\r\n" + str3, "Swiss Army Knife");
            this.p1tabControl.SelectedIndex = 1;
            this.defP3ComboBox.Focus();
            this.defP3ComboBox.SelectAll();
            return false;
          }
          string path2 = Path.GetFileName(str3) + ".def";
          string path = Path.Combine(str3, path2);
          if (!File.Exists(path))
          {
            int num = (int) MessageBox.Show("The specified def file for player 3 does not exist. \r\n\r\n" + path, "Swiss Army Knife");
            this.p1tabControl.SelectedIndex = 1;
            this.defP3ComboBox.Focus();
            this.defP3ComboBox.SelectAll();
            return false;
          }
        }
        if (!int.TryParse(this.colorP3ComboBox.Text, out result))
        {
          int num = (int) MessageBox.Show("The color for player 3 should be an integer between 1 and 12.", "Swiss Army Knife");
          this.p1tabControl.SelectedIndex = 1;
          this.colorP3ComboBox.Focus();
          this.colorP3ComboBox.SelectAll();
          return false;
        }
      }
      string text4 = this.defP4ComboBox.Text;
      if (text4 != null && text4 != "" && text4 != "(No setting)")
      {
        string str3 = Path.Combine(path1, text4);
        if (Path.GetExtension(text4) == ".def")
        {
          if (!File.Exists(str3))
          {
            int num = (int) MessageBox.Show("The specified def file for player 4 does not exist. \r\n\r\n" + str3, "Swiss Army Knife");
            this.p2tabControl.SelectedIndex = 1;
            this.defP4ComboBox.Focus();
            this.defP4ComboBox.SelectAll();
            return false;
          }
        }
        else
        {
          if (!Directory.Exists(str3))
          {
            int num = (int) MessageBox.Show("The specified folder for player 4 does not exist. \r\n\r\n" + str3, "Swiss Army Knife");
            this.p2tabControl.SelectedIndex = 1;
            this.defP4ComboBox.Focus();
            this.defP4ComboBox.SelectAll();
            return false;
          }
          string path2 = Path.GetFileName(str3) + ".def";
          string path = Path.Combine(str3, path2);
          if (!File.Exists(path))
          {
            int num = (int) MessageBox.Show("The specified def file for player 4 does not exist. \r\n\r\n" + path, "Swiss Army Knife");
            this.p2tabControl.SelectedIndex = 1;
            this.defP4ComboBox.Focus();
            this.defP4ComboBox.SelectAll();
            return false;
          }
        }
        if (!int.TryParse(this.colorP4ComboBox.Text, out result))
        {
          int num = (int) MessageBox.Show("The color for player 4 should be an integer between 1 and 12.", "Swiss Army Knife");
          this.p2tabControl.SelectedIndex = 1;
          this.colorP4ComboBox.Focus();
          this.colorP4ComboBox.SelectAll();
          return false;
        }
      }
      string text5 = this.stageComboBox.Text;
      if (text5 != null && text5 != "" && text5 != "(No setting)")
      {
        string path = Path.Combine(Path.Combine(mugenPath, "stages"), text5) + ".def";
        if (!File.Exists(path))
        {
          int num = (int) MessageBox.Show("The specified def file for the stage does not exist. \r\n\r\n" + path, "Swiss Army Knife");
          this.stageComboBox.Focus();
          this.stageComboBox.SelectAll();
          return false;
        }
      }
      if (!int.TryParse(this.roundComboBox.Text, out result))
      {
        int num = (int) MessageBox.Show("The number of rounds should be an integer between 1 and 99.", "Swiss Army Knife");
        this.roundComboBox.Focus();
        this.roundComboBox.SelectAll();
        return false;
      }
      if (result >= 1 && result <= 99)
        return true;
      int num1 = (int) MessageBox.Show("The number of rounds should be an integer between 1 and 99.", "Swiss Army Knife");
      this.roundComboBox.Focus();
      this.roundComboBox.SelectAll();
      return false;
    }

    private void QuickVSModePanel_Load(object sender, EventArgs e)
    {
      this.SetDesktopLocation(this.locX, this.locY);
      this.Init();
      this.LoadProfile();
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
          num1 = QuickVSModePanel.GetForegroundWindow();
          if (num1 == this.Handle)
            num1 = IntPtr.Zero;
        }
        else
          break;
      }
      while (num1 == IntPtr.Zero);
      if (!(num1 != IntPtr.Zero))
        return;
      IntPtr hWnd = num1;
      Point location = this.Location;
      int num3 = location.X + this.Size.Width + 5;
      location = this.Location;
      int y = location.Y;
      Size size = this.Size;
      int width = size.Width;
      size = this.Size;
      int height = size.Height;
      int x = num3;
      int Y = y;
      int cx = width;
      int cy = height;
      QuickVSModePanel.SetWindowPos(hWnd, 0, x, Y, cx, cy, 69);
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
      if (!this.CheckValues())
        return;
      this._result = DialogResult.OK;
      this.SaveProfile();
      MainForm.MainObj().ActivateAll(true);
      this.Close();
    }

    private void cancelButton_Click(object sender, EventArgs e)
    {
      this._result = DialogResult.Cancel;
      this.Close();
    }

    private void defP1ComboBox_DragEnter(object sender, DragEventArgs e)
    {
      e.Effect = !e.Data.GetDataPresent(DataFormats.FileDrop) ? DragDropEffects.None : DragDropEffects.Link;
      this.Activate();
    }

    private void defP1ComboBox_DragDrop(object sender, DragEventArgs e)
    {
      string[] data = (string[]) e.Data.GetData(DataFormats.FileDrop, false);
      if (data == null || data.Length < 1)
        return;
      MugenProfile currentProfile = ProfileManager.MainObj().GetCurrentProfile();
      if (currentProfile == null)
      {
        int num1 = (int) MessageBox.Show("No profile has been selected.", "Swiss Army Knife");
      }
      else
      {
        string mugenPath = currentProfile.GetMugenPath();
        if (mugenPath == null)
        {
          int num2 = (int) MessageBox.Show("The mugen executable has not been specified.", "Swiss Army Knife");
        }
        else
        {
          string str1 = (string) null;
          string str2 = (string) null;
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
              int num3 = (int) MessageBox.Show("The path of the def file and the path of the chars folder do not belong to the same root. This character possibly belongs to a different mugen installation.", "Swiss Army Knife");
              return;
            }
            this.defP1ComboBox.Text = str1.Substring(str3.Length + 1);
          }
          if (str2 == null)
            return;
          if (str2.IndexOf(str3) < 0)
          {
            int num4 = (int) MessageBox.Show("The path of the def file and the path of the chars folder do not belong to the same root. This character possibly belongs to a different mugen installation.", "Swiss Army Knife");
          }
          else
            this.defP2ComboBox.Text = str2.Substring(str3.Length + 1);
        }
      }
    }

    private void p1Well_DragEnter(object sender, DragEventArgs e) => this.defP1ComboBox_DragEnter(sender, e);

    private void p1Well_DragDrop(object sender, DragEventArgs e) => this.defP1ComboBox_DragDrop(sender, e);

    private void defP2ComboBox_DragEnter(object sender, DragEventArgs e)
    {
      e.Effect = !e.Data.GetDataPresent(DataFormats.FileDrop) ? DragDropEffects.None : DragDropEffects.Link;
      this.Activate();
    }

    private void defP2ComboBox_DragDrop(object sender, DragEventArgs e)
    {
      string[] data = (string[]) e.Data.GetData(DataFormats.FileDrop, false);
      if (data == null || data.Length < 1)
        return;
      MugenProfile currentProfile = ProfileManager.MainObj().GetCurrentProfile();
      if (currentProfile == null)
      {
        int num1 = (int) MessageBox.Show("No profile has been selected.", "Swiss Army Knife");
      }
      else
      {
        string mugenPath = currentProfile.GetMugenPath();
        if (mugenPath == null)
        {
          int num2 = (int) MessageBox.Show("The mugen executable has not been specified.", "Swiss Army Knife");
        }
        else
        {
          string str1 = (string) null;
          string str2 = (string) null;
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
              int num3 = (int) MessageBox.Show("The path of the def file and the path of the chars folder do not belong to the same root. This character possibly belongs to a different mugen installation.", "Swiss Army Knife");
              return;
            }
            this.defP1ComboBox.Text = str1.Substring(str3.Length + 1);
          }
          if (str2 == null)
            return;
          if (str2.IndexOf(str3) < 0)
          {
            int num4 = (int) MessageBox.Show("The path of the def file and the path of the chars folder do not belong to the same root. This character possibly belongs to a different mugen installation.", "Swiss Army Knife");
          }
          else
            this.defP2ComboBox.Text = str2.Substring(str3.Length + 1);
        }
      }
    }

    private void p2Well_DragEnter(object sender, DragEventArgs e) => this.defP2ComboBox_DragEnter(sender, e);

    private void p2Well_DragDrop(object sender, DragEventArgs e) => this.defP2ComboBox_DragDrop(sender, e);

    private void swapButton_Click(object sender, EventArgs e)
    {
      string text1 = this.defP1ComboBox.Text;
      string text2 = this.colorP1ComboBox.Text;
      bool flag1 = this.aiP1CheckBox.Checked;
      bool flag2 = this.lifeP1CheckBox.Checked;
      string text3 = this.lifeP1NumericUpDown.Text;
      bool flag3 = this.powerP1CheckBox.Checked;
      string text4 = this.powerP1NumericUpDown.Text;
      this.defP1ComboBox.Text = this.defP2ComboBox.Text;
      this.colorP1ComboBox.Text = this.colorP2ComboBox.Text;
      this.aiP1CheckBox.Checked = this.aiP2CheckBox.Checked;
      this.lifeP1CheckBox.Checked = this.lifeP2CheckBox.Checked;
      this.lifeP1NumericUpDown.Text = this.lifeP2NumericUpDown.Text;
      this.powerP1CheckBox.Checked = this.powerP2CheckBox.Checked;
      this.powerP1NumericUpDown.Text = this.powerP2NumericUpDown.Text;
      this.defP2ComboBox.Text = text1;
      this.colorP2ComboBox.Text = text2;
      this.aiP2CheckBox.Checked = flag1;
      this.lifeP2CheckBox.Checked = flag2;
      this.lifeP2NumericUpDown.Text = text3;
      this.powerP2CheckBox.Checked = flag3;
      this.powerP2NumericUpDown.Text = text4;
      string text5 = this.defP3ComboBox.Text;
      string text6 = this.colorP3ComboBox.Text;
      bool flag4 = this.aiP3CheckBox.Checked;
      bool flag5 = this.lifeP3CheckBox.Checked;
      string text7 = this.lifeP3NumericUpDown.Text;
      bool flag6 = this.powerP3CheckBox.Checked;
      string text8 = this.powerP3NumericUpDown.Text;
      this.defP3ComboBox.Text = this.defP4ComboBox.Text;
      this.colorP3ComboBox.Text = this.colorP4ComboBox.Text;
      this.aiP3CheckBox.Checked = this.aiP4CheckBox.Checked;
      this.lifeP3CheckBox.Checked = this.lifeP4CheckBox.Checked;
      this.lifeP3NumericUpDown.Text = this.lifeP4NumericUpDown.Text;
      this.powerP3CheckBox.Checked = this.powerP4CheckBox.Checked;
      this.powerP3NumericUpDown.Text = this.powerP4NumericUpDown.Text;
      this.defP4ComboBox.Text = text5;
      this.colorP4ComboBox.Text = text6;
      this.aiP4CheckBox.Checked = flag4;
      this.lifeP4CheckBox.Checked = flag5;
      this.lifeP4NumericUpDown.Text = text7;
      this.powerP4CheckBox.Checked = flag6;
      this.powerP4NumericUpDown.Text = text8;
    }

    private void defP3ComboBox_DragEnter(object sender, DragEventArgs e)
    {
      e.Effect = !e.Data.GetDataPresent(DataFormats.FileDrop) ? DragDropEffects.None : DragDropEffects.Link;
      this.Activate();
    }

    private void defP3ComboBox_DragDrop(object sender, DragEventArgs e)
    {
      string[] data = (string[]) e.Data.GetData(DataFormats.FileDrop, false);
      if (data == null || data.Length < 1)
        return;
      MugenProfile currentProfile = ProfileManager.MainObj().GetCurrentProfile();
      if (currentProfile == null)
      {
        int num1 = (int) MessageBox.Show("No profile has been selected.", "Swiss Army Knife");
      }
      else
      {
        string mugenPath = currentProfile.GetMugenPath();
        if (mugenPath == null)
        {
          int num2 = (int) MessageBox.Show("The mugen executable has not been specified.", "Swiss Army Knife");
        }
        else
        {
          string str1 = (string) null;
          string str2 = (string) null;
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
              int num3 = (int) MessageBox.Show("The path of the def file and the path of the chars folder do not belong to the same root. This character possibly belongs to a different mugen installation.", "Swiss Army Knife");
              return;
            }
            this.defP3ComboBox.Text = str1.Substring(str3.Length + 1);
          }
          if (str2 == null)
            return;
          if (str2.IndexOf(str3) < 0)
          {
            int num4 = (int) MessageBox.Show("The path of the def file and the path of the chars folder do not belong to the same root. This character possibly belongs to a different mugen installation.", "Swiss Army Knife");
          }
          else
            this.defP4ComboBox.Text = str2.Substring(str3.Length + 1);
        }
      }
    }

    private void p3Well_DragEnter(object sender, DragEventArgs e) => this.defP3ComboBox_DragEnter(sender, e);

    private void p3Well_DragDrop(object sender, DragEventArgs e) => this.defP3ComboBox_DragDrop(sender, e);

    private void defP4ComboBox_DragEnter(object sender, DragEventArgs e)
    {
      e.Effect = !e.Data.GetDataPresent(DataFormats.FileDrop) ? DragDropEffects.None : DragDropEffects.Link;
      this.Activate();
    }

    private void defP4ComboBox_DragDrop(object sender, DragEventArgs e)
    {
      string[] data = (string[]) e.Data.GetData(DataFormats.FileDrop, false);
      if (data == null || data.Length < 1)
        return;
      MugenProfile currentProfile = ProfileManager.MainObj().GetCurrentProfile();
      if (currentProfile == null)
      {
        int num1 = (int) MessageBox.Show("No profile has been selected.", "Swiss Army Knife");
      }
      else
      {
        string mugenPath = currentProfile.GetMugenPath();
        if (mugenPath == null)
        {
          int num2 = (int) MessageBox.Show("The mugen executable has not been specified.", "Swiss Army Knife");
        }
        else
        {
          string str1 = (string) null;
          string str2 = (string) null;
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
              int num3 = (int) MessageBox.Show("The path of the def file and the path of the chars folder do not belong to the same root. This character possibly belongs to a different mugen installation.", "Swiss Army Knife");
              return;
            }
            this.defP3ComboBox.Text = str1.Substring(str3.Length + 1);
          }
          if (str2 == null)
            return;
          if (str2.IndexOf(str3) < 0)
          {
            int num4 = (int) MessageBox.Show("The path of the def file and the path of the chars folder do not belong to the same root. This character possibly belongs to a different mugen installation.", "Swiss Army Knife");
          }
          else
            this.defP4ComboBox.Text = str2.Substring(str3.Length + 1);
        }
      }
    }

    private void p4Well_DragEnter(object sender, DragEventArgs e) => this.defP4ComboBox_DragEnter(sender, e);

    private void p4Well_DragDrop(object sender, DragEventArgs e) => this.defP4ComboBox_DragDrop(sender, e);

    private void lifeP1CheckBox_CheckedChanged(object sender, EventArgs e) => this.lifeP1NumericUpDown.Enabled = this.lifeP1CheckBox.Checked;

    private void powerP1CheckBox_CheckedChanged(object sender, EventArgs e) => this.powerP1NumericUpDown.Enabled = this.powerP1CheckBox.Checked;

    private void lifeP2CheckBox_CheckedChanged(object sender, EventArgs e) => this.lifeP2NumericUpDown.Enabled = this.lifeP2CheckBox.Checked;

    private void powerP2CheckBox_CheckedChanged(object sender, EventArgs e) => this.powerP2NumericUpDown.Enabled = this.powerP2CheckBox.Checked;

    private void lifeP3CheckBox_CheckedChanged(object sender, EventArgs e) => this.lifeP3NumericUpDown.Enabled = this.lifeP3CheckBox.Checked;

    private void powerP3CheckBox_CheckedChanged(object sender, EventArgs e) => this.powerP3NumericUpDown.Enabled = this.powerP3CheckBox.Checked;

    private void lifeP4CheckBox_CheckedChanged(object sender, EventArgs e) => this.lifeP4NumericUpDown.Enabled = this.lifeP4CheckBox.Checked;

    private void powerP4CheckBox_CheckedChanged(object sender, EventArgs e) => this.powerP4NumericUpDown.Enabled = this.powerP4CheckBox.Checked;

    private void p1tabControl_SelectedIndexChanged(object sender, EventArgs e) => this.p2tabControl.SelectedIndex = this.p1tabControl.SelectedIndex;

    private void p2tabControl_SelectedIndexChanged(object sender, EventArgs e) => this.p1tabControl.SelectedIndex = this.p2tabControl.SelectedIndex;

    private void defP1ComboBox_KeyDown(object sender, KeyEventArgs e)
    {
      if (e.KeyCode != Keys.Return)
        return;
      this.defP2ComboBox.Select();
    }

    private void defP2ComboBox_KeyDown(object sender, KeyEventArgs e)
    {
      if (e.KeyCode != Keys.Return || !this.CheckValues())
        return;
      this._result = DialogResult.OK;
      this.SaveProfile();
      MainForm.MainObj().ActivateAll(true);
      this.Close();
    }

    private void defP3ComboBox_KeyDown(object sender, KeyEventArgs e)
    {
      if (e.KeyCode != Keys.Return)
        return;
      this.defP4ComboBox.Select();
    }

    private void defP4ComboBox_KeyDown(object sender, KeyEventArgs e)
    {
      if (e.KeyCode != Keys.Return || !this.CheckValues())
        return;
      this._result = DialogResult.OK;
      this.SaveProfile();
      MainForm.MainObj().ActivateAll(true);
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
      this.cancelButton = new Button();
      this.okButton = new Button();
      this.groupBox1 = new GroupBox();
      this.p1tabControl = new TabControl();
      this.p1tabPage = new TabPage();
      this.powerP1NumericUpDown = new NumericUpDown();
      this.lifeP1NumericUpDown = new NumericUpDown();
      this.powerP1CheckBox = new CheckBox();
      this.lifeP1CheckBox = new CheckBox();
      this.aiP1CheckBox = new CheckBox();
      this.colorP1ComboBox = new ComboBox();
      this.label2 = new Label();
      this.defP1ComboBox = new ComboBox();
      this.label1 = new Label();
      this.p3tabPage = new TabPage();
      this.powerP3NumericUpDown = new NumericUpDown();
      this.lifeP3NumericUpDown = new NumericUpDown();
      this.powerP3CheckBox = new CheckBox();
      this.lifeP3CheckBox = new CheckBox();
      this.aiP3CheckBox = new CheckBox();
      this.colorP3ComboBox = new ComboBox();
      this.label7 = new Label();
      this.defP3ComboBox = new ComboBox();
      this.label8 = new Label();
      this.textBox3 = new TextBox();
      this.folderButton = new Button();
      this.groupBox2 = new GroupBox();
      this.p2tabControl = new TabControl();
      this.p2tabPage = new TabPage();
      this.powerP2NumericUpDown = new NumericUpDown();
      this.lifeP2NumericUpDown = new NumericUpDown();
      this.powerP2CheckBox = new CheckBox();
      this.lifeP2CheckBox = new CheckBox();
      this.aiP2CheckBox = new CheckBox();
      this.colorP2ComboBox = new ComboBox();
      this.label3 = new Label();
      this.defP2ComboBox = new ComboBox();
      this.label4 = new Label();
      this.p4tabPage = new TabPage();
      this.powerP4NumericUpDown = new NumericUpDown();
      this.lifeP4NumericUpDown = new NumericUpDown();
      this.powerP4CheckBox = new CheckBox();
      this.lifeP4CheckBox = new CheckBox();
      this.aiP4CheckBox = new CheckBox();
      this.colorP4ComboBox = new ComboBox();
      this.label9 = new Label();
      this.defP4ComboBox = new ComboBox();
      this.label10 = new Label();
      this.label5 = new Label();
      this.roundComboBox = new ComboBox();
      this.swapButton = new Button();
      this.noMusicCheckBox = new CheckBox();
      this.noSoundCheckBox = new CheckBox();
      this.label6 = new Label();
      this.stageComboBox = new ComboBox();
      this.groupBox1.SuspendLayout();
      this.p1tabControl.SuspendLayout();
      this.p1tabPage.SuspendLayout();
      this.powerP1NumericUpDown.BeginInit();
      this.lifeP1NumericUpDown.BeginInit();
      this.p3tabPage.SuspendLayout();
      this.powerP3NumericUpDown.BeginInit();
      this.lifeP3NumericUpDown.BeginInit();
      this.groupBox2.SuspendLayout();
      this.p2tabControl.SuspendLayout();
      this.p2tabPage.SuspendLayout();
      this.powerP2NumericUpDown.BeginInit();
      this.lifeP2NumericUpDown.BeginInit();
      this.p4tabPage.SuspendLayout();
      this.powerP4NumericUpDown.BeginInit();
      this.lifeP4NumericUpDown.BeginInit();
      this.SuspendLayout();
      this.cancelButton.Location = new Point(361, 442);
      this.cancelButton.Name = "cancelButton";
      this.cancelButton.Size = new Size(90, 28);
      this.cancelButton.TabIndex = 3;
      this.cancelButton.Text = "Cancel";
      this.cancelButton.UseVisualStyleBackColor = true;
      this.cancelButton.Click += new EventHandler(this.cancelButton_Click);
      this.okButton.Location = new Point(265, 442);
      this.okButton.Name = "okButton";
      this.okButton.Size = new Size(90, 28);
      this.okButton.TabIndex = 2;
      this.okButton.Text = "Start";
      this.okButton.UseVisualStyleBackColor = true;
      this.okButton.Click += new EventHandler(this.okButton_Click);
      this.groupBox1.Controls.Add((Control) this.p1tabControl);
      this.groupBox1.Location = new Point(12, 78);
      this.groupBox1.Name = "groupBox1";
      this.groupBox1.Size = new Size(440, 139);
      this.groupBox1.TabIndex = 10;
      this.groupBox1.TabStop = false;
      this.groupBox1.Text = "Player 1 side";
      this.p1tabControl.AllowDrop = true;
      this.p1tabControl.Controls.Add((Control) this.p1tabPage);
      this.p1tabControl.Controls.Add((Control) this.p3tabPage);
      this.p1tabControl.Location = new Point(6, 18);
      this.p1tabControl.Name = "p1tabControl";
      this.p1tabControl.SelectedIndex = 0;
      this.p1tabControl.Size = new Size(433, 115);
      this.p1tabControl.TabIndex = 102;
      this.p1tabControl.SelectedIndexChanged += new EventHandler(this.p1tabControl_SelectedIndexChanged);
      this.p1tabPage.AllowDrop = true;
      this.p1tabPage.Controls.Add((Control) this.powerP1NumericUpDown);
      this.p1tabPage.Controls.Add((Control) this.lifeP1NumericUpDown);
      this.p1tabPage.Controls.Add((Control) this.powerP1CheckBox);
      this.p1tabPage.Controls.Add((Control) this.lifeP1CheckBox);
      this.p1tabPage.Controls.Add((Control) this.aiP1CheckBox);
      this.p1tabPage.Controls.Add((Control) this.colorP1ComboBox);
      this.p1tabPage.Controls.Add((Control) this.label2);
      this.p1tabPage.Controls.Add((Control) this.defP1ComboBox);
      this.p1tabPage.Controls.Add((Control) this.label1);
      this.p1tabPage.Location = new Point(4, 22);
      this.p1tabPage.Name = "p1tabPage";
      this.p1tabPage.Padding = new Padding(3);
      this.p1tabPage.Size = new Size(425, 89);
      this.p1tabPage.TabIndex = 0;
      this.p1tabPage.Text = "player1";
      this.p1tabPage.UseVisualStyleBackColor = true;
      this.p1tabPage.DragDrop += new DragEventHandler(this.p1Well_DragDrop);
      this.p1tabPage.DragEnter += new DragEventHandler(this.p1Well_DragEnter);
      this.powerP1NumericUpDown.Enabled = false;
      this.powerP1NumericUpDown.Location = new Point(350, 62);
      this.powerP1NumericUpDown.Margin = new Padding(3, 3, 3, 0);
      this.powerP1NumericUpDown.Maximum = new Decimal(new int[4]
      {
        int.MaxValue,
        0,
        0,
        0
      });
      this.powerP1NumericUpDown.Name = "powerP1NumericUpDown";
      this.powerP1NumericUpDown.Size = new Size(68, 19);
      this.powerP1NumericUpDown.TabIndex = 107;
      this.powerP1NumericUpDown.TextAlign = HorizontalAlignment.Right;
      this.lifeP1NumericUpDown.Enabled = false;
      this.lifeP1NumericUpDown.Location = new Point(154, 62);
      this.lifeP1NumericUpDown.Margin = new Padding(3, 3, 3, 0);
      this.lifeP1NumericUpDown.Maximum = new Decimal(new int[4]
      {
        int.MaxValue,
        0,
        0,
        0
      });
      this.lifeP1NumericUpDown.Name = "lifeP1NumericUpDown";
      this.lifeP1NumericUpDown.Size = new Size(68, 19);
      this.lifeP1NumericUpDown.TabIndex = 105;
      this.lifeP1NumericUpDown.TextAlign = HorizontalAlignment.Right;
      this.lifeP1NumericUpDown.Value = new Decimal(new int[4]
      {
        1000,
        0,
        0,
        0
      });
      this.powerP1CheckBox.AutoSize = true;
      this.powerP1CheckBox.Location = new Point(258, 63);
      this.powerP1CheckBox.Name = "powerP1CheckBox";
      this.powerP1CheckBox.Size = new Size(91, 16);
      this.powerP1CheckBox.TabIndex = 106;
      this.powerP1CheckBox.Text = "Initial power:";
      this.powerP1CheckBox.UseVisualStyleBackColor = true;
      this.powerP1CheckBox.CheckedChanged += new EventHandler(this.powerP1CheckBox_CheckedChanged);
      this.lifeP1CheckBox.AutoSize = true;
      this.lifeP1CheckBox.Location = new Point(66, 63);
      this.lifeP1CheckBox.Name = "lifeP1CheckBox";
      this.lifeP1CheckBox.Size = new Size(87, 16);
      this.lifeP1CheckBox.TabIndex = 104;
      this.lifeP1CheckBox.Text = "Maximum life:";
      this.lifeP1CheckBox.UseVisualStyleBackColor = true;
      this.lifeP1CheckBox.CheckedChanged += new EventHandler(this.lifeP1CheckBox_CheckedChanged);
      this.aiP1CheckBox.AutoSize = true;
      this.aiP1CheckBox.Checked = true;
      this.aiP1CheckBox.CheckState = CheckState.Checked;
      this.aiP1CheckBox.Location = new Point(152, 36);
      this.aiP1CheckBox.Name = "aiP1CheckBox";
      this.aiP1CheckBox.Size = new Size(90, 16);
      this.aiP1CheckBox.TabIndex = 103;
      this.aiP1CheckBox.Text = "Enable AI";
      this.aiP1CheckBox.UseVisualStyleBackColor = true;
      this.colorP1ComboBox.AutoCompleteMode = AutoCompleteMode.Append;
      this.colorP1ComboBox.AutoCompleteSource = AutoCompleteSource.ListItems;
      this.colorP1ComboBox.FormattingEnabled = true;
      this.colorP1ComboBox.Location = new Point(65, 34);
      this.colorP1ComboBox.MaxDropDownItems = 12;
      this.colorP1ComboBox.MaxLength = 2;
      this.colorP1ComboBox.Name = "colorP1ComboBox";
      this.colorP1ComboBox.Size = new Size(45, 20);
      this.colorP1ComboBox.TabIndex = 102;
      this.colorP1ComboBox.Text = "1";
      this.label2.AutoSize = true;
      this.label2.Location = new Point(5, 37);
      this.label2.Name = "label2";
      this.label2.Size = new Size(34, 12);
      this.label2.TabIndex = 108;
      this.label2.Text = "Color:";
      this.defP1ComboBox.AllowDrop = true;
      this.defP1ComboBox.AutoCompleteMode = AutoCompleteMode.Suggest;
      this.defP1ComboBox.AutoCompleteSource = AutoCompleteSource.ListItems;
      this.defP1ComboBox.FormattingEnabled = true;
      this.defP1ComboBox.Location = new Point(65, 8);
      this.defP1ComboBox.MaxDropDownItems = 20;
      this.defP1ComboBox.MaxLength = 64;
      this.defP1ComboBox.Name = "defP1ComboBox";
      this.defP1ComboBox.Size = new Size(353, 20);
      this.defP1ComboBox.TabIndex = 101;
      this.defP1ComboBox.Text = "(No setting)";
      this.defP1ComboBox.DragDrop += new DragEventHandler(this.defP1ComboBox_DragDrop);
      this.defP1ComboBox.DragEnter += new DragEventHandler(this.defP1ComboBox_DragEnter);
      this.defP1ComboBox.KeyDown += new KeyEventHandler(this.defP1ComboBox_KeyDown);
      this.label1.AutoSize = true;
      this.label1.Location = new Point(5, 11);
      this.label1.Name = "label1";
      this.label1.Size = new Size(57, 12);
      this.label1.TabIndex = 109;
      this.label1.Text = "def file:";
      this.p3tabPage.Controls.Add((Control) this.powerP3NumericUpDown);
      this.p3tabPage.Controls.Add((Control) this.lifeP3NumericUpDown);
      this.p3tabPage.Controls.Add((Control) this.powerP3CheckBox);
      this.p3tabPage.Controls.Add((Control) this.lifeP3CheckBox);
      this.p3tabPage.Controls.Add((Control) this.aiP3CheckBox);
      this.p3tabPage.Controls.Add((Control) this.colorP3ComboBox);
      this.p3tabPage.Controls.Add((Control) this.label7);
      this.p3tabPage.Controls.Add((Control) this.defP3ComboBox);
      this.p3tabPage.Controls.Add((Control) this.label8);
      this.p3tabPage.Location = new Point(4, 22);
      this.p3tabPage.Name = "p3tabPage";
      this.p3tabPage.Padding = new Padding(3);
      this.p3tabPage.Size = new Size(425, 89);
      this.p3tabPage.TabIndex = 1;
      this.p3tabPage.Text = "player3";
      this.p3tabPage.UseVisualStyleBackColor = true;
      this.p3tabPage.DragDrop += new DragEventHandler(this.p3Well_DragDrop);
      this.p3tabPage.DragEnter += new DragEventHandler(this.p3Well_DragEnter);
      this.powerP3NumericUpDown.Enabled = false;
      this.powerP3NumericUpDown.Location = new Point(350, 62);
      this.powerP3NumericUpDown.Margin = new Padding(3, 3, 3, 0);
      this.powerP3NumericUpDown.Maximum = new Decimal(new int[4]
      {
        int.MaxValue,
        0,
        0,
        0
      });
      this.powerP3NumericUpDown.Name = "powerP3NumericUpDown";
      this.powerP3NumericUpDown.Size = new Size(68, 19);
      this.powerP3NumericUpDown.TabIndex = 116;
      this.powerP3NumericUpDown.TextAlign = HorizontalAlignment.Right;
      this.lifeP3NumericUpDown.Enabled = false;
      this.lifeP3NumericUpDown.Location = new Point(154, 62);
      this.lifeP3NumericUpDown.Margin = new Padding(3, 3, 3, 0);
      this.lifeP3NumericUpDown.Maximum = new Decimal(new int[4]
      {
        int.MaxValue,
        0,
        0,
        0
      });
      this.lifeP3NumericUpDown.Name = "lifeP3NumericUpDown";
      this.lifeP3NumericUpDown.Size = new Size(68, 19);
      this.lifeP3NumericUpDown.TabIndex = 114;
      this.lifeP3NumericUpDown.TextAlign = HorizontalAlignment.Right;
      this.lifeP3NumericUpDown.Value = new Decimal(new int[4]
      {
        1000,
        0,
        0,
        0
      });
      this.powerP3CheckBox.AutoSize = true;
      this.powerP3CheckBox.Location = new Point(258, 63);
      this.powerP3CheckBox.Name = "powerP3CheckBox";
      this.powerP3CheckBox.Size = new Size(91, 16);
      this.powerP3CheckBox.TabIndex = 115;
      this.powerP3CheckBox.Text = "Initial power:";
      this.powerP3CheckBox.UseVisualStyleBackColor = true;
      this.powerP3CheckBox.CheckedChanged += new EventHandler(this.powerP3CheckBox_CheckedChanged);
      this.lifeP3CheckBox.AutoSize = true;
      this.lifeP3CheckBox.Location = new Point(66, 63);
      this.lifeP3CheckBox.Name = "lifeP3CheckBox";
      this.lifeP3CheckBox.Size = new Size(87, 16);
      this.lifeP3CheckBox.TabIndex = 113;
      this.lifeP3CheckBox.Text = "Maximum life:";
      this.lifeP3CheckBox.UseVisualStyleBackColor = true;
      this.lifeP3CheckBox.CheckedChanged += new EventHandler(this.lifeP3CheckBox_CheckedChanged);
      this.aiP3CheckBox.AutoSize = true;
      this.aiP3CheckBox.Checked = true;
      this.aiP3CheckBox.CheckState = CheckState.Checked;
      this.aiP3CheckBox.Location = new Point(152, 36);
      this.aiP3CheckBox.Name = "aiP3CheckBox";
      this.aiP3CheckBox.Size = new Size(90, 16);
      this.aiP3CheckBox.TabIndex = 112;
      this.aiP3CheckBox.Text = "Enable AI";
      this.aiP3CheckBox.UseVisualStyleBackColor = true;
      this.colorP3ComboBox.AutoCompleteMode = AutoCompleteMode.Append;
      this.colorP3ComboBox.AutoCompleteSource = AutoCompleteSource.ListItems;
      this.colorP3ComboBox.FormattingEnabled = true;
      this.colorP3ComboBox.Location = new Point(65, 34);
      this.colorP3ComboBox.MaxDropDownItems = 12;
      this.colorP3ComboBox.MaxLength = 2;
      this.colorP3ComboBox.Name = "colorP3ComboBox";
      this.colorP3ComboBox.Size = new Size(45, 20);
      this.colorP3ComboBox.TabIndex = 111;
      this.colorP3ComboBox.Text = "1";
      this.label7.AutoSize = true;
      this.label7.Location = new Point(5, 37);
      this.label7.Name = "label7";
      this.label7.Size = new Size(34, 12);
      this.label7.TabIndex = 117;
      this.label7.Text = "Color:";
      this.defP3ComboBox.AllowDrop = true;
      this.defP3ComboBox.AutoCompleteMode = AutoCompleteMode.Suggest;
      this.defP3ComboBox.AutoCompleteSource = AutoCompleteSource.ListItems;
      this.defP3ComboBox.FormattingEnabled = true;
      this.defP3ComboBox.Location = new Point(65, 8);
      this.defP3ComboBox.MaxDropDownItems = 20;
      this.defP3ComboBox.MaxLength = 64;
      this.defP3ComboBox.Name = "defP3ComboBox";
      this.defP3ComboBox.Size = new Size(353, 20);
      this.defP3ComboBox.TabIndex = 110;
      this.defP3ComboBox.Text = "(No setting)";
      this.defP3ComboBox.DragDrop += new DragEventHandler(this.defP3ComboBox_DragDrop);
      this.defP3ComboBox.DragEnter += new DragEventHandler(this.defP3ComboBox_DragEnter);
      this.defP3ComboBox.KeyDown += new KeyEventHandler(this.defP3ComboBox_KeyDown);
      this.label8.AutoSize = true;
      this.label8.Location = new Point(5, 11);
      this.label8.Name = "label8";
      this.label8.Size = new Size(57, 12);
      this.label8.TabIndex = 118;
      this.label8.Text = "def file:";
      this.textBox3.BorderStyle = BorderStyle.None;
      this.textBox3.Cursor = Cursors.Default;
      this.textBox3.Location = new Point(35, 9);
      this.textBox3.Multiline = true;
      this.textBox3.Name = "textBox3";
      this.textBox3.ReadOnly = true;
      this.textBox3.Size = new Size(310, 60);
      this.textBox3.TabIndex = 100;
      this.textBox3.TabStop = false;
      this.textBox3.Text = "Select characters by specifying the name of their def files. \r\n\r\nYou can add characters by typing their names or by dragging and dropping the def files in this window.";
      this.folderButton.Location = new Point(362, 13);
      this.folderButton.Name = "folderButton";
      this.folderButton.Size = new Size(90, 49);
      this.folderButton.TabIndex = 9;
      this.folderButton.Text = "Open chars folder in explorer";
      this.folderButton.UseVisualStyleBackColor = true;
      this.folderButton.Click += new EventHandler(this.folderButton_Click);
      this.groupBox2.Controls.Add((Control) this.p2tabControl);
      this.groupBox2.Location = new Point(12, 227);
      this.groupBox2.Name = "groupBox2";
      this.groupBox2.Size = new Size(440, 139);
      this.groupBox2.TabIndex = 20;
      this.groupBox2.TabStop = false;
      this.groupBox2.Text = "Player 2 side";
      this.p2tabControl.AllowDrop = true;
      this.p2tabControl.Controls.Add((Control) this.p2tabPage);
      this.p2tabControl.Controls.Add((Control) this.p4tabPage);
      this.p2tabControl.Location = new Point(6, 18);
      this.p2tabControl.Name = "p2tabControl";
      this.p2tabControl.SelectedIndex = 0;
      this.p2tabControl.Size = new Size(433, 115);
      this.p2tabControl.TabIndex = 0;
      this.p2tabControl.SelectedIndexChanged += new EventHandler(this.p2tabControl_SelectedIndexChanged);
      this.p2tabPage.AllowDrop = true;
      this.p2tabPage.Controls.Add((Control) this.powerP2NumericUpDown);
      this.p2tabPage.Controls.Add((Control) this.lifeP2NumericUpDown);
      this.p2tabPage.Controls.Add((Control) this.powerP2CheckBox);
      this.p2tabPage.Controls.Add((Control) this.lifeP2CheckBox);
      this.p2tabPage.Controls.Add((Control) this.aiP2CheckBox);
      this.p2tabPage.Controls.Add((Control) this.colorP2ComboBox);
      this.p2tabPage.Controls.Add((Control) this.label3);
      this.p2tabPage.Controls.Add((Control) this.defP2ComboBox);
      this.p2tabPage.Controls.Add((Control) this.label4);
      this.p2tabPage.Location = new Point(4, 22);
      this.p2tabPage.Name = "p2tabPage";
      this.p2tabPage.Padding = new Padding(3);
      this.p2tabPage.Size = new Size(425, 89);
      this.p2tabPage.TabIndex = 0;
      this.p2tabPage.Text = "player2";
      this.p2tabPage.UseVisualStyleBackColor = true;
      this.p2tabPage.DragDrop += new DragEventHandler(this.p2Well_DragDrop);
      this.p2tabPage.DragEnter += new DragEventHandler(this.p2Well_DragEnter);
      this.powerP2NumericUpDown.Enabled = false;
      this.powerP2NumericUpDown.Location = new Point(350, 62);
      this.powerP2NumericUpDown.Margin = new Padding(3, 3, 3, 0);
      this.powerP2NumericUpDown.Maximum = new Decimal(new int[4]
      {
        int.MaxValue,
        0,
        0,
        0
      });
      this.powerP2NumericUpDown.Name = "powerP2NumericUpDown";
      this.powerP2NumericUpDown.Size = new Size(68, 19);
      this.powerP2NumericUpDown.TabIndex = 109;
      this.powerP2NumericUpDown.TextAlign = HorizontalAlignment.Right;
      this.lifeP2NumericUpDown.Enabled = false;
      this.lifeP2NumericUpDown.Location = new Point(154, 62);
      this.lifeP2NumericUpDown.Margin = new Padding(3, 3, 3, 0);
      this.lifeP2NumericUpDown.Maximum = new Decimal(new int[4]
      {
        int.MaxValue,
        0,
        0,
        0
      });
      this.lifeP2NumericUpDown.Name = "lifeP2NumericUpDown";
      this.lifeP2NumericUpDown.Size = new Size(68, 19);
      this.lifeP2NumericUpDown.TabIndex = 107;
      this.lifeP2NumericUpDown.TextAlign = HorizontalAlignment.Right;
      this.lifeP2NumericUpDown.Value = new Decimal(new int[4]
      {
        1000,
        0,
        0,
        0
      });
      this.powerP2CheckBox.AutoSize = true;
      this.powerP2CheckBox.Location = new Point(258, 63);
      this.powerP2CheckBox.Name = "powerP2CheckBox";
      this.powerP2CheckBox.Size = new Size(91, 16);
      this.powerP2CheckBox.TabIndex = 108;
      this.powerP2CheckBox.Text = "Initial power:";
      this.powerP2CheckBox.UseVisualStyleBackColor = true;
      this.powerP2CheckBox.CheckedChanged += new EventHandler(this.powerP2CheckBox_CheckedChanged);
      this.lifeP2CheckBox.AutoSize = true;
      this.lifeP2CheckBox.Location = new Point(66, 63);
      this.lifeP2CheckBox.Name = "lifeP2CheckBox";
      this.lifeP2CheckBox.Size = new Size(87, 16);
      this.lifeP2CheckBox.TabIndex = 106;
      this.lifeP2CheckBox.Text = "Maximum life:";
      this.lifeP2CheckBox.UseVisualStyleBackColor = true;
      this.lifeP2CheckBox.CheckedChanged += new EventHandler(this.lifeP2CheckBox_CheckedChanged);
      this.aiP2CheckBox.AutoSize = true;
      this.aiP2CheckBox.Checked = true;
      this.aiP2CheckBox.CheckState = CheckState.Checked;
      this.aiP2CheckBox.Location = new Point(152, 36);
      this.aiP2CheckBox.Name = "aiP2CheckBox";
      this.aiP2CheckBox.Size = new Size(90, 16);
      this.aiP2CheckBox.TabIndex = 105;
      this.aiP2CheckBox.Text = "Enable AI";
      this.aiP2CheckBox.UseVisualStyleBackColor = true;
      this.colorP2ComboBox.AutoCompleteMode = AutoCompleteMode.Append;
      this.colorP2ComboBox.AutoCompleteSource = AutoCompleteSource.ListItems;
      this.colorP2ComboBox.FormattingEnabled = true;
      this.colorP2ComboBox.Location = new Point(65, 34);
      this.colorP2ComboBox.MaxDropDownItems = 12;
      this.colorP2ComboBox.MaxLength = 2;
      this.colorP2ComboBox.Name = "colorP2ComboBox";
      this.colorP2ComboBox.Size = new Size(45, 20);
      this.colorP2ComboBox.TabIndex = 104;
      this.colorP2ComboBox.Text = "1";
      this.label3.AutoSize = true;
      this.label3.Location = new Point(5, 37);
      this.label3.Name = "label3";
      this.label3.Size = new Size(34, 12);
      this.label3.TabIndex = 110;
      this.label3.Text = "Color:";
      this.defP2ComboBox.AllowDrop = true;
      this.defP2ComboBox.AutoCompleteMode = AutoCompleteMode.Suggest;
      this.defP2ComboBox.AutoCompleteSource = AutoCompleteSource.ListItems;
      this.defP2ComboBox.FormattingEnabled = true;
      this.defP2ComboBox.Location = new Point(65, 8);
      this.defP2ComboBox.MaxDropDownItems = 20;
      this.defP2ComboBox.MaxLength = 64;
      this.defP2ComboBox.Name = "defP2ComboBox";
      this.defP2ComboBox.Size = new Size(353, 20);
      this.defP2ComboBox.TabIndex = 103;
      this.defP2ComboBox.Text = "(No setting)";
      this.defP2ComboBox.DragDrop += new DragEventHandler(this.defP2ComboBox_DragDrop);
      this.defP2ComboBox.DragEnter += new DragEventHandler(this.defP2ComboBox_DragEnter);
      this.defP2ComboBox.KeyDown += new KeyEventHandler(this.defP2ComboBox_KeyDown);
      this.label4.AutoSize = true;
      this.label4.Location = new Point(5, 11);
      this.label4.Name = "label4";
      this.label4.Size = new Size(57, 12);
      this.label4.TabIndex = 111;
      this.label4.Text = "def file:";
      this.p4tabPage.Controls.Add((Control) this.powerP4NumericUpDown);
      this.p4tabPage.Controls.Add((Control) this.lifeP4NumericUpDown);
      this.p4tabPage.Controls.Add((Control) this.powerP4CheckBox);
      this.p4tabPage.Controls.Add((Control) this.lifeP4CheckBox);
      this.p4tabPage.Controls.Add((Control) this.aiP4CheckBox);
      this.p4tabPage.Controls.Add((Control) this.colorP4ComboBox);
      this.p4tabPage.Controls.Add((Control) this.label9);
      this.p4tabPage.Controls.Add((Control) this.defP4ComboBox);
      this.p4tabPage.Controls.Add((Control) this.label10);
      this.p4tabPage.Location = new Point(4, 22);
      this.p4tabPage.Name = "p4tabPage";
      this.p4tabPage.Padding = new Padding(3);
      this.p4tabPage.Size = new Size(425, 89);
      this.p4tabPage.TabIndex = 1;
      this.p4tabPage.Text = "player4";
      this.p4tabPage.UseVisualStyleBackColor = true;
      this.p4tabPage.DragDrop += new DragEventHandler(this.p4Well_DragDrop);
      this.p4tabPage.DragEnter += new DragEventHandler(this.p4Well_DragEnter);
      this.powerP4NumericUpDown.Enabled = false;
      this.powerP4NumericUpDown.Location = new Point(350, 62);
      this.powerP4NumericUpDown.Margin = new Padding(3, 3, 3, 0);
      this.powerP4NumericUpDown.Maximum = new Decimal(new int[4]
      {
        int.MaxValue,
        0,
        0,
        0
      });
      this.powerP4NumericUpDown.Name = "powerP4NumericUpDown";
      this.powerP4NumericUpDown.Size = new Size(68, 19);
      this.powerP4NumericUpDown.TabIndex = 118;
      this.powerP4NumericUpDown.TextAlign = HorizontalAlignment.Right;
      this.lifeP4NumericUpDown.Enabled = false;
      this.lifeP4NumericUpDown.Location = new Point(154, 62);
      this.lifeP4NumericUpDown.Margin = new Padding(3, 3, 3, 0);
      this.lifeP4NumericUpDown.Maximum = new Decimal(new int[4]
      {
        int.MaxValue,
        0,
        0,
        0
      });
      this.lifeP4NumericUpDown.Name = "lifeP4NumericUpDown";
      this.lifeP4NumericUpDown.Size = new Size(68, 19);
      this.lifeP4NumericUpDown.TabIndex = 116;
      this.lifeP4NumericUpDown.TextAlign = HorizontalAlignment.Right;
      this.lifeP4NumericUpDown.Value = new Decimal(new int[4]
      {
        1000,
        0,
        0,
        0
      });
      this.powerP4CheckBox.AutoSize = true;
      this.powerP4CheckBox.Location = new Point(258, 63);
      this.powerP4CheckBox.Name = "powerP4CheckBox";
      this.powerP4CheckBox.Size = new Size(91, 16);
      this.powerP4CheckBox.TabIndex = 117;
      this.powerP4CheckBox.Text = "Initial power:";
      this.powerP4CheckBox.UseVisualStyleBackColor = true;
      this.powerP4CheckBox.CheckedChanged += new EventHandler(this.powerP4CheckBox_CheckedChanged);
      this.lifeP4CheckBox.AutoSize = true;
      this.lifeP4CheckBox.Location = new Point(66, 63);
      this.lifeP4CheckBox.Name = "lifeP4CheckBox";
      this.lifeP4CheckBox.Size = new Size(87, 16);
      this.lifeP4CheckBox.TabIndex = 115;
      this.lifeP4CheckBox.Text = "Maximum life:";
      this.lifeP4CheckBox.UseVisualStyleBackColor = true;
      this.lifeP4CheckBox.CheckedChanged += new EventHandler(this.lifeP4CheckBox_CheckedChanged);
      this.aiP4CheckBox.AutoSize = true;
      this.aiP4CheckBox.Checked = true;
      this.aiP4CheckBox.CheckState = CheckState.Checked;
      this.aiP4CheckBox.Location = new Point(152, 36);
      this.aiP4CheckBox.Name = "aiP4CheckBox";
      this.aiP4CheckBox.Size = new Size(90, 16);
      this.aiP4CheckBox.TabIndex = 114;
      this.aiP4CheckBox.Text = "Enable AI";
      this.aiP4CheckBox.UseVisualStyleBackColor = true;
      this.colorP4ComboBox.AutoCompleteMode = AutoCompleteMode.Append;
      this.colorP4ComboBox.AutoCompleteSource = AutoCompleteSource.ListItems;
      this.colorP4ComboBox.FormattingEnabled = true;
      this.colorP4ComboBox.Location = new Point(65, 34);
      this.colorP4ComboBox.MaxDropDownItems = 12;
      this.colorP4ComboBox.MaxLength = 2;
      this.colorP4ComboBox.Name = "colorP4ComboBox";
      this.colorP4ComboBox.Size = new Size(45, 20);
      this.colorP4ComboBox.TabIndex = 113;
      this.colorP4ComboBox.Text = "1";
      this.label9.AutoSize = true;
      this.label9.Location = new Point(5, 37);
      this.label9.Name = "label9";
      this.label9.Size = new Size(34, 12);
      this.label9.TabIndex = 119;
      this.label9.Text = "Color:";
      this.defP4ComboBox.AllowDrop = true;
      this.defP4ComboBox.AutoCompleteMode = AutoCompleteMode.Suggest;
      this.defP4ComboBox.AutoCompleteSource = AutoCompleteSource.ListItems;
      this.defP4ComboBox.FormattingEnabled = true;
      this.defP4ComboBox.Location = new Point(65, 8);
      this.defP4ComboBox.MaxDropDownItems = 20;
      this.defP4ComboBox.MaxLength = 64;
      this.defP4ComboBox.Name = "defP4ComboBox";
      this.defP4ComboBox.Size = new Size(353, 20);
      this.defP4ComboBox.TabIndex = 112;
      this.defP4ComboBox.Text = "(No setting)";
      this.defP4ComboBox.DragDrop += new DragEventHandler(this.defP4ComboBox_DragDrop);
      this.defP4ComboBox.DragEnter += new DragEventHandler(this.defP4ComboBox_DragEnter);
      this.defP4ComboBox.KeyDown += new KeyEventHandler(this.defP4ComboBox_KeyDown);
      this.label10.AutoSize = true;
      this.label10.Location = new Point(5, 11);
      this.label10.Name = "label10";
      this.label10.Size = new Size(57, 12);
      this.label10.TabIndex = 120;
      this.label10.Text = "def file:";
      this.label5.AutoSize = true;
      this.label5.Location = new Point(18, 445);
      this.label5.Name = "label5";
      this.label5.Size = new Size(54, 12);
      this.label5.TabIndex = 100;
      this.label5.Text = "Rounds:";
      this.roundComboBox.AutoCompleteMode = AutoCompleteMode.Append;
      this.roundComboBox.AutoCompleteSource = AutoCompleteSource.ListItems;
      this.roundComboBox.FormattingEnabled = true;
      this.roundComboBox.Location = new Point(78, 442);
      this.roundComboBox.MaxDropDownItems = 5;
      this.roundComboBox.MaxLength = 2;
      this.roundComboBox.Name = "roundComboBox";
      this.roundComboBox.Size = new Size(45, 20);
      this.roundComboBox.TabIndex = 50;
      this.roundComboBox.Text = "2";
      this.roundComboBox.SelectedIndexChanged += new EventHandler(this.roundComboBox_SelectedIndexChanged);
      this.swapButton.Location = new Point(138, 442);
      this.swapButton.Name = "swapButton";
      this.swapButton.Size = new Size(90, 28);
      this.swapButton.TabIndex = 51;
      this.swapButton.Text = "Swap players";
      this.swapButton.UseVisualStyleBackColor = true;
      this.swapButton.Click += new EventHandler(this.swapButton_Click);
      this.noMusicCheckBox.AutoSize = true;
      this.noMusicCheckBox.Location = new Point(32, 407);
      this.noMusicCheckBox.Name = "noMusicCheckBox";
      this.noMusicCheckBox.Size = new Size(73, 16);
      this.noMusicCheckBox.TabIndex = 41;
      this.noMusicCheckBox.Text = "No BGM";
      this.noMusicCheckBox.UseVisualStyleBackColor = true;
      this.noSoundCheckBox.AutoSize = true;
      this.noSoundCheckBox.Location = new Point(111, 407);
      this.noSoundCheckBox.Name = "noSoundCheckBox";
      this.noSoundCheckBox.Size = new Size(85, 16);
      this.noSoundCheckBox.TabIndex = 42;
      this.noSoundCheckBox.Text = "No sound";
      this.noSoundCheckBox.UseVisualStyleBackColor = true;
      this.label6.AutoSize = true;
      this.label6.Location = new Point(30, 383);
      this.label6.Name = "label6";
      this.label6.Size = new Size(45, 12);
      this.label6.TabIndex = 103;
      this.label6.Text = "Stage:";
      this.stageComboBox.AllowDrop = true;
      this.stageComboBox.AutoCompleteMode = AutoCompleteMode.Suggest;
      this.stageComboBox.AutoCompleteSource = AutoCompleteSource.ListItems;
      this.stageComboBox.FormattingEnabled = true;
      this.stageComboBox.Location = new Point(86, 380);
      this.stageComboBox.MaxLength = 64;
      this.stageComboBox.Name = "stageComboBox";
      this.stageComboBox.Size = new Size(356, 20);
      this.stageComboBox.TabIndex = 40;
      this.stageComboBox.Text = "(No setting)";
      this.AutoScaleDimensions = new SizeF(6f, 12f);
      this.AutoScaleMode = AutoScaleMode.Font;
      this.ClientSize = new Size(464, 480);
      this.Controls.Add((Control) this.stageComboBox);
      this.Controls.Add((Control) this.label6);
      this.Controls.Add((Control) this.noSoundCheckBox);
      this.Controls.Add((Control) this.noMusicCheckBox);
      this.Controls.Add((Control) this.swapButton);
      this.Controls.Add((Control) this.roundComboBox);
      this.Controls.Add((Control) this.label5);
      this.Controls.Add((Control) this.groupBox2);
      this.Controls.Add((Control) this.folderButton);
      this.Controls.Add((Control) this.textBox3);
      this.Controls.Add((Control) this.groupBox1);
      this.Controls.Add((Control) this.cancelButton);
      this.Controls.Add((Control) this.okButton);
      this.FormBorderStyle = FormBorderStyle.FixedDialog;
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = nameof (QuickVSModePanel);
      this.ShowIcon = false;
      this.ShowInTaskbar = false;
      this.StartPosition = FormStartPosition.Manual;
      this.Text = "Quick Vs. Mode";
      this.Load += new EventHandler(this.QuickVSModePanel_Load);
      this.groupBox1.ResumeLayout(false);
      this.p1tabControl.ResumeLayout(false);
      this.p1tabPage.ResumeLayout(false);
      this.p1tabPage.PerformLayout();
      this.powerP1NumericUpDown.EndInit();
      this.lifeP1NumericUpDown.EndInit();
      this.p3tabPage.ResumeLayout(false);
      this.p3tabPage.PerformLayout();
      this.powerP3NumericUpDown.EndInit();
      this.lifeP3NumericUpDown.EndInit();
      this.groupBox2.ResumeLayout(false);
      this.p2tabControl.ResumeLayout(false);
      this.p2tabPage.ResumeLayout(false);
      this.p2tabPage.PerformLayout();
      this.powerP2NumericUpDown.EndInit();
      this.lifeP2NumericUpDown.EndInit();
      this.p4tabPage.ResumeLayout(false);
      this.p4tabPage.PerformLayout();
      this.powerP4NumericUpDown.EndInit();
      this.lifeP4NumericUpDown.EndInit();
      this.ResumeLayout(false);
      this.PerformLayout();
    }
  }
}
