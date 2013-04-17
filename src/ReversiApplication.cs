// Reversi
// Brian Hebert
//

using System;
using System.Windows.Forms;

namespace Reversi
{
    // The application entry point
    public static class ReversiApplication
    {
        // Color constants
        public static int WHITE = Properties.Settings.Default.WHITE;
        public static int BLACK = Properties.Settings.Default.BLACK;
        public static int EMPTY = Properties.Settings.Default.EMPTY;

        private static ReversiForm MainForm;
        private static Game CurrentGame;

        public static Game getCurrentGame() { return CurrentGame; }
        public static void resetCurrentGame(int BoardSize = 8) { CurrentGame = new Game(BoardSize); }

        static void Main()
        {
            MainForm = new ReversiForm();
            Application.Run(MainForm);
        }
    }
}