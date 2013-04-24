using System;
using System.Windows;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.ComponentModel;

namespace Reversi
{
    // Adorners must subclass the abstract base class Adorner. 
    public class GameBoard : FrameworkElement
    {

        private static ImageSource gBlackPieceImage;
        private static ImageSource gWhitePieceImage;
        private static ImageSource gSuggestedPieceImage;

       //private static Board MainBoard;
        private static Board LastDrawnBoard;
        private static Board DisplayBoard;

        //private static List<Visual> BoardPieces;

        // Be sure to call the base class constructor. 
        public GameBoard() : base()
        {
            Initialize();
        }

        private void Initialize()
        {
            //BoardPieces = new List<Visual>();

            LastDrawnBoard = new Board();
            LastDrawnBoard.ClearBoard();

            DisplayBoard = new Board();

            gBlackPieceImage = GraphicsUtil.GenerateImageSource(Properties.Resources.BlackPiece);
            gWhitePieceImage = GraphicsUtil.GenerateImageSource(Properties.Resources.WhitePiece);
            gSuggestedPieceImage = GraphicsUtil.GenerateImageSource(Properties.Resources.SuggestedPiece);
        }

        /// <summary>
        /// Returns the global application game instance
        /// </summary>
        /// <returns>The current application game instance</returns>
        public ImageSource GetGamePiece(int PieceColor)
        {
            if (PieceColor == App.BLACK)
                return gBlackPieceImage;
            else if (PieceColor == App.WHITE)
                return gWhitePieceImage;
            else
                return null;
        }

        public void DrawPieces(DrawingContext dc)
        {
            DisplayBoard = new Board(App.GetActiveGameBoard());

            for (int Y = 0; Y < DisplayBoard.GetBoardSize(); Y++)
                for (int X = 0; X < DisplayBoard.GetBoardSize(); X++)
                //if ((LastDrawnBoard.ColorAt(X, Y) != MainBoard.ColorAt(X, Y)))
                    //dc.DrawRectangle(null, new Pen(System.Windows.Media.Brushes.White, 1), GetBoardRect(X,Y));
                    dc.DrawImage(GetGamePiece(DisplayBoard.ColorAt(X, Y)), GetBoardRect(X, Y));

            LastDrawnBoard = new Board(DisplayBoard);
        }

        /// <summary>
        /// Marks all of the available moves for the given turn on the current game board
        /// </summary>
        /// <param name="Turn">The turn to use</param>
        public void DrawAvailableMoves(DrawingContext dc)
        {
            DrawAvailableMoves(dc, App.GetActiveGameBoard(), App.GetCurrentGame().GetCurrentTurn());
        }

        /// <summary>
        /// Marks all of the available moves for the given turn on the given game board
        /// </summary>
        /// <param name="SourceBoard">The game board</param>
        /// <param name="Turn">The turn to use</param>
        public void DrawAvailableMoves(DrawingContext dc, Board SourceBoard, int Turn)
        {
            if ((App.GetCurrentGame().GetCurrentTurn() != App.GetComputerPlayer().GetColor()) || (!App.GetCurrentGame().IsVsComputer()))
                // Loop through all available moves and place a dot at the location
                foreach (Point CurrentPiece in DisplayBoard.AvailableMoves(App.GetCurrentGame().GetCurrentTurn()))
                    dc.DrawImage(gSuggestedPieceImage, GetBoardRect(CurrentPiece));
        }

        private Rect GetBoardRect(Point SourceLocation)
        {
            return ( GetBoardRect( Convert.ToInt32( SourceLocation.X ), Convert.ToInt32( SourceLocation.Y ) ) );
        }

        private Rect GetBoardRect(int X, int Y)
        {
            return( new Rect(X * Properties.Settings.Default.GRID_SIZE, Y * Properties.Settings.Default.GRID_SIZE, Properties.Settings.Default.GRID_SIZE, Properties.Settings.Default.GRID_SIZE));
        }

        protected override void OnRender(DrawingContext dc)
        {
            base.OnRender(dc);

            // Filter for the design time tool
            if (App.GetCurrentGame() != null)
            {
                DrawPieces(dc);
                DrawAvailableMoves(dc);
            }
        }
    }
}