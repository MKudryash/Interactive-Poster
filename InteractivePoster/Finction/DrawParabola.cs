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

        double x, p, y, maxCorrdinatX;

        public DrawParabola(double x, double y,  Canvas cv,double p,double maxCorrdinatX)
        {
            this.p = p;
            this.x = x;
            this.y = y;
            this.maxCorrdinatX = maxCorrdinatX;
            count = Convert.ToDouble(cv.Tag);//получаем масштабы области
            maxX = cv.ActualWidth; //получаем ширину канвы
            FocusParabola(cv);
        }
        public PathGeometry Parabola()
        {
            PathFigure pathFigure = new PathFigure();
            PathGeometry pathGeometry = new PathGeometry();
            QuadraticBezierSegment quadraticBezierSegment = new QuadraticBezierSegment();
            if (MaxMinCoordinat.equation) //уравнение вида 2py = x^2
            {               
                pathFigure.StartPoint = new Point(convertCoordX(x+maxCorrdinatX / -1), convertCoordY(Math.Pow(maxCorrdinatX, 2) / 2 * p+y));           
                quadraticBezierSegment.Point1 = new Point(convertCoordX(x), convertCoordY(Math.Pow(maxCorrdinatX, 2) / 2 * p*(-1)+y));
                quadraticBezierSegment.Point2 = new Point(convertCoordX(x + maxCorrdinatX), convertCoordY(Math.Pow(maxCorrdinatX, 2) / 2 * p+y));
                pathFigure.Segments.Add(quadraticBezierSegment);                
                pathGeometry.Figures.Add(pathFigure);               
                return pathGeometry;
            }
            else //уравнение вида 2px= y^2
            {
                pathFigure.StartPoint = new Point(convertCoordX(Math.Pow(maxCorrdinatX, 2) / 2 * p + x), convertCoordY(y+ maxCorrdinatX / -1));
                quadraticBezierSegment.Point1 = new Point(convertCoordX(Math.Pow(maxCorrdinatX, 2) / 2 * p * (-1) + x), convertCoordY(y));
                quadraticBezierSegment.Point2 = new Point(convertCoordX(Math.Pow(maxCorrdinatX, 2) / 2 * p + x), convertCoordY(y+ maxCorrdinatX));
                pathFigure.Segments.Add(quadraticBezierSegment);
                pathGeometry.Figures.Add(pathFigure);
                return pathGeometry;
            }
            
        }
        void FocusParabola(Canvas cv)
        {
            double focus =p/2;
            TextBlock TB = new TextBlock();
            if (MaxMinCoordinat.equation)
            {
                DrawPoinFocus(x, focus+y, cv);//Отображение фокуса на координатной плоскости
                TB.Text = "F( " + y.ToString("F1") + "; " + (focus + x).ToString("F1") + ")";
                TB.SetValue(Canvas.LeftProperty, convertCoordX(x));
                TB.SetValue(Canvas.TopProperty, convertCoordY(focus+y));
            }
            else
            {
                DrawPoinFocus(focus+x, y, cv);//Отображение фокуса на координатной плоскости
                TB.Text = "F( " + (focus + x).ToString("F1") + "; " + y.ToString("F1") + ")";
                TB.SetValue(Canvas.LeftProperty, convertCoordX(focus+x));
                TB.SetValue(Canvas.TopProperty, convertCoordY(y));
            }
            cv.Children.Add(TB);            
            TB.TextWrapping = System.Windows.TextWrapping.Wrap;
            TB.Width = 120;

        }
        Ellipse PointFocus;
        void DrawPoinFocus(double x, double y, Canvas cv)
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
    }
}
