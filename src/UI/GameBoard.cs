/// <summary>
/// Reversi.GameBoard.cs
/// Brian A. Hebert
/// </summary>

using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Globalization;
using System.Collections.Generic;
using System.Windows.Media.Animation;
using System.Collections.Concurrent;
using System.Threading;
using System.Timers;
using System.ComponentModel;

namespace Reversi
{
    /// <summary>
    /// The window component that the game board is drawn on
    /// </summary>
    public class GameBoard : Grid
    {
        // Static binds to game board images
        protected static ImageSource gBlackPieceImage;
        protected static ImageSource gWhitePieceImage;
        private static ImageSource gAvailableMoveImage;

        // Static binds to the computer player visualization images
        private static ImageSource gProcessingWorking;
        private static ImageSource gProcessingCompleted;
        private static ImageSource gProcessingQueued;
        private static ImageSource gProcessingChosen;

        // The locking object used to multithread the analysis
        //private static object HighLightLock = new object();

        // A list of all game board pieces
        protected static ConcurrentDictionary<Point, Image> GameBoardImages;

        // A list of all spots on the board that have had non-piece graphcis drawn on them
        // Non-Piece Graphics include: available move markers and computer player analysis visulizations
        private static List<Point> DirtySpots;

        // The locking object used to multithread the analysis
        private static UIElement StaticDispatcher = new UIElement();

        // The game board that is used 
        private static Board DisplayBoard;

        // The game board that has last been drawn onto screen 
        private static Board LastDrawnBoard;

        // The animation timer that cycles between 0 and 360 degrees, used to spin the green "processing turn" image
        private static DoubleAnimation AngleRotation;
      
        // The rotation animation that applies the AngleRotation timer to the angle of the "processing turn" image
        private static RotateTransform RotationAnimation;

        // Timer that is activated when an animation is running
        private static int ActiveAnimations = 0;

        // The locking object used to multithread the analysis
        private readonly object SpinLock;

        /// <summary>
        /// Creates an instance of GameBoard, loading image assets and setting up properties
        /// </summary>
        public GameBoard() : base()
        {
            // Reset the graphics layers
            GameBoardImages = new ConcurrentDictionary<Point, Image>();

            // Resets the dirty spots monitor
            DirtySpots = new List<Point>();

            // A lock to keep the AnimationsActive parameter thread safe
            SpinLock = new Object();

            // Static binds to the processing visualization image sources
            gProcessingWorking = GraphicsTools.GenerateImageSource("img/Pieces/ProcessingWorking.png");
            gProcessingCompleted = GraphicsTools.GenerateImageSource("img/Pieces/ProcessingCompleted.png");
            gProcessingQueued = GraphicsTools.GenerateImageSource("img/Pieces/ProcessingQueued.png");
            gProcessingChosen = GraphicsTools.GenerateImageSource("img/Pieces/ProcessingChosen.png");

            // Static binds to the game piece image sources
            gBlackPieceImage = GraphicsTools.GenerateImageSource("img/Pieces/BlackPiece.png");
            gWhitePieceImage = GraphicsTools.GenerateImageSource("img/Pieces/WhitePiece.png");
            gAvailableMoveImage = GraphicsTools.GenerateImageSource("img/Pieces/SuggestedPiece.png");

            // Setup the angle rotation timer for the green "processing space" gear image
            AngleRotation = new DoubleAnimation(360, 0, new Duration(TimeSpan.FromSeconds(5)));
            RotationAnimation = new RotateTransform();
            AngleRotation.RepeatBehavior = RepeatBehavior.Forever;
            RotationAnimation.BeginAnimation(RotateTransform.AngleProperty, AngleRotation);

            // Clears the game board
            Clear();
        }

        /// <summary>
        /// Clears all of the visual elements and resets the game board properties
        /// </summary>
        public void Clear()
        {
            // Clear the dirty spots list
            DirtySpots = new List<Point>();

            ActiveAnimations = 0;

            Point CurrentPoint;

            // Rebuild the board images
            if (App.GetActiveGameBoard() != null)
            {
                for (int Y = 0; Y < App.GetActiveGameBoard().GetBoardSize(); Y++)
                    for (int X = 0; X < App.GetActiveGameBoard().GetBoardSize(); X++)
                    {
                        CurrentPoint = new Point(X, Y);
                        UpdateBoardPiece(CurrentPoint, GetGamePiece(App.GetActiveGameBoard().ColorAt(X, Y)));
                    }

                // Reset the last drawn board
                LastDrawnBoard = new Board(App.GetActiveGameBoard());
            }
        }

        public delegate void RefreshDelegate(bool FullRefresh = true);
        public void Refresh( bool FullRefresh = true )
        {
            Application.Current.Dispatcher.Invoke(new RefreshDelegate(RefreshGameBoard), FullRefresh);
        }

        /// <summary>
        /// The thread safe way to refresh the game graphic elements
        /// </summary>
        public void RefreshGameBoard(bool FullRefresh = true)
        {
            if (FullRefresh)
                Clear();

            // Clean all non-piece graphics from the board
            WipeDirtySpots();
            
            // Draw the available moves for the current turn
            DrawAvailableMoves();

            // Refresh the scoreboard
            ReversiWindow.GetScoreBoardSurface().Refresh();

            // Draw the net new pieces
            if( FullRefresh )
                DrawPieces();
        }

        #region Drawing Methods

        /// <summary>
        /// Removes all annotations and non-piece graphcis from the previous display pass, resets the dirty spots list
        /// </summary>
        public void WipeDirtySpots()
        {
            // Clear the dirty spots from the display stack
            DirtySpots.ForEach(delegate(Point CurrentPoint) {
                if( App.GetActiveGameBoard().ColorAt(CurrentPoint) == Piece.EMPTY )
                    ClearBoardPiece(CurrentPoint); 
            });

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
            // The current piece that is being considered
            Point CurrentPiece;

            // Loop through all of the pieces in the given board
            for (int Y = 0; Y < BoardToDisplay.GetBoardSize(); Y++)
                for (int X = 0; X < BoardToDisplay.GetBoardSize(); X++)

                    // Only draw a game piece if it is either net new or has been flipped since the last drawing pass
                    if (BoardToDisplay.ColorAt(X, Y) != Piece.EMPTY)
                    {
                        CurrentPiece = new Point(X, Y);

                        // Add the canvas to the display list
                        UpdateBoardPiece(CurrentPiece, GetGamePiece(BoardToDisplay.ColorAt(CurrentPiece)));
                    }

            // Update the last drawn board with the board that was just created
            //LastDrawnBoard = new Board(App.GetActiveGameBoard());
        }

        public delegate void FlipCapturedPiecesDelegate(Point SourceMove);
        public void FlipCapturedPieces(Point SourceMove)
        {
            Application.Current.Dispatcher.Invoke(new FlipCapturedPiecesDelegate(FlipCapturedPieces_Invoke), SourceMove);
        }

        public void FlipCapturedPieces_Invoke(Point SourceMove)
        {
            int MoveX = Convert.ToInt16(SourceMove.X);
            int MoveY = Convert.ToInt16(SourceMove.Y);

            // Add the current move
            UpdateBoardPiece(SourceMove, GetGamePiece(App.GetActiveGameBoard().ColorAt(SourceMove)));

            for (int Y = 0; Y < App.GetActiveGameBoard().GetBoardSize(); Y++)
                for (int X = 0; X < App.GetActiveGameBoard().GetBoardSize(); X++)
                    if ((App.GetActiveGameBoard().ColorAt(X, Y) != LastDrawnBoard.ColorAt(X, Y)) && (new Point(X, Y) != SourceMove))
                    {
                        AddAnimation();
                        AnimateFlippedPiece(new Point(X, Y), LastDrawnBoard.ColorAt(X, Y), RemovePiece: true);
                    }

            // Update the last drawn board with the board that was just created
            LastDrawnBoard = new Board(App.GetActiveGameBoard());
        }

        public delegate void AnimateFlippedPieceDelegate(Point PieceToFlip, Piece PieceColor, bool RemovePiece = true);
        public void FlipPiece(Point PieceToFlip, Piece PieceColor, bool RemovePiece = true)
        {
            Application.Current.Dispatcher.Invoke(new AnimateFlippedPieceDelegate(AnimateFlippedPiece), PieceToFlip, PieceColor, RemovePiece);
        }

        protected void AnimateFlippedPiece(Point PieceToFlip, Piece PieceColor, bool RemovePiece = true)
        {
            // Add the canvas to the display list
            UpdateBoardPiece(PieceToFlip, GetGamePiece(PieceColor));

            ScaleTransform FlipTransformation = new ScaleTransform();
            DoubleAnimation FlipAnimation = new DoubleAnimation(RemovePiece ? 1 : 0, RemovePiece ? 0 : 1, new Duration(TimeSpan.FromSeconds(0.35)));

            GameBoardImages[PieceToFlip].RenderTransform = FlipTransformation;
            GameBoardImages[PieceToFlip].RenderTransformOrigin = new Point(0.5, 0.5);
            FlipTransformation.BeginAnimation(ScaleTransform.ScaleXProperty, FlipAnimation);
            AnimationClock FlipTimer = FlipAnimation.CreateClock();

            if (RemovePiece)
            {
                FlipTimer.Completed += new AnimationEventArg(PieceToFlip, Game.GetOtherTurn(PieceColor)).FlipPieceBack;
            }
            else
            {
                FlipTimer.Completed += AnimationCompleted;
            }
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
        public void DrawAvailableMoves(Board SourceBoard, Piece Turn)
        {
            // Do not display the available moves during a comptuer turn
            if ((App.GetActiveGame().GetCurrentTurn() != App.GetComputerPlayer().GetColor()) || (!App.GetActiveGame().IsSinglePlayerGame()))

                // Loop through all available moves and place a dot at the location
                foreach (Point CurrentPiece in SourceBoard.AvailableMoves(App.GetActiveGame().GetCurrentTurn()))
                {
                    // Add this space to the dirty spots list
                    DirtySpots.Add(CurrentPiece);

                    // Add the work visual to the display list
                    UpdateBoardPiece(CurrentPiece, gAvailableMoveImage);
                }
        }

        /// <summary>
        /// Places a highlight circle at the given location
        /// </summary>
        /// <param name="Piece">The piece to highlight</param>
        /// <param name="PieceColor">The highlight color</param>
        /// <param name="PieceLabel">(optional) Text to place in the center of the spot</param>
        public delegate void HighlightMoveDelegate(Point Piece, AnalysisStatus ProcessingState);
        public void HighlightMove(Point Piece, AnalysisStatus ProcessingState)
        {
            Application.Current.Dispatcher.Invoke(new HighlightMoveDelegate(DrawHighlightedMove), Piece, ProcessingState);
        }

        /// <summary>
        /// Places a highlight circle at the given locations
        /// </summary>
        private void DrawHighlightedMove(Point CurrentPiece, AnalysisStatus ProcessingState)
        {
            // Add this space to the dirty spots list
            //DirtySpots.Add(CurrentPiece);

            if (ProcessingState == AnalysisStatus.COMPLETE)
            {
                DoubleAnimation OpacityFader = new DoubleAnimation(0.6, 0, new Duration(TimeSpan.FromSeconds(0.15)));
                GameBoardImages[CurrentPiece].BeginAnimation(Image.OpacityProperty, OpacityFader);

                //AddAnimation();
                //AnimationClock OpacityTimer = OpacityFader.CreateClock();
                //OpacityTimer.Completed += AnimationCompleted;
            }
            else
            {
                // Remove this spot from the display list
                ClearBoardPiece(CurrentPiece);

                // Place a visualization icon on the board based off of the current Processing State
                if (ProcessingState == AnalysisStatus.QUEUED)
                {
                    // Place a grey "queued" gear
                    AddBoardPiece(CurrentPiece, gProcessingQueued);
                }
                else if (ProcessingState == AnalysisStatus.CHOSEN)
                {
                    // Place a glowing "chosen" gear
                    AddBoardPiece(CurrentPiece, gProcessingChosen);
                    GameBoardImages[CurrentPiece].Opacity = 0.5;
                }
                else if (ProcessingState == AnalysisStatus.WORKING)
                {
                    // Place a green "working" gear
                    AddBoardPiece(CurrentPiece, gProcessingWorking);

                    // Dim the piece (looks nicer)
                    GameBoardImages[CurrentPiece].Opacity = 0.6;

                    // Start spinning the gear
                    GameBoardImages[CurrentPiece].RenderTransform = RotationAnimation;
                    GameBoardImages[CurrentPiece].RenderTransformOrigin = new Point(0.5, 0.5);
                }
            }
        }

        #endregion

        #region Utility Methods


        /// <summary>
        /// Updates an existing game board piece (removes then adds it)
        /// </summary>
        public void UpdateBoardPiece(Point CurrentPoint, ImageSource SourceImage)
        {
            ClearBoardPiece(CurrentPoint);
            AddBoardPiece(CurrentPoint, SourceImage);
        }

        /// <summary>
        /// Adds a piece image onto the game board
        /// </summary>
        public void AddBoardPiece(Point CurrentPoint, ImageSource SourceImage)
        {
            // If this board spot hasn't been processed yet, add an entry for it in the dictionary
            if (!GameBoardImages.ContainsKey(CurrentPoint))
                GameBoardImages.AddOrUpdate(CurrentPoint, new Image(), (key, oldValue) => new Image());

            // Update the board image with the given image
            GameBoardImages[CurrentPoint].Source = SourceImage;
            
            // Add the given visual to the display list
            Children.Add(GameBoardImages[CurrentPoint]);

            // Set the image on the correct spot on the game board grid
            SetRow(GameBoardImages[CurrentPoint], Convert.ToInt16(CurrentPoint.Y));
            SetColumn(GameBoardImages[CurrentPoint], Convert.ToInt16(CurrentPoint.X));
        }

        /// <summary>
        /// Clears the given spot on the game board, typically called right before updating that spot
        /// </summary>
        public void ClearBoardPiece(Point CurrentPoint)
        {
            // Only attempt if this spot has been registered
            if (GameBoardImages.ContainsKey(CurrentPoint))
            {
                Children.Remove(GameBoardImages[CurrentPoint]);

                // Clear the board image
                GameBoardImages[CurrentPoint].Source = null;

                // The out parameter used in TryRemove
                Image OutBuffer;

                // Remove the given spot from the board lookup
                if (!GameBoardImages.TryRemove(CurrentPoint, out OutBuffer))
                    Console.WriteLine("Could not remove " + CurrentPoint.X + "," + CurrentPoint.Y);
            }
        }

        /// <summary>
        /// Returns the global application game instance
        /// </summary>
        /// <returns>The current application game instance</returns>
        public static ImageSource GetGamePiece(Piece PieceColor)
        {
            if (PieceColor == Piece.BLACK)
                return gBlackPieceImage;
            else if (PieceColor == Piece.WHITE)
                return gWhitePieceImage;
            else
                return null;
        }

        public void AddAnimation()
        {
            lock(SpinLock)
                ActiveAnimations++;
        }

        public void AnimationCompleted(object sender, EventArgs e)
        {
            RemoveAnimation();
        }

        public void RemoveAnimation()
        {
            lock (SpinLock)
                ActiveAnimations--;

            if( ActiveAnimations == 0 )
                App.GetActiveGame().CompleteTurn();
        }

        #endregion
    }
}