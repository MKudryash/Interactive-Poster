using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace InteractivePoster.Finction
{
    class DrawParabola : GeometricPatterns
    {

        double p, maxCorrdinatX = -15;
        public DrawParabola(double x, double y, Canvas cv, double p,double point, Canvas canvas)
        {
            this.p = p;
            this.x = x;
            this.cv = cv;
            this.y = y;
            countX = Convert.ToDouble(canvas.Tag);//получаем масштабы области
            countY = Math.Round(canvas.ActualHeight / (canvas.ActualWidth / countX));
            maxX = canvas.ActualWidth; //получаем ширину канвы
            maxY = canvas.ActualHeight; //получаем высоту канвы     


            Parabola();
            FocusParabola(cv);
            DrawDirectrix();
            MovePoint(point);
        }
        public void Parabola()
        {
            System.Windows.Shapes.Path path = new System.Windows.Shapes.Path();
            

           
            PathFigure pathFigure = new PathFigure();
            PathGeometry pathGeometry = new PathGeometry();
            QuadraticBezierSegment quadraticBezierSegment = new QuadraticBezierSegment();
            if (MaxMinCoordinat.equationforParabola) //уравнение вида 2py = x^2
            {
              
                pathFigure.StartPoint = new Point(convertCoordX(x + maxCorrdinatX / -1), convertCoordY(Math.Pow(maxCorrdinatX, 2) / (2 * p) + y));
                quadraticBezierSegment.Point1 = new Point(convertCoordX(x), convertCoordY(Math.Pow(maxCorrdinatX, 2) / (2 * p) * (-1) + y));
                quadraticBezierSegment.Point2 = new Point(convertCoordX(x + maxCorrdinatX), convertCoordY(Math.Pow(maxCorrdinatX, 2) / (2 * p) + y));
                pathFigure.Segments.Add(quadraticBezierSegment);
                pathGeometry.Figures.Add(pathFigure);
               
            }
            else //уравнение вида 2px= y^2
            {
                pathFigure.StartPoint = new Point(convertCoordX(Math.Pow(maxCorrdinatX, 2) / (2 * p) + x), convertCoordY(y + maxCorrdinatX / -1));
                quadraticBezierSegment.Point1 = new Point(convertCoordX(Math.Pow(maxCorrdinatX, 2) / (2 * p) * (-1) + x), convertCoordY(y));
                quadraticBezierSegment.Point2 = new Point(convertCoordX(Math.Pow(maxCorrdinatX, 2) / (2 * p) + x), convertCoordY(y + maxCorrdinatX));
                pathFigure.Segments.Add(quadraticBezierSegment);
                pathGeometry.Figures.Add(pathFigure);              
            }
            path.Data = pathGeometry;
            path.Stroke = colorFigure;
            path.StrokeThickness = 3;
            cv.Children.Add(path);

        }
        Line line;
        TextBlock textBlock;
        void MovePoint(double y)
        {
            double mX=0, mY=0;

            switch (MaxMinCoordinat.equationforParabola)
            {
                case true:
                    {
                        if (p > 0)
                        {
                            if (y >= 0)
                            {
                                mX = x + Math.Round(Math.Sqrt(y * 2 * p), 2);
                                mY = this.y + y;
                            }
                            else
                            {
                                mX = x + (Math.Round(Math.Sqrt(Math.Abs(y) * 2 * p), 2)) / -1;
                                mY = this.y + Math.Abs(y);
                            }
                        }
                        else
                        {
                            if (y >= 0)
                            {
                                mX = x + Math.Round(Math.Sqrt(Math.Abs(y * 2 * p)), 2);
                                mY = this.y + y / -1;
                            }
                            else
                            {
                                mX = x + (Math.Round(Math.Sqrt(Math.Abs(y * 2 * p)), 2)) / -1;
                                mY = this.y + y;
                            }
                        }
                    }
                    break;
                case false:
                    {
                        if (p > 0)
                        {
                            if (y <= 0)
                            {
                                mX = x + Math.Abs(y);
                                mY = this.y + +Math.Round(Math.Sqrt(Math.Abs(y) * 2 * p), 2);
                            }
                            else
                            {
                                mX = x + y;
                                mY = this.y + (Math.Round(Math.Sqrt(Math.Abs(y) * 2 * p), 2)) / -1;
                            }
                        }
                        else
                        {

                            if (y <= 0)
                            {
                                mX = x + y;
                                mY = this.y + +Math.Round(Math.Sqrt(Math.Abs(y * 2 * p)), 2);
                            }
                            else
                            {
                                mX = x + y * -1;
                                mY = this.y + (Math.Round(Math.Sqrt(Math.Abs(y * 2 * p)), 2)) / -1;
                            }
                        }
                    }
                    break;
                default:
                    break;
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

            PointFocus.SetValue(Canvas.LeftProperty, convertCoordX(mX - 0.1));
            PointFocus.SetValue(Canvas.TopProperty, convertCoordY(mY + 0.1));
            if (MaxMinCoordinat.ElementParabolaPoint)
            {
                cv.Children.Add(textBlock);
                cv.Children.Add(PointFocus);
            }
            else
            {
                cv.Children.Remove(textBlock);
                cv.Children.Remove(PointFocus);
            }
        }
        void DrawDirectrix()
        {
            if (!MaxMinCoordinat.equationforParabola)
            {
                line = new Line()
                {
                    Stroke = colorTools,
                    StrokeThickness = 2,
                    StrokeDashArray = { 4, 3 },
                    SnapsToDevicePixels = true,
                    Y1 = 0,
                    Y2 = maxY,
                    X1 = convertCoordX(x-p / 2),
                    X2 = convertCoordX(x-p / 2)
                };
            }
            else
            {
                line = new Line()
                {
                    Stroke = colorTools,
                    StrokeThickness = 2,
                    StrokeDashArray = { 4, 3 },
                    SnapsToDevicePixels = true,
                    Y1 = convertCoordY(y-p / 2),
                    Y2 = convertCoordY(y-p / 2),
                    X1 = 0,
                    X2 = maxX
                };
            }
            line.SetValue(RenderOptions.EdgeModeProperty, EdgeMode.Aliased);
            if (MaxMinCoordinat.ElementParabolaD)
            {
                cv.Children.Add(line);
            }
            else
            {
                cv.Children.Remove(line);
            }
            
            
        }
        void FocusParabola(Canvas cv)
        {
            double focus = p / 2;
            TextBlock TB = new TextBlock();
            Ellipse ellipse = new Ellipse();

            if (MaxMinCoordinat.equationforParabola)
            {
                ellipse =  DrawPoinFocus(x, focus + y, cv);//Отображение фокуса на координатной плоскости
                TB.Text = "F( " + y.ToString("F1") + "; " + (focus + x).ToString("F1") + ")";
                TB.SetValue(Canvas.LeftProperty, convertCoordX(x));
                TB.SetValue(Canvas.TopProperty, convertCoordY(focus + y));
            }
            else
            {
                ellipse = DrawPoinFocus(focus + x, y, cv);//Отображение фокуса на координатной плоскости
                TB.Text = "F( " + (focus + x).ToString("F1") + "; " + y.ToString("F1") + ")";
                TB.SetValue(Canvas.LeftProperty, convertCoordX(focus + x));
                TB.SetValue(Canvas.TopProperty, convertCoordY(y));
            }
           
            TB.TextWrapping = System.Windows.TextWrapping.Wrap;
            TB.Width = double.NaN;

            if (MaxMinCoordinat.ElementParabolaFocus)
            {
                cv.Children.Add(TB);
                cv.Children.Add(ellipse);
            }
            else
            {
                cv.Children.Remove(TB);
                cv.Children.Remove(ellipse);
            }
        }
        Ellipse PointFocus;
        private Ellipse DrawPoinFocus(double x, double y, Canvas cv)
        {
            PointFocus = new Ellipse()//задаем прочие параметры
            {
                Width = (maxX / countX) * 0.2,
                Height = (maxX / countX) * 0.2,
                Fill = colorFigure,
                Stroke = colorFigure,
                StrokeThickness = 3
            };

          
            PointFocus.SetValue(Canvas.LeftProperty, convertCoordX(x - 0.1));
            PointFocus.SetValue(Canvas.TopProperty, convertCoordY(y + 0.1));
            return PointFocus;
        }

        public string CanonicalEquation()
        {
            switch (MaxMinCoordinat.equationforParabola)
            {
                case true:
                    if (p > 0)
                    {
                        if (x == 0 && y == 0) return @"x^2= 2p*y";
                        if (y == 0) return @"(x- (" + x.ToString("F1") + "))^2= 2p*y";
                        if (x == 0) return @"x^2= 2p(y- (" + y.ToString("F1") + "))";

                        return @"(x- (" + x.ToString("F1") + "))^2= 2p(y- (" + y.ToString("F1") + "))";
                    };
                    if (p < 0)
                    {
                        if (x == 0 && y == 0) return @"x^2= -2p*y";
                        if (y == 0) return @"(x- (" + x.ToString("F1") + "))^2= -2p*y";
                        if (x == 0) return @"x^2= -2p(y- (" + y.ToString("F1") + "))";
                        return @"(x- (" + x.ToString("F1") + "))^2= -2p(y- (" + y.ToString("F1") + "))";
                    };
                    break;

                case false:
                    if (p > 0)
                    {
                        if (x == 0 && y == 0) return @"y^2= 2p*x";
                        if (y == 0) return @"y^2= 2p(x- (" + x.ToString("F1") + "))";
                        if (x == 0) return @"(y - (" + y.ToString("F1") + "))^2= 2p*x";
                        return @"(y - (" + y.ToString("F1") + "))^2= 2p(x- (" + x.ToString("F1") + "))";
                    };
                    if (p < 0)
                    {
                        if (x == 0 && y == 0) return @"y^2= -2p*x";
                        if (y == 0) return @"(y - (" + x.ToString("F1") + "))^2= -2p*x";
                        if (x == 0) return @"(y - (" + y.ToString("F1") + "))^2= -2p*x";
                        return @"(y - (" + y.ToString("F1") + "))^2= -2p(x- (" + x.ToString("F1") + "))";
                    };
                    break;
                default:
                    return " ";
            }
            return "";

        }
    }
}
