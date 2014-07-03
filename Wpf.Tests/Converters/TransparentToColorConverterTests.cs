using System.Drawing;
using MccTomskHelpers.Wpf.Converters;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MccTomskHelpers.Wpf.Tests.Converters
{
    [TestClass]
    public class TransparentToColorConverterTest
    {
        private Bitmap generateBitmap(Color color)
        {
            var bitmap = new Bitmap(2, 2);
            for (int x = 0; x < bitmap.Height; ++x)
                for (int y = 0; y < bitmap.Width; ++y)
                    bitmap.SetPixel(x, y, color);
            return bitmap;
        }

        [TestMethod]
        public void Convert_TransparentBitmap_ReturnsWhiteBitmap()
        {
            var converter = new TransparentToColorConverter();
            var bitmap = generateBitmap(Color.Transparent);

            var result = (Bitmap)converter.Convert(bitmap, null, null, null);

            for (int x = 0; x < result.Height; ++x)
               for (int y = 0; y < result.Width; ++y)
                   Assert.IsTrue(result.GetPixel(x, y).ToArgb() == Color.White.ToArgb());
        }

        [TestMethod]
        public void Convert_TransparentBitmapParameterRed_ReturnsRedBitmap()
        {
            var converter = new TransparentToColorConverter();
            var bitmap = generateBitmap(Color.Transparent);

            var result = (Bitmap)converter.Convert(bitmap, null, Color.Red, null);

            for (int x = 0; x < bitmap.Height; ++x)
                for (int y = 0; y < bitmap.Width; ++y)
                    Assert.IsTrue(result.GetPixel(x, y).ToArgb() == Color.Red.ToArgb());
        }

        [TestMethod]
        public void Convert_TransparentBitmapWithBlackPixel_ReturnsBitmapWithBlackPixel()
        {
            var converter = new TransparentToColorConverter();
            var bitmap = generateBitmap(Color.Transparent);

            bitmap.SetPixel(0,0, Color.Black);
            var result = (Bitmap)converter.Convert(bitmap, null, null, null);

            Assert.IsTrue(result.GetPixel(0, 0).ToArgb() == Color.Black.ToArgb());
        }

        [TestMethod]
        public void Convert_GreenBitmap_ReturnsGreenBitmap()
        {
            var converter = new TransparentToColorConverter();
            var bitmap = generateBitmap(Color.Green);

            var result = (Bitmap)converter.Convert(bitmap, null, null, null);

            for (int x = 0; x < bitmap.Height; ++x)
                for (int y = 0; y < bitmap.Width; ++y)
                    Assert.IsTrue(result.GetPixel(x, y).ToArgb() == Color.Green.ToArgb());
        }
    }
}
