using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class ReversiWindow : Window
    {
        protected static GameBoard gGameBoardSurface;

        public ReversiWindow()
        {
            InitializeComponent();
            StartNewGame();

            gGameBoardSurface = GameBoardSurface;
        }

        private void PlaceUserPiece(object sender, MouseButtonEventArgs e)
        {
            Point PlayerMove = e.GetPosition(GameBoardSurface);

            int GridX = Convert.ToInt32(Math.Floor((PlayerMove.X) / Properties.Settings.Default.GRID_SIZE));
            int GridY = Convert.ToInt32(Math.Floor((PlayerMove.Y) / Properties.Settings.Default.GRID_SIZE));

            if (!App.GetCurrentGame().GetTurnInProgress())
                if (App.GetCurrentGame().ProcessUserTurn(GridX, GridY))
                    RefreshGameBoard();

            Console.WriteLine("Click at " + PlayerMove.X + "," + PlayerMove.Y );
        }

        public static void RefreshGameBoard()
        {
            if (gGameBoardSurface != null)
                gGameBoardSurface.Dispatcher.Invoke(InvalidateGameBoard);
        }

        public static void InvalidateGameBoard()
        {        
                gGameBoardSurface.InvalidateVisual();
        }

        /// <summary>
        /// Resets the form elements to prepare for a new game
        /// </summary>
        public static void StartNewGame()
        {
            // Start a new game
            App.ResetCurrentGame();

            // Setup the AI player
            App.GetComputerPlayer().SetMaxDepth(Properties.Settings.Default.MAX_SIM_DEPTH);
            App.GetComputerPlayer().SetVisualizeProcess(true);

            // Force a repaint of the game board and score board
            RefreshGameBoard();
        }
    }
}
