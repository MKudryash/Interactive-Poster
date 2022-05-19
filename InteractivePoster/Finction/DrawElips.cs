using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
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

        double sinGradusElpis;
        double cosGradusElpis;
        double gradusValueElips, rH, rW;

        public DrawElips(double x, double y, double rW, double rH, Canvas cv, double gradusValueElips,Canvas canvas)
        {
            countX = Convert.ToDouble(canvas.Tag);//получаем масштабы области
            countY = Math.Round(canvas.ActualHeight / (canvas.ActualWidth / countX));
            maxX = canvas.ActualWidth; //получаем ширину канвы
            maxY = canvas.ActualHeight; //получаем высоту канвы     
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
                Stroke = new SolidColorBrush(Color.FromRgb(248, 94, 94)),
                StrokeThickness = 3

            };
            cv.Children.Add(circle);//помещаем на канву
                                    //в нужную точку канвы
            RotateTransform rotateTransform = new RotateTransform()
            {
                CenterX = radiusW,
                CenterY = radiusH,
                Angle = gradusValueElips
            };
            circle.RenderTransform = rotateTransform;



            circle.SetValue(Canvas.LeftProperty, convertX(x));
            circle.SetValue(Canvas.TopProperty, convertY(y));
            circle = new Ellipse()//задаем прочие параметры
            {
                Width = (maxX / countX) * 0.2,
                Height = (maxX / countX) * 0.2,
                Fill = new SolidColorBrush(Color.FromRgb(248, 94, 94)),
                Stroke = new SolidColorBrush(Color.FromRgb(248, 94, 94)),
                StrokeThickness = 1
            };

            cv.Children.Add(circle);//помещаем на канву
                                    //в нужную точку канвы
            circle.SetValue(Canvas.LeftProperty, convertCoordX(x - 0.1));
            circle.SetValue(Canvas.TopProperty, convertCoordY(y + 0.1));
            //вращение круга




            string text = "О( " + x.ToString("F1") + "; " + y.ToString("F1") + ")";
            TextBlock centre = DrawText(x, y, text);//Центр окружности
            cv.Children.Add(centre);

            PointRadius();
            FocusElips(rW, rH, gradusValueElips);//Отрисовка и подсчет Фокусов
            DrawRadius(rW, rH);//Отрисовка радиуса Элипса
        }

        private void PointRadius()
        {
            string text = "A1( " + (x + rW * cosGradusElpis).ToString("F1") + "; " + (y - rW * sinGradusElpis).ToString("F1") + ")";
            TextBlock A1 = DrawText(x + rW * cosGradusElpis, y - rW * sinGradusElpis, text);//Точка А1
            text = "A2( " + (x - rW * cosGradusElpis).ToString("F1") + "; " + (y + rW * sinGradusElpis).ToString("F1") + ")";
            TextBlock A2 = DrawText(x - rW * cosGradusElpis, y + rW * sinGradusElpis, text);//Точка А2
            text = "B1( " + (x - rH * sinGradusElpis).ToString("F1") + "; " + (y + rH * cosGradusElpis).ToString("F1") + ")";
            TextBlock B1 = DrawText(x + rH * sinGradusElpis, y + rH * cosGradusElpis, text);//Точка В1
            text = "B2( " + (x + rH * sinGradusElpis).ToString("F1") + "; " + (y - rH * cosGradusElpis).ToString("F1") + ")";
            TextBlock B2 = DrawText(x - rH * sinGradusElpis, y - rH * cosGradusElpis, text);//Точка В2
            if (MaxMinCoordinat.ElementElipseRadius)
            {
                cv.Children.Add(A1);
                cv.Children.Add(A2);
                cv.Children.Add(B1);
                cv.Children.Add(B2);
            }
            else
            {
                cv.Children.Remove(A1);
                cv.Children.Remove(A2);
                cv.Children.Remove(B1);
                cv.Children.Remove(B2);
            }
        }
        void DrawRadius(double rW, double rH)
        {
            double aa = Math.Atan(rH / rW * Math.Tan(convertRadian(MaxMinCoordinat.gradusValue)));

            if (MaxMinCoordinat.gradusValue <= 270 && MaxMinCoordinat.gradusValue > 90) { aa = aa + Math.PI; }


            double circleX = x + (rW * Math.Cos(aa)) * cosGradusElpis + (rH * Math.Sin(aa)) * sinGradusElpis;
            double circleY = y + (rH * Math.Sin(aa)) * cosGradusElpis - (rW * Math.Cos(aa)) * sinGradusElpis;

            line = new Line()
            {
                Stroke = new SolidColorBrush(Color.FromRgb(108, 165, 250)),
                StrokeThickness = 3,
                SnapsToDevicePixels = true,
                X1 = convertCoordX(x),
                X2 = convertCoordX(circleX),
                Y1 = convertCoordY(y),
                Y2 = convertCoordY(circleY),
            };
            line.SetValue(RenderOptions.EdgeModeProperty, EdgeMode.Aliased);
            string text = "M( " + circleX.ToString("F1") + "; " + circleY.ToString("F1") + ")";
            TextBlock textBlock = DrawText(circleX, circleY, text);

            if (MaxMinCoordinat.elementElipsPoint)
            {
                cv.Children.Add(line);
                cv.Children.Add(textBlock);
            }
            else
            {
                cv.Children.Remove(line);
                cv.Children.Remove(textBlock);
            }


        } //Орисовка радиуса + текст с точкой на окружности
        void FocusElips(double rW, double rH, double gradusValueElips)
        {
            TextBlock F1 = new TextBlock();
            TextBlock F2 = new TextBlock();
            Ellipse PointsOne = new Ellipse();
            Ellipse PointsTwo = new Ellipse();
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
                    PointsOne = DrawPoinFocus(x + c * cosGradusElpis, y - c * sinGradusElpis);
                    F1 = DrawText(x + c * cosGradusElpis, y - c * sinGradusElpis, text);
                    text = "F2( " + (x - c * cosGradusElpis).ToString("F1") + "; " + (y + c * sinGradusElpis).ToString("F1") + ")";
                    PointsTwo = DrawPoinFocus(x - c * cosGradusElpis, y + c * sinGradusElpis);
                    F2 = DrawText(x - c * cosGradusElpis, y + c * sinGradusElpis, text);
                    line = new Line
                    {
                        X1 = convertCoordX(x + rW * cosGradusElpis),
                        X2 = convertCoordX(x - rW * cosGradusElpis),
                        Y1 = convertCoordY(y - rW * sinGradusElpis),
                        Y2 = convertCoordY(y + rW * sinGradusElpis),
                        Stroke = new SolidColorBrush(Color.FromRgb(248, 94, 94)),
                        StrokeThickness = 2,
                        SnapsToDevicePixels = true
                    };

                    line.SetValue(RenderOptions.EdgeModeProperty, EdgeMode.Aliased);

                }
                else
                {
                    string text = "F2( " + (x - c * sinGradusElpis).ToString("F1") + "; " + (y - c * cosGradusElpis).ToString("F1") + ")";
                    PointsOne = DrawPoinFocus(x - c * sinGradusElpis, y - c * cosGradusElpis);
                    F1 = DrawText(x - c * sinGradusElpis, y - c * cosGradusElpis, text);
                    text = "F1( " + (x + c * sinGradusElpis).ToString("F1") + "; " + (y + c * cosGradusElpis).ToString("F1") + ")";
                    PointsTwo = DrawPoinFocus(x + c * sinGradusElpis, y + c * cosGradusElpis);
                    F2 = DrawText(x + c * sinGradusElpis, y + c * cosGradusElpis, text);
                    line = new Line
                    {
                        X1 = convertCoordX(x + rH * sinGradusElpis),
                        X2 = convertCoordX(x - rH * sinGradusElpis),
                        Y1 = convertCoordY(y + rH * cosGradusElpis),
                        Y2 = convertCoordY(y - rH * cosGradusElpis),
                        Stroke = new SolidColorBrush(Color.FromRgb(248, 94, 94)),
                        StrokeThickness = 2,
                        SnapsToDevicePixels = true
                    };
                    line.SetValue(RenderOptions.EdgeModeProperty, EdgeMode.Aliased);

                }

                if (MaxMinCoordinat.ElementElipseFocus)
                {
                    cv.Children.Add(line);
                    cv.Children.Add(PointsOne);
                    cv.Children.Add(PointsTwo);
                    cv.Children.Add(F1);
                    cv.Children.Add(F2);
                }
                else
                {
                    cv.Children.Remove(line);
                    cv.Children.Remove(PointsOne);
                    cv.Children.Remove(PointsTwo);
                    cv.Children.Remove(F1);
                    cv.Children.Remove(F2);
                }
            }
        }
        Ellipse PointFocus;

        Ellipse DrawPoinFocus(double x, double y)
        {

            PointFocus = new Ellipse()//задаем прочие параметры
            {
                Width = (maxX / countX) * 0.2,
                Height = (maxX / countX) * 0.2,
                Fill  = new SolidColorBrush(Color.FromRgb(248, 94, 94)),
                Stroke = new SolidColorBrush(Color.FromRgb(248, 94, 94)),
                StrokeThickness = 1
            };

            PointFocus.SetValue(Canvas.LeftProperty, convertCoordX(x - 0.1));
            PointFocus.SetValue(Canvas.TopProperty, convertCoordY(y + 0.1));
            return PointFocus;
        }
        public async void ChangedGradus(object sender, MouseEventArgs e)
        {
            double x = e.GetPosition(cv).X;
            double y = e.GetPosition(cv).Y;

            double coordX = convertXCoord(x);
            double coordY = convertYCoord(y);

            double p = Math.Atan((coordY - this.y) / (coordX - this.x));


            if (coordX < this.x) { p = p + Math.PI; }
            MaxMinCoordinat.gradusValue = (int)(180 / Math.PI * p);

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
            if (MaxMinCoordinat.SinCosEllipse)
            {
                switch (MaxMinCoordinat.equationforElips)
                {
                    case true:
                        {
                            if (x == 0 && y == 0) return @"\frac{("+Math.Round(Math.Cos(gradusValueElips),2)+ "x)-(" + Math.Round(Math.Sin(gradusValueElips), 2) + "y))^2}{" + rW.ToString("F1") +
                     @"^2}+ \frac{(" + Math.Round(Math.Sin(gradusValueElips), 2) + "y)+(" + Math.Round(Math.Cos(gradusValueElips), 2) + "x)))^2}{" + rH.ToString("F1") + @"^2} = 1";
                            if (y == 0) return @"\frac{(" + Math.Round(Math.Cos(gradusValueElips), 2) + "x)-(" + Math.Round(Math.Sin(gradusValueElips), 2) + "y))-(" + x.ToString("F1") + ")" + @"))^2}{" 
+ rW.ToString("F1") +
                      @"^2}+ \frac{(" + Math.Round(Math.Sin(gradusValueElips), 2) + "x)+(" + Math.Round(Math.Cos(gradusValueElips), 2) + "y)))^2}{" + rH.ToString("F1") + @"^2} = 1";
                            if (x == 0) return @"\frac{(" + Math.Round(Math.Cos(gradusValueElips), 2) + "x)-(" + Math.Round(Math.Sin(gradusValueElips), 2) + "y)))^2}{" + rW.ToString("F1") +
                     @"^2}+ \frac{(" + Math.Round(Math.Sin(gradusValueElips), 2) + "x)+(" + Math.Round(Math.Cos(gradusValueElips), 2) + "y))-(" + y.ToString("F1") + ")" + @"))^2}{" + rH.ToString("F1") + @"^2} = 1";
                            return @"\frac{(" + Math.Round(Math.Cos(gradusValueElips), 2) + "x)-(" + Math.Round(Math.Sin(gradusValueElips), 2) + "y))-(" + x.ToString("F1") + ")" + @"))^2}{" + rW.ToString("F1") +
                      @"^2}+ \frac{(" + Math.Round(Math.Sin(gradusValueElips), 2) + "x)+(" + Math.Round(Math.Cos(gradusValueElips), 2) + "y))-(" + y.ToString("F1") + ")" + @"))^2}{" + rH.ToString("F1") + @"^2} = 1";
                        }

                    case false:
                        if (x == 0 && y == 0) return @"\frac{x^2}{" + rW.ToString("F1") + @"^2}+ \frac{y^2}{" + rH.ToString("F1") + @"^2} = 1";
                        if (y == 0) return @"\frac{(x-(" + x.ToString("F1") + @"))^2}{" + rW.ToString("F1") + @"^2}+ \frac{y^2}{" + rH.ToString("F1") + @"^2} = 1";
                        if (x == 0) return @"\frac{x^2}{" + rW.ToString("F1") + @"^2}+ \frac{(y-(" + y.ToString("F1") + @"))^2}{" + rH.ToString("F1") + @"^2} = 1";

                        return @"\frac{(x-(" + x.ToString("F1") + @"))^2}{" + rW.ToString("F1") + @"^2}+ \frac{(y-(" + y.ToString("F1") + @"))^2}{" + rH.ToString("F1") + @"^2} = 1";
                    default:
                        return " ";
                }
            }
            else
            {
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
                        if (y == 0) return @"\frac{(x-(" + x.ToString("F1") + @"))^2}{" + rW.ToString("F1") + @"^2}+ \frac{y^2}{" + rH.ToString("F1") + @"^2} = 1";
                        if (x == 0) return @"\frac{x^2}{" + rW.ToString("F1") + @"^2}+ \frac{(y-(" + y.ToString("F1") + @"))^2}{" + rH.ToString("F1") + @"^2} = 1";

                        return @"\frac{(x-(" + x.ToString("F1") + @"))^2}{" + rW.ToString("F1") + @"^2}+ \frac{(y-(" + y.ToString("F1") + @"))^2}{" + rH.ToString("F1") + @"^2} = 1";
                    default:
                        return " ";
                }
            }

        }
    }
}
