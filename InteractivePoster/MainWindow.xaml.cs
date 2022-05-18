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
            NavigateFrame.Navigate(new BackPage());
        }


    }
}
