using System;
using System.Windows;
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
                

        Canvas cv;
        public DrawCircle(double x, double y, double r, Canvas cv)
        {
            count = Convert.ToDouble(cv.Tag);//получаем масштабы области
            maxX = cv.ActualWidth; //получаем ширину канвы       
            radius = r * (maxX / count);//преобразуем радиус из декартовой системы
            this.cv = cv;
            circle = new Ellipse()//задаем прочие параметры
            {
                Width = 2 * radius,//ширина и длина по сути равна диаметру окружности
                Height = 2 * radius,
                Stroke = Brushes.Black,
                StrokeThickness = 3
            };

            cv.Children.Add(circle);//помещаем на канву
            //в нужную точку канвы
            circle.SetValue(Canvas.LeftProperty, convertX(x));
            circle.SetValue(Canvas.TopProperty, convertY(y));
            string text = "О( " + x.ToString("F1") + "; " + y.ToString("F1") + ")";
            DrawRadius(x, y , r);
            DrawText(x, y,text);
        }
        void DrawText(double x, double y,string s)
        {
            TextBlock TB = new TextBlock();
            TB.Text = s;
            cv.Children.Add(TB);
            TB.SetValue(Canvas.LeftProperty, convertCoordX(x));
            TB.SetValue(Canvas.TopProperty, convertCoordY(y));
            TB.TextWrapping = TextWrapping.Wrap;
            TB.Width = double.NaN;
            TB.FontSize = maxX/count*0.5;
        } //Текст c содержанием точек
        void DrawRadius(double x, double y, double r)
        {

            double circleX = x + r * Math.Cos(convertRadian(MaxMinCoordinat.gradusValue));
            double circleY = y + r * Math.Sin(convertRadian(MaxMinCoordinat.gradusValue));

            line = new Line()
            {
                Stroke = Brushes.Black,
                StrokeThickness = 3,
                SnapsToDevicePixels = true
            };
            line.X1 = convertCoordX(x);
            line.X2 = convertCoordX(circleX);
            line.Y1 = convertCoordY(y);
            line.Y2 = convertCoordY(circleY);

            line.SetValue(RenderOptions.EdgeModeProperty, EdgeMode.Aliased);
            cv.Children.Add(line);
            string text = "M( " + circleX.ToString("F1") + "; " + circleY.ToString("F1") + ")";
            DrawText(circleX, circleY, text);
        } //Орисовка радиуса + текст с точкой на окружности

        /// <summary>
        /// метод для преобразования координаты Х их канвы в декартово значение
        /// </summary>
        /// <param name="x">декартова координата (как нам надо с точки зрения математики)</param>
        /// <returns>актуальная координата Х на канве</returns>
        public double convertX(double x)
        {
            //радиус вычитаем, т.к. по умолчанию передаются координаты левого верхнего угла
            return maxX / 2 + x * (maxX / count) - radius;
        }
        /// <summary>
        /// метод для преобразования координаты Y их канвы в декартово значение
        /// </summary>
        /// <param name="y">декартова координата (как нам надо с точки зрения математики)</param>
        /// <returns>актуальная координата Y на канве</returns>
        public double convertY(double y)
        {
            return maxX / 2 + y / -1 * (maxX / count) - radius;
        }
        /// <summary>
        /// метод для преобразования градусов в радиан
        /// </summary>
        /// <param name="r">значение в градусах (как нам надо с точки зрения математики)</param>
        /// <returns>значаение в радианах</returns>
        public double convertRadian(double r)
        {
            return r / 180 * Math.PI;
        }
        public double MaxRadius(double x, double y)
        {
            return  Math.Abs(x)> Math.Abs(y) ? count/2-Math.Abs(x) : count / 2 - Math.Abs(y);
        }
    }
}
