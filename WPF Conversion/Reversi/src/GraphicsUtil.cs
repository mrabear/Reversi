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
    }
}