
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
        double sinGradusHyperbole;
        double cosGradusHyperbole;
        Rectangle rectangle;
        Canvas cv;
        Line line;
        double x, y, a, b;
        public DrawHyperbole(double x, double y, double a, double b, Canvas cv, double gradusTransform)
        {
            this.x = x;
            this.y = y;
            this.a = a;
            this.b = b;
            this.cv = cv;

            sinGradusHyperbole = Math.Sin(convertRadian(gradusTransform));
            cosGradusHyperbole = Math.Cos(convertRadian(gradusTransform));

            count = Convert.ToDouble(cv.Tag);//получаем масштабы области
            maxX = cv.ActualWidth; //получаем ширину канвы       
            rectangleA = a * (maxX / count);//преобразуем ширину прямоугольника из декартовой системы
            rectangleB = b * (maxX / count);//преобразуем высоту прямоугольника из декартовой системы
            DrawRectangle(x, y, cv, gradusTransform);
            FocusHyperbole();
            DrawAsymptotes(gradusTransform);

            double mX = (a + 1) * cosGradusHyperbole + Math.Sqrt(Math.Pow(b, 2) * (((Math.Pow(a + 1, 2)) / (a * a)) - 1)) * sinGradusHyperbole + x;
            double mY = (a + 1) * sinGradusHyperbole * -1 + Math.Sqrt(Math.Pow(b, 2) * (((Math.Pow(a + 1, 2)) / (a * a)) - 1)) * cosGradusHyperbole + y;

            DrawText(mX , mY, "M( " + (mX).ToString("F1") + "; " + (mY).ToString("F1") + ")");
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
            PointFocus.SetValue(Canvas.LeftProperty, convertCoordX(mX - 0.1));
            PointFocus.SetValue(Canvas.TopProperty, convertCoordY(mY + 0.1));


        }
        QuadraticBezierSegment quadraticBezierSegment;
        public PathGeometry Hyperbola()
        {
            PathGeometry pathGeometry = new PathGeometry();

            //правая ветвь
            PathFigure pathFigure = new PathFigure()
            {
                StartPoint = new Point(convertCoordX(a + 1 + x), convertCoordY(Math.Sqrt(Math.Pow(b, 2) * ((Math.Pow(a + 1, 2) / (a * a)) - 1)) + y))
            };
            quadraticBezierSegment = new QuadraticBezierSegment()
            {
                Point1 = new Point(convertCoordX(a - 1 + x), convertCoordY(y)),
                Point2 = new Point(convertCoordX(a + 1 + x), convertCoordY(Math.Sqrt(Math.Pow(b, 2) * (((Math.Pow(a + 1, 2)) / (a * a)) - 1)) * -1 + y))
            };
            pathFigure.Segments.Add(quadraticBezierSegment);
            pathGeometry.Figures.Add(pathFigure);


            double AvgX = (((count / 2) - (a + 1)) / 2) + (a + 1);




            pathFigure = new PathFigure()
            {
                StartPoint = new Point(convertCoordX(a + 1 + x), convertCoordY(Math.Sqrt(Math.Pow(b, 2) * ((Math.Pow(a + 1, 2) / (a * a)) - 1)) + y))
            };
            quadraticBezierSegment = new QuadraticBezierSegment()
            {
                Point1 = new Point(convertCoordX(AvgX + x), convertCoordY(Math.Sqrt(Math.Abs(Math.Pow(b, 2) * ((Math.Pow(AvgX, 2) / (a * a)) - 1))) + y)),
                Point2 = new Point(convertCoordX(count / 2 + x), convertCoordY(Math.Sqrt(Math.Pow(b, 2) * ((Math.Pow(count / 2, 2) / (a * a)) - 1)) + y))
            };
            pathFigure.Segments.Add(quadraticBezierSegment);
            pathGeometry.Figures.Add(pathFigure);



            pathFigure = new PathFigure()
            {
                StartPoint = new Point(convertCoordX(a + 1 + x), convertCoordY(Math.Sqrt(Math.Pow(b, 2) * ((Math.Pow(a + 1, 2) / (a * a)) - 1)) * -1 + y))
            };
            quadraticBezierSegment = new QuadraticBezierSegment()
            {
                Point1 = new Point(convertCoordX(AvgX + x), convertCoordY(Math.Sqrt(Math.Pow(b, 2) * ((Math.Pow(AvgX, 2) / (a * a)) - 1)) * -1 + y)),
                Point2 = new Point(convertCoordX(count / 2 + x), convertCoordY(Math.Sqrt(Math.Pow(b, 2) * ((Math.Pow(count / 2, 2) / (a * a)) - 1)) * -1 + y))
            };
            pathFigure.Segments.Add(quadraticBezierSegment);
            pathGeometry.Figures.Add(pathFigure);



            //левая ветвь
            pathFigure = new PathFigure()
            {
                StartPoint = new Point(convertCoordX(-a - 1 + x), convertCoordY(Math.Sqrt(Math.Pow(b, 2) * ((Math.Pow(-a - 1, 2) / (a * a)) - 1)) + y))
            };
            quadraticBezierSegment = new QuadraticBezierSegment()
            {
                Point1 = new Point(convertCoordX(-a + 1 + x), convertCoordY(y)),
                Point2 = new Point(convertCoordX(-a - 1 + x), convertCoordY(Math.Sqrt(Math.Pow(b, 2) * ((Math.Pow(-a - 1, 2) / (a * a)) - 1)) * -1 + y))
            };
            pathFigure.Segments.Add(quadraticBezierSegment);
            pathGeometry.Figures.Add(pathFigure);



            pathFigure = new PathFigure()
            {
                StartPoint = new Point(convertCoordX(-a - 1 + x), convertCoordY(Math.Sqrt(Math.Pow(b, 2) * (((Math.Pow(-a - 1, 2)) / (a * a)) - 1)) + y))
            };
            quadraticBezierSegment = new QuadraticBezierSegment()
            {
                Point1 = new Point(convertCoordX(AvgX * -1 + x), convertCoordY(Math.Sqrt(Math.Pow(b, 2) * ((Math.Pow(AvgX, 2) / (a * a)) - 1)) + y)),
                Point2 = new Point(convertCoordX((count / 2) * -1 + x), convertCoordY(Math.Sqrt(Math.Pow(b, 2) * (((Math.Pow(count / 2, 2)) / (a * a)) - 1)) + y))
            };
            pathFigure.Segments.Add(quadraticBezierSegment);
            pathGeometry.Figures.Add(pathFigure);



            pathFigure = new PathFigure()
            {
                StartPoint = new Point(convertCoordX(-a - 1 + x), convertCoordY(Math.Sqrt(Math.Pow(b, 2) * (((Math.Pow(-a - 1, 2)) / (a * a)) - 1)) * -1 + y))
            };
            quadraticBezierSegment = new QuadraticBezierSegment()
            {
                Point1 = new Point(convertCoordX(AvgX * -1 + x), convertCoordY(Math.Sqrt(Math.Pow(b, 2) * (((Math.Pow(AvgX, 2)) / (a * a)) - 1)) * -1 + y)),
                Point2 = new Point(convertCoordX((count / 2) * -1 + x), convertCoordY(Math.Sqrt(Math.Pow(b, 2) * (((Math.Pow(count / 2, 2)) / (a * a)) - 1)) * -1 + y))
            };
            pathFigure.Segments.Add(quadraticBezierSegment);
            pathGeometry.Figures.Add(pathFigure);

            return pathGeometry;
        }

        void DrawAsymptotes(double gradusTransform)
        {
            line = new Line()
            {
                Stroke = Brushes.Black,
                StrokeThickness = 2,
                StrokeDashArray = { 4, 3 },
                SnapsToDevicePixels = true,
                X1 = convertCoordX(count / 2 * -1 + x),
                X2 = convertCoordX(count / 2 + x),
                Y1 = convertCoordY(b / a * count / 2 * -1 + y),
                Y2 = convertCoordY(b / a * count / 2 + y),
            };
            RotateTransform rotateTransform = new RotateTransform();
            rotateTransform.CenterX = maxX / 2 + x * (maxX / count); //центр оси X по отношению к параболе, не к координатной плоскости
            rotateTransform.CenterY = maxX / 2 + y * (-1) * (maxX / count);//центр оси Y по отношению к параболе, не к координатной плоскости
            rotateTransform.Angle = gradusTransform;//поворот на количетсво градусов  
            line.RenderTransform = rotateTransform;
            line.SetValue(RenderOptions.EdgeModeProperty, EdgeMode.Aliased);
            cv.Children.Add(line);
            line = new Line()
            {
                Stroke = Brushes.Black,
                StrokeThickness = 2,
                StrokeDashArray = { 4, 3 },
                SnapsToDevicePixels = true,
                X1 = convertCoordX((count / 2) * -1 + x),
                X2 = convertCoordX((count / 2 + x)),
                Y2 = convertCoordY((b / a * count / 2) * -1 + y),
                Y1 = convertCoordY(b / a * count / 2 + y),
            };
            line.RenderTransform = rotateTransform;
            line.SetValue(RenderOptions.EdgeModeProperty, EdgeMode.Aliased);
            cv.Children.Add(line);
        }



        void DrawRectangle(double x, double y, Canvas cv, double gradusTransform)
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
                Stroke = Brushes.Black,
                StrokeThickness = 2,
                StrokeDashArray = { 4, 3 },
                RenderTransform = rotateTransform
            };

            cv.Children.Add(rectangle);//помещаем на канву
                                       //в нужную точку канвы
            rectangle.SetValue(Canvas.LeftProperty, convertX(x));
            rectangle.SetValue(Canvas.TopProperty, convertY(y));
        }
        void DrawText(double x, double y, string text)
        {
            TextBlock TB = new TextBlock()
            {
                Text = text,
                TextWrapping = TextWrapping.Wrap,
                Width = double.NaN,
                FontSize = maxX / count * 0.5
            };
            cv.Children.Add(TB);
            TB.SetValue(Canvas.LeftProperty, convertCoordX(x));
            TB.SetValue(Canvas.TopProperty, convertCoordY(y));
        } //Текст c содержанием точек
        void FocusHyperbole()
        {
            double c = Math.Sqrt(Math.Pow(a, 2) + Math.Pow(b, 2));

            string text = "F1( " + (x + c * cosGradusHyperbole).ToString("F1") + "; " + (y - c * sinGradusHyperbole).ToString("F1") + ")";
            DrawPoinFocus(x + c * cosGradusHyperbole, y - c * sinGradusHyperbole);
            DrawText(x + c * cosGradusHyperbole, y - c * sinGradusHyperbole, text);
            text = "F2( " + (x - c * cosGradusHyperbole).ToString("F1") + "; " + (y + c * sinGradusHyperbole).ToString("F1") + ")";
            DrawPoinFocus(x - c * cosGradusHyperbole, y + c * sinGradusHyperbole);
            DrawText(x - c * cosGradusHyperbole, y + c * sinGradusHyperbole, text);
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
        /// метод для преобразования градусов в радианы
        public double convertRadian(double r)
        {
            return r / 180 * Math.PI;
        }
    }

}
