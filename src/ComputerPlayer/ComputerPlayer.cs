/// <summary>
/// Reversi.AI.cs
/// Brian A. Hebert
/// </summary>

using System;
using System.Windows;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading;
using System.Threading.Tasks;

namespace Reversi
{
    /// <summary>
    /// Stores the game simulation logic used by the AI opponent
    /// </summary>
    public class ComputerPlayer
    {
        // The turn of the computer player
        private Piece AITurn;

        // The maximum number of turns to look ahead
        private static int MaxSimDepth;

        // True to have the analysis visualized on the gameboard
        private bool VisualizeProcess;

        // The locking object used to multithread the analysis
        private readonly object SpinLock;

        // The background worker used to separate the AI crunch from the UI
        private readonly BackgroundWorker AIBGWorker = new BackgroundWorker();

        private static Dictionary<Point, AnalysisResultRow> AnalysisResults = new Dictionary<Point, AnalysisResultRow>();

        /// <summary>
        /// Creates a new AI player
        /// </summary>
        /// <param name="AIcolor">The color of the AI player</param>
        public ComputerPlayer(Piece AIcolor)
        {
            AITurn = AIcolor;
            VisualizeProcess = false;
            MaxSimDepth = Properties.Settings.Default.MAX_SIM_DEPTH;
            SpinLock = new object();

            AIBGWorker.DoWork += AIBGWorker_DoWork;
            AIBGWorker.RunWorkerCompleted += AIBGWorker_Completed;
        }

        #region Getters and Setters

        /// <summary>
        /// Returns the color of the AI opponent
        /// </summary>
        public Piece GetColor() { return AITurn; }

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

        /// <summary>
        /// Gets the current state of the computer player's turn analysis
        /// </summary>
        public Dictionary<Point, AnalysisResultRow> GetAnalysisResults() { return AnalysisResults; }

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
                AnalysisResults = new Dictionary<Point,AnalysisResultRow>();

                foreach( Point CurrentPoint in PossibleMoves )
                {
                    AnalysisResults.Add( CurrentPoint, new AnalysisResultRow());
                }

                if (VisualizeProcess)
                    foreach( Point CurrentPoint in PossibleMoves)
                        ReversiWindow.GetGameBoardSurface().HighlightMove(CurrentPoint, AnalysisStatus.QUEUED);

                //****FormUtil.ReportDebugMessage("#### New Turn Analysis ####\n", overwrite: true);

                //foreach (Point CurrentPoint in PossibleMoves)
                Parallel.ForEach(PossibleMoves, CurrentPoint =>
                {
                                        // Serializes the theads to make sure the update functions properly
                    lock (SpinLock)
                        if( VisualizeProcess )
                            ReversiWindow.GetGameBoardSurface().HighlightMove(CurrentPoint, AnalysisStatus.WORKING);

                    double[] EvalResult = new double[MaxSimDepth];

                    EvaluatePotentialMove(CurrentPoint, SimBoard, AITurn, ref EvalResult);

                    // Serializes the theads to make sure the update functions properly
                    lock (SpinLock)
                    {
                        //****FormUtil.ReportDebugMessage(" Depth| Sign * Value *  Weight  =  Score");
                        //****FormUtil.ReportDebugMessage("|------------------------------------------|");

                        double MoveWeight = AnalyzeWeightTable(EvalResult);

                        //****FormUtil.ReportDebugMessage("|------------------------------------------|");

                        MoveResults.Add(CurrentPoint, MoveWeight);

                        if (VisualizeProcess)
                        {
                            AnalysisResults[CurrentPoint].AnalysisResult = MoveWeight;
                            AnalysisResults[CurrentPoint].AnalysisCompleted = true;
                            ReversiWindow.GetGameBoardSurface().HighlightMove(CurrentPoint, AnalysisStatus.COMPLETE);
                        }

                        //****FormUtil.ReportDebugMessage("Point (" + CurrentPoint.X + "," + CurrentPoint.Y + ") score=" + MoveWeight + "\n");
                    }
                });
                //}

                foreach (Point ResultMove in MoveResults.Keys)
                {
                    if (MoveResults[ResultMove] > MoveResults[ChosenMove])
                        ChosenMove = ResultMove;
                }

                SourceBoard.MakeMove(ChosenMove, AITurn);

                AnalysisResults = new Dictionary<Point, AnalysisResultRow>();

                //if (VisualizeProcess)
                //    GameBoard.HighlightPieces();

                //****FormUtil.ReportDebugMessage("Point (" + ChosenMove.X + "," + ChosenMove.Y + ") Chosen\n");
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
        private void EvaluatePotentialMove(Point SourceMove, Board CurrentBoard, Piece Turn, ref double[] BandedWeightTable, int SimulationDepth = 0)
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
                        BandedWeightTable[SimulationDepth] = AnalysisConfiguration.VictoryWeight;
                    else
                        BandedWeightTable[SimulationDepth] = AnalysisConfiguration.VictoryWeight * -1;
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

                //***FormUtil.ReportDebugMessage(String.Format("|  " + (SimDepth + 1).ToString().PadLeft(2) + " | " + Sign.ToString().PadLeft(3) + "  *" + BandedWeightTable[SimDepth].ToString().PadLeft(4) + "   *" + Penalty.ToString("0.00000").PadLeft(9) + " =" + SubTotal.ToString("0.00000").PadLeft(9)) + " |");
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
            return (ScoreMove(CurrentBoard, Convert.ToInt32(Move.X), Convert.ToInt32(Move.Y)));
        }

        /// <summary>
        /// Returns the value of a single spot on the board
        /// </summary>
        /// <param name="CurrentBoard">The game board to use</param>
        /// <param name="Move">The move to consider</param>
        /// <returns>The net value of a single spot on the board</returns>
        private double ScoreMove(Board CurrentBoard, int SourceX, int SourceY)
        {
            double score = 0;
        
            foreach (Point CurrentPoint in CurrentBoard.MovesAround(new Point(SourceX, SourceY)))
                if ((AnalysisConfiguration.BoardValueMask[Convert.ToInt16(CurrentPoint.X), Convert.ToInt16(CurrentPoint.Y)] > score) && (CurrentBoard.ColorAt(CurrentPoint) == Piece.EMPTY))
                    score = AnalysisConfiguration.BoardValueMask[Convert.ToInt16(CurrentPoint.X), Convert.ToInt16(CurrentPoint.Y)];

            return (score * -1 + AnalysisConfiguration.BoardValueMask[SourceX, SourceY]);
        }

        /// <summary>
        /// Returns the turn opposite to the one given
        /// </summary>
        /// <param name="turn">The turn to check</param>
        /// <returns>The turn opposite of the one given</returns>
        public Piece GetOtherTurn(Piece turn)
        {
            return (turn == Piece.WHITE ? Piece.BLACK : Piece.WHITE);
        }

        #region AI Background Workers

        /// <summary>
        /// Called when the AI monitor has no more moves to place
        /// </summary>
        public void StartComputerTurnAnalysis()
        {
            AIBGWorker.RunWorkerAsync();
        }

        /// <summary>
        /// Called when the AI monitor has no more moves to place
        /// </summary>
        public void AIBGWorker_Completed(object sender, RunWorkerCompletedEventArgs e)
        {
            App.GetActiveGame().SetTurnInProgress(false);
            App.GetActiveGame().SwitchTurn();
            App.GetActiveGame().AddBoardToMoveHistory();
            ReversiWindow.GetGameBoardSurface().Refresh();
        }

        /// <summary>
        /// Called asynchronously when it is time for the AI to wake up and do some work
        /// </summary>
        private void AIBGWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            while (App.GetActiveGameBoard().MovePossible(GetColor()))
            {
                MakeNextMove(App.GetActiveGameBoard());

                if (App.GetActiveGameBoard().MovePossible(GetColor() == Piece.BLACK ? Piece.WHITE : Piece.BLACK))
                    break;
                else
                    ReversiWindow.GetGameBoardSurface().Refresh();
            }
        }

        #endregion
    }
}