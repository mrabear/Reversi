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
using System.Threading;
using System.Windows.Threading;

public static class ExtensionMethods
{

    private static Action EmptyDelegate = delegate() { };


    public static void Refresh(this UIElement uiElement)
    {
        uiElement.Dispatcher.Invoke(DispatcherPriority.Render, EmptyDelegate);
    }
}

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

        /// <summary>
        /// Returns the global application game instance
        /// </summary>
        /// <returns>The current application game instance</returns>
        public static ImageSource GetGamePiece( int PieceColor) {
            if (PieceColor == WHITE)
                return gWhitePieceImage;
            else if (PieceColor == BLACK)
                return gBlackPieceImage;
            else
                return gSuggestedPieceImage;
        }

        public ReversiWindow()
        {
            InitializeComponent();


            // Static binds
            //gGameBoardGrid = GameBoardGrid;
            //gWhitePieceImage.Source = .UriSource = new Uri("/Reversi;/img/WhitePiece.png");
            FormUtil.StartNewGame();
        }

        private void GameBoardSurface_MouseDown(object sender, MouseButtonEventArgs e)
        {
            //gBlackPieceImage = new ImageSourceConverter().ConvertFromString("/Reversi;component/img/BlackPiece.png") as ImageSource;
            //gWhitePieceImage = new ImageSourceConverter().ConvertFromString("/Reversi;component/img/WhitePiece.png") as ImageSource;
            //gSuggestedPieceImage = new ImageSourceConverter().ConvertFromString("/Reversi;component/img/SuggestedPiece.png") as ImageSource;


        }

        private void Reversi_Window_MouseDown(object sender, MouseButtonEventArgs e)
        {

            Point PlayerMove = e.GetPosition(this);

            if (FormUtil.PlaceUserPiece(PlayerMove))
            {
                GameBoardSurface.UpdateBoard(CurrentGame.GetGameBoard());
                GameBoardSurface.Refresh();
            }

            Console.WriteLine("mouseLeft is clicked");
        }
    }
}
