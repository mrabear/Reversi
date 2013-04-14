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