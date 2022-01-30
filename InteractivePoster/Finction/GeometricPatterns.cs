using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        public double MaxMinValueCoordinat
        {
            set
            {
                MaxValue = value;
                MinValue = value / (-1);
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
        public double convertCoord(double coord)
        {
            return maxX / 2 + coord * (maxX / count);
        }
        /// <summary>
        /// метод для преобразования координаты Х их канвы в декартово значение
        /// </summary>
        /// <param name="x">декартова координата (как нам надо с точки зрения математики)</param>
        /// <returns>актуальная координата Х на канве</returns>
        public double convertX(double x)
        {
            //радиус вычитаем, т.к. по умолчанию передаются координаты левого верхнего угла
            return maxX / 2 + x * (maxX / count) - radius;
        }
        /// <summary>
        /// метод для преобразования координаты Y их канвы в декартово значение
        /// </summary>
        /// <param name="y">декартова координата (как нам надо с точки зрения математики)</param>
        /// <returns>актуальная координата Y на канве</returns>
        public double convertY(double y)
        {
            return maxX / 2 + y * (maxX / count) - radius;
        }
       
    }
}
