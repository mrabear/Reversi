﻿/// <summary>
/// Reversi.ReversiWindow.cs
/// Brian A. Hebert
/// </summary>

using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Controls;

namespace Reversi
{
    /// <summary>
    /// The main application window
    /// </summary>
    public partial class ReversiWindow : Window
    {
        // Static copies of the drawing surfaces
        private static GameBoard gGameBoardSurface;
        private static ScoreBoard gScoreBoardSurface;

        // True if the single player button is the currently active
        private static bool SinglePlayerButtonSelected = true;

        // Returns the instance of the game board drawing surface
        public static GameBoard GetGameBoardSurface() { return gGameBoardSurface; }

        /// <summary>
        /// Creates a new instance of the main window
        /// </summary>
        public ReversiWindow()
        {
            // Initialize the WPF form components (autogenerated code)
            InitializeComponent();

            // Bind the current form drawing surfaces to static objects
            gGameBoardSurface = GameBoardSurface;
            gScoreBoardSurface = ScoreBoardSurface;

            // Start a new game
            StartNewGame();
        }

        /// <summary>
        /// Responds to the MouseDown event on the click surface
        /// </summary>
        private void PlaceUserPiece(object sender, MouseButtonEventArgs e)
        {
            // Find where the mouse was clicked
            Point PlayerMove = e.GetPosition(GameBoardSurface);

            // Conver the mouse location to a grid location
            int GridClickX = Convert.ToInt32(Math.Floor((PlayerMove.X) / Properties.Settings.Default.GRID_SIZE));
            int GridClickY = Convert.ToInt32(Math.Floor((PlayerMove.Y) / Properties.Settings.Default.GRID_SIZE));

            // If there isn't a turn in progress, attempt to execute the given move
            if (!App.GetActiveGame().GetTurnInProgress())
                if (App.GetActiveGame().ProcessUserTurn(GridClickX, GridClickY))
                    gGameBoardSurface.Refresh();

            // Reset the next/previous button states
            UpdateMoveChangeButtons();
        }

        /// <summary>
        /// Resets the form elements to prepare for a new game
        /// </summary>
        public void StartNewGame()
        {
            // Start a new game
            App.ResetActiveGame();
            App.GetActiveGame().SetSinglePlayerGame(SinglePlayerButtonSelected);

            // Clear the display game board
            gGameBoardSurface.Clear();
            ScoreBoard.Clear();

            // Setup the AI player
            App.GetComputerPlayer().SetMaxDepth(Properties.Settings.Default.MAX_SIM_DEPTH);
            App.GetComputerPlayer().SetVisualizeProcess(true);

            // Force a repaint of the game board and score board
            gGameBoardSurface.Refresh();

            // Reset the next/previous button states
            UpdateMoveChangeButtons();
        }

        #region Top Menu Event Handlers
        
        /// <summary>
        /// Handles the MouseUp event for the new game button, starts a new game
        /// </summary>
        private void NewGameButton_MouseUp(object sender, MouseButtonEventArgs e)
        {
            StartNewGame();
            TopMenuBorder.Opacity = 0.25;
        }

        /// <summary>
        /// Handles the MouseDown event for the new game button, starts a new game
        /// </summary>
        private void TopMenuBorderGlow(object sender, MouseButtonEventArgs e)
        {
            TopMenuBorder.Opacity = 0.6;
        }

        private void HighlightGameType(bool SinglePlayerGame)
        {
            if (SinglePlayerGame)
            {
                SinglePlayerButton.Visibility = Visibility.Visible;
                SinglePlayerButtonInactive.Visibility = Visibility.Hidden;

                MultiPlayerButton.Visibility = Visibility.Hidden;
                MultiPlayerButtonInactive.Visibility = Visibility.Visible;
            }
            else
            {
                SinglePlayerButton.Visibility = Visibility.Hidden;
                SinglePlayerButtonInactive.Visibility = Visibility.Visible;

                MultiPlayerButton.Visibility = Visibility.Visible;
                MultiPlayerButtonInactive.Visibility = Visibility.Hidden;
            }
        }

        private void MultiPlayerButton_MouseEnter(object sender, MouseEventArgs e)
        {
            HighlightGameType(false);
        }

        private void MultiPlayerButton_MouseLeave(object sender, MouseEventArgs e)
        {
            HighlightGameType(SinglePlayerButtonSelected);
        }

        private void MultiPlayerButton_MouseUp(object sender, MouseButtonEventArgs e)
        {
            SinglePlayerButtonSelected = false;
            TopMenuBorder.Opacity = 0.25;
            StartNewGame();
        }

        private void SinglePlayerButton_MouseEnter(object sender, MouseEventArgs e)
        {
            HighlightGameType(true);
        }

        private void SinglePlayerButton_MouseLeave(object sender, MouseEventArgs e)
        {
            HighlightGameType(SinglePlayerButtonSelected);
        }

        private void SinglePlayerButton_MouseUp(object sender, MouseButtonEventArgs e)
        {
            SinglePlayerButtonSelected = true;
            TopMenuBorder.Opacity = 0.25;
            StartNewGame();
        }

        private void PreviousMoveButton_MouseUp(object sender, MouseButtonEventArgs e)
        {
            TopMenuBorder.Opacity = 0.25;

            if (App.GetActiveGame().CanRewind())
                App.GetActiveGame().RewindHistoricalState();

            UpdateMoveChangeButtons();
        }

        private void NextMoveButton_MouseUp(object sender, MouseButtonEventArgs e)
        {
            TopMenuBorder.Opacity = 0.25;
            
            if (App.GetActiveGame().CanAdvance())
                App.GetActiveGame().AdvanceHistoricalState();
            
            UpdateMoveChangeButtons();
        }

        /// <summary>
        /// Activates / Deactivates the game state change buttons depending on whether or not they can be activated
        /// </summary>
        private void UpdateMoveChangeButtons()
        {
            if (App.GetActiveGame().CanAdvance())
            {
                NextMoveButton.Opacity = 1;
                NextMoveButton.ToolTip = "Next Move";
            }
            else
            {
                NextMoveButton.Opacity = 0.1;
                NextMoveButton.ToolTip = null;
            }

            if (App.GetActiveGame().CanRewind())
            {
                PreviousMoveButton.Opacity = 1;
                PreviousMoveButton.ToolTip = "Previous Move";
            }
            else
            {
                PreviousMoveButton.Opacity = 0.1;
                PreviousMoveButton.ToolTip = null;
            }
        }

        #endregion
    }
}
