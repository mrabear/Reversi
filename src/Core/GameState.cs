using System;

namespace Reversi
{
    /// <summary>
    /// Represents a snapshot of a single game state
    /// </summary>
    class GameState
    {
        public Board BoardState;
        public Piece TurnState;

        public GameState(Board SourceBoard, Piece SourceTurn)
        {
            BoardState = new Board(SourceBoard);
            TurnState = SourceTurn;
        }
    }
}
