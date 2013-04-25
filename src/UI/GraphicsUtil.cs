/// <summary>
/// Reversi.GraphicsTools.cs
/// Brian A. Hebert
/// </summary>

using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;


namespace Reversi
{
    /// <summary>
    /// Contains general graphics utilities
    /// </summary>
    public static class GraphicsTools
    {
        /// <summary>
        /// Converts a Drawing.Bitmap object into Media.ImageSource 
        /// </summary>
        public static ImageSource GenerateImageSource(System.Drawing.Bitmap GivenImage)
        {
            BitmapSource GivenImageSource = null;
            
            if (GivenImage != null)
                GivenImageSource = System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(GivenImage.GetHbitmap(), IntPtr.Zero, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());

            return GivenImageSource;
        }  
    }
}