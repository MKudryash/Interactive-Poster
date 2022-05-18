﻿using InteractivePoster.Finction;
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
using WpfMath;

namespace InteractivePoster.Pages
{
    /// <summary>
    /// Логика взаимодействия для Parabola.xaml
    /// </summary>
    public partial class Parabola : Page
    {
        double count;
        MaxMinCoordinat MMC = new MaxMinCoordinat();
        Paint paint;
        MouseButtonState previousMouseEvent = new MouseButtonState();
        public Parabola()
        {
            InitializeComponent();
            DataContext = MMC;
            CommandBindings.Add(MMC.SoundPlayBinding);
            paint = new Paint(PaintCanvas);
        }

        private void UpdateBackPattern(object sender, SizeChangedEventArgs e)
        {
            count = Convert.ToDouble(PaintCanvas.Tag);//вынимаем информацию о количестве клеток из самой канвы       
            double countY = Math.Round(PaintCanvas.ActualHeight / (PaintCanvas.ActualWidth / count));
            MMC.GetCanvas = PaintCanvas;
            MMC.nameSound = "ParabolaSound";
            if (MaxMinCoordinat.equationforParabola)
            {
                MMC.MaxMinPoint = countY / 2;
            }
            else
            {
                MMC.MaxMinPoint = count / 2;
            }
          

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

            DrawParabola drawParabola = new DrawParabola(slCoordX.Value, slCoordY.Value, Background,SlParametrParabola.Value, slCoordPoint.Value,PaintCanvas);
            Path path = new Path();
            path.Data = drawParabola.Parabola();
            path.Stroke = Brushes.Black;
            path.StrokeThickness = 3;

            Background.Children.Add(path);

            Formula.Formula = drawParabola.CanonicalEquation();
            string latex = drawParabola.CanonicalEquation();

            var parser = new TexFormulaParser();
            var formula = parser.Parse(latex);
        }
       
        private void Area_PreviewMouseMove(object sender, MouseEventArgs e)
        {
            UpdateBackPattern(null, null);
        }

        private void ComeBack(object sender, RoutedEventArgs e)
        {
            LoadPage.MainFrame.GoBack();
        }
        

        private void ChangedParabola(object sender, RoutedEventArgs e)
        {
            UpdateBackPattern(null, null);
        }
        MediaElement soundCircle = new MediaElement();
        bool isPlay = true;
        private void PlaySound(object sender, RoutedEventArgs e)
        {
            soundCircle.LoadedBehavior = MediaState.Manual;
            soundCircle.Source = new Uri("Resource\\Sounds\\ParabolaSound.mp3", UriKind.RelativeOrAbsolute);
            soundCircle.Position = TimeSpan.Zero;
            if (!isPlay)
            {
                soundCircle.Stop();
                Background.Children.Remove(soundCircle);
               
                isPlay = true;
            }
            else
            {
                Background.Children.Add(soundCircle);
                soundCircle.Play();
                isPlay = false;
                
            }
        }

        private void Update(object sender, RoutedEventArgs e)
        {
            UpdateBackPattern(null, null);
        }

        private void Redo(object sender, RoutedEventArgs e)
        {
            paint.Redo();
        }

        private void Undo(object sender, RoutedEventArgs e)
        {
            paint.Undo();
        }

        bool isMouse = false;
        private void MouseDown_Background(object sender, MouseButtonEventArgs e)
        {
            isMouse = true;
            paint.StartDraw(e);
        }

        private void MouseUp_Background(object sender, MouseButtonEventArgs e)
        {
            isMouse = false;
        }

        private void MouseMove_Background(object sender, MouseEventArgs e)
        {
            if ((bool)PaintDraw.IsChecked && !(bool)EraserCB.IsChecked && isMouse)
            {
                if (e.LeftButton == MouseButtonState.Pressed)
                {
                    PaintCanvas.Children.Remove(paint.currentPath);

                    paint.BuildPoint(e);
                }             
            }
            else if (e.LeftButton == MouseButtonState.Released && previousMouseEvent == MouseButtonState.Pressed)
            {
                paint.rr();

            }
            previousMouseEvent = e.LeftButton;
            if ((bool)EraserCB.IsChecked && isMouse)
            {
                paint.RemoveObj(sender, e);
            }
            UpdateBackPattern(null, null);
        }

        private void ClearAll(object sender, RoutedEventArgs e)
        {
            paint.ClearAll();
        }

        private void Eraser(object sender, MouseButtonEventArgs e)
        {
            paint.RemoveObj(sender, e);
        }
    }
}
