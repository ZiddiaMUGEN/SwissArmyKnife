// Decompiled with JetBrains decompiler
// Type: SwissArmyKnifeForMugen.VarForm
// Assembly: SAKnifeWM, Version=1.0.8.0, Culture=neutral, PublicKeyToken=null
// MVID: 09478AD8-365C-4BF3-BEA1-B5785151259B
// Assembly location: C:\Users\ziddi\Downloads\Swiss Army Knife 1.1 Conversion\Swiss Army Knife 1.1 Conversion\SAKnifeWM.exe

using SwissArmyKnifeForMugen.Utils;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace SwissArmyKnifeForMugen.Displays
{
    /// <summary>
    /// displays variables and a bunch of other player data.
    /// <br/>also used as the primary storage for all of this data.
    /// </summary>
    public class VarForm : Form
    {
        private bool _isDirty = true;
        // represents all the core data for selected player
        private SysInfo_t _sysInfo = new SysInfo_t();
        // variable value storage
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
            if (disposing && components != null)
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            components = new Container();
            ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(VarForm));
            radioButton1 = new RadioButton();
            radioButton2 = new RadioButton();
            radioButton3 = new RadioButton();
            radioButton4 = new RadioButton();
            radioButton5 = new RadioButton();
            radioButton6 = new RadioButton();
            radioButton7 = new RadioButton();
            varListView = new BufferedListView();
            columnHeader1 = new ColumnHeader();
            columnHeader2 = new ColumnHeader();
            columnHeader3 = new ColumnHeader();
            columnHeader4 = new ColumnHeader();
            columnHeader5 = new ColumnHeader();
            varContextMenuStrip = new ContextMenuStrip(components);
            binaryToolStripMenuItem = new ToolStripMenuItem();
            copyAllToolStripMenuItem = new ToolStripMenuItem();
            toolTip1 = new ToolTip(components);
            varContextMenuStrip.SuspendLayout();
            SuspendLayout();
            radioButton1.AutoSize = true;
            radioButton1.Location = new Point(19, 10);
            radioButton1.Name = "radioButton1";
            radioButton1.Size = new Size(57, 17);
            radioButton1.TabIndex = 0;
            radioButton1.TabStop = true;
            radioButton1.Text = "system";
            radioButton1.UseVisualStyleBackColor = true;
            radioButton1.CheckedChanged += new EventHandler(radioButton1_CheckedChanged);
            radioButton1.MouseClick += new MouseEventHandler(radioButton1_MouseClick);
            radioButton2.AutoSize = true;
            radioButton2.Location = new Point(82, 10);
            radioButton2.Name = "radioButton2";
            radioButton2.Size = new Size(77, 17);
            radioButton2.TabIndex = 1;
            radioButton2.TabStop = true;
            radioButton2.Text = "var(0~19)";
            radioButton2.UseVisualStyleBackColor = true;
            radioButton2.CheckedChanged += new EventHandler(radioButton2_CheckedChanged);
            radioButton2.MouseClick += new MouseEventHandler(radioButton2_MouseClick);
            radioButton3.AutoSize = true;
            radioButton3.Location = new Point(165, 10);
            radioButton3.Name = "radioButton3";
            radioButton3.Size = new Size(77, 17);
            radioButton3.TabIndex = 2;
            radioButton3.TabStop = true;
            radioButton3.Text = "var(20~39)";
            radioButton3.UseVisualStyleBackColor = true;
            radioButton3.CheckedChanged += new EventHandler(radioButton3_CheckedChanged);
            radioButton3.MouseClick += new MouseEventHandler(radioButton3_MouseClick);
            radioButton4.AutoSize = true;
            radioButton4.Location = new Point(248, 10);
            radioButton4.Name = "radioButton4";
            radioButton4.Size = new Size(77, 17);
            radioButton4.TabIndex = 3;
            radioButton4.TabStop = true;
            radioButton4.Text = "var(40~59)";
            radioButton4.UseVisualStyleBackColor = true;
            radioButton4.CheckedChanged += new EventHandler(radioButton4_CheckedChanged);
            radioButton4.MouseClick += new MouseEventHandler(radioButton4_MouseClick);
            radioButton5.AutoSize = true;
            radioButton5.Location = new Point(331, 10);
            radioButton5.Name = "radioButton5";
            radioButton5.Size = new Size(77, 17);
            radioButton5.TabIndex = 4;
            radioButton5.TabStop = true;
            radioButton5.Text = "fvar(0~19)";
            radioButton5.UseVisualStyleBackColor = true;
            radioButton5.CheckedChanged += new EventHandler(radioButton5_CheckedChanged);
            radioButton5.MouseClick += new MouseEventHandler(radioButton5_MouseClick);
            radioButton6.AutoSize = true;
            radioButton6.Location = new Point(414, 10);
            radioButton6.Name = "radioButton6";
            radioButton6.Size = new Size(77, 17);
            radioButton6.TabIndex = 5;
            radioButton6.TabStop = true;
            radioButton6.Text = "fvar(20~39)";
            radioButton6.UseVisualStyleBackColor = true;
            radioButton6.CheckedChanged += new EventHandler(radioButton6_CheckedChanged);
            radioButton6.MouseClick += new MouseEventHandler(radioButton6_MouseClick);
            radioButton7.AutoSize = true;
            radioButton7.Location = new Point(497, 10);
            radioButton7.Name = "radioButton7";
            radioButton7.Size = new Size(77, 17);
            radioButton7.TabIndex = 6;
            radioButton7.TabStop = true;
            radioButton7.Text = "AssertSpecial";
            radioButton7.UseVisualStyleBackColor = true;
            radioButton7.CheckedChanged += new EventHandler(radioButton7_CheckedChanged);
            radioButton7.MouseClick += new MouseEventHandler(radioButton7_MouseClick);
            varListView.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            varListView.Columns.AddRange(new ColumnHeader[5]
            {
        columnHeader1,
        columnHeader2,
        columnHeader3,
        columnHeader4,
        columnHeader5
            });
            varListView.ContextMenuStrip = varContextMenuStrip;
            varListView.FullRowSelect = true;
            varListView.GridLines = true;
            varListView.LabelWrap = false;
            varListView.Location = new Point(12, 35);
            varListView.MultiSelect = false;
            varListView.Name = "varListView";
            varListView.Size = new Size(600, 135);
            varListView.TabIndex = 6;
            varListView.TabStop = false;
            varListView.UseCompatibleStateImageBehavior = false;
            varListView.View = View.Details;
            varListView.ColumnClick += new ColumnClickEventHandler(varListView_ColumnClick);
            varListView.SelectedIndexChanged += new EventHandler(varListView_SelectedIndexChanged);
            varListView.MouseClick += new MouseEventHandler(varListView_MouseClick);
            varListView.MouseDown += new MouseEventHandler(varListView_MouseDown);
            varListView.MouseUp += new MouseEventHandler(varListView_MouseUp);
            columnHeader1.Text = "Click to adjust size.";
            columnHeader1.Width = 115;
            columnHeader2.Text = "";
            columnHeader2.Width = 115;
            columnHeader3.Text = "";
            columnHeader3.Width = 115;
            columnHeader4.Text = "";
            columnHeader4.Width = 115;
            columnHeader5.Text = "";
            columnHeader5.Width = 115;
            varContextMenuStrip.Items.AddRange(new ToolStripItem[2]
            {
         binaryToolStripMenuItem,
         copyAllToolStripMenuItem
            });
            varContextMenuStrip.Name = "debugContextMenuStrip";
            varContextMenuStrip.Size = new Size(239, 48);
            binaryToolStripMenuItem.CheckOnClick = true;
            binaryToolStripMenuItem.Name = "binaryToolStripMenuItem";
            binaryToolStripMenuItem.ShortcutKeyDisplayString = "Ctrl+B";
            binaryToolStripMenuItem.Size = new Size(238, 22);
            binaryToolStripMenuItem.Text = "Display values in binary";
            binaryToolStripMenuItem.Click += new EventHandler(binaryToolStripMenuItem_Click);
            copyAllToolStripMenuItem.Name = "copyAllToolStripMenuItem";
            copyAllToolStripMenuItem.ShortcutKeyDisplayString = "Ctrl+C";
            copyAllToolStripMenuItem.Size = new Size(238, 22);
            copyAllToolStripMenuItem.Text = "Copy All items...";
            copyAllToolStripMenuItem.Click += new EventHandler(copyAllToolStripMenuItem_Click);
            toolTip1.AutomaticDelay = 500000;
            toolTip1.AutoPopDelay = 5000000;
            toolTip1.InitialDelay = 5000000;
            toolTip1.IsBalloon = true;
            toolTip1.ReshowDelay = 1000000;
            toolTip1.ShowAlways = true;
            AutoScaleDimensions = new SizeF(6f, 13f);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(624, 176);
            Controls.Add(varListView);
            Controls.Add(radioButton7);
            Controls.Add(radioButton6);
            Controls.Add(radioButton5);
            Controls.Add(radioButton4);
            Controls.Add(radioButton3);
            Controls.Add(radioButton2);
            Controls.Add(radioButton1);
            Icon = (Icon)componentResourceManager.GetObject("$this.Icon");
            KeyPreview = true;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = nameof(VarForm);
            ShowIcon = false;
            ShowInTaskbar = false;
            Text = "Variable Display";
            Activated += new EventHandler(VarForm_Activated);
            Deactivate += new EventHandler(VarForm_Deactivate);
            FormClosing += new FormClosingEventHandler(VarForm_FormClosing);
            Load += new EventHandler(VarForm_Load);
            KeyDown += new KeyEventHandler(VarForm_KeyDown);
            KeyPress += new KeyPressEventHandler(VarForm_KeyPress);
            varContextMenuStrip.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        private VarForm() => InitializeComponent();

        [DllImport("user32.dll")]
        private static extern bool IsWindowVisible(IntPtr hWnd);

        public static VarForm MainObj()
        {
            if (selfObj == null)
                selfObj = new VarForm();
            return selfObj;
        }

        /// <summary>
        /// enable/disable form based on debug mode
        /// </summary>
        /// <param name="isDebugMode"></param>
        public void SetDebugMode(bool isDebugMode)
        {
            if (isDebugMode)
            {
                _isDirty = true;
                varListView.ForeColor = DefaultForeColor;
            }
            else
                varListView.ForeColor = Color.Gray;
        }

        /// <summary>
        /// util for converting int to a string-based binary representation
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        private string Convert2Binary(int value)
        {
            uint num1 = (uint)value;
            long[] numArray = new long[8];
            int num2 = 1;
            for (int index = 0; index < 8; ++index)
            {
                numArray[index] = num1 / num2 & 15L;
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

        /// <summary>
        /// populates the display with variable and SysInfo_t data
        /// </summary>
        /// <param name="playerId"></param>
        /// <param name="sysInfo"></param>
        /// <param name="mugenSysvar"></param>
        /// <param name="mugenVar"></param>
        /// <param name="mugenSysfvar"></param>
        /// <param name="mugenFvar"></param>
        public void DisplayVars(
          int playerId,
          SysInfo_t sysInfo,
          int[] mugenSysvar,
          int[] mugenVar,
          float[] mugenSysfvar,
          float[] mugenFvar)
        {
            if (varListView.Items.Count < 5 || !IsWindowVisible(Handle))
                return;
            Text = "Variable Display (playerId=" + (playerId != 0 ? playerId.ToString() : "None") + ")";
            if (playerId == 0)
            {
                if (!(varListView.ForeColor != Color.Gray))
                    return;
                varListView.ForeColor = Color.Gray;
            }
            else
            {
                if (varListView.ForeColor != DefaultForeColor)
                    varListView.ForeColor = DefaultForeColor;
                switch (_selectedGroup)
                {
                    case 0:
                        if (sysInfo.alive != _sysInfo.alive || _isDirty)
                            varListView.Items[0].SubItems[0].Text = "alive=" + sysInfo.alive;
                        if (sysInfo.life != _sysInfo.life || _isDirty)
                            varListView.Items[1].SubItems[0].Text = "life=" + sysInfo.life;
                        if (sysInfo.win != _sysInfo.win || _isDirty)
                            varListView.Items[0].SubItems[1].Text = sysInfo.win == -1 ? "-" : "win=" + sysInfo.win;
                        if (sysInfo.lose != _sysInfo.lose || _isDirty)
                            varListView.Items[1].SubItems[1].Text = sysInfo.lose == -1 ? "-" : "lose=" + sysInfo.lose;
                        if (sysInfo.prevstateno != _sysInfo.prevstateno || _isDirty)
                            varListView.Items[0].SubItems[2].Text = "prevstateno=" + sysInfo.prevstateno;
                        if (sysInfo.stateno != _sysInfo.stateno || sysInfo.stateowner != _sysInfo.stateowner || sysInfo.specialstateno != _sysInfo.specialstateno || _isDirty)
                        {
                            string str1;
                            if (sysInfo.specialstateno != 0)
                                str1 = "stateno={" + sysInfo.specialstateno + "} " + sysInfo.stateno;
                            else
                                str1 = "stateno=" + sysInfo.stateno;
                            string str2 = str1;
                            if (sysInfo.stateowner != -1)
                                str2 = str2 + " (" + sysInfo.stateowner + "'s state)";
                            varListView.Items[1].SubItems[2].Text = str2;
                        }
                        if (sysInfo.palno != _sysInfo.palno || _isDirty)
                            varListView.Items[0].SubItems[3].Text = "palno=" + sysInfo.palno;
                        if (sysInfo.hitpausetime != _sysInfo.hitpausetime || _isDirty)
                            varListView.Items[1].SubItems[3].Text = "hitpausetime=" + sysInfo.hitpausetime;
                        if (sysInfo.roundtime != _sysInfo.roundtime || _isDirty)
                            varListView.Items[0].SubItems[4].Text = "(round-timer=" + sysInfo.roundtime + ")";
                        if (sysInfo.roundstate != _sysInfo.roundstate || _isDirty)
                            varListView.Items[1].SubItems[4].Text = "(roundstate=" + sysInfo.roundstate + ")";
                        if (!DebugForm.MainObj().IsPaused() && !MugenWindow.MainObj().isDebugBreakMode())
                            --_sysInfo._damageInterval;
                        if ((sysInfo.damage != _sysInfo.damage || _sysInfo._damageInterval <= 0 || _isDirty) && (sysInfo.damage != 0 || _sysInfo.damage == 0 || _sysInfo._damageInterval <= 0 || _isDirty))
                        {
                            _sysInfo._damageInterval = 30;
                            varListView.Items[2].SubItems[0].Text = "damage=" + sysInfo.damage;
                        }
                        if (sysInfo.power != _sysInfo.power || _isDirty)
                            varListView.Items[2].SubItems[1].Text = "power=" + sysInfo.power;
                        if (sysInfo.ctrl != _sysInfo.ctrl || _isDirty)
                            varListView.Items[2].SubItems[2].Text = "ctrl=" + sysInfo.ctrl;
                        if (sysInfo.statetype != _sysInfo.statetype || _isDirty)
                        {
                            string str = string.Format("statetype={0}", sysInfo.statetype);
                            if (Enum.IsDefined(typeof(SysInfo_t.StateType), sysInfo.statetype))
                            {
                                str = string.Format("statetype={0:G}", (SysInfo_t.StateType)sysInfo.statetype);
                            }
                            varListView.Items[2].SubItems[3].Text = str;
                        }
                        if (sysInfo.movetype != _sysInfo.movetype || _isDirty)
                        {
                            string str = string.Format("movetype={0}", sysInfo.movetype);
                            if (Enum.IsDefined(typeof(SysInfo_t.MoveType), sysInfo.movetype))
                            {
                                str = string.Format("movetype={0:G}", (SysInfo_t.MoveType)sysInfo.movetype);
                            }
                            varListView.Items[2].SubItems[4].Text = str;
                        }
                        for (int index = 0; index < 5; ++index)
                        {
                            if (mugenSysvar[index] != _mugenSysvar[index] || _isDirty)
                            {
                                string str;
                                if (binaryToolStripMenuItem.Checked)
                                    str = "sysvar(" + index + ")=b" + Convert2Binary(mugenSysvar[index]);
                                else
                                    str = "sysvar(" + index + ")=" + mugenSysvar[index];
                                varListView.Items[3].SubItems[index].Text = str;
                            }
                        }
                        for (int index = 0; index < 5; ++index)
                        {
                            if (mugenSysfvar[index] != (double)_mugenSysfvar[index] || _isDirty)
                            {
                                string str;
                                if (binaryToolStripMenuItem.Checked)
                                {
                                    if (mugenSysfvar[index] == (double)(long)mugenSysfvar[index] && mugenSysfvar[index] >= 0.0)
                                        str = "sysfvar(" + index + ")=b" + Convert2Binary((int)mugenSysfvar[index]);
                                    else
                                        str = "sysfvar(" + index + ")=" + mugenSysfvar[index];
                                }
                                else
                                    str = "sysfvar(" + index + ")=" + mugenSysfvar[index];
                                varListView.Items[4].SubItems[index].Text = str;
                            }
                        }
                        break;
                    case 1:
                    case 2:
                    case 3:
                        int num1 = (_selectedGroup - 1) * 20;
                        for (int index1 = 0; index1 < 4; ++index1)
                        {
                            for (int index2 = 0; index2 < 5; ++index2)
                            {
                                int index3 = index2 + index1 * 5 + num1;
                                if (mugenVar[index3] != _mugenVar[index3] || _isDirty)
                                {
                                    string str;
                                    if (binaryToolStripMenuItem.Checked)
                                        str = "var(" + index3 + ")=b" + Convert2Binary(mugenVar[index3]);
                                    else
                                        str = "var(" + index3 + ")=" + mugenVar[index3];
                                    varListView.Items[index1].SubItems[index2].Text = str;
                                }
                            }
                        }
                        switch (_selectedGroup)
                        {
                            case 1:
                                if (sysInfo.fall_damage != _sysInfo.fall_damage || _isDirty)
                                    varListView.Items[4].SubItems[0].Text = "fall.damage=" + sysInfo.fall_damage;
                                if (sysInfo.movecontact != _sysInfo.movecontact || _isDirty)
                                    varListView.Items[4].SubItems[1].Text = "movecontact=" + sysInfo.movecontact;
                                if (sysInfo.movehit != _sysInfo.movehit || _isDirty)
                                    varListView.Items[4].SubItems[2].Text = "movehit=" + sysInfo.movehit;
                                if (sysInfo.moveguarded != _sysInfo.moveguarded || _isDirty)
                                    varListView.Items[4].SubItems[3].Text = "moveguarded=" + sysInfo.moveguarded;
                                if (sysInfo.movereversed != _sysInfo.movereversed || _isDirty)
                                {
                                    varListView.Items[4].SubItems[4].Text = "movereversed=" + sysInfo.movereversed;
                                    break;
                                }
                                break;
                            case 2:
                                if (sysInfo.posx != (double)_sysInfo.posx || _isDirty)
                                    varListView.Items[4].SubItems[0].Text = "pos x=" + sysInfo.posx;
                                if (sysInfo.posy != (double)_sysInfo.posy || _isDirty)
                                    varListView.Items[4].SubItems[1].Text = "pos y=" + sysInfo.posy;
                                if (sysInfo.velx != (double)_sysInfo.velx || _isDirty)
                                    varListView.Items[4].SubItems[2].Text = "vel x=" + sysInfo.velx;
                                if (sysInfo.vely != (double)_sysInfo.vely || _isDirty)
                                    varListView.Items[4].SubItems[3].Text = "vel y=" + sysInfo.vely;
                                if (sysInfo.facing != _sysInfo.facing || _isDirty)
                                {
                                    varListView.Items[4].SubItems[4].Text = "facing=" + (sysInfo.facing == 1 ? "Right (1)" : "Left (-1)");
                                    break;
                                }
                                break;
                            case 3:
                                if (sysInfo.attackmulset != (double)_sysInfo.attackmulset || _isDirty)
                                    varListView.Items[4].SubItems[0].Text = "attack-mul=" + sysInfo.attackmulset;
                                if (sysInfo.localx != _sysInfo.localx || _isDirty)
                                    varListView.Items[4].SubItems[1].Text = "localcoord-x=" + sysInfo.localx;
                                if (sysInfo.localy != _sysInfo.localy || _isDirty)
                                    varListView.Items[4].SubItems[2].Text = "localcoord-y=" + sysInfo.localy;
                                if (sysInfo.hasclsn1 != _sysInfo.hasclsn1 || _isDirty)
                                    varListView.Items[4].SubItems[3].Text = "has-clsn1=" + sysInfo.hasclsn1.ToString();
                                varListView.Items[4].SubItems[4].Text = "";
                                break;
                        }
                        break;
                    case 4:
                    case 5:
                        int num2 = (_selectedGroup - 4) * 20;
                        for (int index1 = 0; index1 < 4; ++index1)
                        {
                            for (int index2 = 0; index2 < 5; ++index2)
                            {
                                int index3 = index2 + index1 * 5 + num2;
                                if (mugenFvar[index3] != (double)_mugenFvar[index3] || _isDirty)
                                {
                                    string str;
                                    if (binaryToolStripMenuItem.Checked)
                                    {
                                        if (mugenFvar[index3] == (double)(long)mugenFvar[index3] && mugenFvar[index3] >= 0.0)
                                            str = "fvar(" + index3 + ")=b" + Convert2Binary((int)mugenFvar[index3]);
                                        else
                                            str = "fvar(" + index3 + ")=" + mugenFvar[index3];
                                    }
                                    else
                                        str = "fvar(" + index3 + ")=" + mugenFvar[index3];
                                    varListView.Items[index1].SubItems[index2].Text = str;
                                }
                            }
                        }
                        switch (_selectedGroup)
                        {
                            case 4:
                                if (sysInfo.target1 != _sysInfo.target1 || sysInfo.target2 != _sysInfo.target2 || sysInfo.target3 != _sysInfo.target3 || sysInfo.target4 != _sysInfo.target4 || sysInfo.target5 != _sysInfo.target5 || sysInfo.target6 != _sysInfo.target6 || sysInfo.target7 != _sysInfo.target7 || sysInfo.target8 != _sysInfo.target8 || _isDirty)
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
                                    varListView.Items[4].SubItems[0].Text = str;
                                }
                                if (sysInfo.hitby != _sysInfo.hitby || sysInfo.noclsn2 != _sysInfo.noclsn2 || sysInfo.muteki != _sysInfo.muteki || _isDirty)
                                    varListView.Items[4].SubItems[1].Text = sysInfo.noclsn2 == 0 ? sysInfo.muteki == 0 ? (sysInfo.hitby & 7) != 0 ? (sysInfo.hitby & 4088) != 0 ? "hitby=" + _GetAttrString(sysInfo.hitby) : "hitby=muteki" : "hitby=muteki" : "hitby=unhittable" : "hitby=no-clsn2";
                                if (sysInfo.hitoverride0 != _sysInfo.hitoverride0 || _isDirty)
                                    varListView.Items[4].SubItems[2].Text = "hitoverride0=" + _GetAttrString(sysInfo.hitoverride0);
                                if (sysInfo.hitoverride1 != _sysInfo.hitoverride1 || _isDirty)
                                    varListView.Items[4].SubItems[3].Text = "hitoverride1=" + _GetAttrString(sysInfo.hitoverride1);
                                if (sysInfo.hitoverride2 != _sysInfo.hitoverride2 || _isDirty)
                                {
                                    varListView.Items[4].SubItems[4].Text = "hitoverride2=" + _GetAttrString(sysInfo.hitoverride2);
                                    break;
                                }
                                break;
                            case 5:
                                if (sysInfo.pausetime != _sysInfo.pausetime || _isDirty)
                                    varListView.Items[4].SubItems[0].Text = "(pause-time=" + sysInfo.pausetime + ")";
                                if (sysInfo.superpausetime != _sysInfo.superpausetime || _isDirty)
                                    varListView.Items[4].SubItems[1].Text = "(s-pause-time=" + sysInfo.superpausetime + ")";
                                if (sysInfo.pausemovetime != _sysInfo.pausemovetime || _isDirty)
                                    varListView.Items[4].SubItems[2].Text = "p-move-time=" + sysInfo.pausemovetime;
                                if (sysInfo.superpausemovetime != _sysInfo.superpausemovetime || _isDirty)
                                    varListView.Items[4].SubItems[3].Text = "s-move-time=" + sysInfo.superpausemovetime;
                                if (sysInfo.active != _sysInfo.active || _isDirty)
                                {
                                    varListView.Items[4].SubItems[4].Text = "is-frozen=" + (sysInfo.active != 0 ? 0 : 1);
                                    break;
                                }
                                break;
                        }
                        break;
                    case 6:
                        if (_isDirty)
                        {
                            for (int index1 = 0; index1 < 5; ++index1)
                            {
                                for (int index2 = 0; index2 < 5; ++index2)
                                    varListView.Items[index1].SubItems[index2].Text = "";
                            }
                        }
                        SysInfo_t.GlobalAssertFlags globalAssertFlags;
                        for (int index = 0; index < 5; ++index)
                        {
                            if (sysInfo.assertFlags[index] != _sysInfo.assertFlags[index] || _isDirty)
                            {
                                ListViewItem.ListViewSubItem subItem = varListView.Items[0].SubItems[index];
                                globalAssertFlags = (SysInfo_t.GlobalAssertFlags)index;
                                string str = globalAssertFlags.ToString() + "=" + sysInfo.assertFlags[index].ToString();
                                subItem.Text = str;
                            }
                        }
                        for (int index = 0; index < 5; ++index)
                        {
                            if (sysInfo.assertFlags[5 + index] != _sysInfo.assertFlags[5 + index] || _isDirty)
                            {
                                ListViewItem.ListViewSubItem subItem = varListView.Items[1].SubItems[index];
                                globalAssertFlags = (SysInfo_t.GlobalAssertFlags)(5 + index);
                                string str = globalAssertFlags.ToString() + "=" + sysInfo.assertFlags[5 + index].ToString();
                                subItem.Text = str;
                            }
                        }
                        if (sysInfo.assertFlags[10] != _sysInfo.assertFlags[10] || _isDirty)
                        {
                            ListViewItem.ListViewSubItem subItem = varListView.Items[2].SubItems[0];
                            globalAssertFlags = SysInfo_t.GlobalAssertFlags.NoFG;
                            string str = globalAssertFlags.ToString() + "=" + sysInfo.assertFlags[10].ToString();
                            subItem.Text = str;
                        }
                        if (sysInfo.selfAssertFlags.Length != 0)
                        {
                            SysInfo_t.SelfAssertFlags selfAssertFlags;
                            for (int index = 0; index < 5; ++index)
                            {
                                if (sysInfo.selfAssertFlags[index] != _sysInfo.selfAssertFlags[index] || _isDirty)
                                {
                                    ListViewItem.ListViewSubItem subItem = varListView.Items[3].SubItems[index];
                                    selfAssertFlags = (SysInfo_t.SelfAssertFlags)index;
                                    string str = selfAssertFlags.ToString() + "=" + sysInfo.selfAssertFlags[index].ToString();
                                    subItem.Text = str;
                                }
                            }
                            for (int index = 0; index < 4; ++index)
                            {
                                if (sysInfo.selfAssertFlags[5 + index] != _sysInfo.selfAssertFlags[5 + index] || _isDirty)
                                {
                                    ListViewItem.ListViewSubItem subItem = varListView.Items[4].SubItems[index];
                                    selfAssertFlags = (SysInfo_t.SelfAssertFlags)(5 + index);
                                    string str = selfAssertFlags.ToString() + "=" + sysInfo.selfAssertFlags[5 + index].ToString();
                                    subItem.Text = str;
                                }
                            }
                            break;
                        }
                        break;
                }
                _isDirty = false;
                sysInfo.CopyTo(_sysInfo);
                mugenSysvar.CopyTo(_mugenSysvar, 0);
                mugenVar.CopyTo(_mugenVar, 0);
                mugenSysfvar.CopyTo(_mugenSysfvar, 0);
                mugenFvar.CopyTo(_mugenFvar, 0);
            }
        }

        

        /// <summary>
        /// util for converting an int HitAttr enum into the actual HitAttr string
        /// </summary>
        /// <param name="attr"></param>
        /// <returns></returns>
        private string _GetAttrString(int attr)
        {
            return string.Format("{0:G}", (SysInfo_t.HitAttr)attr).Replace(" ", "");
        }

        private void VarForm_Load(object sender, EventArgs e)
        {
            varListView.Items.Clear();
            for (int index = 0; index < 5; ++index)
                varListView.Items.Add(new ListViewItem(new string[5]
                {
          " ",
          " ",
          " ",
          " ",
          " "
                }));
            Activate();
        }

        private void VarForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason != CloseReason.UserClosing)
                return;
            e.Cancel = true;
            Hide();
        }

        private void radioButton1_MouseClick(object sender, MouseEventArgs e)
        {
            Control radioButton1 = this.radioButton1;
            if (radioButton1 == null || !(radioButton1.Cursor == Cursors.Help))
                return;
            string text = "Displays the values of system parameters such as alive or life and the values of system variables." + Environment.NewLine + "Information that does not belong to any particular player is displayed within brackets." + Environment.NewLine + Environment.NewLine + "Data information:" + Environment.NewLine + "　・round-timer -- Displays how many frames have to be drawn before a time over occurs." + Environment.NewLine + "　・damage -- Displays the value of the gethitvar(damage) parameter." + Environment.NewLine + "Note: Since the value of gethitvar(damage) is technically only reported for one frame, the display function will keep showing the same value for 30 frames to make it easier to see." + Environment.NewLine + "　・stateno -- Displays the current state number the player is in." + Environment.NewLine + "If the player is executing code from the minus states, those will be displayed as follows:" + Environment.NewLine + "       　　　stateno={The minus state whose code is being run} stateno. Where state no is the actual state the player is in." + Environment.NewLine + "For example, if the player is in state 200 while running code from statedef -2 then this will be reported as:" + Environment.NewLine + "       　　　stateno={-2} 200." + Environment.NewLine;
            toolTip1.Hide(radioButton1);
            toolTip1.IsBalloon = false;
            toolTip1.IsBalloon = true;
            toolTip1.SetToolTip(radioButton1, " ");
            toolTip1.Show(text, radioButton1, radioButton1.Width / 2, radioButton1.Height / 2);
        }

        private void radioButton2_MouseClick(object sender, MouseEventArgs e)
        {
            Control radioButton2 = this.radioButton2;
            if (radioButton2 == null || !(radioButton2.Cursor == Cursors.Help))
                return;
            string text = "Displays the values of int variables 0 to 19." + Environment.NewLine + "(fall.damage, movecontact, movehit, moveguarded, movereversed)" + Environment.NewLine + Environment.NewLine + "Data information:" + Environment.NewLine + "　・fall.damage -- Displays the value of the gethitvar(fall.damage) parameter." + Environment.NewLine;
            toolTip1.Hide(radioButton2);
            toolTip1.IsBalloon = false;
            toolTip1.IsBalloon = true;
            toolTip1.SetToolTip(radioButton2, " ");
            toolTip1.Show(text, radioButton2, radioButton2.Width / 2, radioButton2.Height / 2);
        }

        private void radioButton3_MouseClick(object sender, MouseEventArgs e)
        {
            Control radioButton3 = this.radioButton3;
            if (radioButton3 == null || !(radioButton3.Cursor == Cursors.Help))
                return;
            string text = "Displays the values of int variables 20 to 39." + Environment.NewLine + "(pos x, pos y, vel x, vel y, facing)" + Environment.NewLine;
            toolTip1.Hide(radioButton3);
            toolTip1.IsBalloon = false;
            toolTip1.IsBalloon = true;
            toolTip1.SetToolTip(radioButton3, " ");
            toolTip1.Show(text, radioButton3, radioButton3.Width / 2, radioButton3.Height / 2);
        }

        private void radioButton4_MouseClick(object sender, MouseEventArgs e)
        {
            Control radioButton4 = this.radioButton4;
            if (radioButton4 == null || !(radioButton4.Cursor == Cursors.Help))
                return;
            string text = "Displays the values of int variables 40 to 59." + Environment.NewLine + "(attack-mul, noko, intro, roundnotover, timerfreeze)" + Environment.NewLine + "Information that does not belong to any particular player is displayed within brackets." + Environment.NewLine + Environment.NewLine + "[特殊な項目の説明]" + Environment.NewLine + "　・attack-mul -- Shows the value of the attackmulset parameter." + Environment.NewLine;
            toolTip1.Hide(radioButton4);
            toolTip1.IsBalloon = false;
            toolTip1.IsBalloon = true;
            toolTip1.SetToolTip(radioButton4, " ");
            toolTip1.Show(text, radioButton4, radioButton4.Width / 2, radioButton4.Height / 2);
        }

        private void radioButton5_MouseClick(object sender, MouseEventArgs e)
        {
            Control radioButton5 = this.radioButton5;
            if (radioButton5 == null || !(radioButton5.Cursor == Cursors.Help))
                return;
            string text = "Displays the values of float variables 0 to 19." + Environment.NewLine + "(target, hitby, hitoverride0, hitoverride1, hitoverride2)" + Environment.NewLine + Environment.NewLine + "Data information:" + Environment.NewLine + "　・hitby -- Shows the hitflags used in an active hitby or nothitby controller." + Environment.NewLine + "              Example: hitby=sca,aa,at,ap" + Environment.NewLine + Environment.NewLine + "       If the flags make the character invinsible, then 'hitby = invincible' will be shown." + Environment.NewLine + "       In the case of a superpause controller making the character invincible, 'hitby = unhittable' will be displayed instead." + Environment.NewLine + "       Finally, if the specified animation contains no vulnerability hitboxes, then 'hitby = no-clsn2' will be shown." + Environment.NewLine + Environment.NewLine + "　・hitoverride0,1,2 --Shows the parameters that are set in each slot of a hitoverride controller." + Environment.NewLine + "              Example: hitoverride0=sca,aa,at" + Environment.NewLine;
            toolTip1.Hide(radioButton5);
            toolTip1.IsBalloon = false;
            toolTip1.IsBalloon = true;
            toolTip1.SetToolTip(radioButton5, " ");
            toolTip1.Show(text, radioButton5, radioButton5.Width / 2, radioButton5.Height / 2);
        }

        private void radioButton6_MouseClick(object sender, MouseEventArgs e)
        {
            Control radioButton6 = this.radioButton6;
            if (radioButton6 == null || !(radioButton6.Cursor == Cursors.Help))
                return;
            string text = "Displays the values of float variables 20 to 39." + Environment.NewLine + "(pause-time, s-pause-time, p-move-time, s-move-time, is-frozen)" + Environment.NewLine + "Information that does not belong to any particular player is displayed within brackets." + Environment.NewLine + Environment.NewLine + "Data information:" + Environment.NewLine + "　・is-frozen -- Displays if the object is frozen by the effect of a pause controller." + Environment.NewLine + "       Normally, this will be 0. This will display 1 if the object is frozen by a pause or superpause." + Environment.NewLine + "　・pause-time -- Shows the time parameter specified in a pause controller." + Environment.NewLine + "　・s-pause-time -- Shows the time specified in a super pause controller." + Environment.NewLine + "　・p-move-time -- Shows the value of movetime specified in a pause controller." + Environment.NewLine + "　・s-move-time -- Shows the value of movetime specified in a superpause controller." + Environment.NewLine;
            toolTip1.Hide(radioButton6);
            toolTip1.IsBalloon = false;
            toolTip1.IsBalloon = true;
            toolTip1.SetToolTip(radioButton6, " ");
            toolTip1.Show(text, radioButton6, radioButton6.Width / 2, radioButton6.Height / 2);
        }

        private void radioButton7_MouseClick(object sender, MouseEventArgs e)
        {
            Control radioButton7 = this.radioButton7;
            if (radioButton7 == null || !(radioButton7.Cursor == Cursors.Help))
                return;
            string text = "Displays the values of globally-active AssertSpecial flags." + Environment.NewLine;
            toolTip1.Hide(radioButton7);
            toolTip1.IsBalloon = false;
            toolTip1.IsBalloon = true;
            toolTip1.SetToolTip(radioButton7, " ");
            toolTip1.Show(text, radioButton7, radioButton7.Width / 2, radioButton7.Height / 2);
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            Control radioButton1 = this.radioButton1;
            if (radioButton1 != null && radioButton1.Cursor == Cursors.Help)
            {
                if (!this.radioButton1.Checked)
                    this.radioButton1.Checked = true;
                if (!this.radioButton1.Checked)
                    return;
                _isDirty = true;
                _selectedGroup = 0;
            }
            else
            {
                if (this.radioButton1.Checked)
                {
                    _isDirty = true;
                    _selectedGroup = 0;
                }
                if (DebugForm.MainObj().IsPaused())
                    return;
                MugenWindow.MainObj().Activate();
            }
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            Control radioButton2 = this.radioButton2;
            if (radioButton2 != null && radioButton2.Cursor == Cursors.Help)
                return;
            if (this.radioButton2.Checked)
            {
                _isDirty = true;
                _selectedGroup = 1;
            }
            if (DebugForm.MainObj().IsPaused())
                return;
            MugenWindow.MainObj().Activate();
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            Control radioButton3 = this.radioButton3;
            if (radioButton3 != null && radioButton3.Cursor == Cursors.Help)
                return;
            if (this.radioButton3.Checked)
            {
                _isDirty = true;
                _selectedGroup = 2;
            }
            if (DebugForm.MainObj().IsPaused())
                return;
            MugenWindow.MainObj().Activate();
        }

        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {
            Control radioButton4 = this.radioButton4;
            if (radioButton4 != null && radioButton4.Cursor == Cursors.Help)
                return;
            if (this.radioButton4.Checked)
            {
                _isDirty = true;
                _selectedGroup = 3;
            }
            if (DebugForm.MainObj().IsPaused())
                return;
            MugenWindow.MainObj().Activate();
        }

        private void radioButton5_CheckedChanged(object sender, EventArgs e)
        {
            Control radioButton5 = this.radioButton5;
            if (radioButton5 != null && radioButton5.Cursor == Cursors.Help)
                return;
            if (this.radioButton5.Checked)
            {
                _isDirty = true;
                _selectedGroup = 4;
            }
            if (DebugForm.MainObj().IsPaused())
                return;
            MugenWindow.MainObj().Activate();
        }

        private void radioButton6_CheckedChanged(object sender, EventArgs e)
        {
            Control radioButton6 = this.radioButton6;
            if (radioButton6 != null && radioButton6.Cursor == Cursors.Help)
                return;
            if (this.radioButton6.Checked)
            {
                _isDirty = true;
                _selectedGroup = 5;
            }
            if (DebugForm.MainObj().IsPaused())
                return;
            MugenWindow.MainObj().Activate();
        }

        private void radioButton7_CheckedChanged(object sender, EventArgs e)
        {
            Control radioButton7 = this.radioButton7;
            if (radioButton7 != null && radioButton7.Cursor == Cursors.Help)
                return;
            if (this.radioButton7.Checked)
            {
                _isDirty = true;
                _selectedGroup = 6;
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
            if (Cursor == Cursors.Help || e.KeyValue != 19)
                return;
            DebugForm.MainObj().TogglePauseCheckBox();
            e.Handled = true;
        }

        private void VarForm_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Cursor == Cursors.Help)
                return;
            if (e.KeyChar == '\x0004')
            {
                DebugForm.MainObj().ShiftDebugTargetNo();
                e.Handled = true;
            }
            else if (e.KeyChar == '\x0003')
            {
                _copyAll();
                e.Handled = true;
            }
            else if (e.KeyChar == '\x0002')
            {
                if (MugenWindow.MainObj().GetWatcher().GetMugenProcess() != null)
                {
                    _isDirty = true;
                    binaryToolStripMenuItem.Checked = !binaryToolStripMenuItem.Checked;
                    if (binaryToolStripMenuItem.Checked)
                    {
                        toolTip1.Hide(varListView);
                        toolTip1.Show("Values are now being displayed in binary.　　　　　　　", varListView, 0, -20, 1000);
                    }
                    else
                    {
                        toolTip1.Hide(varListView);
                        toolTip1.Show("Values are now being displayed in decimal.　　　　　　　", varListView, 0, -20, 1000);
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
                if (MugenWindow.MainObj().GetWatcher().GetMugenProcess() != null)
                    MugenWindow.MainObj().InjectESC();
                e.Handled = true;
            }
        }

        /// <summary>
        /// utility to copy all variable data to clipboard
        /// </summary>
        private void _copyAll()
        {
            if (varListView.Items.Count == 0 || varListView.ForeColor != DefaultForeColor)
                return;
            string text1 = "Variable name\tValue\tVariable name\tValue\tVariable name\tValue\tVariable name\tValue\tVariable name\tValue" + Environment.NewLine;
            for (int index1 = 0; index1 < varListView.Items.Count; ++index1)
            {
                for (int index2 = 0; index2 < 5; ++index2)
                {
                    string text2 = varListView.Items[index1].SubItems[index2].Text;
                    int length = text2.IndexOf('=');
                    if (text2 != null && length > 0)
                        text1 = text1 + text2.Substring(0, length) + "\t" + text2.Substring(length + 1);
                    if (index2 < 4)
                        text1 += "\t";
                }
                text1 += Environment.NewLine;
            }
            Clipboard.SetText(text1);
            toolTip1.Hide(varListView);
            toolTip1.Show("Copied the current variable list to the clipboard.", varListView, 0, -20, 1000);
        }

        private void copyAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Control contextMenuStrip = varContextMenuStrip;
            if (contextMenuStrip != null && contextMenuStrip.Cursor == Cursors.Help)
            {
                varContextMenuStrip.Show();
                string text = "Copies the current variable list in table form to the clipboard." + Environment.NewLine + "(The data is pre-formatted for use in programs such as Excel.)" + Environment.NewLine;
                toolTip1.Hide(contextMenuStrip);
                toolTip1.IsBalloon = false;
                toolTip1.IsBalloon = true;
                toolTip1.SetToolTip(contextMenuStrip, " ");
                toolTip1.Show(text, contextMenuStrip, binaryToolStripMenuItem.Width / 2, binaryToolStripMenuItem.Height * 3 / 2);
            }
            else
                _copyAll();
        }

        private void binaryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Control contextMenuStrip = varContextMenuStrip;
            if (contextMenuStrip != null && contextMenuStrip.Cursor == Cursors.Help)
            {
                varContextMenuStrip.Show();
                string text = "Displays the values in binary notation. The 'b' prefix is used to denote a number in binary notation." + Environment.NewLine + "　　　　Example: b101101 which is 45 in decimal." + Environment.NewLine + "(Note: The value of system variables such as alive or life will not be affected.)" + Environment.NewLine + "(Note: For float variables sysfvar and fvar, the value will only be shown in binary notation if it is an integer.)" + Environment.NewLine;
                toolTip1.Hide(contextMenuStrip);
                toolTip1.IsBalloon = false;
                toolTip1.IsBalloon = true;
                toolTip1.SetToolTip(contextMenuStrip, " ");
                toolTip1.Show(text, contextMenuStrip, binaryToolStripMenuItem.Width / 2, binaryToolStripMenuItem.Height / 2);
            }
            else
            {
                if (MugenWindow.MainObj().GetWatcher().GetMugenProcess() == null)
                    return;
                if (binaryToolStripMenuItem.Checked)
                {
                    toolTip1.Hide(varListView);
                    toolTip1.Show("Values are now being displayed in binary.　　　　　　　", varListView, 0, -20, 1000);
                }
                else
                {
                    toolTip1.Hide(varListView);
                    toolTip1.Show("values are now being displayed in decimal.　　　　　　　", varListView, 0, -20, 1000);
                }
                _isDirty = true;
            }
        }

        private void varListView_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            if (varListView.Cursor == Cursors.Help)
            {
                toolTip1.Hide(this);
                toolTip1.IsBalloon = false;
                toolTip1.IsBalloon = true;
                string text = "If the cell sizes are too small for the data to be displayed, you can expand it by double clicking the parameter cell at the top." + Environment.NewLine + "(You can also restore the sizes to default by clicking once more.)" + Environment.NewLine;
                toolTip1.SetToolTip(varListView, " ");
                int num = 0;
                for (int index = 0; index < e.Column; ++index)
                    num += varListView.Columns[index].Width;
                toolTip1.Show(text, varListView, num + varListView.Columns[e.Column].Width / 2, 10);
            }
            else
            {
                if (!_columState[e.Column])
                {
                    _columState[e.Column] = true;
                    if (binaryToolStripMenuItem.Checked)
                        varListView.Columns[e.Column].Width = 280;
                    else
                        varListView.Columns[e.Column].Width = 200;
                }
                else
                {
                    _columState[e.Column] = false;
                    varListView.Columns[e.Column].Width = 115;
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
                Cursor = Cursors.Help;
                varListView.Cursor = Cursors.Help;
                radioButton1.Cursor = Cursors.Help;
                radioButton2.Cursor = Cursors.Help;
                radioButton3.Cursor = Cursors.Help;
                radioButton4.Cursor = Cursors.Help;
                radioButton5.Cursor = Cursors.Help;
                radioButton6.Cursor = Cursors.Help;
                varContextMenuStrip.Cursor = Cursors.Help;
            }
            else
            {
                Cursor = Cursors.Default;
                varListView.Cursor = Cursors.Default;
                radioButton1.Cursor = Cursors.Default;
                radioButton2.Cursor = Cursors.Default;
                radioButton3.Cursor = Cursors.Default;
                radioButton4.Cursor = Cursors.Default;
                radioButton5.Cursor = Cursors.Default;
                radioButton6.Cursor = Cursors.Default;
                varContextMenuStrip.Cursor = Cursors.Default;
            }
        }

        private void VarForm_Deactivate(object sender, EventArgs e)
        {
            if (!(Cursor == Cursors.Help))
                return;
            toolTip1.Hide(this);
        }

        private void varListView_DisplayHelp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                if (!(varListView.Cursor == Cursors.Help))
                    return;
                toolTip1.Hide(this);
            }
            else
            {
                if (e.Button != MouseButtons.Left || !(varListView.Cursor == Cursors.Help))
                    return;
                toolTip1.Hide(this);
                toolTip1.IsBalloon = false;
                toolTip1.IsBalloon = true;
                string text = "Displays the current value of player variables." + Environment.NewLine + "If the cell sizes are too small for the data to be displayed, you can expand it by double clicking the parameter cell at the top." + Environment.NewLine + "(You can also restore the sizes to default by clicking once more.)" + Environment.NewLine + Environment.NewLine + "Finally, more tools are available if you right click on one of the items.";
                toolTip1.SetToolTip(varListView, " ");
                toolTip1.Show(text, varListView, varListView.Width / 2, varListView.Height / 2);
            }
        }

        private void varListView_MouseClick(object sender, MouseEventArgs e) => varListView_DisplayHelp(sender, e);

        private void varListView_MouseDown(object sender, MouseEventArgs e) => varListView_DisplayHelp(sender, e);

        private void VarForm_Activated(object sender, EventArgs e)
        {
        }

        private void varListView_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        /// <summary>
        /// structure to store data about a particular player
        /// </summary>
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

            public void CopyTo(SysInfo_t obj)
            {
                obj.assertFlags = assertFlags;
                obj.selfAssertFlags = selfAssertFlags;
                obj.localx = localx;
                obj.localy = localy;
                obj.alive = alive;
                obj.win = win;
                obj.lose = lose;
                obj.life = life;
                obj.palno = palno;
                obj.hitpausetime = hitpausetime;
                obj.stateowner = stateowner;
                obj.stateno = stateno;
                obj.specialstateno = specialstateno;
                obj.prevstateno = prevstateno;
                obj.roundtime = roundtime;
                obj.roundstate = roundstate;
                obj.power = power;
                obj.ctrl = ctrl;
                obj.damage = damage;
                obj.statetype = statetype;
                obj.movetype = movetype;
                obj.posx = posx;
                obj.posy = posy;
                obj.velx = velx;
                obj.vely = vely;
                obj.facing = facing;
                obj.fall_damage = fall_damage;
                obj.movecontact = movecontact;
                obj.movehit = movehit;
                obj.moveguarded = moveguarded;
                obj.movereversed = movereversed;
                obj.active = active;
                obj.noko = noko;
                obj.intro = intro;
                obj.roundnotover = roundnotover;
                obj.timerfreeze = timerfreeze;
                obj.hitby = hitby;
                obj.muteki = muteki;
                obj.noclsn2 = noclsn2;
                obj.hasclsn1 = hasclsn1;
                obj.hitoverride0 = hitoverride0;
                obj.hitoverride1 = hitoverride1;
                obj.hitoverride2 = hitoverride2;
                obj.target1 = target1;
                obj.target2 = target2;
                obj.target3 = target3;
                obj.target4 = target4;
                obj.target5 = target5;
                obj.target6 = target6;
                obj.target7 = target7;
                obj.target8 = target8;
                obj.pausetime = pausetime;
                obj.superpausetime = superpausetime;
                obj.pausemovetime = pausemovetime;
                obj.superpausemovetime = superpausemovetime;
                obj.attackmulset = attackmulset;
            }

            /// <summary>
            /// globally active AssertSpecial flags
            /// </summary>
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

            /// <summary>
            /// player specific AssertSpecial flags
            /// </summary>
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

            [Flags]
            internal enum HitAttr
            {
                S = 1,
                C = 2,
                A = 4,
                SCA = 7,
                NA = 8,
                SA = 16,
                HA = 32,
                AA = NA | SA | HA,
                NT = 64,
                ST = 128,
                HT = 256,
                AT = NT | ST | HT,
                NP = 512,
                SP = 1024,
                HP = 2048,
                AP = NP | SP | HP
            }

            internal enum StateType
            {
                S = 1,
                C = 2,
                A = 3,
                L = 4
            }

            internal enum MoveType
            {
                I = 0,
                A = 1,
                H = 2
            }
        }
    }
}
