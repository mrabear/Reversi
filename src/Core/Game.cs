/// <summary>
/// Reversi.Game.cs
/// Brian A. Hebert
/// </summary>

using System;
using System.Windows;
using System.Linq;
using System.Collections.Generic;
using System.Collections.Specialized;

namespace Reversi
{
    /// <summary>
    /// Manages Game state and mostly handles turn processing
    /// </summary>
    public class Game
    {
        // The historical timeline of game states
        private List<GameState> MoveHistory = new List<GameState>();

        // Where on the MoveHistory list the current game is
        private int HistoricalIndex;

        // The player who is currently moving
        private Piece CurrentTurn;

        // The player who is moving next
        private Piece NextTurn;

        // True if the game is vs Computer, false if it is multiplayer
        private Boolean SinglePlayerGame = true;

        // True if the game is over
        private Boolean IsComplete = false;

        // True if a turn is currently being processed
        private Boolean TurnInProgress = false;

        /// <summary>
        /// Creates a new Game instance
        /// </summary>
        /// <param name="BoardSize">The size of the game board</param>
        public Game(int BoardSize = 8)
        {
            CurrentTurn = Piece.WHITE;
            NextTurn = Piece.BLACK;
            //VsComputer = ReversiForm.VsComputer();
            App.ResetActiveGameBoard(BoardSize);

            HistoricalIndex = 0;
            AddBoardToMoveHistory();

            IsComplete = false;
            App.ResetComputerPlayer(Piece.BLACK);
        }

        #region Getters and Setters

        /// <summary>
        /// Returns the current game turn
        /// </summary>
        public Piece GetCurrentTurn() { return CurrentTurn; }

        public bool IsSinglePlayerGame() { return SinglePlayerGame; }
        public void SetSinglePlayerGame(bool SinglePlayerGameFlag) { SinglePlayerGame = SinglePlayerGameFlag; }

        /// <summary>
        /// Sets the current game turn
        /// </summary>
        /// <param name="Turn">The current turn</param>
        public void SetCurrentTurn(Piece Turn) { 
            CurrentTurn = Turn;
            NextTurn = (Turn == Piece.WHITE ? Piece.BLACK : Piece.WHITE);
        }

        /// <summary>
        /// Returns the next turn
        /// </summary>
        public Piece GetNextTurn() { return NextTurn; }

        /// <summary>
        /// Sets the next turn
        /// </summary>
        /// <param name="Turn">The next move</param>
        public void SetNextTurn(Piece Turn) { 
            NextTurn = Turn;
            CurrentTurn = (Turn == Piece.WHITE ? Piece.BLACK : Piece.WHITE);
        }

        /// <summary>
        /// Returns the color of the AI opponent
        /// </summary>
        public Boolean GetTurnInProgress() { return TurnInProgress; }

        /// <summary>
        /// Sets to True if the game is currently processing a turn
        /// </summary>
        /// <param name="isMoveProcessing">Set to True if the game is processing a turn</param>
        public void SetTurnInProgress(Boolean isTurninProgress) { TurnInProgress = isTurninProgress; }

        /// <summary>
        /// Returns true if the game can be advanced (if there are states to move forward to)
        /// </summary>
        public bool CanAdvance() 
        {
            if ((HistoricalIndex < MoveHistory.Count - 1) && ((!SinglePlayerGame) || (HistoricalIndex < MoveHistory.Count - 1)))
                return true;

            return false;
        }

        /// <summary>
        /// Returns true if the game can be rewound (if there are states to move back to)
        /// </summary>
        public bool CanRewind()
        {
            if (HistoricalIndex > 0)
                return true;

            return false;
        }

        #endregion

        /// <summary>
        /// Processes a single turn of gameplay, two if it is vs. AI
        /// </summary>
        /// <param name="Move">The move to process</param>
        public bool ProcessUserTurn(Point Move)
        {
            return (ProcessUserTurn(Convert.ToInt32(Move.X), Convert.ToInt32(Move.Y)));
        }

        /// <summary>
        /// Processes a single turn of gameplay, two if it is vs. AI
        /// </summary>
        /// <param name="X">The X value of the move</param>
        /// <param name="Y">The Y value of the move</param>
        public bool ProcessUserTurn(int X, int Y)
        {
            bool MoveOutcome = false;

            TurnInProgress = true;

            if (!IsComplete)
            {
                // As long as this isn't an AI turn, process the requested move
                if (!((SinglePlayerGame) && (CurrentTurn == App.GetComputerPlayer().GetColor())))
                {
                    if (App.GetActiveGameBoard().MovePossible(CurrentTurn))
                    {
                        MoveOutcome = App.GetActiveGameBoard().MakeMove(X, Y, CurrentTurn);
                        if (MoveOutcome)
                        {
                            SwitchTurn();
                            AddBoardToMoveHistory();
                        }
                    }
                    else
                    {
                        SwitchTurn();
                    }
                }

                if ((SinglePlayerGame) && (CurrentTurn == App.GetComputerPlayer().GetColor()))
                    App.GetComputerPlayer().StartComputerTurnAnalysis();
                else
                    TurnInProgress = false;
            }

            return (MoveOutcome);
        }

        /// <summary>
        /// Switches the current turn and updates the turn image
        /// </summary>
        public void SwitchTurn()
        {
            if (CurrentTurn == Piece.WHITE)
            {
                CurrentTurn = Piece.BLACK;
                NextTurn = Piece.WHITE;
            }
            else
            {
                CurrentTurn = Piece.WHITE;
                NextTurn = Piece.BLACK;
            }
        }

        /// <summary>
        /// Snapshot the current game and store it in the historical timeline
        /// </summary>
        public void AddBoardToMoveHistory() { AddBoardToMoveHistory(App.GetActiveGameBoard(), CurrentTurn); }

        /// <summary>
        /// Snapshot the game and store it in the historical timeline
        /// </summary>
        /// <param name="HistoricalBoard">The board to snapshot</param>
        /// <param name="PlayerTurn">The player who is moving next</param>
        public void AddBoardToMoveHistory(Board HistoricalBoard, Piece PlayerTurn)
        {
            // If the move is happening in the past (when the user has advanced or rewound the game)
            // erase the moves in front of it, as they belong to a completely different game timeline now
            if (HistoricalIndex != Math.Max(0,MoveHistory.Count - 1))
                MoveHistory.RemoveRange(Math.Max(1,HistoricalIndex), MoveHistory.Count - HistoricalIndex - 1);

            // Add the current gamestate to the move history 
            MoveHistory.Add(new GameState(HistoricalBoard, PlayerTurn));

            // Make the current addition 
            HistoricalIndex = MoveHistory.Count - 1;
        }

        /// <summary>
        /// Advances the game 1 turn into the future
        /// </summary>
        public void AdvanceHistoricalState()
        {
            MoveHistoricalState(1);
        }

        /// <summary>
        /// Rewinds the game 1 turn into the past
        /// </summary>
        public void RewindHistoricalState()
        {
            MoveHistoricalState(-1);
        }

        /// <summary>
        /// Alters the game state, replacing it with a previously stored historical state.
        /// </summary>
        /// <param name="Direction">Set to +1 to move forward, -1 to move backwards</param>
        private void MoveHistoricalState(int Direction = -1)
        {
            // If it is a single player game, step over two turns
            if (SinglePlayerGame)
                Direction = 2 * Direction;

            // Move the historical index that we are looking at, forward or backwards depending on the Direction parameter
            HistoricalIndex = Math.Min(MoveHistory.Count - 1, Math.Max(0, HistoricalIndex + Direction));

            // Overwrite the current game board with the new historical one
            App.SetActiveGameBoard(MoveHistory[HistoricalIndex].BoardState);

            // Overwrite the current game turn with the historical one
            SetCurrentTurn(MoveHistory[HistoricalIndex].TurnState);

            // Reset the graphics objects to display the new game state
            ScoreBoard.Clear();
            ReversiWindow.GetGameBoardSurface().Clear();
            ReversiWindow.GetGameBoardSurface().Refresh();
        }
    }
}