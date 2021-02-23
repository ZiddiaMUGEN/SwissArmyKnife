// Decompiled with JetBrains decompiler
// Type: SwissArmyKnifeForMugen.VarForm
// Assembly: SAKnifeWM, Version=1.0.8.0, Culture=neutral, PublicKeyToken=null
// MVID: 09478AD8-365C-4BF3-BEA1-B5785151259B
// Assembly location: C:\Users\ziddi\Downloads\Swiss Army Knife 1.1 Conversion\Swiss Army Knife 1.1 Conversion\SAKnifeWM.exe

using System;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace SwissArmyKnifeForMugen
{
  public class VarForm : Form
  {
    private bool _isDirty = true;
    private VarForm.SysInfo_t _sysInfo = new VarForm.SysInfo_t();
    private int[] _mugenSysvar = new int[5];
    private int[] _mugenVar = new int[60];
    private float[] _mugenSysfvar = new float[5];
    private float[] _mugenFvar = new float[40];
    private bool[] _columState = new bool[5];
    private IContainer components;
    private RadioButton radioButton1;
    private RadioButton radioButton2;
    private RadioButton radioButton3;
    private RadioButton radioButton4;
    private RadioButton radioButton5;
    private RadioButton radioButton6;
    private RadioButton radioButton7;
    private BufferedListView varListView;
    private ColumnHeader columnHeader1;
    private ColumnHeader columnHeader2;
    private ColumnHeader columnHeader3;
    private ColumnHeader columnHeader4;
    private ColumnHeader columnHeader5;
    private ContextMenuStrip varContextMenuStrip;
    private ToolStripMenuItem copyAllToolStripMenuItem;
    private ToolStripMenuItem binaryToolStripMenuItem;
    private ToolTip toolTip1;
    private static VarForm selfObj;
    private int _selectedGroup;

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      this.components = (IContainer) new Container();
      ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (VarForm));
      this.radioButton1 = new RadioButton();
      this.radioButton2 = new RadioButton();
      this.radioButton3 = new RadioButton();
      this.radioButton4 = new RadioButton();
      this.radioButton5 = new RadioButton();
      this.radioButton6 = new RadioButton();
      this.radioButton7 = new RadioButton();
      this.varListView = new BufferedListView();
      this.columnHeader1 = new ColumnHeader();
      this.columnHeader2 = new ColumnHeader();
      this.columnHeader3 = new ColumnHeader();
      this.columnHeader4 = new ColumnHeader();
      this.columnHeader5 = new ColumnHeader();
      this.varContextMenuStrip = new ContextMenuStrip(this.components);
      this.binaryToolStripMenuItem = new ToolStripMenuItem();
      this.copyAllToolStripMenuItem = new ToolStripMenuItem();
      this.toolTip1 = new ToolTip(this.components);
      this.varContextMenuStrip.SuspendLayout();
      this.SuspendLayout();
      this.radioButton1.AutoSize = true;
      this.radioButton1.Location = new Point(19, 10);
      this.radioButton1.Name = "radioButton1";
      this.radioButton1.Size = new Size(57, 17);
      this.radioButton1.TabIndex = 0;
      this.radioButton1.TabStop = true;
      this.radioButton1.Text = "system";
      this.radioButton1.UseVisualStyleBackColor = true;
      this.radioButton1.CheckedChanged += new EventHandler(this.radioButton1_CheckedChanged);
      this.radioButton1.MouseClick += new MouseEventHandler(this.radioButton1_MouseClick);
      this.radioButton2.AutoSize = true;
      this.radioButton2.Location = new Point(82, 10);
      this.radioButton2.Name = "radioButton2";
      this.radioButton2.Size = new Size(77, 17);
      this.radioButton2.TabIndex = 1;
      this.radioButton2.TabStop = true;
      this.radioButton2.Text = "var(0~19)";
      this.radioButton2.UseVisualStyleBackColor = true;
      this.radioButton2.CheckedChanged += new EventHandler(this.radioButton2_CheckedChanged);
      this.radioButton2.MouseClick += new MouseEventHandler(this.radioButton2_MouseClick);
      this.radioButton3.AutoSize = true;
      this.radioButton3.Location = new Point(165, 10);
      this.radioButton3.Name = "radioButton3";
      this.radioButton3.Size = new Size(77, 17);
      this.radioButton3.TabIndex = 2;
      this.radioButton3.TabStop = true;
      this.radioButton3.Text = "var(20~39)";
      this.radioButton3.UseVisualStyleBackColor = true;
      this.radioButton3.CheckedChanged += new EventHandler(this.radioButton3_CheckedChanged);
      this.radioButton3.MouseClick += new MouseEventHandler(this.radioButton3_MouseClick);
      this.radioButton4.AutoSize = true;
      this.radioButton4.Location = new Point(248, 10);
      this.radioButton4.Name = "radioButton4";
      this.radioButton4.Size = new Size(77, 17);
      this.radioButton4.TabIndex = 3;
      this.radioButton4.TabStop = true;
      this.radioButton4.Text = "var(40~59)";
      this.radioButton4.UseVisualStyleBackColor = true;
      this.radioButton4.CheckedChanged += new EventHandler(this.radioButton4_CheckedChanged);
      this.radioButton4.MouseClick += new MouseEventHandler(this.radioButton4_MouseClick);
      this.radioButton5.AutoSize = true;
      this.radioButton5.Location = new Point(331, 10);
      this.radioButton5.Name = "radioButton5";
      this.radioButton5.Size = new Size(77, 17);
      this.radioButton5.TabIndex = 4;
      this.radioButton5.TabStop = true;
      this.radioButton5.Text = "fvar(0~19)";
      this.radioButton5.UseVisualStyleBackColor = true;
      this.radioButton5.CheckedChanged += new EventHandler(this.radioButton5_CheckedChanged);
      this.radioButton5.MouseClick += new MouseEventHandler(this.radioButton5_MouseClick);
      this.radioButton6.AutoSize = true;
      this.radioButton6.Location = new Point(414, 10);
      this.radioButton6.Name = "radioButton6";
      this.radioButton6.Size = new Size(77, 17);
      this.radioButton6.TabIndex = 5;
      this.radioButton6.TabStop = true;
      this.radioButton6.Text = "fvar(20~39)";
      this.radioButton6.UseVisualStyleBackColor = true;
      this.radioButton6.CheckedChanged += new EventHandler(this.radioButton6_CheckedChanged);
      this.radioButton6.MouseClick += new MouseEventHandler(this.radioButton6_MouseClick);
      this.radioButton7.AutoSize = true;
      this.radioButton7.Location = new Point(497, 10);
      this.radioButton7.Name = "radioButton7";
      this.radioButton7.Size = new Size(77, 17);
      this.radioButton7.TabIndex = 6;
      this.radioButton7.TabStop = true;
      this.radioButton7.Text = "AssertSpecial";
      this.radioButton7.UseVisualStyleBackColor = true;
      this.radioButton7.CheckedChanged += new EventHandler(this.radioButton7_CheckedChanged);
      this.radioButton7.MouseClick += new MouseEventHandler(this.radioButton7_MouseClick);
      this.varListView.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
      this.varListView.Columns.AddRange(new ColumnHeader[5]
      {
        this.columnHeader1,
        this.columnHeader2,
        this.columnHeader3,
        this.columnHeader4,
        this.columnHeader5
      });
      this.varListView.ContextMenuStrip = this.varContextMenuStrip;
      this.varListView.FullRowSelect = true;
      this.varListView.GridLines = true;
      this.varListView.LabelWrap = false;
      this.varListView.Location = new Point(12, 35);
      this.varListView.MultiSelect = false;
      this.varListView.Name = "varListView";
      this.varListView.Size = new Size(600, 135);
      this.varListView.TabIndex = 6;
      this.varListView.TabStop = false;
      this.varListView.UseCompatibleStateImageBehavior = false;
      this.varListView.View = View.Details;
      this.varListView.ColumnClick += new ColumnClickEventHandler(this.varListView_ColumnClick);
      this.varListView.SelectedIndexChanged += new EventHandler(this.varListView_SelectedIndexChanged);
      this.varListView.MouseClick += new MouseEventHandler(this.varListView_MouseClick);
      this.varListView.MouseDown += new MouseEventHandler(this.varListView_MouseDown);
      this.varListView.MouseUp += new MouseEventHandler(this.varListView_MouseUp);
      this.columnHeader1.Text = "Click to adjust size.";
      this.columnHeader1.Width = 115;
      this.columnHeader2.Text = "";
      this.columnHeader2.Width = 115;
      this.columnHeader3.Text = "";
      this.columnHeader3.Width = 115;
      this.columnHeader4.Text = "";
      this.columnHeader4.Width = 115;
      this.columnHeader5.Text = "";
      this.columnHeader5.Width = 115;
      this.varContextMenuStrip.Items.AddRange(new ToolStripItem[2]
      {
        (ToolStripItem) this.binaryToolStripMenuItem,
        (ToolStripItem) this.copyAllToolStripMenuItem
      });
      this.varContextMenuStrip.Name = "debugContextMenuStrip";
      this.varContextMenuStrip.Size = new Size(239, 48);
      this.binaryToolStripMenuItem.CheckOnClick = true;
      this.binaryToolStripMenuItem.Name = "binaryToolStripMenuItem";
      this.binaryToolStripMenuItem.ShortcutKeyDisplayString = "Ctrl+B";
      this.binaryToolStripMenuItem.Size = new Size(238, 22);
      this.binaryToolStripMenuItem.Text = "Display values in binary";
      this.binaryToolStripMenuItem.Click += new EventHandler(this.binaryToolStripMenuItem_Click);
      this.copyAllToolStripMenuItem.Name = "copyAllToolStripMenuItem";
      this.copyAllToolStripMenuItem.ShortcutKeyDisplayString = "Ctrl+C";
      this.copyAllToolStripMenuItem.Size = new Size(238, 22);
      this.copyAllToolStripMenuItem.Text = "Copy All items...";
      this.copyAllToolStripMenuItem.Click += new EventHandler(this.copyAllToolStripMenuItem_Click);
      this.toolTip1.AutomaticDelay = 500000;
      this.toolTip1.AutoPopDelay = 5000000;
      this.toolTip1.InitialDelay = 5000000;
      this.toolTip1.IsBalloon = true;
      this.toolTip1.ReshowDelay = 1000000;
      this.toolTip1.ShowAlways = true;
      this.AutoScaleDimensions = new SizeF(6f, 13f);
      this.AutoScaleMode = AutoScaleMode.Font;
      this.ClientSize = new Size(624, 176);
      this.Controls.Add((Control) this.varListView);
      this.Controls.Add((Control) this.radioButton7);
      this.Controls.Add((Control) this.radioButton6);
      this.Controls.Add((Control) this.radioButton5);
      this.Controls.Add((Control) this.radioButton4);
      this.Controls.Add((Control) this.radioButton3);
      this.Controls.Add((Control) this.radioButton2);
      this.Controls.Add((Control) this.radioButton1);
      this.Icon = (Icon) componentResourceManager.GetObject("$this.Icon");
      this.KeyPreview = true;
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = nameof (VarForm);
      this.ShowIcon = false;
      this.ShowInTaskbar = false;
      this.Text = "Variable Display";
      this.Activated += new EventHandler(this.VarForm_Activated);
      this.Deactivate += new EventHandler(this.VarForm_Deactivate);
      this.FormClosing += new FormClosingEventHandler(this.VarForm_FormClosing);
      this.Load += new EventHandler(this.VarForm_Load);
      this.KeyDown += new KeyEventHandler(this.VarForm_KeyDown);
      this.KeyPress += new KeyPressEventHandler(this.VarForm_KeyPress);
      this.varContextMenuStrip.ResumeLayout(false);
      this.ResumeLayout(false);
      this.PerformLayout();
    }

    private VarForm() => this.InitializeComponent();

    [DllImport("user32.dll")]
    private static extern bool IsWindowVisible(IntPtr hWnd);

    public static VarForm MainObj()
    {
      if (VarForm.selfObj == null)
        VarForm.selfObj = new VarForm();
      return VarForm.selfObj;
    }

    public void SetDebugMode(bool isDebugMode)
    {
      if (isDebugMode)
      {
        this._isDirty = true;
        this.varListView.ForeColor = Control.DefaultForeColor;
      }
      else
        this.varListView.ForeColor = Color.Gray;
    }

    private string Convert2Binary(int value)
    {
      uint num1 = (uint) value;
      long[] numArray = new long[8];
      int num2 = 1;
      for (int index = 0; index < 8; ++index)
      {
        numArray[index] = (long) num1 / (long) num2 & 15L;
        num2 *= 16;
      }
      string str1 = "";
      for (int index = 7; index >= 0; --index)
      {
        string str2 = Convert.ToString(numArray[index], 2);
        switch (str2.Length)
        {
          case 0:
            str2 = "0000";
            goto case 4;
          case 1:
            str2 = "000" + str2;
            goto case 4;
          case 2:
            str2 = "00" + str2;
            goto case 4;
          case 3:
            str2 = "0" + str2;
            goto case 4;
          case 4:
            str1 = str1 + ":" + str2;
            continue;
          default:
            str2 = "0000";
            goto case 4;
        }
      }
      return str1;
    }

    public void DisplayVars(
      int playerId,
      VarForm.SysInfo_t sysInfo,
      int[] mugenSysvar,
      int[] mugenVar,
      float[] mugenSysfvar,
      float[] mugenFvar)
    {
      if (this.varListView.Items.Count < 5 || !VarForm.IsWindowVisible(this.Handle))
        return;
      this.Text = "Variable Display (playerId=" + (playerId != 0 ? playerId.ToString() : "None") + ")";
      if (playerId == 0)
      {
        if (!(this.varListView.ForeColor != Color.Gray))
          return;
        this.varListView.ForeColor = Color.Gray;
      }
      else
      {
        if (this.varListView.ForeColor != Control.DefaultForeColor)
          this.varListView.ForeColor = Control.DefaultForeColor;
        switch (this._selectedGroup)
        {
          case 0:
            if (sysInfo.alive != this._sysInfo.alive || this._isDirty)
              this.varListView.Items[0].SubItems[0].Text = "alive=" + (object) sysInfo.alive;
            if (sysInfo.life != this._sysInfo.life || this._isDirty)
              this.varListView.Items[1].SubItems[0].Text = "life=" + (object) sysInfo.life;
            if (sysInfo.win != this._sysInfo.win || this._isDirty)
              this.varListView.Items[0].SubItems[1].Text = sysInfo.win == -1 ? "-" : "win=" + (object) sysInfo.win;
            if (sysInfo.lose != this._sysInfo.lose || this._isDirty)
              this.varListView.Items[1].SubItems[1].Text = sysInfo.lose == -1 ? "-" : "lose=" + (object) sysInfo.lose;
            if (sysInfo.prevstateno != this._sysInfo.prevstateno || this._isDirty)
              this.varListView.Items[0].SubItems[2].Text = "prevstateno=" + (object) sysInfo.prevstateno;
            if (sysInfo.stateno != this._sysInfo.stateno || sysInfo.stateowner != this._sysInfo.stateowner || (sysInfo.specialstateno != this._sysInfo.specialstateno || this._isDirty))
            {
              string str1;
              if (sysInfo.specialstateno != 0)
                str1 = "stateno={" + (object) sysInfo.specialstateno + "} " + (object) sysInfo.stateno;
              else
                str1 = "stateno=" + (object) sysInfo.stateno;
              string str2 = str1;
              if (sysInfo.stateowner != -1)
                str2 = str2 + " (" + (object) sysInfo.stateowner + "'s state)";
              this.varListView.Items[1].SubItems[2].Text = str2;
            }
            if (sysInfo.palno != this._sysInfo.palno || this._isDirty)
              this.varListView.Items[0].SubItems[3].Text = "palno=" + (object) sysInfo.palno;
            if (sysInfo.hitpausetime != this._sysInfo.hitpausetime || this._isDirty)
              this.varListView.Items[1].SubItems[3].Text = "hitpausetime=" + (object) sysInfo.hitpausetime;
            if (sysInfo.roundtime != this._sysInfo.roundtime || this._isDirty)
              this.varListView.Items[0].SubItems[4].Text = "(round-timer=" + (object) sysInfo.roundtime + ")";
            if (sysInfo.roundstate != this._sysInfo.roundstate || this._isDirty)
              this.varListView.Items[1].SubItems[4].Text = "(roundstate=" + (object) sysInfo.roundstate + ")";
            if (!DebugForm.MainObj().IsPaused() && !MugenWindow.MainObj().isDebugBreakMode())
              --this._sysInfo._damageInterval;
            if ((sysInfo.damage != this._sysInfo.damage || this._sysInfo._damageInterval <= 0 || this._isDirty) && (sysInfo.damage != 0 || this._sysInfo.damage == 0 || (this._sysInfo._damageInterval <= 0 || this._isDirty)))
            {
              this._sysInfo._damageInterval = 30;
              this.varListView.Items[2].SubItems[0].Text = "damage=" + (object) sysInfo.damage;
            }
            if (sysInfo.power != this._sysInfo.power || this._isDirty)
              this.varListView.Items[2].SubItems[1].Text = "power=" + (object) sysInfo.power;
            if (sysInfo.ctrl != this._sysInfo.ctrl || this._isDirty)
              this.varListView.Items[2].SubItems[2].Text = "ctrl=" + (object) sysInfo.ctrl;
            if (sysInfo.statetype != this._sysInfo.statetype || this._isDirty)
            {
              string str;
              switch (sysInfo.statetype)
              {
                case 1:
                  str = "statetype=S";
                  break;
                case 2:
                  str = "statetype=C";
                  break;
                case 3:
                  str = "statetype=A";
                  break;
                case 4:
                  str = "statetype=L";
                  break;
                default:
                  str = "statetype=" + (object) sysInfo.statetype;
                  break;
              }
              this.varListView.Items[2].SubItems[3].Text = str;
            }
            if (sysInfo.movetype != this._sysInfo.movetype || this._isDirty)
            {
              string str;
              switch (sysInfo.movetype)
              {
                case 0:
                  str = "movetype=I";
                  break;
                case 1:
                  str = "movetype=A";
                  break;
                case 2:
                  str = "movetype=H";
                  break;
                default:
                  str = "movetype=" + (object) sysInfo.movetype;
                  break;
              }
              this.varListView.Items[2].SubItems[4].Text = str;
            }
            for (int index = 0; index < 5; ++index)
            {
              if (mugenSysvar[index] != this._mugenSysvar[index] || this._isDirty)
              {
                string str;
                if (this.binaryToolStripMenuItem.Checked)
                  str = "sysvar(" + (object) index + ")=b" + this.Convert2Binary(mugenSysvar[index]);
                else
                  str = "sysvar(" + (object) index + ")=" + (object) mugenSysvar[index];
                this.varListView.Items[3].SubItems[index].Text = str;
              }
            }
            for (int index = 0; index < 5; ++index)
            {
              if ((double) mugenSysfvar[index] != (double) this._mugenSysfvar[index] || this._isDirty)
              {
                string str;
                if (this.binaryToolStripMenuItem.Checked)
                {
                  if ((double) mugenSysfvar[index] == (double) (long) mugenSysfvar[index] && (double) mugenSysfvar[index] >= 0.0)
                    str = "sysfvar(" + (object) index + ")=b" + this.Convert2Binary((int) mugenSysfvar[index]);
                  else
                    str = "sysfvar(" + (object) index + ")=" + (object) mugenSysfvar[index];
                }
                else
                  str = "sysfvar(" + (object) index + ")=" + (object) mugenSysfvar[index];
                this.varListView.Items[4].SubItems[index].Text = str;
              }
            }
            break;
          case 1:
          case 2:
          case 3:
            int num1 = (this._selectedGroup - 1) * 20;
            for (int index1 = 0; index1 < 4; ++index1)
            {
              for (int index2 = 0; index2 < 5; ++index2)
              {
                int index3 = index2 + index1 * 5 + num1;
                if (mugenVar[index3] != this._mugenVar[index3] || this._isDirty)
                {
                  string str;
                  if (this.binaryToolStripMenuItem.Checked)
                    str = "var(" + (object) index3 + ")=b" + this.Convert2Binary(mugenVar[index3]);
                  else
                    str = "var(" + (object) index3 + ")=" + (object) mugenVar[index3];
                  this.varListView.Items[index1].SubItems[index2].Text = str;
                }
              }
            }
            switch (this._selectedGroup)
            {
              case 1:
                if (sysInfo.fall_damage != this._sysInfo.fall_damage || this._isDirty)
                  this.varListView.Items[4].SubItems[0].Text = "fall.damage=" + (object) sysInfo.fall_damage;
                if (sysInfo.movecontact != this._sysInfo.movecontact || this._isDirty)
                  this.varListView.Items[4].SubItems[1].Text = "movecontact=" + (object) sysInfo.movecontact;
                if (sysInfo.movehit != this._sysInfo.movehit || this._isDirty)
                  this.varListView.Items[4].SubItems[2].Text = "movehit=" + (object) sysInfo.movehit;
                if (sysInfo.moveguarded != this._sysInfo.moveguarded || this._isDirty)
                  this.varListView.Items[4].SubItems[3].Text = "moveguarded=" + (object) sysInfo.moveguarded;
                if (sysInfo.movereversed != this._sysInfo.movereversed || this._isDirty)
                {
                  this.varListView.Items[4].SubItems[4].Text = "movereversed=" + (object) sysInfo.movereversed;
                  break;
                }
                break;
              case 2:
                if ((double) sysInfo.posx != (double) this._sysInfo.posx || this._isDirty)
                  this.varListView.Items[4].SubItems[0].Text = "pos x=" + (object) sysInfo.posx;
                if ((double) sysInfo.posy != (double) this._sysInfo.posy || this._isDirty)
                  this.varListView.Items[4].SubItems[1].Text = "pos y=" + (object) sysInfo.posy;
                if ((double) sysInfo.velx != (double) this._sysInfo.velx || this._isDirty)
                  this.varListView.Items[4].SubItems[2].Text = "vel x=" + (object) sysInfo.velx;
                if ((double) sysInfo.vely != (double) this._sysInfo.vely || this._isDirty)
                  this.varListView.Items[4].SubItems[3].Text = "vel y=" + (object) sysInfo.vely;
                if (sysInfo.facing != this._sysInfo.facing || this._isDirty)
                {
                  this.varListView.Items[4].SubItems[4].Text = "facing=" + (sysInfo.facing == 1 ? "Right (1)" : "Left (-1)");
                  break;
                }
                break;
              case 3:
                if ((double) sysInfo.attackmulset != (double) this._sysInfo.attackmulset || this._isDirty)
                  this.varListView.Items[4].SubItems[0].Text = "attack-mul=" + (object) sysInfo.attackmulset;
                if (sysInfo.localx != this._sysInfo.localx || this._isDirty)
                  this.varListView.Items[4].SubItems[1].Text = "localcoord-x=" + (object) sysInfo.localx;
                if (sysInfo.localy != this._sysInfo.localy || this._isDirty)
                  this.varListView.Items[4].SubItems[2].Text = "localcoord-y=" + (object) sysInfo.localy;
                if (sysInfo.hasclsn1 != this._sysInfo.hasclsn1 || this._isDirty)
                  this.varListView.Items[4].SubItems[3].Text = "has-clsn1=" + sysInfo.hasclsn1.ToString();
                this.varListView.Items[4].SubItems[4].Text = "";
                break;
            }
            break;
          case 4:
          case 5:
            int num2 = (this._selectedGroup - 4) * 20;
            for (int index1 = 0; index1 < 4; ++index1)
            {
              for (int index2 = 0; index2 < 5; ++index2)
              {
                int index3 = index2 + index1 * 5 + num2;
                if ((double) mugenFvar[index3] != (double) this._mugenFvar[index3] || this._isDirty)
                {
                  string str;
                  if (this.binaryToolStripMenuItem.Checked)
                  {
                    if ((double) mugenFvar[index3] == (double) (long) mugenFvar[index3] && (double) mugenFvar[index3] >= 0.0)
                      str = "fvar(" + (object) index3 + ")=b" + this.Convert2Binary((int) mugenFvar[index3]);
                    else
                      str = "fvar(" + (object) index3 + ")=" + (object) mugenFvar[index3];
                  }
                  else
                    str = "fvar(" + (object) index3 + ")=" + (object) mugenFvar[index3];
                  this.varListView.Items[index1].SubItems[index2].Text = str;
                }
              }
            }
            switch (this._selectedGroup)
            {
              case 4:
                if (sysInfo.target1 != this._sysInfo.target1 || sysInfo.target2 != this._sysInfo.target2 || (sysInfo.target3 != this._sysInfo.target3 || sysInfo.target4 != this._sysInfo.target4) || (sysInfo.target5 != this._sysInfo.target5 || sysInfo.target6 != this._sysInfo.target6 || (sysInfo.target7 != this._sysInfo.target7 || sysInfo.target8 != this._sysInfo.target8)) || this._isDirty)
                {
                  string str = "target=";
                  if (sysInfo.target1 != 0)
                    str += sysInfo.target1.ToString();
                  if (sysInfo.target2 != 0)
                    str = str + "," + sysInfo.target2.ToString();
                  if (sysInfo.target3 != 0)
                    str = str + "," + sysInfo.target3.ToString();
                  if (sysInfo.target4 != 0)
                    str = str + "," + sysInfo.target4.ToString();
                  if (sysInfo.target5 != 0)
                    str = str + "," + sysInfo.target5.ToString();
                  if (sysInfo.target6 != 0)
                    str = str + "," + sysInfo.target6.ToString();
                  if (sysInfo.target7 != 0)
                    str = str + "," + sysInfo.target7.ToString();
                  if (sysInfo.target8 != 0)
                    str = str + "," + sysInfo.target8.ToString();
                  this.varListView.Items[4].SubItems[0].Text = str;
                }
                if (sysInfo.hitby != this._sysInfo.hitby || sysInfo.noclsn2 != this._sysInfo.noclsn2 || (sysInfo.muteki != this._sysInfo.muteki || this._isDirty))
                  this.varListView.Items[4].SubItems[1].Text = sysInfo.noclsn2 == 0 ? (sysInfo.muteki == 0 ? ((sysInfo.hitby & 7) != 0 ? ((sysInfo.hitby & 4088) != 0 ? "hitby=" + this._GetAttrString(sysInfo.hitby) : "hitby=muteki") : "hitby=muteki") : "hitby=unhittable") : "hitby=no-clsn2";
                if (sysInfo.hitoverride0 != this._sysInfo.hitoverride0 || this._isDirty)
                  this.varListView.Items[4].SubItems[2].Text = "hitoverride0=" + this._GetAttrString(sysInfo.hitoverride0);
                if (sysInfo.hitoverride1 != this._sysInfo.hitoverride1 || this._isDirty)
                  this.varListView.Items[4].SubItems[3].Text = "hitoverride1=" + this._GetAttrString(sysInfo.hitoverride1);
                if (sysInfo.hitoverride2 != this._sysInfo.hitoverride2 || this._isDirty)
                {
                  this.varListView.Items[4].SubItems[4].Text = "hitoverride2=" + this._GetAttrString(sysInfo.hitoverride2);
                  break;
                }
                break;
              case 5:
                if (sysInfo.pausetime != this._sysInfo.pausetime || this._isDirty)
                  this.varListView.Items[4].SubItems[0].Text = "(pause-time=" + (object) sysInfo.pausetime + ")";
                if (sysInfo.superpausetime != this._sysInfo.superpausetime || this._isDirty)
                  this.varListView.Items[4].SubItems[1].Text = "(s-pause-time=" + (object) sysInfo.superpausetime + ")";
                if (sysInfo.pausemovetime != this._sysInfo.pausemovetime || this._isDirty)
                  this.varListView.Items[4].SubItems[2].Text = "p-move-time=" + (object) sysInfo.pausemovetime;
                if (sysInfo.superpausemovetime != this._sysInfo.superpausemovetime || this._isDirty)
                  this.varListView.Items[4].SubItems[3].Text = "s-move-time=" + (object) sysInfo.superpausemovetime;
                if (sysInfo.active != this._sysInfo.active || this._isDirty)
                {
                  this.varListView.Items[4].SubItems[4].Text = "is-frozen=" + (object) (sysInfo.active != 0 ? 0 : 1);
                  break;
                }
                break;
            }
            break;
          case 6:
            if (this._isDirty)
            {
              for (int index1 = 0; index1 < 5; ++index1)
              {
                for (int index2 = 0; index2 < 5; ++index2)
                  this.varListView.Items[index1].SubItems[index2].Text = "";
              }
            }
            VarForm.SysInfo_t.GlobalAssertFlags globalAssertFlags;
            for (int index = 0; index < 5; ++index)
            {
              if (sysInfo.assertFlags[index] != this._sysInfo.assertFlags[index] || this._isDirty)
              {
                ListViewItem.ListViewSubItem subItem = this.varListView.Items[0].SubItems[index];
                globalAssertFlags = (VarForm.SysInfo_t.GlobalAssertFlags) index;
                string str = globalAssertFlags.ToString() + "=" + sysInfo.assertFlags[index].ToString();
                subItem.Text = str;
              }
            }
            for (int index = 0; index < 5; ++index)
            {
              if (sysInfo.assertFlags[5 + index] != this._sysInfo.assertFlags[5 + index] || this._isDirty)
              {
                ListViewItem.ListViewSubItem subItem = this.varListView.Items[1].SubItems[index];
                globalAssertFlags = (VarForm.SysInfo_t.GlobalAssertFlags) (5 + index);
                string str = globalAssertFlags.ToString() + "=" + sysInfo.assertFlags[5 + index].ToString();
                subItem.Text = str;
              }
            }
            if (sysInfo.assertFlags[10] != this._sysInfo.assertFlags[10] || this._isDirty)
            {
              ListViewItem.ListViewSubItem subItem = this.varListView.Items[2].SubItems[0];
              globalAssertFlags = VarForm.SysInfo_t.GlobalAssertFlags.NoFG;
              string str = globalAssertFlags.ToString() + "=" + sysInfo.assertFlags[10].ToString();
              subItem.Text = str;
            }
            if (sysInfo.selfAssertFlags.Length != 0)
            {
              VarForm.SysInfo_t.SelfAssertFlags selfAssertFlags;
              for (int index = 0; index < 5; ++index)
              {
                if (sysInfo.selfAssertFlags[index] != this._sysInfo.selfAssertFlags[index] || this._isDirty)
                {
                  ListViewItem.ListViewSubItem subItem = this.varListView.Items[3].SubItems[index];
                  selfAssertFlags = (VarForm.SysInfo_t.SelfAssertFlags) index;
                  string str = selfAssertFlags.ToString() + "=" + sysInfo.selfAssertFlags[index].ToString();
                  subItem.Text = str;
                }
              }
              for (int index = 0; index < 4; ++index)
              {
                if (sysInfo.selfAssertFlags[5 + index] != this._sysInfo.selfAssertFlags[5 + index] || this._isDirty)
                {
                  ListViewItem.ListViewSubItem subItem = this.varListView.Items[4].SubItems[index];
                  selfAssertFlags = (VarForm.SysInfo_t.SelfAssertFlags) (5 + index);
                  string str = selfAssertFlags.ToString() + "=" + sysInfo.selfAssertFlags[5 + index].ToString();
                  subItem.Text = str;
                }
              }
              break;
            }
            break;
        }
        this._isDirty = false;
        sysInfo.CopyTo(this._sysInfo);
        mugenSysvar.CopyTo((Array) this._mugenSysvar, 0);
        mugenVar.CopyTo((Array) this._mugenVar, 0);
        mugenSysfvar.CopyTo((Array) this._mugenSysfvar, 0);
        mugenFvar.CopyTo((Array) this._mugenFvar, 0);
      }
    }

    private string _GetAttrString(int attr)
    {
      string str = "";
      if ((attr & 1) != 0)
        str += "s";
      if ((attr & 2) != 0)
        str += "c";
      if ((attr & 4) != 0)
        str += "a";
      if ((attr & 56) == 56)
      {
        str += ",aa";
      }
      else
      {
        if ((attr & 8) != 0)
          str += ",na";
        if ((attr & 16) != 0)
          str += ",sa";
        if ((attr & 32) != 0)
          str += ",ha";
      }
      if ((attr & 448) == 448)
      {
        str += ",at";
      }
      else
      {
        if ((attr & 64) != 0)
          str += ",nt";
        if ((attr & 128) != 0)
          str += ",st";
        if ((attr & 256) != 0)
          str += ",ht";
      }
      if ((attr & 3584) == 3584)
      {
        str += ",ap";
      }
      else
      {
        if ((attr & 512) != 0)
          str += ",np";
        if ((attr & 1024) != 0)
          str += ",sp";
        if ((attr & 2048) != 0)
          str += ",hp";
      }
      return str;
    }

    private void VarForm_Load(object sender, EventArgs e)
    {
      this.varListView.Items.Clear();
      for (int index = 0; index < 5; ++index)
        this.varListView.Items.Add(new ListViewItem(new string[5]
        {
          " ",
          " ",
          " ",
          " ",
          " "
        }));
      this.Activate();
    }

    private void VarForm_FormClosing(object sender, FormClosingEventArgs e)
    {
      if (e.CloseReason != CloseReason.UserClosing)
        return;
      e.Cancel = true;
      this.Hide();
    }

    private void radioButton1_MouseClick(object sender, MouseEventArgs e)
    {
      Control radioButton1 = (Control) this.radioButton1;
      if (radioButton1 == null || !(radioButton1.Cursor == Cursors.Help))
        return;
      string text = "Displays the values of system parameters such as alive or life and the values of system variables." + Environment.NewLine + "Information that does not belong to any particular player is displayed within brackets." + Environment.NewLine + Environment.NewLine + "Data information:" + Environment.NewLine + "　・round-timer -- Displays how many frames have to be drawn before a time over occurs." + Environment.NewLine + "　・damage -- Displays the value of the gethitvar(damage) parameter." + Environment.NewLine + "Note: Since the value of gethitvar(damage) is technically only reported for one frame, the display function will keep showing the same value for 30 frames to make it easier to see." + Environment.NewLine + "　・stateno -- Displays the current state number the player is in." + Environment.NewLine + "If the player is executing code from the minus states, those will be displayed as follows:" + Environment.NewLine + "       　　　stateno={The minus state whose code is being run} stateno. Where state no is the actual state the player is in." + Environment.NewLine + "For example, if the player is in state 200 while running code from statedef -2 then this will be reported as:" + Environment.NewLine + "       　　　stateno={-2} 200." + Environment.NewLine;
      this.toolTip1.Hide((IWin32Window) radioButton1);
      this.toolTip1.IsBalloon = false;
      this.toolTip1.IsBalloon = true;
      this.toolTip1.SetToolTip(radioButton1, " ");
      this.toolTip1.Show(text, (IWin32Window) radioButton1, radioButton1.Width / 2, radioButton1.Height / 2);
    }

    private void radioButton2_MouseClick(object sender, MouseEventArgs e)
    {
      Control radioButton2 = (Control) this.radioButton2;
      if (radioButton2 == null || !(radioButton2.Cursor == Cursors.Help))
        return;
      string text = "Displays the values of int variables 0 to 19." + Environment.NewLine + "(fall.damage, movecontact, movehit, moveguarded, movereversed)" + Environment.NewLine + Environment.NewLine + "Data information:" + Environment.NewLine + "　・fall.damage -- Displays the value of the gethitvar(fall.damage) parameter." + Environment.NewLine;
      this.toolTip1.Hide((IWin32Window) radioButton2);
      this.toolTip1.IsBalloon = false;
      this.toolTip1.IsBalloon = true;
      this.toolTip1.SetToolTip(radioButton2, " ");
      this.toolTip1.Show(text, (IWin32Window) radioButton2, radioButton2.Width / 2, radioButton2.Height / 2);
    }

    private void radioButton3_MouseClick(object sender, MouseEventArgs e)
    {
      Control radioButton3 = (Control) this.radioButton3;
      if (radioButton3 == null || !(radioButton3.Cursor == Cursors.Help))
        return;
      string text = "Displays the values of int variables 20 to 39." + Environment.NewLine + "(pos x, pos y, vel x, vel y, facing)" + Environment.NewLine;
      this.toolTip1.Hide((IWin32Window) radioButton3);
      this.toolTip1.IsBalloon = false;
      this.toolTip1.IsBalloon = true;
      this.toolTip1.SetToolTip(radioButton3, " ");
      this.toolTip1.Show(text, (IWin32Window) radioButton3, radioButton3.Width / 2, radioButton3.Height / 2);
    }

    private void radioButton4_MouseClick(object sender, MouseEventArgs e)
    {
      Control radioButton4 = (Control) this.radioButton4;
      if (radioButton4 == null || !(radioButton4.Cursor == Cursors.Help))
        return;
      string text = "Displays the values of int variables 40 to 59." + Environment.NewLine + "(attack-mul, noko, intro, roundnotover, timerfreeze)" + Environment.NewLine + "Information that does not belong to any particular player is displayed within brackets." + Environment.NewLine + Environment.NewLine + "[特殊な項目の説明]" + Environment.NewLine + "　・attack-mul -- Shows the value of the attackmulset parameter." + Environment.NewLine;
      this.toolTip1.Hide((IWin32Window) radioButton4);
      this.toolTip1.IsBalloon = false;
      this.toolTip1.IsBalloon = true;
      this.toolTip1.SetToolTip(radioButton4, " ");
      this.toolTip1.Show(text, (IWin32Window) radioButton4, radioButton4.Width / 2, radioButton4.Height / 2);
    }

    private void radioButton5_MouseClick(object sender, MouseEventArgs e)
    {
      Control radioButton5 = (Control) this.radioButton5;
      if (radioButton5 == null || !(radioButton5.Cursor == Cursors.Help))
        return;
      string text = "Displays the values of float variables 0 to 19." + Environment.NewLine + "(target, hitby, hitoverride0, hitoverride1, hitoverride2)" + Environment.NewLine + Environment.NewLine + "Data information:" + Environment.NewLine + "　・hitby -- Shows the hitflags used in an active hitby or nothitby controller." + Environment.NewLine + "              Example: hitby=sca,aa,at,ap" + Environment.NewLine + Environment.NewLine + "       If the flags make the character invinsible, then 'hitby = invincible' will be shown." + Environment.NewLine + "       In the case of a superpause controller making the character invincible, 'hitby = unhittable' will be displayed instead." + Environment.NewLine + "       Finally, if the specified animation contains no vulnerability hitboxes, then 'hitby = no-clsn2' will be shown." + Environment.NewLine + Environment.NewLine + "　・hitoverride0,1,2 --Shows the parameters that are set in each slot of a hitoverride controller." + Environment.NewLine + "              Example: hitoverride0=sca,aa,at" + Environment.NewLine;
      this.toolTip1.Hide((IWin32Window) radioButton5);
      this.toolTip1.IsBalloon = false;
      this.toolTip1.IsBalloon = true;
      this.toolTip1.SetToolTip(radioButton5, " ");
      this.toolTip1.Show(text, (IWin32Window) radioButton5, radioButton5.Width / 2, radioButton5.Height / 2);
    }

    private void radioButton6_MouseClick(object sender, MouseEventArgs e)
    {
      Control radioButton6 = (Control) this.radioButton6;
      if (radioButton6 == null || !(radioButton6.Cursor == Cursors.Help))
        return;
      string text = "Displays the values of float variables 20 to 39." + Environment.NewLine + "(pause-time, s-pause-time, p-move-time, s-move-time, is-frozen)" + Environment.NewLine + "Information that does not belong to any particular player is displayed within brackets." + Environment.NewLine + Environment.NewLine + "Data information:" + Environment.NewLine + "　・is-frozen -- Displays if the object is frozen by the effect of a pause controller." + Environment.NewLine + "       Normally, this will be 0. This will display 1 if the object is frozen by a pause or superpause." + Environment.NewLine + "　・pause-time -- Shows the time parameter specified in a pause controller." + Environment.NewLine + "　・s-pause-time -- Shows the time specified in a super pause controller." + Environment.NewLine + "　・p-move-time -- Shows the value of movetime specified in a pause controller." + Environment.NewLine + "　・s-move-time -- Shows the value of movetime specified in a superpause controller." + Environment.NewLine;
      this.toolTip1.Hide((IWin32Window) radioButton6);
      this.toolTip1.IsBalloon = false;
      this.toolTip1.IsBalloon = true;
      this.toolTip1.SetToolTip(radioButton6, " ");
      this.toolTip1.Show(text, (IWin32Window) radioButton6, radioButton6.Width / 2, radioButton6.Height / 2);
    }

    private void radioButton7_MouseClick(object sender, MouseEventArgs e)
    {
      Control radioButton7 = (Control) this.radioButton7;
      if (radioButton7 == null || !(radioButton7.Cursor == Cursors.Help))
        return;
      string text = "Displays the values of globally-active AssertSpecial flags." + Environment.NewLine;
      this.toolTip1.Hide((IWin32Window) radioButton7);
      this.toolTip1.IsBalloon = false;
      this.toolTip1.IsBalloon = true;
      this.toolTip1.SetToolTip(radioButton7, " ");
      this.toolTip1.Show(text, (IWin32Window) radioButton7, radioButton7.Width / 2, radioButton7.Height / 2);
    }

    private void radioButton1_CheckedChanged(object sender, EventArgs e)
    {
      Control radioButton1 = (Control) this.radioButton1;
      if (radioButton1 != null && radioButton1.Cursor == Cursors.Help)
      {
        if (!this.radioButton1.Checked)
          this.radioButton1.Checked = true;
        if (!this.radioButton1.Checked)
          return;
        this._isDirty = true;
        this._selectedGroup = 0;
      }
      else
      {
        if (this.radioButton1.Checked)
        {
          this._isDirty = true;
          this._selectedGroup = 0;
        }
        if (DebugForm.MainObj().IsPaused())
          return;
        MugenWindow.MainObj().Activate();
      }
    }

    private void radioButton2_CheckedChanged(object sender, EventArgs e)
    {
      Control radioButton2 = (Control) this.radioButton2;
      if (radioButton2 != null && radioButton2.Cursor == Cursors.Help)
        return;
      if (this.radioButton2.Checked)
      {
        this._isDirty = true;
        this._selectedGroup = 1;
      }
      if (DebugForm.MainObj().IsPaused())
        return;
      MugenWindow.MainObj().Activate();
    }

    private void radioButton3_CheckedChanged(object sender, EventArgs e)
    {
      Control radioButton3 = (Control) this.radioButton3;
      if (radioButton3 != null && radioButton3.Cursor == Cursors.Help)
        return;
      if (this.radioButton3.Checked)
      {
        this._isDirty = true;
        this._selectedGroup = 2;
      }
      if (DebugForm.MainObj().IsPaused())
        return;
      MugenWindow.MainObj().Activate();
    }

    private void radioButton4_CheckedChanged(object sender, EventArgs e)
    {
      Control radioButton4 = (Control) this.radioButton4;
      if (radioButton4 != null && radioButton4.Cursor == Cursors.Help)
        return;
      if (this.radioButton4.Checked)
      {
        this._isDirty = true;
        this._selectedGroup = 3;
      }
      if (DebugForm.MainObj().IsPaused())
        return;
      MugenWindow.MainObj().Activate();
    }

    private void radioButton5_CheckedChanged(object sender, EventArgs e)
    {
      Control radioButton5 = (Control) this.radioButton5;
      if (radioButton5 != null && radioButton5.Cursor == Cursors.Help)
        return;
      if (this.radioButton5.Checked)
      {
        this._isDirty = true;
        this._selectedGroup = 4;
      }
      if (DebugForm.MainObj().IsPaused())
        return;
      MugenWindow.MainObj().Activate();
    }

    private void radioButton6_CheckedChanged(object sender, EventArgs e)
    {
      Control radioButton6 = (Control) this.radioButton6;
      if (radioButton6 != null && radioButton6.Cursor == Cursors.Help)
        return;
      if (this.radioButton6.Checked)
      {
        this._isDirty = true;
        this._selectedGroup = 5;
      }
      if (DebugForm.MainObj().IsPaused())
        return;
      MugenWindow.MainObj().Activate();
    }

    private void radioButton7_CheckedChanged(object sender, EventArgs e)
    {
      Control radioButton7 = (Control) this.radioButton7;
      if (radioButton7 != null && radioButton7.Cursor == Cursors.Help)
        return;
      if (this.radioButton7.Checked)
      {
        this._isDirty = true;
        this._selectedGroup = 6;
      }
      if (DebugForm.MainObj().IsPaused())
        return;
      MugenWindow.MainObj().Activate();
    }

    private void varListView_MouseUp(object sender, MouseEventArgs e)
    {
    }

    private void VarForm_KeyDown(object sender, KeyEventArgs e)
    {
      if (this.Cursor == Cursors.Help || e.KeyValue != 19)
        return;
      DebugForm.MainObj().TogglePauseCheckBox();
      e.Handled = true;
    }

    private void VarForm_KeyPress(object sender, KeyPressEventArgs e)
    {
      if (this.Cursor == Cursors.Help)
        return;
      if (e.KeyChar == '\x0004')
      {
        DebugForm.MainObj().ShiftDebugTargetNo();
        e.Handled = true;
      }
      else if (e.KeyChar == '\x0003')
      {
        this._copyAll();
        e.Handled = true;
      }
      else if (e.KeyChar == '\x0002')
      {
        if (MugenWindow.MainObj().getMugenProcess() != null)
        {
          this._isDirty = true;
          this.binaryToolStripMenuItem.Checked = !this.binaryToolStripMenuItem.Checked;
          if (this.binaryToolStripMenuItem.Checked)
          {
            this.toolTip1.Hide((IWin32Window) this.varListView);
            this.toolTip1.Show("Values are now being displayed in binary.　　　　　　　", (IWin32Window) this.varListView, 0, -20, 1000);
          }
          else
          {
            this.toolTip1.Hide((IWin32Window) this.varListView);
            this.toolTip1.Show("Values are now being displayed in decimal.　　　　　　　", (IWin32Window) this.varListView, 0, -20, 1000);
          }
        }
        e.Handled = true;
      }
      else if (e.KeyChar == '\x0012')
      {
        DebugForm.MainObj().ToggleSpeedModeCheckBox();
        e.Handled = true;
      }
      else
      {
        if (e.KeyChar != '\x001B')
          return;
        if (MugenWindow.MainObj().getMugenProcess() != null)
          MugenWindow.MainObj().InjectESC();
        e.Handled = true;
      }
    }

    private void _copyAll()
    {
      if (this.varListView.Items.Count == 0 || this.varListView.ForeColor != Control.DefaultForeColor)
        return;
      string text1 = "Variable name\tValue\tVariable name\tValue\tVariable name\tValue\tVariable name\tValue\tVariable name\tValue" + Environment.NewLine;
      for (int index1 = 0; index1 < this.varListView.Items.Count; ++index1)
      {
        for (int index2 = 0; index2 < 5; ++index2)
        {
          string text2 = this.varListView.Items[index1].SubItems[index2].Text;
          int length = text2.IndexOf('=');
          if (text2 != null && length > 0)
            text1 = text1 + text2.Substring(0, length) + "\t" + text2.Substring(length + 1);
          if (index2 < 4)
            text1 += "\t";
        }
        text1 += Environment.NewLine;
      }
      Clipboard.SetText(text1);
      this.toolTip1.Hide((IWin32Window) this.varListView);
      this.toolTip1.Show("Copied the current variable list to the clipboard.", (IWin32Window) this.varListView, 0, -20, 1000);
    }

    private void copyAllToolStripMenuItem_Click(object sender, EventArgs e)
    {
      Control contextMenuStrip = (Control) this.varContextMenuStrip;
      if (contextMenuStrip != null && contextMenuStrip.Cursor == Cursors.Help)
      {
        this.varContextMenuStrip.Show();
        string text = "Copies the current variable list in table form to the clipboard." + Environment.NewLine + "(The data is pre-formatted for use in programs such as Excel.)" + Environment.NewLine;
        this.toolTip1.Hide((IWin32Window) contextMenuStrip);
        this.toolTip1.IsBalloon = false;
        this.toolTip1.IsBalloon = true;
        this.toolTip1.SetToolTip(contextMenuStrip, " ");
        this.toolTip1.Show(text, (IWin32Window) contextMenuStrip, this.binaryToolStripMenuItem.Width / 2, this.binaryToolStripMenuItem.Height * 3 / 2);
      }
      else
        this._copyAll();
    }

    private void binaryToolStripMenuItem_Click(object sender, EventArgs e)
    {
      Control contextMenuStrip = (Control) this.varContextMenuStrip;
      if (contextMenuStrip != null && contextMenuStrip.Cursor == Cursors.Help)
      {
        this.varContextMenuStrip.Show();
        string text = "Displays the values in binary notation. The 'b' prefix is used to denote a number in binary notation." + Environment.NewLine + "　　　　Example: b101101 which is 45 in decimal." + Environment.NewLine + "(Note: The value of system variables such as alive or life will not be affected.)" + Environment.NewLine + "(Note: For float variables sysfvar and fvar, the value will only be shown in binary notation if it is an integer.)" + Environment.NewLine;
        this.toolTip1.Hide((IWin32Window) contextMenuStrip);
        this.toolTip1.IsBalloon = false;
        this.toolTip1.IsBalloon = true;
        this.toolTip1.SetToolTip(contextMenuStrip, " ");
        this.toolTip1.Show(text, (IWin32Window) contextMenuStrip, this.binaryToolStripMenuItem.Width / 2, this.binaryToolStripMenuItem.Height / 2);
      }
      else
      {
        if (MugenWindow.MainObj().getMugenProcess() == null)
          return;
        if (this.binaryToolStripMenuItem.Checked)
        {
          this.toolTip1.Hide((IWin32Window) this.varListView);
          this.toolTip1.Show("Values are now being displayed in binary.　　　　　　　", (IWin32Window) this.varListView, 0, -20, 1000);
        }
        else
        {
          this.toolTip1.Hide((IWin32Window) this.varListView);
          this.toolTip1.Show("values are now being displayed in decimal.　　　　　　　", (IWin32Window) this.varListView, 0, -20, 1000);
        }
        this._isDirty = true;
      }
    }

    private void varListView_ColumnClick(object sender, ColumnClickEventArgs e)
    {
      if (this.varListView.Cursor == Cursors.Help)
      {
        this.toolTip1.Hide((IWin32Window) this);
        this.toolTip1.IsBalloon = false;
        this.toolTip1.IsBalloon = true;
        string text = "If the cell sizes are too small for the data to be displayed, you can expand it by double clicking the parameter cell at the top." + Environment.NewLine + "(You can also restore the sizes to default by clicking once more.)" + Environment.NewLine;
        this.toolTip1.SetToolTip((Control) this.varListView, " ");
        int num = 0;
        for (int index = 0; index < e.Column; ++index)
          num += this.varListView.Columns[index].Width;
        this.toolTip1.Show(text, (IWin32Window) this.varListView, num + this.varListView.Columns[e.Column].Width / 2, 10);
      }
      else
      {
        if (!this._columState[e.Column])
        {
          this._columState[e.Column] = true;
          if (this.binaryToolStripMenuItem.Checked)
            this.varListView.Columns[e.Column].Width = 280;
          else
            this.varListView.Columns[e.Column].Width = 200;
        }
        else
        {
          this._columState[e.Column] = false;
          this.varListView.Columns[e.Column].Width = 115;
        }
        if (DebugForm.MainObj().IsPaused())
          return;
        MugenWindow.MainObj().Activate();
      }
    }

    public void SetHelpMode(bool isHelpMode)
    {
      if (isHelpMode)
      {
        this.Cursor = Cursors.Help;
        this.varListView.Cursor = Cursors.Help;
        this.radioButton1.Cursor = Cursors.Help;
        this.radioButton2.Cursor = Cursors.Help;
        this.radioButton3.Cursor = Cursors.Help;
        this.radioButton4.Cursor = Cursors.Help;
        this.radioButton5.Cursor = Cursors.Help;
        this.radioButton6.Cursor = Cursors.Help;
        this.varContextMenuStrip.Cursor = Cursors.Help;
      }
      else
      {
        this.Cursor = Cursors.Default;
        this.varListView.Cursor = Cursors.Default;
        this.radioButton1.Cursor = Cursors.Default;
        this.radioButton2.Cursor = Cursors.Default;
        this.radioButton3.Cursor = Cursors.Default;
        this.radioButton4.Cursor = Cursors.Default;
        this.radioButton5.Cursor = Cursors.Default;
        this.radioButton6.Cursor = Cursors.Default;
        this.varContextMenuStrip.Cursor = Cursors.Default;
      }
    }

    private void VarForm_Deactivate(object sender, EventArgs e)
    {
      if (!(this.Cursor == Cursors.Help))
        return;
      this.toolTip1.Hide((IWin32Window) this);
    }

    private void varListView_DisplayHelp(object sender, MouseEventArgs e)
    {
      if (e.Button == MouseButtons.Right)
      {
        if (!(this.varListView.Cursor == Cursors.Help))
          return;
        this.toolTip1.Hide((IWin32Window) this);
      }
      else
      {
        if (e.Button != MouseButtons.Left || !(this.varListView.Cursor == Cursors.Help))
          return;
        this.toolTip1.Hide((IWin32Window) this);
        this.toolTip1.IsBalloon = false;
        this.toolTip1.IsBalloon = true;
        string text = "Displays the current value of player variables." + Environment.NewLine + "If the cell sizes are too small for the data to be displayed, you can expand it by double clicking the parameter cell at the top." + Environment.NewLine + "(You can also restore the sizes to default by clicking once more.)" + Environment.NewLine + Environment.NewLine + "Finally, more tools are available if you right click on one of the items.";
        this.toolTip1.SetToolTip((Control) this.varListView, " ");
        this.toolTip1.Show(text, (IWin32Window) this.varListView, this.varListView.Width / 2, this.varListView.Height / 2);
      }
    }

    private void varListView_MouseClick(object sender, MouseEventArgs e) => this.varListView_DisplayHelp(sender, e);

    private void varListView_MouseDown(object sender, MouseEventArgs e) => this.varListView_DisplayHelp(sender, e);

    private void VarForm_Activated(object sender, EventArgs e)
    {
    }

    private void varListView_SelectedIndexChanged(object sender, EventArgs e)
    {
    }

    public class SysInfo_t
    {
      public int alive;
      public int win;
      public int lose;
      public int life;
      public int palno;
      public int hitpausetime;
      public int stateowner;
      public int stateno;
      public int specialstateno;
      public int prevstateno;
      public int roundtime;
      public int roundstate;
      public int power;
      public int ctrl;
      public int damage;
      public int _damageInterval;
      public int statetype;
      public int movetype;
      public float posx;
      public float posy;
      public float velx;
      public float vely;
      public int facing;
      public int fall_damage;
      public int movecontact;
      public int movehit;
      public int moveguarded;
      public int movereversed;
      public int active;
      public int noko;
      public int intro;
      public int roundnotover;
      public int timerfreeze;
      public bool[] assertFlags = new bool[11];
      public bool[] selfAssertFlags = new bool[9];
      public int hitby;
      public int muteki;
      public int noclsn2;
      public bool hasclsn1;
      public int hitoverride0;
      public int hitoverride1;
      public int hitoverride2;
      public int target1;
      public int target2;
      public int target3;
      public int target4;
      public int target5;
      public int target6;
      public int target7;
      public int target8;
      public int pausetime;
      public int superpausetime;
      public int pausemovetime;
      public int superpausemovetime;
      public float attackmulset;
      public double localx;
      public double localy;

      public void CopyTo(VarForm.SysInfo_t obj)
      {
        obj.assertFlags = this.assertFlags;
        obj.selfAssertFlags = this.selfAssertFlags;
        obj.localx = this.localx;
        obj.localy = this.localy;
        obj.alive = this.alive;
        obj.win = this.win;
        obj.lose = this.lose;
        obj.life = this.life;
        obj.palno = this.palno;
        obj.hitpausetime = this.hitpausetime;
        obj.stateowner = this.stateowner;
        obj.stateno = this.stateno;
        obj.specialstateno = this.specialstateno;
        obj.prevstateno = this.prevstateno;
        obj.roundtime = this.roundtime;
        obj.roundstate = this.roundstate;
        obj.power = this.power;
        obj.ctrl = this.ctrl;
        obj.damage = this.damage;
        obj.statetype = this.statetype;
        obj.movetype = this.movetype;
        obj.posx = this.posx;
        obj.posy = this.posy;
        obj.velx = this.velx;
        obj.vely = this.vely;
        obj.facing = this.facing;
        obj.fall_damage = this.fall_damage;
        obj.movecontact = this.movecontact;
        obj.movehit = this.movehit;
        obj.moveguarded = this.moveguarded;
        obj.movereversed = this.movereversed;
        obj.active = this.active;
        obj.noko = this.noko;
        obj.intro = this.intro;
        obj.roundnotover = this.roundnotover;
        obj.timerfreeze = this.timerfreeze;
        obj.hitby = this.hitby;
        obj.muteki = this.muteki;
        obj.noclsn2 = this.noclsn2;
        obj.hasclsn1 = this.hasclsn1;
        obj.hitoverride0 = this.hitoverride0;
        obj.hitoverride1 = this.hitoverride1;
        obj.hitoverride2 = this.hitoverride2;
        obj.target1 = this.target1;
        obj.target2 = this.target2;
        obj.target3 = this.target3;
        obj.target4 = this.target4;
        obj.target5 = this.target5;
        obj.target6 = this.target6;
        obj.target7 = this.target7;
        obj.target8 = this.target8;
        obj.pausetime = this.pausetime;
        obj.superpausetime = this.superpausetime;
        obj.pausemovetime = this.pausemovetime;
        obj.superpausemovetime = this.superpausemovetime;
        obj.attackmulset = this.attackmulset;
      }

      public enum GlobalAssertFlags
      {
        Intro,
        RoundNotOver,
        NoKO,
        NoKOSnd,
        NoKOSlow,
        NoMusic,
        GlobalNoShadow,
        TimerFreeze,
        NoBarDisplay,
        NoBG,
        NoFG,
      }

      public enum SelfAssertFlags
      {
        NoStandGuard,
        NoCrouchGuard,
        NoAirGuard,
        NoAutoTurn,
        NoShadow,
        NoJuggleCheck,
        NoWalk,
        Unguardable,
        Invisible,
      }

      public enum HitByFlags
      {
        HITBY_S = 1,
        HITBY_C = 2,
        HITBY_A = 4,
        HITBY_na = 8,
        HITBY_sa = 16, // 0x00000010
        HITBY_ha = 32, // 0x00000020
        HITBY_nt = 64, // 0x00000040
        HITBY_st = 128, // 0x00000080
        HITBY_ht = 256, // 0x00000100
        HITBY_np = 512, // 0x00000200
        HITBY_sp = 1024, // 0x00000400
        HITBY_hp = 2048, // 0x00000800
      }
    }
  }
}
