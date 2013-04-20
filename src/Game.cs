// Reversi
// Brian Hebert
//

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
            Difficulty = ReversiForm.getAIDifficulty();
            VsComputer = ReversiForm.isVsComputer();
            GameBoard = new Board(BoardSize);
            IsComplete = false;
            AI = new AI(ReversiApplication.BLACK);

            // Reset the board that tracks which pieces have been drawn on the screen
            ReversiForm.setLastDrawnBoard( BoardSize );
            ReversiForm.getLastDrawnBoard().ClearBoard();
        }

        #region Getters and Setters

        public int getCurrentTurn() { return CurrentTurn; }
        public void setCurrentTurn(int turn) { CurrentTurn = turn; }
        public Board getGameBoard() { return GameBoard; }
        public Boolean getProcessMoves() { return ProcessMoves; }
        public void setProcessMoves(Boolean flag) { ProcessMoves = flag; }
        public AI getAI() { return AI; }
        public int getNextTurn() { return NextTurn; }
        public void setNextTurn(int turn) { NextTurn = turn; }
        public Boolean getTurnInProgress() { return TurnInProgress; }
        public void setTurnInProgress(Boolean flag) { TurnInProgress = flag; }

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
                if (!((VsComputer) && (CurrentTurn == AI.getColor())) )
                {
                    if (GameBoard.MovePossible(CurrentTurn))
                    {
                        if (GameBoard.MakeMove(X, Y, CurrentTurn))
                        {
                            ReversiForm.RefreshPieces(FullRefresh: true);
                            ReversiForm.UpdateScoreBoard();
                            ReversiForm.ShowWinner(GameBoard.DetermineWinner());

                            SwitchTurn();
                        }
                    }
                    else
                    {
                        SwitchTurn();
                    }
                }

                if ((VsComputer) && (CurrentTurn == AI.getColor()))
                    ReversiForm.StartAITurnWorker();
                else
                    TurnInProgress = false;
            }
        }

        /// <summary>
        /// Processes a single AI turn
        /// </summary>
        public void ProcessAITurn()
        {
            while (GameBoard.MovePossible(AI.getColor()))
            {
                AI.MakeNextMove(GameBoard);

                if (GameBoard.MovePossible(AI.getColor() == ReversiApplication.BLACK ? ReversiApplication.WHITE : ReversiApplication.BLACK))
                    break;
                else
                    ReversiForm.RefreshPieces(FullRefresh: true);
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
            ReversiForm.UpdateTurnImage(CurrentTurn);
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