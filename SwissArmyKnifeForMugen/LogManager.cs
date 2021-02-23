// Decompiled with JetBrains decompiler
// Type: SwissArmyKnifeForMugen.LogManager
// Assembly: SAKnifeWM, Version=1.0.8.0, Culture=neutral, PublicKeyToken=null
// MVID: 09478AD8-365C-4BF3-BEA1-B5785151259B
// Assembly location: C:\Users\ziddi\Downloads\Swiss Army Knife 1.1 Conversion\Swiss Army Knife 1.1 Conversion\SAKnifeWM.exe

using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace SwissArmyKnifeForMugen
{
  public class LogManager : Form
  {
    private Encoding _encode = Encoding.GetEncoding("SHIFT_JIS");
    private static LogManager selfObj;
    private string logFile;
    private IContainer components;
    private TextBox logText;
    private ContextMenuStrip logContextMenuStrip;
    private ToolStripMenuItem selectAllStripMenuItem;
    private ToolStripMenuItem copyStripMenuItem;
    private ToolStripSeparator toolStripSeparator1;
    private ToolStripMenuItem resetStripMenuItem;
    private ToolStripMenuItem logFolderStripMenuItem;
    private ToolStripSeparator toolStripSeparator2;
    private ToolStripMenuItem displayStripMenuItem;
    private Button logCmdButton;
    private ToolStripMenuItem copyResultStripMenuItem;
    private SaveFileDialog saveFileDialog1;
    private ToolTip toolTip1;
    private Button startButton;
    private ToolStripMenuItem saveResultStripMenuItem;

    private LogManager()
    {
      this.InitializeComponent();
      this.logFile = MainForm.GetFullPath("logs\\log-" + DateTime.Now.ToString("MM-dd_HH-mm-ss") + ".txt");
      if (Directory.Exists(MainForm.GetFullPath("logs")))
        return;
      try
      {
        Directory.CreateDirectory(MainForm.GetFullPath("logs"));
      }
      catch
      {
      }
    }

    public static LogManager MainObj()
    {
      if (LogManager.selfObj == null)
        LogManager.selfObj = new LogManager();
      return LogManager.selfObj;
    }

    public bool IsAvailable() => true;

    public void _appendLog(string msg)
    {
      string str = DateTime.Now.ToString("HH:mm:ss - ");
      try
      {
        File.AppendAllText(this.logFile, Environment.NewLine + str + msg, this._encode);
      }
      catch
      {
        this.logText.AppendText(Environment.NewLine + "Error：Could not write to the log file.");
      }
    }

    public void _append(string msg)
    {
      try
      {
        File.AppendAllText(this.logFile, msg, this._encode);
      }
      catch
      {
        this.logText.AppendText(Environment.NewLine + "Error：Could not write to the log file.");
      }
    }

    public void appendLog(string msg)
    {
      DateTime now = DateTime.Now;
      if (this.logText.TextLength + 100 >= this.logText.MaxLength)
      {
        try
        {
          File.AppendAllText(this.logFile, "!!The log became too long and has been cleared.!!", this._encode);
        }
        catch
        {
        }
        this.logFile = MainForm.GetFullPath("logs\\log-" + now.ToString("MM-dd-_HH-mm-ss") + ".txt");
        this.logText.Text = "!!The log became too long and has been cleared.!!";
        this.logText.AppendText("Filename:" + Path.GetFileName(this.logFile));
      }
      if (this.logText.TextLength == 0)
        this.logText.AppendText("Filename:" + Path.GetFileName(this.logFile));
      string str = now.ToString("HH:mm:ss - ");
      this.logText.AppendText(Environment.NewLine + str + msg);
      try
      {
        File.AppendAllText(this.logFile, Environment.NewLine + str + msg, this._encode);
      }
      catch
      {
        this.logText.AppendText(Environment.NewLine + "Error：Could not write to the log file.");
      }
    }

    public void append(string msg)
    {
      this.logText.AppendText(msg);
      try
      {
        File.AppendAllText(this.logFile, msg, this._encode);
      }
      catch
      {
        this.logText.AppendText(Environment.NewLine + "Error：Could not write to the log file.");
      }
    }

    public void DumpPlayers(
      int playerCount,
      int[] playerId,
      int[] helperId,
      int[] stateno,
      int[] prevstateno,
      string[] name)
    {
      this.append("---- player and helper list ----" + Environment.NewLine);
      for (int index = 0; index < 4; ++index)
      {
        if (playerId[index] != -1)
          this.append(string.Format("playerid={0},stateno={1},prevstateno={2},name={3}", (object) playerId[index], (object) stateno[index], (object) prevstateno[index], (object) name[index]) + Environment.NewLine);
      }
      for (int index = 4; index < playerCount; ++index)
      {
        if (playerId[index] != -1)
          this.append(string.Format("playerid={0},helperid={1},stateno={2},prevstateno={3},name={4}", (object) playerId[index], (object) helperId[index], (object) stateno[index], (object) prevstateno[index], (object) name[index]) + Environment.NewLine);
      }
      this.append("--------------------");
      this.appendLog("");
    }

    public void ResetPlayers()
    {
      GameLogger.MainObj().DisplayAll(false);
      this.logFile = MainForm.GetFullPath("logs\\log-" + DateTime.Now.ToString("MM-dd_HH-mm-ss") + ".txt");
      this.logText.Text = "";
      GameLogger.MainObj().ResetCharData();
      this.appendLog("The battle history log has been reset.");
      this.appendLog("");
      MugenWindow.MainObj().ResetNumOfGames();
    }

    private void LogManager_FormClosing(object sender, FormClosingEventArgs e)
    {
      if (e.CloseReason != CloseReason.UserClosing)
        return;
      e.Cancel = true;
      this.Hide();
    }

    private void selectAllStripMenuItem_Click(object sender, EventArgs e)
    {
      if (this.logContextMenuStrip.Cursor == Cursors.Help)
      {
        this.logContextMenuStrip.Show();
        this.toolTip1.Hide((IWin32Window) this);
        this.toolTip1.IsBalloon = false;
        this.toolTip1.IsBalloon = true;
        this.toolTip1.SetToolTip((Control) this.logContextMenuStrip, " ");
        this.toolTip1.Show("Selects all the text in the battle history window", (IWin32Window) this.logContextMenuStrip, this.selectAllStripMenuItem.Width / 2, this.selectAllStripMenuItem.Height / 2);
      }
      else
      {
        this.logText.SelectAll();
        MugenWindow.MainObj().Activate();
      }
    }

    private void copyStripMenuItem_Click(object sender, EventArgs e)
    {
      if (this.logContextMenuStrip.Cursor == Cursors.Help)
      {
        this.logContextMenuStrip.Show();
        this.toolTip1.Hide((IWin32Window) this);
        this.toolTip1.IsBalloon = false;
        this.toolTip1.IsBalloon = true;
        this.toolTip1.SetToolTip((Control) this.logContextMenuStrip, " ");
        this.toolTip1.Show("Copies the selected text to the clipboard.", (IWin32Window) this.logContextMenuStrip, this.copyStripMenuItem.Width / 2, this.copyStripMenuItem.Height * 3 / 2);
      }
      else
      {
        if (this.logText.SelectedText != null && this.logText.SelectedText != "")
          Clipboard.SetText(this.logText.SelectedText);
        MugenWindow.MainObj().Activate();
      }
    }

    private void displayStripMenuItem_Click(object sender, EventArgs e)
    {
      if (this.logContextMenuStrip.Cursor == Cursors.Help)
      {
        this.logContextMenuStrip.Show();
        this.toolTip1.Hide((IWin32Window) this);
        this.toolTip1.IsBalloon = false;
        this.toolTip1.IsBalloon = true;
        this.toolTip1.SetToolTip((Control) this.logContextMenuStrip, " ");
        this.toolTip1.Show("Displays the current standings from the battle history.", (IWin32Window) this.logContextMenuStrip, this.copyStripMenuItem.Width / 2, this.copyStripMenuItem.Height * 6 / 2);
      }
      else
        GameLogger.MainObj().DisplayAll(true);
    }

    private void logCmdButton_Click(object sender, EventArgs e)
    {
      this.toolTip1.Hide((IWin32Window) this);
      this.logContextMenuStrip.Show((Control) this.logCmdButton, 0, 0);
    }

    private void copyResultStripMenuItem_Click(object sender, EventArgs e)
    {
      if (this.logContextMenuStrip.Cursor == Cursors.Help)
      {
        this.logContextMenuStrip.Show();
        this.toolTip1.Hide((IWin32Window) this);
        this.toolTip1.IsBalloon = false;
        this.toolTip1.IsBalloon = true;
        this.toolTip1.SetToolTip((Control) this.logContextMenuStrip, " ");
        this.toolTip1.Show("Saves the battle history log as a cvs file. \r\nThis file can be opened in excelt for further editing.", (IWin32Window) this.logContextMenuStrip, this.copyStripMenuItem.Width / 2, this.copyStripMenuItem.Height * 8 / 2);
      }
      else
      {
        this.saveFileDialog1.FileName = "Battle log_" + DateTime.Now.ToString("MM-dd_HH-mm-ss") + ".csv";
        this.saveFileDialog1.InitialDirectory = Environment.SpecialFolder.DesktopDirectory.ToString();
        if (this.saveFileDialog1.ShowDialog() != DialogResult.OK)
          return;
        GameLogger.MainObj().CopyAll(this.saveFileDialog1.FileName);
      }
    }

    private void saveResultStripMenuItem_Click(object sender, EventArgs e)
    {
      if (this.logContextMenuStrip.Cursor == Cursors.Help)
      {
        this.logContextMenuStrip.Show();
        this.toolTip1.Hide((IWin32Window) this);
        this.toolTip1.IsBalloon = false;
        this.toolTip1.IsBalloon = true;
        this.toolTip1.SetToolTip((Control) this.logContextMenuStrip, " ");
        this.toolTip1.Show("Saves the battle history log to a file", (IWin32Window) this.logContextMenuStrip, this.copyStripMenuItem.Width / 2, this.copyStripMenuItem.Height * 8 / 2);
      }
      else
      {
        this.saveFileDialog1.FileName = "Battle log_" + DateTime.Now.ToString("MM-dd_HH-mm-ss") + ".txt";
        this.saveFileDialog1.InitialDirectory = Environment.SpecialFolder.DesktopDirectory.ToString();
        if (this.saveFileDialog1.ShowDialog() != DialogResult.OK)
          return;
        GameLogger.MainObj().SaveAll(this.saveFileDialog1.FileName);
      }
    }

    private void logFolderStripMenuItem_Click(object sender, EventArgs e)
    {
      if (this.logContextMenuStrip.Cursor == Cursors.Help)
      {
        this.logContextMenuStrip.Show();
        this.toolTip1.Hide((IWin32Window) this);
        this.toolTip1.IsBalloon = false;
        this.toolTip1.IsBalloon = true;
        this.toolTip1.SetToolTip((Control) this.logContextMenuStrip, " ");
        this.toolTip1.Show("Opens the folder that contains the battle history logs.", (IWin32Window) this.logContextMenuStrip, this.copyStripMenuItem.Width / 2, this.copyStripMenuItem.Height * 10 / 2);
      }
      else
      {
        ProcessStartInfo startInfo = new ProcessStartInfo();
        startInfo.FileName = MainForm.GetFullPath("logs");
        startInfo.WorkingDirectory = MainForm.GetFullPath("logs");
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

    private void resetStripMenuItem_Click(object sender, EventArgs e)
    {
      if (this.logContextMenuStrip.Cursor == Cursors.Help)
      {
        this.logContextMenuStrip.Show();
        string text = "Resets the current battle history." + Environment.NewLine + "(A new dialog to confirm the selection will appear.)" + Environment.NewLine;
        this.toolTip1.Hide((IWin32Window) this);
        this.toolTip1.IsBalloon = false;
        this.toolTip1.IsBalloon = true;
        this.toolTip1.SetToolTip((Control) this.logContextMenuStrip, " ");
        this.toolTip1.Show(text, (IWin32Window) this.logContextMenuStrip, this.copyStripMenuItem.Width / 2, this.copyStripMenuItem.Height * 12 / 2);
      }
      else
      {
        if (MessageBox.Show("Are you sure you wish to reset the battle history log?", "Swiss Army Knife", MessageBoxButtons.OKCancel) == DialogResult.OK)
          this.ResetPlayers();
        MugenWindow.MainObj().Activate();
      }
    }

    public void SetHelpMode(bool isHelpMode)
    {
      if (isHelpMode)
      {
        this.toolTip1.Active = true;
        this.Cursor = Cursors.Help;
        this.logText.Cursor = Cursors.Help;
        this.logContextMenuStrip.Cursor = Cursors.Help;
        this.startButton.Cursor = Cursors.Help;
      }
      else
      {
        this.toolTip1.Active = false;
        this.Cursor = Cursors.Default;
        this.logText.Cursor = Cursors.Default;
        this.logContextMenuStrip.Cursor = Cursors.Default;
        this.startButton.Cursor = Cursors.Default;
      }
    }

    private void toolTip1_Popup(object sender, PopupEventArgs e)
    {
    }

    private void logText_Click(object sender, EventArgs e)
    {
      if (!(this.logText.Cursor == Cursors.Help))
        return;
      this.toolTip1.Hide((IWin32Window) this);
      this.toolTip1.IsBalloon = false;
      this.toolTip1.IsBalloon = true;
      string text = "Displays the battle history. This history is also automatically saved in a log file." + Environment.NewLine + "You can freely check these log files by clicking the 'Open log folder' button." + Environment.NewLine + "Additionally, you can also access more tools by right clicking the battle history list." + Environment.NewLine + Environment.NewLine + "How to read the battle history log:" + Environment.NewLine + "When a battle begins the log will display the characters used and their palette." + Environment.NewLine + Environment.NewLine + "　　22:54:27 - P1 side: Kung Fu Man (1p)" + Environment.NewLine + "　　22:54:27 - P2 side: Suave Dude (1p)" + Environment.NewLine + Environment.NewLine + "After a match is completed the battle results will be displayed in the battle history window." + Environment.NewLine + Environment.NewLine + "　　22:55:30 - ◆Kung Fu Man (1p)'s battle record ---" + Environment.NewLine + "　　22:55:30 - Total matches = 4" + Environment.NewLine + "　　22:55:30 - Wins=1,Losses=1,Draws=0,Errors=2,Cancelled matches=0" + Environment.NewLine + "　　22:55:30 - Total number of rounds = 6" + Environment.NewLine + "　　22:55:30 - Wins=2(KO=2),Losses=2(KO=1),Draws=0,Errors=2,Cancelled matches=0" + Environment.NewLine + Environment.NewLine + "The third line displays from the total number of matches: The number of wins, losses, draws, errors, and matches cancelled by closing mugen." + Environment.NewLine + "The fifth line displays from the total number of rounds: The number of wins, wins by K.O, losses, losses by K.O, draws, errors, and matches cancelled by closing mugen." + Environment.NewLine + Environment.NewLine + "In case of a crash, the battle history will display the error message, state numbers and the value of the prevstateno trigger for all active players and helpers at the time of the crash." + Environment.NewLine + Environment.NewLine + "　　14:44:37 - mugen closed abruptly." + Environment.NewLine + "　　--------------------" + Environment.NewLine + "　　Error message" + Environment.NewLine + "　　Assert failure in array.h line 110" + Environment.NewLine + "　　--------------------" + Environment.NewLine + "　　---- player and helper list ----" + Environment.NewLine + "　　playerid=56,stateno=130,prevstateno=1,name=Kung Fu Man" + Environment.NewLine + "　　playerid=57,stateno=200,prevstateno=0,name=Suave Dude" + Environment.NewLine + "　　--------------------" + Environment.NewLine + Environment.NewLine;
      this.toolTip1.SetToolTip((Control) this.logText, " ");
      this.toolTip1.Show(text, (IWin32Window) this.logText, this.logText.Width, this.logText.Height / 2);
    }

    private void LogManager_Deactivate(object sender, EventArgs e) => this.toolTip1.Hide((IWin32Window) this);

    private void startButton_Click(object sender, EventArgs e)
    {
      if (this.startButton.Cursor == Cursors.Help)
      {
        this.toolTip1.Hide((IWin32Window) this);
        this.toolTip1.IsBalloon = false;
        this.toolTip1.IsBalloon = true;
        this.toolTip1.SetToolTip((Control) this.startButton, " ");
        this.toolTip1.Show("Opens the folder containing past battle history logs.", (IWin32Window) this.startButton, this.startButton.Width / 2, this.startButton.Height / 2);
      }
      else
      {
        ProcessStartInfo startInfo = new ProcessStartInfo();
        startInfo.FileName = MainForm.GetFullPath("logs");
        startInfo.WorkingDirectory = MainForm.GetFullPath("logs");
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

    private void LogManager_Activated(object sender, EventArgs e)
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
      this.components = (IContainer) new Container();
      ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (LogManager));
      this.logText = new TextBox();
      this.logContextMenuStrip = new ContextMenuStrip(this.components);
      this.selectAllStripMenuItem = new ToolStripMenuItem();
      this.copyStripMenuItem = new ToolStripMenuItem();
      this.toolStripSeparator1 = new ToolStripSeparator();
      this.displayStripMenuItem = new ToolStripMenuItem();
      this.copyResultStripMenuItem = new ToolStripMenuItem();
      this.logFolderStripMenuItem = new ToolStripMenuItem();
      this.toolStripSeparator2 = new ToolStripSeparator();
      this.resetStripMenuItem = new ToolStripMenuItem();
      this.logCmdButton = new Button();
      this.saveFileDialog1 = new SaveFileDialog();
      this.toolTip1 = new ToolTip(this.components);
      this.startButton = new Button();
      this.saveResultStripMenuItem = new ToolStripMenuItem();
      this.logContextMenuStrip.SuspendLayout();
      this.SuspendLayout();
      this.logText.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      this.logText.BorderStyle = BorderStyle.FixedSingle;
      this.logText.ContextMenuStrip = this.logContextMenuStrip;
      this.logText.Font = new Font("MS UI Gothic", 9f, FontStyle.Regular, GraphicsUnit.Point, (byte) 128);
      this.logText.HideSelection = false;
      this.logText.ImeMode = ImeMode.Disable;
      this.logText.Location = new Point(0, 0);
      this.logText.MaxLength = 65534;
      this.logText.Multiline = true;
      this.logText.Name = "logText";
      this.logText.ReadOnly = true;
      this.logText.ScrollBars = ScrollBars.Vertical;
      this.logText.Size = new Size(283, 212);
      this.logText.TabIndex = 0;
      this.logText.TabStop = false;
      this.logText.Click += new EventHandler(this.logText_Click);
      this.logContextMenuStrip.Items.AddRange(new ToolStripItem[9]
      {
        (ToolStripItem) this.selectAllStripMenuItem,
        (ToolStripItem) this.copyStripMenuItem,
        (ToolStripItem) this.toolStripSeparator1,
        (ToolStripItem) this.displayStripMenuItem,
        (ToolStripItem) this.saveResultStripMenuItem,
        (ToolStripItem) this.copyResultStripMenuItem,
        (ToolStripItem) this.logFolderStripMenuItem,
        (ToolStripItem) this.toolStripSeparator2,
        (ToolStripItem) this.resetStripMenuItem
      });
      this.logContextMenuStrip.Name = "logContextMenuStrip";
      this.logContextMenuStrip.Size = new Size(245, 192);
      this.selectAllStripMenuItem.Name = "selectAllStripMenuItem";
      this.selectAllStripMenuItem.ShortcutKeyDisplayString = "Ctrl+A";
      this.selectAllStripMenuItem.ShortcutKeys = Keys.A | Keys.Control;
      this.selectAllStripMenuItem.Size = new Size(244, 22);
      this.selectAllStripMenuItem.Text = "Select All";
      this.selectAllStripMenuItem.Click += new EventHandler(this.selectAllStripMenuItem_Click);
      this.copyStripMenuItem.Name = "copyStripMenuItem";
      this.copyStripMenuItem.ShortcutKeyDisplayString = "Ctrl+C";
      this.copyStripMenuItem.ShortcutKeys = Keys.C | Keys.Control;
      this.copyStripMenuItem.Size = new Size(244, 22);
      this.copyStripMenuItem.Text = "Copy";
      this.copyStripMenuItem.Click += new EventHandler(this.copyStripMenuItem_Click);
      this.toolStripSeparator1.Name = "toolStripSeparator1";
      this.toolStripSeparator1.Size = new Size(241, 6);
      this.displayStripMenuItem.Name = "displayStripMenuItem";
      this.displayStripMenuItem.Size = new Size(244, 22);
      this.displayStripMenuItem.Text = "Display battle record log";
      this.displayStripMenuItem.Click += new EventHandler(this.displayStripMenuItem_Click);
      this.copyResultStripMenuItem.Name = "copyResultStripMenuItem";
      this.copyResultStripMenuItem.Size = new Size(244, 22);
      this.copyResultStripMenuItem.Text = "Save battle results log as a csv file";
      this.copyResultStripMenuItem.Click += new EventHandler(this.copyResultStripMenuItem_Click);
      this.logFolderStripMenuItem.Name = "logFolderStripMenuItem";
      this.logFolderStripMenuItem.Size = new Size(244, 22);
      this.logFolderStripMenuItem.Text = "Open the battle history logs folder";
      this.logFolderStripMenuItem.Click += new EventHandler(this.logFolderStripMenuItem_Click);
      this.toolStripSeparator2.Name = "toolStripSeparator2";
      this.toolStripSeparator2.Size = new Size(241, 6);
      this.resetStripMenuItem.Name = "resetStripMenuItem";
      this.resetStripMenuItem.Size = new Size(244, 22);
      this.resetStripMenuItem.Text = "Reset battle history";
      this.resetStripMenuItem.Click += new EventHandler(this.resetStripMenuItem_Click);
      this.logCmdButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
      this.logCmdButton.Location = new Point(3, 215);
      this.logCmdButton.Name = "logCmdButton";
      this.logCmdButton.Size = new Size(131, 25);
      this.logCmdButton.TabIndex = 7;
      this.logCmdButton.Text = "More commands";
      this.logCmdButton.UseVisualStyleBackColor = true;
      this.logCmdButton.Click += new EventHandler(this.logCmdButton_Click);
      this.saveFileDialog1.DefaultExt = "csv";
      this.saveFileDialog1.Filter = "csv file|*.csv";
      this.saveFileDialog1.RestoreDirectory = true;
      this.saveFileDialog1.SupportMultiDottedExtensions = true;
      this.saveFileDialog1.Title = "Save battle results list";
      this.toolTip1.AutomaticDelay = 500000;
      this.toolTip1.AutoPopDelay = 5000000;
      this.toolTip1.InitialDelay = 5000000;
      this.toolTip1.IsBalloon = true;
      this.toolTip1.ReshowDelay = 1000000;
      this.toolTip1.Popup += new PopupEventHandler(this.toolTip1_Popup);
      this.startButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
      this.startButton.Location = new Point(146, 215);
      this.startButton.Name = "startButton";
      this.startButton.Size = new Size(130, 25);
      this.startButton.TabIndex = 8;
      this.startButton.Text = "Open log folder";
      this.startButton.UseVisualStyleBackColor = true;
      this.startButton.Click += new EventHandler(this.startButton_Click);
      this.saveResultStripMenuItem.Name = "saveResultStripMenuItem";
      this.saveResultStripMenuItem.Size = new Size(244, 22);
      this.saveResultStripMenuItem.Text = "Save battle history log in a file";
      this.saveResultStripMenuItem.Click += new EventHandler(this.saveResultStripMenuItem_Click);
      this.AutoScaleDimensions = new SizeF(6f, 12f);
      this.AutoScaleMode = AutoScaleMode.Font;
      this.ClientSize = new Size(283, 243);
      this.Controls.Add((Control) this.startButton);
      this.Controls.Add((Control) this.logCmdButton);
      this.Controls.Add((Control) this.logText);
      this.Icon = (Icon) componentResourceManager.GetObject("$this.Icon");
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = nameof (LogManager);
      this.ShowIcon = false;
      this.ShowInTaskbar = false;
      this.StartPosition = FormStartPosition.Manual;
      this.Text = "Battle History";
      this.Activated += new EventHandler(this.LogManager_Activated);
      this.Deactivate += new EventHandler(this.LogManager_Deactivate);
      this.FormClosing += new FormClosingEventHandler(this.LogManager_FormClosing);
      this.logContextMenuStrip.ResumeLayout(false);
      this.ResumeLayout(false);
      this.PerformLayout();
    }
  }
}
