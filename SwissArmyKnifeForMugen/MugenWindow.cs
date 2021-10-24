// Decompiled with JetBrains decompiler
// Type: SwissArmyKnifeForMugen.MugenWindow
// Assembly: SAKnifeWM, Version=1.0.8.0, Culture=neutral, PublicKeyToken=null
// MVID: 09478AD8-365C-4BF3-BEA1-B5785151259B
// Assembly location: C:\Users\ziddi\Downloads\Swiss Army Knife 1.1 Conversion\Swiss Army Knife 1.1 Conversion\SAKnifeWM.exe

using Microsoft.Samples.Debugging.Native;
using MugenWatcher.Watcher;
using MugenWatcher.Databases;
using MugenWatcher.EnumTypes;
using SwissArmyKnifeForMugen.Configs;
using SwissArmyKnifeForMugen.Displays;
using SwissArmyKnifeForMugen.Properties;
using SwissArmyKnifeForMugen.Triggers;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;
using static SwissArmyKnifeForMugen.Triggers.TriggerDatabase;
using MugenWatcher.Utils;
using static MugenWatcher.ExternalFuncs;

namespace SwissArmyKnifeForMugen
{
    /// <summary>
    /// heart of the application. most of the monitoring/handling code is in here.
    /// </summary>
    public class MugenWindow : Form, IDebugProcessRunner
    {
        private MugenProcessWatcher watcher;
        // currently-running trigger target
        private TriggerCheckTarget _triggerCheckTarget = new TriggerCheckTarget();
        private int _invokeWaitTime = 10;
        private long _stepModeInterval = 500;
        private int _skipModeFrames = 30;
        // color of Mugen debug text
        private DebugColor _debugColor = DebugColor.NONE;
        // used to show current panel in debug window
        private MugenWindow.DebugListMode _debugListMode = MugenWindow.DebugListMode.PLAYER_LIST_MODE;
        // mugen data lists
        private int[] mugenPlayerId = new int[60];
        private int[] muenHelperId = new int[60];
        private int[] muenParentId = new int[60];
        private int[] muenOwnerId = new int[60];
        private string[] mugenName = new string[60];
        private int[] mugenExplodId = new int[50];
        private int[] mugenOwnerId = new int[50];
        private int[] mugenAnim = new int[50];
        private int[] mugenProjId = new int[50];
        private int[] mugenProjX = new int[50];
        private int[] mugenProjY = new int[50];
        private int[] mugenProjAnim = new int[50];
        private VarForm.SysInfo_t mugenSysInfo = new VarForm.SysInfo_t();
        private int[] mugenSysvar = new int[5];
        private int[] mugenVar = new int[60];
        private float[] mugenSysfvar = new float[5];
        private float[] mugenFvar = new float[40];
        // for DebugColor.CUSTOM, these are the custom RGB values.
        private int[] customDebugColors = new int[] { 256, 256, 256 };
        private const int GWL_STYLE = -16;
        private const uint WS_CAPTION = 0x00C00000;
        private const uint WS_POPUP = 2147483648;
        private const uint WS_CHILD = 1073741824;
        private const short SWP_NOMOVE = 2;
        private const short SWP_NOSIZE = 1;
        private const short SWP_NOZORDER = 4;
        private const int SWP_SHOWWINDOW = 64;
        private const int SWP_HIDEWINDOW = 128;
        private const int SWP_NOACTIVATE = 16;
        private const int SWP_ASYNC = 16384;
        private const int WM_NULL = 0;
        private const int INVOKE_WAIT_TIMEOUT_LONG = 20;
        private const int INVOKE_WAIT_TIMEOUT_SHORT = 10;
        private const int LSFW_LOCK = 1;
        private const int LSFW_UNLOCK = 2;
        private const uint WM_GETTEXT = 13;
        private const uint WM_KEYDOWN = 256;
        private const uint WM_KEYUP = 257;
        private const uint WM_CHAR = 258;
        private const uint VK_CONTROL = 17;
        // maximums for one page of the list
        public const int MAX_PLAYER_COUNT = 60;
        public const int MAX_EXPLOD_COUNT = 50;
        public const int MAX_PROJ_COUNT = 50;
        // freezes Mugen/holds hardware breakpoint
        private bool _isDebugBreakMode;
        private bool _stopDebugBreakFlag;
        private uint _debugSpPointer;
        private static MugenWindow selfObj;
        private int _workingProfileId;
        // some flags from Mugen for handling window/process state
        private bool _isMugenHidden;
        private bool _isActive;
        private bool _isActivated;
        private bool _isActivatedOnce;
        private bool _isShownOnce;
        private bool _isMugenFrozen;
        private bool _isGameQuitted;
        private bool _isMugenCrashed;
        private int numOfGames;
        private bool _isRetryGame;
        private bool _wasBtnDown;
        private bool _ignoreUnPauseRequestOnce;
        private bool _isMugenActive;
        private bool _isStepMode;
        private long _stepModeCounter;
        private long _stepModeLastCounter;
        // for debug form
        private bool _isSpeedMode;
        private bool _isSpeedModeChanged;
        private bool _wasSpeedModeChanged;
        private bool _isSkipMode;
        private bool _isDebugMode;
        private bool _isDebugModeChanged;
        private int _debugTargetNo;
        private bool _isDebugTargetNoChanged;
        private int _varInspectTargetNo;
        private int _debugTargetPlayer;
        private int _varInspectTargetPlayer;
        private bool _debugColorChanged;
        private bool _flagDumpPlayers;
        private int _explodHeadNo;
        private int _projHeadNo;
        private MugenWindow.ProjOwner _projOwner;
        private uint player1AnimAddr;
        private uint player2AnimAddr;
        private uint player3AnimAddr;
        private uint player4AnimAddr;
        private int player1Id;
        private int player2Id;
        private int player3Id;
        private int player4Id;
        private IContainer components;
        private PictureBox backgroundBox;
        private ToolTip toolTip1;
        private System.Windows.Forms.Timer activateTimer;
        private uint watchAddr;
        internal uint WatchInitVal { get; set; }

        internal bool isWindowCaptured = false;


        // win, 1.0, 1.1a4, 1.1b1 -- placed here over MW
        private uint[] projHitBPAddrs = { 0x00446267, 0x0046FAD0, 0x004407B0, 0x00440CE0 };

        private MugenWindow()
        {
            this.watcher = new MugenProcessWatcher();
            this.watcher.SetDebugProcessRunner(this);
            this.InitializeComponent();
            this.ClientSize = new Size(640, 480);
            if (!File.Exists(MainForm.GetFullPath("background.jpg")))
                return;
            this.backgroundBox.ImageLocation = MainForm.GetFullPath("background.jpg");
            this.backgroundBox.SizeMode = PictureBoxSizeMode.StretchImage;
        }

        internal void ResizeToProfile(MugenProfile profile)
        {
            // don't resize during MUGEN runtime!
            if (this.watcher.CheckMugenProcessActive()) return;
            this.ClientSize = new Size(profile.GetScreenWidth(), profile.GetScreenHeight());
            this.backgroundBox.Size = new Size(profile.GetScreenWidth(), profile.GetScreenHeight());

            DebugForm.MainObj().SetDesktopBounds(this.Location.X + this.Size.Width + 20, this.Location.Y, DebugForm.MainObj().Size.Width, this.Size.Height);
            VarForm.MainObj().SetDesktopBounds(this.Location.X, this.Location.Y + this.Size.Height + 20, this.Size.Width, VarForm.MainObj().Size.Height);
        }

        /// <summary>
        /// currently-selected profile's Mugen version
        /// </summary>
        /// <returns></returns>
        public MugenType_t getMugenType() => this.watcher.MugenVersion;

        /// <summary>
        /// set if a breakpoint is activated + mugen is frozen
        /// </summary>
        /// <returns></returns>
        public bool isDebugBreakMode() => this._isDebugBreakMode;

        public void stopDebugBreak()
        {
            if (!this._isDebugBreakMode)
                return;
            this._stopDebugBreakFlag = true;
        }

        /// <summary>
        /// changes the current trigger target
        /// </summary>
        /// <param name="target"></param>
        public void SetTriggerCheckTarget(TriggerCheckTarget target)
        {
            this._triggerCheckTarget = target;
            if (this._triggerCheckTarget.GetCurrentMode() == TriggerCheckTarget.CheckMode.CHECKMODE_STOPPED)
                return;
            this._triggerCheckTarget.SetDirty();
        }

        // below functions all change the trigger mode between start, stop, etc

        public void StartTriggerCheckMode()
        {
            this._triggerCheckTarget.SetNextCommand(TriggerCheckTarget.CheckCommand.CHECKCMD_START);
            this._triggerCheckTarget.SetDirty();
        }

        public void StopTriggerCheckMode()
        {
            this._triggerCheckTarget.SetNextCommand(TriggerCheckTarget.CheckCommand.CHECKCMD_STOP);
            this._triggerCheckTarget.SetDirty();
        }

        public void ResumeTriggerCheckMode()
        {
            this._triggerCheckTarget.SetNextCommand(TriggerCheckTarget.CheckCommand.CHECKCMD_RESUME);
            this._triggerCheckTarget.SetDirty();
        }

        public TriggerCheckTarget.CheckMode GetTriggerCheckMode() => this._triggerCheckTarget.GetCurrentMode();

        [DllImport("user32.dll", SetLastError = true)]
        private static extern uint SetParent(IntPtr hWndChild, IntPtr hWndNewParent);

        [DllImport("user32.dll", SetLastError = true)]
        private static extern IntPtr GetParent(IntPtr hWnd);

        [DllImport("user32.dll")]
        private static extern int MoveWindow(
          IntPtr hwnd,
          int x,
          int y,
          int nWidth,
          int nHeight,
          int bRepaint);

        [DllImport("user32.dll")]
        public static extern IntPtr SetWindowPos(
          IntPtr hWnd,
          int hWndInsertAfter,
          int x,
          int Y,
          int cx,
          int cy,
          int wFlags);

        public void ShowTop(IntPtr hWnd, bool isMugenWindow = false)
        {
            if (this._isDebugBreakMode || this._isMugenCrashed || this._isMugenFrozen)
                return;
            MugenWindow.SetWindowPos(hWnd, 0, 0, 0, this.backgroundBox.Size.Width, this.backgroundBox.Size.Height, 16387);
        }

        [DllImport("user32.dll")]
        private static extern uint SetWindowLong(IntPtr hWnd, int index, uint unValue);

        [DllImport("user32.dll")]
        private static extern uint GetWindowLong(IntPtr hWnd, int index);

        [DllImport("User32.Dll")]
        private static extern int GetWindowRect(IntPtr hWnd, out MugenWindow.RECT rect);

        [DllImport("User32.Dll")]
        private static extern int GetClientRect(IntPtr hWnd, out MugenWindow.RECT rect);

        [DllImport("USER32.DLL", CharSet = CharSet.Auto)]
        private static extern bool SetForegroundWindow(IntPtr hWnd);

        public bool SetForegroundWindowEx(IntPtr hWnd)
        {
            if (this._isDebugBreakMode || this._isMugenCrashed || this._isMugenFrozen)
                return false;
            return this.watcher.GetDebugProcess() == null || this._triggerCheckTarget == null || this._triggerCheckTarget.GetCurrentMode() != TriggerCheckTarget.CheckMode.CHECKMODE_STARTED;
        }

        [DllImport("USER32.DLL", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool LockSetForegroundWindow(uint uUnlockCode);

        [DllImport("user32.dll")]
        private static extern int GetWindowThreadProcessId(IntPtr hWnd, IntPtr ProcessId);

        [DllImport("user32.dll")]
        private static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll")]
        private static extern bool AttachThreadInput(int idAttach, int idAttachTo, bool fAttach);

        [DllImport("user32.dll")]
        private static extern bool IsWindowVisible(IntPtr hWnd);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern int GetWindowText(IntPtr hWnd, StringBuilder lpString, int nMaxCount);

        [DllImport("user32.dll")]
        private static extern uint GetWindowThreadProcessId(IntPtr hWnd, out int lpdwProcessId);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool EnumChildWindows(
          IntPtr hwndParent,
          MugenWindow.EnumChildProc lpEnumFunc,
          IntPtr lParam);

        [DllImport("User32.dll")]
        public static extern int SendMessageTimeout(
          IntPtr hWnd,
          uint Msg,
          IntPtr wParam,
          IntPtr lParam,
          uint fuFlags,
          uint timeout);

        [DllImport("user32.dll", EntryPoint = "SendMessage")]
        private static extern bool SendMessage1(IntPtr hWnd, uint Msg, int wParam, int lParam);

        [DllImport("user32.dll", EntryPoint = "SendMessage")]
        private static extern IntPtr SendMessageText(
          IntPtr hWnd,
          uint Msg,
          int wParam,
          StringBuilder lParam);

        [DllImport("user32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool PostMessage(IntPtr hWnd, uint Msg, IntPtr wParam, IntPtr lParam);

        public static MugenWindow MainObj()
        {
            if (MugenWindow.selfObj == null)
                MugenWindow.selfObj = new MugenWindow();
            return MugenWindow.selfObj;
        }

        public int GetWorkingProfileId() => this._workingProfileId;

        public void DelayedActivate()
        {
            this.activateTimer.Enabled = true;
            this.activateTimer.Stop();
            this.activateTimer.Start();
        }

        private void activateTimer_Tick(object sender, EventArgs e)
        {
            this.activateTimer.Stop();
            this.Activate();
        }

        public new void Activate()
        {
            if (this._isDebugBreakMode || this._isMugenCrashed || this._isMugenFrozen || this.watcher.GetDebugProcess() != null && this._triggerCheckTarget != null && this._triggerCheckTarget.GetCurrentMode() == TriggerCheckTarget.CheckMode.CHECKMODE_STARTED)
                return;
            base.Activate();
        }

        protected override void WndProc(ref Message m)
        {
            if (this._isDebugBreakMode && (m.Msg == 132 || m.Msg == 161 || (m.Msg == 6 || m.Msg == 28)))
                return;
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
                    MainForm.MainObj().ActivateAll(true);
                this._wasBtnDown = false;
            }
            base.WndProc(ref m);
        }

        /// <summary>
        /// launches the Mugen process for a given profile
        /// </summary>
        /// <param name="profileNo"></param>
        /// <returns></returns>
        public bool LoadMugen(int profileNo)
        {
            this.watchAddr = 0U;
            this.WatchInitVal = 0U;
            // loop to wait for the monitor to become available/to kill the old Mugen process
            while (this.watcher.GetProcessWatcher().IsBusy)
            {
                this.watcher.GetProcessWatcher().CancelAsync();
                this.watcher.KillAndAwaitMugenProcessEnd();
                Application.DoEvents();
            }
            // doublecheck the old process has died
            if (!this.watcher.CheckMugenProcessActive())
            {
                this._isMugenHidden = false;
                this._isActivated = false;
                this._isActivatedOnce = false;
                this._isShownOnce = false;
                // type is determined on launch
                this.watcher.ResetMugenVersion();
                MugenProfile profile = ProfileManager.MainObj().GetProfile(profileNo);
                if (profile == null)
                    return false;
                ProcessStartInfo startInfo = new ProcessStartInfo();
                if (profile.GetMugenExePath() == null)
                    return false;
                if (!File.Exists(profile.GetMugenExePath()))
                {
                    if (LogManager.MainObj() != null)
                        AsyncAppendLog("The mugen executable was not found.");
                    return false;
                }
                // determine the actual MugenType_t (ie version)
                if (!this.watcher.DetectMugenVersion(FileVersionInfo.GetVersionInfo(profile.GetMugenExePath())))
                {
                    if (LogManager.MainObj() != null)
                        AsyncAppendLog("There is an issue with the defined MUGEN executable for the current profile.");
                }
                // #3: disable fullscreen
                // backup mugen config
                if (File.Exists(profile.GetMugenPath() + "/data/mugen.cfg"))
                {
                    // check
                    string text = File.ReadAllText(profile.GetMugenPath() + "/data/mugen.cfg");
                    if (text.Contains("FullScreen = 1"))
                    {
                        // delete and overwrite
                        File.Delete(profile.GetMugenPath() + "/data/mugen.cfg.swissarmy.bak");
                        File.Copy(profile.GetMugenPath() + "/data/mugen.cfg", profile.GetMugenPath() + "/data/mugen.cfg.swissarmy.bak");
                        // replace
                        text = text.Replace("FullScreen = 1", "FullScreen = 0");
                        // write back
                        File.WriteAllText(profile.GetMugenPath() + "/data/mugen.cfg", text);
                    }
                }
                // setup to launch a debugging process
                startInfo.FileName = Path.GetFileName(profile.GetMugenExePath());
                startInfo.Arguments = profile.GetMugenCommandLineOptions() == null ? "" : profile.GetMugenCommandLineOptions();
                startInfo.WorkingDirectory = profile.GetMugenPath();
                startInfo.CreateNoWindow = false;
                startInfo.UseShellExecute = false;
                startInfo.WindowStyle = ProcessWindowStyle.Minimized;
                try
                {
                    // launch the base process
                    this.watcher.LaunchMugenProcess(startInfo);
                }
                catch
                {
                    if (LogManager.MainObj() != null)
                        AsyncAppendLog("Failed to load the exe file.");
                    return false;
                }
            }
            // confirm we launched successfully
            if (this.watcher.CheckMugenProcessActive())
            {
                this.watcher.GetMugenProcess().WaitForInputIdle(5000);
                int num1 = 200;
                // waiting for the process to launch the Mugen window
                while (num1-- >= 0 && this.watcher.CheckMugenProcessActive() && !this.watcher.CheckMugenWindowStatus())
                {
                    Thread.Sleep(50);
                    this.watcher.GetMugenProcess().Refresh();
                    Application.DoEvents();
                }
                // failsafe, may have crashed pre-load
                if (!this.watcher.CheckMugenProcessActive())
                {
                    this.watcher.ResetMugenVersion();
                    if (this.watcher.GetMugenProcess() != null)
                        this.watcher.GetMugenProcess().Dispose();
                    this.watcher.DestroyMugenProcess();
                    if (LogManager.MainObj() != null)
                        AsyncAppendLog("Failed to load the exe file.");
                    return false;
                }
                if (!this.watcher.CheckMugenWindowStatus())
                {
                    if (LogManager.MainObj() != null)
                        AsyncAppendLog("Failed to load the exe file.");
                    this.CloseMugen();
                    return false;
                }
                StringBuilder lpString = new StringBuilder(4132);
                // doublecheck the window looks good
                if (this.watcher.CheckMugenWindowStatus() && MugenWindow.GetWindowText(this.watcher.GetMugenWindowHandle(), lpString, lpString.Capacity) != 0)
                {
                    string lower = lpString.ToString().ToLower();
                    if (!lower.Contains("mugen") && !lower.Contains("m.u.g.e.n"))
                    {
                        if (LogManager.MainObj() != null)
                            AsyncAppendLog("The specified exe file is not mugen.");
                        this.CloseMugen();
                        return false;
                    }
                }
                // capture the Mugen window and force it into our container
                this.CaptureWindow();

                // set predetermined flags based on profile setup
                MugenProfile profile = ProfileManager.MainObj().GetProfile(profileNo);
                if (profile != null)
                {
                    DebugForm.MainObj().SetSpeedModeCheckBox(profile.IsSpeedMode(), true);
                    DebugForm.MainObj().SetSkipModeCheckBox(profile.IsSkipMode());
                    DebugForm.MainObj().SetDebugModeCheckBox(profile.IsDebugMode(), true);
                    DebugForm.MainObj().PreInitTriggerCheck(this.watcher.MugenVersion); // in case of saved triggers
                    this._workingProfileId = profile.GetProfileNo();
                }
                this._isMugenFrozen = false;
                this._isGameQuitted = false;
                this._isMugenCrashed = false;
                // launch monitor
                if (!this.watcher.GetProcessWatcher().IsBusy)
                    this.watcher.GetProcessWatcher().RunWorkerAsync();
                return true;
            }
            // failure case
            this.watcher.ResetMugenVersion();
            return false;
        }

        public void CaptureWindow()
        {
            if (!this.isWindowCaptured)
            {
                // get window style and bitwise OR to disable captions
                uint windowStyles = (uint)((int)MugenWindow.GetWindowLong(this.watcher.GetMugenWindowHandle(), GWL_STYLE) & -WS_CAPTION);

                MugenWindow.SetParent(this.watcher.GetMugenWindowHandle(), this.backgroundBox.Handle);
                MugenWindow.SetWindowLong(this.watcher.GetMugenWindowHandle(), GWL_STYLE, windowStyles);
                MugenWindow.SetWindowPos(this.watcher.GetMugenWindowHandle(), 0, 0, 0, this.backgroundBox.Size.Width, this.backgroundBox.Size.Height, SWP_SHOWWINDOW | SWP_NOZORDER);
                MugenWindow.SetParent(this.watcher.GetMugenWindowHandle(), this.backgroundBox.Handle);

                this.isWindowCaptured = true;
            }
            
        }

        public void AttachMugen() => MugenWindow.AttachThreadInput(MugenWindow.GetWindowThreadProcessId(this.watcher.GetMugenWindowHandle(), IntPtr.Zero), MugenWindow.GetWindowThreadProcessId(this.Handle, IntPtr.Zero), true);

        public void DettachMugen() => MugenWindow.AttachThreadInput(MugenWindow.GetWindowThreadProcessId(this.watcher.GetMugenWindowHandle(), IntPtr.Zero), MugenWindow.GetWindowThreadProcessId(this.Handle, IntPtr.Zero), false);

        /// <summary>
        /// show mugen + do not unpause (some mugens unpause on focus/unfocus)
        /// </summary>
        public void ShowMugen()
        {
            if (this._isDebugBreakMode)
                return;
            this._ignoreUnPauseRequestOnce = true;
            MugenWindow.SetWindowPos(this.Handle, 0, 0, 0, 0, 0, 16451);
        }

        public void HideMugen()
        {
            if (this.watcher.GetMugenProcess() == null)
                return;
            if (MugenWindow.IsWindowVisible(this.watcher.GetMugenWindowHandle()))
            {
                this._isMugenHidden = true;
                MugenWindow.SetWindowPos(this.watcher.GetMugenWindowHandle(), 0, 0, 0, this.backgroundBox.Size.Width, this.backgroundBox.Size.Height, 16532);
            }
            else
            {
                this._isMugenHidden = false;
                MugenWindow.SetWindowPos(this.watcher.GetMugenWindowHandle(), 0, 0, 0, this.backgroundBox.Size.Width, this.backgroundBox.Size.Height, 16532);
            }
        }

        public bool IsBusyMugen() => this.watcher.GetProcessWatcher() != null && this.watcher.GetProcessWatcher().IsBusy;

        public void RestartMugen()
        {
            this.CloseMugen();
            this.LoadMugen(this._workingProfileId);
        }

        /// <summary>
        /// cleanly close the process + stop monitors
        /// </summary>
        public void CloseMugen()
        {
            this.isWindowCaptured = false;
            // stop trigger breakpoint freeze
            this.stopDebugBreak();
            if (this.watcher.GetMugenProcess() != null)
            {
                // kill the monitor process
                while (this.watcher.GetProcessWatcher().IsBusy)
                {
                    this.watcher.GetProcessWatcher().CancelAsync();
                    Application.DoEvents();
                }
                if (this.watcher.GetMugenProcess() != null)
                {
                    if (!this.watcher.GetMugenProcess().HasExited)
                    {
                        if (!this._isDebugBreakMode)
                        {
                            // confirmation that the process is ready to be killed
                            if (this.watcher.GetMugenProcess().Responding)
                            {
                                MugenWindow.SetForegroundWindow(this.watcher.GetMugenWindowHandle());
                                this.watcher.GetMugenProcess(isUnsafe: true).CloseMainWindow();
                                if (this.watcher.MugenVersion == MugenType_t.MUGEN_TYPE_MUGEN10)
                                {
                                    this.watcher.KillAndAwaitMugenProcessEnd();
                                }
                                else
                                {
                                    int lpdwProcessId = 0;
                                    IntPtr hWnd = IntPtr.Zero;
                                    int num = 20;
                                    do
                                    {
                                        Thread.Sleep(50);
                                        if (num-- > 0)
                                        {
                                            hWnd = MugenWindow.GetForegroundWindow();
                                            int windowThreadProcessId = (int)MugenWindow.GetWindowThreadProcessId(hWnd, out lpdwProcessId);
                                        }
                                        else
                                            break;
                                    }
                                    while (hWnd == this.watcher.GetMugenWindowHandle() || lpdwProcessId != this.watcher.GetMugenProcess(isUnsafe: true).Id);
                                    if (hWnd != IntPtr.Zero && this.watcher.GetMugenProcess() != null && lpdwProcessId == this.watcher.GetMugenProcess().Id)
                                    {
                                        MugenWindow.PostMessage(hWnd, 273U, (IntPtr)6, (IntPtr)1967936);
                                        this.watcher.KillAndAwaitMugenProcessEnd();
                                    }
                                    else if (!this.watcher.GetMugenProcess(isUnsafe: true).HasExited)
                                    {
                                        this.watcher.KillAndAwaitMugenProcessEnd();
                                    }
                                }
                            }
                            else if (!this.watcher.GetMugenProcess().HasExited)
                            {
                                this.watcher.KillAndAwaitMugenProcessEnd();
                            }
                        }
                        else if (!this.watcher.GetMugenProcess().HasExited)
                        {
                            this.watcher.KillAndAwaitMugenProcessEnd();
                        }
                    }
                }
            }
            else
            {
                while (this.watcher.GetProcessWatcher().IsBusy)
                {
                    this.watcher.GetProcessWatcher().CancelAsync();
                    Application.DoEvents();
                }
            }
            this.SetTitleActive(false);
            // #3: revert back to backed-up config
            MugenProfile profile = ProfileManager.MainObj().GetProfile(this.GetWorkingProfileId());
            if (File.Exists(profile.GetMugenPath() + "/data/mugen.cfg.swissarmy.bak"))
            {
                // delete and overwrite
                File.Delete(profile.GetMugenPath() + "/data/mugen.cfg");
                File.Copy(profile.GetMugenPath() + "/data/mugen.cfg.swissarmy.bak", profile.GetMugenPath() + "/data/mugen.cfg");
                // delete backup
                File.Delete(profile.GetMugenPath() + "/data/mugen.cfg.swissarmy.bak");
            }
        }

        private void KillGracefully()
        {
            if (!this.watcher.CheckMugenProcessActive())
                return;
            if (this.watcher.GetMugenProcess().Responding)
            {
                this.InjectESC();
                if (this.watcher.GetMugenProcess().WaitForExit(2000))
                    return;
            }
            this.watcher.GetMugenProcess().Kill();
        }

        public void ResetNumOfGames() => this.numOfGames = 0;

        public void IncrementNumOfGames()
        {
            ++this.numOfGames;
            this._isRetryGame = false;
        }

        public void DecrementNumOfGames()
        {
            if (this.numOfGames <= 0)
                return;
            --this.numOfGames;
        }

        public bool SyncNumOfGames(int nog)
        {
            bool flag = false;
            if (this.numOfGames != nog)
            {
                this._isRetryGame = false;
                flag = true;
            }
            this.numOfGames = nog;
            return flag;
        }

        public bool IsRetryGame() => this._isRetryGame;

        public void SetRetryGame() => this._isRetryGame = true;

        public bool IsMugenCrashed() => this._isMugenCrashed;

        public bool IsGameQuitted() => this._isGameQuitted;

        public void SetGameQuitted() => this._isGameQuitted = true;

        public void ResetGameQuitted() => this._isGameQuitted = false;

        /// <summary>
        /// checks if Mugen process has experienced an error
        /// </summary>
        /// <returns></returns>
        public bool CheckMugenError()
        {
            bool result = false;
            // flag is set elsewhere (by monitor)
            if (this._isMugenCrashed)
                return this.watcher.CheckMugenProcessActive();
            if (this.watcher.GetMugenProcess() != null)
            {
                MugenProfile profile = ProfileManager.MainObj().GetProfile(this._workingProfileId);
                if (this.watcher.GetMugenProcess().HasExited)
                    result = false;
                // if it's frozen, we treat as an error/crash and prompt to close
                else if (this._isMugenFrozen)
                {
                    this._isMugenCrashed = true;
                    if (LogManager.MainObj() != null)
                        AsyncAppendLog("mugen has frozen." + Environment.NewLine);
                    result = true;
                    // log current player data to file+console (to help identify cause of freeze)
                    this.DumpPlayers();
                    bool foundMugen = false;
                    Process[] processes = Process.GetProcesses();
                    StringBuilder lpString = new StringBuilder(4132);
                    // make sure to identify+kill mugen
                    foreach (Process process in processes)
                    {
                        IntPtr mainWindowHandle = process.MainWindowHandle;
                        if (!mainWindowHandle.Equals((object)IntPtr.Zero))
                        {
                            mainWindowHandle = process.MainWindowHandle;
                            if (!mainWindowHandle.Equals((object)this.watcher.GetMugenWindowHandle()) && MugenWindow.GetWindowText(process.MainWindowHandle, lpString, lpString.Capacity) != 0)
                            {
                                string lower = lpString.ToString().ToLower();
                                if (lower.Contains("mugen") && lower.Contains(".exe"))
                                {
                                    foundMugen = true;
                                    process.CloseMainWindow();
                                }
                            }
                        }
                    }
                    // isautomode causes this to automatically die
                    if (profile != null && profile.IsAutoMode())
                    {
                        if (!foundMugen)
                            TimedMessageBox.Show("mugen has frozen. The process wil be terminated. \r\n\r\n(This dialog will automatically close.)", true);
                        else
                            TimedMessageBox.Show("mugen has been terminated. \r\n\r\n(This dialog will automatically close.)", true);
                        this.CloseMugen();
                    }
                    else if (!foundMugen)
                    {
                        if (this.watcher.CheckMugenProcessActive())
                        {
                            // prompt to kill the process
                            if (MessageBox.Show("mugen has frozen. Would you like to terminate the process?", "Error", MessageBoxButtons.YesNo) == DialogResult.Yes)
                                this.CloseMugen();
                            else
                                result = false;
                        }
                        else
                        {
                            TimedMessageBox.Show("mugen has been terminated. \r\n\r\n(This dialog will automatically close.)", true);
                            this.CloseMugen();
                        }
                    }
                    else
                    {
                        // kill automatically
                        TimedMessageBox.Show("mugen has been terminated. \r\n\r\n(This dialog will automatically close.)", true);
                        this.CloseMugen();
                    }
                }
                else
                    MugenWindow.EnumChildWindows(IntPtr.Zero, (MugenWindow.EnumChildProc)((hWnd, lParam) =>
                   {
                       int lpdwProcessId = 0;
                       int windowThreadProcessId = (int)MugenWindow.GetWindowThreadProcessId(hWnd, out lpdwProcessId);
                       if (this.watcher.CheckMugenProcessActive() && this.watcher.GetMugenProcess().Id == lpdwProcessId)
                       {
                           StringBuilder lpString = new StringBuilder(4132);
                           if (MugenWindow.IsWindowVisible(hWnd) && MugenWindow.GetWindowText(hWnd, lpString, lpString.Capacity) != 0)
                           {
                               string str1 = lpString.ToString();
                               if (str1.Contains("Error"))
                               {
                                   this._isMugenCrashed = true;
                                   if (profile != null && profile.IsAutoMode())
                                       MugenWindow.SetWindowPos(hWnd, 0, this.Location.X + 160, this.Location.Y + 100, 0, 0, 16401);
                                   result = true;
                                   if (LogManager.MainObj() != null)
                                   {
                                       AsyncAppendLog("Unexpected behavior has been detected." + Environment.NewLine);
                                       LogManager.MainObj().append("--------------------" + Environment.NewLine);
                                       LogManager.MainObj().append(str1 + Environment.NewLine);
                                   }
                                   MugenWindow.EnumChildWindows(hWnd, (MugenWindow.EnumChildProc)((hWnd2, lParam2) =>
                         {
                             StringBuilder lParam1 = new StringBuilder(4132);
                             MugenWindow.SendMessageText(hWnd2, 13U, lParam1.Capacity, lParam1);
                             string str = lParam1.ToString();
                             if (str != null && str != "" && !str.Contains("OK"))
                             {
                                 string msg = str.Replace("\n", "\r\n");
                                 if (LogManager.MainObj() != null)
                                 {
                                     LogManager.MainObj().append(msg);
                                     LogManager.MainObj().append(Environment.NewLine + "--------------------" + Environment.NewLine);
                                     this.DumpPlayers();
                                     if (profile != null && profile.IsAutoMode())
                                     {
                                         TimedMessageBox.DummyShow(1000);
                                         this.CloseMugen();
                                     }
                                 }
                             }
                             return true;
                         }), IntPtr.Zero);
                               }
                           }
                       }
                       return true;
                   }), IntPtr.Zero);
            }
            return result;
        }

        public void ActivateEx()
        {
            if (!this.watcher.CheckMugenProcessActive())
                return;
            int lpdwProcessId;
            int windowThreadProcessId = (int)MugenWindow.GetWindowThreadProcessId(MugenWindow.GetForegroundWindow(), out lpdwProcessId);
            if (lpdwProcessId != Process.GetCurrentProcess().Id || this._isActivated || Form.ActiveForm == this)
                return;
            this._isActivated = true;
            this.Activate();
        }

        public bool IsActivatedOnce() => this._isActivatedOnce;

        /// <summary>
        /// update the MugenWindow title to display timing info/active flag
        /// </summary>
        public void UpdateTitle()
        {
            DateTime now = DateTime.Now;
            string str = "M.U.G.E.N";
            if (this._isActive)
            {
                this._isActivatedOnce = true;
                str = str + now.ToString(" - HH:mm:ss -") + " (Active)";
            }
            this.Text = str;
        }

        public void SetTitleActive(bool isActive)
        {
            this._isActive = isActive;
            this.UpdateTitle();
        }

        public bool IsTitleActive() => this._isActive;

        /// <summary>
        /// pushes keypresses from MugenWindow to the subprocess Mugen
        /// </summary>
        /// <param name="c"></param>
        public void SendKey(char c)
        {
            uint Msg = 258;
            if (!this.watcher.CheckMugenProcessActive())
                return;
            MugenWindow.SendMessage1(this.watcher.GetMugenWindowHandle(), Msg, (int)c, 0);
        }

        private void MugenWindow_FormClosing(object sender, FormClosingEventArgs e) => this.CloseMugen();

        private void MugenWindow_FormClosed(object sender, FormClosedEventArgs e) => MugenWindow.selfObj = (MugenWindow)null;

        private void MugenWindow_Activated(object sender, EventArgs e)
        {
            if (!this.watcher.CheckMugenProcessActive())
                return;
            MugenWindow.SetWindowPos(this.watcher.GetMugenWindowHandle(), 0, 0, 0, this.backgroundBox.Size.Width, this.backgroundBox.Size.Height, 16400);
            if (!this._ignoreUnPauseRequestOnce)
                this.SetForegroundWindowEx(this.watcher.GetMugenWindowHandle());
            this._ignoreUnPauseRequestOnce = false;
        }

        private void MugenWindow_Deactivate(object sender, EventArgs e)
        {
            if (!(this.Cursor == Cursors.Help))
                return;
            this.toolTip1.Hide((IWin32Window)this);
        }

        private void backgroundBox_Paint(object sender, PaintEventArgs e)
        {
            Process mugenProcess = MugenWindow.MainObj().watcher.GetMugenProcess();
            if (mugenProcess == null || mugenProcess.HasExited)
                return;
            if (MugenWindow.IsWindowVisible(mugenProcess.MainWindowHandle))
            {
                if (this._isShownOnce)
                    return;
                this._isShownOnce = true;
                if (this.watcher.MugenVersion == MugenType_t.MUGEN_TYPE_MUGEN10)
                {
                    if (MugenWindow.GetParent(mugenProcess.MainWindowHandle) != this.backgroundBox.Handle)
                    {
                        this._isShownOnce = false;
                        int num = (int)MugenWindow.SetParent(mugenProcess.MainWindowHandle, this.backgroundBox.Handle);
                    }
                    uint windowLong = MugenWindow.GetWindowLong(mugenProcess.MainWindowHandle, -16);
                    if (((int)windowLong & 12582912) != 0)
                    {
                        this._isShownOnce = false;
                        uint unValue = (uint)((int)windowLong & -12582913 & int.MaxValue | 1073741824);
                        int num = (int)MugenWindow.SetWindowLong(mugenProcess.MainWindowHandle, -16, unValue);
                    }
                }
                MugenWindow.SetWindowPos(mugenProcess.MainWindowHandle, 0, 0, 0, this.backgroundBox.Size.Width, this.backgroundBox.Size.Height, 16384);
            }
            else
            {
                if (!this._isMugenHidden)
                    return;
                Font font = new Font("MS UI Gothic", 60f);
                PointF point = new PointF(100f, 160f);
                e.Graphics.FillRectangle(Brushes.Black, 90, 150, 480, 100);
                e.Graphics.DrawString("Please wait...", font, Brushes.White, point);
            }
        }

        private void DisplayWindowHelp()
        {
            if (!(this.backgroundBox.Cursor == Cursors.Help))
                return;
            this.toolTip1.Hide((IWin32Window)this);
            this.toolTip1.IsBalloon = false;
            this.toolTip1.IsBalloon = true;
            string text = "Mugen will be displayed inside this window." + Environment.NewLine + Environment.NewLine + "The current time is also displayed in the title of the window." + Environment.NewLine + "Additionally, the window will display (Active) when mugen is the current active window." + Environment.NewLine + "　Example:  M.U.G.E.N - 15:38:00 - (Active)" + Environment.NewLine + Environment.NewLine + "You can make the mugen window active by clicking on it." + Environment.NewLine;
            this.toolTip1.SetToolTip((Control)this.backgroundBox, " ");
            this.toolTip1.Show(text, (IWin32Window)this.backgroundBox, this.backgroundBox.Width / 2, this.backgroundBox.Height / 2);
        }

        private void backgroundBox_MouseClick(object sender, MouseEventArgs e) => this.DisplayWindowHelp();

        public void SetHelpMode(bool isHelpMode)
        {
            if (isHelpMode)
            {
                this.Cursor = Cursors.Help;
                this.backgroundBox.Cursor = Cursors.Help;
            }
            else
            {
                this.Cursor = Cursors.Default;
                this.backgroundBox.Cursor = Cursors.Default;
            }
        }

        /// <summary>
        /// indicates if experimental breakpoints are enabled
        /// </summary>
        /// <returns></returns>
        public bool GetIsExperimental() => ProfileManager.MainObj().GetProfile(this._workingProfileId).IsExperimentalBreakpoints();

        /// <summary>
        /// unset an existing experimental BP
        /// </summary>
        public void RemoveExpBP()
        {
            this.watchAddr = 0U;
            this.WatchInitVal = 0U;
        }

        /// <summary>
        /// gets the display name (this is relative to the addr returned by <c>GetPlayerInfoAdder</c>)
        /// </summary>
        /// <param name="playerAddr"></param>
        /// <param name="displayName"></param>
        /// <returns></returns>
        private int GetDisplayName(uint playerAddr, ref string displayName)
        {
            int numBytes = PlayerUtils.GetDisplayName(this.watcher, playerAddr, ref displayName);
            if (numBytes <= 0 || displayName.Length <= 0) return 0;
            return displayName.Length;
        }

        /// <summary>
        /// helper function to find a player slot/index from player ID.
        /// <br/>iterates all 60 possible indices until it finds one that matches the input ID.
        /// </summary>
        /// <param name="baseAddr"></param>
        /// <param name="playerId"></param>
        /// <returns></returns>
        private int GetPlayerNoFromId(uint baseAddr, int playerId)
        {
            for (int index = 0; index < 60; ++index)
            {
                uint playerAddr = (uint)GameUtils.GetPlayerAddress(this.watcher, baseAddr, index + 1);
                if (playerAddr != 0U && PlayerUtils.DoesPlayerExist(this.watcher, playerAddr) && playerId == PlayerUtils.GetPlayerId(this.watcher, playerAddr))
                    return index;
            }
            return -1;
        }

        /// <summary>
        /// helper function to find a player address from player ID.
        /// <br/>iterates all 60 possible indices until it finds one that matches the input ID.
        /// </summary>
        /// <param name="baseAddr"></param>
        /// <param name="playerId"></param>
        /// <returns></returns>
        private uint GetPlayerAddrFromId(uint baseAddr, int playerId)
        {
            for (int index = 0; index < 60; ++index)
            {
                uint playerAddr = (uint)GameUtils.GetPlayerAddress(this.watcher, baseAddr, index + 1);
                if (playerAddr != 0U && PlayerUtils.DoesPlayerExist(this.watcher, playerAddr) && playerId == PlayerUtils.GetPlayerId(this.watcher, playerAddr))
                    return playerAddr;
            }
            return 0;
        }

        /// <summary>
        /// helper function to find a player slot/index from helper ID.
        /// <br/>iterates all 56 possible Helper indices until it finds one that matches the input ID.
        /// <br/>additionally checks that the root ID for the Helper matches the input owner.
        /// </summary>
        /// <param name="baseAddr"></param>
        /// <param name="helperId"></param>
        /// <param name="owner"></param>
        /// <returns></returns>
        private uint GetPlayerAddrFromHelperId(uint baseAddr, int helperId, int owner)
        {
            uint playerAddr1 = (uint)GameUtils.GetPlayerAddress(this.watcher, baseAddr, owner + 1);
            if (playerAddr1 == 0U)
                return 0;
            int playerId = PlayerUtils.GetPlayerId(this.watcher, playerAddr1);
            if (playerId == 0)
                return 0;
            for (int index = 4; index < 60; ++index)
            {
                uint playerAddr2 = (uint)GameUtils.GetPlayerAddress(this.watcher, baseAddr, index + 1);
                if (playerAddr2 != 0U && PlayerUtils.DoesPlayerExist(this.watcher, playerAddr2) && helperId == PlayerUtils.GetHelperId(this.watcher, playerAddr2))
                {
                    int rootId = this.GetRootId(baseAddr, PlayerUtils.GetPlayerId(this.watcher, playerAddr2));
                    if (playerId == rootId)
                        return playerAddr2;
                }
            }
            return 0;
        }

        /// <summary>
        /// gets the root ID for a given helper's player ID
        /// </summary>
        /// <param name="baseAddr"></param>
        /// <param name="myId"></param>
        /// <returns></returns>
        private int GetRootId(uint baseAddr, int myId)
        {
            int num = 0;
            uint playerAddr;
            for (int playerId = myId; playerId > 0; playerId = playerAddr <= 0U ? 0 : PlayerUtils.GetParentId(this.watcher, playerAddr))
            {
                num = playerId;
                int playerNoFromId = this.GetPlayerNoFromId(baseAddr, playerId);
                if (playerNoFromId < 0)
                    return num;
                playerAddr = (uint)GameUtils.GetPlayerAddress(this.watcher, baseAddr, playerNoFromId + 1);
            }
            return num;
        }

        private int GetSpecialStateNo(uint playerAddr)
        {
            if (this._debugSpPointer == 0U)
                return 0;
            uint num = 544;
            for (uint index = 0; index <= 128U; index += 4U)
            {
                if (this.watcher.GetInt32Data(this._debugSpPointer, num + index) == 1 && (int)playerAddr == this.watcher.GetInt32Data(this._debugSpPointer, (uint)((int)num + (int)index + 48)))
                {
                    int int32Data = this.watcher.GetInt32Data(this._debugSpPointer, (uint)((int)num + (int)index + 152));
                    switch (int32Data)
                    {
                        case -3:
                        case -2:
                        case -1:
                            return int32Data;
                        default:
                            continue;
                    }
                }
            }
            return 0;
        }

        public MugenProcessWatcher GetWatcher()
        {
            return this.watcher;
        }

        /// <summary>
        /// sets the color of the debug text.
        /// </summary>
        /// <param name="baseAddr"></param>
        /// <param name="debugColor">DebugColor enum to use</param>
        private void SetDebugColor(uint baseAddr, DebugColor debugColor)
        {
            if (this.watcher.MugenDatabase.USE_NEW_DEBUG_COLOR_ADDR)
            {
                this.SetDebugColorEX(baseAddr, debugColor);
                return;
            }
            switch (debugColor)
            {
                case DebugColor.WHITE:
                    this.watcher.SetInt32Data(baseAddr, this.watcher.MugenDatabase.PAL_TIME_BASE_OFFSET, (int)byte.MaxValue);
                    this.watcher.SetInt32Data(baseAddr, this.watcher.MugenDatabase.PAL_R_BASE_OFFSET, 0);
                    this.watcher.SetInt32Data(baseAddr, this.watcher.MugenDatabase.PAL_G_BASE_OFFSET, 0);
                    this.watcher.SetInt32Data(baseAddr, this.watcher.MugenDatabase.PAL_B_BASE_OFFSET, 0);
                    break;
                case DebugColor.YELLOW:
                    this.watcher.SetInt32Data(baseAddr, this.watcher.MugenDatabase.PAL_TIME_BASE_OFFSET, (int)byte.MaxValue);
                    this.watcher.SetInt32Data(baseAddr, this.watcher.MugenDatabase.PAL_R_BASE_OFFSET, 0);
                    this.watcher.SetInt32Data(baseAddr, this.watcher.MugenDatabase.PAL_G_BASE_OFFSET, 0);
                    this.watcher.SetInt32Data(baseAddr, this.watcher.MugenDatabase.PAL_B_BASE_OFFSET, -255);
                    break;
                case DebugColor.PURPLE:
                    this.watcher.SetInt32Data(baseAddr, this.watcher.MugenDatabase.PAL_TIME_BASE_OFFSET, (int)byte.MaxValue);
                    this.watcher.SetInt32Data(baseAddr, this.watcher.MugenDatabase.PAL_R_BASE_OFFSET, 0);
                    this.watcher.SetInt32Data(baseAddr, this.watcher.MugenDatabase.PAL_G_BASE_OFFSET, -255);
                    this.watcher.SetInt32Data(baseAddr, this.watcher.MugenDatabase.PAL_B_BASE_OFFSET, 0);
                    break;
                case DebugColor.RED:
                    this.watcher.SetInt32Data(baseAddr, this.watcher.MugenDatabase.PAL_TIME_BASE_OFFSET, (int)byte.MaxValue);
                    this.watcher.SetInt32Data(baseAddr, this.watcher.MugenDatabase.PAL_R_BASE_OFFSET, 0);
                    this.watcher.SetInt32Data(baseAddr, this.watcher.MugenDatabase.PAL_G_BASE_OFFSET, -255);
                    this.watcher.SetInt32Data(baseAddr, this.watcher.MugenDatabase.PAL_B_BASE_OFFSET, -255);
                    break;
                case DebugColor.BLACK:
                    this.watcher.SetInt32Data(baseAddr, this.watcher.MugenDatabase.PAL_TIME_BASE_OFFSET, (int)byte.MaxValue);
                    this.watcher.SetInt32Data(baseAddr, this.watcher.MugenDatabase.PAL_R_BASE_OFFSET, -255);
                    this.watcher.SetInt32Data(baseAddr, this.watcher.MugenDatabase.PAL_G_BASE_OFFSET, -255);
                    this.watcher.SetInt32Data(baseAddr, this.watcher.MugenDatabase.PAL_B_BASE_OFFSET, -255);
                    break;
                case DebugColor.GREEN:
                    this.watcher.SetInt32Data(baseAddr, this.watcher.MugenDatabase.PAL_TIME_BASE_OFFSET, (int)byte.MaxValue);
                    this.watcher.SetInt32Data(baseAddr, this.watcher.MugenDatabase.PAL_R_BASE_OFFSET, -255);
                    this.watcher.SetInt32Data(baseAddr, this.watcher.MugenDatabase.PAL_G_BASE_OFFSET, 0);
                    this.watcher.SetInt32Data(baseAddr, this.watcher.MugenDatabase.PAL_B_BASE_OFFSET, -255);
                    break;
                default:
                    this.watcher.SetInt32Data(baseAddr, this.watcher.MugenDatabase.PAL_TIME_BASE_OFFSET, 1);
                    this.watcher.SetInt32Data(baseAddr, this.watcher.MugenDatabase.PAL_R_BASE_OFFSET, 0);
                    this.watcher.SetInt32Data(baseAddr, this.watcher.MugenDatabase.PAL_G_BASE_OFFSET, 0);
                    this.watcher.SetInt32Data(baseAddr, this.watcher.MugenDatabase.PAL_B_BASE_OFFSET, 0);
                    break;
            }
        }

        /// <summary>
        /// sets the color of the debug text, in a way that works with Mugen 1.0+.
        /// </summary>
        /// <param name="baseAddr"></param>
        /// <param name="debugColor">DebugColor enum to use</param>
        private void SetDebugColorEX(uint baseAddr, DebugColor debugColor)
        {
            // iterate all the new debug color offsets
            foreach (uint colorBase in this.watcher.MugenDatabase.NEW_DEBUG_COLOR_OFFSETS)
            {
                this.ApplyDebugColorInt(baseAddr, colorBase, debugColor);
            }
            // stateno is split up, so apply differently
            if (this.watcher.MugenDatabase.NEW_DEBUG_COLOR_SN_OFFSETS.Length == 3)
                this.ApplyDebugColorSplit(baseAddr, this.watcher.MugenDatabase.NEW_DEBUG_COLOR_SN_OFFSETS, debugColor);
        }

        /// <summary>
        /// helper function for Mugen 1.0+ debug text coloring
        /// <br/>this one sets three disjointed offsets as individual red, green, blue offsets
        /// <br/>keep in mind each offset is 5 bytes long (as we are modifying instructions here,
        /// <br/>so the value 256 is actually `68 00 01 00 00`, 68=push)
        /// </summary>
        /// <param name="baseAddr"></param>
        /// <param name="offsets"></param>
        /// <param name="debugColor"></param>
        private void ApplyDebugColorSplit(uint baseAddr, uint[] offsets, DebugColor debugColor)
        {
            switch (debugColor)
            {
                case DebugColor.WHITE:
                    this.watcher.SetInt32Data(offsets[0], 1, 256);
                    this.watcher.SetInt32Data(offsets[1], 1, 256);
                    this.watcher.SetInt32Data(offsets[2], 1, 256);
                    break;
                case DebugColor.YELLOW:
                    this.watcher.SetInt32Data(offsets[0], 1, 0);
                    this.watcher.SetInt32Data(offsets[1], 1, 256);
                    this.watcher.SetInt32Data(offsets[2], 1, 256);
                    break;
                case DebugColor.PURPLE:
                    this.watcher.SetInt32Data(offsets[0], 1, 256);
                    this.watcher.SetInt32Data(offsets[1], 1, 0);
                    this.watcher.SetInt32Data(offsets[2], 1, 256);
                    break;
                case DebugColor.RED:
                    this.watcher.SetInt32Data(offsets[0], 1, 0);
                    this.watcher.SetInt32Data(offsets[1], 1, 0);
                    this.watcher.SetInt32Data(offsets[2], 1, 256);
                    break;
                case DebugColor.BLACK:
                    this.watcher.SetInt32Data(offsets[0], 1, 0);
                    this.watcher.SetInt32Data(offsets[1], 1, 0);
                    this.watcher.SetInt32Data(offsets[2], 1, 0);
                    break;
                case DebugColor.GREEN:
                    this.watcher.SetInt32Data(offsets[0], 1, 0);
                    this.watcher.SetInt32Data(offsets[1], 1, 256);
                    this.watcher.SetInt32Data(offsets[2], 1, 0);
                    break;
                case DebugColor.CUSTOM:
                    this.watcher.SetInt32Data(offsets[0], 1, this.customDebugColors[2]);
                    this.watcher.SetInt32Data(offsets[1], 1, this.customDebugColors[1]);
                    this.watcher.SetInt32Data(offsets[2], 1, this.customDebugColors[0]);
                    break;
                default:
                    this.watcher.SetInt32Data(offsets[0], 1, 256);
                    this.watcher.SetInt32Data(offsets[1], 1, 256);
                    this.watcher.SetInt32Data(offsets[2], 1, 256);
                    break;
            }
        }

        /// <summary>
        /// helper function for Mugen 1.0+ debug text coloring
        /// <br/>keep in mind each offset is 5 bytes long (as we are modifying instructions here,
        /// <br/>so the value 256 is actually `68 00 01 00 00`, 68=push)
        /// </summary>
        /// <param name="baseAddr"></param>
        /// <param name="offsets"></param>
        /// <param name="debugColor"></param>
        private void ApplyDebugColorInt(uint baseAddr, uint colorBase, DebugColor debugColor)
        {
            switch (debugColor)
            {
                case DebugColor.WHITE:
                    this.watcher.SetInt32Data(colorBase, 1, 256);
                    this.watcher.SetInt32Data(colorBase, 6, 256);
                    this.watcher.SetInt32Data(colorBase, 11, 256);
                    break;
                case DebugColor.YELLOW:
                    this.watcher.SetInt32Data(colorBase, 1, 0);
                    this.watcher.SetInt32Data(colorBase, 6, 256);
                    this.watcher.SetInt32Data(colorBase, 11, 256);
                    break;
                case DebugColor.PURPLE:
                    this.watcher.SetInt32Data(colorBase, 1, 256);
                    this.watcher.SetInt32Data(colorBase, 6, 0);
                    this.watcher.SetInt32Data(colorBase, 11, 256);
                    break;
                case DebugColor.RED:
                    this.watcher.SetInt32Data(colorBase, 1, 0);
                    this.watcher.SetInt32Data(colorBase, 6, 0);
                    this.watcher.SetInt32Data(colorBase, 11, 256);
                    break;
                case DebugColor.BLACK:
                    this.watcher.SetInt32Data(colorBase, 1, 0);
                    this.watcher.SetInt32Data(colorBase, 6, 0);
                    this.watcher.SetInt32Data(colorBase, 11, 0);
                    break;
                case DebugColor.GREEN:
                    this.watcher.SetInt32Data(colorBase, 1, 0);
                    this.watcher.SetInt32Data(colorBase, 6, 256);
                    this.watcher.SetInt32Data(colorBase, 11, 0);
                    break;
                case DebugColor.CUSTOM:
                    this.watcher.SetInt32Data(colorBase, 1, this.customDebugColors[2]);
                    this.watcher.SetInt32Data(colorBase, 6, this.customDebugColors[1]);
                    this.watcher.SetInt32Data(colorBase, 11, this.customDebugColors[0]);
                    break;
                default:
                    this.watcher.SetInt32Data(colorBase, 1, 256);
                    this.watcher.SetInt32Data(colorBase, 6, 256);
                    this.watcher.SetInt32Data(colorBase, 11, 256);
                    break;
            }
        }

        public void SetDebugCustomColors(int red, int green, int blue)
        {
            this.customDebugColors = new int[] { red, green, blue };
        }

        public void SetSpeedMode(bool isSpeedMode)
        {
            if (this._isSpeedMode != isSpeedMode)
                this._isSpeedModeChanged = true;
            this._isSpeedMode = isSpeedMode;
        }

        public void SetSkipMode(bool isSkipMode) => this._isSkipMode = isSkipMode;

        public void SetStepMode(bool isOn)
        {
            this._isStepMode = isOn;
            this._stepModeCounter = 0L;
            this._stepModeLastCounter = 0L;
        }

        public void SetStepInterval(long interval) => this._stepModeInterval = interval;

        public void SetSkipFrames(int frames) => this._skipModeFrames = frames;

        public void SetDebugMode(bool isDebugMode, bool forceUpdate)
        {
            if (this._isDebugMode != isDebugMode | forceUpdate)
                this._isDebugModeChanged = true;
            this._isDebugMode = isDebugMode;
        }

        public void SetDebugTargetNo(int targetNo)
        {
            if (this._debugTargetNo != targetNo)
                this._isDebugTargetNoChanged = true;
            this._debugTargetNo = targetNo;
        }

        public void SetVarInspectTargetNo(int targetNo) => this._varInspectTargetNo = targetNo;

        public void SetDebugTargetPlayer(int playerId) => this._debugTargetPlayer = playerId;

        public void SetVarInspectTargetPlayer(int playerId) => this._varInspectTargetPlayer = playerId;

        public int GetCurrentDebugTargetNo() => this._debugTargetNo;

        public void SetDebugListMode(MugenWindow.DebugListMode mode) => this._debugListMode = mode;

        public void SetExplodHeadNo(int headNo) => this._explodHeadNo = headNo;

        public void SetProjHeadNo(int headNo) => this._projHeadNo = headNo;

        public void SetProjOwner(MugenWindow.ProjOwner owner) => this._projOwner = owner;

        public void SetDebugColor(DebugColor color)
        {
            if (this._debugColor != color)
                this._debugColorChanged = true;
            this._debugColor = color;
        }

        public void DumpPlayers() => this._flagDumpPlayers = true;

        /// <summary>
        /// force a stepframe to run
        /// </summary>
        public void InjectStepCommand()
        {
            if (this.watcher.MugenVersion != MugenType_t.MUGEN_TYPE_WINMUGEN)
                GameUtils.InjectCommand(this.watcher, 46);
            else
                GameUtils.InjectCommand(this.watcher, 70);
        }

        public void InjectESC()
        {
            if (this.watcher.MugenVersion != MugenType_t.MUGEN_TYPE_WINMUGEN)
                GameUtils._InjectCommand(this.watcher, 27);
            else
                GameUtils.InjectCommand(this.watcher, 1);
        }

        public void InjectF4()
        {
            if (this.watcher.MugenVersion != MugenType_t.MUGEN_TYPE_WINMUGEN)
                GameUtils.InjectCommand(this.watcher, 29);
            else
                GameUtils.InjectCommand(this.watcher, 62);
        }

        private void _DumpPlayers(uint baseAddr)
        {
            int[] playerId = new int[60];
            int[] helperId = new int[60];
            int[] stateno = new int[60];
            int[] prevstateno = new int[60];
            string[] name = new string[60];
            for (int index = 0; index < 60; ++index)
            {
                uint playerAddr = (uint)GameUtils.GetPlayerAddress(this.watcher, baseAddr, index + 1);
                if (playerAddr != 0U && PlayerUtils.DoesPlayerExist(this.watcher, playerAddr))
                {
                    playerId[index] = PlayerUtils.GetPlayerId(this.watcher, playerAddr);
                    helperId[index] = PlayerUtils.GetHelperId(this.watcher, playerAddr);
                    stateno[index] = PlayerUtils.GetStateNo(this.watcher, playerAddr);
                    prevstateno[index] = PlayerUtils.GetPrevStateNo(this.watcher, playerAddr);
                    this.GetDisplayName(playerAddr, ref name[index]);
                }
                else
                {
                    playerId[index] = -1;
                    helperId[index] = -1;
                    stateno[index] = -1;
                    prevstateno[index] = -1;
                    name[index] = "-";
                }
            }
            if (LogManager.MainObj() == null)
                return;
            this.BeginInvoke((Action)(() => LogManager.MainObj().DumpPlayers(60, playerId, helperId, stateno, prevstateno, name)))?.AsyncWaitHandle.WaitOne(this._invokeWaitTime);
        }

        private void ListUpPlayers(uint baseAddr)
        {
            for (int index = 0; index < 60; ++index)
            {
                uint playerAddr = (uint)GameUtils.GetPlayerAddress(this.watcher, baseAddr, index + 1);
                if (playerAddr != 0U && PlayerUtils.DoesPlayerExist(this.watcher, playerAddr))
                {
                    this.mugenPlayerId[index] = PlayerUtils.GetPlayerId(this.watcher, playerAddr);
                    this.muenHelperId[index] = index < 4 ? -1 : PlayerUtils.GetHelperId(this.watcher, playerAddr);
                    this.muenParentId[index] = index < 4 ? -1 : PlayerUtils.GetParentId(this.watcher, playerAddr);
                    this.muenOwnerId[index] = PlayerUtils.GetStateOwner(this.watcher, baseAddr, playerAddr);
                    this.GetDisplayName(playerAddr, ref this.mugenName[index]);
                }
                else
                {
                    this.mugenPlayerId[index] = -1;
                    this.muenHelperId[index] = -1;
                    this.muenParentId[index] = -1;
                    this.muenOwnerId[index] = -1;
                    this.mugenName[index] = "-";
                }
            }
            if (DebugForm.MainObj() == null)
                return;
            this.BeginInvoke((Action)(() => DebugForm.MainObj().DisplayPlayers(60, this.mugenPlayerId, this.muenHelperId, this.muenParentId, this.muenOwnerId, this.mugenName)))?.AsyncWaitHandle.WaitOne(this._invokeWaitTime);
        }

        private void ListUpExplods(uint baseAddr)
        {
            uint explodListAdder = (uint)GameUtils.GetExplodListAddress(this.watcher, baseAddr);
            if (explodListAdder == 0U)
                return;
            uint playerAddr = 0;
            if (this._varInspectTargetNo > 0)
                playerAddr = (uint)GameUtils.GetPlayerAddress(this.watcher, baseAddr, this._varInspectTargetNo);
            int playerId = 0;
            if (playerAddr != 0U)
                playerId = PlayerUtils.GetPlayerId(this.watcher, playerAddr);
            for (int index = 0; index < 50; ++index)
            {
                uint explodAdder = GameUtils.GetExplodAddress(this.watcher, explodListAdder, (uint)(this._explodHeadNo + index));
                if (GameUtils.DoesExplodExist(this.watcher, explodAdder))
                {
                    int explodOwnerId = GameUtils.GetExplodOwnerId(this.watcher, explodAdder);
                    if (explodOwnerId >= this.player1Id)
                    {
                        this.mugenExplodId[index] = GameUtils.GetExplodId(this.watcher, explodAdder);
                        int animIndex = GameUtils.GetAnimIndex(this.watcher, explodAdder);
                        int rootId = this.GetRootId(baseAddr, explodOwnerId);
                        uint animListAddr = rootId != this.player1Id ? (rootId != this.player2Id ? (rootId != this.player3Id ? (rootId != this.player4Id ? this.player1AnimAddr : this.player4AnimAddr) : this.player3AnimAddr) : this.player2AnimAddr) : this.player1AnimAddr;
                        this.mugenOwnerId[index] = explodOwnerId;
                        this.mugenAnim[index] = PlayerUtils.GetAnimNo(this.watcher, animListAddr, animIndex);
                    }
                    else
                    {
                        this.mugenExplodId[index] = -1;
                        this.mugenOwnerId[index] = -1;
                        this.mugenAnim[index] = -1;
                    }
                }
                else
                {
                    this.mugenExplodId[index] = -1;
                    this.mugenOwnerId[index] = -1;
                    this.mugenAnim[index] = -1;
                }
            }
            if (DebugForm.MainObj() == null)
                return;
            this.BeginInvoke((Action)(() => DebugForm.MainObj().DisplayExplods(playerId, this._explodHeadNo, 50, this.mugenExplodId, this.mugenAnim, this.mugenOwnerId)))?.AsyncWaitHandle.WaitOne(this._invokeWaitTime);
        }

        private void ListUpProjs(int playerId, uint playerAddr)
        {
            if (playerId == 0 || playerAddr == 0U)
                return;
            uint projBaseAdder = (uint)PlayerUtils.GetProjBaseAddress(this.watcher, playerAddr);
            if (projBaseAdder == 0U)
                return;
            uint projListAdder = (uint)PlayerUtils.GetProjListAddress(this.watcher, projBaseAdder);
            if (projListAdder == 0U)
                return;
            uint animAdder = PlayerUtils.GetAnimListAddr(this.watcher, (uint)GameUtils.GetBaseAddress(this.watcher), playerAddr);
            if (animAdder == 0U)
                return;
            for (int index = 0; index < 50; ++index)
            {
                uint projAdder = PlayerUtils.GetProjAddress(this.watcher, projListAdder, (uint)(this._projHeadNo + index));
                int projAnimIdx = this.watcher.GetInt32Data(projAdder, this.watcher.MugenDatabase.PROJ_ANIM_INDEX_PROJ_OFFSET);
                if (PlayerUtils.DoesProjExist(this.watcher, projBaseAdder, this._projHeadNo + index, projAdder))
                {
                    this.mugenProjId[index] = PlayerUtils.GetProjId(this.watcher, projAdder);
                    this.mugenProjX[index] = PlayerUtils.GetProjX(this.watcher, playerAddr, projAdder);
                    this.mugenProjY[index] = PlayerUtils.GetProjY(this.watcher, playerAddr, projAdder);
                    this.mugenProjAnim[index] = PlayerUtils.GetAnimNo(this.watcher, animAdder, projAnimIdx);
                }
                else
                {
                    this.mugenProjId[index] = -1;
                    this.mugenProjX[index] = 0;
                    this.mugenProjY[index] = 0;
                    this.mugenProjAnim[index] = 0;
                }
            }
            if (DebugForm.MainObj() == null)
                return;
            this.BeginInvoke((Action)(() => DebugForm.MainObj().DisplayProjs(playerId, this._projHeadNo, 50, this.mugenProjId, this.mugenProjX, this.mugenProjY, this.mugenProjAnim)))?.AsyncWaitHandle.WaitOne(this._invokeWaitTime);
        }

        /// <summary>
        /// update all of the data the current debug player.
        /// </summary>
        /// <param name="baseAddr"></param>
        private void UpdateVariables(uint baseAddr)
        {
            uint playerAddr = 0;
            if (this._varInspectTargetNo > 0)
                playerAddr = (uint)GameUtils.GetPlayerAddress(this.watcher, baseAddr, this._varInspectTargetNo);
            int playerId = 0;
            if (playerAddr != 0U)
                playerId = PlayerUtils.GetPlayerId(this.watcher, playerAddr);
            this.mugenSysInfo.alive = PlayerUtils.GetAlive(this.watcher, playerAddr);
            this.mugenSysInfo.life = PlayerUtils.GetLife(this.watcher, playerAddr);
            this.mugenSysInfo.palno = PlayerUtils.GetPalno(this.watcher, playerAddr);
            this.mugenSysInfo.hitpausetime = PlayerUtils.GetHitPauseTime(this.watcher, playerAddr);
            switch (this._varInspectTargetNo)
            {
                case 1:
                case 3:
                    this.mugenSysInfo.win = GameUtils.IsTeam1Win(this.watcher) ? 1 : 0;
                    this.mugenSysInfo.lose = GameUtils.IsTeam2Win(this.watcher) ? 1 : 0;
                    break;
                case 2:
                case 4:
                    this.mugenSysInfo.win = GameUtils.IsTeam2Win(this.watcher) ? 1 : 0;
                    this.mugenSysInfo.lose = GameUtils.IsTeam1Win(this.watcher) ? 1 : 0;
                    break;
                default:
                    this.mugenSysInfo.win = -1;
                    this.mugenSysInfo.lose = -1;
                    break;
            }
            this.mugenSysInfo.stateowner = PlayerUtils.GetStateOwner(this.watcher, baseAddr, playerAddr);
            this.mugenSysInfo.stateno = PlayerUtils.GetStateNo(this.watcher, playerAddr);
            this.mugenSysInfo.specialstateno = !this._isDebugBreakMode ? 0 : this.GetSpecialStateNo(playerAddr);
            this.mugenSysInfo.prevstateno = PlayerUtils.GetPrevStateNo(this.watcher, playerAddr);
            this.mugenSysInfo.roundstate = GameUtils.GetRoundState(this.watcher);
            this.mugenSysInfo.roundtime = GameUtils.GetRoundTime(this.watcher);
            this.mugenSysInfo.power = PlayerUtils.GetPower(this.watcher, playerAddr);
            this.mugenSysInfo.ctrl = PlayerUtils.GetCtrl(this.watcher, playerAddr);
            this.mugenSysInfo.damage = PlayerUtils.GetDamage(this.watcher, playerAddr);
            this.mugenSysInfo.statetype = PlayerUtils.GetStateType(this.watcher, playerAddr);
            this.mugenSysInfo.movetype = PlayerUtils.GetMoveType(this.watcher, playerAddr);
            this.mugenSysInfo.posx = PlayerUtils.GetPosX(this.watcher, baseAddr, playerAddr);
            this.mugenSysInfo.posy = PlayerUtils.GetPosY(this.watcher, baseAddr, playerAddr);
            this.mugenSysInfo.velx = PlayerUtils.GetVelX(this.watcher, baseAddr, playerAddr);
            this.mugenSysInfo.vely = PlayerUtils.GetVelY(this.watcher, baseAddr, playerAddr);
            this.mugenSysInfo.localx = PlayerUtils.GetLocalCoordX(this.watcher, playerAddr);
            this.mugenSysInfo.localy = PlayerUtils.GetLocalCoordY(this.watcher, playerAddr);
            this.mugenSysInfo.facing = PlayerUtils.GetFacing(this.watcher, playerAddr);
            this.mugenSysInfo.fall_damage = PlayerUtils.GetFallDamage(this.watcher, playerAddr);
            this.mugenSysInfo.movecontact = PlayerUtils.GetMoveContact(this.watcher, playerAddr);
            this.mugenSysInfo.movehit = PlayerUtils.GetMoveHit(this.watcher, playerAddr);
            this.mugenSysInfo.moveguarded = PlayerUtils.GetMoveGuarded(this.watcher, playerAddr);
            this.mugenSysInfo.movereversed = PlayerUtils.GetMoveReversed(this.watcher, playerAddr);
            this.mugenSysInfo.active = PlayerUtils.GetActiveFlag(this.watcher, baseAddr, this._varInspectTargetNo - 1);
            this.mugenSysInfo.assertFlags = GameUtils.GetGlobalAssertSpecials(this.watcher, baseAddr);
            this.mugenSysInfo.selfAssertFlags = PlayerUtils.GetSelfAssertSpecials(this.watcher, playerAddr);
            this.mugenSysInfo.noko = this.mugenSysInfo.assertFlags[(int)VarForm.SysInfo_t.GlobalAssertFlags.NoKO] ? 1 : 0;
            this.mugenSysInfo.intro = this.mugenSysInfo.assertFlags[(int)VarForm.SysInfo_t.GlobalAssertFlags.Intro] ? 1 : 0;
            this.mugenSysInfo.roundnotover = this.mugenSysInfo.assertFlags[(int)VarForm.SysInfo_t.GlobalAssertFlags.RoundNotOver] ? 1 : 0;
            this.mugenSysInfo.timerfreeze = this.mugenSysInfo.assertFlags[(int)VarForm.SysInfo_t.GlobalAssertFlags.TimerFreeze] ? 1 : 0;
            this.mugenSysInfo.muteki = PlayerUtils.GetMuteki(this.watcher, playerAddr);
            this.mugenSysInfo.noclsn2 = PlayerUtils.GetNoClsn2Flag(this.watcher, playerAddr);
            this.mugenSysInfo.hasclsn1 = PlayerUtils.GetHasClsn1Flag(this.watcher, playerAddr) == 1;
            this.mugenSysInfo.hitby = PlayerUtils.GetHitBy(this.watcher, playerAddr);
            this.mugenSysInfo.hitoverride0 = PlayerUtils.GetHitOverRide(this.watcher, playerAddr, 0);
            this.mugenSysInfo.hitoverride1 = PlayerUtils.GetHitOverRide(this.watcher, playerAddr, 1);
            this.mugenSysInfo.hitoverride2 = PlayerUtils.GetHitOverRide(this.watcher, playerAddr, 2);
            this.mugenSysInfo.target1 = PlayerUtils.GetTarget(this.watcher, playerAddr, 0);
            this.mugenSysInfo.target2 = PlayerUtils.GetTarget(this.watcher, playerAddr, 1);
            this.mugenSysInfo.target3 = PlayerUtils.GetTarget(this.watcher, playerAddr, 2);
            this.mugenSysInfo.target4 = PlayerUtils.GetTarget(this.watcher, playerAddr, 3);
            this.mugenSysInfo.target5 = PlayerUtils.GetTarget(this.watcher, playerAddr, 4);
            this.mugenSysInfo.target6 = PlayerUtils.GetTarget(this.watcher, playerAddr, 5);
            this.mugenSysInfo.target7 = PlayerUtils.GetTarget(this.watcher, playerAddr, 6);
            this.mugenSysInfo.target8 = PlayerUtils.GetTarget(this.watcher, playerAddr, 7);
            this.mugenSysInfo.pausetime = GameUtils.GetPauseTime(this.watcher, baseAddr);
            this.mugenSysInfo.superpausetime = GameUtils.GetSuperPauseTime(this.watcher, baseAddr);
            this.mugenSysInfo.pausemovetime = PlayerUtils.GetPauseMoveTime(this.watcher, playerAddr);
            this.mugenSysInfo.superpausemovetime = PlayerUtils.GetSuperPauseMoveTime(this.watcher, playerAddr);
            this.mugenSysInfo.attackmulset = PlayerUtils.GetAttackMulSet(this.watcher, playerAddr);
            for (int index = 0; index < 5; ++index)
                this.mugenSysvar[index] = PlayerUtils.GetSysvar(this.watcher, playerAddr, index);
            for (int index = 0; index < 60; ++index)
                this.mugenVar[index] = PlayerUtils.GetVar(this.watcher, playerAddr, index);
            for (int index = 0; index < 5; ++index)
                this.mugenSysfvar[index] = PlayerUtils.GetSysfvar(this.watcher, playerAddr, index);
            for (int index = 0; index < 40; ++index)
                this.mugenFvar[index] = PlayerUtils.GetFvar(this.watcher, playerAddr, index);
            if (VarForm.MainObj() == null)
                return;
            this.BeginInvoke((Action)(() => VarForm.MainObj().DisplayVars(playerId, this.mugenSysInfo, this.mugenSysvar, this.mugenVar, this.mugenSysfvar, this.mugenFvar)))?.AsyncWaitHandle.WaitOne(this._invokeWaitTime);
        }

        private void UpdatePlayerData(
          string displayName,
          int palno,
          int winGames,
          int loseGames,
          int drawGames,
          int errorGames,
          int canceledGames,
          int winRounds,
          int winKORounds,
          int loseRounds,
          int loseKORounds,
          int drawRounds,
          int errorRounds,
          int canceledRounds)
        {
            if (displayName == null)
                return;
            GameLogger.MainObj().UpdateCharData(displayName, palno, new GameLogger.CharData_t()
            {
                winGames = winGames,
                loseGames = loseGames,
                drawGames = drawGames,
                errorGames = errorGames,
                canceledGames = canceledGames,
                winRounds = winRounds,
                winKORounds = winKORounds,
                loseRounds = loseRounds,
                loseKORounds = loseKORounds,
                drawRounds = drawRounds,
                errorRounds = errorRounds,
                canceledRounds = canceledRounds
            });
            if (this.watcher.GetMugenProcess() == null)
                return;
            this.BeginInvoke((Action)(() => GameLogger.MainObj().DumpCharData(displayName, palno)))?.AsyncWaitHandle.WaitOne(this._invokeWaitTime);
        }

        private void UpdatePlayerDataEx(
          string displayNameA,
          int palnoA,
          string displayNameB,
          int palnoB,
          int winGames,
          int loseGames,
          int drawGames,
          int errorGames,
          int canceledGames,
          int winRounds,
          int winKORounds,
          int loseRounds,
          int loseKORounds,
          int drawRounds,
          int errorRounds,
          int canceledRounds)
        {
            string displayName = "";
            if (displayNameA != null)
                displayName = displayName + displayNameA + "(" + palnoA.ToString() + "p)";
            if (displayNameB != null)
            {
                if (displayName != null && displayName != "")
                    displayName += "＆";
                displayName = displayName + displayNameB + "(" + palnoB.ToString() + "p)";
            }
            if (displayName == null || !(displayName != ""))
                return;
            GameLogger.MainObj().UpdateCharDataEx(displayName, new GameLogger.CharData_t()
            {
                winGames = winGames,
                loseGames = loseGames,
                drawGames = drawGames,
                errorGames = errorGames,
                canceledGames = canceledGames,
                winRounds = winRounds,
                winKORounds = winKORounds,
                loseRounds = loseRounds,
                loseKORounds = loseKORounds,
                drawRounds = drawRounds,
                errorRounds = errorRounds,
                canceledRounds = canceledRounds
            });
            if (this.watcher.GetMugenProcess() == null)
                return;
            this.BeginInvoke((Action)(() => GameLogger.MainObj().DumpCharDataEx(displayName)))?.AsyncWaitHandle.WaitOne(this._invokeWaitTime);
        }

        /// <summary>
        /// gets the current value of the location pointed to by a TriggerValue
        /// <br/>involves reading a predetermined location in memory based on the PlayerType for the TriggerValue
        /// </summary>
        /// <param name="baseAddr"></param>
        /// <returns></returns>
        private TriggerDatabase.TriggerValue_t GetTriggerValue(uint baseAddr)
        {
            if (baseAddr == 0U)
                return (TriggerDatabase.TriggerValue_t)null;
            uint num = 0;
            // used in custom monitors
            uint rootAdder = 0;
            TriggerCheckTarget.Player_t targetPlayer = this._triggerCheckTarget.GetTargetPlayer();
            // determine right address to read memory from based on type
            switch (targetPlayer.playerType)
            {
                case TriggerCheckTarget.Player_t.PlayerType.PLAYER_NONE:
                    return (TriggerDatabase.TriggerValue_t)null;
                case TriggerCheckTarget.Player_t.PlayerType.PLAYER_P1:
                    num = GameUtils.GetP1Addr(this.watcher);
                    rootAdder = num;
                    break;
                case TriggerCheckTarget.Player_t.PlayerType.PLAYER_P2:
                    num = GameUtils.GetP2Addr(this.watcher);
                    rootAdder = num;
                    break;
                case TriggerCheckTarget.Player_t.PlayerType.PLAYER_P3:
                    num = GameUtils.GetP3Addr(this.watcher);
                    rootAdder = num;
                    break;
                case TriggerCheckTarget.Player_t.PlayerType.PLAYER_P4:
                    num = GameUtils.GetP4Addr(this.watcher);
                    rootAdder = num;
                    break;
                case TriggerCheckTarget.Player_t.PlayerType.PLAYER_PLAYERID:
                    num = this.GetPlayerAddrFromId(baseAddr, targetPlayer.pid);
                    rootAdder = this.GetPlayerAddrFromId(baseAddr, this.GetRootId(baseAddr, targetPlayer.pid));
                    break;
                case TriggerCheckTarget.Player_t.PlayerType.PLAYER_P1_HELPERID:
                    num = this.GetPlayerAddrFromHelperId(baseAddr, targetPlayer.pid, 0);
                    rootAdder = this.GetPlayerAddrFromId(baseAddr, this.GetRootId(baseAddr, PlayerUtils.GetPlayerId(this.watcher, num)));
                    break;
                case TriggerCheckTarget.Player_t.PlayerType.PLAYER_P2_HELPERID:
                    num = this.GetPlayerAddrFromHelperId(baseAddr, targetPlayer.pid, 1);
                    rootAdder = this.GetPlayerAddrFromId(baseAddr, this.GetRootId(baseAddr, PlayerUtils.GetPlayerId(this.watcher, num)));
                    break;
                case TriggerCheckTarget.Player_t.PlayerType.PLAYER_P3_HELPERID:
                    num = this.GetPlayerAddrFromHelperId(baseAddr, targetPlayer.pid, 2);
                    rootAdder = this.GetPlayerAddrFromId(baseAddr, this.GetRootId(baseAddr, PlayerUtils.GetPlayerId(this.watcher, num)));
                    break;
                case TriggerCheckTarget.Player_t.PlayerType.PLAYER_P4_HELPERID:
                    num = this.GetPlayerAddrFromHelperId(baseAddr, targetPlayer.pid, 3);
                    rootAdder = this.GetPlayerAddrFromId(baseAddr, this.GetRootId(baseAddr, PlayerUtils.GetPlayerId(this.watcher, num)));
                    break;
            }
            if (num == 0U)
                return (TriggerDatabase.TriggerValue_t)null;
            // check the target trigger
            TriggerCheckTarget.Trigger_t targetTrigger = this._triggerCheckTarget.GetTargetTrigger();
            TriggerId triggerType = targetTrigger.triggerType;
            if (!TriggerDatabase.IsTriggerAvailable(triggerType))
                return (TriggerDatabase.TriggerValue_t)null;
            uint addr;
            // check to see if we offset the addr from player addr or base addr
            bool isOffsetFromBase = false;
            // get the exact offset
            uint offs = TriggerDatabase.GetTriggerAddrForType(this.watcher.MugenDatabase, triggerType, ref isOffsetFromBase);
            // find exact addr based on offsets
            if (isOffsetFromBase) addr = baseAddr + offs;
            else addr = num + offs;
            // used to ensure vars read the right offset based on targetTrigger.index
            switch (targetTrigger.triggerType)
            {
                case TriggerDatabase.TriggerId.TRIGGER_SYSVAR:
                case TriggerDatabase.TriggerId.TRIGGER_SYSFVAR:
                case TriggerDatabase.TriggerId.TRIGGER_VAR:
                case TriggerDatabase.TriggerId.TRIGGER_FVAR:
                    addr += (uint)(targetTrigger.index * 4);
                    break;
                default:
                    break;
            }
            // if this is a GameTime based trigger, it's a custom monitor.
            if (offs == this.watcher.MugenDatabase.GAMETIME_BASE_OFFSET)
            {
                // for NumExplod/NumTarget, pass the player addr.
                // otherwise pass the root addr.
                if (targetTrigger.triggerType == TriggerId.TRIGGER_NUMEXPLOD_ID
                    || targetTrigger.triggerType == TriggerId.TRIGGER_NUMTARGET
                    || targetTrigger.triggerType == TriggerId.TRIGGER_HASTARGET)
                    return GetTriggerValueEx(baseAddr, num);
                return GetTriggerValueEx(baseAddr, rootAdder);
            }
            // build an updated triggervalue
            TriggerDatabase.TriggerValue_t triggerValueT = new TriggerDatabase.TriggerValue_t();
            switch (TriggerDatabase.GetTriggerValueType(triggerType))
            {
                case TriggerDatabase.ValueType.VALUE_NONE:
                    triggerValueT.valueType = TriggerDatabase.ValueType.VALUE_NONE;
                    break;
                case TriggerDatabase.ValueType.VALUE_ANY:
                    triggerValueT.valueType = TriggerDatabase.ValueType.VALUE_ANY;
                    break;
                case TriggerDatabase.ValueType.VALUE_INT:
                    triggerValueT.valueType = TriggerDatabase.ValueType.VALUE_INT;
                    triggerValueT.SetInt32Value(this.watcher.GetInt32Data(addr, 0U));
                    break;
                case TriggerDatabase.ValueType.VALUE_FLOAT:
                    triggerValueT.valueType = TriggerDatabase.ValueType.VALUE_FLOAT;
                    triggerValueT.SetSingleValue(this.watcher.GetFloatData(addr, 0U));
                    break;
            }
            return triggerValueT;
        }

        /// <summary>
        /// returns the value for a trigger given that the trigger value is a custom monitor
        /// <br/>a custom monitor requires custom logic to determine the value (e.g. counting the number of Helpers owned by that player)
        /// <br/>all custom monitors use <c>GAMETIME_BASE_OFFSET</c> as their offset so it breaks every frame.
        /// </summary>
        /// <param name="baseAddr">mugen base address</param>
        /// <param name="targetAddr">address for the target of this trigger</param>
        /// <returns></returns>
        private TriggerValue_t GetTriggerValueEx(uint baseAddr, uint targetAddr)
        {
            // we don't have error checking here bc this only gets called from GetTriggerValue
            // if this becomes more extensible, add error checking
            TriggerCheckTarget.Trigger_t targetTrigger = this._triggerCheckTarget.GetTargetTrigger();
            // build the value
            TriggerDatabase.TriggerValue_t triggerValueT = new TriggerDatabase.TriggerValue_t();
            // do logic based on type
            int playerID = PlayerUtils.GetPlayerId(this.watcher, targetAddr);
            switch (targetTrigger.triggerType)
            {
                case TriggerId.TRIGGER_NUMHELPER:
                    // count the number of helpers owned by the selected root
                    int numHelper = this.GetNumHelper(baseAddr, playerID);
                    triggerValueT.valueType = TriggerDatabase.ValueType.VALUE_INT;
                    triggerValueT.SetInt32Value(numHelper);
                    break;
                case TriggerId.TRIGGER_NUMHELPER_ID:
                    // count the number of helpers owned by the selected root, with a specified ID
                    int numHelperID = this.GetNumHelperID(baseAddr, playerID, targetTrigger.index);
                    triggerValueT.valueType = TriggerDatabase.ValueType.VALUE_INT;
                    triggerValueT.SetInt32Value(numHelperID);
                    break;
                case TriggerId.TRIGGER_NUMPROJ_ID:
                    // count the number of helpers owned by the selected root, with a specified ID
                    int numProjID = this.GetNumProjID(baseAddr, playerID, targetTrigger.index);
                    triggerValueT.valueType = TriggerDatabase.ValueType.VALUE_INT;
                    triggerValueT.SetInt32Value(numProjID);
                    break;
                case TriggerId.TRIGGER_NUMEXPLOD_ID:
                    // count the number of helpers owned by the selected root, with a specified ID
                    int numExplodID = this.GetNumExplodID(baseAddr, playerID, targetTrigger.index);
                    triggerValueT.valueType = TriggerDatabase.ValueType.VALUE_INT;
                    triggerValueT.SetInt32Value(numExplodID);
                    break;
                case TriggerId.TRIGGER_NUMTARGET:
                    // count the number of targets a player owns
                    int numTarget = this.GetNumTarget(baseAddr, playerID);
                    triggerValueT.valueType = TriggerDatabase.ValueType.VALUE_INT;
                    triggerValueT.SetInt32Value(numTarget);
                    break;
                case TriggerId.TRIGGER_HASTARGET:
                    // count the number of targets a player owns
                    bool hasTarget = this.GetHasTarget(baseAddr, playerID, targetTrigger.index);
                    triggerValueT.valueType = TriggerDatabase.ValueType.VALUE_BOOL;
                    triggerValueT.SetBoolValue(hasTarget);
                    break;
                default:
                    return (TriggerDatabase.TriggerValue_t)null;
            }
            return triggerValueT;
        }

        private bool GetHasTarget(uint baseAddr, int playerID, int targetID)
        {
            // iterate all possible targets
            for (int i = 0; i < 8; i++)
            {
                // get target
                int currTarget = PlayerUtils.GetTarget(this.watcher, this.GetPlayerAddrFromId(baseAddr, playerID), i);
                if (currTarget == targetID) return true;
            }
            return false;
        }

        /// <summary>
        /// gets number of targets owned by a specific player.
        /// </summary>
        /// <param name="baseAddr"></param>
        /// <param name="playerID"></param>
        /// <returns></returns>
        private int GetNumTarget(uint baseAddr, int playerID)
        {
            int num = 0;
            // iterate all possible targets
            for (int i = 0; i < 8; i++)
            {
                // get target
                int targetID = PlayerUtils.GetTarget(this.watcher, this.GetPlayerAddrFromId(baseAddr, playerID), i);
                // note: does not account for persistent targets becoming destroyed (i.e. CT)
                // in which case targetID is non-zero and indeed the player holds a target,
                // but the target does not really exist.
                if (targetID != 0) num++;
            }
            return num;
        }

        /// <summary>
        /// gets number of explods with a specific ID owned by a specific player.
        /// </summary>
        /// <param name="baseAddr"></param>
        /// <param name="playerID"></param>
        /// <param name="explodID"></param>
        /// <returns></returns>
        private int GetNumExplodID(uint baseAddr, int playerID, int explodID)
        {
            // get explod list address
            uint explodList = (uint)GameUtils.GetExplodListAddress(this.watcher, baseAddr);
            int num = 0;
            // iterate all proj
            // TODO: 256 chosen arbitrarily and probably not good enough for cheap. find a way to find the max number.
            for (uint i = 0; i < 256; i++)
            {
                // get proj
                uint explodAdder = GameUtils.GetExplodAddress(this.watcher, explodList, i);
                int explod = GameUtils.GetExplodId(this.watcher, explodAdder);
                // sum if ID matches
                if (explod == explodID && GameUtils.GetExplodOwnerId(this.watcher, explodAdder) == playerID) num++;
            }
            return num;
        }

        /// <summary>
        /// gets number of proj with a specific ID owned by a specific root.
        /// </summary>
        /// <param name="baseAddr"></param>
        /// <param name="playerID"></param>
        /// <param name="projID"></param>
        /// <returns></returns>
        private int GetNumProjID(uint baseAddr, int playerID, int projID)
        {
            // get proj base and list addresses
            uint projBase = (uint)PlayerUtils.GetProjBaseAddress(this.watcher, this.GetPlayerAddrFromId(baseAddr, playerID));
            uint projList = (uint)PlayerUtils.GetProjListAddress(this.watcher, projBase);
            // get max proj count
            int maxProj = this.watcher.GetInt32Data(projBase, this.watcher.MugenDatabase.PROJ_MAX_PROJ_BASE_OFFSET);
            int num = 0;
            // iterate all proj
            for (uint i = 0; i < maxProj; i++)
            {
                // get proj
                uint projAdder = PlayerUtils.GetProjAddress(this.watcher, projList, i);
                int proj = PlayerUtils.GetProjId(this.watcher, projAdder);
                // sum if ID matches
                if (proj == projID) num++;
            }
            return num;
        }

        /// <summary>
        /// gets the number of helpers owned by a specific root.
        /// </summary>
        /// <param name="baseAddr"></param>
        /// <param name="playerID"></param>
        /// <returns></returns>
        private int GetNumHelper(uint baseAddr, int playerID)
        {
            // iterate 56 helpers
            int num = 0;
            for (int index = 4; index < 60; ++index)
            {
                uint playerAddr = (uint)GameUtils.GetPlayerAddress(this.watcher, baseAddr, index + 1);
                if (playerAddr != 0U && PlayerUtils.DoesPlayerExist(this.watcher, playerAddr) && playerID == this.GetRootId(baseAddr, PlayerUtils.GetPlayerId(this.watcher, playerAddr)))
                    num++;
            }
            return num;
        }

        /// <summary>
        /// helper function to get the number of Helpers with a given ID owned by a given root
        /// </summary>
        /// <param name="baseAddr"></param>
        /// <param name="playerID"></param>
        /// <param name="helperID"></param>
        /// <returns></returns>
        private int GetNumHelperID(uint baseAddr, int playerID, int helperID)
        {
            // iterate 56 helpers
            int num = 0;
            for (int index = 4; index < 60; ++index)
            {
                uint playerAddr = (uint)GameUtils.GetPlayerAddress(this.watcher, baseAddr, index + 1);
                if (playerAddr != 0U && PlayerUtils.DoesPlayerExist(this.watcher, playerAddr) && playerID == this.GetRootId(baseAddr, PlayerUtils.GetPlayerId(this.watcher, playerAddr)) && helperID == PlayerUtils.GetHelperId(this.watcher, playerAddr))
                    num++;
            }
            return num;
        }

        /// <summary>
        /// sets a breakpoint based on the trigger type and player type in use by the curremt TriggerCheckTarget
        /// </summary>
        /// <param name="baseAddr"></param>
        /// <returns></returns>
        private bool _SetBreakPoint(uint baseAddr)
        {
            if (baseAddr == 0U)
                return false;
            uint num = 0;
            TriggerCheckTarget.Player_t targetPlayer = this._triggerCheckTarget.GetTargetPlayer();
            // find base address to monitor based on PlayerType
            switch (targetPlayer.playerType)
            {
                case TriggerCheckTarget.Player_t.PlayerType.PLAYER_NONE:
                    return false;
                case TriggerCheckTarget.Player_t.PlayerType.PLAYER_P1:
                    num = GameUtils.GetP1Addr(this.watcher);
                    break;
                case TriggerCheckTarget.Player_t.PlayerType.PLAYER_P2:
                    num = GameUtils.GetP2Addr(this.watcher);
                    break;
                case TriggerCheckTarget.Player_t.PlayerType.PLAYER_P3:
                    num = GameUtils.GetP3Addr(this.watcher);
                    break;
                case TriggerCheckTarget.Player_t.PlayerType.PLAYER_P4:
                    num = GameUtils.GetP4Addr(this.watcher);
                    break;
                case TriggerCheckTarget.Player_t.PlayerType.PLAYER_PLAYERID:
                    num = this.GetPlayerAddrFromId(baseAddr, targetPlayer.pid);
                    break;
                case TriggerCheckTarget.Player_t.PlayerType.PLAYER_P1_HELPERID:
                    num = this.GetPlayerAddrFromHelperId(baseAddr, targetPlayer.pid, 0);
                    break;
                case TriggerCheckTarget.Player_t.PlayerType.PLAYER_P2_HELPERID:
                    num = this.GetPlayerAddrFromHelperId(baseAddr, targetPlayer.pid, 1);
                    break;
                case TriggerCheckTarget.Player_t.PlayerType.PLAYER_P3_HELPERID:
                    num = this.GetPlayerAddrFromHelperId(baseAddr, targetPlayer.pid, 2);
                    break;
                case TriggerCheckTarget.Player_t.PlayerType.PLAYER_P4_HELPERID:
                    num = this.GetPlayerAddrFromHelperId(baseAddr, targetPlayer.pid, 3);
                    break;
            }
            if (num == 0U)
                return false;
            TriggerCheckTarget.Trigger_t targetTrigger = this._triggerCheckTarget.GetTargetTrigger();
            TriggerId triggerType = targetTrigger.triggerType;
            if (!TriggerDatabase.IsTriggerAvailable(triggerType))
                return false;
            // target address for the trigger check
            uint targetAdder;
            // check to see if we offset the addr from player addr or base addr
            bool isOffsetFromBase = false;
            // get the exact offset
            uint offs = TriggerDatabase.GetTriggerAddrForType(this.watcher.MugenDatabase, triggerType, ref isOffsetFromBase);
            // find exact addr based on offsets
            if (isOffsetFromBase) targetAdder = baseAddr + offs;
            else targetAdder = num + offs;
            // offset if var type
            switch (targetTrigger.triggerType)
            {
                case TriggerDatabase.TriggerId.TRIGGER_SYSVAR:
                case TriggerDatabase.TriggerId.TRIGGER_SYSFVAR:
                case TriggerDatabase.TriggerId.TRIGGER_VAR:
                case TriggerDatabase.TriggerId.TRIGGER_FVAR:
                    targetAdder += (uint)(targetTrigger.index * 4);
                    break;
                default:
                    break;
            }
            // set either hardware breakpoint or experimental/software breakpoint
            if (ProfileManager.MainObj().GetProfile(this._workingProfileId).IsExperimentalBreakpoints())
                this.__SetExperimentalBreakPoint(targetAdder);
            else
                return this.watcher.SetDataBreakpoint(targetAdder);
            return true;
        }

        /// <summary>
        /// sets a software/experimental breakpoint at the provided address
        /// <br/>saves the address to monitor and an initial value.
        /// </summary>
        /// <param name="targetAdder"></param>
        private void __SetExperimentalBreakPoint(uint targetAdder)
        {
            if (targetAdder == 0U)
                return;
            byte[] buf = new byte[4];
            this.watchAddr = targetAdder;
            this.WatchInitVal = (uint)this.watcher.GetInt32Data(this.watchAddr, 0);
        }

        /// <summary>
        /// added by Ziddia to consolidate async+threadsafe log writes
        /// </summary>
        /// <param name="v"></param>
        private void AsyncAppendLog(string v)
        {
            this.BeginInvoke(new Action(() => LogManager.MainObj().appendLog(v)));
        }

        private void MugenWindow_KeyPress(object sender, KeyPressEventArgs e)
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
            this.components = (IContainer)new Container();
            ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(MugenWindow));
            this.backgroundBox = new PictureBox();
            this.toolTip1 = new ToolTip(this.components);
            this.activateTimer = new System.Windows.Forms.Timer(this.components);
            ((ISupportInitialize)this.backgroundBox).BeginInit();
            this.SuspendLayout();
            this.backgroundBox.Image = (Image)Resources.background;
            this.backgroundBox.Location = new Point(0, 0);
            this.backgroundBox.Margin = new Padding(0);
            this.backgroundBox.Name = "backgroundBox";
            this.backgroundBox.Size = new Size(640, 480);
            this.backgroundBox.SizeMode = PictureBoxSizeMode.StretchImage;
            this.backgroundBox.TabIndex = 0;
            this.backgroundBox.TabStop = false;
            this.backgroundBox.Paint += new PaintEventHandler(this.backgroundBox_Paint);
            this.backgroundBox.MouseClick += new MouseEventHandler(this.backgroundBox_MouseClick);
            this.activateTimer.Enabled = true;
            this.activateTimer.Interval = 1000;
            this.activateTimer.Tick += new EventHandler(this.activateTimer_Tick);
            this.AutoScaleMode = AutoScaleMode.None;
            this.ClientSize = new Size(256, 234);
            this.ControlBox = false;
            this.Controls.Add((Control)this.backgroundBox);
            this.FormBorderStyle = FormBorderStyle.FixedToolWindow;
            this.Icon = (Icon)componentResourceManager.GetObject("$this.Icon");
            this.Location = new Point(200, 0);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = nameof(MugenWindow);
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = FormStartPosition.Manual;
            this.Text = "M.U.G.E.N";
            this.Activated += new EventHandler(this.MugenWindow_Activated);
            this.Deactivate += new EventHandler(this.MugenWindow_Deactivate);
            this.FormClosing += new FormClosingEventHandler(this.MugenWindow_FormClosing);
            this.FormClosed += new FormClosedEventHandler(this.MugenWindow_FormClosed);
            this.KeyPress += new KeyPressEventHandler(this.MugenWindow_KeyPress);
            ((ISupportInitialize)this.backgroundBox).EndInit();
            this.ResumeLayout(false);
        }

        /// <summary>
        /// main loop for the monitor function. this function is responsible for handling most data reads, updates, breakpoints, etc.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// 

        internal struct DebugPassInfo
        {
            internal uint baseAddr;
            internal uint playerAddr1;
            internal uint playerAddr2;
            internal uint playerAddr3;
            internal uint playerAddr4;
            internal bool flag1;
            internal bool flag2;
            internal bool flag3;
            internal bool flag4;
            internal bool flag5;
            internal bool flag6;
            internal bool flag7;
            internal bool flag8;
            internal int team1Wins;
            internal int team2Wins;
            internal int trueTeam1WinCount;
            internal int trueTeam2WinCount;
            internal int num3;
            internal int num4;
            internal int drawRounds;
            internal string displayName1;
            internal string displayName2;
            internal string displayName3;
            internal string displayName4;
            internal string displayName5;
            internal int palnoA1;
            internal int palnoA2;
            internal int palnoB1;
            internal int palnoB2;
            internal int num5;
            internal int num6;
            internal uint gameTime;
            internal int num8;
            internal long num9;
            internal long num10;
            internal long num11;
            internal long num12;
            internal long num13;
            internal long num14;
            internal long num15;
            internal long num16;
            internal int num17;
            internal int num18;
            internal int num19;
            internal NativeEvent backupNativeEvent;
            internal TriggerDatabase.TriggerValue_t triggerValueT;
        }

        private DebugPassInfo debugPassInfo;

        public void Initialize()
        {
            this.debugPassInfo = new DebugPassInfo
            {
                baseAddr = 0,
                playerAddr1 = 0,
                playerAddr2 = 0,
                playerAddr3 = 0,
                playerAddr4 = 0,
                flag1 = false,
                flag2 = false,
                flag3 = false,
                flag4 = false,
                flag5 = false,
                flag6 = false,
                flag7 = false,
                flag8 = false,
                team1Wins = 0,
                team2Wins = 0,
                trueTeam1WinCount = 0,
                trueTeam2WinCount = 0,
                num3 = 0,
                num4 = 0,
                drawRounds = 0,
                displayName1 = "",
                displayName2 = (string) null,
                displayName3 = (string) null,
                displayName4 = (string) null,
                displayName5 = (string) null,
                palnoA1 = 0,
                palnoA2 = 0,
                palnoB1 = 0,
                palnoB2 = 0,
                num5 = 300,
                num6 = 0,
                gameTime = 0,
                num8 = 0,
                num9 = 0,
                num10 = 0,
                num11 = 0,
                num12 = 0,
                num13 = 0,
                num14 = 0,
                num15 = 0,
                num16 = 0,
                num17 = 0,
                num18 = 0,
                num19 = 2,
                backupNativeEvent = (NativeEvent) null
            };
        }

        // called during event loop when the debug process is set up (polling debug events)
        public void HandleNativeEvent(NativeEvent awaitedNativeEvent)
        {
            LogManager logManager = LogManager.MainObj();
            DebugForm debugForm = DebugForm.MainObj();
            // backup native event
            this.debugPassInfo.backupNativeEvent = (NativeEvent)null;
            // this is false by default+we set to true if mugen is really active
            this._isMugenActive = false;
            switch (awaitedNativeEvent.m_union.Exception.ExceptionRecord.ExceptionCode)
            {
                // these cases correspond to hardware bp?
                case ExceptionCode.STATUS_WOW64_SINGLESTEP:
                case ExceptionCode.STATUS_SINGLESTEP:
                    // check for proj hit breakpoints
                    if (this.watcher.GetDebugThreadContext().Eip == this.projHitBPAddrs[(int) this.watcher.MugenVersion - 1])
                    {
                        // grab player address from CONTEXT object
                        CONTEXT ctx = this.watcher.GetDebugThreadContext();
                        uint playerAddr = ctx.Ebp;

                        // add proj to the list
                        DebugForm.MainObj().ProjectileHit(GameUtils.GetGameTime(this.watcher), PlayerUtils.GetProjHitID(this.watcher, playerAddr), PlayerUtils.GetPlayerId(this.watcher, playerAddr));

                        // skip to next instruction
                        ctx.Eip += 6;
                        this.watcher.SetThreadContext(ctx);
                        this.watcher.ContinueEvent(awaitedNativeEvent, false);
                        break;
                    }
                    // check if read value type matches expected type + is in range/matching
                    bool triggerValueValid = false;
                    // read current value
                    TriggerDatabase.TriggerValue_t triggerValue = this.GetTriggerValue(this.debugPassInfo.baseAddr);
                    // make sure not empty
                    if (triggerValue != null && (this.debugPassInfo.triggerValueT == null || !this.debugPassInfo.triggerValueT.isEqual(triggerValue)))
                    {
                        this.debugPassInfo.triggerValueT = triggerValue;
                        // used for verification, switching first on value type and then on operator.
                        // complicated but its mostly the same for each.
                        switch (triggerValue.valueType)
                        {
                            case TriggerDatabase.ValueType.VALUE_BOOL:
                                TriggerDatabase.TriggerValue_t targetValueFrom1 = this._triggerCheckTarget.GetTargetValueFrom();
                                if (targetValueFrom1.valueType == TriggerDatabase.ValueType.VALUE_BOOL)
                                {
                                    switch (this._triggerCheckTarget.GetTargetValueOpType())
                                    {
                                        case TriggerCheckTarget.ValueOpType.VALUE_OP_EQ:
                                            if (triggerValue.GetBoolValue() == targetValueFrom1.GetBoolValue())
                                            {
                                                triggerValueValid = true;
                                                break;
                                            }
                                            break;
                                        case TriggerCheckTarget.ValueOpType.VALUE_OP_NOT_EQ:
                                            if (triggerValue.GetBoolValue() != targetValueFrom1.GetBoolValue())
                                            {
                                                triggerValueValid = true;
                                                break;
                                            }
                                            break;
                                    }
                                    if (triggerValueValid)
                                    {
                                        this._isDebugBreakMode = true;
                                        this.debugPassInfo.backupNativeEvent = awaitedNativeEvent;
                                        break;
                                    }
                                    break;
                                }
                                if (targetValueFrom1.valueType == TriggerDatabase.ValueType.VALUE_ANY)
                                {
                                    triggerValueValid = true;
                                    this._isDebugBreakMode = true;
                                    this.debugPassInfo.backupNativeEvent = awaitedNativeEvent;
                                    break;
                                }
                                break;
                            case TriggerDatabase.ValueType.VALUE_INT:
                                targetValueFrom1 = this._triggerCheckTarget.GetTargetValueFrom();
                                TriggerDatabase.TriggerValue_t targetValueTo1 = this._triggerCheckTarget.GetTargetValueTo();
                                triggerValue.mask = targetValueFrom1.mask;
                                if (targetValueFrom1.valueType == TriggerDatabase.ValueType.VALUE_INT)
                                {
                                    switch (this._triggerCheckTarget.GetTargetValueOpType())
                                    {
                                        case TriggerCheckTarget.ValueOpType.VALUE_OP_EQ:
                                            if (triggerValue.GetMaskedInt32Value() == targetValueFrom1.GetInt32Value())
                                            {
                                                triggerValueValid = true;
                                                break;
                                            }
                                            break;
                                        case TriggerCheckTarget.ValueOpType.VALUE_OP_NOT_EQ:
                                            if (triggerValue.GetMaskedInt32Value() != targetValueFrom1.GetInt32Value())
                                            {
                                                triggerValueValid = true;
                                                break;
                                            }
                                            break;
                                        case TriggerCheckTarget.ValueOpType.VALUE_OP_LT:
                                            if (triggerValue.GetMaskedInt32Value() < targetValueFrom1.GetInt32Value())
                                            {
                                                triggerValueValid = true;
                                                break;
                                            }
                                            break;
                                        case TriggerCheckTarget.ValueOpType.VALUE_OP_LE:
                                            if (triggerValue.GetMaskedInt32Value() <= targetValueFrom1.GetInt32Value())
                                            {
                                                triggerValueValid = true;
                                                break;
                                            }
                                            break;
                                        case TriggerCheckTarget.ValueOpType.VALUE_OP_GT:
                                            if (triggerValue.GetMaskedInt32Value() > targetValueFrom1.GetInt32Value())
                                            {
                                                triggerValueValid = true;
                                                break;
                                            }
                                            break;
                                        case TriggerCheckTarget.ValueOpType.VALUE_OP_GE:
                                            if (triggerValue.GetMaskedInt32Value() >= targetValueFrom1.GetInt32Value())
                                            {
                                                triggerValueValid = true;
                                                break;
                                            }
                                            break;
                                        case TriggerCheckTarget.ValueOpType.VALUE_OP_FROM_TO:
                                            if (triggerValue.GetMaskedInt32Value() >= targetValueFrom1.GetInt32Value() && triggerValue.GetMaskedInt32Value() <= targetValueTo1.GetInt32Value())
                                            {
                                                triggerValueValid = true;
                                                break;
                                            }
                                            break;
                                        case TriggerCheckTarget.ValueOpType.VALUE_OP_L_FROM_TO:
                                            if (triggerValue.GetMaskedInt32Value() > targetValueFrom1.GetInt32Value() && triggerValue.GetMaskedInt32Value() <= targetValueTo1.GetInt32Value())
                                            {
                                                triggerValueValid = true;
                                                break;
                                            }
                                            break;
                                        case TriggerCheckTarget.ValueOpType.VALUE_OP_G_FROM_TO:
                                            if (triggerValue.GetMaskedInt32Value() >= targetValueFrom1.GetInt32Value() && triggerValue.GetMaskedInt32Value() < targetValueTo1.GetInt32Value())
                                            {
                                                triggerValueValid = true;
                                                break;
                                            }
                                            break;
                                        case TriggerCheckTarget.ValueOpType.VALUE_OP_LG_FROM_TO:
                                            if (triggerValue.GetMaskedInt32Value() > targetValueFrom1.GetInt32Value() && triggerValue.GetMaskedInt32Value() < targetValueTo1.GetInt32Value())
                                            {
                                                triggerValueValid = true;
                                                break;
                                            }
                                            break;
                                        case TriggerCheckTarget.ValueOpType.VALUE_OP_NOT_FROM_TO:
                                            if (triggerValue.GetMaskedInt32Value() < targetValueFrom1.GetInt32Value() || triggerValue.GetMaskedInt32Value() > targetValueTo1.GetInt32Value())
                                            {
                                                triggerValueValid = true;
                                                break;
                                            }
                                            break;
                                        case TriggerCheckTarget.ValueOpType.VALUE_OP_NOT_L_FROM_TO:
                                            if (triggerValue.GetMaskedInt32Value() <= targetValueFrom1.GetInt32Value() || triggerValue.GetMaskedInt32Value() > targetValueTo1.GetInt32Value())
                                            {
                                                triggerValueValid = true;
                                                break;
                                            }
                                            break;
                                        case TriggerCheckTarget.ValueOpType.VALUE_OP_NOT_G_FROM_TO:
                                            if (triggerValue.GetMaskedInt32Value() < targetValueFrom1.GetInt32Value() || triggerValue.GetMaskedInt32Value() >= targetValueTo1.GetInt32Value())
                                            {
                                                triggerValueValid = true;
                                                break;
                                            }
                                            break;
                                        case TriggerCheckTarget.ValueOpType.VALUE_OP_NOT_LG_FROM_TO:
                                            if (triggerValue.GetMaskedInt32Value() <= targetValueFrom1.GetInt32Value() || triggerValue.GetMaskedInt32Value() >= targetValueTo1.GetInt32Value())
                                            {
                                                triggerValueValid = true;
                                                break;
                                            }
                                            break;
                                    }
                                    if (triggerValueValid)
                                    {
                                        this._isDebugBreakMode = true;
                                        this.debugPassInfo.backupNativeEvent = awaitedNativeEvent;
                                        break;
                                    }
                                    break;
                                }
                                if (targetValueFrom1.valueType == TriggerDatabase.ValueType.VALUE_ANY)
                                {
                                    triggerValueValid = true;
                                    this._isDebugBreakMode = true;
                                    this.debugPassInfo.backupNativeEvent = awaitedNativeEvent;
                                    break;
                                }
                                break;
                            case TriggerDatabase.ValueType.VALUE_FLOAT:
                                TriggerDatabase.TriggerValue_t targetValueFrom2 = this._triggerCheckTarget.GetTargetValueFrom();
                                TriggerDatabase.TriggerValue_t targetValueTo2 = this._triggerCheckTarget.GetTargetValueTo();
                                if (targetValueFrom2.valueType == TriggerDatabase.ValueType.VALUE_FLOAT)
                                {
                                    switch (this._triggerCheckTarget.GetTargetValueOpType())
                                    {
                                        case TriggerCheckTarget.ValueOpType.VALUE_OP_EQ:
                                            if ((double)triggerValue.GetSingleValue() == (double)targetValueFrom2.GetSingleValue())
                                            {
                                                triggerValueValid = true;
                                                break;
                                            }
                                            break;
                                        case TriggerCheckTarget.ValueOpType.VALUE_OP_NOT_EQ:
                                            if ((double)triggerValue.GetSingleValue() != (double)targetValueFrom2.GetSingleValue())
                                            {
                                                triggerValueValid = true;
                                                break;
                                            }
                                            break;
                                        case TriggerCheckTarget.ValueOpType.VALUE_OP_LT:
                                            if ((double)triggerValue.GetSingleValue() < (double)targetValueFrom2.GetSingleValue())
                                            {
                                                triggerValueValid = true;
                                                break;
                                            }
                                            break;
                                        case TriggerCheckTarget.ValueOpType.VALUE_OP_LE:
                                            if ((double)triggerValue.GetSingleValue() <= (double)targetValueFrom2.GetSingleValue())
                                            {
                                                triggerValueValid = true;
                                                break;
                                            }
                                            break;
                                        case TriggerCheckTarget.ValueOpType.VALUE_OP_GT:
                                            if ((double)triggerValue.GetSingleValue() > (double)targetValueFrom2.GetSingleValue())
                                            {
                                                triggerValueValid = true;
                                                break;
                                            }
                                            break;
                                        case TriggerCheckTarget.ValueOpType.VALUE_OP_GE:
                                            if ((double)triggerValue.GetSingleValue() >= (double)targetValueFrom2.GetSingleValue())
                                            {
                                                triggerValueValid = true;
                                                break;
                                            }
                                            break;
                                        case TriggerCheckTarget.ValueOpType.VALUE_OP_FROM_TO:
                                            if ((double)triggerValue.GetSingleValue() >= (double)targetValueFrom2.GetSingleValue() && (double)triggerValue.GetSingleValue() <= (double)targetValueTo2.GetSingleValue())
                                            {
                                                triggerValueValid = true;
                                                break;
                                            }
                                            break;
                                        case TriggerCheckTarget.ValueOpType.VALUE_OP_L_FROM_TO:
                                            if ((double)triggerValue.GetSingleValue() > (double)targetValueFrom2.GetSingleValue() && (double)triggerValue.GetSingleValue() <= (double)targetValueTo2.GetSingleValue())
                                            {
                                                triggerValueValid = true;
                                                break;
                                            }
                                            break;
                                        case TriggerCheckTarget.ValueOpType.VALUE_OP_G_FROM_TO:
                                            if ((double)triggerValue.GetSingleValue() >= (double)targetValueFrom2.GetSingleValue() && (double)triggerValue.GetSingleValue() < (double)targetValueTo2.GetSingleValue())
                                            {
                                                triggerValueValid = true;
                                                break;
                                            }
                                            break;
                                        case TriggerCheckTarget.ValueOpType.VALUE_OP_LG_FROM_TO:
                                            if ((double)triggerValue.GetSingleValue() > (double)targetValueFrom2.GetSingleValue() && (double)triggerValue.GetSingleValue() < (double)targetValueTo2.GetSingleValue())
                                            {
                                                triggerValueValid = true;
                                                break;
                                            }
                                            break;
                                        case TriggerCheckTarget.ValueOpType.VALUE_OP_NOT_FROM_TO:
                                            if ((double)triggerValue.GetSingleValue() < (double)targetValueFrom2.GetSingleValue() || (double)triggerValue.GetSingleValue() > (double)targetValueTo2.GetSingleValue())
                                            {
                                                triggerValueValid = true;
                                                break;
                                            }
                                            break;
                                        case TriggerCheckTarget.ValueOpType.VALUE_OP_NOT_L_FROM_TO:
                                            if ((double)triggerValue.GetSingleValue() <= (double)targetValueFrom2.GetSingleValue() || (double)triggerValue.GetSingleValue() > (double)targetValueTo2.GetSingleValue())
                                            {
                                                triggerValueValid = true;
                                                break;
                                            }
                                            break;
                                        case TriggerCheckTarget.ValueOpType.VALUE_OP_NOT_G_FROM_TO:
                                            if ((double)triggerValue.GetSingleValue() < (double)targetValueFrom2.GetSingleValue() || (double)triggerValue.GetSingleValue() >= (double)targetValueTo2.GetSingleValue())
                                            {
                                                triggerValueValid = true;
                                                break;
                                            }
                                            break;
                                        case TriggerCheckTarget.ValueOpType.VALUE_OP_NOT_LG_FROM_TO:
                                            if ((double)triggerValue.GetSingleValue() <= (double)targetValueFrom2.GetSingleValue() || (double)triggerValue.GetSingleValue() >= (double)targetValueTo2.GetSingleValue())
                                            {
                                                triggerValueValid = true;
                                                break;
                                            }
                                            break;
                                    }
                                    if (triggerValueValid)
                                    {
                                        this._isDebugBreakMode = true;
                                        this.debugPassInfo.backupNativeEvent = awaitedNativeEvent;
                                        break;
                                    }
                                    break;
                                }
                                if (targetValueFrom2.valueType == TriggerDatabase.ValueType.VALUE_ANY)
                                {
                                    triggerValueValid = true;
                                    this._isDebugBreakMode = true;
                                    this.debugPassInfo.backupNativeEvent = awaitedNativeEvent;
                                    break;
                                }
                                break;
                        }
                    }
                    uint p1Addr = GameUtils.GetP1Addr(this.watcher);
                    // for un-stopping mugen from trigger breakpoints
                    if (!triggerValueValid || !this.debugPassInfo.flag6 || (p1Addr == 0U || this._triggerCheckTarget.GetNextCommand() == TriggerCheckTarget.CheckCommand.CHECKCMD_STOP))
                    {
                        this.watcher.ContinueEvent(awaitedNativeEvent, false);
                        this.BeginInvoke((Action)(() => DebugForm.MainObj().DisableTriggerCheckResumeButton()));
                        if (p1Addr == 0U)
                            this.BeginInvoke((Action)(() => DebugForm.MainObj().FinalizeTriggerCheck(this.watcher.MugenVersion)));
                        this._isDebugBreakMode = false;
                        break;
                    }
                    IAsyncResult asyncResult = this.BeginInvoke((Action)(() => DebugForm.MainObj().EnableTriggerCheckResumeButton()));
                    if (asyncResult != null)
                    {
                        asyncResult.AsyncWaitHandle.WaitOne(this._invokeWaitTime);
                        break;
                    }
                    break;
                // skip over this? since we wait for step event
                case ExceptionCode.STATUS_WOW64_BREAKPOINT:
                case ExceptionCode.STATUS_BREAKPOINT:
                    this.watcher.ContinueEvent(awaitedNativeEvent, false);
                    break;
                default:
                    this.watcher.ContinueEvent(awaitedNativeEvent, false);
                    if (this.watcher.GetDebugProcess() != null)
                    {
                        this.watcher.DisposeDebugProcess();
                    }
                    if (this.watcher.CheckMugenProcessActive())
                    {
                        this.watcher.GetMugenProcess().Kill();
                        break;
                    }
                    break;
            }
        }

        // called during event loop when the debug process is set up (polling debug setup)
        public void HandleUnsetDebugProcess()
        {
        }

        public bool MainLoop()
        {
            LogManager logManager = LogManager.MainObj();
            DebugForm debugForm = DebugForm.MainObj();
            // setup for standard BPs
            // just initialize the debug process
            if (this.watcher.GetDebugProcess() == null && !this.GetIsExperimental() && this._triggerCheckTarget.GetCurrentMode() != TriggerCheckTarget.CheckMode.CHECKMODE_SUSPENDED)
            {
                this.watcher.AttachDebugProcess();
            }
            if (this.watchAddr == 0U)
            {
                if (this.watcher.GetDebugProcess() != null)
                {
                    // handler for ProjHit instruction breakpoint -- goes to slot 1 to avoid conflicts with data BPs
                    // no support with experimental BPs for now.
                    if (!this.GetIsExperimental()) this.watcher.SetInstructionBreakpoint(this.projHitBPAddrs[(int)this.watcher.MugenVersion - 1], 1);

                    // handle other BP styles
                    if (this._stopDebugBreakFlag || this._triggerCheckTarget.GetNextCommand() == TriggerCheckTarget.CheckCommand.CHECKCMD_RESUME || this._triggerCheckTarget.GetNextCommand() == TriggerCheckTarget.CheckCommand.CHECKCMD_STOP)
                    {
                        if (this._triggerCheckTarget.GetNextCommand() != TriggerCheckTarget.CheckCommand.CHECKCMD_STOP && this._triggerCheckTarget.GetCurrentMode() != TriggerCheckTarget.CheckMode.CHECKMODE_STOPPED)
                        {
                            this._triggerCheckTarget.SetNextCommand(TriggerCheckTarget.CheckCommand.CHECKCMD_NONE);
                            this._triggerCheckTarget.SetCurrentMode(TriggerCheckTarget.CheckMode.CHECKMODE_STARTED);
                            this._triggerCheckTarget.ResetDirty();
                            if (this.debugPassInfo.backupNativeEvent != null)
                            {
                                this.watcher.ContinueEvent(this.debugPassInfo.backupNativeEvent, false);
                                this.debugPassInfo.backupNativeEvent = (NativeEvent)null;
                            }
                        }
                        else if (this._triggerCheckTarget.GetNextCommand() == TriggerCheckTarget.CheckCommand.CHECKCMD_STOP && this._triggerCheckTarget.GetCurrentMode() != TriggerCheckTarget.CheckMode.CHECKMODE_STOPPED)
                        {
                            this._triggerCheckTarget.SetNextCommand(TriggerCheckTarget.CheckCommand.CHECKCMD_NONE);
                            this._triggerCheckTarget.SetCurrentMode(TriggerCheckTarget.CheckMode.CHECKMODE_STOPPED);
                            this._triggerCheckTarget.ResetDirty();
                            if (this.debugPassInfo.backupNativeEvent != null)
                            {
                                this.watcher.ContinueEvent(this.debugPassInfo.backupNativeEvent, false);
                                this.debugPassInfo.backupNativeEvent = (NativeEvent)null;
                            }
                            this.BeginInvoke((Action)(() => DebugForm.MainObj().DisableTriggerCheckResumeButton()));
                            this.watcher.ClearHardwareBreakpoint();
                            this.BeginInvoke((Action)(() => DebugForm.MainObj().EnableTriggerCheckStartButton()))?.AsyncWaitHandle.WaitOne(this._invokeWaitTime);
                        }
                        else if (this.debugPassInfo.backupNativeEvent != null)
                        {
                            this.watcher.ContinueEvent(this.debugPassInfo.backupNativeEvent, false);
                            this.debugPassInfo.backupNativeEvent = (NativeEvent)null;
                        }
                        this._stopDebugBreakFlag = false;
                        if (this._triggerCheckTarget.GetCurrentMode() != TriggerCheckTarget.CheckMode.CHECKMODE_STOPPED)
                            this.BeginInvoke((Action)(() => DebugForm.MainObj().EnableTriggerCheckStopButton()))?.AsyncWaitHandle.WaitOne(this._invokeWaitTime);
                        this.BeginInvoke((Action)(() => this.DelayedActivate()));
                        this._isDebugBreakMode = false;
                    }
                    else if (this._triggerCheckTarget.GetNextCommand() == TriggerCheckTarget.CheckCommand.CHECKCMD_NONE && this._triggerCheckTarget.GetCurrentMode() == TriggerCheckTarget.CheckMode.CHECKMODE_STOPPED && this.debugPassInfo.backupNativeEvent != null)
                    {
                        this.watcher.ContinueEvent(this.debugPassInfo.backupNativeEvent, false);
                        this.debugPassInfo.backupNativeEvent = (NativeEvent)null;
                        this._isDebugBreakMode = false;
                        this.BeginInvoke((Action)(() => DebugForm.MainObj().DisableTriggerCheckResumeButton()));
                    }
                }
            }
            // trigger checking for experimental/sw bps (compare value at watchAddr, no hardware bp involved)
            if (this.watchAddr != 0U)
            {
                uint num20 = 0;
                // read current value of watchAddr
                byte[] buf = new byte[4];
                num20 = (uint)this.watcher.GetInt32Data(this.watchAddr, 0);
                if ((int)this.WatchInitVal != (int)num20)
                {
                    bool isValidTrigger = false;
                    TriggerDatabase.TriggerValue_t triggerValue = this.GetTriggerValue(this.debugPassInfo.baseAddr);
                    // everything below here is very similar to hw bp above
                    if (triggerValue != null && !this.debugPassInfo.triggerValueT.isEqual(triggerValue))
                    {
                        this.debugPassInfo.triggerValueT = triggerValue;
                        switch (triggerValue.valueType)
                        {
                            case TriggerDatabase.ValueType.VALUE_BOOL:
                                TriggerDatabase.TriggerValue_t targetValueFrom1 = this._triggerCheckTarget.GetTargetValueFrom();
                                if (targetValueFrom1.valueType == TriggerDatabase.ValueType.VALUE_BOOL)
                                {
                                    switch (this._triggerCheckTarget.GetTargetValueOpType())
                                    {
                                        case TriggerCheckTarget.ValueOpType.VALUE_OP_EQ:
                                            if (triggerValue.GetBoolValue() == targetValueFrom1.GetBoolValue())
                                            {
                                                isValidTrigger = true;
                                                break;
                                            }
                                            break;
                                        case TriggerCheckTarget.ValueOpType.VALUE_OP_NOT_EQ:
                                            if (triggerValue.GetBoolValue() != targetValueFrom1.GetBoolValue())
                                            {
                                                isValidTrigger = true;
                                                break;
                                            }
                                            break;
                                    }
                                    if (isValidTrigger)
                                    {
                                        GameUtils.SetPaused(this.watcher, true);
                                        break;
                                    }
                                    break;
                                }
                                if (targetValueFrom1.valueType == TriggerDatabase.ValueType.VALUE_ANY)
                                {
                                    GameUtils.SetPaused(this.watcher, true);
                                    break;
                                }
                                break;
                            case TriggerDatabase.ValueType.VALUE_INT:
                                targetValueFrom1 = this._triggerCheckTarget.GetTargetValueFrom();
                                TriggerDatabase.TriggerValue_t targetValueTo1 = this._triggerCheckTarget.GetTargetValueTo();
                                triggerValue.mask = targetValueFrom1.mask;
                                if (targetValueFrom1.valueType == TriggerDatabase.ValueType.VALUE_INT)
                                {
                                    switch (this._triggerCheckTarget.GetTargetValueOpType())
                                    {
                                        case TriggerCheckTarget.ValueOpType.VALUE_OP_EQ:
                                            if (triggerValue.GetMaskedInt32Value() == targetValueFrom1.GetInt32Value())
                                            {
                                                isValidTrigger = true;
                                                break;
                                            }
                                            break;
                                        case TriggerCheckTarget.ValueOpType.VALUE_OP_NOT_EQ:
                                            if (triggerValue.GetMaskedInt32Value() != targetValueFrom1.GetInt32Value())
                                            {
                                                isValidTrigger = true;
                                                break;
                                            }
                                            break;
                                        case TriggerCheckTarget.ValueOpType.VALUE_OP_LT:
                                            if (triggerValue.GetMaskedInt32Value() < targetValueFrom1.GetInt32Value())
                                            {
                                                isValidTrigger = true;
                                                break;
                                            }
                                            break;
                                        case TriggerCheckTarget.ValueOpType.VALUE_OP_LE:
                                            if (triggerValue.GetMaskedInt32Value() <= targetValueFrom1.GetInt32Value())
                                            {
                                                isValidTrigger = true;
                                                break;
                                            }
                                            break;
                                        case TriggerCheckTarget.ValueOpType.VALUE_OP_GT:
                                            if (triggerValue.GetMaskedInt32Value() > targetValueFrom1.GetInt32Value())
                                            {
                                                isValidTrigger = true;
                                                break;
                                            }
                                            break;
                                        case TriggerCheckTarget.ValueOpType.VALUE_OP_GE:
                                            if (triggerValue.GetMaskedInt32Value() >= targetValueFrom1.GetInt32Value())
                                            {
                                                isValidTrigger = true;
                                                break;
                                            }
                                            break;
                                        case TriggerCheckTarget.ValueOpType.VALUE_OP_FROM_TO:
                                            if (triggerValue.GetMaskedInt32Value() >= targetValueFrom1.GetInt32Value() && triggerValue.GetMaskedInt32Value() <= targetValueTo1.GetInt32Value())
                                            {
                                                isValidTrigger = true;
                                                break;
                                            }
                                            break;
                                        case TriggerCheckTarget.ValueOpType.VALUE_OP_L_FROM_TO:
                                            if (triggerValue.GetMaskedInt32Value() > targetValueFrom1.GetInt32Value() && triggerValue.GetMaskedInt32Value() <= targetValueTo1.GetInt32Value())
                                            {
                                                isValidTrigger = true;
                                                break;
                                            }
                                            break;
                                        case TriggerCheckTarget.ValueOpType.VALUE_OP_G_FROM_TO:
                                            if (triggerValue.GetMaskedInt32Value() >= targetValueFrom1.GetInt32Value() && triggerValue.GetMaskedInt32Value() < targetValueTo1.GetInt32Value())
                                            {
                                                isValidTrigger = true;
                                                break;
                                            }
                                            break;
                                        case TriggerCheckTarget.ValueOpType.VALUE_OP_LG_FROM_TO:
                                            if (triggerValue.GetMaskedInt32Value() > targetValueFrom1.GetInt32Value() && triggerValue.GetMaskedInt32Value() < targetValueTo1.GetInt32Value())
                                            {
                                                isValidTrigger = true;
                                                break;
                                            }
                                            break;
                                        case TriggerCheckTarget.ValueOpType.VALUE_OP_NOT_FROM_TO:
                                            if (triggerValue.GetMaskedInt32Value() < targetValueFrom1.GetInt32Value() || triggerValue.GetMaskedInt32Value() > targetValueTo1.GetInt32Value())
                                            {
                                                isValidTrigger = true;
                                                break;
                                            }
                                            break;
                                        case TriggerCheckTarget.ValueOpType.VALUE_OP_NOT_L_FROM_TO:
                                            if (triggerValue.GetMaskedInt32Value() <= targetValueFrom1.GetInt32Value() || triggerValue.GetMaskedInt32Value() > targetValueTo1.GetInt32Value())
                                            {
                                                isValidTrigger = true;
                                                break;
                                            }
                                            break;
                                        case TriggerCheckTarget.ValueOpType.VALUE_OP_NOT_G_FROM_TO:
                                            if (triggerValue.GetMaskedInt32Value() < targetValueFrom1.GetInt32Value() || triggerValue.GetMaskedInt32Value() >= targetValueTo1.GetInt32Value())
                                            {
                                                isValidTrigger = true;
                                                break;
                                            }
                                            break;
                                        case TriggerCheckTarget.ValueOpType.VALUE_OP_NOT_LG_FROM_TO:
                                            if (triggerValue.GetMaskedInt32Value() <= targetValueFrom1.GetInt32Value() || triggerValue.GetMaskedInt32Value() >= targetValueTo1.GetInt32Value())
                                            {
                                                isValidTrigger = true;
                                                break;
                                            }
                                            break;
                                    }
                                    if (isValidTrigger)
                                    {
                                        GameUtils.SetPaused(this.watcher, true);
                                        break;
                                    }
                                    break;
                                }
                                if (targetValueFrom1.valueType == TriggerDatabase.ValueType.VALUE_ANY)
                                {
                                    GameUtils.SetPaused(this.watcher, true);
                                    break;
                                }
                                break;
                            case TriggerDatabase.ValueType.VALUE_FLOAT:
                                TriggerDatabase.TriggerValue_t targetValueFrom2 = this._triggerCheckTarget.GetTargetValueFrom();
                                TriggerDatabase.TriggerValue_t targetValueTo2 = this._triggerCheckTarget.GetTargetValueTo();
                                if (targetValueFrom2.valueType == TriggerDatabase.ValueType.VALUE_FLOAT)
                                {
                                    switch (this._triggerCheckTarget.GetTargetValueOpType())
                                    {
                                        case TriggerCheckTarget.ValueOpType.VALUE_OP_EQ:
                                            if ((double)triggerValue.GetSingleValue() == (double)targetValueFrom2.GetSingleValue())
                                            {
                                                isValidTrigger = true;
                                                break;
                                            }
                                            break;
                                        case TriggerCheckTarget.ValueOpType.VALUE_OP_NOT_EQ:
                                            if ((double)triggerValue.GetSingleValue() != (double)targetValueFrom2.GetSingleValue())
                                            {
                                                isValidTrigger = true;
                                                break;
                                            }
                                            break;
                                        case TriggerCheckTarget.ValueOpType.VALUE_OP_LT:
                                            if ((double)triggerValue.GetSingleValue() < (double)targetValueFrom2.GetSingleValue())
                                            {
                                                isValidTrigger = true;
                                                break;
                                            }
                                            break;
                                        case TriggerCheckTarget.ValueOpType.VALUE_OP_LE:
                                            if ((double)triggerValue.GetSingleValue() <= (double)targetValueFrom2.GetSingleValue())
                                            {
                                                isValidTrigger = true;
                                                break;
                                            }
                                            break;
                                        case TriggerCheckTarget.ValueOpType.VALUE_OP_GT:
                                            if ((double)triggerValue.GetSingleValue() > (double)targetValueFrom2.GetSingleValue())
                                            {
                                                isValidTrigger = true;
                                                break;
                                            }
                                            break;
                                        case TriggerCheckTarget.ValueOpType.VALUE_OP_GE:
                                            if ((double)triggerValue.GetSingleValue() >= (double)targetValueFrom2.GetSingleValue())
                                            {
                                                isValidTrigger = true;
                                                break;
                                            }
                                            break;
                                        case TriggerCheckTarget.ValueOpType.VALUE_OP_FROM_TO:
                                            if ((double)triggerValue.GetSingleValue() >= (double)targetValueFrom2.GetSingleValue() && (double)triggerValue.GetSingleValue() <= (double)targetValueTo2.GetSingleValue())
                                            {
                                                isValidTrigger = true;
                                                break;
                                            }
                                            break;
                                        case TriggerCheckTarget.ValueOpType.VALUE_OP_L_FROM_TO:
                                            if ((double)triggerValue.GetSingleValue() > (double)targetValueFrom2.GetSingleValue() && (double)triggerValue.GetSingleValue() <= (double)targetValueTo2.GetSingleValue())
                                            {
                                                isValidTrigger = true;
                                                break;
                                            }
                                            break;
                                        case TriggerCheckTarget.ValueOpType.VALUE_OP_G_FROM_TO:
                                            if ((double)triggerValue.GetSingleValue() >= (double)targetValueFrom2.GetSingleValue() && (double)triggerValue.GetSingleValue() < (double)targetValueTo2.GetSingleValue())
                                            {
                                                isValidTrigger = true;
                                                break;
                                            }
                                            break;
                                        case TriggerCheckTarget.ValueOpType.VALUE_OP_LG_FROM_TO:
                                            if ((double)triggerValue.GetSingleValue() > (double)targetValueFrom2.GetSingleValue() && (double)triggerValue.GetSingleValue() < (double)targetValueTo2.GetSingleValue())
                                            {
                                                isValidTrigger = true;
                                                break;
                                            }
                                            break;
                                        case TriggerCheckTarget.ValueOpType.VALUE_OP_NOT_FROM_TO:
                                            if ((double)triggerValue.GetSingleValue() < (double)targetValueFrom2.GetSingleValue() || (double)triggerValue.GetSingleValue() > (double)targetValueTo2.GetSingleValue())
                                            {
                                                isValidTrigger = true;
                                                break;
                                            }
                                            break;
                                        case TriggerCheckTarget.ValueOpType.VALUE_OP_NOT_L_FROM_TO:
                                            if ((double)triggerValue.GetSingleValue() <= (double)targetValueFrom2.GetSingleValue() || (double)triggerValue.GetSingleValue() > (double)targetValueTo2.GetSingleValue())
                                            {
                                                isValidTrigger = true;
                                                break;
                                            }
                                            break;
                                        case TriggerCheckTarget.ValueOpType.VALUE_OP_NOT_G_FROM_TO:
                                            if ((double)triggerValue.GetSingleValue() < (double)targetValueFrom2.GetSingleValue() || (double)triggerValue.GetSingleValue() >= (double)targetValueTo2.GetSingleValue())
                                            {
                                                isValidTrigger = true;
                                                break;
                                            }
                                            break;
                                        case TriggerCheckTarget.ValueOpType.VALUE_OP_NOT_LG_FROM_TO:
                                            if ((double)triggerValue.GetSingleValue() <= (double)targetValueFrom2.GetSingleValue() || (double)triggerValue.GetSingleValue() >= (double)targetValueTo2.GetSingleValue())
                                            {
                                                isValidTrigger = true;
                                                break;
                                            }
                                            break;
                                    }
                                    if (isValidTrigger)
                                    {
                                        GameUtils.SetPaused(this.watcher, true);
                                        break;
                                    }
                                    break;
                                }
                                if (targetValueFrom2.valueType == TriggerDatabase.ValueType.VALUE_ANY)
                                {
                                    GameUtils.SetPaused(this.watcher, true);
                                    break;
                                }
                                break;
                        }
                    }
                    // fire the experimental bp
                    if (isValidTrigger)
                        this.BeginInvoke((Action)(() => DebugForm.MainObj().SetExpBPFired()))?.AsyncWaitHandle.WaitOne(this._invokeWaitTime);
                }
            }
            // load data + checks for whether Mugen is frozen, inactive, stuck, etc.
            if (this.watcher.CheckMugenProcessActive())
            {
                DateTime now;
                // failsafe against unending RoundState=4, kills mugen if detected
                if (this.debugPassInfo.num16 != 0L)
                {
                    MugenProfile profile = ProfileManager.MainObj().GetProfile(this._workingProfileId);
                    if (profile != null && profile.IsAutoMode())
                    {
                        long maxRoundState4Time = (long)profile.GetMaxRoundState4Time();
                        now = DateTime.Now;
                        long num20 = now.Ticks / 10000000L;
                        if (maxRoundState4Time > 0L && num20 > this.debugPassInfo.num16 + maxRoundState4Time)
                        {
                            this.debugPassInfo.num16 = 0L;
                            if (this.watcher.CheckMugenProcessActive())
                                this.watcher.GetMugenProcess().Kill();
                        }
                    }
                }
                // other checks
                if (this.watcher.CheckMugenProcessActive())
                {
                    if (this.debugPassInfo.flag7 || !this.debugPassInfo.flag6 && this._isActivatedOnce)
                    {
                        IAsyncResult asyncResult = this.BeginInvoke((Action)(() => LogManager.MainObj().IsAvailable()));
                        if (asyncResult != null && this.watchAddr == 0U)
                        {
                            if (!asyncResult.AsyncWaitHandle.WaitOne(5000))
                            {
                                ++this.debugPassInfo.num17;
                                if (this.debugPassInfo.num17 > 1)
                                {
                                    switch (this._triggerCheckTarget.GetCurrentMode())
                                    {
                                        case TriggerCheckTarget.CheckMode.CHECKMODE_STOPPED:
                                        case TriggerCheckTarget.CheckMode.CHECKMODE_STARTED:
                                            this.watcher.GetMugenProcess().Kill();
                                            break;
                                        case TriggerCheckTarget.CheckMode.CHECKMODE_SUSPENDED:
                                            this._triggerCheckTarget.SetNextCommand(TriggerCheckTarget.CheckCommand.CHECKCMD_STOP);
                                            this._triggerCheckTarget.SetCurrentMode(TriggerCheckTarget.CheckMode.CHECKMODE_STARTED);
                                            this._triggerCheckTarget.ResetDirty();
                                            if (this.debugPassInfo.backupNativeEvent != null)
                                            {
                                                this.watcher.ContinueEvent(this.debugPassInfo.backupNativeEvent, false);
                                                this.debugPassInfo.backupNativeEvent = (NativeEvent)null;
                                            }
                                            this._stopDebugBreakFlag = false;
                                            this.BeginInvoke((Action)(() => DebugForm.MainObj().EnableTriggerCheckStartButton()));
                                            this.watcher.GetMugenProcess().Kill();
                                            this._isDebugBreakMode = false;
                                            break;
                                    }
                                }
                            }
                            else
                                this.debugPassInfo.num17 = 0;
                        }
                    }
                    // data loading segment?
                    if (this.debugPassInfo.baseAddr == 0U)
                        this.debugPassInfo.baseAddr = (uint)GameUtils.GetBaseAddress(this.watcher);
                    // check for re-capture
                    int tmpRoundState = GameUtils.GetRoundState(this.watcher);
                    if (tmpRoundState == 0)
                    {
                        this.isWindowCaptured = false;
                    }
                    if (this.debugPassInfo.baseAddr != 0U && !GameUtils.IsDemo(this.watcher))
                    {
                        if (this.debugPassInfo.baseAddr != 0U)
                        {
                            if (this._flagDumpPlayers)
                            {
                                this._flagDumpPlayers = false;
                                this._DumpPlayers(this.debugPassInfo.baseAddr);
                                if (this.debugPassInfo.flag6 | this.debugPassInfo.flag7)
                                {
                                    this.debugPassInfo.flag6 = false;
                                    this.debugPassInfo.flag7 = false;
                                    MugenProfile profile = ProfileManager.MainObj().GetProfile(this._workingProfileId);
                                    if (!GameUtils.IsTurnsMode(this.watcher) && profile != null && !profile.IsQuickMode() || profile != null && profile.IsAutoMode())
                                    {
                                        this.UpdatePlayerDataEx(this.debugPassInfo.displayName2, this.debugPassInfo.palnoA1, this.debugPassInfo.displayName4, this.debugPassInfo.palnoB1, 0, 0, 0, 1, 0, this.debugPassInfo.trueTeam1WinCount, this.debugPassInfo.num3, this.debugPassInfo.trueTeam2WinCount, this.debugPassInfo.num4, this.debugPassInfo.drawRounds, 1, 0);
                                        this.UpdatePlayerDataEx(this.debugPassInfo.displayName3, this.debugPassInfo.palnoA2, this.debugPassInfo.displayName5, this.debugPassInfo.palnoB2, 0, 0, 0, 1, 0, this.debugPassInfo.trueTeam2WinCount, this.debugPassInfo.num4, this.debugPassInfo.trueTeam1WinCount, this.debugPassInfo.num3, this.debugPassInfo.drawRounds, 1, 0);
                                        return false;
                                    }
                                }
                            }
                            if (this.watcher.CheckMugenProcessActive() && this.debugPassInfo.num19 > 0)
                            {
                                now = DateTime.Now;
                                if (now.Ticks / 10000000L % 2L == 0L)
                                {
                                    this.SetForegroundWindowEx(this.watcher.GetMugenWindowHandle());
                                    if (GameUtils.IsMugenActive(this.watcher))
                                        --this.debugPassInfo.num19;
                                }
                            }
                            this.debugPassInfo.playerAddr1 = GameUtils.GetP1Addr(this.watcher);
                            if (this.debugPassInfo.playerAddr1 == 0U)
                            {
                                this.debugPassInfo.flag1 = false;
                                if (this.debugPassInfo.flag6 | this.debugPassInfo.flag7)
                                {
                                    this.debugPassInfo.flag6 = false;
                                    this.debugPassInfo.flag7 = false;
                                    int num20 = GameUtils.IsSpeedMode(this.watcher) ? 1 : 0;
                                    bool flag9 = GameUtils.IsSkipMode(this.watcher);
                                    if (num20 != 0)
                                        GameUtils.SetSpeedMode(this.watcher, this.debugPassInfo.baseAddr, false);
                                    if (flag9)
                                        GameUtils.SetSkipMode(this.watcher, this.debugPassInfo.baseAddr, false, this._skipModeFrames);
                                    if (!GameUtils.IsTurnsMode(this.watcher))
                                    {
                                        this._isGameQuitted = true;
                                        if (logManager != null)
                                            AsyncAppendLog("The match has been cancelled.");
                                        MugenProfile profile = ProfileManager.MainObj().GetProfile(this._workingProfileId);
                                        if (profile != null && !profile.IsQuickMode())
                                        {
                                            this.UpdatePlayerDataEx(this.debugPassInfo.displayName2, this.debugPassInfo.palnoA1, this.debugPassInfo.displayName4, this.debugPassInfo.palnoB1, 0, 0, 0, 0, 1, this.debugPassInfo.trueTeam1WinCount, this.debugPassInfo.num3, this.debugPassInfo.trueTeam2WinCount, this.debugPassInfo.num4, this.debugPassInfo.drawRounds, 0, 1);
                                            this.UpdatePlayerDataEx(this.debugPassInfo.displayName3, this.debugPassInfo.palnoA2, this.debugPassInfo.displayName5, this.debugPassInfo.palnoB2, 0, 0, 0, 0, 1, this.debugPassInfo.trueTeam2WinCount, this.debugPassInfo.num4, this.debugPassInfo.trueTeam1WinCount, this.debugPassInfo.num3, this.debugPassInfo.drawRounds, 0, 1);
                                        }
                                    }
                                }
                            }
                            if (this.debugPassInfo.playerAddr1 == 0U || this.player1Id != PlayerUtils.GetPlayerId(this.watcher, this.debugPassInfo.playerAddr1))
                            {
                                this.debugPassInfo.flag2 = false;
                                this.player1AnimAddr = 0U;
                                if (this.debugPassInfo.num9 == 0L)
                                {
                                    now = DateTime.Now;
                                    this.debugPassInfo.num9 = now.Ticks / 10000000L;
                                }
                                MugenProfile profile = ProfileManager.MainObj().GetProfile(this._workingProfileId);
                                if (profile != null && profile.IsAutoMode())
                                {
                                    long num20 = (long)(profile.GetMaxRoundState1Time() * 2);
                                    now = DateTime.Now;
                                    long num21 = now.Ticks / 10000000L;
                                    if (num20 > 0L && num21 > this.debugPassInfo.num9 + num20 && !this._isMugenFrozen)
                                    {
                                        this._isMugenFrozen = true;
                                        if (logManager != null)
                                            AsyncAppendLog("Loading the characters is taking too long.");
                                    }
                                }
                            }
                            this.debugPassInfo.playerAddr2 = GameUtils.GetP2Addr(this.watcher);
                            if (this.debugPassInfo.playerAddr2 == 0U || this.player2Id != PlayerUtils.GetPlayerId(this.watcher, this.debugPassInfo.playerAddr2))
                            {
                                this.debugPassInfo.flag3 = false;
                                this.player2AnimAddr = 0U;
                                if (this.debugPassInfo.num10 == 0L)
                                {
                                    now = DateTime.Now;
                                    this.debugPassInfo.num10 = now.Ticks / 10000000L;
                                }
                                MugenProfile profile = ProfileManager.MainObj().GetProfile(this._workingProfileId);
                                if (profile != null && profile.IsAutoMode())
                                {
                                    long num20 = (long)(profile.GetMaxRoundState1Time() * 2);
                                    now = DateTime.Now;
                                    long num21 = now.Ticks / 10000000L;
                                    if (num20 > 0L && num21 > this.debugPassInfo.num10 + num20 && !this._isMugenFrozen)
                                    {
                                        this._isMugenFrozen = true;
                                        if (logManager != null)
                                            AsyncAppendLog("Loading the characters is taking too long.");
                                    }
                                }
                            }
                            this.debugPassInfo.playerAddr3 = GameUtils.GetP3Addr(this.watcher);
                            if (this.debugPassInfo.playerAddr3 == 0U || this.player3Id != PlayerUtils.GetPlayerId(this.watcher, this.debugPassInfo.playerAddr3))
                            {
                                this.debugPassInfo.flag4 = false;
                                this.player3AnimAddr = 0U;
                            }
                            this.debugPassInfo.playerAddr4 = GameUtils.GetP4Addr(this.watcher);
                            if (this.debugPassInfo.playerAddr4 == 0U || this.player4Id != PlayerUtils.GetPlayerId(this.watcher, this.debugPassInfo.playerAddr4))
                            {
                                this.debugPassInfo.flag5 = false;
                                this.player4AnimAddr = 0U;
                            }
                        }
                        if (this.debugPassInfo.playerAddr1 != 0U && !this.debugPassInfo.flag1 && this.GetDisplayName(this.debugPassInfo.playerAddr1, ref this.debugPassInfo.displayName1) > 0)
                        {
                            this.debugPassInfo.flag1 = true;
                            this._invokeWaitTime = 20;
                            if (this.watcher.GetMugenProcess() != null)
                            {
                                this.BeginInvoke((Action)(() => DebugForm.MainObj().InitPlayers(60)))?.AsyncWaitHandle.WaitOne(this._invokeWaitTime);
                                this.BeginInvoke((Action)(() => DebugForm.MainObj().InitExplods(50)))?.AsyncWaitHandle.WaitOne(this._invokeWaitTime);
                                this.BeginInvoke((Action)(() => DebugForm.MainObj().InitProjs(50)))?.AsyncWaitHandle.WaitOne(this._invokeWaitTime);
                                this.BeginInvoke((Action)(() => DebugForm.MainObj().InitTriggerCheck(this.watcher.MugenVersion)))?.AsyncWaitHandle.WaitOne(this._invokeWaitTime);
                                if (this._triggerCheckTarget.GetCurrentMode() == TriggerCheckTarget.CheckMode.CHECKMODE_STARTED)
                                    this.BeginInvoke((Action)(() => this.StartTriggerCheckMode()))?.AsyncWaitHandle.WaitOne(this._invokeWaitTime);
                            }
                            this.debugPassInfo.trueTeam1WinCount = 0;
                            this.debugPassInfo.trueTeam2WinCount = 0;
                            this.debugPassInfo.num3 = 0;
                            this.debugPassInfo.num4 = 0;
                            MugenProfile profile = ProfileManager.MainObj().GetProfile(this._workingProfileId);
                            if (profile == null || !profile.IsAutoMode() || this.numOfGames == 0)
                                ++this.numOfGames;
                            if (logManager != null)
                            {
                                AsyncAppendLog(" ");
                                if (profile == null || !profile.IsAutoMode() || !this._isRetryGame)
                                {
                                    AsyncAppendLog("//////Match " + (object)this.numOfGames + " //////");
                                    if (profile != null && profile.IsAutoMode())
                                        logManager.append(" - (" + (object)profile.GetCharacterCount() + " total)");
                                }
                                else
                                    AsyncAppendLog("//////Match " + (object)this.numOfGames + " (retry)//////");
                                if (profile != null && profile.IsQuickMode())
                                    AsyncAppendLog("(No overall results are calculated in Quick Vs. mode)");
                            }
                        }
                        if (this.debugPassInfo.playerAddr1 != 0U && !this.debugPassInfo.flag2)
                        {
                            this.debugPassInfo.num9 = 0L;
                            if (this.GetDisplayName(this.debugPassInfo.playerAddr1, ref this.debugPassInfo.displayName2) > 0)
                            {
                                this.debugPassInfo.flag2 = true;
                                this.player1Id = PlayerUtils.GetPlayerId(this.watcher, this.debugPassInfo.playerAddr1);
                                this.player1AnimAddr = PlayerUtils.GetAnimListAddr(this.watcher, this.debugPassInfo.baseAddr, this.debugPassInfo.playerAddr1);
                                this.debugPassInfo.palnoA1 = PlayerUtils.GetPalno(this.watcher, this.debugPassInfo.playerAddr1);
                                string msg = "P1 side: " + this.debugPassInfo.displayName2;
                                msg = msg + " (" + this.debugPassInfo.palnoA1.ToString() + "p)";
                                this.BeginInvoke((Action)(() => DebugForm.MainObj().EnablePlayer(1)));
                                if (logManager != null)
                                    AsyncAppendLog(msg);
                            }
                            else
                                this.debugPassInfo.displayName2 = (string)null;
                        }
                        if (this.debugPassInfo.playerAddr3 != 0U && !this.debugPassInfo.flag4)
                        {
                            if (this.GetDisplayName(this.debugPassInfo.playerAddr3, ref this.debugPassInfo.displayName4) > 0)
                            {
                                this.debugPassInfo.flag4 = true;
                                this.player3Id = PlayerUtils.GetPlayerId(this.watcher, this.debugPassInfo.playerAddr3);
                                this.player3AnimAddr = PlayerUtils.GetAnimListAddr(this.watcher, this.debugPassInfo.baseAddr, this.debugPassInfo.playerAddr3);
                                this.debugPassInfo.palnoB1 = PlayerUtils.GetPalno(this.watcher, this.debugPassInfo.playerAddr3);
                                string msg = "P1 side: " + this.debugPassInfo.displayName4;
                                msg = msg + " (" + this.debugPassInfo.palnoB1.ToString() + "p)";
                                this.BeginInvoke((Action)(() => DebugForm.MainObj().EnablePlayer(3)));
                                if (logManager != null)
                                    AsyncAppendLog(msg);
                            }
                            else
                                this.debugPassInfo.displayName4 = (string)null;
                        }
                        if (this.debugPassInfo.playerAddr2 != 0U && !this.debugPassInfo.flag3)
                        {
                            this.debugPassInfo.num10 = 0L;
                            if (this.GetDisplayName(this.debugPassInfo.playerAddr2, ref this.debugPassInfo.displayName3) > 0)
                            {
                                this.debugPassInfo.flag3 = true;
                                this.player2Id = PlayerUtils.GetPlayerId(this.watcher, this.debugPassInfo.playerAddr2);
                                this.player2AnimAddr = PlayerUtils.GetAnimListAddr(this.watcher, this.debugPassInfo.baseAddr, this.debugPassInfo.playerAddr2);
                                this.debugPassInfo.palnoA2 = PlayerUtils.GetPalno(this.watcher, this.debugPassInfo.playerAddr2);
                                string msg = "P2 side: " + this.debugPassInfo.displayName3;
                                msg = msg + " (" + this.debugPassInfo.palnoA2.ToString() + "p)";
                                this.BeginInvoke((Action)(() => DebugForm.MainObj().EnablePlayer(2)));
                                if (logManager != null)
                                    AsyncAppendLog(msg);
                            }
                            else
                                this.debugPassInfo.displayName3 = (string)null;
                        }
                        if (this.debugPassInfo.playerAddr4 != 0U && !this.debugPassInfo.flag5)
                        {
                            if (this.GetDisplayName(this.debugPassInfo.playerAddr4, ref this.debugPassInfo.displayName5) > 0)
                            {
                                this.debugPassInfo.flag5 = true;
                                this.player4Id = PlayerUtils.GetPlayerId(this.watcher, this.debugPassInfo.playerAddr4);
                                this.player4AnimAddr = PlayerUtils.GetAnimListAddr(this.watcher, this.debugPassInfo.baseAddr, this.debugPassInfo.playerAddr4);
                                this.debugPassInfo.palnoB2 = PlayerUtils.GetPalno(this.watcher, this.debugPassInfo.playerAddr4);
                                string msg = "P2 side: " + this.debugPassInfo.displayName5;
                                msg = msg + " (" + this.debugPassInfo.palnoB2.ToString() + "p)";
                                this.BeginInvoke((Action)(() => DebugForm.MainObj().EnablePlayer(4)));
                                if (logManager != null)
                                    AsyncAppendLog(msg);
                            }
                            else
                                this.debugPassInfo.displayName5 = (string)null;
                        }
                        if (this._isMugenActive != GameUtils.IsMugenActive(this.watcher))
                        {
                            this._isMugenActive = GameUtils.IsMugenActive(this.watcher);
                            if (this.watcher.GetMugenProcess() != null)
                                this.BeginInvoke((Action)(() => this.SetTitleActive(this._isMugenActive)))?.AsyncWaitHandle.WaitOne(this._invokeWaitTime);
                        }
                        bool currentDebugMode = GameUtils.IsDebugMode(this.watcher);
                        if (this._isDebugModeChanged)
                        {
                            this._isDebugModeChanged = false;
                            if (currentDebugMode != this._isDebugMode)
                                GameUtils.SetDebugMode(this.watcher, this.debugPassInfo.baseAddr, this._isDebugMode);
                        }
                        else if (currentDebugMode != this._isDebugMode)
                        {
                            this._isDebugMode = currentDebugMode;
                            if (this.watcher.GetMugenProcess() != null)
                                this.BeginInvoke((Action)(() => debugForm.SetDebugModeCheckBox(currentDebugMode, false)))?.AsyncWaitHandle.WaitOne(this._invokeWaitTime);
                        }
                        if (this._isDebugMode)
                        {
                            if (this._varInspectTargetPlayer > 0)
                            {
                                int playerNoFromId = this.GetPlayerNoFromId(this.debugPassInfo.baseAddr, this._varInspectTargetPlayer);
                                if (playerNoFromId >= 0)
                                {
                                    this._varInspectTargetNo = playerNoFromId + 1;
                                    this._varInspectTargetPlayer = 0;
                                }
                            }
                            if (this._debugTargetPlayer > 0)
                            {
                                int playerNoFromId = this.GetPlayerNoFromId(this.debugPassInfo.baseAddr, this._debugTargetPlayer);
                                if (playerNoFromId >= 0)
                                {
                                    this._debugTargetNo = playerNoFromId + 1;
                                    this._debugTargetPlayer = 0;
                                    if (this._debugTargetNo != GameUtils.GetDebugTargetNo(this.watcher))
                                    {
                                        GameUtils.SetDebugTargetNo(this.watcher, this.debugPassInfo.baseAddr, this._debugTargetNo);
                                        if (this.watcher.GetMugenProcess() != null)
                                            debugForm.SetDebugTargetNo(this._debugTargetNo);
                                    }
                                }
                            }
                            if (this._isDebugTargetNoChanged)
                            {
                                this._isDebugTargetNoChanged = false;
                                GameUtils.SetDebugTargetNo(this.watcher, this.debugPassInfo.baseAddr, this._debugTargetNo);
                            }
                            else if (this._debugTargetNo == 0)
                            {
                                this._debugTargetNo = 1;
                                this._varInspectTargetNo = 1;
                                GameUtils.SetDebugTargetNo(this.watcher, this.debugPassInfo.baseAddr, this._debugTargetNo);
                            }
                            else
                            {
                                int debugNo = GameUtils.GetDebugTargetNo(this.watcher);
                                if (this._debugTargetNo != debugNo)
                                {
                                    this._debugTargetNo = debugNo;
                                    if (this.watcher.GetMugenProcess() != null)
                                        this.BeginInvoke((Action)(() => debugForm.SetDebugTargetNo(debugNo)))?.AsyncWaitHandle.WaitOne(this._invokeWaitTime);
                                }
                            }
                        }
                        if (this._isDebugMode)
                        {
                            if (this._debugColor != DebugColor.NONE)
                            {
                                this._debugColorChanged = false;
                                this.SetDebugColor(this.debugPassInfo.baseAddr, this._debugColor);
                            }
                            else if (this._debugColorChanged)
                            {
                                this._debugColorChanged = false;
                                this.SetDebugColor(this.debugPassInfo.baseAddr, DebugColor.NONE);
                            }
                        }
                        if (this.watcher.GetDebugProcess() != null && this._triggerCheckTarget != null && (this._triggerCheckTarget.IsDirty() && this.watchAddr == 0U))
                        {
                            if(this.debugPassInfo.triggerValueT != null) this.debugPassInfo.triggerValueT.reset();
                            if (this._triggerCheckTarget.GetNextCommand() == TriggerCheckTarget.CheckCommand.CHECKCMD_START)
                            {
                                if (this._SetBreakPoint(this.debugPassInfo.baseAddr))
                                {
                                    this._triggerCheckTarget.SetNextCommand(TriggerCheckTarget.CheckCommand.CHECKCMD_NONE);
                                    this._triggerCheckTarget.SetCurrentMode(TriggerCheckTarget.CheckMode.CHECKMODE_STARTED);
                                    this._triggerCheckTarget.ResetDirty();
                                    this.BeginInvoke((Action)(() => DebugForm.MainObj().EnableTriggerCheckStopButton()))?.AsyncWaitHandle.WaitOne(this._invokeWaitTime);
                                    this.BeginInvoke((Action)(() => this.Activate()));
                                }
                            }
                            else if (this._triggerCheckTarget.GetNextCommand() == TriggerCheckTarget.CheckCommand.CHECKCMD_STOP)
                            {
                                this._triggerCheckTarget.SetNextCommand(TriggerCheckTarget.CheckCommand.CHECKCMD_NONE);
                                this._triggerCheckTarget.SetCurrentMode(TriggerCheckTarget.CheckMode.CHECKMODE_STOPPED);
                                this._triggerCheckTarget.ResetDirty();
                                this.watcher.ClearHardwareBreakpoint();
                                this.BeginInvoke((Action)(() => DebugForm.MainObj().EnableTriggerCheckStartButton()))?.AsyncWaitHandle.WaitOne(this._invokeWaitTime);
                            }
                        }
                        ++this.debugPassInfo.num8;
                        if (this.debugPassInfo.num8 >= 1)
                        {
                            this.debugPassInfo.num8 = 0;
                            if (this._isDebugMode)
                            {
                                // setup values for various debug form views
                                switch (this._debugListMode)
                                {
                                    case MugenWindow.DebugListMode.PLAYER_LIST_MODE:
                                        this.ListUpPlayers(this.debugPassInfo.baseAddr);
                                        break;
                                    case MugenWindow.DebugListMode.EXPLOD_LIST_MODE:
                                        this.ListUpExplods(this.debugPassInfo.baseAddr);
                                        break;
                                    case MugenWindow.DebugListMode.PROJ_LIST_MODE:
                                        switch (this._projOwner)
                                        {
                                            case MugenWindow.ProjOwner.P1:
                                                this.ListUpProjs(this.player1Id, this.debugPassInfo.playerAddr1);
                                                break;
                                            case MugenWindow.ProjOwner.P2:
                                                this.ListUpProjs(this.player2Id, this.debugPassInfo.playerAddr2);
                                                break;
                                            case MugenWindow.ProjOwner.P3:
                                                this.ListUpProjs(this.player3Id, this.debugPassInfo.playerAddr3);
                                                break;
                                            case MugenWindow.ProjOwner.P4:
                                                this.ListUpProjs(this.player4Id, this.debugPassInfo.playerAddr4);
                                                break;
                                        }
                                        break;
                                    default:
                                        this.ListUpPlayers(this.debugPassInfo.baseAddr);
                                        break;
                                }
                                this.UpdateVariables(this.debugPassInfo.baseAddr);
                            }
                        }
                        if (this.debugPassInfo.flag1)
                        {
                            // handling for long RoundState=0,4
                            int roundState1 = GameUtils.GetRoundState(this.watcher);
                            if (roundState1 == 0)
                            {
                                this._invokeWaitTime = 20;
                                this.debugPassInfo.team1Wins = GameUtils.GetTeam1WinCount(this.watcher);
                                this.debugPassInfo.team2Wins = GameUtils.GetTeam2WinCount(this.watcher);
                                this.debugPassInfo.drawRounds = GameUtils.GetDrawGameCount(this.watcher);
                                this.debugPassInfo.gameTime = (uint)GameUtils.GetGameTime(this.watcher);
                                int speedMode = GameUtils.IsSpeedMode(this.watcher) ? 1 : 0;
                                bool isSkipMode = GameUtils.IsSkipMode(this.watcher);
                                int prevSpeedMode = this._isSpeedMode ? 1 : 0;
                                if (speedMode != prevSpeedMode)
                                    GameUtils.SetSpeedMode(this.watcher, this.debugPassInfo.baseAddr, this._isSpeedMode);
                                if (isSkipMode != this._isSkipMode)
                                    GameUtils.SetSkipMode(this.watcher, this.debugPassInfo.baseAddr, this._isSkipMode, this._skipModeFrames);
                                if (this.debugPassInfo.num11 == 0L)
                                {
                                    now = DateTime.Now;
                                    this.debugPassInfo.num11 = now.Ticks / 10000000L;
                                }
                                MugenProfile profile = ProfileManager.MainObj().GetProfile(this._workingProfileId);
                                if (profile != null && profile.IsAutoMode())
                                {
                                    long maxRoundState1Time = (long)profile.GetMaxRoundState1Time();
                                    now = DateTime.Now;
                                    long num22 = now.Ticks / 10000000L;
                                    if (maxRoundState1Time > 0L && num22 > this.debugPassInfo.num11 + maxRoundState1Time && !this._isMugenFrozen)
                                    {
                                        this._isMugenFrozen = true;
                                        if (logManager != null)
                                            AsyncAppendLog("Startup is taking too long.");
                                    }
                                }
                            }
                            else if (this.debugPassInfo.flag6 || roundState1 == 4)
                            {
                                if (!GameUtils.IsPaused(this.watcher) && !this._isDebugBreakMode)
                                {
                                    ++this.debugPassInfo.num6;
                                    if (this.debugPassInfo.num6 == this.debugPassInfo.num5)
                                    {
                                        if ((int)GameUtils.GetGameTime(this.watcher) - (int)this.debugPassInfo.gameTime == 0)
                                        {
                                            this._isMugenFrozen = true;
                                            this._invokeWaitTime = 10;
                                        }
                                        this.debugPassInfo.num6 = 0;
                                        this.debugPassInfo.gameTime = (uint)GameUtils.GetGameTime(this.watcher);
                                    }
                                }
                                else
                                    this.debugPassInfo.num6 = 0;
                                if (this.watcher.GetMugenProcess() != null)
                                    this.BeginInvoke((Action)(() => debugForm.SetPauseCheckBox(GameUtils.IsPaused(this.watcher))))?.AsyncWaitHandle.WaitOne(this._invokeWaitTime);
                                if (GameUtils.IsPaused(this.watcher) && this._isStepMode)
                                {
                                    now = DateTime.Now;
                                    this._stepModeCounter = now.Ticks / 10000L;
                                    if (this._stepModeCounter - this._stepModeLastCounter > this._stepModeInterval)
                                    {
                                        this._stepModeLastCounter = this._stepModeCounter;
                                        this.InjectStepCommand();
                                    }
                                }
                                bool currentSpeedMode = GameUtils.IsSpeedMode(this.watcher);
                                bool flag9 = GameUtils.IsSkipMode(this.watcher);
                                int skipFrames = GameUtils.GetSkipFrames(this.watcher);
                                if (roundState1 < 3)
                                {
                                    if (this._isSpeedModeChanged)
                                    {
                                        this._isSpeedModeChanged = false;
                                        if (currentSpeedMode != this._isSpeedMode)
                                        {
                                            GameUtils.SetSpeedMode(this.watcher, this.debugPassInfo.baseAddr, this._isSpeedMode);
                                            this._wasSpeedModeChanged = true;
                                        }
                                    }
                                    else if (currentSpeedMode != this._isSpeedMode)
                                    {
                                        if (this._wasSpeedModeChanged)
                                        {
                                            GameUtils.SetSpeedMode(this.watcher, this.debugPassInfo.baseAddr, this._isSpeedMode);
                                            this._wasSpeedModeChanged = false;
                                        }
                                        else
                                        {
                                            this._isSpeedMode = currentSpeedMode;
                                            if (this.watcher.GetMugenProcess() != null)
                                                this.BeginInvoke((Action)(() => debugForm.SetSpeedModeCheckBox(currentSpeedMode, false)))?.AsyncWaitHandle.WaitOne(this._invokeWaitTime);
                                        }
                                    }
                                    if (flag9 != this._isSkipMode || flag9 && skipFrames != this._skipModeFrames)
                                        GameUtils.SetSkipMode(this.watcher, this.debugPassInfo.baseAddr, this._isSkipMode, this._skipModeFrames);
                                }
                            }
                            // handler for checking if Mugen crashed or froze during RoundState=[1,3]
                            // as well as data read/load/update
                            int roundState2 = GameUtils.GetRoundState(this.watcher);
                            if (roundState2 >= 1 && roundState2 <= 3 && (this.debugPassInfo.flag2 & this.debugPassInfo.flag3 && !this._isMugenCrashed))
                            {
                                if (!this.debugPassInfo.flag6)
                                {
                                    this.debugPassInfo.flag6 = true;
                                    this.debugPassInfo.num11 = 0L;
                                    this.debugPassInfo.num12 = 0L;
                                    this.debugPassInfo.num13 = 0L;
                                    this.debugPassInfo.num14 = 0L;
                                    this.debugPassInfo.num15 = 0L;
                                    this.debugPassInfo.num16 = 0L;
                                    this.debugPassInfo.num18 = GameUtils.GetRoundNoOfMatch(this.watcher);
                                    this.debugPassInfo.team1Wins = GameUtils.GetTeam1WinCount(this.watcher);
                                    this.debugPassInfo.team2Wins = GameUtils.GetTeam2WinCount(this.watcher);
                                    this.debugPassInfo.drawRounds = GameUtils.GetDrawGameCount(this.watcher);
                                    this._isGameQuitted = false;
                                    if (this._triggerCheckTarget != null)
                                        this._triggerCheckTarget.SetDirty();
                                    MugenProfile profile = ProfileManager.MainObj().GetProfile(this._workingProfileId);
                                    if (profile != null && profile.IsAutoMode())
                                    {
                                        this.debugPassInfo.num18 = !profile.IsStrictRoundMode() ? GameUtils.GetRoundNoOfMatch(this.watcher) : GameUtils.GetRoundNoOfMatch(this.watcher) - 1;
                                        if (!GameUtils.IsDebugMode(this.watcher))
                                            GameUtils.EnableDebugKey(this.watcher, this.debugPassInfo.baseAddr, true);
                                        this.BeginInvoke((Action)(() => DebugForm.MainObj().SetSpeedModeCheckBox(profile.IsSpeedMode(), true)));
                                        this.BeginInvoke((Action)(() => DebugForm.MainObj().SetSkipModeCheckBox(profile.IsSkipMode())));
                                        this.BeginInvoke((Action)(() => DebugForm.MainObj().SetDebugModeCheckBox(profile.IsDebugMode(), true)));
                                        if (profile.IsStrictRoundMode())
                                        {
                                            int team1WinCount = GameUtils.GetTeam1WinCount(this.watcher);
                                            int team2WinCount = GameUtils.GetTeam2WinCount(this.watcher);
                                            int num20 = this.debugPassInfo.num18;
                                            if (team1WinCount >= num20 || team2WinCount >= this.debugPassInfo.num18)
                                            {
                                                this.debugPassInfo.flag6 = false;
                                                if (this.watcher.CheckMugenProcessActive())
                                                {
                                                    this.KillGracefully();
                                                    return true;
                                                }
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    MugenProfile profile = ProfileManager.MainObj().GetProfile(this._workingProfileId);
                                    if (profile != null && profile.IsAutoMode() && GameUtils.GetRoundNo(this.watcher) > this.debugPassInfo.trueTeam1WinCount + this.debugPassInfo.trueTeam2WinCount + this.debugPassInfo.drawRounds + 1)
                                    {
                                        this.debugPassInfo.flag7 = false;
                                        this.debugPassInfo.flag6 = false;
                                        this.debugPassInfo.flag8 = true;
                                        int num20 = GameUtils.GetDrawGameCount(this.watcher) + 1;
                                        GameUtils.SetDrawGameCount(this.watcher, num20);
                                        if (logManager != null)
                                        {
                                            AsyncAppendLog("Quit possibly, this game was fixed in some manner.");
                                            AsyncAppendLog("⇒ Draw.");
                                        }
                                    }
                                }
                            }
                            if (roundState2 == 1)
                            {
                                this._invokeWaitTime = 20;
                                this.debugPassInfo.num11 = 0L;
                                this.debugPassInfo.num13 = 0L;
                                this.debugPassInfo.num14 = 0L;
                                this.debugPassInfo.num15 = 0L;
                                this.debugPassInfo.num16 = 0L;
                                if (this.debugPassInfo.num12 == 0L)
                                {
                                    now = DateTime.Now;
                                    this.debugPassInfo.num12 = now.Ticks / 10000000L;
                                }
                                MugenProfile profile = ProfileManager.MainObj().GetProfile(this._workingProfileId);
                                if (profile != null && profile.IsAutoMode())
                                {
                                    long maxRoundState1Time = (long)profile.GetMaxRoundState1Time();
                                    now = DateTime.Now;
                                    long num20 = now.Ticks / 10000000L;
                                    if (maxRoundState1Time > 0L && num20 > this.debugPassInfo.num12 + maxRoundState1Time)
                                    {
                                        if (logManager != null)
                                            AsyncAppendLog("The introductions phase has been skipped as it was too long.");
                                        GameUtils.SetRoundState(this.watcher, 2);
                                        if (this.debugPassInfo.playerAddr1 != 0U)
                                            PlayerUtils.SetCtrl(this.watcher, this.debugPassInfo.playerAddr1, true);
                                        if (this.debugPassInfo.playerAddr2 != 0U)
                                            PlayerUtils.SetCtrl(this.watcher, this.debugPassInfo.playerAddr2, true);
                                        if (this.debugPassInfo.playerAddr3 != 0U)
                                            PlayerUtils.SetCtrl(this.watcher, this.debugPassInfo.playerAddr3, true);
                                        if (this.debugPassInfo.playerAddr4 != 0U)
                                            PlayerUtils.SetCtrl(this.watcher, this.debugPassInfo.playerAddr4, true);
                                    }
                                }
                            }
                            if (roundState2 == 2)
                            {
                                this._invokeWaitTime = 20;
                                this.debugPassInfo.num11 = 0L;
                                this.debugPassInfo.num12 = 0L;
                                this.debugPassInfo.num14 = 0L;
                                this.debugPassInfo.num15 = 0L;
                                this.debugPassInfo.num16 = 0L;
                                if (this.debugPassInfo.num13 == 0L)
                                {
                                    now = DateTime.Now;
                                    this.debugPassInfo.num13 = now.Ticks / 10000000L;
                                    if (logManager != null)
                                        AsyncAppendLog("Round " + (object)GameUtils.GetRoundNo(this.watcher) + " start!");
                                }
                                MugenProfile profile = ProfileManager.MainObj().GetProfile(this._workingProfileId);
                                if (profile != null && profile.IsAutoMode())
                                {
                                    if (GameUtils.GetRoundTime(this.watcher) <= 30 && GameUtils.IsSpeedMode(this.watcher))
                                        GameUtils.SetSpeedMode(this.watcher, this.debugPassInfo.baseAddr, false);
                                    long maxRoundTime = (long)profile.GetMaxRoundTime();
                                    now = DateTime.Now;
                                    long num20 = now.Ticks / 10000000L;
                                    if (maxRoundTime > 0L && num20 > this.debugPassInfo.num13 + maxRoundTime)
                                    {
                                        this.debugPassInfo.flag7 = false;
                                        this.debugPassInfo.flag6 = false;
                                        this.debugPassInfo.flag8 = true;
                                        int num21 = GameUtils.GetDrawGameCount(this.watcher) + 1;
                                        GameUtils.SetDrawGameCount(this.watcher, num21);
                                        this.watcher.SetInt32Data(this.debugPassInfo.baseAddr, this.watcher.MugenDatabase.ROUND_STATE_BASE_OFFSET, 3);
                                        int roundNo = GameUtils.GetRoundNo(this.watcher);
                                        GameUtils.SetRoundNo(this.watcher, roundNo + 1);
                                        this.InjectF4();
                                        if (logManager != null)
                                        {
                                            AsyncAppendLog(profile.GetMaxRoundTimeRawData().ToString() + "Minutes have elapsed.");
                                            AsyncAppendLog("⇒ Draw.");
                                        }
                                    }
                                }
                            }
                            if (roundState2 == 3)
                            {
                                this._invokeWaitTime = 10;
                                this.debugPassInfo.num11 = 0L;
                                this.debugPassInfo.num12 = 0L;
                                this.debugPassInfo.num13 = 0L;
                                this.debugPassInfo.num15 = 0L;
                                this.debugPassInfo.num16 = 0L;
                                if (this.debugPassInfo.num14 == 0L)
                                {
                                    now = DateTime.Now;
                                    this.debugPassInfo.num14 = now.Ticks / 10000000L;
                                }
                                MugenProfile profile = ProfileManager.MainObj().GetProfile(this._workingProfileId);
                                if (profile != null && profile.IsAutoMode())
                                {
                                    long maxRoundState4Time = (long)profile.GetMaxRoundState4Time();
                                    now = DateTime.Now;
                                    long num20 = now.Ticks / 10000000L;
                                    if (maxRoundState4Time > 0L && num20 > this.debugPassInfo.num14 + maxRoundState4Time)
                                    {
                                        if (logManager != null)
                                            AsyncAppendLog("This round was suspended in order to begin the next one.");
                                        this.debugPassInfo.flag7 = false;
                                        this.debugPassInfo.flag6 = false;
                                        this.debugPassInfo.flag8 = true;
                                        int num21 = GameUtils.GetDrawGameCount(this.watcher) + 1;
                                        GameUtils.SetDrawGameCount(this.watcher, num21);
                                        int roundNo = GameUtils.GetRoundNo(this.watcher);
                                        GameUtils.SetRoundNo(this.watcher, roundNo + 1);
                                        this.InjectF4();
                                        if (logManager != null)
                                            AsyncAppendLog("⇒ Draw.");
                                    }
                                }
                            }
                            if (roundState2 == 4)
                            {
                                this._invokeWaitTime = 10;
                                this.debugPassInfo.num11 = 0L;
                                this.debugPassInfo.num12 = 0L;
                                this.debugPassInfo.num13 = 0L;
                                this.debugPassInfo.num14 = 0L;
                                this.debugPassInfo.num16 = 0L;
                                if (this.debugPassInfo.num15 == 0L)
                                {
                                    now = DateTime.Now;
                                    this.debugPassInfo.num15 = now.Ticks / 10000000L;
                                }
                                MugenProfile profile = ProfileManager.MainObj().GetProfile(this._workingProfileId);
                                if (profile != null && profile.IsAutoMode())
                                {
                                    long maxRoundState4Time = (long)profile.GetMaxRoundState4Time();
                                    now = DateTime.Now;
                                    long num20 = now.Ticks / 10000000L;
                                    if (maxRoundState4Time > 0L && num20 > this.debugPassInfo.num15 + maxRoundState4Time)
                                    {
                                        GameUtils.GetRoundTime(this.watcher);
                                        if (logManager != null)
                                            AsyncAppendLog("This round was suspended in order to begin the next one.");
                                        if (!this.debugPassInfo.flag6)
                                        {
                                            int roundNo = GameUtils.GetRoundNo(this.watcher);
                                            GameUtils.SetRoundNo(this.watcher, roundNo + 1);
                                            this.InjectF4();
                                        }
                                        else
                                        {
                                            this.debugPassInfo.flag7 = false;
                                            this.debugPassInfo.flag6 = false;
                                            this.debugPassInfo.flag8 = true;
                                            int num21 = GameUtils.GetDrawGameCount(this.watcher) + 1;
                                            GameUtils.SetDrawGameCount(this.watcher, num21);
                                            int roundNo = GameUtils.GetRoundNo(this.watcher);
                                            GameUtils.SetRoundNo(this.watcher, roundNo + 1);
                                            this.InjectF4();
                                            if (logManager != null)
                                                AsyncAppendLog("⇒ Draw.");
                                        }
                                    }
                                }
                            }
                            // win determination segment
                            if (roundState2 >= 2 & this.debugPassInfo.flag6 | this.debugPassInfo.flag8)
                            {
                                if (roundState2 >= 3)
                                {
                                    int num20 = GameUtils.IsSpeedMode(this.watcher) ? 1 : 0;
                                    bool flag9 = GameUtils.IsSkipMode(this.watcher);
                                    if (num20 != 0)
                                        GameUtils.SetSpeedMode(this.watcher, this.debugPassInfo.baseAddr, false);
                                    if (flag9)
                                        GameUtils.SetSkipMode(this.watcher, this.debugPassInfo.baseAddr, false, this._skipModeFrames);
                                }
                                this.debugPassInfo.num11 = 0L;
                                this.debugPassInfo.num12 = 0L;
                                int team1WinCount = GameUtils.GetTeam1WinCount(this.watcher);
                                int team2WinCount = GameUtils.GetTeam2WinCount(this.watcher);
                                int newDrawGameCount = GameUtils.GetDrawGameCount(this.watcher);
                                if (!this.debugPassInfo.flag8)
                                {
                                    if (team1WinCount > this.debugPassInfo.team1Wins && team2WinCount == this.debugPassInfo.team2Wins)
                                    {
                                        this.debugPassInfo.flag7 = true;
                                        this.debugPassInfo.flag6 = false;
                                        ++this.debugPassInfo.trueTeam1WinCount;
                                        if (GameUtils.IsTeam1WinKO(this.watcher))
                                            ++this.debugPassInfo.num3;
                                        if (logManager != null)
                                        {
                                            if (GameUtils.IsTeam1WinKO(this.watcher))
                                                AsyncAppendLog("⇒ Player 1 side wins by K.O.");
                                            else
                                                AsyncAppendLog("⇒  Player 1 side wins by judgement.");
                                        }
                                    }
                                    else if (team2WinCount > this.debugPassInfo.team2Wins && team1WinCount == this.debugPassInfo.team1Wins)
                                    {
                                        this.debugPassInfo.flag7 = true;
                                        this.debugPassInfo.flag6 = false;
                                        ++this.debugPassInfo.trueTeam2WinCount;
                                        if (GameUtils.IsTeam2WinKO(this.watcher))
                                            ++this.debugPassInfo.num4;
                                        if (logManager != null)
                                        {
                                            if (GameUtils.IsTeam2WinKO(this.watcher))
                                                AsyncAppendLog("⇒ Player 2 side wins by K.O.");
                                            else
                                                AsyncAppendLog("⇒ Player 2 side wins by judgement.");
                                        }
                                    }
                                    else if (newDrawGameCount > this.debugPassInfo.drawRounds)
                                    {
                                        this.debugPassInfo.flag7 = true;
                                        this.debugPassInfo.flag6 = false;
                                        if (logManager != null && this.watcher.GetMugenProcess() != null)
                                            AsyncAppendLog("⇒ Draw.");
                                    }
                                }
                                if (!this.debugPassInfo.flag6 | this.debugPassInfo.flag8)
                                {
                                    this.debugPassInfo.flag8 = false;
                                    MugenProfile profile = ProfileManager.MainObj().GetProfile(this._workingProfileId);
                                    if (!GameUtils.IsTurnsMode(this.watcher) || profile != null && profile.IsAutoMode() && profile.IsStrictRoundMode())
                                    {
                                        if (team1WinCount >= this.debugPassInfo.num18 && team2WinCount < this.debugPassInfo.num18 || profile != null && profile.IsAutoMode() && (profile.IsStrictRoundMode() && this.debugPassInfo.trueTeam1WinCount + newDrawGameCount >= this.debugPassInfo.num18) && this.debugPassInfo.trueTeam2WinCount + newDrawGameCount < this.debugPassInfo.num18 || profile != null && profile.IsAutoMode() && (profile.IsStrictRoundMode() && this.debugPassInfo.trueTeam1WinCount > this.debugPassInfo.trueTeam2WinCount) && this.debugPassInfo.trueTeam1WinCount + this.debugPassInfo.trueTeam2WinCount + newDrawGameCount >= this.debugPassInfo.num18)
                                        {
                                            this.debugPassInfo.flag7 = false;
                                            this.BeginInvoke((Action)(() => DebugForm.MainObj().FinalizeTriggerCheck(this.watcher.MugenVersion)));
                                            if (this.watcher.GetMugenProcess() != null)
                                                AsyncAppendLog(">>> " + (object)this.debugPassInfo.trueTeam1WinCount + "Wins" + (object)this.debugPassInfo.trueTeam2WinCount + "Losses" + (object)newDrawGameCount + "Draws. Resulting in victory for the player 1 side.");
                                            if (profile != null && !profile.IsQuickMode())
                                            {
                                                this.UpdatePlayerDataEx(this.debugPassInfo.displayName2, this.debugPassInfo.palnoA1, this.debugPassInfo.displayName4, this.debugPassInfo.palnoB1, 1, 0, 0, 0, 0, this.debugPassInfo.trueTeam1WinCount, this.debugPassInfo.num3, this.debugPassInfo.trueTeam2WinCount, this.debugPassInfo.num4, newDrawGameCount, 0, 0);
                                                this.UpdatePlayerDataEx(this.debugPassInfo.displayName3, this.debugPassInfo.palnoA2, this.debugPassInfo.displayName5, this.debugPassInfo.palnoB2, 0, 1, 0, 0, 0, this.debugPassInfo.trueTeam2WinCount, this.debugPassInfo.num4, this.debugPassInfo.trueTeam1WinCount, this.debugPassInfo.num3, newDrawGameCount, 0, 0);
                                            }
                                            if (profile != null && profile.IsAutoMode())
                                            {
                                                profile.IncrementTempGameCount();
                                                if (this.debugPassInfo.num16 == 0L)
                                                {
                                                    now = DateTime.Now;
                                                    this.debugPassInfo.num16 = now.Ticks / 10000000L;
                                                }
                                                if (team1WinCount < this.debugPassInfo.num18)
                                                    GameUtils.SetTeam1WinCount(this.watcher, 100);
                                                if (this.watcher.GetMugenProcess() != null && !profile.WasLastFight())
                                                    AsyncAppendLog("Starting next match");
                                            }
                                        }
                                        else if (team1WinCount < this.debugPassInfo.num18 && team2WinCount >= this.debugPassInfo.num18 || profile != null && profile.IsAutoMode() && (profile.IsStrictRoundMode() && this.debugPassInfo.trueTeam1WinCount + newDrawGameCount < this.debugPassInfo.num18) && this.debugPassInfo.trueTeam2WinCount + newDrawGameCount >= this.debugPassInfo.num18 || profile != null && profile.IsAutoMode() && (profile.IsStrictRoundMode() && this.debugPassInfo.trueTeam1WinCount < this.debugPassInfo.trueTeam2WinCount) && this.debugPassInfo.trueTeam1WinCount + this.debugPassInfo.trueTeam2WinCount + newDrawGameCount >= this.debugPassInfo.num18)
                                        {
                                            this.debugPassInfo.flag7 = false;
                                            this.BeginInvoke((Action)(() => DebugForm.MainObj().FinalizeTriggerCheck(this.watcher.MugenVersion)));
                                            if (this.watcher.GetMugenProcess() != null && !profile.WasLastFight())
                                                AsyncAppendLog(">>> " + (object)this.debugPassInfo.trueTeam2WinCount + "Wins" + (object)this.debugPassInfo.trueTeam1WinCount + "Losses" + (object)newDrawGameCount + "Draws. Resulting in victory for the player 2 side.");
                                            if (profile != null && !profile.IsQuickMode())
                                            {
                                                this.UpdatePlayerDataEx(this.debugPassInfo.displayName2, this.debugPassInfo.palnoA1, this.debugPassInfo.displayName4, this.debugPassInfo.palnoB1, 0, 1, 0, 0, 0, this.debugPassInfo.trueTeam1WinCount, this.debugPassInfo.num3, this.debugPassInfo.trueTeam2WinCount, this.debugPassInfo.num4, newDrawGameCount, 0, 0);
                                                this.UpdatePlayerDataEx(this.debugPassInfo.displayName3, this.debugPassInfo.palnoA2, this.debugPassInfo.displayName5, this.debugPassInfo.palnoB2, 1, 0, 0, 0, 0, this.debugPassInfo.trueTeam2WinCount, this.debugPassInfo.num4, this.debugPassInfo.trueTeam1WinCount, this.debugPassInfo.num3, newDrawGameCount, 0, 0);
                                            }
                                            if (profile != null && profile.IsAutoMode())
                                            {
                                                profile.IncrementTempGameCount();
                                                if (this.debugPassInfo.num16 == 0L)
                                                {
                                                    now = DateTime.Now;
                                                    this.debugPassInfo.num16 = now.Ticks / 10000000L;
                                                }
                                                if (team2WinCount < this.debugPassInfo.num18)
                                                    GameUtils.SetTeam2WinCount(this.watcher, 100);
                                                if (this.watcher.GetMugenProcess() != null && !profile.WasLastFight())
                                                    AsyncAppendLog("Starting next match");
                                            }
                                        }
                                        else if (team1WinCount >= this.debugPassInfo.num18 && team2WinCount >= this.debugPassInfo.num18 || profile != null && profile.IsAutoMode() && (profile.IsStrictRoundMode() && this.debugPassInfo.trueTeam1WinCount + newDrawGameCount >= this.debugPassInfo.num18) && this.debugPassInfo.trueTeam2WinCount + newDrawGameCount >= this.debugPassInfo.num18 || profile != null && profile.IsAutoMode() && (profile.IsStrictRoundMode() && this.debugPassInfo.trueTeam1WinCount == this.debugPassInfo.trueTeam2WinCount) && this.debugPassInfo.trueTeam1WinCount + this.debugPassInfo.trueTeam2WinCount + newDrawGameCount >= this.debugPassInfo.num18)
                                        {
                                            this.debugPassInfo.flag7 = false;
                                            this.BeginInvoke((Action)(() => DebugForm.MainObj().FinalizeTriggerCheck(this.watcher.MugenVersion)));
                                            if (this.watcher.GetMugenProcess() != null)
                                                AsyncAppendLog(">>> " + (object)this.debugPassInfo.trueTeam1WinCount + "Wins" + (object)this.debugPassInfo.trueTeam2WinCount + "Losses" + (object)newDrawGameCount + "Draws. Resulting in an overall draw.");
                                            if (profile != null && !profile.IsQuickMode())
                                            {
                                                this.UpdatePlayerDataEx(this.debugPassInfo.displayName2, this.debugPassInfo.palnoA1, this.debugPassInfo.displayName4, this.debugPassInfo.palnoB1, 0, 0, 1, 0, 0, this.debugPassInfo.trueTeam1WinCount, this.debugPassInfo.num3, this.debugPassInfo.trueTeam2WinCount, this.debugPassInfo.num4, newDrawGameCount, 0, 0);
                                                this.UpdatePlayerDataEx(this.debugPassInfo.displayName3, this.debugPassInfo.palnoA2, this.debugPassInfo.displayName5, this.debugPassInfo.palnoB2, 0, 0, 1, 0, 0, this.debugPassInfo.trueTeam2WinCount, this.debugPassInfo.num4, this.debugPassInfo.trueTeam1WinCount, this.debugPassInfo.num3, newDrawGameCount, 0, 0);
                                            }
                                            if (profile != null && profile.IsAutoMode())
                                            {
                                                profile.IncrementTempGameCount();
                                                if (this.debugPassInfo.num16 == 0L)
                                                {
                                                    now = DateTime.Now;
                                                    this.debugPassInfo.num16 = now.Ticks / 10000000L;
                                                }
                                                if (team1WinCount < this.debugPassInfo.num18 || team2WinCount < this.debugPassInfo.num18)
                                                {
                                                    GameUtils.SetTeam1WinCount(this.watcher, 100);
                                                    GameUtils.SetTeam2WinCount(this.watcher, 100);
                                                }
                                                if (this.watcher.GetMugenProcess() != null && !profile.WasLastFight())
                                                    AsyncAppendLog("Starting next match");
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            else
                return false;
            return true;
        }

        public void Cleanup()
        {
            LogManager logManager = LogManager.MainObj();
            DebugForm debugForm = DebugForm.MainObj();
            if (this.debugPassInfo.flag6 | this.debugPassInfo.flag7)
            {
                this.BeginInvoke((Action)(() => DebugForm.MainObj().FinalizeTriggerCheck(this.watcher.MugenVersion)));
                this._invokeWaitTime = 20;
                MugenProfile profile = ProfileManager.MainObj().GetProfile(this._workingProfileId);
                if (this.debugPassInfo.baseAddr != 0U && !GameUtils.IsTurnsMode(this.watcher) || profile != null && profile.IsAutoMode())
                {
                    if (this._isMugenFrozen)
                    {
                        this._isGameQuitted = true;
                        if (logManager != null)
                            AsyncAppendLog("Process quit unexpectedly");
                        if (profile != null && !profile.IsQuickMode())
                        {
                            this.UpdatePlayerDataEx(this.debugPassInfo.displayName2, this.debugPassInfo.palnoA1, this.debugPassInfo.displayName4, this.debugPassInfo.palnoB1, 0, 0, 0, 1, 0, this.debugPassInfo.trueTeam1WinCount, this.debugPassInfo.num3, this.debugPassInfo.trueTeam2WinCount, this.debugPassInfo.num4, this.debugPassInfo.drawRounds, 1, 0);
                            this.UpdatePlayerDataEx(this.debugPassInfo.displayName3, this.debugPassInfo.palnoA2, this.debugPassInfo.displayName5, this.debugPassInfo.palnoB2, 0, 0, 0, 1, 0, this.debugPassInfo.trueTeam2WinCount, this.debugPassInfo.num4, this.debugPassInfo.trueTeam1WinCount, this.debugPassInfo.num3, this.debugPassInfo.drawRounds, 1, 0);
                        }
                    }
                    else
                    {
                        this._isGameQuitted = true;
                        if (logManager != null)
                            AsyncAppendLog("The match was cancelled");
                        if (profile != null && !profile.IsQuickMode())
                        {
                            this.UpdatePlayerDataEx(this.debugPassInfo.displayName2, this.debugPassInfo.palnoA1, this.debugPassInfo.displayName4, this.debugPassInfo.palnoB1, 0, 0, 0, 0, 1, this.debugPassInfo.trueTeam1WinCount, this.debugPassInfo.num3, this.debugPassInfo.trueTeam2WinCount, this.debugPassInfo.num4, this.debugPassInfo.drawRounds, 0, 1);
                            this.UpdatePlayerDataEx(this.debugPassInfo.displayName3, this.debugPassInfo.palnoA2, this.debugPassInfo.displayName5, this.debugPassInfo.palnoB2, 0, 0, 0, 0, 1, this.debugPassInfo.trueTeam2WinCount, this.debugPassInfo.num4, this.debugPassInfo.trueTeam1WinCount, this.debugPassInfo.num3, this.debugPassInfo.drawRounds, 0, 1);
                        }
                    }
                    if (profile != null && profile.IsAutoMode() && this.watcher.CheckMugenProcessActive())
                        this.watcher.GetMugenProcess().Kill();
                }
            }
        }

        public void Uninitialize()
        {
            this.BeginInvoke((Action)(() => DebugForm.MainObj().PostFinalizeTriggerCheck(this.watcher.MugenVersion)));
        }

        [StructLayout(LayoutKind.Sequential, Pack = 4)]
        private struct RECT
        {
            public int left;
            public int top;
            public int right;
            public int bottom;
        }

        private delegate bool EnumChildProc(IntPtr hWnd, int lParam);

        private delegate void appendLogDelegate();

        private delegate void uiDelegate();

        /// <summary>
        /// modes to display the debug form in
        /// </summary>
        public enum DebugListMode
        {
            NONE,
            PLAYER_LIST_MODE,
            EXPLOD_LIST_MODE,
            PROJ_LIST_MODE,
        }

        /// <summary>
        /// possible owners of a proj (since Helpers cannot own)
        /// </summary>
        public enum ProjOwner
        {
            P1,
            P2,
            P3,
            P4,
        }

        /// <summary>
        /// Enum representing different options for Debug text colors.
        /// </summary>
        public enum DebugColor
        {
            NONE = -1,
            WHITE,
            YELLOW,
            PURPLE,
            RED,
            BLACK,
            GREEN,
            CUSTOM
        }
    }
}
