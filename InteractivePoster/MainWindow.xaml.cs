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

        public MainWindow()
        {
            InitializeComponent();
            frmMain.Navigate(new MenuPage());
            LoadPage.MainFrame = frmMain;
        }


    }
}
