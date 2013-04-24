/// <summary>
/// Reversi.ReversiForm.FormUtil.cs
/// </summary>

using System;
using System.Management;

using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Reversi
{
    /// <summary>
    /// A subclass of ReversiForm, used to manipulate the various form elements
    /// </summary>
    public class FormUtil : ReversiWindow
    {
        /// <summary>
        /// Resets the form elements to prepare for a new game
        /// </summary>
        public static void StartNewGame()
        {
            // Start a new game
            ResetCurrentGame();

            // Setup the AI player
            GetCurrentGame().GetAI().SetMaxDepth(Properties.Settings.Default.MAX_SIM_DEPTH);
            GetCurrentGame().GetAI().SetVisualizeProcess(true);



            // Force a repaint of the game board and score board
            //gGameBoardSurface.Invalidate();
            //gScoreBoardSurface.Invalidate();
        }

        /// <summary>
        /// Updates the current game and form elements with the current simulation max depth
        /// </summary>
        public static void UpdateMaxDepth()
        {
            //gSimDepthCount.Text = gSimulationDepthSlider.Value.ToString();
            //gCurrentGame.GetAI().SetMaxDepth(gSimulationDepthSlider.Value - 1);
        }

        /// <summary>
        /// Responds to the MouseUp event on the board image, processes the click as a placed piece
        /// </summary>
        public static bool PlaceUserPiece( Point MouseClick )
        {
            // Don't process the mouse click if there is a turn already being processed
            if (!GetCurrentGame().GetTurnInProgress())
                return( GetCurrentGame().ProcessTurn(Convert.ToInt32( (MouseClick.X + 1) / Properties.Settings.Default.GRID_SIZE ), Convert.ToInt32( (MouseClick.Y + 1) / Properties.Settings.Default.GRID_SIZE )));

            return (false);
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
            /*
            if (gDebugLogCheckBox.Checked && updateWindow)
                if (overwrite)
                    gDebugText.Invoke(new setDebugTextDelagate(SetDebugText), newDebugMsg + Environment.NewLine);
                else
                    gDebugText.Invoke(new appendDebugTextDelagate(AppendDebugText), newDebugMsg + Environment.NewLine);

            if (updateConsole)
                Console.WriteLine(newDebugMsg);
             */
        }

        /// <summary>
        /// Thread safe way to clear the debug message box
        /// </summary>
        public static void ClearDebugMessage()
        {
            //gDebugText.Invoke(new setDebugTextDelagate(SetDebugText), "");
        }
    }
}