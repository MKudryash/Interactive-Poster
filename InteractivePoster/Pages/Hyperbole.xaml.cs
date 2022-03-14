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
    /// Логика взаимодействия для Hyperbole.xaml
    /// </summary>
    public partial class Hyperbole : Page
    {
        double count;
        MaxMinCoordinat MMC = new MaxMinCoordinat();
        public Hyperbole()
        {
            InitializeComponent();
            DataContext = MMC;
        }

        private void UpdateBackPattern(object sender, SizeChangedEventArgs e)
        {
            count = Convert.ToDouble(Background.Tag);//вынимаем информацию о количестве клеток из самой канвы  
            FormulaHyperbole.Formula = @"\frac{(x*cos("+SlTransform.Value.ToString()+")-y*sin("+ SlTransform.Value.ToString() + ")-(" + slCoordX.Value.ToString("F1")+ @"))^2}{" + slRectangleA.Value.ToString("F1") +
                @"^2}+ \frac{(x*sin("+SlTransform.Value.ToString()+")+y*cos("+SlTransform.Value.ToString()+")-(" + slCoordY.Value.ToString("F1") +  @"))^2}{" + slRectangleB.Value.ToString("F1") + @"^2} = 1";


            Background.Children.Clear();
            //просто добавляем на канву объекты наших созданных классов            
            for (double i = -count / 2; i < count / 2; i++)
            {
                DrawСoordinateLine lineH = new DrawСoordinateLine(i, Orientation.Horizontal, 1, Background);
                DrawСoordinateLine lineV = new DrawСoordinateLine(i, Orientation.Vertical, 1, Background);
            }
            DrawСoordinateLine lineX = new DrawСoordinateLine(0, Orientation.Horizontal, 3, Background);
            DrawСoordinateLine lineY = new DrawСoordinateLine(0, Orientation.Vertical, 3, Background);
            lineX.DrawArrow(count / 2, 0, Orientation.Horizontal, 3, Background);
            lineY.DrawArrow(0, count / 2, Orientation.Vertical, 3, Background);

            DrawHyperbole drawHyperbole = new DrawHyperbole(slCoordX.Value, slCoordY.Value, slRectangleA.Value, slRectangleB.Value, Background,SlTransform.Value);
        }

        private void Area_PreviewMouseMove(object sender, MouseEventArgs e)
        {
            UpdateBackPattern(null,null);
        }

        private void ComeBack(object sender, RoutedEventArgs e)
        {
            LoadPage.MainFrame.GoBack();
        }
        MediaElement soundCircle = new MediaElement();
        bool isPlay = true;
        private void PlaySound(object sender, RoutedEventArgs e)
        {
            soundCircle.LoadedBehavior = MediaState.Manual;
            soundCircle.Source = new Uri("Resource\\Sounds\\HyperboleSound.mp3", UriKind.RelativeOrAbsolute);
            soundCircle.Position = TimeSpan.Zero;
            if (isPlay)
            {
                Background.Children.Add(soundCircle);
                soundCircle.Play();
                isPlay = false;
            }
            else
            {
                Background.Children.Remove(soundCircle);
                soundCircle.Stop();
                isPlay = true;
            }
        }
    }
}
