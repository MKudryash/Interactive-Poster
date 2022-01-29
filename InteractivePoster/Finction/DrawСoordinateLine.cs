using System;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace InteractivePoster.Finction
{
    class DrawСoordinateLine
    {

        double maxX;
        double maxY;
        double count;
        Line line;
        Orientation orientation;//для вертикальной или горизонтальной линии
        double convertCoord(double coord)
        {
            return maxX / 2 + coord * (maxX / count);
        }
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
    }
}
