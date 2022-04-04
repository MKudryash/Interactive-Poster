using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
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
        public static bool equation { get; set; } = true;

        
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
       public void DrawText(double x, double y, string text)
        {
            TextBlock TB = new TextBlock()
            {
                Text = text,
                TextWrapping = TextWrapping.Wrap,
                Width = double.NaN,
                FontSize = maxX / countX * 0.5
            };
            cv.Children.Add(TB);
            TB.SetValue(Canvas.LeftProperty, convertCoordX(x));
            TB.SetValue(Canvas.TopProperty, convertCoordY(y));
        } //Текст c содержанием точек

    }
}
