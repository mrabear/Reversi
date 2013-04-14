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
            Difficulty = ReversiForm.AIDifficulty;
            VsComputer = ReversiForm.PvC;
            GameBoard = new Board(BoardSize);
            IsComplete = false;
            AI = new AI(BLACK);

            // Reset the board image to clear any pieces from previous games
            ReversiForm.gBoardGFX.DrawImage(ReversiForm.BoardImage, 0, 0, ReversiForm.BoardImage.Width, ReversiForm.BoardImage.Height);

            // Reset the board that tracks which pieces have been drawn on the screen
            ReversiForm.LastDrawnBoard = new Board(BoardSize);
            ReversiForm.LastDrawnBoard.ClearBoard();

            GameBoard.RefreshPieces();
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
            int WhiteScore = ReversiForm.CurrentGame.GameBoard.FindScore(WHITE);
            int BlackScore = ReversiForm.CurrentGame.GameBoard.FindScore(BLACK);

            if (WhiteScore == 0)
            {
                ReversiForm.CurrentGame.IsComplete = true;
                ReversiForm.CurrentGame.Winner = BLACK;
            }
            else if (BlackScore == 0)
            {
                ReversiForm.CurrentGame.IsComplete = true;
                ReversiForm.CurrentGame.Winner = WHITE;
            }
            else if (((WhiteScore + BlackScore) == 64) ||
                ((!ReversiForm.CurrentGame.GameBoard.MovePossible(ReversiForm.CurrentGame.CurrentTurn)) && (!ReversiForm.CurrentGame.GameBoard.MovePossible(ReversiForm.CurrentGame.NextTurn))))
            {
                ReversiForm.CurrentGame.IsComplete = true;
                if (BlackScore > WhiteScore)
                    ReversiForm.CurrentGame.Winner = BLACK;
                else if (BlackScore < WhiteScore)
                    ReversiForm.CurrentGame.Winner = WHITE;
                else
                    ReversiForm.CurrentGame.Winner = EMPTY;
            }

            if (IsComplete)
            {
                if (Winner == EMPTY)
                {
                    ReversiForm.gCurrentTurnLabel.Text = "Tie";
                    ReversiForm.gCurrentTurnImage.Visible = false;
                }
                else
                {
                    ReversiForm.gCurrentTurnLabel.Text = "Winner";
                    UpdateTurnImage(Winner);
                }
            }

            return (ReversiForm.CurrentGame.IsComplete);
        }

        public void UpdateScoreBoard()
        {
            ReversiForm.gBlackScoreBoard.Text = GameBoard.FindScore(BLACK).ToString();
            ReversiForm.gWhiteScoreBoard.Text = GameBoard.FindScore(WHITE).ToString();
            ReversiForm.gBlackScoreBoard.Refresh();
            ReversiForm.gWhiteScoreBoard.Refresh();
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

                    GameBoard.RefreshPieces();
                    UpdateScoreBoard();
                }

                if ((VsComputer) && (CurrentTurn == AI.getColor()))
                    ReversiForm.gAITurnWorker.RunWorkerAsync();
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

                if (GameBoard.MovePossible(ReversiForm.CurrentGame.NextTurn))
                    break;
                else
                    ReversiForm.gAITurnWorker.ReportProgress(0);
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
            UpdateTurnImage(CurrentTurn);
        }

        public void UpdateTurnImage(int turn)
        {
            if (turn == WHITE)
                ReversiForm.gCurrentTurnImage.CreateGraphics().DrawImage(ReversiForm.WhitePieceImage, 0, 0, ReversiForm.WhitePieceImage.Width, ReversiForm.WhitePieceImage.Height);
            else
                ReversiForm.gCurrentTurnImage.CreateGraphics().DrawImage(ReversiForm.BlackPieceImage, 0, 0, ReversiForm.BlackPieceImage.Width, ReversiForm.BlackPieceImage.Height);
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