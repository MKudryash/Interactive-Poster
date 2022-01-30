using System;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace InteractivePoster.Finction
{
    class DrawCircle : GeometricPatterns
    {
        Ellipse circle;//непосредственно сама окружность
        Line line;
        /// <summary>
        /// Конструктор класса
        /// </summary>
        /// <param name="x">Декартова абсцисса центра окружности</param>
        /// <param name="y">Декартова ордината центра окружности</param>
        /// <param name="r">Радиус окружности в декартовой системе</param>
        /// <param name="cv">Объект канвы, на котором появится окружность</param>
        public DrawCircle(double x, double y, double r, Canvas cv)
        {
            count = Convert.ToDouble(cv.Tag);//получаем масштабы области
            maxX = cv.ActualWidth; //получаем ширину канвы       
            radius = r * (maxX / count);//преобразуем радиус из декартовой системы

            circle = new Ellipse()//задаем прочие параметры
            {
                Width = 2 * radius,//ширина и длина по сути равна диаметру окружности
                Height = 2 * radius,
                Stroke = Brushes.Red,
                StrokeThickness = 1
            };

            cv.Children.Add(circle);//помещаем на канву
            //в нужную точку канвы
            circle.SetValue(Canvas.LeftProperty, convertX(x));
            circle.SetValue(Canvas.TopProperty, convertY(y));
            DrawRadius(x, y, cv, r);
            DrawText(x, y, cv);
        }
        void DrawText(double x, double y, Canvas cv)
        {
            TextBlock TB = new TextBlock();
            TB.Text = "О("+x.ToString()+";"+(y / (-1)).ToString()+")";
            cv.Children.Add(TB);
            TB.SetValue(Canvas.LeftProperty, convertCoord(x));
            TB.SetValue(Canvas.TopProperty, convertCoord(y));
        } //Текст с центром окружности
        void DrawRadius(double x, double y, Canvas cv, double r)
        {
            line = new Line()
            {
                Stroke = Brushes.Blue,
                StrokeThickness = 4,
                SnapsToDevicePixels = true
            };
            line.X1 = line.X2 = convertCoord(x);
            line.Y1 = convertCoord(y);
            line.Y2 = convertCoord(y+r);

            line.SetValue(RenderOptions.EdgeModeProperty, EdgeMode.Aliased);
            cv.Children.Add(line);

            TextBlock TB = new TextBlock();
            TB.Text = "О(" + x.ToString() + ";" + ((y+r)/(-1)).ToString() + ")";
            cv.Children.Add(TB);
            TB.SetValue(Canvas.LeftProperty, convertCoord(x));
            TB.SetValue(Canvas.TopProperty, convertCoord(y+r));
        } //Орисовка радиуса + текст с точкой на окружности


    }
}
