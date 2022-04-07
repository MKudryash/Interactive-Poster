using InteractivePoster.BuildPages;
using InteractivePoster.Finction;
using System;
using System.IO;
using System.Media;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

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
        DrawCircle c;
        public Circle()
        {
            InitializeComponent();
            DataContext = MMC;
            CommandBindings.Add(MMC.SoundPlayBinding);
        }
        private void UpdateBackPattern(object sender, SizeChangedEventArgs e)
        {

            count = Convert.ToDouble(Background.Tag);//вынимаем информацию о количестве клеток из самой канвы  
            MMC.MaxMinValueCoordinat = count / 2 - 1;//Максимальные и минамальные сдвиги по координатной плоскости
            SlPoint.Value = MMC.GradusValue;
            double countY = Math.Round( Background.ActualHeight/ (Background.ActualWidth / count));
            MMC.GetCanvas = Background;
            MMC.nameSound = "CircleSound";


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
           lineY.DrawArrow(0, countY/2, Orientation.Vertical, 3, Background);
            // наша целевая окружность
         c = new DrawCircle(slCoordX.Value, slCoordY.Value, slRadius.Value, Background);
           MMC.MaxRadius = (int)c.MaxRadius(slCoordX.Value, slCoordY.Value);
            FormulaCircle.Formula = c.CanonicalEquation();
        }

        private void Area_PreviewMouseMove(object sender, MouseEventArgs e)
        {
            UpdateBackPattern(null, null);
        }

        private void ComeBack(object sender, RoutedEventArgs e)
        {
            LoadPage.MainFrame.GoBack();
        }

        MediaElement soundCircle = new MediaElement();
        bool isPlay = true;

        bool isMouse = false;
        private void MouseDown_Background(object sender, MouseButtonEventArgs e)
        {
            isMouse = true;
        }

        private void MouseUp_Background(object sender, MouseButtonEventArgs e)
        {
            isMouse = false;
        }

        private void MouseMove_Background(object sender, MouseEventArgs e)
        {
            if (isMouse)
            {
                c.ChangedGradus(sender, e);
            }
            UpdateBackPattern(null, null);
        }

        private void BuildCircleOpen(object sender, RoutedEventArgs e)
        {
            LoadPage.MainFrame.Navigate(new BuildCircle());
        }
    }
}
