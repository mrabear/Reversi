/// <summary>
/// Reversi.ReversiForm.GraphicsUtil.cs
/// </summary>

using System;
using System.Drawing;
using System.Windows.Forms;
using System.Management;

namespace Reversi
{
    /// <summary>
    /// A subclass of ReversiForm, used to manipulate the game board and other graphics assets
    /// </summary>
    public class GraphicsUtil : ReversiForm
    {

        /// <summary>
        /// Repaints all game graphics surfaces
        /// </summary>
        public static void RefreshAll()
        {
            gScoreBoardSurface.Invalidate();
            gGameBoardSurface.Invalidate();
        }

        /// <summary>
        /// Updates the score board for both players
        /// </summary>
        public static void UpdateScoreBoard()
        {
            gScoreBoardSurface.Invalidate();
        }

        /// <summary>
        /// Updates the score board for both players
        /// </summary>
        public static void UpdateScoreBoard( int Turn )
        {
            if (Turn == ReversiApplication.WHITE)
                gScoreBoardGFX.DrawImage(Reversi.Properties.Resources.ScoreBoard_WhiteTurn, 0, 0);
            else
                gScoreBoardGFX.DrawImage(Reversi.Properties.Resources.ScoreBoard_BlackTurn, 0, 0);

            StringFormat sf = new StringFormat();
            sf.LineAlignment = StringAlignment.Center;
            sf.Alignment = StringAlignment.Center;

            gScoreBoardGFX.DrawString(gCurrentGame.GetGameBoard().FindScore(ReversiApplication.BLACK).ToString(), new Font("Segoe UI", (float)30, FontStyle.Regular), Brushes.White, new RectangleF(51, 47, 100, 28), sf);
            gScoreBoardGFX.DrawString(gCurrentGame.GetGameBoard().FindScore(ReversiApplication.WHITE).ToString(), new Font("Segoe UI", (float)30, FontStyle.Regular), Brushes.Black, new RectangleF(189, 47, 100, 28), sf);
        }

        /// <summary>
        /// Draw all game pieces on the board
        /// </summary>
        /// <param name="FullRefresh">(optional) True forces a full refresh of all pieces</param>
        public static void RefreshPieces(bool FullRefresh = false)
        {
            RefreshPieces(gCurrentGame.GetGameBoard(), FullRefresh);
        }

        /// <summary>
        /// Draw all game pieces on the board
        /// </summary>
        /// <param name="SourceBoard">The board to draw onto the screen</param>
        /// <param name="FullRefresh">(optional) True forces a full refresh of all pieces</param>
        public static void RefreshPieces(Board SourceBoard, bool FullRefresh = false)
        {
            if (FullRefresh)
                RedrawBoardImage();

            for (int Y = 0; Y < SourceBoard.GetBoardSize(); Y++)
                for (int X = 0; X < SourceBoard.GetBoardSize(); X++)
                    if ((GetLastDrawnBoard().ColorAt(X, Y) != SourceBoard.ColorAt(X, Y)) || (FullRefresh))
                        DrawPiece(X, Y, SourceBoard.ColorAt(X, Y));

            GetLastDrawnBoard().CopyBoard(SourceBoard.GetBoardPieces());
        }

        /// <summary>
        /// Draw a single game pieces on the board
        /// </summary>
        /// <param name="Piece">The piece to place</param>
        /// <param name="color">The piece color</param>
        public static void DrawPiece(Point Piece, int Color)
        {
            DrawPiece(Piece.X, Piece.Y, Color);
        }

        /// <summary>
        /// Draw a single game pieces on the board
        /// </summary>
        /// <param name="X">The X value of the piece</param>
        /// <param name="Y">The Y value of the piece</param>
        /// <param name="color">The piece color</param>
        public static void DrawPiece(int X, int Y, int Color)
        {
            if ((Color == ReversiApplication.WHITE) || (Color == ReversiApplication.BLACK))
                gGameBoardBackBufferGFX.DrawImage(GetPieceImage(Color), X * BoardGridSize, Y * BoardGridSize, BoardGridSize, BoardGridSize);
        }

        /// <summary>
        /// Redraws the board image, erasing all pieces
        /// </summary>
        public static void RedrawBoardImage()
        {
            gGameBoardBackBufferGFX.DrawImage(Reversi.Properties.Resources.GameBoard, 0, 0, gGameBoardSurface.Width, gGameBoardSurface.Height);
        }

        public static void PromoteBackBuffer()
        {
            gGameBoardSurface.CreateGraphics().DrawImage(gGameBoardBackBuffer, 0, 0);
        }

        /// <summary>
        /// Marks all of the available moves for the given turn on the current game board
        /// </summary>
        /// <param name="Turn">The turn to use</param>
        public static void MarkAvailableMoves(int Turn)
        {
            MarkAvailableMoves(gCurrentGame.GetGameBoard(), Turn);
        }

        /// <summary>
        /// Marks all of the available moves for the given turn on the given game board
        /// </summary>
        /// <param name="SourceBoard">The game board</param>
        /// <param name="Turn">The turn to use</param>
        public static void MarkAvailableMoves(Board SourceBoard, int Turn)
        {
            // Only display if the 'Show Available Moves' box is checked
            if ((gShowAvailableMoves.Checked) && (gCurrentGame.GetCurrentTurn() != gCurrentGame.GetAI().GetColor()))
                // Loop through all available moves and place a dot at the location
                foreach (Point CurrentPiece in SourceBoard.AvailableMoves(Turn))
                    gGameBoardBackBufferGFX.DrawImage(Reversi.Properties.Resources.SuggestedPiece, CurrentPiece.X * BoardGridSize, CurrentPiece.Y * BoardGridSize, BoardGridSize, BoardGridSize);
        }

        /// <summary>
        /// Places a highlight circle at the given locations
        /// </summary>
        /// <param name="PieceList">The list of pieces</param>
        /// <param name="PieceColor">The highlight color</param>
        public static void HighlightPiece(Point[] PieceList, Color PieceColor)
        {
            foreach (Point CurrentPiece in PieceList)
                HighlightPiece(CurrentPiece.X, CurrentPiece.Y, PieceColor);
        }

        /// <summary>
        /// Places a highlight circle at the given location
        /// </summary>
        /// <param name="Piece">The piece to highlight</param>
        /// <param name="PieceColor">The highlight color</param>
        /// <param name="PieceLabel">(optional) Text to place in the center of the spot</param>
        public static void HighlightPiece(Point Piece, Color PieceColor, String PieceLabel = "")
        {
            HighlightPiece(Piece.X, Piece.Y, PieceColor, PieceLabel);
        }

        /// <summary>
        /// Places a highlight circle at the given location
        /// </summary>
        /// <param name="X">The X value of the piece to highlight</param>
        /// <param name="Y">The Y value of the piece to highlight</param>
        /// <param name="PieceColor">The highlight color</param>
        /// <param name="PieceLabel">(optional) Text to place in the center of the spot</param>
        public static void HighlightPiece(int X, int Y, Color PieceColor, String PieceLabel = "")
        {
            gGameBoardBackBufferGFX.DrawEllipse(new Pen(PieceColor, 4), X * BoardGridSize + 24, Y * BoardGridSize + 24, 30, 30);

            StringFormat sf = new StringFormat();
            sf.LineAlignment = StringAlignment.Center;
            sf.Alignment = StringAlignment.Center;

            if (PieceLabel != "")
                gGameBoardBackBufferGFX.DrawString(PieceLabel, new Font("Segoe UI", (float)9, FontStyle.Regular), Brushes.White, new RectangleF(X * BoardGridSize + 5, (Y + 1) * BoardGridSize - 35, BoardGridSize - 10, BoardGridSize - 28), sf);
        }

        /// <summary>
        /// Returns the correct image for a given turn
        /// </summary>
        /// <param name="Turn">The turn</param>
        /// <returns>Image for the given turn</returns>
        public static Image GetPieceImage(int Turn)
        {
            if (Turn == ReversiApplication.WHITE)
                return (Reversi.Properties.Resources.WhitePiece);

            return (Reversi.Properties.Resources.BlackPiece);
        }

        /// <summary>
        /// Updates the "Current Turn" image to indicate a winner (or tie)
        /// </summary>
        /// <param name="WinningTurn">The winning turn</param>
        public static void ShowWinner(int WinningTurn)
        {
            if ((WinningTurn == ReversiApplication.BLACK) || (WinningTurn == ReversiApplication.WHITE))
                //UpdateScoreBoard(WinningTurn);
                gScoreBoardSurface.Invalidate();
        }

        /// <summary>
        /// Thread safe delegate for updateDatabaseProgress()
        /// </summary>
        /// <param name="TimeElapsed">The time that has elapsed so far in the build</param>
        /// <param name="WorkNodeCount">The number of nodes sitting in the work queue</param>
        /// <param name="NodeTotal">The total number of nodes processed</param>
        /// <param name="VictoryTotal">The total number of victory states discovered so far</param>
        public delegate void updateDatabaseProgressDelegate(TimeSpan TimeElapsed, int WorkNodeCount, int NodeTotal, int VictoryTotal);

        /// <summary>
        /// Thread safe way to update the database build progress form elements
        /// </summary>
        /// <param name="TimeElapsed">The time that has elapsed so far in the build</param>
        /// <param name="WorkNodeCount">The number of nodes sitting in the work queue</param>
        /// <param name="NodeTotal">The total number of nodes processed</param>
        /// <param name="VictoryTotal">The total number of victory states discovered so far</param>
        public static void UpdateDatabaseProgress(TimeSpan TimeElapsed, int WorkNodeCount, int NodeTotal, int VictoryTotal)
        {
            gNodeCounter.Invoke(new updateDatabaseProgressDelegate(UpdateDatabaseProgressForm), TimeElapsed, WorkNodeCount, NodeTotal, VictoryTotal);
        }

        /// <summary>
        /// Thread UNSAFE way to update the database build progress form elements
        /// </summary>
        /// <param name="TimeElapsed">The time that has elapsed so far in the build</param>
        /// <param name="WorkNodeCount">The number of nodes sitting in the work queue</param>
        /// <param name="NodeTotal">The total number of nodes processed</param>
        /// <param name="VictoryTotal">The total number of victory states discovered so far</param>
        public static void UpdateDatabaseProgressForm(TimeSpan TimeElapsed, int WorkNodeCount, int NodeTotal, int VictoryTotal)
        {
            gSimTimerLabel.Text = TimeElapsed.ToString(@"hh\:mm\:ss");
            gNodeCounter.Text = NodeTotal.ToString();
            gWorkCounter.Text = WorkNodeCount.ToString();
            gVictoryCounter.Text = VictoryTotal.ToString();
        }

        /// <summary>
        /// Update the RAM usage meter
        /// </summary>
        public static void UpdateRAMprogress()
        {
            ObjectQuery wql = new ObjectQuery("SELECT * FROM Win32_OperatingSystem");
            ManagementObjectSearcher searcher = new ManagementObjectSearcher(wql);
            ManagementObjectCollection results = searcher.Get();

            foreach (ManagementObject result in results)
            {
                if (gRAMUsageBar.Maximum < 1024)
                    gRAMUsageBar.Maximum = Convert.ToInt32(result["FreePhysicalMemory"]);

                gRAMUsageBar.Value = Math.Min(Convert.ToInt32(result["FreePhysicalMemory"]), gRAMUsageBar.Maximum);
            }

            // If RAM is running low, emergency stop any running jobs
            if ((float)gRAMUsageBar.Value / (float)gRAMUsageBar.Maximum < Properties.Settings.Default.MemoryFloor)
            {
                FormUtil.CancelAIWorkers();
                gDebugText.Text += "#####   DB Build Aborted: Memory floor reached (" + gRAMUsageBar.Value.ToString("0,0.") + " KB free)  #####";
            }

            Graphics RAMGfx = gRAMUsageBar.CreateGraphics();
            int MemoryAbortLine = Convert.ToInt32(gRAMUsageBar.Width * Properties.Settings.Default.MemoryFloor);

            RAMGfx.DrawString(gRAMUsageBar.Value.ToString("0,0.") + " KB free", new Font("Arial", (float)11, FontStyle.Regular), Brushes.White, new PointF(120, 2));
            RAMGfx.DrawLine(new Pen(Color.Red, 2), MemoryAbortLine, 0, MemoryAbortLine, gRAMUsageBar.Height);
            RAMGfx.DrawString("Abort   Line", new Font("Arial", (float)8, FontStyle.Regular), Brushes.White, new PointF(MemoryAbortLine - 34, 4));
        }
    }
}