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
        public static int WHITE = Properties.Settings.Default.WHITE;
        public static int BLACK = Properties.Settings.Default.BLACK;
        public static int EMPTY = Properties.Settings.Default.EMPTY;
        public static int ERROR = Properties.Settings.Default.ERROR;

        protected static Game CurrentGame;
        protected static Grid gGameBoardGrid;

        protected static ImageSource gBlackPieceImage;
        protected static ImageSource gWhitePieceImage;
        protected static ImageSource gSuggestedPieceImage;

        // The board used to track what has been drawn on the screen
        protected static Board LastDrawnBoard;

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
        public static void ResetLastDrawnBoard(int BoardSize) { LastDrawnBoard = new Board(BoardSize); }

        /// <summary>
        /// Returns the global application game instance
        /// </summary>
        /// <returns>The current application game instance</returns>
        public static Game GetCurrentGame() { return CurrentGame; }

        #endregion

        /// <summary>
        /// Resets the global application game instance
        /// </summary>
        /// <param name="BoardSize">The size of the board to use in the new game</param>
        public static void ResetCurrentGame(int BoardSize = 8) { CurrentGame = new Game(BoardSize); }

        public ReversiWindow()
        {
            InitializeComponent();


            // Static binds
            //gGameBoardGrid = GameBoardGrid;
            //gWhitePieceImage.Source = .UriSource = new Uri("/Reversi;/img/WhitePiece.png");
            StartNewGame();
        }

        private void GameBoardSurface_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Point PlayerMove = e.GetPosition(GameBoardSurface);

            if (PlaceUserPiece(PlayerMove))
                GameBoardSurface.InvalidateVisual();

            Console.WriteLine("Click at " + PlayerMove.X + "," + PlayerMove.Y );
        }

        /// <summary>
        /// Responds to the MouseUp event on the board image, processes the click as a placed piece
        /// </summary>
        public static bool PlaceUserPiece(Point MouseClick)
        {
            int GridX = Convert.ToInt32(Math.Floor(( MouseClick.X ) / Properties.Settings.Default.GRID_SIZE));
            int GridY = Convert.ToInt32(Math.Floor(( MouseClick.Y ) / Properties.Settings.Default.GRID_SIZE));

            // Don't process the mouse click if there is a turn already being processed
            if (!GetCurrentGame().GetTurnInProgress())
                return (GetCurrentGame().ProcessTurn(GridX, GridY));

            return (false);
        }

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
    }
}
