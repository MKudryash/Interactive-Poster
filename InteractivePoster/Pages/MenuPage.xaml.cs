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
    /// Логика взаимодействия для MenuPage.xaml
    /// </summary>
    public partial class MenuPage : Page
    {
        public MenuPage()
        {
            InitializeComponent();
        }

        private void ThemeMath(object sender, RoutedEventArgs e)
        {
            string themePage = (sender as Button).Tag.ToString();
            switch (themePage)
            {
                case "CircleTag":
                    LoadPage.MainFrame.Navigate(new Circle());
                    break;
                case "ElipsTag":
                    LoadPage.MainFrame.Navigate(new Elips());
                    break;
                case "HyperboleTag":
                    LoadPage.MainFrame.Navigate(new Hyperbole());
                    break;
                case "ParabolaTag":
                    LoadPage.MainFrame.Navigate(new Parabola());
                    break;
                default:
                    break;
            }
        }
    }
}
