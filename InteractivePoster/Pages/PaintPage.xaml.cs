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
        public static RoutedCommand MyCommand = new RoutedCommand();
        public void MyCommandExecuted(object sender, ExecutedRoutedEventArgs e) {
            paint.Undo();
        
    }
        private void ComeBack(object sender, RoutedEventArgs e)
        {
            LoadPage.MainFrame.GoBack();
        }

        bool isMouse = false;
        private void MouseDown_Background(object sender, MouseButtonEventArgs e)
        {
          
            isMouse = true;
            AreaGB.IsEnabled = false;
            paint.StartDraw(e);
        }

        private void MouseUp_Background(object sender, MouseButtonEventArgs e)
        {
            isMouse = false;
        }

        private void MouseMove_Background(object sender, MouseEventArgs e)
        {
            if (!(bool)EraserCB.IsChecked&&isMouse)
                MyCommand.InputGestures.Add(new KeyGesture(Key.Z, ModifierKeys.Control));
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
            AreaGB.IsEnabled = true;

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

        private void PaintCanvas_StylusDown(object sender, StylusDownEventArgs e)
        {
            isMouse = true;
            AreaGB.IsEnabled = false;
            paint.StartDrawStylus(e);
        }

        ChangeTheme CT = new ChangeTheme();
        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            if (PaintCanvas != null)
                CT.Checked(AllGrid);
        }
        private void CoordinateLine_Click(object sender, RoutedEventArgs e)
        {
            if ((bool)CoordinateLine.IsChecked)
            {
                Background.Visibility = Visibility.Visible;
                AreaGB.IsEnabled = true;
            }
            else
            {
                Background.Visibility = Visibility.Collapsed;
                AreaGB.IsEnabled = false;
            }
            UpdateBackPattern(null, null);
        }
        private void RadioButton_Unchecked(object sender, RoutedEventArgs e)
        {
            CT.Unchecked(AllGrid);
            
        }
        double hh;
        private async void PaintTgBtn_Unchecked(object sender, RoutedEventArgs e)
        {
            row = 30;
            GridElement.Visibility = Visibility.Visible;
            PaintElementStack.Visibility = Visibility.Visible;
            for (int i = 0; i < 10; i++)
            {
                GridHide.Height = new GridLength(row);

                row += hh;
                await System.Threading.Tasks.Task.Delay(50);
            }
            GridHide.Height = new GridLength(weirow);
        }
        double row, weirow;


        private void ChangedStrokeThickness(object sender, MouseEventArgs e)
        {
            paint.strokeThickness = (int)ThicknessPero.Value;
        }

        private void changeColor(object sender, RoutedEventArgs e)
        {
            int numberColor = Convert.ToInt32((sender as Button).Tag.ToString());
            colorPicker.SelectedColor = CT.ChangedColor(numberColor);
            paint.GetBrush(new SolidColorBrush((Color)colorPicker.SelectedColor));
        }


        private void colorPicker_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (colorPicker.SelectedColor != null)
            {
                paint.GetBrush(new SolidColorBrush((Color)colorPicker.SelectedColor));
            }
        }

        private async void PaintTgBtn_Checked(object sender, RoutedEventArgs e)
        {
            weirow = row = GridHide.ActualHeight;
            hh = (row - 20) / 10;
            for (int i = 0; i < 10; i++)
            {
                GridHide.Height = new GridLength(row);

                row -= hh;
                await System.Threading.Tasks.Task.Delay(50);
            }
            GridElement.Visibility = Visibility.Collapsed;
            PaintElementStack.Visibility = Visibility.Collapsed;
            BurgerGridRow.Height = new GridLength(30);
        }
    

        private void OpenPaint(object sender, RoutedEventArgs e)
        {
            LoadPage.MainFrame.Navigate(new MenuPage());
        }


    }
}
