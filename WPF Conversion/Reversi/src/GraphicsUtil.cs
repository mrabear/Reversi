/// <summary>
/// Reversi.ReversiForm.GraphicsUtil.cs
/// </summary>

using System;
using System.IO;
using System.Management;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Reversi
{
    /// <summary>
    /// A subclass of ReversiForm, used to manipulate the game board and other graphics assets
    /// </summary>
    public class GraphicsUtil
    {


        public static ImageSource GenerateImageSource(System.Drawing.Bitmap bm)
        {

            BitmapSource bms = null;
            if (bm != null)
            {
                IntPtr h_bm = bm.GetHbitmap();
                bms = System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(h_bm, IntPtr.Zero, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());
            }
            return bms;
        }  
















/////////////////////////////
        /// <summary>
        /// Repaints all game graphics surfaces
        /// </summary>
        public static void RefreshAll(Grid BoardGrid)
        {
            RefreshPieces(BoardGrid, FullRefresh: true);
            //gScoreBoardSurface.Invalidate();
            //gGameBoardSurface.Invalidate();
        }

        /// <summary>
        /// Updates the score board for both players
        /// </summary>
        public static void UpdateScoreBoard()
        {
            //gScoreBoardSurface.Invalidate();
        }

        /// <summary>
        /// Updates the score board for both players
        /// </summary>
        public static void UpdateScoreBoard( int Turn )
        {
            /*
            if (Turn == ReversiApplication.WHITE)
                gScoreBoardGFX.DrawImage(Reversi.Properties.Resources.ScoreBoard_WhiteTurn, 0, 0);
            else
                gScoreBoardGFX.DrawImage(Reversi.Properties.Resources.ScoreBoard_BlackTurn, 0, 0);

            StringFormat sf = new StringFormat();
            sf.LineAlignment = StringAlignment.Center;
            sf.Alignment = StringAlignment.Center;

            gScoreBoardGFX.DrawString(gCurrentGame.GetGameBoard().FindScore(ReversiApplication.BLACK).ToString(), new Font("Segoe UI", (float)30, FontStyle.Regular), Brushes.White, new RectangleF(51, 47, 100, 28), sf);
            gScoreBoardGFX.DrawString(gCurrentGame.GetGameBoard().FindScore(ReversiApplication.WHITE).ToString(), new Font("Segoe UI", (float)30, FontStyle.Regular), Brushes.Black, new RectangleF(189, 47, 100, 28), sf);
            */
        }

        /// <summary>
        /// Draw all game pieces on the board
        /// </summary>
        /// <param name="FullRefresh">(optional) True forces a full refresh of all pieces</param>
        public static void RefreshPieces(Grid BoardGrid, bool FullRefresh = false)
        {
            RefreshPieces(BoardGrid,ReversiWindow.GetCurrentGame().GetGameBoard(), FullRefresh);
        }

        /// <summary>
        /// Draw all game pieces on the board
        /// </summary>
        /// <param name="SourceBoard">The board to draw onto the screen</param>
        /// <param name="FullRefresh">(optional) True forces a full refresh of all pieces</param>
        public static void RefreshPieces(Grid BoardGrid, Board SourceBoard, bool FullRefresh = false)
        {

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
        public static void DrawPiece(double X, double Y, int Color)
        {
            
            if ((Color == ReversiWindow.WHITE) || (Color == ReversiWindow.BLACK)) ;

                //gGameBoardBackBufferGFX.DrawImage(GetPieceImage(Color), X * BoardGridSize, Y * BoardGridSize, BoardGridSize, BoardGridSize);
        }

        /// <summary>
        /// Redraws the board image, erasing all pieces
        /// </summary>
        public static void RedrawBoardImage()
        {
            //gGameBoardBackBufferGFX.DrawImage(Reversi.Properties.Resources.GameBoard, 0, 0, gGameBoardSurface.Width, gGameBoardSurface.Height);
        }

        public static void PromoteBackBuffer()
        {
            //gGameBoardSurface.CreateGraphics().DrawImage(gGameBoardBackBuffer, 0, 0);
        }

        /// <summary>
        /// Marks all of the available moves for the given turn on the current game board
        /// </summary>
        /// <param name="Turn">The turn to use</param>
        public static void MarkAvailableMoves(int Turn)
        {
            //MarkAvailableMoves(gCurrentGame.GetGameBoard(), Turn);
        }

        /// <summary>
        /// Marks all of the available moves for the given turn on the given game board
        /// </summary>
        /// <param name="SourceBoard">The game board</param>
        /// <param name="Turn">The turn to use</param>
        public static void MarkAvailableMoves(Board SourceBoard, int Turn)
        {
            /*
            // Only display if the 'Show Available Moves' box is checked
            if ((gShowAvailableMoves.Checked) && (gCurrentGame.GetCurrentTurn() != gCurrentGame.GetAI().GetColor()))
                // Loop through all available moves and place a dot at the location
                foreach (Point CurrentPiece in SourceBoard.AvailableMoves(Turn))
                    gGameBoardBackBufferGFX.DrawImage(Reversi.Properties.Resources.SuggestedPiece, CurrentPiece.X * BoardGridSize, CurrentPiece.Y * BoardGridSize, BoardGridSize, BoardGridSize);
             */
        }

        /// <summary>
        /// Places a highlight circle at the given locations
        /// </summary>
        /// <param name="PieceList">The list of pieces</param>
        /// <param name="PieceColor">The highlight color</param>
        public static void HighlightPiece(Point[] PieceList, Color PieceColor)
        {
            /*
            foreach (Point CurrentPiece in PieceList)
                HighlightPiece(CurrentPiece.X, CurrentPiece.Y, PieceColor);
             */
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
        public static void HighlightPiece(double X, double Y, Color PieceColor, String PieceLabel = "")
        {
            //gGameBoardBackBufferGFX.DrawEllipse(new Pen(PieceColor, 4), X * BoardGridSize + 24, Y * BoardGridSize + 24, 30, 30);

            //if (PieceLabel != "")
            //    gGameBoardBackBufferGFX.DrawString(PieceLabel, new Font("Segoe UI", (float)9, FontStyle.Regular), Brushes.White, new RectangleF(X * BoardGridSize + 5, (Y + 1) * BoardGridSize - 35, BoardGridSize - 10, BoardGridSize - 28), sf);
        }

        /// <summary>
        /// Returns the correct image for a given turn
        /// </summary>
        /// <param name="Turn">The turn</param>
        /// <returns>Image for the given turn</returns>
       /* public static Image GetPieceImage(int Turn)
        {
            //if (Turn == ReversiApplication.WHITE)
            //    return (Reversi.Properties.Resources.WhitePiece);

            return (Reversi.Properties.Resources.BlackPiece);
        }*/

        /// <summary>
        /// Updates the "Current Turn" image to indicate a winner (or tie)
        /// </summary>
        /// <param name="WinningTurn">The winning turn</param>
        public static void ShowWinner(int WinningTurn)
        {
           // if ((WinningTurn == ReversiApplication.BLACK) || (WinningTurn == ReversiApplication.WHITE))
                //UpdateScoreBoard(WinningTurn);
            //    gScoreBoardSurface.Invalidate();
        }
    }
}