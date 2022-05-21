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
    }
}
