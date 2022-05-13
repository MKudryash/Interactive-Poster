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
        /// <param name="canvas">объект канвы, с которого беруться параметры</param>
        public DrawСoordinateLine(double coord, Orientation orientation, double th, Canvas cv,Canvas canvas)
        {
            this.orientation = orientation;
            countX = Convert.ToDouble(canvas.Tag);
            countY = Math.Round(canvas.ActualHeight / (canvas.ActualWidth / countX));
            maxX = canvas.ActualWidth;
            maxY = canvas.ActualHeight;
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
                line.Y1 = line.Y2 = convertCoordY(coord);
            }
            if (orientation == Orientation.Vertical)
            {
                line.Y1 = 0;
                line.Y2 = maxY;
                line.X1 = line.X2 = convertCoordX(coord); 
            }
            line.SetValue(RenderOptions.EdgeModeProperty, EdgeMode.Aliased);
            cv.Children.Add(line);
        }
        public void DrawArrow(double x, double y, Orientation orientation, double th, Canvas cv)
        {
            line = new Line()
            {
                Stroke = Brushes.Black,
                StrokeThickness = th,
                SnapsToDevicePixels = true
            };
            if (orientation == Orientation.Horizontal)
            {
                line.X1 = convertCoordX(x);
                line.X2 = convertCoordX(x - 0.5);
                line.Y1 = convertCoordY(y);
                line.Y2 = convertCoordY(y + 0.2);
            }
            else
            {
                line.X1 = convertCoordX(x);
                line.X2 = convertCoordX(x + 0.2);
                line.Y1 = convertCoordY(y);
                line.Y2 = convertCoordY(y - 0.5);
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
                line.X1 = convertCoordX(x);
                line.X2 = convertCoordX(x - 0.5);
                line.Y1 = convertCoordY(y);
                line.Y2 = convertCoordY(y - 0.2);
            }
            else 
            {
                line.X1 = convertCoordX(x);
                line.X2 = convertCoordX(x - 0.2);
                line.Y1 = convertCoordY(y);
                line.Y2 = convertCoordY(y - 0.5);
            }
            line.SetValue(RenderOptions.EdgeModeProperty, EdgeMode.Aliased);
            cv.Children.Add(line);
        }
    }
}
