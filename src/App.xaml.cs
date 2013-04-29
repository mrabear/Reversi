using System;
using System.Windows;

namespace Reversi
{
    /// <summary>
    /// The Reversi Application main entry point.
    /// </summary>
    public partial class App : Application
    {
        // The currently active game instance
        private static Game ActiveGame;

        // The currently active computer player
        private static ComputerPlayer ComputerPlayer;

        // The currently active game board
        private static Board ActiveGameBoard;

        /// <summary>
        /// Resets the global application game instance
        /// </summary>
        /// <param name="BoardSize">The size of the board to use in the new game</param>
        public static void ResetActiveGame(int BoardSize = 8) { ActiveGame = new Game(BoardSize); }

        /// <summary>
        /// Gets the active game instance
        /// </summary>
        public static Game GetActiveGame() { return (ActiveGame); }

        /// <summary>
        /// Gets the active game board
        /// </summary>
        public static Board GetActiveGameBoard() { return (ActiveGameBoard); }

        /// <summary>
        /// Resets the active game board
        /// </summary>
        /// <param name="BoardSize">The size of the board to use in the new game</param>
        public static void ResetActiveGameBoard(int BoardSize = 8) { ActiveGameBoard = new Board(BoardSize); }

        /// <summary>
        /// Gets the active computer player
        /// </summary>
        public static ComputerPlayer GetComputerPlayer() { return (ComputerPlayer); }

        /// <summary>
        /// Resets the active computer player
        /// </summary>
        /// <param name="PlayerColor">The size of the board to use in the new game</param>
        public static void ResetComputerPlayer(Piece PlayerColor = Piece.BLACK) { ComputerPlayer = new ComputerPlayer(PlayerColor); }
    }
}
