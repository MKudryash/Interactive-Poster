using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace InteractivePoster.Finction
{
    public class MaxMinCoordinat : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public double MaxValue { get; set; } = 9;
        public double MinValue { get; set; } = -9;
        public double maxRadius { get; set; } = 10;
        public static int gradusValue { get; set; } = 45;
        public string FormalaElips { get; set; }
        public int GradusValue
        {
            get
            {
                return gradusValue;
            }
            set
            {
                gradusValue = value;
                PropertyChanged(this, new PropertyChangedEventArgs("gradusValue"));
            }
        }
        public int MaxRadius
        {
            set
            {
                maxRadius = value;
                PropertyChanged(this, new PropertyChangedEventArgs("maxRadius"));
            }
        }

        public double MaxMinValueCoordinat
        {
            set
            {
                MaxValue = value;
                MinValue = value * (-1);
                PropertyChanged(this, new PropertyChangedEventArgs("MaxValue"));
                PropertyChanged(this, new PropertyChangedEventArgs("MinValue"));
            }
        }
        public static bool equationforParabola { get; set; } = true;
        public static bool equationforElips { get; set; } = true;
        public static bool equationforHyperbole { get; set; } = true;

        public string nameSound { get; set; }
        public static Canvas cv { get; set; }
        public Canvas GetCanvas { get => cv; set { cv = value; } }

        public RoutedCommand SoundPlayCommand { get; set; } = new RoutedCommand();
        public CommandBinding SoundPlayBinding;

        public MaxMinCoordinat()
        {
            SoundPlayBinding = new CommandBinding(SoundPlayCommand);
            SoundPlayBinding.Executed += SoundPlayBinding_Executed; ;

        }
        MediaElement soundCircle = new MediaElement();
        bool isPlay = true;
        private void SoundPlayBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            soundCircle.LoadedBehavior = MediaState.Manual;
            soundCircle.Source = new Uri($"Resource\\Sounds\\{nameSound}.mp3", UriKind.RelativeOrAbsolute);
            soundCircle.Position = TimeSpan.Zero;
            if (isPlay)
            {
                cv.Children.Add(soundCircle);
                soundCircle.Play();
                isPlay = false;
            }
            else
            {
                cv.Children.Remove(soundCircle);
                soundCircle.Stop();
                isPlay = true;
            }
        }
    }
    class GeometricPatterns
    {
        public double maxX;
        public double maxY;
        public double countX;
        public double countY;
        public double radius;
        public Canvas cv;
        public double convertCoordX(double coord)
        {
            return maxX / 2 + coord * (maxX / countX);
        }
        public double convertCoordY(double coord)
        {
            return maxY / 2 + coord * (-1) * (maxY / countY);
        }
        public double convertXCoord(double coord)
        {
            return (coord - maxX / 2) * countX / maxX;
        }
        public double convertYCoord(double coord)
        {
            return (coord - maxY / 2) * countY / maxY*-1;
        }
        public void DrawText(double x, double y, string text)
        {
            TextBlock TB = new TextBlock()
            {
                Text = text,
                TextWrapping = TextWrapping.Wrap,
                Width = double.NaN,
                FontSize =countX
            };
            cv.Children.Add(TB);
            TB.SetValue(Canvas.LeftProperty, convertCoordX(x));
            TB.SetValue(Canvas.TopProperty, convertCoordY(y));
        } //Текст c содержанием точек

    }
}
