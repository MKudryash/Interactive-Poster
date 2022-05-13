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

        public double MaxMinPoint
        {
            set
            {
                maxPoint = value;
                PropertyChanged(this, new PropertyChangedEventArgs("maxPoint"));
                minPoint = value * (-1);
                PropertyChanged(this, new PropertyChangedEventArgs("minPoint"));
            }
        }
        public double maxPoint { get; set; } = 8;
        public double minPoint { get; set; } = -8;

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



        public static bool ElementCircle { get; set; } = true;
        public static bool ElementCirclePoint { get; set; } = true;

        public static bool elementElipsPoint { get; set; } = true;

        public static bool ElementElipseRadius { get; set; } = true;
        public bool ElementElipsePoint
        {
            get => elementElipsPoint;
            set
            {
                elementElipsPoint = value;
                PropertyChanged(this, new PropertyChangedEventArgs("ElementElipsePoint"));
            }
        }
        public static bool ElementElipseFocus { get; set; } = true;

        public static bool ElementParabolaD { get; set; } = true;
        public static bool ElementParabolaFocus { get; set; } = true;

        public static bool ElementHyperboleRectangle { get; set; } = true;
        public static bool ElementHyperboleFocus { get; set; } = true;
        public static bool ElementHyperboleAssim { get; set; } = true;

        public static bool SinCosEllipse { get; set; } = false;
        public static bool SinCosHyperbole { get; set; } = false;


        public bool DrawPen { get; set; } = false;
    }
    class GeometricPatterns
    {
        public double maxX,x,y, maxY, countX, countY, radius;

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
            return (coord - maxY / 2) * countY / maxY * -1;
        }
        public TextBlock DrawText(double x, double y, string text)
        {
            TextBlock TB = new TextBlock()
            {
                Text = text,
                TextWrapping = TextWrapping.Wrap,
                Width = double.NaN,
                FontSize = countX
            };
            TB.SetValue(Canvas.LeftProperty, convertCoordX(x));
            TB.SetValue(Canvas.TopProperty, convertCoordY(y));
            return TB;
        } //Текст c содержанием точек     


    }
}
