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

        public double GetC
        {
            get => c;
            set { c = value; }
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
            BuildCircleHand buildCircleHand = new BuildCircleHand(cv);
            centreX = e.GetPosition(buildCircleHand.cv).X;
            centreY = e.GetPosition(buildCircleHand.cv).Y;
            coordCX = Math.Round(buildCircleHand.convertXCoord(centreX), 1);
            coordCY = Math.Round(buildCircleHand.convertYCoord(centreY), 1);


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
            pointForElipse.SetValue(Canvas.LeftProperty, buildCircleHand.convertCoordX(coordCX) - 3);
            pointForElipse.SetValue(Canvas.TopProperty, buildCircleHand.convertCoordY(coordCY) - 3);
            centerElips = false;
            parametrC = true;
            Property();
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
            cv.Children.Remove(line);
            coordCX = 0;
            coordCY = 0;
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
