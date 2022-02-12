using InteractivePoster.Finction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace InteractivePoster.Pages
{
    /// <summary>
    /// Логика взаимодействия для Parabola.xaml
    /// </summary>
    public partial class Parabola : Page
    {
        double count;
        MaxMinCoordinat MMC = new MaxMinCoordinat();
        public Parabola()
        {
            InitializeComponent();
            DataContext = MMC;
        }

        private void UpdateBackPattern(object sender, SizeChangedEventArgs e)
        {
            count = Convert.ToDouble(Background.Tag);//вынимаем информацию о количестве клеток из самой канвы       

            Background.Children.Clear();
            //просто добавляем на канву объекты наших созданных классов            
            for (double i = -count / 2; i < count / 2; i++)
            {
                DrawСoordinateLine lineH = new DrawСoordinateLine(i, Orientation.Horizontal, 1, Background);
                DrawСoordinateLine lineV = new DrawСoordinateLine(i, Orientation.Vertical, 1, Background);
            }
            DrawСoordinateLine lineX = new DrawСoordinateLine(0, Orientation.Horizontal, 3, Background);
            DrawСoordinateLine lineY = new DrawСoordinateLine(0, Orientation.Vertical, 3, Background);
            lineX.DrawArrow(count / 2, 0, Orientation.Horizontal, 3, Background);
            lineY.DrawArrow(0, count / 2, Orientation.Vertical, 3, Background);

            DrawParabola drawParabola = new DrawParabola(slCoordX.Value, slCoordY.Value, Background,SlParametrParabola.Value,SlParametrParabolaX.Value);
            Path path = new Path();
            path.Data = drawParabola.Parabola();
            path.Stroke = Brushes.Black;
            path.StrokeThickness = 3;


            double maxX = Background.ActualWidth;
            double y = slCoordX.Value * slCoordX.Value / 2 * SlParametrParabola.Value * (-1)+slCoordY.Value;

            RotateTransform rotateTransform = new RotateTransform();
            rotateTransform.CenterX = maxX / 2 + slCoordX.Value * (maxX / count); //центр оси X по отношению к параболе, не к координатной плоскости
            rotateTransform.CenterY = maxX / 2 + y * (-1)*(maxX / count);//центр оси Y по отношению к параболе, не к координатной плоскости
            rotateTransform.Angle = SlTransform.Value;//поворот на количетсво градусов     
            path.RenderTransform = rotateTransform;

            Background.Children.Add(path);
        }
       
        private void Area_PreviewMouseMove(object sender, MouseEventArgs e)
        {
            UpdateBackPattern(null, null);
        }

        private void ComeBack(object sender, RoutedEventArgs e)
        {
            LoadPage.MainFrame.GoBack();
        }
    }
}
