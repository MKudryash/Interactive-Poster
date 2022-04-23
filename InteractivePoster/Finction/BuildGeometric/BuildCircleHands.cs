using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

using System.Windows;
using System.Windows.Controls;
using System.Windows.Shapes;
using System.Windows.Media;

namespace InteractivePoster.Finction.BuildGeometric
{
    class BuildCircleHands : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        Ellipse pointForCircle;
        List<Ellipse> PointForCircle { get; set; } = new List<Ellipse>();
        List<Path> pathFigure { get; set; } = new List<Path>();
       

        //вспомогательные перменные для построение в ручную
        public static Canvas cv { get; set; }
        public Canvas GetCanvas { get => cv; set { cv = value; } }

        public bool mouseDown = false;
        public bool isMouseDownRadius = false;
        public bool IsMouseDownRadius
        {
            get { return isMouseDownRadius; }
            set
            {
                isMouseDownRadius = value;
            }
        }
        public bool MouseDown
        {
            get { return mouseDown; }
            set
            {
                mouseDown = value;
            }
        }

        public bool centerCircle = true;
        public bool flag = false;
        public bool CenterCircle
        {
            get => centerCircle;
            set
            {
                centerCircle = value;
            }
        }
        public bool RadiusCircle { get; set; } = false;

        public double coordCX { get; set; } = 0;
        public double coordCY { get; set; } = 0;
        public double circleR { get; set; } = 0;

        double centreX, centreY,coordCirX, coordCirY;
        public void GetCenterPoint(MouseEventArgs e)
        {
            BuildCircleHand buildCircleHand = new BuildCircleHand(cv);
            centreX = e.GetPosition(buildCircleHand.cv).X;
            centreY = e.GetPosition(buildCircleHand.cv).Y;
            coordCX = Math.Round(buildCircleHand.convertXCoord(centreX), 1);
            coordCY = Math.Round(buildCircleHand.convertYCoord(centreY), 1);
           
            pointForCircle = new Ellipse()
            {
                Width = 6,
                Height = 6,
                Fill = Brushes.Black,
                Stroke = Brushes.Black,
                StrokeThickness = 1
            };

            PointForCircle.Add(pointForCircle);

            cv.Children.Add(pointForCircle);
            pointForCircle.SetValue(Canvas.LeftProperty, buildCircleHand.convertCoordX(coordCX) - 3);
            pointForCircle.SetValue(Canvas.TopProperty, buildCircleHand.convertCoordY(coordCY) - 3);

            CenterCircle = false;
            RadiusCircle = true;
            Property();
        }
        public void Property()
        {
            PropertyChanged(this, new PropertyChangedEventArgs("CenterCircle"));
            PropertyChanged(this, new PropertyChangedEventArgs("RadiusCircle"));
            PropertyChanged(this, new PropertyChangedEventArgs("coordCX"));
            PropertyChanged(this, new PropertyChangedEventArgs("coordCY"));
            PropertyChanged(this, new PropertyChangedEventArgs("circleR"));
        }
        Line line;
        public void GetPointRadius(MouseEventArgs e)
        {
            BuildCircleHand buildCircleHand = new BuildCircleHand(cv);
            coordCirX = buildCircleHand.convertXCoord( e.GetPosition(cv).X);
            coordCirY = buildCircleHand.convertYCoord( e.GetPosition(cv).Y);

            line = new Line()
            {
                Stroke = Brushes.Black,
                StrokeThickness = 3,
                SnapsToDevicePixels = true,
                X1 = buildCircleHand.convertCoordX(coordCX),
                X2 = e.GetPosition(cv).X,
                Y1 = buildCircleHand.convertCoordY(coordCY),
                Y2 = e.GetPosition(cv).Y
            };
            circleR = Math.Round(Math.Sqrt(Math.Pow(buildCircleHand.convertYCoord(e.GetPosition(cv).Y) - buildCircleHand.convertYCoord(centreY), 2)
                + Math.Pow(buildCircleHand.convertXCoord(e.GetPosition(cv).X) - buildCircleHand.convertXCoord(centreX), 2)), 2);
           
            Property();
            line.SetValue(RenderOptions.EdgeModeProperty, EdgeMode.Aliased);
            cv.Children.Add(line);
            if (!isMouseDownRadius)
            {
                flag = true;
                CenterCircle = false;
            }
        }
        public async void BuildCirclePoint(MouseEventArgs e)
        {
            BuildCircleHand buildCircleHand = new BuildCircleHand(cv);
            double x = e.GetPosition(cv).X;
            double y = e.GetPosition(cv).Y;


            double coordX = buildCircleHand.convertXCoord(x);
            double coordY = buildCircleHand.convertYCoord(y);


            double p = Math.Atan((coordY - coordCY) / (coordX - coordCX));


            if (coordX < coordCX) { p = p + Math.PI; }


            double circleX = coordCX + circleR * Math.Cos(p);
            double circleY = coordCY + circleR * Math.Sin(p);
          
            Point ppp = new Point(buildCircleHand.convertCoordX(circleX), buildCircleHand.convertCoordY(circleY));
            currentFigure.Segments.Add(new LineSegment(ppp, isStroked: true));
            currentPath.Data = new PathGeometry() { Figures = { currentFigure } };
            cv.Children.Add(currentPath);
            pathFigure.Add(currentPath);          
        }
       
       
        public Point startPoint;                                          
        Brush currentBrush = Brushes.Black;
        PathFigure currentFigure;                                 
        public Path currentPath = null;
        
        public void StartDraw(MouseEventArgs e)
        {
            BuildCircleHand buildCircleHand = new BuildCircleHand(cv);
            double x = e.GetPosition(cv).X;
            double y = e.GetPosition(cv).Y;
            double coordX = buildCircleHand.convertXCoord(x);
            double coordY = buildCircleHand.convertYCoord(y);
            double p = Math.Atan((coordY - coordCY) / (coordX - coordCX));
            if (coordX < coordCX) { p = p + Math.PI; }


            double circleX = coordCX + circleR * Math.Cos(p);
            double circleY = coordCY + circleR * Math.Sin(p);
           startPoint  = new Point(buildCircleHand.convertCoordX(circleX), buildCircleHand.convertCoordY(circleY));
            currentFigure = new PathFigure() { StartPoint = startPoint };
            System.Windows.Shapes.Path path = new System.Windows.Shapes.Path()
            {
                Stroke = currentBrush,
                StrokeThickness = 2,
                Data = new PathGeometry() { Figures = { currentFigure } }
            };          
            cv.Children.Add(path);
            currentPath = path;
            pathFigure.Add(currentPath);
        }
        public RoutedCommand clearCanvasCommand { get; set; } = new RoutedCommand();
        public CommandBinding clearCanvasBinding;
        public BuildCircleHands()
        {
            clearCanvasBinding = new CommandBinding(clearCanvasCommand);
            clearCanvasBinding.Executed += ClearCanvasBinding_Executed; ;
        }

        private void ClearCanvasBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            CenterCircle = false;
            foreach (var item in PointForCircle)
            {
                cv.Children.Remove(item);
            }
            foreach (var item in pathFigure)
            {
                cv.Children.Remove(item);
            }
           
            PointForCircle.Clear();
          
            cv.Children.Remove(line);
            coordCX = 0;
            coordCY = 0;
            circleR = 0;
            flag = false;
            CenterCircle = true;
            Property();
        }

      

    }
    class BuildCircleHand : GeometricPatterns
    {
        public BuildCircleHand(Canvas cv)
        {
            this.cv = cv;
            countX = Convert.ToDouble(cv.Tag);//получаем масштабы области
            countY = Math.Round(cv.ActualHeight / (cv.ActualWidth / countX));
            maxX = cv.ActualWidth; //получаем ширину канвы
            maxY = cv.ActualHeight; //получаем высоту канвы    
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
