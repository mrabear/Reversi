/// <summary>
/// Reversi.ReversiForm.cs
/// </summary>

using System;
using System.Drawing;
using System.ComponentModel;
using System.Windows.Forms;

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

        protected Label Title;

        #region Form Designer Variables

        private IContainer components;
        protected Timer NewGameTimer;
        protected MainMenu MainDropDownMenu;
        protected Button BuildAIDBButton;
        protected CheckBox VisualizeCheckbox;
        protected GroupBox DBBuilderButtonsBox;
        protected Button AnaylzeDBButton;
        protected Label GridSizeTitleLabel;
        protected BackgroundWorker DBBuildWorker;
        protected Button CancelBuildButton;
        protected BackgroundWorker DBAnalysisWorker;
        protected ComboBox GridSizeDropDown;
        protected Label GridDimensionLabel;
        protected Label SimTimerLabel;
        protected Label NodeCounter;
        protected Label WorkCounter;
        protected Label NodeCounterLabel;
        protected Label WorkCounterLabel;
        protected Label VictoryCounterLabel;
        protected Label VictoryCounter;
        protected ProgressBar RAMUsageBar;
        protected Timer RAMCheckTimer;
        protected Label RAMLabel;
        protected BackgroundWorker AITurnWorker;
        protected Label SimDepthTitle;
        protected TrackBar SimulationDepthSlider;
        protected Label SimDepthCountLabel;
        protected Label SimDepthCount;
        #endregion
        protected RichTextBox DebugAITrace;
        protected Button ClearDebugLogButton;
        protected Label AITraceLabel;
        protected CheckBox DebugLogCheckBox;
        protected Panel ScoreBoardPanel;
        private Panel panel1;
        private PictureBox ShowDebugInfo;
        protected Label label1;
        protected CheckBox ShowAvailableMovesCheckbox;
        private PictureBox NewGameButton;
        protected Panel BoardSurface;

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ReversiForm));
            this.Title = new System.Windows.Forms.Label();
            this.MainDropDownMenu = new System.Windows.Forms.MainMenu(this.components);
            this.BuildAIDBButton = new System.Windows.Forms.Button();
            this.VisualizeCheckbox = new System.Windows.Forms.CheckBox();
            this.DBBuilderButtonsBox = new System.Windows.Forms.GroupBox();
            this.GridDimensionLabel = new System.Windows.Forms.Label();
            this.GridSizeDropDown = new System.Windows.Forms.ComboBox();
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
            this.SimDepthCountLabel = new System.Windows.Forms.Label();
            this.SimDepthCount = new System.Windows.Forms.Label();
            this.SimulationDepthSlider = new System.Windows.Forms.TrackBar();
            this.SimDepthTitle = new System.Windows.Forms.Label();
            this.AITurnWorker = new System.ComponentModel.BackgroundWorker();
            this.DebugAITrace = new System.Windows.Forms.RichTextBox();
            this.ClearDebugLogButton = new System.Windows.Forms.Button();
            this.AITraceLabel = new System.Windows.Forms.Label();
            this.DebugLogCheckBox = new System.Windows.Forms.CheckBox();
            this.BoardSurface = new System.Windows.Forms.Panel();
            this.ScoreBoardPanel = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.ShowAvailableMovesCheckbox = new System.Windows.Forms.CheckBox();
            this.ShowDebugInfo = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.NewGameButton = new System.Windows.Forms.PictureBox();
            this.DBBuilderButtonsBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SimulationDepthSlider)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ShowDebugInfo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.NewGameButton)).BeginInit();
            this.SuspendLayout();
            // 
            // Title
            // 
            this.Title.BackColor = System.Drawing.Color.Transparent;
            this.Title.Font = new System.Drawing.Font("Segoe UI", 39.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Title.ForeColor = System.Drawing.Color.White;
            this.Title.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.Title.Location = new System.Drawing.Point(17, 5);
            this.Title.Margin = new System.Windows.Forms.Padding(0);
            this.Title.Name = "Title";
            this.Title.Size = new System.Drawing.Size(340, 93);
            this.Title.TabIndex = 1;
            this.Title.Text = "Reversi";
            this.Title.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // BuildAIDBButton
            // 
            this.BuildAIDBButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BuildAIDBButton.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BuildAIDBButton.Location = new System.Drawing.Point(17, 33);
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
            this.VisualizeCheckbox.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.VisualizeCheckbox.ForeColor = System.Drawing.Color.White;
            this.VisualizeCheckbox.Location = new System.Drawing.Point(19, 189);
            this.VisualizeCheckbox.Margin = new System.Windows.Forms.Padding(0);
            this.VisualizeCheckbox.Name = "VisualizeCheckbox";
            this.VisualizeCheckbox.Size = new System.Drawing.Size(151, 21);
            this.VisualizeCheckbox.TabIndex = 8;
            this.VisualizeCheckbox.Text = "Display Visualizations";
            this.VisualizeCheckbox.UseVisualStyleBackColor = false;
            this.VisualizeCheckbox.CheckedChanged += new System.EventHandler(this.VisualizeCheckbox_CheckedChanged);
            // 
            // DBBuilderButtonsBox
            // 
            this.DBBuilderButtonsBox.BackColor = System.Drawing.Color.Transparent;
            this.DBBuilderButtonsBox.Controls.Add(this.GridDimensionLabel);
            this.DBBuilderButtonsBox.Controls.Add(this.GridSizeDropDown);
            this.DBBuilderButtonsBox.Controls.Add(this.GridSizeTitleLabel);
            this.DBBuilderButtonsBox.Controls.Add(this.AnaylzeDBButton);
            this.DBBuilderButtonsBox.Controls.Add(this.BuildAIDBButton);
            this.DBBuilderButtonsBox.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DBBuilderButtonsBox.ForeColor = System.Drawing.Color.White;
            this.DBBuilderButtonsBox.Location = new System.Drawing.Point(19, 8);
            this.DBBuilderButtonsBox.Name = "DBBuilderButtonsBox";
            this.DBBuilderButtonsBox.Size = new System.Drawing.Size(379, 160);
            this.DBBuilderButtonsBox.TabIndex = 9;
            this.DBBuilderButtonsBox.TabStop = false;
            this.DBBuilderButtonsBox.Text = "AI Database Job";
            // 
            // GridDimensionLabel
            // 
            this.GridDimensionLabel.AutoSize = true;
            this.GridDimensionLabel.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.GridDimensionLabel.ForeColor = System.Drawing.Color.White;
            this.GridDimensionLabel.Location = new System.Drawing.Point(219, 59);
            this.GridDimensionLabel.Name = "GridDimensionLabel";
            this.GridDimensionLabel.Size = new System.Drawing.Size(28, 17);
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
            this.GridSizeDropDown.Location = new System.Drawing.Point(166, 55);
            this.GridSizeDropDown.Name = "GridSizeDropDown";
            this.GridSizeDropDown.Size = new System.Drawing.Size(41, 25);
            this.GridSizeDropDown.TabIndex = 14;
            this.GridSizeDropDown.SelectedIndexChanged += new System.EventHandler(this.GridSizeDropDown_SelectedIndexChanged);
            // 
            // GridSizeTitleLabel
            // 
            this.GridSizeTitleLabel.AutoSize = true;
            this.GridSizeTitleLabel.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.GridSizeTitleLabel.ForeColor = System.Drawing.Color.White;
            this.GridSizeTitleLabel.Location = new System.Drawing.Point(153, 35);
            this.GridSizeTitleLabel.Name = "GridSizeTitleLabel";
            this.GridSizeTitleLabel.Size = new System.Drawing.Size(102, 15);
            this.GridSizeTitleLabel.TabIndex = 10;
            this.GridSizeTitleLabel.Text = "Game Board Size";
            // 
            // AnaylzeDBButton
            // 
            this.AnaylzeDBButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.AnaylzeDBButton.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.AnaylzeDBButton.Location = new System.Drawing.Point(17, 63);
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
            this.CancelBuildButton.Location = new System.Drawing.Point(303, 25);
            this.CancelBuildButton.Name = "CancelBuildButton";
            this.CancelBuildButton.Size = new System.Drawing.Size(64, 49);
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
            this.SimTimerLabel.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SimTimerLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.SimTimerLabel.Location = new System.Drawing.Point(285, 70);
            this.SimTimerLabel.Name = "SimTimerLabel";
            this.SimTimerLabel.Size = new System.Drawing.Size(98, 34);
            this.SimTimerLabel.TabIndex = 12;
            this.SimTimerLabel.Text = "01:12:12";
            this.SimTimerLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // NodeCounter
            // 
            this.NodeCounter.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.NodeCounter.ForeColor = System.Drawing.Color.White;
            this.NodeCounter.Location = new System.Drawing.Point(29, 124);
            this.NodeCounter.Name = "NodeCounter";
            this.NodeCounter.Size = new System.Drawing.Size(124, 32);
            this.NodeCounter.TabIndex = 13;
            this.NodeCounter.Text = "0";
            this.NodeCounter.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // WorkCounter
            // 
            this.WorkCounter.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.WorkCounter.ForeColor = System.Drawing.Color.White;
            this.WorkCounter.Location = new System.Drawing.Point(273, 124);
            this.WorkCounter.Name = "WorkCounter";
            this.WorkCounter.Size = new System.Drawing.Size(113, 32);
            this.WorkCounter.TabIndex = 14;
            this.WorkCounter.Text = "0";
            this.WorkCounter.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // NodeCounterLabel
            // 
            this.NodeCounterLabel.AutoSize = true;
            this.NodeCounterLabel.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.NodeCounterLabel.ForeColor = System.Drawing.Color.White;
            this.NodeCounterLabel.Location = new System.Drawing.Point(44, 110);
            this.NodeCounterLabel.Name = "NodeCounterLabel";
            this.NodeCounterLabel.Size = new System.Drawing.Size(102, 15);
            this.NodeCounterLabel.TabIndex = 16;
            this.NodeCounterLabel.Text = "Total Nodes in DB";
            // 
            // WorkCounterLabel
            // 
            this.WorkCounterLabel.AutoSize = true;
            this.WorkCounterLabel.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.WorkCounterLabel.ForeColor = System.Drawing.Color.White;
            this.WorkCounterLabel.Location = new System.Drawing.Point(287, 109);
            this.WorkCounterLabel.Name = "WorkCounterLabel";
            this.WorkCounterLabel.Size = new System.Drawing.Size(92, 15);
            this.WorkCounterLabel.TabIndex = 17;
            this.WorkCounterLabel.Text = "Nodes in Queue";
            // 
            // VictoryCounterLabel
            // 
            this.VictoryCounterLabel.AutoSize = true;
            this.VictoryCounterLabel.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.VictoryCounterLabel.ForeColor = System.Drawing.Color.White;
            this.VictoryCounterLabel.Location = new System.Drawing.Point(175, 110);
            this.VictoryCounterLabel.Name = "VictoryCounterLabel";
            this.VictoryCounterLabel.Size = new System.Drawing.Size(82, 15);
            this.VictoryCounterLabel.TabIndex = 19;
            this.VictoryCounterLabel.Text = "Total Victories";
            // 
            // VictoryCounter
            // 
            this.VictoryCounter.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.VictoryCounter.ForeColor = System.Drawing.Color.White;
            this.VictoryCounter.Location = new System.Drawing.Point(151, 124);
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
            this.RAMUsageBar.Location = new System.Drawing.Point(32, 330);
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
            this.RAMLabel.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RAMLabel.ForeColor = System.Drawing.Color.White;
            this.RAMLabel.Location = new System.Drawing.Point(32, 306);
            this.RAMLabel.Name = "RAMLabel";
            this.RAMLabel.Size = new System.Drawing.Size(84, 25);
            this.RAMLabel.TabIndex = 23;
            this.RAMLabel.Text = "Memory:";
            this.RAMLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.RAMLabel.Visible = false;
            // 
            // SimDepthCountLabel
            // 
            this.SimDepthCountLabel.AutoSize = true;
            this.SimDepthCountLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SimDepthCountLabel.ForeColor = System.Drawing.Color.White;
            this.SimDepthCountLabel.Location = new System.Drawing.Point(360, 258);
            this.SimDepthCountLabel.Name = "SimDepthCountLabel";
            this.SimDepthCountLabel.Size = new System.Drawing.Size(38, 26);
            this.SimDepthCountLabel.TabIndex = 41;
            this.SimDepthCountLabel.Text = "Turns\r\nAhead";
            // 
            // SimDepthCount
            // 
            this.SimDepthCount.Font = new System.Drawing.Font("Calibri", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SimDepthCount.ForeColor = System.Drawing.Color.White;
            this.SimDepthCount.Location = new System.Drawing.Point(352, 231);
            this.SimDepthCount.Name = "SimDepthCount";
            this.SimDepthCount.Size = new System.Drawing.Size(49, 31);
            this.SimDepthCount.TabIndex = 40;
            this.SimDepthCount.Text = "20";
            this.SimDepthCount.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // SimulationDepthSlider
            // 
            this.SimulationDepthSlider.LargeChange = 2;
            this.SimulationDepthSlider.Location = new System.Drawing.Point(19, 247);
            this.SimulationDepthSlider.Maximum = 20;
            this.SimulationDepthSlider.Minimum = 2;
            this.SimulationDepthSlider.Name = "SimulationDepthSlider";
            this.SimulationDepthSlider.Size = new System.Drawing.Size(327, 45);
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
            this.SimDepthTitle.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SimDepthTitle.ForeColor = System.Drawing.Color.White;
            this.SimDepthTitle.Location = new System.Drawing.Point(19, 221);
            this.SimDepthTitle.Name = "SimDepthTitle";
            this.SimDepthTitle.Size = new System.Drawing.Size(174, 30);
            this.SimDepthTitle.TabIndex = 36;
            this.SimDepthTitle.Text = "Simulation Depth";
            // 
            // AITurnWorker
            // 
            this.AITurnWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.AITurnMonitor_DoWork);
            this.AITurnWorker.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.AITurnMonitor_ProgressChanged);
            this.AITurnWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.AITurnMonitor_RunWorkerCompleted);
            // 
            // DebugAITrace
            // 
            this.DebugAITrace.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.DebugAITrace.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.DebugAITrace.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DebugAITrace.ForeColor = System.Drawing.Color.White;
            this.DebugAITrace.HideSelection = false;
            this.DebugAITrace.Location = new System.Drawing.Point(758, 457);
            this.DebugAITrace.Name = "DebugAITrace";
            this.DebugAITrace.Size = new System.Drawing.Size(413, 290);
            this.DebugAITrace.TabIndex = 5;
            this.DebugAITrace.Text = "";
            // 
            // ClearDebugLogButton
            // 
            this.ClearDebugLogButton.BackColor = System.Drawing.Color.Transparent;
            this.ClearDebugLogButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ClearDebugLogButton.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ClearDebugLogButton.ForeColor = System.Drawing.Color.White;
            this.ClearDebugLogButton.Location = new System.Drawing.Point(759, 425);
            this.ClearDebugLogButton.Name = "ClearDebugLogButton";
            this.ClearDebugLogButton.Size = new System.Drawing.Size(64, 23);
            this.ClearDebugLogButton.TabIndex = 36;
            this.ClearDebugLogButton.Text = "Clear";
            this.ClearDebugLogButton.UseVisualStyleBackColor = false;
            this.ClearDebugLogButton.Click += new System.EventHandler(this.ClearDebugLogButton_Click);
            // 
            // AITraceLabel
            // 
            this.AITraceLabel.BackColor = System.Drawing.Color.Transparent;
            this.AITraceLabel.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.AITraceLabel.ForeColor = System.Drawing.Color.White;
            this.AITraceLabel.Location = new System.Drawing.Point(758, 423);
            this.AITraceLabel.Name = "AITraceLabel";
            this.AITraceLabel.Size = new System.Drawing.Size(413, 27);
            this.AITraceLabel.TabIndex = 6;
            this.AITraceLabel.Text = "Debug Log";
            this.AITraceLabel.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // DebugLogCheckBox
            // 
            this.DebugLogCheckBox.AutoSize = true;
            this.DebugLogCheckBox.BackColor = System.Drawing.Color.Transparent;
            this.DebugLogCheckBox.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.DebugLogCheckBox.Checked = true;
            this.DebugLogCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.DebugLogCheckBox.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DebugLogCheckBox.ForeColor = System.Drawing.Color.White;
            this.DebugLogCheckBox.Location = new System.Drawing.Point(1085, 427);
            this.DebugLogCheckBox.Name = "DebugLogCheckBox";
            this.DebugLogCheckBox.Size = new System.Drawing.Size(75, 21);
            this.DebugLogCheckBox.TabIndex = 38;
            this.DebugLogCheckBox.Text = "Logging";
            this.DebugLogCheckBox.UseVisualStyleBackColor = false;
            // 
            // BoardSurface
            // 
            this.BoardSurface.BackColor = System.Drawing.Color.Transparent;
            this.BoardSurface.BackgroundImage = global::Reversi.Properties.Resources.GameBoard;
            this.BoardSurface.Cursor = System.Windows.Forms.Cursors.Hand;
            this.BoardSurface.ForeColor = System.Drawing.Color.Transparent;
            this.BoardSurface.Location = new System.Drawing.Point(30, 83);
            this.BoardSurface.Name = "BoardSurface";
            this.BoardSurface.Size = new System.Drawing.Size(640, 640);
            this.BoardSurface.TabIndex = 39;
            this.BoardSurface.Paint += new System.Windows.Forms.PaintEventHandler(this.BoardSurface_Paint);
            this.BoardSurface.MouseDown += new System.Windows.Forms.MouseEventHandler(this.BoardSurface_MouseDown);
            // 
            // ScoreBoardPanel
            // 
            this.ScoreBoardPanel.BackColor = System.Drawing.Color.Transparent;
            this.ScoreBoardPanel.Location = new System.Drawing.Point(355, 0);
            this.ScoreBoardPanel.Name = "ScoreBoardPanel";
            this.ScoreBoardPanel.Size = new System.Drawing.Size(343, 83);
            this.ScoreBoardPanel.TabIndex = 40;
            this.ScoreBoardPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.ScoreBoardPanel_Paint);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.ShowAvailableMovesCheckbox);
            this.panel1.Controls.Add(this.VisualizeCheckbox);
            this.panel1.Controls.Add(this.SimDepthCountLabel);
            this.panel1.Controls.Add(this.SimDepthCount);
            this.panel1.Controls.Add(this.RAMUsageBar);
            this.panel1.Controls.Add(this.SimulationDepthSlider);
            this.panel1.Controls.Add(this.SimDepthTitle);
            this.panel1.Controls.Add(this.WorkCounter);
            this.panel1.Controls.Add(this.RAMLabel);
            this.panel1.Controls.Add(this.NodeCounterLabel);
            this.panel1.Controls.Add(this.VictoryCounterLabel);
            this.panel1.Controls.Add(this.NodeCounter);
            this.panel1.Controls.Add(this.CancelBuildButton);
            this.panel1.Controls.Add(this.WorkCounterLabel);
            this.panel1.Controls.Add(this.VictoryCounter);
            this.panel1.Controls.Add(this.SimTimerLabel);
            this.panel1.Controls.Add(this.DBBuilderButtonsBox);
            this.panel1.Location = new System.Drawing.Point(758, 37);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(413, 368);
            this.panel1.TabIndex = 41;
            // 
            // ShowAvailableMovesCheckbox
            // 
            this.ShowAvailableMovesCheckbox.AutoSize = true;
            this.ShowAvailableMovesCheckbox.BackColor = System.Drawing.Color.Transparent;
            this.ShowAvailableMovesCheckbox.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ShowAvailableMovesCheckbox.Checked = true;
            this.ShowAvailableMovesCheckbox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ShowAvailableMovesCheckbox.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ShowAvailableMovesCheckbox.ForeColor = System.Drawing.Color.White;
            this.ShowAvailableMovesCheckbox.Location = new System.Drawing.Point(247, 189);
            this.ShowAvailableMovesCheckbox.Margin = new System.Windows.Forms.Padding(0);
            this.ShowAvailableMovesCheckbox.Name = "ShowAvailableMovesCheckbox";
            this.ShowAvailableMovesCheckbox.Size = new System.Drawing.Size(157, 21);
            this.ShowAvailableMovesCheckbox.TabIndex = 42;
            this.ShowAvailableMovesCheckbox.Text = "Show Available Moves";
            this.ShowAvailableMovesCheckbox.UseVisualStyleBackColor = false;
            // 
            // ShowDebugInfo
            // 
            this.ShowDebugInfo.BackColor = System.Drawing.Color.Transparent;
            this.ShowDebugInfo.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ShowDebugInfo.Image = global::Reversi.Properties.Resources.DebugButton;
            this.ShowDebugInfo.Location = new System.Drawing.Point(686, 347);
            this.ShowDebugInfo.Name = "ShowDebugInfo";
            this.ShowDebugInfo.Size = new System.Drawing.Size(32, 77);
            this.ShowDebugInfo.TabIndex = 42;
            this.ShowDebugInfo.TabStop = false;
            this.ShowDebugInfo.Click += new System.EventHandler(this.HideDebugButton_Click);
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(758, 6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(413, 27);
            this.label1.TabIndex = 43;
            this.label1.Text = "Debug Options";
            this.label1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // NewGameButton
            // 
            this.NewGameButton.BackColor = System.Drawing.Color.Transparent;
            this.NewGameButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.NewGameButton.Image = global::Reversi.Properties.Resources.NewGameButton;
            this.NewGameButton.Location = new System.Drawing.Point(277, 34);
            this.NewGameButton.Name = "NewGameButton";
            this.NewGameButton.Size = new System.Drawing.Size(69, 48);
            this.NewGameButton.TabIndex = 44;
            this.NewGameButton.TabStop = false;
            this.NewGameButton.Click += new System.EventHandler(this.NewGameButton_Click);
            this.NewGameButton.MouseEnter += new System.EventHandler(this.NewGameButton_MouseEnter);
            this.NewGameButton.MouseLeave += new System.EventHandler(this.NewGameButton_MouseLeave);
            // 
            // ReversiForm
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.BackgroundImage = global::Reversi.Properties.Resources.GreenBackground;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ClientSize = new System.Drawing.Size(1191, 756);
            this.Controls.Add(this.BoardSurface);
            this.Controls.Add(this.NewGameButton);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.ShowDebugInfo);
            this.Controls.Add(this.ScoreBoardPanel);
            this.Controls.Add(this.DebugLogCheckBox);
            this.Controls.Add(this.ClearDebugLogButton);
            this.Controls.Add(this.DebugAITrace);
            this.Controls.Add(this.AITraceLabel);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.Title);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Menu = this.MainDropDownMenu;
            this.Name = "ReversiForm";
            this.Text = "Reversi";
            this.DBBuilderButtonsBox.ResumeLayout(false);
            this.DBBuilderButtonsBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SimulationDepthSlider)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ShowDebugInfo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.NewGameButton)).EndInit();
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

            DatabaseFactory = new AIDatabase();
            LastDrawnBoard = new Board();

            // Esablish graphics handles
            //gGameBoardFrontBuffer   = GameBoard.CreateGraphics();
            gGameBoardBackBuffer    = new Bitmap(BoardSurface.Width, BoardSurface.Height);
            gGameBoardBackBufferGFX = Graphics.FromImage(gGameBoardBackBuffer);

            gScoreBoardGFX          = ScoreBoardPanel.CreateGraphics();

            // Static global binds for form elements
            gDebugText              = DebugAITrace;
            gAITurnWorker           = AITurnWorker;
            gDebugLogCheckBox       = DebugLogCheckBox;
            gSimTimerLabel          = SimTimerLabel;
            gNodeCounter            = NodeCounter;
            gWorkCounter            = WorkCounter;
            gVictoryCounter         = VictoryCounter;
            gScoreBoardSurface      = ScoreBoardPanel;
            gShowAvailableMoves     = ShowAvailableMovesCheckbox;            
            gRAMUsageBar            = RAMUsageBar;
            gDBBuildWorker          = DBBuildWorker;
            gDBAnalysisWorker       = DBAnalysisWorker;
            gSimulationDepthSlider  = SimulationDepthSlider;
            gVisualizeCheckbox      = VisualizeCheckbox;
            gSimDepthCount          = SimDepthCount;
            gCancelBuildButton      = CancelBuildButton;
            gRAMLabel               = RAMLabel;
            gRAMCheckTimer          = RAMCheckTimer;
            gGridSizeDropDown       = GridSizeDropDown;
            gGridDimensionLabel     = GridDimensionLabel;
            gGameBoardSurface       = BoardSurface;

            // Graphic dimensions
            BoardGridSize           = Properties.Settings.Default.GridSize;

            // AI Opponent worker thread settings
            AITurnWorker.WorkerSupportsCancellation = true;
            AITurnWorker.WorkerReportsProgress = true;

            // Setup form elements
            GridSizeDropDown.SelectedIndex = 4;
            SimTimerLabel.Text = "";
            FormUtil.UpdateMaxDepth();
            //AIInfoTabControl.SelectTab(AISimTab);

            // Setup game opponent
            VSComputer = true;
            AIDifficulty = 1;

            MainDropDownMenu.Dispose();

            // Establish a static binding to the global game instance
            gCurrentGame = ReversiApplication.GetCurrentGame();
        }

        #region Global Variables

        // Static handles to graphical assets
        protected static Graphics           gGameBoardFrontBuffer;
        protected static Graphics           gCurrentTurnImageGFX;
        protected static Graphics           gScoreBoardGFX;
        protected static Graphics           gGameBoardBackBufferGFX;
        protected static Bitmap             gGameBoardBackBuffer;

        // Static handles to form objects
        protected static Panel              gGameBoardSurface;
        protected static Panel              gScoreBoardSurface;
        protected static BackgroundWorker   gAITurnWorker;
        protected static BackgroundWorker   gDBBuildWorker;
        protected static BackgroundWorker   gDBAnalysisWorker;
        protected static CheckBox           gShowAvailableMoves;
        protected static CheckBox           gDebugLogCheckBox;
        protected static CheckBox           gVisualizeCheckbox;
        protected static Button             gCancelBuildButton;
        protected static RichTextBox        gDebugText;
        protected static ProgressBar        gRAMUsageBar;
        protected static TrackBar           gSimulationDepthSlider;
        protected static Timer              gRAMCheckTimer;
        protected static ComboBox           gGridSizeDropDown;
        protected static Label              gSimTimerLabel;
        protected static Label              gNodeCounter;
        protected static Label              gWorkCounter;
        protected static Label              gVictoryCounter;
        protected static Label              gSimDepthCount;
        protected static Label              gRAMLabel;
        protected static Label              gGridDimensionLabel;

        // Piece dimensions
        protected static int                BoardGridSize;

        // The AI Database
        protected static AIDatabase         DatabaseFactory;

        // The board used to track what has been drawn on the screen
        protected static Board              LastDrawnBoard;

        // The Global Game Object
        protected static Game               gCurrentGame;

        // Flags that determine who is playing (ai or human)
        protected static Boolean            VSComputer;
        protected static int                AIDifficulty ;

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

        #region Form Event Handelers

        #region Paint Event Handelers

        /// <summary>
        /// Called when the "Game Board" is repainted, refreshes the pieces
        /// </summary>
        public void BoardSurface_Paint(object sender, PaintEventArgs e)
        {
            GraphicsUtil.RefreshPieces(FullRefresh: true);
            GraphicsUtil.MarkAvailableMoves(gCurrentGame.GetCurrentTurn());
            GraphicsUtil.PromoteBackBuffer();
            //gGameBoardSurface.CreateGraphics().DrawImage(gGameBoardBackBuffer, 0, 0);

        }

        /// <summary>
        /// Called when the "Score Board" is repainted, refreshes the current turn image
        /// </summary>
        private void ScoreBoardPanel_Paint(object sender, PaintEventArgs e)
        {
            GraphicsUtil.UpdateScoreBoard(gCurrentGame.GetCurrentTurn());
        }

        #endregion

        #region AI Database Form & Event Handelers

        /// <summary>
        /// Responds to the analyze database button press, starts the job
        /// </summary>
        private void BuildAIDBButton_Click(object sender, EventArgs e)
        {
            FormUtil.SetupSimulationForm();
            DBBuildWorker.RunWorkerAsync(FormUtil.GetCurrentBoardSize());
        }

        /// <summary>
        /// Starts the database background worker (button press)
        /// </summary>
        private void DBBuildWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            FormUtil.StartBuildDB(Convert.ToInt32(e.Argument.ToString()));
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
            GraphicsUtil.UpdateRAMprogress();
        }

        /// <summary>
        /// Runs when the database background worker thread is finished
        /// </summary>
        private void DBBuildWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            FormUtil.ClearSimulationForm();
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
            FormUtil.SetupSimulationForm();
            DBAnalysisWorker.RunWorkerAsync();
        }

        /// <summary>
        /// Responds to the "Cancel Job" button
        /// </summary>
        private void CancelButton_Click(object sender, EventArgs e)
        {
            FormUtil.CancelAIWorkers();
            FormUtil.ClearSimulationForm();
        }

        /// <summary>
        /// Starts of the database analysis background worker
        /// </summary>
        private void DBAnalysisWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            Boolean DisplayDebug = true;
            DatabaseFactory.AnalyzeAIDatabase(DBAnalysisWorker, VisualizeCheckbox.Checked, gDebugText, DisplayDebug);
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

            FormUtil.UpdateMaxDepth();
        }

        /// <summary>
        /// Responds to the visulize checkbox being changed
        /// </summary>
        private void VisualizeCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            gCurrentGame.GetAI().SetVisualizeProcess(VisualizeCheckbox.Checked);
        }

        #endregion

        #region AI Turn BG Worker Event Handelers


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
        /// Called mid-way through an AI turn analysis, used to report back progress
        /// </summary>
        public static void ReportAITurnWorkerProgress(int progress = 0)
        {
            gAITurnWorker.ReportProgress(progress);
        }

        /// <summary>
        /// Called when the AI monitor has no more moves to place
        /// </summary>
        public void AITurnMonitor_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            FormUtil.AIWorkerCompleted();
        }

        #endregion

        #region Debug Form Element Event Handelers

        /// <summary>
        /// If the grid size drop down changes, updates the board with the new dimensions
        /// </summary>
        private void GridSizeDropDown_SelectedIndexChanged(object sender, EventArgs e)
        {
            FormUtil.ChangeGameBoardSize();
        }

        /// <summary>
        /// Responds to the 'Clear' button on top of the debug window
        /// </summary>
        private void ClearDebugLogButton_Click(object sender, EventArgs e)
        {
            FormUtil.ClearDebugMessage();
        }

        /// <summary>
        /// Hides/Shows the debug panel
        /// </summary>
        private void HideDebugButton_Click(object sender, EventArgs e)
        {
            ToggleDebugVisibility();
        }

        /// <summary>
        /// Hides/Shows the debug panel
        /// </summary>
        public void ToggleDebugVisibility()
        {
            //ShowDebugInfo.Image.RotateFlip(RotateFlipType.Rotate90FlipX);

            if (Width < 800)
            {
                Width = 1207;
                DebugAITrace.Visible = true;
                ClearDebugLogButton.Visible = true;
            }
            else
            {
                Width = 750;
                DebugAITrace.Visible = false;
                ClearDebugLogButton.Visible = false;
            }
        }

        #endregion

        #region Game Flow Event Handelers

        /// <summary>
        /// Responds to the "New Game" button being clicked
        /// </summary>
        private void NewGameButton_Click(object sender, EventArgs e)
        {
            FormUtil.StartNewGame();
        }

        /// <summary>
        /// Responds to the mouse being pushed down on the game board, attempts to place a piece there
        /// </summary>
        private void BoardSurface_MouseDown(object sender, MouseEventArgs e)
        {
            FormUtil.PlaceUserPiece(e.X, e.Y);
        }

        /// <summary>
        /// Starts a new game 100ms after the form has loaded
        /// </summary>
        private void NewGameTimer_Tick(object sender, EventArgs e)
        {
            FormUtil.StartNewGame();
            NewGameTimer.Enabled = false;
            ToggleDebugVisibility();
        }

        private void NewGameButton_MouseEnter(object sender, EventArgs e)
        {
            NewGameButton.Image = Reversi.Properties.Resources.NewGameButton_Hover;
        }

        private void NewGameButton_MouseLeave(object sender, EventArgs e)
        {
            NewGameButton.Image = Reversi.Properties.Resources.NewGameButton;
        }

        #endregion

        #endregion
    }
}
