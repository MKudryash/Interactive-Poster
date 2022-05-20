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
    /// Логика взаимодействия для BuildCircle.xaml
    /// </summary>
    public partial class BuildCircle : Page
    {
        double count, countY;
        BuildCircleHands BCH = new BuildCircleHands();
        Paint paint;
        MouseButtonState previousMouseEvent = new MouseButtonState();
        public BuildCircle()
        {
            InitializeComponent();
            DataContext = BCH;
            CommandBindings.Add(BCH.clearCanvasBinding);
            paint = new Paint(PaintCanvas);
        }

        private void MouseDown_Background(object sender, MouseButtonEventArgs e)
        {
            isMouse = true;
            if ((bool)PaintDraw.IsChecked)
            {
                paint.StartDraw(e);
            }
            else
            {
                if (BCH.flag)
                {
                    BCH.MouseDown = true;
                    BCH.StartDraw(e);
                }
                else
                {
                    if (BCH.RadiusCircle)
                    {
                        BCH.isMouseDownRadius = false;
                        BCH.GetPointRadius(e);

                        RadiusCircleCheck.IsChecked = false;
                    }

                    if (BCH.CenterCircle)
                    {
                        BCH.GetCenterPoint(e);
                        BCH.CenterCircle = false;
                        RadiusCircleCheck.IsChecked = true;
                    }
                }
            }
         

        }

        bool isMouse = false;
        private void MouseUp_Background(object sender, MouseButtonEventArgs e)
        {
            BCH.MouseDown = false;
            isMouse = false;
            BCH.IsMouseDownRadius = false;
        }

        private void MouseMove_Background(object sender, MouseEventArgs e)
        {
            MaxMinCoordinat.Eraser = false;
            if ((bool)PaintDraw.IsChecked && !(bool)EraserCB.IsChecked&& isMouse)
            {
                if (e.LeftButton == MouseButtonState.Pressed)
                {
                    PaintCanvas.Children.Remove(paint.currentPath);

                    paint.BuildPoint(e);
                }
            }          
             if ((bool)EraserCB.IsChecked && isMouse)
            {
                MaxMinCoordinat.Eraser = true;
                paint.RemoveObj(sender, e);
            }
            
            if (BCH.MouseDown && !(bool)EraserCB.IsChecked)
            {
                Background.Children.Remove(BCH.currentPath);
                BCH.BuildCirclePoint(e);
            }
            if (e.LeftButton == MouseButtonState.Released && previousMouseEvent == MouseButtonState.Pressed)
            {
                paint.rr();
            }
            previousMouseEvent = e.LeftButton;
           
        }

        private void UpdateBackPattern(object sender, SizeChangedEventArgs e)
        {
            count = Convert.ToDouble(PaintCanvas.Tag);//вынимаем информацию о количестве клеток из самой канвы  
            countY = Math.Round(PaintCanvas.ActualHeight / (PaintCanvas.ActualWidth / count));
            BCH.GetCanvas = Background;
            BCH.GetCanvass = PaintCanvas;

            Background.Children.Clear();

            //просто добавляем на канву объекты наших созданных классов            
            for (double i = -count / 2; i < count / 2; i++)
            {
                DrawСoordinateLine lineH = new DrawСoordinateLine(i, Orientation.Horizontal, 1, Background, PaintCanvas);
                DrawСoordinateLine lineV = new DrawСoordinateLine(i, Orientation.Vertical, 1, Background, PaintCanvas);
            }
            DrawСoordinateLine lineX = new DrawСoordinateLine(0, Orientation.Horizontal, 3, Background, PaintCanvas);
            DrawСoordinateLine lineY = new DrawСoordinateLine(0, Orientation.Vertical, 3, Background, PaintCanvas);
            lineX.DrawArrow(count / 2, 0, Orientation.Horizontal, 3, Background);
            lineY.DrawArrow(0, countY / 2, Orientation.Vertical, 3, Background);

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

        private void Redo(object sender, RoutedEventArgs e)
        {
            paint.Redo();
        }

        private void Undo(object sender, RoutedEventArgs e)
        {
            paint.Undo();
        }

        private void ClearAll(object sender, RoutedEventArgs e)
        {
            paint.ClearAll();
        }

        private void Eraser(object sender, MouseButtonEventArgs e)
        {
            paint.RemoveObj(sender, e);
        }

        double h;
        private async void TgBtn_Unchecked(object sender, RoutedEventArgs e)
        {

            double column = 30;
            for (int i = 0; i < 10; i++)
            {
                ColumnForElement.Width = new GridLength(column);
                GridCanvas.Width = column;
                column += h;
                await System.Threading.Tasks.Task.Delay(50);
            }
            ElementStack.Visibility = Visibility.Visible;
        }

        private async void TgBtn_Checked(object sender, RoutedEventArgs e)
        {
            double column = ColumnForElement.ActualWidth;
            h = column / 10;
            for (int i = 0; i < 10; i++)
            {
                ColumnForElement.Width = new GridLength(column);
                GridCanvas.Width = column;
                column -= h;
                await System.Threading.Tasks.Task.Delay(50);
            }
            ElementStack.Visibility = Visibility.Collapsed;
            GridCanvas.Width = 30;
        }

        private void PlaySound(object sender, RoutedEventArgs e)
        {
            soundCircle.LoadedBehavior = MediaState.Manual;
            soundCircle.Source = new Uri("Resource\\Sounds\\CircleSound.mp3", UriKind.RelativeOrAbsolute);
            soundCircle.Position = TimeSpan.Zero;
            if (isPlay)
            {
                Background.Children.Add(soundCircle);
                soundCircle.Play();
                isPlay = false;
            }
            else
            {
                Background.Children.Remove(soundCircle);
                soundCircle.Stop();
                isPlay = true;
            }
        }
    }
}
