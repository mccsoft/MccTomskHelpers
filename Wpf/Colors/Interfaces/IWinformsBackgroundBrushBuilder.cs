using System.Drawing;
using WpfInfrastructure.Colors.Model;

namespace WpfInfrastructure.Colors.Interfaces
{
    public interface IWinformsBackgroundBrushBuilder
    {
        Brush GetBrush(Color backgroundColor, BackgroundFillType backgroundFillType, Color borderColor);
    }
}