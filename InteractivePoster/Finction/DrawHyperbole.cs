
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
        Rectangle rectangle;
        Line line;
        double x, y,a,b, maxCorrdinatX;
        public DrawHyperbole(double x, double y, double a, double b, Canvas cv, double maxCorrdinatX,double gradusTransform)
        {
            this.x = x;
            this.y = y;
            this.a = a;
            this.b = b;
            this.maxCorrdinatX = maxCorrdinatX;
            count = Convert.ToDouble(cv.Tag);//получаем масштабы области
            maxX = cv.ActualWidth; //получаем ширину канвы       
            rectangleA = a * (maxX / count);//преобразуем ширину прямоугольника из декартовой системы
            rectangleB = b * (maxX / count);//преобразуем высоту прямоугольника из декартовой системы
            DrawRectangle(x, y, cv, gradusTransform);
        }
        QuadraticBezierSegment quadraticBezierSegment;
        public PathGeometry Hyperbola()
        {
            //y=b/a*x
            PathFigure pathFigure = new PathFigure();
            PathGeometry pathGeometry = new PathGeometry();
            quadraticBezierSegment = new QuadraticBezierSegment();
            pathFigure.StartPoint = new Point(convertCoordX(maxCorrdinatX + x), convertCoordY(b/a* maxCorrdinatX + y));
            quadraticBezierSegment.Point1 = new Point(convertCoordX(a*2+x-maxCorrdinatX), convertCoordY(y));
            quadraticBezierSegment.Point2 = new Point(convertCoordX(maxCorrdinatX + x), convertCoordY(-1*b / a * maxCorrdinatX + y));
            pathFigure.Segments.Add(quadraticBezierSegment);
            pathGeometry.Figures.Add(pathFigure);


            //y=-b/a*x
            PathFigure pathFigureTwo = new PathFigure();
            quadraticBezierSegment = new QuadraticBezierSegment();
            pathFigureTwo.StartPoint = new Point(convertCoordX(maxCorrdinatX*-1 + x), convertCoordY(b / a * maxCorrdinatX + y));
            quadraticBezierSegment.Point1 = new Point(convertCoordX((a * 2 - maxCorrdinatX)*(-1) + x), convertCoordY(y));
            quadraticBezierSegment.Point2 = new Point(convertCoordX(maxCorrdinatX *-1+ x), convertCoordY(-1 * b / a * maxCorrdinatX + y));
            pathFigureTwo.Segments.Add(quadraticBezierSegment);
            pathGeometry.Figures.Add(pathFigureTwo);

            return pathGeometry;
        }
        void DrawRectangle(double x, double y, Canvas cv, double gradusTransform)
        {
            rectangle = new Rectangle()
            {
                Width = 2 * rectangleA,//ширина и длина по сути равна диаметру окружности
                Height = 2 * rectangleB,
                Stroke = Brushes.Black,
                StrokeThickness = 2,
                StrokeDashArray = { 4, 3 }
            };
            RotateTransform rotateTransform = new RotateTransform();
            rotateTransform.CenterX = rectangleA;//центр оси X по отношению к кругу, не к координатной плоскости
            rotateTransform.CenterY = rectangleB;//центр оси Y по отношению к кругу, не к координатной плоскости
            rotateTransform.Angle = gradusTransform;//поворот на количетсво градусов     
            rectangle.RenderTransform = rotateTransform;
            cv.Children.Add(rectangle);//помещаем на канву
                                       //в нужную точку канвы
            rectangle.SetValue(Canvas.LeftProperty, convertX(x));
            rectangle.SetValue(Canvas.TopProperty, convertY(y));
        }


        /// <summary>
        /// метод для преобразования координаты Х их канвы в декартово значение
        /// </summary>
        /// <param name="x">декартова координата (как нам надо с точки зрения математики)</param>
        /// <returns>актуальная координата Х на канве</returns>
        public double convertX(double x)
        {
            //радиус вычитаем, т.к. по умолчанию передаются координаты левого верхнего угла
            return maxX / 2 + x * (maxX / count) - rectangleA;
        }
        /// <summary>
        /// метод для преобразования координаты Y их канвы в декартово значение
        /// </summary>
        /// <param name="y">декартова координата (как нам надо с точки зрения математики)</param>
        /// <returns>актуальная координата Y на канве</returns>
        public double convertY(double y)
        {
            return maxX / 2 + y / -1 * (maxX / count) - rectangleB;
        }
    }

}
