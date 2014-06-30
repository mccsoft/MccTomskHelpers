using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace WpfInfrastructure
{
    /// <summary>
    /// Taken here: http://www.dreamincode.net/code/snippet4326.htm
    /// </summary>
    public class FrameworkElementImage
    {
        /// <summary>
        /// Convert any control to a PngBitmapEncoder
        /// </summary>
        /// <param name="controlToConvert">The control to convert to an ImageSource</param>
        /// <returns>The returned ImageSource of the controlToConvert</returns>
        public static PngBitmapEncoder GetPngBitmapEncoderFromFrameworkElement(FrameworkElement controlToConvert)
        {
            // get size of control
            var sizeOfControl = new Size(controlToConvert.ActualWidth, controlToConvert.ActualHeight);
            // measure and arrange the control
            controlToConvert.Measure(sizeOfControl);
            // arrange the surface
            controlToConvert.Arrange(new Rect(sizeOfControl));

            // craete and render surface and push bitmap to it
            var renderBitmap = new RenderTargetBitmap((Int32)sizeOfControl.Width, (Int32)sizeOfControl.Height, 96d, 96d, PixelFormats.Pbgra32);
            // now render surface to bitmap
            renderBitmap.Render(controlToConvert);

            // encode png data
            var pngEncoder = new PngBitmapEncoder();
            // puch rendered bitmap into it
            pngEncoder.Frames.Add(BitmapFrame.Create(renderBitmap));

            // return encoder
            return pngEncoder;
        }

        /// <summary>
        /// Get an ImageSource of a control
        /// </summary>
        /// <param name="controlToConvert">The control to convert to an ImageSource</param>
        /// <returns>The returned ImageSource of the controlToConvert</returns>
        public static ImageSource GetImageSourceFromFrameworkElement(FrameworkElement controlToConvert)
        {
            // return first frame of image 
            return GetPngBitmapEncoderFromFrameworkElement(controlToConvert).Frames[0];
        }
    }
}