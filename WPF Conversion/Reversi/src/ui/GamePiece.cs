using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Documents;

namespace Reversi
{
    // Adorners must subclass the abstract base class Adorner. 
    public class GamePiece : Adorner
    {
        private Rect DisplayRect;
        private int PieceColor;

        // Be sure to call the base class constructor. 
        public GamePiece(UIElement adornedElement, Rect SourceRect, int SourceColor)
            : base(adornedElement)
        {
            DisplayRect = SourceRect;
            PieceColor = SourceColor;
        }

        // A common way to implement an adorner's rendering behavior is to override the OnRender 
        // method, which is called by the layout system as part of a rendering pass. 
        protected override void OnRender(DrawingContext DrawingContext)
        {
            base.OnRender(DrawingContext);

            DrawingContext.DrawEllipse(new SolidColorBrush(PieceColor==ReversiWindow.BLACK?Colors.Black:Colors.White), new Pen(new SolidColorBrush(Colors.Yellow), 1.5), new Point(DisplayRect.Left + 40, DisplayRect.Top + 40), 25, 25);
            //DrawingContext.DrawImage(ReversiWindow.GetGamePiece(PieceColor), DisplayRect);
            //new ImageSourceConverter().ConvertFromString("/Reversi;component/img/BlackPiece.png") as ImageSource
        }
    }
}