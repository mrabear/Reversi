/// <summary>
/// Reversi.Game.cs
/// Brian A. Hebert
/// </summary>

using System;
using System.Windows;

namespace Reversi
{
    /// <summary>
    /// Manages Game state and mostly handles turn processing
    /// </summary>
    public class Game
    {
        private Piece CurrentTurn;
        private Piece NextTurn;
        private Boolean SinglePlayerGame = true;
        private Boolean IsComplete = false;
        private Boolean ProcessMoves = true;
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
        public void SetCurrentTurn(Piece Turn) { CurrentTurn = Turn; }

        /// <summary>
        /// Returns the next turn
        /// </summary>
        public Piece GetNextTurn() { return NextTurn; }

        /// <summary>
        /// Sets the next turn
        /// </summary>
        /// <param name="Turn">The next move</param>
        public void SetNextTurn(Piece Turn) { NextTurn = Turn; }

        /// <summary>
        /// Returns True if the game is currently processing moves
        /// </summary>
        public Boolean GetProcessMoves() { return ProcessMoves; }

        /// <summary>
        /// Sets to True if the game is currently processing moves
        /// </summary>
        /// <param name="isMoveProcessing">Set to True if the game is processing a move</param>
        public void SetProcessMoves(Boolean isMoveProcessing) { ProcessMoves = isMoveProcessing; }

        /// <summary>
        /// Returns the color of the AI opponent
        /// </summary>
        public Boolean GetTurnInProgress() { return TurnInProgress; }

        /// <summary>
        /// Sets to True if the game is currently processing a turn
        /// </summary>
        /// <param name="isMoveProcessing">Set to True if the game is processing a turn</param>
        public void SetTurnInProgress(Boolean isTurninProgress) { TurnInProgress = isTurninProgress; }

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
                        if( MoveOutcome )
                            SwitchTurn();
                    }
                    else
                    {
                        SwitchTurn();
                    }
                }

                if ((SinglePlayerGame) && (CurrentTurn == App.GetComputerPlayer().GetColor()))
                    App.GetComputerPlayer().ProcessAITurn();
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
    }

}