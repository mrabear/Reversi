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
            CurrentTurn = Properties.Settings.Default.WHITE;
            NextTurn = Properties.Settings.Default.BLACK;
            Difficulty = ReversiForm.getAIDifficulty();
            VsComputer = ReversiForm.getPvC();
            GameBoard = new Board(BoardSize);
            IsComplete = false;
            AI = new AI(Properties.Settings.Default.BLACK);

            // Reset the board image to clear any pieces from previous games
            ReversiForm.ResetBoardImage();

            // Reset the board that tracks which pieces have been drawn on the screen
            ReversiForm.setLastDrawnBoard( new Board(BoardSize) );
            ReversiForm.getLastDrawnBoard().ClearBoard();

            ReversiForm.RefreshPieces( GameBoard );
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
            int WhiteScore = ReversiForm.getCurrentGame().GameBoard.FindScore(Properties.Settings.Default.WHITE);
            int BlackScore = ReversiForm.getCurrentGame().GameBoard.FindScore(Properties.Settings.Default.BLACK);

            if (WhiteScore == 0)
            {
                ReversiForm.getCurrentGame().IsComplete = true;
                ReversiForm.getCurrentGame().Winner = Properties.Settings.Default.BLACK;
            }
            else if (BlackScore == 0)
            {
                ReversiForm.getCurrentGame().IsComplete = true;
                ReversiForm.getCurrentGame().Winner = Properties.Settings.Default.WHITE;
            }
            else if (((WhiteScore + BlackScore) == 64) ||
                ((!ReversiForm.getCurrentGame().GameBoard.MovePossible(ReversiForm.getCurrentGame().CurrentTurn)) && (!ReversiForm.getCurrentGame().GameBoard.MovePossible(ReversiForm.getCurrentGame().NextTurn))))
            {
                ReversiForm.getCurrentGame().IsComplete = true;
                if (BlackScore > WhiteScore)
                    ReversiForm.getCurrentGame().Winner = Properties.Settings.Default.BLACK;
                else if (BlackScore < WhiteScore)
                    ReversiForm.getCurrentGame().Winner = Properties.Settings.Default.WHITE;
                else
                    ReversiForm.getCurrentGame().Winner = Properties.Settings.Default.EMPTY;
            }

            if (IsComplete)
                ReversiForm.ShowWinner(Winner);

            return (ReversiForm.getCurrentGame().IsComplete);
        }

        // Processes a single turn of gameplay, two if it is vs. AI
        public void ProcessTurn(int x, int y)
        {
            TurnInProgress = true;

            if (!IsComplete)
            {
                // As long as this isn't an AI turn, process the requested move
                if (!((VsComputer) && (CurrentTurn == AI.getColor())))
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

                    ReversiForm.RefreshPieces( GameBoard );
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

                if (GameBoard.MovePossible(ReversiForm.getCurrentGame().NextTurn))
                    break;
                else
                    ReversiForm.ReportAITurnWorkerProgress(0);
            }
        }

        public void SwitchTurn()
        {
            if (CurrentTurn == Properties.Settings.Default.WHITE)
            {
                CurrentTurn = Properties.Settings.Default.BLACK;
                NextTurn = Properties.Settings.Default.WHITE;
            }
            else
            {
                CurrentTurn = Properties.Settings.Default.WHITE;
                NextTurn = Properties.Settings.Default.BLACK;
            }
            ReversiForm.UpdateTurnImage(CurrentTurn);
        }

        public string GetTurnString(int color)
        {
            if (color == Properties.Settings.Default.WHITE)
                return ("White");
            else if (color == Properties.Settings.Default.BLACK)
                return ("Black");
            else
                return ("Illegal Color!");
        }
    }
}