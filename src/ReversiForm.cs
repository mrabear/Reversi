// Reversi
// Brian Hebert

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
        private PictureBox BoardPicture;
        private IContainer components;
        private System.Windows.Forms.Timer NewGameTimer;
        private System.Windows.Forms.MainMenu mainDropDownMenu;
        private System.Windows.Forms.MenuItem fileDropDownMenu;
        private System.Windows.Forms.MenuItem gameSetupDropDownMenu;
        private System.Windows.Forms.MenuItem menuItem8;
        private System.Windows.Forms.MenuItem DiffMenu_Easy;
        private System.Windows.Forms.MenuItem DiffMenu_Normal;
        private System.Windows.Forms.MenuItem DiffMenu_Hard;
        private System.Windows.Forms.MenuItem DiffMenu_VeryHard;
        private System.Windows.Forms.MenuItem menuItem9;
        private System.Windows.Forms.MenuItem PvPMenu;
        private System.Windows.Forms.MenuItem PvCMenu;
        private System.Windows.Forms.MenuItem ExitMenu;
        private System.Windows.Forms.MenuItem NewGameMenu;
        private System.Windows.Forms.MenuItem debugDropDownMenu;
        private System.Windows.Forms.MenuItem DebugSkip;
        private System.Windows.Forms.MenuItem DebugProcess;

        private System.Windows.Forms.MenuItem menuItem4;
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
        private PictureBox unusedGrid;
        private Label simTimerLabel;
        private Label nodeCounter;
        private Label workCounter;
        private Label nodeCounterLabel;
        private Label workCounterLabel;
        private Label victoryCounterLabel;
        private Label victoryCounter;
        private PictureBox blackPieceImg;
        private ProgressBar RAMUsageBar;
        private System.Windows.Forms.Timer RAMCheckTimer;
        private Label RAMLabel;
        private TabControl AIInfoTabControl;
        private TabPage AIDBTab;
        private TabPage AISimTab;
        private Label CurrentTurnLabel;
        private PictureBox CurrentTurnImage;
        private Label blackScoreBoardTitle;
        private Label whiteScoreBoardTitle;
        private Label whiteScoreBoard;
        private Label blackScoreBoard;
        private BackgroundWorker AITurnWorker;
        private PictureBox emptyPieceImg;
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
        private PictureBox whitePieceImg;
        #endregion

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ReversiForm));
            this.BoardPicture = new System.Windows.Forms.PictureBox();
            this.Title = new System.Windows.Forms.Label();
            this.mainDropDownMenu = new System.Windows.Forms.MainMenu(this.components);
            this.fileDropDownMenu = new System.Windows.Forms.MenuItem();
            this.NewGameMenu = new System.Windows.Forms.MenuItem();
            this.ExitMenu = new System.Windows.Forms.MenuItem();
            this.gameSetupDropDownMenu = new System.Windows.Forms.MenuItem();
            this.PvPMenu = new System.Windows.Forms.MenuItem();
            this.PvCMenu = new System.Windows.Forms.MenuItem();
            this.menuItem9 = new System.Windows.Forms.MenuItem();
            this.menuItem8 = new System.Windows.Forms.MenuItem();
            this.DiffMenu_Easy = new System.Windows.Forms.MenuItem();
            this.DiffMenu_Normal = new System.Windows.Forms.MenuItem();
            this.DiffMenu_Hard = new System.Windows.Forms.MenuItem();
            this.DiffMenu_VeryHard = new System.Windows.Forms.MenuItem();
            this.debugDropDownMenu = new System.Windows.Forms.MenuItem();
            this.DebugSkip = new System.Windows.Forms.MenuItem();
            this.DebugProcess = new System.Windows.Forms.MenuItem();
            this.menuItem4 = new System.Windows.Forms.MenuItem();
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
            this.unusedGrid = new System.Windows.Forms.PictureBox();
            this.simTimerLabel = new System.Windows.Forms.Label();
            this.nodeCounter = new System.Windows.Forms.Label();
            this.workCounter = new System.Windows.Forms.Label();
            this.nodeCounterLabel = new System.Windows.Forms.Label();
            this.workCounterLabel = new System.Windows.Forms.Label();
            this.victoryCounterLabel = new System.Windows.Forms.Label();
            this.victoryCounter = new System.Windows.Forms.Label();
            this.blackPieceImg = new System.Windows.Forms.PictureBox();
            this.whitePieceImg = new System.Windows.Forms.PictureBox();
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
            this.CurrentTurnImage = new System.Windows.Forms.PictureBox();
            this.blackScoreBoardTitle = new System.Windows.Forms.Label();
            this.whiteScoreBoardTitle = new System.Windows.Forms.Label();
            this.whiteScoreBoard = new System.Windows.Forms.Label();
            this.blackScoreBoard = new System.Windows.Forms.Label();
            this.AITurnWorker = new System.ComponentModel.BackgroundWorker();
            this.emptyPieceImg = new System.Windows.Forms.PictureBox();
            this.boardXaxisLabel = new System.Windows.Forms.PictureBox();
            this.boardYaxisLabel = new System.Windows.Forms.PictureBox();
            this.hideDebugButton = new System.Windows.Forms.Button();
            this.DebugAITrace = new System.Windows.Forms.RichTextBox();
            this.clearDebugLogButton = new System.Windows.Forms.Button();
            this.AITraceLabel = new System.Windows.Forms.Label();
            this.debugLogCheckBox = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.BoardPicture)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.unusedGrid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.blackPieceImg)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.whitePieceImg)).BeginInit();
            this.AIInfoTabControl.SuspendLayout();
            this.AIDBTab.SuspendLayout();
            this.AISimTab.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.simulationDepthSlider)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.CurrentTurnImage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptyPieceImg)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.boardXaxisLabel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.boardYaxisLabel)).BeginInit();
            this.SuspendLayout();
            // 
            // BoardPicture
            // 
            this.BoardPicture.Image = ((System.Drawing.Image)(resources.GetObject("BoardPicture.Image")));
            this.BoardPicture.Location = new System.Drawing.Point(30, 61);
            this.BoardPicture.Name = "BoardPicture";
            this.BoardPicture.Size = new System.Drawing.Size(320, 320);
            this.BoardPicture.TabIndex = 0;
            this.BoardPicture.TabStop = false;
            this.BoardPicture.MouseUp += new System.Windows.Forms.MouseEventHandler(this.PlaceUserPiece);
            // 
            // Title
            // 
            this.Title.Font = new System.Drawing.Font("Calibri", 26.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Title.Location = new System.Drawing.Point(30, -1);
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
            this.menuItem9,
            this.menuItem8});
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
            // menuItem9
            // 
            this.menuItem9.Index = 2;
            this.menuItem9.Text = "-";
            // 
            // menuItem8
            // 
            this.menuItem8.Index = 3;
            this.menuItem8.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.DiffMenu_Easy,
            this.DiffMenu_Normal,
            this.DiffMenu_Hard,
            this.DiffMenu_VeryHard});
            this.menuItem8.Text = "Computer Difficulty";
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
            // debugDropDownMenu
            // 
            this.debugDropDownMenu.Index = 2;
            this.debugDropDownMenu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.DebugSkip,
            this.DebugProcess,
            this.menuItem4});
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
            // menuItem4
            // 
            this.menuItem4.Index = 2;
            this.menuItem4.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.DebugScenario_NoWhite,
            this.DebugScenario_NoBlack,
            this.DebugScenario_MidGame});
            this.menuItem4.Text = "New Game Scenario";
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
            this.visualizeCheckbox.Location = new System.Drawing.Point(659, 7);
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
            // unusedGrid
            // 
            this.unusedGrid.Image = ((System.Drawing.Image)(resources.GetObject("unusedGrid.Image")));
            this.unusedGrid.Location = new System.Drawing.Point(30, 61);
            this.unusedGrid.Name = "unusedGrid";
            this.unusedGrid.Size = new System.Drawing.Size(320, 320);
            this.unusedGrid.TabIndex = 11;
            this.unusedGrid.TabStop = false;
            this.unusedGrid.Visible = false;
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
            // blackPieceImg
            // 
            this.blackPieceImg.Image = ((System.Drawing.Image)(resources.GetObject("blackPieceImg.Image")));
            this.blackPieceImg.InitialImage = null;
            this.blackPieceImg.Location = new System.Drawing.Point(150, 181);
            this.blackPieceImg.Name = "blackPieceImg";
            this.blackPieceImg.Size = new System.Drawing.Size(38, 38);
            this.blackPieceImg.TabIndex = 20;
            this.blackPieceImg.TabStop = false;
            this.blackPieceImg.Visible = false;
            // 
            // whitePieceImg
            // 
            this.whitePieceImg.Image = ((System.Drawing.Image)(resources.GetObject("whitePieceImg.Image")));
            this.whitePieceImg.InitialImage = null;
            this.whitePieceImg.Location = new System.Drawing.Point(190, 222);
            this.whitePieceImg.Name = "whitePieceImg";
            this.whitePieceImg.Size = new System.Drawing.Size(38, 38);
            this.whitePieceImg.TabIndex = 21;
            this.whitePieceImg.TabStop = false;
            this.whitePieceImg.Visible = false;
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
            this.AIInfoTabControl.Location = new System.Drawing.Point(383, 8);
            this.AIInfoTabControl.Name = "AIInfoTabControl";
            this.AIInfoTabControl.SelectedIndex = 0;
            this.AIInfoTabControl.Size = new System.Drawing.Size(375, 431);
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
            this.AIDBTab.Size = new System.Drawing.Size(367, 405);
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
            this.AISimTab.Size = new System.Drawing.Size(367, 405);
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
            this.CurrentTurnLabel.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CurrentTurnLabel.ForeColor = System.Drawing.Color.Black;
            this.CurrentTurnLabel.Location = new System.Drawing.Point(34, 390);
            this.CurrentTurnLabel.Name = "CurrentTurnLabel";
            this.CurrentTurnLabel.Size = new System.Drawing.Size(107, 49);
            this.CurrentTurnLabel.TabIndex = 3;
            this.CurrentTurnLabel.Text = "Current\r\nTurn";
            this.CurrentTurnLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // CurrentTurnImage
            // 
            this.CurrentTurnImage.Image = ((System.Drawing.Image)(resources.GetObject("CurrentTurnImage.Image")));
            this.CurrentTurnImage.InitialImage = null;
            this.CurrentTurnImage.Location = new System.Drawing.Point(96, 396);
            this.CurrentTurnImage.Name = "CurrentTurnImage";
            this.CurrentTurnImage.Size = new System.Drawing.Size(38, 38);
            this.CurrentTurnImage.TabIndex = 25;
            this.CurrentTurnImage.TabStop = false;
            this.CurrentTurnImage.Visible = false;
            // 
            // blackScoreBoardTitle
            // 
            this.blackScoreBoardTitle.Font = new System.Drawing.Font("Calibri", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.blackScoreBoardTitle.Location = new System.Drawing.Point(271, 390);
            this.blackScoreBoardTitle.Name = "blackScoreBoardTitle";
            this.blackScoreBoardTitle.Size = new System.Drawing.Size(79, 21);
            this.blackScoreBoardTitle.TabIndex = 26;
            this.blackScoreBoardTitle.Text = "Black:";
            this.blackScoreBoardTitle.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // whiteScoreBoardTitle
            // 
            this.whiteScoreBoardTitle.Font = new System.Drawing.Font("Calibri", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.whiteScoreBoardTitle.Location = new System.Drawing.Point(172, 390);
            this.whiteScoreBoardTitle.Name = "whiteScoreBoardTitle";
            this.whiteScoreBoardTitle.Size = new System.Drawing.Size(79, 21);
            this.whiteScoreBoardTitle.TabIndex = 27;
            this.whiteScoreBoardTitle.Text = "White:";
            this.whiteScoreBoardTitle.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // whiteScoreBoard
            // 
            this.whiteScoreBoard.Font = new System.Drawing.Font("Calibri", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.whiteScoreBoard.Location = new System.Drawing.Point(171, 404);
            this.whiteScoreBoard.Name = "whiteScoreBoard";
            this.whiteScoreBoard.Size = new System.Drawing.Size(79, 31);
            this.whiteScoreBoard.TabIndex = 28;
            this.whiteScoreBoard.Text = "0";
            this.whiteScoreBoard.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // blackScoreBoard
            // 
            this.blackScoreBoard.Font = new System.Drawing.Font("Calibri", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.blackScoreBoard.Location = new System.Drawing.Point(271, 404);
            this.blackScoreBoard.Name = "blackScoreBoard";
            this.blackScoreBoard.Size = new System.Drawing.Size(79, 31);
            this.blackScoreBoard.TabIndex = 29;
            this.blackScoreBoard.Text = "0";
            this.blackScoreBoard.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // AITurnWorker
            // 
            this.AITurnWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.AITurnMonitor_DoWork);
            this.AITurnWorker.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.AITurnMonitor_ProgressChanged);
            this.AITurnWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.AITurnMonitor_RunWorkerCompleted);
            // 
            // emptyPieceImg
            // 
            this.emptyPieceImg.Image = ((System.Drawing.Image)(resources.GetObject("emptyPieceImg.Image")));
            this.emptyPieceImg.InitialImage = null;
            this.emptyPieceImg.Location = new System.Drawing.Point(271, 262);
            this.emptyPieceImg.Name = "emptyPieceImg";
            this.emptyPieceImg.Size = new System.Drawing.Size(38, 38);
            this.emptyPieceImg.TabIndex = 33;
            this.emptyPieceImg.TabStop = false;
            this.emptyPieceImg.Visible = false;
            // 
            // boardXaxisLabel
            // 
            this.boardXaxisLabel.Image = ((System.Drawing.Image)(resources.GetObject("boardXaxisLabel.Image")));
            this.boardXaxisLabel.Location = new System.Drawing.Point(37, 41);
            this.boardXaxisLabel.Name = "boardXaxisLabel";
            this.boardXaxisLabel.Size = new System.Drawing.Size(320, 29);
            this.boardXaxisLabel.TabIndex = 34;
            this.boardXaxisLabel.TabStop = false;
            // 
            // boardYaxisLabel
            // 
            this.boardYaxisLabel.Image = ((System.Drawing.Image)(resources.GetObject("boardYaxisLabel.Image")));
            this.boardYaxisLabel.Location = new System.Drawing.Point(5, 62);
            this.boardYaxisLabel.Name = "boardYaxisLabel";
            this.boardYaxisLabel.Size = new System.Drawing.Size(27, 323);
            this.boardYaxisLabel.TabIndex = 35;
            this.boardYaxisLabel.TabStop = false;
            // 
            // hideDebugButton
            // 
            this.hideDebugButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.hideDebugButton.Location = new System.Drawing.Point(354, 192);
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
            this.DebugAITrace.Location = new System.Drawing.Point(760, 28);
            this.DebugAITrace.Name = "DebugAITrace";
            this.DebugAITrace.Size = new System.Drawing.Size(367, 411);
            this.DebugAITrace.TabIndex = 5;
            this.DebugAITrace.Text = "";
            // 
            // clearDebugLogButton
            // 
            this.clearDebugLogButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.clearDebugLogButton.Location = new System.Drawing.Point(764, 3);
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
            this.AITraceLabel.Location = new System.Drawing.Point(858, 2);
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
            this.debugLogCheckBox.Location = new System.Drawing.Point(1061, 5);
            this.debugLogCheckBox.Name = "debugLogCheckBox";
            this.debugLogCheckBox.Size = new System.Drawing.Size(67, 19);
            this.debugLogCheckBox.TabIndex = 38;
            this.debugLogCheckBox.Text = "Logging";
            this.debugLogCheckBox.UseVisualStyleBackColor = true;
            // 
            // ReversiForm
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(1132, 447);
            this.Controls.Add(this.debugLogCheckBox);
            this.Controls.Add(this.visualizeCheckbox);
            this.Controls.Add(this.AITraceLabel);
            this.Controls.Add(this.hideDebugButton);
            this.Controls.Add(this.clearDebugLogButton);
            this.Controls.Add(this.emptyPieceImg);
            this.Controls.Add(this.DebugAITrace);
            this.Controls.Add(this.whiteScoreBoardTitle);
            this.Controls.Add(this.blackScoreBoardTitle);
            this.Controls.Add(this.CurrentTurnImage);
            this.Controls.Add(this.AIInfoTabControl);
            this.Controls.Add(this.whitePieceImg);
            this.Controls.Add(this.blackPieceImg);
            this.Controls.Add(this.CurrentTurnLabel);
            this.Controls.Add(this.BoardPicture);
            this.Controls.Add(this.unusedGrid);
            this.Controls.Add(this.whiteScoreBoard);
            this.Controls.Add(this.blackScoreBoard);
            this.Controls.Add(this.boardXaxisLabel);
            this.Controls.Add(this.boardYaxisLabel);
            this.Controls.Add(this.Title);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Menu = this.mainDropDownMenu;
            this.Name = "ReversiForm";
            this.Text = "Reversi";
            ((System.ComponentModel.ISupportInitialize)(this.BoardPicture)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.unusedGrid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.blackPieceImg)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.whitePieceImg)).EndInit();
            this.AIInfoTabControl.ResumeLayout(false);
            this.AIDBTab.ResumeLayout(false);
            this.AIDBTab.PerformLayout();
            this.AISimTab.ResumeLayout(false);
            this.AISimTab.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.simulationDepthSlider)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.CurrentTurnImage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptyPieceImg)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.boardXaxisLabel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.boardYaxisLabel)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        // The main form constructor
        public ReversiForm()
        {
            InitializeComponent();
            gBoardGFX = BoardPicture.CreateGraphics();

            gDebugText = DebugAITrace;
            simTimerLabel.Text = "";

            gWhiteScoreBoard = whiteScoreBoard;
            gBlackScoreBoard = blackScoreBoard;
            gCurrentTurnImage = CurrentTurnImage;
            gCurrentTurnLabel = CurrentTurnLabel;
            gAITurnWorker = AITurnWorker;
            gDebugLogCheckBox = debugLogCheckBox;
            gSimTimerLabel = simTimerLabel;
            gNodeCounter = nodeCounter;
            gWorkCounter = workCounter;
            gVictoryCounter = victoryCounter;
            gBoardImage = BoardPicture.Image;
            gWhitePieceImage = whitePieceImg.Image;
            gBlackPieceImage = blackPieceImg.Image;
            gEmptyPieceImage = emptyPieceImg.Image;

            boardPieceImageSize = blackPieceImg.Width;

            boardGridSize = Properties.Settings.Default.GridSize;

            AITurnWorker.WorkerSupportsCancellation = true;
            AITurnWorker.WorkerReportsProgress = true;

            gridSizeDropDown.SelectedIndex = 4;

            updateMaxDepth();

            AIInfoTabControl.SelectTab(AISimTab);

            unusedGrid.SendToBack();
        }

        #region Global Variables

        // Static handles to graphical assets
        private static System.ComponentModel.ComponentResourceManager imgResourceHandle = new System.ComponentModel.ComponentResourceManager(typeof(ReversiForm));
        private static Image gBlackPieceImage;
        private static Image gWhitePieceImage;
        private static Image gEmptyPieceImage;
        private static Image gBoardImage;

        // Static handles to form objects
        private static RichTextBox gDebugText = new RichTextBox();
        private static Graphics gBoardGFX;
        private static Label gWhiteScoreBoard;
        private static Label gBlackScoreBoard;
        private static Label gCurrentTurnLabel;
        private static PictureBox gCurrentTurnImage;
        private static BackgroundWorker gAITurnWorker;
        private static CheckBox gDebugLogCheckBox;
        private static Label gSimTimerLabel;
        private static Label gNodeCounter;
        private static Label gWorkCounter;
        private static Label gVictoryCounter;

        // Piece dimensions
        private static int boardPieceImageSize;
        private static int boardGridSize;

        // The AI Database
        private AIDatabase DatabaseFactory = new AIDatabase();

        // The board used to track what has been drawn on the screen
        private static Board LastDrawnBoard = new Board();

        // The Global Game Object
        private static Game gCurrentGame = ReversiApplication.getCurrentGame();

        // Flags that determine who is playing (ai or human)
        private static Boolean PvC = true;
        private static int AIDifficulty = 1;

        #endregion

        #region Getters and Setters

        public static Board getLastDrawnBoard() { return LastDrawnBoard; }
        public static void setLastDrawnBoard(Board SourceBoard) { LastDrawnBoard.CopyBoard(SourceBoard); }
        public static void setLastDrawnBoard(int BoardSize) { LastDrawnBoard = new Board( BoardSize ); }

        public static Boolean isVsComputer() { return PvC; }

        public static int getAIDifficulty() { return AIDifficulty; }
        public static void setAIDifficulty(int setAIDifficulty) { AIDifficulty = setAIDifficulty; }

        public static void appendDebugText(String newDebugText) { gDebugText.Text += newDebugText; }
        public static void setDebugText( String newDebugText ) { gDebugText.Text = newDebugText; }
 
        #endregion

        // Resets the form elements to prepare for a new game
        private void StartNewGame()
        {
            gBlackScoreBoard.Text = "0";
            gWhiteScoreBoard.Text = "0";
            gCurrentTurnLabel.Text = "Current\nTurn";
            gCurrentTurnImage.Visible = true;

            // Reset the score board
            gBlackScoreBoard.Visible = true;
            gWhiteScoreBoard.Visible = true;
            CurrentTurnLabel.Visible = true;
            whiteScoreBoardTitle.Visible = true;
            blackScoreBoardTitle.Visible = true;
            CurrentTurnImage.Visible = true;

            ReversiApplication.resetCurrentGame(getBoardSize());
            gCurrentGame = ReversiApplication.getCurrentGame();

            gCurrentGame.getAI().setMaxDepth(simulationDepthSlider.Value);

            gCurrentGame.getAI().setVisualizeProcess(visualizeCheckbox.Checked);

            RefreshPieces(FullRefresh: true);

            UpdateTurnImage(gCurrentGame.getCurrentTurn());
            UpdateScoreBoard();
        }

        #region Game Board graphical manipulation methods

        public static void UpdateScoreBoard()
        {
            ReversiForm.gBlackScoreBoard.Text = gCurrentGame.getGameBoard().FindScore(ReversiApplication.BLACK).ToString();
            ReversiForm.gWhiteScoreBoard.Text = gCurrentGame.getGameBoard().FindScore(ReversiApplication.WHITE).ToString();
            ReversiForm.gBlackScoreBoard.Refresh();
            ReversiForm.gWhiteScoreBoard.Refresh();
        }

        // Redraw the piece images on the board
        public static void RefreshPieces(bool FullRefresh = false)
        {
            RefreshPieces(gCurrentGame.getGameBoard(), FullRefresh);
        }

        public static void RefreshPieces(Board SourceBoard, bool FullRefresh = false)
        {
            for (int Y = 0; Y < SourceBoard.getBoardSize(); Y++)
                for (int X = 0; X < SourceBoard.getBoardSize(); X++)
                    if (((SourceBoard.ColorAt(X, Y) != ReversiApplication.EMPTY) && 
                         (getLastDrawnBoard().ColorAt(X, Y) != SourceBoard.ColorAt(X, Y))) || FullRefresh)
                        DrawPiece(SourceBoard.ColorAt(X, Y), X, Y);

            getLastDrawnBoard().CopyBoard(SourceBoard.getBoardPieces());
        }

        public static void DrawPiece(int color, Point SourcePoint)
        {
            DrawPiece(color, SourcePoint.X, SourcePoint.Y);
        }

        public static void DrawPiece(int color, int X, int Y)
        {
            gBoardGFX.DrawImage(getTurnImage(color), X * boardGridSize + 1, Y * boardGridSize + 1, boardPieceImageSize, boardPieceImageSize);
        }

        public static void ClearBoardPieces()
        {
            gBoardGFX.DrawImage(gBoardImage, 0, 0, boardPieceImageSize, boardPieceImageSize);
        }

        public static void HighlightPiece(Color PieceColor, Point[] PieceList )
        {
            foreach (Point CurrentPiece in PieceList)
                HighlightPiece(PieceColor, CurrentPiece.X, CurrentPiece.Y );
        }

        public static void HighlightPiece(Color PieceColor, Point Piece, String PieceLabel = "")
        {
            HighlightPiece(PieceColor, Piece.X, Piece.Y, PieceLabel);
        }

        public static void HighlightPiece(Color PieceColor, int X, int Y, String PieceLabel = "")
        {
            gBoardGFX.DrawEllipse(new Pen(PieceColor, 3), X * boardGridSize + 3, Y * boardGridSize + 3, boardPieceImageSize - 6, boardPieceImageSize - 6);

            StringFormat sf = new StringFormat();
            sf.LineAlignment = StringAlignment.Center;
            sf.Alignment = StringAlignment.Center;

            if( PieceLabel != "" )
                gBoardGFX.DrawString(PieceLabel, new Font("Tahoma", (float)7, FontStyle.Regular), Brushes.White, new RectangleF(X * boardGridSize + 5, Y * boardGridSize + 14, boardGridSize - 10, boardGridSize - 28), sf);
        }

        public static Image getTurnImage(int color)
        {
            if (color == ReversiApplication.WHITE)
                return (gWhitePieceImage);
            else if (color == ReversiApplication.BLACK)
                return (gBlackPieceImage);
            else
                return (gEmptyPieceImage);
        }

        public static void UpdateTurnImage(int color)
        {
            if (color == ReversiApplication.WHITE)
                gCurrentTurnImage.CreateGraphics().DrawImage(getTurnImage(color), 0, 0, boardPieceImageSize, boardPieceImageSize);
            else
                gCurrentTurnImage.CreateGraphics().DrawImage(getTurnImage(color), 0, 0, boardPieceImageSize, boardPieceImageSize);
        }

        public static void ShowWinner(int WinningColor)
        {
            if (WinningColor == ReversiApplication.EMPTY)
            {
                gCurrentTurnLabel.Text = "Tie";
                gCurrentTurnImage.Visible = false;
            }
            else
            {
                gCurrentTurnLabel.Text = "Winner";
                UpdateTurnImage(WinningColor);
            }
        }

        #endregion

        #region Drop Down Menu Event Handelers

        // Easy AI difficulty selected
        private void DiffMenu_EasyClick(object sender, System.EventArgs e)
        {
            DiffMenu_Easy.Checked = true;
            DiffMenu_Normal.Checked = false;
            DiffMenu_Hard.Checked = false;
            DiffMenu_VeryHard.Checked = false;
            AIDifficulty = 0;
        }

        // Normal AI difficulty selected
        private void DiffMenu_NormalClick(object sender, System.EventArgs e)
        {
            DiffMenu_Easy.Checked = false;
            DiffMenu_Normal.Checked = true;
            DiffMenu_Hard.Checked = false;
            DiffMenu_VeryHard.Checked = false;
            AIDifficulty = 1;
        }

        // Hard AI difficulty selected
        private void DiffMenu_HardClick(object sender, System.EventArgs e)
        {
            DiffMenu_Easy.Checked = false;
            DiffMenu_Normal.Checked = false;
            DiffMenu_Hard.Checked = true;
            DiffMenu_VeryHard.Checked = false;
            AIDifficulty = 2;
        }

        // Very Hard AI difficulty selected
        private void DiffMenu_VeryHardClick(object sender, System.EventArgs e)
        {
            DiffMenu_Easy.Checked = false;
            DiffMenu_Normal.Checked = false;
            DiffMenu_Hard.Checked = false;
            DiffMenu_VeryHard.Checked = true;
            AIDifficulty = 3;
        }

        // Player vs Player selected
        private void PvPMenu_Click(object sender, System.EventArgs e)
        {
            PvPMenu.Checked = true;
            PvCMenu.Checked = false;
            PvC = false;
        }

        // Player vs AI selected
        private void PvCMenu_Click(object sender, System.EventArgs e)
        {
            PvPMenu.Checked = false;
            PvCMenu.Checked = true;
            PvC = true;
        }

        // Game exit selected
        private void ExitMenu_Click(object sender, System.EventArgs e)
        {
            ActiveForm.Close();
        }

        // New game selected
        private void NewGameMenu_Click(object sender, System.EventArgs e)
        {
            //CurrentGame = new Game( getBoardSize() );
            StartNewGame();
        }

        // Skip turn (debug option) selected
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

        // Start a new game scenario where white cannot move
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
            ReversiForm.StartAITurnWorker();
        }

        // Sets up the a new game with the middle of the board already filled in
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

        // Responds to the analyze database button press, starts the job
        private void BuildAIDBButton_Click(object sender, EventArgs e)
        {
            SetupSimulationForm();
            DBBuildWorker.RunWorkerAsync(getBoardSize());
        }

        // Starts the database background worker (button press)
        private void DBBuildWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            StartBuildDB(Convert.ToInt32(e.Argument.ToString()));
        }

        // Starts the database build background worker
        private void StartBuildDB(int BoardSize = 4)
        {
            ReversiApplication.resetCurrentGame(BoardSize);
            gCurrentGame = ReversiApplication.getCurrentGame();

            DatabaseFactory.BuildAIDatabase(DBBuildWorker, BoardSize, visualizeCheckbox.Checked, true);
        }

        // Called from within the database build background woker to report the progress of the build
        private void DBBuildWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {

        }

        public delegate void updateDatabaseProgressDelegate(TimeSpan TimeElapsed, int WorkNodeCount, int NodeTotal, int LeafCount);

        public static void updateDatabaseProgress(TimeSpan TimeElapsed, int WorkNodeCount, int NodeTotal, int LeafCount)
        {
            gNodeCounter.Invoke(new updateDatabaseProgressDelegate(updateDatabaseProgressForm), TimeElapsed, WorkNodeCount, NodeTotal, LeafCount);
        }

        private static void updateDatabaseProgressForm(TimeSpan TimeElapsed, int WorkNodeCount, int NodeTotal, int LeafCount)
        {
            gSimTimerLabel.Text = TimeElapsed.ToString(@"hh\:mm\:ss");
            gNodeCounter.Text = NodeTotal.ToString();
            gWorkCounter.Text = WorkNodeCount.ToString();
            gVictoryCounter.Text = LeafCount.ToString();
        }

        // Runs when the database background worker thread is finished
        private void DBBuildWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            ClearSimulationForm();
        }

        // Dumps the database information to the debug window
        private void dumpDBInfoButton_Click(object sender, EventArgs e)
        {
            gDebugText.Text += DatabaseFactory.DumpSimulationInfo();
        }

        // Responds to the analyze database button press
        private void anaylzeDBButton_Click(object sender, EventArgs e)
        {
            SetupSimulationForm();
            DBAnalysisWorker.RunWorkerAsync();
        }

        // Cancels any of the background jobs that are currently running
        private void cancelButton_Click(object sender, EventArgs e)
        {
            cancelAIWorkers();
        }

        private void cancelAIWorkers()
        {
            if (DBBuildWorker.IsBusy)
                DBBuildWorker.CancelAsync();

            if (DBAnalysisWorker.IsBusy)
                DBAnalysisWorker.CancelAsync();

            ClearSimulationForm();
        }

        // Starts of the database analysis background worker
        private void DBAnalysisWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            Boolean DisplayDebug = true;
            DatabaseFactory.AnalyzeAIDatabase(DBAnalysisWorker, visualizeCheckbox.Checked, gDebugText, DisplayDebug);
        }

        private void ClearSimulationForm()
        {
            simTimerLabel.Text = "";
            cancelButton.Visible = false;
            RAMCheckTimer.Enabled = false;
            RAMUsageBar.Visible = false;
            RAMLabel.Visible = false;
            RAMUsageBar.Maximum = 100;
        }

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
            CurrentTurnImage.Visible = false;
        }

        private void RAMCheckTimer_Tick(object sender, EventArgs e)
        {
            UpdateRAMprogress();
        }

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

        #region Miscellaneous Form Level Methods

        // Returns an integer board size as selected on the form
        private int getBoardSize()
        {
            return (Convert.ToInt32(gridSizeDropDown.Items[gridSizeDropDown.SelectedIndex].ToString()));
        }

        // Responds to the MouseUp event on the board image, processes the click as a placed piece
        private void PlaceUserPiece(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            int x = (e.X + 1) / boardGridSize;
            int y = (e.Y + 1) / boardGridSize;

            // Don't process the mouse click if there is a turn already being processed
            if (!gCurrentGame.getTurnInProgress())
                gCurrentGame.ProcessTurn(x, y);
        }

        // If the grid size drop down changes, updates the board with the new dimensions
        private void gridSizeDropDown_SelectedIndexChanged(object sender, EventArgs e)
        {
            gridDimensionLabel.Text = getBoardSize() + "x" + getBoardSize();

            BoardPicture.Width = boardGridSize * getBoardSize();
            BoardPicture.Height = boardGridSize * getBoardSize();

            StartNewGame();
        }

        // Starts a new game 100ms after the form has loaded
        private void NewGameTimer_Tick(object sender, EventArgs e)
        {
            StartNewGame();
            NewGameTimer.Enabled = false;
        }

        public delegate void setDebugTextDelagate(string newText);
        public delegate void appendDebugTextDelagate(string newText);

        public static void clearDebugMessage()
        {
            gDebugText.Invoke(new setDebugTextDelagate(setDebugText), "");
        }

        public static void reportDebugMessage(String newDebugMsg, bool updateConsole = false, bool updateWindow = true, bool overwrite = false)
        {
            if( gDebugLogCheckBox.Checked )
                if ( overwrite )
                    gDebugText.Invoke(new setDebugTextDelagate(setDebugText), newDebugMsg + Environment.NewLine);
                else
                    gDebugText.Invoke(new appendDebugTextDelagate(appendDebugText), newDebugMsg + Environment.NewLine);

            if (updateConsole)
                Console.WriteLine(newDebugMsg);
        }

        private void clearDebugLogButton_Click(object sender, EventArgs e)
        {
            clearDebugMessage();
        }

        private void hideDebugButton_Click(object sender, EventArgs e)
        {
            if (Width > 400)
            {
                Width = 400;
                hideDebugButton.Text = ">>\n>>\n>>";
                AIInfoTabControl.Visible = false;
            }
            else
            {
                Width = 1148;
                hideDebugButton.Text = "<<\n<<\n<<";
                AIInfoTabControl.Visible = true;
            }
        }

        #endregion

        #region AI Turn BG Worker Event Handelers

        public static void StartAITurnWorker()
        {
            gAITurnWorker.RunWorkerAsync();
        }

        public static void ReportAITurnWorkerProgress(int progress = 0)
        {
            ReversiForm.gAITurnWorker.ReportProgress(progress);
        }

        // Called asynchronously when it is time for the AI to wake up and do some work
        private void AITurnMonitor_DoWork(object sender, DoWorkEventArgs e)
        {
            gCurrentGame.ProcessAITurn();
        }

        // Called every time the AI monitor has a move to render
        private void AITurnMonitor_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            
        }

        // Called when the AI monitor has no more moves to place
        private void AITurnMonitor_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            gCurrentGame.SwitchTurn();
            UpdateScoreBoard();
            gCurrentGame.DetermineWinner();
            gCurrentGame.setTurnInProgress(false);
        }

        #endregion

        #region AI Simulation Tab event handlers

        private void simulationDepthSlider_Scroll(object sender, EventArgs e)
        {
            if (simulationDepthSlider.Value % 2 != 0)
                simulationDepthSlider.Value++;

            updateMaxDepth();
        }

        private void updateMaxDepth()
        {
            simDepthCount.Text = simulationDepthSlider.Value.ToString();
            gCurrentGame.getAI().setMaxDepth(simulationDepthSlider.Value - 1);
        }

        private void visualizeCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            gCurrentGame.getAI().setVisualizeProcess(visualizeCheckbox.Checked);
        }

        #endregion
    }
}
