// Decompiled with JetBrains decompiler
// Type: SwissArmyKnifeForMugen.MainForm
// Assembly: SAKnifeWM, Version=1.0.8.0, Culture=neutral, PublicKeyToken=null
// MVID: 09478AD8-365C-4BF3-BEA1-B5785151259B
// Assembly location: C:\Users\ziddi\Downloads\Swiss Army Knife 1.1 Conversion\Swiss Army Knife 1.1 Conversion\SAKnifeWM.exe

using SwissArmyKnifeForMugen.Configs;
using SwissArmyKnifeForMugen.Displays;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Media;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace SwissArmyKnifeForMugen
{
    public class MainForm : Form
  {
    private IContainer components;
    private MenuStrip fileMenuStrip;
    private ToolStripMenuItem ファイルFToolStripMenuItem;
    private ToolStripMenuItem configToolStripMenuItem;
    private ToolStripSeparator toolStripSeparator2;
    private ToolStripMenuItem exitAllToolStripMenuItem;
    private ToolStripMenuItem ツールTToolStripMenuItem;
    private ToolStripMenuItem logWinToolStripMenuItem;
    private ToolStripMenuItem debugWinToolStripMenuItem;
    private ToolStripMenuItem ヘルプHToolStripMenuItem;
    private ToolStripMenuItem versionAToolStripMenuItem;
    private ListBox profileListBox;
    private ContextMenuStrip profileContextMenuStrip;
    private ToolStripMenuItem launchToolStripMenuItem;
    private ToolStripSeparator toolStripSeparator3;
    private ToolStripMenuItem openSelectStripMenuItem;
    private ToolStripMenuItem openMugenStripMenuItem;
    private ToolStripMenuItem openCharStripMenuItem;
    private ToolStripSeparator toolStripSeparator4;
    private ToolStripMenuItem openProfileStripMenuItem;
    private ToolStripSeparator toolStripSeparator5;
    private ToolStripMenuItem quitToolStripMenuItem;
    private ToolStripTextBox toolStripTextBox1;
    private ToolStripSeparator toolStripSeparator6;
    private ToolStripMenuItem varWinStripMenuItem;
    private Timer autoModeTimer;
    private ToolStripMenuItem restartToolStripMenuItem;
    private ToolStripMenuItem restart2ToolStripMenuItem;
    private Button quitButton;
    private Button startButton;
    private ToolStripMenuItem deleteProfileToolStripMenuItem;
    private ToolStripMenuItem arrangeWinStripMenuItem;
    private ToolStripMenuItem helpToolStripMenuItem;
    private ToolTip toolTip1;
    private ToolStripMenuItem mugenWinToolStripMenuItem;
    private ToolStripSeparator toolStripSeparator1;
    private ToolStripMenuItem readmeToolStripMenuItem;
    private ToolStripMenuItem activeteStripMenuItem;
    private ToolStripMenuItem helpBtnStripMenuItem;
    private ToolStripSeparator toolStripSeparator7;
    private ToolStripMenuItem copyProfileStripMenuItem;
    private ToolStripMenuItem pasteProfileStripMenuItem;
    private ToolStripSeparator toolStripSeparator8;
    private ToolStripMenuItem launchAutoModeToolStripMenuItem;
    private ToolStripSeparator toolStripSeparator9;
    private ToolStripMenuItem launchQuickVSToolStripMenuItem;
    private Timer watchTimer;
    private Timer delayTimer;
    private const uint ES_SYSTEM_REQUIRED = 1;
    private const uint ES_DISPLAY_REQUIRED = 2;
    private const short SWP_NOMOVE = 2;
    private const short SWP_NOSIZE = 1;
    private const short SWP_NOZORDER = 4;
    private const int SWP_SHOWWINDOW = 64;
    private const int SWP_HIDEWINDOW = 128;
    private const int WM_SYSCOMMAND = 274;
    private const int SC_CONTEXTHELP = 61824;
    private static MainForm selfObj;
    private bool _isClosing;
    private bool _didMugenCrashed;
    private int _selectedProfileNo;
    private bool _isAutoModeRunning;
    private bool _didAutoModeQuitted;
    private int _retryCount;
    private bool _isRetrying;
    private bool _disableQuitButton;
    private bool _isMenuVisible;
    private static string _exeFolder;
    private bool _needActievateAll;
    private bool _wasBtnDown;
    private int _autoModeRetryCount;
    private int _copyTargetProfileNo;
    private bool _isHelpMode;

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      this.components = (IContainer) new Container();
      ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (MainForm));
      this.watchTimer = new Timer(this.components);
      this.fileMenuStrip = new MenuStrip();
      this.ファイルFToolStripMenuItem = new ToolStripMenuItem();
      this.configToolStripMenuItem = new ToolStripMenuItem();
      this.toolStripSeparator2 = new ToolStripSeparator();
      this.exitAllToolStripMenuItem = new ToolStripMenuItem();
      this.ツールTToolStripMenuItem = new ToolStripMenuItem();
      this.activeteStripMenuItem = new ToolStripMenuItem();
      this.arrangeWinStripMenuItem = new ToolStripMenuItem();
      this.toolStripSeparator1 = new ToolStripSeparator();
      this.mugenWinToolStripMenuItem = new ToolStripMenuItem();
      this.logWinToolStripMenuItem = new ToolStripMenuItem();
      this.debugWinToolStripMenuItem = new ToolStripMenuItem();
      this.varWinStripMenuItem = new ToolStripMenuItem();
      this.ヘルプHToolStripMenuItem = new ToolStripMenuItem();
      this.helpToolStripMenuItem = new ToolStripMenuItem();
      this.readmeToolStripMenuItem = new ToolStripMenuItem();
      this.versionAToolStripMenuItem = new ToolStripMenuItem();
      this.helpBtnStripMenuItem = new ToolStripMenuItem();
      this.profileListBox = new ListBox();
      this.profileContextMenuStrip = new ContextMenuStrip(this.components);
      this.toolStripTextBox1 = new ToolStripTextBox();
      this.toolStripSeparator6 = new ToolStripSeparator();
      this.launchToolStripMenuItem = new ToolStripMenuItem();
      this.toolStripSeparator9 = new ToolStripSeparator();
      this.launchQuickVSToolStripMenuItem = new ToolStripMenuItem();
      this.toolStripSeparator8 = new ToolStripSeparator();
      this.launchAutoModeToolStripMenuItem = new ToolStripMenuItem();
      this.restartToolStripMenuItem = new ToolStripMenuItem();
      this.restart2ToolStripMenuItem = new ToolStripMenuItem();
      this.toolStripSeparator3 = new ToolStripSeparator();
      this.openSelectStripMenuItem = new ToolStripMenuItem();
      this.openMugenStripMenuItem = new ToolStripMenuItem();
      this.openCharStripMenuItem = new ToolStripMenuItem();
      this.toolStripSeparator4 = new ToolStripSeparator();
      this.openProfileStripMenuItem = new ToolStripMenuItem();
      this.copyProfileStripMenuItem = new ToolStripMenuItem();
      this.pasteProfileStripMenuItem = new ToolStripMenuItem();
      this.toolStripSeparator7 = new ToolStripSeparator();
      this.deleteProfileToolStripMenuItem = new ToolStripMenuItem();
      this.toolStripSeparator5 = new ToolStripSeparator();
      this.quitToolStripMenuItem = new ToolStripMenuItem();
      this.autoModeTimer = new Timer(this.components);
      this.quitButton = new Button();
      this.startButton = new Button();
      this.toolTip1 = new ToolTip(this.components);
      this.delayTimer = new Timer(this.components);
      this.fileMenuStrip.SuspendLayout();
      this.profileContextMenuStrip.SuspendLayout();
      this.SuspendLayout();
      this.watchTimer.Interval = 500;
      this.watchTimer.Tick += new EventHandler(this.watchTimer_Tick);
      this.fileMenuStrip.ImageScalingSize = new Size(24, 24);
      this.fileMenuStrip.Items.AddRange(new ToolStripItem[4]
      {
        (ToolStripItem) this.ファイルFToolStripMenuItem,
        (ToolStripItem) this.ツールTToolStripMenuItem,
        (ToolStripItem) this.ヘルプHToolStripMenuItem,
        (ToolStripItem) this.helpBtnStripMenuItem
      });
      this.fileMenuStrip.Location = new Point(3, 3);
      this.fileMenuStrip.Name = "fileMenuStrip";
      this.fileMenuStrip.Padding = new Padding(6, 0, 0, 2);
      this.fileMenuStrip.Size = new Size(277, 24);
      this.fileMenuStrip.TabIndex = 3;
      this.fileMenuStrip.Text = "File";
      this.ファイルFToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[3]
      {
        (ToolStripItem) this.configToolStripMenuItem,
        (ToolStripItem) this.toolStripSeparator2,
        (ToolStripItem) this.exitAllToolStripMenuItem
      });
      this.ファイルFToolStripMenuItem.Name = "ファイルFToolStripMenuItem";
      this.ファイルFToolStripMenuItem.Size = new Size(85, 22);
      this.ファイルFToolStripMenuItem.Text = "File";
      this.ファイルFToolStripMenuItem.MouseUp += new MouseEventHandler(this.ファイルFToolStripMenuItem_MouseUp);
      this.configToolStripMenuItem.DisplayStyle = ToolStripItemDisplayStyle.Text;
      this.configToolStripMenuItem.Image = (Image) componentResourceManager.GetObject("configToolStripMenuItem.Image");
      this.configToolStripMenuItem.ImageTransparentColor = Color.Magenta;
      this.configToolStripMenuItem.Name = "configToolStripMenuItem";
      this.configToolStripMenuItem.Size = new Size(148, 22);
      this.configToolStripMenuItem.Text = "Main settings...";
      this.configToolStripMenuItem.Click += new EventHandler(this.configToolStripMenuItem_Click);
      this.toolStripSeparator2.Name = "toolStripSeparator2";
      this.toolStripSeparator2.Size = new Size(145, 6);
      this.exitAllToolStripMenuItem.DisplayStyle = ToolStripItemDisplayStyle.Text;
      this.exitAllToolStripMenuItem.Name = "exitAllToolStripMenuItem";
      this.exitAllToolStripMenuItem.Size = new Size(148, 22);
      this.exitAllToolStripMenuItem.Text = "Quit";
      this.exitAllToolStripMenuItem.Click += new EventHandler(this.exitToolStripMenuItem_Click);
      this.ツールTToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[7]
      {
        (ToolStripItem) this.activeteStripMenuItem,
        (ToolStripItem) this.arrangeWinStripMenuItem,
        (ToolStripItem) this.toolStripSeparator1,
        (ToolStripItem) this.mugenWinToolStripMenuItem,
        (ToolStripItem) this.logWinToolStripMenuItem,
        (ToolStripItem) this.debugWinToolStripMenuItem,
        (ToolStripItem) this.varWinStripMenuItem
      });
      this.ツールTToolStripMenuItem.Name = "ツールTToolStripMenuItem";
      this.ツールTToolStripMenuItem.Size = new Size(80, 22);
      this.ツールTToolStripMenuItem.Text = "Window";
      this.ツールTToolStripMenuItem.DropDownOpening += new EventHandler(this.ツールTToolStripMenuItem_DropDownOpening);
      this.ツールTToolStripMenuItem.MouseUp += new MouseEventHandler(this.ツールTToolStripMenuItem_MouseUp);
      this.activeteStripMenuItem.Name = "activeteStripMenuItem";
      this.activeteStripMenuItem.Size = new Size(208, 22);
      this.activeteStripMenuItem.Text = "Bring other windows to the front";
      this.activeteStripMenuItem.Click += new EventHandler(this.activeteStripMenuItem_Click);
      this.arrangeWinStripMenuItem.Name = "arrangeWinStripMenuItem";
      this.arrangeWinStripMenuItem.Size = new Size(208, 22);
      this.arrangeWinStripMenuItem.Text = "Reset window positions";
      this.arrangeWinStripMenuItem.Click += new EventHandler(this.arrangeWinStripMenuItem_Click);
      this.toolStripSeparator1.Name = "toolStripSeparator1";
      this.toolStripSeparator1.Size = new Size(205, 6);
      this.mugenWinToolStripMenuItem.Name = "mugenWinToolStripMenuItem";
      this.mugenWinToolStripMenuItem.Size = new Size(208, 22);
      this.mugenWinToolStripMenuItem.Text = "M.U.G.E.N window";
      this.mugenWinToolStripMenuItem.Click += new EventHandler(this.mugenWinToolStripMenuItem_Click);
      this.logWinToolStripMenuItem.DisplayStyle = ToolStripItemDisplayStyle.Text;
      this.logWinToolStripMenuItem.Name = "logWinToolStripMenuItem";
      this.logWinToolStripMenuItem.Size = new Size(208, 22);
      this.logWinToolStripMenuItem.Text = "Battle log window";
      this.logWinToolStripMenuItem.Click += new EventHandler(this.logWinToolStripMenuItem_Click);
      this.debugWinToolStripMenuItem.DisplayStyle = ToolStripItemDisplayStyle.Text;
      this.debugWinToolStripMenuItem.Name = "debugWinToolStripMenuItem";
      this.debugWinToolStripMenuItem.Size = new Size(208, 22);
      this.debugWinToolStripMenuItem.Text = "Debug Tools window";
      this.debugWinToolStripMenuItem.Click += new EventHandler(this.debugWinToolStripMenuItem_Click);
      this.varWinStripMenuItem.Name = "varWinStripMenuItem";
      this.varWinStripMenuItem.Size = new Size(208, 22);
      this.varWinStripMenuItem.Text = "Variable View window";
      this.varWinStripMenuItem.Click += new EventHandler(this.varWinStripMenuItem_Click);
      this.ヘルプHToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[3]
      {
        (ToolStripItem) this.helpToolStripMenuItem,
        (ToolStripItem) this.readmeToolStripMenuItem,
        (ToolStripItem) this.versionAToolStripMenuItem
      });
      this.ヘルプHToolStripMenuItem.Name = "ヘルプHToolStripMenuItem";
      this.ヘルプHToolStripMenuItem.Size = new Size(56, 22);
      this.ヘルプHToolStripMenuItem.Text = "Help";
      this.ヘルプHToolStripMenuItem.TextAlign = ContentAlignment.MiddleRight;
      this.ヘルプHToolStripMenuItem.MouseUp += new MouseEventHandler(this.ヘルプHToolStripMenuItem_MouseUp);
      this.helpToolStripMenuItem.CheckOnClick = true;
      this.helpToolStripMenuItem.ImageScaling = ToolStripItemImageScaling.None;
      this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
      this.helpToolStripMenuItem.ShortcutKeyDisplayString = "";
      this.helpToolStripMenuItem.Size = new Size(227, 22);
      this.helpToolStripMenuItem.Text = "Swiss Army Knife help";
      this.helpToolStripMenuItem.Click += new EventHandler(this.toolStripMenuItem1_Click);
      this.readmeToolStripMenuItem.Name = "readmeToolStripMenuItem";
      this.readmeToolStripMenuItem.Size = new Size(227, 22);
      this.readmeToolStripMenuItem.Text = "Open the \"Operations.txt\" file";
      this.readmeToolStripMenuItem.Click += new EventHandler(this.readmeToolStripMenuItem_Click);
      this.versionAToolStripMenuItem.Name = "versionAToolStripMenuItem";
      this.versionAToolStripMenuItem.Size = new Size(227, 22);
      this.versionAToolStripMenuItem.Text = "About Swiss Army Knife...";
      this.versionAToolStripMenuItem.Click += new EventHandler(this.versionAToolStripMenuItem_Click);
      this.helpBtnStripMenuItem.Alignment = ToolStripItemAlignment.Right;
      this.helpBtnStripMenuItem.DisplayStyle = ToolStripItemDisplayStyle.Image;
      this.helpBtnStripMenuItem.Name = "helpBtnStripMenuItem";
      this.helpBtnStripMenuItem.Size = new Size(12, 22);
      this.helpBtnStripMenuItem.Text = "toolStripMenuItem1";
      this.helpBtnStripMenuItem.Click += new EventHandler(this.helpBtnStripMenuItem_Click);
      this.helpBtnStripMenuItem.MouseHover += new EventHandler(this.helpBtnStripMenuItem_MouseHover);
      this.profileListBox.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      this.profileListBox.ContextMenuStrip = this.profileContextMenuStrip;
      this.profileListBox.Font = new Font("MS UI Gothic", 14.25f, FontStyle.Regular, GraphicsUnit.Point, (byte) 128);
      this.profileListBox.FormattingEnabled = true;
      this.profileListBox.IntegralHeight = false;
      this.profileListBox.ItemHeight = 19;
      this.profileListBox.Location = new Point(3, 32);
      this.profileListBox.Name = "profileListBox";
      this.profileListBox.Size = new Size(277, 166);
      this.profileListBox.TabIndex = 4;
      this.profileListBox.Click += new EventHandler(this.profileListBox_Click);
      this.profileListBox.SelectedIndexChanged += new EventHandler(this.profileListBox_SelectedIndexChanged);
      this.profileListBox.KeyUp += new KeyEventHandler(this.profileListBox_KeyUp);
      this.profileListBox.MouseDoubleClick += new MouseEventHandler(this.profileListBox_MouseDoubleClick);
      this.profileListBox.MouseDown += new MouseEventHandler(this.profileListBox_MouseDown);
      this.profileListBox.MouseUp += new MouseEventHandler(this.profileListBox_MouseUp);
      this.profileContextMenuStrip.Items.AddRange(new ToolStripItem[21]
      {
        (ToolStripItem) this.toolStripTextBox1,
        (ToolStripItem) this.toolStripSeparator6,
        (ToolStripItem) this.launchToolStripMenuItem,
        (ToolStripItem) this.toolStripSeparator9,
        (ToolStripItem) this.launchQuickVSToolStripMenuItem,
        (ToolStripItem) this.toolStripSeparator8,
        (ToolStripItem) this.launchAutoModeToolStripMenuItem,
        (ToolStripItem) this.restartToolStripMenuItem,
        (ToolStripItem) this.restart2ToolStripMenuItem,
        (ToolStripItem) this.toolStripSeparator3,
        (ToolStripItem) this.openSelectStripMenuItem,
        (ToolStripItem) this.openMugenStripMenuItem,
        (ToolStripItem) this.openCharStripMenuItem,
        (ToolStripItem) this.toolStripSeparator4,
        (ToolStripItem) this.openProfileStripMenuItem,
        (ToolStripItem) this.copyProfileStripMenuItem,
        (ToolStripItem) this.pasteProfileStripMenuItem,
        (ToolStripItem) this.toolStripSeparator7,
        (ToolStripItem) this.deleteProfileToolStripMenuItem,
        (ToolStripItem) this.toolStripSeparator5,
        (ToolStripItem) this.quitToolStripMenuItem
      });
      this.profileContextMenuStrip.Name = "profileContextMenuStrip";
      this.profileContextMenuStrip.Size = new Size(281, 352);
      this.profileContextMenuStrip.Text = "設定1";
      this.profileContextMenuStrip.Opening += new CancelEventHandler(this.profileContextMenuStrip_Opening);
      this.toolStripTextBox1.BackColor = SystemColors.Control;
      this.toolStripTextBox1.BorderStyle = BorderStyle.None;
      this.toolStripTextBox1.Enabled = false;
      this.toolStripTextBox1.Name = "toolStripTextBox1";
      this.toolStripTextBox1.ReadOnly = true;
      this.toolStripTextBox1.Size = new Size(220, 18);
      this.toolStripSeparator6.Name = "toolStripSeparator6";
      this.toolStripSeparator6.Size = new Size(277, 6);
      this.launchToolStripMenuItem.DisplayStyle = ToolStripItemDisplayStyle.Text;
      this.launchToolStripMenuItem.Enabled = false;
      this.launchToolStripMenuItem.Name = "launchToolStripMenuItem";
      this.launchToolStripMenuItem.Size = new Size(280, 22);
      this.launchToolStripMenuItem.Text = "Start mugen";
      this.launchToolStripMenuItem.Click += new EventHandler(this.launchToolStripMenuItem_Click);
      this.toolStripSeparator9.Name = "toolStripSeparator9";
      this.toolStripSeparator9.Size = new Size(277, 6);
      this.launchQuickVSToolStripMenuItem.DisplayStyle = ToolStripItemDisplayStyle.Text;
      this.launchQuickVSToolStripMenuItem.Enabled = false;
      this.launchQuickVSToolStripMenuItem.Name = "launchQuickVSToolStripMenuItem";
      this.launchQuickVSToolStripMenuItem.Size = new Size(280, 22);
      this.launchQuickVSToolStripMenuItem.Text = "Start Quick Vs.";
      this.launchQuickVSToolStripMenuItem.Click += new EventHandler(this.launchQuickVSToolStripMenuItem_Click);
      this.toolStripSeparator8.Name = "toolStripSeparator8";
      this.toolStripSeparator8.Size = new Size(277, 6);
      this.launchAutoModeToolStripMenuItem.DisplayStyle = ToolStripItemDisplayStyle.Text;
      this.launchAutoModeToolStripMenuItem.Enabled = false;
      this.launchAutoModeToolStripMenuItem.Name = "launchAutoModeToolStripMenuItem";
      this.launchAutoModeToolStripMenuItem.Size = new Size(280, 22);
      this.launchAutoModeToolStripMenuItem.Text = "Start continuous battle";
      this.launchAutoModeToolStripMenuItem.Click += new EventHandler(this.launchAutoModeToolStripMenuItem_Click);
      this.restartToolStripMenuItem.Enabled = false;
      this.restartToolStripMenuItem.Name = "restartToolStripMenuItem";
      this.restartToolStripMenuItem.Size = new Size(280, 22);
      this.restartToolStripMenuItem.Text = "Restart from the cancelled match";
      this.restartToolStripMenuItem.Click += new EventHandler(this.restartToolStripMenuItem_Click);
      this.restart2ToolStripMenuItem.Enabled = false;
      this.restart2ToolStripMenuItem.Name = "restart2ToolStripMenuItem";
      this.restart2ToolStripMenuItem.Size = new Size(280, 22);
      this.restart2ToolStripMenuItem.Text = "Restart from the next match";
      this.restart2ToolStripMenuItem.Click += new EventHandler(this.restart2ToolStripMenuItem_Click);
      this.toolStripSeparator3.Name = "toolStripSeparator3";
      this.toolStripSeparator3.Size = new Size(277, 6);
      this.openSelectStripMenuItem.DisplayStyle = ToolStripItemDisplayStyle.Text;
      this.openSelectStripMenuItem.Name = "openSelectStripMenuItem";
      this.openSelectStripMenuItem.Size = new Size(280, 22);
      this.openSelectStripMenuItem.Text = "Open the select.def file";
      this.openSelectStripMenuItem.Click += new EventHandler(this.openSelectStripMenuItem_Click);
      this.openMugenStripMenuItem.DisplayStyle = ToolStripItemDisplayStyle.Text;
      this.openMugenStripMenuItem.Name = "openMugenStripMenuItem";
      this.openMugenStripMenuItem.Size = new Size(280, 22);
      this.openMugenStripMenuItem.Text = "Open the mugen.cfg file";
      this.openMugenStripMenuItem.Click += new EventHandler(this.openMugenStripMenuItem_Click);
      this.openCharStripMenuItem.DisplayStyle = ToolStripItemDisplayStyle.Text;
      this.openCharStripMenuItem.Name = "openCharStripMenuItem";
      this.openCharStripMenuItem.Size = new Size(280, 22);
      this.openCharStripMenuItem.Text = "Open the chars folder";
      this.openCharStripMenuItem.Click += new EventHandler(this.openCharStripMenuItem_Click);
      this.toolStripSeparator4.Name = "toolStripSeparator4";
      this.toolStripSeparator4.Size = new Size(277, 6);
      this.openProfileStripMenuItem.Name = "openProfileStripMenuItem";
      this.openProfileStripMenuItem.Size = new Size(280, 22);
      this.openProfileStripMenuItem.Text = "Profile settings...";
      this.openProfileStripMenuItem.Click += new EventHandler(this.openProfileStripMenuItem_Click);
      this.copyProfileStripMenuItem.Enabled = false;
      this.copyProfileStripMenuItem.Name = "copyProfileStripMenuItem";
      this.copyProfileStripMenuItem.Size = new Size(280, 22);
      this.copyProfileStripMenuItem.Text = "Copy profile";
      this.copyProfileStripMenuItem.Click += new EventHandler(this.copyProfileStripMenuItem_Click);
      this.pasteProfileStripMenuItem.Enabled = false;
      this.pasteProfileStripMenuItem.Name = "pasteProfileStripMenuItem";
      this.pasteProfileStripMenuItem.Size = new Size(280, 22);
      this.pasteProfileStripMenuItem.Text = "Paste profile";
      this.pasteProfileStripMenuItem.Click += new EventHandler(this.pasteProfileStripMenuItem_Click);
      this.toolStripSeparator7.Name = "toolStripSeparator7";
      this.toolStripSeparator7.Size = new Size(277, 6);
      this.deleteProfileToolStripMenuItem.Name = "deleteProfileToolStripMenuItem";
      this.deleteProfileToolStripMenuItem.Size = new Size(280, 22);
      this.deleteProfileToolStripMenuItem.Text = "Delete profile";
      this.deleteProfileToolStripMenuItem.Click += new EventHandler(this.deleteProfileToolStripMenuItem_Click);
      this.toolStripSeparator5.Name = "toolStripSeparator5";
      this.toolStripSeparator5.Size = new Size(277, 6);
      this.quitToolStripMenuItem.DisplayStyle = ToolStripItemDisplayStyle.Text;
      this.quitToolStripMenuItem.Name = "quitToolStripMenuItem";
      this.quitToolStripMenuItem.Size = new Size(280, 22);
      this.quitToolStripMenuItem.Text = "Close mugen";
      this.quitToolStripMenuItem.Click += new EventHandler(this.quitToolStripMenuItem_Click);
      this.autoModeTimer.Interval = 500;
      this.autoModeTimer.Tick += new EventHandler(this.autoModeTimer_Tick);
      this.quitButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
      this.quitButton.Enabled = false;
      this.quitButton.Location = new Point(147, 201);
      this.quitButton.Name = "quitButton";
      this.quitButton.Size = new Size(130, 25);
      this.quitButton.TabIndex = 5;
      this.quitButton.Text = "Start mugen";
      this.quitButton.UseVisualStyleBackColor = true;
      this.quitButton.Click += new EventHandler(this.quitButton_Click);
      this.startButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
      this.startButton.Location = new Point(3, 201);
      this.startButton.Name = "startButton";
      this.startButton.Size = new Size(131, 25);
      this.startButton.TabIndex = 6;
      this.startButton.Text = "More commands";
      this.startButton.UseVisualStyleBackColor = true;
      this.startButton.Click += new EventHandler(this.startButton_Click);
      this.toolTip1.AutomaticDelay = 500000;
      this.toolTip1.AutoPopDelay = 5000000;
      this.toolTip1.InitialDelay = 5000000;
      this.toolTip1.IsBalloon = true;
      this.toolTip1.ReshowDelay = 5000000;
      this.delayTimer.Interval = 200;
      this.delayTimer.Tick += new EventHandler(this.delayTimer_Tick);
      this.AutoScaleDimensions = new SizeF(6f, 12f);
      this.AutoScaleMode = AutoScaleMode.Font;
      this.ClientSize = new Size(283, 230);
      this.Controls.Add((Control) this.startButton);
      this.Controls.Add((Control) this.quitButton);
      this.Controls.Add((Control) this.profileListBox);
      this.Controls.Add((Control) this.fileMenuStrip);
      this.DoubleBuffered = true;
      this.Icon = (Icon) componentResourceManager.GetObject("$this.Icon");
      this.KeyPreview = true;
      this.Location = new Point(10, 10);
      this.MainMenuStrip = this.fileMenuStrip;
      this.MaximizeBox = false;
      this.Name = nameof (MainForm);
      this.Padding = new Padding(3);
      this.SizeGripStyle = SizeGripStyle.Hide;
      this.StartPosition = FormStartPosition.Manual;
      this.Text = "Swiss Army Knife";
      this.Activated += new EventHandler(this.Form1_Activated);
      this.Deactivate += new EventHandler(this.MainForm_Deactivate);
      this.FormClosing += new FormClosingEventHandler(this.Form1_FormClosing);
      this.Load += new EventHandler(this.Form1_Load);
      this.Shown += new EventHandler(this.Form1_Shown);
      this.KeyPress += new KeyPressEventHandler(this.MainForm_KeyPress);
      this.Resize += new EventHandler(this.MainForm_Resize);
      this.fileMenuStrip.ResumeLayout(false);
      this.fileMenuStrip.PerformLayout();
      this.profileContextMenuStrip.ResumeLayout(false);
      this.profileContextMenuStrip.PerformLayout();
      this.ResumeLayout(false);
      this.PerformLayout();
    }

    private MainForm()
    {
      this.InitializeComponent();
      ProfileManager.MainObj().SetCurrentProfile(1);
      Application.Idle += new EventHandler(this.app_Idle);
    }

    public static MainForm MainObj()
    {
      if (MainForm.selfObj == null)
        MainForm.selfObj = new MainForm();
      return MainForm.selfObj;
    }

    [DllImport("user32.dll")]
    private static extern uint GetWindowThreadProcessId(IntPtr hWnd, out int lpdwProcessId);

    [DllImport("user32.dll")]
    private static extern IntPtr GetForegroundWindow();

    [DllImport("Kernel32.DLL", CharSet = CharSet.Auto, SetLastError = true)]
    private static extern uint SetThreadExecutionState(uint state);

    public bool SetForegroundWindowEx(IntPtr hWnd) => MugenWindow.MainObj().SetForegroundWindowEx(hWnd);

    public void ShowTop(IntPtr hWnd, bool isMugenWindow = false) => MugenWindow.MainObj().ShowTop(hWnd, isMugenWindow);

    [DllImport("user32.dll", EntryPoint = "SendMessage")]
    private static extern bool SendMessage1(IntPtr hWnd, uint Msg, int wParam, int lParam);

    public static string ExeFolder()
    {
      if (MainForm._exeFolder == null)
        MainForm._exeFolder = Path.GetDirectoryName(Application.ExecutablePath);
      return MainForm._exeFolder;
    }

    public static string GetFullPath(string file) => !Path.IsPathRooted(file) ? Path.Combine(MainForm.ExeFolder(), file) : file;

    public bool IsMugenRunning() => MugenWindow.MainObj() != null;

    public bool DidMugenCrashed() => this._didMugenCrashed;

    private bool _LaunchMugen()
    {
      if (MugenWindow.MainObj() == null || ProfileManager.MainObj().GetProfile(this._selectedProfileNo) == null)
        return false;
      if (MugenWindow.MainObj().getMugenProcess() == null)
      {
        if (!MugenWindow.MainObj().LoadMugen(this._selectedProfileNo))
          return false;
        MugenWindow.MainObj().Show();
        if (MugenWindow.MainObj().getMugenProcess() != null)
        {
          this._didMugenCrashed = false;
          this._isRetrying = false;
          if (!this._isAutoModeRunning)
            this.watchTimer.Start();
        }
      }
      return true;
    }

    private void LaunchMugen(bool isRestart)
    {
      if (this._isAutoModeRunning || MugenWindow.MainObj() == null)
        return;
      MugenProfile currentProfile = ProfileManager.MainObj().GetCurrentProfile();
      if (currentProfile == null)
        return;
      MugenWindow.MainObj().ResetGameQuitted();
      this._selectedProfileNo = currentProfile.GetProfileNo();
      switch (currentProfile.GetCurrentGameMode())
      {
        case MugenProfile.GameMode.QUICK_VS:
          QuickVSModePanel quickVsModePanel = new QuickVSModePanel();
          Point location = this.Location;
          int x = location.X + 205;
          location = this.Location;
          int y = location.Y;
          quickVsModePanel.SetLoc(x, y);
          int num = (int) quickVsModePanel.ShowDialog();
          if (quickVsModePanel.GetResult() == DialogResult.OK && this._LaunchMugen())
          {
            this.EnableQuitButton(true);
            LogManager.MainObj().appendLog(" ");
            LogManager.MainObj().appendLog(currentProfile.GetProfileName() + "' has been started in Quick Vs. mode.");
          }
          quickVsModePanel.Dispose();
          break;
        case MugenProfile.GameMode.AUTO_MODE:
          this._isAutoModeRunning = true;
          if (isRestart)
          {
            currentProfile.SetTempGameCount(-1);
            MugenWindow.MainObj().ResetNumOfGames();
          }
          else
            MugenWindow.MainObj().DecrementNumOfGames();
          this.autoModeTimer.Enabled = true;
          if (GameLogger.MainObj().GetPlayersCount() > 0 && currentProfile.GetTempGameCount() <= 0)
            LogManager.MainObj().ResetPlayers();
          LogManager.MainObj().appendLog(" ");
          LogManager.MainObj().appendLog(currentProfile.GetProfileName() + "' is now running.");
          if (currentProfile.GetTempGameCount() <= 0)
            LogManager.MainObj().appendLog("Continuous battle mode has started. (" + (object) currentProfile.GetCharacterCount() + "total matches)");
          else
            LogManager.MainObj().appendLog("Resume continuous battle mode. (" + (object) (currentProfile.GetCharacterCount() - currentProfile.GetTempGameCount()) + " matches remaining)");
          LogManager.MainObj().appendLog("----- Continuous battle settings -----");
          if (currentProfile.GetMatchMode() == MugenProfile.MatchMode.ALLvsALL)
            LogManager.MainObj().appendLog("Battle setup: round robin");
          else
            LogManager.MainObj().appendLog("Battle setup: one vs. others");
          if (currentProfile.IsStrictRoundMode())
            LogManager.MainObj().appendLog("Round settings: " + (object) currentProfile.GetRoundCount() + " round(s) to win.");
          else
            LogManager.MainObj().appendLog("Round settings: " + (object) currentProfile.GetRoundCount() + " total round(s).");
          LogManager.MainObj().appendLog("Round time: " + (object) currentProfile.GetMaxRoundTimeRawData() + " minute(s) maximum.");
          LogManager.MainObj().appendLog("Error retries: " + (object) currentProfile.GetErrorRetryCount() + " time(s) maximum.");
          this.autoModeTimer.Start();
          this.EnableQuitButton(true);
          break;
        default:
          if (!this._LaunchMugen())
            break;
          this.EnableQuitButton(true);
          LogManager.MainObj().appendLog(" ");
          LogManager.MainObj().appendLog("'" + currentProfile.GetProfileName() + "' is now running");
          break;
      }
    }

    private void CloseMugen()
    {
      if (MugenWindow.MainObj() == null)
        return;
      this.watchTimer.Stop();
      MugenWindow.MainObj().CloseMugen();
      this.EnableQuitButton(false);
      if (this._isClosing)
        return;
      LogManager.MainObj().appendLog("mugen has been closed.");
    }

    private void EnableStartButton(bool isEnable, MugenProfile.GameMode mode)
    {
      MugenProfile currentProfile = ProfileManager.MainObj().GetCurrentProfile();
      if (currentProfile == null)
        return;
      if (isEnable)
      {
        switch (mode)
        {
          case MugenProfile.GameMode.QUICK_VS:
            this.quitButton.Text = "Start Quick Vs.";
            break;
          case MugenProfile.GameMode.AUTO_MODE:
            if (currentProfile.IsAutoModeAvailable())
            {
              if (currentProfile.GetTempGameCount() >= 0)
              {
                this.quitButton.Text = "Restart cancelled match.";
                break;
              }
              this.quitButton.Text = "Start continuous battle.";
              break;
            }
            this.quitButton.Text = "Start mugen.";
            break;
          default:
            this.quitButton.Text = "Start mugen.";
            break;
        }
      }
      else
        this.quitButton.Text = "Profile registration.";
      this.quitButton.Enabled = true;
    }

    private void EnableQuitButton(bool isEnable)
    {
      MugenProfile currentProfile = ProfileManager.MainObj().GetCurrentProfile();
      if (currentProfile == null)
        return;
      if (!isEnable)
      {
        switch (currentProfile.GetDefaultGameMode())
        {
          case MugenProfile.GameMode.QUICK_VS:
            this.quitButton.Text = "Start Quick Vs.";
            break;
          case MugenProfile.GameMode.AUTO_MODE:
            if (currentProfile.IsAutoModeAvailable())
            {
              if (currentProfile.GetTempGameCount() >= 0)
              {
                this.quitButton.Text = "Restart cancelled match.";
                break;
              }
              this.quitButton.Text = "Start continuous battle.";
              break;
            }
            this.quitButton.Text = "Start mugen.";
            break;
          default:
            this.quitButton.Text = "Start mugen.";
            break;
        }
      }
      else
      {
        switch (currentProfile.GetCurrentGameMode())
        {
          case MugenProfile.GameMode.QUICK_VS:
            this.quitButton.Text = "Stop Quick Vs.";
            break;
          case MugenProfile.GameMode.AUTO_MODE:
            this.quitButton.Text = "Stop continuous battle mode.";
            break;
          default:
            this.quitButton.Text = "Close mugen.";
            break;
        }
      }
      string profileName = currentProfile.GetProfileName();
      string mugenExePath = currentProfile.GetMugenExePath();
      if (profileName == null || profileName == "")
      {
        this.quitButton.Text = "Profile registration.";
        this.quitButton.Enabled = true;
      }
      else if (mugenExePath == null || mugenExePath == "" || !File.Exists(mugenExePath))
      {
        this.quitButton.Text = "Profile registration.";
        this.quitButton.Enabled = true;
      }
      else
        this.quitButton.Enabled = true;
    }

    private void Form1_FormClosing(object sender, FormClosingEventArgs e)
    {
      this._isClosing = true;
      if (e.CloseReason == CloseReason.UserClosing)
      {
        if (MessageBox.Show("Are you sure you wish to quit？", "Swiss Army Knife", MessageBoxButtons.OKCancel) == DialogResult.Cancel)
        {
          this._isClosing = false;
          e.Cancel = true;
          return;
        }
        if (MugenWindow.MainObj() != null && MugenWindow.MainObj().Visible)
        {
          MainConfig mainConfig1 = ProfileManager.MainObj().GetMainConfig();
          if (mainConfig1 != null)
          {
            MainConfig mainConfig2 = mainConfig1;
            int x1 = this.Location.X;
            int y1 = this.Location.Y;
            int width1 = this.Size.Width;
            int height1 = this.Size.Height;
            int main_x = x1;
            int main_y = y1;
            int main_w = width1;
            int main_h = height1;
            mainConfig2.SetMainWindowPos(main_x, main_y, main_w, main_h);
            if (LogManager.MainObj() != null)
            {
              MainConfig mainConfig3 = mainConfig1;
              int x2 = LogManager.MainObj().Location.X;
              int y2 = LogManager.MainObj().Location.Y;
              int width2 = LogManager.MainObj().Size.Width;
              int height2 = LogManager.MainObj().Size.Height;
              int log_x = x2;
              int log_y = y2;
              int log_w = width2;
              int log_h = height2;
              mainConfig3.SetLogWindowPos(log_x, log_y, log_w, log_h);
            }
            if (MugenWindow.MainObj() != null)
              mainConfig1.SetMugenWindowPos(MugenWindow.MainObj().Location.X, MugenWindow.MainObj().Location.Y);
            if (DebugForm.MainObj() != null)
            {
              MainConfig mainConfig3 = mainConfig1;
              int x2 = DebugForm.MainObj().Location.X;
              int y2 = DebugForm.MainObj().Location.Y;
              int width2 = DebugForm.MainObj().Size.Width;
              int height2 = DebugForm.MainObj().Size.Height;
              int debug_x = x2;
              int debug_y = y2;
              int debug_w = width2;
              int debug_h = height2;
              mainConfig3.SetDebugWindowPos(debug_x, debug_y, debug_w, debug_h);
            }
            if (VarForm.MainObj() != null)
            {
              MainConfig mainConfig3 = mainConfig1;
              int x2 = VarForm.MainObj().Location.X;
              int y2 = VarForm.MainObj().Location.Y;
              int width2 = VarForm.MainObj().Size.Width;
              int height2 = VarForm.MainObj().Size.Height;
              int var_x = x2;
              int var_y = y2;
              int var_w = width2;
              int var_h = height2;
              mainConfig3.SetVarWindowPos(var_x, var_y, var_w, var_h);
            }
            mainConfig1.SaveConfigData();
          }
        }
      }
      this._isClosing = true;
      this.CloseMugen();
    }

    private void watchTimer_Tick(object sender, EventArgs e)
    {
      if (MugenWindow.MainObj() == null)
        return;
      int num = (int) MainForm.SetThreadExecutionState(3U);
      this.watchTimer.Stop();
      if (MugenWindow.MainObj().getMugenProcess() == null)
      {
        if (!this._isAutoModeRunning)
        {
          this.EnableQuitButton(false);
          MugenProfile profile = ProfileManager.MainObj().GetProfile(this._selectedProfileNo);
          if (profile == null || !profile.IsQuickMode())
            LogManager.MainObj().appendLog("mugen has been closed.");
          this.Activate();
        }
        MugenWindow.MainObj().SetTitleActive(false);
      }
      else if (MugenWindow.MainObj().CheckMugenError())
      {
        if (!this._isAutoModeRunning)
          this.EnableQuitButton(false);
        else
          this._didMugenCrashed = true;
        MugenWindow.MainObj().SetTitleActive(false);
      }
      else
      {
        MugenWindow.MainObj().UpdateTitle();
        this.watchTimer.Start();
      }
    }

    private void Form1_Load(object sender, EventArgs e)
    {
      this.helpBtnStripMenuItem.Image = (Image) SystemIcons.Question.ToBitmap();
      this.helpToolStripMenuItem.Image = (Image) SystemIcons.Question.ToBitmap();
      MugenWindow.MainObj().Hide();
      LogManager.MainObj().Hide();
      DebugForm.MainObj().Hide();
      VarForm.MainObj().Hide();
      MainConfig mainConfig = ProfileManager.MainObj().GetMainConfig();
      if (mainConfig != null)
      {
        if (mainConfig.GetMainWinW() > 0 && mainConfig.GetMainWinH() > 0)
          this.SetDesktopBounds(mainConfig.GetMainWinX(), mainConfig.GetMainWinY(), mainConfig.GetMainWinW(), mainConfig.GetMainWinH());
        else
          this.SetDesktopLocation(mainConfig.GetMainWinX(), mainConfig.GetMainWinY());
        if (mainConfig.GetLogWinW() > 0 && mainConfig.GetLogWinH() > 0)
          LogManager.MainObj().SetDesktopBounds(mainConfig.GetLogWinX(), mainConfig.GetLogWinY(), mainConfig.GetLogWinW(), mainConfig.GetLogWinH());
        else
          LogManager.MainObj().SetDesktopLocation(mainConfig.GetLogWinX(), mainConfig.GetLogWinY());
        MugenWindow.MainObj().SetDesktopLocation(mainConfig.GetMugenWinX(), mainConfig.GetMugenWinY());
        DebugForm.MainObj().StartPosition = FormStartPosition.Manual;
        if (mainConfig.GetDebugWinW() > 0 && mainConfig.GetDebugWinH() > 0)
          DebugForm.MainObj().SetDesktopBounds(mainConfig.GetDebugWinX(), mainConfig.GetDebugWinY(), mainConfig.GetDebugWinW(), mainConfig.GetDebugWinH());
        else
          DebugForm.MainObj().SetDesktopLocation(mainConfig.GetDebugWinX(), mainConfig.GetDebugWinY());
        VarForm.MainObj().StartPosition = FormStartPosition.Manual;
        if (mainConfig.GetVarWinW() > 0 && mainConfig.GetVarWinH() > 0)
          VarForm.MainObj().SetDesktopBounds(mainConfig.GetVarWinX(), mainConfig.GetVarWinY(), mainConfig.GetVarWinW(), mainConfig.GetVarWinH());
        else
          VarForm.MainObj().SetDesktopLocation(mainConfig.GetVarWinX(), mainConfig.GetVarWinY());
      }
      int profileCount = ProfileManager.MainObj().GetProfileCount();
      this.profileListBox.Items.Clear();
      for (int profile_no = 1; profile_no <= profileCount; ++profile_no)
      {
        MugenProfile profile = ProfileManager.MainObj().GetProfile(profile_no);
        if (profile != null)
        {
          string str = profile.GetProfileName();
          if (str == null || str == "")
            str = "(New Profile)";
          this.profileListBox.Items.Add((object) str);
        }
      }
      this.profileListBox.SetSelected(0, true);
    }

    public void SetNeedActivateAll()
    {
    }

    public void ActivateAll(bool forceActivate)
    {
      if (!forceActivate && MugenWindow.MainObj().getMugenProcess() != null && !MugenWindow.MainObj().IsActivatedOnce() || this._isClosing)
        return;
      this.ShowTop(LogManager.MainObj().Handle);
      this.ShowTop(DebugForm.MainObj().Handle);
      this.ShowTop(VarForm.MainObj().Handle);
      if (MugenWindow.MainObj().getMugenProcess() != null)
      {
        this.SetForegroundWindowEx(this.Handle);
        if (!MugenWindow.MainObj().Visible)
          return;
        MugenWindow.MainObj().ShowMugen();
      }
      else
      {
        if (MugenWindow.MainObj().Visible)
          this.ShowTop(MugenWindow.MainObj().Handle, true);
        this.ShowTop(this.Handle);
      }
    }

    protected override void WndProc(ref Message m)
    {
      if (m.Msg == 161)
      {
        if (m.WParam.ToInt32() == 2)
          this._wasBtnDown = true;
      }
      else if (m.Msg == 561)
        this._wasBtnDown = false;
      else if (m.Msg == 674)
      {
        if (this._wasBtnDown)
          this.ActivateAll(false);
        this._wasBtnDown = false;
      }
      base.WndProc(ref m);
    }

    private void Form1_Activated(object sender, EventArgs e)
    {
      int num = this._isClosing ? 1 : 0;
    }

    private void delayTimer_Tick(object sender, EventArgs e)
    {
      this._needActievateAll = false;
      this.delayTimer.Stop();
      this.delayTimer.Enabled = false;
    }

    private void MainForm_Deactivate(object sender, EventArgs e)
    {
      this._isMenuVisible = false;
      this.toolTip1.Hide((IWin32Window) this);
    }

    public void ArrangeWin() => this._ArrangeWin(false);

    private void _ArrangeWin(bool isFirstLaunch)
    {
      MainConfig mainConfig = ProfileManager.MainObj().GetMainConfig();
      int num1 = MugenWindow.MainObj().Size.Width - 640;
      Size size;
      Point location;
      if (LogManager.MainObj() != null)
      {
        if (mainConfig.GetLogWinX() == 0 && mainConfig.GetLogWinY() == 0 || !isFirstLaunch)
        {
          LogManager logManager = LogManager.MainObj();
          int num2 = this.Location.X + this.Size.Width;
          size = LogManager.MainObj().Size;
          int width = size.Width;
          int num3 = num2 - width;
          location = this.Location;
          int y1 = location.Y;
          size = this.Size;
          int height = size.Height;
          int num4 = y1 + height + 12;
          int x = num3;
          int y2 = num4;
          logManager.SetDesktopLocation(x, y2);
        }
        LogManager.MainObj().Show();
        if (!isFirstLaunch)
          LogManager.MainObj().Activate();
      }
      if (MugenWindow.MainObj() != null)
      {
        if (mainConfig.GetMugenWinX() == 0 && mainConfig.GetMugenWinY() == 0 || !isFirstLaunch)
        {
          MugenWindow mugenWindow = MugenWindow.MainObj();
          location = this.Location;
          int x1 = location.X;
          size = this.Size;
          int width = size.Width;
          int num2 = x1 + width + 11;
          location = this.Location;
          int num3 = location.Y + num1;
          int x2 = num2;
          int y = num3;
          mugenWindow.SetDesktopLocation(x2, y);
        }
        MugenWindow.MainObj().Show();
        if (!isFirstLaunch)
          MugenWindow.MainObj().Activate();
      }
      if (mainConfig == null)
        return;
      if (isFirstLaunch)
      {
        string path = mainConfig.GetTextEditor();
        if (path != null)
          path = path.ToLower();
        if (path == null || !path.Contains("notepad") && !File.Exists(path))
        {
          WelcomeForm welcomeForm = new WelcomeForm();
          if (welcomeForm != null)
          {
            int num2 = (int) welcomeForm.ShowDialog();
            bool flag = welcomeForm.DidClickOK();
            welcomeForm.Dispose();
            if (!flag)
              Application.Exit();
            MainConfigPanel mainConfigPanel = new MainConfigPanel();
            if (mainConfigPanel != null)
            {
              mainConfigPanel.DisableCancel();
              int num3 = (int) mainConfigPanel.ShowDialog();
              mainConfigPanel.Dispose();
              NavigatorForm navigatorForm1 = new NavigatorForm((Form) this);
              if (navigatorForm1 != null)
              {
                NavigatorForm navigatorForm2 = navigatorForm1;
                location = this.Location;
                int num4 = location.X + 300;
                location = this.Location;
                int num5 = location.Y + 50;
                int x = num4;
                int y = num5;
                navigatorForm2.SetLoc(x, y);
                int num6 = (int) navigatorForm1.ShowDialog();
                navigatorForm1.Dispose();
              }
            }
          }
        }
      }
      if (DebugForm.MainObj() != null)
      {
        if (mainConfig != null)
        {
          if (mainConfig.DoesShowDebugWin())
          {
            if (mainConfig.GetDebugWinX() == 0 && mainConfig.GetDebugWinY() == 0 || !isFirstLaunch)
            {
              DebugForm.MainObj().StartPosition = FormStartPosition.Manual;
              DebugForm debugForm = DebugForm.MainObj();
              location = this.Location;
              int x1 = location.X;
              size = this.Size;
              int width1 = size.Width;
              int num2 = x1 + width1;
              size = MugenWindow.MainObj().Size;
              int width2 = size.Width;
              int num3 = num2 + width2 + 20;
              location = this.Location;
              int y1 = location.Y;
              int x2 = num3;
              int y2 = y1;
              debugForm.SetDesktopLocation(x2, y2);
            }
            DebugForm.MainObj().Show();
            if (!isFirstLaunch)
              DebugForm.MainObj().Activate();
          }
          else if (isFirstLaunch)
            DebugForm.MainObj().Hide();
        }
        else if (isFirstLaunch)
          DebugForm.MainObj().Hide();
      }
      if (VarForm.MainObj() != null)
      {
        if (mainConfig != null)
        {
          if (mainConfig.DoesShowDebugWin())
          {
            if (mainConfig.GetDebugWinX() == 0 && mainConfig.GetDebugWinY() == 0 || !isFirstLaunch)
            {
              VarForm.MainObj().StartPosition = FormStartPosition.Manual;
              VarForm varForm = VarForm.MainObj();
              location = LogManager.MainObj().Location;
              int x1 = location.X;
              size = LogManager.MainObj().Size;
              int width = size.Width;
              int num2 = x1 + width + 11;
              location = this.Location;
              int y1 = location.Y;
              size = MugenWindow.MainObj().Size;
              int height = size.Height;
              int num3 = y1 + height + num1 + 10;
              int x2 = num2;
              int y2 = num3;
              varForm.SetDesktopLocation(x2, y2);
            }
            VarForm.MainObj().Show();
            if (!isFirstLaunch)
              VarForm.MainObj().Activate();
          }
          else if (isFirstLaunch)
            VarForm.MainObj().Hide();
        }
        else if (isFirstLaunch)
          VarForm.MainObj().Hide();
      }
      this.Activate();
    }

    private void Form1_Shown(object sender, EventArgs e) => this._ArrangeWin(true);

    private void exitToolStripMenuItem_Click(object sender, EventArgs e)
    {
      Control dropDown = (Control) this.ファイルFToolStripMenuItem.DropDown;
      if (dropDown != null && dropDown.Cursor == Cursors.Help)
      {
        this.ファイルFToolStripMenuItem.DropDown.Show();
        string text = "Exit." + Environment.NewLine;
        this.toolTip1.Hide((IWin32Window) dropDown);
        this.toolTip1.IsBalloon = false;
        this.toolTip1.IsBalloon = true;
        this.toolTip1.SetToolTip(dropDown, " ");
        this.toolTip1.Show(text, (IWin32Window) dropDown, this.exitAllToolStripMenuItem.Width / 2, this.exitAllToolStripMenuItem.Height * 4 / 2);
      }
      else
        this.Close();
    }

    private void versionAToolStripMenuItem_Click(object sender, EventArgs e)
    {
      int num = (int) MessageBox.Show("Swiss Army Knife for winmugen - ver. 1.08 \n English translation by Vans.", "Swiss Army Knife");
    }

    private void activeteStripMenuItem_Click(object sender, EventArgs e)
    {
      Control dropDown = (Control) this.ツールTToolStripMenuItem.DropDown;
      if (dropDown != null && dropDown.Cursor == Cursors.Help)
      {
        this.ツールTToolStripMenuItem.DropDown.Show();
        string text = "Displays the other windows to the front." + Environment.NewLine + "(This has the same effect as clicking all the other windows to bring them to focus and then selecting the 'Swiss Army Knife' dialog.)" + Environment.NewLine + Environment.NewLine + "(Note: Windows that are not enabled will not be affected.)";
        this.toolTip1.Hide((IWin32Window) dropDown);
        this.toolTip1.IsBalloon = false;
        this.toolTip1.IsBalloon = true;
        this.toolTip1.SetToolTip(dropDown, " ");
        this.toolTip1.Show(text, (IWin32Window) dropDown, this.logWinToolStripMenuItem.Width / 2, this.logWinToolStripMenuItem.Height / 2);
      }
      else
        this.ActivateAll(true);
    }

    private void arrangeWinStripMenuItem_Click(object sender, EventArgs e)
    {
      Control dropDown = (Control) this.ツールTToolStripMenuItem.DropDown;
      if (dropDown != null && dropDown.Cursor == Cursors.Help)
      {
        this.ツールTToolStripMenuItem.DropDown.Show();
        string text = "Aligns all the other windows to the default position relative to the main 'Swiss Army Knife' window." + Environment.NewLine + Environment.NewLine + "(Note: Only the enabled windows will be aligned.)";
        this.toolTip1.Hide((IWin32Window) dropDown);
        this.toolTip1.IsBalloon = false;
        this.toolTip1.IsBalloon = true;
        this.toolTip1.SetToolTip(dropDown, " ");
        this.toolTip1.Show(text, (IWin32Window) dropDown, this.logWinToolStripMenuItem.Width / 2, this.logWinToolStripMenuItem.Height * 3 / 2);
      }
      else
        this._ArrangeWin(false);
    }

    private void mugenWinToolStripMenuItem_Click(object sender, EventArgs e)
    {
      Control dropDown = (Control) this.ツールTToolStripMenuItem.DropDown;
      if (dropDown != null && dropDown.Cursor == Cursors.Help)
      {
        this.ツールTToolStripMenuItem.DropDown.Show();
        string text = "Toggles the M.U.G.E.N window," + Environment.NewLine + "(Note: Only changes the focus of the M.U.G.E.N window.)";
        this.toolTip1.Hide((IWin32Window) dropDown);
        this.toolTip1.IsBalloon = false;
        this.toolTip1.IsBalloon = true;
        this.toolTip1.SetToolTip(dropDown, " ");
        this.toolTip1.Show(text, (IWin32Window) dropDown, this.logWinToolStripMenuItem.Width / 2, this.logWinToolStripMenuItem.Height * 6 / 2);
      }
      else
      {
        if (MugenWindow.MainObj() == null)
          return;
        if (!MugenWindow.MainObj().Visible)
          MugenWindow.MainObj().Show();
        else
          MugenWindow.MainObj().Activate();
      }
    }

    private void logWinToolStripMenuItem_Click(object sender, EventArgs e)
    {
      Control dropDown = (Control) this.ツールTToolStripMenuItem.DropDown;
      if (dropDown != null && dropDown.Cursor == Cursors.Help)
      {
        this.ツールTToolStripMenuItem.DropDown.Show();
        string text = "Toggles the Battle History window." + Environment.NewLine;
        this.toolTip1.Hide((IWin32Window) dropDown);
        this.toolTip1.IsBalloon = false;
        this.toolTip1.IsBalloon = true;
        this.toolTip1.SetToolTip(dropDown, " ");
        this.toolTip1.Show(text, (IWin32Window) dropDown, this.logWinToolStripMenuItem.Width / 2, this.logWinToolStripMenuItem.Height * 8 / 2);
      }
      else
      {
        if (LogManager.MainObj() == null)
          return;
        if (!LogManager.MainObj().Visible)
          LogManager.MainObj().Show();
        else
          LogManager.MainObj().Hide();
      }
    }

    private void debugWinToolStripMenuItem_Click(object sender, EventArgs e)
    {
      Control dropDown = (Control) this.ツールTToolStripMenuItem.DropDown;
      if (dropDown != null && dropDown.Cursor == Cursors.Help)
      {
        this.ツールTToolStripMenuItem.DropDown.Show();
        string text = "Toggles the Debug Tools window." + Environment.NewLine;
        this.toolTip1.Hide((IWin32Window) dropDown);
        this.toolTip1.IsBalloon = false;
        this.toolTip1.IsBalloon = true;
        this.toolTip1.SetToolTip(dropDown, " ");
        this.toolTip1.Show(text, (IWin32Window) dropDown, this.logWinToolStripMenuItem.Width / 2, this.logWinToolStripMenuItem.Height * 10 / 2);
      }
      else
      {
        if (DebugForm.MainObj() == null)
          return;
        if (!DebugForm.MainObj().Visible)
          DebugForm.MainObj().Show();
        else
          DebugForm.MainObj().Hide();
      }
    }

    private void varWinStripMenuItem_Click(object sender, EventArgs e)
    {
      Control dropDown = (Control) this.ツールTToolStripMenuItem.DropDown;
      if (dropDown != null && dropDown.Cursor == Cursors.Help)
      {
        this.ツールTToolStripMenuItem.DropDown.Show();
        string text = "Toggles the Variable Display window." + Environment.NewLine;
        this.toolTip1.Hide((IWin32Window) dropDown);
        this.toolTip1.IsBalloon = false;
        this.toolTip1.IsBalloon = true;
        this.toolTip1.SetToolTip(dropDown, " ");
        this.toolTip1.Show(text, (IWin32Window) dropDown, this.logWinToolStripMenuItem.Width / 2, this.logWinToolStripMenuItem.Height * 12 / 2);
      }
      else
      {
        if (VarForm.MainObj() == null)
          return;
        if (!VarForm.MainObj().Visible)
          VarForm.MainObj().Show();
        else
          VarForm.MainObj().Hide();
      }
    }

    private void configToolStripMenuItem_Click(object sender, EventArgs e)
    {
      Control dropDown = (Control) this.ファイルFToolStripMenuItem.DropDown;
      if (dropDown != null && dropDown.Cursor == Cursors.Help)
      {
        this.ファイルFToolStripMenuItem.DropDown.Show();
        string text = "Opens the general settings dialog." + Environment.NewLine;
        this.toolTip1.Hide((IWin32Window) dropDown);
        this.toolTip1.IsBalloon = false;
        this.toolTip1.IsBalloon = true;
        this.toolTip1.SetToolTip(dropDown, " ");
        this.toolTip1.Show(text, (IWin32Window) dropDown, this.exitAllToolStripMenuItem.Width / 2, this.exitAllToolStripMenuItem.Height / 2);
      }
      else
      {
        if (ProfileManager.MainObj().GetMainConfig() == null)
          return;
        MainConfigPanel mainConfigPanel = new MainConfigPanel();
        if (mainConfigPanel == null)
          return;
        int num = (int) mainConfigPanel.ShowDialog();
        mainConfigPanel.Dispose();
      }
    }

    private void reloadConfigStripMenuItem_Click(object sender, EventArgs e) => ProfileManager.MainObj().GetMainConfig()?.ReLoad();

    private void profileListBox_SelectedIndexChanged(object sender, EventArgs e)
    {
      ProfileManager.MainObj().SetCurrentProfile(this.profileListBox.SelectedIndex + 1);
      MugenProfile currentProfile = ProfileManager.MainObj().GetCurrentProfile();
      if (currentProfile == null || MugenWindow.MainObj().getMugenProcess() != null || this._isAutoModeRunning)
        return;
      string profileName = currentProfile.GetProfileName();
      string mugenExePath = currentProfile.GetMugenExePath();
      if (profileName == null || profileName == "")
        this.EnableStartButton(false, currentProfile.GetDefaultGameMode());
      else if (mugenExePath == null || mugenExePath == "" || !File.Exists(mugenExePath))
        this.EnableStartButton(false, currentProfile.GetDefaultGameMode());
      else
        this.EnableStartButton(true, currentProfile.GetDefaultGameMode());
    }

    private void profileListBox_MouseDoubleClick(object sender, MouseEventArgs e)
    {
      if (this.profileListBox.Cursor == Cursors.Help)
      {
        this.profileListBox_ShowHelp();
      }
      else
      {
        ProfileManager.MainObj().SetCurrentProfile(this.profileListBox.SelectedIndex + 1);
        MugenProfile currentProfile1 = ProfileManager.MainObj().GetCurrentProfile();
        if (currentProfile1 == null || currentProfile1.GetProfileName() == null || (currentProfile1.GetProfileName() == "" || currentProfile1.GetMugenExePath() == null) || (currentProfile1.GetMugenExePath() == "" || !File.Exists(currentProfile1.GetMugenExePath())))
        {
          ProfileConfigPanel profileConfigPanel = new ProfileConfigPanel();
          if (profileConfigPanel == null)
            return;
          int num = (int) profileConfigPanel.ShowDialog();
          profileConfigPanel.Dispose();
          MugenProfile currentProfile2 = ProfileManager.MainObj().GetCurrentProfile();
          if (currentProfile2 == null)
            return;
          int selectedIndex = this.profileListBox.SelectedIndex;
          string str = currentProfile2.GetProfileName();
          if (str == null || str == "")
            str = "(New Profile)";
          this.profileListBox.Items[selectedIndex] = (object) str;
        }
        else
        {
          if (MugenWindow.MainObj().getMugenProcess() != null || this._isAutoModeRunning)
            return;
          if (currentProfile1 != null)
          {
            switch (currentProfile1.GetDefaultGameMode())
            {
              case MugenProfile.GameMode.QUICK_VS:
                currentProfile1.SetGameMode(MugenProfile.GameMode.QUICK_VS);
                this.LaunchMugen(true);
                break;
              case MugenProfile.GameMode.AUTO_MODE:
                if (currentProfile1.IsAutoModeAvailable())
                {
                  currentProfile1.SetGameMode(MugenProfile.GameMode.AUTO_MODE);
                  this._didMugenCrashed = false;
                  currentProfile1.SetIncremented();
                  if (!currentProfile1.PrepareNextMatch(currentProfile1.GetTempGameCount()))
                  {
                    this.LaunchMugen(true);
                    break;
                  }
                  this.LaunchMugen(false);
                  break;
                }
                currentProfile1.SetGameMode(MugenProfile.GameMode.NORMAL);
                this.LaunchMugen(true);
                break;
              default:
                currentProfile1.SetGameMode(MugenProfile.GameMode.NORMAL);
                this.LaunchMugen(true);
                break;
            }
          }
          MugenWindow.MainObj().ActivateEx();
        }
      }
    }

    private void profileContextMenuStrip_Opening(object sender, CancelEventArgs e)
    {
      Control contextMenuStrip = (Control) this.profileContextMenuStrip;
      if (contextMenuStrip != null && contextMenuStrip.Cursor == Cursors.Help)
      {
        this.launchToolStripMenuItem.Enabled = true;
        this.launchQuickVSToolStripMenuItem.Enabled = true;
        this.launchAutoModeToolStripMenuItem.Enabled = true;
        this.restartToolStripMenuItem.Enabled = true;
        this.restart2ToolStripMenuItem.Enabled = true;
        this.openMugenStripMenuItem.Enabled = true;
        this.openSelectStripMenuItem.Enabled = true;
        this.openCharStripMenuItem.Enabled = true;
        this.quitToolStripMenuItem.Enabled = true;
        this.openProfileStripMenuItem.Enabled = true;
        this.copyProfileStripMenuItem.Enabled = true;
        this.pasteProfileStripMenuItem.Enabled = true;
        this.deleteProfileToolStripMenuItem.Enabled = true;
      }
      else
      {
        MugenProfile currentProfile = ProfileManager.MainObj().GetCurrentProfile();
        if (currentProfile == null)
          return;
        this.toolStripTextBox1.Text = "Profile name: " + currentProfile.GetProfileName();
        if (currentProfile.GetProfileName() == null || currentProfile.GetProfileName() == "" || (currentProfile.GetMugenExePath() == null || currentProfile.GetMugenExePath() == ""))
        {
          this.launchToolStripMenuItem.Enabled = false;
          this.launchQuickVSToolStripMenuItem.Enabled = false;
          this.launchAutoModeToolStripMenuItem.Enabled = false;
          this.restartToolStripMenuItem.Enabled = false;
          this.restart2ToolStripMenuItem.Enabled = false;
          this.openMugenStripMenuItem.Enabled = false;
          this.openSelectStripMenuItem.Enabled = false;
          this.openCharStripMenuItem.Enabled = false;
          this.quitToolStripMenuItem.Enabled = false;
          this.openProfileStripMenuItem.Enabled = true;
          if (currentProfile.GetProfileName() == null || currentProfile.GetProfileName() == "")
          {
            this.copyProfileStripMenuItem.Enabled = false;
            this.deleteProfileToolStripMenuItem.Enabled = false;
          }
          else
          {
            this.copyProfileStripMenuItem.Enabled = true;
            this.deleteProfileToolStripMenuItem.Enabled = true;
          }
          if (ProfileManager.MainObj().IsValidProfileNo(this._copyTargetProfileNo))
            this.pasteProfileStripMenuItem.Enabled = true;
          else
            this.pasteProfileStripMenuItem.Enabled = false;
        }
        else
        {
          this.launchToolStripMenuItem.Enabled = true;
          this.launchQuickVSToolStripMenuItem.Enabled = true;
          this.launchAutoModeToolStripMenuItem.Enabled = true;
          this.restartToolStripMenuItem.Enabled = true;
          this.restart2ToolStripMenuItem.Enabled = true;
          this.openMugenStripMenuItem.Enabled = true;
          this.openSelectStripMenuItem.Enabled = true;
          this.openCharStripMenuItem.Enabled = true;
          this.copyProfileStripMenuItem.Enabled = true;
          this.deleteProfileToolStripMenuItem.Enabled = true;
          this.quitToolStripMenuItem.Enabled = true;
          if (currentProfile.GetMugenSelectCfgPath() == null || currentProfile.GetMugenSelectCfgPath() == "")
            this.openSelectStripMenuItem.Enabled = false;
          if (currentProfile.IsAutoModeAvailable())
          {
            if (currentProfile.GetTempGameCount() >= 0 && !this._isAutoModeRunning)
            {
              this.restartToolStripMenuItem.Enabled = true;
              this.restart2ToolStripMenuItem.Enabled = true;
              this.launchAutoModeToolStripMenuItem.Text = "Restart continuous battle from the beginning.";
            }
            else
            {
              this.restartToolStripMenuItem.Enabled = false;
              this.restart2ToolStripMenuItem.Enabled = false;
              this.launchAutoModeToolStripMenuItem.Text = "Start continuous battle.";
            }
          }
          else
          {
            this.launchAutoModeToolStripMenuItem.Enabled = false;
            this.restartToolStripMenuItem.Enabled = false;
            this.restart2ToolStripMenuItem.Enabled = false;
          }
          switch (currentProfile.GetCurrentGameMode())
          {
            case MugenProfile.GameMode.QUICK_VS:
              this.quitToolStripMenuItem.Text = "Stop Quick Vs.";
              break;
            case MugenProfile.GameMode.AUTO_MODE:
              this.quitToolStripMenuItem.Text = "Stop continuous battle.";
              break;
            default:
              this.quitToolStripMenuItem.Text = "Close mugen.";
              break;
          }
          if (MugenWindow.MainObj().getMugenProcess() != null || this._isAutoModeRunning)
          {
            this.launchToolStripMenuItem.Enabled = false;
            this.launchQuickVSToolStripMenuItem.Enabled = false;
            this.launchAutoModeToolStripMenuItem.Enabled = false;
            this.restartToolStripMenuItem.Enabled = false;
            this.restart2ToolStripMenuItem.Enabled = false;
            if (this._selectedProfileNo == currentProfile.GetProfileNo())
            {
              this.openProfileStripMenuItem.Enabled = false;
              this.copyProfileStripMenuItem.Enabled = false;
              this.pasteProfileStripMenuItem.Enabled = false;
              this.deleteProfileToolStripMenuItem.Enabled = false;
              this.quitToolStripMenuItem.Enabled = true;
            }
            else
            {
              this.openProfileStripMenuItem.Enabled = true;
              this.copyProfileStripMenuItem.Enabled = true;
              this.deleteProfileToolStripMenuItem.Enabled = true;
              this.quitToolStripMenuItem.Enabled = false;
              if (ProfileManager.MainObj().IsValidProfileNo(this._copyTargetProfileNo))
              {
                if (this._copyTargetProfileNo == currentProfile.GetProfileNo())
                  this.pasteProfileStripMenuItem.Enabled = false;
                else
                  this.pasteProfileStripMenuItem.Enabled = true;
              }
              else
                this.pasteProfileStripMenuItem.Enabled = false;
            }
          }
          else
          {
            this.launchToolStripMenuItem.Enabled = true;
            this.launchQuickVSToolStripMenuItem.Enabled = true;
            if (currentProfile.IsAutoModeAvailable())
              this.launchAutoModeToolStripMenuItem.Enabled = true;
            else
              this.launchAutoModeToolStripMenuItem.Enabled = false;
            this.quitToolStripMenuItem.Enabled = false;
            this.openProfileStripMenuItem.Enabled = true;
            this.copyProfileStripMenuItem.Enabled = true;
            this.deleteProfileToolStripMenuItem.Enabled = true;
            if (ProfileManager.MainObj().IsValidProfileNo(this._copyTargetProfileNo))
            {
              if (this._copyTargetProfileNo == currentProfile.GetProfileNo())
                this.pasteProfileStripMenuItem.Enabled = false;
              else
                this.pasteProfileStripMenuItem.Enabled = true;
            }
            else
              this.pasteProfileStripMenuItem.Enabled = false;
          }
        }
      }
    }

    private void profileListBox_MouseDown(object sender, MouseEventArgs e)
    {
      if (e.Button != MouseButtons.Right)
        return;
      int index = this.profileListBox.IndexFromPoint(e.Location);
      if (index < 0 || index >= this.profileListBox.Items.Count)
        return;
      this.profileListBox.SetSelected(index, true);
      ProfileManager.MainObj().SetCurrentProfile(index + 1);
    }

    private void profileListBox_MouseUp(object sender, MouseEventArgs e)
    {
      if (e.Button != MouseButtons.Right)
        return;
      int index = this.profileListBox.IndexFromPoint(e.Location);
      if (index < 0 || index >= this.profileListBox.Items.Count)
        return;
      this.profileListBox.SetSelected(index, true);
      ProfileManager.MainObj().SetCurrentProfile(index + 1);
      MugenProfile currentProfile = ProfileManager.MainObj().GetCurrentProfile();
      if (currentProfile == null)
        return;
      this.toolStripTextBox1.Text = "Profile name: " + currentProfile.GetProfileName();
    }

    private void launchToolStripMenuItem_Click(object sender, EventArgs e)
    {
      Control contextMenuStrip = (Control) this.profileContextMenuStrip;
      if (contextMenuStrip != null && contextMenuStrip.Cursor == Cursors.Help)
      {
        this.profileContextMenuStrip.Show();
        string text = "Starts mugen using the specified profile settings." + Environment.NewLine;
        this.toolTip1.Hide((IWin32Window) contextMenuStrip);
        this.toolTip1.IsBalloon = false;
        this.toolTip1.IsBalloon = true;
        this.toolTip1.SetToolTip(contextMenuStrip, " ");
        this.toolTip1.Show(text, (IWin32Window) contextMenuStrip, this.launchToolStripMenuItem.Width / 2, this.launchToolStripMenuItem.Height * 4 / 2);
      }
      else
      {
        ProfileManager.MainObj().GetCurrentProfile()?.SetGameMode(MugenProfile.GameMode.NORMAL);
        this.LaunchMugen(true);
        MugenWindow.MainObj().ActivateEx();
      }
    }

    private void launchQuickVSToolStripMenuItem_Click(object sender, EventArgs e)
    {
      Control contextMenuStrip = (Control) this.profileContextMenuStrip;
      if (contextMenuStrip != null && contextMenuStrip.Cursor == Cursors.Help)
      {
        this.profileContextMenuStrip.Show();
        string text = "Starts Quick Vs. mode." + Environment.NewLine;
        this.toolTip1.Hide((IWin32Window) contextMenuStrip);
        this.toolTip1.IsBalloon = false;
        this.toolTip1.IsBalloon = true;
        this.toolTip1.SetToolTip(contextMenuStrip, " ");
        this.toolTip1.Show(text, (IWin32Window) contextMenuStrip, this.launchToolStripMenuItem.Width / 2, this.launchToolStripMenuItem.Height * 6 / 2);
      }
      else
      {
        ProfileManager.MainObj().GetCurrentProfile()?.SetGameMode(MugenProfile.GameMode.QUICK_VS);
        this.LaunchMugen(true);
        MugenWindow.MainObj().ActivateEx();
      }
    }

    private void launchAutoModeToolStripMenuItem_Click(object sender, EventArgs e)
    {
      Control contextMenuStrip = (Control) this.profileContextMenuStrip;
      if (contextMenuStrip != null && contextMenuStrip.Cursor == Cursors.Help)
      {
        this.profileContextMenuStrip.Show();
        string text = "Starts continuous battle mode." + Environment.NewLine;
        this.toolTip1.Hide((IWin32Window) contextMenuStrip);
        this.toolTip1.IsBalloon = false;
        this.toolTip1.IsBalloon = true;
        this.toolTip1.SetToolTip(contextMenuStrip, " ");
        this.toolTip1.Show(text, (IWin32Window) contextMenuStrip, this.launchToolStripMenuItem.Width / 2, this.launchToolStripMenuItem.Height * 9 / 2);
      }
      else
      {
        ProfileManager.MainObj().GetCurrentProfile()?.SetGameMode(MugenProfile.GameMode.AUTO_MODE);
        this.LaunchMugen(true);
        MugenWindow.MainObj().ActivateEx();
      }
    }

    private void restartToolStripMenuItem_Click(object sender, EventArgs e)
    {
      Control contextMenuStrip = (Control) this.profileContextMenuStrip;
      if (contextMenuStrip != null && contextMenuStrip.Cursor == Cursors.Help)
      {
        this.profileContextMenuStrip.Show();
        string text = "Used in continuous battle. Restarts continuous battle mode from the match that was cancelled." + Environment.NewLine;
        this.toolTip1.Hide((IWin32Window) contextMenuStrip);
        this.toolTip1.IsBalloon = false;
        this.toolTip1.IsBalloon = true;
        this.toolTip1.SetToolTip(contextMenuStrip, " ");
        this.toolTip1.Show(text, (IWin32Window) contextMenuStrip, this.launchToolStripMenuItem.Width / 2, this.launchToolStripMenuItem.Height * 11 / 2);
      }
      else if (this._selectedProfileNo != this.profileListBox.SelectedIndex + 1)
      {
        this.LaunchMugen(true);
        MugenWindow.MainObj().ActivateEx();
      }
      else
      {
        if (MugenWindow.MainObj().getMugenProcess() != null || this._isAutoModeRunning)
          return;
        ProfileManager.MainObj().SetCurrentProfile(this.profileListBox.SelectedIndex + 1);
        MugenProfile currentProfile = ProfileManager.MainObj().GetCurrentProfile();
        if (currentProfile != null)
        {
          if (currentProfile.IsAutoModeAvailable())
          {
            currentProfile.SetGameMode(MugenProfile.GameMode.AUTO_MODE);
            this._didMugenCrashed = false;
            currentProfile.SetIncremented();
            if (!currentProfile.PrepareNextMatch(currentProfile.GetTempGameCount()))
              this.LaunchMugen(true);
            else
              this.LaunchMugen(false);
          }
          else
          {
            currentProfile.SetGameMode(MugenProfile.GameMode.NORMAL);
            this.LaunchMugen(true);
          }
        }
        else
          this.LaunchMugen(true);
        MugenWindow.MainObj().ActivateEx();
      }
    }

    private void restart2ToolStripMenuItem_Click(object sender, EventArgs e)
    {
      Control contextMenuStrip = (Control) this.profileContextMenuStrip;
      if (contextMenuStrip != null && contextMenuStrip.Cursor == Cursors.Help)
      {
        this.profileContextMenuStrip.Show();
        string text = "Used in continuous battle. Skips the match that was cancelled and restarts from the next scheduled match." + Environment.NewLine;
        this.toolTip1.Hide((IWin32Window) contextMenuStrip);
        this.toolTip1.IsBalloon = false;
        this.toolTip1.IsBalloon = true;
        this.toolTip1.SetToolTip(contextMenuStrip, " ");
        this.toolTip1.Show(text, (IWin32Window) contextMenuStrip, this.launchToolStripMenuItem.Width / 2, this.launchToolStripMenuItem.Height * 13 / 2);
      }
      else if (this._selectedProfileNo != this.profileListBox.SelectedIndex + 1)
      {
        this.LaunchMugen(true);
        MugenWindow.MainObj().ActivateEx();
      }
      else
      {
        MugenProfile currentProfile = ProfileManager.MainObj().GetCurrentProfile();
        if (currentProfile != null)
        {
          if (currentProfile.IsAutoModeAvailable())
          {
            currentProfile.SetGameMode(MugenProfile.GameMode.AUTO_MODE);
            currentProfile.IncrementTempGameCount();
            MugenWindow.MainObj().SyncNumOfGames(currentProfile.GetTempGameCount() + 1);
            this._didMugenCrashed = false;
            LogManager.MainObj().appendLog("");
            LogManager.MainObj().appendLog("Restart from the next match.");
            currentProfile.SetIncremented();
            if (!currentProfile.PrepareNextMatch(currentProfile.GetTempGameCount()))
              this.LaunchMugen(true);
            else
              this.LaunchMugen(false);
          }
          else
          {
            currentProfile.SetGameMode(MugenProfile.GameMode.NORMAL);
            this.LaunchMugen(true);
          }
        }
        else
          this.LaunchMugen(true);
        MugenWindow.MainObj().ActivateEx();
      }
    }

    private void openSelectStripMenuItem_Click(object sender, EventArgs e)
    {
      Control contextMenuStrip = (Control) this.profileContextMenuStrip;
      if (contextMenuStrip != null && contextMenuStrip.Cursor == Cursors.Help)
      {
        this.profileContextMenuStrip.Show();
        string text = "Opens the select.def file in a text editor." + Environment.NewLine + Environment.NewLine + "(Note: Specifying the select.def is required for operation.)" + Environment.NewLine;
        this.toolTip1.Hide((IWin32Window) contextMenuStrip);
        this.toolTip1.IsBalloon = false;
        this.toolTip1.IsBalloon = true;
        this.toolTip1.SetToolTip(contextMenuStrip, " ");
        this.toolTip1.Show(text, (IWin32Window) contextMenuStrip, this.launchToolStripMenuItem.Width / 2, this.launchToolStripMenuItem.Height * 15 / 2);
      }
      else
      {
        MugenProfile currentProfile = ProfileManager.MainObj().GetCurrentProfile();
        if (currentProfile == null)
          return;
        string mugenSelectCfgPath = currentProfile.GetMugenSelectCfgPath();
        if (mugenSelectCfgPath == null)
          return;
        string textEditor = ProfileManager.MainObj().GetMainConfig().GetTextEditor();
        if (textEditor != null)
        {
          try
          {
            Process.Start(textEditor, mugenSelectCfgPath);
          }
          catch
          {
          }
        }
        else
        {
          try
          {
            Process.Start("notepad.exe", mugenSelectCfgPath);
          }
          catch
          {
          }
        }
      }
    }

    private void openMugenStripMenuItem_Click(object sender, EventArgs e)
    {
      Control contextMenuStrip = (Control) this.profileContextMenuStrip;
      if (contextMenuStrip != null && contextMenuStrip.Cursor == Cursors.Help)
      {
        this.profileContextMenuStrip.Show();
        string text = "Opens the mugen.cfg in a text editor." + Environment.NewLine;
        this.toolTip1.Hide((IWin32Window) contextMenuStrip);
        this.toolTip1.IsBalloon = false;
        this.toolTip1.IsBalloon = true;
        this.toolTip1.SetToolTip(contextMenuStrip, " ");
        this.toolTip1.Show(text, (IWin32Window) contextMenuStrip, this.launchToolStripMenuItem.Width / 2, this.launchToolStripMenuItem.Height * 17 / 2);
      }
      else
      {
        MugenProfile currentProfile = ProfileManager.MainObj().GetCurrentProfile();
        if (currentProfile == null)
          return;
        string mugenPath = currentProfile.GetMugenPath();
        if (mugenPath == null)
          return;
        string arguments = Path.Combine(mugenPath, "data\\mugen.cfg");
        string textEditor = ProfileManager.MainObj().GetMainConfig().GetTextEditor();
        if (textEditor != null)
        {
          try
          {
            Process.Start(textEditor, arguments);
          }
          catch
          {
          }
        }
        else
        {
          try
          {
            Process.Start("notepad.exe", arguments);
          }
          catch
          {
          }
        }
      }
    }

    private void openCharStripMenuItem_Click(object sender, EventArgs e)
    {
      Control contextMenuStrip = (Control) this.profileContextMenuStrip;
      if (contextMenuStrip != null && contextMenuStrip.Cursor == Cursors.Help)
      {
        this.profileContextMenuStrip.Show();
        string text = "Opens the chars folder belonging to the mugen installation." + Environment.NewLine;
        this.toolTip1.Hide((IWin32Window) contextMenuStrip);
        this.toolTip1.IsBalloon = false;
        this.toolTip1.IsBalloon = true;
        this.toolTip1.SetToolTip(contextMenuStrip, " ");
        this.toolTip1.Show(text, (IWin32Window) contextMenuStrip, this.launchToolStripMenuItem.Width / 2, this.launchToolStripMenuItem.Height * 19 / 2);
      }
      else
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
        startInfo.ErrorDialog = true;
        startInfo.ErrorDialogParentHandle = this.Handle;
        startInfo.Verb = "Open";
        try
        {
          Process.Start(startInfo);
        }
        catch
        {
        }
      }
    }

    private void openProfileStripMenuItem_Click(object sender, EventArgs e)
    {
      Control contextMenuStrip = (Control) this.profileContextMenuStrip;
      if (contextMenuStrip != null && contextMenuStrip.Cursor == Cursors.Help)
      {
        this.profileContextMenuStrip.Show();
        string text = "Opens the profile settings dialog." + Environment.NewLine;
        this.toolTip1.Hide((IWin32Window) contextMenuStrip);
        this.toolTip1.IsBalloon = false;
        this.toolTip1.IsBalloon = true;
        this.toolTip1.SetToolTip(contextMenuStrip, " ");
        this.toolTip1.Show(text, (IWin32Window) contextMenuStrip, this.launchToolStripMenuItem.Width / 2, this.launchToolStripMenuItem.Height * 22 / 2);
      }
      else
      {
        MugenProfile currentProfile = ProfileManager.MainObj().GetCurrentProfile();
        if (currentProfile == null)
          return;
        ProfileConfigPanel profileConfigPanel = new ProfileConfigPanel();
        if (profileConfigPanel == null)
          return;
        int num = (int) profileConfigPanel.ShowDialog();
        profileConfigPanel.Dispose();
        if (ProfileManager.MainObj().GetCurrentProfile() == null)
          return;
        int selectedIndex = this.profileListBox.SelectedIndex;
        string str = currentProfile.GetProfileName();
        if (str == null || str == "")
          str = "(New Profile)";
        this.profileListBox.Items[selectedIndex] = (object) str;
      }
    }

    private void reloadStripMenuItem_Click(object sender, EventArgs e)
    {
      MugenProfile currentProfile = ProfileManager.MainObj().GetCurrentProfile();
      if (currentProfile == null)
        return;
      currentProfile.ReLoad();
      this.profileListBox.Items[this.profileListBox.SelectedIndex] = (object) currentProfile.GetProfileName();
    }

    private void profileListBox_KeyUp(object sender, KeyEventArgs e)
    {
      if (e.KeyValue != 13)
        return;
      if (this.profileListBox.Cursor == Cursors.Help)
      {
        this.profileListBox_ShowHelp();
      }
      else
      {
        ProfileManager.MainObj().SetCurrentProfile(this.profileListBox.SelectedIndex + 1);
        MugenProfile currentProfile = ProfileManager.MainObj().GetCurrentProfile();
        if (currentProfile == null || currentProfile.GetProfileName() == null || (currentProfile.GetProfileName() == "" || currentProfile.GetMugenExePath() == null) || currentProfile.GetMugenExePath() == "")
        {
          ProfileConfigPanel profileConfigPanel = new ProfileConfigPanel();
          if (profileConfigPanel == null)
            return;
          int num = (int) profileConfigPanel.ShowDialog();
          profileConfigPanel.Dispose();
          if (ProfileManager.MainObj().GetCurrentProfile() == null)
            return;
          int selectedIndex = this.profileListBox.SelectedIndex;
          string str = currentProfile.GetProfileName();
          if (str == null || str == "")
            str = "(New Profile)";
          this.profileListBox.Items[selectedIndex] = (object) str;
        }
        else
        {
          if (MugenWindow.MainObj().getMugenProcess() != null || this._isAutoModeRunning)
            return;
          if (currentProfile != null)
          {
            switch (currentProfile.GetDefaultGameMode())
            {
              case MugenProfile.GameMode.QUICK_VS:
                currentProfile.SetGameMode(MugenProfile.GameMode.QUICK_VS);
                this.LaunchMugen(true);
                break;
              case MugenProfile.GameMode.AUTO_MODE:
                if (currentProfile.IsAutoModeAvailable())
                {
                  currentProfile.SetGameMode(MugenProfile.GameMode.AUTO_MODE);
                  this._didMugenCrashed = false;
                  currentProfile.SetIncremented();
                  if (!currentProfile.PrepareNextMatch(currentProfile.GetTempGameCount()))
                  {
                    this.LaunchMugen(true);
                    break;
                  }
                  this.LaunchMugen(false);
                  break;
                }
                currentProfile.SetGameMode(MugenProfile.GameMode.NORMAL);
                this.LaunchMugen(true);
                break;
              default:
                currentProfile.SetGameMode(MugenProfile.GameMode.NORMAL);
                this.LaunchMugen(true);
                break;
            }
          }
          MugenWindow.MainObj().ActivateEx();
        }
      }
    }

    private void autoModeTimer_Tick(object sender, EventArgs e)
    {
      this.autoModeTimer.Stop();
      if (!this._isAutoModeRunning)
        return;
      int num1 = (int) MainForm.SetThreadExecutionState(3U);
      if (MugenWindow.MainObj().getMugenProcess() == null)
        MugenWindow.MainObj().SetTitleActive(false);
      else if (MugenWindow.MainObj().CheckMugenError())
      {
        this._didMugenCrashed = true;
        MugenWindow.MainObj().SetTitleActive(false);
      }
      else
        MugenWindow.MainObj().UpdateTitle();
      MugenProfile profile = ProfileManager.MainObj().GetProfile(this._selectedProfileNo);
      MugenWindow mugenWindow = MugenWindow.MainObj();
      if (profile != null && mugenWindow != null)
      {
        if (profile.IsAutoMode() && mugenWindow.IsMugenCrashed())
          this._didMugenCrashed = true;
        if (mugenWindow.getMugenProcess() == null)
        {
          if (mugenWindow.IsGameQuitted() && (!this._didMugenCrashed || this._didAutoModeQuitted))
          {
            ResumeConfirmForm.Result result = ResumeConfirmForm.Result.QUIT;
            if (!this._didAutoModeQuitted && !this._isClosing)
            {
              ResumeConfirmForm resumeConfirmForm = new ResumeConfirmForm();
              if (resumeConfirmForm != null)
              {
                int num2 = (int) resumeConfirmForm.ShowDialog();
                result = resumeConfirmForm.GetResult();
                resumeConfirmForm.Dispose();
              }
            }
            this._didAutoModeQuitted = false;
            this.EnableQuitButton(false);
            this._isAutoModeRunning = false;
            this._retryCount = 0;
            mugenWindow.ResetGameQuitted();
            profile.SetIncremented();
            switch (result)
            {
              case ResumeConfirmForm.Result.NONE:
              case ResumeConfirmForm.Result.QUIT:
                mugenWindow.SyncNumOfGames(profile.GetTempGameCount() + 1);
                LogManager.MainObj().appendLog("");
                LogManager.MainObj().appendLog("Stop continuous battle mode.");
                return;
              case ResumeConfirmForm.Result.RESUME:
                this._didMugenCrashed = false;
                profile.SetIncremented();
                if (!profile.PrepareNextMatch(profile.GetTempGameCount()))
                {
                  this.LaunchMugen(true);
                  return;
                }
                this.LaunchMugen(false);
                return;
              case ResumeConfirmForm.Result.RESUME_NEXT:
                profile.IncrementTempGameCount();
                mugenWindow.SyncNumOfGames(profile.GetTempGameCount() + 1);
                LogManager.MainObj().appendLog("");
                LogManager.MainObj().appendLog("Restart from next match.");
                this._didMugenCrashed = false;
                if (!profile.PrepareNextMatch(profile.GetTempGameCount()))
                {
                  this.LaunchMugen(true);
                  return;
                }
                this.LaunchMugen(false);
                return;
              default:
                return;
            }
          }
          else
          {
            if (!this._didMugenCrashed || this._retryCount >= profile.GetErrorRetryCount() && !this._isRetrying)
            {
              if (this._didMugenCrashed)
                profile.IncrementTempGameCount();
              if (mugenWindow.IsBusyMugen() && !profile.CheckIncremented(false))
              {
                ++this._autoModeRetryCount;
                if (this._autoModeRetryCount < 60)
                {
                  this.autoModeTimer.Start();
                  return;
                }
              }
              this._autoModeRetryCount = 0;
              profile.CheckIncremented(true);
              mugenWindow.SyncNumOfGames(profile.GetTempGameCount() + 1);
              this._retryCount = 0;
              this._didMugenCrashed = false;
              if (!profile.PrepareNextMatch(profile.GetTempGameCount()))
              {
                profile.SetTempGameCount(-1);
                this.EnableQuitButton(false);
                this._isAutoModeRunning = false;
                profile.SetIncremented();
                mugenWindow.ResetGameQuitted();
                LogManager.MainObj().appendLog("");
                LogManager.MainObj().appendLog("The continuous battle schedule has finished.");
                if (File.Exists(MainForm.GetFullPath("gameover.wav")))
                {
                  new SoundPlayer(MainForm.GetFullPath("gameover.wav"))?.Play();
                  return;
                }
                SystemSounds.Exclamation.Play();
                return;
              }
            }
            else if (this._didMugenCrashed)
            {
              this._didMugenCrashed = false;
              this._didAutoModeQuitted = false;
              mugenWindow.ResetGameQuitted();
              if (!mugenWindow.SyncNumOfGames(profile.GetTempGameCount() + 1) && !this._isRetrying)
              {
                ++this._retryCount;
                if (this._retryCount <= profile.GetErrorRetryCount())
                {
                  this._isRetrying = true;
                  mugenWindow.SetRetryGame();
                  LogManager.MainObj().appendLog("Retry running the match.");
                }
              }
              profile.PrepareNextMatch(profile.GetTempGameCount());
            }
            this._LaunchMugen();
          }
        }
        else if (profile.IsAutoMode() && mugenWindow.IsMugenCrashed())
          this._didMugenCrashed = true;
      }
      this.autoModeTimer.Start();
    }

    private void quitToolStripMenuItem_Click(object sender, EventArgs e)
    {
      Control contextMenuStrip = (Control) this.profileContextMenuStrip;
      if (contextMenuStrip != null && contextMenuStrip.Cursor == Cursors.Help)
      {
        this.profileContextMenuStrip.Show();
        string text = "Stops running mugen. If mugen is being ran in continuous battle mode then this will cancel the current match." + Environment.NewLine;
        this.toolTip1.Hide((IWin32Window) contextMenuStrip);
        this.toolTip1.IsBalloon = false;
        this.toolTip1.IsBalloon = true;
        this.toolTip1.SetToolTip(contextMenuStrip, " ");
        this.toolTip1.Show(text, (IWin32Window) contextMenuStrip, this.launchToolStripMenuItem.Width / 2, this.launchToolStripMenuItem.Height * 31 / 2);
      }
      else if (!this._isAutoModeRunning)
      {
        this.CloseMugen();
        MugenWindow.MainObj().ResetGameQuitted();
      }
      else
      {
        this._didAutoModeQuitted = true;
        MugenWindow.MainObj().SetGameQuitted();
        if (MugenWindow.MainObj().getMugenProcess() == null)
          return;
        this.CloseMugen();
      }
    }

    private void MainForm_KeyPress(object sender, KeyPressEventArgs e)
    {
      if (e.KeyChar != '\x001B')
        return;
      if (MugenWindow.MainObj().getMugenProcess() != null)
        MugenWindow.MainObj().InjectESC();
      else if (this._isAutoModeRunning)
        MugenWindow.MainObj().SetGameQuitted();
      e.Handled = true;
    }

    private void app_Idle(object sender, EventArgs e) => this._disableQuitButton = false;

    private void quitButton_Click(object sender, EventArgs e)
    {
      Control quitButton = (Control) this.quitButton;
      if (quitButton != null && quitButton.Cursor == Cursors.Help)
      {
        string text = "Starts running mugen in continuous battle mode with the selected profile." + Environment.NewLine + "If the profile is empty, the profile settings menu will open instead." + Environment.NewLine;
        this.toolTip1.Hide((IWin32Window) quitButton);
        this.toolTip1.IsBalloon = false;
        this.toolTip1.IsBalloon = true;
        this.toolTip1.SetToolTip(quitButton, " ");
        this.toolTip1.Show(text, (IWin32Window) quitButton, quitButton.Width / 2, quitButton.Height / 2);
      }
      else
      {
        if (this._disableQuitButton)
          return;
        this._disableQuitButton = true;
        this.ActivateAll(true);
        if (MugenWindow.MainObj().getMugenProcess() == null && !this._isAutoModeRunning)
        {
          ProfileManager.MainObj().SetCurrentProfile(this.profileListBox.SelectedIndex + 1);
          MugenProfile currentProfile1 = ProfileManager.MainObj().GetCurrentProfile();
          if (currentProfile1 == null || currentProfile1.GetProfileName() == null || (currentProfile1.GetProfileName() == "" || currentProfile1.GetMugenExePath() == null) || (currentProfile1.GetMugenExePath() == "" || !File.Exists(currentProfile1.GetMugenExePath())))
          {
            ProfileConfigPanel profileConfigPanel = new ProfileConfigPanel();
            if (profileConfigPanel == null)
              return;
            int num = (int) profileConfigPanel.ShowDialog();
            profileConfigPanel.Dispose();
            MugenProfile currentProfile2 = ProfileManager.MainObj().GetCurrentProfile();
            if (currentProfile2 == null)
              return;
            int selectedIndex = this.profileListBox.SelectedIndex;
            string str = currentProfile2.GetProfileName();
            if (str == null || str == "")
              str = "(New Profile)";
            this.profileListBox.Items[selectedIndex] = (object) str;
          }
          else
          {
            if (MugenWindow.MainObj().getMugenProcess() != null || this._isAutoModeRunning)
              return;
            if (currentProfile1 != null)
            {
              switch (currentProfile1.GetDefaultGameMode())
              {
                case MugenProfile.GameMode.QUICK_VS:
                  currentProfile1.SetGameMode(MugenProfile.GameMode.QUICK_VS);
                  this.LaunchMugen(true);
                  break;
                case MugenProfile.GameMode.AUTO_MODE:
                  if (currentProfile1.IsAutoModeAvailable())
                  {
                    currentProfile1.SetGameMode(MugenProfile.GameMode.AUTO_MODE);
                    this._didMugenCrashed = false;
                    currentProfile1.SetIncremented();
                    if (!currentProfile1.PrepareNextMatch(currentProfile1.GetTempGameCount()))
                    {
                      this.LaunchMugen(true);
                      break;
                    }
                    this.LaunchMugen(false);
                    break;
                  }
                  currentProfile1.SetGameMode(MugenProfile.GameMode.NORMAL);
                  this.LaunchMugen(true);
                  break;
                default:
                  currentProfile1.SetGameMode(MugenProfile.GameMode.NORMAL);
                  this.LaunchMugen(true);
                  break;
              }
            }
            MugenWindow.MainObj().ActivateEx();
          }
        }
        else if (!this._isAutoModeRunning)
        {
          this.CloseMugen();
          MugenWindow.MainObj().ResetGameQuitted();
        }
        else
        {
          this._didAutoModeQuitted = true;
          MugenWindow.MainObj().SetGameQuitted();
          if (MugenWindow.MainObj().getMugenProcess() == null)
            return;
          this.CloseMugen();
        }
      }
    }

    private void startButton_Click(object sender, EventArgs e) => this.profileContextMenuStrip.Show((Control) this.startButton, 0, 0);

    private void copyProfileStripMenuItem_Click(object sender, EventArgs e)
    {
      Control contextMenuStrip = (Control) this.profileContextMenuStrip;
      if (contextMenuStrip != null && contextMenuStrip.Cursor == Cursors.Help)
      {
        this.profileContextMenuStrip.Show();
        string text = "Copies the settings of the selected profile." + Environment.NewLine;
        this.toolTip1.Hide((IWin32Window) contextMenuStrip);
        this.toolTip1.IsBalloon = false;
        this.toolTip1.IsBalloon = true;
        this.toolTip1.SetToolTip(contextMenuStrip, " ");
        this.toolTip1.Show(text, (IWin32Window) contextMenuStrip, this.launchToolStripMenuItem.Width / 2, this.launchToolStripMenuItem.Height * 24 / 2);
      }
      else
      {
        ProfileManager.MainObj().SetCurrentProfile(this.profileListBox.SelectedIndex + 1);
        MugenProfile currentProfile = ProfileManager.MainObj().GetCurrentProfile();
        if (currentProfile == null)
          return;
        switch (currentProfile.GetProfileName())
        {
          case "":
            break;
          default:
            this._copyTargetProfileNo = this.profileListBox.SelectedIndex + 1;
            break;
        }
      }
    }

    private void pasteProfileStripMenuItem_Click(object sender, EventArgs e)
    {
      Control contextMenuStrip = (Control) this.profileContextMenuStrip;
      if (contextMenuStrip != null && contextMenuStrip.Cursor == Cursors.Help)
      {
        this.profileContextMenuStrip.Show();
        string text = "Overwrites the selected profile with the information from the copied one." + Environment.NewLine + "(A confirm dialog will appear before deletion.)" + Environment.NewLine;
        this.toolTip1.Hide((IWin32Window) contextMenuStrip);
        this.toolTip1.IsBalloon = false;
        this.toolTip1.IsBalloon = true;
        this.toolTip1.SetToolTip(contextMenuStrip, " ");
        this.toolTip1.Show(text, (IWin32Window) contextMenuStrip, this.launchToolStripMenuItem.Width / 2, this.launchToolStripMenuItem.Height * 26 / 2);
      }
      else if (!ProfileManager.MainObj().IsValidProfileNo(this._copyTargetProfileNo))
      {
        this.pasteProfileStripMenuItem.Enabled = false;
      }
      else
      {
        if (this._copyTargetProfileNo == this.profileListBox.SelectedIndex + 1)
          return;
        MugenProfile profile = ProfileManager.MainObj().GetProfile(this._copyTargetProfileNo);
        if (profile == null || profile.GetProfileName() == null || profile.GetProfileName() == "")
        {
          this.pasteProfileStripMenuItem.Enabled = false;
        }
        else
        {
          ProfileManager.MainObj().SetCurrentProfile(this.profileListBox.SelectedIndex + 1);
          MugenProfile currentProfile = ProfileManager.MainObj().GetCurrentProfile();
          if (currentProfile == null)
            return;
          string str = currentProfile.GetProfileName();
          if (str == null || str == "")
            str = "(New Profile)";
          if (MessageBox.Show("The '" + str + "' will be overwritten. Do you wish to continue?", "Swiss Army Knife", MessageBoxButtons.OKCancel) != DialogResult.OK)
            return;
          currentProfile.ResetConf();
          currentProfile.SetTempGameCount(-1);
          currentProfile.MakeBackup();
          currentProfile.CopyConfig((ProfileBase) profile);
          currentProfile.ReLoad();
          this.profileListBox.Items[this.profileListBox.SelectedIndex] = (object) currentProfile.GetProfileName();
        }
      }
    }

    private void deleteProfileToolStripMenuItem_Click(object sender, EventArgs e)
    {
      Control contextMenuStrip = (Control) this.profileContextMenuStrip;
      if (contextMenuStrip != null && contextMenuStrip.Cursor == Cursors.Help)
      {
        this.profileContextMenuStrip.Show();
        string text = "This will delete a registered profile." + Environment.NewLine + "(A confirm dialog will appear before deletion.)" + Environment.NewLine;
        this.toolTip1.Hide((IWin32Window) contextMenuStrip);
        this.toolTip1.IsBalloon = false;
        this.toolTip1.IsBalloon = true;
        this.toolTip1.SetToolTip(contextMenuStrip, " ");
        this.toolTip1.Show(text, (IWin32Window) contextMenuStrip, this.launchToolStripMenuItem.Width / 2, this.launchToolStripMenuItem.Height * 28 / 2);
      }
      else
      {
        ProfileManager.MainObj().SetCurrentProfile(this.profileListBox.SelectedIndex + 1);
        MugenProfile currentProfile = ProfileManager.MainObj().GetCurrentProfile();
        if (currentProfile == null || currentProfile.GetProfileName() == null || MessageBox.Show("The '" + currentProfile.GetProfileName() + "' profile will be deleted. Do you wish to continue?", "Swiss Army Knife", MessageBoxButtons.OKCancel) != DialogResult.OK)
          return;
        currentProfile.ResetConf();
        currentProfile.SetTempGameCount(-1);
        currentProfile.MakeBackup();
        currentProfile.SaveConfigText("");
        this.profileListBox.Items[this.profileListBox.SelectedIndex] = (object) "(New Profile)";
        this.deleteProfileToolStripMenuItem.Enabled = false;
        if (this.profileListBox.SelectedIndex + 1 != this._copyTargetProfileNo)
          return;
        this._copyTargetProfileNo = 0;
        this.pasteProfileStripMenuItem.Enabled = false;
      }
    }

    private void MainForm_Resize(object sender, EventArgs e)
    {
      if (MugenWindow.MainObj().isDebugBreakMode())
        return;
      if (this.WindowState == FormWindowState.Minimized)
      {
        if (MugenWindow.MainObj().Visible)
        {
          MugenWindow.MainObj().WindowState = FormWindowState.Minimized;
          MugenWindow.MainObj().Hide();
        }
        if (LogManager.MainObj().Visible)
        {
          LogManager.MainObj().WindowState = FormWindowState.Minimized;
          LogManager.MainObj().Hide();
        }
        if (DebugForm.MainObj().Visible)
        {
          DebugForm.MainObj().WindowState = FormWindowState.Minimized;
          DebugForm.MainObj().Hide();
        }
        if (!VarForm.MainObj().Visible)
          return;
        VarForm.MainObj().WindowState = FormWindowState.Minimized;
        VarForm.MainObj().Hide();
      }
      else
      {
        if (this.WindowState != FormWindowState.Normal)
          return;
        if (LogManager.MainObj().WindowState == FormWindowState.Minimized)
        {
          LogManager.MainObj().Show();
          LogManager.MainObj().WindowState = FormWindowState.Normal;
          LogManager.MainObj().Activate();
        }
        if (DebugForm.MainObj().WindowState == FormWindowState.Minimized)
        {
          DebugForm.MainObj().Show();
          DebugForm.MainObj().WindowState = FormWindowState.Normal;
          DebugForm.MainObj().Activate();
        }
        if (VarForm.MainObj().WindowState == FormWindowState.Minimized)
        {
          VarForm.MainObj().Show();
          VarForm.MainObj().WindowState = FormWindowState.Normal;
          VarForm.MainObj().Activate();
        }
        if (MugenWindow.MainObj().WindowState != FormWindowState.Minimized)
          return;
        MugenWindow.MainObj().Show();
        MugenWindow.MainObj().ShowMugen();
        MugenWindow.MainObj().WindowState = FormWindowState.Normal;
        MugenWindow.MainObj().Activate();
      }
    }

    private void ToggleHelpMode()
    {
      this.toolTip1.Hide((IWin32Window) this);
      this.toolTip1.SetToolTip((Control) this, " ");
      this.toolTip1.IsBalloon = false;
      this.toolTip1.IsBalloon = true;
      if (!this._isHelpMode)
      {
        this._isHelpMode = true;
        this.Cursor = Cursors.Help;
        this.profileListBox.Cursor = Cursors.Help;
        this.startButton.Cursor = Cursors.Help;
        this.quitButton.Cursor = Cursors.Help;
        this.profileContextMenuStrip.Cursor = Cursors.Help;
        this.ファイルFToolStripMenuItem.DropDown.Cursor = Cursors.Help;
        this.ツールTToolStripMenuItem.DropDown.Cursor = Cursors.Help;
        MugenWindow.MainObj().SetHelpMode(true);
        LogManager.MainObj().SetHelpMode(true);
        DebugForm.MainObj().SetHelpMode(true);
        VarForm.MainObj().SetHelpMode(true);
        this.toolTip1.Show("Help mode has been activated. Please click the items you would like to learn more about." + Environment.NewLine + "Click the help button again to disable the help function at any time.", (IWin32Window) this, this.Width, 60, 5000);
      }
      else
      {
        this._isHelpMode = false;
        this.Cursor = Cursors.Default;
        this.profileListBox.Cursor = Cursors.Default;
        this.startButton.Cursor = Cursors.Default;
        this.quitButton.Cursor = Cursors.Default;
        this.profileContextMenuStrip.Cursor = Cursors.Default;
        this.ファイルFToolStripMenuItem.DropDown.Cursor = Cursors.Default;
        this.ツールTToolStripMenuItem.DropDown.Cursor = Cursors.Default;
        MugenWindow.MainObj().SetHelpMode(false);
        LogManager.MainObj().SetHelpMode(false);
        DebugForm.MainObj().SetHelpMode(false);
        VarForm.MainObj().SetHelpMode(false);
        this.toolTip1.Show("Help mode has been disabled.", (IWin32Window) this, this.Width, 60, 1000);
      }
    }

    private void toolStripMenuItem1_Click(object sender, EventArgs e) => this.ToggleHelpMode();

    private void ツールTToolStripMenuItem_DropDownOpening(object sender, EventArgs e)
    {
      this.mugenWinToolStripMenuItem.Checked = MugenWindow.MainObj().Visible;
      this.logWinToolStripMenuItem.Checked = LogManager.MainObj().Visible;
      this.debugWinToolStripMenuItem.Checked = DebugForm.MainObj().Visible;
      this.varWinStripMenuItem.Checked = VarForm.MainObj().Visible;
    }

    private void profileListBox_ShowHelp()
    {
      if (!(this.profileListBox.Cursor == Cursors.Help))
        return;
      string text = "This is the profile list. 'Profiles' are used to specify the parameters and settings to run mugen with." + Environment.NewLine + "A filepath to the mugen executable and command line arguments can also be specified." + Environment.NewLine + "With profiles, you can run many customized builds of mugen to improve organization!" + Environment.NewLine + "(You can specify up to 8 profiles." + Environment.NewLine + Environment.NewLine + "Additionaly, you can see more commands by using the right click button." + Environment.NewLine + Environment.NewLine + "The profiles marked as (New Profile) are empty and can be used to store a new profile." + Environment.NewLine + "By double clicking it, you can access the settings for registering a new profile." + Environment.NewLine + Environment.NewLine + "The names of the registered profiles are displayed.（Example: 'The normal setup','Evil characters only'）" + Environment.NewLine + "By double clicking a registered profile, you can edit its settings." + Environment.NewLine;
      this.toolTip1.Hide((IWin32Window) this.profileListBox);
      this.toolTip1.IsBalloon = false;
      this.toolTip1.IsBalloon = true;
      this.toolTip1.SetToolTip((Control) this.profileListBox, " ");
      this.toolTip1.Show(text, (IWin32Window) this.profileListBox, this.profileListBox.Width, this.profileListBox.Height / 2);
    }

    private void profileListBox_Click(object sender, EventArgs e)
    {
      if (!(this.profileListBox.Cursor == Cursors.Help))
        return;
      this.profileListBox_ShowHelp();
    }

    private void readmeToolStripMenuItem_Click(object sender, EventArgs e)
    {
      if (ProfileManager.MainObj().GetCurrentProfile() == null)
        return;
      string textEditor = ProfileManager.MainObj().GetMainConfig().GetTextEditor();
      if (textEditor != null)
      {
        try
        {
          Process.Start(textEditor, MainForm.GetFullPath("主な機能の説明.txt"));
        }
        catch
        {
        }
      }
      else
      {
        try
        {
          Process.Start("notepad.exe", MainForm.GetFullPath("主な機能の説明.txt"));
        }
        catch
        {
        }
      }
    }

    private void ファイルFToolStripMenuItem_MouseUp(object sender, MouseEventArgs e)
    {
      if (!this.ファイルFToolStripMenuItem.DropDown.Visible)
      {
        if (!this._isMenuVisible)
        {
          this._isMenuVisible = true;
          this.ファイルFToolStripMenuItem.ShowDropDown();
        }
        else
          this._isMenuVisible = false;
      }
      else if (this._isMenuVisible)
      {
        this._isMenuVisible = false;
        this.ファイルFToolStripMenuItem.HideDropDown();
      }
      else
        this._isMenuVisible = true;
    }

    private void ツールTToolStripMenuItem_MouseUp(object sender, MouseEventArgs e)
    {
      if (!this.ツールTToolStripMenuItem.DropDown.Visible)
      {
        if (!this._isMenuVisible)
        {
          this._isMenuVisible = true;
          this.ツールTToolStripMenuItem.ShowDropDown();
        }
        else
          this._isMenuVisible = false;
      }
      else if (this._isMenuVisible)
      {
        this._isMenuVisible = false;
        this.ツールTToolStripMenuItem.HideDropDown();
      }
      else
        this._isMenuVisible = true;
    }

    private void ヘルプHToolStripMenuItem_MouseUp(object sender, MouseEventArgs e)
    {
      if (!this.ヘルプHToolStripMenuItem.DropDown.Visible)
      {
        if (!this._isMenuVisible)
        {
          this._isMenuVisible = true;
          this.ヘルプHToolStripMenuItem.ShowDropDown();
        }
        else
          this._isMenuVisible = false;
      }
      else if (this._isMenuVisible)
      {
        this._isMenuVisible = false;
        this.ヘルプHToolStripMenuItem.HideDropDown();
      }
      else
        this._isMenuVisible = true;
    }

    private void helpBtnStripMenuItem_Click(object sender, EventArgs e)
    {
      this.ToggleHelpMode();
      if (this._isHelpMode)
        this.helpToolStripMenuItem.Checked = true;
      else
        this.helpToolStripMenuItem.Checked = false;
    }

    private void helpBtnStripMenuItem_MouseHover(object sender, EventArgs e)
    {
      if (this._isHelpMode)
        return;
      this.toolTip1.Hide((IWin32Window) this);
      this.toolTip1.IsBalloon = false;
      this.toolTip1.SetToolTip((Control) this, " ");
      this.toolTip1.Show("Help Button", (IWin32Window) this, this.Width, this.helpBtnStripMenuItem.Height, 1000);
    }
  }
}
