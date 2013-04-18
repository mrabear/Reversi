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
        private Boolean IsComplete = false;
        private Boolean ProcessMoves = true;
        private Boolean TurnInProgress = false;
        private AI AI;

        public Game(int BoardSize = 8)
        {
            CurrentTurn = ReversiApplication.WHITE;
            NextTurn = ReversiApplication.BLACK;
            Difficulty = ReversiForm.getAIDifficulty();
            VsComputer = ReversiForm.isVsComputer();
            GameBoard = new Board(BoardSize);
            IsComplete = false;
            AI = new AI(ReversiApplication.BLACK);

            // Reset the board image to clear any pieces from previous games
            ReversiForm.ClearBoardPieces();

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

                    ReversiForm.RefreshPieces( FullRefresh: true );
                    ReversiForm.UpdateScoreBoard();
                }

                if ((VsComputer) && (CurrentTurn == AI.getColor()))
                    ReversiForm.StartAITurnWorker();
                else
                    TurnInProgress = false;

                ReversiForm.ShowWinner( GameBoard.DetermineWinner() );
            }
        }

        public void ProcessAITurn()
        {
            while (GameBoard.MovePossible(AI.getColor()))
            {
                AI.MakeNextMove(this);

                if (GameBoard.MovePossible(AI.getColor() == ReversiApplication.BLACK ? ReversiApplication.WHITE : ReversiApplication.BLACK))
                    break;
                else
                    ReversiForm.RefreshPieces(FullRefresh: true);
            }
        }

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