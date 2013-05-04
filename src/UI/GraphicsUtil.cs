/// <summary>
/// Reversi.GraphicsTools.cs
/// Brian A. Hebert
/// </summary>

using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Resources;

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
        public static ImageSource GenerateImageSource(string ImagePath)
        {
            StreamResourceInfo sri = Application.GetResourceStream(new Uri("/Reversi;component/" + ImagePath, UriKind.Relative));
            BitmapImage bmp = new BitmapImage();
            bmp.BeginInit();
            bmp.StreamSource = sri.Stream;
            bmp.EndInit();

            return bmp;
        }
    }
}