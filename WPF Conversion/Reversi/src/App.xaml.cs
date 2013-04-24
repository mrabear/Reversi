using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace Reversi
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static int WHITE = Reversi.Properties.Settings.Default.WHITE;
        public static int BLACK = Reversi.Properties.Settings.Default.BLACK;
        public static int EMPTY = Reversi.Properties.Settings.Default.EMPTY;
        public static int ERROR = Reversi.Properties.Settings.Default.ERROR;

        private static Game CurrentGame;
        private static AI ComputerPlayer;
        private static Board ActiveGameBoard;

        /// <summary>
        /// Resets the global application game instance
        /// </summary>
        /// <param name="BoardSize">The size of the board to use in the new game</param>
        public static void ResetCurrentGame(int BoardSize = 8) { CurrentGame = new Game(BoardSize); }
        public static Game GetCurrentGame() { return( CurrentGame ); }

        public static void ResetActiveGameBoard(int BoardSize = 8) { ActiveGameBoard = new Board(BoardSize); }
        public static Board GetActiveGameBoard() { return (ActiveGameBoard); }

        public static AI GetComputerPlayer() { return (ComputerPlayer); }
        public static void ResetComputerPlayer(int PlayerColor = 2) { ComputerPlayer = new AI(PlayerColor); }
    }
}
