/// <summary>
/// Reversi.ReversiForm.FormUtil.cs
/// </summary>

using System;
using System.Drawing;
using System.Windows.Forms;
using System.Management;

namespace Reversi
{
    /// <summary>
    /// A subclass of ReversiForm, used to manipulate the various form elements
    /// </summary>
    public class FormUtil : ReversiForm
    {
        /// <summary>
        /// Resets the form elements to prepare for a new game
        /// </summary>
        public static void StartNewGame()
        {
            gBlackScoreBoard.Text = "0";
            gWhiteScoreBoard.Text = "0";
            gCurrentTurnLabel.Text = "Turn:";
            gCurrentTurnSurface.Visible = true;

            // Reset the score board
            gBlackScoreBoard.Visible = true;
            gWhiteScoreBoard.Visible = true;
            gCurrentTurnLabel.Visible = true;
            gWhiteScoreBoardTitle.Visible = true;
            gBlackScoreBoardTitle.Visible = true;

            ReversiApplication.ResetCurrentGame(GetCurrentBoardSize());
            gCurrentGame = ReversiApplication.GetCurrentGame();

            gCurrentGame.GetAI().SetMaxDepth(gSimulationDepthSlider.Value);
            gCurrentGame.GetAI().SetVisualizeProcess(gVisualizeCheckbox.Checked);

            GraphicsUtil.RefreshPieces(FullRefresh: true);
            GraphicsUtil.MarkAvailableMoves(gCurrentGame.GetCurrentTurn());
            GraphicsUtil.UpdateTurnImage(gCurrentGame.GetCurrentTurn());
            GraphicsUtil.UpdateScoreBoard();
        }

        /// <summary>
        /// Updates the current game and form elements with the current simulation max depth
        /// </summary>
        public static void UpdateMaxDepth()
        {
            gSimDepthCount.Text = gSimulationDepthSlider.Value.ToString();
            gCurrentGame.GetAI().SetMaxDepth(gSimulationDepthSlider.Value - 1);
        }

        /// <summary>
        /// Hides / Resets the form elements associated with the database builder
        /// </summary>
        public static void ClearSimulationForm()
        {
            gSimTimerLabel.Text = "";
            gCancelBuildButton.Visible = false;
            gRAMCheckTimer.Enabled = false;
            gRAMUsageBar.Visible = false;
            gRAMLabel.Visible = false;
            gRAMUsageBar.Maximum = 100;
        }

        /// <summary>
        /// Cancels any of the background jobs that are currently running
        /// </summary>
        public static void CancelAIWorkers()
        {
            if (gDBBuildWorker.IsBusy)
                gDBBuildWorker.CancelAsync();

            if (gDBAnalysisWorker.IsBusy)
                gDBAnalysisWorker.CancelAsync();
        }


        /// <summary>
        /// Displays the form elements associated with the database builder
        /// </summary>
        public static void SetupSimulationForm()
        {
            gCancelBuildButton.Visible = true;
            gDBAnalysisWorker.WorkerSupportsCancellation = true;
            gDBAnalysisWorker.WorkerReportsProgress = true;
            gDBBuildWorker.WorkerSupportsCancellation = true;
            gDBBuildWorker.WorkerReportsProgress = true;
            gRAMCheckTimer.Enabled = true;
            gRAMUsageBar.Visible = true;
            gRAMLabel.Visible = true;
            GraphicsUtil.UpdateRAMprogress();

            // Reset the score board
            gBlackScoreBoard.Visible = false;
            gWhiteScoreBoard.Visible = false;
            gCurrentTurnLabel.Visible = false;
            gWhiteScoreBoardTitle.Visible = false;
            gBlackScoreBoardTitle.Visible = false;
            gCurrentTurnSurface.Visible = false;
        }

        /// <summary>
        /// Starts the database build background worker
        /// </summary>
        public static void StartBuildDB(int BoardSize = 4)
        {
            ReversiApplication.ResetCurrentGame(BoardSize);
            gCurrentGame = ReversiApplication.GetCurrentGame();

            DatabaseFactory.BuildAIDatabase(gDBBuildWorker, BoardSize, gVisualizeCheckbox.Checked, true);
        }

        /// <summary>
        /// Returns an integer board size as selected on the form
        /// </summary>
        /// <returns>An integer board size as selected on the form</returns>
        public static int GetCurrentBoardSize()
        {
            return (Convert.ToInt32(gGridSizeDropDown.Items[gGridSizeDropDown.SelectedIndex].ToString()));
        }

        /// <summary>
        /// Responds to the MouseUp event on the board image, processes the click as a placed piece
        /// </summary>
        public static void PlaceUserPiece(int MouseX, int MouseY)
        {
            int X = (MouseX + 1) / BoardGridSize;
            int Y = (MouseY + 1) / BoardGridSize;

            // Don't process the mouse click if there is a turn already being processed
            if (!gCurrentGame.GetTurnInProgress())
                gCurrentGame.ProcessTurn(X, Y);
        }

        /// <summary>
        /// Changes the size of the game board
        /// </summary>
        public static void ChangeGameBoardSize()
        {
            int newBoardSize = GetCurrentBoardSize();

            gGridDimensionLabel.Text = newBoardSize + "x" + newBoardSize;

            gBoardSurface.Width = BoardGridSize * newBoardSize;
            gBoardSurface.Height = BoardGridSize * newBoardSize;

            StartNewGame();
        }

        /// <summary>
        /// Thread safe delegates for setting the debug window with the new text
        /// </summary>
        /// <param name="newText">The information to report</param>
        public delegate void setDebugTextDelagate(string newText);

        /// <summary>
        /// Thread safe delegates for appending to the debug window with the new text
        /// </summary>
        /// <param name="newText">The information to report</param>
        public delegate void appendDebugTextDelagate(string newText);

        /// <summary>
        /// Thread safe way to update the debug message box
        /// </summary>
        /// <param name="newDebugMsg">The information to report</param>
        /// <param name="updateConsole">(optional: false) True to update the console</param>
        /// <param name="updateWindow">(optional: true) True to update the debug window</param>
        /// <param name="overwrite">(optional: false) To reset the debug window</param>
        public static void ReportDebugMessage(String newDebugMsg, bool updateConsole = false, bool updateWindow = true, bool overwrite = false)
        {
            if (gDebugLogCheckBox.Checked && updateWindow)
                if (overwrite)
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
        /// Starts the AI turn worker job, which will analyze the board and make a move
        /// </summary>
        public static void StartAITurnWorker()
        {
            gAITurnWorker.RunWorkerAsync();
        }

        /// <summary>
        /// Updates the various screen elements after the AI worker has completed it's turn
        /// </summary>
        public static void AIWorkerCompleted()
        {
            gCurrentGame.SetTurnInProgress(false);
            gCurrentGame.SwitchTurn();
            GraphicsUtil.UpdateScoreBoard();
            GraphicsUtil.ShowWinner(gCurrentGame.GetGameBoard().DetermineWinner());
            GraphicsUtil.MarkAvailableMoves(gCurrentGame.GetCurrentTurn());
        }
    }
}