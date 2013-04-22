/// <summary>
/// Reversi.Game.cs
/// </summary>

using System;
using System.Drawing;

namespace Reversi
{
    /// <summary>
    /// Manages Game state and mostly handles turn processing
    /// </summary>
    public class Game
    {
        private int CurrentTurn;
        private int NextTurn;
        private int Difficulty;
        private Boolean VsComputer = true;
        private Board GameBoard;
        private Boolean IsComplete = false;
        private Boolean ProcessMoves = true;
        private Boolean TurnInProgress = false;
        private AI AI;

        /// <summary>
        /// Creates a new Game instance
        /// </summary>
        /// <param name="BoardSize">The size of the game board</param>
        public Game(int BoardSize = 8)
        {
            CurrentTurn = ReversiApplication.WHITE;
            NextTurn = ReversiApplication.BLACK;
            Difficulty = ReversiForm.GetAIDifficulty();
            VsComputer = ReversiForm.VsComputer();
            GameBoard = new Board(BoardSize);
            IsComplete = false;
            AI = new AI(ReversiApplication.BLACK);

            // Reset the board that tracks which pieces have been drawn on the screen
            ReversiForm.SetLastDrawnBoard( BoardSize );
            ReversiForm.GetLastDrawnBoard().ClearBoard();
        }

        #region Getters and Setters

        /// <summary>
        /// Returns the current game turn
        /// </summary>
        public int GetCurrentTurn() { return CurrentTurn; }

        /// <summary>
        /// Sets the current game turn
        /// </summary>
        /// <param name="Turn">The current turn</param>
        public void SetCurrentTurn(int Turn) { CurrentTurn = Turn; }

        /// <summary>
        /// Returns the next turn
        /// </summary>
        public int GetNextTurn() { return NextTurn; }

        /// <summary>
        /// Sets the next turn
        /// </summary>
        /// <param name="Turn">The next move</param>
        public void SetNextTurn(int Turn) { NextTurn = Turn; }

        /// <summary>
        /// Returns the current game board
        /// </summary>
        public Board GetGameBoard() { return GameBoard; }

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
        /// Returns the AI opponent object
        /// </summary>
        public AI GetAI() { return AI; }

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
        public void ProcessTurn(Point Move)
        {
            ProcessTurn(Move.X, Move.Y);
        }

        /// <summary>
        /// Processes a single turn of gameplay, two if it is vs. AI
        /// </summary>
        /// <param name="X">The X value of the move</param>
        /// <param name="Y">The Y value of the move</param>
        public void ProcessTurn(int X, int Y)
        {
            TurnInProgress = true;

            if (!IsComplete)
            {
                // As long as this isn't an AI turn, process the requested move
                if (!((VsComputer) && (CurrentTurn == AI.GetColor())) )
                {
                    if (GameBoard.MovePossible(CurrentTurn))
                    {
                        if (GameBoard.MakeMove(X, Y, CurrentTurn))
                        {
                            GraphicsUtil.RefreshPieces(FullRefresh: true);
                            GraphicsUtil.UpdateScoreBoard();
                            GraphicsUtil.ShowWinner(GameBoard.DetermineWinner());

                            SwitchTurn();
                        }
                    }
                    else
                    {
                        SwitchTurn();
                    }
                    //GraphicsUtil.MarkAvailableMoves(CurrentTurn);
                }

                if ((VsComputer) && (CurrentTurn == AI.GetColor()))
                    FormUtil.StartAITurnWorker();
                else
                    TurnInProgress = false;
            }
        }

        /// <summary>
        /// Processes a single AI turn
        /// </summary>
        public void ProcessAITurn()
        {
            while (GameBoard.MovePossible(AI.GetColor()))
            {
                AI.MakeNextMove(GameBoard);

                if (GameBoard.MovePossible(AI.GetColor() == ReversiApplication.BLACK ? ReversiApplication.WHITE : ReversiApplication.BLACK))
                    break;
                else
                    GraphicsUtil.RefreshPieces(FullRefresh: true);
            }
        }

        /// <summary>
        /// Switches the current turn and updates the turn image
        /// </summary>
        public void SwitchTurn()
        {
            if (CurrentTurn == ReversiApplication.WHITE)
            {
                CurrentTurn = ReversiApplication.BLACK;
                NextTurn = ReversiApplication.WHITE;
            }
            else
            {
                CurrentTurn = ReversiApplication.WHITE;
                NextTurn = ReversiApplication.BLACK;
            }
            GraphicsUtil.UpdateScoreBoard(CurrentTurn);
        }

        /// <summary>
        /// Returns a string representation of the current turn, used for display purposes
        /// </summary>
        /// <param name="color">The color to convert to a string</param>
        /// <returns>A string representation of the current turn</returns>
        public string GetTurnString(int color)
        {
            if (color == ReversiApplication.WHITE)
                return ("White");
            else if (color == ReversiApplication.BLACK)
                return ("Black");
            else if (color == ReversiApplication.EMPTY)
                return ("Empty");
            else
                return ("Illegal Color!");
        }
    }

}