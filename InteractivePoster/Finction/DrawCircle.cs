using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace InteractivePoster.Finction
{
    class DrawCircle : GeometricPatterns
    {
        Ellipse circle, point;//непосредственно сама окружность
        Line line;
        /// <summary>
        /// Конструктор класса
        /// </summary>
        /// <param name="x">Декартова абсцисса центра окружности</param>
        /// <param name="y">Декартова ордината центра окружности</param>
        /// <param name="r">Радиус окружности в декартовой системе</param>
        /// <param name="cv">Объект канвы, на котором появится окружность</param>

        double x, y,r;
        public DrawCircle(double x, double y, double r, Canvas cv)
        {
            countX = Convert.ToDouble(cv.Tag);//получаем масштабы области
            countY = Math.Round(cv.ActualHeight / (cv.ActualWidth / countX));
            maxX = cv.ActualWidth; //получаем ширину канвы
            maxY = cv.ActualHeight; //получаем высоту канвы     
            radius = r * (maxY / countY);//преобразуем радиус из декартовой системы
            this.cv = cv;
            this.x = x;
            this.y = y;
            this.r = r;
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
            //центр окружности

            point = new Ellipse()//задаем прочие параметры
            {
                Width = (maxX / countX) * 0.2,
                Height = (maxX / countX) * 0.2,
                Fill = Brushes.Black,
                Stroke = Brushes.Black,
                StrokeThickness = 1
            };

            cv.Children.Add(point);//помещаем на канву
                                   //в нужную точку канвы
            point.SetValue(Canvas.LeftProperty, convertCoordX(x - 0.1));
            point.SetValue(Canvas.TopProperty, convertCoordY(y + 0.1));

            DrawRadius(x, y, r);
            //надпись центра окружности
           TextBlock textBlock =  DrawText(x, y, text);
            cv.Children.Add(textBlock);
           
        }


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
            string text = "M( " + circleX.ToString("F1") + "; " + circleY.ToString("F1") + ")";
            PointOnCircle(circleX, circleY, text);
            if (MaxMinCoordinat.ElementCircle)
            {                
                cv.Children.Add(line);                           
            }
            else
            {
                cv.Children.Remove(line);               
            }
        } //Орисовка радиуса + текст с точкой на окружности

        private void PointOnCircle(double circleX, double circleY, string text)
        {
            TextBlock textBlock = DrawText(circleX, circleY, text);
            if (MaxMinCoordinat.ElementCirclePoint)
            {
                
                cv.Children.Add(textBlock);
            }
            else
            {
                cv.Children.Remove(textBlock);
            }

        }


        public async void ChangedGradus(object sender, MouseEventArgs e)
        {
            double x = e.GetPosition(cv).X;
            double y = e.GetPosition(cv).Y;

            double coordX = convertXCoord(x);
            double coordY = convertYCoord(y);
            double p = Math.Atan((coordY - this.y) / (coordX - this.x));


            if (coordX < this.x) { p = p + Math.PI; }
            MaxMinCoordinat.gradusValue = (int)((p) * 180 / Math.PI);
        }

        /// <summary>
        /// метод для преобразования координаты Х их канвы в декартово значение
        /// </summary>
        /// <param name="x">декартова координата (как нам надо с точки зрения математики)</param>
        /// <returns>актуальная координата Х на канве</returns>
        public double convertX(double x)
        {
            //радиус вычитаем, т.к. по умолчанию передаются координаты левого верхнего угла
            return maxX / 2 + x * (maxX / countX) - radius;
        }
        /// <summary>
        /// метод для преобразования координаты Y их канвы в декартово значение
        /// </summary>
        /// <param name="y">декартова координата (как нам надо с точки зрения математики)</param>
        /// <returns>актуальная координата Y на канве</returns>
        public double convertY(double y)
        {
            return maxY / 2 + y / -1 * (maxY / countY) - radius;
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
            return Math.Abs(x) > Math.Abs(y) ? countX / 2 - Math.Abs(x) : countX / 2 - Math.Abs(y);
        }
        public string CanonicalEquation()
        {
            if (x == 0 && y == 0) return @"x^2+ y^2 = " + r.ToString("F1") + @"^2";
            if (y == 0) return @"(x-(" + x.ToString("F1") + @"))^2+ y^2 = " + r.ToString("F1") + @"^2";
            if (x == 0) return @"x^2+ (y-(" + y.ToString("F1") + @"))^2 = " + r.ToString("F1") + @"^2";
            return @"(x-(" + x.ToString("F1") + @"))^2+ (y-(" + y.ToString("F1") + @"))^2 = " + r.ToString("F1") + @"^2"; 
        }
    }
}
