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

        // The move currently chosen by the computer player
        private Point ChosenMove;

        /// <summary>
        /// Creates a new AI player
        /// </summary>
        /// <param name="AIcolor">The color of the AI player</param>
        public ComputerPlayer(Piece AIcolor)
        {
            AITurn = AIcolor;
            VisualizeProcess = true;
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
                ChosenMove = PossibleMoves[0];
                Board SimBoard = new Board(SourceBoard);
                Dictionary<Point, double> AnalysisResults = new Dictionary<Point, double>();

                // Puts the initial grey 'disabled' gear icons up
                if (VisualizeProcess)
                    foreach( Point CurrentPoint in PossibleMoves)
                        ReversiWindow.GetGameBoardSurface().HighlightMove(CurrentPoint, AnalysisStatus.QUEUED);

                // Loops through each possible move, analyzing the value of each
                // Uncomment the foreach loop and comment out the parallel.foreach loop to single thread the turn analyis
                //foreach (Point CurrentPoint in PossibleMoves)
                Parallel.ForEach(PossibleMoves, CurrentMove =>
                {
                    // Serializes the theads to make sure the update functions properly
                    lock (SpinLock)
                        if( VisualizeProcess )
                            ReversiWindow.GetGameBoardSurface().HighlightMove(CurrentMove, AnalysisStatus.WORKING);

                    // Starts the analysis for this move
                    double MoveWeight = EvaluatePotentialMove(CurrentMove, SimBoard, AITurn, 0);

                    // Serializes the theads to make sure the update functions properly
                    lock (SpinLock)
                    {
                        // Add the current batch of analysis to the analysis results
                        AnalysisResults.Add(CurrentMove, MoveWeight);

                        // Updates the on screen visualizations of this analysis
                        if (VisualizeProcess)
                            ReversiWindow.GetGameBoardSurface().HighlightMove(CurrentMove, AnalysisStatus.COMPLETE);
                    }
                });
                //}

                // Determine the best selection from the analysis table
                foreach (Point ResultMove in AnalysisResults.Keys)
                    if (AnalysisResults[ResultMove] > AnalysisResults[ChosenMove])
                        ChosenMove = ResultMove;

                SourceBoard.MakeMove(ChosenMove, AITurn);
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
        private double EvaluatePotentialMove(Point SourceMove, Board CurrentBoard, Piece Turn, double CurrentWeight, int SimulationDepth = 0)
        {
            // Look ahead to the impact of this move
            if (SimulationDepth < MaxSimDepth - 1)
            {
                // Capture the moves available prior to making this change (we'll ignore them later)
                //HashSet<Point> IgnoreList = new HashSet<Point>(CurrentBoard.AvailableMoves(GetOtherTurn(Turn)));
                Board SimulationBoard = new Board(CurrentBoard);

                // Perform the requested move
                SimulationBoard.PutPiece(SourceMove, Turn);

                // Score the result
                CurrentWeight += TurnAnalysis.ScoreMove(CurrentBoard, SimulationBoard, SourceMove, Turn);

                // If there are still moves left for the current player, start a new simulation for each of them
                if (SimulationBoard.MovePossible(Turn))
                {
                    double MaxWeight = 0;

                    // Start a simulation for the next player with the updated board
                    foreach( Point CurrentMove in SimulationBoard.AvailableMoves(Turn) )
                        //if (!IgnoreList.Contains(CurrentMove))
                        MaxWeight = Math.Max(EvaluatePotentialMove(CurrentMove, SimulationBoard, Game.GetOtherTurn(Turn), CurrentWeight, SimulationDepth + 1), MaxWeight);

                    return (MaxWeight);
                }
                // If there are no more moves for the current player, but the game is not over, start a new simulation for the other player
                else if (SimulationBoard.MovePossible(Game.GetOtherTurn(Turn)))
                {
                    return (EvaluatePotentialMove(SourceMove, SimulationBoard, Game.GetOtherTurn(Turn), CurrentWeight, SimulationDepth + 1));
                }
                // If there are no moves left in the game, collapse the simulation
                else
                {
                    if (SimulationBoard.CalculateScore(AITurn) > SimulationBoard.CalculateScore(Game.GetOtherTurn(Turn)))
                        return (CurrentWeight + TurnAnalysis.VictoryWeight);
                    else
                        return (CurrentWeight - TurnAnalysis.VictoryWeight);
                }
            }

            return (CurrentWeight);
        }

        #region AI Background Workers

        /// <summary>
        /// Called when the AI monitor has no more moves to place
        /// </summary>
        public void StartComputerTurnAnalysis()
        {
            if( !AIBGWorker.IsBusy )
                AIBGWorker.RunWorkerAsync();
        }

        /// <summary>
        /// Called when the AI monitor has no more moves to place
        /// </summary>
        public void AIBGWorker_Completed(object sender, RunWorkerCompletedEventArgs e)
        {
            App.GetActiveGame().SwitchTurn();
            App.GetActiveGame().AddBoardToMoveHistory();
            ReversiWindow.GetGameBoardSurface().FlipCapturedPieces(ChosenMove);
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
                    ReversiWindow.GetGameBoardSurface().FlipCapturedPieces(ChosenMove);
            }
        }

        #endregion
    }
}