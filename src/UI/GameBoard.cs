/// <summary>
/// Reversi.GameBoard.cs
/// Brian A. Hebert
/// </summary>

using System;
using System.Windows;
using System.Windows.Media;
using System.Globalization;
using System.Collections.Generic;
using System.Windows.Media.Animation;

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
        private static ImageSource gProcessingPieceImage;

        // Font and colors used to display the computers player current analysis
        private static FormattedText StartedMoveFont;
        private static FormattedText WorkingMoveFont;
        private static FormattedText CompletedMoveFont;
        private static Color LightGrey = Color.FromArgb(255, 200, 200, 200);
        private static Color MatrixGreen = Color.FromArgb(255, 0, 255, 12);
        private static Color DarkGreen = Color.FromArgb(255, 0, 41, 0);
        private static Color DarkGrey = Color.FromArgb(255, 100, 100, 100);

        // The locking object used to multithread the analysis
        private static object HighLightLock = new object();

        // A list of all graphics display layers
        private static VisualCollection GameBoardVisualLayers;

        // The individual graphics layers
        private static DrawingVisual GamePiecesLayer;
        private static DrawingVisual ComputerPlayerVizLayer;
        private static DrawingVisual SuggestedMovesLayer;

        // The locking object used to multithread the analysis
        private static UIElement StaticDispatcher = new UIElement();

        // The game board that is used 
        private static Board DisplayBoard;

        // The rotating animation clock used for the computer processing visualizations
        //private static AnimationClock RotationAnimationClock;

        /// <summary>
        /// Creates an instance of GameBoard, loading image assets and setting up properties
        /// </summary>
        public GameBoard() : base()
        {
            // Reset the graphics layers
            GameBoardVisualLayers = new VisualCollection(this);

            Clear();

            StartedMoveFont = new FormattedText("?", CultureInfo.GetCultureInfo("en-us"), FlowDirection.LeftToRight, new Typeface("Segoe UI"), 36, new SolidColorBrush(LightGrey));
            StartedMoveFont.TextAlignment = TextAlignment.Center;
            StartedMoveFont.SetFontWeight(FontWeights.Bold);

            WorkingMoveFont = new FormattedText("?", CultureInfo.GetCultureInfo("en-us"), FlowDirection.LeftToRight, new Typeface("Segoe UI"), 36, new SolidColorBrush(MatrixGreen));
            WorkingMoveFont.TextAlignment = TextAlignment.Center;
            WorkingMoveFont.SetFontWeight(FontWeights.Bold);

            CompletedMoveFont = new FormattedText("?", CultureInfo.GetCultureInfo("en-us"), FlowDirection.LeftToRight, new Typeface("Segoe UI"), 36, new SolidColorBrush(DarkGreen));
            CompletedMoveFont.TextAlignment = TextAlignment.Center;
            CompletedMoveFont.SetFontWeight(FontWeights.Bold);

            gProcessingPieceImage = GraphicsTools.GenerateImageSource(Properties.Resources.ProcessingPiece);
            gBlackPieceImage = GraphicsTools.GenerateImageSource(Properties.Resources.BlackPiece);
            gWhitePieceImage = GraphicsTools.GenerateImageSource(Properties.Resources.WhitePiece);
            gSuggestedPieceImage = GraphicsTools.GenerateImageSource(Properties.Resources.SuggestedPiece);
        }

        /// <summary>
        /// Clears all of the visual elements and resets the game board properties
        /// </summary>
        public static void Clear()
        {
            //LastDrawnBoard = new Board();
            //LastDrawnBoard.ClearBoard();

            //GameBoardVisualLayers.Remove(GamePiecesLayer);
            //GameBoardVisualLayers.Remove(SuggestedMovesLayer);
            //GameBoardVisualLayers.Remove(ComputerPlayerVizLayer);
            GameBoardVisualLayers.Clear();

            DisplayBoard = new Board();

            GamePiecesLayer = new DrawingVisual();
            SuggestedMovesLayer = new DrawingVisual();
            ComputerPlayerVizLayer = new DrawingVisual();
        }

        /// <summary>
        /// The thread safe way to refresh the game graphic elements
        /// </summary>
        public static void Refresh()
        {
            Clear();
            //DrawHighlightPieces();
            DrawAvailableMoves();
            DrawPieces();
            ScoreBoard.Refresh();
        }

        #region Drawing Methods

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
            return (GetBoardRect(Convert.ToInt32(SourceLocation.X), Convert.ToInt32(SourceLocation.Y)));
        }

        /// <summary>
        /// Resets the computer player visualization layer
        /// </summary>
        public delegate void StartNewVisualizationDelegate();
        public static void StartNewVisualization()
        {
            Application.Current.Dispatcher.Invoke(new StartNewVisualizationDelegate(ClearVisualizationLayer));
        }

        /// <summary>
        /// Clears all visualizations graphics,usually to start a new round of turn analysis
        /// </summary>
        public static void ClearVisualizationLayer()
        {
            GameBoardVisualLayers.Remove(ComputerPlayerVizLayer);
            ComputerPlayerVizLayer.Children.Clear();
            GameBoardVisualLayers.Add(ComputerPlayerVizLayer);
        }

        /// <summary>
        /// Places a highlight circle at the given location
        /// </summary>
        /// <param name="Piece">The piece to highlight</param>
        /// <param name="PieceColor">The highlight color</param>
        /// <param name="PieceLabel">(optional) Text to place in the center of the spot</param>
        public delegate void HighlightMoveDelegate(Point Piece, String ProcessingState, String PieceLabel = "");
        public static void HighlightMove(Point Piece, String ProcessingState, String PieceLabel = "")
        {
            Application.Current.Dispatcher.Invoke(new HighlightMoveDelegate(DrawHighlightedMove), Piece, ProcessingState, PieceLabel);
        }

        /// <summary>
        /// Places a highlight circle at the given locations
        /// </summary>
        private static void DrawHighlightedMove(Point Piece, String ProcessingState, String PieceLabel = "")
        {
            DrawingVisual MoveVisualLayer = new DrawingVisual();

            using (DrawingContext dc = MoveVisualLayer.RenderOpen())
            {
                if (ProcessingState == ComputerPlayer.COMPLETE)
                {
                    dc.DrawText(CompletedMoveFont, GetSpaceCenterPoint(Piece));

                    Geometry TextOutline = CompletedMoveFont.BuildGeometry(GetSpaceCenterPoint(Piece));
                    dc.DrawGeometry(null, new Pen(new SolidColorBrush(DarkGrey), 2), TextOutline.GetOutlinedPathGeometry());
                }
                else if (ProcessingState == ComputerPlayer.STARTED)
                    dc.DrawText(StartedMoveFont, GetSpaceCenterPoint(Piece));
                else if (ProcessingState == ComputerPlayer.WORKING)
                    dc.DrawText(WorkingMoveFont, GetSpaceCenterPoint(Piece));

                //dc.DrawText(CompletedMoveFont, GetSpaceCenterPoint(CurrentPiece.X, CurrentPiece.Y));
                //else
                //dc.DrawImage(gProcessingPieceImage, GetBoardRect(CurrentPiece), RotationAnimationClock);
                //dc.DrawText(ProcessingMoveFont, GetSpaceCenterPoint(CurrentPiece.X, CurrentPiece.Y));
            }

            ComputerPlayerVizLayer.Children.Add(MoveVisualLayer);
        }

        #endregion

        #region Utility Methods

        /// <summary>
        /// Returns a the point at the cetner of the given board space
        /// </summary>
        /// <param name="X">The X board coordinate to use</param>
        /// <param name="Y">The Y board coordinate to use</param>
        private static Point GetSpaceCenterPoint(Point PointToCheck)
        {
            return (new Point((PointToCheck.X * Properties.Settings.Default.GRID_SIZE) + (Properties.Settings.Default.GRID_SIZE / 2), (PointToCheck.Y * Properties.Settings.Default.GRID_SIZE) + 12));
        }

        /// <summary>
        /// Returns a Rect that bounds the given board space
        /// </summary>
        /// <param name="X">The X board coordinate to use</param>
        /// <param name="Y">The Y board coordinate to use</param>
        private static Rect GetBoardRect(int X, int Y)
        {
            return (new Rect(X * Properties.Settings.Default.GRID_SIZE, Y * Properties.Settings.Default.GRID_SIZE, Properties.Settings.Default.GRID_SIZE, Properties.Settings.Default.GRID_SIZE));
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

        private static AnimationClock CreateAnimationClock(Point GameBoardLocation)
        {
            RectAnimation RotationAnimation = new RectAnimation();
            RotationAnimation.Duration = TimeSpan.FromSeconds(2);
            RotationAnimation.FillBehavior = FillBehavior.HoldEnd;

            // Set the animation to repeat forever. 
            RotationAnimation.RepeatBehavior = RepeatBehavior.Forever;

            // Set the From and To properties of the animation.
            RotationAnimation.From = GetBoardRect(Convert.ToInt32(GameBoardLocation.X), Convert.ToInt32(GameBoardLocation.Y));
            RotationAnimation.To = GetBoardRect(Convert.ToInt32(GameBoardLocation.X + 5), Convert.ToInt32(GameBoardLocation.Y));

            return (RotationAnimation.CreateClock());
        }

        #endregion

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