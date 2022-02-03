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
    }
    class GeometricPatterns
    {

        public double maxX;
        public double maxY;
        public double count;
        public double radius;
        public double convertCoordX(double coord)
        {
            return maxX / 2 + coord * (maxX / count);
        }
        public double convertCoordY(double coord)
        {
            return maxX / 2 + coord * (-1) * (maxX / count);
        }

    }
}
