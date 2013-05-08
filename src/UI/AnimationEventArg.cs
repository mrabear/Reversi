using System;
using System.Windows;
using System.Windows.Media.Animation;
using System.Timers;
using System.Windows.Controls;
using System.Windows.Media;

namespace Reversi
{
    class AnimationEventArg
    {
        private Point AnimationPoint;
        private Piece Color;

        public AnimationEventArg()
        {
        }

        public AnimationEventArg(Point SourcePoint, Piece SourceColor)
        {
            AnimationPoint = SourcePoint;
            Color = SourceColor;
        }

        public void CompleteAnimation(object sender, EventArgs args)
        {

            var AnimationTimer = sender as AnimationClock;
            if (AnimationTimer != null)
            {
                AnimationTimer.Completed -= CompleteAnimation;
            }

            ReversiWindow.GetGameBoardSurface().FlipPiece(AnimationPoint, Color, RemovePiece: false);
        }
    }
}
