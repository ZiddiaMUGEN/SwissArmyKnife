// Decompiled with JetBrains decompiler
// Type: SwissArmyKnifeForMugen.MainForm
// Assembly: SAKnifeWM, Version=1.0.8.0, Culture=neutral, PublicKeyToken=null
// MVID: 09478AD8-365C-4BF3-BEA1-B5785151259B
// Assembly location: C:\Users\ziddi\Downloads\Swiss Army Knife 1.1 Conversion\Swiss Army Knife 1.1 Conversion\SAKnifeWM.exe

using SwissArmyKnifeForMugen.Configs;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Media;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace SwissArmyKnifeForMugen.Displays
{
    /// <summary>
    /// main window for SAK, this handles a decent amount of logic.
    /// </summary>
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
        private bool _wasBtnDown;
        private int _autoModeRetryCount;
        private int _copyTargetProfileNo;
        private bool _isHelpMode;

        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null)
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            components = new Container();
            ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(MainForm));
            watchTimer = new Timer(components);
            fileMenuStrip = new MenuStrip();
            ファイルFToolStripMenuItem = new ToolStripMenuItem();
            configToolStripMenuItem = new ToolStripMenuItem();
            toolStripSeparator2 = new ToolStripSeparator();
            exitAllToolStripMenuItem = new ToolStripMenuItem();
            ツールTToolStripMenuItem = new ToolStripMenuItem();
            activeteStripMenuItem = new ToolStripMenuItem();
            arrangeWinStripMenuItem = new ToolStripMenuItem();
            toolStripSeparator1 = new ToolStripSeparator();
            mugenWinToolStripMenuItem = new ToolStripMenuItem();
            logWinToolStripMenuItem = new ToolStripMenuItem();
            debugWinToolStripMenuItem = new ToolStripMenuItem();
            varWinStripMenuItem = new ToolStripMenuItem();
            ヘルプHToolStripMenuItem = new ToolStripMenuItem();
            helpToolStripMenuItem = new ToolStripMenuItem();
            readmeToolStripMenuItem = new ToolStripMenuItem();
            versionAToolStripMenuItem = new ToolStripMenuItem();
            helpBtnStripMenuItem = new ToolStripMenuItem();
            profileListBox = new ListBox();
            profileContextMenuStrip = new ContextMenuStrip(components);
            toolStripTextBox1 = new ToolStripTextBox();
            toolStripSeparator6 = new ToolStripSeparator();
            launchToolStripMenuItem = new ToolStripMenuItem();
            toolStripSeparator9 = new ToolStripSeparator();
            launchQuickVSToolStripMenuItem = new ToolStripMenuItem();
            toolStripSeparator8 = new ToolStripSeparator();
            launchAutoModeToolStripMenuItem = new ToolStripMenuItem();
            restartToolStripMenuItem = new ToolStripMenuItem();
            restart2ToolStripMenuItem = new ToolStripMenuItem();
            toolStripSeparator3 = new ToolStripSeparator();
            openSelectStripMenuItem = new ToolStripMenuItem();
            openMugenStripMenuItem = new ToolStripMenuItem();
            openCharStripMenuItem = new ToolStripMenuItem();
            toolStripSeparator4 = new ToolStripSeparator();
            openProfileStripMenuItem = new ToolStripMenuItem();
            copyProfileStripMenuItem = new ToolStripMenuItem();
            pasteProfileStripMenuItem = new ToolStripMenuItem();
            toolStripSeparator7 = new ToolStripSeparator();
            deleteProfileToolStripMenuItem = new ToolStripMenuItem();
            toolStripSeparator5 = new ToolStripSeparator();
            quitToolStripMenuItem = new ToolStripMenuItem();
            autoModeTimer = new Timer(components);
            quitButton = new Button();
            startButton = new Button();
            toolTip1 = new ToolTip(components);
            delayTimer = new Timer(components);
            fileMenuStrip.SuspendLayout();
            profileContextMenuStrip.SuspendLayout();
            SuspendLayout();
            watchTimer.Interval = 500;
            watchTimer.Tick += new EventHandler(watchTimer_Tick);
            fileMenuStrip.ImageScalingSize = new Size(24, 24);
            fileMenuStrip.Items.AddRange(new ToolStripItem[4]
            {
         ファイルFToolStripMenuItem,
         ツールTToolStripMenuItem,
         ヘルプHToolStripMenuItem,
         helpBtnStripMenuItem
            });
            fileMenuStrip.Location = new Point(3, 3);
            fileMenuStrip.Name = "fileMenuStrip";
            fileMenuStrip.Padding = new Padding(6, 0, 0, 2);
            fileMenuStrip.Size = new Size(277, 24);
            fileMenuStrip.TabIndex = 3;
            fileMenuStrip.Text = "File";
            ファイルFToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[3]
            {
         configToolStripMenuItem,
         toolStripSeparator2,
         exitAllToolStripMenuItem
            });
            ファイルFToolStripMenuItem.Name = "ファイルFToolStripMenuItem";
            ファイルFToolStripMenuItem.Size = new Size(85, 22);
            ファイルFToolStripMenuItem.Text = "File";
            ファイルFToolStripMenuItem.MouseUp += new MouseEventHandler(ファイルFToolStripMenuItem_MouseUp);
            configToolStripMenuItem.DisplayStyle = ToolStripItemDisplayStyle.Text;
            configToolStripMenuItem.Image = (Image)componentResourceManager.GetObject("configToolStripMenuItem.Image");
            configToolStripMenuItem.ImageTransparentColor = Color.Magenta;
            configToolStripMenuItem.Name = "configToolStripMenuItem";
            configToolStripMenuItem.Size = new Size(148, 22);
            configToolStripMenuItem.Text = "Main settings...";
            configToolStripMenuItem.Click += new EventHandler(configToolStripMenuItem_Click);
            toolStripSeparator2.Name = "toolStripSeparator2";
            toolStripSeparator2.Size = new Size(145, 6);
            exitAllToolStripMenuItem.DisplayStyle = ToolStripItemDisplayStyle.Text;
            exitAllToolStripMenuItem.Name = "exitAllToolStripMenuItem";
            exitAllToolStripMenuItem.Size = new Size(148, 22);
            exitAllToolStripMenuItem.Text = "Quit";
            exitAllToolStripMenuItem.Click += new EventHandler(exitToolStripMenuItem_Click);
            ツールTToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[7]
            {
         activeteStripMenuItem,
         arrangeWinStripMenuItem,
         toolStripSeparator1,
         mugenWinToolStripMenuItem,
         logWinToolStripMenuItem,
         debugWinToolStripMenuItem,
         varWinStripMenuItem
            });
            ツールTToolStripMenuItem.Name = "ツールTToolStripMenuItem";
            ツールTToolStripMenuItem.Size = new Size(80, 22);
            ツールTToolStripMenuItem.Text = "Window";
            ツールTToolStripMenuItem.DropDownOpening += new EventHandler(ツールTToolStripMenuItem_DropDownOpening);
            ツールTToolStripMenuItem.MouseUp += new MouseEventHandler(ツールTToolStripMenuItem_MouseUp);
            activeteStripMenuItem.Name = "activeteStripMenuItem";
            activeteStripMenuItem.Size = new Size(208, 22);
            activeteStripMenuItem.Text = "Bring other windows to the front";
            activeteStripMenuItem.Click += new EventHandler(activeteStripMenuItem_Click);
            arrangeWinStripMenuItem.Name = "arrangeWinStripMenuItem";
            arrangeWinStripMenuItem.Size = new Size(208, 22);
            arrangeWinStripMenuItem.Text = "Reset window positions";
            arrangeWinStripMenuItem.Click += new EventHandler(arrangeWinStripMenuItem_Click);
            toolStripSeparator1.Name = "toolStripSeparator1";
            toolStripSeparator1.Size = new Size(205, 6);
            mugenWinToolStripMenuItem.Name = "mugenWinToolStripMenuItem";
            mugenWinToolStripMenuItem.Size = new Size(208, 22);
            mugenWinToolStripMenuItem.Text = "M.U.G.E.N Window";
            mugenWinToolStripMenuItem.Click += new EventHandler(mugenWinToolStripMenuItem_Click);
            logWinToolStripMenuItem.DisplayStyle = ToolStripItemDisplayStyle.Text;
            logWinToolStripMenuItem.Name = "logWinToolStripMenuItem";
            logWinToolStripMenuItem.Size = new Size(208, 22);
            logWinToolStripMenuItem.Text = "Battle log window";
            logWinToolStripMenuItem.Click += new EventHandler(logWinToolStripMenuItem_Click);
            debugWinToolStripMenuItem.DisplayStyle = ToolStripItemDisplayStyle.Text;
            debugWinToolStripMenuItem.Name = "debugWinToolStripMenuItem";
            debugWinToolStripMenuItem.Size = new Size(208, 22);
            debugWinToolStripMenuItem.Text = "Debug Tools Window";
            debugWinToolStripMenuItem.Click += new EventHandler(debugWinToolStripMenuItem_Click);
            varWinStripMenuItem.Name = "varWinStripMenuItem";
            varWinStripMenuItem.Size = new Size(208, 22);
            varWinStripMenuItem.Text = "Variable View Window";
            varWinStripMenuItem.Click += new EventHandler(varWinStripMenuItem_Click);
            ヘルプHToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[3]
            {
         helpToolStripMenuItem,
         readmeToolStripMenuItem,
         versionAToolStripMenuItem
            });
            ヘルプHToolStripMenuItem.Name = "ヘルプHToolStripMenuItem";
            ヘルプHToolStripMenuItem.Size = new Size(56, 22);
            ヘルプHToolStripMenuItem.Text = "Help";
            ヘルプHToolStripMenuItem.TextAlign = ContentAlignment.MiddleRight;
            ヘルプHToolStripMenuItem.MouseUp += new MouseEventHandler(ヘルプHToolStripMenuItem_MouseUp);
            helpToolStripMenuItem.CheckOnClick = true;
            helpToolStripMenuItem.ImageScaling = ToolStripItemImageScaling.None;
            helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            helpToolStripMenuItem.ShortcutKeyDisplayString = "";
            helpToolStripMenuItem.Size = new Size(227, 22);
            helpToolStripMenuItem.Text = "Swiss Army Knife help";
            helpToolStripMenuItem.Click += new EventHandler(toolStripMenuItem1_Click);
            readmeToolStripMenuItem.Name = "readmeToolStripMenuItem";
            readmeToolStripMenuItem.Size = new Size(227, 22);
            readmeToolStripMenuItem.Text = "Open the \"Operations.txt\" file";
            readmeToolStripMenuItem.Click += new EventHandler(readmeToolStripMenuItem_Click);
            versionAToolStripMenuItem.Name = "versionAToolStripMenuItem";
            versionAToolStripMenuItem.Size = new Size(227, 22);
            versionAToolStripMenuItem.Text = "About Swiss Army Knife...";
            versionAToolStripMenuItem.Click += new EventHandler(versionAToolStripMenuItem_Click);
            helpBtnStripMenuItem.Alignment = ToolStripItemAlignment.Right;
            helpBtnStripMenuItem.DisplayStyle = ToolStripItemDisplayStyle.Image;
            helpBtnStripMenuItem.Name = "helpBtnStripMenuItem";
            helpBtnStripMenuItem.Size = new Size(12, 22);
            helpBtnStripMenuItem.Text = "toolStripMenuItem1";
            helpBtnStripMenuItem.Click += new EventHandler(helpBtnStripMenuItem_Click);
            helpBtnStripMenuItem.MouseHover += new EventHandler(helpBtnStripMenuItem_MouseHover);
            profileListBox.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            profileListBox.ContextMenuStrip = profileContextMenuStrip;
            profileListBox.Font = new Font("MS UI Gothic", 14.25f, FontStyle.Regular, GraphicsUnit.Point, 128);
            profileListBox.FormattingEnabled = true;
            profileListBox.IntegralHeight = false;
            profileListBox.ItemHeight = 19;
            profileListBox.Location = new Point(3, 32);
            profileListBox.Name = "profileListBox";
            profileListBox.Size = new Size(277, 166);
            profileListBox.TabIndex = 4;
            profileListBox.Click += new EventHandler(profileListBox_Click);
            profileListBox.SelectedIndexChanged += new EventHandler(profileListBox_SelectedIndexChanged);
            profileListBox.KeyUp += new KeyEventHandler(profileListBox_KeyUp);
            profileListBox.MouseDoubleClick += new MouseEventHandler(profileListBox_MouseDoubleClick);
            profileListBox.MouseDown += new MouseEventHandler(profileListBox_MouseDown);
            profileListBox.MouseUp += new MouseEventHandler(profileListBox_MouseUp);
            profileContextMenuStrip.Items.AddRange(new ToolStripItem[21]
            {
         toolStripTextBox1,
         toolStripSeparator6,
         launchToolStripMenuItem,
         toolStripSeparator9,
         launchQuickVSToolStripMenuItem,
         toolStripSeparator8,
         launchAutoModeToolStripMenuItem,
         restartToolStripMenuItem,
         restart2ToolStripMenuItem,
         toolStripSeparator3,
         openSelectStripMenuItem,
         openMugenStripMenuItem,
         openCharStripMenuItem,
         toolStripSeparator4,
         openProfileStripMenuItem,
         copyProfileStripMenuItem,
         pasteProfileStripMenuItem,
         toolStripSeparator7,
         deleteProfileToolStripMenuItem,
         toolStripSeparator5,
         quitToolStripMenuItem
            });
            profileContextMenuStrip.Name = "profileContextMenuStrip";
            profileContextMenuStrip.Size = new Size(281, 352);
            profileContextMenuStrip.Text = "設定1";
            profileContextMenuStrip.Opening += new CancelEventHandler(profileContextMenuStrip_Opening);
            toolStripTextBox1.BackColor = SystemColors.Control;
            toolStripTextBox1.BorderStyle = BorderStyle.None;
            toolStripTextBox1.Enabled = false;
            toolStripTextBox1.Name = "toolStripTextBox1";
            toolStripTextBox1.ReadOnly = true;
            toolStripTextBox1.Size = new Size(220, 18);
            toolStripSeparator6.Name = "toolStripSeparator6";
            toolStripSeparator6.Size = new Size(277, 6);
            launchToolStripMenuItem.DisplayStyle = ToolStripItemDisplayStyle.Text;
            launchToolStripMenuItem.Enabled = false;
            launchToolStripMenuItem.Name = "launchToolStripMenuItem";
            launchToolStripMenuItem.Size = new Size(280, 22);
            launchToolStripMenuItem.Text = "Start mugen";
            launchToolStripMenuItem.Click += new EventHandler(launchToolStripMenuItem_Click);
            toolStripSeparator9.Name = "toolStripSeparator9";
            toolStripSeparator9.Size = new Size(277, 6);
            launchQuickVSToolStripMenuItem.DisplayStyle = ToolStripItemDisplayStyle.Text;
            launchQuickVSToolStripMenuItem.Enabled = false;
            launchQuickVSToolStripMenuItem.Name = "launchQuickVSToolStripMenuItem";
            launchQuickVSToolStripMenuItem.Size = new Size(280, 22);
            launchQuickVSToolStripMenuItem.Text = "Start Quick Vs.";
            launchQuickVSToolStripMenuItem.Click += new EventHandler(launchQuickVSToolStripMenuItem_Click);
            toolStripSeparator8.Name = "toolStripSeparator8";
            toolStripSeparator8.Size = new Size(277, 6);
            launchAutoModeToolStripMenuItem.DisplayStyle = ToolStripItemDisplayStyle.Text;
            launchAutoModeToolStripMenuItem.Enabled = false;
            launchAutoModeToolStripMenuItem.Name = "launchAutoModeToolStripMenuItem";
            launchAutoModeToolStripMenuItem.Size = new Size(280, 22);
            launchAutoModeToolStripMenuItem.Text = "Start continuous battle";
            launchAutoModeToolStripMenuItem.Click += new EventHandler(launchAutoModeToolStripMenuItem_Click);
            restartToolStripMenuItem.Enabled = false;
            restartToolStripMenuItem.Name = "restartToolStripMenuItem";
            restartToolStripMenuItem.Size = new Size(280, 22);
            restartToolStripMenuItem.Text = "Restart from the cancelled match";
            restartToolStripMenuItem.Click += new EventHandler(restartToolStripMenuItem_Click);
            restart2ToolStripMenuItem.Enabled = false;
            restart2ToolStripMenuItem.Name = "restart2ToolStripMenuItem";
            restart2ToolStripMenuItem.Size = new Size(280, 22);
            restart2ToolStripMenuItem.Text = "Restart from the next match";
            restart2ToolStripMenuItem.Click += new EventHandler(restart2ToolStripMenuItem_Click);
            toolStripSeparator3.Name = "toolStripSeparator3";
            toolStripSeparator3.Size = new Size(277, 6);
            openSelectStripMenuItem.DisplayStyle = ToolStripItemDisplayStyle.Text;
            openSelectStripMenuItem.Name = "openSelectStripMenuItem";
            openSelectStripMenuItem.Size = new Size(280, 22);
            openSelectStripMenuItem.Text = "Open the select.def file";
            openSelectStripMenuItem.Click += new EventHandler(openSelectStripMenuItem_Click);
            openMugenStripMenuItem.DisplayStyle = ToolStripItemDisplayStyle.Text;
            openMugenStripMenuItem.Name = "openMugenStripMenuItem";
            openMugenStripMenuItem.Size = new Size(280, 22);
            openMugenStripMenuItem.Text = "Open the mugen.cfg file";
            openMugenStripMenuItem.Click += new EventHandler(openMugenStripMenuItem_Click);
            openCharStripMenuItem.DisplayStyle = ToolStripItemDisplayStyle.Text;
            openCharStripMenuItem.Name = "openCharStripMenuItem";
            openCharStripMenuItem.Size = new Size(280, 22);
            openCharStripMenuItem.Text = "Open the chars folder";
            openCharStripMenuItem.Click += new EventHandler(openCharStripMenuItem_Click);
            toolStripSeparator4.Name = "toolStripSeparator4";
            toolStripSeparator4.Size = new Size(277, 6);
            openProfileStripMenuItem.Name = "openProfileStripMenuItem";
            openProfileStripMenuItem.Size = new Size(280, 22);
            openProfileStripMenuItem.Text = "Profile settings...";
            openProfileStripMenuItem.Click += new EventHandler(openProfileStripMenuItem_Click);
            copyProfileStripMenuItem.Enabled = false;
            copyProfileStripMenuItem.Name = "copyProfileStripMenuItem";
            copyProfileStripMenuItem.Size = new Size(280, 22);
            copyProfileStripMenuItem.Text = "Copy profile";
            copyProfileStripMenuItem.Click += new EventHandler(copyProfileStripMenuItem_Click);
            pasteProfileStripMenuItem.Enabled = false;
            pasteProfileStripMenuItem.Name = "pasteProfileStripMenuItem";
            pasteProfileStripMenuItem.Size = new Size(280, 22);
            pasteProfileStripMenuItem.Text = "Paste profile";
            pasteProfileStripMenuItem.Click += new EventHandler(pasteProfileStripMenuItem_Click);
            toolStripSeparator7.Name = "toolStripSeparator7";
            toolStripSeparator7.Size = new Size(277, 6);
            deleteProfileToolStripMenuItem.Name = "deleteProfileToolStripMenuItem";
            deleteProfileToolStripMenuItem.Size = new Size(280, 22);
            deleteProfileToolStripMenuItem.Text = "Delete profile";
            deleteProfileToolStripMenuItem.Click += new EventHandler(deleteProfileToolStripMenuItem_Click);
            toolStripSeparator5.Name = "toolStripSeparator5";
            toolStripSeparator5.Size = new Size(277, 6);
            quitToolStripMenuItem.DisplayStyle = ToolStripItemDisplayStyle.Text;
            quitToolStripMenuItem.Name = "quitToolStripMenuItem";
            quitToolStripMenuItem.Size = new Size(280, 22);
            quitToolStripMenuItem.Text = "Close mugen";
            quitToolStripMenuItem.Click += new EventHandler(quitToolStripMenuItem_Click);
            autoModeTimer.Interval = 500;
            autoModeTimer.Tick += new EventHandler(autoModeTimer_Tick);
            quitButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            quitButton.Enabled = false;
            quitButton.Location = new Point(147, 201);
            quitButton.Name = "quitButton";
            quitButton.Size = new Size(130, 25);
            quitButton.TabIndex = 5;
            quitButton.Text = "Start mugen";
            quitButton.UseVisualStyleBackColor = true;
            quitButton.Click += new EventHandler(quitButton_Click);
            startButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            startButton.Location = new Point(3, 201);
            startButton.Name = "startButton";
            startButton.Size = new Size(131, 25);
            startButton.TabIndex = 6;
            startButton.Text = "More commands";
            startButton.UseVisualStyleBackColor = true;
            startButton.Click += new EventHandler(startButton_Click);
            toolTip1.AutomaticDelay = 500000;
            toolTip1.AutoPopDelay = 5000000;
            toolTip1.InitialDelay = 5000000;
            toolTip1.IsBalloon = true;
            toolTip1.ReshowDelay = 5000000;
            delayTimer.Interval = 200;
            delayTimer.Tick += new EventHandler(delayTimer_Tick);
            AutoScaleDimensions = new SizeF(6f, 12f);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(283, 230);
            Controls.Add(startButton);
            Controls.Add(quitButton);
            Controls.Add(profileListBox);
            Controls.Add(fileMenuStrip);
            DoubleBuffered = true;
            Icon = (Icon)componentResourceManager.GetObject("$this.Icon");
            KeyPreview = true;
            Location = new Point(10, 10);
            MainMenuStrip = fileMenuStrip;
            MaximizeBox = false;
            Name = nameof(MainForm);
            Padding = new Padding(3);
            SizeGripStyle = SizeGripStyle.Hide;
            StartPosition = FormStartPosition.Manual;
            Text = "Swiss Army Knife";
            Activated += new EventHandler(Form1_Activated);
            Deactivate += new EventHandler(MainForm_Deactivate);
            FormClosing += new FormClosingEventHandler(Form1_FormClosing);
            Load += new EventHandler(Form1_Load);
            Shown += new EventHandler(Form1_Shown);
            KeyPress += new KeyPressEventHandler(MainForm_KeyPress);
            Resize += new EventHandler(MainForm_Resize);
            fileMenuStrip.ResumeLayout(false);
            fileMenuStrip.PerformLayout();
            profileContextMenuStrip.ResumeLayout(false);
            profileContextMenuStrip.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        private MainForm()
        {
            InitializeComponent();
            ProfileManager.MainObj().SetCurrentProfile(1);
            Application.Idle += new EventHandler(app_Idle);
        }

        public static MainForm MainObj()
        {
            if (selfObj == null)
                selfObj = new MainForm();
            return selfObj;
        }

        [DllImport("user32.dll")]
        private static extern uint GetWindowThreadProcessId(IntPtr hWnd, out int lpdwProcessId);

        [DllImport("user32.dll")]
        private static extern IntPtr GetForegroundWindow();

        [DllImport("Kernel32.DLL", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern uint SetThreadExecutionState(uint state);

        public bool SetForegroundWindowEx(IntPtr hWnd) => MugenWindow.MainObj().SetForegroundWindowEx(hWnd);

        /// <summary>
        /// used to put a particular window in front.
        /// </summary>
        /// <param name="hWnd">window handle</param>
        /// <param name="isMugenWindow"></param>
        public void ShowTop(IntPtr hWnd, bool isMugenWindow = false) => MugenWindow.MainObj().ShowTop(hWnd, isMugenWindow);

        [DllImport("user32.dll", EntryPoint = "SendMessage")]
        private static extern bool SendMessage1(IntPtr hWnd, uint Msg, int wParam, int lParam);

        /// <summary>
        /// get the folder mugen.exe lives in.
        /// </summary>
        /// <returns></returns>
        public static string ExeFolder()
        {
            if (_exeFolder == null)
                _exeFolder = Path.GetDirectoryName(Application.ExecutablePath);
            return _exeFolder;
        }

        public static string GetFullPath(string file) => !Path.IsPathRooted(file) ? Path.Combine(ExeFolder(), file) : file;

        public bool IsMugenRunning() => MugenWindow.MainObj() != null;

        public bool DidMugenCrashed() => _didMugenCrashed;

        /// <summary>
        /// launches an instance of Mugen based on the currently-selected profile.
        /// </summary>
        /// <returns></returns>
        private bool _LaunchMugen()
        {
            if (MugenWindow.MainObj() == null || ProfileManager.MainObj().GetProfile(_selectedProfileNo) == null)
                return false;
            if (MugenWindow.MainObj().getMugenProcess() == null)
            {
                if (!MugenWindow.MainObj().LoadMugen(_selectedProfileNo))
                    return false;
                MugenWindow.MainObj().Show();
                if (MugenWindow.MainObj().getMugenProcess() != null)
                {
                    _didMugenCrashed = false;
                    _isRetrying = false;
                    if (!_isAutoModeRunning)
                        watchTimer.Start();
                }
            }
            return true;
        }

        /// <summary>
        /// launches mugen with some logging/window setup boilerplate.
        /// </summary>
        /// <param name="isRestart"></param>
        private void LaunchMugen(bool isRestart)
        {
            if (_isAutoModeRunning || MugenWindow.MainObj() == null)
                return;
            MugenProfile currentProfile = ProfileManager.MainObj().GetCurrentProfile();
            if (currentProfile == null)
                return;
            MugenWindow.MainObj().ResetGameQuitted();
            _selectedProfileNo = currentProfile.GetProfileNo();
            switch (currentProfile.GetCurrentGameMode())
            {
                case MugenProfile.GameMode.QUICK_VS:
                    QuickVSModePanel quickVsModePanel = new QuickVSModePanel();
                    Point location = Location;
                    int x = location.X + 205;
                    location = Location;
                    int y = location.Y;
                    quickVsModePanel.SetLoc(x, y);
                    int num = (int)quickVsModePanel.ShowDialog();
                    if (quickVsModePanel.GetResult() == DialogResult.OK && _LaunchMugen())
                    {
                        EnableQuitButton(true);
                        LogManager.MainObj().appendLog(" ");
                        LogManager.MainObj().appendLog(currentProfile.GetProfileName() + "' has been started in Quick Vs. mode.");
                    }
                    quickVsModePanel.Dispose();
                    break;
                case MugenProfile.GameMode.AUTO_MODE:
                    _isAutoModeRunning = true;
                    if (isRestart)
                    {
                        currentProfile.SetTempGameCount(-1);
                        MugenWindow.MainObj().ResetNumOfGames();
                    }
                    else
                        MugenWindow.MainObj().DecrementNumOfGames();
                    autoModeTimer.Enabled = true;
                    if (GameLogger.MainObj().GetPlayersCount() > 0 && currentProfile.GetTempGameCount() <= 0)
                        LogManager.MainObj().ResetPlayers();
                    LogManager.MainObj().appendLog(" ");
                    LogManager.MainObj().appendLog(currentProfile.GetProfileName() + "' is now running.");
                    if (currentProfile.GetTempGameCount() <= 0)
                        LogManager.MainObj().appendLog("Continuous battle mode has started. (" + currentProfile.GetCharacterCount() + "total matches)");
                    else
                        LogManager.MainObj().appendLog("Resume continuous battle mode. (" + (currentProfile.GetCharacterCount() - currentProfile.GetTempGameCount()) + " matches remaining)");
                    LogManager.MainObj().appendLog("----- Continuous battle settings -----");
                    if (currentProfile.GetMatchMode() == MugenProfile.MatchMode.ALLvsALL)
                        LogManager.MainObj().appendLog("Battle setup: round robin");
                    else
                        LogManager.MainObj().appendLog("Battle setup: one vs. others");
                    if (currentProfile.IsStrictRoundMode())
                        LogManager.MainObj().appendLog("Round settings: " + currentProfile.GetRoundCount() + " round(s) to win.");
                    else
                        LogManager.MainObj().appendLog("Round settings: " + currentProfile.GetRoundCount() + " total round(s).");
                    LogManager.MainObj().appendLog("Round time: " + currentProfile.GetMaxRoundTimeRawData() + " minute(s) maximum.");
                    LogManager.MainObj().appendLog("Error retries: " + currentProfile.GetErrorRetryCount() + " time(s) maximum.");
                    autoModeTimer.Start();
                    EnableQuitButton(true);
                    break;
                default:
                    if (!_LaunchMugen())
                        break;
                    EnableQuitButton(true);
                    LogManager.MainObj().appendLog(" ");
                    LogManager.MainObj().appendLog("'" + currentProfile.GetProfileName() + "' is now running");
                    break;
            }
        }

        private void CloseMugen()
        {
            if (MugenWindow.MainObj() == null)
                return;
            watchTimer.Stop();
            MugenWindow.MainObj().CloseMugen();
            EnableQuitButton(false);
            if (_isClosing)
                return;
            LogManager.MainObj().appendLog("mugen has been closed.");
        }

        /// <summary>
        /// Used to enable Start button in Profiles if the Profile is valid.
        /// </summary>
        /// <param name="isEnable"></param>
        /// <param name="mode"></param>
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
                        quitButton.Text = "Start Quick Vs.";
                        break;
                    case MugenProfile.GameMode.AUTO_MODE:
                        if (currentProfile.IsAutoModeAvailable())
                        {
                            if (currentProfile.GetTempGameCount() >= 0)
                            {
                                quitButton.Text = "Restart cancelled match.";
                                break;
                            }
                            quitButton.Text = "Start continuous battle.";
                            break;
                        }
                        quitButton.Text = "Start mugen.";
                        break;
                    default:
                        quitButton.Text = "Start mugen.";
                        break;
                }
            }
            else
                quitButton.Text = "Profile registration.";
            quitButton.Enabled = true;
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
                        quitButton.Text = "Start Quick Vs.";
                        break;
                    case MugenProfile.GameMode.AUTO_MODE:
                        if (currentProfile.IsAutoModeAvailable())
                        {
                            if (currentProfile.GetTempGameCount() >= 0)
                            {
                                quitButton.Text = "Restart cancelled match.";
                                break;
                            }
                            quitButton.Text = "Start continuous battle.";
                            break;
                        }
                        quitButton.Text = "Start mugen.";
                        break;
                    default:
                        quitButton.Text = "Start mugen.";
                        break;
                }
            }
            else
            {
                switch (currentProfile.GetCurrentGameMode())
                {
                    case MugenProfile.GameMode.QUICK_VS:
                        quitButton.Text = "Stop Quick Vs.";
                        break;
                    case MugenProfile.GameMode.AUTO_MODE:
                        quitButton.Text = "Stop continuous battle mode.";
                        break;
                    default:
                        quitButton.Text = "Close mugen.";
                        break;
                }
            }
            string profileName = currentProfile.GetProfileName();
            string mugenExePath = currentProfile.GetMugenExePath();
            if (profileName == null || profileName == "")
            {
                quitButton.Text = "Profile registration.";
                quitButton.Enabled = true;
            }
            else if (mugenExePath == null || mugenExePath == "" || !File.Exists(mugenExePath))
            {
                quitButton.Text = "Profile registration.";
                quitButton.Enabled = true;
            }
            else
                quitButton.Enabled = true;
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            _isClosing = true;
            if (e.CloseReason == CloseReason.UserClosing)
            {
                if (MessageBox.Show("Are you sure you wish to quit？", "Swiss Army Knife", MessageBoxButtons.OKCancel) == DialogResult.Cancel)
                {
                    _isClosing = false;
                    e.Cancel = true;
                    return;
                }
                if (MugenWindow.MainObj() != null && MugenWindow.MainObj().Visible)
                {
                    MainConfig mainConfig1 = ProfileManager.MainObj().GetMainConfig();
                    if (mainConfig1 != null)
                    {
                        MainConfig mainConfig2 = mainConfig1;
                        int x1 = Location.X;
                        int y1 = Location.Y;
                        int width1 = Size.Width;
                        int height1 = Size.Height;
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
            _isClosing = true;
            CloseMugen();
        }

        /// <summary>
        /// triggers whenever the Mugen watch timer activates.
        /// <br/>used to poll Mugen for errors.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void watchTimer_Tick(object sender, EventArgs e)
        {
            if (MugenWindow.MainObj() == null)
                return;
            int num = (int)SetThreadExecutionState(3U);
            watchTimer.Stop();
            if (MugenWindow.MainObj().getMugenProcess() == null)
            {
                if (!_isAutoModeRunning)
                {
                    EnableQuitButton(false);
                    MugenProfile profile = ProfileManager.MainObj().GetProfile(_selectedProfileNo);
                    if (profile == null || !profile.IsQuickMode())
                        LogManager.MainObj().appendLog("mugen has been closed.");
                    Activate();
                }
                MugenWindow.MainObj().SetTitleActive(false);
            }
            else if (MugenWindow.MainObj().CheckMugenError())
            {
                if (!_isAutoModeRunning)
                    EnableQuitButton(false);
                else
                    _didMugenCrashed = true;
                MugenWindow.MainObj().SetTitleActive(false);
            }
            else
            {
                MugenWindow.MainObj().UpdateTitle();
                watchTimer.Start();
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            helpBtnStripMenuItem.Image = SystemIcons.Question.ToBitmap();
            helpToolStripMenuItem.Image = SystemIcons.Question.ToBitmap();
            MugenWindow.MainObj().Hide();
            LogManager.MainObj().Hide();
            DebugForm.MainObj().Hide();
            VarForm.MainObj().Hide();
            MainConfig mainConfig = ProfileManager.MainObj().GetMainConfig();
            if (mainConfig != null)
            {
                if (mainConfig.GetMainWinW() > 0 && mainConfig.GetMainWinH() > 0)
                    SetDesktopBounds(mainConfig.GetMainWinX(), mainConfig.GetMainWinY(), mainConfig.GetMainWinW(), mainConfig.GetMainWinH());
                else
                    SetDesktopLocation(mainConfig.GetMainWinX(), mainConfig.GetMainWinY());
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
            profileListBox.Items.Clear();
            for (int profile_no = 1; profile_no <= profileCount; ++profile_no)
            {
                MugenProfile profile = ProfileManager.MainObj().GetProfile(profile_no);
                if (profile != null)
                {
                    string str = profile.GetProfileName();
                    if (str == null || str == "")
                        str = "(New Profile)";
                    profileListBox.Items.Add(str);
                }
            }
            profileListBox.SetSelected(0, true);
        }

        public void SetNeedActivateAll()
        {
        }

        /// <summary>
        /// activate all windows, used by bring-to-front functionality.
        /// </summary>
        /// <param name="forceActivate"></param>
        public void ActivateAll(bool forceActivate)
        {
            if (!forceActivate && MugenWindow.MainObj().getMugenProcess() != null && !MugenWindow.MainObj().IsActivatedOnce() || _isClosing)
                return;
            ShowTop(LogManager.MainObj().Handle);
            ShowTop(DebugForm.MainObj().Handle);
            ShowTop(VarForm.MainObj().Handle);
            if (MugenWindow.MainObj().getMugenProcess() != null)
            {
                SetForegroundWindowEx(Handle);
                if (!MugenWindow.MainObj().Visible)
                    return;
                MugenWindow.MainObj().ShowMugen();
            }
            else
            {
                if (MugenWindow.MainObj().Visible)
                    ShowTop(MugenWindow.MainObj().Handle, true);
                ShowTop(Handle);
            }
        }

        protected override void WndProc(ref Message m)
        {
            if (m.Msg == 161)
            {
                if (m.WParam.ToInt32() == 2)
                    _wasBtnDown = true;
            }
            else if (m.Msg == 561)
                _wasBtnDown = false;
            else if (m.Msg == 674)
            {
                if (_wasBtnDown)
                    ActivateAll(false);
                _wasBtnDown = false;
            }
            base.WndProc(ref m);
        }

        private void Form1_Activated(object sender, EventArgs e)
        {
            int num = _isClosing ? 1 : 0;
        }

        private void delayTimer_Tick(object sender, EventArgs e)
        {
            delayTimer.Stop();
            delayTimer.Enabled = false;
        }

        private void MainForm_Deactivate(object sender, EventArgs e)
        {
            _isMenuVisible = false;
            toolTip1.Hide(this);
        }

        public void ArrangeWin() => _ArrangeWin(false);

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
                    int num2 = Location.X + Size.Width;
                    size = LogManager.MainObj().Size;
                    int width = size.Width;
                    int num3 = num2 - width;
                    location = Location;
                    int y1 = location.Y;
                    size = Size;
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
                    location = Location;
                    int x1 = location.X;
                    size = Size;
                    int width = size.Width;
                    int num2 = x1 + width + 11;
                    location = Location;
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
                        int num2 = (int)welcomeForm.ShowDialog();
                        bool flag = welcomeForm.DidClickOK();
                        welcomeForm.Dispose();
                        if (!flag)
                            Application.Exit();
                        MainConfigPanel mainConfigPanel = new MainConfigPanel();
                        if (mainConfigPanel != null)
                        {
                            mainConfigPanel.DisableCancel();
                            int num3 = (int)mainConfigPanel.ShowDialog();
                            mainConfigPanel.Dispose();
                            NavigatorForm navigatorForm1 = new NavigatorForm(this);
                            if (navigatorForm1 != null)
                            {
                                NavigatorForm navigatorForm2 = navigatorForm1;
                                location = Location;
                                int num4 = location.X + 300;
                                location = Location;
                                int num5 = location.Y + 50;
                                int x = num4;
                                int y = num5;
                                navigatorForm2.SetLoc(x, y);
                                int num6 = (int)navigatorForm1.ShowDialog();
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
                            location = Location;
                            int x1 = location.X;
                            size = Size;
                            int width1 = size.Width;
                            int num2 = x1 + width1;
                            size = MugenWindow.MainObj().Size;
                            int width2 = size.Width;
                            int num3 = num2 + width2 + 20;
                            location = Location;
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
                            location = Location;
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
            Activate();
        }

        private void Form1_Shown(object sender, EventArgs e) => _ArrangeWin(true);

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Control dropDown = ファイルFToolStripMenuItem.DropDown;
            if (dropDown != null && dropDown.Cursor == Cursors.Help)
            {
                ファイルFToolStripMenuItem.DropDown.Show();
                string text = "Exit." + Environment.NewLine;
                toolTip1.Hide(dropDown);
                toolTip1.IsBalloon = false;
                toolTip1.IsBalloon = true;
                toolTip1.SetToolTip(dropDown, " ");
                toolTip1.Show(text, dropDown, exitAllToolStripMenuItem.Width / 2, exitAllToolStripMenuItem.Height * 4 / 2);
            }
            else
                Close();
        }

        private void versionAToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int num = (int)MessageBox.Show("Swiss Army Knife for winmugen - ver. 1.08 \n English translation by Vans.", "Swiss Army Knife");
        }

        private void activeteStripMenuItem_Click(object sender, EventArgs e)
        {
            Control dropDown = ツールTToolStripMenuItem.DropDown;
            if (dropDown != null && dropDown.Cursor == Cursors.Help)
            {
                ツールTToolStripMenuItem.DropDown.Show();
                string text = "Displays the other windows to the front." + Environment.NewLine + "(This has the same effect as clicking all the other windows to bring them to focus and then selecting the 'Swiss Army Knife' dialog.)" + Environment.NewLine + Environment.NewLine + "(Note: Windows that are not enabled will not be affected.)";
                toolTip1.Hide(dropDown);
                toolTip1.IsBalloon = false;
                toolTip1.IsBalloon = true;
                toolTip1.SetToolTip(dropDown, " ");
                toolTip1.Show(text, dropDown, logWinToolStripMenuItem.Width / 2, logWinToolStripMenuItem.Height / 2);
            }
            else
                ActivateAll(true);
        }

        private void arrangeWinStripMenuItem_Click(object sender, EventArgs e)
        {
            Control dropDown = ツールTToolStripMenuItem.DropDown;
            if (dropDown != null && dropDown.Cursor == Cursors.Help)
            {
                ツールTToolStripMenuItem.DropDown.Show();
                string text = "Aligns all the other windows to the default position relative to the main 'Swiss Army Knife' window." + Environment.NewLine + Environment.NewLine + "(Note: Only the enabled windows will be aligned.)";
                toolTip1.Hide(dropDown);
                toolTip1.IsBalloon = false;
                toolTip1.IsBalloon = true;
                toolTip1.SetToolTip(dropDown, " ");
                toolTip1.Show(text, dropDown, logWinToolStripMenuItem.Width / 2, logWinToolStripMenuItem.Height * 3 / 2);
            }
            else
                _ArrangeWin(false);
        }

        private void mugenWinToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Control dropDown = ツールTToolStripMenuItem.DropDown;
            if (dropDown != null && dropDown.Cursor == Cursors.Help)
            {
                ツールTToolStripMenuItem.DropDown.Show();
                string text = "Toggles the M.U.G.E.N window," + Environment.NewLine + "(Note: Only changes the focus of the M.U.G.E.N window.)";
                toolTip1.Hide(dropDown);
                toolTip1.IsBalloon = false;
                toolTip1.IsBalloon = true;
                toolTip1.SetToolTip(dropDown, " ");
                toolTip1.Show(text, dropDown, logWinToolStripMenuItem.Width / 2, logWinToolStripMenuItem.Height * 6 / 2);
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
            Control dropDown = ツールTToolStripMenuItem.DropDown;
            if (dropDown != null && dropDown.Cursor == Cursors.Help)
            {
                ツールTToolStripMenuItem.DropDown.Show();
                string text = "Toggles the Battle History window." + Environment.NewLine;
                toolTip1.Hide(dropDown);
                toolTip1.IsBalloon = false;
                toolTip1.IsBalloon = true;
                toolTip1.SetToolTip(dropDown, " ");
                toolTip1.Show(text, dropDown, logWinToolStripMenuItem.Width / 2, logWinToolStripMenuItem.Height * 8 / 2);
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
            Control dropDown = ツールTToolStripMenuItem.DropDown;
            if (dropDown != null && dropDown.Cursor == Cursors.Help)
            {
                ツールTToolStripMenuItem.DropDown.Show();
                string text = "Toggles the Debug Tools window." + Environment.NewLine;
                toolTip1.Hide(dropDown);
                toolTip1.IsBalloon = false;
                toolTip1.IsBalloon = true;
                toolTip1.SetToolTip(dropDown, " ");
                toolTip1.Show(text, dropDown, logWinToolStripMenuItem.Width / 2, logWinToolStripMenuItem.Height * 10 / 2);
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
            Control dropDown = ツールTToolStripMenuItem.DropDown;
            if (dropDown != null && dropDown.Cursor == Cursors.Help)
            {
                ツールTToolStripMenuItem.DropDown.Show();
                string text = "Toggles the Variable Display window." + Environment.NewLine;
                toolTip1.Hide(dropDown);
                toolTip1.IsBalloon = false;
                toolTip1.IsBalloon = true;
                toolTip1.SetToolTip(dropDown, " ");
                toolTip1.Show(text, dropDown, logWinToolStripMenuItem.Width / 2, logWinToolStripMenuItem.Height * 12 / 2);
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
            Control dropDown = ファイルFToolStripMenuItem.DropDown;
            if (dropDown != null && dropDown.Cursor == Cursors.Help)
            {
                ファイルFToolStripMenuItem.DropDown.Show();
                string text = "Opens the general settings dialog." + Environment.NewLine;
                toolTip1.Hide(dropDown);
                toolTip1.IsBalloon = false;
                toolTip1.IsBalloon = true;
                toolTip1.SetToolTip(dropDown, " ");
                toolTip1.Show(text, dropDown, exitAllToolStripMenuItem.Width / 2, exitAllToolStripMenuItem.Height / 2);
            }
            else
            {
                if (ProfileManager.MainObj().GetMainConfig() == null)
                    return;
                MainConfigPanel mainConfigPanel = new MainConfigPanel();
                if (mainConfigPanel == null)
                    return;
                int num = (int)mainConfigPanel.ShowDialog();
                mainConfigPanel.Dispose();
            }
        }

        private void reloadConfigStripMenuItem_Click(object sender, EventArgs e) => ProfileManager.MainObj().GetMainConfig()?.ReLoad();

        private void profileListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            ProfileManager.MainObj().SetCurrentProfile(profileListBox.SelectedIndex + 1);
            MugenProfile currentProfile = ProfileManager.MainObj().GetCurrentProfile();
            if (currentProfile == null || MugenWindow.MainObj().getMugenProcess() != null || _isAutoModeRunning)
                return;
            string profileName = currentProfile.GetProfileName();
            string mugenExePath = currentProfile.GetMugenExePath();
            if (profileName == null || profileName == "")
                EnableStartButton(false, currentProfile.GetDefaultGameMode());
            else if (mugenExePath == null || mugenExePath == "" || !File.Exists(mugenExePath))
                EnableStartButton(false, currentProfile.GetDefaultGameMode());
            else
                EnableStartButton(true, currentProfile.GetDefaultGameMode());
        }

        private void profileListBox_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (profileListBox.Cursor == Cursors.Help)
            {
                profileListBox_ShowHelp();
            }
            else
            {
                ProfileManager.MainObj().SetCurrentProfile(profileListBox.SelectedIndex + 1);
                MugenProfile currentProfile1 = ProfileManager.MainObj().GetCurrentProfile();
                if (currentProfile1 == null || currentProfile1.GetProfileName() == null || currentProfile1.GetProfileName() == "" || currentProfile1.GetMugenExePath() == null || currentProfile1.GetMugenExePath() == "" || !File.Exists(currentProfile1.GetMugenExePath()))
                {
                    ProfileConfigPanel profileConfigPanel = new ProfileConfigPanel();
                    if (profileConfigPanel == null)
                        return;
                    int num = (int)profileConfigPanel.ShowDialog();
                    profileConfigPanel.Dispose();
                    MugenProfile currentProfile2 = ProfileManager.MainObj().GetCurrentProfile();
                    if (currentProfile2 == null)
                        return;
                    int selectedIndex = profileListBox.SelectedIndex;
                    string str = currentProfile2.GetProfileName();
                    if (str == null || str == "")
                        str = "(New Profile)";
                    profileListBox.Items[selectedIndex] = str;
                }
                else
                {
                    if (MugenWindow.MainObj().getMugenProcess() != null || _isAutoModeRunning)
                        return;
                    if (currentProfile1 != null)
                    {
                        switch (currentProfile1.GetDefaultGameMode())
                        {
                            case MugenProfile.GameMode.QUICK_VS:
                                currentProfile1.SetGameMode(MugenProfile.GameMode.QUICK_VS);
                                LaunchMugen(true);
                                break;
                            case MugenProfile.GameMode.AUTO_MODE:
                                if (currentProfile1.IsAutoModeAvailable())
                                {
                                    currentProfile1.SetGameMode(MugenProfile.GameMode.AUTO_MODE);
                                    _didMugenCrashed = false;
                                    currentProfile1.SetIncremented();
                                    if (!currentProfile1.PrepareNextMatch(currentProfile1.GetTempGameCount()))
                                    {
                                        LaunchMugen(true);
                                        break;
                                    }
                                    LaunchMugen(false);
                                    break;
                                }
                                currentProfile1.SetGameMode(MugenProfile.GameMode.NORMAL);
                                LaunchMugen(true);
                                break;
                            default:
                                currentProfile1.SetGameMode(MugenProfile.GameMode.NORMAL);
                                LaunchMugen(true);
                                break;
                        }
                    }
                    MugenWindow.MainObj().ActivateEx();
                }
            }
        }

        private void profileContextMenuStrip_Opening(object sender, CancelEventArgs e)
        {
            Control contextMenuStrip = profileContextMenuStrip;
            if (contextMenuStrip != null && contextMenuStrip.Cursor == Cursors.Help)
            {
                launchToolStripMenuItem.Enabled = true;
                launchQuickVSToolStripMenuItem.Enabled = true;
                launchAutoModeToolStripMenuItem.Enabled = true;
                restartToolStripMenuItem.Enabled = true;
                restart2ToolStripMenuItem.Enabled = true;
                openMugenStripMenuItem.Enabled = true;
                openSelectStripMenuItem.Enabled = true;
                openCharStripMenuItem.Enabled = true;
                quitToolStripMenuItem.Enabled = true;
                openProfileStripMenuItem.Enabled = true;
                copyProfileStripMenuItem.Enabled = true;
                pasteProfileStripMenuItem.Enabled = true;
                deleteProfileToolStripMenuItem.Enabled = true;
            }
            else
            {
                MugenProfile currentProfile = ProfileManager.MainObj().GetCurrentProfile();
                if (currentProfile == null)
                    return;
                toolStripTextBox1.Text = "Profile name: " + currentProfile.GetProfileName();
                if (currentProfile.GetProfileName() == null || currentProfile.GetProfileName() == "" || currentProfile.GetMugenExePath() == null || currentProfile.GetMugenExePath() == "")
                {
                    launchToolStripMenuItem.Enabled = false;
                    launchQuickVSToolStripMenuItem.Enabled = false;
                    launchAutoModeToolStripMenuItem.Enabled = false;
                    restartToolStripMenuItem.Enabled = false;
                    restart2ToolStripMenuItem.Enabled = false;
                    openMugenStripMenuItem.Enabled = false;
                    openSelectStripMenuItem.Enabled = false;
                    openCharStripMenuItem.Enabled = false;
                    quitToolStripMenuItem.Enabled = false;
                    openProfileStripMenuItem.Enabled = true;
                    if (currentProfile.GetProfileName() == null || currentProfile.GetProfileName() == "")
                    {
                        copyProfileStripMenuItem.Enabled = false;
                        deleteProfileToolStripMenuItem.Enabled = false;
                    }
                    else
                    {
                        copyProfileStripMenuItem.Enabled = true;
                        deleteProfileToolStripMenuItem.Enabled = true;
                    }
                    if (ProfileManager.MainObj().IsValidProfileNo(_copyTargetProfileNo))
                        pasteProfileStripMenuItem.Enabled = true;
                    else
                        pasteProfileStripMenuItem.Enabled = false;
                }
                else
                {
                    launchToolStripMenuItem.Enabled = true;
                    launchQuickVSToolStripMenuItem.Enabled = true;
                    launchAutoModeToolStripMenuItem.Enabled = true;
                    restartToolStripMenuItem.Enabled = true;
                    restart2ToolStripMenuItem.Enabled = true;
                    openMugenStripMenuItem.Enabled = true;
                    openSelectStripMenuItem.Enabled = true;
                    openCharStripMenuItem.Enabled = true;
                    copyProfileStripMenuItem.Enabled = true;
                    deleteProfileToolStripMenuItem.Enabled = true;
                    quitToolStripMenuItem.Enabled = true;
                    if (currentProfile.GetMugenSelectCfgPath() == null || currentProfile.GetMugenSelectCfgPath() == "")
                        openSelectStripMenuItem.Enabled = false;
                    if (currentProfile.IsAutoModeAvailable())
                    {
                        if (currentProfile.GetTempGameCount() >= 0 && !_isAutoModeRunning)
                        {
                            restartToolStripMenuItem.Enabled = true;
                            restart2ToolStripMenuItem.Enabled = true;
                            launchAutoModeToolStripMenuItem.Text = "Restart continuous battle from the beginning.";
                        }
                        else
                        {
                            restartToolStripMenuItem.Enabled = false;
                            restart2ToolStripMenuItem.Enabled = false;
                            launchAutoModeToolStripMenuItem.Text = "Start continuous battle.";
                        }
                    }
                    else
                    {
                        launchAutoModeToolStripMenuItem.Enabled = false;
                        restartToolStripMenuItem.Enabled = false;
                        restart2ToolStripMenuItem.Enabled = false;
                    }
                    switch (currentProfile.GetCurrentGameMode())
                    {
                        case MugenProfile.GameMode.QUICK_VS:
                            quitToolStripMenuItem.Text = "Stop Quick Vs.";
                            break;
                        case MugenProfile.GameMode.AUTO_MODE:
                            quitToolStripMenuItem.Text = "Stop continuous battle.";
                            break;
                        default:
                            quitToolStripMenuItem.Text = "Close mugen.";
                            break;
                    }
                    if (MugenWindow.MainObj().getMugenProcess() != null || _isAutoModeRunning)
                    {
                        launchToolStripMenuItem.Enabled = false;
                        launchQuickVSToolStripMenuItem.Enabled = false;
                        launchAutoModeToolStripMenuItem.Enabled = false;
                        restartToolStripMenuItem.Enabled = false;
                        restart2ToolStripMenuItem.Enabled = false;
                        if (_selectedProfileNo == currentProfile.GetProfileNo())
                        {
                            openProfileStripMenuItem.Enabled = false;
                            copyProfileStripMenuItem.Enabled = false;
                            pasteProfileStripMenuItem.Enabled = false;
                            deleteProfileToolStripMenuItem.Enabled = false;
                            quitToolStripMenuItem.Enabled = true;
                        }
                        else
                        {
                            openProfileStripMenuItem.Enabled = true;
                            copyProfileStripMenuItem.Enabled = true;
                            deleteProfileToolStripMenuItem.Enabled = true;
                            quitToolStripMenuItem.Enabled = false;
                            if (ProfileManager.MainObj().IsValidProfileNo(_copyTargetProfileNo))
                            {
                                if (_copyTargetProfileNo == currentProfile.GetProfileNo())
                                    pasteProfileStripMenuItem.Enabled = false;
                                else
                                    pasteProfileStripMenuItem.Enabled = true;
                            }
                            else
                                pasteProfileStripMenuItem.Enabled = false;
                        }
                    }
                    else
                    {
                        launchToolStripMenuItem.Enabled = true;
                        launchQuickVSToolStripMenuItem.Enabled = true;
                        if (currentProfile.IsAutoModeAvailable())
                            launchAutoModeToolStripMenuItem.Enabled = true;
                        else
                            launchAutoModeToolStripMenuItem.Enabled = false;
                        quitToolStripMenuItem.Enabled = false;
                        openProfileStripMenuItem.Enabled = true;
                        copyProfileStripMenuItem.Enabled = true;
                        deleteProfileToolStripMenuItem.Enabled = true;
                        if (ProfileManager.MainObj().IsValidProfileNo(_copyTargetProfileNo))
                        {
                            if (_copyTargetProfileNo == currentProfile.GetProfileNo())
                                pasteProfileStripMenuItem.Enabled = false;
                            else
                                pasteProfileStripMenuItem.Enabled = true;
                        }
                        else
                            pasteProfileStripMenuItem.Enabled = false;
                    }
                }
            }
        }

        private void profileListBox_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Right)
                return;
            int index = profileListBox.IndexFromPoint(e.Location);
            if (index < 0 || index >= profileListBox.Items.Count)
                return;
            profileListBox.SetSelected(index, true);
            ProfileManager.MainObj().SetCurrentProfile(index + 1);
        }

        private void profileListBox_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Right)
                return;
            int index = profileListBox.IndexFromPoint(e.Location);
            if (index < 0 || index >= profileListBox.Items.Count)
                return;
            profileListBox.SetSelected(index, true);
            ProfileManager.MainObj().SetCurrentProfile(index + 1);
            MugenProfile currentProfile = ProfileManager.MainObj().GetCurrentProfile();
            if (currentProfile == null)
                return;
            toolStripTextBox1.Text = "Profile name: " + currentProfile.GetProfileName();
        }

        private void launchToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Control contextMenuStrip = profileContextMenuStrip;
            if (contextMenuStrip != null && contextMenuStrip.Cursor == Cursors.Help)
            {
                profileContextMenuStrip.Show();
                string text = "Starts mugen using the specified profile settings." + Environment.NewLine;
                toolTip1.Hide(contextMenuStrip);
                toolTip1.IsBalloon = false;
                toolTip1.IsBalloon = true;
                toolTip1.SetToolTip(contextMenuStrip, " ");
                toolTip1.Show(text, contextMenuStrip, launchToolStripMenuItem.Width / 2, launchToolStripMenuItem.Height * 4 / 2);
            }
            else
            {
                ProfileManager.MainObj().GetCurrentProfile()?.SetGameMode(MugenProfile.GameMode.NORMAL);
                LaunchMugen(true);
                MugenWindow.MainObj().ActivateEx();
            }
        }

        private void launchQuickVSToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Control contextMenuStrip = profileContextMenuStrip;
            if (contextMenuStrip != null && contextMenuStrip.Cursor == Cursors.Help)
            {
                profileContextMenuStrip.Show();
                string text = "Starts Quick Vs. mode." + Environment.NewLine;
                toolTip1.Hide(contextMenuStrip);
                toolTip1.IsBalloon = false;
                toolTip1.IsBalloon = true;
                toolTip1.SetToolTip(contextMenuStrip, " ");
                toolTip1.Show(text, contextMenuStrip, launchToolStripMenuItem.Width / 2, launchToolStripMenuItem.Height * 6 / 2);
            }
            else
            {
                ProfileManager.MainObj().GetCurrentProfile()?.SetGameMode(MugenProfile.GameMode.QUICK_VS);
                LaunchMugen(true);
                MugenWindow.MainObj().ActivateEx();
            }
        }

        private void launchAutoModeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Control contextMenuStrip = profileContextMenuStrip;
            if (contextMenuStrip != null && contextMenuStrip.Cursor == Cursors.Help)
            {
                profileContextMenuStrip.Show();
                string text = "Starts continuous battle mode." + Environment.NewLine;
                toolTip1.Hide(contextMenuStrip);
                toolTip1.IsBalloon = false;
                toolTip1.IsBalloon = true;
                toolTip1.SetToolTip(contextMenuStrip, " ");
                toolTip1.Show(text, contextMenuStrip, launchToolStripMenuItem.Width / 2, launchToolStripMenuItem.Height * 9 / 2);
            }
            else
            {
                ProfileManager.MainObj().GetCurrentProfile()?.SetGameMode(MugenProfile.GameMode.AUTO_MODE);
                LaunchMugen(true);
                MugenWindow.MainObj().ActivateEx();
            }
        }

        private void restartToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Control contextMenuStrip = profileContextMenuStrip;
            if (contextMenuStrip != null && contextMenuStrip.Cursor == Cursors.Help)
            {
                profileContextMenuStrip.Show();
                string text = "Used in continuous battle. Restarts continuous battle mode from the match that was cancelled." + Environment.NewLine;
                toolTip1.Hide(contextMenuStrip);
                toolTip1.IsBalloon = false;
                toolTip1.IsBalloon = true;
                toolTip1.SetToolTip(contextMenuStrip, " ");
                toolTip1.Show(text, contextMenuStrip, launchToolStripMenuItem.Width / 2, launchToolStripMenuItem.Height * 11 / 2);
            }
            else if (_selectedProfileNo != profileListBox.SelectedIndex + 1)
            {
                LaunchMugen(true);
                MugenWindow.MainObj().ActivateEx();
            }
            else
            {
                if (MugenWindow.MainObj().getMugenProcess() != null || _isAutoModeRunning)
                    return;
                ProfileManager.MainObj().SetCurrentProfile(profileListBox.SelectedIndex + 1);
                MugenProfile currentProfile = ProfileManager.MainObj().GetCurrentProfile();
                if (currentProfile != null)
                {
                    if (currentProfile.IsAutoModeAvailable())
                    {
                        currentProfile.SetGameMode(MugenProfile.GameMode.AUTO_MODE);
                        _didMugenCrashed = false;
                        currentProfile.SetIncremented();
                        if (!currentProfile.PrepareNextMatch(currentProfile.GetTempGameCount()))
                            LaunchMugen(true);
                        else
                            LaunchMugen(false);
                    }
                    else
                    {
                        currentProfile.SetGameMode(MugenProfile.GameMode.NORMAL);
                        LaunchMugen(true);
                    }
                }
                else
                    LaunchMugen(true);
                MugenWindow.MainObj().ActivateEx();
            }
        }

        private void restart2ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Control contextMenuStrip = profileContextMenuStrip;
            if (contextMenuStrip != null && contextMenuStrip.Cursor == Cursors.Help)
            {
                profileContextMenuStrip.Show();
                string text = "Used in continuous battle. Skips the match that was cancelled and restarts from the next scheduled match." + Environment.NewLine;
                toolTip1.Hide(contextMenuStrip);
                toolTip1.IsBalloon = false;
                toolTip1.IsBalloon = true;
                toolTip1.SetToolTip(contextMenuStrip, " ");
                toolTip1.Show(text, contextMenuStrip, launchToolStripMenuItem.Width / 2, launchToolStripMenuItem.Height * 13 / 2);
            }
            else if (_selectedProfileNo != profileListBox.SelectedIndex + 1)
            {
                LaunchMugen(true);
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
                        _didMugenCrashed = false;
                        LogManager.MainObj().appendLog("");
                        LogManager.MainObj().appendLog("Restart from the next match.");
                        currentProfile.SetIncremented();
                        if (!currentProfile.PrepareNextMatch(currentProfile.GetTempGameCount()))
                            LaunchMugen(true);
                        else
                            LaunchMugen(false);
                    }
                    else
                    {
                        currentProfile.SetGameMode(MugenProfile.GameMode.NORMAL);
                        LaunchMugen(true);
                    }
                }
                else
                    LaunchMugen(true);
                MugenWindow.MainObj().ActivateEx();
            }
        }

        private void openSelectStripMenuItem_Click(object sender, EventArgs e)
        {
            Control contextMenuStrip = profileContextMenuStrip;
            if (contextMenuStrip != null && contextMenuStrip.Cursor == Cursors.Help)
            {
                profileContextMenuStrip.Show();
                string text = "Opens the select.def file in a text editor." + Environment.NewLine + Environment.NewLine + "(Note: Specifying the select.def is required for operation.)" + Environment.NewLine;
                toolTip1.Hide(contextMenuStrip);
                toolTip1.IsBalloon = false;
                toolTip1.IsBalloon = true;
                toolTip1.SetToolTip(contextMenuStrip, " ");
                toolTip1.Show(text, contextMenuStrip, launchToolStripMenuItem.Width / 2, launchToolStripMenuItem.Height * 15 / 2);
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
            Control contextMenuStrip = profileContextMenuStrip;
            if (contextMenuStrip != null && contextMenuStrip.Cursor == Cursors.Help)
            {
                profileContextMenuStrip.Show();
                string text = "Opens the mugen.cfg in a text editor." + Environment.NewLine;
                toolTip1.Hide(contextMenuStrip);
                toolTip1.IsBalloon = false;
                toolTip1.IsBalloon = true;
                toolTip1.SetToolTip(contextMenuStrip, " ");
                toolTip1.Show(text, contextMenuStrip, launchToolStripMenuItem.Width / 2, launchToolStripMenuItem.Height * 17 / 2);
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
            Control contextMenuStrip = profileContextMenuStrip;
            if (contextMenuStrip != null && contextMenuStrip.Cursor == Cursors.Help)
            {
                profileContextMenuStrip.Show();
                string text = "Opens the chars folder belonging to the mugen installation." + Environment.NewLine;
                toolTip1.Hide(contextMenuStrip);
                toolTip1.IsBalloon = false;
                toolTip1.IsBalloon = true;
                toolTip1.SetToolTip(contextMenuStrip, " ");
                toolTip1.Show(text, contextMenuStrip, launchToolStripMenuItem.Width / 2, launchToolStripMenuItem.Height * 19 / 2);
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
                startInfo.ErrorDialogParentHandle = Handle;
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
            Control contextMenuStrip = profileContextMenuStrip;
            if (contextMenuStrip != null && contextMenuStrip.Cursor == Cursors.Help)
            {
                profileContextMenuStrip.Show();
                string text = "Opens the profile settings dialog." + Environment.NewLine;
                toolTip1.Hide(contextMenuStrip);
                toolTip1.IsBalloon = false;
                toolTip1.IsBalloon = true;
                toolTip1.SetToolTip(contextMenuStrip, " ");
                toolTip1.Show(text, contextMenuStrip, launchToolStripMenuItem.Width / 2, launchToolStripMenuItem.Height * 22 / 2);
            }
            else
            {
                MugenProfile currentProfile = ProfileManager.MainObj().GetCurrentProfile();
                if (currentProfile == null)
                    return;
                ProfileConfigPanel profileConfigPanel = new ProfileConfigPanel();
                if (profileConfigPanel == null)
                    return;
                int num = (int)profileConfigPanel.ShowDialog();
                profileConfigPanel.Dispose();
                if (ProfileManager.MainObj().GetCurrentProfile() == null)
                    return;
                int selectedIndex = profileListBox.SelectedIndex;
                string str = currentProfile.GetProfileName();
                if (str == null || str == "")
                    str = "(New Profile)";
                profileListBox.Items[selectedIndex] = str;
            }
        }

        private void reloadStripMenuItem_Click(object sender, EventArgs e)
        {
            MugenProfile currentProfile = ProfileManager.MainObj().GetCurrentProfile();
            if (currentProfile == null)
                return;
            currentProfile.ReLoad();
            profileListBox.Items[profileListBox.SelectedIndex] = currentProfile.GetProfileName();
        }

        private void profileListBox_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyValue != 13)
                return;
            if (profileListBox.Cursor == Cursors.Help)
            {
                profileListBox_ShowHelp();
            }
            else
            {
                ProfileManager.MainObj().SetCurrentProfile(profileListBox.SelectedIndex + 1);
                MugenProfile currentProfile = ProfileManager.MainObj().GetCurrentProfile();
                if (currentProfile == null || currentProfile.GetProfileName() == null || currentProfile.GetProfileName() == "" || currentProfile.GetMugenExePath() == null || currentProfile.GetMugenExePath() == "")
                {
                    ProfileConfigPanel profileConfigPanel = new ProfileConfigPanel();
                    if (profileConfigPanel == null)
                        return;
                    int num = (int)profileConfigPanel.ShowDialog();
                    profileConfigPanel.Dispose();
                    if (ProfileManager.MainObj().GetCurrentProfile() == null)
                        return;
                    int selectedIndex = profileListBox.SelectedIndex;
                    string str = currentProfile.GetProfileName();
                    if (str == null || str == "")
                        str = "(New Profile)";
                    profileListBox.Items[selectedIndex] = str;
                }
                else
                {
                    if (MugenWindow.MainObj().getMugenProcess() != null || _isAutoModeRunning)
                        return;
                    if (currentProfile != null)
                    {
                        switch (currentProfile.GetDefaultGameMode())
                        {
                            case MugenProfile.GameMode.QUICK_VS:
                                currentProfile.SetGameMode(MugenProfile.GameMode.QUICK_VS);
                                LaunchMugen(true);
                                break;
                            case MugenProfile.GameMode.AUTO_MODE:
                                if (currentProfile.IsAutoModeAvailable())
                                {
                                    currentProfile.SetGameMode(MugenProfile.GameMode.AUTO_MODE);
                                    _didMugenCrashed = false;
                                    currentProfile.SetIncremented();
                                    if (!currentProfile.PrepareNextMatch(currentProfile.GetTempGameCount()))
                                    {
                                        LaunchMugen(true);
                                        break;
                                    }
                                    LaunchMugen(false);
                                    break;
                                }
                                currentProfile.SetGameMode(MugenProfile.GameMode.NORMAL);
                                LaunchMugen(true);
                                break;
                            default:
                                currentProfile.SetGameMode(MugenProfile.GameMode.NORMAL);
                                LaunchMugen(true);
                                break;
                        }
                    }
                    MugenWindow.MainObj().ActivateEx();
                }
            }
        }

        private void autoModeTimer_Tick(object sender, EventArgs e)
        {
            autoModeTimer.Stop();
            if (!_isAutoModeRunning)
                return;
            int num1 = (int)SetThreadExecutionState(3U);
            if (MugenWindow.MainObj().getMugenProcess() == null)
                MugenWindow.MainObj().SetTitleActive(false);
            else if (MugenWindow.MainObj().CheckMugenError())
            {
                _didMugenCrashed = true;
                MugenWindow.MainObj().SetTitleActive(false);
            }
            else
                MugenWindow.MainObj().UpdateTitle();
            MugenProfile profile = ProfileManager.MainObj().GetProfile(_selectedProfileNo);
            MugenWindow mugenWindow = MugenWindow.MainObj();
            if (profile != null && mugenWindow != null)
            {
                if (profile.IsAutoMode() && mugenWindow.IsMugenCrashed())
                    _didMugenCrashed = true;
                if (mugenWindow.getMugenProcess() == null)
                {
                    if (mugenWindow.IsGameQuitted() && (!_didMugenCrashed || _didAutoModeQuitted))
                    {
                        ResumeConfirmForm.Result result = ResumeConfirmForm.Result.QUIT;
                        if (!_didAutoModeQuitted && !_isClosing)
                        {
                            ResumeConfirmForm resumeConfirmForm = new ResumeConfirmForm();
                            if (resumeConfirmForm != null)
                            {
                                int num2 = (int)resumeConfirmForm.ShowDialog();
                                result = resumeConfirmForm.GetResult();
                                resumeConfirmForm.Dispose();
                            }
                        }
                        _didAutoModeQuitted = false;
                        EnableQuitButton(false);
                        _isAutoModeRunning = false;
                        _retryCount = 0;
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
                                _didMugenCrashed = false;
                                profile.SetIncremented();
                                if (!profile.PrepareNextMatch(profile.GetTempGameCount()))
                                {
                                    LaunchMugen(true);
                                    return;
                                }
                                LaunchMugen(false);
                                return;
                            case ResumeConfirmForm.Result.RESUME_NEXT:
                                profile.IncrementTempGameCount();
                                mugenWindow.SyncNumOfGames(profile.GetTempGameCount() + 1);
                                LogManager.MainObj().appendLog("");
                                LogManager.MainObj().appendLog("Restart from next match.");
                                _didMugenCrashed = false;
                                if (!profile.PrepareNextMatch(profile.GetTempGameCount()))
                                {
                                    LaunchMugen(true);
                                    return;
                                }
                                LaunchMugen(false);
                                return;
                            default:
                                return;
                        }
                    }
                    else
                    {
                        if (!_didMugenCrashed || _retryCount >= profile.GetErrorRetryCount() && !_isRetrying)
                        {
                            if (_didMugenCrashed)
                                profile.IncrementTempGameCount();
                            if (mugenWindow.IsBusyMugen() && !profile.CheckIncremented(false))
                            {
                                ++_autoModeRetryCount;
                                if (_autoModeRetryCount < 60)
                                {
                                    autoModeTimer.Start();
                                    return;
                                }
                            }
                            _autoModeRetryCount = 0;
                            profile.CheckIncremented(true);
                            mugenWindow.SyncNumOfGames(profile.GetTempGameCount() + 1);
                            _retryCount = 0;
                            _didMugenCrashed = false;
                            if (!profile.PrepareNextMatch(profile.GetTempGameCount()))
                            {
                                profile.SetTempGameCount(-1);
                                EnableQuitButton(false);
                                _isAutoModeRunning = false;
                                profile.SetIncremented();
                                mugenWindow.ResetGameQuitted();
                                LogManager.MainObj().appendLog("");
                                LogManager.MainObj().appendLog("The continuous battle schedule has finished.");
                                if (File.Exists(GetFullPath("gameover.wav")))
                                {
                                    new SoundPlayer(GetFullPath("gameover.wav"))?.Play();
                                    return;
                                }
                                SystemSounds.Exclamation.Play();
                                return;
                            }
                        }
                        else if (_didMugenCrashed)
                        {
                            _didMugenCrashed = false;
                            _didAutoModeQuitted = false;
                            mugenWindow.ResetGameQuitted();
                            if (!mugenWindow.SyncNumOfGames(profile.GetTempGameCount() + 1) && !_isRetrying)
                            {
                                ++_retryCount;
                                if (_retryCount <= profile.GetErrorRetryCount())
                                {
                                    _isRetrying = true;
                                    mugenWindow.SetRetryGame();
                                    LogManager.MainObj().appendLog("Retry running the match.");
                                }
                            }
                            profile.PrepareNextMatch(profile.GetTempGameCount());
                        }
                        _LaunchMugen();
                    }
                }
                else if (profile.IsAutoMode() && mugenWindow.IsMugenCrashed())
                    _didMugenCrashed = true;
            }
            autoModeTimer.Start();
        }

        private void quitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Control contextMenuStrip = profileContextMenuStrip;
            if (contextMenuStrip != null && contextMenuStrip.Cursor == Cursors.Help)
            {
                profileContextMenuStrip.Show();
                string text = "Stops running mugen. If mugen is being ran in continuous battle mode then this will cancel the current match." + Environment.NewLine;
                toolTip1.Hide(contextMenuStrip);
                toolTip1.IsBalloon = false;
                toolTip1.IsBalloon = true;
                toolTip1.SetToolTip(contextMenuStrip, " ");
                toolTip1.Show(text, contextMenuStrip, launchToolStripMenuItem.Width / 2, launchToolStripMenuItem.Height * 31 / 2);
            }
            else if (!_isAutoModeRunning)
            {
                CloseMugen();
                MugenWindow.MainObj().ResetGameQuitted();
            }
            else
            {
                _didAutoModeQuitted = true;
                MugenWindow.MainObj().SetGameQuitted();
                if (MugenWindow.MainObj().getMugenProcess() == null)
                    return;
                CloseMugen();
            }
        }

        private void MainForm_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != '\x001B')
                return;
            if (MugenWindow.MainObj().getMugenProcess() != null)
                MugenWindow.MainObj().InjectESC();
            else if (_isAutoModeRunning)
                MugenWindow.MainObj().SetGameQuitted();
            e.Handled = true;
        }

        private void app_Idle(object sender, EventArgs e) => _disableQuitButton = false;

        private void quitButton_Click(object sender, EventArgs e)
        {
            Control quitButton = this.quitButton;
            if (quitButton != null && quitButton.Cursor == Cursors.Help)
            {
                string text = "Starts running mugen in continuous battle mode with the selected profile." + Environment.NewLine + "If the profile is empty, the profile settings menu will open instead." + Environment.NewLine;
                toolTip1.Hide(quitButton);
                toolTip1.IsBalloon = false;
                toolTip1.IsBalloon = true;
                toolTip1.SetToolTip(quitButton, " ");
                toolTip1.Show(text, quitButton, quitButton.Width / 2, quitButton.Height / 2);
            }
            else
            {
                if (_disableQuitButton)
                    return;
                _disableQuitButton = true;
                ActivateAll(true);
                if (MugenWindow.MainObj().getMugenProcess() == null && !_isAutoModeRunning)
                {
                    ProfileManager.MainObj().SetCurrentProfile(profileListBox.SelectedIndex + 1);
                    MugenProfile currentProfile1 = ProfileManager.MainObj().GetCurrentProfile();
                    if (currentProfile1 == null || currentProfile1.GetProfileName() == null || currentProfile1.GetProfileName() == "" || currentProfile1.GetMugenExePath() == null || currentProfile1.GetMugenExePath() == "" || !File.Exists(currentProfile1.GetMugenExePath()))
                    {
                        ProfileConfigPanel profileConfigPanel = new ProfileConfigPanel();
                        if (profileConfigPanel == null)
                            return;
                        int num = (int)profileConfigPanel.ShowDialog();
                        profileConfigPanel.Dispose();
                        MugenProfile currentProfile2 = ProfileManager.MainObj().GetCurrentProfile();
                        if (currentProfile2 == null)
                            return;
                        int selectedIndex = profileListBox.SelectedIndex;
                        string str = currentProfile2.GetProfileName();
                        if (str == null || str == "")
                            str = "(New Profile)";
                        profileListBox.Items[selectedIndex] = str;
                    }
                    else
                    {
                        if (MugenWindow.MainObj().getMugenProcess() != null || _isAutoModeRunning)
                            return;
                        if (currentProfile1 != null)
                        {
                            switch (currentProfile1.GetDefaultGameMode())
                            {
                                case MugenProfile.GameMode.QUICK_VS:
                                    currentProfile1.SetGameMode(MugenProfile.GameMode.QUICK_VS);
                                    LaunchMugen(true);
                                    break;
                                case MugenProfile.GameMode.AUTO_MODE:
                                    if (currentProfile1.IsAutoModeAvailable())
                                    {
                                        currentProfile1.SetGameMode(MugenProfile.GameMode.AUTO_MODE);
                                        _didMugenCrashed = false;
                                        currentProfile1.SetIncremented();
                                        if (!currentProfile1.PrepareNextMatch(currentProfile1.GetTempGameCount()))
                                        {
                                            LaunchMugen(true);
                                            break;
                                        }
                                        LaunchMugen(false);
                                        break;
                                    }
                                    currentProfile1.SetGameMode(MugenProfile.GameMode.NORMAL);
                                    LaunchMugen(true);
                                    break;
                                default:
                                    currentProfile1.SetGameMode(MugenProfile.GameMode.NORMAL);
                                    LaunchMugen(true);
                                    break;
                            }
                        }
                        MugenWindow.MainObj().ActivateEx();
                    }
                }
                else if (!_isAutoModeRunning)
                {
                    CloseMugen();
                    MugenWindow.MainObj().ResetGameQuitted();
                }
                else
                {
                    _didAutoModeQuitted = true;
                    MugenWindow.MainObj().SetGameQuitted();
                    if (MugenWindow.MainObj().getMugenProcess() == null)
                        return;
                    CloseMugen();
                }
            }
        }

        private void startButton_Click(object sender, EventArgs e) => profileContextMenuStrip.Show(startButton, 0, 0);

        private void copyProfileStripMenuItem_Click(object sender, EventArgs e)
        {
            Control contextMenuStrip = profileContextMenuStrip;
            if (contextMenuStrip != null && contextMenuStrip.Cursor == Cursors.Help)
            {
                profileContextMenuStrip.Show();
                string text = "Copies the settings of the selected profile." + Environment.NewLine;
                toolTip1.Hide(contextMenuStrip);
                toolTip1.IsBalloon = false;
                toolTip1.IsBalloon = true;
                toolTip1.SetToolTip(contextMenuStrip, " ");
                toolTip1.Show(text, contextMenuStrip, launchToolStripMenuItem.Width / 2, launchToolStripMenuItem.Height * 24 / 2);
            }
            else
            {
                ProfileManager.MainObj().SetCurrentProfile(profileListBox.SelectedIndex + 1);
                MugenProfile currentProfile = ProfileManager.MainObj().GetCurrentProfile();
                if (currentProfile == null)
                    return;
                switch (currentProfile.GetProfileName())
                {
                    case "":
                        break;
                    default:
                        _copyTargetProfileNo = profileListBox.SelectedIndex + 1;
                        break;
                }
            }
        }

        private void pasteProfileStripMenuItem_Click(object sender, EventArgs e)
        {
            Control contextMenuStrip = profileContextMenuStrip;
            if (contextMenuStrip != null && contextMenuStrip.Cursor == Cursors.Help)
            {
                profileContextMenuStrip.Show();
                string text = "Overwrites the selected profile with the information from the copied one." + Environment.NewLine + "(A confirm dialog will appear before deletion.)" + Environment.NewLine;
                toolTip1.Hide(contextMenuStrip);
                toolTip1.IsBalloon = false;
                toolTip1.IsBalloon = true;
                toolTip1.SetToolTip(contextMenuStrip, " ");
                toolTip1.Show(text, contextMenuStrip, launchToolStripMenuItem.Width / 2, launchToolStripMenuItem.Height * 26 / 2);
            }
            else if (!ProfileManager.MainObj().IsValidProfileNo(_copyTargetProfileNo))
            {
                pasteProfileStripMenuItem.Enabled = false;
            }
            else
            {
                if (_copyTargetProfileNo == profileListBox.SelectedIndex + 1)
                    return;
                MugenProfile profile = ProfileManager.MainObj().GetProfile(_copyTargetProfileNo);
                if (profile == null || profile.GetProfileName() == null || profile.GetProfileName() == "")
                {
                    pasteProfileStripMenuItem.Enabled = false;
                }
                else
                {
                    ProfileManager.MainObj().SetCurrentProfile(profileListBox.SelectedIndex + 1);
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
                    currentProfile.CopyConfig(profile);
                    currentProfile.ReLoad();
                    profileListBox.Items[profileListBox.SelectedIndex] = currentProfile.GetProfileName();
                }
            }
        }

        private void deleteProfileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Control contextMenuStrip = profileContextMenuStrip;
            if (contextMenuStrip != null && contextMenuStrip.Cursor == Cursors.Help)
            {
                profileContextMenuStrip.Show();
                string text = "This will delete a registered profile." + Environment.NewLine + "(A confirm dialog will appear before deletion.)" + Environment.NewLine;
                toolTip1.Hide(contextMenuStrip);
                toolTip1.IsBalloon = false;
                toolTip1.IsBalloon = true;
                toolTip1.SetToolTip(contextMenuStrip, " ");
                toolTip1.Show(text, contextMenuStrip, launchToolStripMenuItem.Width / 2, launchToolStripMenuItem.Height * 28 / 2);
            }
            else
            {
                ProfileManager.MainObj().SetCurrentProfile(profileListBox.SelectedIndex + 1);
                MugenProfile currentProfile = ProfileManager.MainObj().GetCurrentProfile();
                if (currentProfile == null || currentProfile.GetProfileName() == null || MessageBox.Show("The '" + currentProfile.GetProfileName() + "' profile will be deleted. Do you wish to continue?", "Swiss Army Knife", MessageBoxButtons.OKCancel) != DialogResult.OK)
                    return;
                currentProfile.ResetConf();
                currentProfile.SetTempGameCount(-1);
                currentProfile.MakeBackup();
                currentProfile.SaveConfigText("");
                profileListBox.Items[profileListBox.SelectedIndex] = "(New Profile)";
                deleteProfileToolStripMenuItem.Enabled = false;
                if (profileListBox.SelectedIndex + 1 != _copyTargetProfileNo)
                    return;
                _copyTargetProfileNo = 0;
                pasteProfileStripMenuItem.Enabled = false;
            }
        }

        private void MainForm_Resize(object sender, EventArgs e)
        {
            if (MugenWindow.MainObj().isDebugBreakMode())
                return;
            if (WindowState == FormWindowState.Minimized)
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
                if (WindowState != FormWindowState.Normal)
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
            toolTip1.Hide(this);
            toolTip1.SetToolTip(this, " ");
            toolTip1.IsBalloon = false;
            toolTip1.IsBalloon = true;
            if (!_isHelpMode)
            {
                _isHelpMode = true;
                Cursor = Cursors.Help;
                profileListBox.Cursor = Cursors.Help;
                startButton.Cursor = Cursors.Help;
                quitButton.Cursor = Cursors.Help;
                profileContextMenuStrip.Cursor = Cursors.Help;
                ファイルFToolStripMenuItem.DropDown.Cursor = Cursors.Help;
                ツールTToolStripMenuItem.DropDown.Cursor = Cursors.Help;
                MugenWindow.MainObj().SetHelpMode(true);
                LogManager.MainObj().SetHelpMode(true);
                DebugForm.MainObj().SetHelpMode(true);
                VarForm.MainObj().SetHelpMode(true);
                toolTip1.Show("Help mode has been activated. Please click the items you would like to learn more about." + Environment.NewLine + "Click the help button again to disable the help function at any time.", this, Width, 60, 5000);
            }
            else
            {
                _isHelpMode = false;
                Cursor = Cursors.Default;
                profileListBox.Cursor = Cursors.Default;
                startButton.Cursor = Cursors.Default;
                quitButton.Cursor = Cursors.Default;
                profileContextMenuStrip.Cursor = Cursors.Default;
                ファイルFToolStripMenuItem.DropDown.Cursor = Cursors.Default;
                ツールTToolStripMenuItem.DropDown.Cursor = Cursors.Default;
                MugenWindow.MainObj().SetHelpMode(false);
                LogManager.MainObj().SetHelpMode(false);
                DebugForm.MainObj().SetHelpMode(false);
                VarForm.MainObj().SetHelpMode(false);
                toolTip1.Show("Help mode has been disabled.", this, Width, 60, 1000);
            }
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e) => ToggleHelpMode();

        private void ツールTToolStripMenuItem_DropDownOpening(object sender, EventArgs e)
        {
            mugenWinToolStripMenuItem.Checked = MugenWindow.MainObj().Visible;
            logWinToolStripMenuItem.Checked = LogManager.MainObj().Visible;
            debugWinToolStripMenuItem.Checked = DebugForm.MainObj().Visible;
            varWinStripMenuItem.Checked = VarForm.MainObj().Visible;
        }

        private void profileListBox_ShowHelp()
        {
            if (!(profileListBox.Cursor == Cursors.Help))
                return;
            string text = "This is the profile list. 'Profiles' are used to specify the parameters and settings to run mugen with." + Environment.NewLine + "A filepath to the mugen executable and command line arguments can also be specified." + Environment.NewLine + "With profiles, you can run many customized builds of mugen to improve organization!" + Environment.NewLine + "(You can specify up to 8 profiles." + Environment.NewLine + Environment.NewLine + "Additionaly, you can see more commands by using the right click button." + Environment.NewLine + Environment.NewLine + "The profiles marked as (New Profile) are empty and can be used to store a new profile." + Environment.NewLine + "By double clicking it, you can access the settings for registering a new profile." + Environment.NewLine + Environment.NewLine + "The names of the registered profiles are displayed.（Example: 'The normal setup','Evil characters only'）" + Environment.NewLine + "By double clicking a registered profile, you can edit its settings." + Environment.NewLine;
            toolTip1.Hide(profileListBox);
            toolTip1.IsBalloon = false;
            toolTip1.IsBalloon = true;
            toolTip1.SetToolTip(profileListBox, " ");
            toolTip1.Show(text, profileListBox, profileListBox.Width, profileListBox.Height / 2);
        }

        private void profileListBox_Click(object sender, EventArgs e)
        {
            if (!(profileListBox.Cursor == Cursors.Help))
                return;
            profileListBox_ShowHelp();
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
                    Process.Start(textEditor, GetFullPath("主な機能の説明.txt"));
                }
                catch
                {
                }
            }
            else
            {
                try
                {
                    Process.Start("notepad.exe", GetFullPath("主な機能の説明.txt"));
                }
                catch
                {
                }
            }
        }

        private void ファイルFToolStripMenuItem_MouseUp(object sender, MouseEventArgs e)
        {
            if (!ファイルFToolStripMenuItem.DropDown.Visible)
            {
                if (!_isMenuVisible)
                {
                    _isMenuVisible = true;
                    ファイルFToolStripMenuItem.ShowDropDown();
                }
                else
                    _isMenuVisible = false;
            }
            else if (_isMenuVisible)
            {
                _isMenuVisible = false;
                ファイルFToolStripMenuItem.HideDropDown();
            }
            else
                _isMenuVisible = true;
        }

        private void ツールTToolStripMenuItem_MouseUp(object sender, MouseEventArgs e)
        {
            if (!ツールTToolStripMenuItem.DropDown.Visible)
            {
                if (!_isMenuVisible)
                {
                    _isMenuVisible = true;
                    ツールTToolStripMenuItem.ShowDropDown();
                }
                else
                    _isMenuVisible = false;
            }
            else if (_isMenuVisible)
            {
                _isMenuVisible = false;
                ツールTToolStripMenuItem.HideDropDown();
            }
            else
                _isMenuVisible = true;
        }

        private void ヘルプHToolStripMenuItem_MouseUp(object sender, MouseEventArgs e)
        {
            if (!ヘルプHToolStripMenuItem.DropDown.Visible)
            {
                if (!_isMenuVisible)
                {
                    _isMenuVisible = true;
                    ヘルプHToolStripMenuItem.ShowDropDown();
                }
                else
                    _isMenuVisible = false;
            }
            else if (_isMenuVisible)
            {
                _isMenuVisible = false;
                ヘルプHToolStripMenuItem.HideDropDown();
            }
            else
                _isMenuVisible = true;
        }

        private void helpBtnStripMenuItem_Click(object sender, EventArgs e)
        {
            ToggleHelpMode();
            if (_isHelpMode)
                helpToolStripMenuItem.Checked = true;
            else
                helpToolStripMenuItem.Checked = false;
        }

        private void helpBtnStripMenuItem_MouseHover(object sender, EventArgs e)
        {
            if (_isHelpMode)
                return;
            toolTip1.Hide(this);
            toolTip1.IsBalloon = false;
            toolTip1.SetToolTip(this, " ");
            toolTip1.Show("Help Button", this, Width, helpBtnStripMenuItem.Height, 1000);
        }
    }
}
