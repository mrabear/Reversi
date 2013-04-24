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
        private static Board WorkBoard;

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

            WorkBoard = new Board();

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
            if (PieceColor == ReversiWindow.WHITE)
                return gBlackPieceImage;
            else if (PieceColor == ReversiWindow.BLACK)
                return gWhitePieceImage;
            else
                return null;
        }

        public void DrawPieces(DrawingContext dc)
        {
            WorkBoard = new Board(ReversiWindow.GetCurrentGame().GetGameBoard());

            for (int Y = 0; Y < WorkBoard.GetBoardSize(); Y++)
                for (int X = 0; X < WorkBoard.GetBoardSize(); X++)
                //if ((LastDrawnBoard.ColorAt(X, Y) != MainBoard.ColorAt(X, Y)))
                    //dc.DrawRectangle(null, new Pen(System.Windows.Media.Brushes.White, 1), GetBoardRect(X,Y));
                    dc.DrawImage(GetGamePiece(WorkBoard.ColorAt(X, Y)), GetBoardRect(X, Y));

            LastDrawnBoard = new Board(WorkBoard);
        }

        /// <summary>
        /// Marks all of the available moves for the given turn on the current game board
        /// </summary>
        /// <param name="Turn">The turn to use</param>
        public void DrawAvailableMoves(DrawingContext dc)
        {
            DrawAvailableMoves(dc, ReversiWindow.GetCurrentGame().GetGameBoard(), ReversiWindow.GetCurrentGame().GetCurrentTurn());
        }

        /// <summary>
        /// Marks all of the available moves for the given turn on the given game board
        /// </summary>
        /// <param name="SourceBoard">The game board</param>
        /// <param name="Turn">The turn to use</param>
        public void DrawAvailableMoves(DrawingContext dc, Board SourceBoard, int Turn)
        {
            if ((ReversiWindow.GetCurrentGame().GetCurrentTurn() != ReversiWindow.GetCurrentGame().GetAI().GetColor()) || (!ReversiWindow.GetCurrentGame().IsVsComputer()))
                // Loop through all available moves and place a dot at the location
                foreach (Point CurrentPiece in WorkBoard.AvailableMoves(ReversiWindow.GetCurrentGame().GetCurrentTurn()))
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
            if (ReversiWindow.GetCurrentGame() != null)
            {
                DrawPieces(dc);
                DrawAvailableMoves(dc);
            }
        }
    }
}