// Reversi
// Brian Hebert
//

using System;
using System.Drawing;

namespace Reversi
{
    // Class:       Game
    // Description: Stores game state information and rules
    public class Game
    {
        // Color constants
        private static int BLACK = Properties.Settings.Default.BLACK;
        private static int WHITE = Properties.Settings.Default.WHITE;
        private static int EMPTY = Properties.Settings.Default.EMPTY;

        private int CurrentTurn;
        private int NextTurn;
        private int Difficulty;
        private Boolean VsComputer = true;
        private Board GameBoard;
        private int Winner;
        private Boolean IsComplete = false;
        private Boolean ProcessMoves = true;
        private Boolean TurnInProgress = false;
        private AI AI;

        public Game(int BoardSize = 8)
        {
            CurrentTurn = WHITE;
            NextTurn = BLACK;
            Difficulty = ReversiForm.getAIDifficulty();
            VsComputer = ReversiForm.isVsComputer();
            GameBoard = new Board(BoardSize);
            IsComplete = false;
            AI = new AI(BLACK);

            // Reset the board image to clear any pieces from previous games
            ReversiForm.ResetBoardImage();

            // Reset the board that tracks which pieces have been drawn on the screen
            ReversiForm.setLastDrawnBoard( BoardSize );
            ReversiForm.getLastDrawnBoard().ClearBoard();

            ReversiForm.RefreshPieces(GameBoard);
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

        // Determines if there is a winner in the current game
        public Boolean DetermineWinner()
        {
            int WhiteScore = GameBoard.FindScore(WHITE);
            int BlackScore = GameBoard.FindScore(BLACK);

            if (WhiteScore == 0)
            {
                IsComplete = true;
                Winner = BLACK;
            }
            else if (BlackScore == 0)
            {
                IsComplete = true;
                Winner = WHITE;
            }
            else if (((WhiteScore + BlackScore) == 64) ||
                ((!GameBoard.MovePossible(CurrentTurn)) && (!GameBoard.MovePossible(NextTurn))))
            {
                IsComplete = true;
                if (BlackScore > WhiteScore)
                    Winner = BLACK;
                else if (BlackScore < WhiteScore)
                    Winner = WHITE;
                else
                    Winner = EMPTY;
            }

            if (IsComplete)
            {
                ReversiForm.ShowWinner(Winner);
            }

            return (IsComplete);
        }

        // Processes a single turn of gameplay, two if it is vs. AI
        public void ProcessTurn(int x, int y)
        {
            TurnInProgress = true;

            if (!IsComplete)
            {
                // As long as this isn't an AI turn, process the requested move
                if (!((VsComputer) && (CurrentTurn == AI.getColor())) )
                {
                    if (GameBoard.MovePossible(CurrentTurn))
                    {
                        if (GameBoard.MakeMove(x, y, CurrentTurn))
                        {
                            SwitchTurn();
                        }
                    }
                    else
                    {
                        SwitchTurn();
                    }

                    ReversiForm.RefreshPieces();
                    ReversiForm.UpdateScoreBoard();
                }

                if ((VsComputer) && (CurrentTurn == AI.getColor()))
                    ReversiForm.StartAITurnWorker();
                else
                    TurnInProgress = false;

                DetermineWinner();
            }
        }

        public void ProcessAITurn()
        {
            while (GameBoard.MovePossible(AI.getColor()))
            {
                Point AIMove = AI.DetermineNextMove(this);
                GameBoard.MakeMove(AIMove.X, AIMove.Y, CurrentTurn);

                if (GameBoard.MovePossible(NextTurn))
                    break;
                else
                    ReversiForm.ReportAITurnWorkerProgress(0);
            }
        }

        public void SwitchTurn()
        {
            if (CurrentTurn == WHITE)
            {
                CurrentTurn = BLACK;
                NextTurn = WHITE;
            }
            else
            {
                CurrentTurn = WHITE;
                NextTurn = BLACK;
            }
            ReversiForm.UpdateTurnImage(CurrentTurn);
        }

        public string GetTurnString(int color)
        {
            if (color == WHITE)
                return ("White");
            else if (color == BLACK)
                return ("Black");
            else
                return ("Illegal Color!");
        }
    }

}