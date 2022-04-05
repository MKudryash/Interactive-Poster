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
        Ellipse circle,point;//непосредственно сама окружность
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
            countX = Convert.ToDouble(cv.Tag);//получаем масштабы области
            countY = Math.Round(cv.ActualHeight / (cv.ActualWidth / countX));
            maxX = cv.ActualWidth; //получаем ширину канвы
            maxY = cv.ActualHeight; //получаем высоту канвы     
            radius = r * (maxY / countY);//преобразуем радиус из декартовой системы
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



            DrawRadius(x, y , r);
            DrawText(x, y,text);
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
            cv.Children.Add(line);
            string text = "M( " + circleX.ToString("F1") + "; " + circleY.ToString("F1") + ")";
            DrawText(circleX, circleY, text);
        } //Орисовка радиуса + текст с точкой на окружности


        public async void ChangedGradus(object sender, MouseEventArgs e)
        {
                double x = e.GetPosition(cv).X;
                double y = e.GetPosition(cv).Y;



                double coordX = countX / cv.ActualWidth * (x - cv.ActualWidth / 2);
                double coordY = -1 * (countY / cv.ActualHeight) * (y - cv.ActualHeight / 2);
                double p = Math.Abs(Math.Round(Math.Atan(coordX / coordY), 2));


                if (coordX > 0)
                {
                    if (coordY > 0)
                    { MaxMinCoordinat.gradusValue= (int)(90 - p * 180 / Math.PI); }
                    else { MaxMinCoordinat.gradusValue = (int)(270 + (p * 180 / Math.PI)); }
                }
                else
                {
                    if (coordY > 0)
                    { MaxMinCoordinat.gradusValue = (int)(90 + p * 180 / Math.PI); }
                    else { MaxMinCoordinat.gradusValue = (int)(270 - p * 180 / Math.PI); }
                }
        }

        public async void BuildCircle(object sender, MouseEventArgs e)
        {
            //центр окружности и радиус 

            //if (centerCircle)
            //{
            //    x = e.GetPosition(Background).X;
            //    y = e.GetPosition(Background).Y;
            //    double coordX = count / Background.ActualWidth * (x - Background.ActualWidth / 2);
            //    double coordY = -1 * (countY / Background.ActualHeight) * (y - Background.ActualHeight / 2);
            //    slCoordX.Value = coordX;
            //    slCoordY.Value = coordY;
            //    centerCircle = false;
            //}
            //if (radiusCircle)
            //{
            //    Line line = new Line()
            //    {
            //        Stroke = Brushes.Black,
            //        StrokeThickness = 3,
            //        SnapsToDevicePixels = true,
            //        X1 = x,
            //        X2 = e.GetPosition(Background).X,
            //        Y1 = y,
            //        Y2 = e.GetPosition(Background).Y
            //    };
            //    radius = Math.Sqrt(Math.Pow(e.GetPosition(Background).X - x, 2) + Math.Pow(e.GetPosition(Background).Y - y, 2));
            //    Btn1.IsEnabled = false;
            //    Btn2.IsEnabled = false;
            //    centerCircle = false;
            //    radiusCircle = false;
            //    line.SetValue(RenderOptions.EdgeModeProperty, EdgeMode.Aliased);
            //    Background.Children.Add(line);
            //    flag = true;
            //}

            //if (isMouseDown)
            //{
            //    double x = e.GetPosition(Background).X;
            //    double y = e.GetPosition(Background).Y;


            //    double coordX = count / Background.ActualWidth * (x - Background.ActualWidth / 2);
            //    double coordY = -1 * (countY / Background.ActualHeight) * (y - Background.ActualHeight / 2);
            //    double p = Math.Abs(Math.Round(Math.Atan(coordX / coordY), 2));


            //    if ((count / Background.ActualWidth * (this.x - Background.ActualWidth / 2)) <= coordX)
            //    {
            //        if ((-1 * (countY / Background.ActualHeight) * (this.y - Background.ActualHeight / 2)) <= coordY)
            //        { gradus = 90 - p * 180 / Math.PI; }
            //        else
            //        { gradus = 270 + p * 180 / Math.PI; }
            //    }
            //    else
            //    {
            //        if ((-1 * (countY / Background.ActualHeight) * (this.y - Background.ActualHeight / 2)) <= coordY)
            //        { gradus = 90 + p * 180 / Math.PI; }
            //        else { gradus = 270 - p * 180 / Math.PI; }
            //    }
            //    gradus = Math.Abs(gradus);
            //    double circleX = this.x + radius * Math.Cos(gradus);
            //    double circleY = this.y + radius * Math.Sin(gradus);
            //    el = new Ellipse()
            //    {
            //        Width = 5,
            //        Height = 5,
            //        Fill = Brushes.Black,
            //        Stroke = Brushes.Black,
            //        StrokeThickness = 1
            //    };
            //    Background.Children.Add(el);
            //    el.SetValue(Canvas.LeftProperty, circleX);
            //    el.SetValue(Canvas.TopProperty, circleY);
            //}

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
            return  Math.Abs(x)> Math.Abs(y) ? countX/2-Math.Abs(x) : countX / 2 - Math.Abs(y);
        }
    }
}
