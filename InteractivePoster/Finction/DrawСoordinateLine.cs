using System;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace InteractivePoster.Finction
{
    class DrawСoordinateLine : GeometricPatterns
    {

        Line line;
        Orientation orientation;//для вертикальной или горизонтальной линии

        /// <summary>
        /// конструктор класса
        /// </summary>
        /// <param name="coord">по сути, если известна ориентация, то достаточно одной координаты</param>
        /// <param name="orientation">непосредственно сама ориентация</param>
        /// <param name="th">толщина границы</param>
        /// <param name="cv">объект канвы, на котором будет нарисована линия</param>
        public DrawСoordinateLine(double coord, Orientation orientation, double th, Canvas cv)
        {
            this.orientation = orientation;
            count = Convert.ToDouble(cv.Tag);
            maxX = cv.ActualWidth;
            maxY = cv.ActualHeight;
            line = new Line()
            {
                Stroke = Brushes.Black,
                StrokeThickness = th,
                SnapsToDevicePixels = true
            };
            if (orientation == Orientation.Horizontal)
            {
                line.X1 = 0;
                line.X2 = maxX;
                line.Y1 = line.Y2 = convertCoord(coord);
            }
            if (orientation == Orientation.Vertical)
            {
                line.Y1 = 0;
                line.Y2 = maxY;
                line.X1 = line.X2 = convertCoord(coord); ;
            }
            line.SetValue(RenderOptions.EdgeModeProperty, EdgeMode.Aliased);
            cv.Children.Add(line);
        }
        public void DrawArrow(double coord, Orientation orientation, double th, Canvas cv)
        {
            line = new Line()
            {
                Stroke = Brushes.Black,
                StrokeThickness = th,
                SnapsToDevicePixels = true
            };
            if (orientation == Orientation.Horizontal)
            {
                line.X1 = maxX;
                line.X2 = convertX(count / 2 - 1);
                line.Y1 = convertCoord(coord);
                line.Y2 = convertY(0.5);
            }
            if (orientation == Orientation.Vertical)
            {
                line.Y1 = 0;
                line.Y2 = convertY(count / (-2) + 1);
                line.X1 = convertCoord(coord);
                line.X2 = convertX(0.5);
            }
            line.SetValue(RenderOptions.EdgeModeProperty, EdgeMode.Aliased);
            cv.Children.Add(line);
            line = new Line()
            {
                Stroke = Brushes.Black,
                StrokeThickness = th,
                SnapsToDevicePixels = true
            };
            if (orientation == Orientation.Horizontal)
            {
                line.X1 = maxX;
                line.X2 = convertX(count / 2 - 1);
                line.Y1 = convertCoord(coord);
                line.Y2 = convertY(-0.5);
            }
            if (orientation == Orientation.Vertical)
            {
                line.Y1 = 0;
                line.Y2 = convertY(count / (-2) + 1);
                line.X1 = convertCoord(coord);
                line.X2 = convertX(-0.5);
            }
            line.SetValue(RenderOptions.EdgeModeProperty, EdgeMode.Aliased);
            cv.Children.Add(line);
        }
    }
}
