﻿using InteractivePoster.Finction;
using InteractivePoster.Pages;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

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
            catch 
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
        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            Application.Current.Resources["TwoColor"] = new SolidColorBrush(Color.FromRgb(0, 172, 193));
            Application.Current.Resources["ThreeColor"] = new SolidColorBrush(Colors.White);
            Application.Current.Resources["Tools"] = new SolidColorBrush(Color.FromRgb(108, 165, 250));
            Application.Current.Resources["Figure"] = new SolidColorBrush(Color.FromRgb(248, 94, 94));
            if (WindowGrid.ActualWidth!=0)
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
            WindowGrid.Width = WindowGrid.ActualWidth+1;
        }
    }
}
