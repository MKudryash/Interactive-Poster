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
    public partial class Circle : Page
    {
        double count;
        public Circle()
        {
            InitializeComponent();
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
            // наша целевая окружность
            DrawCircle c = new DrawCircle(slCoordX.Value, slCoordY.Value, slRadius.Value, Background);
        }

        private void Area_PreviewMouseMove(object sender, MouseEventArgs e)
        {
            UpdateBackPattern(null, null);
        }
    }
}
