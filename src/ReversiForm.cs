/// <summary>
/// Reversi.ReversiForm.cs
/// </summary>

using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.Threading;
using System.Management;

namespace Reversi
{
    /// <summary>
    /// The main application form, used to display all UI content
    /// </summary>
    public class ReversiForm : System.Windows.Forms.Form
    {
        #region Windows Form Designer generated code
        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null)
                {
                    components.Dispose();
                }
            }
            base.Dispose(disposing);
        }

        #region Form Designer Variables

        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.Windows.Forms.Label Title;
        private IContainer components;
        private System.Windows.Forms.Timer NewGameTimer;
        private System.Windows.Forms.MainMenu mainDropDownMenu;
        private System.Windows.Forms.MenuItem fileDropDownMenu;
        private System.Windows.Forms.MenuItem gameSetupDropDownMenu;
        private System.Windows.Forms.MenuItem AIDifficultyMenu;
        private System.Windows.Forms.MenuItem DiffMenu_Easy;
        private System.Windows.Forms.MenuItem DiffMenu_Normal;
        private System.Windows.Forms.MenuItem DiffMenu_Hard;
        private System.Windows.Forms.MenuItem DiffMenu_VeryHard;
        private System.Windows.Forms.MenuItem GameSetupMenuHorzBar1;
        private System.Windows.Forms.MenuItem PvPMenu;
        private System.Windows.Forms.MenuItem PvCMenu;
        private System.Windows.Forms.MenuItem ExitMenu;
        private System.Windows.Forms.MenuItem NewGameMenu;
        private System.Windows.Forms.MenuItem debugDropDownMenu;
        private System.Windows.Forms.MenuItem DebugSkip;
        private System.Windows.Forms.MenuItem DebugProcess;
        private System.Windows.Forms.MenuItem newDebugGameScenarios;
        private System.Windows.Forms.MenuItem DebugScenario_NoWhite;
        private System.Windows.Forms.MenuItem DebugScenario_NoBlack;
        private System.Windows.Forms.MenuItem DebugScenario_MidGame;
        private Button BuildAIDBButton;
        private CheckBox visualizeCheckbox;
        private GroupBox groupBox1;
        private Button anaylzeDBButton;
        private Label label1;
        private Button dumpDBInfoButton;
        private BackgroundWorker DBBuildWorker;
        private Button cancelButton;
        private BackgroundWorker DBAnalysisWorker;
        private ComboBox gridSizeDropDown;
        private Label gridDimensionLabel;
        private Label simTimerLabel;
        private Label nodeCounter;
        private Label workCounter;
        private Label nodeCounterLabel;
        private Label workCounterLabel;
        private Label victoryCounterLabel;
        private Label victoryCounter;
        private ProgressBar RAMUsageBar;
        private System.Windows.Forms.Timer RAMCheckTimer;
        private Label RAMLabel;
        private TabControl AIInfoTabControl;
        private TabPage AIDBTab;
        private TabPage AISimTab;
        private Label CurrentTurnLabel;
        private Label blackScoreBoardTitle;
        private Label whiteScoreBoardTitle;
        private Label whiteScoreBoard;
        private Label blackScoreBoard;
        private BackgroundWorker AITurnWorker;
        private PictureBox boardXaxisLabel;
        private PictureBox boardYaxisLabel;
        private Label simDepthTitle;
        private Button hideDebugButton;
        private TrackBar simulationDepthSlider;
        private Label simDepthCountLabel;
        private Label simDepthCount;
        public RichTextBox DebugAITrace;
        private Button clearDebugLogButton;
        private Label AITraceLabel;
        private CheckBox debugLogCheckBox;
        private Button newGameButton;
        private Panel currentTurnImageSurface;
        private MenuItem GameSetupMenuHorzBar2;
        private MenuItem showAvailableMoves;
        private Panel BoardSurface;
        #endregion

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ReversiForm));
            this.Title = new System.Windows.Forms.Label();
            this.mainDropDownMenu = new System.Windows.Forms.MainMenu(this.components);
            this.fileDropDownMenu = new System.Windows.Forms.MenuItem();
            this.NewGameMenu = new System.Windows.Forms.MenuItem();
            this.ExitMenu = new System.Windows.Forms.MenuItem();
            this.gameSetupDropDownMenu = new System.Windows.Forms.MenuItem();
            this.PvPMenu = new System.Windows.Forms.MenuItem();
            this.PvCMenu = new System.Windows.Forms.MenuItem();
            this.GameSetupMenuHorzBar1 = new System.Windows.Forms.MenuItem();
            this.AIDifficultyMenu = new System.Windows.Forms.MenuItem();
            this.DiffMenu_Easy = new System.Windows.Forms.MenuItem();
            this.DiffMenu_Normal = new System.Windows.Forms.MenuItem();
            this.DiffMenu_Hard = new System.Windows.Forms.MenuItem();
            this.DiffMenu_VeryHard = new System.Windows.Forms.MenuItem();
            this.GameSetupMenuHorzBar2 = new System.Windows.Forms.MenuItem();
            this.showAvailableMoves = new System.Windows.Forms.MenuItem();
            this.debugDropDownMenu = new System.Windows.Forms.MenuItem();
            this.DebugSkip = new System.Windows.Forms.MenuItem();
            this.DebugProcess = new System.Windows.Forms.MenuItem();
            this.newDebugGameScenarios = new System.Windows.Forms.MenuItem();
            this.DebugScenario_NoWhite = new System.Windows.Forms.MenuItem();
            this.DebugScenario_NoBlack = new System.Windows.Forms.MenuItem();
            this.DebugScenario_MidGame = new System.Windows.Forms.MenuItem();
            this.BuildAIDBButton = new System.Windows.Forms.Button();
            this.visualizeCheckbox = new System.Windows.Forms.CheckBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.gridDimensionLabel = new System.Windows.Forms.Label();
            this.gridSizeDropDown = new System.Windows.Forms.ComboBox();
            this.dumpDBInfoButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.anaylzeDBButton = new System.Windows.Forms.Button();
            this.DBBuildWorker = new System.ComponentModel.BackgroundWorker();
            this.cancelButton = new System.Windows.Forms.Button();
            this.DBAnalysisWorker = new System.ComponentModel.BackgroundWorker();
            this.simTimerLabel = new System.Windows.Forms.Label();
            this.nodeCounter = new System.Windows.Forms.Label();
            this.workCounter = new System.Windows.Forms.Label();
            this.nodeCounterLabel = new System.Windows.Forms.Label();
            this.workCounterLabel = new System.Windows.Forms.Label();
            this.victoryCounterLabel = new System.Windows.Forms.Label();
            this.victoryCounter = new System.Windows.Forms.Label();
            this.NewGameTimer = new System.Windows.Forms.Timer(this.components);
            this.RAMUsageBar = new System.Windows.Forms.ProgressBar();
            this.RAMCheckTimer = new System.Windows.Forms.Timer(this.components);
            this.RAMLabel = new System.Windows.Forms.Label();
            this.AIInfoTabControl = new System.Windows.Forms.TabControl();
            this.AIDBTab = new System.Windows.Forms.TabPage();
            this.AISimTab = new System.Windows.Forms.TabPage();
            this.simDepthCountLabel = new System.Windows.Forms.Label();
            this.simDepthCount = new System.Windows.Forms.Label();
            this.simulationDepthSlider = new System.Windows.Forms.TrackBar();
            this.simDepthTitle = new System.Windows.Forms.Label();
            this.CurrentTurnLabel = new System.Windows.Forms.Label();
            this.blackScoreBoardTitle = new System.Windows.Forms.Label();
            this.whiteScoreBoardTitle = new System.Windows.Forms.Label();
            this.whiteScoreBoard = new System.Windows.Forms.Label();
            this.blackScoreBoard = new System.Windows.Forms.Label();
            this.AITurnWorker = new System.ComponentModel.BackgroundWorker();
            this.hideDebugButton = new System.Windows.Forms.Button();
            this.DebugAITrace = new System.Windows.Forms.RichTextBox();
            this.clearDebugLogButton = new System.Windows.Forms.Button();
            this.AITraceLabel = new System.Windows.Forms.Label();
            this.debugLogCheckBox = new System.Windows.Forms.CheckBox();
            this.BoardSurface = new System.Windows.Forms.Panel();
            this.newGameButton = new System.Windows.Forms.Button();
            this.boardXaxisLabel = new System.Windows.Forms.PictureBox();
            this.boardYaxisLabel = new System.Windows.Forms.PictureBox();
            this.currentTurnImageSurface = new System.Windows.Forms.Panel();
            this.groupBox1.SuspendLayout();
            this.AIInfoTabControl.SuspendLayout();
            this.AIDBTab.SuspendLayout();
            this.AISimTab.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.simulationDepthSlider)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.boardXaxisLabel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.boardYaxisLabel)).BeginInit();
            this.SuspendLayout();
            // 
            // Title
            // 
            this.Title.Font = new System.Drawing.Font("Calibri", 26.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Title.Location = new System.Drawing.Point(103, -3);
            this.Title.Name = "Title";
            this.Title.Size = new System.Drawing.Size(320, 41);
            this.Title.TabIndex = 1;
            this.Title.Text = "Reversi";
            this.Title.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // mainDropDownMenu
            // 
            this.mainDropDownMenu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.fileDropDownMenu,
            this.gameSetupDropDownMenu,
            this.debugDropDownMenu});
            // 
            // fileDropDownMenu
            // 
            this.fileDropDownMenu.Index = 0;
            this.fileDropDownMenu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.NewGameMenu,
            this.ExitMenu});
            this.fileDropDownMenu.Text = "File";
            // 
            // NewGameMenu
            // 
            this.NewGameMenu.Index = 0;
            this.NewGameMenu.Text = "&New Game";
            this.NewGameMenu.Click += new System.EventHandler(this.NewGameMenu_Click);
            // 
            // ExitMenu
            // 
            this.ExitMenu.Index = 1;
            this.ExitMenu.Text = "E&xit";
            this.ExitMenu.Click += new System.EventHandler(this.ExitMenu_Click);
            // 
            // gameSetupDropDownMenu
            // 
            this.gameSetupDropDownMenu.Index = 1;
            this.gameSetupDropDownMenu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.PvPMenu,
            this.PvCMenu,
            this.GameSetupMenuHorzBar1,
            this.AIDifficultyMenu,
            this.GameSetupMenuHorzBar2,
            this.showAvailableMoves});
            this.gameSetupDropDownMenu.Text = "Game Setup";
            // 
            // PvPMenu
            // 
            this.PvPMenu.Checked = true;
            this.PvPMenu.Index = 0;
            this.PvPMenu.Text = "Player vs Player";
            this.PvPMenu.Click += new System.EventHandler(this.PvPMenu_Click);
            // 
            // PvCMenu
            // 
            this.PvCMenu.Checked = true;
            this.PvCMenu.Index = 1;
            this.PvCMenu.Text = "Player vs Computer";
            this.PvCMenu.Click += new System.EventHandler(this.PvCMenu_Click);
            // 
            // GameSetupMenuHorzBar1
            // 
            this.GameSetupMenuHorzBar1.Index = 2;
            this.GameSetupMenuHorzBar1.Text = "-";
            // 
            // AIDifficultyMenu
            // 
            this.AIDifficultyMenu.Index = 3;
            this.AIDifficultyMenu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.DiffMenu_Easy,
            this.DiffMenu_Normal,
            this.DiffMenu_Hard,
            this.DiffMenu_VeryHard});
            this.AIDifficultyMenu.Text = "Computer Difficulty";
            // 
            // DiffMenu_Easy
            // 
            this.DiffMenu_Easy.Index = 0;
            this.DiffMenu_Easy.Text = "&Easy";
            this.DiffMenu_Easy.Click += new System.EventHandler(this.DiffMenu_EasyClick);
            // 
            // DiffMenu_Normal
            // 
            this.DiffMenu_Normal.Checked = true;
            this.DiffMenu_Normal.Index = 1;
            this.DiffMenu_Normal.Text = "&Normal";
            this.DiffMenu_Normal.Click += new System.EventHandler(this.DiffMenu_NormalClick);
            // 
            // DiffMenu_Hard
            // 
            this.DiffMenu_Hard.Index = 2;
            this.DiffMenu_Hard.Text = "&Hard";
            this.DiffMenu_Hard.Click += new System.EventHandler(this.DiffMenu_HardClick);
            // 
            // DiffMenu_VeryHard
            // 
            this.DiffMenu_VeryHard.Index = 3;
            this.DiffMenu_VeryHard.Text = "&Very Hard";
            this.DiffMenu_VeryHard.Click += new System.EventHandler(this.DiffMenu_VeryHardClick);
            // 
            // GameSetupMenuHorzBar2
            // 
            this.GameSetupMenuHorzBar2.Index = 4;
            this.GameSetupMenuHorzBar2.Text = "-";
            // 
            // showAvailableMoves
            // 
            this.showAvailableMoves.Checked = true;
            this.showAvailableMoves.Index = 5;
            this.showAvailableMoves.Text = "Show Available Moves";
            this.showAvailableMoves.Click += new System.EventHandler(this.showAvailableMoves_Click);
            // 
            // debugDropDownMenu
            // 
            this.debugDropDownMenu.Index = 2;
            this.debugDropDownMenu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.DebugSkip,
            this.DebugProcess,
            this.newDebugGameScenarios});
            this.debugDropDownMenu.Text = "Debug";
            // 
            // DebugSkip
            // 
            this.DebugSkip.Index = 0;
            this.DebugSkip.Text = "SKip Turn";
            this.DebugSkip.Click += new System.EventHandler(this.DebugSkip_Click);
            // 
            // DebugProcess
            // 
            this.DebugProcess.Checked = true;
            this.DebugProcess.Index = 1;
            this.DebugProcess.Text = "Process Piece Captures";
            this.DebugProcess.Click += new System.EventHandler(this.DebugProcess_Click);
            // 
            // newDebugGameScenarios
            // 
            this.newDebugGameScenarios.Index = 2;
            this.newDebugGameScenarios.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.DebugScenario_NoWhite,
            this.DebugScenario_NoBlack,
            this.DebugScenario_MidGame});
            this.newDebugGameScenarios.Text = "New Game Scenario";
            // 
            // DebugScenario_NoWhite
            // 
            this.DebugScenario_NoWhite.Index = 0;
            this.DebugScenario_NoWhite.Text = "White cannot move";
            this.DebugScenario_NoWhite.Click += new System.EventHandler(this.DebugScenario_NoWhite_Click);
            // 
            // DebugScenario_NoBlack
            // 
            this.DebugScenario_NoBlack.Index = 1;
            this.DebugScenario_NoBlack.Text = "Black cannot move";
            // 
            // DebugScenario_MidGame
            // 
            this.DebugScenario_MidGame.Index = 2;
            this.DebugScenario_MidGame.Text = "Mid Game";
            this.DebugScenario_MidGame.Click += new System.EventHandler(this.DebugScenario_MidGame_Click);
            // 
            // BuildAIDBButton
            // 
            this.BuildAIDBButton.Location = new System.Drawing.Point(6, 22);
            this.BuildAIDBButton.Name = "BuildAIDBButton";
            this.BuildAIDBButton.Size = new System.Drawing.Size(108, 23);
            this.BuildAIDBButton.TabIndex = 7;
            this.BuildAIDBButton.Text = "Build Database";
            this.BuildAIDBButton.UseVisualStyleBackColor = true;
            this.BuildAIDBButton.Click += new System.EventHandler(this.BuildAIDBButton_Click);
            // 
            // visualizeCheckbox
            // 
            this.visualizeCheckbox.AutoSize = true;
            this.visualizeCheckbox.BackColor = System.Drawing.Color.Transparent;
            this.visualizeCheckbox.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.visualizeCheckbox.Checked = true;
            this.visualizeCheckbox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.visualizeCheckbox.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.visualizeCheckbox.Location = new System.Drawing.Point(822, 7);
            this.visualizeCheckbox.Margin = new System.Windows.Forms.Padding(0);
            this.visualizeCheckbox.Name = "visualizeCheckbox";
            this.visualizeCheckbox.Size = new System.Drawing.Size(99, 19);
            this.visualizeCheckbox.TabIndex = 8;
            this.visualizeCheckbox.Text = "Visualizations";
            this.visualizeCheckbox.UseVisualStyleBackColor = false;
            this.visualizeCheckbox.CheckedChanged += new System.EventHandler(this.visualizeCheckbox_CheckedChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.gridDimensionLabel);
            this.groupBox1.Controls.Add(this.gridSizeDropDown);
            this.groupBox1.Controls.Add(this.dumpDBInfoButton);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.anaylzeDBButton);
            this.groupBox1.Controls.Add(this.BuildAIDBButton);
            this.groupBox1.Location = new System.Drawing.Point(6, 6);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(244, 107);
            this.groupBox1.TabIndex = 9;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Game Simulator";
            // 
            // gridDimensionLabel
            // 
            this.gridDimensionLabel.AutoSize = true;
            this.gridDimensionLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gridDimensionLabel.Location = new System.Drawing.Point(200, 51);
            this.gridDimensionLabel.Name = "gridDimensionLabel";
            this.gridDimensionLabel.Size = new System.Drawing.Size(28, 16);
            this.gridDimensionLabel.TabIndex = 15;
            this.gridDimensionLabel.Text = "4x4";
            // 
            // gridSizeDropDown
            // 
            this.gridSizeDropDown.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.gridSizeDropDown.FormattingEnabled = true;
            this.gridSizeDropDown.Items.AddRange(new object[] {
            "4",
            "5",
            "6",
            "7",
            "8"});
            this.gridSizeDropDown.Location = new System.Drawing.Point(147, 47);
            this.gridSizeDropDown.Name = "gridSizeDropDown";
            this.gridSizeDropDown.Size = new System.Drawing.Size(41, 21);
            this.gridSizeDropDown.TabIndex = 14;
            this.gridSizeDropDown.SelectedIndexChanged += new System.EventHandler(this.gridSizeDropDown_SelectedIndexChanged);
            // 
            // dumpDBInfoButton
            // 
            this.dumpDBInfoButton.Location = new System.Drawing.Point(75, 78);
            this.dumpDBInfoButton.Name = "dumpDBInfoButton";
            this.dumpDBInfoButton.Size = new System.Drawing.Size(108, 23);
            this.dumpDBInfoButton.TabIndex = 13;
            this.dumpDBInfoButton.Text = "Dump DB Info";
            this.dumpDBInfoButton.UseVisualStyleBackColor = true;
            this.dumpDBInfoButton.Click += new System.EventHandler(this.dumpDBInfoButton_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(134, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(104, 13);
            this.label1.TabIndex = 10;
            this.label1.Text = "Game Board Size";
            // 
            // anaylzeDBButton
            // 
            this.anaylzeDBButton.Location = new System.Drawing.Point(6, 52);
            this.anaylzeDBButton.Name = "anaylzeDBButton";
            this.anaylzeDBButton.Size = new System.Drawing.Size(108, 23);
            this.anaylzeDBButton.TabIndex = 9;
            this.anaylzeDBButton.Text = "Analyze Database";
            this.anaylzeDBButton.UseVisualStyleBackColor = true;
            this.anaylzeDBButton.Click += new System.EventHandler(this.anaylzeDBButton_Click);
            // 
            // DBBuildWorker
            // 
            this.DBBuildWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.DBBuildWorker_DoWork);
            this.DBBuildWorker.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.DBBuildWorker_ProgressChanged);
            this.DBBuildWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.DBBuildWorker_RunWorkerCompleted);
            // 
            // cancelButton
            // 
            this.cancelButton.BackColor = System.Drawing.Color.Red;
            this.cancelButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cancelButton.ForeColor = System.Drawing.Color.White;
            this.cancelButton.Location = new System.Drawing.Point(273, 18);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(64, 64);
            this.cancelButton.TabIndex = 10;
            this.cancelButton.Text = "Cancel Job";
            this.cancelButton.UseVisualStyleBackColor = false;
            this.cancelButton.Visible = false;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // DBAnalysisWorker
            // 
            this.DBAnalysisWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.DBAnalysisWorker_DoWork);
            // 
            // simTimerLabel
            // 
            this.simTimerLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.simTimerLabel.ForeColor = System.Drawing.Color.Red;
            this.simTimerLabel.Location = new System.Drawing.Point(255, 81);
            this.simTimerLabel.Name = "simTimerLabel";
            this.simTimerLabel.Size = new System.Drawing.Size(98, 34);
            this.simTimerLabel.TabIndex = 12;
            this.simTimerLabel.Text = "01:12:12";
            this.simTimerLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // nodeCounter
            // 
            this.nodeCounter.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nodeCounter.Location = new System.Drawing.Point(3, 137);
            this.nodeCounter.Name = "nodeCounter";
            this.nodeCounter.Size = new System.Drawing.Size(124, 32);
            this.nodeCounter.TabIndex = 13;
            this.nodeCounter.Text = "0";
            this.nodeCounter.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // workCounter
            // 
            this.workCounter.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.workCounter.Location = new System.Drawing.Point(247, 137);
            this.workCounter.Name = "workCounter";
            this.workCounter.Size = new System.Drawing.Size(113, 32);
            this.workCounter.TabIndex = 14;
            this.workCounter.Text = "0";
            this.workCounter.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // nodeCounterLabel
            // 
            this.nodeCounterLabel.AutoSize = true;
            this.nodeCounterLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nodeCounterLabel.Location = new System.Drawing.Point(18, 124);
            this.nodeCounterLabel.Name = "nodeCounterLabel";
            this.nodeCounterLabel.Size = new System.Drawing.Size(94, 13);
            this.nodeCounterLabel.TabIndex = 16;
            this.nodeCounterLabel.Text = "Total Nodes in DB";
            // 
            // workCounterLabel
            // 
            this.workCounterLabel.AutoSize = true;
            this.workCounterLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.workCounterLabel.Location = new System.Drawing.Point(261, 124);
            this.workCounterLabel.Name = "workCounterLabel";
            this.workCounterLabel.Size = new System.Drawing.Size(84, 13);
            this.workCounterLabel.TabIndex = 17;
            this.workCounterLabel.Text = "Nodes in Queue";
            // 
            // victoryCounterLabel
            // 
            this.victoryCounterLabel.AutoSize = true;
            this.victoryCounterLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.victoryCounterLabel.Location = new System.Drawing.Point(149, 124);
            this.victoryCounterLabel.Name = "victoryCounterLabel";
            this.victoryCounterLabel.Size = new System.Drawing.Size(74, 13);
            this.victoryCounterLabel.TabIndex = 19;
            this.victoryCounterLabel.Text = "Total Victories";
            // 
            // victoryCounter
            // 
            this.victoryCounter.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.victoryCounter.Location = new System.Drawing.Point(125, 137);
            this.victoryCounter.Name = "victoryCounter";
            this.victoryCounter.Size = new System.Drawing.Size(123, 32);
            this.victoryCounter.TabIndex = 18;
            this.victoryCounter.Text = "0";
            this.victoryCounter.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // NewGameTimer
            // 
            this.NewGameTimer.Enabled = true;
            this.NewGameTimer.Tick += new System.EventHandler(this.NewGameTimer_Tick);
            // 
            // RAMUsageBar
            // 
            this.RAMUsageBar.BackColor = System.Drawing.Color.Silver;
            this.RAMUsageBar.ForeColor = System.Drawing.Color.Navy;
            this.RAMUsageBar.Location = new System.Drawing.Point(3, 189);
            this.RAMUsageBar.Name = "RAMUsageBar";
            this.RAMUsageBar.Size = new System.Drawing.Size(357, 24);
            this.RAMUsageBar.Step = 1;
            this.RAMUsageBar.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.RAMUsageBar.TabIndex = 22;
            this.RAMUsageBar.Tag = "";
            this.RAMUsageBar.Visible = false;
            // 
            // RAMCheckTimer
            // 
            this.RAMCheckTimer.Interval = 500;
            this.RAMCheckTimer.Tick += new System.EventHandler(this.RAMCheckTimer_Tick);
            // 
            // RAMLabel
            // 
            this.RAMLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RAMLabel.Location = new System.Drawing.Point(3, 167);
            this.RAMLabel.Name = "RAMLabel";
            this.RAMLabel.Size = new System.Drawing.Size(84, 25);
            this.RAMLabel.TabIndex = 23;
            this.RAMLabel.Text = "Memory:";
            this.RAMLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.RAMLabel.Visible = false;
            // 
            // AIInfoTabControl
            // 
            this.AIInfoTabControl.Controls.Add(this.AIDBTab);
            this.AIInfoTabControl.Controls.Add(this.AISimTab);
            this.AIInfoTabControl.HotTrack = true;
            this.AIInfoTabControl.Location = new System.Drawing.Point(546, 8);
            this.AIInfoTabControl.Name = "AIInfoTabControl";
            this.AIInfoTabControl.SelectedIndex = 0;
            this.AIInfoTabControl.Size = new System.Drawing.Size(375, 259);
            this.AIInfoTabControl.TabIndex = 24;
            // 
            // AIDBTab
            // 
            this.AIDBTab.BackColor = System.Drawing.Color.Transparent;
            this.AIDBTab.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.AIDBTab.Controls.Add(this.groupBox1);
            this.AIDBTab.Controls.Add(this.RAMUsageBar);
            this.AIDBTab.Controls.Add(this.RAMLabel);
            this.AIDBTab.Controls.Add(this.victoryCounterLabel);
            this.AIDBTab.Controls.Add(this.cancelButton);
            this.AIDBTab.Controls.Add(this.victoryCounter);
            this.AIDBTab.Controls.Add(this.simTimerLabel);
            this.AIDBTab.Controls.Add(this.workCounterLabel);
            this.AIDBTab.Controls.Add(this.nodeCounter);
            this.AIDBTab.Controls.Add(this.nodeCounterLabel);
            this.AIDBTab.Controls.Add(this.workCounter);
            this.AIDBTab.Location = new System.Drawing.Point(4, 22);
            this.AIDBTab.Name = "AIDBTab";
            this.AIDBTab.Padding = new System.Windows.Forms.Padding(3);
            this.AIDBTab.Size = new System.Drawing.Size(367, 233);
            this.AIDBTab.TabIndex = 0;
            this.AIDBTab.Text = "AI Database";
            // 
            // AISimTab
            // 
            this.AISimTab.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.AISimTab.Controls.Add(this.simDepthCountLabel);
            this.AISimTab.Controls.Add(this.simDepthCount);
            this.AISimTab.Controls.Add(this.simulationDepthSlider);
            this.AISimTab.Controls.Add(this.simDepthTitle);
            this.AISimTab.Location = new System.Drawing.Point(4, 22);
            this.AISimTab.Name = "AISimTab";
            this.AISimTab.Padding = new System.Windows.Forms.Padding(3);
            this.AISimTab.Size = new System.Drawing.Size(367, 233);
            this.AISimTab.TabIndex = 1;
            this.AISimTab.Text = "AI Game Simulations";
            this.AISimTab.UseVisualStyleBackColor = true;
            // 
            // simDepthCountLabel
            // 
            this.simDepthCountLabel.AutoSize = true;
            this.simDepthCountLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.simDepthCountLabel.Location = new System.Drawing.Point(313, 40);
            this.simDepthCountLabel.Name = "simDepthCountLabel";
            this.simDepthCountLabel.Size = new System.Drawing.Size(38, 26);
            this.simDepthCountLabel.TabIndex = 41;
            this.simDepthCountLabel.Text = "Turns\r\nAhead";
            // 
            // simDepthCount
            // 
            this.simDepthCount.Font = new System.Drawing.Font("Calibri", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.simDepthCount.Location = new System.Drawing.Point(305, 13);
            this.simDepthCount.Name = "simDepthCount";
            this.simDepthCount.Size = new System.Drawing.Size(49, 31);
            this.simDepthCount.TabIndex = 40;
            this.simDepthCount.Text = "20";
            this.simDepthCount.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // simulationDepthSlider
            // 
            this.simulationDepthSlider.LargeChange = 2;
            this.simulationDepthSlider.Location = new System.Drawing.Point(6, 29);
            this.simulationDepthSlider.Maximum = 20;
            this.simulationDepthSlider.Minimum = 2;
            this.simulationDepthSlider.Name = "simulationDepthSlider";
            this.simulationDepthSlider.Size = new System.Drawing.Size(299, 45);
            this.simulationDepthSlider.SmallChange = 2;
            this.simulationDepthSlider.TabIndex = 38;
            this.simulationDepthSlider.TickFrequency = 2;
            this.simulationDepthSlider.TickStyle = System.Windows.Forms.TickStyle.Both;
            this.simulationDepthSlider.Value = 6;
            this.simulationDepthSlider.Scroll += new System.EventHandler(this.simulationDepthSlider_Scroll);
            // 
            // simDepthTitle
            // 
            this.simDepthTitle.AutoSize = true;
            this.simDepthTitle.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.simDepthTitle.Location = new System.Drawing.Point(6, 4);
            this.simDepthTitle.Name = "simDepthTitle";
            this.simDepthTitle.Size = new System.Drawing.Size(147, 23);
            this.simDepthTitle.TabIndex = 36;
            this.simDepthTitle.Text = "Simulation Depth";
            // 
            // CurrentTurnLabel
            // 
            this.CurrentTurnLabel.BackColor = System.Drawing.Color.Transparent;
            this.CurrentTurnLabel.Font = new System.Drawing.Font("Calibri", 26.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CurrentTurnLabel.ForeColor = System.Drawing.Color.Black;
            this.CurrentTurnLabel.Location = new System.Drawing.Point(22, 547);
            this.CurrentTurnLabel.Name = "CurrentTurnLabel";
            this.CurrentTurnLabel.Size = new System.Drawing.Size(107, 49);
            this.CurrentTurnLabel.TabIndex = 3;
            this.CurrentTurnLabel.Text = "Turn:";
            this.CurrentTurnLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // blackScoreBoardTitle
            // 
            this.blackScoreBoardTitle.Font = new System.Drawing.Font("Calibri", 26.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.blackScoreBoardTitle.Location = new System.Drawing.Point(360, 550);
            this.blackScoreBoardTitle.Name = "blackScoreBoardTitle";
            this.blackScoreBoardTitle.Size = new System.Drawing.Size(112, 42);
            this.blackScoreBoardTitle.TabIndex = 26;
            this.blackScoreBoardTitle.Text = "Black:";
            this.blackScoreBoardTitle.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // whiteScoreBoardTitle
            // 
            this.whiteScoreBoardTitle.Font = new System.Drawing.Font("Calibri", 26.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.whiteScoreBoardTitle.Location = new System.Drawing.Point(186, 550);
            this.whiteScoreBoardTitle.Name = "whiteScoreBoardTitle";
            this.whiteScoreBoardTitle.Size = new System.Drawing.Size(121, 40);
            this.whiteScoreBoardTitle.TabIndex = 27;
            this.whiteScoreBoardTitle.Text = "White:";
            this.whiteScoreBoardTitle.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // whiteScoreBoard
            // 
            this.whiteScoreBoard.Font = new System.Drawing.Font("Calibri", 27.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.whiteScoreBoard.Location = new System.Drawing.Point(292, 554);
            this.whiteScoreBoard.Name = "whiteScoreBoard";
            this.whiteScoreBoard.Size = new System.Drawing.Size(62, 40);
            this.whiteScoreBoard.TabIndex = 28;
            this.whiteScoreBoard.Text = "0";
            this.whiteScoreBoard.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // blackScoreBoard
            // 
            this.blackScoreBoard.Font = new System.Drawing.Font("Calibri", 27.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.blackScoreBoard.Location = new System.Drawing.Point(455, 552);
            this.blackScoreBoard.Name = "blackScoreBoard";
            this.blackScoreBoard.Size = new System.Drawing.Size(64, 44);
            this.blackScoreBoard.TabIndex = 29;
            this.blackScoreBoard.Text = "0";
            this.blackScoreBoard.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // AITurnWorker
            // 
            this.AITurnWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.AITurnMonitor_DoWork);
            this.AITurnWorker.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.AITurnMonitor_ProgressChanged);
            this.AITurnWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.AITurnMonitor_RunWorkerCompleted);
            // 
            // hideDebugButton
            // 
            this.hideDebugButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.hideDebugButton.Location = new System.Drawing.Point(517, 270);
            this.hideDebugButton.Margin = new System.Windows.Forms.Padding(0);
            this.hideDebugButton.Name = "hideDebugButton";
            this.hideDebugButton.Size = new System.Drawing.Size(25, 57);
            this.hideDebugButton.TabIndex = 38;
            this.hideDebugButton.Text = "<<\r\n<<\r\n<<";
            this.hideDebugButton.UseVisualStyleBackColor = true;
            this.hideDebugButton.Click += new System.EventHandler(this.hideDebugButton_Click);
            // 
            // DebugAITrace
            // 
            this.DebugAITrace.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.DebugAITrace.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DebugAITrace.HideSelection = false;
            this.DebugAITrace.Location = new System.Drawing.Point(551, 303);
            this.DebugAITrace.Name = "DebugAITrace";
            this.DebugAITrace.Size = new System.Drawing.Size(367, 290);
            this.DebugAITrace.TabIndex = 5;
            this.DebugAITrace.Text = "";
            // 
            // clearDebugLogButton
            // 
            this.clearDebugLogButton.Location = new System.Drawing.Point(554, 274);
            this.clearDebugLogButton.Name = "clearDebugLogButton";
            this.clearDebugLogButton.Size = new System.Drawing.Size(57, 23);
            this.clearDebugLogButton.TabIndex = 36;
            this.clearDebugLogButton.Text = "Clear";
            this.clearDebugLogButton.UseVisualStyleBackColor = true;
            this.clearDebugLogButton.Click += new System.EventHandler(this.clearDebugLogButton_Click);
            // 
            // AITraceLabel
            // 
            this.AITraceLabel.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.AITraceLabel.Location = new System.Drawing.Point(648, 273);
            this.AITraceLabel.Name = "AITraceLabel";
            this.AITraceLabel.Size = new System.Drawing.Size(175, 21);
            this.AITraceLabel.TabIndex = 6;
            this.AITraceLabel.Text = "Debug Log";
            this.AITraceLabel.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // debugLogCheckBox
            // 
            this.debugLogCheckBox.AutoSize = true;
            this.debugLogCheckBox.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.debugLogCheckBox.Checked = true;
            this.debugLogCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.debugLogCheckBox.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.debugLogCheckBox.Location = new System.Drawing.Point(851, 276);
            this.debugLogCheckBox.Name = "debugLogCheckBox";
            this.debugLogCheckBox.Size = new System.Drawing.Size(67, 19);
            this.debugLogCheckBox.TabIndex = 38;
            this.debugLogCheckBox.Text = "Logging";
            this.debugLogCheckBox.UseVisualStyleBackColor = true;
            // 
            // BoardSurface
            // 
            this.BoardSurface.Location = new System.Drawing.Point(30, 61);
            this.BoardSurface.Name = "BoardSurface";
            this.BoardSurface.Size = new System.Drawing.Size(480, 480);
            this.BoardSurface.TabIndex = 39;
            this.BoardSurface.Paint += new System.Windows.Forms.PaintEventHandler(this.BoardSurface_Paint);
            this.BoardSurface.MouseDown += new System.Windows.Forms.MouseEventHandler(this.PlaceUserPiece);
            // 
            // newGameButton
            // 
            this.newGameButton.Location = new System.Drawing.Point(438, 8);
            this.newGameButton.Name = "newGameButton";
            this.newGameButton.Size = new System.Drawing.Size(72, 23);
            this.newGameButton.TabIndex = 40;
            this.newGameButton.Text = "New Game";
            this.newGameButton.UseVisualStyleBackColor = true;
            this.newGameButton.Click += new System.EventHandler(this.newGameButton_Click);
            // 
            // boardXaxisLabel
            // 
            this.boardXaxisLabel.Image = global::Reversi.Properties.Resources.boardXaxis;
            this.boardXaxisLabel.Location = new System.Drawing.Point(30, 41);
            this.boardXaxisLabel.Name = "boardXaxisLabel";
            this.boardXaxisLabel.Size = new System.Drawing.Size(473, 29);
            this.boardXaxisLabel.TabIndex = 34;
            this.boardXaxisLabel.TabStop = false;
            // 
            // boardYaxisLabel
            // 
            this.boardYaxisLabel.Image = global::Reversi.Properties.Resources.boardYaxis;
            this.boardYaxisLabel.Location = new System.Drawing.Point(5, 62);
            this.boardYaxisLabel.Name = "boardYaxisLabel";
            this.boardYaxisLabel.Size = new System.Drawing.Size(27, 476);
            this.boardYaxisLabel.TabIndex = 35;
            this.boardYaxisLabel.TabStop = false;
            // 
            // currentTurnImageSurface
            // 
            this.currentTurnImageSurface.Location = new System.Drawing.Point(104, 554);
            this.currentTurnImageSurface.Name = "currentTurnImageSurface";
            this.currentTurnImageSurface.Size = new System.Drawing.Size(38, 38);
            this.currentTurnImageSurface.TabIndex = 40;
            this.currentTurnImageSurface.Paint += new System.Windows.Forms.PaintEventHandler(this.currentTurnImageSurface_Paint);
            // 
            // ReversiForm
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(925, 603);
            this.Controls.Add(this.currentTurnImageSurface);
            this.Controls.Add(this.BoardSurface);
            this.Controls.Add(this.newGameButton);
            this.Controls.Add(this.debugLogCheckBox);
            this.Controls.Add(this.visualizeCheckbox);
            this.Controls.Add(this.AITraceLabel);
            this.Controls.Add(this.hideDebugButton);
            this.Controls.Add(this.clearDebugLogButton);
            this.Controls.Add(this.DebugAITrace);
            this.Controls.Add(this.AIInfoTabControl);
            this.Controls.Add(this.CurrentTurnLabel);
            this.Controls.Add(this.whiteScoreBoard);
            this.Controls.Add(this.blackScoreBoard);
            this.Controls.Add(this.boardXaxisLabel);
            this.Controls.Add(this.boardYaxisLabel);
            this.Controls.Add(this.Title);
            this.Controls.Add(this.whiteScoreBoardTitle);
            this.Controls.Add(this.blackScoreBoardTitle);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Menu = this.mainDropDownMenu;
            this.Name = "ReversiForm";
            this.Text = "Reversi";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.AIInfoTabControl.ResumeLayout(false);
            this.AIDBTab.ResumeLayout(false);
            this.AIDBTab.PerformLayout();
            this.AISimTab.ResumeLayout(false);
            this.AISimTab.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.simulationDepthSlider)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.boardXaxisLabel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.boardYaxisLabel)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        /// <summary>
        /// Initializes the form and global variables
        /// </summary>
        public ReversiForm()
        {
            // Initialize the form (generated code)
            InitializeComponent();

            // Esablish graphics handles
            gBoardGFX = BoardSurface.CreateGraphics();
            gCurrentTurnImageGFX = currentTurnImageSurface.CreateGraphics();

            // Static global binds for important form elements
            gDebugText          = DebugAITrace;
            gWhiteScoreBoard    = whiteScoreBoard;
            gBlackScoreBoard    = blackScoreBoard;
            gCurrentTurnLabel   = CurrentTurnLabel;
            gAITurnWorker       = AITurnWorker;
            gDebugLogCheckBox   = debugLogCheckBox;
            gSimTimerLabel      = simTimerLabel;
            gNodeCounter        = nodeCounter;
            gWorkCounter        = workCounter;
            gVictoryCounter     = victoryCounter;
            gCurrentTurnSurface = currentTurnImageSurface;
            gShowAvailableMoves = showAvailableMoves;

            // Game board and piece image handles
            gBoardImage      = Image.FromHbitmap(Properties.Resources.reversi_grid.GetHbitmap());
            gWhitePieceImage = Image.FromHbitmap(Properties.Resources.whitepiece.GetHbitmap());
            gBlackPieceImage = Image.FromHbitmap(Properties.Resources.blackpiece.GetHbitmap());

            // Graphic dimensions
            boardPieceImageSize = gBlackPieceImage.Width;
            boardGridSize = Properties.Settings.Default.GridSize;

            // AI Opponent worker thread settings
            AITurnWorker.WorkerSupportsCancellation = true;
            AITurnWorker.WorkerReportsProgress = true;

            // Setup form elements
            gridSizeDropDown.SelectedIndex = 4;
            simTimerLabel.Text = "";
            updateMaxDepth();
            AIInfoTabControl.SelectTab(AISimTab);

            // Establish a static binding to the global game instance
            gCurrentGame = ReversiApplication.getCurrentGame();
        }

        #region Global Variables

        // Static handles to graphical assets
        private static Image gBlackPieceImage;
        private static Image gWhitePieceImage;
        private static Image gBoardImage;

        // Static handles to form objects
        private static RichTextBox gDebugText = new RichTextBox();
        private static Graphics gBoardGFX;
        private static Label gWhiteScoreBoard;
        private static Label gBlackScoreBoard;
        private static Label gCurrentTurnLabel;
        private static Graphics gCurrentTurnImageGFX;
        private static Panel gCurrentTurnSurface;
        private static BackgroundWorker gAITurnWorker;
        private static CheckBox gDebugLogCheckBox;
        private static Label gSimTimerLabel;
        private static Label gNodeCounter;
        private static Label gWorkCounter;
        private static Label gVictoryCounter;
        private static MenuItem gShowAvailableMoves;

        // Piece dimensions
        private static int boardPieceImageSize;
        private static int boardGridSize;

        // The AI Database
        private AIDatabase DatabaseFactory = new AIDatabase();

        // The board used to track what has been drawn on the screen
        private static Board LastDrawnBoard = new Board();

        // The Global Game Object
        private static Game gCurrentGame;

        // Flags that determine who is playing (ai or human)
        private static Boolean isVSComputer = true;
        private static int AIDifficulty = 1;

        #endregion

        #region Getters and Setters

        /// <summary>
        /// Gets the board that was last drawn onto the screen
        /// </summary>
        /// <returns>The last drawn Board object</returns>
        public static Board getLastDrawnBoard() { return LastDrawnBoard; }

        /// <summary>
        /// Resets the board that was last drawn onto the screen
        /// </summary>
        /// <param name="BoardSize">The new board size</param>
        public static void setLastDrawnBoard(int BoardSize) { LastDrawnBoard = new Board( BoardSize ); }

        /// <summary>
        /// True if the current game is versus the computer opponent
        /// </summary>
        public static Boolean isVsComputer() { return isVSComputer; }

        /// <summary>
        /// Returns the current computer difficulty setting
        /// </summary>
        public static int getAIDifficulty() { return AIDifficulty; }

        /// <summary>
        /// Sets the difficulty of the computer opponent
        /// </summary>
        /// <param name="setAIDifficulty">The difficulty level (0-4)</param>
        public static void setAIDifficulty(int setAIDifficulty) { AIDifficulty = setAIDifficulty; }

        /// <summary>
        /// Thread unsafe way to append text to the debug window
        /// </summary>
        /// <param name="newDebugText">The debug message to append</param>
        public static void appendDebugText(String newDebugText) { gDebugText.Text += newDebugText; }

        /// <summary>
        /// Thread unsafe way to set the text of the debug window
        /// </summary>
        /// <param name="newDebugText">The debug message set</param>
        public static void setDebugText( String newDebugText ) { gDebugText.Text = newDebugText; }
 
        #endregion

        /// <summary>
        /// Resets the form elements to prepare for a new game
        /// </summary>
        private void StartNewGame()
        {
            gBlackScoreBoard.Text = "0";
            gWhiteScoreBoard.Text = "0";
            gCurrentTurnLabel.Text = "Turn:";
            gCurrentTurnSurface.Visible = true;

            // Reset the score board
            gBlackScoreBoard.Visible = true;
            gWhiteScoreBoard.Visible = true;
            CurrentTurnLabel.Visible = true;
            whiteScoreBoardTitle.Visible = true;
            blackScoreBoardTitle.Visible = true;

            ReversiApplication.resetCurrentGame(getCurrentBoardSize());
            gCurrentGame = ReversiApplication.getCurrentGame();

            gCurrentGame.getAI().setMaxDepth(simulationDepthSlider.Value);

            gCurrentGame.getAI().setVisualizeProcess(visualizeCheckbox.Checked);

            RefreshPieces(FullRefresh: true);
            MarkAvailableMoves(gCurrentGame.getCurrentTurn());
            UpdateTurnImage(gCurrentGame.getCurrentTurn());
            UpdateScoreBoard();
        }

        #region Game Board graphical manipulation methods

        /// <summary>
        /// Updates the score board for both players
        /// </summary>
        public static void UpdateScoreBoard()
        {
            gBlackScoreBoard.Text = gCurrentGame.getGameBoard().FindScore(ReversiApplication.BLACK).ToString();
            gWhiteScoreBoard.Text = gCurrentGame.getGameBoard().FindScore(ReversiApplication.WHITE).ToString();
            gBlackScoreBoard.Refresh();
            gWhiteScoreBoard.Refresh();
        }

        /// <summary>
        /// Draw all game pieces on the board
        /// </summary>
        /// <param name="FullRefresh">(optional) True forces a full refresh of all pieces</param>
        public static void RefreshPieces(bool FullRefresh = false)
        {
            RefreshPieces(gCurrentGame.getGameBoard(), FullRefresh);
        }

        /// <summary>
        /// Draw all game pieces on the board
        /// </summary>
        /// <param name="SourceBoard">The board to draw onto the screen</param>
        /// <param name="FullRefresh">(optional) True forces a full refresh of all pieces</param>
        public static void RefreshPieces(Board SourceBoard, bool FullRefresh = false)
        {
            if (FullRefresh)
                RedrawBoardImage();

            for (int Y = 0; Y < SourceBoard.getBoardSize(); Y++)
                for (int X = 0; X < SourceBoard.getBoardSize(); X++)
                    if (( getLastDrawnBoard().ColorAt(X, Y) != SourceBoard.ColorAt(X, Y)) || (FullRefresh))
                        DrawPiece(X, Y, SourceBoard.ColorAt(X, Y));

            getLastDrawnBoard().CopyBoard(SourceBoard.getBoardPieces());
        }

        /// <summary>
        /// Draw a single game pieces on the board
        /// </summary>
        /// <param name="Piece">The piece to place</param>
        /// <param name="color">The piece color</param>
        public static void DrawPiece(Point Piece, int Color)
        {
            DrawPiece(Piece.X, Piece.Y, Color);
        }

        /// <summary>
        /// Draw a single game pieces on the board
        /// </summary>
        /// <param name="X">The X value of the piece</param>
        /// <param name="Y">The Y value of the piece</param>
        /// <param name="color">The piece color</param>
        public static void DrawPiece(int X, int Y, int Color)
        {
            if ((Color == ReversiApplication.WHITE) || (Color == ReversiApplication.BLACK))
                gBoardGFX.DrawImage(getTurnImage(Color), X * boardGridSize + 1, Y * boardGridSize + 1, boardPieceImageSize, boardPieceImageSize);
        }

        /// <summary>
        /// Redraws the board image, erasing all pieces
        /// </summary>
        public static void RedrawBoardImage()
        {
            gBoardGFX.DrawImage(gBoardImage, 0, 0, 480, 480);
        }

        /// <summary>
        /// Marks all of the available moves for the given turn on the current game board
        /// </summary>
        /// <param name="Turn">The turn to use</param>
        public static void MarkAvailableMoves(int Turn)
        {
            MarkAvailableMoves(gCurrentGame.getGameBoard(), Turn);
        }

        /// <summary>
        /// Marks all of the available moves for the given turn on the given game board
        /// </summary>
        /// <param name="SourceBoard">The game board</param>
        /// <param name="Turn">The turn to use</param>
        public static void MarkAvailableMoves(Board SourceBoard, int Turn)
        {
            // Only display if the 'Show Available Moves' box is checked
            if( gShowAvailableMoves.Checked )
                // Loop through all available moves and place a dot at the location
                foreach (Point CurrentPiece in SourceBoard.AvailableMoves( Turn ))
                    gBoardGFX.FillEllipse(new SolidBrush(Color.DarkSeaGreen), CurrentPiece.X * boardGridSize + boardGridSize / 2 - 4, CurrentPiece.Y * boardGridSize + boardGridSize / 2 - 4, 8, 8);
        }

        /// <summary>
        /// Places a highlight circle at the given locations
        /// </summary>
        /// <param name="PieceList">The list of pieces</param>
        /// <param name="PieceColor">The highlight color</param>
        public static void HighlightPiece(Point[] PieceList, Color PieceColor)
        {
            foreach (Point CurrentPiece in PieceList)
                HighlightPiece(CurrentPiece.X, CurrentPiece.Y, PieceColor);
        }

        /// <summary>
        /// Places a highlight circle at the given location
        /// </summary>
        /// <param name="Piece">The piece to highlight</param>
        /// <param name="PieceColor">The highlight color</param>
        /// <param name="PieceLabel">(optional) Text to place in the center of the spot</param>
        public static void HighlightPiece(Point Piece, Color PieceColor, String PieceLabel = "")
        {
            HighlightPiece(Piece.X, Piece.Y, PieceColor, PieceLabel);
        }

        /// <summary>
        /// Places a highlight circle at the given location
        /// </summary>
        /// <param name="X">The X value of the piece to highlight</param>
        /// <param name="Y">The Y value of the piece to highlight</param>
        /// <param name="PieceColor">The highlight color</param>
        /// <param name="PieceLabel">(optional) Text to place in the center of the spot</param>
        public static void HighlightPiece(int X, int Y, Color PieceColor, String PieceLabel = "")
        {
            gBoardGFX.DrawEllipse(new Pen(PieceColor, 4), X * boardGridSize + 5, Y * boardGridSize + 5, boardPieceImageSize - 8, boardPieceImageSize - 8);

            StringFormat sf = new StringFormat();
            sf.LineAlignment = StringAlignment.Center;
            sf.Alignment = StringAlignment.Center;

            if( PieceLabel != "" )
                gBoardGFX.DrawString(PieceLabel, new Font("Tahoma", (float)9, FontStyle.Regular), Brushes.White, new RectangleF(X * boardGridSize + 5, Y * boardGridSize + 14, boardGridSize - 10, boardGridSize - 28), sf);
        }

        /// <summary>
        /// Returns the correct image for a given turn
        /// </summary>
        /// <param name="Turn">The turn</param>
        /// <returns>Image for the given turn</returns>
        public static Image getTurnImage(int Turn)
        {
            if (Turn == ReversiApplication.WHITE)
                return (gWhitePieceImage);
            
            return (gBlackPieceImage);
        }

        /// <summary>
        /// Updates the "current turn" image on the form
        /// </summary>
        /// <param name="Turn">The turn to update the form with</param>
        public static void UpdateTurnImage(int Turn)
        {
            if (Turn == ReversiApplication.WHITE)
                gCurrentTurnImageGFX.DrawImage(getTurnImage(Turn), 0, 0, gCurrentTurnSurface.Width, gCurrentTurnSurface.Height);
            else
                gCurrentTurnImageGFX.DrawImage(getTurnImage(Turn), 0, 0, gCurrentTurnSurface.Width, gCurrentTurnSurface.Height);
        }

        /// <summary>
        /// Updates the "Current Turn" image to indicate a winner (or tie)
        /// </summary>
        /// <param name="WinningTurn">The winning turn</param>
        public static void ShowWinner(int WinningTurn)
        {
            if (WinningTurn == ReversiApplication.EMPTY)
            {
                gCurrentTurnLabel.Text = "Tie";
                gCurrentTurnSurface.Visible = false;
            }
            else if ((WinningTurn == ReversiApplication.BLACK) || (WinningTurn == ReversiApplication.WHITE))
            {
                gCurrentTurnLabel.Text = "Win!";
                UpdateTurnImage(WinningTurn);
            }
        }

        /// <summary>
        /// Called when the "Game Board" is repainted, refreshes the pieces
        /// </summary>
        private void BoardSurface_Paint(object sender, PaintEventArgs e)
        {
            RefreshPieces(FullRefresh: true);
        }

        /// <summary>
        /// Called when the "Current Turn Image" is repainted, refreshes the current turn image
        /// </summary>
        private void currentTurnImageSurface_Paint(object sender, PaintEventArgs e)
        {
            UpdateTurnImage(gCurrentGame.getCurrentTurn());
        }

        /// <summary>
        /// Thread safe delegate for updateDatabaseProgress()
        /// </summary>
        /// <param name="TimeElapsed">The time that has elapsed so far in the build</param>
        /// <param name="WorkNodeCount">The number of nodes sitting in the work queue</param>
        /// <param name="NodeTotal">The total number of nodes processed</param>
        /// <param name="VictoryTotal">The total number of victory states discovered so far</param>
        public delegate void updateDatabaseProgressDelegate(TimeSpan TimeElapsed, int WorkNodeCount, int NodeTotal, int VictoryTotal);

        /// <summary>
        /// Thread safe way to update the database build progress form elements
        /// </summary>
        /// <param name="TimeElapsed">The time that has elapsed so far in the build</param>
        /// <param name="WorkNodeCount">The number of nodes sitting in the work queue</param>
        /// <param name="NodeTotal">The total number of nodes processed</param>
        /// <param name="VictoryTotal">The total number of victory states discovered so far</param>
        public static void updateDatabaseProgress(TimeSpan TimeElapsed, int WorkNodeCount, int NodeTotal, int VictoryTotal)
        {
            gNodeCounter.Invoke(new updateDatabaseProgressDelegate(updateDatabaseProgressForm), TimeElapsed, WorkNodeCount, NodeTotal, VictoryTotal);
        }

        /// <summary>
        /// Thread UNSAFE way to update the database build progress form elements
        /// </summary>
        /// <param name="TimeElapsed">The time that has elapsed so far in the build</param>
        /// <param name="WorkNodeCount">The number of nodes sitting in the work queue</param>
        /// <param name="NodeTotal">The total number of nodes processed</param>
        /// <param name="VictoryTotal">The total number of victory states discovered so far</param>
        private static void updateDatabaseProgressForm(TimeSpan TimeElapsed, int WorkNodeCount, int NodeTotal, int VictoryTotal)
        {
            gSimTimerLabel.Text = TimeElapsed.ToString(@"hh\:mm\:ss");
            gNodeCounter.Text = NodeTotal.ToString();
            gWorkCounter.Text = WorkNodeCount.ToString();
            gVictoryCounter.Text = VictoryTotal.ToString();
        }

        /// <summary>
        /// Update the RAM usage meter
        /// </summary>
        private void UpdateRAMprogress()
        {
            ObjectQuery wql = new ObjectQuery("SELECT * FROM Win32_OperatingSystem");
            ManagementObjectSearcher searcher = new ManagementObjectSearcher(wql);
            ManagementObjectCollection results = searcher.Get();

            foreach (ManagementObject result in results)
            {
                if (RAMUsageBar.Maximum < 1024)
                    RAMUsageBar.Maximum = Convert.ToInt32(result["FreePhysicalMemory"]);

                RAMUsageBar.Value = Math.Min(Convert.ToInt32(result["FreePhysicalMemory"]), RAMUsageBar.Maximum);
            }

            // If RAM is running low, emergency stop any running jobs
            if ((float)RAMUsageBar.Value / (float)RAMUsageBar.Maximum < Properties.Settings.Default.MemoryFloor)
            {
                cancelAIWorkers();
                gDebugText.Text += "#####   DB Build Aborted: Memory floor reached (" + RAMUsageBar.Value.ToString("0,0.") + " KB free)  #####";
            }

            Graphics RAMGfx = RAMUsageBar.CreateGraphics();
            int MemoryAbortLine = Convert.ToInt32(RAMUsageBar.Width * Properties.Settings.Default.MemoryFloor);

            RAMGfx.DrawString(RAMUsageBar.Value.ToString("0,0.") + " KB free", new Font("Arial", (float)11, FontStyle.Regular), Brushes.White, new PointF(120, 2));
            RAMGfx.DrawLine(new Pen(Color.Red, 2), MemoryAbortLine, 0, MemoryAbortLine, RAMUsageBar.Height);
            RAMGfx.DrawString("Abort   Line", new Font("Arial", (float)8, FontStyle.Regular), Brushes.White, new PointF(MemoryAbortLine - 34, 4));
        }

        #endregion

        #region Drop Down Menu Event Handelers

        /// <summary>
        /// Easy AI difficulty menu option selected
        /// </summary>
        private void DiffMenu_EasyClick(object sender, System.EventArgs e)
        {
            DiffMenu_Easy.Checked = true;
            DiffMenu_Normal.Checked = false;
            DiffMenu_Hard.Checked = false;
            DiffMenu_VeryHard.Checked = false;
            AIDifficulty = 0;
        }

        /// <summary>
        /// Normal AI difficulty menu option selected
        /// </summary>
        private void DiffMenu_NormalClick(object sender, System.EventArgs e)
        {
            DiffMenu_Easy.Checked = false;
            DiffMenu_Normal.Checked = true;
            DiffMenu_Hard.Checked = false;
            DiffMenu_VeryHard.Checked = false;
            AIDifficulty = 1;
        }

        /// <summary>
        /// Hard AI difficulty menu option selected
        /// </summary>
        private void DiffMenu_HardClick(object sender, System.EventArgs e)
        {
            DiffMenu_Easy.Checked = false;
            DiffMenu_Normal.Checked = false;
            DiffMenu_Hard.Checked = true;
            DiffMenu_VeryHard.Checked = false;
            AIDifficulty = 2;
        }

        /// <summary>
        /// Very Hard AI difficulty menu option selected
        /// </summary>
        private void DiffMenu_VeryHardClick(object sender, System.EventArgs e)
        {
            DiffMenu_Easy.Checked = false;
            DiffMenu_Normal.Checked = false;
            DiffMenu_Hard.Checked = false;
            DiffMenu_VeryHard.Checked = true;
            AIDifficulty = 3;
        }

        /// <summary>
        /// Player vs Player menu option selected
        /// </summary>
        private void PvPMenu_Click(object sender, System.EventArgs e)
        {
            PvPMenu.Checked = true;
            PvCMenu.Checked = false;
            isVSComputer = false;
        }

        /// <summary>
        /// Player vs Computer menu option selected
        /// </summary>
        private void PvCMenu_Click(object sender, System.EventArgs e)
        {
            PvPMenu.Checked = false;
            PvCMenu.Checked = true;
            isVSComputer = true;
        }

        /// <summary>
        /// Game Exit menu option selected
        /// </summary>
        private void ExitMenu_Click(object sender, System.EventArgs e)
        {
            ActiveForm.Close();
        }

        /// <summary>
        /// Show Available Moves menu option selected
        /// </summary>
        private void showAvailableMoves_Click(object sender, EventArgs e)
        {
            showAvailableMoves.Checked = !showAvailableMoves.Checked;
        }

        /// <summary>
        /// New Game menu option selected
        /// </summary>
        private void NewGameMenu_Click(object sender, System.EventArgs e)
        {
            StartNewGame();
        }

        /// <summary>
        /// Skip turn (debug option) menu option selected
        /// </summary>
        private void DebugSkip_Click(object sender, System.EventArgs e)
        {
            gCurrentGame.SwitchTurn();
        }

        // unused
        private void DebugProcess_Click(object sender, System.EventArgs e)
        {
            gCurrentGame.setProcessMoves(!gCurrentGame.getProcessMoves());
            DebugProcess.Checked = !DebugProcess.Checked;
        }

        /// <summary>
        /// Start a new game scenario where white cannot move
        /// </summary>
        private void DebugScenario_NoWhite_Click(object sender, System.EventArgs e)
        {
            StartNewGame();
            gCurrentGame.getGameBoard().ClearBoard();
            gCurrentGame.getGameBoard().PutPiece(0, 0, ReversiApplication.BLACK);
            gCurrentGame.getGameBoard().PutPiece(0, 1, ReversiApplication.BLACK);
            gCurrentGame.getGameBoard().PutPiece(0, 2, ReversiApplication.BLACK);
            gCurrentGame.getGameBoard().PutPiece(0, 3, ReversiApplication.BLACK);
            gCurrentGame.getGameBoard().PutPiece(0, 4, ReversiApplication.BLACK);
            gCurrentGame.getGameBoard().PutPiece(0, 5, ReversiApplication.BLACK);
            gCurrentGame.getGameBoard().PutPiece(0, 6, ReversiApplication.BLACK);
            gCurrentGame.getGameBoard().PutPiece(0, 7, ReversiApplication.BLACK);
            gCurrentGame.getGameBoard().PutPiece(1, 0, ReversiApplication.WHITE);
            gCurrentGame.getGameBoard().PutPiece(1, 1, ReversiApplication.WHITE);
            gCurrentGame.getGameBoard().PutPiece(1, 2, ReversiApplication.WHITE);
            gCurrentGame.getGameBoard().PutPiece(1, 3, ReversiApplication.WHITE);
            gCurrentGame.getGameBoard().PutPiece(1, 4, ReversiApplication.WHITE);
            gCurrentGame.getGameBoard().PutPiece(1, 5, ReversiApplication.WHITE);
            gCurrentGame.getGameBoard().PutPiece(1, 6, ReversiApplication.WHITE);
            gCurrentGame.getGameBoard().PutPiece(1, 7, ReversiApplication.WHITE);
            RefreshPieces(FullRefresh: true);
            UpdateScoreBoard();
            UpdateTurnImage(gCurrentGame.getCurrentTurn());
            gCurrentGame.setCurrentTurn(ReversiApplication.BLACK);
            StartAITurnWorker();
        }

        /// <summary>
        /// Sets up the a new game with the middle of the board already filled in
        /// </summary>
        private void DebugScenario_MidGame_Click(object sender, EventArgs e)
        {
            StartNewGame();
            gCurrentGame.getGameBoard().PutPiece(2, 2, ReversiApplication.WHITE);
            gCurrentGame.getGameBoard().PutPiece(2, 3, ReversiApplication.BLACK);
            gCurrentGame.getGameBoard().PutPiece(2, 4, ReversiApplication.BLACK);
            gCurrentGame.getGameBoard().PutPiece(2, 5, ReversiApplication.BLACK);
            gCurrentGame.getGameBoard().PutPiece(3, 2, ReversiApplication.WHITE);
            gCurrentGame.getGameBoard().PutPiece(3, 3, ReversiApplication.BLACK);
            gCurrentGame.getGameBoard().PutPiece(3, 4, ReversiApplication.BLACK);
            gCurrentGame.getGameBoard().PutPiece(3, 5, ReversiApplication.BLACK);
            gCurrentGame.getGameBoard().PutPiece(4, 2, ReversiApplication.BLACK);
            gCurrentGame.getGameBoard().PutPiece(4, 3, ReversiApplication.BLACK);
            gCurrentGame.getGameBoard().PutPiece(4, 4, ReversiApplication.BLACK);
            gCurrentGame.getGameBoard().PutPiece(4, 5, ReversiApplication.BLACK);
            gCurrentGame.getGameBoard().PutPiece(5, 2, ReversiApplication.BLACK);
            gCurrentGame.getGameBoard().PutPiece(5, 3, ReversiApplication.WHITE);
            gCurrentGame.getGameBoard().PutPiece(5, 4, ReversiApplication.WHITE);
            gCurrentGame.getGameBoard().PutPiece(5, 5, ReversiApplication.WHITE);
            RefreshPieces(FullRefresh: true);
            UpdateScoreBoard();
            UpdateTurnImage(gCurrentGame.getCurrentTurn());
        }

        #endregion

        #region AI Database Form & Event Handelers

        /// <summary>
        /// Responds to the analyze database button press, starts the job
        /// </summary>
        private void BuildAIDBButton_Click(object sender, EventArgs e)
        {
            SetupSimulationForm();
            DBBuildWorker.RunWorkerAsync(getCurrentBoardSize());
        }

        /// <summary>
        /// Starts the database background worker (button press)
        /// </summary>
        private void DBBuildWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            StartBuildDB(Convert.ToInt32(e.Argument.ToString()));
        }

        /// <summary>
        /// Starts the database build background worker
        /// </summary>
        private void StartBuildDB(int BoardSize = 4)
        {
            ReversiApplication.resetCurrentGame(BoardSize);
            gCurrentGame = ReversiApplication.getCurrentGame();

            DatabaseFactory.BuildAIDatabase(DBBuildWorker, BoardSize, visualizeCheckbox.Checked, true);
        }

        /// <summary>
        /// Called from within the database build background woker to report the progress of the build
        /// </summary>
        private void DBBuildWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {

        }

        /// <summary>
        /// Called when the RAM timer ticks, used to update the RAM usage meter during database builds
        /// </summary>
        private void RAMCheckTimer_Tick(object sender, EventArgs e)
        {
            UpdateRAMprogress();
        }

        /// <summary>
        /// Runs when the database background worker thread is finished
        /// </summary>
        private void DBBuildWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            ClearSimulationForm();
        }

        /// <summary>
        /// Dumps the database information to the debug window
        /// </summary>
        private void dumpDBInfoButton_Click(object sender, EventArgs e)
        {
            gDebugText.Text += DatabaseFactory.DumpSimulationInfo();
        }

        /// <summary>
        /// Responds to the analyze database button press
        /// </summary>
        private void anaylzeDBButton_Click(object sender, EventArgs e)
        {
            SetupSimulationForm();
            DBAnalysisWorker.RunWorkerAsync();
        }

        /// <summary>
        /// Responds to the "Cancel Job" button
        /// </summary>
        private void cancelButton_Click(object sender, EventArgs e)
        {
            cancelAIWorkers();
        }


        /// <summary>
        /// Cancels any of the background jobs that are currently running
        /// </summary>
        private void cancelAIWorkers()
        {
            if (DBBuildWorker.IsBusy)
                DBBuildWorker.CancelAsync();

            if (DBAnalysisWorker.IsBusy)
                DBAnalysisWorker.CancelAsync();

            ClearSimulationForm();
        }

        /// <summary>
        /// Starts of the database analysis background worker
        /// </summary>
        private void DBAnalysisWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            Boolean DisplayDebug = true;
            DatabaseFactory.AnalyzeAIDatabase(DBAnalysisWorker, visualizeCheckbox.Checked, gDebugText, DisplayDebug);
        }

        /// <summary>
        /// Hides / Resets the form elements associated with the database builder
        /// </summary>
        private void ClearSimulationForm()
        {
            simTimerLabel.Text = "";
            cancelButton.Visible = false;
            RAMCheckTimer.Enabled = false;
            RAMUsageBar.Visible = false;
            RAMLabel.Visible = false;
            RAMUsageBar.Maximum = 100;
        }

        /// <summary>
        /// Displays the form elements associated with the database builder
        /// </summary>
        private void SetupSimulationForm()
        {
            cancelButton.Visible = true;
            DBAnalysisWorker.WorkerSupportsCancellation = true;
            DBAnalysisWorker.WorkerReportsProgress = true;
            DBBuildWorker.WorkerSupportsCancellation = true;
            DBBuildWorker.WorkerReportsProgress = true;
            RAMCheckTimer.Enabled = true;
            RAMUsageBar.Visible = true;
            RAMLabel.Visible = true;
            UpdateRAMprogress();

            // Reset the score board
            gBlackScoreBoard.Visible = false;
            gWhiteScoreBoard.Visible = false;
            CurrentTurnLabel.Visible = false;
            whiteScoreBoardTitle.Visible = false;
            blackScoreBoardTitle.Visible = false;
            gCurrentTurnSurface.Visible = false;
        }

        #endregion

        #region Miscellaneous Form Level Methods

        /// <summary>
        /// Returns an integer board size as selected on the form
        /// </summary>
        /// <returns>An integer board size as selected on the form</returns>
        private int getCurrentBoardSize()
        {
            return (Convert.ToInt32(gridSizeDropDown.Items[gridSizeDropDown.SelectedIndex].ToString()));
        }

        /// <summary>
        /// Responds to the "New Game" button being clicked
        /// </summary>
        private void newGameButton_Click(object sender, EventArgs e)
        {
            StartNewGame();
        }

        /// <summary>
        /// Responds to the MouseUp event on the board image, processes the click as a placed piece
        /// </summary>
        private void PlaceUserPiece(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            int x = (e.X + 1) / boardGridSize;
            int y = (e.Y + 1) / boardGridSize;

            // Don't process the mouse click if there is a turn already being processed
            if (!gCurrentGame.getTurnInProgress())
                gCurrentGame.ProcessTurn(x, y);
        }
        
        /// <summary>
        /// If the grid size drop down changes, updates the board with the new dimensions
        /// </summary>
        private void gridSizeDropDown_SelectedIndexChanged(object sender, EventArgs e)
        {
            int newBoardSize = getCurrentBoardSize();

            gridDimensionLabel.Text = newBoardSize + "x" + newBoardSize;

            BoardSurface.Width = boardGridSize * newBoardSize;
            BoardSurface.Height = boardGridSize * newBoardSize;

            StartNewGame();
        }

        /// <summary>
        /// Starts a new game 100ms after the form has loaded
        /// </summary>
        private void NewGameTimer_Tick(object sender, EventArgs e)
        {
            StartNewGame();
            NewGameTimer.Enabled = false;
        }

        /// <summary>
        /// Thread safe delegates for setting the debug window with the new text
        /// </summary>
        /// <param name="newText">The information to report</param>
        private delegate void setDebugTextDelagate(string newText);

        /// <summary>
        /// Thread safe delegates for appending to the debug window with the new text
        /// </summary>
        /// <param name="newText">The information to report</param>
        private delegate void appendDebugTextDelagate(string newText);

        /// <summary>
        /// Thread safe way to update the debug message box
        /// </summary>
        /// <param name="newDebugMsg">The information to report</param>
        /// <param name="updateConsole">(optional: false) True to update the console</param>
        /// <param name="updateWindow">(optional: true) True to update the debug window</param>
        /// <param name="overwrite">(optional: false) To reset the debug window</param>
        public static void reportDebugMessage(String newDebugMsg, bool updateConsole = false, bool updateWindow = true, bool overwrite = false)
        {
            if( gDebugLogCheckBox.Checked && updateWindow )
                if ( overwrite )
                    gDebugText.Invoke(new setDebugTextDelagate(setDebugText), newDebugMsg + Environment.NewLine);
                else
                    gDebugText.Invoke(new appendDebugTextDelagate(appendDebugText), newDebugMsg + Environment.NewLine);

            if (updateConsole)
                Console.WriteLine(newDebugMsg);
        }

        /// <summary>
        /// Thread safe way to clear the debug message box
        /// </summary>
        public static void clearDebugMessage()
        {
            gDebugText.Invoke(new setDebugTextDelagate(setDebugText), "");
        }

        /// <summary>
        /// Responds to the 'Clear' button on top of the debug window
        /// </summary>
        private void clearDebugLogButton_Click(object sender, EventArgs e)
        {
            clearDebugMessage();
        }

        /// <summary>
        /// Hides/Shows the debug panel
        /// </summary>
        private void hideDebugButton_Click(object sender, EventArgs e)
        {
            if (Width > 565)
            {
                Width = 565;
                hideDebugButton.Text = ">>\n>>\n>>";
                AIInfoTabControl.Visible = false;
                DebugAITrace.Visible = false;
                clearDebugLogButton.Visible = false;
            }
            else
            {
                Width = 941;
                hideDebugButton.Text = "<<\n<<\n<<";
                AIInfoTabControl.Visible = true;
                DebugAITrace.Visible = true;
                clearDebugLogButton.Visible = true;
            }
        }

        #endregion

        #region AI Turn BG Worker Event Handelers

        /// <summary>
        /// Starts the AI turn worker job, which will analyze the board and make a move
        /// </summary>
        public static void StartAITurnWorker()
        {
            gAITurnWorker.RunWorkerAsync();
        }

        /// <summary>
        /// Called mid-way through an AI turn analysis, used to report back progress
        /// </summary>
        public static void ReportAITurnWorkerProgress(int progress = 0)
        {
            gAITurnWorker.ReportProgress(progress);
        }

        /// <summary>
        /// Called asynchronously when it is time for the AI to wake up and do some work
        /// </summary>
        private void AITurnMonitor_DoWork(object sender, DoWorkEventArgs e)
        {
            gCurrentGame.ProcessAITurn();
        }

        /// <summary>
        /// Called every time the AI monitor has a move to render
        /// </summary>
        private void AITurnMonitor_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            
        }

        /// <summary>
        /// Called when the AI monitor has no more moves to place
        /// </summary>
        private void AITurnMonitor_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            gCurrentGame.SwitchTurn();
            UpdateScoreBoard();
            ShowWinner( gCurrentGame.getGameBoard().DetermineWinner() );
            gCurrentGame.setTurnInProgress(false);
            MarkAvailableMoves(gCurrentGame.getCurrentTurn());
        }

        #endregion

        #region AI Simulation Tab event handlers

        /// <summary>
        /// Responds to the simulation depth slider being moved
        /// </summary>
        private void simulationDepthSlider_Scroll(object sender, EventArgs e)
        {
            if (simulationDepthSlider.Value % 2 != 0)
                simulationDepthSlider.Value++;

            updateMaxDepth();
        }

        /// <summary>
        /// Updates the current game and form elements with the current simulation max depth
        /// </summary>
        private void updateMaxDepth()
        {
            simDepthCount.Text = simulationDepthSlider.Value.ToString();
            gCurrentGame.getAI().setMaxDepth(simulationDepthSlider.Value - 1);
        }

        /// <summary>
        /// Responds to the visulize checkbox being changed
        /// </summary>
        private void visualizeCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            gCurrentGame.getAI().setVisualizeProcess(visualizeCheckbox.Checked);
        }

        #endregion
    }
}
