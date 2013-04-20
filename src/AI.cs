/// <summary>
/// Reversi.AI.cs
/// </summary>

using System;
using System.Drawing;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using System.Threading;
using System.Threading.Tasks;

namespace Reversi
{

    /// <summary>
    /// Stores the game simulation logic used by the AI opponent
    /// </summary>
    public class AI
    {
        private int AITurn;
        private static int MaxSimDepth;
        private bool VisualizeProcess;
        private object SpinLock;

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

        /// <summary>
        /// Creates a new AI player
        /// </summary>
        /// <param name="AIcolor">The color of the AI player</param>
        public AI(int AIcolor)
        {
            AITurn = AIcolor;
            VisualizeProcess = false;
            MaxSimDepth = Properties.Settings.Default.MaxDepth;
            SpinLock = new object();
        }

        /// <summary>
        /// Creates a new AI player
        /// </summary>
        /// <param name="AIcolor">The color of the AI player</param>
        /// <param name="NewMaxDepth">The number of turns to look ahead (should be an even number)</param>
        public AI(int AIcolor, int NewMaxDepth)
        {
            AITurn = AIcolor;
            VisualizeProcess = false;
            MaxSimDepth = NewMaxDepth;
            SpinLock = new object();
        }

        #region Getters and Setters

        /// <summary>
        /// Returns the color of the AI opponent
        /// </summary>
        public int GetColor() { return AITurn; }

        /// <summary>
        /// Sets the maximum simulation depth
        /// </summary>
        /// <param name="NewMaxDepth">The maximum number of turns to look ahead</param>
        public void SetMaxDepth(int NewMaxDepth) { MaxSimDepth = NewMaxDepth; }

        /// <summary>
        /// Sets the VisualizeProcess flag
        /// </summary>
        /// <param name="newVisualizeProcess">True if the AI should display the move analysis results</param>
        public void SetVisualizeProcess(bool newVisualizeProcess) { VisualizeProcess = newVisualizeProcess; }

        #endregion

        /// <summary>
        /// Performs a single AI player move
        /// </summary>
        /// <param name="SourceBoard">The board to consider</param>
        public void MakeNextMove(Board SourceBoard)
        {
            Point[] PossibleMoves = SourceBoard.AvailableMoves(AITurn);

            if (PossibleMoves.Length > 0)
            {
                Point ChosenMove = PossibleMoves[0];
                Board SimBoard = new Board(SourceBoard);
                Dictionary<Point, double> MoveResults = new Dictionary<Point, double>();

                if (VisualizeProcess)
                    GraphicsUtil.HighlightPiece(PossibleMoves, Color.Yellow);

                ReversiForm.ReportDebugMessage("#### New Turn Analysis ####\n", overwrite: true);

                //foreach (Point CurrentPoint in PossibleMoves)
                Parallel.ForEach(PossibleMoves, CurrentPoint =>
                {
                    double[] EvalResult = new double[MaxSimDepth];

                    EvaluatePotentialMove(CurrentPoint, SimBoard, AITurn, ref EvalResult);

                    // Serializes the theads to make sure the update functions properly
                    lock (SpinLock)
                    {
                        ReversiForm.ReportDebugMessage(" Depth| Sign * Value *  Weight  =  Score");
                        ReversiForm.ReportDebugMessage("|------------------------------------------|");

                        double MoveWeight = AnalyzeWeightTable(EvalResult);

                        ReversiForm.ReportDebugMessage("|------------------------------------------|");

                        MoveResults.Add(CurrentPoint, MoveWeight);

                        if (VisualizeProcess)
                            GraphicsUtil.HighlightPiece(CurrentPoint, Color.DarkRed, MoveWeight.ToString("0.00"));

                        ReversiForm.ReportDebugMessage("Point (" + CurrentPoint.X + "," + CurrentPoint.Y + ") score=" + MoveWeight + "\n");
                    }
                });
                //}

                foreach (Point ResultMove in MoveResults.Keys)
                {
                    if (MoveResults[ResultMove] > MoveResults[ChosenMove])
                        ChosenMove = ResultMove;
                }

                SourceBoard.MakeMove(ChosenMove.X, ChosenMove.Y, AITurn);

                GraphicsUtil.RefreshPieces();

                if (VisualizeProcess)
                    GraphicsUtil.HighlightPiece(ChosenMove, Color.Green, MoveResults[ChosenMove].ToString("0.00"));

                ReversiForm.ReportDebugMessage("Point (" + ChosenMove.X + "," + ChosenMove.Y + ") Chosen\n");
            }
        }

        /// <summary>
        /// Performs a single move of an AI board simulation
        /// </summary>
        /// <param name="SourceMove">The current simulation move</param>
        /// <param name="CurrentBoard">The current simulation board</param>
        /// <param name="Turn">The current simulation turn</param>
        /// <param name="BandedWeightTable">The table of values used to analyze the simulation</param>
        /// <param name="SimulationDepth">The current depth of the simulation (number of moves ahead)</param>
        private void EvaluatePotentialMove(Point SourceMove, Board CurrentBoard, int Turn, ref double[] BandedWeightTable, int SimulationDepth = 0)
        {
            // Look ahead to the impact of this move
            if (SimulationDepth < MaxSimDepth - 1)
            {
                // Capture the moves available prior to making this change (we'll ignore them later)
                HashSet<Point> IgnoreList = new HashSet<Point>(CurrentBoard.AvailableMoves(GetOtherTurn(Turn)));
                Board SimulationBoard = new Board(CurrentBoard);

                // Perform the requested move
                SimulationBoard.PutPiece(SourceMove, Turn);

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
                    if (SimulationBoard.FindScore(AITurn) > SimulationBoard.FindScore(GetOtherTurn(Turn)))
                        BandedWeightTable[SimulationDepth] = Properties.Settings.Default.VictoryWeight;
                    else
                        BandedWeightTable[SimulationDepth] = Properties.Settings.Default.VictoryWeight * -1;
                }
            }
        }

        /// <summary>
        /// Analyzes a list of banded rows and produces a single digit representing the value of the tree
        /// </summary>
        /// <param name="BandedWeightTable">The table of values to analyze</param>
        /// <returns>A number representing the net value of the given BandedWeightTable</returns>
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

                ReversiForm.ReportDebugMessage(String.Format("|  " + (SimDepth + 1).ToString().PadLeft(2) + " | " + Sign.ToString().PadLeft(3) + "  *" + BandedWeightTable[SimDepth].ToString().PadLeft(4) + "   *" + Penalty.ToString("0.00000").PadLeft(9) + " =" + SubTotal.ToString("0.00000").PadLeft(9)) + " |");
            }

            return (WeightedTotal);
        }

        /// <summary>
        /// Returns the value of a single spot on the board
        /// </summary>
        /// <param name="CurrentBoard">The game board to use</param>
        /// <param name="Move">The move to consider</param>
        /// <returns>The net value of a single spot on the board</returns>
        private double ScoreMove(Board CurrentBoard, Point Move)
        {
            double score = 0;

            /*
            foreach (Point CurrentPoint in CurrentBoard.MovesAround(NewPiece))
                if ((BoardValueMask[CurrentPoint.X, CurrentPoint.Y] > score) && (CurrentBoard.ColorAt(CurrentPoint) == ReversiApplication.EMPTY))
                    score = BoardValueMask[CurrentPoint.X, CurrentPoint.Y];
            */

            return (score * -1 + BoardValueMask[Move.X, Move.Y]);
        }

        /// <summary>
        /// Returns the turn opposite to the one given
        /// </summary>
        /// <param name="turn">The turn to check</param>
        /// <returns>The turn opposite of the one given</returns>
        public int GetOtherTurn(int turn)
        {
            return (turn == ReversiApplication.WHITE ? ReversiApplication.BLACK : ReversiApplication.WHITE);
        }
    }
}