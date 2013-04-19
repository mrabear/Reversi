// Reversi
// Brian Hebert
//

using System;
using System.Drawing;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using System.Threading;
using System.Threading.Tasks;

namespace Reversi
{

    // Class:       AI
    // Description: Stores the game simulation code used by the AI opponent to play the game
    public class AI
    {
        private int color;
        private static int MaxSimDepth;
        private bool VisualizeProcess;

        // This is an attempt to rate the value of each spot on the board
        private int[,] BoardValueMask = new int[,]
            {
	            {9,5,5,5,5,5,5,9},
   	            {5,0,0,0,0,0,0,5},
   	            {5,0,3,1,1,3,0,5},
   	            {5,0,1,0,0,1,0,5},
   	            {5,0,1,0,0,1,0,5},
   	            {5,0,3,1,1,3,0,5},
   	            {5,0,0,0,0,0,0,5},
	            {9,5,5,5,5,5,5,9}
            };

        public AI(int AIcolor)
        {
            color = AIcolor;
            VisualizeProcess = false;
            MaxSimDepth = Properties.Settings.Default.MaxDepth;
        }

        public AI(int AIcolor, int NewMaxDepth)
        {
            color = AIcolor;
            VisualizeProcess = false;
            MaxSimDepth = NewMaxDepth;
        }

        #region Getters and Setters

        public int  getColor() { return color; }
        public void setMaxDepth(int NewMaxDepth) { MaxSimDepth = NewMaxDepth; }
        public void setVisualizeProcess(bool newVisualizeProcess) { VisualizeProcess = newVisualizeProcess; }

        #endregion

        // Return a point representing the best possible next move for this AI
        public void MakeNextMove(Game SourceGame)
        {
            Point[] PossibleMoves = SourceGame.getGameBoard().AvailableMoves(SourceGame.getCurrentTurn());

            if (PossibleMoves.Length > 0)
            {
                Point ChosenMove = PossibleMoves[0];
                Board SimBoard = new Board(SourceGame.getGameBoard());
                Dictionary<Point, double> MoveResults = new Dictionary<Point, double>();

                if (VisualizeProcess)
                    ReversiForm.HighlightPiece(Color.Yellow, PossibleMoves);

                ReversiForm.reportDebugMessage("#### New Turn Analysis ####\n", overwrite: true);

                //foreach (Point CurrentPoint in PossibleMoves)
                Parallel.ForEach(PossibleMoves, CurrentPoint =>
                {
                    double[] EvalResult = new double[MaxSimDepth];

                    EvaluatePotentialMove(CurrentPoint, SimBoard, SourceGame.getCurrentTurn(), ref EvalResult);

                    // Serializes the theads to make sure the update functions properly
                    lock (this)
                    {
                        ReversiForm.reportDebugMessage(" Depth| Sign * Value *  Weight  =  Score");
                        ReversiForm.reportDebugMessage("|------------------------------------------|");

                        double MoveWeight = AnalyzeWeightTable(EvalResult);

                        ReversiForm.reportDebugMessage("|------------------------------------------|");

                        MoveResults.Add(CurrentPoint, MoveWeight);

                        if (VisualizeProcess)
                            ReversiForm.HighlightPiece(Color.DarkRed, CurrentPoint, MoveWeight.ToString("0.00"));

                        ReversiForm.reportDebugMessage("Point (" + CurrentPoint.X + "," + CurrentPoint.Y + ") score=" + MoveWeight + "\n");
                    }
                });
                //}

                foreach (Point ResultMove in MoveResults.Keys)
                {
                    if (MoveResults[ResultMove] > MoveResults[ChosenMove])
                        ChosenMove = ResultMove;
                }

                SourceGame.getGameBoard().MakeMove(ChosenMove.X, ChosenMove.Y, SourceGame.getCurrentTurn());

                ReversiForm.RefreshPieces();

                if (VisualizeProcess)
                    ReversiForm.HighlightPiece(Color.Green, ChosenMove, MoveResults[ChosenMove].ToString("0.00"));

                ReversiForm.reportDebugMessage("Point (" + ChosenMove.X + "," + ChosenMove.Y + ") Chosen\n");
            }
        }

        // Called for each potential board state in our look ahead search
        private void EvaluatePotentialMove(Point SourceMove, Board CurrentBoard, int Turn, ref double[] BandedWeightTable, int SimulationDepth = 0)
        {
            // Look ahead to the impact of this move
            if (SimulationDepth < MaxSimDepth - 1)
            {
                // Capture the moves available prior to making this change (we'll ignore them later)
                HashSet<Point> IgnoreList = new HashSet<Point>(CurrentBoard.AvailableMoves(GetOtherTurn(Turn)));
                Board SimulationBoard = new Board(CurrentBoard);

                // Perform the requested move
                SimulationBoard.PutPiece(SourceMove.X, SourceMove.Y, Turn);

                // Score the result
                if (ScoreMove(SimulationBoard, SourceMove) > BandedWeightTable[SimulationDepth])
                    BandedWeightTable[SimulationDepth] = ScoreMove(SimulationBoard, SourceMove);

                // If there are still moves left for the current player, start a new simulation for each of them
                if (SimulationBoard.MovePossible(Turn))
                {
                    // Start a simulation for the next player with the updated board
                    foreach( Point CurrentMove in SimulationBoard.AvailableMoves(Turn) )
                        //if (!IgnoreList.Contains(CurrentMove))
                        EvaluatePotentialMove(CurrentMove, SimulationBoard, GetOtherTurn(Turn), ref BandedWeightTable, SimulationDepth + 1);
                }
                // If there are no more moves for the current player, but the game is not over, start a new simulation for the other player
                else if (SimulationBoard.MovePossible(GetOtherTurn(Turn)))
                {
                    EvaluatePotentialMove(SourceMove, SimulationBoard, GetOtherTurn(Turn), ref BandedWeightTable, SimulationDepth + 1);
                }
                // If there are no moves left in the game, collapse the simulation
                else
                {
                    if (SimulationBoard.FindScore(color) > SimulationBoard.FindScore(GetOtherTurn(Turn)))
                        BandedWeightTable[SimulationDepth] = Properties.Settings.Default.VictoryWeight;
                    else
                        BandedWeightTable[SimulationDepth] = Properties.Settings.Default.VictoryWeight * -1;
                }
            }
        }

        // Analyzes a list of banded rows and produces a single digit representing the value of the tree
        private double AnalyzeWeightTable(double[] BandedWeightTable)
        {
            double CurrentStep, MaxStep = Math.Floor((double)MaxSimDepth / 2);
            double Penalty, SubTotal, WeightedTotal = 0;
            int Sign;

            for (int SimDepth = 0; SimDepth < BandedWeightTable.Length ; SimDepth++)
            {
                // Current weighted tier 
                CurrentStep = Math.Floor((double)SimDepth / 2);

                // The penalty to assign to this simulation depth
                // 0.5*e^(-1*x)
                Penalty = ( CurrentStep == 0 ) ? 1 : 0.5 * Math.Exp(-1 * CurrentStep);

                // The sign to apply to this analysis (+ for beneficial moves, - for opponent moves)
                Sign = (SimDepth % 2 == 0 ? 1 : -1);

                // The average of all of the values for the current simulation depth
                SubTotal = Sign * BandedWeightTable[SimDepth] * Penalty;

                // The end calculation
                WeightedTotal += SubTotal;

                ReversiForm.reportDebugMessage(String.Format("|  " + (SimDepth + 1).ToString().PadLeft(2) + " | " + Sign.ToString().PadLeft(3) + "  *" + BandedWeightTable[SimDepth].ToString().PadLeft(4) + "   *" + Penalty.ToString("0.00000").PadLeft(9) + " =" + SubTotal.ToString("0.00000").PadLeft(9)) + " |");
            }

            return (WeightedTotal);
        }

        // Returns the value of a single spot on the board
        private double ScoreMove(Board CurrentBoard, Point NewPiece)
        {
            double score = 0;

            /*
            foreach (Point CurrentPoint in CurrentBoard.MovesAround(NewPiece))
                if ((BoardValueMask[CurrentPoint.X, CurrentPoint.Y] > score) && (CurrentBoard.ColorAt(CurrentPoint) == ReversiApplication.EMPTY))
                    score = BoardValueMask[CurrentPoint.X, CurrentPoint.Y];
            */

            return( score * -1 + BoardValueMask[NewPiece.X, NewPiece.Y] );
        }

        public int GetOtherTurn(int color)
        {
            return (color == ReversiApplication.WHITE ? ReversiApplication.BLACK : ReversiApplication.WHITE);
        }

    }
}