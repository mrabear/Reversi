using System;

namespace Reversi
{
    /// <summary>
    /// A game piece / player turn
    /// </summary>
    public enum Piece : byte
    {
        ERROR = 9,  // Should never happen, indicates an invalid state
        EMPTY = 0,  // An empty spot
        WHITE = 1,  // A white turn/piece
        BLACK = 2   // A black turn/piece
    }
}
