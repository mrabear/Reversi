// Reversi
// Brian Hebert
//

using System;
using System.Drawing;

namespace Reversi
{
    // Class:       Board
    // Description: Stores the game board and all of the methods to manipulate it
    public class Board
    {
        // The array of pieces that represents the board state
        private int[,] BoardPieces;

        // The size of the board, which will always be BoardSize x BoardSize
        private int BoardSize;

        // The default constructor creates an 8x8 board
        public Board()
        {
            this.BoardSize = 8;
            BoardPieces = new int[BoardSize, BoardSize];
            ClearBoard();
            PlaceStartingPieces();
        }

        // Create an NxN board (min size 4)
        public Board(int SourceSize)
        {
            BoardSize = Math.Max(4, SourceSize);
            BoardPieces = new int[BoardSize, BoardSize];
            ClearBoard();
            PlaceStartingPieces();
        }

        // Create a board using another board as a template
        public Board(Board SourceBoard)
        {
            BoardSize = SourceBoard.BoardSize;
            BoardPieces = new int[BoardSize, BoardSize];
            this.CopyBoard(SourceBoard);
        }

        #region Getters and Setters

        public int[,] getBoardPieces() { return BoardPieces; }
        public int getBoardSize() { return BoardSize; }

        #endregion

        // Returns true if the piece is within the bounds of the game board
        public Boolean InBounds(int x, int y)
        {
            if ((x > BoardSize - 1) || (y > BoardSize - 1) || (x < 0) || (y < 0))
                return false;
            else
                return true;
        }

        // Copies the content of one board to another
        public void CopyBoard(int[,] NewBoardPieces)
        {
            Array.Copy(NewBoardPieces, BoardPieces, NewBoardPieces.Length);
        }

        // Overload of copyboard that takes a source board as input
        public void CopyBoard(Board SourceBoard)
        {
            CopyBoard(SourceBoard.BoardPieces);
        }

        // Overrides the default ToString method to return a string representation of the board
        public override String ToString()
        {
            return (BuildBoardString());
        }

        // Returns a string representation of the board
        public String BuildBoardString(Boolean SingleLine = false)
        {
            string boardString = "";

            for (int Y = 0; Y < BoardSize; Y++)
            {
                for (int X = 0; X < BoardSize; X++)
                {
                    boardString += ColorAt(X, Y);
                }

                if (!SingleLine)
                    boardString += "\n";
            }

            return boardString;
        }

        // Returns a unique identifier for a specific board state and turn
        public String GetID(int CurrentTurn)
        {
            return (CurrentTurn + this.GetID());
        }

        // Returns a unique identifier for a specific board state irrespective of turn
        public String GetID()
        {
            return (BuildBoardString(true));
        }

        // Returns the color at the given board location
        public int ColorAt(int x, int y)
        {
            if ((x < 0) || (x > BoardSize - 1) || (y < 0) || (y > BoardSize - 1))
                return Properties.Settings.Default.EMPTY;

            return (BoardPieces[x, y]);
        }

        // Attempts to process the implications of a legal move and updates the board if ProcessMove = true
        public Boolean MakeMove(int x, int y, int color, Boolean ProcessMove = true)
        {
            int CurrentTurn = color;
            int NextTurn;

            if (CurrentTurn == Properties.Settings.Default.WHITE)
                NextTurn = Properties.Settings.Default.BLACK;
            else
                NextTurn = Properties.Settings.Default.WHITE;

            // Check for already existing piece
            if (ColorAt(x, y) != Properties.Settings.Default.EMPTY)
                return false;

            Boolean findStatus = false;
            Boolean takeStatus = false;

            for (int olc = Math.Max(y - 1, 0); olc <= Math.Min(y + 1, BoardSize - 1); olc++)
            {
                for (int ilc = Math.Max(x - 1, 0); ilc <= Math.Min(x + 1, BoardSize - 1); ilc++)
                {
                    if (ColorAt(ilc, olc) == (NextTurn))
                    {
                        findStatus = true;

                        int newX = ilc;
                        int newY = olc;
                        int dirX = ilc - x;
                        int dirY = olc - y;

                        Board TempBoard = new Board(this);

                        while (TempBoard.ColorAt(newX, newY) == NextTurn)
                        {
                            TempBoard.PutPiece(newX, newY, CurrentTurn);
                            newX += dirX;
                            newY += dirY;

                            if (!TempBoard.InBounds(newX, newY))
                                break;
                        }

                        if (TempBoard.InBounds(newX, newY))
                        {
                            if (TempBoard.ColorAt(newX, newY) == CurrentTurn)
                            {
                                if (ProcessMove)
                                {
                                    TempBoard.PutPiece(x, y, color);
                                    CopyBoard(TempBoard);
                                }

                                takeStatus = true;
                            }
                        }
                    }
                }
            }

            if ((!findStatus) && (ProcessMove))
            {
                //gDebugText.Text = "You must place your piece adjacent to an opponents piece.";
            }
            if ((!takeStatus) && (ProcessMove))
            {
                //gDebugText.Text = "You must place capture at least one piece on each turn.";
            }

            return takeStatus;
        }

        // Returns a list of all available moves for a given player
        public Point[] AvailableMoves(int CurrentTurn)
        {
            Point[] Moves = new Point[64];
            int foundMoves = 0;
            for (int Y = 0; Y < BoardSize; Y++)
                for (int X = 0; X < BoardSize; X++)
                    if (ColorAt(X, Y) == Properties.Settings.Default.EMPTY)
                        if (MakeMove(X, Y, CurrentTurn, false))
                        {
                            Moves[foundMoves] = new Point(X, Y);
                            foundMoves++;
                        }

            Point[] FinalMoves = new Point[foundMoves];
            for (int index = 0; index < FinalMoves.Length; index++)
                FinalMoves[index] = Moves[index];

            return FinalMoves;
        }

        // Returns true if a move is possible for the given player
        public Boolean MovePossible(int color)
        {
            for (int Y = 0; Y < BoardSize; Y++)
                for (int X = 0; X < BoardSize; X++)
                    if (ColorAt(X, Y) == Properties.Settings.Default.EMPTY)
                        if (MakeMove(X, Y, color, false))
                            return true;

            return false;
        }

        // Places a piece at the given location
        public void PutPiece(int x, int y, int color)
        {
            if ((color == Properties.Settings.Default.WHITE) || (color == Properties.Settings.Default.BLACK))
                BoardPieces[x, y] = color;
            else
                BoardPieces[x, y] = Properties.Settings.Default.EMPTY;
        }

        // Empty the board
        public void ClearBoard()
        {
            Array.Clear(BoardPieces, 0, BoardSize * BoardSize);
        }

        // Initialize the game board with the starting pieces
        public void PlaceStartingPieces()
        {
            if (BoardSize == 8)
            {
                PutPiece(3, 3, Properties.Settings.Default.WHITE);
                PutPiece(4, 4, Properties.Settings.Default.WHITE);
                PutPiece(3, 4, Properties.Settings.Default.BLACK);
                PutPiece(4, 3, Properties.Settings.Default.BLACK);
            }
            else if (BoardSize == 6)
            {
                PutPiece(2, 2, Properties.Settings.Default.WHITE);
                PutPiece(3, 3, Properties.Settings.Default.WHITE);
                PutPiece(2, 3, Properties.Settings.Default.BLACK);
                PutPiece(3, 2, Properties.Settings.Default.BLACK);
            }
            else
            {
                PutPiece(1, 1, Properties.Settings.Default.WHITE);
                PutPiece(2, 2, Properties.Settings.Default.WHITE);
                PutPiece(1, 2, Properties.Settings.Default.BLACK);
                PutPiece(2, 1, Properties.Settings.Default.BLACK);
            }
        }

        // Return the score of the given player
        public int FindScore(int colorToCheck)
        {
            int score = 0;
            for (int Y = 0; Y < BoardSize; Y++)
                for (int X = 0; X < BoardSize; X++)
                    if (ColorAt(X, Y) == colorToCheck)
                        score++;

            return score;
        }

    }
}