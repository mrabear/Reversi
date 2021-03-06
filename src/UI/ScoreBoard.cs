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

        // All of the graphics layers for the scoreboard
        private static VisualCollection ScoreBoardVisualLayers;

        // Individual graphics layers
        private static DrawingVisual ScoreBoardLayer;
        private static DrawingVisual PlayerScoresLayer;

        /// <summary>
        /// Creates an instance of ScoreBoard, loading image assets
        /// </summary>
        public ScoreBoard()
            : base()
        {
            ScoreBoardVisualLayers = new VisualCollection(this);

            Clear();

            gWhiteTurnImage = GraphicsTools.GenerateImageSource("img/ScoreBoard/ScoreBoard_WhiteTurn.png");
            gBlackTurnImage = GraphicsTools.GenerateImageSource("img/ScoreBoard/ScoreBoard_BlackTurn.png");
            gScoreBoardImage = GraphicsTools.GenerateImageSource("img/ScoreBoard/ScoreBoard.png");
        }

        public static void Clear()
        {
            ScoreBoardVisualLayers.Remove(ScoreBoardLayer);
            ScoreBoardVisualLayers.Remove(PlayerScoresLayer);

            ScoreBoardLayer = new DrawingVisual();
            PlayerScoresLayer = new DrawingVisual();
        }

        /// <summary>
        /// Overrides the default renderer, draws all of the score board elements onto the screen
        /// </summary>
        /// 

        public delegate void RefreshDelegate();
        public void Refresh()
        {
            Application.Current.Dispatcher.Invoke(new RefreshDelegate(RedrawScoreBoard));
        }

        public void RedrawScoreBoard()
        {
            // Filter for the design time tool
            if (App.GetActiveGame() != null)
            {
                DrawScoreBoard();
                DrawPlayerScores();
            }
        }

        #region Drawing Methods

        /// <summary>
        /// Draws the scoreboard, highlighting the current turn
        /// </summary>
        /// <param name="dc">The drawing context to paint onto</param>
        private void DrawScoreBoard()
        {
            ScoreBoardVisualLayers.Remove(ScoreBoardLayer);

            using (DrawingContext dc = ScoreBoardLayer.RenderOpen())
            {
                dc.DrawImage(GetScoreBoardImage(App.GetActiveGame().GetCurrentTurn()), new Rect(0, 0, gBlackTurnImage.Width, gBlackTurnImage.Height));
            }

            ScoreBoardVisualLayers.Add(ScoreBoardLayer);
        }

        /// <summary>
        /// Draws the score of both players onto the scoreboard
        /// </summary>
        /// <param name="dc">The drawing context to paint onto</param>
        private void DrawPlayerScores()
        {
            ScoreBoardVisualLayers.Remove(PlayerScoresLayer);

            BlackScore = new FormattedText(App.GetActiveGameBoard().CalculateScore(Piece.BLACK).ToString("00"), CultureInfo.GetCultureInfo("en-us"), FlowDirection.LeftToRight, new Typeface("Segoe UI"), 28, Brushes.White);
            BlackScore.TextAlignment = TextAlignment.Center;
            BlackScore.SetFontWeight(FontWeights.Bold);

            WhiteScore = new FormattedText(App.GetActiveGameBoard().CalculateScore(Piece.WHITE).ToString("00"), CultureInfo.GetCultureInfo("en-us"), FlowDirection.LeftToRight, new Typeface("Segoe UI"), 28, Brushes.Black);
            WhiteScore.TextAlignment = TextAlignment.Center;
            WhiteScore.SetFontWeight(FontWeights.Bold);

            using (DrawingContext dc = PlayerScoresLayer.RenderOpen())
            {
                dc.DrawText(WhiteScore, new Point(46, 24));
                dc.DrawText(BlackScore, new Point(128, 24));
            }

            ScoreBoardVisualLayers.Add(PlayerScoresLayer);
        }

        #endregion

        #region Utility Methods

        /// <summary>
        /// Returns the scoreboard image appropriate for the given turn
        /// </summary>
        /// <param name="Turn">The currently active turn</param>
        private static ImageSource GetScoreBoardImage(Piece Turn)
        {
            if (Turn == Piece.BLACK)
                return gBlackTurnImage;
            else if (Turn == Piece.WHITE)
                return gWhiteTurnImage;
            else
                return gScoreBoardImage;
        }

        #endregion

        #region Visual class linkers

        protected override Visual GetVisualChild(int index)
        {
            return ScoreBoardVisualLayers[index];
        }

        protected override int VisualChildrenCount
        {
            get
            {
                return ScoreBoardVisualLayers.Count;
            }
        }

        #endregion

    }
}