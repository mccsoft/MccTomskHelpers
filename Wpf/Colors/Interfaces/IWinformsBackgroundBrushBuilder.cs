using System.Drawing;
using MccTomskHelpers.Wpf.Colors.Model;

namespace MccTomskHelpers.Wpf.Colors.Interfaces
{
    public interface IWinformsBackgroundBrushBuilder
    {
        Brush GetBrush(Color backgroundColor, BackgroundFillType backgroundFillType, Color borderColor);
    }
}