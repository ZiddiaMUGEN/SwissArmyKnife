// Decompiled with JetBrains decompiler
// Type: SwissArmyKnifeForMugen.DebugForm
// Assembly: SAKnifeWM, Version=1.0.8.0, Culture=neutral, PublicKeyToken=null
// MVID: 09478AD8-365C-4BF3-BEA1-B5785151259B
// Assembly location: C:\Users\ziddi\Downloads\Swiss Army Knife 1.1 Conversion\Swiss Army Knife 1.1 Conversion\SAKnifeWM.exe

using SwissArmyKnifeForMugen.Configs;
using SwissArmyKnifeForMugen.Triggers;
using SwissArmyKnifeForMugen.Utils;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using static SwissArmyKnifeForMugen.MugenWindow;
using static SwissArmyKnifeForMugen.Triggers.TriggerDatabase;

namespace SwissArmyKnifeForMugen.Displays
{
    public class DebugForm : Form
    {
        private bool _isDirty = true;
        private bool _isExplodListDirty = true;
        private bool _isProjListDirty = true;
        private bool[] _columState = new bool[4];
        private bool[] _explodColumState = new bool[4];
        private bool[] _projColumState = new bool[4];
        private IContainer components;
        private GroupBox groupBox1;
        private CheckBox debugModeCheckBox;
        private GroupBox groupBox2;
        private CheckBox pauseCheckBox;
        private Button stepButton;
        private CheckBox autoStepCheckBox;
        private CheckBox speedModeCheckBox;
        private ComboBox debugColorList;
        private TrackBar debugColorCustomRed;
        private Label debugColorCustomRedLabel;
        private TrackBar debugColorCustomBlue;
        private Label debugColorCustomBlueLabel;
        private TrackBar debugColorCustomGreen;
        private Label debugColorCustomGreenLabel;
        private Label label1;
        private CheckBox skipModeCheckBox;
        private ContextMenuStrip debugContextMenuStrip;
        private ToolStripMenuItem copyAllToolStripMenuItem;
        private ToolStripMenuItem ctrlDhandleToolStripMenuItem1;
        private ToolTip toolTip1;
        private Label label3;
        private Label label2;
        private ComboBox stepTimeComboBox;
        private Label label4;
        private ComboBox skipFrameComboBox;
        private BufferedListView playerListView;
        private ColumnHeader noHeader;
        private ColumnHeader playeridHeader;
        private ColumnHeader helperidHeader;
        private ColumnHeader parentidHeader;
        private ColumnHeader nameHeader;
        private TabControl listTabControl;
        private TabPage playerTabPage;
        private TabPage explodTabPage;
        private BufferedListView explodListView;
        private ColumnHeader columnHeader1;
        private ColumnHeader columnHeader2;
        private ColumnHeader columnHeader3;
        private NumericUpDown explodHeadNumericUpDown;
        private Label explodHeadLabel;
        private Label explodTailLabel;
        private ColumnHeader columnHeader4;
        private ContextMenuStrip explodContextMenuStrip;
        private ToolStripMenuItem explodToolStripMenuItem1;
        private TabPage projTabPage;
        private Label projTailLabel;
        private Label projHeadLabel;
        private NumericUpDown projHeadNumericUpDown;
        private BufferedListView projListView;
        private ColumnHeader columnHeader5;
        private ColumnHeader columnHeader6;
        private ColumnHeader columnHeader7;
        private ColumnHeader columnHeader8;
        private RadioButton p4RadioButton;
        private RadioButton p3RadioButton;
        private RadioButton p2RadioButton;
        private RadioButton p1RadioButton;
        private Label label5;
        private ContextMenuStrip projContextMenuStrip;
        private ToolStripMenuItem projToolStripMenuItem1;
        private ToolStripMenuItem projToolStripMenuItem2;
        private TabPage tabPage1;
        private GroupBox triggerGroupBox;
        private TextBox checkModeStateText;
        private Button triggerCheckStopButton;
        private Button triggerCheckResumeButton;
        private Button triggerCheckStartButton;
        private GroupBox groupBox4;
        private Label label8;
        private Label label7;
        private Label label6;
        private ComboBox valueComboBox;
        private ComboBox triggerComboBox;
        private ComboBox playerComboBox;
        private Button setButton;
        private Button revertButton;
        private Timer setButtonTimer;
        private Timer blinkTimer;
        private static DebugForm selfObj;
        private int _selectedIndex;
        private int[] _playerId;
        private int[] _parentId;
        private int[] _stateOwnerId;
        private int[] _explodId;
        private int[] _anim;
        private int[] _ownerId;
        private int[] _projId;
        private int[] _projX;
        private int[] _projY;
        private Color _defaultBgColor;
        private Color _defaultFgColor;
        private bool _ignoreUnPauseRequestOnce;
        private bool _isRightMouseButton_OnPlayerList;
        private bool _isRightMouseButton_OnExplodList;
        private bool _isRightMouseButton_OnProjList;
        private string _triggerPlayerString;
        private string _triggerTriggerString;
        private string _triggerValueString;
        private int _blinkCount;

        protected override void Dispose(bool disposing)
        {
            if (disposing && this.components != null)
                this.components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.components = (IContainer)new Container();
            ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(DebugForm));
            this.groupBox1 = new GroupBox();
            this.label1 = new Label();

            this.debugColorList = new ComboBox();
            this.debugColorCustomRed = new TrackBar();
            this.debugColorCustomBlue = new TrackBar();
            this.debugColorCustomGreen = new TrackBar();
            this.debugColorCustomRedLabel = new Label();
            this.debugColorCustomBlueLabel = new Label();
            this.debugColorCustomGreenLabel = new Label();

            this.debugModeCheckBox = new CheckBox();
            this.debugContextMenuStrip = new ContextMenuStrip(this.components);
            this.ctrlDhandleToolStripMenuItem1 = new ToolStripMenuItem();
            this.copyAllToolStripMenuItem = new ToolStripMenuItem();
            this.groupBox2 = new GroupBox();
            this.label4 = new Label();
            this.skipFrameComboBox = new ComboBox();
            this.stepTimeComboBox = new ComboBox();
            this.label3 = new Label();
            this.label2 = new Label();
            this.skipModeCheckBox = new CheckBox();
            this.speedModeCheckBox = new CheckBox();
            this.stepButton = new Button();
            this.autoStepCheckBox = new CheckBox();
            this.pauseCheckBox = new CheckBox();
            this.toolTip1 = new ToolTip(this.components);
            this.listTabControl = new TabControl();
            this.playerTabPage = new TabPage();
            this.playerListView = new BufferedListView();
            this.noHeader = new ColumnHeader();
            this.playeridHeader = new ColumnHeader();
            this.helperidHeader = new ColumnHeader();
            this.parentidHeader = new ColumnHeader();
            this.nameHeader = new ColumnHeader();
            this.explodTabPage = new TabPage();
            this.explodTailLabel = new Label();
            this.explodHeadLabel = new Label();
            this.explodHeadNumericUpDown = new NumericUpDown();
            this.explodListView = new BufferedListView();
            this.columnHeader1 = new ColumnHeader();
            this.columnHeader2 = new ColumnHeader();
            this.columnHeader3 = new ColumnHeader();
            this.columnHeader4 = new ColumnHeader();
            this.explodContextMenuStrip = new ContextMenuStrip(this.components);
            this.explodToolStripMenuItem1 = new ToolStripMenuItem();
            this.projTabPage = new TabPage();
            this.label5 = new Label();
            this.p4RadioButton = new RadioButton();
            this.p3RadioButton = new RadioButton();
            this.p2RadioButton = new RadioButton();
            this.p1RadioButton = new RadioButton();
            this.projTailLabel = new Label();
            this.projHeadLabel = new Label();
            this.projHeadNumericUpDown = new NumericUpDown();
            this.projListView = new BufferedListView();
            this.columnHeader5 = new ColumnHeader();
            this.columnHeader6 = new ColumnHeader();
            this.columnHeader7 = new ColumnHeader();
            this.columnHeader8 = new ColumnHeader();
            this.projContextMenuStrip = new ContextMenuStrip(this.components);
            this.projToolStripMenuItem1 = new ToolStripMenuItem();
            this.projToolStripMenuItem2 = new ToolStripMenuItem();
            this.tabPage1 = new TabPage();
            this.groupBox4 = new GroupBox();
            this.setButton = new Button();
            this.revertButton = new Button();
            this.valueComboBox = new ComboBox();
            this.triggerComboBox = new ComboBox();
            this.playerComboBox = new ComboBox();
            this.label8 = new Label();
            this.label7 = new Label();
            this.label6 = new Label();
            this.triggerGroupBox = new GroupBox();
            this.checkModeStateText = new TextBox();
            this.triggerCheckStopButton = new Button();
            this.triggerCheckResumeButton = new Button();
            this.triggerCheckStartButton = new Button();
            this.setButtonTimer = new Timer(this.components);
            this.blinkTimer = new Timer(this.components);
            this.groupBox1.SuspendLayout();
            this.debugContextMenuStrip.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.listTabControl.SuspendLayout();
            this.playerTabPage.SuspendLayout();
            this.explodTabPage.SuspendLayout();
            this.explodHeadNumericUpDown.BeginInit();
            this.explodContextMenuStrip.SuspendLayout();
            this.projTabPage.SuspendLayout();
            this.projHeadNumericUpDown.BeginInit();
            this.projContextMenuStrip.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.triggerGroupBox.SuspendLayout();
            this.SuspendLayout();
            this.groupBox1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            this.groupBox1.AutoSize = true;
            this.groupBox1.Controls.Add((Control)this.label1);
            this.groupBox1.Controls.Add((Control)this.debugColorList);
            this.groupBox1.Controls.Add((Control)this.debugColorCustomRed);
            this.groupBox1.Controls.Add((Control)this.debugColorCustomRedLabel);
            this.groupBox1.Controls.Add((Control)this.debugColorCustomBlue);
            this.groupBox1.Controls.Add((Control)this.debugColorCustomBlueLabel);
            this.groupBox1.Controls.Add((Control)this.debugColorCustomGreen);
            this.groupBox1.Controls.Add((Control)this.debugColorCustomGreenLabel);
            this.groupBox1.Controls.Add((Control)this.debugModeCheckBox);
            this.groupBox1.Location = new Point(12, 102);
            this.groupBox1.Margin = new Padding(3, 3, 3, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new Padding(3, 3, 3, 0);
            this.groupBox1.Size = new Size(367, 120);
            this.groupBox1.TabIndex = 100;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Debug Mode";
            this.debugColorList.AutoSize = true;
            this.debugColorList.Location = new Point(22, 62);
            this.debugColorList.Margin = new Padding(3, 3, 3, 0);
            this.debugColorList.Name = "debugColorList";
            this.debugColorList.Size = new Size(59, 17);
            this.debugColorList.TabIndex = 11;
            this.debugColorList.TabStop = true;
            this.debugColorList.SelectedValueChanged += new EventHandler(this.defaultRadioButton_CheckedChanged);

            this.debugColorCustomRed.Location = new Point(260, 10);
            this.debugColorCustomRed.Margin = new Padding(3, 3, 3, 0);
            this.debugColorCustomRed.Name = "debugColorCustomRed";
            this.debugColorCustomRed.TabIndex = 12;
            this.debugColorCustomRed.TabStop = true;
            this.debugColorCustomRed.Value = 0;
            this.debugColorCustomRed.TickFrequency = 1;
            this.debugColorCustomRed.Minimum = 0;
            this.debugColorCustomRed.Maximum = 256;
            this.debugColorCustomRed.Enabled = false;
            this.debugColorCustomRed.ValueChanged += new EventHandler(this.debugColorSliders_ValueChanged);

            this.debugColorCustomRedLabel.AutoSize = true;
            this.debugColorCustomRedLabel.Location = new Point(200, 15);
            this.debugColorCustomRedLabel.Name = "debugColorCustomRedLabel";
            this.debugColorCustomRedLabel.Size = new Size(30, 13);
            this.debugColorCustomRedLabel.TabIndex = 100;
            this.debugColorCustomRedLabel.Text = "Red (0):";

            this.debugColorCustomBlue.Location = new Point(260, 55);
            this.debugColorCustomBlue.Margin = new Padding(3, 3, 3, 0);
            this.debugColorCustomBlue.Name = "debugColorCustomBlue";
            this.debugColorCustomBlue.TabIndex = 13;
            this.debugColorCustomBlue.TabStop = true;
            this.debugColorCustomBlue.Value = 0;
            this.debugColorCustomBlue.TickFrequency = 1;
            this.debugColorCustomBlue.Minimum = 0;
            this.debugColorCustomBlue.Maximum = 256;
            this.debugColorCustomBlue.Enabled = false;
            this.debugColorCustomBlue.ValueChanged += new EventHandler(this.debugColorSliders_ValueChanged);

            this.debugColorCustomBlueLabel.AutoSize = true;
            this.debugColorCustomBlueLabel.Location = new Point(200, 60);
            this.debugColorCustomBlueLabel.Name = "debugColorCustomBlueLabel";
            this.debugColorCustomBlueLabel.Size = new Size(30, 13);
            this.debugColorCustomBlueLabel.TabIndex = 100;
            this.debugColorCustomBlueLabel.Text = "Blue (0):";

            this.debugColorCustomGreen.Location = new Point(260, 100);
            this.debugColorCustomGreen.Margin = new Padding(3, 3, 3, 0);
            this.debugColorCustomGreen.Name = "debugColorCustomGreen";
            this.debugColorCustomGreen.TabIndex = 14;
            this.debugColorCustomGreen.TabStop = true;
            this.debugColorCustomGreen.Value = 0;
            this.debugColorCustomGreen.TickFrequency = 1;
            this.debugColorCustomGreen.Minimum = 0;
            this.debugColorCustomGreen.Maximum = 256;
            this.debugColorCustomGreen.Enabled = false;
            this.debugColorCustomGreen.ValueChanged += new EventHandler(this.debugColorSliders_ValueChanged);

            this.debugColorCustomGreenLabel.AutoSize = true;
            this.debugColorCustomGreenLabel.Location = new Point(200, 105);
            this.debugColorCustomGreenLabel.Name = "debugColorCustomGreenLabel";
            this.debugColorCustomGreenLabel.Size = new Size(30, 13);
            this.debugColorCustomGreenLabel.TabIndex = 100;
            this.debugColorCustomGreenLabel.Text = "Green (0):";

            this.label1.AutoSize = true;
            this.label1.Location = new Point(21, 46);
            this.label1.Name = "label1";
            this.label1.Size = new Size(93, 13);
            this.label1.TabIndex = 100;
            this.label1.Text = "Debug Text Color:";
            this.debugModeCheckBox.AutoSize = true;
            this.debugModeCheckBox.Location = new Point(23, 20);
            this.debugModeCheckBox.Name = "debugModeCheckBox";
            this.debugModeCheckBox.Size = new Size(95, 17);
            this.debugModeCheckBox.TabIndex = 10;
            this.debugModeCheckBox.Text = "Debug Display";
            this.debugModeCheckBox.UseVisualStyleBackColor = true;
            this.debugModeCheckBox.CheckedChanged += new EventHandler(this.debugModeCheckBox_CheckedChanged);
            this.debugContextMenuStrip.Items.AddRange(new ToolStripItem[2]
            {
        (ToolStripItem) this.ctrlDhandleToolStripMenuItem1,
        (ToolStripItem) this.copyAllToolStripMenuItem
            });
            this.debugContextMenuStrip.Name = "debugContextMenuStrip";
            this.debugContextMenuStrip.Size = new Size(327, 48);
            this.ctrlDhandleToolStripMenuItem1.CheckOnClick = true;
            this.ctrlDhandleToolStripMenuItem1.Name = "ctrlDhandleToolStripMenuItem1";
            this.ctrlDhandleToolStripMenuItem1.ShortcutKeyDisplayString = "Ctrl+Z";
            this.ctrlDhandleToolStripMenuItem1.Size = new Size(326, 22);
            this.ctrlDhandleToolStripMenuItem1.Text = "Disable sync with Mugen's debug mode";
            this.ctrlDhandleToolStripMenuItem1.Click += new EventHandler(this.ctrlDhandleToolStripMenuItem1_Click);
            this.copyAllToolStripMenuItem.Name = "copyAllToolStripMenuItem";
            this.copyAllToolStripMenuItem.ShortcutKeyDisplayString = "Ctrl+C";
            this.copyAllToolStripMenuItem.Size = new Size(326, 22);
            this.copyAllToolStripMenuItem.Text = "Copy All items";
            this.copyAllToolStripMenuItem.Click += new EventHandler(this.copyAllToolStripMenuItem_Click);
            this.groupBox2.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            this.groupBox2.Controls.Add((Control)this.label4);
            this.groupBox2.Controls.Add((Control)this.skipFrameComboBox);
            this.groupBox2.Controls.Add((Control)this.stepTimeComboBox);
            this.groupBox2.Controls.Add((Control)this.label3);
            this.groupBox2.Controls.Add((Control)this.label2);
            this.groupBox2.Controls.Add((Control)this.skipModeCheckBox);
            this.groupBox2.Controls.Add((Control)this.speedModeCheckBox);
            this.groupBox2.Controls.Add((Control)this.stepButton);
            this.groupBox2.Controls.Add((Control)this.autoStepCheckBox);
            this.groupBox2.Controls.Add((Control)this.pauseCheckBox);
            this.groupBox2.Location = new Point(12, 2);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new Size(365, 93);
            this.groupBox2.TabIndex = 100;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Speed settings";
            this.groupBox2.Enter += new EventHandler(this.groupBox2_Enter);
            this.label4.AutoSize = true;
            this.label4.Location = new Point(319, 72);
            this.label4.Name = "label4";
            this.label4.Size = new Size(41, 13);
            this.label4.TabIndex = 104;
            this.label4.Text = "frames)";
            this.skipFrameComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            this.skipFrameComboBox.FlatStyle = FlatStyle.Flat;
            this.skipFrameComboBox.Font = new Font("MS UI Gothic", 8f);
            this.skipFrameComboBox.FormattingEnabled = true;
            this.skipFrameComboBox.Items.AddRange(new object[6]
            {
        (object) "120",
        (object) "60",
        (object) "30",
        (object) "10",
        (object) "5",
        (object) "1"
            });
            this.skipFrameComboBox.Location = new Point(264, 68);
            this.skipFrameComboBox.Margin = new Padding(1);
            this.skipFrameComboBox.Name = "skipFrameComboBox";
            this.skipFrameComboBox.Size = new Size(48, 19);
            this.skipFrameComboBox.TabIndex = 7;
            this.skipFrameComboBox.SelectedIndexChanged += new EventHandler(this.skipFrameComboBox_SelectedIndexChanged);
            this.stepTimeComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            this.stepTimeComboBox.FlatStyle = FlatStyle.Flat;
            this.stepTimeComboBox.Font = new Font("MS UI Gothic", 8f);
            this.stepTimeComboBox.FormattingEnabled = true;
            this.stepTimeComboBox.Items.AddRange(new object[4]
            {
        (object) "1.0",
        (object) "0.5",
        (object) "0.2",
        (object) "0.1"
            });
            this.stepTimeComboBox.Location = new Point(182, 48);
            this.stepTimeComboBox.Margin = new Padding(1);
            this.stepTimeComboBox.Name = "stepTimeComboBox";
            this.stepTimeComboBox.Size = new Size(38, 19);
            this.stepTimeComboBox.TabIndex = 4;
            this.stepTimeComboBox.SelectedIndexChanged += new EventHandler(this.stepTimeComboBox_SelectedIndexChanged);
            this.label3.AutoSize = true;
            this.label3.Location = new Point(226, 50);
            this.label3.Name = "label3";
            this.label3.Size = new Size(87, 13);
            this.label3.TabIndex = 102;
            this.label3.Text = "second intervals)";
            this.label2.AutoSize = true;
            this.label2.Location = new Point(172, 50);
            this.label2.Name = "label2";
            this.label2.Size = new Size(10, 13);
            this.label2.TabIndex = 101;
            this.label2.Text = "(";
            this.skipModeCheckBox.AutoSize = true;
            this.skipModeCheckBox.Location = new Point(175, 72);
            this.skipModeCheckBox.Name = "skipModeCheckBox";
            this.skipModeCheckBox.Size = new Size(92, 17);
            this.skipModeCheckBox.TabIndex = 6;
            this.skipModeCheckBox.Text = "Frameskip to (";
            this.skipModeCheckBox.UseVisualStyleBackColor = true;
            this.skipModeCheckBox.CheckedChanged += new EventHandler(this.skipModeCheckBox_CheckedChanged);
            this.speedModeCheckBox.AutoSize = true;
            this.speedModeCheckBox.Location = new Point(23, 72);
            this.speedModeCheckBox.Name = "speedModeCheckBox";
            this.speedModeCheckBox.Size = new Size(152, 17);
            this.speedModeCheckBox.TabIndex = 5;
            this.speedModeCheckBox.Text = "High speed mode (Ctrl + S)";
            this.speedModeCheckBox.UseVisualStyleBackColor = true;
            this.speedModeCheckBox.CheckedChanged += new EventHandler(this.speedModeCheckBox_CheckedChanged);
            this.stepButton.Location = new Point(175, 17);
            this.stepButton.Name = "stepButton";
            this.stepButton.Size = new Size(101, 27);
            this.stepButton.TabIndex = 2;
            this.stepButton.Text = "Stepframe";
            this.stepButton.UseVisualStyleBackColor = true;
            this.stepButton.Click += new EventHandler(this.stepButton_Click);
            this.autoStepCheckBox.AutoSize = true;
            this.autoStepCheckBox.Location = new Point(23, 49);
            this.autoStepCheckBox.Name = "autoStepCheckBox";
            this.autoStepCheckBox.Size = new Size(97, 17);
            this.autoStepCheckBox.TabIndex = 3;
            this.autoStepCheckBox.Text = "Auto stepframe";
            this.autoStepCheckBox.UseVisualStyleBackColor = true;
            this.autoStepCheckBox.CheckedChanged += new EventHandler(this.autoStepCheckBox_CheckedChanged);
            this.pauseCheckBox.AutoSize = true;
            this.pauseCheckBox.Location = new Point(23, 23);
            this.pauseCheckBox.Name = "pauseCheckBox";
            this.pauseCheckBox.Size = new Size(56, 17);
            this.pauseCheckBox.TabIndex = 1;
            this.pauseCheckBox.Text = "Pause";
            this.pauseCheckBox.UseVisualStyleBackColor = true;
            this.pauseCheckBox.CheckedChanged += new EventHandler(this.pauseCheckBox_CheckedChanged);
            this.toolTip1.AutomaticDelay = 500000;
            this.toolTip1.AutoPopDelay = 5000000;
            this.toolTip1.InitialDelay = 5000000;
            this.toolTip1.IsBalloon = true;
            this.toolTip1.ReshowDelay = 1000000;
            this.listTabControl.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            this.listTabControl.Controls.Add((Control)this.playerTabPage);
            this.listTabControl.Controls.Add((Control)this.explodTabPage);
            this.listTabControl.Controls.Add((Control)this.projTabPage);
            this.listTabControl.Controls.Add((Control)this.tabPage1);
            this.listTabControl.Location = new Point(7, 230);
            this.listTabControl.Name = "listTabControl";
            this.listTabControl.Padding = new Point(0, 0);
            this.listTabControl.SelectedIndex = 0;
            this.listTabControl.Size = new Size(380, 365);
            this.listTabControl.TabIndex = 102;
            this.listTabControl.SelectedIndexChanged += new EventHandler(this.listTabControl_SelectedIndexChanged);
            this.playerTabPage.Controls.Add((Control)this.playerListView);
            this.playerTabPage.Location = new Point(4, 22);
            this.playerTabPage.Name = "playerTabPage";
            this.playerTabPage.Padding = new Padding(3);
            this.playerTabPage.Size = new Size(372, 339);
            this.playerTabPage.TabIndex = 0;
            this.playerTabPage.Text = "Player list";
            this.playerTabPage.UseVisualStyleBackColor = true;
            this.playerListView.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            this.playerListView.Columns.AddRange(new ColumnHeader[5]
            {
        this.noHeader,
        this.playeridHeader,
        this.helperidHeader,
        this.parentidHeader,
        this.nameHeader
            });
            this.playerListView.ContextMenuStrip = this.debugContextMenuStrip;
            this.playerListView.FullRowSelect = true;
            this.playerListView.GridLines = true;
            this.playerListView.Location = new Point(-4, -1);
            this.playerListView.Margin = new Padding(0);
            this.playerListView.MultiSelect = false;
            this.playerListView.Name = "playerListView";
            this.playerListView.Size = new Size(380, 342);
            this.playerListView.TabIndex = 101;
            this.playerListView.TabStop = false;
            this.playerListView.UseCompatibleStateImageBehavior = false;
            this.playerListView.View = View.Details;
            this.playerListView.ColumnClick += new ColumnClickEventHandler(this.playerListView_ColumnClick);
            this.playerListView.SelectedIndexChanged += new EventHandler(this.playerListView_SelectedIndexChanged);
            this.playerListView.MouseClick += new MouseEventHandler(this.playerListView_MouseClick);
            this.playerListView.MouseDown += new MouseEventHandler(this.playerListView_MouseDown);
            this.playerListView.MouseUp += new MouseEventHandler(this.playerListView_MouseUp);
            this.noHeader.Text = "No.";
            this.noHeader.Width = 30;
            this.playeridHeader.Text = "Player ID";
            this.helperidHeader.Text = "Helper ID";
            this.parentidHeader.Text = "Owner's ID";
            this.parentidHeader.Width = 74;
            this.nameHeader.Text = "Name";
            this.nameHeader.Width = 120;
            this.explodTabPage.Controls.Add((Control)this.explodTailLabel);
            this.explodTabPage.Controls.Add((Control)this.explodHeadLabel);
            this.explodTabPage.Controls.Add((Control)this.explodHeadNumericUpDown);
            this.explodTabPage.Controls.Add((Control)this.explodListView);
            this.explodTabPage.Location = new Point(4, 22);
            this.explodTabPage.Name = "explodTabPage";
            this.explodTabPage.Padding = new Padding(3);
            this.explodTabPage.Size = new Size(372, 339);
            this.explodTabPage.TabIndex = 1;
            this.explodTabPage.Text = "Explod list";
            this.explodTabPage.UseVisualStyleBackColor = true;
            this.explodTailLabel.AutoSize = true;
            this.explodTailLabel.Location = new Point(186, 12);
            this.explodTailLabel.Name = "explodTailLabel";
            this.explodTailLabel.Size = new Size(31, 13);
            this.explodTailLabel.TabIndex = 105;
            this.explodTailLabel.Text = "～49";
            this.explodHeadLabel.AutoSize = true;
            this.explodHeadLabel.Location = new Point(6, 12);
            this.explodHeadLabel.Name = "explodHeadLabel";
            this.explodHeadLabel.Size = new Size(56, 13);
            this.explodHeadLabel.TabIndex = 104;
            this.explodHeadLabel.Text = "Object no.";
            this.explodHeadLabel.MouseClick += new MouseEventHandler(this.explodHeadLabel_MouseClick);
            this.explodHeadNumericUpDown.Increment = new Decimal(new int[4]
            {
        50,
        0,
        0,
        0
            });
            this.explodHeadNumericUpDown.Location = new Point(85, 9);
            this.explodHeadNumericUpDown.Margin = new Padding(3, 3, 3, 0);
            this.explodHeadNumericUpDown.Maximum = new Decimal(new int[4]
            {
        20000,
        0,
        0,
        0
            });
            this.explodHeadNumericUpDown.Name = "explodHeadNumericUpDown";
            this.explodHeadNumericUpDown.Size = new Size(95, 20);
            this.explodHeadNumericUpDown.TabIndex = 103;
            this.explodHeadNumericUpDown.TextAlign = HorizontalAlignment.Right;
            this.explodHeadNumericUpDown.ValueChanged += new EventHandler(this.explodHeadNumericUpDown_ValueChanged);
            this.explodHeadNumericUpDown.MouseClick += new MouseEventHandler(this.explodHeadNumericUpDown_MouseClick);
            this.explodListView.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            this.explodListView.Columns.AddRange(new ColumnHeader[4]
            {
        this.columnHeader1,
        this.columnHeader2,
        this.columnHeader3,
        this.columnHeader4
            });
            this.explodListView.ContextMenuStrip = this.explodContextMenuStrip;
            this.explodListView.FullRowSelect = true;
            this.explodListView.GridLines = true;
            this.explodListView.Location = new Point(-4, 36);
            this.explodListView.Margin = new Padding(0);
            this.explodListView.MultiSelect = false;
            this.explodListView.Name = "explodListView";
            this.explodListView.Size = new Size(380, 303);
            this.explodListView.TabIndex = 102;
            this.explodListView.TabStop = false;
            this.explodListView.UseCompatibleStateImageBehavior = false;
            this.explodListView.View = View.Details;
            this.explodListView.ColumnClick += new ColumnClickEventHandler(this.explodListView_ColumnClick);
            this.explodListView.SelectedIndexChanged += new EventHandler(this.explodListView_SelectedIndexChanged);
            this.explodListView.MouseClick += new MouseEventHandler(this.explodListView_MouseClick);
            this.explodListView.MouseDown += new MouseEventHandler(this.explodListView_MouseDown);
            this.explodListView.MouseUp += new MouseEventHandler(this.explodListView_MouseUp);
            this.columnHeader1.Text = "No.";
            this.columnHeader1.Width = 50;
            this.columnHeader2.Text = "ID";
            this.columnHeader2.Width = 62;
            this.columnHeader3.Text = "Anim";
            this.columnHeader4.Text = "Owner's Player ID";
            this.columnHeader4.Width = 102;
            this.explodContextMenuStrip.Items.AddRange(new ToolStripItem[1]
            {
        (ToolStripItem) this.explodToolStripMenuItem1
            });
            this.explodContextMenuStrip.Name = "debugContextMenuStrip";
            this.explodContextMenuStrip.Size = new Size(194, 26);
            this.explodToolStripMenuItem1.Name = "explodToolStripMenuItem1";
            this.explodToolStripMenuItem1.ShortcutKeyDisplayString = "Ctrl+C";
            this.explodToolStripMenuItem1.Size = new Size(193, 22);
            this.explodToolStripMenuItem1.Text = "Copy All items";
            this.explodToolStripMenuItem1.Click += new EventHandler(this.explodToolStripMenuItem1_Click);
            this.projTabPage.Controls.Add((Control)this.label5);
            this.projTabPage.Controls.Add((Control)this.p4RadioButton);
            this.projTabPage.Controls.Add((Control)this.p3RadioButton);
            this.projTabPage.Controls.Add((Control)this.p2RadioButton);
            this.projTabPage.Controls.Add((Control)this.p1RadioButton);
            this.projTabPage.Controls.Add((Control)this.projTailLabel);
            this.projTabPage.Controls.Add((Control)this.projHeadLabel);
            this.projTabPage.Controls.Add((Control)this.projHeadNumericUpDown);
            this.projTabPage.Controls.Add((Control)this.projListView);
            this.projTabPage.Location = new Point(4, 22);
            this.projTabPage.Name = "projTabPage";
            this.projTabPage.Padding = new Padding(3);
            this.projTabPage.Size = new Size(372, 339);
            this.projTabPage.TabIndex = 2;
            this.projTabPage.Text = "Projectile list";
            this.projTabPage.UseVisualStyleBackColor = true;
            this.label5.AutoSize = true;
            this.label5.Location = new Point(6, 11);
            this.label5.Name = "label5";
            this.label5.Size = new Size(41, 13);
            this.label5.TabIndex = 114;
            this.label5.Text = "Owner:";
            this.label5.MouseClick += new MouseEventHandler(this.label5_MouseClick);
            this.p4RadioButton.AutoSize = true;
            this.p4RadioButton.Location = new Point(209, 9);
            this.p4RadioButton.Margin = new Padding(3, 3, 3, 0);
            this.p4RadioButton.Name = "p4RadioButton";
            this.p4RadioButton.Size = new Size(38, 17);
            this.p4RadioButton.TabIndex = 113;
            this.p4RadioButton.TabStop = true;
            this.p4RadioButton.Text = "P4";
            this.p4RadioButton.UseVisualStyleBackColor = true;
            this.p4RadioButton.CheckedChanged += new EventHandler(this.p4RadioButton_CheckedChanged);
            this.p3RadioButton.AutoSize = true;
            this.p3RadioButton.Location = new Point(161, 9);
            this.p3RadioButton.Margin = new Padding(3, 3, 3, 0);
            this.p3RadioButton.Name = "p3RadioButton";
            this.p3RadioButton.Size = new Size(38, 17);
            this.p3RadioButton.TabIndex = 112;
            this.p3RadioButton.TabStop = true;
            this.p3RadioButton.Text = "P3";
            this.p3RadioButton.UseVisualStyleBackColor = true;
            this.p3RadioButton.CheckedChanged += new EventHandler(this.p3RadioButton_CheckedChanged);
            this.p2RadioButton.AutoSize = true;
            this.p2RadioButton.Location = new Point(113, 9);
            this.p2RadioButton.Margin = new Padding(3, 3, 3, 0);
            this.p2RadioButton.Name = "p2RadioButton";
            this.p2RadioButton.Size = new Size(38, 17);
            this.p2RadioButton.TabIndex = 111;
            this.p2RadioButton.TabStop = true;
            this.p2RadioButton.Text = "P2";
            this.p2RadioButton.UseVisualStyleBackColor = true;
            this.p2RadioButton.CheckedChanged += new EventHandler(this.p2RadioButton_CheckedChanged);
            this.p1RadioButton.AutoSize = true;
            this.p1RadioButton.Location = new Point(66, 9);
            this.p1RadioButton.Margin = new Padding(3, 3, 3, 0);
            this.p1RadioButton.Name = "p1RadioButton";
            this.p1RadioButton.Size = new Size(38, 17);
            this.p1RadioButton.TabIndex = 110;
            this.p1RadioButton.TabStop = true;
            this.p1RadioButton.Text = "P1";
            this.p1RadioButton.UseVisualStyleBackColor = true;
            this.p1RadioButton.CheckedChanged += new EventHandler(this.p1RadioButton_CheckedChanged);
            this.projTailLabel.AutoSize = true;
            this.projTailLabel.Location = new Point(186, 37);
            this.projTailLabel.Name = "projTailLabel";
            this.projTailLabel.Size = new Size(31, 13);
            this.projTailLabel.TabIndex = 109;
            this.projTailLabel.Text = "～49";
            this.projHeadLabel.AutoSize = true;
            this.projHeadLabel.Location = new Point(6, 37);
            this.projHeadLabel.Name = "projHeadLabel";
            this.projHeadLabel.Size = new Size(56, 13);
            this.projHeadLabel.TabIndex = 108;
            this.projHeadLabel.Text = "Object no.";
            this.projHeadLabel.MouseClick += new MouseEventHandler(this.projHeadLabel_MouseClick);
            this.projHeadNumericUpDown.Increment = new Decimal(new int[4]
            {
        50,
        0,
        0,
        0
            });
            this.projHeadNumericUpDown.Location = new Point(85, 34);
            this.projHeadNumericUpDown.Margin = new Padding(3, 3, 3, 0);
            this.projHeadNumericUpDown.Maximum = new Decimal(new int[4]
            {
        20000,
        0,
        0,
        0
            });
            this.projHeadNumericUpDown.Name = "projHeadNumericUpDown";
            this.projHeadNumericUpDown.Size = new Size(95, 20);
            this.projHeadNumericUpDown.TabIndex = 107;
            this.projHeadNumericUpDown.TextAlign = HorizontalAlignment.Right;
            this.projHeadNumericUpDown.ValueChanged += new EventHandler(this.projHeadNumericUpDown_ValueChanged);
            this.projHeadNumericUpDown.MouseClick += new MouseEventHandler(this.projHeadNumericUpDown_MouseClick);
            this.projListView.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            this.projListView.Columns.AddRange(new ColumnHeader[4]
            {
        this.columnHeader5,
        this.columnHeader6,
        this.columnHeader7,
        this.columnHeader8
            });
            this.projListView.ContextMenuStrip = this.projContextMenuStrip;
            this.projListView.FullRowSelect = true;
            this.projListView.GridLines = true;
            this.projListView.Location = new Point(-4, 61);
            this.projListView.Margin = new Padding(0);
            this.projListView.MultiSelect = false;
            this.projListView.Name = "projListView";
            this.projListView.Size = new Size(380, 273);
            this.projListView.TabIndex = 106;
            this.projListView.TabStop = false;
            this.projListView.UseCompatibleStateImageBehavior = false;
            this.projListView.View = View.Details;
            this.projListView.ColumnClick += new ColumnClickEventHandler(this.projListView_ColumnClick);
            this.projListView.SelectedIndexChanged += new EventHandler(this.projListView_SelectedIndexChanged);
            this.projListView.MouseClick += new MouseEventHandler(this.projListView_MouseClick);
            this.projListView.MouseDown += new MouseEventHandler(this.projListView_MouseDown);
            this.projListView.MouseUp += new MouseEventHandler(this.projListView_MouseUp);
            this.columnHeader5.Text = "No.";
            this.columnHeader5.Width = 50;
            this.columnHeader6.Text = "ProjID";
            this.columnHeader6.Width = 80;
            this.columnHeader7.Text = "X";
            this.columnHeader8.Text = "Y";
            this.projContextMenuStrip.Items.AddRange(new ToolStripItem[2]
            {
        (ToolStripItem) this.projToolStripMenuItem1,
        (ToolStripItem) this.projToolStripMenuItem2
            });
            this.projContextMenuStrip.Name = "debugContextMenuStrip";
            this.projContextMenuStrip.Size = new Size(296, 48);
            this.projToolStripMenuItem1.Name = "projToolStripMenuItem1";
            this.projToolStripMenuItem1.ShortcutKeyDisplayString = "Ctrl+C";
            this.projToolStripMenuItem1.Size = new Size(295, 22);
            this.projToolStripMenuItem1.Text = "Copy All Items";
            this.projToolStripMenuItem1.Click += new EventHandler(this.projToolStripMenuItem1_Click);
            this.projToolStripMenuItem2.Name = "projToolStripMenuItem2";
            this.projToolStripMenuItem2.ShortcutKeyDisplayString = "Ctrl+V";
            this.projToolStripMenuItem2.Size = new Size(295, 22);
            this.projToolStripMenuItem2.Text = "Show variables of projectile owner";
            this.projToolStripMenuItem2.Click += new EventHandler(this.projToolStripMenuItem2_Click);
            this.tabPage1.Controls.Add((Control)this.groupBox4);
            this.tabPage1.Controls.Add((Control)this.triggerGroupBox);
            this.tabPage1.Location = new Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new Padding(3);
            this.tabPage1.Size = new Size(372, 339);
            this.tabPage1.TabIndex = 3;
            this.tabPage1.Text = "Triggers";
            this.tabPage1.UseVisualStyleBackColor = true;
            this.groupBox4.Controls.Add((Control)this.setButton);
            this.groupBox4.Controls.Add((Control)this.revertButton);
            this.groupBox4.Controls.Add((Control)this.valueComboBox);
            this.groupBox4.Controls.Add((Control)this.triggerComboBox);
            this.groupBox4.Controls.Add((Control)this.playerComboBox);
            this.groupBox4.Controls.Add((Control)this.label8);
            this.groupBox4.Controls.Add((Control)this.label7);
            this.groupBox4.Controls.Add((Control)this.label6);
            this.groupBox4.Location = new Point(6, 91);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new Size(251, 129);
            this.groupBox4.TabIndex = 6;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Testing parameters";
            this.setButton.Location = new Point(101, 98);
            this.setButton.Name = "setButton";
            this.setButton.Size = new Size(69, 25);
            this.setButton.TabIndex = 6;
            this.setButton.Text = "Confirm";
            this.setButton.UseVisualStyleBackColor = true;
            this.setButton.Click += new EventHandler(this.setButton_Click);
            this.revertButton.Enabled = false;
            this.revertButton.Location = new Point(176, 98);
            this.revertButton.Name = "revertButton";
            this.revertButton.Size = new Size(69, 25);
            this.revertButton.TabIndex = 7;
            this.revertButton.Text = "Undo";
            this.revertButton.UseVisualStyleBackColor = true;
            this.revertButton.Click += new EventHandler(this.revertButton_Click);
            this.valueComboBox.FormattingEnabled = true;
            // list of available operators and comparisons
            this.valueComboBox.Items.AddRange(new object[14]
            {
        (object) "= xxx",
        (object) "!= xxx",
        (object) "< xxx",
        (object) "<= xxx",
        (object) "> xxx",
        (object) ">= xxx",
        (object) "= [ xxx , yyy ]",
        (object) "= ( xxx , yyy ]",
        (object) "= [ xxx , yyy )",
        (object) "= ( xxx , yyy )",
        (object) "!= [ xxx , yyy ]",
        (object) "!= ( xxx , yyy ]",
        (object) "!= [ xxx , yyy )",
        (object) "!= ( xxx , yyy )"
            });
            this.valueComboBox.Location = new Point(58, 72);
            this.valueComboBox.Name = "valueComboBox";
            this.valueComboBox.Size = new Size(184, 21);
            this.valueComboBox.TabIndex = 5;
            this.valueComboBox.Text = "(Unspecified)";
            this.valueComboBox.MouseClick += new MouseEventHandler(this.valueComboBox_MouseClick);
            this.triggerComboBox.FormattingEnabled = true;
            // list of available trigger types
            this.triggerComboBox.Items.AddRange(new object[7]
            {
        (object) "Alive",
        (object) "FVar( xxx )",
        (object) "StateNo",
        (object) "SysFVar( xxx )",
        (object) "SysVar( xxx )",
        (object) "Var( xxx )",
        (object) "NumHelper"
            });
            this.triggerComboBox.Location = new Point(58, 44);
            this.triggerComboBox.Name = "triggerComboBox";
            this.triggerComboBox.Size = new Size(184, 21);
            this.triggerComboBox.TabIndex = 4;
            this.triggerComboBox.Text = "(Unspecified)";
            this.triggerComboBox.MouseClick += new MouseEventHandler(this.triggerComboBox_MouseClick);
            this.playerComboBox.FormattingEnabled = true;
            this.playerComboBox.Items.AddRange(new object[9]
            {
        (object) "P1",
        (object) "P2",
        (object) "P3",
        (object) "P4",
        (object) "PlayerId( xxx )",
        (object) "P1,HelperId( xxx )",
        (object) "P2,HelperId( xxx )",
        (object) "P3,HelperId( xxx )",
        (object) "P4,HelperId( xxx )"
            });
            this.playerComboBox.Location = new Point(58, 17);
            this.playerComboBox.Name = "playerComboBox";
            this.playerComboBox.Size = new Size(183, 21);
            this.playerComboBox.TabIndex = 3;
            this.playerComboBox.Text = "(Unspecified)";
            this.playerComboBox.MouseClick += new MouseEventHandler(this.playerComboBox_MouseClick);
            this.label8.AutoSize = true;
            this.label8.Location = new Point(13, 75);
            this.label8.Name = "label8";
            this.label8.Size = new Size(42, 13);
            this.label8.TabIndex = 2;
            this.label8.Text = "Values:";
            this.label7.AutoSize = true;
            this.label7.Location = new Point(13, 48);
            this.label7.Name = "label7";
            this.label7.Size = new Size(43, 13);
            this.label7.TabIndex = 1;
            this.label7.Text = "Trigger:";
            this.label6.AutoSize = true;
            this.label6.Location = new Point(13, 21);
            this.label6.Name = "label6";
            this.label6.Size = new Size(39, 13);
            this.label6.TabIndex = 0;
            this.label6.Text = "Player:";
            this.triggerGroupBox.Controls.Add((Control)this.checkModeStateText);
            this.triggerGroupBox.Controls.Add((Control)this.triggerCheckStopButton);
            this.triggerGroupBox.Controls.Add((Control)this.triggerCheckResumeButton);
            this.triggerGroupBox.Controls.Add((Control)this.triggerCheckStartButton);
            this.triggerGroupBox.Location = new Point(6, 7);
            this.triggerGroupBox.Name = "triggerGroupBox";
            this.triggerGroupBox.Size = new Size(252, 78);
            this.triggerGroupBox.TabIndex = 5;
            this.triggerGroupBox.TabStop = false;
            this.triggerGroupBox.Text = "Trigger breakpoints status";
            this.checkModeStateText.Font = new Font("MS UI Gothic", 11f, FontStyle.Bold, GraphicsUnit.Point, (byte)128);
            this.checkModeStateText.Location = new Point(9, 20);
            this.checkModeStateText.Name = "checkModeStateText";
            this.checkModeStateText.ReadOnly = true;
            this.checkModeStateText.Size = new Size(232, 22);
            this.checkModeStateText.TabIndex = 2;
            this.checkModeStateText.TabStop = false;
            this.checkModeStateText.Text = "Waiting for settings";
            this.checkModeStateText.MouseClick += new MouseEventHandler(this.checkModeStateText_MouseClick);
            this.triggerCheckStopButton.Enabled = false;
            this.triggerCheckStopButton.Location = new Point(172, 49);
            this.triggerCheckStopButton.Name = "triggerCheckStopButton";
            this.triggerCheckStopButton.Size = new Size(69, 25);
            this.triggerCheckStopButton.TabIndex = 3;
            this.triggerCheckStopButton.Text = "Stop";
            this.triggerCheckStopButton.UseVisualStyleBackColor = true;
            this.triggerCheckStopButton.Click += new EventHandler(this.triggerCheckStopButton_Click);
            this.triggerCheckResumeButton.Enabled = false;
            this.triggerCheckResumeButton.Location = new Point(91, 49);
            this.triggerCheckResumeButton.Name = "triggerCheckResumeButton";
            this.triggerCheckResumeButton.Size = new Size(69, 25);
            this.triggerCheckResumeButton.TabIndex = 4;
            this.triggerCheckResumeButton.Text = "Resume";
            this.triggerCheckResumeButton.UseVisualStyleBackColor = true;
            this.triggerCheckResumeButton.Click += new EventHandler(this.triggerCheckResumeButton_Click);
            this.triggerCheckStartButton.Enabled = false;
            this.triggerCheckStartButton.Location = new Point(10, 49);
            this.triggerCheckStartButton.Name = "triggerCheckStartButton";
            this.triggerCheckStartButton.Size = new Size(69, 25);
            this.triggerCheckStartButton.TabIndex = 1;
            this.triggerCheckStartButton.Text = "Start";
            this.triggerCheckStartButton.UseVisualStyleBackColor = true;
            this.triggerCheckStartButton.Click += new EventHandler(this.triggerCheckStartButton_Click);
            this.setButtonTimer.Enabled = true;
            this.setButtonTimer.Interval = 600;
            this.setButtonTimer.Tick += new EventHandler(this.setButtonTimer_Tick);
            this.blinkTimer.Enabled = true;
            this.blinkTimer.Interval = 500;
            this.blinkTimer.Tick += new EventHandler(this.blinkTimer_Tick);
            this.AutoScaleDimensions = new SizeF(6f, 13f);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.ClientSize = new Size(390, 625);
            this.Controls.Add((Control)this.listTabControl);
            this.Controls.Add((Control)this.groupBox2);
            this.Controls.Add((Control)this.groupBox1);
            this.Icon = (Icon)componentResourceManager.GetObject("$this.Icon");
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = nameof(DebugForm);
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "Debug Tools";
            this.Activated += new EventHandler(this.DebugForm_Activated);
            this.Deactivate += new EventHandler(this.DebugForm_Deactivate);
            this.FormClosing += new FormClosingEventHandler(this.DebugForm_FormClosing);
            this.Load += new EventHandler(this.DebugForm_Load);
            this.KeyDown += new KeyEventHandler(this.DebugForm_KeyDown);
            this.KeyPress += new KeyPressEventHandler(this.DebugForm_KeyPress);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.debugContextMenuStrip.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.listTabControl.ResumeLayout(false);
            this.playerTabPage.ResumeLayout(false);
            this.explodTabPage.ResumeLayout(false);
            this.explodTabPage.PerformLayout();
            this.explodHeadNumericUpDown.EndInit();
            this.explodContextMenuStrip.ResumeLayout(false);
            this.projTabPage.ResumeLayout(false);
            this.projTabPage.PerformLayout();
            this.projHeadNumericUpDown.EndInit();
            this.projContextMenuStrip.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.triggerGroupBox.ResumeLayout(false);
            this.triggerGroupBox.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private void debugColorSliders_ValueChanged(object sender, EventArgs e)
        {
            this.debugColorCustomRedLabel.Text = string.Format("Red ({0})", this.debugColorCustomRed.Value);
            this.debugColorCustomBlueLabel.Text = string.Format("Blue ({0})", this.debugColorCustomBlue.Value);
            this.debugColorCustomGreenLabel.Text = string.Format("Green ({0})", this.debugColorCustomGreen.Value);
            MugenWindow.MainObj().SetDebugCustomColors(this.debugColorCustomRed.Value, this.debugColorCustomGreen.Value, this.debugColorCustomBlue.Value);
        }

        private DebugForm() => this.InitializeComponent();

        [DllImport("user32.dll")]
        private static extern bool IsWindowVisible(IntPtr hWnd);

        public static DebugForm MainObj()
        {
            if (DebugForm.selfObj == null)
                DebugForm.selfObj = new DebugForm();
            return DebugForm.selfObj;
        }

        public bool IsPaused() => this.pauseCheckBox.Checked;

        public void SetPauseCheckBox(bool isPaused)
        {
            if ((isPaused || !this.pauseCheckBox.Checked || Form.ActiveForm != this && Form.ActiveForm != VarForm.MainObj()) && (isPaused || !this.pauseCheckBox.Checked || !this._ignoreUnPauseRequestOnce))
            {
                this.pauseCheckBox.CheckedChanged -= new EventHandler(this.pauseCheckBox_CheckedChanged);
                this.pauseCheckBox.Checked = isPaused;
                this.pauseCheckBox.CheckedChanged += new EventHandler(this.pauseCheckBox_CheckedChanged);
            }
            if (!this._ignoreUnPauseRequestOnce)
                return;
            this._ignoreUnPauseRequestOnce = false;
        }

        public void IgnoreUnPauseRequestOnce()
        {
            if (!this.pauseCheckBox.Checked)
                return;
            this._ignoreUnPauseRequestOnce = true;
        }

        public void TogglePauseCheckBox() => this.pauseCheckBox.Checked = !this.pauseCheckBox.Checked;

        public void SetSpeedModeCheckBox(bool isSpeedMode, bool forced)
        {
            if (forced)
            {
                this.speedModeCheckBox.Checked = isSpeedMode;
            }
            else
            {
                this.speedModeCheckBox.CheckedChanged -= new EventHandler(this.speedModeCheckBox_CheckedChanged);
                this.speedModeCheckBox.Checked = isSpeedMode;
                this.speedModeCheckBox.CheckedChanged += new EventHandler(this.speedModeCheckBox_CheckedChanged);
            }
        }

        public void ToggleSpeedModeCheckBox() => this.speedModeCheckBox.Checked = !this.speedModeCheckBox.Checked;

        public void SetSkipModeCheckBox(bool isSkipMode) => this.skipModeCheckBox.Checked = isSkipMode;

        public void SetDebugModeCheckBox(bool isDebugMode, bool forceUpdate)
        {
            this.debugModeCheckBox.Checked = isDebugMode;
            if (forceUpdate)
            {
                VarForm.MainObj().SetDebugMode(this.debugModeCheckBox.Checked);
                MugenWindow.MainObj().SetDebugMode(this.debugModeCheckBox.Checked, true);
            }
            if (this.debugModeCheckBox.Checked)
            {
                this.playerListView.ForeColor = Control.DefaultForeColor;
                this.explodListView.ForeColor = Control.DefaultForeColor;
                this.projListView.ForeColor = Control.DefaultForeColor;
                this.explodHeadNumericUpDown.Enabled = true;
                this.projHeadNumericUpDown.Enabled = true;
            }
            else
            {
                this.playerListView.ForeColor = Color.Gray;
                this.explodListView.ForeColor = Color.Gray;
                this.projListView.ForeColor = Color.Gray;
                this.explodHeadNumericUpDown.Enabled = false;
                this.projHeadNumericUpDown.Enabled = false;
            }
        }

        public void SetDebugTargetNo(int debugNo)
        {
            if (this.ctrlDhandleToolStripMenuItem1.Checked || debugNo < 1 || this.playerListView.Items.Count == 0)
                return;
            this.playerListView.Items[this._selectedIndex].BackColor = this.playerListView.BackColor;
            this._selectedIndex = debugNo - 1;
            this.playerListView.Items[this._selectedIndex].Selected = true;
            MugenWindow.MainObj().SetVarInspectTargetNo(debugNo);
        }

        public void ShiftDebugTargetNo()
        {
            if (this.playerListView.Items.Count == 0 || this._playerId == null)
                return;
            if (!this.ctrlDhandleToolStripMenuItem1.Checked)
            {
                if (this.debugModeCheckBox.Checked)
                {
                    this.playerListView.Items[this._selectedIndex].BackColor = this.playerListView.BackColor;
                    while (this._playerId.Length > this._selectedIndex)
                    {
                        ++this._selectedIndex;
                        if (this._playerId.Length <= this._selectedIndex || this._playerId[this._selectedIndex] != -1)
                            break;
                    }
                    if (this._playerId.Length <= this._selectedIndex)
                    {
                        this._selectedIndex = 0;
                        this.debugModeCheckBox.CheckedChanged -= new EventHandler(this.debugModeCheckBox_CheckedChanged);
                        this.debugModeCheckBox.Checked = false;
                        this.debugModeCheckBox.CheckedChanged += new EventHandler(this.debugModeCheckBox_CheckedChanged);
                        this._debugModeCheckBox_CheckedChanged();
                    }
                    else
                        this.playerListView.Items[this._selectedIndex].Selected = true;
                }
                else
                {
                    this.debugModeCheckBox.CheckedChanged -= new EventHandler(this.debugModeCheckBox_CheckedChanged);
                    this.debugModeCheckBox.Checked = true;
                    this.debugModeCheckBox.CheckedChanged += new EventHandler(this.debugModeCheckBox_CheckedChanged);
                    this.playerListView.Items[this._selectedIndex].Selected = true;
                    this._debugModeCheckBox_CheckedChanged();
                }
            }
            else if (this.debugModeCheckBox.Checked)
            {
                int currentDebugTargetNo = MugenWindow.MainObj().GetCurrentDebugTargetNo();
                if (currentDebugTargetNo > 0)
                    --currentDebugTargetNo;
                while (this._playerId.Length > currentDebugTargetNo)
                {
                    ++currentDebugTargetNo;
                    if (this._playerId.Length <= currentDebugTargetNo || this._playerId[currentDebugTargetNo] != -1)
                        break;
                }
                if (this._playerId.Length <= currentDebugTargetNo)
                {
                    this.debugModeCheckBox.CheckedChanged -= new EventHandler(this.debugModeCheckBox_CheckedChanged);
                    this.debugModeCheckBox.Checked = false;
                    this.debugModeCheckBox.CheckedChanged += new EventHandler(this.debugModeCheckBox_CheckedChanged);
                    this._debugModeCheckBox_CheckedChanged();
                }
                else
                    MugenWindow.MainObj().SetDebugTargetNo(currentDebugTargetNo + 1);
            }
            else
            {
                this.debugModeCheckBox.CheckedChanged -= new EventHandler(this.debugModeCheckBox_CheckedChanged);
                this.debugModeCheckBox.Checked = true;
                this.debugModeCheckBox.CheckedChanged += new EventHandler(this.debugModeCheckBox_CheckedChanged);
                this.playerListView.Items[this._selectedIndex].Selected = true;
                MugenWindow.MainObj().SetDebugTargetNo(1);
                this._debugModeCheckBox_CheckedChanged();
            }
        }

        public void InitPlayers(int playerCount)
        {
            this.playerListView.Items.Clear();
            for (int index = 0; index < playerCount; ++index)
                this.playerListView.Items.Add(new ListViewItem(new string[5]
                {
          index.ToString(),
          "-",
          "-",
          "-",
          "-"
                }));
            this._selectedIndex = 0;
            this.playerListView.Items[0].Selected = true;
            this._playerId = (int[])null;
        }

        public void InitExplods(int explodCount)
        {
            this.explodListView.Items.Clear();
            for (int index = 0; index < explodCount; ++index)
                this.explodListView.Items.Add(new ListViewItem(new string[4]
                {
          index.ToString(),
          "-",
          "-",
          "-"
                }));
            this.explodListView.Items[0].Selected = true;
            this.explodListView.Items[0].Selected = false;
            this._explodId = (int[])null;
            this._anim = (int[])null;
            this._ownerId = (int[])null;
            this.explodHeadNumericUpDown.Value = 0M;
            this._isExplodListDirty = true;
            MugenWindow.MainObj().SetExplodHeadNo(0);
        }

        public void InitProjs(int projCount)
        {
            this.projListView.Items.Clear();
            for (int index = 0; index < projCount; ++index)
                this.projListView.Items.Add(new ListViewItem(new string[4]
                {
          index.ToString(),
          "-",
          "-",
          "-"
                }));
            this.projListView.Items[0].Selected = true;
            this.projListView.Items[0].Selected = false;
            this._projId = (int[])null;
            this._projX = (int[])null;
            this._projY = (int[])null;
            this.projHeadNumericUpDown.Value = 0M;
            this._isProjListDirty = true;
            MugenWindow.MainObj().SetProjHeadNo(0);
            this.p1RadioButton.Checked = true;
            this.p1RadioButton.Enabled = true;
            this.p2RadioButton.Enabled = false;
            this.p3RadioButton.Enabled = false;
            this.p4RadioButton.Enabled = false;
        }

        public void InitTriggerCheck(MugenWindow.MugenType_t mugen_type)
        {
        }

        public void PreInitTriggerCheck(MugenWindow.MugenType_t mugen_type)
        {
            this.label1.Text = "Debug Text Color:";
            if (MugenWindow.MainObj().GetTriggerCheckMode() == TriggerCheckTarget.CheckMode.CHECKMODE_STARTED)
            {
                this.checkModeStateText.BackColor = this._defaultBgColor;
                this.checkModeStateText.ForeColor = this._defaultFgColor;
                this.checkModeStateText.Text = "Waiting";
                this.triggerCheckStartButton.Enabled = false;
                this.triggerCheckStopButton.Enabled = true;
                this.triggerCheckResumeButton.Enabled = false;
            }
            else
            {
                this.checkModeStateText.BackColor = this._defaultBgColor;
                this.checkModeStateText.ForeColor = this._defaultFgColor;
                this.checkModeStateText.Text = "Waiting for start";
                this.triggerCheckStartButton.Enabled = true;
                this.triggerCheckStopButton.Enabled = false;
                this.triggerCheckResumeButton.Enabled = false;
            }
        }

        public void FinalizeTriggerCheck(MugenWindow.MugenType_t mugen_type)
        {
        }

        public void PostFinalizeTriggerCheck(MugenWindow.MugenType_t mugen_type)
        {
            if (MugenWindow.MainObj().GetTriggerCheckMode() != TriggerCheckTarget.CheckMode.CHECKMODE_STARTED || MugenWindow.MainObj().IsPaused())
                return;
            this.checkModeStateText.BackColor = this._defaultBgColor;
            this.checkModeStateText.ForeColor = this._defaultFgColor;
            this.checkModeStateText.Text = "Waiting";
        }

        public void EnablePlayer(int playerNo)
        {
            switch (playerNo)
            {
                case 1:
                    this.p1RadioButton.Enabled = true;
                    break;
                case 2:
                    this.p2RadioButton.Enabled = true;
                    break;
                case 3:
                    this.p3RadioButton.Enabled = true;
                    break;
                case 4:
                    this.p4RadioButton.Enabled = true;
                    break;
            }
        }

        public void DisplayPlayers(
          int playerCount,
          int[] playerId,
          int[] helperId,
          int[] parentId,
          int[] stateOwnerId,
          string[] name)
        {
            if (!DebugForm.IsWindowVisible(this.Handle))
                return;
            if (this._playerId == null)
            {
                this._playerId = new int[playerCount];
                if (this._playerId == null)
                    return;
            }
            if (this._parentId == null)
            {
                this._parentId = new int[playerCount];
                if (this._parentId == null)
                    return;
            }
            if (this._stateOwnerId == null)
            {
                this._stateOwnerId = new int[playerCount];
                if (this._stateOwnerId == null)
                    return;
            }
            if (this.playerListView.Items.Count < playerCount)
                return;
            if (playerId[0] == -1)
            {
                if (this.playerListView.ForeColor != Color.Gray)
                    this.playerListView.ForeColor = Color.Gray;
                playerId.CopyTo((Array)this._playerId, 0);
            }
            else
            {
                if (this.playerListView.ForeColor != Control.DefaultForeColor)
                    this.playerListView.ForeColor = Control.DefaultForeColor;
                for (int index = 0; index < playerCount; ++index)
                {
                    if (playerId[index] != this._playerId[index] || parentId[index] != this._parentId[index] || (stateOwnerId[index] != this._stateOwnerId[index] || this._isDirty))
                    {
                        if (stateOwnerId[index] != -1)
                            this.playerListView.Items[index].ForeColor = Color.Red;
                        else
                            this.playerListView.Items[index].ForeColor = Control.DefaultForeColor;
                        this.playerListView.Items[index].SubItems[1].Text = playerId[index] == -1 ? "-" : playerId[index].ToString();
                        this.playerListView.Items[index].SubItems[2].Text = helperId[index] == -1 ? "-" : helperId[index].ToString();
                        this.playerListView.Items[index].SubItems[3].Text = parentId[index] == -1 ? "-" : parentId[index].ToString();
                        this.playerListView.Items[index].SubItems[4].Text = name[index];
                    }
                }
                this._isDirty = false;
                playerId.CopyTo((Array)this._playerId, 0);
                parentId.CopyTo((Array)this._parentId, 0);
                stateOwnerId.CopyTo((Array)this._stateOwnerId, 0);
            }
        }

        public void DisplayExplods(
          int playerId,
          int headNo,
          int explodCount,
          int[] explodId,
          int[] anim,
          int[] ownerId)
        {
            if (!DebugForm.IsWindowVisible(this.Handle))
                return;
            if (this._explodId == null)
            {
                this._explodId = new int[explodCount];
                if (this._explodId == null)
                    return;
            }
            if (this._anim == null)
            {
                this._anim = new int[explodCount];
                if (this._anim == null)
                    return;
            }
            if (this._ownerId == null)
            {
                this._ownerId = new int[explodCount];
                if (this._ownerId == null)
                    return;
            }
            if (this.explodListView.Items.Count < explodCount)
                return;
            if (playerId <= 0)
            {
                if (!(this.explodListView.ForeColor != Color.Gray))
                    return;
                this.explodListView.ForeColor = Color.Gray;
            }
            else
            {
                if (this.explodListView.ForeColor != Control.DefaultForeColor)
                    this.explodListView.ForeColor = Control.DefaultForeColor;
                for (int index = 0; index < explodCount; ++index)
                {
                    if (explodId[index] != this._explodId[index] || ownerId[index] != this._ownerId[index] || (anim[index] != this._anim[index] || this._isExplodListDirty))
                    {
                        if (this._isExplodListDirty)
                            this.explodListView.Items[index].SubItems[0].Text = (headNo + index).ToString();
                        this.explodListView.Items[index].SubItems[1].Text = explodId[index] == -1 ? "" : explodId[index].ToString();
                        this.explodListView.Items[index].SubItems[2].Text = anim[index] == -1 ? "" : anim[index].ToString();
                        this.explodListView.Items[index].SubItems[3].Text = ownerId[index] == -1 ? "" : ownerId[index].ToString();
                    }
                }
                this._isExplodListDirty = false;
                explodId.CopyTo((Array)this._explodId, 0);
                anim.CopyTo((Array)this._anim, 0);
                ownerId.CopyTo((Array)this._ownerId, 0);
            }
        }

        public void DisplayProjs(
          int playerId,
          int headNo,
          int projCount,
          int[] projId,
          int[] projX,
          int[] projY)
        {
            if (!DebugForm.IsWindowVisible(this.Handle))
                return;
            if (this._projId == null)
            {
                this._projId = new int[projCount];
                if (this._projId == null)
                    return;
            }
            if (this._projX == null)
            {
                this._projX = new int[projCount];
                if (this._projX == null)
                    return;
            }
            if (this._projY == null)
            {
                this._projY = new int[projCount];
                if (this._projY == null)
                    return;
            }
            if (this.projListView.Items.Count < projCount)
                return;
            if (playerId <= 0)
            {
                if (!(this.projListView.ForeColor != Color.Gray))
                    return;
                this.projListView.ForeColor = Color.Gray;
            }
            else
            {
                if (this.projListView.ForeColor != Control.DefaultForeColor)
                    this.projListView.ForeColor = Control.DefaultForeColor;
                for (int index = 0; index < projCount; ++index)
                {
                    if (projId[index] != this._projId[index] || projX[index] != this._projX[index] || (projY[index] != this._projY[index] || this._isProjListDirty))
                    {
                        if (this._isProjListDirty)
                            this.projListView.Items[index].SubItems[0].Text = (headNo + index).ToString();
                        if (projId[index] != -1)
                        {
                            this.projListView.Items[index].SubItems[1].Text = projId[index].ToString();
                            this.projListView.Items[index].SubItems[2].Text = projX[index].ToString();
                            this.projListView.Items[index].SubItems[3].Text = projY[index].ToString();
                        }
                        else
                        {
                            this.projListView.Items[index].SubItems[1].Text = "";
                            this.projListView.Items[index].SubItems[2].Text = "";
                            this.projListView.Items[index].SubItems[3].Text = "";
                        }
                    }
                }
                this._isProjListDirty = false;
                projId.CopyTo((Array)this._projId, 0);
                projX.CopyTo((Array)this._projX, 0);
                projY.CopyTo((Array)this._projY, 0);
            }
        }

        private void DebugForm_Load(object sender, EventArgs e)
        {
            this.debugColorList.DropDownStyle = ComboBoxStyle.DropDownList;
            this.debugColorList.Items.Add("Default");
            this.debugColorList.Items.Add("Yellow");
            this.debugColorList.Items.Add("Purple");
            this.debugColorList.Items.Add("Red");
            this.debugColorList.Items.Add("Black");
            this.debugColorList.Items.Add("Green");
            this.debugColorList.Items.Add("Custom");
            this.debugColorList.SelectedIndex = 0;
            if (this.debugModeCheckBox.Checked)
            {
                this.playerListView.ForeColor = Control.DefaultForeColor;
                this.explodListView.ForeColor = Control.DefaultForeColor;
                this.projListView.ForeColor = Control.DefaultForeColor;
                this.explodHeadNumericUpDown.Enabled = true;
                this.projHeadNumericUpDown.Enabled = true;
            }
            else
            {
                this.playerListView.ForeColor = Color.Gray;
                this.explodListView.ForeColor = Color.Gray;
                this.projListView.ForeColor = Color.Gray;
                this.explodHeadNumericUpDown.Enabled = false;
                this.projHeadNumericUpDown.Enabled = false;
            }
            VarForm.MainObj().SetDebugMode(this.debugModeCheckBox.Checked);
            this.stepTimeComboBox.SelectedIndex = 1;
            this.skipFrameComboBox.Text = "30";
            this.p1RadioButton.Checked = true;
            this._defaultBgColor = this.checkModeStateText.BackColor;
            this._defaultFgColor = this.checkModeStateText.ForeColor;
            this.LoadProfile();
        }

        private void DebugForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason != CloseReason.UserClosing)
                return;
            e.Cancel = true;
            this.Hide();
        }

        private void pauseCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            Control pauseCheckBox = (Control)this.pauseCheckBox;
            if (pauseCheckBox != null && pauseCheckBox.Cursor == Cursors.Help)
            {
                this.pauseCheckBox.CheckedChanged -= new EventHandler(this.pauseCheckBox_CheckedChanged);
                this.pauseCheckBox.Checked = !this.pauseCheckBox.Checked;
                this.pauseCheckBox.CheckedChanged += new EventHandler(this.pauseCheckBox_CheckedChanged);
                string text = "Pause/Unpause MUGEN" + Environment.NewLine + "(Same as Pause)" + Environment.NewLine;
                this.toolTip1.Hide((IWin32Window)pauseCheckBox);
                this.toolTip1.IsBalloon = false;
                this.toolTip1.IsBalloon = true;
                this.toolTip1.SetToolTip(pauseCheckBox, " ");
                this.toolTip1.Show(text, (IWin32Window)pauseCheckBox, pauseCheckBox.Width / 2, pauseCheckBox.Height / 2);
            }
            else
            {
                MugenWindow.MainObj().SetPaused(this.pauseCheckBox.Checked);
                this._SetStepInterval();
                if (this.pauseCheckBox.Checked)
                    return;
                MugenWindow.MainObj().Activate();
            }
        }

        private void stepButton_Click(object sender, EventArgs e)
        {
            Control stepButton = (Control)this.stepButton;
            if (stepButton != null && stepButton.Cursor == Cursors.Help)
            {
                string text = "Framestep" + Environment.NewLine + "(Only while paused)" + Environment.NewLine;
                this.toolTip1.Hide((IWin32Window)stepButton);
                this.toolTip1.IsBalloon = false;
                this.toolTip1.IsBalloon = true;
                this.toolTip1.SetToolTip(stepButton, " ");
                this.toolTip1.Show(text, (IWin32Window)stepButton, stepButton.Width / 2, stepButton.Height / 2);
            }
            else
                MugenWindow.MainObj().InjectStepCommand();
        }

        private void autoStepCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            Control autoStepCheckBox = (Control)this.autoStepCheckBox;
            if (autoStepCheckBox != null && autoStepCheckBox.Cursor == Cursors.Help)
            {
                this.autoStepCheckBox.CheckedChanged -= new EventHandler(this.autoStepCheckBox_CheckedChanged);
                this.autoStepCheckBox.Checked = !this.autoStepCheckBox.Checked;
                this.autoStepCheckBox.CheckedChanged += new EventHandler(this.autoStepCheckBox_CheckedChanged);
                string text = "Framestep at 0.1~1.0 second intervals" + Environment.NewLine + "(Only while paused)" + Environment.NewLine;
                this.toolTip1.Hide((IWin32Window)autoStepCheckBox);
                this.toolTip1.IsBalloon = false;
                this.toolTip1.IsBalloon = true;
                this.toolTip1.SetToolTip(autoStepCheckBox, " ");
                this.toolTip1.Show(text, (IWin32Window)autoStepCheckBox, autoStepCheckBox.Width / 2, autoStepCheckBox.Height / 2);
            }
            else
            {
                MugenWindow.MainObj().SetStepMode(this.autoStepCheckBox.Checked);
                this._SetStepInterval();
            }
        }

        private void _SetStepInterval()
        {
            float result;
            if (float.TryParse(this.stepTimeComboBox.Text, out result))
            {
                MugenWindow.MainObj().SetStepInterval((long)((double)result * 1000.0));
            }
            else
            {
                this.stepTimeComboBox.Text = "0.5";
                MugenWindow.MainObj().SetStepInterval(500L);
            }
        }

        private void stepTimeComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            this._SetStepInterval();
            if (this.pauseCheckBox.Checked)
                return;
            MugenWindow.MainObj().Activate();
        }

        private void _SetSkipFrames()
        {
            int result;
            if (int.TryParse(this.skipFrameComboBox.Text, out result))
            {
                MugenWindow.MainObj().SetSkipFrames(result);
            }
            else
            {
                this.skipFrameComboBox.Text = "30";
                MugenWindow.MainObj().SetSkipFrames(30);
            }
        }

        private void skipFrameComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            this._SetSkipFrames();
            if (this.pauseCheckBox.Checked)
                return;
            MugenWindow.MainObj().Activate();
        }

        private void speedModeCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            Control speedModeCheckBox = (Control)this.speedModeCheckBox;
            if (speedModeCheckBox != null && speedModeCheckBox.Cursor == Cursors.Help)
            {
                this.speedModeCheckBox.CheckedChanged -= new EventHandler(this.speedModeCheckBox_CheckedChanged);
                this.speedModeCheckBox.Checked = !this.speedModeCheckBox.Checked;
                this.speedModeCheckBox.CheckedChanged += new EventHandler(this.speedModeCheckBox_CheckedChanged);
                string text = "High Speed Mode" + Environment.NewLine + "(Same as Ctrl+S)" + Environment.NewLine;
                this.toolTip1.Hide((IWin32Window)speedModeCheckBox);
                this.toolTip1.IsBalloon = false;
                this.toolTip1.IsBalloon = true;
                this.toolTip1.SetToolTip(speedModeCheckBox, " ");
                this.toolTip1.Show(text, (IWin32Window)speedModeCheckBox, speedModeCheckBox.Width / 2, speedModeCheckBox.Height / 2);
            }
            else
            {
                MugenWindow.MainObj().SetSpeedMode(this.speedModeCheckBox.Checked);
                if (this.pauseCheckBox.Checked)
                    return;
                MugenWindow.MainObj().Activate();
            }
        }

        private void skipModeCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            Control skipModeCheckBox = (Control)this.skipModeCheckBox;
            if (skipModeCheckBox != null && skipModeCheckBox.Cursor == Cursors.Help)
            {
                this.skipModeCheckBox.CheckedChanged -= new EventHandler(this.skipModeCheckBox_CheckedChanged);
                this.skipModeCheckBox.Checked = !this.skipModeCheckBox.Checked;
                this.skipModeCheckBox.CheckedChanged += new EventHandler(this.skipModeCheckBox_CheckedChanged);
                string text = "Speed up MUGEN by skipping frames." + Environment.NewLine + "You can shorten the game time further by combining with High Speed Mode" + Environment.NewLine;
                this.toolTip1.Hide((IWin32Window)skipModeCheckBox);
                this.toolTip1.IsBalloon = false;
                this.toolTip1.IsBalloon = true;
                this.toolTip1.SetToolTip(skipModeCheckBox, " ");
                this.toolTip1.Show(text, (IWin32Window)skipModeCheckBox, skipModeCheckBox.Width / 2, skipModeCheckBox.Height / 2);
            }
            else
            {
                MugenWindow.MainObj().SetSkipMode(this.skipModeCheckBox.Checked);
                if (this.pauseCheckBox.Checked)
                    return;
                MugenWindow.MainObj().Activate();
            }
        }

        private void _debugModeCheckBox_CheckedChanged()
        {
            if (this.debugModeCheckBox.Checked)
            {
                this._isDirty = true;
                this.playerListView.ForeColor = Control.DefaultForeColor;
                this.explodListView.ForeColor = Control.DefaultForeColor;
                this.projListView.ForeColor = Control.DefaultForeColor;
                this.explodHeadNumericUpDown.Enabled = true;
                this.projHeadNumericUpDown.Enabled = true;
            }
            else
            {
                this.playerListView.ForeColor = Color.Gray;
                this.explodListView.ForeColor = Color.Gray;
                this.projListView.ForeColor = Color.Gray;
                this.explodHeadNumericUpDown.Enabled = false;
                this.projHeadNumericUpDown.Enabled = false;
            }
            VarForm.MainObj().SetDebugMode(this.debugModeCheckBox.Checked);
            MugenWindow.MainObj().SetDebugMode(this.debugModeCheckBox.Checked, false);
        }

        private void debugModeCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            Control debugModeCheckBox = (Control)this.debugModeCheckBox;
            if (debugModeCheckBox != null && debugModeCheckBox.Cursor == Cursors.Help)
            {
                this.debugModeCheckBox.CheckedChanged -= new EventHandler(this.debugModeCheckBox_CheckedChanged);
                this.debugModeCheckBox.Checked = !this.debugModeCheckBox.Checked;
                this.debugModeCheckBox.CheckedChanged += new EventHandler(this.debugModeCheckBox_CheckedChanged);
                string text = "Debug Display Toggle" + Environment.NewLine;
                this.toolTip1.Hide((IWin32Window)debugModeCheckBox);
                this.toolTip1.IsBalloon = false;
                this.toolTip1.IsBalloon = true;
                this.toolTip1.SetToolTip(debugModeCheckBox, " ");
                this.toolTip1.Show(text, (IWin32Window)debugModeCheckBox, debugModeCheckBox.Width / 2, debugModeCheckBox.Height / 2);
            }
            else
            {
                this._debugModeCheckBox_CheckedChanged();
                if (this.pauseCheckBox.Checked || this != Form.ActiveForm)
                    return;
                MugenWindow.MainObj().Activate();
            }
        }

        private void grayBGcheckBox_CheckedChanged(object sender, EventArgs e)
        {
        }

        private void radioButton_MouseClick(object sender, MouseEventArgs e)
        {
            if (sender == null || !(((Control)sender).Cursor == Cursors.Help))
                return;
            string text = string.Format("Return the debug text color to {0}.\n(Note: When combined with the AllPalFX SCTRL,\n\tthis may become invalidated.)\n(Note: This changes the background color instead of the letter color in 1.0)\n", (object)((Control)sender).Text);
            this.toolTip1.Hide((IWin32Window)sender);
            this.toolTip1.IsBalloon = false;
            this.toolTip1.IsBalloon = true;
            this.toolTip1.SetToolTip((Control)sender, " ");
            this.toolTip1.Show(text, (IWin32Window)sender, ((Control)sender).Width / 2, ((Control)sender).Height / 2);
        }

        private void defaultRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            this.debugColorCustomRed.Enabled = false;
            this.debugColorCustomBlue.Enabled = false;
            this.debugColorCustomGreen.Enabled = false;
            ComboBox debugColorList = (ComboBox)this.debugColorList;
            if (debugColorList != null && debugColorList.Cursor == Cursors.Help)
            {
                MugenWindow.MainObj().SetDebugColor(DebugColor.NONE);
            }
            else
            {
                switch (debugColorList.SelectedItem)
                {
                    case "White":
                        MugenWindow.MainObj().SetDebugColor(DebugColor.WHITE);
                        break;
                    case "Yellow":
                        MugenWindow.MainObj().SetDebugColor(DebugColor.YELLOW);
                        break;
                    case "Purple":
                        MugenWindow.MainObj().SetDebugColor(DebugColor.PURPLE);
                        break;
                    case "Red":
                        MugenWindow.MainObj().SetDebugColor(DebugColor.RED);
                        break;
                    case "Black":
                        MugenWindow.MainObj().SetDebugColor(DebugColor.BLACK);
                        break;
                    case "Green":
                        MugenWindow.MainObj().SetDebugColor(DebugColor.GREEN);
                        break;
                    case "Custom":
                        this.debugColorCustomRed.Enabled = true;
                        this.debugColorCustomBlue.Enabled = true;
                        this.debugColorCustomGreen.Enabled = true;
                        MugenWindow.MainObj().SetDebugColor(DebugColor.CUSTOM);
                        break;
                    case "Default":
                    default:
                        MugenWindow.MainObj().SetDebugColor(DebugColor.NONE);
                        break;
                }
                if (this.pauseCheckBox.Checked)
                    return;
                MugenWindow.MainObj().Activate();
            }
        }

        private void playerListView_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.playerListView.SelectedIndices.Count <= 0)
            {
                this.playerListView.Items[this._selectedIndex].BackColor = this.playerListView.BackColor;
            }
            else
            {
                int selectedIndex = this.playerListView.SelectedIndices[0];
                if (this._isRightMouseButton_OnPlayerList)
                {
                    this.playerListView.Items[selectedIndex].Selected = false;
                    this.playerListView.Items[this._selectedIndex].BackColor = Color.Gray;
                    this._isRightMouseButton_OnPlayerList = false;
                }
                else
                {
                    this.playerListView.Items[this._selectedIndex].BackColor = this.playerListView.BackColor;
                    this._selectedIndex = selectedIndex;
                    if (!this.ctrlDhandleToolStripMenuItem1.Checked)
                        MugenWindow.MainObj().SetDebugTargetNo(selectedIndex + 1);
                    MugenWindow.MainObj().SetVarInspectTargetNo(selectedIndex + 1);
                    this.playerListView.Items[this._selectedIndex].BackColor = Color.Gray;
                }
            }
        }

        private void explodListView_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.explodListView.SelectedIndices.Count <= 0)
                return;
            int selectedIndex = this.explodListView.SelectedIndices[0];
            if (this._isRightMouseButton_OnExplodList)
            {
                this.explodListView.Items[selectedIndex].Selected = false;
                this._isRightMouseButton_OnExplodList = false;
            }
            else
            {
                int result = 0;
                int.TryParse(this.explodListView.Items[selectedIndex].SubItems[3].Text, out result);
                if (result <= 0)
                    return;
                if (!this.ctrlDhandleToolStripMenuItem1.Checked)
                    MugenWindow.MainObj().SetDebugTargetPlayer(result);
                MugenWindow.MainObj().SetVarInspectTargetPlayer(result);
            }
        }

        private void projListView_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.projListView.SelectedIndices.Count <= 0)
                return;
            int selectedIndex = this.projListView.SelectedIndices[0];
            if (!this._isRightMouseButton_OnProjList)
                return;
            this.projListView.Items[selectedIndex].Selected = false;
            this._isRightMouseButton_OnProjList = false;
        }

        private void playerListView_MouseUp(object sender, MouseEventArgs e)
        {
            this._isRightMouseButton_OnPlayerList = false;
            if (this.playerListView.Cursor == Cursors.Help || e.Button == MouseButtons.Right || DebugForm.MainObj().IsPaused())
                return;
            MugenWindow.MainObj().Activate();
        }

        private void explodListView_MouseUp(object sender, MouseEventArgs e)
        {
            this._isRightMouseButton_OnExplodList = false;
            if (this.explodListView.Cursor == Cursors.Help || e.Button == MouseButtons.Right)
                return;
            if (this.explodListView.SelectedIndices.Count > 0)
                this.explodListView.Items[this.explodListView.SelectedIndices[0]].Selected = false;
            if (DebugForm.MainObj().IsPaused())
                return;
            MugenWindow.MainObj().Activate();
        }

        private void projListView_MouseUp(object sender, MouseEventArgs e)
        {
            this._isRightMouseButton_OnProjList = false;
            if (this.projListView.Cursor == Cursors.Help || e.Button == MouseButtons.Right)
                return;
            if (this.projListView.SelectedIndices.Count > 0)
                this.projListView.Items[this.projListView.SelectedIndices[0]].Selected = false;
            if (DebugForm.MainObj().IsPaused())
                return;
            MugenWindow.MainObj().Activate();
        }

        private void DebugForm_Activated(object sender, EventArgs e)
        {
        }

        private void DebugForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (this.Cursor == Cursors.Help || e.KeyValue != 19)
                return;
            this.TogglePauseCheckBox();
            e.Handled = true;
        }

        private void DebugForm_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (this.Cursor == Cursors.Help)
                return;
            if (e.KeyChar == '\x0004')
            {
                this.ShiftDebugTargetNo();
                e.Handled = true;
            }
            else if (e.KeyChar == '\x0003')
            {
                switch (this.listTabControl.SelectedIndex)
                {
                    case 0:
                        this._copyAll();
                        break;
                    case 1:
                        this._copyAllExplod();
                        break;
                    case 2:
                        this._copyAllProj();
                        break;
                }
                e.Handled = true;
            }
            else if (e.KeyChar == '\x0016')
            {
                this.projToolStripMenuItem2Handle();
                e.Handled = true;
            }
            else if (e.KeyChar == '\x001A')
            {
                this.ctrlDhandleToolStripMenuItem1.Checked = !this.ctrlDhandleToolStripMenuItem1.Checked;
                this._ctrlDhandle();
                e.Handled = true;
            }
            else if (e.KeyChar == '\x0013')
            {
                this.ToggleSpeedModeCheckBox();
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
            if (this.playerListView.Items.Count == 0 || this.playerListView.ForeColor != Control.DefaultForeColor)
                return;
            string text = "No.\tplayerid\thelperid\tparentid\tname" + Environment.NewLine;
            for (int index = 0; index < this.playerListView.Items.Count; ++index)
                text = text + this.playerListView.Items[index].SubItems[0].Text + "\t" + this.playerListView.Items[index].SubItems[1].Text + "\t" + this.playerListView.Items[index].SubItems[2].Text + "\t" + this.playerListView.Items[index].SubItems[3].Text + "\t" + this.playerListView.Items[index].SubItems[4].Text + Environment.NewLine;
            Clipboard.SetText(text);
            this.toolTip1.Hide((IWin32Window)this.playerListView);
            this.toolTip1.Show("Copied the helper list to the clipboard.", (IWin32Window)this.playerListView, 0, -20, 1000);
        }

        private void _copyAllExplod()
        {
            if (this.explodListView.Items.Count == 0 || this.explodListView.ForeColor != Control.DefaultForeColor)
                return;
            string text = "No.\tid\tanim\townerid" + Environment.NewLine;
            for (int index = 0; index < this.explodListView.Items.Count; ++index)
                text = text + this.explodListView.Items[index].SubItems[0].Text + "\t" + this.explodListView.Items[index].SubItems[1].Text + "\t" + this.explodListView.Items[index].SubItems[2].Text + "\t" + this.explodListView.Items[index].SubItems[3].Text + Environment.NewLine;
            Clipboard.SetText(text);
            this.toolTip1.Hide((IWin32Window)this.explodListView);
            this.toolTip1.Show("Copied the explod list to the clipboard.", (IWin32Window)this.explodListView, 0, -20, 1000);
        }

        private void _copyAllProj()
        {
            if (this.projListView.Items.Count == 0 || this.projListView.ForeColor != Control.DefaultForeColor)
                return;
            string text = "No.\tprojid\tx\ty" + Environment.NewLine;
            for (int index = 0; index < this.explodListView.Items.Count; ++index)
                text = text + this.projListView.Items[index].SubItems[0].Text + "\t" + this.projListView.Items[index].SubItems[1].Text + "\t" + this.projListView.Items[index].SubItems[2].Text + "\t" + this.projListView.Items[index].SubItems[3].Text + Environment.NewLine;
            Clipboard.SetText(text);
            this.toolTip1.Hide((IWin32Window)this.projListView);
            this.toolTip1.Show("Copied the projectile list to the clipboard.", (IWin32Window)this.projListView, 0, -20, 1000);
        }

        private void copyAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Control playerListView = (Control)this.playerListView;
            if (playerListView != null && playerListView.Cursor == Cursors.Help)
            {
                this.playerListView.Show();
                string text = "Copies the entire player list in table form to the clipboard." + Environment.NewLine + "(The data is pre-formatted for use in programs such as Excel.)" + Environment.NewLine;
                this.toolTip1.Hide((IWin32Window)playerListView);
                this.toolTip1.IsBalloon = false;
                this.toolTip1.IsBalloon = true;
                this.toolTip1.SetToolTip(playerListView, " ");
                this.toolTip1.Show(text, (IWin32Window)playerListView, this.ctrlDhandleToolStripMenuItem1.Width / 2, this.ctrlDhandleToolStripMenuItem1.Height * 3 / 2);
            }
            else
                this._copyAll();
        }

        private void _ctrlDhandle()
        {
            if (this.playerListView.Items.Count <= 0)
                return;
            if (this.ctrlDhandleToolStripMenuItem1.Checked)
            {
                this.toolTip1.Hide((IWin32Window)this.playerListView);
                this.toolTip1.Show("Disabled sync with mugen's debug display.　　　　　　　", (IWin32Window)this.playerListView, 0, -20, 1000);
                this.Activate();
            }
            else
            {
                this.toolTip1.Hide((IWin32Window)this.playerListView);
                this.toolTip1.Show("Resumed sync with mugen's debug display.　　　　　　　", (IWin32Window)this.playerListView, 0, -20, 1000);
                if (this.playerListView.SelectedIndices.Count <= 0)
                {
                    this.playerListView.Items[this._selectedIndex].BackColor = this.playerListView.BackColor;
                }
                else
                {
                    int selectedIndex = this.playerListView.SelectedIndices[0];
                    this._selectedIndex = selectedIndex;
                    MugenWindow.MainObj().SetDebugTargetNo(selectedIndex + 1);
                    MugenWindow.MainObj().SetVarInspectTargetNo(selectedIndex + 1);
                    this.playerListView.Items[this._selectedIndex].BackColor = Color.Gray;
                }
            }
        }

        private void ctrlDhandleToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Control contextMenuStrip = (Control)this.debugContextMenuStrip;
            if (contextMenuStrip != null && contextMenuStrip.Cursor == Cursors.Help)
            {
                this.debugContextMenuStrip.Show();
                string text = "Normally the selection of items in this list would be in sync with mugen's debug display, but it is possible to disable this behavior." + Environment.NewLine + "By disabling this feature, you can choose to view the variables of one player in the Variable Display window while keeping another \nplayer's data in MUGEN's debug display." + Environment.NewLine;
                this.toolTip1.Hide((IWin32Window)contextMenuStrip);
                this.toolTip1.IsBalloon = false;
                this.toolTip1.IsBalloon = true;
                this.toolTip1.SetToolTip(contextMenuStrip, " ");
                this.toolTip1.Show(text, (IWin32Window)contextMenuStrip, this.ctrlDhandleToolStripMenuItem1.Width / 2, this.ctrlDhandleToolStripMenuItem1.Height / 2);
            }
            else
                this._ctrlDhandle();
        }

        private void playerListView_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            if (this.playerListView.Cursor == Cursors.Help && e.Column > 0)
            {
                this.toolTip1.Hide((IWin32Window)this);
                this.toolTip1.IsBalloon = false;
                this.toolTip1.IsBalloon = true;
                string text = "By double clicking this tab you can adjust the cell size to fit the displayed data." + Environment.NewLine + "(By clicking it once more you can reset the size to default.)" + Environment.NewLine;
                this.toolTip1.SetToolTip((Control)this.playerListView, " ");
                int num = 0;
                for (int index = 0; index < e.Column; ++index)
                    num += this.playerListView.Columns[index].Width;
                this.toolTip1.Show(text, (IWin32Window)this.playerListView, num + this.playerListView.Columns[e.Column].Width / 2, 10);
            }
            else
            {
                if (e.Column <= 0 || e.Column > 4)
                    return;
                if (!this._columState[e.Column - 1])
                {
                    this._columState[e.Column - 1] = true;
                    this.playerListView.Columns[e.Column].Width += 40;
                }
                else
                {
                    this._columState[e.Column - 1] = false;
                    this.playerListView.Columns[e.Column].Width -= 40;
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
                this.playerListView.Cursor = Cursors.Help;
                this.explodListView.Cursor = Cursors.Help;
                this.explodHeadNumericUpDown.Cursor = Cursors.Help;
                this.projListView.Cursor = Cursors.Help;
                this.projHeadNumericUpDown.Cursor = Cursors.Help;
                this.debugContextMenuStrip.Cursor = Cursors.Help;
                this.explodContextMenuStrip.Cursor = Cursors.Help;
                this.projContextMenuStrip.Cursor = Cursors.Help;
                this.pauseCheckBox.Cursor = Cursors.Help;
                this.stepButton.Cursor = Cursors.Help;
                this.autoStepCheckBox.Cursor = Cursors.Help;
                this.speedModeCheckBox.Cursor = Cursors.Help;
                this.skipModeCheckBox.Cursor = Cursors.Help;
                this.debugModeCheckBox.Cursor = Cursors.Help;
                this.label1.Cursor = Cursors.Help;
                this.debugColorList.Cursor = Cursors.Help;
                this.checkModeStateText.Cursor = Cursors.Help;
                this.triggerCheckStartButton.Cursor = Cursors.Help;
                this.triggerCheckResumeButton.Cursor = Cursors.Help;
                this.triggerCheckStopButton.Cursor = Cursors.Help;
                this.label6.Cursor = Cursors.Help;
                this.label7.Cursor = Cursors.Help;
                this.label8.Cursor = Cursors.Help;
                this.playerComboBox.Cursor = Cursors.Help;
                this.triggerComboBox.Cursor = Cursors.Help;
                this.valueComboBox.Cursor = Cursors.Help;
                this.setButton.Cursor = Cursors.Help;
                this.revertButton.Cursor = Cursors.Help;
            }
            else
            {
                this.Cursor = Cursors.Default;
                this.playerListView.Cursor = Cursors.Default;
                this.explodListView.Cursor = Cursors.Default;
                this.explodHeadNumericUpDown.Cursor = Cursors.Default;
                this.projListView.Cursor = Cursors.Default;
                this.projHeadNumericUpDown.Cursor = Cursors.Default;
                this.debugContextMenuStrip.Cursor = Cursors.Default;
                this.explodContextMenuStrip.Cursor = Cursors.Default;
                this.projContextMenuStrip.Cursor = Cursors.Default;
                this.pauseCheckBox.Cursor = Cursors.Default;
                this.stepButton.Cursor = Cursors.Default;
                this.autoStepCheckBox.Cursor = Cursors.Default;
                this.speedModeCheckBox.Cursor = Cursors.Default;
                this.skipModeCheckBox.Cursor = Cursors.Default;
                this.debugModeCheckBox.Cursor = Cursors.Default;
                this.label1.Cursor = Cursors.Default;
                this.debugColorList.Cursor = Cursors.Default;
                this.checkModeStateText.Cursor = Cursors.Default;
                this.triggerCheckStartButton.Cursor = Cursors.Default;
                this.triggerCheckResumeButton.Cursor = Cursors.Default;
                this.triggerCheckStopButton.Cursor = Cursors.Default;
                this.label6.Cursor = Cursors.Default;
                this.label7.Cursor = Cursors.Default;
                this.label8.Cursor = Cursors.Default;
                this.playerComboBox.Cursor = Cursors.Default;
                this.triggerComboBox.Cursor = Cursors.Default;
                this.valueComboBox.Cursor = Cursors.Default;
                this.setButton.Cursor = Cursors.Default;
                this.revertButton.Cursor = Cursors.Default;
            }
        }

        private void DebugForm_Deactivate(object sender, EventArgs e)
        {
            if (!(this.Cursor == Cursors.Help))
                return;
            this.toolTip1.Hide((IWin32Window)this);
        }

        private void playerListView_DisplayHelp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                if (!(this.playerListView.Cursor == Cursors.Help))
                    return;
                this.toolTip1.Hide((IWin32Window)this);
            }
            else
            {
                if (e.Button != MouseButtons.Left || !(this.playerListView.Cursor == Cursors.Help))
                    return;
                this.toolTip1.Hide((IWin32Window)this);
                this.toolTip1.IsBalloon = false;
                this.toolTip1.IsBalloon = true;
                string text = "Displays the list of active players and helpers." + Environment.NewLine + Environment.NewLine + "Data information:" + Environment.NewLine + "- playerid: Displays the unique player ID of the object." + Environment.NewLine + "- helperid: Displays the helper ID specified in its state controller." + Environment.NewLine + "- Owner id: The player ID of the helper's owner." + Environment.NewLine + "- name: The character name that is checked by the name trigger. This coincides for players and helpers." + Environment.NewLine + Environment.NewLine + "After clicking one of the objects displayed on this list, its variables will be displayed in Variable Display window." + Environment.NewLine + "(This will also adjust mugen's debug display accordingly)" + Environment.NewLine + Environment.NewLine + "By the same token, changing the object in mugen's debug display will also change the highlighted object accordingly." + Environment.NewLine + "If the cell sizes are too small for the data to be displayed, you can expand it by double clicking the parameter cell at the top." + Environment.NewLine + "(You can also restore the sizes to default by clicking once more.)" + Environment.NewLine + Environment.NewLine + "Finally, more tools are available if you right click on one of the items.";
                this.toolTip1.SetToolTip((Control)this.playerListView, " ");
                this.toolTip1.Show(text, (IWin32Window)this.playerListView, this.playerListView.Width / 2, this.playerListView.Height / 2);
            }
        }

        private void explodListView_DisplayHelp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                if (!(this.explodListView.Cursor == Cursors.Help))
                    return;
                this.toolTip1.Hide((IWin32Window)this);
            }
            else
            {
                if (e.Button != MouseButtons.Left || !(this.explodListView.Cursor == Cursors.Help))
                    return;
                this.toolTip1.Hide((IWin32Window)this);
                this.toolTip1.IsBalloon = false;
                this.toolTip1.IsBalloon = true;
                string text = "Displays the list of active explods." + Environment.NewLine + Environment.NewLine + "Data information:" + Environment.NewLine + "- id: The explod ID given in its state controller." + Environment.NewLine + "- anim: The animation number." + Environment.NewLine + "- Onwer's Player ID: The player ID of the player or helper that spawned the explod." + Environment.NewLine + Environment.NewLine + "After clicking one of the explods displayed on this list, the variables of the owner will be displayed in Variable Display window." + Environment.NewLine + "(This will also adjust mugen's debug display accordingly)" + Environment.NewLine + Environment.NewLine + "If the cell sizes are too small for the data to be displayed, you can expand it by double clicking the parameter cell at the top." + Environment.NewLine + "(You can also restore the sizes to default by clicking once more.)" + Environment.NewLine + Environment.NewLine + "Finally, more tools are available if you right click on one of the items.";
                this.toolTip1.SetToolTip((Control)this.explodListView, " ");
                this.toolTip1.Show(text, (IWin32Window)this.explodListView, this.explodListView.Width / 2, this.explodListView.Height / 2);
            }
        }

        private void projListView_DisplayHelp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                if (!(this.projListView.Cursor == Cursors.Help))
                    return;
                this.toolTip1.Hide((IWin32Window)this);
            }
            else
            {
                if (e.Button != MouseButtons.Left || !(this.projListView.Cursor == Cursors.Help))
                    return;
                this.toolTip1.Hide((IWin32Window)this);
                this.toolTip1.IsBalloon = false;
                this.toolTip1.IsBalloon = true;
                string text = "Displays the list of active projectiles." + Environment.NewLine + Environment.NewLine + "Data information:" + Environment.NewLine + "- projid: The projectile ID given in its state controller." + Environment.NewLine + "- x coordinate: The horizontal position relative to the left side of the screen." + Environment.NewLine + "         (This value is displayed in the same way as the screenpos trigger would.)" + Environment.NewLine + "- y coordinate: The vertical position of the projectile, relative to the top of the screen." + Environment.NewLine + "          (This value is displayed in the same way as the screenpos trigger would.)" + Environment.NewLine + Environment.NewLine + "If the cell sizes are too small for the data to be displayed, you can expand it by double clicking the parameter cell at the top." + Environment.NewLine + "(You can also restore the sizes to default by clicking once more.)" + Environment.NewLine + Environment.NewLine + "Finally, more tools are available if you right click on one of the items.";
                this.toolTip1.SetToolTip((Control)this.projListView, " ");
                this.toolTip1.Show(text, (IWin32Window)this.projListView, this.projListView.Width / 2, this.projListView.Height / 2);
            }
        }

        private void playerListView_MouseClick(object sender, MouseEventArgs e) => this.playerListView_DisplayHelp(sender, e);

        private void playerListView_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
                this._isRightMouseButton_OnPlayerList = true;
            this.playerListView_DisplayHelp(sender, e);
        }

        private void explodListView_MouseClick(object sender, MouseEventArgs e) => this.explodListView_DisplayHelp(sender, e);

        private void explodListView_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
                this._isRightMouseButton_OnExplodList = true;
            this.explodListView_DisplayHelp(sender, e);
        }

        private void projListView_MouseClick(object sender, MouseEventArgs e) => this.projListView_DisplayHelp(sender, e);

        private void projListView_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
                this._isRightMouseButton_OnProjList = true;
            this.projListView_DisplayHelp(sender, e);
        }

        private void explodHeadNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            int headNo = (int)this.explodHeadNumericUpDown.Value;
            if (headNo / 50 != 0)
            {
                headNo = headNo / 50 * 50;
                this.explodHeadNumericUpDown.Value = (Decimal)headNo;
            }
            this.explodTailLabel.Text = "～" + (headNo + 50 - 1).ToString();
            this._isExplodListDirty = true;
            MugenWindow.MainObj().SetExplodHeadNo(headNo);
            if (this.pauseCheckBox.Checked)
                return;
            MugenWindow.MainObj().Activate();
        }

        private void projHeadNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            int headNo = (int)this.projHeadNumericUpDown.Value;
            if (headNo / 50 != 0)
            {
                headNo = headNo / 50 * 50;
                this.projHeadNumericUpDown.Value = (Decimal)headNo;
            }
            this.projTailLabel.Text = "～" + (headNo + 50 - 1).ToString();
            this._isProjListDirty = true;
            MugenWindow.MainObj().SetProjHeadNo(headNo);
            if (this.pauseCheckBox.Checked)
                return;
            MugenWindow.MainObj().Activate();
        }

        private void listTabControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (this.listTabControl.SelectedIndex)
            {
                case 0:
                    this._isDirty = true;
                    MugenWindow.MainObj().SetDebugListMode(MugenWindow.DebugListMode.PLAYER_LIST_MODE);
                    break;
                case 1:
                    this._isExplodListDirty = true;
                    MugenWindow.MainObj().SetDebugListMode(MugenWindow.DebugListMode.EXPLOD_LIST_MODE);
                    break;
                case 2:
                    MugenWindow.MainObj().SetDebugListMode(MugenWindow.DebugListMode.PROJ_LIST_MODE);
                    break;
                default:
                    this._isDirty = true;
                    MugenWindow.MainObj().SetDebugListMode(MugenWindow.DebugListMode.PLAYER_LIST_MODE);
                    break;
            }
            if (this.pauseCheckBox.Checked)
                return;
            MugenWindow.MainObj().Activate();
        }

        private void explodToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Control explodListView = (Control)this.explodListView;
            if (explodListView != null && explodListView.Cursor == Cursors.Help)
            {
                this.explodListView.Show();
                string text = "Copies the entire explod list in table form to the clipboard." + Environment.NewLine + "(The data is pre-formatted for use in programs such as Excel.)" + Environment.NewLine;
                this.toolTip1.Hide((IWin32Window)explodListView);
                this.toolTip1.IsBalloon = false;
                this.toolTip1.IsBalloon = true;
                this.toolTip1.SetToolTip(explodListView, " ");
                this.toolTip1.Show(text, (IWin32Window)explodListView, this.ctrlDhandleToolStripMenuItem1.Width / 2, this.ctrlDhandleToolStripMenuItem1.Height * 3 / 2);
            }
            else
                this._copyAllExplod();
        }

        private void explodListView_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            if (this.explodListView.Cursor == Cursors.Help && e.Column > 0)
            {
                this.toolTip1.Hide((IWin32Window)this);
                this.toolTip1.IsBalloon = false;
                this.toolTip1.IsBalloon = true;
                string text = "By double clicking this tab you can adjust the cell size to fit the displayed data." + Environment.NewLine + "(By clicking it once more you can reset the size to default.)" + Environment.NewLine;
                this.toolTip1.SetToolTip((Control)this.playerListView, " ");
                int num = 0;
                for (int index = 0; index < e.Column; ++index)
                    num += this.explodListView.Columns[index].Width;
                this.toolTip1.Show(text, (IWin32Window)this.explodListView, num + this.explodListView.Columns[e.Column].Width / 2, 10);
            }
            else
            {
                if (e.Column <= 0 || e.Column > 3)
                    return;
                if (!this._explodColumState[e.Column - 1])
                {
                    this._explodColumState[e.Column - 1] = true;
                    this.explodListView.Columns[e.Column].Width += 40;
                }
                else
                {
                    this._explodColumState[e.Column - 1] = false;
                    this.explodListView.Columns[e.Column].Width -= 40;
                }
                if (DebugForm.MainObj().IsPaused())
                    return;
                MugenWindow.MainObj().Activate();
            }
        }

        private void projListView_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            if (this.projListView.Cursor == Cursors.Help && e.Column > 0)
            {
                this.toolTip1.Hide((IWin32Window)this);
                this.toolTip1.IsBalloon = false;
                this.toolTip1.IsBalloon = true;
                string text = "By double clicking this tab you can adjust the cell size to fix the displayed data." + Environment.NewLine + "(By clicking it once more you can reset the size to default.)" + Environment.NewLine;
                this.toolTip1.SetToolTip((Control)this.playerListView, " ");
                int num = 0;
                for (int index = 0; index < e.Column; ++index)
                    num += this.projListView.Columns[index].Width;
                this.toolTip1.Show(text, (IWin32Window)this.projListView, num + this.projListView.Columns[e.Column].Width / 2, 10);
            }
            else
            {
                if (e.Column <= 0 || e.Column > 3)
                    return;
                if (!this._projColumState[e.Column - 1])
                {
                    this._projColumState[e.Column - 1] = true;
                    this.projListView.Columns[e.Column].Width += 40;
                }
                else
                {
                    this._projColumState[e.Column - 1] = false;
                    this.projListView.Columns[e.Column].Width -= 40;
                }
                if (DebugForm.MainObj().IsPaused())
                    return;
                MugenWindow.MainObj().Activate();
            }
        }

        private void explodHeadNumericUpDown_MouseClick(object sender, MouseEventArgs e)
        {
            Control headNumericUpDown = (Control)this.explodHeadNumericUpDown;
            if (headNumericUpDown == null || !(headNumericUpDown.Cursor == Cursors.Help))
                return;
            string text = "Indicates the object set to show explods from." + Environment.NewLine + "The list will display exactly 50 explods after this index of your choosing." + Environment.NewLine;
            this.toolTip1.Hide((IWin32Window)headNumericUpDown);
            this.toolTip1.IsBalloon = false;
            this.toolTip1.IsBalloon = true;
            this.toolTip1.SetToolTip(headNumericUpDown, " ");
            this.toolTip1.Show(text, (IWin32Window)headNumericUpDown, headNumericUpDown.Width / 2, headNumericUpDown.Height / 2);
        }

        private void projHeadNumericUpDown_MouseClick(object sender, MouseEventArgs e)
        {
            Control headNumericUpDown = (Control)this.projHeadNumericUpDown;
            if (headNumericUpDown == null || !(headNumericUpDown.Cursor == Cursors.Help))
                return;
            string text = "Indicates the object set to show projectiles from." + Environment.NewLine + "The list will display exactly 50 projectiles after this index of your choosing." + Environment.NewLine;
            this.toolTip1.Hide((IWin32Window)headNumericUpDown);
            this.toolTip1.IsBalloon = false;
            this.toolTip1.IsBalloon = true;
            this.toolTip1.SetToolTip(headNumericUpDown, " ");
            this.toolTip1.Show(text, (IWin32Window)headNumericUpDown, headNumericUpDown.Width / 2, headNumericUpDown.Height / 2);
        }

        private void explodHeadLabel_MouseClick(object sender, MouseEventArgs e)
        {
            Control headNumericUpDown = (Control)this.explodHeadNumericUpDown;
            if (headNumericUpDown == null || !(headNumericUpDown.Cursor == Cursors.Help))
                return;
            string text = "Indicates the object set to show explods from." + Environment.NewLine + "The explod display will begin showing objects after the desired number." + Environment.NewLine;
            this.toolTip1.Hide((IWin32Window)headNumericUpDown);
            this.toolTip1.IsBalloon = false;
            this.toolTip1.IsBalloon = true;
            this.toolTip1.SetToolTip(headNumericUpDown, " ");
            this.toolTip1.Show(text, (IWin32Window)headNumericUpDown, headNumericUpDown.Width / 2, headNumericUpDown.Height / 2);
        }

        private void projHeadLabel_MouseClick(object sender, MouseEventArgs e)
        {
            Control headNumericUpDown = (Control)this.projHeadNumericUpDown;
            if (headNumericUpDown == null || !(headNumericUpDown.Cursor == Cursors.Help))
                return;
            string text = "Indicates the object set to show projectlies from." + Environment.NewLine + "The projectile display will begin showing objects after the desired number." + Environment.NewLine;
            this.toolTip1.Hide((IWin32Window)headNumericUpDown);
            this.toolTip1.IsBalloon = false;
            this.toolTip1.IsBalloon = true;
            this.toolTip1.SetToolTip(headNumericUpDown, " ");
            this.toolTip1.Show(text, (IWin32Window)headNumericUpDown, headNumericUpDown.Width / 2, headNumericUpDown.Height / 2);
        }

        private void label5_MouseClick(object sender, MouseEventArgs e)
        {
            Control label5 = (Control)this.label5;
            if (label5 == null || !(label5.Cursor == Cursors.Help))
                return;
            string text = "Selects which player to show projectile information for." + Environment.NewLine;
            this.toolTip1.Hide((IWin32Window)label5);
            this.toolTip1.IsBalloon = false;
            this.toolTip1.IsBalloon = true;
            this.toolTip1.SetToolTip(label5, " ");
            this.toolTip1.Show(text, (IWin32Window)label5, label5.Width / 2, label5.Height / 2);
        }

        private void p1RadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (!this.p1RadioButton.Checked)
                return;
            MugenWindow.MainObj().SetProjOwner(MugenWindow.ProjOwner.P1);
            if (this.pauseCheckBox.Checked)
                return;
            MugenWindow.MainObj().Activate();
        }

        private void p2RadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (!this.p2RadioButton.Checked)
                return;
            MugenWindow.MainObj().SetProjOwner(MugenWindow.ProjOwner.P2);
            if (this.pauseCheckBox.Checked)
                return;
            MugenWindow.MainObj().Activate();
        }

        private void p3RadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (!this.p3RadioButton.Checked)
                return;
            MugenWindow.MainObj().SetProjOwner(MugenWindow.ProjOwner.P3);
            if (this.pauseCheckBox.Checked)
                return;
            MugenWindow.MainObj().Activate();
        }

        private void p4RadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (!this.p4RadioButton.Checked)
                return;
            MugenWindow.MainObj().SetProjOwner(MugenWindow.ProjOwner.P4);
            if (this.pauseCheckBox.Checked)
                return;
            MugenWindow.MainObj().Activate();
        }

        private void projToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Control projListView = (Control)this.projListView;
            if (projListView != null && projListView.Cursor == Cursors.Help)
            {
                this.explodListView.Show();
                string text = "Copies the entire projectile list in table form to the clipboard." + Environment.NewLine + "(The data is pre-formatted for use in programs such as Excel.)" + Environment.NewLine;
                this.toolTip1.Hide((IWin32Window)projListView);
                this.toolTip1.IsBalloon = false;
                this.toolTip1.IsBalloon = true;
                this.toolTip1.SetToolTip(projListView, " ");
                this.toolTip1.Show(text, (IWin32Window)projListView, this.ctrlDhandleToolStripMenuItem1.Width / 2, this.ctrlDhandleToolStripMenuItem1.Height * 3 / 2);
            }
            else
                this._copyAllProj();
        }

        private void projToolStripMenuItem2Handle()
        {
            if (this.p1RadioButton.Checked)
            {
                if (!this.ctrlDhandleToolStripMenuItem1.Checked)
                    MugenWindow.MainObj().SetDebugTargetNo(1);
                MugenWindow.MainObj().SetVarInspectTargetNo(1);
            }
            else if (this.p2RadioButton.Checked)
            {
                if (!this.ctrlDhandleToolStripMenuItem1.Checked)
                    MugenWindow.MainObj().SetDebugTargetNo(2);
                MugenWindow.MainObj().SetVarInspectTargetNo(2);
            }
            else if (this.p3RadioButton.Checked)
            {
                if (!this.ctrlDhandleToolStripMenuItem1.Checked)
                    MugenWindow.MainObj().SetDebugTargetNo(3);
                MugenWindow.MainObj().SetVarInspectTargetNo(3);
            }
            else
            {
                if (!this.p4RadioButton.Checked)
                    return;
                if (!this.ctrlDhandleToolStripMenuItem1.Checked)
                    MugenWindow.MainObj().SetDebugTargetNo(4);
                MugenWindow.MainObj().SetVarInspectTargetNo(4);
            }
        }

        private void projToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            Control projListView = (Control)this.projListView;
            if (projListView != null && projListView.Cursor == Cursors.Help)
            {
                this.explodListView.Show();
                string text = "Changes the Variable Display information to show information of the projectile owner." + Environment.NewLine;
                this.toolTip1.Hide((IWin32Window)projListView);
                this.toolTip1.IsBalloon = false;
                this.toolTip1.IsBalloon = true;
                this.toolTip1.SetToolTip(projListView, " ");
                this.toolTip1.Show(text, (IWin32Window)projListView, this.ctrlDhandleToolStripMenuItem1.Width / 2, this.ctrlDhandleToolStripMenuItem1.Height * 3 / 2);
            }
            else
            {
                this.projToolStripMenuItem2Handle();
                if (this.pauseCheckBox.Checked)
                    return;
                MugenWindow.MainObj().Activate();
            }
        }

        private void setButton_Click(object sender, EventArgs e)
        {
            Control setButton = (Control)this.setButton;
            if (setButton != null && setButton.Cursor == Cursors.Help)
            {
                string text = "Confirms the selected Player and Trigger information to perform the test." + Environment.NewLine + "After confirming the selection, testing will begin by pressing the 'Start' button." + Environment.NewLine;
                this.toolTip1.Hide((IWin32Window)setButton);
                this.toolTip1.IsBalloon = false;
                this.toolTip1.IsBalloon = true;
                this.toolTip1.SetToolTip(setButton, " ");
                this.toolTip1.Show(text, (IWin32Window)setButton, setButton.Width / 2, setButton.Height / 2);
            }
            else
                this.setButton_Click_Handler(true);
        }

        private void setButton_Click_Handler(bool bDelayDiaply)
        {
            TriggerCheckTarget target = new TriggerCheckTarget();
            TriggerCheckTarget.Player_t player = new TriggerCheckTarget.Player_t();
            TriggerCheckTarget.Trigger_t trigger = new TriggerCheckTarget.Trigger_t();
            TriggerDatabase.TriggerValue_t triggerValueT1 = new TriggerDatabase.TriggerValue_t();
            TriggerDatabase.TriggerValue_t triggerValueT2 = new TriggerDatabase.TriggerValue_t();
            if (this.playerComboBox.Text != null && this.playerComboBox.Text != "" && this.playerComboBox.Text != "(Unspecified)")
            {
                string input = this.playerComboBox.Text.ToLower().Trim();
                if (input == "p1")
                    player.playerType = TriggerCheckTarget.Player_t.PlayerType.PLAYER_P1;
                else if (input == "p2")
                    player.playerType = TriggerCheckTarget.Player_t.PlayerType.PLAYER_P2;
                else if (input == "p3")
                    player.playerType = TriggerCheckTarget.Player_t.PlayerType.PLAYER_P3;
                else if (input == "p4")
                    player.playerType = TriggerCheckTarget.Player_t.PlayerType.PLAYER_P4;
                int result1;
                if (player.playerType == TriggerCheckTarget.Player_t.PlayerType.PLAYER_NONE && int.TryParse(Regex.Replace(input, "playerid\\(\\s*([0-9]+)\\s*\\)", "$1"), out result1))
                {
                    player.playerType = TriggerCheckTarget.Player_t.PlayerType.PLAYER_PLAYERID;
                    player.pid = result1;
                }
                int result2;
                if (player.playerType == TriggerCheckTarget.Player_t.PlayerType.PLAYER_NONE && int.TryParse(Regex.Replace(input, "p1\\,helperid\\(\\s*([0-9]+)\\s*\\)", "$1"), out result2))
                {
                    player.playerType = TriggerCheckTarget.Player_t.PlayerType.PLAYER_P1_HELPERID;
                    player.pid = result2;
                }
                int result3;
                if (player.playerType == TriggerCheckTarget.Player_t.PlayerType.PLAYER_NONE && int.TryParse(Regex.Replace(input, "p2\\,helperid\\(\\s*([0-9]+)\\s*\\)", "$1"), out result3))
                {
                    player.playerType = TriggerCheckTarget.Player_t.PlayerType.PLAYER_P2_HELPERID;
                    player.pid = result3;
                }
                int result4;
                if (player.playerType == TriggerCheckTarget.Player_t.PlayerType.PLAYER_NONE && int.TryParse(Regex.Replace(input, "p3\\,helperid\\(\\s*([0-9]+)\\s*\\)", "$1"), out result4))
                {
                    player.playerType = TriggerCheckTarget.Player_t.PlayerType.PLAYER_P3_HELPERID;
                    player.pid = result4;
                }
                int result5;
                if (player.playerType == TriggerCheckTarget.Player_t.PlayerType.PLAYER_NONE && int.TryParse(Regex.Replace(input, "p4\\,helperid\\(\\s*([0-9]+)\\s*\\)", "$1"), out result5))
                {
                    player.playerType = TriggerCheckTarget.Player_t.PlayerType.PLAYER_P4_HELPERID;
                    player.pid = result5;
                }
            }
            if (player.playerType == TriggerCheckTarget.Player_t.PlayerType.PLAYER_NONE)
            {
                int num = (int)MessageBox.Show("There's a mistake in the specified player.", "Swiss Army Knife");
                this.playerComboBox.Focus();
                this.playerComboBox.SelectAll();
                this.triggerCheckStartButton.Enabled = false;
                this.revertButton.Enabled = true;
            }
            else
            {
                if (this.triggerComboBox.Text != null && this.triggerComboBox.Text != "" && this.triggerComboBox.Text != "(Unspecified)")
                {
                    string input = this.triggerComboBox.Text.ToLower().Trim();
                    string s = Regex.Replace(input, "[A-Za-z0-9\\(\\)\\s]+\\s*\\&\\s*(\\-?[0-9]+)", "$1");
                    string str = Regex.Replace(input, "([A-Za-z0-9\\(\\)\\s]+)\\s*\\&\\s*\\-?[0-9]+", "$1");
                    int result1;
                    if (int.TryParse(s, out result1))
                    {
                        triggerValueT1.mask = (uint)result1;
                        triggerValueT2.mask = (uint)result1;
                        input = str;
                    }
                    if (input == "alive")
                        trigger.triggerType = TriggerDatabase.TriggerId.TRIGGER_ALIVE;
                    else if (input == "stateno")
                        trigger.triggerType = TriggerDatabase.TriggerId.TRIGGER_STATENO;
                    else if (input == "numhelper")
                        trigger.triggerType = TriggerDatabase.TriggerId.TRIGGER_NUMHELPER;
                    int result2;
                    if (trigger.triggerType == TriggerDatabase.TriggerId.TRIGGER_NONE && int.TryParse(Regex.Replace(input, "sysvar\\(\\s*([0-9]+)\\s*\\)", "$1"), out result2) && (result2 >= 0 && result2 <= 4))
                    {
                        trigger.triggerType = TriggerDatabase.TriggerId.TRIGGER_SYSVAR;
                        trigger.index = result2;
                    }
                    int result3;
                    if (trigger.triggerType == TriggerDatabase.TriggerId.TRIGGER_NONE && int.TryParse(Regex.Replace(input, "sysfvar\\(\\s*([0-9]+)\\s*\\)", "$1"), out result3) && (result3 >= 0 && result3 <= 4))
                    {
                        trigger.triggerType = TriggerDatabase.TriggerId.TRIGGER_SYSFVAR;
                        trigger.index = result3;
                    }
                    int result4;
                    if (trigger.triggerType == TriggerDatabase.TriggerId.TRIGGER_NONE && int.TryParse(Regex.Replace(input, "var\\(\\s*([0-9]+)\\s*\\)", "$1"), out result4) && (result4 >= 0 && result4 <= 59))
                    {
                        trigger.triggerType = TriggerDatabase.TriggerId.TRIGGER_VAR;
                        trigger.index = result4;
                    }
                    int result5;
                    if (trigger.triggerType == TriggerDatabase.TriggerId.TRIGGER_NONE && int.TryParse(Regex.Replace(input, "fvar\\(\\s*([0-9]+)\\s*\\)", "$1"), out result5) && (result5 >= 0 && result5 <= 39))
                    {
                        trigger.triggerType = TriggerDatabase.TriggerId.TRIGGER_FVAR;
                        trigger.index = result5;
                    }
                }
                if (trigger.triggerType == TriggerDatabase.TriggerId.TRIGGER_NONE)
                {
                    int num = (int)MessageBox.Show("There's a mistake in the specified triggers.", "Swiss Army Knife");
                    this.triggerComboBox.Focus();
                    this.triggerComboBox.SelectAll();
                    this.triggerCheckStartButton.Enabled = false;
                    this.revertButton.Enabled = true;
                }
                else
                {
                    if (this.valueComboBox.Text != null && this.valueComboBox.Text != "" && this.valueComboBox.Text != "(Unspecified)")
                    {
                        string input = this.valueComboBox.Text.ToLower().Trim();
                        TriggerId triggerType = trigger.triggerType;
                        if (!TriggerDatabase.IsTriggerAvailable(triggerType))
                        {
                            int num = (int)MessageBox.Show("There's an error in the specified values.", "Swiss Army Knife");
                            this.valueComboBox.Focus();
                            this.valueComboBox.SelectAll();
                            this.triggerCheckStartButton.Enabled = false;
                            this.revertButton.Enabled = true;
                            return;
                        }
                        if (triggerValueT1.valueType == TriggerDatabase.ValueType.VALUE_NONE)
                        {
                            string s = Regex.Replace(input, "\\=\\s*(\\-?[0-9\\.]+)", "$1");
                            if (TriggerDatabase.GetTriggerValueType(triggerType) == TriggerDatabase.ValueType.VALUE_INT)
                            {
                                int result;
                                if (int.TryParse(s, out result))
                                {
                                    target.SetTargetValueOpType(TriggerCheckTarget.ValueOpType.VALUE_OP_EQ);
                                    triggerValueT1.valueType = TriggerDatabase.ValueType.VALUE_INT;
                                    triggerValueT1.SetInt32Value(result);
                                }
                            }
                            else
                            {
                                float result;
                                if (TriggerDatabase.GetTriggerValueType(triggerType) == TriggerDatabase.ValueType.VALUE_FLOAT && float.TryParse(s, out result))
                                {
                                    target.SetTargetValueOpType(TriggerCheckTarget.ValueOpType.VALUE_OP_EQ);
                                    triggerValueT1.valueType = TriggerDatabase.ValueType.VALUE_FLOAT;
                                    triggerValueT1.SetSingleValue(result);
                                }
                            }
                        }
                        if (triggerValueT1.valueType == TriggerDatabase.ValueType.VALUE_NONE)
                        {
                            string s = Regex.Replace(input, "\\!\\=\\s*(\\-?[0-9\\.]+)", "$1");
                            if (TriggerDatabase.GetTriggerValueType(triggerType) == TriggerDatabase.ValueType.VALUE_INT)
                            {
                                int result;
                                if (int.TryParse(s, out result))
                                {
                                    target.SetTargetValueOpType(TriggerCheckTarget.ValueOpType.VALUE_OP_NOT_EQ);
                                    triggerValueT1.valueType = TriggerDatabase.ValueType.VALUE_INT;
                                    triggerValueT1.SetInt32Value(result);
                                }
                            }
                            else
                            {
                                float result;
                                if (TriggerDatabase.GetTriggerValueType(triggerType) == TriggerDatabase.ValueType.VALUE_FLOAT && float.TryParse(s, out result))
                                {
                                    target.SetTargetValueOpType(TriggerCheckTarget.ValueOpType.VALUE_OP_NOT_EQ);
                                    triggerValueT1.valueType = TriggerDatabase.ValueType.VALUE_FLOAT;
                                    triggerValueT1.SetSingleValue(result);
                                }
                            }
                        }
                        if (triggerValueT1.valueType == TriggerDatabase.ValueType.VALUE_NONE)
                        {
                            string s = Regex.Replace(input, "\\<\\s*(\\-?[0-9\\.]+)", "$1");
                            if (TriggerDatabase.GetTriggerValueType(triggerType) == TriggerDatabase.ValueType.VALUE_INT)
                            {
                                int result;
                                if (int.TryParse(s, out result))
                                {
                                    target.SetTargetValueOpType(TriggerCheckTarget.ValueOpType.VALUE_OP_LT);
                                    triggerValueT1.valueType = TriggerDatabase.ValueType.VALUE_INT;
                                    triggerValueT1.SetInt32Value(result);
                                }
                            }
                            else
                            {
                                float result;
                                if (TriggerDatabase.GetTriggerValueType(triggerType) == TriggerDatabase.ValueType.VALUE_FLOAT && float.TryParse(s, out result))
                                {
                                    target.SetTargetValueOpType(TriggerCheckTarget.ValueOpType.VALUE_OP_LT);
                                    triggerValueT1.valueType = TriggerDatabase.ValueType.VALUE_FLOAT;
                                    triggerValueT1.SetSingleValue(result);
                                }
                            }
                        }
                        if (triggerValueT1.valueType == TriggerDatabase.ValueType.VALUE_NONE)
                        {
                            string s = Regex.Replace(input, "\\<\\=\\s*(\\-?[0-9\\.]+)", "$1");
                            if (TriggerDatabase.GetTriggerValueType(triggerType) == TriggerDatabase.ValueType.VALUE_INT)
                            {
                                int result;
                                if (int.TryParse(s, out result))
                                {
                                    target.SetTargetValueOpType(TriggerCheckTarget.ValueOpType.VALUE_OP_LE);
                                    triggerValueT1.valueType = TriggerDatabase.ValueType.VALUE_INT;
                                    triggerValueT1.SetInt32Value(result);
                                }
                            }
                            else
                            {
                                float result;
                                if (TriggerDatabase.GetTriggerValueType(triggerType) == TriggerDatabase.ValueType.VALUE_FLOAT && float.TryParse(s, out result))
                                {
                                    target.SetTargetValueOpType(TriggerCheckTarget.ValueOpType.VALUE_OP_LE);
                                    triggerValueT1.valueType = TriggerDatabase.ValueType.VALUE_FLOAT;
                                    triggerValueT1.SetSingleValue(result);
                                }
                            }
                        }
                        if (triggerValueT1.valueType == TriggerDatabase.ValueType.VALUE_NONE)
                        {
                            string s = Regex.Replace(input, "\\>\\s*(\\-?[0-9\\.]+)", "$1");
                            if (TriggerDatabase.GetTriggerValueType(triggerType) == TriggerDatabase.ValueType.VALUE_INT)
                            {
                                int result;
                                if (int.TryParse(s, out result))
                                {
                                    target.SetTargetValueOpType(TriggerCheckTarget.ValueOpType.VALUE_OP_GT);
                                    triggerValueT1.valueType = TriggerDatabase.ValueType.VALUE_INT;
                                    triggerValueT1.SetInt32Value(result);
                                }
                            }
                            else
                            {
                                float result;
                                if (TriggerDatabase.GetTriggerValueType(triggerType) == TriggerDatabase.ValueType.VALUE_FLOAT && float.TryParse(s, out result))
                                {
                                    target.SetTargetValueOpType(TriggerCheckTarget.ValueOpType.VALUE_OP_GT);
                                    triggerValueT1.valueType = TriggerDatabase.ValueType.VALUE_FLOAT;
                                    triggerValueT1.SetSingleValue(result);
                                }
                            }
                        }
                        if (triggerValueT1.valueType == TriggerDatabase.ValueType.VALUE_NONE)
                        {
                            string s = Regex.Replace(input, "\\>\\=\\s*(\\-?[0-9\\.]+)", "$1");
                            if (TriggerDatabase.GetTriggerValueType(triggerType) == TriggerDatabase.ValueType.VALUE_INT)
                            {
                                int result;
                                if (int.TryParse(s, out result))
                                {
                                    target.SetTargetValueOpType(TriggerCheckTarget.ValueOpType.VALUE_OP_GE);
                                    triggerValueT1.valueType = TriggerDatabase.ValueType.VALUE_INT;
                                    triggerValueT1.SetInt32Value(result);
                                }
                            }
                            else
                            {
                                float result;
                                if (TriggerDatabase.GetTriggerValueType(triggerType) == TriggerDatabase.ValueType.VALUE_FLOAT && float.TryParse(s, out result))
                                {
                                    target.SetTargetValueOpType(TriggerCheckTarget.ValueOpType.VALUE_OP_GE);
                                    triggerValueT1.valueType = TriggerDatabase.ValueType.VALUE_FLOAT;
                                    triggerValueT1.SetSingleValue(result);
                                }
                            }
                        }
                        if (triggerValueT1.valueType == TriggerDatabase.ValueType.VALUE_NONE)
                        {
                            string s1 = Regex.Replace(input, "\\=\\s*\\[\\s*(\\-?[0-9\\.]+)\\s*\\,\\s*(\\-?[0-9\\.]+)\\s*\\]\\s*", "$1");
                            string s2 = Regex.Replace(input, "\\=\\s*\\[\\s*(\\-?[0-9\\.]+)\\s*\\,\\s*(\\-?[0-9\\.]+)\\s*\\]\\s*", "$2");
                            if (TriggerDatabase.GetTriggerValueType(triggerType) == TriggerDatabase.ValueType.VALUE_INT)
                            {
                                int result1;
                                int result2;
                                if (int.TryParse(s1, out result1) && int.TryParse(s2, out result2))
                                {
                                    target.SetTargetValueOpType(TriggerCheckTarget.ValueOpType.VALUE_OP_FROM_TO);
                                    triggerValueT1.valueType = TriggerDatabase.ValueType.VALUE_INT;
                                    triggerValueT1.SetInt32Value(result1);
                                    triggerValueT2.valueType = TriggerDatabase.ValueType.VALUE_INT;
                                    triggerValueT2.SetInt32Value(result2);
                                }
                            }
                            else
                            {
                                float result1;
                                float result2;
                                if (TriggerDatabase.GetTriggerValueType(triggerType) == TriggerDatabase.ValueType.VALUE_FLOAT && float.TryParse(s1, out result1) && float.TryParse(s2, out result2))
                                {
                                    target.SetTargetValueOpType(TriggerCheckTarget.ValueOpType.VALUE_OP_FROM_TO);
                                    triggerValueT1.valueType = TriggerDatabase.ValueType.VALUE_FLOAT;
                                    triggerValueT1.SetSingleValue(result1);
                                    triggerValueT2.valueType = TriggerDatabase.ValueType.VALUE_FLOAT;
                                    triggerValueT2.SetSingleValue(result2);
                                }
                            }
                        }
                        if (triggerValueT1.valueType == TriggerDatabase.ValueType.VALUE_NONE)
                        {
                            string s1 = Regex.Replace(input, "\\=\\s*\\(\\s*(\\-?[0-9\\.]+)\\s*\\,\\s*(\\-?[0-9\\.]+)\\s*\\]\\s*", "$1");
                            string s2 = Regex.Replace(input, "\\=\\s*\\(\\s*(\\-?[0-9\\.]+)\\s*\\,\\s*(\\-?[0-9\\.]+)\\s*\\]\\s*", "$2");
                            if (TriggerDatabase.GetTriggerValueType(triggerType) == TriggerDatabase.ValueType.VALUE_INT)
                            {
                                int result1;
                                int result2;
                                if (int.TryParse(s1, out result1) && int.TryParse(s2, out result2))
                                {
                                    target.SetTargetValueOpType(TriggerCheckTarget.ValueOpType.VALUE_OP_L_FROM_TO);
                                    triggerValueT1.valueType = TriggerDatabase.ValueType.VALUE_INT;
                                    triggerValueT1.SetInt32Value(result1);
                                    triggerValueT2.valueType = TriggerDatabase.ValueType.VALUE_INT;
                                    triggerValueT2.SetInt32Value(result2);
                                }
                            }
                            else
                            {
                                float result1;
                                float result2;
                                if (TriggerDatabase.GetTriggerValueType(triggerType) == TriggerDatabase.ValueType.VALUE_FLOAT && float.TryParse(s1, out result1) && float.TryParse(s2, out result2))
                                {
                                    target.SetTargetValueOpType(TriggerCheckTarget.ValueOpType.VALUE_OP_L_FROM_TO);
                                    triggerValueT1.valueType = TriggerDatabase.ValueType.VALUE_FLOAT;
                                    triggerValueT1.SetSingleValue(result1);
                                    triggerValueT2.valueType = TriggerDatabase.ValueType.VALUE_FLOAT;
                                    triggerValueT2.SetSingleValue(result2);
                                }
                            }
                        }
                        if (triggerValueT1.valueType == TriggerDatabase.ValueType.VALUE_NONE)
                        {
                            string s1 = Regex.Replace(input, "\\=\\s*\\[\\s*(\\-?[0-9\\.]+)\\s*\\,\\s*(\\-?[0-9\\.]+)\\s*\\)\\s*", "$1");
                            string s2 = Regex.Replace(input, "\\=\\s*\\[\\s*(\\-?[0-9\\.]+)\\s*\\,\\s*(\\-?[0-9\\.]+)\\s*\\)\\s*", "$2");
                            if (TriggerDatabase.GetTriggerValueType(triggerType) == TriggerDatabase.ValueType.VALUE_INT)
                            {
                                int result1;
                                int result2;
                                if (int.TryParse(s1, out result1) && int.TryParse(s2, out result2))
                                {
                                    target.SetTargetValueOpType(TriggerCheckTarget.ValueOpType.VALUE_OP_G_FROM_TO);
                                    triggerValueT1.valueType = TriggerDatabase.ValueType.VALUE_INT;
                                    triggerValueT1.SetInt32Value(result1);
                                    triggerValueT2.valueType = TriggerDatabase.ValueType.VALUE_INT;
                                    triggerValueT2.SetInt32Value(result2);
                                }
                            }
                            else
                            {
                                float result1;
                                float result2;
                                if (TriggerDatabase.GetTriggerValueType(triggerType) == TriggerDatabase.ValueType.VALUE_FLOAT && float.TryParse(s1, out result1) && float.TryParse(s2, out result2))
                                {
                                    target.SetTargetValueOpType(TriggerCheckTarget.ValueOpType.VALUE_OP_G_FROM_TO);
                                    triggerValueT1.valueType = TriggerDatabase.ValueType.VALUE_FLOAT;
                                    triggerValueT1.SetSingleValue(result1);
                                    triggerValueT2.valueType = TriggerDatabase.ValueType.VALUE_FLOAT;
                                    triggerValueT2.SetSingleValue(result2);
                                }
                            }
                        }
                        if (triggerValueT1.valueType == TriggerDatabase.ValueType.VALUE_NONE)
                        {
                            string s1 = Regex.Replace(input, "\\=\\s*\\(\\s*(\\-?[0-9\\.]+)\\s*\\,\\s*(\\-?[0-9\\.]+)\\s*\\)\\s*", "$1");
                            string s2 = Regex.Replace(input, "\\=\\s*\\(\\s*(\\-?[0-9\\.]+)\\s*\\,\\s*(\\-?[0-9\\.]+)\\s*\\)\\s*", "$2");
                            if (TriggerDatabase.GetTriggerValueType(triggerType) == TriggerDatabase.ValueType.VALUE_INT)
                            {
                                int result1;
                                int result2;
                                if (int.TryParse(s1, out result1) && int.TryParse(s2, out result2))
                                {
                                    target.SetTargetValueOpType(TriggerCheckTarget.ValueOpType.VALUE_OP_LG_FROM_TO);
                                    triggerValueT1.valueType = TriggerDatabase.ValueType.VALUE_INT;
                                    triggerValueT1.SetInt32Value(result1);
                                    triggerValueT2.valueType = TriggerDatabase.ValueType.VALUE_INT;
                                    triggerValueT2.SetInt32Value(result2);
                                }
                            }
                            else
                            {
                                float result1;
                                float result2;
                                if (TriggerDatabase.GetTriggerValueType(triggerType) == TriggerDatabase.ValueType.VALUE_FLOAT && float.TryParse(s1, out result1) && float.TryParse(s2, out result2))
                                {
                                    target.SetTargetValueOpType(TriggerCheckTarget.ValueOpType.VALUE_OP_LG_FROM_TO);
                                    triggerValueT1.valueType = TriggerDatabase.ValueType.VALUE_FLOAT;
                                    triggerValueT1.SetSingleValue(result1);
                                    triggerValueT2.valueType = TriggerDatabase.ValueType.VALUE_FLOAT;
                                    triggerValueT2.SetSingleValue(result2);
                                }
                            }
                        }
                        if (triggerValueT1.valueType == TriggerDatabase.ValueType.VALUE_NONE)
                        {
                            string s1 = Regex.Replace(input, "\\!\\=\\s*\\[\\s*(\\-?[0-9\\.]+)\\s*\\,\\s*(\\-?[0-9\\.]+)\\s*\\]\\s*", "$1");
                            string s2 = Regex.Replace(input, "\\!\\=\\s*\\[\\s*(\\-?[0-9\\.]+)\\s*\\,\\s*(\\-?[0-9\\.]+)\\s*\\]\\s*", "$2");
                            if (TriggerDatabase.GetTriggerValueType(triggerType) == TriggerDatabase.ValueType.VALUE_INT)
                            {
                                int result1;
                                int result2;
                                if (int.TryParse(s1, out result1) && int.TryParse(s2, out result2))
                                {
                                    target.SetTargetValueOpType(TriggerCheckTarget.ValueOpType.VALUE_OP_NOT_FROM_TO);
                                    triggerValueT1.valueType = TriggerDatabase.ValueType.VALUE_INT;
                                    triggerValueT1.SetInt32Value(result1);
                                    triggerValueT2.valueType = TriggerDatabase.ValueType.VALUE_INT;
                                    triggerValueT2.SetInt32Value(result2);
                                }
                            }
                            else
                            {
                                float result1;
                                float result2;
                                if (TriggerDatabase.GetTriggerValueType(triggerType) == TriggerDatabase.ValueType.VALUE_FLOAT && float.TryParse(s1, out result1) && float.TryParse(s2, out result2))
                                {
                                    target.SetTargetValueOpType(TriggerCheckTarget.ValueOpType.VALUE_OP_NOT_FROM_TO);
                                    triggerValueT1.valueType = TriggerDatabase.ValueType.VALUE_FLOAT;
                                    triggerValueT1.SetSingleValue(result1);
                                    triggerValueT2.valueType = TriggerDatabase.ValueType.VALUE_FLOAT;
                                    triggerValueT2.SetSingleValue(result2);
                                }
                            }
                        }
                        if (triggerValueT1.valueType == TriggerDatabase.ValueType.VALUE_NONE)
                        {
                            string s1 = Regex.Replace(input, "\\!\\=\\s*\\(\\s*(\\-?[0-9\\.]+)\\s*\\,\\s*(\\-?[0-9\\.]+)\\s*\\]\\s*", "$1");
                            string s2 = Regex.Replace(input, "\\!\\=\\s*\\(\\s*(\\-?[0-9\\.]+)\\s*\\,\\s*(\\-?[0-9\\.]+)\\s*\\]\\s*", "$2");
                            if (TriggerDatabase.GetTriggerValueType(triggerType) == TriggerDatabase.ValueType.VALUE_INT)
                            {
                                int result1;
                                int result2;
                                if (int.TryParse(s1, out result1) && int.TryParse(s2, out result2))
                                {
                                    target.SetTargetValueOpType(TriggerCheckTarget.ValueOpType.VALUE_OP_NOT_L_FROM_TO);
                                    triggerValueT1.valueType = TriggerDatabase.ValueType.VALUE_INT;
                                    triggerValueT1.SetInt32Value(result1);
                                    triggerValueT2.valueType = TriggerDatabase.ValueType.VALUE_INT;
                                    triggerValueT2.SetInt32Value(result2);
                                }
                            }
                            else
                            {
                                float result1;
                                float result2;
                                if (TriggerDatabase.GetTriggerValueType(triggerType) == TriggerDatabase.ValueType.VALUE_FLOAT && float.TryParse(s1, out result1) && float.TryParse(s2, out result2))
                                {
                                    target.SetTargetValueOpType(TriggerCheckTarget.ValueOpType.VALUE_OP_NOT_L_FROM_TO);
                                    triggerValueT1.valueType = TriggerDatabase.ValueType.VALUE_FLOAT;
                                    triggerValueT1.SetSingleValue(result1);
                                    triggerValueT2.valueType = TriggerDatabase.ValueType.VALUE_FLOAT;
                                    triggerValueT2.SetSingleValue(result2);
                                }
                            }
                        }
                        if (triggerValueT1.valueType == TriggerDatabase.ValueType.VALUE_NONE)
                        {
                            string s1 = Regex.Replace(input, "\\!\\=\\s*\\[\\s*(\\-?[0-9\\.]+)\\s*\\,\\s*(\\-?[0-9\\.]+)\\s*\\)\\s*", "$1");
                            string s2 = Regex.Replace(input, "\\!\\=\\s*\\[\\s*(\\-?[0-9\\.]+)\\s*\\,\\s*(\\-?[0-9\\.]+)\\s*\\)\\s*", "$2");
                            if (TriggerDatabase.GetTriggerValueType(triggerType) == TriggerDatabase.ValueType.VALUE_INT)
                            {
                                int result1;
                                int result2;
                                if (int.TryParse(s1, out result1) && int.TryParse(s2, out result2))
                                {
                                    target.SetTargetValueOpType(TriggerCheckTarget.ValueOpType.VALUE_OP_NOT_G_FROM_TO);
                                    triggerValueT1.valueType = TriggerDatabase.ValueType.VALUE_INT;
                                    triggerValueT1.SetInt32Value(result1);
                                    triggerValueT2.valueType = TriggerDatabase.ValueType.VALUE_INT;
                                    triggerValueT2.SetInt32Value(result2);
                                }
                            }
                            else
                            {
                                float result1;
                                float result2;
                                if (TriggerDatabase.GetTriggerValueType(triggerType) == TriggerDatabase.ValueType.VALUE_FLOAT && float.TryParse(s1, out result1) && float.TryParse(s2, out result2))
                                {
                                    target.SetTargetValueOpType(TriggerCheckTarget.ValueOpType.VALUE_OP_NOT_G_FROM_TO);
                                    triggerValueT1.valueType = TriggerDatabase.ValueType.VALUE_FLOAT;
                                    triggerValueT1.SetSingleValue(result1);
                                    triggerValueT2.valueType = TriggerDatabase.ValueType.VALUE_FLOAT;
                                    triggerValueT2.SetSingleValue(result2);
                                }
                            }
                        }
                        if (triggerValueT1.valueType == TriggerDatabase.ValueType.VALUE_NONE)
                        {
                            string s1 = Regex.Replace(input, "\\!\\=\\s*\\(\\s*(\\-?[0-9\\.]+)\\s*\\,\\s*(\\-?[0-9\\.]+)\\s*\\)\\s*", "$1");
                            string s2 = Regex.Replace(input, "\\!\\=\\s*\\(\\s*(\\-?[0-9\\.]+)\\s*\\,\\s*(\\-?[0-9\\.]+)\\s*\\)\\s*", "$2");
                            if (TriggerDatabase.GetTriggerValueType(triggerType) == TriggerDatabase.ValueType.VALUE_INT)
                            {
                                int result1;
                                int result2;
                                if (int.TryParse(s1, out result1) && int.TryParse(s2, out result2))
                                {
                                    target.SetTargetValueOpType(TriggerCheckTarget.ValueOpType.VALUE_OP_NOT_LG_FROM_TO);
                                    triggerValueT1.valueType = TriggerDatabase.ValueType.VALUE_INT;
                                    triggerValueT1.SetInt32Value(result1);
                                    triggerValueT2.valueType = TriggerDatabase.ValueType.VALUE_INT;
                                    triggerValueT2.SetInt32Value(result2);
                                }
                            }
                            else
                            {
                                float result1;
                                float result2;
                                if (TriggerDatabase.GetTriggerValueType(triggerType) == TriggerDatabase.ValueType.VALUE_FLOAT && float.TryParse(s1, out result1) && float.TryParse(s2, out result2))
                                {
                                    target.SetTargetValueOpType(TriggerCheckTarget.ValueOpType.VALUE_OP_NOT_LG_FROM_TO);
                                    triggerValueT1.valueType = TriggerDatabase.ValueType.VALUE_FLOAT;
                                    triggerValueT1.SetSingleValue(result1);
                                    triggerValueT2.valueType = TriggerDatabase.ValueType.VALUE_FLOAT;
                                    triggerValueT2.SetSingleValue(result2);
                                }
                            }
                        }
                    }
                    if (triggerValueT1.valueType == TriggerDatabase.ValueType.VALUE_NONE)
                    {
                        int num = (int)MessageBox.Show("There's an error in the specified values.", "Swiss Army Knife.");
                        this.valueComboBox.Focus();
                        this.valueComboBox.SelectAll();
                        this.triggerCheckStartButton.Enabled = false;
                        this.revertButton.Enabled = true;
                    }
                    else
                    {
                        if (player.playerType == TriggerCheckTarget.Player_t.PlayerType.PLAYER_NONE || trigger.triggerType == TriggerDatabase.TriggerId.TRIGGER_NONE || triggerValueT1.valueType == TriggerDatabase.ValueType.VALUE_NONE)
                            return;
                        target.SetTargetPlayer(player);
                        this._triggerPlayerString = this.playerComboBox.Text;
                        target.SetTargetTrigger(trigger);
                        this._triggerTriggerString = this.triggerComboBox.Text;
                        target.SetTargetValueFrom(triggerValueT1);
                        target.SetTargetValueTo(triggerValueT2);
                        this._triggerValueString = this.valueComboBox.Text;
                        if (MugenWindow.MainObj().GetTriggerCheckMode() == TriggerCheckTarget.CheckMode.CHECKMODE_STOPPED)
                        {
                            this.triggerCheckStartButton.Enabled = true;
                            this.revertButton.Enabled = false;
                            if (bDelayDiaply)
                            {
                                this.checkModeStateText.BackColor = this._defaultBgColor;
                                this.checkModeStateText.ForeColor = this._defaultFgColor;
                                this.checkModeStateText.Text = "Configuring...";
                                this.setButtonTimer.Start();
                            }
                            else
                            {
                                this.checkModeStateText.BackColor = this._defaultBgColor;
                                this.checkModeStateText.ForeColor = this._defaultFgColor;
                                this.checkModeStateText.Text = "Waiting for start";
                            }
                        }
                        MugenWindow.MainObj().SetTriggerCheckTarget(target);
                        this.SaveProfile();
                    }
                }
            }
        }

        private void setButtonTimer_Tick(object sender, EventArgs e)
        {
            this.setButtonTimer.Stop();
            this.checkModeStateText.BackColor = this._defaultBgColor;
            this.checkModeStateText.ForeColor = this._defaultFgColor;
            this.checkModeStateText.Text = "Waiting for start";
        }

        private void revertButton_Click(object sender, EventArgs e)
        {
            Control revertButton = (Control)this.revertButton;
            if (revertButton != null && revertButton.Cursor == Cursors.Help)
            {
                string text = "Returns the Player and Trigger settings to the previous setting." + Environment.NewLine;
                this.toolTip1.Hide((IWin32Window)revertButton);
                this.toolTip1.IsBalloon = false;
                this.toolTip1.IsBalloon = true;
                this.toolTip1.SetToolTip(revertButton, " ");
                this.toolTip1.Show(text, (IWin32Window)revertButton, revertButton.Width / 2, revertButton.Height / 2);
            }
            else
            {
                if (this._triggerPlayerString != null)
                    this.playerComboBox.Text = this._triggerPlayerString;
                else
                    this.playerComboBox.Text = "(Unspecified)";
                if (this._triggerTriggerString != null)
                    this.triggerComboBox.Text = this._triggerTriggerString;
                else
                    this.triggerComboBox.Text = "(Unspecified)";
                if (this._triggerValueString != null)
                    this.valueComboBox.Text = this._triggerValueString;
                else
                    this.valueComboBox.Text = "(Unspecified)";
                if (this._triggerPlayerString != null && this._triggerTriggerString != null && this._triggerValueString != null)
                    this.triggerCheckStartButton.Enabled = true;
                else
                    this.triggerCheckStartButton.Enabled = false;
                this.revertButton.Enabled = false;
            }
        }

        private void triggerCheckStartButton_Click(object sender, EventArgs e)
        {
            Control checkStartButton = (Control)this.triggerCheckStartButton;
            if (checkStartButton != null && checkStartButton.Cursor == Cursors.Help)
            {
                string text = "Begins the trigger breakpoint function. (This can also be enabled before the mugen process is started.)" + Environment.NewLine;
                this.toolTip1.Hide((IWin32Window)checkStartButton);
                this.toolTip1.IsBalloon = false;
                this.toolTip1.IsBalloon = true;
                this.toolTip1.SetToolTip(checkStartButton, " ");
                this.toolTip1.Show(text, (IWin32Window)checkStartButton, checkStartButton.Width / 2, checkStartButton.Height / 2);
            }
            else
            {
                if (!this.pauseCheckBox.Checked)
                    MugenWindow.MainObj().Activate();
                this.playerComboBox.Text = this._triggerPlayerString;
                this.triggerComboBox.Text = this._triggerTriggerString;
                this.valueComboBox.Text = this._triggerValueString;
                this.triggerCheckStartButton.Enabled = false;
                this.triggerCheckStopButton.Enabled = true;
                this.setButton.Enabled = false;
                this.playerComboBox.Enabled = false;
                this.triggerComboBox.Enabled = false;
                this.valueComboBox.Enabled = false;
                this.checkModeStateText.BackColor = this._defaultBgColor;
                this.checkModeStateText.ForeColor = this._defaultFgColor;
                this.checkModeStateText.Text = "Waiting";
                MugenWindow.MainObj().StartTriggerCheckMode();
            }
        }

        private void triggerCheckResumeButton_Click(object sender, EventArgs e)
        {
            Control checkResumeButton = (Control)this.triggerCheckResumeButton;
            if (checkResumeButton != null && checkResumeButton.Cursor == Cursors.Help)
            {
                string text = "Resumes the mugen process. Used after the trigger testing finds a match and freezes the process." + Environment.NewLine;
                this.toolTip1.Hide((IWin32Window)checkResumeButton);
                this.toolTip1.IsBalloon = false;
                this.toolTip1.IsBalloon = true;
                this.toolTip1.SetToolTip(checkResumeButton, " ");
                this.toolTip1.Show(text, (IWin32Window)checkResumeButton, checkResumeButton.Width / 2, checkResumeButton.Height / 2);
            }
            else
            {
                this.triggerCheckResumeButton.Enabled = false;
                if (MugenWindow.MainObj().GetIsExperimental())
                    MugenWindow.MainObj().SetPaused(false);
                MugenWindow.MainObj().ResumeTriggerCheckMode();
            }
        }

        private void triggerCheckStopButton_Click(object sender, EventArgs e)
        {
            Control triggerCheckStopButton = (Control)this.triggerCheckStopButton;
            if (triggerCheckStopButton != null && triggerCheckStopButton.Cursor == Cursors.Help)
            {
                string text = "Stops the trigger testing function." + Environment.NewLine;
                this.toolTip1.Hide((IWin32Window)triggerCheckStopButton);
                this.toolTip1.IsBalloon = false;
                this.toolTip1.IsBalloon = true;
                this.toolTip1.SetToolTip(triggerCheckStopButton, " ");
                this.toolTip1.Show(text, (IWin32Window)triggerCheckStopButton, triggerCheckStopButton.Width / 2, triggerCheckStopButton.Height / 2);
            }
            else
            {
                this.triggerCheckStartButton.Enabled = true;
                this.triggerCheckStopButton.Enabled = false;
                this.setButton.Enabled = true;
                this.playerComboBox.Enabled = true;
                this.triggerComboBox.Enabled = true;
                this.valueComboBox.Enabled = true;
                this.checkModeStateText.BackColor = this._defaultBgColor;
                this.checkModeStateText.ForeColor = this._defaultFgColor;
                this.checkModeStateText.Text = "Waiting for start";
                if (MugenWindow.MainObj().GetIsExperimental())
                    MugenWindow.MainObj().SetPaused(false);
                MugenWindow.MainObj().RemoveExpBP();
                MugenWindow.MainObj().StopTriggerCheckMode();
            }
        }

        public void EnableTriggerCheckStartButton()
        {
            this.triggerCheckStartButton.Enabled = true;
            this.triggerCheckStopButton.Enabled = false;
            this.checkModeStateText.BackColor = this._defaultBgColor;
            this.checkModeStateText.ForeColor = this._defaultFgColor;
            this.checkModeStateText.Text = "Waiting for start";
        }

        public void EnableTriggerCheckStopButton()
        {
            this.triggerCheckStartButton.Enabled = false;
            this.triggerCheckStopButton.Enabled = true;
            this.triggerCheckResumeButton.Enabled = false;
            this.checkModeStateText.BackColor = this._defaultBgColor;
            this.checkModeStateText.ForeColor = this._defaultFgColor;
            this.checkModeStateText.Text = "Testing...";
        }

        public void DisableTriggerCheckResumeButton() => this.triggerCheckResumeButton.Enabled = false;

        public void EnableTriggerCheckResumeButton()
        {
            if (MugenWindow.MainObj().GetTriggerCheckMode() == TriggerCheckTarget.CheckMode.CHECKMODE_STOPPED)
            {
                this.triggerCheckResumeButton.Enabled = false;
                MugenWindow.MainObj().ResumeTriggerCheckMode();
            }
            else
            {
                this.triggerCheckResumeButton.Enabled = true;
                this.checkModeStateText.Text = "Found a match for the trigger!";
                this.listTabControl.SelectedIndex = 3;
                this._blinkCount = 0;
                this.blinkTimer.Start();
            }
        }

        public void SetExpBPFired()
        {
            MugenWindow.MainObj().StopTriggerCheckMode();
            this.triggerCheckResumeButton.Enabled = true;
            this.checkModeStateText.Text = "Found a match for the trigger!";
            this.listTabControl.SelectedIndex = 3;
            this._blinkCount = 0;
            this.blinkTimer.Start();
        }

        private void blinkTimer_Tick(object sender, EventArgs e)
        {
            ++this._blinkCount;
            if (this._blinkCount < 10)
            {
                if (this.checkModeStateText.Text == "Found a match for the trigger!")
                {
                    if (this.checkModeStateText.ForeColor == this._defaultFgColor)
                    {
                        this.checkModeStateText.BackColor = Color.DarkOrange;
                        this.checkModeStateText.ForeColor = Color.White;
                        this.checkModeStateText.Text = "Found a match for the trigger!";
                    }
                    else
                    {
                        this.checkModeStateText.BackColor = this._defaultBgColor;
                        this.checkModeStateText.ForeColor = this._defaultFgColor;
                        this.checkModeStateText.Text = "Found a match for the trigger!";
                    }
                }
                else
                {
                    this.checkModeStateText.BackColor = this._defaultBgColor;
                    this.checkModeStateText.ForeColor = this._defaultFgColor;
                    this.blinkTimer.Stop();
                }
            }
            else if (this._blinkCount == 10)
            {
                this.checkModeStateText.BackColor = this._defaultBgColor;
                this.checkModeStateText.ForeColor = this._defaultFgColor;
                this.checkModeStateText.Text = "Mugen is frozen";
                this.blinkTimer.Stop();
            }
            else
            {
                this.checkModeStateText.BackColor = this._defaultBgColor;
                this.checkModeStateText.ForeColor = this._defaultFgColor;
                this.blinkTimer.Stop();
            }
        }

        private void LoadProfile()
        {
            TriggerCheckProfile triggerCheckProfile = ProfileManager.MainObj().GetTriggerCheckProfile();
            if (triggerCheckProfile == null)
                return;
            this._triggerPlayerString = triggerCheckProfile.GetPlayerString();
            this._triggerTriggerString = triggerCheckProfile.GetTriggerString();
            this._triggerValueString = triggerCheckProfile.GetValueString();
            this.playerComboBox.Text = this._triggerPlayerString;
            this.triggerComboBox.Text = this._triggerTriggerString;
            this.valueComboBox.Text = this._triggerValueString;
            if (!(this._triggerPlayerString != "") || !(this._triggerPlayerString != "(Unspecified)") || (!(this._triggerTriggerString != "") || !(this._triggerTriggerString != "(Unspecified)")) || !(this._triggerValueString != ""))
                return;
            int num = this._triggerValueString != "(Unspecified)" ? 1 : 0;
        }

        private void SaveProfile()
        {
            TriggerCheckProfile triggerCheckProfile = ProfileManager.MainObj().GetTriggerCheckProfile();
            if (triggerCheckProfile == null)
                return;
            triggerCheckProfile.SetPlayerString(this._triggerPlayerString);
            triggerCheckProfile.SetTriggerString(this._triggerTriggerString);
            triggerCheckProfile.SetValueString(this._triggerValueString);
            triggerCheckProfile.SaveConfigData();
        }

        private void checkModeStateText_MouseClick(object sender, MouseEventArgs e)
        {
            Control triggerComboBox = (Control)this.triggerComboBox;
            if (triggerComboBox == null || !(triggerComboBox.Cursor == Cursors.Help))
                return;
            string text = "Displays the status of the trigger testing function." + Environment.NewLine + "The following statuses are displayed." + Environment.NewLine + "       Waiting for parameters        -- Waiting for the user to specify the trigger testing parameters." + Environment.NewLine + "       Waiting for start      　-- Waiting for the user to begin the trigger testing function." + Environment.NewLine + "　　　　　　　　　　　　　(The trigger function begins after pressing the 'Start' button.)" + Environment.NewLine + "       Stand by        　-- Waiting for the match to begin before testing." + Environment.NewLine + "       Testing...     　-- Testing the specified triggers in the current match." + Environment.NewLine + "       Mugen is frozen -- This is displayed when mugen is frozen when the trigger test is succesful." + Environment.NewLine + "　　　　　　　　　　　　　(You can resume mugen by clicking the 'Resume' button.)" + Environment.NewLine + Environment.NewLine + "About the trigger testing function:" + Environment.NewLine + "This is a function to help observe when a determined trigger atains a particular value." + Environment.NewLine + "This function has the ability to freeze mugen in the precise moment where a trigger becomes true in order to help study these moments." + Environment.NewLine + "(For example, if the function is used to test the Alive = 0 trigger, then the user can observe the state number and variables at the precise moment a player dies.)" + Environment.NewLine;
            this.toolTip1.Hide((IWin32Window)triggerComboBox);
            this.toolTip1.IsBalloon = false;
            this.toolTip1.IsBalloon = true;
            this.toolTip1.SetToolTip(triggerComboBox, " ");
            this.toolTip1.Show(text, (IWin32Window)triggerComboBox, triggerComboBox.Width / 2, triggerComboBox.Height / 2);
        }

        private void playerComboBox_MouseClick(object sender, MouseEventArgs e)
        {
            Control playerComboBox = (Control)this.playerComboBox;
            if (playerComboBox == null || !(playerComboBox.Cursor == Cursors.Help))
                return;
            string text = "Specifies the object to perform a test on." + Environment.NewLine + "The following types are available:" + Environment.NewLine + "       P1 -- Checks Player 1." + Environment.NewLine + "       P2 -- Checks Player 2." + Environment.NewLine + "       P3 -- Checks Player 3." + Environment.NewLine + "       P4 -- Checks Player 4." + Environment.NewLine + "       PlayerId(xxx) -- Checks the object with PlayerID xxx, where xxx is a positive integer." + Environment.NewLine + "       P1,HelperId(xxx) -- Checks a Helper with ID xxx that belongs to Player 1. " + Environment.NewLine + "       　　　　　　　　　 (Note: If several helpers with the same ID exist, then the very first helper on the list will be tested.)" + Environment.NewLine + "       P2,HelperId(xxx) -- Checks a Helper with ID xxx that belongs to Player 2." + Environment.NewLine + "       P3,HelperId(xxx) -- Checks a Helper with ID xxx that belongs to Player 3." + Environment.NewLine + "       P4,HelperId(xxx) -- Checks a Helper with ID xxx that belongs to Player 4." + Environment.NewLine;
            this.toolTip1.Hide((IWin32Window)playerComboBox);
            this.toolTip1.IsBalloon = false;
            this.toolTip1.IsBalloon = true;
            this.toolTip1.SetToolTip(playerComboBox, " ");
            this.toolTip1.Show(text, (IWin32Window)playerComboBox, playerComboBox.Width / 2, playerComboBox.Height / 2);
        }

        private void triggerComboBox_MouseClick(object sender, MouseEventArgs e)
        {
            Control triggerComboBox = (Control)this.triggerComboBox;
            if (triggerComboBox == null || !(triggerComboBox.Cursor == Cursors.Help))
                return;
            string text = "Specifies a trigger to perform a test with." + Environment.NewLine + "The following types are available:" + Environment.NewLine + "       Alive        -- The Alive trigger." + Environment.NewLine + "       StateNo      -- The StateNo trigger." + Environment.NewLine + "       SysVar(xxx)  -- Value of a sysvar. Valid values for xxx are integers 0 to 4." + Environment.NewLine + "       SysFVar(xxx) -- Value of a sysfvar. Valid values for xxx are integers 0 to 4." + Environment.NewLine + "       Var(xxx)     -- Value of a var. Valid values for xxx are integers 0 to 59." + Environment.NewLine + "       FVar(xxx)    -- Value of an fvar. Valid values for xxx are integers from 0 to 39." + Environment.NewLine + "(More triggers will be available over time.)" + Environment.NewLine;
            this.toolTip1.Hide((IWin32Window)triggerComboBox);
            this.toolTip1.IsBalloon = false;
            this.toolTip1.IsBalloon = true;
            this.toolTip1.SetToolTip(triggerComboBox, " ");
            this.toolTip1.Show(text, (IWin32Window)triggerComboBox, triggerComboBox.Width / 2, triggerComboBox.Height / 2);
        }

        private void valueComboBox_MouseClick(object sender, MouseEventArgs e)
        {
            Control triggerComboBox = (Control)this.triggerComboBox;
            if (triggerComboBox == null || !(triggerComboBox.Cursor == Cursors.Help))
                return;
            string text = "Specifies a value range to check for the trigger." + Environment.NewLine + "The following types are available:" + Environment.NewLine + "       = xxx         -- Is equal to this exact value." + Environment.NewLine + "       != xxx        -- Is not equal to this value." + Environment.NewLine + "       < xxx         -- Is strictly less than this value." + Environment.NewLine + "       <= xxx        -- Is less or equal to this value." + Environment.NewLine + "       > xxx         -- Is strictly greater than this value." + Environment.NewLine + "       >= xxx        -- Is greater or equal to this value." + Environment.NewLine + "       = [xxx,yyy]   -- Is in between or equal to these two values." + Environment.NewLine + "       != [xxx,yyy]  -- Is not in between nor equal to these values." + Environment.NewLine;
            this.toolTip1.Hide((IWin32Window)triggerComboBox);
            this.toolTip1.IsBalloon = false;
            this.toolTip1.IsBalloon = true;
            this.toolTip1.SetToolTip(triggerComboBox, " ");
            this.toolTip1.Show(text, (IWin32Window)triggerComboBox, triggerComboBox.Width / 2, triggerComboBox.Height / 2);
        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {
        }
    }
}
