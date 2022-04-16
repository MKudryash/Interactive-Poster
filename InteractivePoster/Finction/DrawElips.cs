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
        double gradusValueElips,rH,rW;

        public DrawElips(double x, double y, double rW, double rH, Canvas cv, double gradusValueElips)
        {
            countX = Convert.ToDouble(cv.Tag);//получаем масштабы области
            countY = Math.Round(cv.ActualHeight / (cv.ActualWidth / countX));
            maxX = cv.ActualWidth; //получаем ширину канвы
            maxY = cv.ActualHeight; //получаем высоту канвы     
            radiusW = rW * (maxX / countX);//преобразуем радиус из декартовой системы
            radiusH = rH * (maxY / countY);//преобразуем радиус из декартовой системы
            this.cv = cv;
            this.x = x;
            this.y = y;
            this.rH = rH;
            this.rW = rW;
            this.gradusValueElips = gradusValueElips;
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
            RotateTransform rotateTransform = new RotateTransform() { 
            CenterX = radiusW,
            CenterY = radiusH,
            Angle = gradusValueElips};
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


            FocusElips(rW, rH, gradusValueElips);//Отрисовка и подсчет Фокусов
            DrawRadius(rW, rH);//Отрисовка радиуса Элипса
        }
      

        void DrawRadius(double rW, double rH)
        {
            double aa = Math.Atan(rH/rW * Math.Tan(convertRadian(MaxMinCoordinat.gradusValue)));

            if (MaxMinCoordinat.gradusValue <= 270 && MaxMinCoordinat.gradusValue > 90) { aa = aa + Math.PI; }           
            

            double circleX = x + (rW * Math.Sin(aa)) * cosGradusElpis + (rH * Math.Cos(aa)) * sinGradusElpis;
            double circleY = y + (rH * Math.Cos(aa)) * cosGradusElpis - (rW * Math.Sin(aa)) * sinGradusElpis;

            line = new Line()
            {
                Stroke = Brushes.Black,
                StrokeThickness = 3,
                SnapsToDevicePixels = true,
                X1 = convertCoordX(x),
                X2 = convertCoordX(circleX),
                Y1 = convertCoordY(y),
                Y2 = convertCoordY(circleY),
            };
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
                //при равных радиусах получается круг, поэтому фокусы не считаются
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
                    line = new Line
                    {
                        X1 = convertCoordX(x + rW * cosGradusElpis),
                        X2 = convertCoordX(x - rW * cosGradusElpis),
                        Y1 = convertCoordY(y - rW * sinGradusElpis),
                        Y2 = convertCoordY(y + rW * sinGradusElpis),
                        Stroke = Brushes.Black,
                        StrokeThickness = 2,
                        SnapsToDevicePixels = true
                    };
                   
                    line.SetValue(RenderOptions.EdgeModeProperty, EdgeMode.Aliased);
                    cv.Children.Add(line);

                }
                else
                {
                    string text = "F2( " + (x - c * sinGradusElpis).ToString("F1") + "; " + (y - c * cosGradusElpis).ToString("F1") + ")";
                    DrawPoinFocus(x - c * sinGradusElpis, y - c * cosGradusElpis);
                    DrawText(x - c * sinGradusElpis, y - c * cosGradusElpis, text);
                    text = "F1( " + (x + c * sinGradusElpis).ToString("F1") + "; " + (y + c * cosGradusElpis).ToString("F1") + ")";
                    DrawPoinFocus(x + c * sinGradusElpis, y + c * cosGradusElpis);
                    DrawText(x + c * sinGradusElpis, y + c * cosGradusElpis, text);
                    line = new Line
                    {
                        X1 = convertCoordX(x + rH * sinGradusElpis),
                        X2 = convertCoordX(x - rH * sinGradusElpis),
                        Y1 = convertCoordY(y + rH * cosGradusElpis),
                        Y2 = convertCoordY(y - rH * cosGradusElpis),
                        Stroke = Brushes.Black,
                        StrokeThickness = 2,
                        SnapsToDevicePixels = true
                    };
                    line.SetValue(RenderOptions.EdgeModeProperty, EdgeMode.Aliased);
                    cv.Children.Add(line);

                }
               
            }
        }
        Ellipse PointFocus;
        void DrawPoinFocus(double x, double y)
        {

            PointFocus = new Ellipse()//задаем прочие параметры
            {
                Width = (maxX / countX) * 0.2,
                Height = (maxX / countX) * 0.2,
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
            return maxX / 2 + x * (maxX / countX) - radiusW;
        }
        /// <summary>
        /// метод для преобразования координаты Y их канвы в декартово значение
        /// </summary>
        /// <param name="y">декартова координата (как нам надо с точки зрения математики)</param>
        /// <returns>актуальная координата Y на канве</returns>
        public double convertY(double y)
        {
            return maxY / 2 + y / -1 * (maxY / countY) - radiusH;
        }
        /// метод для подсчета фокусов
        public double FindFocusElips(double x, double y)
        {
            return x > y ? Math.Sqrt(Math.Pow(x, 2) - Math.Pow(y, 2)) : Math.Sqrt(Math.Pow(y, 2) - Math.Pow(x, 2));
        }
        /// метод для перевода в радианы
        public double convertRadian(double r)
        {
            return r / 180 * Math.PI;
        }
        public string CanonicalEquation()
        {
          
            if (x == 0 || y == 0) MaxMinCoordinat.equationforElips = false;
            if (gradusValueElips > 0)
            { MaxMinCoordinat.equationforElips = true; }
            else
            {
                MaxMinCoordinat.equationforElips = false;
            }
            switch (MaxMinCoordinat.equationforElips)
            {
                case true:
                    {
                        if (x == 0 && y == 0) return @"\frac{(x*cos(" + gradusValueElips.ToString() + ")-y*sin(" + gradusValueElips.ToString() + "))^2}{" + rW.ToString("F1") +
                 @"^2}+ \frac{(y*sin(" + gradusValueElips.ToString() + ")+y*cos(" + gradusValueElips.ToString() + "))^2}{" + rH.ToString("F1") + @"^2} = 1";
                        if (y == 0) return @"\frac{(x*cos(" + gradusValueElips.ToString() + ")-y*sin(" + gradusValueElips.ToString() + ")-(" + x.ToString("F1") + ")" + @"))^2}{" + rW.ToString("F1") +
                  @"^2}+ \frac{(y*sin(" + gradusValueElips.ToString() + ")+y*cos(" + gradusValueElips.ToString() + "))^2}{" + rH.ToString("F1") + @"^2} = 1";
                        if (x == 0) return @"\frac{(x*cos(" + gradusValueElips.ToString() + ")-y*sin(" + gradusValueElips.ToString() + "))^2}{" + rW.ToString("F1") +
                 @"^2}+ \frac{(y*sin(" + gradusValueElips.ToString() + ")+y*cos(" + gradusValueElips.ToString() + ")-(" + y.ToString("F1") + ")" + @"))^2}{" + rH.ToString("F1") + @"^2} = 1";
                        return @"\frac{(x*cos(" + gradusValueElips.ToString() + ")-y*sin(" + gradusValueElips.ToString() + ")-(" + x.ToString("F1") + ")" + @"))^2}{" + rW.ToString("F1") +
                  @"^2}+ \frac{(y*sin(" + gradusValueElips.ToString() + ")+y*cos(" + gradusValueElips.ToString() + ")-(" + y.ToString("F1") + ")" + @"))^2}{" + rH.ToString("F1") + @"^2} = 1";
                    }

                case false:
                    if (x == 0 && y == 0) return @"\frac{x^2}{" + rW.ToString("F1") + @"^2}+ \frac{y^2}{" + rH.ToString("F1") + @"^2} = 1";
                    if (y == 0)  return @"\frac{(x-(" + x.ToString("F1") + @"))^2}{" + rW.ToString("F1") + @"^2}+ \frac{y^2}{" + rH.ToString("F1") + @"^2} = 1";                  
                    if (x == 0) return @"\frac{x^2}{" + rW.ToString("F1") + @"^2}+ \frac{(y-(" + y.ToString("F1") + @"))^2}{" + rH.ToString("F1") + @"^2} = 1";
                   
                    return @"\frac{(x-(" + x.ToString("F1") + @"))^2}{" + rW.ToString("F1") + @"^2}+ \frac{(y-(" + y.ToString("F1") + @"))^2}{" + rH.ToString("F1") + @"^2} = 1";
                default:
                    return " ";   
            }
        }
    }
}
