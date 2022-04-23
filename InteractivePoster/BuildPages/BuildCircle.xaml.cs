using InteractivePoster.Finction;
using InteractivePoster.Finction.BuildGeometric;
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

namespace InteractivePoster.BuildPages
{
    /// <summary>
    /// Логика взаимодействия для BuildCircle.xaml
    /// </summary>
    public partial class BuildCircle : Page
    {
        double count, countY;
        BuildCircleHands BCH = new BuildCircleHands();
        public BuildCircle()
        {
            InitializeComponent();
            DataContext = BCH;
            CommandBindings.Add(BCH.clearCanvasBinding);
        }

        private void MouseDown_Background(object sender, MouseButtonEventArgs e)
        {
            if (BCH.flag)
            {
                BCH.MouseDown = true;               
                BCH.StartDraw(e);
            }
            else
            {
                if (BCH.RadiusCircle)
                {
                    BCH.isMouseDownRadius = false;
                    BCH.GetPointRadius(e);

                    RadiusCircleCheck.IsChecked = false;
                }

                if (BCH.CenterCircle)
                {
                    BCH.GetCenterPoint(e);
                    BCH.CenterCircle = false;
                    RadiusCircleCheck.IsChecked = true;
                }
            }

        }

        private void MouseUp_Background(object sender, MouseButtonEventArgs e)
        {
            BCH.MouseDown = false;
            BCH.IsMouseDownRadius = false;
        }

        private async void MouseMove_Background(object sender, MouseEventArgs e)
        {
            if (BCH.MouseDown)
            {
                    Background.Children.Remove(BCH.currentPath);                  
                    BCH.BuildCirclePoint(e);                   
            }
        }

        private void UpdateBackPattern(object sender, SizeChangedEventArgs e)
        {
            count = Convert.ToDouble(Background.Tag);//вынимаем информацию о количестве клеток из самой канвы  
            countY = Math.Round(Background.ActualHeight / (Background.ActualWidth / count));
            BCH.GetCanvas = Background;

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
            lineY.DrawArrow(0, countY / 2, Orientation.Vertical, 3, Background);
           
        }

        private void Area_PreviewMouseMove(object sender, MouseEventArgs e)
        {
            UpdateBackPattern(null, null);
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
            soundCircle.Source = new Uri("Resource\\Sounds\\CircleSound.mp3", UriKind.RelativeOrAbsolute);
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
