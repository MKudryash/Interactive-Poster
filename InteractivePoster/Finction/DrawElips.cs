using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace InteractivePoster.Finction
{
    class DrawElips : GeometricPatterns
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

        double radiusW;
        double radiusH;
        double x;
        double y;

        double sinGradusElpis;
        double cosGradusElpis;
        Canvas cv;
        public DrawElips(double x, double y, double rW, double rH, Canvas cv, double gradusValueElips)
        {
            count = Convert.ToDouble(cv.Tag);//получаем масштабы области
            maxX = cv.ActualWidth; //получаем ширину канвы       
            radiusW = rW * (maxX / count);//преобразуем радиус из декартовой системы
            radiusH = rH * (maxX / count);//преобразуем радиус из декартовой системы
            this.cv = cv;
            this.x = x;
            this.y = y;
            sinGradusElpis = Math.Sin(convertRadian(gradusValueElips));
            cosGradusElpis = Math.Cos(convertRadian(gradusValueElips));


            circle = new Ellipse()//задаем прочие параметры
            {
                Width = 2 * radiusW,//ширина и длина по сути равна диаметру окружности
                Height = 2 * radiusH,
                Stroke = Brushes.Black,
                StrokeThickness = 3

            };
            //вращение круга
            RotateTransform rotateTransform = new RotateTransform();
            rotateTransform.CenterX = radiusW;//центр оси X по отношению к кругу, не к координатной плоскости
            rotateTransform.CenterY = radiusH;//центр оси Y по отношению к кругу, не к координатной плоскости
            rotateTransform.Angle = gradusValueElips;//поворот на количетсво градусов     
            circle.RenderTransform = rotateTransform;


            cv.Children.Add(circle);//помещаем на канву
                                    //в нужную точку канвы
            circle.SetValue(Canvas.LeftProperty, convertX(x));
            circle.SetValue(Canvas.TopProperty, convertY(y));



            string text = "О( " + x.ToString("F1") + "; " + y.ToString("F1") + ")";
            DrawText(x, y, text);//Центр окружности
            text = "A1( " + (x + rW * cosGradusElpis).ToString("F1") + "; " + (y - rW * sinGradusElpis).ToString("F1") + ")";
            DrawText(x + rW * cosGradusElpis, y - rW * sinGradusElpis, text);//Точка А1
            text = "A2( " + (x - rW * cosGradusElpis).ToString("F1") + "; " + (y + rW * sinGradusElpis).ToString("F1") + ")";
            DrawText(x - rW * cosGradusElpis, y + rW * sinGradusElpis, text);//Точка А2
            text = "B1( " + (x - rH * sinGradusElpis).ToString("F1") + "; " + (y + rH * cosGradusElpis).ToString("F1") + ")";
            DrawText(x + rH * sinGradusElpis, y + rH * cosGradusElpis, text);//Точка В1
            text = "B2( " + (x + rH * sinGradusElpis).ToString("F1") + "; " + (y - rH * cosGradusElpis).ToString("F1") + ")";
            DrawText(x - rH * sinGradusElpis, y - rH * cosGradusElpis, text);//Точка В2


            FocusElips(rW, rH, gradusValueElips);
            DrawRadius(rW, rH, gradusValueElips);
        }
        void DrawText(double x, double y, string s)
        {
            TextBlock TB = new TextBlock();
            TB.Text = s;
            cv.Children.Add(TB);
            TB.SetValue(Canvas.LeftProperty, convertCoordX(x));
            TB.SetValue(Canvas.TopProperty, convertCoordY(y));
            TB.TextWrapping = System.Windows.TextWrapping.Wrap;
            TB.Width = 120;
        } //Текст c содержанием точек

        void DrawRadius(double rW, double rH, double gradusValueElips)
        {

            double circleX = x + (rW * Math.Sin(convertRadian(MaxMinCoordinat.gradusValue))) * cosGradusElpis + (rH * Math.Cos(convertRadian(MaxMinCoordinat.gradusValue))) * sinGradusElpis;
            double circleY = y + (rH * Math.Cos(convertRadian(MaxMinCoordinat.gradusValue))) * cosGradusElpis - (rW * Math.Sin(convertRadian(MaxMinCoordinat.gradusValue))) * sinGradusElpis;

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
        void FocusElips(double rW, double rH, double gradusValueElips)
        {
            double c = FindFocusElips(rW, rH);
            if (rW == rH)
            {

            }
            else
            {
                if (rW > rH)
                {
                    string text = "F1( " + (x + c * cosGradusElpis).ToString("F1") + "; " + (y - c * sinGradusElpis).ToString("F1") + ")";
                    DrawPoinFocus(x + c * cosGradusElpis, y - c * sinGradusElpis);
                    DrawText(x + c * cosGradusElpis, y - c * sinGradusElpis, text);
                    text = "F2( " + (x - c * cosGradusElpis).ToString("F1") + "; " + (y + c * sinGradusElpis).ToString("F1") + ")";
                    DrawPoinFocus(x - c * cosGradusElpis, y + c * sinGradusElpis);
                    DrawText(x - c * cosGradusElpis, y + c * sinGradusElpis, text);
                }
                else
                {
                    string text = "F2( " + (x - c * sinGradusElpis).ToString("F1") + "; " + (y - c * cosGradusElpis).ToString("F1") + ")";
                    DrawPoinFocus(x - c * sinGradusElpis, y - c * cosGradusElpis);
                    DrawText(x - c * sinGradusElpis, y - c * cosGradusElpis, text);
                    text = "F1( " + (x + c * sinGradusElpis).ToString("F1") + "; " + (y + c * cosGradusElpis).ToString("F1") + ")";
                    DrawPoinFocus(x + c * sinGradusElpis, y + c * cosGradusElpis);
                    DrawText(x + c * sinGradusElpis, y + c * cosGradusElpis, text);


                }
            }
        }
        Ellipse PointFocus;
        void DrawPoinFocus(double x, double y)
        {

            PointFocus = new Ellipse()//задаем прочие параметры
            {
                Width = (maxX / count) * 0.2,
                Height = (maxX / count) * 0.2,
                Fill = Brushes.Black,
                Stroke = Brushes.Black,
                StrokeThickness = 1
            };

            cv.Children.Add(PointFocus);//помещаем на канву
                                        //в нужную точку канвы
            PointFocus.SetValue(Canvas.LeftProperty, convertCoordX(x - 0.1));
            PointFocus.SetValue(Canvas.TopProperty, convertCoordY(y + 0.1));
        }

        /// <summary>
        /// метод для преобразования координаты Х их канвы в декартово значение
        /// </summary>
        /// <param name="x">декартова координата (как нам надо с точки зрения математики)</param>
        /// <returns>актуальная координата Х на канве</returns>
        public double convertX(double x)
        {
            //радиус вычитаем, т.к. по умолчанию передаются координаты левого верхнего угла
            return maxX / 2 + x * (maxX / count) - radiusW;
        }
        /// <summary>
        /// метод для преобразования координаты Y их канвы в декартово значение
        /// </summary>
        /// <param name="y">декартова координата (как нам надо с точки зрения математики)</param>
        /// <returns>актуальная координата Y на канве</returns>
        public double convertY(double y)
        {
            return maxX / 2 + y / -1 * (maxX / count) - radiusH;
        }

        public double FindFocusElips(double x, double y)
        {
            return x > y ? Math.Sqrt(Math.Pow(x, 2) - Math.Pow(y, 2)) : Math.Sqrt(Math.Pow(y, 2) - Math.Pow(x, 2));
        }

        public double convertRadian(double r)
        {
            return r / 180 * Math.PI;
        }
    }
}
