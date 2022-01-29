using System;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace InteractivePoster.Finction
{
    class DrawCircle
    {
       
            double maxX;//максиммум по оси Х (масштабы по Y также пересчитываем через значения по Х, иначе круг может получиться не круглым, а квадрат не квадратным)   
            double radius;//радиус окружности
            double count;//масштаб (количество клеток)
            Ellipse circle;//непосредственно сама окружность
            /// <summary>
            /// метод для преобразования координаты Х их канвы в декартово значение
            /// </summary>
            /// <param name="x">декартова координата (как нам надо с точки зрения математики)</param>
            /// <returns>актуальная координата Х на канве</returns>
            double convertX(double x)
            {
                //радиус вычитаем, т.к. по умолчанию передаются координаты левого верхнего угла
                return maxX / 2 + x * (maxX / count) - radius;
            }
            /// <summary>
            /// метод для преобразования координаты Y их канвы в декартово значение
            /// </summary>
            /// <param name="y">декартова координата (как нам надо с точки зрения математики)</param>
            /// <returns>актуальная координата Y на канве</returns>
            double convertY(double y)
            {
                return maxX / 2 - y * (maxX / count) - radius;
            }
            /// <summary>
            /// Конструктор класса
            /// </summary>
            /// <param name="x">Декартова абсцисса центра окружности</param>
            /// <param name="y">Декартова ордината центра окружности</param>
            /// <param name="r">Радиус окружности в декартовой системе</param>
            /// <param name="cv">Объект канвы, на котором появится окружность</param>
            public DrawCircle(double x, double y, double r, Canvas cv)
            {
                count = Convert.ToDouble(cv.Tag);//получаем масштабы области
                maxX = cv.ActualWidth; //получаем ширину канвы       
                radius = r * (maxX / count);//преобразуем радиус из декартовой системы

                circle = new Ellipse()//задаем прочие параметры
                {
                    Width = 2 * radius,//ширина и длина по сути равна диаметру окружности
                    Height = 2 * radius,
                    Stroke = Brushes.Red,
                    StrokeThickness = 1
                };

                cv.Children.Add(circle);//помещаем на канву
                                        //в нужную точку канвы
                circle.SetValue(Canvas.LeftProperty, convertX(x));
                circle.SetValue(Canvas.TopProperty, convertY(y));
            }
        
    }
}
