/// <summary>
/// Reversi.GameBoard.cs
/// Brian A. Hebert
/// </summary>

using System;
using System.Windows;
using System.Windows.Media;

namespace Reversi
{
    /// <summary>
    /// The window component that the game board is drawn on
    /// </summary>
    public class GameBoard : FrameworkElement
    {
        // Static binds to game board images
        private static ImageSource gBlackPieceImage;
        private static ImageSource gWhitePieceImage;
        private static ImageSource gSuggestedPieceImage;

        // The game board last drawn onto the screen
        //private static Board LastDrawnBoard;

        private static VisualCollection GameBoardVisualLayers;

        // The game board that is used 
        private static Board DisplayBoard;

        private static DrawingVisual GamePiecesLayer;
        private static DrawingVisual ComputerPlayerVizLayer;
        private static DrawingVisual SuggestedMovesLayer;

        /// <summary>
        /// Creates an instance of GameBoard, loading image assets and setting up properties
        /// </summary>
        public GameBoard() : base()
        {
            Clear();

            gBlackPieceImage = GraphicsTools.GenerateImageSource(Properties.Resources.BlackPiece);
            gWhitePieceImage = GraphicsTools.GenerateImageSource(Properties.Resources.WhitePiece);
            gSuggestedPieceImage = GraphicsTools.GenerateImageSource(Properties.Resources.SuggestedPiece);
        }

        /// <summary>
        /// Clears all of the visual elements and resets the game board properties
        /// </summary>
        public void Clear()
        {
            //LastDrawnBoard = new Board();
            //LastDrawnBoard.ClearBoard();

            DisplayBoard = new Board();

            GameBoardVisualLayers = new VisualCollection(this);

            GamePiecesLayer = new DrawingVisual();
            SuggestedMovesLayer = new DrawingVisual();
            ComputerPlayerVizLayer = new DrawingVisual();
        }

        /// <summary>
        /// The thread safe way to refresh the game graphic elements
        /// </summary>
        public static void Refresh()
        {
            DrawAvailableMoves();
            DrawPieces();
            ScoreBoard.Refresh();
        }

        /// <summary>
        /// Returns the global application game instance
        /// </summary>
        /// <returns>The current application game instance</returns>
        public static ImageSource GetGamePiece(int PieceColor)
        {
            if (PieceColor == Board.BLACK)
                return gBlackPieceImage;
            else if (PieceColor == Board.WHITE)
                return gWhitePieceImage;
            else
                return null;
        }

        /// <summary>
        /// Draws the active game board pieces onto the screen
        /// </summary>
        /// <param name="dc">The drawing context to paint onto</param>
        public static void DrawPieces()
        {
            DrawPieces(App.GetActiveGameBoard());
        }

        /// <summary>
        /// Draws the given game board pieces onto the screen
        /// </summary>
        /// <param name="dc">The drawing context to paint onto</param>
        /// <param name="BoardToDisplay">The board to draw onto the screen</param>
        public static void DrawPieces(Board BoardToDisplay)
        {
            GameBoardVisualLayers.Remove(GamePiecesLayer);

            DrawingVisual WorkVisual = GamePiecesLayer;

            using (DrawingContext dc = GamePiecesLayer.RenderOpen())
            {
                for (int Y = 0; Y < BoardToDisplay.GetBoardSize(); Y++)
                    for (int X = 0; X < BoardToDisplay.GetBoardSize(); X++)
                        //if ((LastDrawnBoard.ColorAt(X, Y) != BoardToDisplay.ColorAt(X, Y)))
                        {
                            //dc.DrawRectangle(null, new Pen(System.Windows.Media.Brushes.White, 1), GetBoardRect(X, Y));
                            dc.DrawImage(GetGamePiece(BoardToDisplay.ColorAt(X, Y)), GetBoardRect(X, Y));
                        }
            }

            GameBoardVisualLayers.Add(GamePiecesLayer);

            //LastDrawnBoard = new Board(BoardToDisplay);
        }

        /// <summary>
        /// Marks all of the available moves for the given turn on the current game board
        /// </summary>
        /// <param name="Turn">The turn to use</param>
        public static void DrawAvailableMoves()
        {
            DrawAvailableMoves(App.GetActiveGameBoard(), App.GetActiveGame().GetCurrentTurn());
        }

        /// <summary>
        /// Marks all of the available moves for the given turn on the given game board
        /// </summary>
        /// <param name="SourceBoard">The game board</param>
        /// <param name="Turn">The turn to use</param>
        public static void DrawAvailableMoves(Board SourceBoard, int Turn)
        {
            GameBoardVisualLayers.Remove(SuggestedMovesLayer);

            SuggestedMovesLayer = new DrawingVisual();

            using (DrawingContext dc = SuggestedMovesLayer.RenderOpen())
            {
                if ((App.GetActiveGame().GetCurrentTurn() != App.GetComputerPlayer().GetColor()) || (!App.GetActiveGame().IsSinglePlayerGame()))
                    // Loop through all available moves and place a dot at the location
                    foreach (Point CurrentPiece in SourceBoard.AvailableMoves(App.GetActiveGame().GetCurrentTurn()))
                        dc.DrawImage(gSuggestedPieceImage, GetBoardRect(CurrentPiece));
            }

            GameBoardVisualLayers.Add(SuggestedMovesLayer);
        }

        /// <summary>
        /// Returns a Rect that bounds the given board space
        /// </summary>
        /// <param name="SourceLocation">The board coordinates to use</param>
        private static Rect GetBoardRect(Point SourceLocation)
        {
            return ( GetBoardRect( Convert.ToInt32( SourceLocation.X ), Convert.ToInt32( SourceLocation.Y ) ) );
        }

        /// <summary>
        /// Returns a Rect that bounds the given board space
        /// </summary>
        /// <param name="X">The X board coordinate to use</param>
        /// <param name="Y">The Y board coordinate to use</param>
        private static Rect GetBoardRect(int X, int Y)
        {
            return( new Rect(X * Properties.Settings.Default.GRID_SIZE, Y * Properties.Settings.Default.GRID_SIZE, Properties.Settings.Default.GRID_SIZE, Properties.Settings.Default.GRID_SIZE));
        }

        #region Visual class linkers

        protected override Visual GetVisualChild(int index)
        {
            return GameBoardVisualLayers[index];
        }

        protected override int VisualChildrenCount
        {
            get
            {
                return GameBoardVisualLayers.Count;
            }
        }

        #endregion
    }
}