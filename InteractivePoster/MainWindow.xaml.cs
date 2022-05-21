using InteractivePoster.Finction;
using InteractivePoster.Pages;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace InteractivePoster
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        MaxMinCoordinat MMC = new MaxMinCoordinat();
        public MainWindow()
        {
            InitializeComponent();
            frmMain.Navigate(new MenuPage(MMC));
            LoadPage.MainFrame = frmMain;
            DataContext = MMC;
        }

        private void ComeBack(object sender, RoutedEventArgs e)
        {
            try
            {
                LoadPage.MainFrame.GoBack();
                MMC.StackPaint = false;
            }
            catch
            {
                //
            }

        }

        double h;
        private async void TgBtn_Unchecked(object sender, RoutedEventArgs e)
        {
            double column = 30;
            GridElement.Visibility = Visibility.Visible;
            ElementStack.Visibility = Visibility.Visible;
            for (int i = 0; i < 10; i++)
            {
                GridHide.Height = column;
                GridRow.Height = new GridLength(column);
                column += h;
                await System.Threading.Tasks.Task.Delay(50);
            }
        }

        private async void TgBtn_Checked(object sender, RoutedEventArgs e)
        {
            double column = GridHide.ActualHeight;
            h = (column - 20) / 10 + 0.5;
            for (int i = 0; i < 10; i++)
            {
                GridHide.Height = column;
                GridRow.Height = new GridLength(column);
                column -= h;
                await System.Threading.Tasks.Task.Delay(50);
            }
            GridElement.Visibility = Visibility.Collapsed;
            ElementStack.Visibility = Visibility.Collapsed;
            BurgerGridRow.Height = new GridLength(30);
        }
        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            Application.Current.Resources["TwoColor"] = new SolidColorBrush(Color.FromRgb(0, 172, 193));
            Application.Current.Resources["ThreeColor"] = new SolidColorBrush(Colors.White);
            Application.Current.Resources["Tools"] = new SolidColorBrush(Color.FromRgb(108, 165, 250));
            Application.Current.Resources["Figure"] = new SolidColorBrush(Color.FromRgb(248, 94, 94));
            Application.Current.Resources["Border"] = new SolidColorBrush(Color.FromRgb(0, 172, 193));
            Application.Current.Resources["tgBtn_default"] = new ImageBrush(new BitmapImage(new System.Uri("Resource/Images/tgBtn_default.png", UriKind.RelativeOrAbsolute)));
            Application.Current.Resources["tb_mouse_over"] = new ImageBrush(new BitmapImage(new System.Uri("Resource/Images/tgBtn_MouseOver.png", UriKind.RelativeOrAbsolute)));
            if (WindowGrid.ActualWidth != 0)
            {
                WindowGrid.Width = WindowGrid.ActualWidth + 1;
            }
        }

        private void RadioButton_Unchecked(object sender, RoutedEventArgs e)
        {
            Application.Current.Resources["TwoColor"] = new SolidColorBrush(Color.FromRgb(243, 95, 74));
            Application.Current.Resources["ThreeColor"] = new SolidColorBrush(Color.FromRgb(244, 213, 187));
            Application.Current.Resources["Figure"] = new SolidColorBrush(Colors.Black);
            Application.Current.Resources["Tools"] = new SolidColorBrush(Colors.Black);
            Application.Current.Resources["Border"] = new SolidColorBrush(Colors.White);
            Application.Current.Resources["tgBtn_default"] = new ImageBrush(new BitmapImage(new System.Uri("Resource/Images/tgBtn_default_two.png", UriKind.RelativeOrAbsolute)));
            Application.Current.Resources["tb_mouse_over"] = new ImageBrush(new BitmapImage(new System.Uri("Resource/Images/tgBtn_MouseOver_two.png", UriKind.RelativeOrAbsolute)));
            WindowGrid.Width = WindowGrid.ActualWidth + 1;
        }

        private void Undo(object sender, RoutedEventArgs e)
        {

        }

        private void Redo(object sender, RoutedEventArgs e)
        {

        }

        private void ClearAll(object sender, RoutedEventArgs e)
        {

        }
    }
}
