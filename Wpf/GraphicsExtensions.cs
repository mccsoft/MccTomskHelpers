using System.Drawing;

namespace WpfInfrastructure
{
    public static class GraphicsExtensions
    {
        /// <summary>
        /// Standard DrawLines method is not smart enough to draw a point in case we have only one point in the array and throws exception
        /// </summary>
        public static void DrawPointOrLines(this Graphics graphics, Pen pen, Point[] points)
        {
            if (points.Length > 1)
                graphics.DrawLines(pen, points);
            if (points.Length == 1)
                graphics.DrawLine(pen, points[0], points[0]);
        }
    }
}