/// <summary>
/// Reversi.GameBoard.cs
/// Brian A. Hebert
/// </summary>

using System;
using System.Linq;
using System.Windows;
using System.Windows.Media;
using System.Globalization;
using System.Collections.Generic;
using System.Windows.Media.Animation;
using System.Collections.Concurrent;

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
        private static ImageSource gAvailableMoveImage;
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

        // A list of all graphics display layers, one layer for each spot on the board
        private static ConcurrentDictionary<Point, DrawingVisual> GameBoardVisualLayers;

        private static List<Point> DirtySpots;

        // The locking object used to multithread the analysis
        private static UIElement StaticDispatcher = new UIElement();

        // The game board that is used 
        private static Board DisplayBoard;

        // The game board that has last been drawn onto screen 
        private static Board LastDrawnBoard;

        // The rotating animation clock used for the computer processing visualizations
        //private static AnimationClock RotationAnimationClock;

        /// <summary>
        /// Creates an instance of GameBoard, loading image assets and setting up properties
        /// </summary>
        public GameBoard() : base()
        {
            // Reset the graphics layers
            GameBoardVisualLayers = new ConcurrentDictionary<Point, DrawingVisual>();

            // Resets the dirty spots monitor
            DirtySpots = new List<Point>();

            // Clears the game board
            Clear();

            // The formatted fonts used for the "?" displayed while the AI is processing a turn: Used when a move has been queued, but has not been assigned a worker thread
            StartedMoveFont = new FormattedText("?", CultureInfo.GetCultureInfo("en-us"), FlowDirection.LeftToRight, new Typeface("Segoe UI"), 36, new SolidColorBrush(LightGrey));
            StartedMoveFont.TextAlignment = TextAlignment.Center;
            StartedMoveFont.SetFontWeight(FontWeights.Bold);

            // The formatted fonts used for the "?" displayed while the AI is processing a turn: Used when a move has been assigned a worker thread
            WorkingMoveFont = new FormattedText("?", CultureInfo.GetCultureInfo("en-us"), FlowDirection.LeftToRight, new Typeface("Segoe UI"), 36, new SolidColorBrush(MatrixGreen));
            WorkingMoveFont.TextAlignment = TextAlignment.Center;
            WorkingMoveFont.SetFontWeight(FontWeights.Bold);

            // The formatted fonts used for the "?" displayed while the AI is processing a turn: Used when a move worker thread has completed
            CompletedMoveFont = new FormattedText("?", CultureInfo.GetCultureInfo("en-us"), FlowDirection.LeftToRight, new Typeface("Segoe UI"), 36, new SolidColorBrush(DarkGreen));
            CompletedMoveFont.TextAlignment = TextAlignment.Center;
            CompletedMoveFont.SetFontWeight(FontWeights.Bold);

            // Static binds to the piece images
            gProcessingPieceImage = GraphicsTools.GenerateImageSource(Properties.Resources.ProcessingPiece);
            gBlackPieceImage = GraphicsTools.GenerateImageSource(Properties.Resources.BlackPiece);
            gWhitePieceImage = GraphicsTools.GenerateImageSource(Properties.Resources.WhitePiece);
            gAvailableMoveImage = GraphicsTools.GenerateImageSource(Properties.Resources.SuggestedPiece);
        }

        /// <summary>
        /// Clears all of the visual elements and resets the game board properties
        /// </summary>
        public void Clear()
        {
            // Reset the last drawn board
            LastDrawnBoard = new Board();
            LastDrawnBoard.ClearBoard();

            // Reset the current working board
            DisplayBoard = new Board();

            // Rebuild the board visual layers
            for (int Y = 0; Y < LastDrawnBoard.GetBoardSize(); Y++)
                for (int X = 0; X < LastDrawnBoard.GetBoardSize(); X++)
                {
                    RemoveVisual(new Point(X, Y));
                    AddVisual(new Point(X, Y), new DrawingVisual());
                }
        }

        /// <summary>
        /// The thread safe way to refresh the game graphic elements
        /// </summary>
        public void Refresh()
        {
            // Clean all non-piece graphics from the board
            WipeDirtySpots();
            
            // Draw the net new pieces
            DrawPieces();

            // Draw the available moves for the current turn
            DrawAvailableMoves();

            // Refresh the scoreboard
            ScoreBoard.Refresh();
        }

        #region Drawing Methods

        /// <summary>
        /// Removes all annotations and non-piece graphcis from the previous display pass, resets the dirty spots list
        /// </summary>
        public void WipeDirtySpots()
        {
            // Clear the dirty spots from the display stack
            DirtySpots.ForEach(delegate(Point CurrentPoint) { RemoveVisual(CurrentPoint); });

            // Rest the dirty spots list
            DirtySpots = new List<Point>();
        }

        /// <summary>
        /// Draws the active game board pieces onto the screen
        /// </summary>
        /// <param name="dc">The drawing context to paint onto</param>
        public void DrawPieces()
        {
            DrawPieces(App.GetActiveGameBoard());
        }

        /// <summary>
        /// Draws the given game board pieces onto the screen
        /// </summary>
        /// <param name="dc">The drawing context to paint onto</param>
        /// <param name="BoardToDisplay">The board to draw onto the screen</param>
        public void DrawPieces(Board BoardToDisplay)
        {
            // A visual that will be used as a workspace, will eventually be added to the display list
            DrawingVisual WorkVisual;

            // The current piece that is being considered
            Point CurrentPiece;

            // Loop through all of the pieces in the given board
            for (int Y = 0; Y < BoardToDisplay.GetBoardSize(); Y++)
                for (int X = 0; X < BoardToDisplay.GetBoardSize(); X++)

                    // Only draw a game piece if it is either net new or has been flipped since the last drawing pass
                    if ((LastDrawnBoard.ColorAt(X, Y) != BoardToDisplay.ColorAt(X, Y)) || (BoardToDisplay.ColorAt(X, Y) == Board.EMPTY))
                    {
                        // The current piece
                        CurrentPiece = new Point(X, Y);

                        // Remove this spot from the display list
                        RemoveVisual(CurrentPiece);

                        // Reset the canvas
                        WorkVisual = new DrawingVisual();

                        // Open the canvas for rendering and draw the correct piece image
                        using (DrawingContext dc = WorkVisual.RenderOpen())
                            dc.DrawImage(GetGamePiece(BoardToDisplay.ColorAt(CurrentPiece)), GetBoardRect(CurrentPiece));

                        // Add the canvas to the display list
                        AddVisual(CurrentPiece, WorkVisual);
                    }

            LastDrawnBoard = new Board(BoardToDisplay);
        }

        /// <summary>
        /// Marks all of the available moves for the given turn on the current game board
        /// </summary>
        /// <param name="Turn">The turn to use</param>
        public void DrawAvailableMoves()
        {
            DrawAvailableMoves(App.GetActiveGameBoard(), App.GetActiveGame().GetCurrentTurn());
        }

        /// <summary>
        /// Marks all of the available moves for the given turn on the given game board
        /// </summary>
        /// <param name="SourceBoard">The game board</param>
        /// <param name="Turn">The turn to use</param>
        public void DrawAvailableMoves(Board SourceBoard, int Turn)
        {
            // A visual that will be used as a workspace, will eventually be added to the display list
            DrawingVisual WorkVisual;

            // Do not display the available moves during a comptuer turn
            if ((App.GetActiveGame().GetCurrentTurn() != App.GetComputerPlayer().GetColor()) || (!App.GetActiveGame().IsSinglePlayerGame()))

                // Loop through all available moves and place a dot at the location
                foreach (Point CurrentPiece in SourceBoard.AvailableMoves(App.GetActiveGame().GetCurrentTurn()))
                {
                    // Add this space to the dirty spots list
                    DirtySpots.Add(CurrentPiece);

                    // Remove this spot from the display list
                    RemoveVisual(CurrentPiece);

                    // Reset the working visual
                    WorkVisual = new DrawingVisual();

                    // Open the working visual for rendering and draw the available moves image
                    using (DrawingContext dc = WorkVisual.RenderOpen())
                        dc.DrawImage(gAvailableMoveImage, GetBoardRect(CurrentPiece));

                    // Add the work visual to the display list
                    AddVisual(CurrentPiece, WorkVisual);
                }
        }

        /// <summary>
        /// Returns a Rect that bounds the given board space
        /// </summary>
        /// <param name="SourceLocation">The board coordinates to use</param>
        private Rect GetBoardRect(Point SourceLocation)
        {
            return (GetBoardRect(Convert.ToInt32(SourceLocation.X), Convert.ToInt32(SourceLocation.Y)));
        }

        /// <summary>
        /// Places a highlight circle at the given location
        /// </summary>
        /// <param name="Piece">The piece to highlight</param>
        /// <param name="PieceColor">The highlight color</param>
        /// <param name="PieceLabel">(optional) Text to place in the center of the spot</param>
        public delegate void HighlightMoveDelegate(Point Piece, String ProcessingState, String PieceLabel = "");
        public void HighlightMove(Point Piece, String ProcessingState, String PieceLabel = "")
        {
            Application.Current.Dispatcher.Invoke(new HighlightMoveDelegate(DrawHighlightedMove), Piece, ProcessingState, PieceLabel);
        }

        /// <summary>
        /// Places a highlight circle at the given locations
        /// </summary>
        private void DrawHighlightedMove(Point CurrentPiece, String ProcessingState, String PieceLabel = "")
        {
            // Add this space to the dirty spots list
            DirtySpots.Add(CurrentPiece);

            // A visual that will be used as a workspace, will eventually be added to the display list
            DrawingVisual WorkVisual = new DrawingVisual();

            // Remove this spot from the display list
            RemoveVisual(CurrentPiece);

            // Open the working visual for rendering
            using (DrawingContext dc = WorkVisual.RenderOpen())
            {
                if (ProcessingState == ComputerPlayer.COMPLETE)
                {
                    dc.DrawText(CompletedMoveFont, GetSpaceCenterPoint(CurrentPiece));

                    Geometry TextOutline = CompletedMoveFont.BuildGeometry(GetSpaceCenterPoint(CurrentPiece));
                    dc.DrawGeometry(null, new Pen(new SolidColorBrush(DarkGrey), 2), TextOutline.GetOutlinedPathGeometry());
                }
                else if (ProcessingState == ComputerPlayer.STARTED)
                    dc.DrawText(StartedMoveFont, GetSpaceCenterPoint(CurrentPiece));
                else if (ProcessingState == ComputerPlayer.WORKING)
                    dc.DrawText(WorkingMoveFont, GetSpaceCenterPoint(CurrentPiece));

                //dc.DrawText(CompletedMoveFont, GetSpaceCenterPoint(CurrentPiece.X, CurrentPiece.Y));
                //else
                //dc.DrawImage(gProcessingPieceImage, GetBoardRect(CurrentPiece), RotationAnimationClock);
                //dc.DrawText(ProcessingMoveFont, GetSpaceCenterPoint(CurrentPiece.X, CurrentPiece.Y));
            }

            // Add the work visual to the display list
            AddVisual(CurrentPiece, WorkVisual);
        }

        #endregion

        #region Utility Methods

        /// <summary>
        /// Adds the given visual layer to the  given spot on the board, typically called right before updating that spot
        /// </summary>
        public void AddVisual(Point CurrentPoint, DrawingVisual SourceVisual)
        {
            // Add the given visual to the board lookup
            GameBoardVisualLayers.AddOrUpdate(CurrentPoint, SourceVisual, (key, oldValue) => SourceVisual);
            
            // Add the given visual to the display list
            this.AddVisualChild(GameBoardVisualLayers[CurrentPoint]);
        }

        /// <summary>
        /// Removes the visual layer that represents the given spot on the board, typically called right before updating that spot
        /// </summary>
        public void RemoveVisual(Point CurrentPoint)
        {
            // Only attempt if this spot has been registered
            if (GameBoardVisualLayers.ContainsKey(CurrentPoint))
            {
                // Remove the given spot from the display list
                this.RemoveVisualChild(GameBoardVisualLayers[CurrentPoint]);

                // The out parameter used in TryRemove
                DrawingVisual OutBuffer;

                // Remove the given spot from the board lookup
                if (!GameBoardVisualLayers.TryRemove(CurrentPoint, out OutBuffer))
                    Console.WriteLine("Could not remove " + CurrentPoint.X + "," + CurrentPoint.Y);
            }
        }

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

        /// <summary>
        /// Overrides the default GetVisualChild and instructs the WPF renderer to use the visuals stored in GameBoardVisualLayers
        /// </summary>
        protected override Visual GetVisualChild(int index)
        {
            return (GameBoardVisualLayers.Values.ElementAt<DrawingVisual>(index));
        }

        /// <summary>
        /// Overrides the default VisualChildrenCount and instructs WPF to count the visuals stored in GameBoardVisualLayers
        /// </summary>
        protected override int VisualChildrenCount
        {
            get
            {
                return GameBoardVisualLayers.Values.Count;
            }
        }

        #endregion
    }
}