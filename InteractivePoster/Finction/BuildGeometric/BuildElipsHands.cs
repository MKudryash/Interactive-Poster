using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace InteractivePoster.Finction.BuildGeometric
{
    class BuildElipsHands : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public static Canvas cv { get; set; }
        public Canvas GetCanvas { get => cv; set { cv = value; } }
        List<Ellipse> FocusPoint { get; set; } = new List<Ellipse>();
        List<Ellipse> PointForEllipse { get; set; } = new List<Ellipse>();
        List<double> Point { get; set; } = new List<double>();
        Line line;

        public bool mouseDown = false;
        public bool flag = false;

        public bool MouseDown
        {
            get { return mouseDown; }
            set
            {
                mouseDown = value;
            }
        }

        public bool centerElips { get; set; } = true;
        public bool exs { get; set; } = true;
        public bool parametrC { get; set; } = false;
        public double c = 1;

        public double cc = 1;
        public double GetC
        {
            get
            {               
                c = cc / 2;
                thread = cc + 0.3;
                return cc;
            }
            set { cc = value; c = cc / 2; PropertyChanged(this, new PropertyChangedEventArgs("Minimum"));
                PropertyChanged(this, new PropertyChangedEventArgs("GetThread"));
            }
        }
        
        public double thread = 1.3;

        public double min = 1.3;
        public double Minimum { get {  PropertyChanged(this, new PropertyChangedEventArgs("GetThread"));return min = cc + 0.3; } }
    public double GetThread
        {
            get => thread;
            set { thread = value;}
        }

        public bool onePoint { get; set; } = false;
        public double coordCX { get; set; } = 0;
        public double coordCY { get; set; } = 0;

        public string focusOne { get; set; }
        public string FocusOne
        {
            get
            {
                return focusOne;
            }
        }
        public string focusTwo { get; set; }
        public string FocusTwo
        {
            get
            {
                return focusTwo;
            }
        }
        double centreX, centreY;
        Ellipse pointForElipse;
        public void GetCenterPoint(MouseEventArgs e)
        {
            BuildElipsHand beh = new BuildElipsHand(cv);

            centreX = e.GetPosition(beh.cv).X;
            centreY = e.GetPosition(beh.cv).Y;
            coordCX = Math.Round(beh.convertXCoord(centreX), 1);
            coordCY = Math.Round(beh.convertYCoord(centreY), 1);
            pointForElipse = new Ellipse()
            {
                Width = 6,
                Height = 6,
                Fill = Brushes.Black,
                Stroke = Brushes.Black,
                StrokeThickness = 1
            };
            PointForEllipse.Add(pointForElipse);
            cv.Children.Add(pointForElipse);
            pointForElipse.SetValue(Canvas.LeftProperty, beh.convertCoordX(coordCX) - 3);
            pointForElipse.SetValue(Canvas.TopProperty, beh.convertCoordY(coordCY) - 3);
            Point.Add(coordCX);
            Point.Add(coordCY);
            centerElips = false;
            parametrC = true;
            Property();
        }
        double a, b;
        public void FindRadius()
        {
           
            if (exs)
            {
                a = thread - c;
                b = Math.Sqrt(a * a - c * c);
            }
            else
            {
                b = thread - c;
                a = Math.Sqrt(b * b - c * c);
            }

        }
        public void BuildElipse(MouseEventArgs e)
        {
            BuildElipsHand beh = new BuildElipsHand(cv);
            double x = e.GetPosition(cv).X;
            double y = e.GetPosition(cv).Y;


            double coordX = beh.convertXCoord(x);
            double coordY = beh.convertYCoord(y);


            double p = Math.Atan((coordY - coordCY) / (coordX - coordCX));

         
            if (coordX < coordCX) { p = p + Math.PI; }
            double c = 180 / Math.PI * p;
            double aa = Math.Atan(a / b * Math.Tan(p));
            if (c <= 270 && c > 90) { aa = aa + Math.PI; }

            double circleX = coordCX + (a * Math.Cos(aa));
            double circleY = coordCY + (b * Math.Sin(aa));

            Point.Add(circleX);
            Point.Add(circleY);

            pointForElipse = new Ellipse()
            {
                Width = 3,
                Height = 3,
                Fill = Brushes.Black,
                Stroke = Brushes.Black,
                StrokeThickness = 1
            };
            PointForEllipse.Add(pointForElipse);
            cv.Children.Add(pointForElipse);
            pointForElipse.SetValue(Canvas.LeftProperty, beh.convertCoordX(circleX));
            pointForElipse.SetValue(Canvas.TopProperty, beh.convertCoordY(circleY));
            DrawThread(circleX, circleY);
        }

        public void Property()
        {
            PropertyChanged(this, new PropertyChangedEventArgs("centerElips"));
            PropertyChanged(this, new PropertyChangedEventArgs("parametrC"));
            PropertyChanged(this, new PropertyChangedEventArgs("coordCX"));
            PropertyChanged(this, new PropertyChangedEventArgs("coordCY"));
            PropertyChanged(this, new PropertyChangedEventArgs("focusOne"));
            PropertyChanged(this, new PropertyChangedEventArgs("focusTwo"));
        }
        Ellipse Focus;
        public void FocusDraw()
        {
            BuildElipsHand beh = new BuildElipsHand(cv);


            foreach (var item in FocusPoint)
            {
                cv.Children.Remove(item);
            }
            cv.Children.Remove(line);
            FocusPoint.Clear();
            if (exs)
            {
                Focus = new Ellipse();
                Focus = beh.DrawPoinFocus(coordCX + c, coordCY);
                FocusPoint.Add(Focus);
                cv.Children.Add(Focus);

                focusOne = $"{coordCX + c}; {coordCY}";
                Focus = new Ellipse();

                Focus = beh.DrawPoinFocus(coordCX - c, coordCY);
                FocusPoint.Add(Focus);
                cv.Children.Add(Focus);
                focusTwo = $"{coordCX - c}; {coordCY}";
                line = new Line
                {
                    X1 = beh.convertCoordX(coordCX + c),
                    X2 = beh.convertCoordX(coordCX - c),
                    Y1 = beh.convertCoordY(coordCY),
                    Y2 = beh.convertCoordY(coordCY),
                    Stroke = Brushes.Black,
                    StrokeThickness = 2,
                    SnapsToDevicePixels = true
                };
                line.SetValue(RenderOptions.EdgeModeProperty, EdgeMode.Aliased);
                cv.Children.Add(line);
            }
            else
            {
                Focus = new Ellipse();
                Focus = beh.DrawPoinFocus(coordCX, coordCY + c);
                FocusPoint.Add(Focus);
                cv.Children.Add(Focus);

                focusOne = $"{coordCX}; {coordCY + c}";
                Focus = new Ellipse();
                Focus = beh.DrawPoinFocus(coordCX, coordCY - c);
                FocusPoint.Add(Focus);
                cv.Children.Add(Focus);
                focusTwo = $"{coordCX}; {coordCY - c}";


                line = new Line
                {
                    X1 = beh.convertCoordX(coordCX),
                    X2 = beh.convertCoordX(coordCX),
                    Y1 = beh.convertCoordY(coordCY + c),
                    Y2 = beh.convertCoordY(coordCY - c),
                    Stroke = Brushes.Black,
                    StrokeThickness = 2,
                    SnapsToDevicePixels = true
                };
                line.SetValue(RenderOptions.EdgeModeProperty, EdgeMode.Aliased);
                cv.Children.Add(line);
            }
            flag = true;
            Property();
        }
        public RoutedCommand clearCanvasCommand { get; set; } = new RoutedCommand();
        public CommandBinding clearCanvasBinding;
        public BuildElipsHands()
        {
            clearCanvasBinding = new CommandBinding(clearCanvasCommand);
            clearCanvasBinding.Executed += ClearCanvasBinding_Executed; ;
        }
        public void DrawPoint()
        {
            BuildElipsHand beh = new BuildElipsHand(cv);
            try
            {
                pointForElipse = new Ellipse()
                {
                    Width = 6,
                    Height = 6,
                    Fill = Brushes.Black,
                    Stroke = Brushes.Black,
                    StrokeThickness = 1
                };

                PointForEllipse.Add(pointForElipse);
                cv.Children.Add(pointForElipse);
                pointForElipse.SetValue(Canvas.LeftProperty, beh.convertCoordX(Point[0]) - 3);
                pointForElipse.SetValue(Canvas.TopProperty, beh.convertCoordY(Point[1]) - 3);

                if (exs)
                {
                    Focus = new Ellipse();
                    Focus = beh.DrawPoinFocus(coordCX + c, coordCY);
                    FocusPoint.Add(Focus);
                    cv.Children.Add(Focus);

                    Focus = new Ellipse();

                    Focus = beh.DrawPoinFocus(coordCX - c, coordCY);
                    FocusPoint.Add(Focus);
                    cv.Children.Add(Focus);
                    line = new Line
                    {
                        X1 = beh.convertCoordX(coordCX + c),
                        X2 = beh.convertCoordX(coordCX - c),
                        Y1 = beh.convertCoordY(coordCY),
                        Y2 = beh.convertCoordY(coordCY),
                        Stroke = Brushes.Black,
                        StrokeThickness = 2,
                        SnapsToDevicePixels = true
                    };
                }
                else
                {
                    Focus = new Ellipse();
                    Focus = beh.DrawPoinFocus(coordCX, coordCY + c);
                    FocusPoint.Add(Focus);

                    Focus = new Ellipse();

                    Focus = beh.DrawPoinFocus(coordCX, coordCY - c);
                    FocusPoint.Add(Focus);
                    cv.Children.Add(Focus);
                    line = new Line
                    {
                        X1 = beh.convertCoordX(coordCX),
                        X2 = beh.convertCoordX(coordCX),
                        Y1 = beh.convertCoordY(coordCY + c),
                        Y2 = beh.convertCoordY(coordCY - c),
                        Stroke = Brushes.Black,
                        StrokeThickness = 2,
                        SnapsToDevicePixels = true
                    };
                }
                line.SetValue(RenderOptions.EdgeModeProperty, EdgeMode.Aliased);
                cv.Children.Add(line);
                for (int i = 6; i < Point.Count - 1; i += 2)
                {
                    pointForElipse = new Ellipse()
                    {
                        Width = 3,
                        Height = 3,
                        Fill = Brushes.Black,
                        Stroke = Brushes.Black,
                        StrokeThickness = 1
                    };
                    PointForEllipse.Add(pointForElipse);
                    cv.Children.Add(pointForElipse);
                    pointForElipse.SetValue(Canvas.LeftProperty, beh.convertCoordX(Point[i]));
                    pointForElipse.SetValue(Canvas.TopProperty, beh.convertCoordY(Point[i + 1]));
                }
            }
            catch (Exception)
            {

            }

        }
        Line threadLine, threadLinetwo;
        public void DrawThread(double x, double y)
        {
            cv.Children.Remove(threadLine);
            cv.Children.Remove(threadLinetwo);
            BuildElipsHand beh = new BuildElipsHand(cv);
            if (exs)
            {
                threadLine = new Line()
                {
                    X1 = beh.convertCoordX(coordCX + c),
                    X2 = beh.convertCoordX(x),
                    Y1 = beh.convertCoordY(coordCY),
                    Y2 = beh.convertCoordY(y),
                    Stroke = Brushes.Black,
                    StrokeThickness = 1,
                    SnapsToDevicePixels = true
                };
               
                threadLinetwo = new Line()
                {
                    X1 = beh.convertCoordX(coordCX - c),
                    X2 = beh.convertCoordX(x),
                    Y1 = beh.convertCoordY(coordCY),
                    Y2 = beh.convertCoordY(y),
                    Stroke = Brushes.Black,
                    StrokeThickness = 1,
                    SnapsToDevicePixels = true
                };
            }
            else
            {
                threadLine = new Line()
                {
                    X1 = beh.convertCoordX(coordCX),
                    X2 = beh.convertCoordX(x),
                    Y1 = beh.convertCoordY(coordCY+c),
                    Y2 = beh.convertCoordY(y),
                    Stroke = Brushes.Black,
                    StrokeThickness = 1,
                    SnapsToDevicePixels = true
                };

                threadLinetwo = new Line()
                {
                    X1 = beh.convertCoordX(coordCX),
                    X2 = beh.convertCoordX(x),
                    Y1 = beh.convertCoordY(coordCY-c),
                    Y2 = beh.convertCoordY(y),
                    Stroke = Brushes.Black,
                    StrokeThickness = 1,
                    SnapsToDevicePixels = true
                };
            }
            threadLine.SetValue(RenderOptions.EdgeModeProperty, EdgeMode.Aliased);
            cv.Children.Add(threadLine);
            threadLinetwo.SetValue(RenderOptions.EdgeModeProperty, EdgeMode.Aliased);
            cv.Children.Add(threadLinetwo);
        }


        private void ClearCanvasBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            parametrC = false;
            foreach (var item in FocusPoint)
            {
                cv.Children.Remove(item);
            }
            foreach (var item in PointForEllipse)
            {
                cv.Children.Remove(item);
            }
            FocusPoint.Clear();
            PointForEllipse.Clear();
            Point.Clear();
            cv.Children.Remove(line);
            cv.Children.Remove(threadLine);
            cv.Children.Remove(threadLinetwo);
            coordCX = 0;
            coordCY = 0;
            focusOne = focusTwo = string.Empty;
            flag = false;
            centerElips = true;
           
            Property();
        }
    }
    class BuildElipsHand : GeometricPatterns
    {
        public BuildElipsHand(Canvas cv)
        {
            this.cv = cv;
            countX = Convert.ToDouble(cv.Tag);//получаем масштабы области
            countY = Math.Round(cv.ActualHeight / (cv.ActualWidth / countX));
            maxX = cv.ActualWidth; //получаем ширину канвы
            maxY = cv.ActualHeight; //получаем высоту канвы    
        }
        public double FindFocusElips(double x, double y)
        {
            return x > y ? Math.Sqrt(Math.Pow(x, 2) - Math.Pow(y, 2)) : Math.Sqrt(Math.Pow(y, 2) - Math.Pow(x, 2));
        }
        Ellipse PointFocus;
        public Ellipse DrawPoinFocus(double x, double y)
        {

            PointFocus = new Ellipse()//задаем прочие параметры
            {
                Width = (maxX / countX) * 0.2,
                Height = (maxX / countX) * 0.2,
                Fill = Brushes.Black,
                Stroke = Brushes.Black,
                StrokeThickness = 1
            };
            PointFocus.SetValue(Canvas.LeftProperty, convertCoordX(x - 0.1));
            PointFocus.SetValue(Canvas.TopProperty, convertCoordY(y + 0.1));
            return PointFocus;
        }
        /// <summary>
        /// метод для преобразования градусов в радиан
        /// </summary>
        /// <param name="r">значение в градусах (как нам надо с точки зрения математики)</param>
        /// <returns>значаение в радианах</returns>
        public double convertRadian(double r)
        {
            return r / 180 * Math.PI;
        }
    }
}
