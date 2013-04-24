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

        public static ImageSource gBlackPieceImage;
        public static ImageSource gWhitePieceImage;
        public static ImageSource gSuggestedPieceImage;

       //private static Board MainBoard;
        private static Board LastDrawnBoard;

        //private static List<Visual> BoardPieces;

        // Be sure to call the base class constructor. 
        public GameBoard()
        {
            Initialize();
        }

        private void Initialize()
        {
            //BoardPieces = new List<Visual>();

            LastDrawnBoard = new Board();
            LastDrawnBoard.ClearBoard();

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

        public void Refresh(DrawingContext dc)
        {   
            Board MainBoard = new Board(ReversiWindow.GetCurrentGame().GetGameBoard());

            for (int Y = 0; Y < MainBoard.GetBoardSize(); Y++)
                for (int X = 0; X < MainBoard.GetBoardSize(); X++)
                    //if ((LastDrawnBoard.ColorAt(X, Y) != MainBoard.ColorAt(X, Y)))
                        //dc.DrawRectangle(System.Windows.Media.Brushes.LightBlue, (System.Windows.Media.Pen)null, GetBoardRect(X, Y));
                        dc.DrawImage(GetGamePiece(MainBoard.ColorAt(X, Y)), GetBoardRect(X, Y));             

            LastDrawnBoard = new Board(MainBoard);
        }

        private Rect GetBoardRect(int X, int Y)
        {
            return( new Rect(X * Properties.Settings.Default.GRID_SIZE, Y * Properties.Settings.Default.GRID_SIZE, Properties.Settings.Default.GRID_SIZE, Properties.Settings.Default.GRID_SIZE));
        }

        protected override void OnRender(DrawingContext dc)
        {
            base.OnRender(dc);
            Refresh(dc);

            //Random rnd = new Random();
            //dc.DrawRectangle(System.Windows.Media.Brushes.LightBlue, (System.Windows.Media.Pen)null, GetBoardRect(rnd.Next(7), rnd.Next(7)));
            //dc.DrawImage(GetGamePiece(1), GetBoardRect(rnd.Next(7), rnd.Next(7)));
            //dc.DrawImage(GetGamePiece(2), GetBoardRect(rnd.Next(7), rnd.Next(7)));
        }
    }
}