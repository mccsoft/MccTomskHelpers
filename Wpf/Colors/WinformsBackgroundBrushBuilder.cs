using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using WpfInfrastructure.Colors.Interfaces;
using WpfInfrastructure.Colors.Model;

namespace WpfInfrastructure.Colors
{
    public class WinformsBackgroundBrushBuilder : IWinformsBackgroundBrushBuilder
    {
        public Brush GetBrush(Color backgroundColor, BackgroundFillType backgroundFillType, Color borderColor)
        {
            switch (backgroundFillType)
            {
                case BackgroundFillType.Solid:
                    return new SolidBrush(backgroundColor);
                case BackgroundFillType.BackwardDiagonal:
                    return new HatchBrush(HatchStyle.BackwardDiagonal, borderColor, backgroundColor);
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}