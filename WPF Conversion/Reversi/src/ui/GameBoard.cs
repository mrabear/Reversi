using System;
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
    // Adorners must subclass the abstract base class Adorner. 
    public class GameBoard : FrameworkElement
    {
        private static Board MainBoard;
        private static Board LastDrawnBoard;
        
        // Be sure to call the base class constructor. 
        public GameBoard(Board SourceBoard)
            : base()
        {
            MainBoard = new Board(SourceBoard);
            LastDrawnBoard = new Board();
            LastDrawnBoard.ClearBoard();
        }

        // Be sure to call the base class constructor. 
        public GameBoard()
            : base()
        {
            MainBoard = new Board(); // Board(ReversiWindow.GetCurrentGame().GetGameBoard());
            MainBoard.PlaceStartingPieces();
            LastDrawnBoard = new Board();
            LastDrawnBoard.ClearBoard();
        }

        public void UpdateBoard()
        {
            UpdateBoard(ReversiWindow.GetCurrentGame().GetGameBoard());
        }

        public void UpdateBoard(Board SourceBoard)
        {
            MainBoard.CopyBoard( SourceBoard );
        }

        public void Refresh()
        {
            //DrawingContext BoardRenderer = ;

            AdornerLayer myAdornerLayer = AdornerLayer.GetAdornerLayer(this);

            for (int Y = 0; Y < MainBoard.GetBoardSize(); Y++)
                for (int X = 0; X < MainBoard.GetBoardSize(); X++)
                    if ((LastDrawnBoard.ColorAt(X, Y) != MainBoard.ColorAt(X, Y)))
                        myAdornerLayer.Add(new GamePiece(this, GetBoardRect(X, Y), MainBoard.ColorAt(X, Y)));

            LastDrawnBoard = new Board(MainBoard);

        }

        private Rect GetBoardRect(int X, int Y)
        {
            return( new Rect(X * Properties.Settings.Default.GRID_SIZE, Y * Properties.Settings.Default.GRID_SIZE, Properties.Settings.Default.GRID_SIZE, Properties.Settings.Default.GRID_SIZE));
        }

        protected override void OnRender(DrawingContext DrawingContext)
        {
            base.OnRender(DrawingContext);

            Refresh();
            //drawingContext.DrawImage(ReversiWindow.GetGamePiece( PieceColor ), adornedElementRect);
        }
    }
}