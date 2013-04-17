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

        // This is an attempt to rate the value of each spot on the board
        private int[,] BoardValueMask = new int[,]
            {
	            {9,2,5,5,5,5,2,9},
   	            {2,0,0,0,0,0,0,2},
   	            {5,0,3,1,1,3,0,5},
   	            {5,0,1,0,0,1,0,5},
   	            {5,0,1,0,0,1,0,5},
   	            {5,0,3,1,1,3,0,6},
   	            {2,0,0,0,0,0,0,2},
	            {9,2,5,5,5,5,2,9}
            };

        public AI(int AIcolor)
        {
            color = AIcolor;
        }

        #region Getters and Setters

        public int getColor() { return color; }

        #endregion

        // Return a point representing the best possible next move for this AI
        public void MakeNextMove(Game SourceGame)
        {
            Point[] PossibleMoves = SourceGame.getGameBoard().AvailableMoves(SourceGame.getCurrentTurn());

            if (PossibleMoves.Length > 1)
            {
                Point ChosenMove = PossibleMoves[0];
                Board SimBoard = new Board(SourceGame.getGameBoard());
                Dictionary<Point, double> MoveResults = new Dictionary<Point, double>();

                ReversiForm.HighlightPiece(Color.Yellow, PossibleMoves);

                Console.WriteLine("#### New Turn Analysis ####\nCurrentBoard:\n" + SimBoard.ToString() + "");

                Parallel.ForEach(PossibleMoves, CurrentPoint =>
                {
                    SimBoard.CopyBoard(SourceGame.getGameBoard());
                    SimBoard.PutPiece(CurrentPoint.X, CurrentPoint.Y, SourceGame.getCurrentTurn());

                    double[] EvalResult = new double[Properties.Settings.Default.MaxDepth];

                    EvaluatePotentialMove(ref EvalResult, SimBoard, SourceGame.getCurrentTurn());

                    // Serializes the theads to make sure the update functions properly
                    lock (this)
                    {
                        Console.WriteLine(" Depth| Sign * Value *  Weight  =  Score");
                        Console.WriteLine("|------------------------------------------|");

                        double MoveWeight = AnalyzeWeightTable(EvalResult);

                        Console.WriteLine("|------------------------------------------|");

                        MoveResults.Add(CurrentPoint, MoveWeight);

                        ReversiForm.HighlightPiece(Color.Red, CurrentPoint, MoveWeight.ToString("0.00"));

                        Console.WriteLine("\t Point (" + CurrentPoint.X + "," + CurrentPoint.Y + ")\t\tscore=" + MoveWeight + "\n");
                    }
                });

                foreach (Point ResultMove in MoveResults.Keys)
                {
                    if (MoveResults[ResultMove] > MoveResults[ChosenMove])
                        ChosenMove = ResultMove;
                }

                SourceGame.getGameBoard().MakeMove(ChosenMove.X, ChosenMove.Y, SourceGame.getCurrentTurn());

                ReversiForm.RefreshPieces();
                ReversiForm.HighlightPiece(Color.Green, ChosenMove, MoveResults[ChosenMove].ToString("0.00"));

                Console.WriteLine("Point (" + ChosenMove.X + "," + ChosenMove.Y + ") Chosen\n");
            }
        }

        // Called for each potential board state in our look ahead search
        private void EvaluatePotentialMove(ref double[] BandedWeightTable, Board CurrentBoard, int Turn, int SimulationDepth = 0)
        {
            if (SimulationDepth < Properties.Settings.Default.MaxDepth)
            {
                // If there are still moves left for the current player, start a new simulation for each of them
                if (CurrentBoard.MovePossible(Turn))
                {
                    Point[] PossibleMoves = CurrentBoard.AvailableMoves(Turn);
                    Board SimulationBoard;
                    Point BestPoint = PossibleMoves[0];

                    // If it is the opponents turn only pick their best moves
                    if ((SimulationDepth % 2 != 0))
                        foreach (Point CurrentPoint in PossibleMoves)
                            if (ScoreMove(CurrentBoard, CurrentPoint) > ScoreMove(CurrentBoard, BestPoint))
                                BestPoint = CurrentPoint;

                    for (int index = 0; index < PossibleMoves.Length; index++)
                    {
                        if (ScoreMove(CurrentBoard, PossibleMoves[index]) >= ScoreMove(CurrentBoard, BestPoint))
                        {
                            // Make a copy of the current board
                            SimulationBoard = new Board(CurrentBoard);

                            // Place the current move on the new board
                            SimulationBoard.PutPiece(PossibleMoves[index].X, PossibleMoves[index].Y, Turn);

                            if (ScoreMove(CurrentBoard, PossibleMoves[index]) > BandedWeightTable[SimulationDepth])
                                BandedWeightTable[SimulationDepth] = ScoreMove(CurrentBoard, PossibleMoves[index]);

                            // Start a simulation for the next player with the updated board
                            EvaluatePotentialMove(ref BandedWeightTable, SimulationBoard, GetOtherTurn(Turn), SimulationDepth + 1);
                        }
                    }
                }
                // If there are no more moves for the current player, but the game is not over, start a new simulation for the other player
                else if (CurrentBoard.MovePossible(GetOtherTurn( Turn )))
                {
                    EvaluatePotentialMove(ref BandedWeightTable, CurrentBoard, GetOtherTurn(Turn), SimulationDepth + 1);
                }
                // If there are no moves left in the game, collapse the simulation
                else
                {
                    if (CurrentBoard.FindScore(color) > CurrentBoard.FindScore(GetOtherTurn(Turn)))
                        BandedWeightTable[SimulationDepth] = Properties.Settings.Default.VictoryWeight;
                    else
                        BandedWeightTable[SimulationDepth] = Properties.Settings.Default.VictoryWeight * -1;
                }
            }
        }

        // Analyzes a list of banded rows and produces a single digit representing the value of the tree
        private double AnalyzeWeightTable(double[] BandedWeightTable)
        {
            double CurrentStep, MaxStep = Math.Floor((double)Properties.Settings.Default.MaxDepth / 2);
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

                Console.WriteLine(String.Format("|  " + SimDepth.ToString().PadLeft(2) + " | " + Sign.ToString().PadLeft(3) + "  *" + BandedWeightTable[SimDepth].ToString().PadLeft(4) + "   *" + Penalty.ToString("0.00000").PadLeft(9) + " =" + SubTotal.ToString("0.00000").PadLeft(9)) + " |");
            }

            return (WeightedTotal);
        }

        // Returns the value of a single spot on the board
        private double ScoreMove(Board CurrentBoard, Point NewPiece)
        {
            return BoardValueMask[NewPiece.X, NewPiece.Y];
        }

        public int GetOtherTurn(int color)
        {
            return (color == ReversiApplication.WHITE ? ReversiApplication.BLACK : ReversiApplication.WHITE);
        }

    }
}