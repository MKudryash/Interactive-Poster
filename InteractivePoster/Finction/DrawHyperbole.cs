
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
        double sinGradusElpis;
        double cosGradusElpis;
        Rectangle rectangle;
        Canvas cv;
        Line line;
        double x, y,a,b, maxCorrdinatX;
        public DrawHyperbole(double x, double y, double a, double b, Canvas cv, double maxCorrdinatX,double gradusTransform)
        {
            this.x = x;
            this.y = y;
            this.a = a;
            this.b = b;
            this.cv = cv;

            sinGradusElpis = Math.Sin(convertRadian(gradusTransform));
            cosGradusElpis = Math.Cos(convertRadian(gradusTransform));

            this.maxCorrdinatX = maxCorrdinatX;
            count = Convert.ToDouble(cv.Tag);//получаем масштабы области
            maxX = cv.ActualWidth; //получаем ширину канвы       
            rectangleA = a * (maxX / count);//преобразуем ширину прямоугольника из декартовой системы
            rectangleB = b * (maxX / count);//преобразуем высоту прямоугольника из декартовой системы
            DrawRectangle(x, y, cv, gradusTransform);
            FocusHyperbole();
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
        void FocusHyperbole()
        {
            double c = Math.Sqrt(Math.Pow(a,2)+ Math.Pow(b, 2));

                    string text = "F1( " + (x + c * cosGradusElpis).ToString("F1") + "; " + (y - c * sinGradusElpis).ToString("F1") + ")";
                    DrawPoinFocus(x + c * cosGradusElpis, y - c * sinGradusElpis);
                    DrawText(x + c * cosGradusElpis, y - c * sinGradusElpis, text);
                    text = "F2( " + (x - c * cosGradusElpis).ToString("F1") + "; " + (y + c * sinGradusElpis).ToString("F1") + ")";
                    DrawPoinFocus(x - c * cosGradusElpis, y + c * sinGradusElpis);
                    DrawText(x - c * cosGradusElpis, y + c * sinGradusElpis, text);

            
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
        public double convertRadian(double r)
        {
            return r / 180 * Math.PI;
        }
    }

}
