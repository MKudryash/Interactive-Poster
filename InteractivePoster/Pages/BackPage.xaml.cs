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
    /// Логика взаимодействия для BackPage.xaml
    /// </summary>
    public partial class BackPage : Page
    {
        public BackPage()
        {
            InitializeComponent();
        }

        private void ComeBack(object sender, RoutedEventArgs e)
        {
            LoadPage.MainFrame.GoBack();
        }
    }
}
