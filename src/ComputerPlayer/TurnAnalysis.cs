using System;
using System.Collections.Generic;
using System.Windows;

namespace Reversi
{
    static class TurnAnalysis
    {
        private static readonly int CornerWeight = 50000;
        private static readonly int CornerGutterWeight = -50000;
        public static readonly int VictoryWeight = 100000;
        private static readonly int BorderWeight = 100;
        private static readonly int InnerGutterWeight = -5;
        private static readonly int InnerCornerWeight = 3;

        // This is an attempt to rate the value of each spot on the board
        private static readonly int[,] BoardValueMask = new int[,]
        {
	        {0,1,2,3,4,5,6,7},
	        {1,0,0,0,0,0,0,0},
	        {2,0,0,0,0,0,0,0},
	        {3,0,0,0,0,0,0,0},
	        {4,0,0,0,0,0,0,0},
	        {5,0,0,0,0,0,0,0},
	        {6,0,0,0,0,0,0,0},
	        {7,0,0,0,0,0,0,0},
        };

        private static List<Point> Corners = new List<Point>()
        {
            new Point(0,0), new Point(7,0), new Point(0,7), new Point(7,7)
        };

        private static List<Point> InnerCorners = new List<Point>()
        {
            new Point(2,2), new Point(2,5), new Point(5,2), new Point(5,5)
        };

        private static List<Point> CornerGutters = new List<Point>()
        {
            // Top Left
            new Point(0,1), new Point(1,0), new Point(1,1),
            // Top Right
            new Point(6,0), new Point(7,1), new Point(6,1),
            // Bottom Left
            new Point(0,6), new Point(1,7), new Point(1,6),
            // Bottom Right
            new Point(6,6), new Point(6,7), new Point(7,6),
        };

        private static List<Point> Borders = new List<Point>()
        {
            // Left
            new Point(0,2), new Point(0,3), new Point(0,4), new Point(0,5),
            // Top
            new Point(2,0), new Point(3,0), new Point(4,0), new Point(5,0),
            // Right
            new Point(7,2), new Point(7,3), new Point(7,4), new Point(7,5),
            // Bottom
            new Point(2,7), new Point(3,7), new Point(4,7), new Point(5,7),
        };

        private static List<Point> InnerGutter = new List<Point>()
        {
            // Left
            new Point(1,2), new Point(1,3), new Point(1,4), new Point(1,5),
            // Top
            new Point(2,1), new Point(3,1), new Point(4,1), new Point(5,1),
            // Right
            new Point(6,2), new Point(6,3), new Point(6,4), new Point(6,5),
            // Bottom
            new Point(2,6), new Point(3,6), new Point(4,6), new Point(5,6),
        };


        /// <summary>
        /// Returns the value of a single spot on the board
        /// </summary>
        /// <param name="CurrentBoard">The game board to use</param>
        /// <param name="Move">The move to consider</param>
        /// <returns>The net value of a single spot on the board</returns>
        static public double ScoreMove(Board OriginalBoard, Board SimulationBoard, Point Move, Piece Turn)
        {
            double Score = 0;
            
            // Negative if this is an opponents turn
            int Sign = App.GetComputerPlayer().GetColor() == Turn ? 1 : -1;

            if (Corners.Contains(Move))
                Score += CornerWeight;
            else if (CornerGutters.Contains(Move))
                Score += CornerGutterWeight * Sign;
            else if (Borders.Contains(Move))
                Score += BorderWeight;
            else if (InnerCorners.Contains(Move))
                Score += InnerCornerWeight;
            else if (InnerGutter.Contains(Move))
                Score += InnerGutterWeight;

            // Add in the number of moves that this turn opens up
            Score += SimulationBoard.AvailableMoves(Turn).Length;
            Score += SimulationBoard.CalculateScore(Turn) - OriginalBoard.CalculateScore(Turn);

            return (Sign * Score);
        }
    }
}
