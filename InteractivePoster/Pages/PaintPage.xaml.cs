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
    /// Логика взаимодействия для PaintPage.xaml
    /// </summary>
    public partial class PaintPage : Page
    {
        Paint paint;
        MouseButtonState previousMouseEvent = new MouseButtonState();
        MaxMinCoordinat MMC = new MaxMinCoordinat();
        public PaintPage()
        {
            InitializeComponent();
            paint = new Paint(PaintCanvas);
            DataContext = MMC;
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
            if (!(bool)EraserCB.IsChecked&&isMouse)
            {
                if (e.LeftButton == MouseButtonState.Pressed)
                {
                    PaintCanvas.Children.Remove(paint.currentPath);
                    paint.BuildPoint(e);
                }
                else if (e.LeftButton == MouseButtonState.Released && previousMouseEvent == MouseButtonState.Pressed)
                {
                    paint.rr();
                }
                previousMouseEvent = e.LeftButton;
            }
            if ((bool)EraserCB.IsChecked)
            {
                paint.RemoveObj(sender,e);
            }
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
        double count;
        private void UpdateBackPattern(object sender, SizeChangedEventArgs e)
        {
            count = Convert.ToDouble(PaintCanvas.Tag);//вынимаем информацию о количестве клеток из самой канвы  

            double countY = Math.Round(PaintCanvas.ActualHeight / (PaintCanvas.ActualWidth / count));

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


        private void CoordinateLine_Click(object sender, RoutedEventArgs e)
        {
            if ((bool)CoordinateLine.IsChecked)
            {
                Background.Visibility = Visibility.Visible;
            }
            else
                Background.Visibility = Visibility.Collapsed;
            UpdateBackPattern(null, null);
        }

        private void colorPicker_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (colorPicker.SelectedColor != null)
            {
                paint.GetBrush(new SolidColorBrush((Color)colorPicker.SelectedColor));
            }
        }
    }
}
