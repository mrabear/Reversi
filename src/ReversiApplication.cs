/// <summary>
/// Reversi.ReversiApplication.cs
/// </summary>

using System;
using System.Windows.Forms;

namespace Reversi
{
    /// <summary>
    /// The main application class, stores the form and game global objects
    /// </summary>
    public static class ReversiApplication
    {
        // Color constants
        public static int WHITE = Properties.Settings.Default.WHITE;
        public static int BLACK = Properties.Settings.Default.BLACK;
        public static int EMPTY = Properties.Settings.Default.EMPTY;
        public static int ERROR = Properties.Settings.Default.ERROR;

        private static ReversiForm MainForm;
        private static Game CurrentGame;

        /// <summary>
        /// Returns the global application game instance
        /// </summary>
        /// <returns>The current application game instance</returns>
        public static Game GetCurrentGame() { return CurrentGame; }

        /// <summary>
        /// Resets the global application game instance
        /// </summary>
        /// <param name="BoardSize">The size of the board to use in the new game</param>
        public static void ResetCurrentGame(int BoardSize = 8) { CurrentGame = new Game(BoardSize); }

        /// <summary>
        /// The application entry point, starts a new form instance
        /// </summary>
        static void Main()
        {
            MainForm = new ReversiForm();
            Application.Run(MainForm);
        }
    }
}