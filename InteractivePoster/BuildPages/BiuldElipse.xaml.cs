﻿using InteractivePoster.Finction;
using InteractivePoster.Finction.BuildGeometric;
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

namespace InteractivePoster.BuildPages
{
    /// <summary>
    /// Логика взаимодействия для BiuldElipse.xaml
    /// </summary>
    public partial class BiuldElipse : Page
    {
        double count, countY;
        BuildElipsHands BEH = new BuildElipsHands();
        public BiuldElipse()
        {
            InitializeComponent();
            DataContext = BEH;
           // CommandBindings.Add(BCH.clearCanvasBinding);
        }

        private void UpdateBackPattern(object sender, SizeChangedEventArgs e)
        {
            count = Convert.ToDouble(Background.Tag);//вынимаем информацию о количестве клеток из самой канвы  
            countY = Math.Round(Background.ActualHeight / (Background.ActualWidth / count));
            BEH.GetCanvas = Background;

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
            lineY.DrawArrow(0, countY / 2, Orientation.Vertical, 3, Background);
           // BCH.DrawPoint();
        }

        private void MouseDown_Background(object sender, MouseButtonEventArgs e)
        {
            if (BEH.flag)
            {
                BEH.MouseDown = true;
            }
            else
            {
                //if (BCH.RadiusCircle)
                //{
                //    BCH.isMouseDownRadius = false;
                //    BCH.GetPointRadius(e);
                //    //    UpdateBackPattern(null, null);
                //    RadiusCircleCheck.IsChecked = false;
                //}

                if (BEH.centerElips)
                {
                    BEH.GetCenterPoint(e);
                   // BCH.CenterCircle = false;
                   // RadiusCircleCheck.IsChecked = true;
                }
            }
        }

        private void MouseUp_Background(object sender, MouseButtonEventArgs e)
        {

        }

        private void MouseMove_Background(object sender, MouseEventArgs e)
        {

        }

        private void Area_PreviewMouseMove(object sender, MouseEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            BEH.parametrC = false;
            BEH.FocusDraw();
        }

        private void ComeBack(object sender, RoutedEventArgs e)
        {
            LoadPage.MainFrame.GoBack();
        }
    }
}
