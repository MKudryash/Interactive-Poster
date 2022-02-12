using InteractivePoster.Finction;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace InteractivePoster.Pages
{
    /// <summary>
    /// Логика взаимодействия для Circle.xaml
    /// </summary>
    /// 

    public partial class Circle : Page
    {
        double count;
        MaxMinCoordinat MMC = new MaxMinCoordinat();
        public Circle()
        {
            InitializeComponent();
            DataContext = MMC;
        }
        private void UpdateBackPattern(object sender, SizeChangedEventArgs e)
        {
           
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
            lineX.DrawArrow(count/2,0, Orientation.Horizontal, 3, Background);
            lineY.DrawArrow(0,count/2, Orientation.Vertical, 3, Background);
            // наша целевая окружность
            DrawCircle c = new DrawCircle(slCoordX.Value, slCoordY.Value, slRadius.Value, Background);
            MMC.MaxRadius = (int)c.MaxRadius(slCoordX.Value, slCoordY.Value);
        }

        private void Area_PreviewMouseMove(object sender, MouseEventArgs e)
        {
            UpdateBackPattern(null, null);
        }
    }
}
