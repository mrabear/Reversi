// Reversi
// Brian Hebert

using System;
using System.Drawing;
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
        public System.Windows.Forms.Timer NewGameTimer;
        private System.Windows.Forms.Label TurnLabel;
        private System.Windows.Forms.MainMenu mainMenu1;
        private System.Windows.Forms.MenuItem menuItem1;
        private System.Windows.Forms.MenuItem menuItem2;
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
        private System.Windows.Forms.MenuItem menuItem3;
        private System.Windows.Forms.MenuItem DebugSkip;
        private System.Windows.Forms.MenuItem DebugProcess;
        private System.Windows.Forms.Label ScoreText;
        public RichTextBox DebugAITrace;
        private System.Windows.Forms.Label AITraceLabel;

        private System.Windows.Forms.MenuItem menuItem4;
        private System.Windows.Forms.MenuItem DebugScenario_NoWhite;
        private System.Windows.Forms.MenuItem DebugScenario_NoBlack;
        private System.Windows.Forms.MenuItem DebugScenario_TieGame;
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
        public System.Windows.Forms.Timer RAMCheckTimer;
        private Label RAMLabel;
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
            this.TurnLabel = new System.Windows.Forms.Label();
            this.mainMenu1 = new System.Windows.Forms.MainMenu(this.components);
            this.menuItem1 = new System.Windows.Forms.MenuItem();
            this.NewGameMenu = new System.Windows.Forms.MenuItem();
            this.ExitMenu = new System.Windows.Forms.MenuItem();
            this.menuItem2 = new System.Windows.Forms.MenuItem();
            this.PvPMenu = new System.Windows.Forms.MenuItem();
            this.PvCMenu = new System.Windows.Forms.MenuItem();
            this.menuItem9 = new System.Windows.Forms.MenuItem();
            this.menuItem8 = new System.Windows.Forms.MenuItem();
            this.DiffMenu_Easy = new System.Windows.Forms.MenuItem();
            this.DiffMenu_Normal = new System.Windows.Forms.MenuItem();
            this.DiffMenu_Hard = new System.Windows.Forms.MenuItem();
            this.DiffMenu_VeryHard = new System.Windows.Forms.MenuItem();
            this.menuItem3 = new System.Windows.Forms.MenuItem();
            this.DebugSkip = new System.Windows.Forms.MenuItem();
            this.DebugProcess = new System.Windows.Forms.MenuItem();
            this.menuItem4 = new System.Windows.Forms.MenuItem();
            this.DebugScenario_NoWhite = new System.Windows.Forms.MenuItem();
            this.DebugScenario_NoBlack = new System.Windows.Forms.MenuItem();
            this.DebugScenario_TieGame = new System.Windows.Forms.MenuItem();
            this.ScoreText = new System.Windows.Forms.Label();
            this.DebugAITrace = new System.Windows.Forms.RichTextBox();
            this.AITraceLabel = new System.Windows.Forms.Label();
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
            ((System.ComponentModel.ISupportInitialize)(this.BoardPicture)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.unusedGrid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.blackPieceImg)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.whitePieceImg)).BeginInit();
            this.SuspendLayout();
            // 
            // BoardPicture
            // 
            this.BoardPicture.Image = ((System.Drawing.Image)(resources.GetObject("BoardPicture.Image")));
            this.BoardPicture.Location = new System.Drawing.Point(20, 56);
            this.BoardPicture.Name = "BoardPicture";
            this.BoardPicture.Size = new System.Drawing.Size(320, 320);
            this.BoardPicture.TabIndex = 0;
            this.BoardPicture.TabStop = false;
            this.BoardPicture.MouseUp += new System.Windows.Forms.MouseEventHandler(this.PlaceUserPiece);
            // 
            // Title
            // 
            this.Title.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Title.Location = new System.Drawing.Point(20, 16);
            this.Title.Name = "Title";
            this.Title.Size = new System.Drawing.Size(320, 32);
            this.Title.TabIndex = 1;
            this.Title.Text = "Reversi";
            this.Title.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // TurnLabel
            // 
            this.TurnLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TurnLabel.Location = new System.Drawing.Point(188, 388);
            this.TurnLabel.Name = "TurnLabel";
            this.TurnLabel.Size = new System.Drawing.Size(88, 41);
            this.TurnLabel.TabIndex = 3;
            // 
            // mainMenu1
            // 
            this.mainMenu1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItem1,
            this.menuItem2,
            this.menuItem3});
            // 
            // menuItem1
            // 
            this.menuItem1.Index = 0;
            this.menuItem1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.NewGameMenu,
            this.ExitMenu});
            this.menuItem1.Text = "File";
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
            // menuItem2
            // 
            this.menuItem2.Index = 1;
            this.menuItem2.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.PvPMenu,
            this.PvCMenu,
            this.menuItem9,
            this.menuItem8});
            this.menuItem2.Text = "Game Setup";
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
            // menuItem3
            // 
            this.menuItem3.Index = 2;
            this.menuItem3.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.DebugSkip,
            this.DebugProcess,
            this.menuItem4});
            this.menuItem3.Text = "Debug";
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
            this.DebugScenario_TieGame});
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
            // DebugScenario_TieGame
            // 
            this.DebugScenario_TieGame.Index = 2;
            this.DebugScenario_TieGame.Text = "Tie Game";
            // 
            // ScoreText
            // 
            this.ScoreText.Location = new System.Drawing.Point(31, 388);
            this.ScoreText.Name = "ScoreText";
            this.ScoreText.Size = new System.Drawing.Size(88, 41);
            this.ScoreText.TabIndex = 4;
            // 
            // DebugAITrace
            // 
            this.DebugAITrace.HideSelection = false;
            this.DebugAITrace.Location = new System.Drawing.Point(364, 241);
            this.DebugAITrace.Name = "DebugAITrace";
            this.DebugAITrace.Size = new System.Drawing.Size(357, 185);
            this.DebugAITrace.TabIndex = 5;
            this.DebugAITrace.Text = "";
            // 
            // AITraceLabel
            // 
            this.AITraceLabel.Location = new System.Drawing.Point(490, 224);
            this.AITraceLabel.Name = "AITraceLabel";
            this.AITraceLabel.Size = new System.Drawing.Size(112, 16);
            this.AITraceLabel.TabIndex = 6;
            this.AITraceLabel.Text = "AI Debug Trace";
            this.AITraceLabel.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // BuildAIDBButton
            // 
            this.BuildAIDBButton.Location = new System.Drawing.Point(6, 23);
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
            this.visualizeCheckbox.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.visualizeCheckbox.Checked = true;
            this.visualizeCheckbox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.visualizeCheckbox.Location = new System.Drawing.Point(129, 56);
            this.visualizeCheckbox.Name = "visualizeCheckbox";
            this.visualizeCheckbox.Size = new System.Drawing.Size(106, 17);
            this.visualizeCheckbox.TabIndex = 8;
            this.visualizeCheckbox.Text = "visualize process";
            this.visualizeCheckbox.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.gridDimensionLabel);
            this.groupBox1.Controls.Add(this.gridSizeDropDown);
            this.groupBox1.Controls.Add(this.dumpDBInfoButton);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.anaylzeDBButton);
            this.groupBox1.Controls.Add(this.BuildAIDBButton);
            this.groupBox1.Controls.Add(this.visualizeCheckbox);
            this.groupBox1.Location = new System.Drawing.Point(367, 8);
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
            this.gridDimensionLabel.Location = new System.Drawing.Point(200, 33);
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
            this.gridSizeDropDown.Location = new System.Drawing.Point(147, 29);
            this.gridSizeDropDown.Name = "gridSizeDropDown";
            this.gridSizeDropDown.Size = new System.Drawing.Size(41, 21);
            this.gridSizeDropDown.TabIndex = 14;
            this.gridSizeDropDown.SelectedIndexChanged += new System.EventHandler(this.gridSizeDropDown_SelectedIndexChanged);
            // 
            // dumpDBInfoButton
            // 
            this.dumpDBInfoButton.Location = new System.Drawing.Point(127, 78);
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
            this.label1.Location = new System.Drawing.Point(134, 13);
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
            this.cancelButton.Location = new System.Drawing.Point(634, 20);
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
            this.unusedGrid.Location = new System.Drawing.Point(20, 56);
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
            this.simTimerLabel.Location = new System.Drawing.Point(616, 83);
            this.simTimerLabel.Name = "simTimerLabel";
            this.simTimerLabel.Size = new System.Drawing.Size(98, 34);
            this.simTimerLabel.TabIndex = 12;
            this.simTimerLabel.Text = "01:12:12";
            this.simTimerLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // nodeCounter
            // 
            this.nodeCounter.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nodeCounter.Location = new System.Drawing.Point(364, 139);
            this.nodeCounter.Name = "nodeCounter";
            this.nodeCounter.Size = new System.Drawing.Size(124, 32);
            this.nodeCounter.TabIndex = 13;
            this.nodeCounter.Text = "0";
            this.nodeCounter.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // workCounter
            // 
            this.workCounter.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.workCounter.Location = new System.Drawing.Point(608, 139);
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
            this.nodeCounterLabel.Location = new System.Drawing.Point(379, 126);
            this.nodeCounterLabel.Name = "nodeCounterLabel";
            this.nodeCounterLabel.Size = new System.Drawing.Size(94, 13);
            this.nodeCounterLabel.TabIndex = 16;
            this.nodeCounterLabel.Text = "Total Nodes in DB";
            // 
            // workCounterLabel
            // 
            this.workCounterLabel.AutoSize = true;
            this.workCounterLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.workCounterLabel.Location = new System.Drawing.Point(622, 126);
            this.workCounterLabel.Name = "workCounterLabel";
            this.workCounterLabel.Size = new System.Drawing.Size(84, 13);
            this.workCounterLabel.TabIndex = 17;
            this.workCounterLabel.Text = "Nodes in Queue";
            // 
            // victoryCounterLabel
            // 
            this.victoryCounterLabel.AutoSize = true;
            this.victoryCounterLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.victoryCounterLabel.Location = new System.Drawing.Point(510, 126);
            this.victoryCounterLabel.Name = "victoryCounterLabel";
            this.victoryCounterLabel.Size = new System.Drawing.Size(74, 13);
            this.victoryCounterLabel.TabIndex = 19;
            this.victoryCounterLabel.Text = "Total Victories";
            // 
            // victoryCounter
            // 
            this.victoryCounter.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.victoryCounter.Location = new System.Drawing.Point(486, 139);
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
            this.blackPieceImg.Location = new System.Drawing.Point(140, 176);
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
            this.whitePieceImg.Location = new System.Drawing.Point(180, 217);
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
            this.RAMUsageBar.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.RAMUsageBar.Location = new System.Drawing.Point(364, 191);
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
            this.RAMLabel.Location = new System.Drawing.Point(364, 169);
            this.RAMLabel.Name = "RAMLabel";
            this.RAMLabel.Size = new System.Drawing.Size(84, 25);
            this.RAMLabel.TabIndex = 23;
            this.RAMLabel.Text = "Memory:";
            this.RAMLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.RAMLabel.Visible = false;
            // 
            // ReversiForm
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(733, 438);
            this.Controls.Add(this.RAMUsageBar);
            this.Controls.Add(this.whitePieceImg);
            this.Controls.Add(this.blackPieceImg);
            this.Controls.Add(this.victoryCounterLabel);
            this.Controls.Add(this.victoryCounter);
            this.Controls.Add(this.workCounterLabel);
            this.Controls.Add(this.nodeCounterLabel);
            this.Controls.Add(this.workCounter);
            this.Controls.Add(this.nodeCounter);
            this.Controls.Add(this.simTimerLabel);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.AITraceLabel);
            this.Controls.Add(this.DebugAITrace);
            this.Controls.Add(this.ScoreText);
            this.Controls.Add(this.TurnLabel);
            this.Controls.Add(this.Title);
            this.Controls.Add(this.BoardPicture);
            this.Controls.Add(this.unusedGrid);
            this.Controls.Add(this.RAMLabel);
            this.MaximizeBox = false;
            this.Menu = this.mainMenu1;
            this.Name = "ReversiForm";
            this.Text = "Reversi";
            ((System.ComponentModel.ISupportInitialize)(this.BoardPicture)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.unusedGrid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.blackPieceImg)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.whitePieceImg)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.Run(new ReversiForm());
        }
        #endregion

        // The main form constructor
        public ReversiForm()
        {
            InitializeComponent();
            BoardGFX = BoardPicture.CreateGraphics();

            DebugText = DebugAITrace;
            TurnLabelText = TurnLabel;
            simTimerLabel.Text = "";
            gridSizeDropDown.SelectedIndex = 4;
            unusedGrid.SendToBack();
        }

        #region Global Variables

        // Color constants
        public static int BLACK = Properties.Settings.Default.BLACK;
        public static int WHITE = Properties.Settings.Default.WHITE;
        public static int EMPTY = Properties.Settings.Default.EMPTY;

        // Static handles to graphical assets
        private static System.ComponentModel.ComponentResourceManager imgResourceHandle = new System.ComponentModel.ComponentResourceManager(typeof(ReversiForm));
        private static Image BlackPieceImage = ((System.Drawing.Image)(imgResourceHandle.GetObject("blackPieceImg.Image")));
        private static Image WhitePieceImage = ((System.Drawing.Image)(imgResourceHandle.GetObject("whitePieceImg.Image")));
        private static Image BoardImage = ((System.Drawing.Image)(imgResourceHandle.GetObject("BoardPicture.Image")));

        // Static handles to form objects
        private static Label TurnLabelText = new Label();
        private static RichTextBox DebugText = new RichTextBox();
        private static Graphics BoardGFX;

        // The board used to track what has been drawn on the screen
        private static Board LastDrawnBoard = new Board();

        // The Global Game Object
        private static Game CurrentGame;

        // Flags that determine who is playing (ai or human)
        private static Boolean PvC = true;
        private static int AIDifficulty = 1;

        #endregion

        // Class:       Board
        // Description: Stores the game board and all of the methods to manipulate it
        private class Board
		{
            // The array of pieces that represents the board state
			public int[,] BoardPieces;

            // The size of the board, which will always be BoardSize x BoardSize
            public int BoardSize;

            // The default constructor creates an 8x8 board
			public Board( )
			{
                this.BoardSize = 8;
                BoardPieces = new int[BoardSize, BoardSize];
                ClearBoard();
                PlaceStartingPieces();
			}

            // Create an NxN board (min size 4)
            public Board(int SourceSize)
            {
                BoardSize = Math.Max( 4, SourceSize );
                BoardPieces = new int[BoardSize, BoardSize];
                ClearBoard();
                PlaceStartingPieces();
            }

            // Create a board using a reference to another board as a template
            public Board(ref Board SourceBoard)
            {
                BoardSize = SourceBoard.BoardSize;
                BoardPieces = new int[BoardSize, BoardSize];
                this.CopyBoard(ref SourceBoard);
            }

            // Create a board using another board as a template
            public Board(Board SourceBoard)
            {
                BoardSize = SourceBoard.BoardSize;
                BoardPieces = new int[BoardSize, BoardSize];
                this.CopyBoard(ref SourceBoard);
            }

            // Returns true if the piece is within the bounds of the game board
			public Boolean InBounds( int x, int y )
			{
                if ((x > BoardSize - 1) || (y > BoardSize - 1) || (x < 0) || (y < 0)) 
					return false;
				else
					return true;
			}

            // Copies the content of one board to another
            public void CopyBoard( ref int[,] NewBoardPieces)
            {
                Array.Copy( NewBoardPieces, BoardPieces, NewBoardPieces.Length );
            }

            // Overload of copyboard that takes a source board as input
			public void CopyBoard( ref Board SourceBoard )
			{
                CopyBoard( ref SourceBoard.BoardPieces );
			}

            // Overrides the default ToString method to return a string representation of the board
			public override String ToString()
			{
                return(BuildBoardString());
			}

            // Returns a string representation of the board
            public String BuildBoardString(Boolean SingleLine = false)
            {
                string boardString = "";

                for (int Y = 0; Y < BoardSize; Y++)
                {
                    for (int X = 0; X < BoardSize; X++)
                    {
                        boardString += ColorAt(X, Y);
                    }

                    if (!SingleLine)
                        boardString += "\n";
                }

                return boardString;
            }

            // Returns a unique identifier for a specific board state and turn
            public String GetID(int CurrentTurn)
            {
                return (CurrentTurn + this.GetID());
            }

            // Returns a unique identifier for a specific board state irrespective of turn
            public String GetID()
            {
                return(BuildBoardString(true));
            }

            // Returns the color at the given board location
            public int ColorAt(int x, int y)
            {
                if ((x < 0) || (x > BoardSize - 1) || (y < 0) || (y > BoardSize - 1))
                    return EMPTY;

                return(BoardPieces[x, y]);
            }

            // Attempts to process the implications of a legal move and updates the board if ProcessMove = true
			public Boolean MakeMove( int x, int y, int color, Boolean ProcessMove = true )
			{
				int CurrentTurn = color ;
				int NextTurn;

				if ( CurrentTurn == WHITE )
					NextTurn = BLACK; 
				else 
					NextTurn = WHITE;

				// Check for already existing piece
				if( ColorAt( x, y ) != EMPTY )
					return false;

				Boolean findStatus = false;
				Boolean takeStatus = false;
			
                for (int olc = Math.Max(y - 1, 0); olc <= Math.Min(y + 1, BoardSize - 1); olc++)
				{
                    for (int ilc = Math.Max(x - 1, 0); ilc <= Math.Min(x + 1, BoardSize - 1); ilc++)
					{
                        if (ColorAt(ilc, olc) == (NextTurn))
						{
							findStatus = true;

							int newX = ilc;
							int newY = olc;
							int dirX = ilc - x;
							int dirY = olc - y;

							Board TempBoard = new Board( this );

                            while (TempBoard.ColorAt(newX, newY) == NextTurn)
							{
								TempBoard.PutPiece( newX, newY, CurrentTurn );
								newX += dirX;
								newY += dirY;
							
								if ( !TempBoard.InBounds( newX, newY ) )
									break;
							}

							if ( TempBoard.InBounds( newX, newY ) )
							{
                                if (TempBoard.ColorAt(newX, newY) == CurrentTurn)
								{
                                    if ( ProcessMove )
									{
										TempBoard.PutPiece( x, y, color );
										CopyBoard( ref TempBoard );
									}

                                    takeStatus = true;
								}
							}
						} 
					}
				}

                if (( !findStatus ) && ( ProcessMove ))
				{
					//DebugText.Text = "You must place your piece adjacent to an opponents piece.";
				}
                if (( !takeStatus ) && ( ProcessMove ))
				{
					//DebugText.Text = "You must place capture at least one piece on each turn.";
				}

				return takeStatus;
			}

            // Returns a list of all available moves for a given player
			public Point[] AvailableMoves( int CurrentTurn )
			{
				Point[] Moves = new Point[64];
				int foundMoves = 0;
                for (int Y = 0; Y < BoardSize; Y++)
                    for (int X = 0; X < BoardSize; X++)
                        if (ColorAt(X, Y) == EMPTY)
							if( MakeMove( X, Y, CurrentTurn, false ) )
							{
								Moves[ foundMoves ] = new Point( X, Y ); 
								foundMoves++;
							}

				Point[] FinalMoves = new Point[ foundMoves ];
                for (int index = 0; index < FinalMoves.Length; index++)
                    FinalMoves[index] = Moves[index];

				return FinalMoves;				
			}

            // Returns true if a move is possible for the given player
			public Boolean MovePossible( int color )
			{
				for( int Y = 0; Y < BoardSize; Y++ )
					for( int X = 0; X < BoardSize; X++ )
                        if (ColorAt(X, Y) == EMPTY)
							if( MakeMove( X, Y, color, false ) )
								return true;

				return false;
			}

            // Places a piece at the given location
			public void PutPiece( int x, int y, int color )
			{
				if ( ( color == WHITE ) || ( color == BLACK ) )
					BoardPieces[x,y] = color;
                else
                    BoardPieces[x, y] = EMPTY;
			}

            // Empty the board
			public void ClearBoard()
			{
                Array.Clear(BoardPieces, 0, BoardSize * BoardSize);
			}

            // Initialize the game board with the starting pieces
            public void PlaceStartingPieces()
            {
                if (BoardSize == 8)
                {
                    PutPiece(3, 3, WHITE);
                    PutPiece(4, 4, WHITE);
                    PutPiece(3, 4, BLACK);
                    PutPiece(4, 3, BLACK);
                }
                else if (BoardSize == 6)
                {
                    PutPiece(2, 2, WHITE);
                    PutPiece(3, 3, WHITE);
                    PutPiece(2, 3, BLACK);
                    PutPiece(3, 2, BLACK);
                }
                else
                {
                    PutPiece(1, 1, WHITE);
                    PutPiece(2, 2, WHITE);
                    PutPiece(1, 2, BLACK);
                    PutPiece(2, 1, BLACK);
                }
            }

            // Redraw the piece images on the board
			public void RefreshPieces()
			{
                //Board lastdrawn = new Board(LastDrawnBoard);
                Image PieceImage = WhitePieceImage;

				for( int Y = 0; Y < BoardSize; Y++ )
				{	
					for( int X = 0; X < BoardSize; X++ )
					{
                        if (ColorAt(X, Y) != EMPTY)
                            if (LastDrawnBoard.ColorAt(X, Y) != this.ColorAt(X, Y) )
                            {
                                // Choose the piece image
                                if (ColorAt(X, Y) == BLACK)
                                    PieceImage = BlackPieceImage;
                                else
                                    PieceImage = WhitePieceImage;

                                // Draw the new piece
                                BoardGFX.DrawImage(PieceImage, X * 40 + 1, Y * 40 + 1, PieceImage.Width, PieceImage.Height);
                            }
					}
				}

                LastDrawnBoard.CopyBoard( ref BoardPieces );
			}

            // Return the score of the given player
			public int FindScore( int colorToCheck )
			{
				int score = 0;
				for( int Y = 0; Y < BoardSize; Y++ )
					for( int X = 0; X < BoardSize; X++ )
                        if (ColorAt(X, Y) == colorToCheck)
							score++;

				return score ;
			}
			
		}

        // Class:       AI
        // Description: Stores the game simulation code used by the AI opponent to play the game
		private class AI
		{
			public string AIDebug = "";
			public int color;

            //Dictionary<string, int> WhiteMoves = new Dictionary<string, int>();
            public Dictionary<string, int> BlackMoves = new Dictionary<string, int>();

            // Represents a sinlge game state with N-number of connections to and from the tree of all possible game states
            public class SimulationNode
            {
                public String   NodeID;                 // The unique identifier of this node

                public Point[]  AvailableMoves;         // The list available moves that haven't been simulated yet

                public Boolean  isLeaf;                 // TRUE if the node represnts a game end state
                public Boolean  isTrunk;                // TRUE if the node is the initial game starting position
                public Boolean  isPassTurn;             // TRUE if the node represents a game board where the current player has to pass

                public Board    GameBoard;              // The board state that this node was generated from

                public int      Turn;                   // The player who is moving in this node

                public int      WhiteWins;              // The potential number of White victory states that this node can lead to
                public int      BlackWins;              // The potential number of Black victory states that this node can lead to

                public List<String>    ChildNodes;      // The list of game nodes that can be created from the current one (i.e. player moves from the current state to one of the children)
                public List<String>    ParentNodes;     // The list of game nodes that can create the current one (i.e. player moves from one of the parent states to the current state)

                public SimulationNode( Board SourceBoard, int SourceTurn, Boolean SetTrunk = false, Boolean SetLeaf = false )
                {
                    // Initialize variable defaults
                    this.Initialize();

                    // Map constructor inputs to variables
                    Turn = SourceTurn;
                    GameBoard  = new Board( ref SourceBoard );
                    isTrunk = SetTrunk;
                    isLeaf = SetLeaf;

                    // Generate a list of all possible moves for the given player
                    AvailableMoves = GameBoard.AvailableMoves(Turn);

                    // Generate a unique ID for the node
                    NodeID = GameBoard.GetID(Turn);
                }

                public void AddParentNode(String NodeID)
                {
                    ParentNodes.Add(NodeID);
                }

                public void AddChildNode(String NodeID)
                {
                    ChildNodes.Add(NodeID);
                }

                public Boolean ContainsChild(String NodeID)
                {
                    return( ChildNodes.Contains(NodeID) );
                }

                public Boolean ContainsParent(String NodeID)
                {
                    return (ParentNodes.Contains(NodeID));
                }

                public void ClearMoves()
                {
                    AvailableMoves = new Point[0];
                }

                public void Initialize()
                {
                    BlackWins = 0;
                    WhiteWins = 0;
                    ChildNodes = new List<String>();
                    ParentNodes = new List<String>();
                    isLeaf = false;
                    isTrunk = false;
                }
            }

            public Board SimulationBoard;

            public Dictionary<string, SimulationNode> NodeMasterList = new Dictionary<string, SimulationNode>();

            public Queue<String> WorkNodes = new Queue<String>();
            public Queue<String> LeafNodes = new Queue<String>();

            public int SimulationCycles = 0;
            public int SimulationDepth = 0;
            public int WinnerTotal = 0;
            public int WhiteWinnerTotal = 0;
            public int BlackWinnerTotal = 0;
            public int TieTotal = 0;
            public int LoserTotal = 0;
            public int LeafTotal = 0;

			public AI( int AIcolor )
			{		
				color = AIcolor;	
			}

			public Point Move( ref Game SourceGame )
			{
				AIDebug = "---------------------\nStarting AI Move Sequence:\nAI is " +
                          SourceGame.GetTurnString(color) + "\n" +
                          "AI is set to difficulty level " + SourceGame.Difficulty + "\n" +
                          "\nInherited Game Board:\n" + SourceGame.GameBoard.ToString() + "\n";

                Point[] PossibleMoves = SourceGame.GameBoard.AvailableMoves(SourceGame.CurrentTurn);

				AIDebug += "\nPossible Moves:\n";
				for( int lc = 0 ; lc < PossibleMoves.Length ; lc++ )
				{
					AIDebug += "(" + PossibleMoves[ lc ].X + "," + PossibleMoves[ lc ].Y + ")\n";
				}
	
				AIDebug += "\nTotal Possible Moves: " + PossibleMoves.Length + "\n";
				if( PossibleMoves.Length < 1 )
				{
					return new Point( -1, -1 );
				}
				
				// This is just a gameplay hack to get by...all the AI is doing at this point is
				// gathering a list of all possible moves and then picking the first move off of
				// that list.  This line should be replaced with algorithims to determine which
				// of the available moves is best.
           		Point ChosenMove = PossibleMoves[0];

				AIDebug += "\nMove Chosen: (" + ChosenMove.X + "," + ChosenMove.Y + ")\n";

				return ChosenMove;
			}

            public String DumpSimulationInfo()
            {
                return( "============================\n" +
                        "Dumping AI DB Info\n" + 
                        "============================\n" +
                        "Total Nodes: " + NodeMasterList.Count + "\n" +
                        "Total Leaf Nodes: " + LeafTotal + "\n" +
                        "Total Black Winners: " + BlackWinnerTotal + "\n" +
                        "Total White Winners: " + WhiteWinnerTotal + "\n" );
            }

            public void BuildAIDatabase(BackgroundWorker WorkerThread, int BoardSize = 8, Boolean VisualizeResults = false, Boolean DisplayDebug = true)
            {
                /////////////////////////////////////////////////////////////
                //DEBUG BULLSHIT
                /////////////////////////////////////////////////////////////
                SimulationDepth = 0;
                SimulationCycles = 0;
                
                DateTime SimulationClock = DateTime.Now;

                if (DisplayDebug)
                {
                    Console.WriteLine("===============================\nBuilding AI Database (" + SimulationClock.ToLocalTime() + ")");
                    WorkerThread.ReportProgress(Convert.ToInt32(DateTime.Now.Subtract(SimulationClock).Ticks), "===============================\nBuilding AI Database (" + SimulationClock.ToLocalTime() + ")\n");
                }
                /////////////////////////////////////////////////////////////

                // Reset the database and work queues
                LeafNodes = new Queue<String>();
                WorkNodes = new Queue<String>();
                NodeMasterList = new Dictionary<string, SimulationNode>();

                //Board CurrentBoard = new Board();
                int ParentTurn = WHITE;
                SimulationNode ParentNode = new SimulationNode(new Board(BoardSize), ParentTurn);
                String ParentNodeID = ParentNode.NodeID;
                String RootNodeID = ParentNode.NodeID;

                int ChildTurn = BLACK;
                //String ChildNodeID;
                Board  ChildBoard;
                SimulationNode ChildNode;

                // Seed the master node list with the the root node that contains the default game positions and settings
                NodeMasterList.Add(RootNodeID, ParentNode);
                
                // Seed the work list with the root node
                WorkNodes.Enqueue(RootNodeID);

                while( WorkNodes.Count > 0 )
                {

                    /////////////////////////////////////////////////////////////
                    //DEBUG BULLSHIT
                    /////////////////////////////////////////////////////////////
                    SimulationCycles++;

                    if( DisplayDebug ){
                        //if (NodeMasterList.Count % 25000 == 0)
                        //    Console.WriteLine("(" + NodeMasterList.Count + ") (" + WorkNodes.Count + " queued) (" + LeafTotal + " end states)"); 
                    }

                    // If the BackgroundWorker.CancellationPending property is true, cancel
                    if (WorkerThread.CancellationPending)
                    {
                        Console.WriteLine("#####Database Build has been cancelled#####");
                        break;
                    }

                    if (SimulationCycles % 75 == 0)
                        WorkerThread.ReportProgress(Convert.ToInt32(DateTime.Now.Subtract(SimulationClock).Ticks), "");  

                    // Grab the next node ID off of the work queue
                    ParentNodeID = WorkNodes.Dequeue();

                    // Fetch the current game node from the master list
                    ParentNode = NodeMasterList[ParentNodeID];

                    // Set the child turn to be the next player
                    ChildTurn = ( ParentNode.Turn == WHITE ) ? BLACK : WHITE;

                    //if (NodeMasterList.Count % 10 == 0)
                    //Console.WriteLine("Turn " + (ParentNode.Turn == WHITE ? "White" : "Black") + "\nScore: B-" + ParentNode.Board.FindScore(BLACK) + " W-" + ParentNode.Board.FindScore(WHITE)  + "\n======================\n" + ParentNode.Board.ToString() );

                    // Update the game board visual
                    if( VisualizeResults )
                        ParentNode.GameBoard.RefreshPieces();

                    if (ParentNode.AvailableMoves.Length == 0)
                    {
                        ParentNode.isPassTurn = true;

                        ChildNode = new SimulationNode(ParentNode.GameBoard, ChildTurn);

                        if (ChildNode.AvailableMoves.Length > 0)
                        {

                            if (NodeMasterList.ContainsKey(ChildNode.NodeID))
                            {
                                // Since the node already exists, just add the current parent to it's parent node list
                                if (!NodeMasterList[ChildNode.NodeID].ContainsParent(ParentNode.NodeID))
                                    NodeMasterList[ChildNode.NodeID].AddParentNode(ParentNode.NodeID);
                            }
                            else
                            {
                                // Add the new node to the master list
                                NodeMasterList.Add(ChildNode.NodeID, ChildNode);

                                // Add the new node to the work list for eventual processing
                                WorkNodes.Enqueue(ChildNode.NodeID);
                            }

                            // Add this child to the parent's child node list
                            if (!ParentNode.ContainsChild(ChildNode.NodeID))
                                ParentNode.AddChildNode(ChildNode.NodeID);

                            // Add the new node to the work list for eventual processing
                            WorkNodes.Enqueue(ChildNode.NodeID);
                        }
                        else
                        {
                            ParentNode.isLeaf = true;
                            LeafTotal++;

                            if (ParentNode.GameBoard.FindScore(BLACK) > ParentNode.GameBoard.FindScore(WHITE))
                            {
                                ParentNode.BlackWins++;
                                BlackWinnerTotal++;
                            }
                            else if (ParentNode.GameBoard.FindScore(BLACK) < ParentNode.GameBoard.FindScore(WHITE))
                            {
                                ParentNode.WhiteWins++;
                                WhiteWinnerTotal++;
                            }
                            else
                            {
                                TieTotal++;
                            }
                        }
                    }
                    else
                    {
                        foreach (Point CurrentMove in ParentNode.AvailableMoves)
                        {
                            ChildBoard = new Board(ref ParentNode.GameBoard);
                            ChildBoard.PutPiece(CurrentMove.X, CurrentMove.Y, ChildTurn);
                            ChildNode = new SimulationNode(ChildBoard, ChildTurn);

                            // Add this child to the parent's child node list
                            if (!ParentNode.ContainsChild(ChildNode.NodeID))
                                ParentNode.AddChildNode(ChildNode.NodeID);

                            if (NodeMasterList.ContainsKey(ChildNode.NodeID))
                            {
                                // Since the node already exists, just add the current parent to it's parent node list
                                if (!NodeMasterList[ChildNode.NodeID].ContainsParent(ParentNode.NodeID))
                                    NodeMasterList[ChildNode.NodeID].AddParentNode(ParentNode.NodeID);
                            }
                            else
                            {
                                // Add the new node to the master list
                                NodeMasterList.Add(ChildNode.NodeID, ChildNode);

                                // Add the new node to the work list for eventual processing
                                WorkNodes.Enqueue(ChildNode.NodeID);
                            }
                        }
                    }
                    // Clear all of the moves from the nodes working list
                    ParentNode.ClearMoves();
                }

                /////////////////////////////////////////////////////////////
                //DEBUG BULLSHIT
                /////////////////////////////////////////////////////////////
                if (DisplayDebug)
                {
                    TimeSpan SimulationElapsedTime = DateTime.Now.Subtract(SimulationClock);
                    Console.WriteLine("===============================\nAI DB Build Complete\nSimulation Time: " + SimulationElapsedTime.ToString() + "\n\n" + DumpSimulationInfo());
                    //DebugTextBox.Text += "===============================\nAI DB Build Complete\nSimulation Time: " + SimulationElapsedTime.ToString() + "\n\n" + DumpSimulationInfo();
                    WorkerThread.ReportProgress(Convert.ToInt32(DateTime.Now.Subtract(SimulationClock).Ticks), "===============================\nAI DB Build Complete\nSimulation Time: " + SimulationElapsedTime.ToString() + "\n\n" + DumpSimulationInfo());
                }
            }

            public void AnalyzeAIDatabase(BackgroundWorker WorkerThread, Boolean VisualizeResults = false, RichTextBox DebugTextBox = null, Boolean DisplayDebug = true)
            {

                /////////////////////////////////////////////////////////////
                //DEBUG BULLSHIT
                /////////////////////////////////////////////////////////////
                DateTime SimulationClock = DateTime.Now;

                if (DisplayDebug)
                {
                    Console.WriteLine("===============================\nAnalyzing AI Database (" + SimulationClock.ToLocalTime() + ")");
                   // DebugTextBox.Text += "===============================\nAnalyzing AI Database (" + SimulationClock.ToLocalTime() + ")\n";
                }
                /////////////////////////////////////////////////////////////

                // Reset all previous analysis values and queue all of the leaf nodes to process
                LeafNodes = new Queue<String>();
                WorkNodes = new Queue<String>();

                foreach (String CurrentNodeID in NodeMasterList.Keys)
                {
                    if (NodeMasterList[ CurrentNodeID ].isLeaf)
                        LeafNodes.Enqueue(NodeMasterList[ CurrentNodeID ].NodeID);

                    NodeMasterList[CurrentNodeID].BlackWins = 0;
                    NodeMasterList[CurrentNodeID].WhiteWins = 0;
                }

                /////////////////////////////////////////////////////////////
                //DEBUG BULLSHIT
                /////////////////////////////////////////////////////////////
                if (DisplayDebug)
                {
                    TimeSpan SimulationElapsedTime = DateTime.Now.Subtract(SimulationClock);
                    Console.WriteLine("(" + SimulationElapsedTime.ToString() + ") Database Stats Reset (" + LeafNodes.Count + " leaf nodes queued)");
                    //DebugTextBox.Text += "(" + SimulationElapsedTime.ToString() + ") Database Stats Reset (" + LeafNodes.Count + " leaf nodes queued)\n";
                }
                /////////////////////////////////////////////////////////////

                SimulationNode CurrentLeafNode;
                String CurrentWorkNodeID;
                int WinningColor;

                while (LeafNodes.Count > 0)
                {
                    // If the BackgroundWorker.CancellationPending property is true, cancel
                    if (WorkerThread.CancellationPending)
                    {
                        Console.WriteLine("#####Database Analysis has been cancelled#####");
                        break;
                    }

                    // Grab the next leaf node from the leaf queue
                    CurrentLeafNode = NodeMasterList[LeafNodes.Dequeue()];

                    // Update the game board visual
                    if (VisualizeResults)
                        CurrentLeafNode.GameBoard.RefreshPieces();

                    // Find who the winner of the leaf node is
                    if (CurrentLeafNode.GameBoard.FindScore(WHITE) > CurrentLeafNode.GameBoard.FindScore(BLACK))
                        WinningColor = WHITE;
                    else if (CurrentLeafNode.GameBoard.FindScore(WHITE) < CurrentLeafNode.GameBoard.FindScore(BLACK))
                        WinningColor = BLACK;
                    else
                        WinningColor = -1;

                    // If this is a tie, there is no reason to process it
                    if ( ( WinningColor == BLACK ) || ( WinningColor == WHITE ) )
                    {
                        // Seed the work list with the leaf
                        WorkNodes.Enqueue(LeafNodes.Dequeue());

                        while (WorkNodes.Count > 0)
                        {
                            CurrentWorkNodeID = WorkNodes.Dequeue();

                            if( WinningColor == BLACK )
                                NodeMasterList[ CurrentWorkNodeID ].BlackWins++;
                            else
                                NodeMasterList[ CurrentWorkNodeID ].WhiteWins++;

                            foreach (String ParentNode in NodeMasterList[CurrentWorkNodeID].ParentNodes)
                                WorkNodes.Enqueue(ParentNode);
                        }
                    }
                }

                /////////////////////////////////////////////////////////////
                //DEBUG BULLSHIT
                /////////////////////////////////////////////////////////////
                if (DisplayDebug)
                {
                    TimeSpan SimulationElapsedTime = DateTime.Now.Subtract(SimulationClock);
                    Console.WriteLine("===============================\nAI DB Analysis Complete\nSimulation Time: " + SimulationElapsedTime.ToString() + "\n\n");
                    //DebugTextBox.Text += "===============================\nAI DB Analysis Complete\nSimulation Time: " + SimulationElapsedTime.ToString() + "\n\n";
                }
                /////////////////////////////////////////////////////////////
            }

            private int SimulateGameMove(Board CurrentBoard, int Turn )
            {
                SimulationCycles++;
                SimulationDepth++;

                if( SimulationCycles % 5000 == 0 )
                    Console.WriteLine(SimulationCycles + " Sim cycles in: " + WinnerTotal + " winners / " + LoserTotal + " losers" );

                if (!BlackMoves.ContainsKey(CurrentBoard.GetID()))
                {
                    BlackMoves.Add(CurrentBoard.GetID(), 0);
                }

                // If there are still moves left for the current player, start a new simulation for each of them
                if ( CurrentBoard.MovePossible( Turn ) )
                {
                    Point[] PossibleMoves = CurrentBoard.AvailableMoves( Turn );

                    for (int lc = 0; lc < PossibleMoves.Length; lc++)
                    {
                        // Make a copy of the current board
                        SimulationBoard = new Board(ref CurrentBoard);

                        // Place the current move on the new board
                        SimulationBoard.PutPiece(PossibleMoves[lc].X, PossibleMoves[lc].Y, Turn);

                        if (!BlackMoves.ContainsKey(SimulationBoard.GetID()))
                        {
                            BlackMoves.Add(SimulationBoard.GetID(), 0);
                        }

                        //Console.WriteLine( (Turn == WHITE ? "White" : "Black" ) + " moved to (" + PossibleMoves[lc].X + "," + PossibleMoves[lc].Y + ")");

                        // Start a simulation for the next player with the updated board
                        BlackMoves[CurrentBoard.GetID()] += SimulateGameMove(SimulationBoard, Turn == WHITE ? BLACK : WHITE);
                    }

                    SimulationDepth--;
                    return (BlackMoves[CurrentBoard.GetID()]);
                }

                // If there are no more moves for the current player, but the game is not over, start a new simulation for the other player
                else if (CurrentBoard.MovePossible( Turn == WHITE ? BLACK : WHITE)) 
                {
                    //Console.WriteLine( (Turn == WHITE ? "White" : "Black") + " cannot move, passing");

                    BlackMoves[CurrentBoard.GetID()] += SimulateGameMove(CurrentBoard, Turn == WHITE ? BLACK : WHITE);

                    SimulationDepth--;
                    return ( BlackMoves[CurrentBoard.GetID()] ) ;
                }
                // If there are no moves left in the game, collapse the simulation
                else
                {
                    if (CurrentBoard.FindScore(BLACK) > CurrentBoard.FindScore(WHITE))
                    {
                        //Console.WriteLine(" ### Black Wins");
                        BlackMoves[CurrentBoard.GetID()] += 1;
                        SimulationDepth--;
                        WinnerTotal++;
                        return (1);
                    }
                    else
                    {
                        //Console.WriteLine(" ### Black Loses");
                        SimulationDepth--;
                        LoserTotal++;
                        return (0);
                    }
                }
            }
		}

        // Class:       Game
        // Description: Stores game state information and rules
		private class Game
		{
			public int CurrentTurn;
			public int NextTurn;
			public int Difficulty;
			public Boolean VsComputer = true;
			public Board GameBoard;
			public int Winner;
			public Boolean IsComplete = false;
			public Boolean ProcessMoves = true;
			public AI AI;

			public Game( int BoardSize = 8 )
			{
				CurrentTurn = WHITE;
				NextTurn = BLACK;
				Difficulty = AIDifficulty;
				VsComputer = PvC;
				GameBoard = new Board( BoardSize );
				IsComplete = false;
				AI = new AI( BLACK );

                // Reset the board image to clear any pieces from previous games
                BoardGFX.DrawImage( BoardImage, 0, 0, BoardImage.Width, BoardImage.Height);

                // Reset the board that tracks which pieces have been drawn on the screen
                LastDrawnBoard = new Board(BoardSize);
                LastDrawnBoard.ClearBoard();

                GameBoard.RefreshPieces();
            }

            // Determines if there is a winner in the current game
            public Boolean DetermineWinner()
            {
                int WhiteScore = CurrentGame.GameBoard.FindScore(WHITE);
                int BlackScore = CurrentGame.GameBoard.FindScore(BLACK);

                //ScoreText.Text = "Current Score:\n" + " White: " + WhiteScore + "\n Black: " + BlackScore;

                if (WhiteScore == 0)
                {
                    CurrentGame.IsComplete = true;
                    CurrentGame.Winner = BLACK;
                }
                else if (BlackScore == 0)
                {
                    CurrentGame.IsComplete = true;
                    CurrentGame.Winner = WHITE;
                }
                else if (((WhiteScore + BlackScore) == 64) ||
                    ((!CurrentGame.GameBoard.MovePossible(CurrentGame.CurrentTurn)) && (!CurrentGame.GameBoard.MovePossible(CurrentGame.NextTurn))))
                {
                    CurrentGame.IsComplete = true;
                    if (BlackScore > WhiteScore)
                        CurrentGame.Winner = BLACK;
                    else if (BlackScore < WhiteScore)
                        CurrentGame.Winner = WHITE;
                    else
                        CurrentGame.Winner = EMPTY;
                }

                return (CurrentGame.IsComplete);
            }

            // Processes a single turn of gameplay, two if it is vs. AI
            public void ProcessTurn(int x, int y)
            {
                if (!CurrentGame.IsComplete)
                {
                    // As long as this isn't an AI turn, process the requested move
                    if (!((CurrentGame.VsComputer) && (CurrentGame.CurrentTurn == CurrentGame.AI.color)))
                    {
                        if (CurrentGame.GameBoard.MovePossible(CurrentGame.CurrentTurn))
                        {
                            if (CurrentGame.GameBoard.MakeMove(x, y, CurrentGame.CurrentTurn))
                            {
                                CurrentGame.SwitchTurn();
                            }
                        }
                        else
                        {
                            CurrentGame.SwitchTurn();
                        }
                    }

                    if ((CurrentGame.VsComputer) && (CurrentGame.CurrentTurn == CurrentGame.AI.color))
                    {
                        while (CurrentGame.GameBoard.MovePossible(CurrentGame.AI.color))
                        {
                            Point AIMove = CurrentGame.AI.Move(ref CurrentGame);

                            DebugText.Text = CurrentGame.AI.AIDebug;
                            DebugText.Text += "\nOutside class...\nPlacing " + CurrentGame.GetTurnString(CurrentGame.CurrentTurn) + " AI piece at (" + AIMove.X + "," + AIMove.Y + ")\n";

                            CurrentGame.GameBoard.MakeMove(AIMove.X, AIMove.Y, CurrentGame.CurrentTurn);

                            DebugText.Text += "\nResult Board:\n" + CurrentGame.GameBoard.ToString() + "\n\n";

                            if (CurrentGame.GameBoard.MovePossible(CurrentGame.NextTurn))
                            {
                                break;
                            }
                            else
                            {
                                CurrentGame.GameBoard.RefreshPieces();

                                DebugText.Text += "------------\n" + CurrentGame.NextTurn + " CANNOT MOVE!  AI moving again\n------------\n";
                            }
                        }

                        CurrentGame.SwitchTurn();
                        DebugText.Text += "------------\nAI " + CurrentGame.GetTurnString(CurrentGame.AI.color) + " turn over!  allowing human player to move\n############\n";
                    }

                    CurrentGame.GameBoard.RefreshPieces();

                    CurrentGame.DetermineWinner();
                }

                if (CurrentGame.IsComplete)
                {
                    if (CurrentGame.Winner == EMPTY)
                    {
                        TurnLabelText.Text = "The game ended in a tie!!!";
                    }
                    else
                    {
                        TurnLabelText.Text = CurrentGame.GetTurnString(CurrentGame.Winner) + " is the winner!!!";
                    }
                }

                TurnLabelText.Text = "Current Turn: " + CurrentGame.GetTurnString(CurrentGame.CurrentTurn) + "\n";
            }

			public void SwitchTurn()
			{
				if( CurrentTurn == WHITE ) 
				{
					CurrentTurn = BLACK;
					NextTurn = WHITE;
				} 
				else 
				{
					CurrentTurn = WHITE;
					NextTurn = BLACK;
				}
			}

			public string GetTurnString( int color )
			{
				if( color == WHITE ) 
					return( "White" );
				else if ( color == BLACK )
					return( "Black" );
				else 
					return( "Illegal Color!" );
			}
		}

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
			ReversiForm.ActiveForm.Close();
		}

        // New game selected
		private void NewGameMenu_Click(object sender, System.EventArgs e)
		{
			CurrentGame = new Game( getBoardSize() );
		}

        // Skip turn (debug option) selected
		private void DebugSkip_Click(object sender, System.EventArgs e)
		{
			CurrentGame.SwitchTurn();
			TurnLabel.Text = "Current Turn: " + CurrentGame.GetTurnString( CurrentGame.CurrentTurn ) + "\n" ;		
		}

        // unused
		private void DebugProcess_Click(object sender, System.EventArgs e)
		{
			CurrentGame.ProcessMoves = !CurrentGame.ProcessMoves;
			DebugProcess.Checked = !DebugProcess.Checked;
		}

        // Start a new game scenario where white cannot move
		private void DebugScenario_NoWhite_Click(object sender, System.EventArgs e)
		{
			CurrentGame = new Game( 8 );
			DebugText.Text = "";
			CurrentGame.GameBoard.ClearBoard();
			CurrentGame.GameBoard.PutPiece( 0, 0, BLACK );
            CurrentGame.GameBoard.PutPiece( 0, 1, BLACK );
            CurrentGame.GameBoard.PutPiece( 0, 2, BLACK );
            CurrentGame.GameBoard.PutPiece( 0, 3, BLACK );
            CurrentGame.GameBoard.PutPiece( 0, 4, BLACK );
            CurrentGame.GameBoard.PutPiece( 0, 5, BLACK );
            CurrentGame.GameBoard.PutPiece( 0, 6, BLACK );
            CurrentGame.GameBoard.PutPiece( 0, 7, BLACK );
			CurrentGame.GameBoard.PutPiece( 1, 0, WHITE );
            CurrentGame.GameBoard.PutPiece( 1, 1, WHITE );
            CurrentGame.GameBoard.PutPiece( 1, 2, WHITE );
            CurrentGame.GameBoard.PutPiece( 1, 3, WHITE );
            CurrentGame.GameBoard.PutPiece( 1, 4, WHITE );
            CurrentGame.GameBoard.PutPiece( 1, 5, WHITE );
            CurrentGame.GameBoard.PutPiece( 1, 6, WHITE );
            CurrentGame.GameBoard.PutPiece( 1, 7, WHITE );
			CurrentGame.CurrentTurn = WHITE;
			CurrentGame.NextTurn = BLACK;
			CurrentGame.GameBoard.RefreshPieces();
			//BoardPicture.Invalidate();
			CurrentGame.ProcessTurn( 0, 0 );
		}
        #endregion

        #region AI Database Form & Event Handelers

        // Responds to the analyze database button press, starts the job
        private void BuildAIDBButton_Click(object sender, EventArgs e)
        {
            SetupSimulationForm();
            DBBuildWorker.RunWorkerAsync(getBoardSize());
        }

        // Dumps the database information to the debug window
        private void dumpDBInfoButton_Click(object sender, EventArgs e)
        {
            DebugText.Text += CurrentGame.AI.DumpSimulationInfo();
        }

        // Responds to the analyze database button press
        private void anaylzeDBButton_Click(object sender, EventArgs e)
        {
            SetupSimulationForm();
            DBAnalysisWorker.RunWorkerAsync();
        }

        // Starts the database background worker (button press)
        private void DBBuildWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            StartBuildDB(Convert.ToInt32( e.Argument.ToString()));
        }

        // Starts the database build background worker
        private void StartBuildDB( int BoardSize = 4 )
        {
            CurrentGame = new Game( BoardSize );
            CurrentGame.AI.BuildAIDatabase(DBBuildWorker, BoardSize, visualizeCheckbox.Checked, true);
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
            CurrentGame.AI.AnalyzeAIDatabase(DBAnalysisWorker, visualizeCheckbox.Checked, DebugText, DisplayDebug);
        }

        // Called from within the database build background woker to report the progress of the build
        private void DBBuildWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            simTimerLabel.Text = TimeSpan.FromTicks(e.ProgressPercentage).ToString(@"hh\:mm\:ss");

            if (e.UserState.ToString() != "")
                DebugText.Text += e.UserState.ToString();

            nodeCounter.Text = CurrentGame.AI.NodeMasterList.Count.ToString();
            workCounter.Text = CurrentGame.AI.WorkNodes.Count.ToString();
            victoryCounter.Text = CurrentGame.AI.LeafTotal.ToString();
        }

        // Runs when the database background worker thread is finished
        private void DBBuildWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            ClearSimulationForm();
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
                DebugText.Text += "#####   DB Build Aborted: Memory floor reached (" + RAMUsageBar.Value.ToString("0,0.") + " KB free)  #####";
            }

            Graphics RAMGfx = RAMUsageBar.CreateGraphics();
            int MemoryAbortLine = Convert.ToInt32(RAMUsageBar.Width * Properties.Settings.Default.MemoryFloor);

            RAMGfx.DrawString(RAMUsageBar.Value.ToString("0,0.") + " KB free", new Font("Arial", (float)11, FontStyle.Regular), Brushes.Black, new PointF(120, 2));
            RAMGfx.DrawLine(new Pen(Color.Red, 2), MemoryAbortLine, 0, MemoryAbortLine, RAMUsageBar.Height);      
            RAMGfx.DrawString("Abort   Line", new Font("Arial", (float)8, FontStyle.Regular), Brushes.Red, new PointF(MemoryAbortLine - 34, 4));
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
            int x = (e.X + 1) / 40;
            int y = (e.Y + 1) / 40;

            CurrentGame.ProcessTurn(x, y);
        }

        // If the grid size drop down changes, updates the board with the new dimensions
        private void gridSizeDropDown_SelectedIndexChanged(object sender, EventArgs e)
        {  
            gridDimensionLabel.Text = getBoardSize() + "x" + getBoardSize();

            BoardPicture.Width = 40 * getBoardSize();
            BoardPicture.Height = 40 * getBoardSize();

            CurrentGame = new Game(getBoardSize());
        }

        // Starts a new game 100ms after the form has loaded
        private void NewGameTimer_Tick(object sender, EventArgs e)
        {
            CurrentGame = new Game(getBoardSize());
            NewGameTimer.Enabled = false;
        }
        #endregion

    }
}
