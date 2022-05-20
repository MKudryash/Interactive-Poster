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
        Paint paint;
        DrawCircle c;
        MouseButtonState previousMouseEvent = new MouseButtonState();

        public Circle()
        {
            InitializeComponent();
            DataContext = MMC;
            CommandBindings.Add(MMC.SoundPlayBinding);
            paint = new Paint(PaintCanvas);
        }
        private void UpdateBackPattern(object sender, SizeChangedEventArgs e)
        {

            count = Convert.ToDouble(PaintCanvas.Tag);//вынимаем информацию о количестве клеток из самой канвы  
            MMC.MaxMinValueCoordinat = count / 2 - 1;//Максимальные и минамальные сдвиги по координатной плоскости
            SlPoint.Value = MMC.GradusValue;
            double countY = Math.Round(PaintCanvas.ActualHeight / (PaintCanvas.ActualWidth / count));
            MMC.GetCanvas = PaintCanvas;
            MMC.nameSound = "CircleSound";


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
            // наша целевая окружность
            c = new DrawCircle(slCoordX.Value, slCoordY.Value, slRadius.Value, Background, PaintCanvas);
            MMC.MaxRadius = (int)c.MaxRadius(slCoordX.Value, slCoordY.Value);
            Formula.Formula = c.CanonicalEquation();
        }

        private void Area_PreviewMouseMove(object sender, MouseEventArgs e)
        {
            UpdateBackPattern(null, null);
        }

        private void ComeBack(object sender, RoutedEventArgs e)
        {
            LoadPage.MainFrame.GoBack();
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
            else if (isMouse && !(bool)PaintDraw.IsChecked)
                c.ChangedGradus(sender, e);

            UpdateBackPattern(null, null);
        }

        private void BuildCircleOpen(object sender, RoutedEventArgs e)
        {
            LoadPage.MainFrame.Navigate(new BuildCircle());
           
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
            h = column/ 10;
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
    }
}
