using System.Windows.Media;

namespace MccTomskHelpers.Wpf.Colors
{
    public static class ColorExtensions
    {
         public static System.Drawing.Color ToWinformsColor(this Color color)
         {
             return System.Drawing.Color.FromArgb(color.A, color.R, color.G, color.B);
         }
    }
}