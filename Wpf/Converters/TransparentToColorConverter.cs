using System;
using System.Drawing;
using System.Globalization;
using System.Windows.Data;

namespace WpfInfrastructure.Converters
{
    public class TransparentToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var bitmapTransparent = (Bitmap)value;
            var bitmapColor = new Bitmap(bitmapTransparent.Width, bitmapTransparent.Height);

            var color = parameter != null ? (Color) parameter : Color.White;

            using (var g = Graphics.FromImage(bitmapColor))
            {
                g.Clear(color);
                g.DrawImage(bitmapTransparent, 0, 0, bitmapTransparent.Width, bitmapTransparent.Height);
            }

            return bitmapColor;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}