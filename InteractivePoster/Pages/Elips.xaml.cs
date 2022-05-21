using InteractivePoster.BuildPages;
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
        Paint paint;
        MouseButtonState previousMouseEvent = new MouseButtonState();
        public Elips()
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
            SlPoint.Value =  MMC.GradusValue;
            double countY = Math.Round(PaintCanvas.ActualHeight / (PaintCanvas.ActualWidth / count));
            MMC.GetCanvas = PaintCanvas;
            MMC.nameSound = "ElipsSound";

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
            c = new DrawElips(slCoordX.Value, slCoordY.Value, slRadiusW.Value, slRadiusH.Value, Background,SlTransform.Value,PaintCanvas);
            Formula.Formula = c.CanonicalEquation();
        }
        DrawElips c;
        private void Area_PreviewMouseMove(object sender, MouseEventArgs e)
        {
            UpdateBackPattern(null,null);
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
            if (!(bool)PaintTgBtn.IsChecked)
            {
                if (!(bool)EraserCB.IsChecked && isMouse)
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
            }
            else
                  if (isMouse)
            {
                c.ChangedGradus(sender, e);
            }
            UpdateBackPattern(null, null);
        }
        private void ChangedElipsFormula(object sender, RoutedEventArgs e)
        {
            UpdateBackPattern(null, null);
        }

        private void BuildElipsOpen(object sender, RoutedEventArgs e)
        {
            LoadPage.MainFrame.Navigate(new BiuldElipse());

        }

        private void SinCos(object sender, RoutedEventArgs e)
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
            h = column  / 10;
            for (int i = 0; i < 10; i++)
            {
                ColumnForElement.Width = new GridLength(column);
                GridCanvas.Width = column;
                column -= h;
                await System.Threading.Tasks.Task.Delay(50);
            }
            ElementStack.Visibility = Visibility.Collapsed;
            GridCanvas.Width = 35;
        }

      
        ChangeTheme CT = new ChangeTheme();
        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            if (PaintCanvas != null)
                CT.Checked(AllGrid);
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

        private void OpenPaint(object sender, RoutedEventArgs e)
        {
            LoadPage.MainFrame.Navigate(new PaintPage());
        }
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
    }
}

