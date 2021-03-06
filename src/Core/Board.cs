/// <summary>
/// Reversi.Board.cs
/// Brian A. Hebert
/// </summary>

using System;
using System.Windows;
using System.Collections.Generic;

namespace Reversi
{
    /// <summary>
    /// Stores the game board and all of the methods to manipulate it
    /// </summary>
    public class Board
    {
        // The array of pieces that represents the board state
        private Piece[,] BoardPieces;

        // The size of the board, which will always be BoardSize x BoardSize
        private int BoardSize;

        /// <summary>
        /// The default constructor creates an 8x8 board
        /// </summary>
        public Board()
        {
            this.BoardSize = 8;
            BoardPieces = new Piece[BoardSize, BoardSize];
            ClearBoard();
            PlaceStartingPieces();
        }

        /// <summary>
        /// Create an NxN board (min size 4)
        /// </summary>
        /// <param name="SourceSize">The size of the board to create</param>
        public Board(int SourceSize)
        {
            BoardSize = Math.Max(4, SourceSize);
            BoardPieces = new Piece[BoardSize, BoardSize];
            ClearBoard();
            PlaceStartingPieces();
        }

        /// <summary>
        /// Create a board using another board as a template
        /// </summary>
        /// <param name="SourceBoard">The board to use as a template</param>
        public Board(Board SourceBoard)
        {
            BoardSize = SourceBoard.BoardSize;
            BoardPieces = new Piece[BoardSize, BoardSize];
            this.CopyBoard(SourceBoard);
        }

        #region Getters and Setters

        /// <summary>
        /// Returns the 2d list of game pieces
        /// </summary>
        public Piece[,] GetBoardPieces() { return BoardPieces; }

        /// <summary>
        /// Returns the board size
        /// </summary>
        public int GetBoardSize() { return BoardSize; }

        #endregion

        /// <summary>
        /// Returns true if the piece is within the bounds of the game board
        /// </summary>
        /// <param name="SourcePoint">The move to consider</param>
        /// <returns>True if the move is located within the game board</returns>
        public Boolean InBounds(Point SourcePoint)
        {
            return(InBounds( Convert.ToInt32(SourcePoint.X), Convert.ToInt32(SourcePoint.Y )));
        }

        /// <summary>
        /// Returns true if the piece is within the bounds of the game board
        /// </summary>
        /// <param name="X">The X value of the move</param>
        /// <param name="Y">The Y value of the move</param>
        /// <returns>True if the move is located within the game board</returns>
        public Boolean InBounds(int X, int Y)
        {
            if ((X > BoardSize - 1) || (Y > BoardSize - 1) || (X < 0) || (Y < 0))
                return false;
            else
                return true;
        }

        /// <summary>
        /// Copies the content of one board to another
        /// </summary>
        /// <param name="NewBoardPieces">The board array to use as a source</param>
        public void CopyBoard(Piece[,] NewBoardPieces)
        {
            Array.Copy(NewBoardPieces, BoardPieces, NewBoardPieces.Length);
        }

        /// <summary>
        /// Copies the content of one board to another
        /// </summary>
        /// <param name="NewBoardPieces">The board to use as a source</param>
        public void CopyBoard(Board SourceBoard)
        {
            CopyBoard(SourceBoard.BoardPieces);
        }

        /// <summary>
        /// Overrides the default ToString method to return a string representation of the board
        /// </summary>
        /// <returns>The string representation of the board</returns>
        public override String ToString()
        {
            return (BuildBoardString());
        }

        /// <summary>
        /// Overrides the default ToString method to return a string representation of the board
        /// </summary>
        /// <param name="SingleLine">(optional) True to output a single line (instead of multiline row format)</param>
        /// <returns>The string representation of the board</returns>
        public String BuildBoardString(Boolean SingleLine = false)
        {
            string boardString = "";

            for (int Y = 0; Y < BoardSize; Y++)
            {
                for (int X = 0; X < BoardSize; X++)
                    boardString += Convert.ToInt16(ColorAt(X, Y));

                if (!SingleLine)
                    boardString += "\n";
            }

            return boardString;
        }

        /// <summary>
        /// Returns the color at a given point
        /// </summary>
        /// <param name="SourcePoint">The point to consider</param>
        /// <returns>The color at the given point</returns>
        public Piece ColorAt(Point SourcePoint)
        {
            return( ColorAt( Convert.ToInt32(SourcePoint.X), Convert.ToInt32(SourcePoint.Y) ));
        }

        /// <summary>
        /// Returns the color at the given board location
        /// </summary>
        /// <param name="X">The X value of the move</param>
        /// <param name="Y">The Y value of the move</param>
        /// <returns>The color at the given point</returns>
        public Piece ColorAt(int X, int Y)
        {
            if( !InBounds( X, Y ))
                return Piece.ERROR;

            return(BoardPieces[X, Y]);
        }

        /// <summary>
        /// Attempts to process the implications of a legal move and updates the board if CommitMove = true
        /// </summary>
        /// <param name="SourceMove">The move to make</param>
        /// <param name="SourceTurn">The turn to use</param>
        /// <param name="CommitMove">True if the move should be committed to the board, false if this is just to see if a move is possible</param>
        /// <returns>True if a move is possible/was made, false if a move is not possible/was not made</returns>
        public Boolean MakeMove(Point SourceMove, Piece SourceTurn, Boolean CommitMove = true)
        {
            return (MakeMove(Convert.ToInt32(SourceMove.X), Convert.ToInt32(SourceMove.Y), SourceTurn, CommitMove));
        }

        /// <summary>
        /// Attempts to process the implications of a legal move and updates the board if CommitMove = true
        /// </summary>
        /// <param name="X">The X value of the move</param>
        /// <param name="Y">The Y value of the move</param>
        /// <param name="SourceTurn">The turn to use</param>
        /// <param name="CommitMove">True if the move should be committed to the board, false if this is just to see if a move is possible</param>
        /// <returns>True if a move is possible/was made, false if a move is not possible/was not made</returns>
        public Boolean MakeMove(int MoveX, int MoveY, Piece SourceTurn, Boolean CommitMove = true)
        {
            Piece CurrentTurn = SourceTurn;
            Piece NextTurn;

            if (CurrentTurn == Piece.WHITE)
                NextTurn = Piece.BLACK;
            else
                NextTurn = Piece.WHITE;

            // Check for already existing piece
            if (ColorAt(MoveX, MoveY) != Piece.EMPTY)
                return false;

            Boolean findStatus = false;
            Boolean takeStatus = false;

            // Search the board for pieces affected by this turn
            for (int Y = Math.Max(MoveY - 1, 0); Y <= Math.Min(MoveY + 1, BoardSize - 1); Y++)
            {
                for (int X = Math.Max(MoveX - 1, 0); X <= Math.Min(MoveX + 1, BoardSize - 1); X++)
                {
                    // If an enemy piece has been found, capture it and all connecting pieces
                    if (ColorAt(X, Y) == (NextTurn))
                    {
                        findStatus = true;

                        int CapturedX = X;
                        int CapturedY = Y;
                        int XDirection = X - MoveX;
                        int YDirection = Y - MoveY;

                        Board SimulationBoard = new Board(this);

                        // Continue to capture pieces until a friendly piece is found
                        while (SimulationBoard.ColorAt(CapturedX, CapturedY) == NextTurn)
                        {
                            SimulationBoard.PutPiece(CapturedX, CapturedY, CurrentTurn);
                            CapturedX += XDirection;
                            CapturedY += YDirection;

                            if (!SimulationBoard.InBounds(CapturedX, CapturedY))
                                break;
                        }

                        // Copy the simulation board to the real board (if the CommitMove flag is set)
                        if (SimulationBoard.InBounds(CapturedX, CapturedY))
                        {
                            if (SimulationBoard.ColorAt(CapturedX, CapturedY) == CurrentTurn)
                            {
                                if (CommitMove)
                                {
                                    SimulationBoard.PutPiece(MoveX, MoveY, SourceTurn);
                                    CopyBoard(SimulationBoard);
                                }

                                takeStatus = true;
                            }
                        }
                    }
                }
            }

            if ((!findStatus) && (CommitMove))
            {
                //gDebugText.Text = "You must place your piece adjacent to an opponents piece.";
            }
            if ((!takeStatus) && (CommitMove))
            {
                //gDebugText.Text = "You must place capture at least one piece on each turn.";
            }

            return takeStatus;
        }

        /// <summary>
        /// Returns a list of all available moves for a given player
        /// </summary>
        /// <param name="CurrentTurn">The turn to consider</param>
        /// <returns>The list of all available moves for a given player</returns>
        public Point[] AvailableMoves(Piece CurrentTurn)
        {
            Point[] Moves = new Point[64];
            int foundMoves = 0;
            for (int Y = 0; Y < BoardSize; Y++)
                for (int X = 0; X < BoardSize; X++)
                    if (ColorAt(X, Y) == Piece.EMPTY)
                        if (MakeMove(X, Y, CurrentTurn, CommitMove: false))
                        {
                            Moves[foundMoves] = new Point(X, Y);
                            foundMoves++;
                        }

            Point[] FinalMoves = new Point[foundMoves];
            for (int index = 0; index < FinalMoves.Length; index++)
                FinalMoves[index] = Moves[index];

            return(FinalMoves);
        }

        /// <summary>
        /// Tests to see if there is a legal move possible for EITHER player
        /// </summary>
        /// <returns>True if either player can make a legal move</returns>
        public bool MovePossible()
        {
            if ((MovePossible(Piece.BLACK)) || (MovePossible(Piece.WHITE)))
                return (true);

            return (false);
        }

        /// <summary>
        /// Tests to see if there is a legal move possible for given player
        /// </summary>
        /// <param name="Turn">The turn to consider</param>
        /// <returns>True if the given player can make a legal move</returns>
        public bool MovePossible(Piece Turn)
        {
            // Loop through all spots on the board
            for (int Y = 0; Y < BoardSize; Y++)
                for (int X = 0; X < BoardSize; X++)
                    // Simulate a move, if successful, we know that there is at least this legal move
                    if (ColorAt(X, Y) == Piece.EMPTY)
                        if (MakeMove(X, Y, Turn, CommitMove: false))
                            return true;

            // No legal moves were found
            return false;
        }

        /// <summary>
        /// Places a piece at the given location
        /// </summary>
        /// <param name="Move">The move to place</param>
        /// <param name="Turn">The move turn</param>
        public void PutPiece(Point Move, Piece Turn)
        {
            PutPiece(Convert.ToInt32(Move.X), Convert.ToInt32(Move.Y), Turn);
        }

        /// <summary>
        /// Places a piece at the given location
        /// </summary>
        /// <param name="X">The X value of the move</param>
        /// <param name="Y">The Y value of the move</param>
        /// <param name="Turn">The move turn</param>
        public void PutPiece(int X, int Y, Piece Turn)
        {
            if ((Turn == Piece.WHITE) || (Turn == Piece.BLACK))
                BoardPieces[X, Y] = Turn;
            else
                BoardPieces[X, Y] = Piece.EMPTY;
        }

        /// <summary>
        /// Empites the game board, resetting all of the pieces
        /// </summary>
        public void ClearBoard()
        {
            Array.Clear(BoardPieces, 0, BoardSize * BoardSize);
        }

        /// <summary>
        /// Initialize the game board with the starting pieces
        /// </summary>
        public void PlaceStartingPieces()
        {
            if (BoardSize == 8)
            {
                PutPiece(3, 3, Piece.WHITE);
                PutPiece(4, 4, Piece.WHITE);
                PutPiece(3, 4, Piece.BLACK);
                PutPiece(4, 3, Piece.BLACK);
            }
            else if (BoardSize == 6)
            {
                PutPiece(2, 2, Piece.WHITE);
                PutPiece(3, 3, Piece.WHITE);
                PutPiece(2, 3, Piece.BLACK);
                PutPiece(3, 2, Piece.BLACK);
            }
            else
            {
                PutPiece(1, 1, Piece.WHITE);
                PutPiece(2, 2, Piece.WHITE);
                PutPiece(1, 2, Piece.BLACK);
                PutPiece(2, 1, Piece.BLACK);
            }
        }

        /// <summary>
        /// Return the score of the given player
        /// </summary>
        /// <param name="Turn">The turn to score</param>
        /// <returns>The score for the given turn</returns>
        public int CalculateScore(Piece Turn)
        {
            int Score = 0;

            for (int Y = 0; Y < BoardSize; Y++)
                for (int X = 0; X < BoardSize; X++)
                    if (ColorAt(X, Y) == Turn)
                        Score++;

            return Score;
        }

        /// <summary>
        /// Determines if there is a winner in the current game
        /// </summary>
        /// <returns>The color of the winning player</returns>
        public Piece DetermineWinner()
        {
            if (!MovePossible())
            {
                int WhiteScore = CalculateScore(Piece.WHITE);
                int BlackScore = CalculateScore(Piece.BLACK);

                // Black Wins
                if (BlackScore > WhiteScore)
                    return (Piece.BLACK);
                // White Wins
                else if (BlackScore < WhiteScore)
                    return (Piece.WHITE);
                // Tie Game
                else
                    return (Piece.EMPTY);
            }

            // No victory
            return (Piece.ERROR);
        }

        /// <summary>
        /// Returns all of the moves around a given point
        /// </summary>
        /// <param name="SourcePoint">The point on the board to check</param>
        /// <returns>A list of all moves surrounding the given point</returns>
        public List<Point> MovesAround(Point SourcePoint)
        {
            List<Point> MovesFound = new List<Point>();

            if (SourcePoint.X > 0)
            {
                if (SourcePoint.X > 0) 
                {
                    MovesFound.Add(new Point(SourcePoint.X - 1, SourcePoint.Y));           // middle left (4)

                    if (SourcePoint.Y < BoardSize - 1)
                        MovesFound.Add(new Point(SourcePoint.X - 1, SourcePoint.Y + 1));   // bottom left (7)

                    if (SourcePoint.Y > 0)
                        MovesFound.Add(new Point(SourcePoint.X - 1, SourcePoint.Y - 1));   // top left (1)
                }
            }

            if (SourcePoint.X < BoardSize - 1) 
            {
                MovesFound.Add(new Point(SourcePoint.X + 1, SourcePoint.Y));               // middle right (6)

                if (SourcePoint.Y < BoardSize - 1)
                    MovesFound.Add(new Point(SourcePoint.X + 1, SourcePoint.Y + 1));       // bottom right (9)

                if (SourcePoint.Y > 0)
                    MovesFound.Add(new Point(SourcePoint.X + 1, SourcePoint.Y - 1));       // top right (3)
            }

            if (SourcePoint.Y < BoardSize - 1)
                MovesFound.Add(new Point(SourcePoint.X, SourcePoint.Y + 1));               // bottom middle (8)

            if (SourcePoint.Y > 0)
                MovesFound.Add(new Point(SourcePoint.X, SourcePoint.Y - 1));               // top middle (2)

            return (MovesFound);
        }
    }
}