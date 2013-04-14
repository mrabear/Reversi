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

        static void Main()
        {
            MainForm = new ReversiForm();
            Application.Run(MainForm);
        }
    }
}