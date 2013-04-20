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
        private System.Windows.Forms.MenuItem FileDropDownMenu;
        private System.Windows.Forms.MenuItem GameSetupDropDownMenu;
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
        private System.Windows.Forms.MenuItem DebugDropDownMenu;
        private System.Windows.Forms.MenuItem DebugSkip;
        private System.Windows.Forms.MenuItem DebugProcess;
        private System.Windows.Forms.MenuItem NewDebugGameScenarios;
        private System.Windows.Forms.MenuItem DebugScenario_NoWhite;
        private System.Windows.Forms.MenuItem DebugScenario_NoBlack;
        private System.Windows.Forms.MenuItem DebugScenario_MidGame;
        private Button BuildAIDBButton;
        private CheckBox VisualizeCheckbox;
        private GroupBox DBBuilderButtonsBox;
        private Button AnaylzeDBButton;
        private Label GridSizeTitleLabel;
        private Button DumpDBInfoButton;
        private BackgroundWorker DBBuildWorker;
        private Button CancelBuildButton;
        private BackgroundWorker DBAnalysisWorker;
        private ComboBox GridSizeDropDown;
        private Label GridDimensionLabel;
        private Label SimTimerLabel;
        private Label NodeCounter;
        private Label WorkCounter;
        private Label NodeCounterLabel;
        private Label WorkCounterLabel;
        private Label VictoryCounterLabel;
        private Label VictoryCounter;
        private ProgressBar RAMUsageBar;
        private System.Windows.Forms.Timer RAMCheckTimer;
        private Label RAMLabel;
        private TabControl AIInfoTabControl;
        private TabPage AIDBTab;
        private TabPage AISimTab;
        private Label CurrentTurnLabel;
        private Label BlackScoreBoardTitle;
        private Label WhiteScoreBoardTitle;
        private Label WhiteScoreBoard;
        private Label BlackScoreBoard;
        private BackgroundWorker AITurnWorker;
        private PictureBox BoardXaxisLabel;
        private PictureBox BoardYaxisLabel;
        private Label SimDepthTitle;
        private Button HideDebugButton;
        private TrackBar SimulationDepthSlider;
        private Label SimDepthCountLabel;
        private Label SimDepthCount;
        public RichTextBox DebugAITrace;
        private Button ClearDebugLogButton;
        private Label AITraceLabel;
        private CheckBox DebugLogCheckBox;
        private Button NewGameButton;
        private Panel CurrentTurnImageSurface;
        private MenuItem GameSetupMenuHorzBar2;
        private MenuItem ShowAvailableMoves;
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
            this.FileDropDownMenu = new System.Windows.Forms.MenuItem();
            this.NewGameMenu = new System.Windows.Forms.MenuItem();
            this.ExitMenu = new System.Windows.Forms.MenuItem();
            this.GameSetupDropDownMenu = new System.Windows.Forms.MenuItem();
            this.PvPMenu = new System.Windows.Forms.MenuItem();
            this.PvCMenu = new System.Windows.Forms.MenuItem();
            this.GameSetupMenuHorzBar1 = new System.Windows.Forms.MenuItem();
            this.AIDifficultyMenu = new System.Windows.Forms.MenuItem();
            this.DiffMenu_Easy = new System.Windows.Forms.MenuItem();
            this.DiffMenu_Normal = new System.Windows.Forms.MenuItem();
            this.DiffMenu_Hard = new System.Windows.Forms.MenuItem();
            this.DiffMenu_VeryHard = new System.Windows.Forms.MenuItem();
            this.GameSetupMenuHorzBar2 = new System.Windows.Forms.MenuItem();
            this.ShowAvailableMoves = new System.Windows.Forms.MenuItem();
            this.DebugDropDownMenu = new System.Windows.Forms.MenuItem();
            this.DebugSkip = new System.Windows.Forms.MenuItem();
            this.DebugProcess = new System.Windows.Forms.MenuItem();
            this.NewDebugGameScenarios = new System.Windows.Forms.MenuItem();
            this.DebugScenario_NoWhite = new System.Windows.Forms.MenuItem();
            this.DebugScenario_NoBlack = new System.Windows.Forms.MenuItem();
            this.DebugScenario_MidGame = new System.Windows.Forms.MenuItem();
            this.BuildAIDBButton = new System.Windows.Forms.Button();
            this.VisualizeCheckbox = new System.Windows.Forms.CheckBox();
            this.DBBuilderButtonsBox = new System.Windows.Forms.GroupBox();
            this.GridDimensionLabel = new System.Windows.Forms.Label();
            this.GridSizeDropDown = new System.Windows.Forms.ComboBox();
            this.DumpDBInfoButton = new System.Windows.Forms.Button();
            this.GridSizeTitleLabel = new System.Windows.Forms.Label();
            this.AnaylzeDBButton = new System.Windows.Forms.Button();
            this.DBBuildWorker = new System.ComponentModel.BackgroundWorker();
            this.CancelBuildButton = new System.Windows.Forms.Button();
            this.DBAnalysisWorker = new System.ComponentModel.BackgroundWorker();
            this.SimTimerLabel = new System.Windows.Forms.Label();
            this.NodeCounter = new System.Windows.Forms.Label();
            this.WorkCounter = new System.Windows.Forms.Label();
            this.NodeCounterLabel = new System.Windows.Forms.Label();
            this.WorkCounterLabel = new System.Windows.Forms.Label();
            this.VictoryCounterLabel = new System.Windows.Forms.Label();
            this.VictoryCounter = new System.Windows.Forms.Label();
            this.NewGameTimer = new System.Windows.Forms.Timer(this.components);
            this.RAMUsageBar = new System.Windows.Forms.ProgressBar();
            this.RAMCheckTimer = new System.Windows.Forms.Timer(this.components);
            this.RAMLabel = new System.Windows.Forms.Label();
            this.AIInfoTabControl = new System.Windows.Forms.TabControl();
            this.AIDBTab = new System.Windows.Forms.TabPage();
            this.AISimTab = new System.Windows.Forms.TabPage();
            this.SimDepthCountLabel = new System.Windows.Forms.Label();
            this.SimDepthCount = new System.Windows.Forms.Label();
            this.SimulationDepthSlider = new System.Windows.Forms.TrackBar();
            this.SimDepthTitle = new System.Windows.Forms.Label();
            this.CurrentTurnLabel = new System.Windows.Forms.Label();
            this.BlackScoreBoardTitle = new System.Windows.Forms.Label();
            this.WhiteScoreBoardTitle = new System.Windows.Forms.Label();
            this.WhiteScoreBoard = new System.Windows.Forms.Label();
            this.BlackScoreBoard = new System.Windows.Forms.Label();
            this.AITurnWorker = new System.ComponentModel.BackgroundWorker();
            this.HideDebugButton = new System.Windows.Forms.Button();
            this.DebugAITrace = new System.Windows.Forms.RichTextBox();
            this.ClearDebugLogButton = new System.Windows.Forms.Button();
            this.AITraceLabel = new System.Windows.Forms.Label();
            this.DebugLogCheckBox = new System.Windows.Forms.CheckBox();
            this.BoardSurface = new System.Windows.Forms.Panel();
            this.NewGameButton = new System.Windows.Forms.Button();
            this.BoardXaxisLabel = new System.Windows.Forms.PictureBox();
            this.BoardYaxisLabel = new System.Windows.Forms.PictureBox();
            this.CurrentTurnImageSurface = new System.Windows.Forms.Panel();
            this.DBBuilderButtonsBox.SuspendLayout();
            this.AIInfoTabControl.SuspendLayout();
            this.AIDBTab.SuspendLayout();
            this.AISimTab.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SimulationDepthSlider)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.BoardXaxisLabel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.BoardYaxisLabel)).BeginInit();
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
            this.FileDropDownMenu,
            this.GameSetupDropDownMenu,
            this.DebugDropDownMenu});
            // 
            // FileDropDownMenu
            // 
            this.FileDropDownMenu.Index = 0;
            this.FileDropDownMenu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.NewGameMenu,
            this.ExitMenu});
            this.FileDropDownMenu.Text = "File";
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
            // GameSetupDropDownMenu
            // 
            this.GameSetupDropDownMenu.Index = 1;
            this.GameSetupDropDownMenu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.PvPMenu,
            this.PvCMenu,
            this.GameSetupMenuHorzBar1,
            this.AIDifficultyMenu,
            this.GameSetupMenuHorzBar2,
            this.ShowAvailableMoves});
            this.GameSetupDropDownMenu.Text = "Game Setup";
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
            // ShowAvailableMoves
            // 
            this.ShowAvailableMoves.Checked = true;
            this.ShowAvailableMoves.Index = 5;
            this.ShowAvailableMoves.Text = "Show Available Moves";
            this.ShowAvailableMoves.Click += new System.EventHandler(this.ShowAvailableMoves_Click);
            // 
            // DebugDropDownMenu
            // 
            this.DebugDropDownMenu.Index = 2;
            this.DebugDropDownMenu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.DebugSkip,
            this.DebugProcess,
            this.NewDebugGameScenarios});
            this.DebugDropDownMenu.Text = "Debug";
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
            // NewDebugGameScenarios
            // 
            this.NewDebugGameScenarios.Index = 2;
            this.NewDebugGameScenarios.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.DebugScenario_NoWhite,
            this.DebugScenario_NoBlack,
            this.DebugScenario_MidGame});
            this.NewDebugGameScenarios.Text = "New Game Scenario";
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
            // VisualizeCheckbox
            // 
            this.VisualizeCheckbox.AutoSize = true;
            this.VisualizeCheckbox.BackColor = System.Drawing.Color.Transparent;
            this.VisualizeCheckbox.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.VisualizeCheckbox.Checked = true;
            this.VisualizeCheckbox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.VisualizeCheckbox.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.VisualizeCheckbox.Location = new System.Drawing.Point(822, 7);
            this.VisualizeCheckbox.Margin = new System.Windows.Forms.Padding(0);
            this.VisualizeCheckbox.Name = "VisualizeCheckbox";
            this.VisualizeCheckbox.Size = new System.Drawing.Size(99, 19);
            this.VisualizeCheckbox.TabIndex = 8;
            this.VisualizeCheckbox.Text = "Visualizations";
            this.VisualizeCheckbox.UseVisualStyleBackColor = false;
            this.VisualizeCheckbox.CheckedChanged += new System.EventHandler(this.VisualizeCheckbox_CheckedChanged);
            // 
            // DBBuilderButtonsBox
            // 
            this.DBBuilderButtonsBox.Controls.Add(this.GridDimensionLabel);
            this.DBBuilderButtonsBox.Controls.Add(this.GridSizeDropDown);
            this.DBBuilderButtonsBox.Controls.Add(this.DumpDBInfoButton);
            this.DBBuilderButtonsBox.Controls.Add(this.GridSizeTitleLabel);
            this.DBBuilderButtonsBox.Controls.Add(this.AnaylzeDBButton);
            this.DBBuilderButtonsBox.Controls.Add(this.BuildAIDBButton);
            this.DBBuilderButtonsBox.Location = new System.Drawing.Point(6, 6);
            this.DBBuilderButtonsBox.Name = "DBBuilderButtonsBox";
            this.DBBuilderButtonsBox.Size = new System.Drawing.Size(244, 107);
            this.DBBuilderButtonsBox.TabIndex = 9;
            this.DBBuilderButtonsBox.TabStop = false;
            this.DBBuilderButtonsBox.Text = "Game Simulator";
            // 
            // GridDimensionLabel
            // 
            this.GridDimensionLabel.AutoSize = true;
            this.GridDimensionLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.GridDimensionLabel.Location = new System.Drawing.Point(200, 51);
            this.GridDimensionLabel.Name = "GridDimensionLabel";
            this.GridDimensionLabel.Size = new System.Drawing.Size(28, 16);
            this.GridDimensionLabel.TabIndex = 15;
            this.GridDimensionLabel.Text = "4x4";
            // 
            // GridSizeDropDown
            // 
            this.GridSizeDropDown.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.GridSizeDropDown.FormattingEnabled = true;
            this.GridSizeDropDown.Items.AddRange(new object[] {
            "4",
            "5",
            "6",
            "7",
            "8"});
            this.GridSizeDropDown.Location = new System.Drawing.Point(147, 47);
            this.GridSizeDropDown.Name = "GridSizeDropDown";
            this.GridSizeDropDown.Size = new System.Drawing.Size(41, 21);
            this.GridSizeDropDown.TabIndex = 14;
            this.GridSizeDropDown.SelectedIndexChanged += new System.EventHandler(this.GridSizeDropDown_SelectedIndexChanged);
            // 
            // DumpDBInfoButton
            // 
            this.DumpDBInfoButton.Location = new System.Drawing.Point(75, 78);
            this.DumpDBInfoButton.Name = "DumpDBInfoButton";
            this.DumpDBInfoButton.Size = new System.Drawing.Size(108, 23);
            this.DumpDBInfoButton.TabIndex = 13;
            this.DumpDBInfoButton.Text = "Dump DB Info";
            this.DumpDBInfoButton.UseVisualStyleBackColor = true;
            this.DumpDBInfoButton.Click += new System.EventHandler(this.DumpDBInfoButton_Click);
            // 
            // GridSizeTitleLabel
            // 
            this.GridSizeTitleLabel.AutoSize = true;
            this.GridSizeTitleLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.GridSizeTitleLabel.Location = new System.Drawing.Point(134, 27);
            this.GridSizeTitleLabel.Name = "GridSizeTitleLabel";
            this.GridSizeTitleLabel.Size = new System.Drawing.Size(104, 13);
            this.GridSizeTitleLabel.TabIndex = 10;
            this.GridSizeTitleLabel.Text = "Game Board Size";
            // 
            // AnaylzeDBButton
            // 
            this.AnaylzeDBButton.Location = new System.Drawing.Point(6, 52);
            this.AnaylzeDBButton.Name = "AnaylzeDBButton";
            this.AnaylzeDBButton.Size = new System.Drawing.Size(108, 23);
            this.AnaylzeDBButton.TabIndex = 9;
            this.AnaylzeDBButton.Text = "Analyze Database";
            this.AnaylzeDBButton.UseVisualStyleBackColor = true;
            this.AnaylzeDBButton.Click += new System.EventHandler(this.AnaylzeDBButton_Click);
            // 
            // DBBuildWorker
            // 
            this.DBBuildWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.DBBuildWorker_DoWork);
            this.DBBuildWorker.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.DBBuildWorker_ProgressChanged);
            this.DBBuildWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.DBBuildWorker_RunWorkerCompleted);
            // 
            // CancelBuildButton
            // 
            this.CancelBuildButton.BackColor = System.Drawing.Color.Red;
            this.CancelBuildButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CancelBuildButton.ForeColor = System.Drawing.Color.White;
            this.CancelBuildButton.Location = new System.Drawing.Point(273, 18);
            this.CancelBuildButton.Name = "CancelBuildButton";
            this.CancelBuildButton.Size = new System.Drawing.Size(64, 64);
            this.CancelBuildButton.TabIndex = 10;
            this.CancelBuildButton.Text = "Cancel Job";
            this.CancelBuildButton.UseVisualStyleBackColor = false;
            this.CancelBuildButton.Visible = false;
            this.CancelBuildButton.Click += new System.EventHandler(this.CancelButton_Click);
            // 
            // DBAnalysisWorker
            // 
            this.DBAnalysisWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.DBAnalysisWorker_DoWork);
            // 
            // SimTimerLabel
            // 
            this.SimTimerLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SimTimerLabel.ForeColor = System.Drawing.Color.Red;
            this.SimTimerLabel.Location = new System.Drawing.Point(255, 81);
            this.SimTimerLabel.Name = "SimTimerLabel";
            this.SimTimerLabel.Size = new System.Drawing.Size(98, 34);
            this.SimTimerLabel.TabIndex = 12;
            this.SimTimerLabel.Text = "01:12:12";
            this.SimTimerLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // NodeCounter
            // 
            this.NodeCounter.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.NodeCounter.Location = new System.Drawing.Point(3, 137);
            this.NodeCounter.Name = "NodeCounter";
            this.NodeCounter.Size = new System.Drawing.Size(124, 32);
            this.NodeCounter.TabIndex = 13;
            this.NodeCounter.Text = "0";
            this.NodeCounter.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // WorkCounter
            // 
            this.WorkCounter.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.WorkCounter.Location = new System.Drawing.Point(247, 137);
            this.WorkCounter.Name = "WorkCounter";
            this.WorkCounter.Size = new System.Drawing.Size(113, 32);
            this.WorkCounter.TabIndex = 14;
            this.WorkCounter.Text = "0";
            this.WorkCounter.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // NodeCounterLabel
            // 
            this.NodeCounterLabel.AutoSize = true;
            this.NodeCounterLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.NodeCounterLabel.Location = new System.Drawing.Point(18, 124);
            this.NodeCounterLabel.Name = "NodeCounterLabel";
            this.NodeCounterLabel.Size = new System.Drawing.Size(94, 13);
            this.NodeCounterLabel.TabIndex = 16;
            this.NodeCounterLabel.Text = "Total Nodes in DB";
            // 
            // WorkCounterLabel
            // 
            this.WorkCounterLabel.AutoSize = true;
            this.WorkCounterLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.WorkCounterLabel.Location = new System.Drawing.Point(261, 124);
            this.WorkCounterLabel.Name = "WorkCounterLabel";
            this.WorkCounterLabel.Size = new System.Drawing.Size(84, 13);
            this.WorkCounterLabel.TabIndex = 17;
            this.WorkCounterLabel.Text = "Nodes in Queue";
            // 
            // VictoryCounterLabel
            // 
            this.VictoryCounterLabel.AutoSize = true;
            this.VictoryCounterLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.VictoryCounterLabel.Location = new System.Drawing.Point(149, 124);
            this.VictoryCounterLabel.Name = "VictoryCounterLabel";
            this.VictoryCounterLabel.Size = new System.Drawing.Size(74, 13);
            this.VictoryCounterLabel.TabIndex = 19;
            this.VictoryCounterLabel.Text = "Total Victories";
            // 
            // VictoryCounter
            // 
            this.VictoryCounter.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.VictoryCounter.Location = new System.Drawing.Point(125, 137);
            this.VictoryCounter.Name = "VictoryCounter";
            this.VictoryCounter.Size = new System.Drawing.Size(123, 32);
            this.VictoryCounter.TabIndex = 18;
            this.VictoryCounter.Text = "0";
            this.VictoryCounter.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
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
            this.AIDBTab.Controls.Add(this.DBBuilderButtonsBox);
            this.AIDBTab.Controls.Add(this.RAMUsageBar);
            this.AIDBTab.Controls.Add(this.RAMLabel);
            this.AIDBTab.Controls.Add(this.VictoryCounterLabel);
            this.AIDBTab.Controls.Add(this.CancelBuildButton);
            this.AIDBTab.Controls.Add(this.VictoryCounter);
            this.AIDBTab.Controls.Add(this.SimTimerLabel);
            this.AIDBTab.Controls.Add(this.WorkCounterLabel);
            this.AIDBTab.Controls.Add(this.NodeCounter);
            this.AIDBTab.Controls.Add(this.NodeCounterLabel);
            this.AIDBTab.Controls.Add(this.WorkCounter);
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
            this.AISimTab.Controls.Add(this.SimDepthCountLabel);
            this.AISimTab.Controls.Add(this.SimDepthCount);
            this.AISimTab.Controls.Add(this.SimulationDepthSlider);
            this.AISimTab.Controls.Add(this.SimDepthTitle);
            this.AISimTab.Location = new System.Drawing.Point(4, 22);
            this.AISimTab.Name = "AISimTab";
            this.AISimTab.Padding = new System.Windows.Forms.Padding(3);
            this.AISimTab.Size = new System.Drawing.Size(367, 233);
            this.AISimTab.TabIndex = 1;
            this.AISimTab.Text = "AI Game Simulations";
            this.AISimTab.UseVisualStyleBackColor = true;
            // 
            // SimDepthCountLabel
            // 
            this.SimDepthCountLabel.AutoSize = true;
            this.SimDepthCountLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SimDepthCountLabel.Location = new System.Drawing.Point(313, 40);
            this.SimDepthCountLabel.Name = "SimDepthCountLabel";
            this.SimDepthCountLabel.Size = new System.Drawing.Size(38, 26);
            this.SimDepthCountLabel.TabIndex = 41;
            this.SimDepthCountLabel.Text = "Turns\r\nAhead";
            // 
            // SimDepthCount
            // 
            this.SimDepthCount.Font = new System.Drawing.Font("Calibri", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SimDepthCount.Location = new System.Drawing.Point(305, 13);
            this.SimDepthCount.Name = "SimDepthCount";
            this.SimDepthCount.Size = new System.Drawing.Size(49, 31);
            this.SimDepthCount.TabIndex = 40;
            this.SimDepthCount.Text = "20";
            this.SimDepthCount.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // SimulationDepthSlider
            // 
            this.SimulationDepthSlider.LargeChange = 2;
            this.SimulationDepthSlider.Location = new System.Drawing.Point(6, 29);
            this.SimulationDepthSlider.Maximum = 20;
            this.SimulationDepthSlider.Minimum = 2;
            this.SimulationDepthSlider.Name = "SimulationDepthSlider";
            this.SimulationDepthSlider.Size = new System.Drawing.Size(299, 45);
            this.SimulationDepthSlider.SmallChange = 2;
            this.SimulationDepthSlider.TabIndex = 38;
            this.SimulationDepthSlider.TickFrequency = 2;
            this.SimulationDepthSlider.TickStyle = System.Windows.Forms.TickStyle.Both;
            this.SimulationDepthSlider.Value = 6;
            this.SimulationDepthSlider.Scroll += new System.EventHandler(this.SimulationDepthSlider_Scroll);
            // 
            // SimDepthTitle
            // 
            this.SimDepthTitle.AutoSize = true;
            this.SimDepthTitle.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SimDepthTitle.Location = new System.Drawing.Point(6, 4);
            this.SimDepthTitle.Name = "SimDepthTitle";
            this.SimDepthTitle.Size = new System.Drawing.Size(147, 23);
            this.SimDepthTitle.TabIndex = 36;
            this.SimDepthTitle.Text = "Simulation Depth";
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
            // BlackScoreBoardTitle
            // 
            this.BlackScoreBoardTitle.Font = new System.Drawing.Font("Calibri", 26.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BlackScoreBoardTitle.Location = new System.Drawing.Point(360, 550);
            this.BlackScoreBoardTitle.Name = "BlackScoreBoardTitle";
            this.BlackScoreBoardTitle.Size = new System.Drawing.Size(112, 42);
            this.BlackScoreBoardTitle.TabIndex = 26;
            this.BlackScoreBoardTitle.Text = "Black:";
            this.BlackScoreBoardTitle.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // WhiteScoreBoardTitle
            // 
            this.WhiteScoreBoardTitle.Font = new System.Drawing.Font("Calibri", 26.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.WhiteScoreBoardTitle.Location = new System.Drawing.Point(186, 550);
            this.WhiteScoreBoardTitle.Name = "WhiteScoreBoardTitle";
            this.WhiteScoreBoardTitle.Size = new System.Drawing.Size(121, 40);
            this.WhiteScoreBoardTitle.TabIndex = 27;
            this.WhiteScoreBoardTitle.Text = "White:";
            this.WhiteScoreBoardTitle.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // WhiteScoreBoard
            // 
            this.WhiteScoreBoard.Font = new System.Drawing.Font("Calibri", 27.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.WhiteScoreBoard.Location = new System.Drawing.Point(292, 554);
            this.WhiteScoreBoard.Name = "WhiteScoreBoard";
            this.WhiteScoreBoard.Size = new System.Drawing.Size(62, 40);
            this.WhiteScoreBoard.TabIndex = 28;
            this.WhiteScoreBoard.Text = "0";
            this.WhiteScoreBoard.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // BlackScoreBoard
            // 
            this.BlackScoreBoard.Font = new System.Drawing.Font("Calibri", 27.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BlackScoreBoard.Location = new System.Drawing.Point(455, 552);
            this.BlackScoreBoard.Name = "BlackScoreBoard";
            this.BlackScoreBoard.Size = new System.Drawing.Size(64, 44);
            this.BlackScoreBoard.TabIndex = 29;
            this.BlackScoreBoard.Text = "0";
            this.BlackScoreBoard.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // AITurnWorker
            // 
            this.AITurnWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.AITurnMonitor_DoWork);
            this.AITurnWorker.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.AITurnMonitor_ProgressChanged);
            this.AITurnWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.AITurnMonitor_RunWorkerCompleted);
            // 
            // HideDebugButton
            // 
            this.HideDebugButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.HideDebugButton.Location = new System.Drawing.Point(517, 270);
            this.HideDebugButton.Margin = new System.Windows.Forms.Padding(0);
            this.HideDebugButton.Name = "HideDebugButton";
            this.HideDebugButton.Size = new System.Drawing.Size(25, 57);
            this.HideDebugButton.TabIndex = 38;
            this.HideDebugButton.Text = "<<\r\n<<\r\n<<";
            this.HideDebugButton.UseVisualStyleBackColor = true;
            this.HideDebugButton.Click += new System.EventHandler(this.HideDebugButton_Click);
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
            // ClearDebugLogButton
            // 
            this.ClearDebugLogButton.Location = new System.Drawing.Point(554, 274);
            this.ClearDebugLogButton.Name = "ClearDebugLogButton";
            this.ClearDebugLogButton.Size = new System.Drawing.Size(57, 23);
            this.ClearDebugLogButton.TabIndex = 36;
            this.ClearDebugLogButton.Text = "Clear";
            this.ClearDebugLogButton.UseVisualStyleBackColor = true;
            this.ClearDebugLogButton.Click += new System.EventHandler(this.ClearDebugLogButton_Click);
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
            // DebugLogCheckBox
            // 
            this.DebugLogCheckBox.AutoSize = true;
            this.DebugLogCheckBox.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.DebugLogCheckBox.Checked = true;
            this.DebugLogCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.DebugLogCheckBox.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DebugLogCheckBox.Location = new System.Drawing.Point(851, 276);
            this.DebugLogCheckBox.Name = "DebugLogCheckBox";
            this.DebugLogCheckBox.Size = new System.Drawing.Size(67, 19);
            this.DebugLogCheckBox.TabIndex = 38;
            this.DebugLogCheckBox.Text = "Logging";
            this.DebugLogCheckBox.UseVisualStyleBackColor = true;
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
            // NewGameButton
            // 
            this.NewGameButton.Location = new System.Drawing.Point(438, 8);
            this.NewGameButton.Name = "NewGameButton";
            this.NewGameButton.Size = new System.Drawing.Size(72, 23);
            this.NewGameButton.TabIndex = 40;
            this.NewGameButton.Text = "New Game";
            this.NewGameButton.UseVisualStyleBackColor = true;
            this.NewGameButton.Click += new System.EventHandler(this.NewGameButton_Click);
            // 
            // BoardXaxisLabel
            // 
            this.BoardXaxisLabel.Image = global::Reversi.Properties.Resources.boardXaxis;
            this.BoardXaxisLabel.Location = new System.Drawing.Point(30, 41);
            this.BoardXaxisLabel.Name = "BoardXaxisLabel";
            this.BoardXaxisLabel.Size = new System.Drawing.Size(473, 29);
            this.BoardXaxisLabel.TabIndex = 34;
            this.BoardXaxisLabel.TabStop = false;
            // 
            // BoardYaxisLabel
            // 
            this.BoardYaxisLabel.Image = global::Reversi.Properties.Resources.boardYaxis;
            this.BoardYaxisLabel.Location = new System.Drawing.Point(5, 62);
            this.BoardYaxisLabel.Name = "BoardYaxisLabel";
            this.BoardYaxisLabel.Size = new System.Drawing.Size(27, 476);
            this.BoardYaxisLabel.TabIndex = 35;
            this.BoardYaxisLabel.TabStop = false;
            // 
            // CurrentTurnImageSurface
            // 
            this.CurrentTurnImageSurface.Location = new System.Drawing.Point(104, 552);
            this.CurrentTurnImageSurface.Name = "CurrentTurnImageSurface";
            this.CurrentTurnImageSurface.Size = new System.Drawing.Size(38, 38);
            this.CurrentTurnImageSurface.TabIndex = 40;
            this.CurrentTurnImageSurface.Paint += new System.Windows.Forms.PaintEventHandler(this.CurrentTurnImageSurface_Paint);
            // 
            // ReversiForm
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(925, 603);
            this.Controls.Add(this.CurrentTurnImageSurface);
            this.Controls.Add(this.BoardSurface);
            this.Controls.Add(this.NewGameButton);
            this.Controls.Add(this.DebugLogCheckBox);
            this.Controls.Add(this.VisualizeCheckbox);
            this.Controls.Add(this.AITraceLabel);
            this.Controls.Add(this.HideDebugButton);
            this.Controls.Add(this.ClearDebugLogButton);
            this.Controls.Add(this.DebugAITrace);
            this.Controls.Add(this.AIInfoTabControl);
            this.Controls.Add(this.CurrentTurnLabel);
            this.Controls.Add(this.WhiteScoreBoard);
            this.Controls.Add(this.BlackScoreBoard);
            this.Controls.Add(this.BoardXaxisLabel);
            this.Controls.Add(this.BoardYaxisLabel);
            this.Controls.Add(this.Title);
            this.Controls.Add(this.WhiteScoreBoardTitle);
            this.Controls.Add(this.BlackScoreBoardTitle);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Menu = this.mainDropDownMenu;
            this.Name = "ReversiForm";
            this.Text = "Reversi";
            this.DBBuilderButtonsBox.ResumeLayout(false);
            this.DBBuilderButtonsBox.PerformLayout();
            this.AIInfoTabControl.ResumeLayout(false);
            this.AIDBTab.ResumeLayout(false);
            this.AIDBTab.PerformLayout();
            this.AISimTab.ResumeLayout(false);
            this.AISimTab.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SimulationDepthSlider)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.BoardXaxisLabel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.BoardYaxisLabel)).EndInit();
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
            gCurrentTurnImageGFX = CurrentTurnImageSurface.CreateGraphics();

            // Static global binds for important form elements
            gDebugText          = DebugAITrace;
            gWhiteScoreBoard    = WhiteScoreBoard;
            gBlackScoreBoard    = BlackScoreBoard;
            gCurrentTurnLabel   = CurrentTurnLabel;
            gAITurnWorker       = AITurnWorker;
            gDebugLogCheckBox   = DebugLogCheckBox;
            gSimTimerLabel      = SimTimerLabel;
            gNodeCounter        = NodeCounter;
            gWorkCounter        = WorkCounter;
            gVictoryCounter     = VictoryCounter;
            gCurrentTurnSurface = CurrentTurnImageSurface;
            gShowAvailableMoves = ShowAvailableMoves;

            // Game board and piece image handles
            gBoardImage      = Image.FromHbitmap(Properties.Resources.reversi_grid.GetHbitmap());
            gWhitePieceImage = Image.FromHbitmap(Properties.Resources.whitepiece.GetHbitmap());
            gBlackPieceImage = Image.FromHbitmap(Properties.Resources.blackpiece.GetHbitmap());

            // Graphic dimensions
            BoardPieceImageSize = gBlackPieceImage.Width;
            BoardGridSize = Properties.Settings.Default.GridSize;

            // AI Opponent worker thread settings
            AITurnWorker.WorkerSupportsCancellation = true;
            AITurnWorker.WorkerReportsProgress = true;

            // Setup form elements
            GridSizeDropDown.SelectedIndex = 4;
            SimTimerLabel.Text = "";
            UpdateMaxDepth();
            AIInfoTabControl.SelectTab(AISimTab);

            // Establish a static binding to the global game instance
            gCurrentGame = ReversiApplication.GetCurrentGame();
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
        private static int BoardPieceImageSize;
        private static int BoardGridSize;

        // The AI Database
        private AIDatabase DatabaseFactory = new AIDatabase();

        // The board used to track what has been drawn on the screen
        private static Board LastDrawnBoard = new Board();

        // The Global Game Object
        private static Game gCurrentGame;

        // Flags that determine who is playing (ai or human)
        private static Boolean VSComputer = true;
        private static int AIDifficulty = 1;

        #endregion

        #region Getters and Setters

        /// <summary>
        /// Gets the board that was last drawn onto the screen
        /// </summary>
        /// <returns>The last drawn Board object</returns>
        public static Board GetLastDrawnBoard() { return LastDrawnBoard; }

        /// <summary>
        /// Resets the board that was last drawn onto the screen
        /// </summary>
        /// <param name="BoardSize">The new board size</param>
        public static void SetLastDrawnBoard(int BoardSize) { LastDrawnBoard = new Board( BoardSize ); }

        /// <summary>
        /// True if the current game is versus the computer opponent
        /// </summary>
        public static Boolean VsComputer() { return VSComputer; }

        /// <summary>
        /// Returns the current computer difficulty setting
        /// </summary>
        public static int GetAIDifficulty() { return AIDifficulty; }

        /// <summary>
        /// Sets the difficulty of the computer opponent
        /// </summary>
        /// <param name="setAIDifficulty">The difficulty level (0-4)</param>
        public static void SetAIDifficulty(int setAIDifficulty) { AIDifficulty = setAIDifficulty; }

        /// <summary>
        /// Thread unsafe way to append text to the debug window
        /// </summary>
        /// <param name="newDebugText">The debug message to append</param>
        public static void AppendDebugText(String newDebugText) { gDebugText.Text += newDebugText; }

        /// <summary>
        /// Thread unsafe way to set the text of the debug window
        /// </summary>
        /// <param name="newDebugText">The debug message set</param>
        public static void SetDebugText( String newDebugText ) { gDebugText.Text = newDebugText; }
 
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
            WhiteScoreBoardTitle.Visible = true;
            BlackScoreBoardTitle.Visible = true;

            ReversiApplication.ResetCurrentGame(GetCurrentBoardSize());
            gCurrentGame = ReversiApplication.GetCurrentGame();

            gCurrentGame.GetAI().SetMaxDepth(SimulationDepthSlider.Value);

            gCurrentGame.GetAI().SetVisualizeProcess(VisualizeCheckbox.Checked);

            RefreshPieces(FullRefresh: true);
            MarkAvailableMoves(gCurrentGame.GetCurrentTurn());
            UpdateTurnImage(gCurrentGame.GetCurrentTurn());
            UpdateScoreBoard();
        }

        #region Game Board graphical manipulation methods

        /// <summary>
        /// Updates the score board for both players
        /// </summary>
        public static void UpdateScoreBoard()
        {
            gBlackScoreBoard.Text = gCurrentGame.GetGameBoard().FindScore(ReversiApplication.BLACK).ToString();
            gWhiteScoreBoard.Text = gCurrentGame.GetGameBoard().FindScore(ReversiApplication.WHITE).ToString();
            gBlackScoreBoard.Refresh();
            gWhiteScoreBoard.Refresh();
        }

        /// <summary>
        /// Draw all game pieces on the board
        /// </summary>
        /// <param name="FullRefresh">(optional) True forces a full refresh of all pieces</param>
        public static void RefreshPieces(bool FullRefresh = false)
        {
            RefreshPieces(gCurrentGame.GetGameBoard(), FullRefresh);
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

            for (int Y = 0; Y < SourceBoard.GetBoardSize(); Y++)
                for (int X = 0; X < SourceBoard.GetBoardSize(); X++)
                    if (( GetLastDrawnBoard().ColorAt(X, Y) != SourceBoard.ColorAt(X, Y)) || (FullRefresh))
                        DrawPiece(X, Y, SourceBoard.ColorAt(X, Y));

            GetLastDrawnBoard().CopyBoard(SourceBoard.GetBoardPieces());
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
                gBoardGFX.DrawImage(GetTurnImage(Color), X * BoardGridSize + 1, Y * BoardGridSize + 1, BoardPieceImageSize, BoardPieceImageSize);
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
            MarkAvailableMoves(gCurrentGame.GetGameBoard(), Turn);
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
                    gBoardGFX.FillEllipse(new SolidBrush(Color.DarkSeaGreen), CurrentPiece.X * BoardGridSize + BoardGridSize / 2 - 4, CurrentPiece.Y * BoardGridSize + BoardGridSize / 2 - 4, 8, 8);
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
            gBoardGFX.DrawEllipse(new Pen(PieceColor, 4), X * BoardGridSize + 5, Y * BoardGridSize + 5, BoardPieceImageSize - 8, BoardPieceImageSize - 8);

            StringFormat sf = new StringFormat();
            sf.LineAlignment = StringAlignment.Center;
            sf.Alignment = StringAlignment.Center;

            if( PieceLabel != "" )
                gBoardGFX.DrawString(PieceLabel, new Font("Tahoma", (float)9, FontStyle.Regular), Brushes.White, new RectangleF(X * BoardGridSize + 5, Y * BoardGridSize + 14, BoardGridSize - 10, BoardGridSize - 28), sf);
        }

        /// <summary>
        /// Returns the correct image for a given turn
        /// </summary>
        /// <param name="Turn">The turn</param>
        /// <returns>Image for the given turn</returns>
        public static Image GetTurnImage(int Turn)
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
                gCurrentTurnImageGFX.DrawImage(GetTurnImage(Turn), 0, 0, gCurrentTurnSurface.Width, gCurrentTurnSurface.Height);
            else
                gCurrentTurnImageGFX.DrawImage(GetTurnImage(Turn), 0, 0, gCurrentTurnSurface.Width, gCurrentTurnSurface.Height);
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
        private void CurrentTurnImageSurface_Paint(object sender, PaintEventArgs e)
        {
            UpdateTurnImage(gCurrentGame.GetCurrentTurn());
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
        public static void UpdateDatabaseProgress(TimeSpan TimeElapsed, int WorkNodeCount, int NodeTotal, int VictoryTotal)
        {
            gNodeCounter.Invoke(new updateDatabaseProgressDelegate(UpdateDatabaseProgressForm), TimeElapsed, WorkNodeCount, NodeTotal, VictoryTotal);
        }

        /// <summary>
        /// Thread UNSAFE way to update the database build progress form elements
        /// </summary>
        /// <param name="TimeElapsed">The time that has elapsed so far in the build</param>
        /// <param name="WorkNodeCount">The number of nodes sitting in the work queue</param>
        /// <param name="NodeTotal">The total number of nodes processed</param>
        /// <param name="VictoryTotal">The total number of victory states discovered so far</param>
        private static void UpdateDatabaseProgressForm(TimeSpan TimeElapsed, int WorkNodeCount, int NodeTotal, int VictoryTotal)
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
                CancelAIWorkers();
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
            VSComputer = false;
        }

        /// <summary>
        /// Player vs Computer menu option selected
        /// </summary>
        private void PvCMenu_Click(object sender, System.EventArgs e)
        {
            PvPMenu.Checked = false;
            PvCMenu.Checked = true;
            VSComputer = true;
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
        private void ShowAvailableMoves_Click(object sender, EventArgs e)
        {
            ShowAvailableMoves.Checked = !ShowAvailableMoves.Checked;
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
            gCurrentGame.SetProcessMoves(!gCurrentGame.GetProcessMoves());
            DebugProcess.Checked = !DebugProcess.Checked;
        }

        /// <summary>
        /// Start a new game scenario where white cannot move
        /// </summary>
        private void DebugScenario_NoWhite_Click(object sender, System.EventArgs e)
        {
            StartNewGame();
            gCurrentGame.GetGameBoard().ClearBoard();
            gCurrentGame.GetGameBoard().PutPiece(0, 0, ReversiApplication.BLACK);
            gCurrentGame.GetGameBoard().PutPiece(0, 1, ReversiApplication.BLACK);
            gCurrentGame.GetGameBoard().PutPiece(0, 2, ReversiApplication.BLACK);
            gCurrentGame.GetGameBoard().PutPiece(0, 3, ReversiApplication.BLACK);
            gCurrentGame.GetGameBoard().PutPiece(0, 4, ReversiApplication.BLACK);
            gCurrentGame.GetGameBoard().PutPiece(0, 5, ReversiApplication.BLACK);
            gCurrentGame.GetGameBoard().PutPiece(0, 6, ReversiApplication.BLACK);
            gCurrentGame.GetGameBoard().PutPiece(0, 7, ReversiApplication.BLACK);
            gCurrentGame.GetGameBoard().PutPiece(1, 0, ReversiApplication.WHITE);
            gCurrentGame.GetGameBoard().PutPiece(1, 1, ReversiApplication.WHITE);
            gCurrentGame.GetGameBoard().PutPiece(1, 2, ReversiApplication.WHITE);
            gCurrentGame.GetGameBoard().PutPiece(1, 3, ReversiApplication.WHITE);
            gCurrentGame.GetGameBoard().PutPiece(1, 4, ReversiApplication.WHITE);
            gCurrentGame.GetGameBoard().PutPiece(1, 5, ReversiApplication.WHITE);
            gCurrentGame.GetGameBoard().PutPiece(1, 6, ReversiApplication.WHITE);
            gCurrentGame.GetGameBoard().PutPiece(1, 7, ReversiApplication.WHITE);
            RefreshPieces(FullRefresh: true);
            UpdateScoreBoard();
            UpdateTurnImage(gCurrentGame.GetCurrentTurn());
            gCurrentGame.SetCurrentTurn(ReversiApplication.BLACK);
            StartAITurnWorker();
        }

        /// <summary>
        /// Sets up the a new game with the middle of the board already filled in
        /// </summary>
        private void DebugScenario_MidGame_Click(object sender, EventArgs e)
        {
            StartNewGame();
            gCurrentGame.GetGameBoard().PutPiece(2, 2, ReversiApplication.WHITE);
            gCurrentGame.GetGameBoard().PutPiece(2, 3, ReversiApplication.BLACK);
            gCurrentGame.GetGameBoard().PutPiece(2, 4, ReversiApplication.BLACK);
            gCurrentGame.GetGameBoard().PutPiece(2, 5, ReversiApplication.BLACK);
            gCurrentGame.GetGameBoard().PutPiece(3, 2, ReversiApplication.WHITE);
            gCurrentGame.GetGameBoard().PutPiece(3, 3, ReversiApplication.BLACK);
            gCurrentGame.GetGameBoard().PutPiece(3, 4, ReversiApplication.BLACK);
            gCurrentGame.GetGameBoard().PutPiece(3, 5, ReversiApplication.BLACK);
            gCurrentGame.GetGameBoard().PutPiece(4, 2, ReversiApplication.BLACK);
            gCurrentGame.GetGameBoard().PutPiece(4, 3, ReversiApplication.BLACK);
            gCurrentGame.GetGameBoard().PutPiece(4, 4, ReversiApplication.BLACK);
            gCurrentGame.GetGameBoard().PutPiece(4, 5, ReversiApplication.BLACK);
            gCurrentGame.GetGameBoard().PutPiece(5, 2, ReversiApplication.BLACK);
            gCurrentGame.GetGameBoard().PutPiece(5, 3, ReversiApplication.WHITE);
            gCurrentGame.GetGameBoard().PutPiece(5, 4, ReversiApplication.WHITE);
            gCurrentGame.GetGameBoard().PutPiece(5, 5, ReversiApplication.WHITE);
            RefreshPieces(FullRefresh: true);
            UpdateScoreBoard();
            UpdateTurnImage(gCurrentGame.GetCurrentTurn());
        }

        #endregion

        #region AI Database Form & Event Handelers

        /// <summary>
        /// Responds to the analyze database button press, starts the job
        /// </summary>
        private void BuildAIDBButton_Click(object sender, EventArgs e)
        {
            SetupSimulationForm();
            DBBuildWorker.RunWorkerAsync(GetCurrentBoardSize());
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
            ReversiApplication.ResetCurrentGame(BoardSize);
            gCurrentGame = ReversiApplication.GetCurrentGame();

            DatabaseFactory.BuildAIDatabase(DBBuildWorker, BoardSize, VisualizeCheckbox.Checked, true);
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
        private void DumpDBInfoButton_Click(object sender, EventArgs e)
        {
            gDebugText.Text += DatabaseFactory.DumpSimulationInfo();
        }

        /// <summary>
        /// Responds to the analyze database button press
        /// </summary>
        private void AnaylzeDBButton_Click(object sender, EventArgs e)
        {
            SetupSimulationForm();
            DBAnalysisWorker.RunWorkerAsync();
        }

        /// <summary>
        /// Responds to the "Cancel Job" button
        /// </summary>
        private void CancelButton_Click(object sender, EventArgs e)
        {
            CancelAIWorkers();
        }


        /// <summary>
        /// Cancels any of the background jobs that are currently running
        /// </summary>
        private void CancelAIWorkers()
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
            DatabaseFactory.AnalyzeAIDatabase(DBAnalysisWorker, VisualizeCheckbox.Checked, gDebugText, DisplayDebug);
        }

        /// <summary>
        /// Hides / Resets the form elements associated with the database builder
        /// </summary>
        private void ClearSimulationForm()
        {
            SimTimerLabel.Text = "";
            CancelBuildButton.Visible = false;
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
            CancelBuildButton.Visible = true;
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
            WhiteScoreBoardTitle.Visible = false;
            BlackScoreBoardTitle.Visible = false;
            gCurrentTurnSurface.Visible = false;
        }

        #endregion

        #region Miscellaneous Form Level Methods

        /// <summary>
        /// Returns an integer board size as selected on the form
        /// </summary>
        /// <returns>An integer board size as selected on the form</returns>
        private int GetCurrentBoardSize()
        {
            return (Convert.ToInt32(GridSizeDropDown.Items[GridSizeDropDown.SelectedIndex].ToString()));
        }

        /// <summary>
        /// Responds to the "New Game" button being clicked
        /// </summary>
        private void NewGameButton_Click(object sender, EventArgs e)
        {
            StartNewGame();
        }

        /// <summary>
        /// Responds to the MouseUp event on the board image, processes the click as a placed piece
        /// </summary>
        private void PlaceUserPiece(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            int x = (e.X + 1) / BoardGridSize;
            int y = (e.Y + 1) / BoardGridSize;

            // Don't process the mouse click if there is a turn already being processed
            if (!gCurrentGame.GetTurnInProgress())
                gCurrentGame.ProcessTurn(x, y);
        }
        
        /// <summary>
        /// If the grid size drop down changes, updates the board with the new dimensions
        /// </summary>
        private void GridSizeDropDown_SelectedIndexChanged(object sender, EventArgs e)
        {
            int newBoardSize = GetCurrentBoardSize();

            GridDimensionLabel.Text = newBoardSize + "x" + newBoardSize;

            BoardSurface.Width = BoardGridSize * newBoardSize;
            BoardSurface.Height = BoardGridSize * newBoardSize;

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
        public static void ReportDebugMessage(String newDebugMsg, bool updateConsole = false, bool updateWindow = true, bool overwrite = false)
        {
            if( gDebugLogCheckBox.Checked && updateWindow )
                if ( overwrite )
                    gDebugText.Invoke(new setDebugTextDelagate(SetDebugText), newDebugMsg + Environment.NewLine);
                else
                    gDebugText.Invoke(new appendDebugTextDelagate(AppendDebugText), newDebugMsg + Environment.NewLine);

            if (updateConsole)
                Console.WriteLine(newDebugMsg);
        }

        /// <summary>
        /// Thread safe way to clear the debug message box
        /// </summary>
        public static void ClearDebugMessage()
        {
            gDebugText.Invoke(new setDebugTextDelagate(SetDebugText), "");
        }

        /// <summary>
        /// Responds to the 'Clear' button on top of the debug window
        /// </summary>
        private void ClearDebugLogButton_Click(object sender, EventArgs e)
        {
            ClearDebugMessage();
        }

        /// <summary>
        /// Hides/Shows the debug panel
        /// </summary>
        private void HideDebugButton_Click(object sender, EventArgs e)
        {
            if (Width > 565)
            {
                Width = 565;
                HideDebugButton.Text = ">>\n>>\n>>";
                AIInfoTabControl.Visible = false;
                DebugAITrace.Visible = false;
                ClearDebugLogButton.Visible = false;
            }
            else
            {
                Width = 941;
                HideDebugButton.Text = "<<\n<<\n<<";
                AIInfoTabControl.Visible = true;
                DebugAITrace.Visible = true;
                ClearDebugLogButton.Visible = true;
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
            ShowWinner( gCurrentGame.GetGameBoard().DetermineWinner() );
            gCurrentGame.SetTurnInProgress(false);
            MarkAvailableMoves(gCurrentGame.GetCurrentTurn());
        }

        #endregion

        #region AI Simulation Tab event handlers

        /// <summary>
        /// Responds to the simulation depth slider being moved
        /// </summary>
        private void SimulationDepthSlider_Scroll(object sender, EventArgs e)
        {
            if (SimulationDepthSlider.Value % 2 != 0)
                SimulationDepthSlider.Value++;

            UpdateMaxDepth();
        }

        /// <summary>
        /// Updates the current game and form elements with the current simulation max depth
        /// </summary>
        private void UpdateMaxDepth()
        {
            SimDepthCount.Text = SimulationDepthSlider.Value.ToString();
            gCurrentGame.GetAI().SetMaxDepth(SimulationDepthSlider.Value - 1);
        }

        /// <summary>
        /// Responds to the visulize checkbox being changed
        /// </summary>
        private void VisualizeCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            gCurrentGame.GetAI().SetVisualizeProcess(VisualizeCheckbox.Checked);
        }

        #endregion
    }
}
