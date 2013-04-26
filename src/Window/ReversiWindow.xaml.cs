﻿/// <summary>
/// Reversi.ReversiWindow.cs
/// Brian A. Hebert
/// </summary>

using System;
using System.Windows;
using System.Windows.Input;

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

        private static bool SinglePlayerButtonSelected = true;

        /// <summary>
        /// Creates a new instance of the main window
        /// </summary>
        public ReversiWindow()
        {
            InitializeComponent();

            gGameBoardSurface = GameBoardSurface;
            gScoreBoardSurface = ScoreBoardSurface;

            StartNewGame();
        }

        /// <summary>
        /// Responds to the MouseDown event on the click surface
        /// </summary>
        private void PlaceUserPiece(object sender, MouseButtonEventArgs e)
        {
            Point PlayerMove = e.GetPosition(GameBoardSurface);

            int GridClickX = Convert.ToInt32(Math.Floor((PlayerMove.X) / Properties.Settings.Default.GRID_SIZE));
            int GridClickY = Convert.ToInt32(Math.Floor((PlayerMove.Y) / Properties.Settings.Default.GRID_SIZE));

            if (!App.GetActiveGame().GetTurnInProgress())
                if (App.GetActiveGame().ProcessUserTurn(GridClickX, GridClickY))
                    GameBoard.Refresh();
        }

        /// <summary>
        /// Resets the form elements to prepare for a new game
        /// </summary>
        public static void StartNewGame()
        {
            // Start a new game
            App.ResetActiveGame();
            App.GetActiveGame().SetSinglePlayerGame(SinglePlayerButtonSelected);

            // Clear the display game board
            gGameBoardSurface.Clear();
            gScoreBoardSurface.Clear();

            // Setup the AI player
            App.GetComputerPlayer().SetMaxDepth(Properties.Settings.Default.MAX_SIM_DEPTH);
            App.GetComputerPlayer().SetVisualizeProcess(true);

            // Force a repaint of the game board and score board
            GameBoard.Refresh();
        }

        #region Top Menu Event Handlers

        /// <summary>
        /// Handles the MouseEnter event for the top button menu, lights the menu
        /// </summary>
        private void MenuButtonGrid_MouseEnter(object sender, MouseEventArgs e)
        {
            NewGameButton.Opacity = 1;
            TopMenuBorder.Opacity = 0.25;
            SinglePlayerButtonInactive.Opacity = 0.35;
            MultiPlayerButtonInactive.Opacity = 0.35;
            SinglePlayerButton.Opacity = 1;
            MultiPlayerButton.Opacity = 1;
        }

        /// <summary>
        /// Handles the MouseLeave event for the top button menu, dims the menu
        /// </summary>
        private void MenuButtonGrid_MouseLeave(object sender, MouseEventArgs e)
        {
            NewGameButton.Opacity = 0.1;
            TopMenuBorder.Opacity = 0.1;
            SinglePlayerButtonInactive.Opacity = 0.15;
            MultiPlayerButtonInactive.Opacity = 0.15;
            SinglePlayerButton.Opacity = 0.4;
            MultiPlayerButton.Opacity = 0.4;
        }
        
        /// <summary>
        /// Handles the MouseUp event for the new game button, starts a new game
        /// </summary>
        private void NewGameButton_MouseUp(object sender, MouseButtonEventArgs e)
        {
            StartNewGame();
            TopMenuBorder.Opacity = 0.25;

        }

        private void NewGameButton_MouseDown(object sender, MouseButtonEventArgs e)
        {
            TopMenuBorder.Opacity = 0.5;
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

        private void MultiPlayerButton_MouseDown(object sender, MouseButtonEventArgs e)
        {
            TopMenuBorder.Opacity = 0.5;
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

        private void SinglePlayerButton_MouseDown(object sender, MouseButtonEventArgs e)
        {
            TopMenuBorder.Opacity = 0.5;
        }

        private void SinglePlayerButton_MouseUp(object sender, MouseButtonEventArgs e)
        {
            SinglePlayerButtonSelected = true;
            TopMenuBorder.Opacity = 0.25;
            StartNewGame();
        }

        #endregion
    }
}