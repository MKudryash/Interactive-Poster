﻿using System;
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

        double x, p, y, maxCorrdinatX = -15;
        public DrawParabola(double x, double y, Canvas cv, double p)
        {
            this.p = p;
            this.x = x;
            this.cv = cv;
            this.y = y;
            countX = Convert.ToDouble(cv.Tag);//получаем масштабы области
            countY = Math.Round(cv.ActualHeight / (cv.ActualWidth / countX));
            maxX = cv.ActualWidth; //получаем ширину канвы
            maxY = cv.ActualHeight; //получаем высоту канвы     
            FocusParabola(cv);
            DrawDirectrix();
        }
        public PathGeometry Parabola()
        {
            PathFigure pathFigure = new PathFigure();
            PathGeometry pathGeometry = new PathGeometry();
            QuadraticBezierSegment quadraticBezierSegment = new QuadraticBezierSegment();
            if (MaxMinCoordinat.equationforParabola) //уравнение вида 2py = x^2
            {
                pathFigure.StartPoint = new Point(convertCoordX(x + maxCorrdinatX / -1), convertCoordY(Math.Pow(maxCorrdinatX, 2) / 2 * p + y));
                quadraticBezierSegment.Point1 = new Point(convertCoordX(x), convertCoordY(Math.Pow(maxCorrdinatX, 2) / 2 * p * (-1) + y));
                quadraticBezierSegment.Point2 = new Point(convertCoordX(x + maxCorrdinatX), convertCoordY(Math.Pow(maxCorrdinatX, 2) / 2 * p + y));
                pathFigure.Segments.Add(quadraticBezierSegment);
                pathGeometry.Figures.Add(pathFigure);
                return pathGeometry;
            }
            else //уравнение вида 2px= y^2
            {
                pathFigure.StartPoint = new Point(convertCoordX(Math.Pow(maxCorrdinatX, 2) / 2 * p + x), convertCoordY(y + maxCorrdinatX / -1));
                quadraticBezierSegment.Point1 = new Point(convertCoordX(Math.Pow(maxCorrdinatX, 2) / 2 * p * (-1) + x), convertCoordY(y));
                quadraticBezierSegment.Point2 = new Point(convertCoordX(Math.Pow(maxCorrdinatX, 2) / 2 * p + x), convertCoordY(y + maxCorrdinatX));
                pathFigure.Segments.Add(quadraticBezierSegment);
                pathGeometry.Figures.Add(pathFigure);
                return pathGeometry;
            }

        }
        Line line;
        void DrawDirectrix()
        {
            if (!MaxMinCoordinat.equationforParabola)
            {
                line = new Line()
                {
                    Stroke = Brushes.Black,
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
                    Stroke = Brushes.Black,
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
                Fill = Brushes.Black,
                Stroke = Brushes.Black,
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
