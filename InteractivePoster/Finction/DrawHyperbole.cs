
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
    class DrawHyperbole : GeometricPatterns
    {
        double rectangleA;
        double rectangleB;
        double sinGradusHyperbole, gradusTransform;
        double cosGradusHyperbole;
        Rectangle rectangle;
        TextBlock textBlock;
        Line line;
        double a, b;
        public DrawHyperbole(double x, double y, double a, double b, Canvas cv, double gradusTransform, double pointRight, double pointLeft, Canvas canvas)
        {
            this.x = x;
            this.y = y;
            this.a = a;
            this.b = b;
            this.cv = cv;
            this.gradusTransform = gradusTransform;

            sinGradusHyperbole = Math.Sin(convertRadian(gradusTransform));
            cosGradusHyperbole = Math.Cos(convertRadian(gradusTransform));

            countX = Convert.ToDouble(canvas.Tag);//получаем масштабы области
            countY = Math.Round(canvas.ActualHeight / (canvas.ActualWidth / countX));
            maxX = canvas.ActualWidth; //получаем ширину канвы
            maxY = canvas.ActualHeight; //получаем высоту канвы        
            rectangleA = a * (maxX / countX);//преобразуем ширину прямоугольника из декартовой системы
            rectangleB = b * (maxY / countY);//преобразуем высоту прямоугольника из декартовой системы
            DrawRectangle(x, y, gradusTransform);
            FocusHyperbole();
            DrawAsymptotes(gradusTransform);

            MovePointRight(pointRight);
            MovePointLeft(pointLeft);

            CalculationPoinHyperbole();

        }

        void MovePointLeft(double y)
        {
            double mX = 0, mY = 0, xx, yy;
            if (y <= 0)
            {
                xx = (Math.Abs(y) + a) * -1;
                yy = Math.Sqrt(Math.Pow(b, 2) * (Math.Round((Math.Pow(Math.Abs(xx), 2) / (a * a)), 2) - 1));
                mX = xx * cosGradusHyperbole + yy * sinGradusHyperbole + this.x;
                mY = -xx * sinGradusHyperbole + yy * cosGradusHyperbole + this.y;
            }
            else
            {
                xx = (y + a) * -1;
                yy = Math.Sqrt(Math.Pow(b, 2) * (Math.Round((Math.Pow(xx, 2) / (a * a)), 2) - 1)) * -1;
                mX = xx * cosGradusHyperbole + yy * sinGradusHyperbole + this.x;
                mY = -xx * sinGradusHyperbole + yy * cosGradusHyperbole + this.y;
            }

            textBlock = DrawText(mX, mY, "M( " + (mX).ToString("F1") + "; " + (mY).ToString("F1") + ")");
            PointFocus = new Ellipse()//задаем прочие параметры
            {
                Width = (maxX / countX) * 0.2,
                Height = (maxX / countX) * 0.2,
                Fill = colorFigure,
                Stroke = colorFigure,
                StrokeThickness = 1
            };
            if (MaxMinCoordinat.ElementHyperbolePoint)
            {
                cv.Children.Add(PointFocus);//помещаем на канву
                cv.Children.Add(textBlock);
            }
            else
            {
                cv.Children.Remove(PointFocus);//помещаем на канву
                cv.Children.Remove(textBlock);
            }

            PointFocus.SetValue(Canvas.LeftProperty, convertCoordX(mX - 0.1));
            PointFocus.SetValue(Canvas.TopProperty, convertCoordY(mY + 0.1));
        }
        void MovePointRight(double y)
        {
            double mX = 0, mY = 0, xx, yy;
            if (y <= 0)
            {
                xx = Math.Abs(y) + a;
                yy = Math.Sqrt(Math.Pow(b, 2) * (Math.Round((Math.Pow(Math.Abs(xx), 2) / (a * a)), 2) - 1));
                mX = xx * cosGradusHyperbole + yy * sinGradusHyperbole + this.x;
                mY = -xx * sinGradusHyperbole + yy * cosGradusHyperbole + this.y;
            }
            else
            {
                xx = y + a;
                yy = Math.Sqrt(Math.Pow(b, 2) * (Math.Round((Math.Pow(xx, 2) / (a * a)), 2) - 1)) * -1;
                mX = xx * cosGradusHyperbole + yy * sinGradusHyperbole + this.x;
                mY = -xx * sinGradusHyperbole + yy * cosGradusHyperbole + this.y;
            }

            textBlock = DrawText(mX, mY, "N( " + (mX).ToString("F1") + "; " + (mY).ToString("F1") + ")");
            PointFocus = new Ellipse()//задаем прочие параметры
            {
                Width = (maxX / countX) * 0.2,
                Height = (maxX / countX) * 0.2,
                Fill = colorFigure,
                Stroke = colorFigure,
                StrokeThickness = 1
            };

            if (MaxMinCoordinat.ElementHyperbolePoint)
            {
                cv.Children.Add(PointFocus);//помещаем на канву
                cv.Children.Add(textBlock);
            }
            else
            {
                cv.Children.Remove(PointFocus);//помещаем на канву
                cv.Children.Remove(textBlock);
            }
            PointFocus.SetValue(Canvas.LeftProperty, convertCoordX(mX - 0.1));
            PointFocus.SetValue(Canvas.TopProperty, convertCoordY(mY + 0.1));
        }
        void DrawAsymptotes(double gradusTransform)
        {
            line = new Line()
            {
                Stroke = colorTools,
                StrokeThickness = 2,
                StrokeDashArray = { 4, 3 },
                SnapsToDevicePixels = true,
                X1 = convertCoordX(countX / 2 * -1 + x),
                X2 = convertCoordX(countX / 2 + x),
                Y1 = convertCoordY(b / a * countX / 2 * -1 + y),
                Y2 = convertCoordY(b / a * countX / 2 + y),
            };
            RotateTransform rotateTransform = new RotateTransform();
            rotateTransform.CenterX = maxX / 2 + x * (maxX / countX); //центр оси X по отношению к параболе, не к координатной плоскости
            rotateTransform.CenterY = maxY / 2 + y * (-1) * (maxY / countY);//центр оси Y по отношению к параболе, не к координатной плоскости
            rotateTransform.Angle = gradusTransform;//поворот на количетсво градусов  
            line.RenderTransform = rotateTransform;
            line.SetValue(RenderOptions.EdgeModeProperty, EdgeMode.Aliased);

            if (MaxMinCoordinat.ElementHyperboleAssim)
            {
                cv.Children.Add(line);
            }
            else
            {
                cv.Children.Remove(line);
            }
            line = new Line()
            {
                Stroke = colorTools,
                StrokeThickness = 2,
                StrokeDashArray = { 4, 3 },
                SnapsToDevicePixels = true,
                X1 = convertCoordX((countX / 2) * -1 + x),
                X2 = convertCoordX((countX / 2 + x)),
                Y2 = convertCoordY((b / a * countX / 2) * -1 + y),
                Y1 = convertCoordY(b / a * countX / 2 + y),
            };
            line.RenderTransform = rotateTransform;
            line.SetValue(RenderOptions.EdgeModeProperty, EdgeMode.Aliased);

            if (MaxMinCoordinat.ElementHyperboleAssim)
            {
                cv.Children.Add(line);
            }
            else
            {
                cv.Children.Remove(line);
            }
        }

        void CalculationPoinHyperbole()
        {
            int razmer = (int)(countX / 2 - a);
            double h = 0.1;
            razmer = razmer == 0 ? 1 : razmer;
            double[,] massPoint = new double[razmer * 10 * 2 + 1, 2];
            int countforPoint = 0;
            double pointX = razmer * -1;
            double pointXTwo = 0;

            for (double i = 0; i < razmer * 10 + 1; i++)
            {
                pointX = Math.Round(pointX, 2);
                massPoint[countforPoint, 0] = Math.Round(pointX + x, 2) - a;
                massPoint[countforPoint, 1] = Math.Sqrt(Math.Pow(b, 2) * (Math.Round((Math.Pow(pointX - a, 2) / (a * a)), 2) - 1)) + y;
                massPoint[countforPoint + razmer * 10, 0] = Math.Round(pointXTwo + x, 2) - a;
                massPoint[countforPoint + razmer * 10, 1] = Math.Sqrt(Math.Pow(b, 2) * (Math.Round((Math.Pow(pointXTwo - a, 2) / (a * a)), 2) - 1)) * -1 + y;
                pointXTwo -= h;
                pointX += h;
                countforPoint++;
            }
            DrawByPoints(massPoint);
            massPoint = new double[razmer * 10 * 2 + 1, 2];
            pointX = razmer * -1;
            countforPoint = 0;
            pointXTwo = 0;
            for (double i = 0; i < razmer * 10 + 1; i++)
            {
                pointX = Math.Round(pointX, 2);
                massPoint[countforPoint, 0] = Math.Round(Math.Abs(pointX) + x, 2) + a;
                massPoint[countforPoint, 1] = Math.Sqrt(Math.Pow(b, 2) * (Math.Round((Math.Pow(Math.Abs(pointX) + a, 2) / (a * a)), 2) - 1)) + y;
                massPoint[countforPoint + razmer * 10, 0] = Math.Round(pointXTwo + x, 2) + a;
                massPoint[countforPoint + razmer * 10, 1] = Math.Sqrt(Math.Pow(b, 2) * (Math.Round((Math.Pow(pointXTwo + a, 2) / (a * a)), 2) - 1)) * -1 + y;
                pointX += h;
                pointXTwo += h;
                countforPoint++;
            }
            DrawByPoints(massPoint);
        }
        void DrawByPoints(double[,] massPoint)
        {
            Canvas canvaFotPoint = new Canvas();
            int countForPoint = 0;
            for (double i = 0; i < massPoint.Length / 2 - 1; i++)
            {
                line = new Line()
                {
                    X1 = convertCoordX(massPoint[countForPoint, 0]),
                    Y1 = convertCoordY(massPoint[countForPoint, 1]),
                    X2 = convertCoordX(massPoint[countForPoint + 1, 0]),
                    Y2 = convertCoordY(massPoint[countForPoint + 1, 1]),
                    Stroke = colorFigure,
                    StrokeThickness = 3
                };
                canvaFotPoint.Children.Add(line);

                countForPoint++;
                RotateTransform rotateTransform = new RotateTransform();
                rotateTransform.CenterX = maxX / 2 + x * (maxX / countX);
                rotateTransform.CenterY = maxY / 2 + y * (-1) * (maxY / countY);
                rotateTransform.Angle = gradusTransform;
                canvaFotPoint.RenderTransform = rotateTransform;
            }
            cv.Children.Add(canvaFotPoint);

        }

        void DrawRectangle(double x, double y, double gradusTransform)
        {
            RotateTransform rotateTransform = new RotateTransform() //вращение прямоугольника
            {
                CenterX = rectangleA,
                CenterY = rectangleB,
                Angle = gradusTransform
            };

            rectangle = new Rectangle()
            {
                Width = 2 * rectangleA,//ширина и длина по сути равна диаметру окружности
                Height = 2 * rectangleB,
                Stroke = colorTools,
                StrokeThickness = 2,
                StrokeDashArray = { 4, 3 },
                RenderTransform = rotateTransform
            };

            rectangle.SetValue(Canvas.LeftProperty, convertX(x));
            rectangle.SetValue(Canvas.TopProperty, convertY(y));
            if (MaxMinCoordinat.ElementHyperboleRectangle)
            {
                cv.Children.Add(rectangle);
            }
            else
            {
                cv.Children.Remove(rectangle);
            }
        }

        void FocusHyperbole()
        {

            double c = Math.Sqrt(Math.Pow(a, 2) + Math.Pow(b, 2));

            string text = "F1( " + (x + c * cosGradusHyperbole).ToString("F1") + "; " + (y - c * sinGradusHyperbole).ToString("F1") + ")";
            Ellipse pointF1 = DrawPoinFocus(x + c * cosGradusHyperbole, y - c * sinGradusHyperbole);
            TextBlock F1 = DrawText(x + c * cosGradusHyperbole, y - c * sinGradusHyperbole, text);
            text = "F2( " + (x - c * cosGradusHyperbole).ToString("F1") + "; " + (y + c * sinGradusHyperbole).ToString("F1") + ")";
            Ellipse pointF2 = DrawPoinFocus(x - c * cosGradusHyperbole, y + c * sinGradusHyperbole);
            TextBlock F2 = DrawText(x - c * cosGradusHyperbole, y + c * sinGradusHyperbole, text);

            if (MaxMinCoordinat.ElementHyperboleFocus)
            {
                cv.Children.Add(pointF1);
                cv.Children.Add(pointF2);
                cv.Children.Add(F1);
                cv.Children.Add(F2);
            }
            else
            {
                cv.Children.Remove(pointF1);
                cv.Children.Remove(pointF2);
                cv.Children.Remove(F1);
                cv.Children.Remove(F2);
            }
        }
        Ellipse PointFocus;
        private Ellipse DrawPoinFocus(double x, double y)
        {

            PointFocus = new Ellipse()//задаем прочие параметры
            {
                Width = (maxX / countX) * 0.2,
                Height = (maxX / countX) * 0.2,
                Fill = colorFigure,
                Stroke = colorFigure,
                StrokeThickness = 1
            };
            PointFocus.SetValue(Canvas.LeftProperty, convertCoordX(x - 0.1));
            PointFocus.SetValue(Canvas.TopProperty, convertCoordY(y + 0.1));
            return PointFocus;
        }

        /// <summary>
        /// метод для преобразования координаты Х их канвы в декартово значение
        /// </summary>
        /// <param name="x">декартова координата (как нам надо с точки зрения математики)</param>
        /// <returns>актуальная координата Х на канве</returns>
        public double convertX(double x)
        {
            //радиус вычитаем, т.к. по умолчанию передаются координаты левого верхнего угла
            return maxX / 2 + x * (maxX / countX) - rectangleA;
        }
        /// <summary>
        /// метод для преобразования координаты Y их канвы в декартово значение
        /// </summary>
        /// <param name="y">декартова координата (как нам надо с точки зрения математики)</param>
        /// <returns>актуальная координата Y на канве</returns>
        public double convertY(double y)
        {
            return maxY / 2 + y / -1 * (maxY / countY) - rectangleB;
        }
        /// метод для преобразования градусов в радианы
        public double convertRadian(double r)
        {
            return r / 180 * Math.PI;
        }
        public string CanonicalEquation()
        {
            if (x == 0 || y == 0) MaxMinCoordinat.equationforHyperbole = false;
            if (gradusTransform > 0)
            { MaxMinCoordinat.equationforHyperbole = true; }
            else
            {
                MaxMinCoordinat.equationforHyperbole = false;
            }
            if (MaxMinCoordinat.SinCosHyperbole)
            {
                switch (MaxMinCoordinat.equationforHyperbole)
                {
                    case true:
                        if (x == 0 && y == 0) return @"\frac{(" + Math.Round(Math.Cos(gradusTransform), 2) + "x)-(" + Math.Round(Math.Sin(gradusTransform), 2) + "y)))^2}{" + a.ToString("F1") +
                    @"^2}+ \frac{(" + Math.Round(Math.Sin(gradusTransform), 2) + "x)+(" + Math.Round(Math.Cos(gradusTransform), 2) + "y)))^2}{" + b.ToString("F1") + @"^2} = 1";
                        if (y == 0) return @"\frac{(" + Math.Round(Math.Cos(gradusTransform), 2) + "x)-(" + Math.Round(Math.Sin(gradusTransform), 2) + "y))-("
                                + x.ToString("F1") + @"))^2}{" + a.ToString("F1") +
                    @"^2}+ \frac{(" + Math.Round(Math.Sin(gradusTransform), 2) + "x)+(" + Math.Round(Math.Cos(gradusTransform), 2) + "y)))^2}{" + b.ToString("F1") + @"^2} = 1";
                        if (x == 0) return @"\frac{(" + Math.Round(Math.Cos(gradusTransform), 2) + "x)-(" + Math.Round(Math.Sin(gradusTransform), 2) + "y)))^2}{" + a.ToString("F1") +
                    @"^2}+ \frac{(" + Math.Round(Math.Sin(gradusTransform), 2) + "x)+(" + Math.Round(Math.Cos(gradusTransform), 2) + "y))-(" + y.ToString("F1") + @"))^2}{" + b.ToString("F1") + @"^2} = 1";
                        return @"\frac{(" + Math.Round(Math.Cos(gradusTransform), 2) + "x)-(" + Math.Round(Math.Sin(gradusTransform), 2) + "y))-(" + x.ToString("F1") + @"))^2}{" + a.ToString("F1") +
                    @"^2}+ \frac{(" + Math.Round(Math.Sin(gradusTransform), 2) + "x)+" + Math.Round(Math.Cos(gradusTransform), 2) + "y))-(" + y.ToString("F1") + @"))^2}{" + b.ToString("F1") + @"^2} = 1";
                    case false:
                        if (x == 0 && y == 0) return @"\frac{x^2}{" + a.ToString("F1") + @"^2}+ \frac{y^2}{" + b.ToString("F1") + @"^2} = 1";
                        if (y == 0) return @"\frac{(x-(" + x.ToString("F1") + @"))^2}{" + a.ToString("F1") + @"^2}+ \frac{y^2}{" + b.ToString("F1") + @"^2} = 1";
                        if (x == 0) return @"\frac{x^2}{" + a.ToString("F1") + @"^2}+ \frac{(y-(" + y.ToString("F1") + @"))^2}{" + b.ToString("F1") + @"^2} = 1";
                        return @"\frac{(x-(" + x.ToString("F1") + @"))^2}{" + a.ToString("F1") + @"^2}+ \frac{(y-(" + y.ToString("F1") + @"))^2}{" + b.ToString("F1") + @"^2} = 1";
                    default:
                        return " ";
                }
            }
            else
            {
                switch (MaxMinCoordinat.equationforHyperbole)
                {
                    case true:
                        if (x == 0 && y == 0) return @"\frac{(x*cos(" + gradusTransform.ToString() + ")-y*sin(" + gradusTransform.ToString() + "))^2}{" + a.ToString("F1") +
                    @"^2}+ \frac{(x*sin(" + gradusTransform.ToString() + ")+y*cos(" + gradusTransform.ToString() + "))^2}{" + b.ToString("F1") + @"^2} = 1";
                        if (y == 0) return @"\frac{(x*cos(" + gradusTransform.ToString() + ")-y*sin(" + gradusTransform.ToString() + ")-(" + x.ToString("F1") + @"))^2}{" + a.ToString("F1") +
                    @"^2}+ \frac{(x*sin(" + gradusTransform.ToString() + ")+y*cos(" + gradusTransform.ToString() + "))^2}{" + b.ToString("F1") + @"^2} = 1";
                        if (x == 0) return @"\frac{(x*cos(" + gradusTransform.ToString() + ")-y*sin(" + gradusTransform.ToString() + "))^2}{" + a.ToString("F1") +
                    @"^2}+ \frac{(x*sin(" + gradusTransform.ToString() + ")+y*cos(" + gradusTransform.ToString() + ")-(" + y.ToString("F1") + @"))^2}{" + b.ToString("F1") + @"^2} = 1";
                        return @"\frac{(x*cos(" + gradusTransform.ToString() + ")-y*sin(" + gradusTransform.ToString() + ")-(" + x.ToString("F1") + @"))^2}{" + a.ToString("F1") +
                    @"^2}+ \frac{(x*sin(" + gradusTransform.ToString() + ")+y*cos(" + gradusTransform.ToString() + ")-(" + y.ToString("F1") + @"))^2}{" + b.ToString("F1") + @"^2} = 1";
                    case false:
                        if (x == 0 && y == 0) return @"\frac{x^2}{" + a.ToString("F1") + @"^2}+ \frac{y^2}{" + b.ToString("F1") + @"^2} = 1";
                        if (y == 0) return @"\frac{(x-(" + x.ToString("F1") + @"))^2}{" + a.ToString("F1") + @"^2}+ \frac{y^2}{" + b.ToString("F1") + @"^2} = 1";
                        if (x == 0) return @"\frac{x^2}{" + a.ToString("F1") + @"^2}+ \frac{(y-(" + y.ToString("F1") + @"))^2}{" + b.ToString("F1") + @"^2} = 1";
                        return @"\frac{(x-(" + x.ToString("F1") + @"))^2}{" + a.ToString("F1") + @"^2}+ \frac{(y-(" + y.ToString("F1") + @"))^2}{" + b.ToString("F1") + @"^2} = 1";
                    default:
                        return " ";
                }
            }

        }



    }

}
