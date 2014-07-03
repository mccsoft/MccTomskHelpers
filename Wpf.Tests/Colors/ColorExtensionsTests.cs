using MccTomskHelpers.Wpf.Colors;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MccTomskHelpers.Wpf.Tests.Colors
{
    [TestClass]
    public class ColorExtensionsTests
    {
         [TestMethod]
         public void ToWinformsColor_Blue_A255R0G0B255()
         {
             var color = System.Windows.Media.Colors.Blue;

             var winformsColor = ColorExtensions.ToWinformsColor(color);

             Assert.AreEqual(255, winformsColor.A);
             Assert.AreEqual(0, winformsColor.R);
             Assert.AreEqual(0, winformsColor.G);
             Assert.AreEqual(255, winformsColor.B);
         }
    }
}