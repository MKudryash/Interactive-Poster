using InteractivePoster.Pages;
using System.Windows;
using System.Windows.Controls;

namespace InteractivePoster
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            frmMain.Navigate(new MenuPage());
            LoadPage.MainFrame = frmMain;
            ToolPainFrame.Navigate(new ToolPaint());
        }

        private void ComeBack(object sender, RoutedEventArgs e)
        {
            try
            {
                LoadPage.MainFrame.GoBack();
            }
            catch (System.Exception)
            {
                //
            }
          
        }

        double h;
        private async void TgBtn_Unchecked(object sender, RoutedEventArgs e)
        {
            double column = 30;
            PaintStack.Visibility = Visibility.Visible;
            ButtonBack.Visibility = Visibility.Visible;
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
            h = (column - 20) / 10;
            for (int i = 0; i < 10; i++)
            {
                GridHide.Height = column;
                GridRow.Height = new GridLength(column);
                column -= h;
                await System.Threading.Tasks.Task.Delay(50);
            }
            PaintStack.Visibility = Visibility.Collapsed;
            ButtonBack.Visibility = Visibility.Collapsed;
        }
    }
}
