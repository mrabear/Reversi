/// <summary>
/// Reversi.ReversiWindow.cs
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

        /// <summary>
        /// Creates a new instance of the main window
        /// </summary>
        public ReversiWindow()
        {
            InitializeComponent();
            StartNewGame();

            gGameBoardSurface = GameBoardSurface;
            gScoreBoardSurface = ScoreBoardSurface;
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
                    RefreshGameBoard();
        }

        /// <summary>
        /// The thread safe way to refresh the game graphic elements
        /// </summary>
        public static void RefreshGameBoard()
        {
            if (gGameBoardSurface != null)
                gGameBoardSurface.Dispatcher.Invoke(InvalidateGraphics);
        }

        /// <summary>
        /// Thread unsafe way to refresh the game graphics
        /// </summary>
        private static void InvalidateGraphics()
        {        
            gGameBoardSurface.InvalidateVisual();
            gScoreBoardSurface.InvalidateVisual();
        }

        /// <summary>
        /// Resets the form elements to prepare for a new game
        /// </summary>
        public static void StartNewGame()
        {
            // Start a new game
            App.ResetActiveGame();

            // Setup the AI player
            App.GetComputerPlayer().SetMaxDepth(Properties.Settings.Default.MAX_SIM_DEPTH);
            App.GetComputerPlayer().SetVisualizeProcess(true);

            // Force a repaint of the game board and score board
            RefreshGameBoard();
        }
    }
}
