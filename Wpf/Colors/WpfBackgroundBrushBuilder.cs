using System;
using System.Windows;
using System.Windows.Media;
using WpfInfrastructure.Colors.Model;

namespace WpfInfrastructure.Colors
{
    public class WpfBackgroundBrushBuilder
    {
        public Brush GetBrush(Color backgroundColor, BackgroundFillType backgroundFillType, Color borderColor)
        {
            switch (backgroundFillType)
            {
                case BackgroundFillType.Solid:
                    return new SolidColorBrush(backgroundColor);
                case BackgroundFillType.BackwardDiagonal:
                    //Here are source code samples for WPF HatchBrush: http://www.sourcecodestore.com/Article.aspx?ID=8

                    var width = 16;
                    var height = 16;

                    var hatchGeometryGroup = new GeometryGroup();
                    hatchGeometryGroup.Children.Add(new LineGeometry(new Point(0, 12), new Point(3, 15)));
                    hatchGeometryGroup.Children.Add(new LineGeometry(new Point(0, 4), new Point(11, 15)));
                    hatchGeometryGroup.Children.Add(new LineGeometry(new Point(4, 0), new Point(15, 11)));
                    hatchGeometryGroup.Children.Add(new LineGeometry(new Point(12, 0), new Point(15, 3)));
                    hatchGeometryGroup.Transform = new RotateTransform(90, width / 2, height / 2);

                    var pen = new Pen(new SolidColorBrush(borderColor), 1);
                    var hatchGeometryDrawing = new GeometryDrawing(null, pen, hatchGeometryGroup);

                    var backgroundGeometry = new RectangleGeometry(new Rect(0, 0, width, height));
                    var backgroundGeometryDrawing = new GeometryDrawing(new SolidColorBrush(backgroundColor), null, backgroundGeometry);

                    var drawingGroup = new DrawingGroup();
                    drawingGroup.Children.Add(backgroundGeometryDrawing);
                    drawingGroup.Children.Add(hatchGeometryDrawing);

                    var drawingBrush = new DrawingBrush();
                    drawingBrush.Stretch = Stretch.None;
                    drawingBrush.ViewportUnits = BrushMappingMode.Absolute;
                    drawingBrush.Viewport = new Rect(0, 0, width, height);
                    drawingBrush.TileMode = TileMode.Tile;
                    drawingBrush.Drawing = drawingGroup;

                    return drawingBrush;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}