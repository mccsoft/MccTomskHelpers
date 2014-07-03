using System.Drawing;
using System.Drawing.Drawing2D;
using FluentAssertions;
using MccTomskHelpers.Wpf.Colors;
using MccTomskHelpers.Wpf.Colors.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MccTomskHelpers.Wpf.Tests.Colors
{
    [TestClass]
    public class WinformsBackgroundBrushBuilderTests
    {
        private WinformsBackgroundBrushBuilder _winformsBackgroundBrushBuilder;

        public WinformsBackgroundBrushBuilderTests()
        {
            _winformsBackgroundBrushBuilder = new WinformsBackgroundBrushBuilder();
        }

        [TestMethod]
        public void GetBrush_Solid()
        {
            var brush = _winformsBackgroundBrushBuilder.GetBrush(System.Windows.Media.Colors.AliceBlue.ToWinformsColor(), BackgroundFillType.Solid, System.Windows.Media.Colors.Black.ToWinformsColor());

            brush.Should().BeOfType<SolidBrush>();
            var solidBrush = (SolidBrush) brush;
            solidBrush.Color.Should().Be(System.Windows.Media.Colors.AliceBlue.ToWinformsColor());
        }

        [TestMethod]
        public void GetBrush_BackwardDiagonal()
        {
            var brush = _winformsBackgroundBrushBuilder.GetBrush(System.Windows.Media.Colors.AliceBlue.ToWinformsColor(), BackgroundFillType.BackwardDiagonal, System.Windows.Media.Colors.Black.ToWinformsColor());

            brush.Should().BeOfType<HatchBrush>();
            var hatchBrush = (HatchBrush) brush;
            hatchBrush.HatchStyle.Should().Be(HatchStyle.BackwardDiagonal);
            hatchBrush.ForegroundColor.Should().Be(System.Windows.Media.Colors.Black.ToWinformsColor());
            hatchBrush.BackgroundColor.Should().Be(System.Windows.Media.Colors.AliceBlue.ToWinformsColor());
        } 
    }
}