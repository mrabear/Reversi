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
                double BestWeight = 0;
                Board SimBoard = new Board(SourceBoard);
                Dictionary<Point, double> AnalysisResults = new Dictionary<Point, double>();

                // Puts the initial grey 'disabled' gear icons up
                if (VisualizeProcess)
                    foreach( Point CurrentPoint in PossibleMoves)
                        ReversiWindow.GetGameBoardSurface().HighlightMove(CurrentPoint, AnalysisStatus.QUEUED);

                //****Console.WriteLine("#### New Turn Analysis ####\n", overwrite: true);

                // Loops through each possible move, analyzing the value of each
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
                        //****Console.WriteLine(" Depth| Sign * Value *  Weight  =  Score");
                        //****Console.WriteLine("|------------------------------------------|");
                        //****Console.WriteLine("|------------------------------------------|");

                        // Add the current batch of analysis to the analysis results
                        AnalysisResults.Add(CurrentMove, MoveWeight);

                        // Updates the on screen visualizations of this analysis
                        if (VisualizeProcess)
                            ReversiWindow.GetGameBoardSurface().HighlightMove(CurrentMove, AnalysisStatus.COMPLETE);

                        //****Console.WriteLine("Point (" + CurrentPoint.X + "," + CurrentPoint.Y + ") score=" + MoveWeight + "\n");
                    }
                });
                //}

                // Determine the best selection from the analysis table
                foreach (Point ResultMove in AnalysisResults.Keys)
                {
                    if (AnalysisResults[ResultMove] > AnalysisResults[ChosenMove])
                        ChosenMove = ResultMove;
                }

                SourceBoard.MakeMove(ChosenMove, AITurn);

                //****Console.WriteLine("Point (" + ChosenMove.X + "," + ChosenMove.Y + ") Chosen\n");
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
                        MaxWeight = Math.Max( EvaluatePotentialMove(CurrentMove, SimulationBoard, GetOtherTurn(Turn), CurrentWeight, SimulationDepth + 1), MaxWeight);

                    return (MaxWeight);
                }
                // If there are no more moves for the current player, but the game is not over, start a new simulation for the other player
                else if (SimulationBoard.MovePossible(GetOtherTurn(Turn)))
                {
                    return( EvaluatePotentialMove(SourceMove, SimulationBoard, GetOtherTurn(Turn), CurrentWeight, SimulationDepth + 1));
                }
                // If there are no moves left in the game, collapse the simulation
                else
                {
                    if (SimulationBoard.CalculateScore(AITurn) > SimulationBoard.CalculateScore(GetOtherTurn(Turn)))
                        return (CurrentWeight + TurnAnalysis.VictoryWeight);
                    else
                        return (CurrentWeight - TurnAnalysis.VictoryWeight);
                }
            }

            return (CurrentWeight);
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