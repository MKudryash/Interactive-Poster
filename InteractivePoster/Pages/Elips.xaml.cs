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
    /// Логика взаимодействия для Elips.xaml
    /// </summary>
    public partial class Elips : Page
    {
        double count;
        MaxMinCoordinat MMC = new MaxMinCoordinat();
        public Elips()
        {
            InitializeComponent();
            DataContext = MMC;
        }
        private void UpdateBackPattern(object sender, SizeChangedEventArgs e)
        {
            Formula.Formula = @"\frac{(x-"+ slCoordX.Value+@")^2}{" + slRadiusW.Value+@"^2}+ \frac{(y-"+ slCoordY.Value + @")^2}{"+ slRadiusH.Value + @"^2} = 1";
            count = Convert.ToDouble(Background.Tag);//вынимаем информацию о количестве клеток из самой канвы  
            MMC.MaxMinValueCoordinat = count / 2 - 1;//Максимальные и минамальные сдвиги по координатной плоскости
            MMC.GradusValue = (int)SlPoint.Value;


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
            // наша целевая окружность
            DrawElips c = new DrawElips(slCoordX.Value, slCoordY.Value, slRadiusW.Value, slRadiusH.Value, Background,SlTransform.Value);
           // MMC.MaxRadius = (int)c.MaxRadius(slCoordX.Value, slCoordY.Value);
        }

        private void Area_PreviewMouseMove(object sender, MouseEventArgs e)
        {
            UpdateBackPattern(null,null);
        }

        private void ComeBack(object sender, RoutedEventArgs e)
        {
            LoadPage.MainFrame.GoBack();
        }
    }
}
