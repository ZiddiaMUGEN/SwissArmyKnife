// Decompiled with JetBrains decompiler
// Type: SwissArmyKnifeForMugen.MugenWindow
// Assembly: SAKnifeWM, Version=1.0.8.0, Culture=neutral, PublicKeyToken=null
// MVID: 09478AD8-365C-4BF3-BEA1-B5785151259B
// Assembly location: C:\Users\ziddi\Downloads\Swiss Army Knife 1.1 Conversion\Swiss Army Knife 1.1 Conversion\SAKnifeWM.exe

using Microsoft.Samples.Debugging.Native;
using SwissArmyKnifeForMugen.Configs;
using SwissArmyKnifeForMugen.Databases;
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
using System.Threading;
using System.Windows.Forms;
using static SwissArmyKnifeForMugen.Triggers.TriggerDatabase;

namespace SwissArmyKnifeForMugen
{
    /// <summary>
    /// heart of the application. most of the monitoring/handling code is in here.
    /// </summary>
    public class MugenWindow : Form
    {
        // tracks Mugen type for current profile
        private MugenWindow.MugenType_t _mugen_type = MugenWindow.MugenType_t.MUGEN_TYPE_WINMUGEN;
        private NativePipeline debugControl = new NativePipeline();
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
        private VarForm.SysInfo_t mugenSysInfo = new VarForm.SysInfo_t();
        private int[] mugenSysvar = new int[5];
        private int[] mugenVar = new int[60];
        private float[] mugenSysfvar = new float[5];
        private float[] mugenFvar = new float[40];
        // for DebugColor.CUSTOM, these are the custom RGB values.
        private int[] customDebugColors = new int[] { 256, 256, 256 };
        private const int GWL_STYLE = -16;
        private const uint WS_CAPTION = 12582912;
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
        // current address database
        private MugenAddrDatabase _addr_db;
        private Process p;
        // debugging Mugen process (used with trigger breakpoints)
        private NativeDbgProcess debugP;
        private int debugTargetThread;
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
        private int _timeOverCount1;
        private int _timeOverCount2;
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
        private BackgroundWorker mugenWatcher;
        private ToolTip toolTip1;
        private System.Windows.Forms.Timer activateTimer;
        private uint watchAddr;
        internal uint WatchInitVal { get; set; }

        private MugenWindow()
        {
            this.InitializeComponent();
            this.ClientSize = new Size(640, 480);
            if (!File.Exists(MainForm.GetFullPath("background.jpg")))
                return;
            this.backgroundBox.ImageLocation = MainForm.GetFullPath("background.jpg");
        }

        /// <summary>
        /// currently-selected profile's Mugen version
        /// </summary>
        /// <returns></returns>
        public MugenWindow.MugenType_t getMugenType() => this._mugen_type;

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
            MugenWindow.SetWindowPos(hWnd, 0, 0, 0, 640, 480, 16387);
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
            return this.debugP == null || this._triggerCheckTarget == null || this._triggerCheckTarget.GetCurrentMode() != TriggerCheckTarget.CheckMode.CHECKMODE_STARTED;
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

        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool ReadProcessMemory(
          IntPtr hProcess,
          IntPtr lpBaseAddress,
          byte[] lpBuffer,
          UIntPtr nSize,
          out int lpNumberOfBytesRead);

        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern bool WriteProcessMemory(
          IntPtr hProcess,
          IntPtr lpBaseAddress,
          byte[] lpBuffer,
          uint nSize,
          out UIntPtr lpNumberOfBytesWritten);

        [DllImport("kernel32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool GetThreadContext(IntPtr hThread, ref MugenWindow.CONTEXT lpContext);

        [DllImport("kernel32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool Wow64GetThreadContext(
          IntPtr hThread,
          ref MugenWindow.CONTEXT lpContext);

        public static bool GetThreadContextEx(IntPtr hThread, ref MugenWindow.CONTEXT context) => IntPtr.Size == 4 ? MugenWindow.GetThreadContext(hThread, ref context) : MugenWindow.Wow64GetThreadContext(hThread, ref context);

        [DllImport("kernel32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool SetThreadContext(IntPtr hThread, ref MugenWindow.CONTEXT lpContext);

        [DllImport("kernel32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool Wow64SetThreadContext(
          IntPtr hThread,
          ref MugenWindow.CONTEXT lpContext);

        public static bool SetThreadContextEx(IntPtr hThread, ref MugenWindow.CONTEXT context) => IntPtr.Size == 4 ? MugenWindow.SetThreadContext(hThread, ref context) : MugenWindow.Wow64SetThreadContext(hThread, ref context);

        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern bool GetThreadSelectorEntry(
          IntPtr hThread,
          uint dwSelector,
          ref MugenWindow.LDT_ENTRY lpSelectorEntry);

        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern bool Wow64GetThreadSelectorEntry(
          IntPtr hThread,
          uint dwSelector,
          ref MugenWindow.LDT_ENTRY lpSelectorEntry);

        private static bool GetThreadSelectorEntryEx(
          IntPtr hThread,
          uint dwSelector,
          ref MugenWindow.LDT_ENTRY lpSelectorEntry)
        {
            return IntPtr.Size == 4 ? MugenWindow.GetThreadSelectorEntry(hThread, dwSelector, ref lpSelectorEntry) : MugenWindow.Wow64GetThreadSelectorEntry(hThread, dwSelector, ref lpSelectorEntry);
        }

        [DllImport("kernel32.dll")]
        public static extern IntPtr OpenThread(
          MugenWindow.ThreadAccessFlags dwDesiredAccess,
          [MarshalAs(UnmanagedType.Bool)] bool bInheritHandle,
          uint dwThreadId);

        [DllImport("kernel32.dll")]
        public static extern int ResumeThread(IntPtr hThread);

        public static int ResumeThreadEx(IntPtr hThread) => MugenWindow.ResumeThread(hThread);

        [DllImport("kernel32.dll")]
        public static extern int SuspendThread(IntPtr hThread);

        [DllImport("kernel32.dll")]
        public static extern int Wow64SuspendThread(IntPtr hThread);

        public static int SuspendThreadEx(IntPtr hThread) => IntPtr.Size == 4 ? MugenWindow.SuspendThread(hThread) : MugenWindow.Wow64SuspendThread(hThread);

        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool CloseHandle(IntPtr handle);

        public static MugenWindow MainObj()
        {
            if (MugenWindow.selfObj == null)
                MugenWindow.selfObj = new MugenWindow();
            return MugenWindow.selfObj;
        }

        public int GetWorkingProfileId() => this._workingProfileId;

        public Process getMugenProcess() => this.p == null || !this.p.HasExited ? this.p : (Process)null;

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
            if (this._isDebugBreakMode || this._isMugenCrashed || this._isMugenFrozen || this.debugP != null && this._triggerCheckTarget != null && this._triggerCheckTarget.GetCurrentMode() == TriggerCheckTarget.CheckMode.CHECKMODE_STARTED)
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
            while (this.mugenWatcher.IsBusy)
            {
                this.mugenWatcher.CancelAsync();
                if (this.p != null)
                {
                    if (!this.p.HasExited)
                        this.p.Kill();
                    this.p.WaitForExit(2000);
                    this.p.Close();
                    this.p.Dispose();
                    this.p = (Process)null;
                }
                Application.DoEvents();
            }
            // doublecheck the old process has died
            if (this.p == null || this.p.HasExited)
            {
                this._isMugenHidden = false;
                this._isActivated = false;
                this._isActivatedOnce = false;
                this._isShownOnce = false;
                // type is determined on launch
                this._mugen_type = MugenWindow.MugenType_t.MUGEN_TYPE_NONE;
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
                this._mugen_type = MugenWindow.MugenType_t.MUGEN_TYPE_WINMUGEN;
                FileVersionInfo versionInfo = FileVersionInfo.GetVersionInfo(profile.GetMugenExePath());
                if (versionInfo != null)
                {
                    if (string.Compare("M.U.G.E.N", versionInfo.ProductName, true) == 0)
                    {
                        if (versionInfo.FileMajorPart == 1 && versionInfo.FileMinorPart == 0)
                            this._mugen_type = MugenWindow.MugenType_t.MUGEN_TYPE_MUGEN10;
                        else if (versionInfo.FileMajorPart == 1 && versionInfo.FileMinorPart == 1 && versionInfo.FileVersion == "1.1.0 Alpha 4")
                            this._mugen_type = MugenWindow.MugenType_t.MUGEN_TYPE_MUGEN11A4;
                        else if (versionInfo.FileMajorPart == 1 && versionInfo.FileMinorPart == 1 && versionInfo.FileVersion == "1.1.0 Beta 1 P1")
                        {
                            this._mugen_type = MugenWindow.MugenType_t.MUGEN_TYPE_MUGEN11B1;
                        }
                        else
                        {
                            this._mugen_type = MugenWindow.MugenType_t.MUGEN_TYPE_NONE;
                            if (LogManager.MainObj() != null)
                                AsyncAppendLog("Incompatible MUGEN version.");
                            return false;
                        }
                    }
                    else if (versionInfo.FileVersion != null)
                    {
                        this._mugen_type = MugenWindow.MugenType_t.MUGEN_TYPE_NONE;
                        if (LogManager.MainObj() != null)
                            AsyncAppendLog("The specified exe file is not mugen.");
                        return false;
                    }
                }
                // setup to launch a debugging process
                startInfo.FileName = Path.GetFileName(profile.GetMugenExePath());
                startInfo.Arguments = profile.GetMugenCommandLineOptions() == null ? "" : profile.GetMugenCommandLineOptions();
                startInfo.WorkingDirectory = profile.GetMugenPath();
                startInfo.CreateNoWindow = false;
                startInfo.UseShellExecute = false;
                startInfo.WindowStyle = ProcessWindowStyle.Minimized;
                string currentDirectory = Environment.CurrentDirectory;
                Environment.CurrentDirectory = startInfo.WorkingDirectory;
                try
                {
                    // launch the base process
                    this.p = Process.Start(startInfo);
                    Environment.CurrentDirectory = currentDirectory;
                }
                catch
                {
                    this._mugen_type = MugenWindow.MugenType_t.MUGEN_TYPE_NONE;
                    Environment.CurrentDirectory = currentDirectory;
                    if (LogManager.MainObj() != null)
                        AsyncAppendLog("Failed to load the exe file.");
                    return false;
                }
            }
            // confirm we launched successfully
            if (this.p != null && !this.p.HasExited)
            {
                this.p.WaitForInputIdle(5000);
                int num1 = 200;
                // waiting for the process to launch the Mugen window
                while (num1-- >= 0 && this.p != null && !this.p.HasExited && this.p.MainWindowHandle.Equals((object)IntPtr.Zero))
                {
                    Thread.Sleep(50);
                    this.p.Refresh();
                    Application.DoEvents();
                }
                // failsafe, may have crashed pre-load
                if (this.p == null || this.p.HasExited)
                {
                    this._mugen_type = MugenWindow.MugenType_t.MUGEN_TYPE_NONE;
                    if (this.p != null)
                        this.p.Dispose();
                    this.p = (Process)null;
                    if (LogManager.MainObj() != null)
                        AsyncAppendLog("Failed to load the exe file.");
                    return false;
                }
                if (this.p.MainWindowHandle.Equals((object)IntPtr.Zero))
                {
                    if (LogManager.MainObj() != null)
                        AsyncAppendLog("Failed to load the exe file.");
                    this.CloseMugen();
                    return false;
                }
                StringBuilder lpString = new StringBuilder(4132);
                // doublecheck the window looks good
                if (!this.p.MainWindowHandle.Equals((object)IntPtr.Zero) && MugenWindow.GetWindowText(this.p.MainWindowHandle, lpString, lpString.Capacity) != 0)
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
                uint unValue = (uint)((int)MugenWindow.GetWindowLong(this.p.MainWindowHandle, -16) & -12582913 & int.MaxValue);
                if (this._mugen_type == MugenWindow.MugenType_t.MUGEN_TYPE_MUGEN10 || this._mugen_type == MugenWindow.MugenType_t.MUGEN_TYPE_MUGEN11A4)
                {
                    int num2 = (int)MugenWindow.SetParent(this.p.MainWindowHandle, this.backgroundBox.Handle);
                    int num3 = (int)MugenWindow.SetWindowLong(this.p.MainWindowHandle, -16, unValue);
                    MugenWindow.SetWindowPos(this.p.MainWindowHandle, 0, 0, 0, 640, 480, 68);
                    int num4 = (int)MugenWindow.SetParent(this.p.MainWindowHandle, this.backgroundBox.Handle);
                }
                else
                {
                    int num2 = (int)MugenWindow.SetWindowLong(this.p.MainWindowHandle, -16, unValue);
                    int num3 = (int)MugenWindow.SetParent(this.p.MainWindowHandle, this.backgroundBox.Handle);
                    MugenWindow.SetWindowPos(this.p.MainWindowHandle, 0, 0, 0, 640, 480, 68);
                }
                // set predetermined flags based on profile setup
                MugenProfile profile = ProfileManager.MainObj().GetProfile(profileNo);
                if (profile != null)
                {
                    DebugForm.MainObj().SetSpeedModeCheckBox(profile.IsSpeedMode(), true);
                    DebugForm.MainObj().SetSkipModeCheckBox(profile.IsSkipMode());
                    DebugForm.MainObj().SetDebugModeCheckBox(profile.IsDebugMode(), true);
                    DebugForm.MainObj().PreInitTriggerCheck(this._mugen_type); // in case of saved triggers
                    this._workingProfileId = profile.GetProfileNo();
                }
                this._isMugenFrozen = false;
                this._isGameQuitted = false;
                this._isMugenCrashed = false;
                this._addr_db = MugenAddrDatabase.GetAddrDatabase(this._mugen_type);
                // launch monitor
                if (!this.mugenWatcher.IsBusy)
                    this.mugenWatcher.RunWorkerAsync();
                return true;
            }
            // failure case
            this._mugen_type = MugenWindow.MugenType_t.MUGEN_TYPE_NONE;
            return false;
        }

        public void AttachMugen() => MugenWindow.AttachThreadInput(MugenWindow.GetWindowThreadProcessId(this.p.MainWindowHandle, IntPtr.Zero), MugenWindow.GetWindowThreadProcessId(this.Handle, IntPtr.Zero), true);

        public void DettachMugen() => MugenWindow.AttachThreadInput(MugenWindow.GetWindowThreadProcessId(this.p.MainWindowHandle, IntPtr.Zero), MugenWindow.GetWindowThreadProcessId(this.Handle, IntPtr.Zero), false);

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
            if (this.p == null)
                return;
            if (MugenWindow.IsWindowVisible(this.p.MainWindowHandle))
            {
                this._isMugenHidden = true;
                MugenWindow.SetWindowPos(this.p.MainWindowHandle, 0, 0, 0, 640, 480, 16532);
            }
            else
            {
                this._isMugenHidden = false;
                MugenWindow.SetWindowPos(this.p.MainWindowHandle, 0, 0, 0, 640, 480, 16532);
            }
        }

        public bool IsBusyMugen() => this.mugenWatcher != null && this.mugenWatcher.IsBusy;

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
            // stop trigger breakpoint freeze
            this.stopDebugBreak();
            if (this.p != null)
            {
                // kill the monitor process
                while (this.mugenWatcher.IsBusy)
                {
                    this.mugenWatcher.CancelAsync();
                    Application.DoEvents();
                }
                if (this.p != null)
                {
                    if (!this.p.HasExited)
                    {
                        if (!this._isDebugBreakMode)
                        {
                            // confirmation that the process is ready to be killed
                            if (this.p.Responding)
                            {
                                MugenWindow.SetForegroundWindow(this.p.MainWindowHandle);
                                this.p.CloseMainWindow();
                                if (this._mugen_type == MugenWindow.MugenType_t.MUGEN_TYPE_MUGEN10)
                                {
                                    this.p.WaitForExit(2000);
                                    if (!this.p.HasExited)
                                    {
                                        this.p.Kill();
                                        this.p.WaitForExit(2000);
                                    }
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
                                    while (hWnd == this.p.MainWindowHandle || lpdwProcessId != this.p.Id);
                                    if (hWnd != IntPtr.Zero && lpdwProcessId == this.p.Id)
                                    {
                                        MugenWindow.PostMessage(hWnd, 273U, (IntPtr)6, (IntPtr)1967936);
                                        this.p.WaitForExit(2000);
                                        if (!this.p.HasExited)
                                        {
                                            this.p.Kill();
                                            this.p.WaitForExit(2000);
                                        }
                                    }
                                    else if (!this.p.HasExited)
                                    {
                                        this.p.Kill();
                                        this.p.WaitForExit(2000);
                                    }
                                }
                            }
                            else if (!this.p.HasExited)
                            {
                                this.p.Kill();
                                this.p.WaitForExit(2000);
                            }
                        }
                        else if (!this.p.HasExited)
                        {
                            this.p.Kill();
                            this.p.WaitForExit(2000);
                        }
                    }
                    this.p.Close();
                    this.p.Dispose();
                    this.p = (Process)null;
                }
            }
            else
            {
                while (this.mugenWatcher.IsBusy)
                {
                    this.mugenWatcher.CancelAsync();
                    Application.DoEvents();
                }
            }
            this.SetTitleActive(false);
        }

        private void KillGracefully()
        {
            if (this.p == null || this.p.HasExited)
                return;
            if (this.p.Responding)
            {
                this.InjectESC();
                if (this.p.WaitForExit(2000))
                    return;
                this.p.Kill();
            }
            else
                this.p.Kill();
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
                return this.p == null || this.p.HasExited;
            if (this.p != null)
            {
                MugenProfile profile = ProfileManager.MainObj().GetProfile(this._workingProfileId);
                if (this.p.HasExited)
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
                            if (!mainWindowHandle.Equals((object)this.p.MainWindowHandle) && MugenWindow.GetWindowText(process.MainWindowHandle, lpString, lpString.Capacity) != 0)
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
                        if (this.p != null && !this.p.HasExited)
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
                       if (this.p != null && !this.p.HasExited && this.p.Id == lpdwProcessId)
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
            if (this.p == null || this.p.HasExited)
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
            if (this.p == null || this.p.HasExited)
                return;
            MugenWindow.SendMessage1(this.p.MainWindowHandle, Msg, (int)c, 0);
        }

        /// <summary>
        /// directly reads memory from the Mugen process in a safe manner, writes to a buffer, and returns the size.
        /// </summary>
        /// <param name="addr">address to read from</param>
        /// <param name="buf">buffer to write to</param>
        /// <param name="bufLen">length of the buffer</param>
        /// <returns></returns>
        public int ReadMemory(IntPtr addr, ref byte[] buf, uint bufLen)
        {
            if (this.p == null || this.p.HasExited)
                return 0;
            int lpNumberOfBytesRead = 0;
            try
            {
                if (!MugenWindow.ReadProcessMemory(this.p.Handle, addr, buf, (UIntPtr)bufLen, out lpNumberOfBytesRead))
                    return 0;
            }
            catch
            {
                return 0;
            }
            return lpNumberOfBytesRead;
        }

        /// <summary>
        /// writes values in a buffer to an address in memory
        /// </summary>
        /// <param name="addr"></param>
        /// <param name="buf"></param>
        /// <param name="bufLen"></param>
        public void WriteMemory(IntPtr addr, ref byte[] buf, uint bufLen)
        {
            if (this.p == null)
                return;
            if (this.p.HasExited)
                return;
            try
            {
                MugenWindow.WriteProcessMemory(this.p.Handle, addr, buf, bufLen, out UIntPtr _);
            }
            catch
            {
            }
        }

        private void MugenWindow_FormClosing(object sender, FormClosingEventArgs e) => this.CloseMugen();

        private void MugenWindow_FormClosed(object sender, FormClosedEventArgs e) => MugenWindow.selfObj = (MugenWindow)null;

        private void MugenWindow_Activated(object sender, EventArgs e)
        {
            if (this.p == null || this.p.HasExited)
                return;
            MugenWindow.SetWindowPos(this.p.MainWindowHandle, 0, 0, 0, 640, 480, 16400);
            if (!this._ignoreUnPauseRequestOnce)
                this.SetForegroundWindowEx(this.p.MainWindowHandle);
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
            Process mugenProcess = MugenWindow.MainObj().getMugenProcess();
            if (mugenProcess == null || mugenProcess.HasExited)
                return;
            if (MugenWindow.IsWindowVisible(mugenProcess.MainWindowHandle))
            {
                if (this._isShownOnce)
                    return;
                this._isShownOnce = true;
                if (this._mugen_type == MugenWindow.MugenType_t.MUGEN_TYPE_MUGEN10)
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
                MugenWindow.SetWindowPos(mugenProcess.MainWindowHandle, 0, 0, 0, 640, 480, 16384);
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
        /// fetch the base address of the Mugen game structure from [MUGEN_POINTER_BASE_OFFSET]
        /// </summary>
        /// <returns></returns>
        private uint GetBaseAddr()
        {
            uint num = 0;
            byte[] buf = new byte[4];
            if (MugenWindow.MainObj().ReadMemory((IntPtr)(long)this._addr_db.MUGEN_POINTER_BASE_OFFSET, ref buf, 4U) > 0)
                num = BitConverter.ToUInt32(buf, 0);
            return num;
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

        public bool IsPaused() => this._addr_db is Mugen11A4DB || this._addr_db is Mugen11B1DB ? (uint)this._GetInt32Data(this.GetBaseAddr(), this._addr_db.PAUSE_ADDR) > 0U : (uint)this._GetInt32Data(0U, this._addr_db.PAUSE_ADDR) > 0U;

        private bool IsDemo(uint baseAddr) => (uint)this._GetInt32Data(baseAddr, this._addr_db.DEMO_BASE_OFFSET) > 0U;

        private bool IsMugenActive(uint baseAddr) => (uint)this._GetInt32Data(baseAddr, this._addr_db.MUGEN_ACTIVE_BASE_OFFSET) > 0U;

        private bool IsSpeedMode(uint baseAddr) => (uint)this._GetInt32Data(baseAddr, this._addr_db.SPEED_MODE_BASE_OFFSET) > 0U;

        private bool IsSkipMode(uint baseAddr) => this._GetInt32Data(baseAddr, this._addr_db.SKIP_MODE_BASE_OFFSET) > 0;

        private int GetSkipFrames(uint baseAddr) => this._GetInt32Data(baseAddr, this._addr_db.SKIP_MODE_BASE_OFFSET);

        private bool IsDebugMode(uint baseAddr) => this._GetInt32Data(baseAddr, this._addr_db.DEBUG_MODE_BASE_OFFSET) != 0 && (uint)this._GetInt32Data(baseAddr, this._addr_db.DEBUG_TARGET_BASE_OFFSET) > 0U;

        private int GetDebugTargetNo(uint baseAddr) => this._GetInt32Data(baseAddr, this._addr_db.DEBUG_TARGET_BASE_OFFSET);

        private uint GetGametime(uint baseAddr) => (uint)this._GetInt32Data(baseAddr, this._addr_db.GAMETIME_BASE_OFFSET);

        private float GetScreenX(uint baseAddr) => (float)this._GetInt32Data(baseAddr, this._addr_db.SCREEN_X_BASE_OFFSET);

        private float GetScreenY(uint baseAddr) => (float)this._GetInt32Data(baseAddr, this._addr_db.SCREEN_Y_BASE_OFFSET);

        private int GetRoundState(uint baseAddr) => this._GetInt32Data(baseAddr, this._addr_db.ROUND_STATE_BASE_OFFSET);

        private void SetRoundState(uint baseAddr, int state) => this._SetInt32Data(baseAddr, this._addr_db.ROUND_STATE_BASE_OFFSET, state);

        private int GetRoundNo(uint baseAddr) => this._GetInt32Data(baseAddr, this._addr_db.ROUND_NO_BASE_OFFSET);

        private void SetRoundNo(uint baseAddr, int num) => this._SetInt32Data(baseAddr, this._addr_db.ROUND_NO_BASE_OFFSET, num);

        private int GetRoundTime(uint baseAddr) => this._GetInt32Data(baseAddr, this._addr_db.ROUND_TIME_BASE_OFFSET);

        private int GetRoundNoOfMatch(uint baseAddr) => this._GetInt32Data(baseAddr, this._addr_db.ROUND_NO_OF_MATCH_TIME_BASE_OFFSET);

        private bool IsTurnsMode(uint baseAddr) => this._GetInt32Data(baseAddr, this._addr_db.TURNS_MODE_BASE_OFFSET) == 0;

        private bool DoesExist(uint playerAddr) => (uint)this._GetInt32Data(playerAddr, this._addr_db.EXIST_PLAYER_OFFSET) > 0U;

        private bool IsTeam1Win(uint baseAddr) => this._GetInt32Data(baseAddr, this._addr_db.TEAM_WIN_BASE_OFFSET) == 1;

        private bool IsTeam1WinKO(uint baseAddr) => this._GetInt32Data(baseAddr, this._addr_db.TEAM_WIN_BASE_OFFSET) == 1 && this._GetInt32Data(baseAddr, this._addr_db.TEAM_WIN_KO_BASE_OFFSET) == 1;

        private bool IsTeam2Win(uint baseAddr) => this._GetInt32Data(baseAddr, this._addr_db.TEAM_WIN_BASE_OFFSET) == 2;

        private bool IsTeam2WinKO(uint baseAddr) => this._GetInt32Data(baseAddr, this._addr_db.TEAM_WIN_BASE_OFFSET) == 2 && this._GetInt32Data(baseAddr, this._addr_db.TEAM_WIN_KO_BASE_OFFSET) == 1;

        private int GetTeam1WinCount(uint baseAddr) => this._GetInt32Data(baseAddr, this._addr_db.TEAM_1_WIN_COUNT_BASE_OFFSET);

        private int GetTeam2WinCount(uint baseAddr) => this._GetInt32Data(baseAddr, this._addr_db.TEAM_2_WIN_COUNT_BASE_OFFSET);

        private int GetDrawGameCount(uint baseAddr) => this._GetInt32Data(baseAddr, this._addr_db.DRAW_GAME_COUNT_BASE_OFFSET);

        private void SetTeam1WinCount(uint baseAddr, int value) => this._SetInt32Data(baseAddr, this._addr_db.TEAM_1_WIN_COUNT_BASE_OFFSET, value);

        private void SetTeam2WinCount(uint baseAddr, int value) => this._SetInt32Data(baseAddr, this._addr_db.TEAM_2_WIN_COUNT_BASE_OFFSET, value);

        private void SetDrawGameCount(uint baseAddr, int value) => this._SetInt32Data(baseAddr, this._addr_db.DRAW_GAME_COUNT_BASE_OFFSET, value);

        /// <summary>
        /// gets a player address from the Mugen base addr and an offset.
        /// <br/>for p1, this would be [[MUGEN_POINTER_BASE_OFFSET] + PLAYER_1_BASE_OFFSET]
        /// <br/>for subsequent players/helpers, add 4 per
        /// </summary>
        /// <param name="baseAddr"></param>
        /// <param name="offset"></param>
        /// <returns></returns>
        private uint _GetPlayerAddr(uint baseAddr, uint offset)
        {
            uint num = 0;
            byte[] buf = new byte[4];
            if (MugenWindow.MainObj().ReadMemory((IntPtr)(long)(baseAddr + offset), ref buf, 4U) > 0)
                num = BitConverter.ToUInt32(buf, 0);
            return num;
        }

        private uint GetP1Addr(uint baseAddr) => this._GetPlayerAddr(baseAddr, this._addr_db.PLAYER_1_BASE_OFFSET);

        private uint GetP2Addr(uint baseAddr) => this._GetPlayerAddr(baseAddr, this._addr_db.PLAYER_1_BASE_OFFSET + 4U);

        private uint GetP3Addr(uint baseAddr) => this._GetPlayerAddr(baseAddr, this._addr_db.PLAYER_1_BASE_OFFSET + 8U);

        private uint GetP4Addr(uint baseAddr) => this._GetPlayerAddr(baseAddr, this._addr_db.PLAYER_1_BASE_OFFSET + 12U);

        /// <summary>
        /// gets a player info address from a player address
        /// <br/>this is exactly [playerAddr] (or, [[[MUGEN_POINTER_BASE_OFFSET] + PLAYER_1_BASE_OFFSET + index*4]])
        /// </summary>
        /// <param name="playerAddr"></param>
        /// <returns></returns>
        private uint GetPlayerInfoAdder(uint playerAddr)
        {
            uint num = 0;
            byte[] buf = new byte[4];
            if (playerAddr != 0U && MugenWindow.MainObj().ReadMemory((IntPtr)(long)playerAddr, ref buf, 4U) > 0)
                num = BitConverter.ToUInt32(buf, 0);
            return num;
        }

        /// <summary>
        /// fetch the global explod list (this is relative to MUGEN_POINTER_BASE_OFFSET)
        /// </summary>
        /// <param name="baseAddr"></param>
        /// <returns></returns>
        private uint GetExplodListAdder(uint baseAddr)
        {
            uint num1 = 0;
            uint num2 = 0;
            byte[] buf = new byte[4];
            if (baseAddr != 0U && MugenWindow.MainObj().ReadMemory((IntPtr)(long)(baseAddr + this._addr_db.EXPLOD_LIST_BASE_OFFSET), ref buf, 4U) > 0)
                num1 = BitConverter.ToUInt32(buf, 0);
            if (num1 != 0U && MugenWindow.MainObj().ReadMemory((IntPtr)(long)num1, ref buf, 4U) > 0)
                num2 = BitConverter.ToUInt32(buf, 0);
            return num2;
        }

        private uint GetExplodAdder(uint explodListAddr, uint index) => explodListAddr + index * this._addr_db.OFFSET_EXPLOD_LIST_OFFSET;

        /// <summary>
        /// gets the display name (this is relative to the addr returned by <c>GetPlayerInfoAdder</c>)
        /// </summary>
        /// <param name="playerAddr"></param>
        /// <param name="displayName"></param>
        /// <returns></returns>
        private int GetDisplayName(uint playerAddr, ref string displayName)
        {
            byte[] buf = new byte[256];
            uint playerInfoAdder = this.GetPlayerInfoAdder(playerAddr);
            if (playerInfoAdder == 0U || MugenWindow.MainObj().ReadMemory((IntPtr)(long)(playerInfoAdder + this._addr_db.DISPLAY_NAME_PLAYER_INFO_OFFSET), ref buf, 256U) <= 0)
                return 0;
            string str = Encoding.ASCII.GetString(buf);
            int length = str.IndexOf(char.MinValue);
            displayName = str.Substring(0, length);
            return displayName.Length;
        }

        /// <summary>
        /// helper function to read an integer from a specified address+offset combo
        /// </summary>
        /// <param name="addr">base address to read at</param>
        /// <param name="offset">offset from addr</param>
        /// <returns>data stored in memory as an integer</returns>
        private int _GetInt32Data(uint addr, uint offset)
        {
            int num = 0;
            byte[] buf = new byte[4];
            if (MugenWindow.MainObj().ReadMemory((IntPtr)(long)(addr + offset), ref buf, 4U) > 0)
                num = BitConverter.ToInt32(buf, 0);
            return num;
        }

        /// <summary>
        /// sets one int of data in Mugen's memory.
        /// </summary>
        /// <param name="addr">address to write to</param>
        /// <param name="offset">offset from addr to write to</param>
        /// <param name="value">value to write</param>
        private void _SetInt32Data(uint addr, uint offset, int value)
        {
            byte[] bytes = BitConverter.GetBytes(value);
            MugenWindow.MainObj().WriteMemory((IntPtr)(long)(addr + offset), ref bytes, 4U);
        }

        /// <summary>
        /// helper function to read a float from a specified address+offset combo
        /// </summary>
        /// <param name="addr">base address to read at</param>
        /// <param name="offset">offset from addr</param>
        /// <returns>data stored in memory as a float (4-byte)</returns>
        private float _GetFloatData(uint addr, uint offset)
        {
            float num = 0.0f;
            byte[] buf = new byte[4];
            if (MugenWindow.MainObj().ReadMemory((IntPtr)(long)(addr + offset), ref buf, 4U) > 0)
                num = BitConverter.ToSingle(buf, 0);
            return num;
        }

        /// <summary>
        /// helper function to read a double from a specified address+offset combo
        /// </summary>
        /// <param name="addr">base address to read at</param>
        /// <param name="offset">offset from addr</param>
        /// <returns>data stored in memory as a double (8-byte float)</returns>
        private double _GetDoubleData(uint addr, uint offset)
        {
            double num = 0.0;
            byte[] buf = new byte[8];
            if (MugenWindow.MainObj().ReadMemory((IntPtr)(long)(addr + offset), ref buf, 8U) > 0)
                num = BitConverter.ToDouble(buf, 0);
            return num;
        }

        private int GetPlayerId(uint playerAddr) => this._GetInt32Data(playerAddr, this._addr_db.PLAYER_ID_PLAYER_OFFSET);

        private int GetHelperId(uint playerAddr) => this._GetInt32Data(playerAddr, this._addr_db.HELPER_ID_PLAYER_OFFSET);

        private int GetParentId(uint playerAddr) => this._GetInt32Data(playerAddr, this._addr_db.PARENT_ID_PLAYER_OFFSET);

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
                uint playerAddr = this._GetPlayerAddr(baseAddr, (uint)((ulong)this._addr_db.PLAYER_1_BASE_OFFSET + (ulong)(index * 4)));
                if (playerAddr != 0U && this.DoesExist(playerAddr) && playerId == this.GetPlayerId(playerAddr))
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
                uint playerAddr = this._GetPlayerAddr(baseAddr, (uint)((ulong)this._addr_db.PLAYER_1_BASE_OFFSET + (ulong)(index * 4)));
                if (playerAddr != 0U && this.DoesExist(playerAddr) && playerId == this.GetPlayerId(playerAddr))
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
            uint playerAddr1 = this._GetPlayerAddr(baseAddr, (uint)((ulong)this._addr_db.PLAYER_1_BASE_OFFSET + (ulong)(owner * 4)));
            if (playerAddr1 == 0U)
                return 0;
            int playerId = this.GetPlayerId(playerAddr1);
            if (playerId == 0)
                return 0;
            for (int index = 4; index < 60; ++index)
            {
                uint playerAddr2 = this._GetPlayerAddr(baseAddr, (uint)((ulong)this._addr_db.PLAYER_1_BASE_OFFSET + (ulong)(index * 4)));
                if (playerAddr2 != 0U && this.DoesExist(playerAddr2) && helperId == this.GetHelperId(playerAddr2))
                {
                    int rootId = this.GetRootId(baseAddr, this.GetPlayerId(playerAddr2));
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
            for (int playerId = myId; playerId > 0; playerId = playerAddr <= 0U ? 0 : this.GetParentId(playerAddr))
            {
                num = playerId;
                int playerNoFromId = this.GetPlayerNoFromId(baseAddr, playerId);
                if (playerNoFromId < 0)
                    return num;
                playerAddr = this._GetPlayerAddr(baseAddr, (uint)((ulong)this._addr_db.PLAYER_1_BASE_OFFSET + (ulong)(playerNoFromId * 4)));
            }
            return num;
        }

        private bool DoesExplodExist(uint explodAddr) => (uint)this._GetInt32Data(explodAddr, this._addr_db.EXIST_EXPLOD_OFFSET) > 0U;

        private int GetExplodId(uint explodAddr) => this._GetInt32Data(explodAddr, this._addr_db.EXPLOD_ID_EXPLOD_OFFSET);

        private int GetExplodOwnerId(uint explodAddr) => this._GetInt32Data(explodAddr, this._addr_db.EXPLOD_OWNER_ID_EXPLOD_OFFSET);

        private uint GetProjBaseAdder(uint playerAddr)
        {
            uint num1 = 0;
            uint num2 = 0;
            byte[] buf = new byte[4];
            if (playerAddr != 0U && MugenWindow.MainObj().ReadMemory((IntPtr)(long)(playerAddr + this._addr_db.PROJ_BASE_PLAYER_OFFSET), ref buf, 4U) > 0)
                num1 = BitConverter.ToUInt32(buf, 0);
            if (num1 != 0U && MugenWindow.MainObj().ReadMemory((IntPtr)(long)num1, ref buf, 4U) > 0)
                num2 = BitConverter.ToUInt32(buf, 0);
            return num2;
        }

        private uint GetProjListAdder(uint projBaseAddr)
        {
            uint num = 0;
            byte[] buf = new byte[4];
            if (projBaseAddr != 0U && MugenWindow.MainObj().ReadMemory((IntPtr)(long)(projBaseAddr + this._addr_db.PROJ_LIST_PROJ_BASE_OFFSET), ref buf, 4U) > 0)
                num = BitConverter.ToUInt32(buf, 0);
            return num;
        }

        private uint GetProjAdder(uint projListAddr, uint index) => projListAddr + index * this._addr_db.OFFSET_PROJ_LIST_OFFSET;

        private bool DoesProjExist(uint projBase, int projNo, uint projAddr)
        {
            int int32Data = this._GetInt32Data(projBase, this._addr_db.PROJ_MAX_PROJ_BASE_OFFSET);
            return projNo <= int32Data && this._GetInt32Data(projAddr, this._addr_db.EXIST_PROJ_OFFSET) == 1;
        }

        private int GetProjId(uint projAddr) => this._GetInt32Data(projAddr, this._addr_db.PROJ_ID_PROJ_OFFSET);

        private int GetProjX(uint playerAddr, uint projAddr)
        {
            if (this._mugen_type == MugenWindow.MugenType_t.MUGEN_TYPE_MUGEN11A4)
                return (int)((double)this._GetFloatData(projAddr, this._addr_db.PROJ_X_PROJ_OFFSET) / (1280.0 / this.GetLocalCoordX(playerAddr)));
            return this._mugen_type != MugenWindow.MugenType_t.MUGEN_TYPE_MUGEN10 ? this._GetInt32Data(projAddr, this._addr_db.PROJ_X_PROJ_OFFSET) : 320 + (int)this._GetFloatData(projAddr, this._addr_db.PROJ_X_PROJ_OFFSET);
        }

        private int GetProjY(uint playerAddr, uint projAddr)
        {
            if (this._mugen_type == MugenWindow.MugenType_t.MUGEN_TYPE_MUGEN11A4)
                return (int)((double)this._GetFloatData(projAddr, this._addr_db.PROJ_Y_PROJ_OFFSET) / (740.0 / this.GetLocalCoordY(playerAddr)));
            return this._mugen_type != MugenWindow.MugenType_t.MUGEN_TYPE_MUGEN10 ? this._GetInt32Data(projAddr, this._addr_db.PROJ_Y_PROJ_OFFSET) : 240 + (int)this._GetFloatData(projAddr, this._addr_db.PROJ_Y_PROJ_OFFSET);
        }

        private int GetAnimIndex(uint explodAddr)
        {
            uint int32Data = (uint)this._GetInt32Data(explodAddr, this._addr_db.ANIM_ADDR_EXPLOD_OFFSET);
            return int32Data != 0U ? this._GetInt32Data(int32Data, this._addr_db.ANIM_INDEX_EXPLOD_OFFSET) : -1;
        }

        private int GetStateOwner(uint baseAddr, uint playerAddr)
        {
            int int32Data = this._GetInt32Data(playerAddr, this._addr_db.STATE_OWNER_PLAYER_OFFSET);
            if (int32Data <= 0 || int32Data > 60)
                return -1;
            uint playerAddr1 = this._GetPlayerAddr(baseAddr, (uint)((ulong)this._addr_db.PLAYER_1_BASE_OFFSET + (ulong)((int32Data - 1) * 4)));
            return playerAddr1 != 0U ? this.GetPlayerId(playerAddr1) : 0;
        }

        private int GetSpecialStateNo(uint playerAddr)
        {
            if (this._debugSpPointer == 0U)
                return 0;
            uint num = 544;
            for (uint index = 0; index <= 128U; index += 4U)
            {
                if (this._GetInt32Data(this._debugSpPointer, num + index) == 1 && (int)playerAddr == this._GetInt32Data(this._debugSpPointer, (uint)((int)num + (int)index + 48)))
                {
                    int int32Data = this._GetInt32Data(this._debugSpPointer, (uint)((int)num + (int)index + 152));
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

        private int GetStateNo(uint playerAddr) => this._GetInt32Data(playerAddr, this._addr_db.STATE_NO_PLAYER_OFFSET);

        private int GetPrevStateNo(uint playerAddr) => this._GetInt32Data(playerAddr, this._addr_db.PREV_STATE_NO_PLAYER_OFFSET);

        private int GetPalno(uint playerAddr) => playerAddr != 0U ? this._GetInt32Data(playerAddr, this._addr_db.PALNO_PLAYER_OFFSET) + 1 : 0;

        private int GetAlive(uint playerAddr) => playerAddr != 0U ? this._GetInt32Data(playerAddr, this._addr_db.ALIVE_PLAYER_OFFSET) : 0;

        private int GetLife(uint playerAddr) => playerAddr != 0U ? this._GetInt32Data(playerAddr, this._addr_db.LIFE_PLAYER_OFFSET) : 0;

        private int GetPower(uint playerAddr) => playerAddr != 0U ? this._GetInt32Data(playerAddr, this._addr_db.POWER_PLAYER_OFFSET) : 0;

        /// <summary>
        /// navigates anim structures to read anim list address
        /// <br/>this one is a complicated one due to 3x redirected pointers.
        /// </summary>
        /// <param name="baseAddr"></param>
        /// <param name="playerAddr"></param>
        /// <returns></returns>
        private uint GetAnimListAddr(uint baseAddr, uint playerAddr)
        {
            if (playerAddr == 0U)
                return 0;
            uint playerInfoAdder = this.GetPlayerInfoAdder(playerAddr);
            if (playerInfoAdder == 0U)
                return 0;
            uint int32Data1 = (uint)this._GetInt32Data(playerInfoAdder, this._addr_db.ANIM_LIST_REF1_PLAYER_INFO_OFFSET);
            if (int32Data1 == 0U)
                return 0;
            try
            {
                uint int32Data2 = (uint)this._GetInt32Data(int32Data1, this._addr_db.ANIM_LIST_REF2_PLAYER_INFO_OFFSET);
                if (int32Data2 == 0U)
                    return 0;
                uint int32Data3 = (uint)this._GetInt32Data(int32Data2, this._addr_db.ANIM_LIST_REF3_PLAYER_INFO_OFFSET);
                if (this._CheckAnimList(int32Data3))
                    return int32Data3;
            }
            catch
            {
            }
            return 0;
        }

        /// <summary>
        /// helper function to verify the anim list was found, this appears to be some sort of 'checksum'.
        /// </summary>
        /// <param name="targetAddr"></param>
        /// <returns></returns>
        private bool _CheckAnimList(uint targetAddr)
        {
            try
            {
                int int32Data1 = this._GetInt32Data(targetAddr, 0U);
                int int32Data2 = this._GetInt32Data(targetAddr, 4U);
                int int32Data3 = this._GetInt32Data(targetAddr, 16U);
                int int32Data4 = this._GetInt32Data(targetAddr, 20U);
                int int32Data5 = this._GetInt32Data(targetAddr, 32U);
                int int32Data6 = this._GetInt32Data(targetAddr, 36U);
                if (int32Data1 == 1)
                {
                    if (int32Data2 == 0)
                    {
                        if (int32Data3 == 1)
                        {
                            if (int32Data4 == 1)
                            {
                                if (int32Data5 == 1)
                                {
                                    if (int32Data6 == 2)
                                        return true;
                                }
                            }
                        }
                    }
                }
            }
            catch
            {
            }
            return false;
        }

        private int GetAnimNo(uint animListAddr, int index) => animListAddr != 0U ? this._GetInt32Data(animListAddr, (uint)((ulong)index * (ulong)this._addr_db.OFFSET_ANIM_LIST_OFFSET + (ulong)this._addr_db.ANIM_NO_ANIM_OFFSET)) : -1;

        private int GetAnim(uint baseAddr, uint playerAddr) => -1;

        private int GetDamage(uint playerAddr) => playerAddr != 0U ? this._GetInt32Data(playerAddr, this._addr_db.DAMAGE_PLAYER_OFFSET) : 0;

        private int GetCtrl(uint playerAddr) => playerAddr != 0U ? this._GetInt32Data(playerAddr, this._addr_db.CTRL_PLAYER_OFFSET) : 0;

        private int GetStateType(uint playerAddr) => playerAddr != 0U ? this._GetInt32Data(playerAddr, this._addr_db.STATE_TYPE_PLAYER_OFFSET) : 0;

        private int GetMoveType(uint playerAddr) => playerAddr != 0U ? this._GetInt32Data(playerAddr, this._addr_db.MOVE_TYPE_PLAYER_OFFSET) : 0;

        private int GetHitPauseTime(uint playerAddr) => playerAddr != 0U ? this._GetInt32Data(playerAddr, this._addr_db.HIT_PAUSE_TIME_PLAYER_OFFSET) : 0;

        private int GetMoveContact(uint playerAddr)
        {
            if (playerAddr == 0U)
                return 0;
            switch (this._GetInt32Data(playerAddr, this._addr_db.MOVE_CONTACT_PLAYER_OFFSET))
            {
                case 1:
                    return this._GetInt32Data(playerAddr, this._addr_db.MOVE_HIT_PLAYER_OFFSET);
                case 2:
                    return this._GetInt32Data(playerAddr, this._addr_db.MOVE_HIT_PLAYER_OFFSET);
                default:
                    return 0;
            }
        }

        private int GetMoveHit(uint playerAddr) => playerAddr != 0U && this._GetInt32Data(playerAddr, this._addr_db.MOVE_CONTACT_PLAYER_OFFSET) == 2 ? this._GetInt32Data(playerAddr, this._addr_db.MOVE_HIT_PLAYER_OFFSET) : 0;

        private int GetMoveGuarded(uint playerAddr) => playerAddr != 0U && this._GetInt32Data(playerAddr, this._addr_db.MOVE_CONTACT_PLAYER_OFFSET) == 1 ? this._GetInt32Data(playerAddr, this._addr_db.MOVE_HIT_PLAYER_OFFSET) : 0;

        private int GetMoveReversed(uint playerAddr) => playerAddr != 0U && this._GetInt32Data(playerAddr, this._addr_db.MOVE_CONTACT_PLAYER_OFFSET) == 4 ? this._GetInt32Data(playerAddr, this._addr_db.MOVE_HIT_PLAYER_OFFSET) : 0;

        private int GetNoClsn2Flag(uint playerAddr)
        {
            if (playerAddr == 0U)
                return 1;
            uint int32Data1 = (uint)this._GetInt32Data(playerAddr, this._addr_db.ANIM_ADDR_PLAYER_OFFSET);
            if (int32Data1 == 0U)
                return 1;
            uint int32Data2 = (uint)this._GetInt32Data(int32Data1, this._addr_db.ANIM_INFO_ANIM_OFFSET);
            return int32Data2 == 0U || this._GetInt32Data(int32Data2, this._addr_db.CLSN2_ADDR_ANIM_INFO_OFFSET) == 0 ? 1 : 0;
        }

        private int GetHasClsn1Flag(uint playerAddr)
        {
            if (playerAddr == 0U)
                return 1;
            uint int32Data1 = (uint)this._GetInt32Data(playerAddr, this._addr_db.ANIM_ADDR_PLAYER_OFFSET);
            if (int32Data1 == 0U)
                return 1;
            uint int32Data2 = (uint)this._GetInt32Data(int32Data1, this._addr_db.ANIM_INFO_ANIM_OFFSET);
            return int32Data2 == 0U || this._GetInt32Data(int32Data2, this._addr_db.CLSN1_ADDR_ANIM_INFO_OFFSET) == 0 ? 0 : 1;
        }

        private int GetMuteki(uint playerAddr) => playerAddr != 0U ? this._GetInt32Data(playerAddr, this._addr_db.MUTEKI_PLAYER_OFFSET) : 0;

        private int GetHitBy(uint playerAddr) => playerAddr != 0U ? this._GetInt32Data(playerAddr, this._addr_db.HITBY_1_PLAYER_OFFSET) & this._GetInt32Data(playerAddr, this._addr_db.HITBY_2_PLAYER_OFFSET) : 0;

        private int GetHitOverRide(uint playerAddr, int indexFrom0)
        {
            if (playerAddr == 0U || indexFrom0 < 0 || indexFrom0 > 7)
                return 0;
            uint addr = (uint)((ulong)(playerAddr + this._addr_db.HITOVERRIDE_LIST_PLAYER_OFFSET) + (ulong)this._addr_db.OFFSET_HITOVERRIDE_LIST_OFFSET * (ulong)indexFrom0);
            return this._GetInt32Data(addr, this._addr_db.EXIST_HITOVERRIDE_OFFSET) != 0 ? this._GetInt32Data(addr, this._addr_db.ATTR_HITOVERRIDE_OFFSET) : 0;
        }

        private int GetTarget(uint playerAddr, int targetNo)
        {
            if (playerAddr == 0U)
                return 0;
            uint int32Data1 = (uint)this._GetInt32Data(playerAddr, this._addr_db.TARGET_ENTRY_PLAYER_OFFSET);
            if (int32Data1 == 0U || this._GetInt32Data(int32Data1, this._addr_db.NUMTARGET_TARGET_ENTRY_OFFSET) <= targetNo)
                return 0;
            uint int32Data2 = (uint)this._GetInt32Data(int32Data1, this._addr_db.TARGET_LIST_TARGET_ENTRY_OFFSET);
            if (int32Data2 == 0U)
                return 0;
            uint int32Data3 = (uint)this._GetInt32Data(int32Data2, (uint)((ulong)this._addr_db.OFFSET_TARGET_LIST_OFFSET * (ulong)targetNo));
            return int32Data3 != 0U ? this.GetPlayerId(int32Data3) : 0;
        }

        private int GetFallDamage(uint playerAddr) => playerAddr != 0U ? this._GetInt32Data(playerAddr, this._addr_db.FALL_DAMAGE_PLAYER_OFFSET) : 0;

        private int GetFacing(uint playerAddr) => playerAddr != 0U ? this._GetInt32Data(playerAddr, this._addr_db.FACING_PLAYER_OFFSET) : 0;

        // all the pos/vel computations rely on LocalCoord as well for later Mugen.
        // they are also inaccurate because they return stage-based co-ords.
        // TODO: review this

        private float GetPosX(uint baseAddr, uint playerAddr)
        {
            if (playerAddr == 0U)
                return 0.0f;
            if (this._mugen_type == MugenWindow.MugenType_t.MUGEN_TYPE_MUGEN11A4)
            {
                double doubleData = this._GetDoubleData(playerAddr, this._addr_db.POS_X_PLAYER_OFFSET);
                double localCoordX = this.GetLocalCoordX(playerAddr);
                double num = (double)this.GetScreenX(baseAddr) / localCoordX;
                return (float)(doubleData / num);
            }
            return this._mugen_type != MugenWindow.MugenType_t.MUGEN_TYPE_WINMUGEN ? (float)(((double)this._GetFloatData(playerAddr, this._addr_db.POS_X_PLAYER_OFFSET) - (double)this.GetScreenX(baseAddr)) / 2.0) : (float)((double)this._GetFloatData(playerAddr, this._addr_db.POS_X_PLAYER_OFFSET) - (double)this.GetScreenX(baseAddr) - 160.0);
        }

        private float GetPosY(uint baseAddr, uint playerAddr)
        {
            if (playerAddr == 0U)
                return 0.0f;
            if (this._mugen_type == MugenWindow.MugenType_t.MUGEN_TYPE_MUGEN11A4)
            {
                double doubleData = this._GetDoubleData(playerAddr, this._addr_db.POS_Y_PLAYER_OFFSET);
                double localCoordY = this.GetLocalCoordY(playerAddr);
                double num = (double)this.GetScreenY(baseAddr) / localCoordY;
                return (float)(doubleData / num);
            }
            return this._mugen_type != MugenWindow.MugenType_t.MUGEN_TYPE_WINMUGEN ? this._GetFloatData(playerAddr, this._addr_db.POS_Y_PLAYER_OFFSET) / 2f : this._GetFloatData(playerAddr, this._addr_db.POS_Y_PLAYER_OFFSET);
        }

        private float GetVelX(uint baseAddr, uint playerAddr)
        {
            if (this._mugen_type == MugenWindow.MugenType_t.MUGEN_TYPE_MUGEN11A4)
            {
                double doubleData = this._GetDoubleData(playerAddr, this._addr_db.VEL_X_PLAYER_OFFSET);
                double localCoordX = this.GetLocalCoordX(playerAddr);
                double num = (double)this.GetScreenX(baseAddr) / localCoordX;
                return (float)(doubleData / num) * (float)this.GetFacing(playerAddr);
            }
            return playerAddr != 0U ? this._GetFloatData(playerAddr, this._addr_db.VEL_X_PLAYER_OFFSET) : 0.0f;
        }

        private float GetVelY(uint baseAddr, uint playerAddr)
        {
            if (this._mugen_type == MugenWindow.MugenType_t.MUGEN_TYPE_MUGEN11A4)
            {
                double doubleData = this._GetDoubleData(playerAddr, this._addr_db.VEL_Y_PLAYER_OFFSET);
                double localCoordY = this.GetLocalCoordY(playerAddr);
                double num = (double)this.GetScreenY(baseAddr) / localCoordY;
                return (float)(doubleData / num);
            }
            return playerAddr != 0U ? this._GetFloatData(playerAddr, this._addr_db.VEL_Y_PLAYER_OFFSET) : 0.0f;
        }

        private double GetLocalCoordX(uint playerAddr) => this._mugen_type != MugenWindow.MugenType_t.MUGEN_TYPE_MUGEN11A4 || playerAddr == 0U ? 0.0 : this._GetDoubleData((uint)this._GetInt32Data(playerAddr, 0U), this._addr_db.LOCALCOORD_X_PLAYER_INFO_OFFSET);

        private double GetLocalCoordY(uint playerAddr) => this._mugen_type != MugenWindow.MugenType_t.MUGEN_TYPE_MUGEN11A4 || playerAddr == 0U ? 0.0 : this._GetDoubleData((uint)this._GetInt32Data(playerAddr, 0U), this._addr_db.LOCALCOORD_Y_PLAYER_INFO_OFFSET);

        private int GetSysvar(uint playerAddr, int index) => playerAddr != 0U ? this._GetInt32Data(playerAddr, (uint)((ulong)this._addr_db.SYS_VAR_PLAYER_OFFSET + (ulong)(index * 4))) : 0;

        private float GetSysfvar(uint playerAddr, int index) => playerAddr != 0U ? this._GetFloatData(playerAddr, (uint)((ulong)this._addr_db.SYS_FVAR_PLAYER_OFFSET + (ulong)(index * 4))) : 0.0f;

        private int GetVar(uint playerAddr, int index) => playerAddr != 0U ? this._GetInt32Data(playerAddr, (uint)((ulong)this._addr_db.VAR_PLAYER_OFFSET + (ulong)(index * 4))) : 0;

        private float GetFvar(uint playerAddr, int index) => playerAddr != 0U ? this._GetFloatData(playerAddr, (uint)((ulong)this._addr_db.FVAR_PLAYER_OFFSET + (ulong)(index * 4))) : 0.0f;

        private int GetActiveFlag(uint baseAddr, int playerNo) => baseAddr != 0U ? this._GetInt32Data(baseAddr, (uint)((ulong)this._addr_db.ACTIVE_PLAYER_OFFSET + (ulong)(playerNo * 4))) : 0;

        private int GetPauseTime(uint baseAddr) => baseAddr != 0U ? this._GetInt32Data(baseAddr, this._addr_db.PAUSE_TIME_BASE_OFFSET) : 0;

        private int GetSuperPauseTime(uint baseAddr) => baseAddr != 0U ? this._GetInt32Data(baseAddr, this._addr_db.SUPER_PAUSE_TIME_BASER_OFFSET) : 0;

        private int GetPauseMoveTime(uint playerAddr) => playerAddr != 0U ? this._GetInt32Data(playerAddr, this._addr_db.PAUSE_MOVE_TIME_PLAYER_OFFSET) : 0;

        private int GetSuperPauseMoveTime(uint playerAddr) => playerAddr != 0U ? this._GetInt32Data(playerAddr, this._addr_db.SUPER_PAUSE_MOVE_TIME_PLAYER_OFFSET) : 0;

        private float GetAttackMulSet(uint playerAddr) => playerAddr != 0U ? this._GetFloatData(playerAddr, this._addr_db.ATTACK_MUL_SET_PLAYER_OFFSET) : 0.0f;

        /// <summary>
        /// fetches the global AssertSpecial flags.
        /// <br/>these are a list of sequential int flags relative to the base address.
        /// </summary>
        /// <param name="baseAddr"></param>
        /// <returns></returns>
        private bool[] GetGlobalAssertSpecials(uint baseAddr)
        {
            bool[] flagArray = new bool[11];
            if (baseAddr == 0U)
                return flagArray;
            int int32Data1 = this._GetInt32Data(baseAddr, this._addr_db.ASSERT_1_PLAYER_OFFSET);
            if (int32Data1 == 0)
                return flagArray;
            flagArray[0] = (uint)(int32Data1 & 1) > 0U;
            flagArray[1] = (uint)(int32Data1 & 256) > 0U;
            flagArray[2] = (uint)(int32Data1 & 65536) > 0U;
            flagArray[3] = (uint)(int32Data1 & 16777216) > 0U;
            int int32Data2 = this._GetInt32Data(baseAddr, this._addr_db.ASSERT_1_PLAYER_OFFSET + 4U);
            if (int32Data2 == 0)
                return flagArray;
            flagArray[4] = (uint)(int32Data2 & 1) > 0U;
            flagArray[5] = (uint)(int32Data2 & 256) > 0U;
            flagArray[6] = (uint)(int32Data2 & 65536) > 0U;
            flagArray[7] = (uint)(int32Data2 & 16777216) > 0U;
            int int32Data3 = this._GetInt32Data(baseAddr, this._addr_db.ASSERT_1_PLAYER_OFFSET + 8U);
            if (int32Data3 == 0)
                return flagArray;
            flagArray[8] = (uint)(int32Data3 & 1) > 0U;
            flagArray[9] = (uint)(int32Data3 & 256) > 0U;
            flagArray[10] = (uint)(int32Data3 & 65536) > 0U;
            return flagArray;
        }

        /// <summary>
        /// fetches the player AssertSpecial flags.
        /// <br/>these are a list of sequential int flags relative to the player's address.
        /// </summary>
        /// <param name="playerAddr"></param>
        /// <returns></returns>
        private bool[] GetSelfAssertSpecials(uint playerAddr)
        {
            bool[] flagArray = new bool[9];
            if (!(this._addr_db is Mugen11A4DB))
                return new bool[0];
            if (playerAddr == 0U)
                return flagArray;
            int int32Data1 = this._GetInt32Data(playerAddr, this._addr_db.SELF_ASSERT_PLAYER_OFFSET);
            if (int32Data1 == 0)
                return flagArray;
            flagArray[0] = (uint)(int32Data1 & 1) > 0U;
            flagArray[1] = (uint)(int32Data1 & 256) > 0U;
            flagArray[2] = (uint)(int32Data1 & 65536) > 0U;
            flagArray[3] = (uint)(int32Data1 & 16777216) > 0U;
            int int32Data2 = this._GetInt32Data(playerAddr, this._addr_db.SELF_ASSERT_PLAYER_OFFSET + 4U);
            if (int32Data2 == 0)
                return flagArray;
            flagArray[4] = (uint)(int32Data2 & 1) > 0U;
            flagArray[5] = (uint)(int32Data2 & 256) > 0U;
            flagArray[6] = (uint)(int32Data2 & 65536) > 0U;
            flagArray[7] = (uint)(int32Data2 & 16777216) > 0U;
            int int32Data3 = this._GetInt32Data(playerAddr, this._addr_db.SELF_ASSERT_PLAYER_OFFSET + 8U);
            if (int32Data3 == 0)
                return flagArray;
            flagArray[8] = (uint)(int32Data3 & 1) > 0U;
            return flagArray;
        }

        private int GetNoko(uint baseAddr) => baseAddr == 0U || (this._GetInt32Data(baseAddr, this._addr_db.ASSERT_1_PLAYER_OFFSET) & 65536) == 0 ? 0 : 1;

        private int GetIntro(uint baseAddr) => baseAddr == 0U || (this._GetInt32Data(baseAddr, this._addr_db.ASSERT_1_PLAYER_OFFSET) & 1) == 0 ? 0 : 1;

        private int GetRoundNotOver(uint baseAddr) => baseAddr == 0U || (this._GetInt32Data(baseAddr, this._addr_db.ASSERT_1_PLAYER_OFFSET) & 256) == 0 ? 0 : 1;

        private int GetTimerFreeze(uint baseAddr) => baseAddr == 0U || (this._GetInt32Data(baseAddr, this._addr_db.ASSERT_1_PLAYER_OFFSET + 4U) & 16777216) == 0 ? 0 : 1;

        private void SetCtrl(uint playerAddr, bool ctrl)
        {
            int num = ctrl ? 1 : 0;
            this._SetInt32Data(playerAddr, this._addr_db.CTRL_PLAYER_OFFSET, num);
        }

        /// <summary>
        /// currently unused - forcibly unsets a player's existence flag to cause Mugen to regard them as invalid
        /// </summary>
        /// <param name="playerAddr"></param>
        /// <param name="exist"></param>
        private void DeletePlayer(uint playerAddr, bool exist)
        {
            int num = exist ? 1 : 0;
            this._SetInt32Data(playerAddr, this._addr_db.EXIST_PLAYER_OFFSET, num);
        }

        private void SetSpeedMode(uint baseAddr, bool isSpeedMode)
        {
            int num = isSpeedMode ? 1 : 0;
            this._SetInt32Data(baseAddr, this._addr_db.SPEED_MODE_BASE_OFFSET, num);
        }

        private void SetSkipMode(uint baseAddr, bool isSkipMode)
        {
            int num = isSkipMode ? this._skipModeFrames : -1;
            this._SetInt32Data(baseAddr, this._addr_db.SKIP_MODE_BASE_OFFSET, num);
        }

        /// <summary>
        /// enables or disabled Mugen's debug text with a focus on player 1.
        /// </summary>
        /// <param name="baseAddr"></param>
        /// <param name="isDebugMode"></param>
        private void EnableDebugKey(uint baseAddr, bool isDebugMode)
        {
            if (isDebugMode)
            {
                this._SetInt32Data(baseAddr, this._addr_db.DEBUG_MODE_BASE_OFFSET, 0);
                this._SetInt32Data(baseAddr, this._addr_db.DEBUG_TARGET_BASE_OFFSET, 1);
            }
            else
            {
                this._SetInt32Data(baseAddr, this._addr_db.DEBUG_MODE_BASE_OFFSET, 0);
                this._SetInt32Data(baseAddr, this._addr_db.DEBUG_TARGET_BASE_OFFSET, 0);
            }
        }

        private void SetDebugMode(uint baseAddr, bool isDebugMode)
        {
            if (isDebugMode)
            {
                this._SetInt32Data(baseAddr, this._addr_db.DEBUG_MODE_BASE_OFFSET, 1);
                if (this.GetDebugTargetNo(baseAddr) != 0)
                    return;
                this._SetInt32Data(baseAddr, this._addr_db.DEBUG_TARGET_BASE_OFFSET, 1);
            }
            else
                this._SetInt32Data(baseAddr, this._addr_db.DEBUG_MODE_BASE_OFFSET, 0);
        }

        /// <summary>
        /// sets the current focus for debug text to the indicated targetNo (value from 1 to 60).
        /// </summary>
        /// <param name="baseAddr"></param>
        /// <param name="targetNo"></param>
        private void SetDebugTargetNo(uint baseAddr, int targetNo) => this._SetInt32Data(baseAddr, this._addr_db.DEBUG_TARGET_BASE_OFFSET, targetNo);

        /// <summary>
        /// sets the color of the debug text.
        /// </summary>
        /// <param name="baseAddr"></param>
        /// <param name="debugColor">DebugColor enum to use</param>
        private void SetDebugColor(uint baseAddr, DebugColor debugColor)
        {
            if (this._addr_db.USE_NEW_DEBUG_COLOR_ADDR)
            {
                this.SetDebugColorEX(baseAddr, debugColor);
                return;
            }
            switch (debugColor)
            {
                case DebugColor.WHITE:
                    this._SetInt32Data(baseAddr, this._addr_db.PAL_TIME_BASE_OFFSET, (int)byte.MaxValue);
                    this._SetInt32Data(baseAddr, this._addr_db.PAL_R_BASE_OFFSET, 0);
                    this._SetInt32Data(baseAddr, this._addr_db.PAL_G_BASE_OFFSET, 0);
                    this._SetInt32Data(baseAddr, this._addr_db.PAL_B_BASE_OFFSET, 0);
                    break;
                case DebugColor.YELLOW:
                    this._SetInt32Data(baseAddr, this._addr_db.PAL_TIME_BASE_OFFSET, (int)byte.MaxValue);
                    this._SetInt32Data(baseAddr, this._addr_db.PAL_R_BASE_OFFSET, 0);
                    this._SetInt32Data(baseAddr, this._addr_db.PAL_G_BASE_OFFSET, 0);
                    this._SetInt32Data(baseAddr, this._addr_db.PAL_B_BASE_OFFSET, -255);
                    break;
                case DebugColor.PURPLE:
                    this._SetInt32Data(baseAddr, this._addr_db.PAL_TIME_BASE_OFFSET, (int)byte.MaxValue);
                    this._SetInt32Data(baseAddr, this._addr_db.PAL_R_BASE_OFFSET, 0);
                    this._SetInt32Data(baseAddr, this._addr_db.PAL_G_BASE_OFFSET, -255);
                    this._SetInt32Data(baseAddr, this._addr_db.PAL_B_BASE_OFFSET, 0);
                    break;
                case DebugColor.RED:
                    this._SetInt32Data(baseAddr, this._addr_db.PAL_TIME_BASE_OFFSET, (int)byte.MaxValue);
                    this._SetInt32Data(baseAddr, this._addr_db.PAL_R_BASE_OFFSET, 0);
                    this._SetInt32Data(baseAddr, this._addr_db.PAL_G_BASE_OFFSET, -255);
                    this._SetInt32Data(baseAddr, this._addr_db.PAL_B_BASE_OFFSET, -255);
                    break;
                case DebugColor.BLACK:
                    this._SetInt32Data(baseAddr, this._addr_db.PAL_TIME_BASE_OFFSET, (int)byte.MaxValue);
                    this._SetInt32Data(baseAddr, this._addr_db.PAL_R_BASE_OFFSET, -255);
                    this._SetInt32Data(baseAddr, this._addr_db.PAL_G_BASE_OFFSET, -255);
                    this._SetInt32Data(baseAddr, this._addr_db.PAL_B_BASE_OFFSET, -255);
                    break;
                case DebugColor.GREEN:
                    this._SetInt32Data(baseAddr, this._addr_db.PAL_TIME_BASE_OFFSET, (int)byte.MaxValue);
                    this._SetInt32Data(baseAddr, this._addr_db.PAL_R_BASE_OFFSET, -255);
                    this._SetInt32Data(baseAddr, this._addr_db.PAL_G_BASE_OFFSET, 0);
                    this._SetInt32Data(baseAddr, this._addr_db.PAL_B_BASE_OFFSET, -255);
                    break;
                default:
                    this._SetInt32Data(baseAddr, this._addr_db.PAL_TIME_BASE_OFFSET, 1);
                    this._SetInt32Data(baseAddr, this._addr_db.PAL_R_BASE_OFFSET, 0);
                    this._SetInt32Data(baseAddr, this._addr_db.PAL_G_BASE_OFFSET, 0);
                    this._SetInt32Data(baseAddr, this._addr_db.PAL_B_BASE_OFFSET, 0);
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
            foreach (uint colorBase in this._addr_db.NEW_DEBUG_COLOR_OFFSETS)
            {
                this.ApplyDebugColorInt(baseAddr, colorBase, debugColor);
            }
            // stateno is split up, so apply differently
            this.ApplyDebugColorSplit(baseAddr, this._addr_db.NEW_DEBUG_COLOR_SN_OFFSETS, debugColor);
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
                    this._SetInt32Data(offsets[0], 1, 256);
                    this._SetInt32Data(offsets[1], 1, 256);
                    this._SetInt32Data(offsets[2], 1, 256);
                    break;
                case DebugColor.YELLOW:
                    this._SetInt32Data(offsets[0], 1, 0);
                    this._SetInt32Data(offsets[1], 1, 256);
                    this._SetInt32Data(offsets[2], 1, 256);
                    break;
                case DebugColor.PURPLE:
                    this._SetInt32Data(offsets[0], 1, 256);
                    this._SetInt32Data(offsets[1], 1, 0);
                    this._SetInt32Data(offsets[2], 1, 256);
                    break;
                case DebugColor.RED:
                    this._SetInt32Data(offsets[0], 1, 0);
                    this._SetInt32Data(offsets[1], 1, 0);
                    this._SetInt32Data(offsets[2], 1, 256);
                    break;
                case DebugColor.BLACK:
                    this._SetInt32Data(offsets[0], 1, 0);
                    this._SetInt32Data(offsets[1], 1, 0);
                    this._SetInt32Data(offsets[2], 1, 0);
                    break;
                case DebugColor.GREEN:
                    this._SetInt32Data(offsets[0], 1, 0);
                    this._SetInt32Data(offsets[1], 1, 256);
                    this._SetInt32Data(offsets[2], 1, 0);
                    break;
                case DebugColor.CUSTOM:
                    this._SetInt32Data(offsets[0], 1, this.customDebugColors[2]);
                    this._SetInt32Data(offsets[1], 1, this.customDebugColors[1]);
                    this._SetInt32Data(offsets[2], 1, this.customDebugColors[0]);
                    break;
                default:
                    this._SetInt32Data(offsets[0], 1, 256);
                    this._SetInt32Data(offsets[1], 1, 256);
                    this._SetInt32Data(offsets[2], 1, 256);
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
                    this._SetInt32Data(colorBase, 1, 256);
                    this._SetInt32Data(colorBase, 6, 256);
                    this._SetInt32Data(colorBase, 11, 256);
                    break;
                case DebugColor.YELLOW:
                    this._SetInt32Data(colorBase, 1, 0);
                    this._SetInt32Data(colorBase, 6, 256);
                    this._SetInt32Data(colorBase, 11, 256);
                    break;
                case DebugColor.PURPLE:
                    this._SetInt32Data(colorBase, 1, 256);
                    this._SetInt32Data(colorBase, 6, 0);
                    this._SetInt32Data(colorBase, 11, 256);
                    break;
                case DebugColor.RED:
                    this._SetInt32Data(colorBase, 1, 0);
                    this._SetInt32Data(colorBase, 6, 0);
                    this._SetInt32Data(colorBase, 11, 256);
                    break;
                case DebugColor.BLACK:
                    this._SetInt32Data(colorBase, 1, 0);
                    this._SetInt32Data(colorBase, 6, 0);
                    this._SetInt32Data(colorBase, 11, 0);
                    break;
                case DebugColor.GREEN:
                    this._SetInt32Data(colorBase, 1, 0);
                    this._SetInt32Data(colorBase, 6, 256);
                    this._SetInt32Data(colorBase, 11, 0);
                    break;
                case DebugColor.CUSTOM:
                    this._SetInt32Data(colorBase, 1, this.customDebugColors[2]);
                    this._SetInt32Data(colorBase, 6, this.customDebugColors[1]);
                    this._SetInt32Data(colorBase, 11, this.customDebugColors[0]);
                    break;
                default:
                    this._SetInt32Data(colorBase, 1, 256);
                    this._SetInt32Data(colorBase, 6, 256);
                    this._SetInt32Data(colorBase, 11, 256);
                    break;
            }
        }

        public void SetDebugCustomColors(int red, int green, int blue)
        {
            this.customDebugColors = new int[] { red, green, blue };
        }

        /// <summary>
        /// UNUSED - forcibly ends a round by time over and sets roundstate to 3.
        /// </summary>
        /// <param name="baseAddr"></param>
        public void ForceTimeOver(uint baseAddr)
        {
            int roundTime = this.GetRoundTime(baseAddr);
            if (roundTime > 30)
            {
                this._timeOverCount1 = 0;
                this._timeOverCount2 = 0;
                this._SetInt32Data(baseAddr, this._addr_db.ROUND_TIME_BASE_OFFSET, 30);
            }
            else
            {
                if (roundTime > 30)
                    return;
                this._timeOverCount2 = 0;
                ++this._timeOverCount1;
                if (this._timeOverCount1 <= 120)
                    return;
                this._timeOverCount1 = 0;
                this._SetInt32Data(baseAddr, this._addr_db.ROUND_TIME_BASE_OFFSET, 0);
                this._SetInt32Data(baseAddr, this._addr_db.ROUND_STATE_BASE_OFFSET, 3);
            }
        }

        /// <summary>
        /// injects a debug key into Mugen by setting the CMD_KEY addresses.
        /// <br/>notice keycodes changed between Win,1.0,1.1a4.
        /// </summary>
        /// <param name="keyCode"></param>
        public void _InjectCommand(int keyCode)
        {
            if (this._mugen_type == MugenWindow.MugenType_t.MUGEN_TYPE_MUGEN11A4)
            {
                this._SetInt32Data(0U, this._addr_db.CMD_KEY_ADDR, keyCode | 256);
                this._SetInt32Data(0U, this._addr_db.CMD_KEY_ADDR + 4U, keyCode | 768);
                this._SetInt32Data(0U, this._addr_db.CMD_NEXT_INDEX_ADDR, 3);
                this._SetInt32Data(0U, this._addr_db.CMD_CURRENT_INDEX_ADDR, 0);
            }
            else if (this._mugen_type == MugenWindow.MugenType_t.MUGEN_TYPE_MUGEN10)
            {
                this._SetInt32Data(0U, this._addr_db.CMD_KEY_ADDR, keyCode | 256);
                this._SetInt32Data(0U, this._addr_db.CMD_KEY_ADDR + 4U, keyCode | 768);
                this._SetInt32Data(0U, this._addr_db.CMD_CURRENT_INDEX_ADDR, 0);
                this._SetInt32Data(0U, this._addr_db.CMD_NEXT_INDEX_ADDR, 1);
            }
            else
            {
                this._SetInt32Data(0U, this._addr_db.CMD_KEY_ADDR, keyCode);
                this._SetInt32Data(0U, this._addr_db.CMD_CURRENT_INDEX_ADDR, 0);
                this._SetInt32Data(0U, this._addr_db.CMD_NEXT_INDEX_ADDR, 1);
            }
        }

        /// <summary>
        /// injects a debug key into Mugen by setting the CMD_KEY addresses.
        /// <br/>notice keycodes/cmd indexes changed between Win,1.0,1.1a4.
        /// </summary>
        /// <param name="keyCode"></param>
        public void InjectCommand(int keyCode)
        {
            if (this._mugen_type == MugenWindow.MugenType_t.MUGEN_TYPE_MUGEN11A4 || this._mugen_type == MugenWindow.MugenType_t.MUGEN_TYPE_MUGEN11B1)
            {
                this._SetInt32Data(0U, this._addr_db.CMD_KEY_ADDR, keyCode | 256);
                this._SetInt32Data(0U, this._addr_db.CMD_KEY_ADDR + 4U, keyCode | 768);
                this._SetInt32Data(0U, this._addr_db.CMD_NEXT_INDEX_ADDR, 1);
                this._SetInt32Data(0U, this._addr_db.CMD_CURRENT_INDEX_ADDR, 0);
            }
            else if (this._mugen_type == MugenWindow.MugenType_t.MUGEN_TYPE_MUGEN10)
            {
                this._SetInt32Data(0U, this._addr_db.CMD_KEY_ADDR, keyCode | 256);
                this._SetInt32Data(0U, this._addr_db.CMD_KEY_ADDR + 4U, keyCode | 768);
                this._SetInt32Data(0U, this._addr_db.CMD_CURRENT_INDEX_ADDR, 0);
                this._SetInt32Data(0U, this._addr_db.CMD_NEXT_INDEX_ADDR, 1);
            }
            else
            {
                this._SetInt32Data(0U, this._addr_db.CMD_CURRENT_INDEX_ADDR, 0);
                this._SetInt32Data(0U, this._addr_db.CMD_NEXT_INDEX_ADDR, 1);
                this._SetInt32Data(0U, this._addr_db.CMD_KEY_ADDR, keyCode);
            }
        }

        /// <summary>
        /// checks the debug key which is going to be processed next by Mugen
        /// </summary>
        /// <returns></returns>
        public int CheckNextCommand()
        {
            int int32Data = this._GetInt32Data(0U, this._addr_db.CMD_NEXT_INDEX_ADDR);
            int num = -1;
            if (int32Data > 0)
                num = this._GetInt32Data(0U, (uint)((ulong)this._addr_db.CMD_KEY_ADDR + (ulong)(int32Data * 4)));
            return num;
        }

        public void SetPaused(bool isPaused)
        {
            if (this._addr_db is Mugen11A4DB)
                this._SetInt32Data(this.GetBaseAddr(), this._addr_db.PAUSE_ADDR, isPaused ? 1 : 0);
            this._SetInt32Data(0U, this._addr_db.PAUSE_ADDR, isPaused ? 1 : 0);
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
            if (this._mugen_type != MugenWindow.MugenType_t.MUGEN_TYPE_WINMUGEN)
                this.InjectCommand(46);
            else
                this.InjectCommand(70);
        }

        public void InjectESC()
        {
            if (this._mugen_type != MugenWindow.MugenType_t.MUGEN_TYPE_WINMUGEN)
                this._InjectCommand(27);
            else
                this.InjectCommand(1);
        }

        public void InjectF4()
        {
            if (this._mugen_type != MugenWindow.MugenType_t.MUGEN_TYPE_WINMUGEN)
                this.InjectCommand(29);
            else
                this.InjectCommand(62);
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
                uint playerAddr = this._GetPlayerAddr(baseAddr, (uint)((ulong)this._addr_db.PLAYER_1_BASE_OFFSET + (ulong)(index * 4)));
                if (playerAddr != 0U && this.DoesExist(playerAddr))
                {
                    playerId[index] = this.GetPlayerId(playerAddr);
                    helperId[index] = this.GetHelperId(playerAddr);
                    stateno[index] = this.GetStateNo(playerAddr);
                    prevstateno[index] = this.GetPrevStateNo(playerAddr);
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
                uint playerAddr = this._GetPlayerAddr(baseAddr, (uint)((ulong)this._addr_db.PLAYER_1_BASE_OFFSET + (ulong)(index * 4)));
                if (playerAddr != 0U && this.DoesExist(playerAddr))
                {
                    this.mugenPlayerId[index] = this.GetPlayerId(playerAddr);
                    this.muenHelperId[index] = index < 4 ? -1 : this.GetHelperId(playerAddr);
                    this.muenParentId[index] = index < 4 ? -1 : this.GetParentId(playerAddr);
                    this.muenOwnerId[index] = this.GetStateOwner(baseAddr, playerAddr);
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
            uint explodListAdder = this.GetExplodListAdder(baseAddr);
            if (explodListAdder == 0U)
                return;
            uint playerAddr = 0;
            if (this._varInspectTargetNo > 0)
                playerAddr = this._GetPlayerAddr(baseAddr, (uint)((ulong)this._addr_db.PLAYER_1_BASE_OFFSET + (ulong)((this._varInspectTargetNo - 1) * 4)));
            int playerId = 0;
            if (playerAddr != 0U)
                playerId = this.GetPlayerId(playerAddr);
            for (int index = 0; index < 50; ++index)
            {
                uint explodAdder = this.GetExplodAdder(explodListAdder, (uint)(this._explodHeadNo + index));
                if (this.DoesExplodExist(explodAdder))
                {
                    int explodOwnerId = this.GetExplodOwnerId(explodAdder);
                    if (explodOwnerId >= this.player1Id)
                    {
                        this.mugenExplodId[index] = this.GetExplodId(explodAdder);
                        int animIndex = this.GetAnimIndex(explodAdder);
                        int rootId = this.GetRootId(baseAddr, explodOwnerId);
                        uint animListAddr = rootId != this.player1Id ? (rootId != this.player2Id ? (rootId != this.player3Id ? (rootId != this.player4Id ? this.player1AnimAddr : this.player4AnimAddr) : this.player3AnimAddr) : this.player2AnimAddr) : this.player1AnimAddr;
                        this.mugenOwnerId[index] = explodOwnerId;
                        this.mugenAnim[index] = this.GetAnimNo(animListAddr, animIndex);
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
            uint projBaseAdder = this.GetProjBaseAdder(playerAddr);
            if (projBaseAdder == 0U)
                return;
            uint projListAdder = this.GetProjListAdder(projBaseAdder);
            if (projListAdder == 0U)
                return;
            for (int index = 0; index < 50; ++index)
            {
                uint projAdder = this.GetProjAdder(projListAdder, (uint)(this._projHeadNo + index));
                if (this.DoesProjExist(projBaseAdder, this._projHeadNo + index, projAdder))
                {
                    this.mugenProjId[index] = this.GetProjId(projAdder);
                    this.mugenProjX[index] = this.GetProjX(playerAddr, projAdder);
                    this.mugenProjY[index] = this.GetProjY(playerAddr, projAdder);
                }
                else
                {
                    this.mugenProjId[index] = -1;
                    this.mugenProjX[index] = 0;
                    this.mugenProjY[index] = 0;
                }
            }
            if (DebugForm.MainObj() == null)
                return;
            this.BeginInvoke((Action)(() => DebugForm.MainObj().DisplayProjs(playerId, this._projHeadNo, 50, this.mugenProjId, this.mugenProjX, this.mugenProjY)))?.AsyncWaitHandle.WaitOne(this._invokeWaitTime);
        }

        /// <summary>
        /// update all of the data the current debug player.
        /// </summary>
        /// <param name="baseAddr"></param>
        private void UpdateVariables(uint baseAddr)
        {
            uint playerAddr = 0;
            if (this._varInspectTargetNo > 0)
                playerAddr = this._GetPlayerAddr(baseAddr, (uint)((ulong)this._addr_db.PLAYER_1_BASE_OFFSET + (ulong)((this._varInspectTargetNo - 1) * 4)));
            int playerId = 0;
            if (playerAddr != 0U)
                playerId = this.GetPlayerId(playerAddr);
            this.mugenSysInfo.alive = this.GetAlive(playerAddr);
            this.mugenSysInfo.life = this.GetLife(playerAddr);
            this.mugenSysInfo.palno = this.GetPalno(playerAddr);
            this.mugenSysInfo.hitpausetime = this.GetHitPauseTime(playerAddr);
            switch (this._varInspectTargetNo)
            {
                case 1:
                case 3:
                    this.mugenSysInfo.win = this.IsTeam1Win(baseAddr) ? 1 : 0;
                    this.mugenSysInfo.lose = this.IsTeam2Win(baseAddr) ? 1 : 0;
                    break;
                case 2:
                case 4:
                    this.mugenSysInfo.win = this.IsTeam2Win(baseAddr) ? 1 : 0;
                    this.mugenSysInfo.lose = this.IsTeam1Win(baseAddr) ? 1 : 0;
                    break;
                default:
                    this.mugenSysInfo.win = -1;
                    this.mugenSysInfo.lose = -1;
                    break;
            }
            this.mugenSysInfo.stateowner = this.GetStateOwner(baseAddr, playerAddr);
            this.mugenSysInfo.stateno = this.GetStateNo(playerAddr);
            this.mugenSysInfo.specialstateno = !this._isDebugBreakMode ? 0 : this.GetSpecialStateNo(playerAddr);
            this.mugenSysInfo.prevstateno = this.GetPrevStateNo(playerAddr);
            this.mugenSysInfo.roundstate = this.GetRoundState(baseAddr);
            this.mugenSysInfo.roundtime = this.GetRoundTime(baseAddr);
            this.mugenSysInfo.power = this.GetPower(playerAddr);
            this.mugenSysInfo.ctrl = this.GetCtrl(playerAddr);
            this.mugenSysInfo.damage = this.GetDamage(playerAddr);
            this.mugenSysInfo.statetype = this.GetStateType(playerAddr);
            this.mugenSysInfo.movetype = this.GetMoveType(playerAddr);
            this.mugenSysInfo.posx = this.GetPosX(baseAddr, playerAddr);
            this.mugenSysInfo.posy = this.GetPosY(baseAddr, playerAddr);
            this.mugenSysInfo.velx = this.GetVelX(baseAddr, playerAddr);
            this.mugenSysInfo.vely = this.GetVelY(baseAddr, playerAddr);
            this.mugenSysInfo.localx = this.GetLocalCoordX(playerAddr);
            this.mugenSysInfo.localy = this.GetLocalCoordY(playerAddr);
            this.mugenSysInfo.facing = this.GetFacing(playerAddr);
            this.mugenSysInfo.fall_damage = this.GetFallDamage(playerAddr);
            this.mugenSysInfo.movecontact = this.GetMoveContact(playerAddr);
            this.mugenSysInfo.movehit = this.GetMoveHit(playerAddr);
            this.mugenSysInfo.moveguarded = this.GetMoveGuarded(playerAddr);
            this.mugenSysInfo.movereversed = this.GetMoveReversed(playerAddr);
            this.mugenSysInfo.active = this.GetActiveFlag(baseAddr, this._varInspectTargetNo - 1);
            this.mugenSysInfo.assertFlags = this.GetGlobalAssertSpecials(baseAddr);
            this.mugenSysInfo.selfAssertFlags = this.GetSelfAssertSpecials(playerAddr);
            this.mugenSysInfo.noko = this.GetNoko(baseAddr);
            this.mugenSysInfo.intro = this.GetIntro(baseAddr);
            this.mugenSysInfo.roundnotover = this.GetRoundNotOver(baseAddr);
            this.mugenSysInfo.timerfreeze = this.GetTimerFreeze(baseAddr);
            this.mugenSysInfo.muteki = this.GetMuteki(playerAddr);
            this.mugenSysInfo.noclsn2 = this.GetNoClsn2Flag(playerAddr);
            this.mugenSysInfo.hasclsn1 = this.GetHasClsn1Flag(playerAddr) == 1;
            this.mugenSysInfo.hitby = this.GetHitBy(playerAddr);
            this.mugenSysInfo.hitoverride0 = this.GetHitOverRide(playerAddr, 0);
            this.mugenSysInfo.hitoverride1 = this.GetHitOverRide(playerAddr, 1);
            this.mugenSysInfo.hitoverride2 = this.GetHitOverRide(playerAddr, 2);
            this.mugenSysInfo.target1 = this.GetTarget(playerAddr, 0);
            this.mugenSysInfo.target2 = this.GetTarget(playerAddr, 1);
            this.mugenSysInfo.target3 = this.GetTarget(playerAddr, 2);
            this.mugenSysInfo.target4 = this.GetTarget(playerAddr, 3);
            this.mugenSysInfo.target5 = this.GetTarget(playerAddr, 4);
            this.mugenSysInfo.target6 = this.GetTarget(playerAddr, 5);
            this.mugenSysInfo.target7 = this.GetTarget(playerAddr, 6);
            this.mugenSysInfo.target8 = this.GetTarget(playerAddr, 7);
            this.mugenSysInfo.pausetime = this.GetPauseTime(baseAddr);
            this.mugenSysInfo.superpausetime = this.GetSuperPauseTime(baseAddr);
            this.mugenSysInfo.pausemovetime = this.GetPauseMoveTime(playerAddr);
            this.mugenSysInfo.superpausemovetime = this.GetSuperPauseMoveTime(playerAddr);
            this.mugenSysInfo.attackmulset = this.GetAttackMulSet(playerAddr);
            for (int index = 0; index < 5; ++index)
                this.mugenSysvar[index] = this.GetSysvar(playerAddr, index);
            for (int index = 0; index < 60; ++index)
                this.mugenVar[index] = this.GetVar(playerAddr, index);
            for (int index = 0; index < 5; ++index)
                this.mugenSysfvar[index] = this.GetSysfvar(playerAddr, index);
            for (int index = 0; index < 40; ++index)
                this.mugenFvar[index] = this.GetFvar(playerAddr, index);
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
            if (this.p == null)
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
            if (this.p == null)
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
                    num = this._GetPlayerAddr(baseAddr, this._addr_db.PLAYER_1_BASE_OFFSET);
                    rootAdder = num;
                    break;
                case TriggerCheckTarget.Player_t.PlayerType.PLAYER_P2:
                    num = this._GetPlayerAddr(baseAddr, this._addr_db.PLAYER_1_BASE_OFFSET + 4U);
                    rootAdder = num;
                    break;
                case TriggerCheckTarget.Player_t.PlayerType.PLAYER_P3:
                    num = this._GetPlayerAddr(baseAddr, this._addr_db.PLAYER_1_BASE_OFFSET + 8U);
                    rootAdder = num;
                    break;
                case TriggerCheckTarget.Player_t.PlayerType.PLAYER_P4:
                    num = this._GetPlayerAddr(baseAddr, this._addr_db.PLAYER_1_BASE_OFFSET + 12U);
                    rootAdder = num;
                    break;
                case TriggerCheckTarget.Player_t.PlayerType.PLAYER_PLAYERID:
                    num = this.GetPlayerAddrFromId(baseAddr, targetPlayer.pid);
                    rootAdder = this.GetPlayerAddrFromId(baseAddr, this.GetRootId(baseAddr, targetPlayer.pid));
                    break;
                case TriggerCheckTarget.Player_t.PlayerType.PLAYER_P1_HELPERID:
                    num = this.GetPlayerAddrFromHelperId(baseAddr, targetPlayer.pid, 0);
                    rootAdder = this.GetPlayerAddrFromId(baseAddr, this.GetRootId(baseAddr, this.GetPlayerId(num)));
                    break;
                case TriggerCheckTarget.Player_t.PlayerType.PLAYER_P2_HELPERID:
                    num = this.GetPlayerAddrFromHelperId(baseAddr, targetPlayer.pid, 1);
                    rootAdder = this.GetPlayerAddrFromId(baseAddr, this.GetRootId(baseAddr, this.GetPlayerId(num)));
                    break;
                case TriggerCheckTarget.Player_t.PlayerType.PLAYER_P3_HELPERID:
                    num = this.GetPlayerAddrFromHelperId(baseAddr, targetPlayer.pid, 2);
                    rootAdder = this.GetPlayerAddrFromId(baseAddr, this.GetRootId(baseAddr, this.GetPlayerId(num)));
                    break;
                case TriggerCheckTarget.Player_t.PlayerType.PLAYER_P4_HELPERID:
                    num = this.GetPlayerAddrFromHelperId(baseAddr, targetPlayer.pid, 3);
                    rootAdder = this.GetPlayerAddrFromId(baseAddr, this.GetRootId(baseAddr, this.GetPlayerId(num)));
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
            uint offs = TriggerDatabase.GetTriggerAddrForType(this._addr_db, triggerType, ref isOffsetFromBase);
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
            if (offs == this._addr_db.GAMETIME_BASE_OFFSET)
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
                    triggerValueT.SetInt32Value(this._GetInt32Data(addr, 0U));
                    break;
                case TriggerDatabase.ValueType.VALUE_FLOAT:
                    triggerValueT.valueType = TriggerDatabase.ValueType.VALUE_FLOAT;
                    triggerValueT.SetSingleValue(this._GetFloatData(addr, 0U));
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
            int playerID = this.GetPlayerId(targetAddr);
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
                int currTarget = this.GetTarget(this.GetPlayerAddrFromId(baseAddr, playerID), i);
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
                int targetID = this.GetTarget(this.GetPlayerAddrFromId(baseAddr, playerID), i);
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
            uint explodList = this.GetExplodListAdder(baseAddr);
            int num = 0;
            // iterate all proj
            // TODO: 256 chosen arbitrarily and probably not good enough for cheap. find a way to find the max number.
            for (uint i = 0; i < 256; i++)
            {
                // get proj
                uint explodAdder = this.GetExplodAdder(explodList, i);
                int explod = this.GetExplodId(explodAdder);
                // sum if ID matches
                if (explod == explodID && this.GetExplodOwnerId(explodAdder) == playerID) num++;
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
            uint projBase = this.GetProjBaseAdder(this.GetPlayerAddrFromId(baseAddr, playerID));
            uint projList = this.GetProjListAdder(projBase);
            // get max proj count
            int maxProj = this._GetInt32Data(projBase, this._addr_db.PROJ_MAX_PROJ_BASE_OFFSET);
            int num = 0;
            // iterate all proj
            for (uint i = 0; i < maxProj; i++)
            {
                // get proj
                uint projAdder = this.GetProjAdder(projList, i);
                int proj = this.GetProjId(projAdder);
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
                uint playerAddr = this._GetPlayerAddr(baseAddr, (uint)((ulong)this._addr_db.PLAYER_1_BASE_OFFSET + (ulong)(index * 4)));
                if (playerAddr != 0U && this.DoesExist(playerAddr) && playerID == this.GetRootId(baseAddr, this.GetPlayerId(playerAddr)))
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
                uint playerAddr = this._GetPlayerAddr(baseAddr, (uint)((ulong)this._addr_db.PLAYER_1_BASE_OFFSET + (ulong)(index * 4)));
                if (playerAddr != 0U && this.DoesExist(playerAddr) && playerID == this.GetRootId(baseAddr, this.GetPlayerId(playerAddr)) && helperID == this.GetHelperId(playerAddr))
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
            if (baseAddr == 0U || this.debugTargetThread == 0)
                return false;
            uint num = 0;
            TriggerCheckTarget.Player_t targetPlayer = this._triggerCheckTarget.GetTargetPlayer();
            // find base address to monitor based on PlayerType
            switch (targetPlayer.playerType)
            {
                case TriggerCheckTarget.Player_t.PlayerType.PLAYER_NONE:
                    return false;
                case TriggerCheckTarget.Player_t.PlayerType.PLAYER_P1:
                    num = this._GetPlayerAddr(baseAddr, this._addr_db.PLAYER_1_BASE_OFFSET);
                    break;
                case TriggerCheckTarget.Player_t.PlayerType.PLAYER_P2:
                    num = this._GetPlayerAddr(baseAddr, this._addr_db.PLAYER_1_BASE_OFFSET + 4U);
                    break;
                case TriggerCheckTarget.Player_t.PlayerType.PLAYER_P3:
                    num = this._GetPlayerAddr(baseAddr, this._addr_db.PLAYER_1_BASE_OFFSET + 8U);
                    break;
                case TriggerCheckTarget.Player_t.PlayerType.PLAYER_P4:
                    num = this._GetPlayerAddr(baseAddr, this._addr_db.PLAYER_1_BASE_OFFSET + 12U);
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
            uint offs = TriggerDatabase.GetTriggerAddrForType(this._addr_db, triggerType, ref isOffsetFromBase);
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
                this.__SetBreakPoint(targetAdder, this.debugTargetThread);
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
            if (MugenWindow.MainObj().ReadMemory((IntPtr)(long)this.watchAddr, ref buf, 4U) > 0)
                this.WatchInitVal = BitConverter.ToUInt32(buf, 0);
            else
                this.watchAddr = 0U;
        }

        /// <summary>
        /// sets a hardware breakpoint at the given address
        /// </summary>
        /// <param name="targetAdder"></param>
        /// <param name="threadId"></param>
        private void __SetBreakPoint(uint targetAdder, int threadId)
        {
            if (this.debugP == null)
                return;
            MugenWindow.CONTEXT context = new MugenWindow.CONTEXT();
            context.ContextFlags = 65552U;
            IntPtr num = IntPtr.Zero;
            try
            {
                num = MugenWindow.OpenThread(MugenWindow.ThreadAccessFlags.SUSPEND_RESUME | MugenWindow.ThreadAccessFlags.GET_CONTEXT | MugenWindow.ThreadAccessFlags.SET_CONTEXT | MugenWindow.ThreadAccessFlags.QUERY_INFORMATION, false, (uint)threadId);
                if (!(num != IntPtr.Zero) || MugenWindow.SuspendThreadEx(num) == -1)
                    return;
                if (MugenWindow.GetThreadContextEx(num, ref context))
                {
                    context.Dr0 = targetAdder;
                    context.Dr7 = 851969U;
                    MugenWindow.SetThreadContextEx(num, ref context);
                }
                MugenWindow.ResumeThreadEx(num);
            }
            finally
            {
                if (num != IntPtr.Zero)
                    MugenWindow.CloseHandle(num);
            }
        }

        private void _ClearBreakPoint()
        {
            if (this.debugTargetThread == 0)
                return;
            this.__ClearBreakPoint(this.debugTargetThread);
        }

        /// <summary>
        /// clears a set hardware breakpoint
        /// </summary>
        /// <param name="threadId"></param>
        private void __ClearBreakPoint(int threadId)
        {
            if (this.debugP == null)
                return;
            MugenWindow.CONTEXT context = new MugenWindow.CONTEXT();
            context.ContextFlags = 65552U;
            IntPtr num = IntPtr.Zero;
            try
            {
                num = MugenWindow.OpenThread(MugenWindow.ThreadAccessFlags.SUSPEND_RESUME | MugenWindow.ThreadAccessFlags.GET_CONTEXT | MugenWindow.ThreadAccessFlags.SET_CONTEXT | MugenWindow.ThreadAccessFlags.QUERY_INFORMATION, false, (uint)threadId);
                if (!(num != IntPtr.Zero) || MugenWindow.SuspendThreadEx(num) == -1)
                    return;
                if (MugenWindow.GetThreadContextEx(num, ref context))
                {
                    context.Dr0 = 0U;
                    context.Dr7 = 0U;
                    MugenWindow.SetThreadContextEx(num, ref context);
                }
                MugenWindow.ResumeThreadEx(num);
            }
            finally
            {
                if (num != IntPtr.Zero)
                    MugenWindow.CloseHandle(num);
            }
        }

        private uint _GetStackPointer()
        {
            if (this.debugP == null || this.debugTargetThread == 0)
                return 0;
            MugenWindow.CONTEXT context = new MugenWindow.CONTEXT();
            context.ContextFlags = 65541U;
            IntPtr num1 = IntPtr.Zero;
            uint num2 = 0;
            try
            {
                num1 = MugenWindow.OpenThread(MugenWindow.ThreadAccessFlags.GET_CONTEXT | MugenWindow.ThreadAccessFlags.QUERY_INFORMATION, false, (uint)this.debugTargetThread);
                if (num1 != IntPtr.Zero)
                {
                    if (MugenWindow.GetThreadContextEx(num1, ref context))
                    {
                        MugenWindow.LDT_ENTRY lpSelectorEntry = new MugenWindow.LDT_ENTRY();
                        if (MugenWindow.GetThreadSelectorEntryEx(num1, context.SegFs, ref lpSelectorEntry))
                            num2 = (uint)(this._GetInt32Data((uint)(lpSelectorEntry.HighWord.Bits.BaseHi << 24 | lpSelectorEntry.HighWord.Bits.BaseMid << 16) | (uint)lpSelectorEntry.BaseLow, 4U) - 8192);
                    }
                }
            }
            finally
            {
                if (num1 != IntPtr.Zero)
                    MugenWindow.CloseHandle(num1);
            }
            return num2;
        }

        /// <summary>
        /// main loop for the monitor function. this function is responsible for handling most data reads, updates, breakpoints, etc.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mugenWatcher_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker backgroundWorker = (BackgroundWorker)sender;
            LogManager logManager = LogManager.MainObj();
            DebugForm debugForm = DebugForm.MainObj();
            uint baseAddr = 0;
            uint playerAddr1 = 0;
            uint playerAddr2 = 0;
            uint playerAddr3 = 0;
            uint playerAddr4 = 0;
            bool flag1 = false;
            bool flag2 = false;
            bool flag3 = false;
            bool flag4 = false;
            bool flag5 = false;
            bool flag6 = false;
            bool flag7 = false;
            bool flag8 = false;
            int num1 = 0;
            int num2 = 0;
            int trueTeam1WinCount = 0;
            int trueTeam2WinCount = 0;
            int num3 = 0;
            int num4 = 0;
            int drawRounds = 0;
            string displayName1 = "";
            string displayName2 = (string)null;
            string displayName3 = (string)null;
            string displayName4 = (string)null;
            string displayName5 = (string)null;
            int palnoA1 = 0;
            int palnoA2 = 0;
            int palnoB1 = 0;
            int palnoB2 = 0;
            int num5 = 300;
            int num6 = 0;
            uint num7 = 0;
            int num8 = 0;
            long num9 = 0;
            long num10 = 0;
            long num11 = 0;
            long num12 = 0;
            long num13 = 0;
            long num14 = 0;
            long num15 = 0;
            long num16 = 0;
            int num17 = 0;
            int num18 = 0;
            int num19 = 2;
            NativeEvent nativeEvent1 = (NativeEvent)null;
            // this is used in comparisons
            TriggerDatabase.TriggerValue_t triggerValueT = new TriggerDatabase.TriggerValue_t();
            // will represent the thread we check triggers against/set bps in
            this.debugTargetThread = 0;
            // this is false by default+we set to true if mugen is really active
            this._isMugenActive = false;
            while (!backgroundWorker.CancellationPending)
            {
                // attach the debug process to the Mugen process if triggers are set up and the next command is START
                if (this.debugP == null && this._triggerCheckTarget != null && (this._triggerCheckTarget.IsDirty() && this._triggerCheckTarget.GetNextCommand() == TriggerCheckTarget.CheckCommand.CHECKCMD_START))
                    this.debugP = this.debugControl.Attach(this.p.Id);
                // check trigger values and apply breaks here if needed
                // there may already be a hw break in effect/coming in, so it's identified in this block
                if (this.debugP != null && !this._isDebugBreakMode)
                {
                    NativeEvent nativeEvent2 = this.debugControl.WaitForDebugEvent(16);
                    // handle bp event if it was found
                    if (nativeEvent2 != null)
                    {
                        // safety check if it's our hw bp that activated
                        nativeEvent2.Process.HandleIfLoaderBreakpoint(nativeEvent2);
                        if (nativeEvent2 is ExceptionNativeEvent)
                        {
                            switch (nativeEvent2.m_union.Exception.ExceptionRecord.ExceptionCode)
                            {
                                // these cases correspond to hardware bp?
                                case ExceptionCode.STATUS_WOW64_SINGLESTEP:
                                case ExceptionCode.STATUS_SINGLESTEP:
                                    // check if read value type matches expected type + is in range/matching
                                    bool triggerValueValid = false;
                                    // read current value
                                    TriggerDatabase.TriggerValue_t triggerValue = this.GetTriggerValue(baseAddr);
                                    // make sure not empty
                                    if (triggerValue != null && !triggerValueT.isEqual(triggerValue))
                                    {
                                        triggerValueT = triggerValue;
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
                                                        nativeEvent1 = nativeEvent2;
                                                        break;
                                                    }
                                                    break;
                                                }
                                                if (targetValueFrom1.valueType == TriggerDatabase.ValueType.VALUE_ANY)
                                                {
                                                    triggerValueValid = true;
                                                    this._isDebugBreakMode = true;
                                                    nativeEvent1 = nativeEvent2;
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
                                                        nativeEvent1 = nativeEvent2;
                                                        break;
                                                    }
                                                    break;
                                                }
                                                if (targetValueFrom1.valueType == TriggerDatabase.ValueType.VALUE_ANY)
                                                {
                                                    triggerValueValid = true;
                                                    this._isDebugBreakMode = true;
                                                    nativeEvent1 = nativeEvent2;
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
                                                        nativeEvent1 = nativeEvent2;
                                                        break;
                                                    }
                                                    break;
                                                }
                                                if (targetValueFrom2.valueType == TriggerDatabase.ValueType.VALUE_ANY)
                                                {
                                                    triggerValueValid = true;
                                                    this._isDebugBreakMode = true;
                                                    nativeEvent1 = nativeEvent2;
                                                    break;
                                                }
                                                break;
                                        }
                                    }
                                    uint p1Addr = this.GetP1Addr(baseAddr);
                                    // for un-stopping mugen from trigger breakpoints
                                    if (!triggerValueValid || !flag6 || (p1Addr == 0U || this._triggerCheckTarget.GetNextCommand() == TriggerCheckTarget.CheckCommand.CHECKCMD_STOP))
                                    {
                                        this.debugControl.ContinueEvent(nativeEvent2, false);
                                        this.BeginInvoke((Action)(() => DebugForm.MainObj().DisableTriggerCheckResumeButton()));
                                        if (p1Addr == 0U)
                                            this.BeginInvoke((Action)(() => DebugForm.MainObj().FinalizeTriggerCheck(this._mugen_type)));
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
                                    this.debugControl.ContinueEvent(nativeEvent2, false);
                                    break;
                                default:
                                    this.debugControl.ContinueEvent(nativeEvent2, false);
                                    if (this.debugP != null)
                                    {
                                        try
                                        {
                                            this.debugControl.Detach(this.debugP);
                                            this.debugP.Dispose();
                                        }
                                        catch
                                        {
                                        }
                                        this.debugP = (NativeDbgProcess)null;
                                    }
                                    if (this.p != null && !this.p.HasExited)
                                    {
                                        this.p.Kill();
                                        break;
                                    }
                                    break;
                            }
                        }
                        // boilerplate
                        else if (nativeEvent2 is CreateProcessDebugEvent)
                        {
                            this.debugControl.ContinueEvent(nativeEvent2);
                            this.debugTargetThread = nativeEvent2.ThreadId;
                            this._debugSpPointer = this._GetStackPointer();
                        }
                        else if (nativeEvent2 is ExitProcessDebugEvent)
                        {
                            this.debugControl.ContinueEvent(nativeEvent2);
                            try
                            {
                                this.debugControl.Detach(this.debugP);
                                this.debugP.Dispose();
                            }
                            catch
                            {
                            }
                            this.debugP = (NativeDbgProcess)null;
                        }
                        else
                            this.debugControl.ContinueEvent(nativeEvent2);
                    }
                }
                // check the status of the trigger check/apply next command
                else if (this.watchAddr == 0U)
                {
                    Thread.Sleep(16);
                    if (this.debugP != null)
                    {
                        if (this._stopDebugBreakFlag || this._triggerCheckTarget.GetNextCommand() == TriggerCheckTarget.CheckCommand.CHECKCMD_RESUME || this._triggerCheckTarget.GetNextCommand() == TriggerCheckTarget.CheckCommand.CHECKCMD_STOP)
                        {
                            if (this._triggerCheckTarget.GetNextCommand() != TriggerCheckTarget.CheckCommand.CHECKCMD_STOP && this._triggerCheckTarget.GetCurrentMode() != TriggerCheckTarget.CheckMode.CHECKMODE_STOPPED)
                            {
                                this._triggerCheckTarget.SetNextCommand(TriggerCheckTarget.CheckCommand.CHECKCMD_NONE);
                                this._triggerCheckTarget.SetCurrentMode(TriggerCheckTarget.CheckMode.CHECKMODE_STARTED);
                                this._triggerCheckTarget.ResetDirty();
                                if (nativeEvent1 != null)
                                {
                                    this.debugControl.ContinueEvent(nativeEvent1, false);
                                    nativeEvent1 = (NativeEvent)null;
                                }
                            }
                            else if (this._triggerCheckTarget.GetNextCommand() == TriggerCheckTarget.CheckCommand.CHECKCMD_STOP && this._triggerCheckTarget.GetCurrentMode() != TriggerCheckTarget.CheckMode.CHECKMODE_STOPPED)
                            {
                                this._triggerCheckTarget.SetNextCommand(TriggerCheckTarget.CheckCommand.CHECKCMD_NONE);
                                this._triggerCheckTarget.SetCurrentMode(TriggerCheckTarget.CheckMode.CHECKMODE_STOPPED);
                                this._triggerCheckTarget.ResetDirty();
                                if (nativeEvent1 != null)
                                {
                                    this.debugControl.ContinueEvent(nativeEvent1, false);
                                    nativeEvent1 = (NativeEvent)null;
                                }
                                this.BeginInvoke((Action)(() => DebugForm.MainObj().DisableTriggerCheckResumeButton()));
                                this._ClearBreakPoint();
                                this.BeginInvoke((Action)(() => DebugForm.MainObj().EnableTriggerCheckStartButton()))?.AsyncWaitHandle.WaitOne(this._invokeWaitTime);
                            }
                            else if (nativeEvent1 != null)
                            {
                                this.debugControl.ContinueEvent(nativeEvent1, false);
                                nativeEvent1 = (NativeEvent)null;
                            }
                            this._stopDebugBreakFlag = false;
                            if (this._triggerCheckTarget.GetCurrentMode() != TriggerCheckTarget.CheckMode.CHECKMODE_STOPPED)
                                this.BeginInvoke((Action)(() => DebugForm.MainObj().EnableTriggerCheckStopButton()))?.AsyncWaitHandle.WaitOne(this._invokeWaitTime);
                            this.BeginInvoke((Action)(() => this.DelayedActivate()));
                            this._isDebugBreakMode = false;
                        }
                        else if (this._triggerCheckTarget.GetNextCommand() == TriggerCheckTarget.CheckCommand.CHECKCMD_NONE && this._triggerCheckTarget.GetCurrentMode() == TriggerCheckTarget.CheckMode.CHECKMODE_STOPPED && nativeEvent1 != null)
                        {
                            this.debugControl.ContinueEvent(nativeEvent1, false);
                            nativeEvent1 = (NativeEvent)null;
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
                    if (MugenWindow.MainObj().ReadMemory((IntPtr)(long)this.watchAddr, ref buf, 4U) > 0)
                        num20 = BitConverter.ToUInt32(buf, 0);
                    if ((int)this.WatchInitVal != (int)num20)
                    {
                        bool isValidTrigger = false;
                        TriggerDatabase.TriggerValue_t triggerValue = this.GetTriggerValue(baseAddr);
                        // everything below here is very similar to hw bp above
                        if (triggerValue != null && !triggerValueT.isEqual(triggerValue))
                        {
                            triggerValueT = triggerValue;
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
                                            this.SetPaused(true);
                                            break;
                                        }
                                        break;
                                    }
                                    if (targetValueFrom1.valueType == TriggerDatabase.ValueType.VALUE_ANY)
                                    {
                                        this.SetPaused(true);
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
                                            this.SetPaused(true);
                                            break;
                                        }
                                        break;
                                    }
                                    if (targetValueFrom1.valueType == TriggerDatabase.ValueType.VALUE_ANY)
                                    {
                                        this.SetPaused(true);
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
                                            this.SetPaused(true);
                                            break;
                                        }
                                        break;
                                    }
                                    if (targetValueFrom2.valueType == TriggerDatabase.ValueType.VALUE_ANY)
                                    {
                                        this.SetPaused(true);
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
                if (this.p != null && !this.p.HasExited)
                {
                    DateTime now;
                    // failsafe against unending RoundState=4, kills mugen if detected
                    if (num16 != 0L)
                    {
                        MugenProfile profile = ProfileManager.MainObj().GetProfile(this._workingProfileId);
                        if (profile != null && profile.IsAutoMode())
                        {
                            long maxRoundState4Time = (long)profile.GetMaxRoundState4Time();
                            now = DateTime.Now;
                            long num20 = now.Ticks / 10000000L;
                            if (maxRoundState4Time > 0L && num20 > num16 + maxRoundState4Time)
                            {
                                num16 = 0L;
                                if (this.p != null && !this.p.HasExited)
                                    this.p.Kill();
                            }
                        }
                    }
                    // other checks
                    if (this.p != null && !this.p.HasExited)
                    {
                        if (flag7 || !flag6 && this._isActivatedOnce)
                        {
                            IAsyncResult asyncResult = this.BeginInvoke((Action)(() => LogManager.MainObj().IsAvailable()));
                            if (asyncResult != null && this.watchAddr == 0U)
                            {
                                if (!asyncResult.AsyncWaitHandle.WaitOne(5000))
                                {
                                    ++num17;
                                    if (num17 > 1)
                                    {
                                        switch (this._triggerCheckTarget.GetCurrentMode())
                                        {
                                            case TriggerCheckTarget.CheckMode.CHECKMODE_STOPPED:
                                            case TriggerCheckTarget.CheckMode.CHECKMODE_STARTED:
                                                this.p.Kill();
                                                break;
                                            case TriggerCheckTarget.CheckMode.CHECKMODE_SUSPENDED:
                                                this._triggerCheckTarget.SetNextCommand(TriggerCheckTarget.CheckCommand.CHECKCMD_STOP);
                                                this._triggerCheckTarget.SetCurrentMode(TriggerCheckTarget.CheckMode.CHECKMODE_STARTED);
                                                this._triggerCheckTarget.ResetDirty();
                                                if (nativeEvent1 != null)
                                                {
                                                    this.debugControl.ContinueEvent(nativeEvent1, false);
                                                    nativeEvent1 = (NativeEvent)null;
                                                }
                                                this._stopDebugBreakFlag = false;
                                                this.BeginInvoke((Action)(() => DebugForm.MainObj().EnableTriggerCheckStartButton()));
                                                this.p.Kill();
                                                this._isDebugBreakMode = false;
                                                break;
                                        }
                                    }
                                }
                                else
                                    num17 = 0;
                            }
                        }
                        // data loading segment?
                        if (baseAddr == 0U)
                            baseAddr = this.GetBaseAddr();
                        if (baseAddr != 0U && !this.IsDemo(baseAddr))
                        {
                            if (baseAddr != 0U)
                            {
                                if (this._flagDumpPlayers)
                                {
                                    this._flagDumpPlayers = false;
                                    this._DumpPlayers(baseAddr);
                                    if (flag6 | flag7)
                                    {
                                        flag6 = false;
                                        flag7 = false;
                                        MugenProfile profile = ProfileManager.MainObj().GetProfile(this._workingProfileId);
                                        if (!this.IsTurnsMode(baseAddr) && profile != null && !profile.IsQuickMode() || profile != null && profile.IsAutoMode())
                                        {
                                            this.UpdatePlayerDataEx(displayName2, palnoA1, displayName4, palnoB1, 0, 0, 0, 1, 0, trueTeam1WinCount, num3, trueTeam2WinCount, num4, drawRounds, 1, 0);
                                            this.UpdatePlayerDataEx(displayName3, palnoA2, displayName5, palnoB2, 0, 0, 0, 1, 0, trueTeam2WinCount, num4, trueTeam1WinCount, num3, drawRounds, 1, 0);
                                            break;
                                        }
                                    }
                                }
                                if (this.p != null && !this.p.HasExited && num19 > 0)
                                {
                                    now = DateTime.Now;
                                    if (now.Ticks / 10000000L % 2L == 0L)
                                    {
                                        this.SetForegroundWindowEx(this.p.MainWindowHandle);
                                        if (this.IsMugenActive(baseAddr))
                                            --num19;
                                    }
                                }
                                playerAddr1 = this.GetP1Addr(baseAddr);
                                if (playerAddr1 == 0U)
                                {
                                    flag1 = false;
                                    if (flag6 | flag7)
                                    {
                                        flag6 = false;
                                        flag7 = false;
                                        int num20 = this.IsSpeedMode(baseAddr) ? 1 : 0;
                                        bool flag9 = this.IsSkipMode(baseAddr);
                                        if (num20 != 0)
                                            this.SetSpeedMode(baseAddr, false);
                                        if (flag9)
                                            this.SetSkipMode(baseAddr, false);
                                        if (!this.IsTurnsMode(baseAddr))
                                        {
                                            this._isGameQuitted = true;
                                            if (logManager != null)
                                                AsyncAppendLog("The match has been cancelled.");
                                            MugenProfile profile = ProfileManager.MainObj().GetProfile(this._workingProfileId);
                                            if (profile != null && !profile.IsQuickMode())
                                            {
                                                this.UpdatePlayerDataEx(displayName2, palnoA1, displayName4, palnoB1, 0, 0, 0, 0, 1, trueTeam1WinCount, num3, trueTeam2WinCount, num4, drawRounds, 0, 1);
                                                this.UpdatePlayerDataEx(displayName3, palnoA2, displayName5, palnoB2, 0, 0, 0, 0, 1, trueTeam2WinCount, num4, trueTeam1WinCount, num3, drawRounds, 0, 1);
                                            }
                                        }
                                    }
                                }
                                if (playerAddr1 == 0U || this.player1Id != this.GetPlayerId(playerAddr1))
                                {
                                    flag2 = false;
                                    this.player1AnimAddr = 0U;
                                    if (num9 == 0L)
                                    {
                                        now = DateTime.Now;
                                        num9 = now.Ticks / 10000000L;
                                    }
                                    MugenProfile profile = ProfileManager.MainObj().GetProfile(this._workingProfileId);
                                    if (profile != null && profile.IsAutoMode())
                                    {
                                        long num20 = (long)(profile.GetMaxRoundState1Time() * 2);
                                        now = DateTime.Now;
                                        long num21 = now.Ticks / 10000000L;
                                        if (num20 > 0L && num21 > num9 + num20 && !this._isMugenFrozen)
                                        {
                                            this._isMugenFrozen = true;
                                            if (logManager != null)
                                                AsyncAppendLog("Loading the characters is taking too long.");
                                        }
                                    }
                                }
                                playerAddr2 = this.GetP2Addr(baseAddr);
                                if (playerAddr2 == 0U || this.player2Id != this.GetPlayerId(playerAddr2))
                                {
                                    flag3 = false;
                                    this.player2AnimAddr = 0U;
                                    if (num10 == 0L)
                                    {
                                        now = DateTime.Now;
                                        num10 = now.Ticks / 10000000L;
                                    }
                                    MugenProfile profile = ProfileManager.MainObj().GetProfile(this._workingProfileId);
                                    if (profile != null && profile.IsAutoMode())
                                    {
                                        long num20 = (long)(profile.GetMaxRoundState1Time() * 2);
                                        now = DateTime.Now;
                                        long num21 = now.Ticks / 10000000L;
                                        if (num20 > 0L && num21 > num10 + num20 && !this._isMugenFrozen)
                                        {
                                            this._isMugenFrozen = true;
                                            if (logManager != null)
                                                AsyncAppendLog("Loading the characters is taking too long.");
                                        }
                                    }
                                }
                                playerAddr3 = this.GetP3Addr(baseAddr);
                                if (playerAddr3 == 0U || this.player3Id != this.GetPlayerId(playerAddr3))
                                {
                                    flag4 = false;
                                    this.player3AnimAddr = 0U;
                                }
                                playerAddr4 = this.GetP4Addr(baseAddr);
                                if (playerAddr4 == 0U || this.player4Id != this.GetPlayerId(playerAddr4))
                                {
                                    flag5 = false;
                                    this.player4AnimAddr = 0U;
                                }
                            }
                            if (playerAddr1 != 0U && !flag1 && this.GetDisplayName(playerAddr1, ref displayName1) > 0)
                            {
                                flag1 = true;
                                this._invokeWaitTime = 20;
                                if (this.p != null)
                                {
                                    this.BeginInvoke((Action)(() => DebugForm.MainObj().InitPlayers(60)))?.AsyncWaitHandle.WaitOne(this._invokeWaitTime);
                                    this.BeginInvoke((Action)(() => DebugForm.MainObj().InitExplods(50)))?.AsyncWaitHandle.WaitOne(this._invokeWaitTime);
                                    this.BeginInvoke((Action)(() => DebugForm.MainObj().InitProjs(50)))?.AsyncWaitHandle.WaitOne(this._invokeWaitTime);
                                    this.BeginInvoke((Action)(() => DebugForm.MainObj().InitTriggerCheck(this._mugen_type)))?.AsyncWaitHandle.WaitOne(this._invokeWaitTime);
                                    if (this._triggerCheckTarget.GetCurrentMode() == TriggerCheckTarget.CheckMode.CHECKMODE_STARTED)
                                        this.BeginInvoke((Action)(() => this.StartTriggerCheckMode()))?.AsyncWaitHandle.WaitOne(this._invokeWaitTime);
                                }
                                trueTeam1WinCount = 0;
                                trueTeam2WinCount = 0;
                                num3 = 0;
                                num4 = 0;
                                this._timeOverCount1 = 0;
                                this._timeOverCount2 = 0;
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
                            if (playerAddr1 != 0U && !flag2)
                            {
                                num9 = 0L;
                                if (this.GetDisplayName(playerAddr1, ref displayName2) > 0)
                                {
                                    flag2 = true;
                                    this.player1Id = this.GetPlayerId(playerAddr1);
                                    this.player1AnimAddr = this.GetAnimListAddr(baseAddr, playerAddr1);
                                    palnoA1 = this.GetPalno(playerAddr1);
                                    string msg = "P1 side: " + displayName2;
                                    msg = msg + " (" + palnoA1.ToString() + "p)";
                                    this.BeginInvoke((Action)(() => DebugForm.MainObj().EnablePlayer(1)));
                                    if (logManager != null)
                                        AsyncAppendLog(msg);
                                }
                                else
                                    displayName2 = (string)null;
                            }
                            if (playerAddr3 != 0U && !flag4)
                            {
                                if (this.GetDisplayName(playerAddr3, ref displayName4) > 0)
                                {
                                    flag4 = true;
                                    this.player3Id = this.GetPlayerId(playerAddr3);
                                    this.player3AnimAddr = this.GetAnimListAddr(baseAddr, playerAddr3);
                                    palnoB1 = this.GetPalno(playerAddr3);
                                    string msg = "P1 side: " + displayName4;
                                    msg = msg + " (" + palnoB1.ToString() + "p)";
                                    this.BeginInvoke((Action)(() => DebugForm.MainObj().EnablePlayer(3)));
                                    if (logManager != null)
                                        AsyncAppendLog(msg);
                                }
                                else
                                    displayName4 = (string)null;
                            }
                            if (playerAddr2 != 0U && !flag3)
                            {
                                num10 = 0L;
                                if (this.GetDisplayName(playerAddr2, ref displayName3) > 0)
                                {
                                    flag3 = true;
                                    this.player2Id = this.GetPlayerId(playerAddr2);
                                    this.player2AnimAddr = this.GetAnimListAddr(baseAddr, playerAddr2);
                                    palnoA2 = this.GetPalno(playerAddr2);
                                    string msg = "P2 side: " + displayName3;
                                    msg = msg + " (" + palnoA2.ToString() + "p)";
                                    this.BeginInvoke((Action)(() => DebugForm.MainObj().EnablePlayer(2)));
                                    if (logManager != null)
                                        AsyncAppendLog(msg);
                                }
                                else
                                    displayName3 = (string)null;
                            }
                            if (playerAddr4 != 0U && !flag5)
                            {
                                if (this.GetDisplayName(playerAddr4, ref displayName5) > 0)
                                {
                                    flag5 = true;
                                    this.player4Id = this.GetPlayerId(playerAddr4);
                                    this.player4AnimAddr = this.GetAnimListAddr(baseAddr, playerAddr4);
                                    palnoB2 = this.GetPalno(playerAddr4);
                                    string msg = "P2 side: " + displayName5;
                                    msg = msg + " (" + palnoB2.ToString() + "p)";
                                    this.BeginInvoke((Action)(() => DebugForm.MainObj().EnablePlayer(4)));
                                    if (logManager != null)
                                        AsyncAppendLog(msg);
                                }
                                else
                                    displayName5 = (string)null;
                            }
                            if (this._isMugenActive != this.IsMugenActive(baseAddr))
                            {
                                this._isMugenActive = this.IsMugenActive(baseAddr);
                                if (this.p != null)
                                    this.BeginInvoke((Action)(() => this.SetTitleActive(this._isMugenActive)))?.AsyncWaitHandle.WaitOne(this._invokeWaitTime);
                            }
                            bool currentDebugMode = this.IsDebugMode(baseAddr);
                            if (this._isDebugModeChanged)
                            {
                                this._isDebugModeChanged = false;
                                if (currentDebugMode != this._isDebugMode)
                                    this.SetDebugMode(baseAddr, this._isDebugMode);
                            }
                            else if (currentDebugMode != this._isDebugMode)
                            {
                                this._isDebugMode = currentDebugMode;
                                if (this.p != null)
                                    this.BeginInvoke((Action)(() => debugForm.SetDebugModeCheckBox(currentDebugMode, false)))?.AsyncWaitHandle.WaitOne(this._invokeWaitTime);
                            }
                            if (this._isDebugMode)
                            {
                                if (this._varInspectTargetPlayer > 0)
                                {
                                    int playerNoFromId = this.GetPlayerNoFromId(baseAddr, this._varInspectTargetPlayer);
                                    if (playerNoFromId >= 0)
                                    {
                                        this._varInspectTargetNo = playerNoFromId + 1;
                                        this._varInspectTargetPlayer = 0;
                                    }
                                }
                                if (this._debugTargetPlayer > 0)
                                {
                                    int playerNoFromId = this.GetPlayerNoFromId(baseAddr, this._debugTargetPlayer);
                                    if (playerNoFromId >= 0)
                                    {
                                        this._debugTargetNo = playerNoFromId + 1;
                                        this._debugTargetPlayer = 0;
                                        if (this._debugTargetNo != this.GetDebugTargetNo(baseAddr))
                                        {
                                            this.SetDebugTargetNo(baseAddr, this._debugTargetNo);
                                            if (this.p != null)
                                                debugForm.SetDebugTargetNo(this._debugTargetNo);
                                        }
                                    }
                                }
                                if (this._isDebugTargetNoChanged)
                                {
                                    this._isDebugTargetNoChanged = false;
                                    this.SetDebugTargetNo(baseAddr, this._debugTargetNo);
                                }
                                else if (this._debugTargetNo == 0)
                                {
                                    this._debugTargetNo = 1;
                                    this._varInspectTargetNo = 1;
                                    this.SetDebugTargetNo(baseAddr, this._debugTargetNo);
                                }
                                else
                                {
                                    int debugNo = this.GetDebugTargetNo(baseAddr);
                                    if (this._debugTargetNo != debugNo)
                                    {
                                        this._debugTargetNo = debugNo;
                                        if (this.p != null)
                                            this.BeginInvoke((Action)(() => debugForm.SetDebugTargetNo(debugNo)))?.AsyncWaitHandle.WaitOne(this._invokeWaitTime);
                                    }
                                }
                            }
                            if (this._isDebugMode)
                            {
                                if (this._debugColor != DebugColor.NONE)
                                {
                                    this._debugColorChanged = false;
                                    this.SetDebugColor(baseAddr, this._debugColor);
                                }
                                else if (this._debugColorChanged)
                                {
                                    this._debugColorChanged = false;
                                    this.SetDebugColor(baseAddr, DebugColor.NONE);
                                }
                            }
                            if (this.debugP != null && this._triggerCheckTarget != null && (this._triggerCheckTarget.IsDirty() && this.watchAddr == 0U))
                            {
                                triggerValueT.reset();
                                if (this._triggerCheckTarget.GetNextCommand() == TriggerCheckTarget.CheckCommand.CHECKCMD_START)
                                {
                                    if (this._SetBreakPoint(baseAddr))
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
                                    this._ClearBreakPoint();
                                    this.BeginInvoke((Action)(() => DebugForm.MainObj().EnableTriggerCheckStartButton()))?.AsyncWaitHandle.WaitOne(this._invokeWaitTime);
                                }
                            }
                            ++num8;
                            if (num8 >= 1)
                            {
                                num8 = 0;
                                if (this._isDebugMode)
                                {
                                    // setup values for various debug form views
                                    switch (this._debugListMode)
                                    {
                                        case MugenWindow.DebugListMode.PLAYER_LIST_MODE:
                                            this.ListUpPlayers(baseAddr);
                                            break;
                                        case MugenWindow.DebugListMode.EXPLOD_LIST_MODE:
                                            this.ListUpExplods(baseAddr);
                                            break;
                                        case MugenWindow.DebugListMode.PROJ_LIST_MODE:
                                            switch (this._projOwner)
                                            {
                                                case MugenWindow.ProjOwner.P1:
                                                    this.ListUpProjs(this.player1Id, playerAddr1);
                                                    break;
                                                case MugenWindow.ProjOwner.P2:
                                                    this.ListUpProjs(this.player2Id, playerAddr2);
                                                    break;
                                                case MugenWindow.ProjOwner.P3:
                                                    this.ListUpProjs(this.player3Id, playerAddr3);
                                                    break;
                                                case MugenWindow.ProjOwner.P4:
                                                    this.ListUpProjs(this.player4Id, playerAddr4);
                                                    break;
                                            }
                                            break;
                                        default:
                                            this.ListUpPlayers(baseAddr);
                                            break;
                                    }
                                    this.UpdateVariables(baseAddr);
                                }
                            }
                            if (flag1)
                            {
                                // handling for long RoundState=0,4
                                int roundState1 = this.GetRoundState(baseAddr);
                                if (roundState1 == 0)
                                {
                                    this._invokeWaitTime = 20;
                                    num1 = this.GetTeam1WinCount(baseAddr);
                                    num2 = this.GetTeam2WinCount(baseAddr);
                                    drawRounds = this.GetDrawGameCount(baseAddr);
                                    num7 = this.GetGametime(baseAddr);
                                    int num20 = this.IsSpeedMode(baseAddr) ? 1 : 0;
                                    bool flag9 = this.IsSkipMode(baseAddr);
                                    int num21 = this._isSpeedMode ? 1 : 0;
                                    if (num20 != num21)
                                        this.SetSpeedMode(baseAddr, this._isSpeedMode);
                                    if (flag9 != this._isSkipMode)
                                        this.SetSkipMode(baseAddr, this._isSkipMode);
                                    if (num11 == 0L)
                                    {
                                        now = DateTime.Now;
                                        num11 = now.Ticks / 10000000L;
                                    }
                                    MugenProfile profile = ProfileManager.MainObj().GetProfile(this._workingProfileId);
                                    if (profile != null && profile.IsAutoMode())
                                    {
                                        long maxRoundState1Time = (long)profile.GetMaxRoundState1Time();
                                        now = DateTime.Now;
                                        long num22 = now.Ticks / 10000000L;
                                        if (maxRoundState1Time > 0L && num22 > num11 + maxRoundState1Time && !this._isMugenFrozen)
                                        {
                                            this._isMugenFrozen = true;
                                            if (logManager != null)
                                                AsyncAppendLog("Startup is taking too long.");
                                        }
                                    }
                                }
                                else if (flag6 || roundState1 == 4)
                                {
                                    if (!this.IsPaused() && !this._isDebugBreakMode)
                                    {
                                        ++num6;
                                        if (num6 == num5)
                                        {
                                            if ((int)this.GetGametime(baseAddr) - (int)num7 == 0)
                                            {
                                                this._isMugenFrozen = true;
                                                this._invokeWaitTime = 10;
                                            }
                                            num6 = 0;
                                            num7 = this.GetGametime(baseAddr);
                                        }
                                    }
                                    else
                                        num6 = 0;
                                    if (this.p != null)
                                        this.BeginInvoke((Action)(() => debugForm.SetPauseCheckBox(this.IsPaused())))?.AsyncWaitHandle.WaitOne(this._invokeWaitTime);
                                    if (this.IsPaused() && this._isStepMode)
                                    {
                                        now = DateTime.Now;
                                        this._stepModeCounter = now.Ticks / 10000L;
                                        if (this._stepModeCounter - this._stepModeLastCounter > this._stepModeInterval)
                                        {
                                            this._stepModeLastCounter = this._stepModeCounter;
                                            this.InjectStepCommand();
                                        }
                                    }
                                    bool currentSpeedMode = this.IsSpeedMode(baseAddr);
                                    bool flag9 = this.IsSkipMode(baseAddr);
                                    int skipFrames = this.GetSkipFrames(baseAddr);
                                    if (roundState1 < 3)
                                    {
                                        if (this._isSpeedModeChanged)
                                        {
                                            this._isSpeedModeChanged = false;
                                            if (currentSpeedMode != this._isSpeedMode)
                                            {
                                                this.SetSpeedMode(baseAddr, this._isSpeedMode);
                                                this._wasSpeedModeChanged = true;
                                            }
                                        }
                                        else if (currentSpeedMode != this._isSpeedMode)
                                        {
                                            if (this._wasSpeedModeChanged)
                                            {
                                                this.SetSpeedMode(baseAddr, this._isSpeedMode);
                                                this._wasSpeedModeChanged = false;
                                            }
                                            else
                                            {
                                                this._isSpeedMode = currentSpeedMode;
                                                if (this.p != null)
                                                    this.BeginInvoke((Action)(() => debugForm.SetSpeedModeCheckBox(currentSpeedMode, false)))?.AsyncWaitHandle.WaitOne(this._invokeWaitTime);
                                            }
                                        }
                                        if (flag9 != this._isSkipMode || flag9 && skipFrames != this._skipModeFrames)
                                            this.SetSkipMode(baseAddr, this._isSkipMode);
                                    }
                                }
                                // handler for checking if Mugen crashed or froze during RoundState=[1,3]
                                // as well as data read/load/update
                                int roundState2 = this.GetRoundState(baseAddr);
                                if (roundState2 >= 1 && roundState2 <= 3 && (flag2 & flag3 && !this._isMugenCrashed))
                                {
                                    if (!flag6)
                                    {
                                        flag6 = true;
                                        num11 = 0L;
                                        num12 = 0L;
                                        num13 = 0L;
                                        num14 = 0L;
                                        num15 = 0L;
                                        num16 = 0L;
                                        num18 = this.GetRoundNoOfMatch(baseAddr);
                                        num1 = this.GetTeam1WinCount(baseAddr);
                                        num2 = this.GetTeam2WinCount(baseAddr);
                                        drawRounds = this.GetDrawGameCount(baseAddr);
                                        this._isGameQuitted = false;
                                        if (this._triggerCheckTarget != null)
                                            this._triggerCheckTarget.SetDirty();
                                        MugenProfile profile = ProfileManager.MainObj().GetProfile(this._workingProfileId);
                                        if (profile != null && profile.IsAutoMode())
                                        {
                                            num18 = !profile.IsStrictRoundMode() ? this.GetRoundNoOfMatch(baseAddr) : this.GetRoundNoOfMatch(baseAddr) - 1;
                                            if (!this.IsDebugMode(baseAddr))
                                                this.EnableDebugKey(baseAddr, true);
                                            this.BeginInvoke((Action)(() => DebugForm.MainObj().SetSpeedModeCheckBox(profile.IsSpeedMode(), true)));
                                            this.BeginInvoke((Action)(() => DebugForm.MainObj().SetSkipModeCheckBox(profile.IsSkipMode())));
                                            this.BeginInvoke((Action)(() => DebugForm.MainObj().SetDebugModeCheckBox(profile.IsDebugMode(), true)));
                                            if (profile.IsStrictRoundMode())
                                            {
                                                int team1WinCount = this.GetTeam1WinCount(baseAddr);
                                                int team2WinCount = this.GetTeam2WinCount(baseAddr);
                                                int num20 = num18;
                                                if (team1WinCount >= num20 || team2WinCount >= num18)
                                                {
                                                    flag6 = false;
                                                    if (this.p != null && !this.p.HasExited)
                                                    {
                                                        this.KillGracefully();
                                                        continue;
                                                    }
                                                }
                                            }
                                        }
                                    }
                                    else
                                    {
                                        MugenProfile profile = ProfileManager.MainObj().GetProfile(this._workingProfileId);
                                        if (profile != null && profile.IsAutoMode() && this.GetRoundNo(baseAddr) > trueTeam1WinCount + trueTeam2WinCount + drawRounds + 1)
                                        {
                                            flag7 = false;
                                            flag6 = false;
                                            flag8 = true;
                                            int num20 = this.GetDrawGameCount(baseAddr) + 1;
                                            this.SetDrawGameCount(baseAddr, num20);
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
                                    num11 = 0L;
                                    num13 = 0L;
                                    num14 = 0L;
                                    num15 = 0L;
                                    num16 = 0L;
                                    if (num12 == 0L)
                                    {
                                        now = DateTime.Now;
                                        num12 = now.Ticks / 10000000L;
                                    }
                                    MugenProfile profile = ProfileManager.MainObj().GetProfile(this._workingProfileId);
                                    if (profile != null && profile.IsAutoMode())
                                    {
                                        long maxRoundState1Time = (long)profile.GetMaxRoundState1Time();
                                        now = DateTime.Now;
                                        long num20 = now.Ticks / 10000000L;
                                        if (maxRoundState1Time > 0L && num20 > num12 + maxRoundState1Time)
                                        {
                                            if (logManager != null)
                                                AsyncAppendLog("The introductions phase has been skipped as it was too long.");
                                            this.SetRoundState(baseAddr, 2);
                                            if (playerAddr1 != 0U)
                                                this.SetCtrl(playerAddr1, true);
                                            if (playerAddr2 != 0U)
                                                this.SetCtrl(playerAddr2, true);
                                            if (playerAddr3 != 0U)
                                                this.SetCtrl(playerAddr3, true);
                                            if (playerAddr4 != 0U)
                                                this.SetCtrl(playerAddr4, true);
                                        }
                                    }
                                }
                                if (roundState2 == 2)
                                {
                                    this._invokeWaitTime = 20;
                                    num11 = 0L;
                                    num12 = 0L;
                                    num14 = 0L;
                                    num15 = 0L;
                                    num16 = 0L;
                                    if (num13 == 0L)
                                    {
                                        now = DateTime.Now;
                                        num13 = now.Ticks / 10000000L;
                                        if (logManager != null)
                                            AsyncAppendLog("Round " + (object)this.GetRoundNo(baseAddr) + " start!");
                                    }
                                    MugenProfile profile = ProfileManager.MainObj().GetProfile(this._workingProfileId);
                                    if (profile != null && profile.IsAutoMode())
                                    {
                                        if (this.GetRoundTime(baseAddr) <= 30 && this.IsSpeedMode(baseAddr))
                                            this.SetSpeedMode(baseAddr, false);
                                        long maxRoundTime = (long)profile.GetMaxRoundTime();
                                        now = DateTime.Now;
                                        long num20 = now.Ticks / 10000000L;
                                        if (maxRoundTime > 0L && num20 > num13 + maxRoundTime)
                                        {
                                            flag7 = false;
                                            flag6 = false;
                                            flag8 = true;
                                            int num21 = this.GetDrawGameCount(baseAddr) + 1;
                                            this.SetDrawGameCount(baseAddr, num21);
                                            this._SetInt32Data(baseAddr, this._addr_db.ROUND_STATE_BASE_OFFSET, 3);
                                            int roundNo = this.GetRoundNo(baseAddr);
                                            this.SetRoundNo(baseAddr, roundNo + 1);
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
                                    num11 = 0L;
                                    num12 = 0L;
                                    num13 = 0L;
                                    num15 = 0L;
                                    num16 = 0L;
                                    if (num14 == 0L)
                                    {
                                        now = DateTime.Now;
                                        num14 = now.Ticks / 10000000L;
                                    }
                                    MugenProfile profile = ProfileManager.MainObj().GetProfile(this._workingProfileId);
                                    if (profile != null && profile.IsAutoMode())
                                    {
                                        long maxRoundState4Time = (long)profile.GetMaxRoundState4Time();
                                        now = DateTime.Now;
                                        long num20 = now.Ticks / 10000000L;
                                        if (maxRoundState4Time > 0L && num20 > num14 + maxRoundState4Time)
                                        {
                                            if (logManager != null)
                                                AsyncAppendLog("This round was suspended in order to begin the next one.");
                                            flag7 = false;
                                            flag6 = false;
                                            flag8 = true;
                                            int num21 = this.GetDrawGameCount(baseAddr) + 1;
                                            this.SetDrawGameCount(baseAddr, num21);
                                            int roundNo = this.GetRoundNo(baseAddr);
                                            this.SetRoundNo(baseAddr, roundNo + 1);
                                            this.InjectF4();
                                            if (logManager != null)
                                                AsyncAppendLog("⇒ Draw.");
                                        }
                                    }
                                }
                                if (roundState2 == 4)
                                {
                                    this._invokeWaitTime = 10;
                                    num11 = 0L;
                                    num12 = 0L;
                                    num13 = 0L;
                                    num14 = 0L;
                                    num16 = 0L;
                                    if (num15 == 0L)
                                    {
                                        now = DateTime.Now;
                                        num15 = now.Ticks / 10000000L;
                                    }
                                    MugenProfile profile = ProfileManager.MainObj().GetProfile(this._workingProfileId);
                                    if (profile != null && profile.IsAutoMode())
                                    {
                                        long maxRoundState4Time = (long)profile.GetMaxRoundState4Time();
                                        now = DateTime.Now;
                                        long num20 = now.Ticks / 10000000L;
                                        if (maxRoundState4Time > 0L && num20 > num15 + maxRoundState4Time)
                                        {
                                            this.GetRoundTime(baseAddr);
                                            if (logManager != null)
                                                AsyncAppendLog("This round was suspended in order to begin the next one.");
                                            if (!flag6)
                                            {
                                                int roundNo = this.GetRoundNo(baseAddr);
                                                this.SetRoundNo(baseAddr, roundNo + 1);
                                                this.InjectF4();
                                            }
                                            else
                                            {
                                                flag7 = false;
                                                flag6 = false;
                                                flag8 = true;
                                                int num21 = this.GetDrawGameCount(baseAddr) + 1;
                                                this.SetDrawGameCount(baseAddr, num21);
                                                int roundNo = this.GetRoundNo(baseAddr);
                                                this.SetRoundNo(baseAddr, roundNo + 1);
                                                this.InjectF4();
                                                if (logManager != null)
                                                    AsyncAppendLog("⇒ Draw.");
                                            }
                                        }
                                    }
                                }
                                // win determination segment
                                if (roundState2 >= 2 & flag6 | flag8)
                                {
                                    if (roundState2 >= 3)
                                    {
                                        int num20 = this.IsSpeedMode(baseAddr) ? 1 : 0;
                                        bool flag9 = this.IsSkipMode(baseAddr);
                                        if (num20 != 0)
                                            this.SetSpeedMode(baseAddr, false);
                                        if (flag9)
                                            this.SetSkipMode(baseAddr, false);
                                    }
                                    num11 = 0L;
                                    num12 = 0L;
                                    int team1WinCount = this.GetTeam1WinCount(baseAddr);
                                    int team2WinCount = this.GetTeam2WinCount(baseAddr);
                                    int newDrawGameCount = this.GetDrawGameCount(baseAddr);
                                    if (!flag8)
                                    {
                                        if (team1WinCount > num1 && team2WinCount == num2)
                                        {
                                            flag7 = true;
                                            flag6 = false;
                                            ++trueTeam1WinCount;
                                            if (this.IsTeam1WinKO(baseAddr))
                                                ++num3;
                                            if (logManager != null)
                                            {
                                                if (this.IsTeam1WinKO(baseAddr))
                                                    AsyncAppendLog("⇒ Player 1 side wins by K.O.");
                                                else
                                                    AsyncAppendLog("⇒  Player 1 side wins by judgement.");
                                            }
                                        }
                                        else if (team2WinCount > num2 && team1WinCount == num1)
                                        {
                                            flag7 = true;
                                            flag6 = false;
                                            ++trueTeam2WinCount;
                                            if (this.IsTeam2WinKO(baseAddr))
                                                ++num4;
                                            if (logManager != null)
                                            {
                                                if (this.IsTeam2WinKO(baseAddr))
                                                    AsyncAppendLog("⇒ Player 2 side wins by K.O.");
                                                else
                                                    AsyncAppendLog("⇒ Player 2 side wins by judgement.");
                                            }
                                        }
                                        else if (newDrawGameCount > drawRounds)
                                        {
                                            flag7 = true;
                                            flag6 = false;
                                            if (logManager != null && this.p != null)
                                                AsyncAppendLog("⇒ Draw.");
                                        }
                                    }
                                    if (!flag6 | flag8)
                                    {
                                        flag8 = false;
                                        MugenProfile profile = ProfileManager.MainObj().GetProfile(this._workingProfileId);
                                        if (!this.IsTurnsMode(baseAddr) || profile != null && profile.IsAutoMode() && profile.IsStrictRoundMode())
                                        {
                                            if (team1WinCount >= num18 && team2WinCount < num18 || profile != null && profile.IsAutoMode() && (profile.IsStrictRoundMode() && trueTeam1WinCount + newDrawGameCount >= num18) && trueTeam2WinCount + newDrawGameCount < num18 || profile != null && profile.IsAutoMode() && (profile.IsStrictRoundMode() && trueTeam1WinCount > trueTeam2WinCount) && trueTeam1WinCount + trueTeam2WinCount + newDrawGameCount >= num18)
                                            {
                                                flag7 = false;
                                                this.BeginInvoke((Action)(() => DebugForm.MainObj().FinalizeTriggerCheck(this._mugen_type)));
                                                if (this.p != null)
                                                    AsyncAppendLog(">>> " + (object)trueTeam1WinCount + "Wins" + (object)trueTeam2WinCount + "Losses" + (object)newDrawGameCount + "Draws. Resulting in victory for the player 1 side.");
                                                if (profile != null && !profile.IsQuickMode())
                                                {
                                                    this.UpdatePlayerDataEx(displayName2, palnoA1, displayName4, palnoB1, 1, 0, 0, 0, 0, trueTeam1WinCount, num3, trueTeam2WinCount, num4, newDrawGameCount, 0, 0);
                                                    this.UpdatePlayerDataEx(displayName3, palnoA2, displayName5, palnoB2, 0, 1, 0, 0, 0, trueTeam2WinCount, num4, trueTeam1WinCount, num3, newDrawGameCount, 0, 0);
                                                }
                                                if (profile != null && profile.IsAutoMode())
                                                {
                                                    profile.IncrementTempGameCount();
                                                    if (num16 == 0L)
                                                    {
                                                        now = DateTime.Now;
                                                        num16 = now.Ticks / 10000000L;
                                                    }
                                                    if (team1WinCount < num18)
                                                        this.SetTeam1WinCount(baseAddr, 100);
                                                    if (this.p != null && !profile.WasLastFight())
                                                        AsyncAppendLog("Starting next match");
                                                }
                                            }
                                            else if (team1WinCount < num18 && team2WinCount >= num18 || profile != null && profile.IsAutoMode() && (profile.IsStrictRoundMode() && trueTeam1WinCount + newDrawGameCount < num18) && trueTeam2WinCount + newDrawGameCount >= num18 || profile != null && profile.IsAutoMode() && (profile.IsStrictRoundMode() && trueTeam1WinCount < trueTeam2WinCount) && trueTeam1WinCount + trueTeam2WinCount + newDrawGameCount >= num18)
                                            {
                                                flag7 = false;
                                                this.BeginInvoke((Action)(() => DebugForm.MainObj().FinalizeTriggerCheck(this._mugen_type)));
                                                if (this.p != null && !profile.WasLastFight())
                                                    AsyncAppendLog(">>> " + (object)trueTeam2WinCount + "Wins" + (object)trueTeam1WinCount + "Losses" + (object)newDrawGameCount + "Draws. Resulting in victory for the player 2 side.");
                                                if (profile != null && !profile.IsQuickMode())
                                                {
                                                    this.UpdatePlayerDataEx(displayName2, palnoA1, displayName4, palnoB1, 0, 1, 0, 0, 0, trueTeam1WinCount, num3, trueTeam2WinCount, num4, newDrawGameCount, 0, 0);
                                                    this.UpdatePlayerDataEx(displayName3, palnoA2, displayName5, palnoB2, 1, 0, 0, 0, 0, trueTeam2WinCount, num4, trueTeam1WinCount, num3, newDrawGameCount, 0, 0);
                                                }
                                                if (profile != null && profile.IsAutoMode())
                                                {
                                                    profile.IncrementTempGameCount();
                                                    if (num16 == 0L)
                                                    {
                                                        now = DateTime.Now;
                                                        num16 = now.Ticks / 10000000L;
                                                    }
                                                    if (team2WinCount < num18)
                                                        this.SetTeam2WinCount(baseAddr, 100);
                                                    if (this.p != null && !profile.WasLastFight())
                                                        AsyncAppendLog("Starting next match");
                                                }
                                            }
                                            else if (team1WinCount >= num18 && team2WinCount >= num18 || profile != null && profile.IsAutoMode() && (profile.IsStrictRoundMode() && trueTeam1WinCount + newDrawGameCount >= num18) && trueTeam2WinCount + newDrawGameCount >= num18 || profile != null && profile.IsAutoMode() && (profile.IsStrictRoundMode() && trueTeam1WinCount == trueTeam2WinCount) && trueTeam1WinCount + trueTeam2WinCount + newDrawGameCount >= num18)
                                            {
                                                flag7 = false;
                                                this.BeginInvoke((Action)(() => DebugForm.MainObj().FinalizeTriggerCheck(this._mugen_type)));
                                                if (this.p != null)
                                                    AsyncAppendLog(">>> " + (object)trueTeam1WinCount + "Wins" + (object)trueTeam2WinCount + "Losses" + (object)newDrawGameCount + "Draws. Resulting in an overall draw.");
                                                if (profile != null && !profile.IsQuickMode())
                                                {
                                                    this.UpdatePlayerDataEx(displayName2, palnoA1, displayName4, palnoB1, 0, 0, 1, 0, 0, trueTeam1WinCount, num3, trueTeam2WinCount, num4, newDrawGameCount, 0, 0);
                                                    this.UpdatePlayerDataEx(displayName3, palnoA2, displayName5, palnoB2, 0, 0, 1, 0, 0, trueTeam2WinCount, num4, trueTeam1WinCount, num3, newDrawGameCount, 0, 0);
                                                }
                                                if (profile != null && profile.IsAutoMode())
                                                {
                                                    profile.IncrementTempGameCount();
                                                    if (num16 == 0L)
                                                    {
                                                        now = DateTime.Now;
                                                        num16 = now.Ticks / 10000000L;
                                                    }
                                                    if (team1WinCount < num18 || team2WinCount < num18)
                                                    {
                                                        this.SetTeam1WinCount(baseAddr, 100);
                                                        this.SetTeam2WinCount(baseAddr, 100);
                                                    }
                                                    if (this.p != null && !profile.WasLastFight())
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
                    break;
            }
            if (flag6 | flag7)
            {
                this.BeginInvoke((Action)(() => DebugForm.MainObj().FinalizeTriggerCheck(this._mugen_type)));
                this._invokeWaitTime = 20;
                MugenProfile profile = ProfileManager.MainObj().GetProfile(this._workingProfileId);
                if (baseAddr != 0U && !this.IsTurnsMode(baseAddr) || profile != null && profile.IsAutoMode())
                {
                    if (this._isMugenFrozen)
                    {
                        this._isGameQuitted = true;
                        if (logManager != null)
                            AsyncAppendLog("Process quit unexpectedly");
                        if (profile != null && !profile.IsQuickMode())
                        {
                            this.UpdatePlayerDataEx(displayName2, palnoA1, displayName4, palnoB1, 0, 0, 0, 1, 0, trueTeam1WinCount, num3, trueTeam2WinCount, num4, drawRounds, 1, 0);
                            this.UpdatePlayerDataEx(displayName3, palnoA2, displayName5, palnoB2, 0, 0, 0, 1, 0, trueTeam2WinCount, num4, trueTeam1WinCount, num3, drawRounds, 1, 0);
                        }
                    }
                    else
                    {
                        this._isGameQuitted = true;
                        if (logManager != null)
                            AsyncAppendLog("The match was cancelled");
                        if (profile != null && !profile.IsQuickMode())
                        {
                            this.UpdatePlayerDataEx(displayName2, palnoA1, displayName4, palnoB1, 0, 0, 0, 0, 1, trueTeam1WinCount, num3, trueTeam2WinCount, num4, drawRounds, 0, 1);
                            this.UpdatePlayerDataEx(displayName3, palnoA2, displayName5, palnoB2, 0, 0, 0, 0, 1, trueTeam2WinCount, num4, trueTeam1WinCount, num3, drawRounds, 0, 1);
                        }
                    }
                    if (profile != null && profile.IsAutoMode() && (this.p != null && !this.p.HasExited))
                        this.p.Kill();
                }
            }
            while (this.debugP != null)
            {
                NativeEvent nativeEvent2 = this.debugControl.WaitForDebugEvent(16);
                if (nativeEvent2 != null)
                {
                    nativeEvent2.Process.HandleIfLoaderBreakpoint(nativeEvent2);
                    this.debugControl.ContinueEvent(nativeEvent2);
                    if (nativeEvent2 is ExitProcessDebugEvent)
                    {
                        try
                        {
                            this.debugControl.Detach(this.debugP);
                            this.debugP.Dispose();
                        }
                        catch
                        {
                        }
                        this.debugP = (NativeDbgProcess)null;
                    }
                }
                else
                {
                    try
                    {
                        this.debugControl.Detach(this.debugP);
                        this.debugP.Dispose();
                    }
                    catch
                    {
                    }
                    this.debugP = (NativeDbgProcess)null;
                }
            }
            this.BeginInvoke((Action)(() => DebugForm.MainObj().PostFinalizeTriggerCheck(this._mugen_type)));
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
            this.mugenWatcher = new BackgroundWorker();
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
            this.mugenWatcher.WorkerSupportsCancellation = true;
            this.mugenWatcher.DoWork += new DoWorkEventHandler(this.mugenWatcher_DoWork);
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
        /// enum of supported mugen versions
        /// </summary>
        public enum MugenType_t
        {
            MUGEN_TYPE_NONE,
            MUGEN_TYPE_WINMUGEN,
            MUGEN_TYPE_MUGEN10,
            MUGEN_TYPE_MUGEN11A4,
            MUGEN_TYPE_MUGEN11B1,
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

        // reference for setting flags in child thread
        [Flags]
        public enum ThreadAccessFlags
        {
            TERMINATE = 1,
            SUSPEND_RESUME = 2,
            GET_CONTEXT = 8,
            SET_CONTEXT = 16, // 0x00000010
            SET_INFORMATION = 32, // 0x00000020
            QUERY_INFORMATION = 64, // 0x00000040
            SET_THREAD_TOKEN = 128, // 0x00000080
            IMPERSONATE = 256, // 0x00000100
            DIRECT_IMPERSONATION = 512, // 0x00000200
        }

        // reference for context flags for debug registers???
        public enum CONTEXT_FLAGS : uint
        {
            CONTEXT_i386 = 65536, // 0x00010000
            CONTEXT_i486 = 65536, // 0x00010000
            CONTEXT_CONTROL = 65537, // 0x00010001
            CONTEXT_INTEGER = 65538, // 0x00010002
            CONTEXT_SEGMENTS = 65540, // 0x00010004
            CONTEXT_FULL = 65543, // 0x00010007
            CONTEXT_FLOATING_POINT = 65544, // 0x00010008
            CONTEXT_DEBUG_REGISTERS = 65552, // 0x00010010
            CONTEXT_EXTENDED_REGISTERS = 65568, // 0x00010020
            CONTEXT_ALL = 65599, // 0x0001003F
        }

        // seems windows internal, but not sure
        public struct FLOATING_SAVE_AREA
        {
            public uint ControlWord;
            public uint StatusWord;
            public uint TagWord;
            public uint ErrorOffset;
            public uint ErrorSelector;
            public uint DataOffset;
            public uint DataSelector;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 80)]
            public byte[] RegisterArea;
            public uint Cr0NpxState;
        }

        // context of a process' registers
        public struct CONTEXT
        {
            public uint ContextFlags;
            public uint Dr0;
            public uint Dr1;
            public uint Dr2;
            public uint Dr3;
            public uint Dr6;
            public uint Dr7;
            public MugenWindow.FLOATING_SAVE_AREA FloatSave;
            public uint SegGs;
            public uint SegFs;
            public uint SegEs;
            public uint SegDs;
            public uint Edi;
            public uint Esi;
            public uint Ebx;
            public uint Edx;
            public uint Ecx;
            public uint Eax;
            public uint Ebp;
            public uint Eip;
            public uint SegCs;
            public uint EFlags;
            public uint Esp;
            public uint SegSs;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 512)]
            public byte[] ExtendedRegisters;
        }

        private struct BYTES
        {
            public byte BaseMid;
            public byte Flags1;
            public byte Flags2;
            public byte BaseHi;
        }

        private struct BITS
        {
            private int Value;

            public int BaseMid
            {
                get => this.Value & (int)byte.MaxValue;
                set => this.Value = this.Value & -256 | value & (int)byte.MaxValue;
            }

            public int Type
            {
                get => (this.Value & 7936) >> 8;
                set => this.Value = this.Value & -7937 | (value & 31) << 8;
            }

            public int Dpl
            {
                get => (this.Value & 24576) >> 13;
                set => this.Value = this.Value & -24577 | (value & 3) << 13;
            }

            public int Pres
            {
                get => (this.Value & 16384) >> 15;
                set => this.Value = this.Value & -16385 | (value & 1) << 15;
            }

            public int LimitHi
            {
                get => (this.Value & 983040) >> 16;
                set => this.Value = this.Value & -983041 | (value & 15) << 16;
            }

            public int Sys
            {
                get => (this.Value & 1048576) >> 20;
                set => this.Value = this.Value & -1048577 | (value & 1) << 20;
            }

            public int Reserved_0
            {
                get => (this.Value & 2097152) >> 21;
                set => this.Value = this.Value & -2097153 | (value & 1) << 21;
            }

            public int Default_Big
            {
                get => (this.Value & 4194304) >> 22;
                set => this.Value = this.Value & -4194305 | (value & 1) << 22;
            }

            public int Granularity
            {
                get => (this.Value & 8388608) >> 23;
                set => this.Value = this.Value & -8388609 | (value & 1) << 23;
            }

            public int BaseHi
            {
                get => (this.Value & -16777216) >> 24;
                set => this.Value = this.Value & 16777215 | (value & (int)byte.MaxValue) << 24;
            }
        }

        [StructLayout(LayoutKind.Explicit)]
        private struct HIGHWORD
        {
            [FieldOffset(0)]
            public MugenWindow.BYTES Bytes;
            [FieldOffset(0)]
            public MugenWindow.BITS Bits;
        }

        private struct LDT_ENTRY
        {
            public ushort LimitLow;
            public ushort BaseLow;
            public MugenWindow.HIGHWORD HighWord;
        }

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
