/// <summary>
/// Reversi.ScoreBoard.cs
/// Brian A. Hebert
/// </summary>

using System;
using System.Windows;
using System.Windows.Media;
using System.Globalization;

namespace Reversi
{
    // Adorners must subclass the abstract base class Adorner. 
    public class ScoreBoard : FrameworkElement
    {
        // The scoreboard images
        private static ImageSource gWhiteTurnImage;
        private static ImageSource gBlackTurnImage;
        private static ImageSource gScoreBoardImage;

        // The player scores
        private static FormattedText WhiteScore;
        private static FormattedText BlackScore;

        /// <summary>
        /// Creates an instance of ScoreBoard, loading image assets
        /// </summary>
        public ScoreBoard()
            : base()
        {
            gWhiteTurnImage = GraphicsTools.GenerateImageSource(Properties.Resources.ScoreBoard_WhiteTurn);
            gBlackTurnImage = GraphicsTools.GenerateImageSource(Properties.Resources.ScoreBoard_BlackTurn);
            gScoreBoardImage = GraphicsTools.GenerateImageSource(Properties.Resources.ScoreBoard);
        }

        /// <summary>
        /// Returns the scoreboard image appropriate for the given turn
        /// </summary>
        /// <param name="Turn">The currently active turn</param>
        private ImageSource GetScoreBoardImage(int Turn)
        {
            if (Turn == Board.BLACK)
                return gBlackTurnImage;
            else if (Turn == Board.WHITE)
                return gWhiteTurnImage;
            else
                return gScoreBoardImage;
        }

        /// <summary>
        /// Draws the scoreboard, highlighting the current turn
        /// </summary>
        /// <param name="dc">The drawing context to paint onto</param>
        private void DrawScoreBoard(DrawingContext dc)
        {
            dc.DrawImage( GetScoreBoardImage( App.GetActiveGame().GetCurrentTurn() ), new Rect(0,0,gBlackTurnImage.Width, gBlackTurnImage.Height));
        }

        /// <summary>
        /// Draws the score of both players onto the scoreboard
        /// </summary>
        /// <param name="dc">The drawing context to paint onto</param>
        private void DrawScore(DrawingContext dc)
        {
            BlackScore = new FormattedText(App.GetActiveGameBoard().FindScore(Board.BLACK).ToString("00"), CultureInfo.GetCultureInfo("en-us"), FlowDirection.LeftToRight, new Typeface("Segoe UI"), 28, Brushes.White);
            BlackScore.TextAlignment = TextAlignment.Center;
            BlackScore.SetFontWeight(FontWeights.Bold);

            WhiteScore = new FormattedText(App.GetActiveGameBoard().FindScore(Board.WHITE).ToString("00"), CultureInfo.GetCultureInfo("en-us"), FlowDirection.LeftToRight, new Typeface("Segoe UI"), 28, Brushes.Black);
            WhiteScore.TextAlignment = TextAlignment.Center;
            WhiteScore.SetFontWeight(FontWeights.Bold);

            dc.DrawText(WhiteScore, new Point(46, 24));
            dc.DrawText(BlackScore, new Point(128, 24));
        }

        /// <summary>
        /// Overrides the default renderer, draws all of the score board elements onto the screen
        /// </summary>
        /// <param name="dc">The drawing context to paint onto</param>
        protected override void OnRender(DrawingContext dc)
        {
            base.OnRender(dc);

            // Filter for the design time tool
            if (App.GetActiveGame() != null)
            {
                DrawScoreBoard(dc);
                DrawScore(dc);
            }
        }
    }
}