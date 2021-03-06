using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace InteractivePoster.Finction
{
    public class ChangeTheme
    {
        public void Checked(Grid cv)
        {
            Application.Current.Resources["TwoColor"] = new SolidColorBrush(Color.FromRgb(108, 165, 250));
            Application.Current.Resources["Coordinate"] = new SolidColorBrush(Colors.Black);
            Application.Current.Resources["ThreeColor"] = new SolidColorBrush(Colors.White);
            Application.Current.Resources["HeadingForGroupBox"] = new SolidColorBrush(Colors.White);
            Application.Current.Resources["Tools"] = new SolidColorBrush(Color.FromRgb(108, 165, 250));
            Application.Current.Resources["Figure"] = new SolidColorBrush(Color.FromRgb(248, 94, 94));
            Application.Current.Resources["Border"] = new SolidColorBrush(Color.FromRgb(108, 165, 250));
            Application.Current.Resources["tgBtn_default"] = new ImageBrush(new BitmapImage(new System.Uri("Resource/Images/tgBtn_default.png", UriKind.RelativeOrAbsolute)));
            Application.Current.Resources["tb_mouse_over"] = new ImageBrush(new BitmapImage(new System.Uri("Resource/Images/tgBtn_MouseOver.png", UriKind.RelativeOrAbsolute)));
            Application.Current.Resources["EraserImage"] = new ImageBrush(new BitmapImage(new System.Uri("Resource/Images/Eraser.png", UriKind.RelativeOrAbsolute)));
            Application.Current.Resources["Undo"] = new ImageBrush(new BitmapImage(new System.Uri("Resource/Images/Return.png", UriKind.RelativeOrAbsolute)));
            Application.Current.Resources["Redo"] = new ImageBrush(new BitmapImage(new System.Uri("Resource/Images/Forward.png", UriKind.RelativeOrAbsolute)));
            Application.Current.Resources["Basket"] = new ImageBrush(new BitmapImage(new System.Uri("Resource/Images/Basket.png", UriKind.RelativeOrAbsolute)));
            Application.Current.Resources["Pencel"] = new ImageBrush(new BitmapImage(new System.Uri("Resource/Images/Pen.png", UriKind.RelativeOrAbsolute)));
            Application.Current.Resources["BackButtonImage"] = new ImageBrush(new BitmapImage(new System.Uri("Resource/Images/BackPageIcon.png", UriKind.RelativeOrAbsolute)));
            Application.Current.Resources["DoskaImage"] = new ImageBrush(new BitmapImage(new System.Uri("Resource/Images/OpenPaint.png", UriKind.RelativeOrAbsolute)));

                cv.Width = cv.ActualWidth+1;

        }

        public void Unchecked(Grid cv)
        {
            Application.Current.Resources["TwoColor"] = new SolidColorBrush(Color.FromRgb(243, 95, 74));
            Application.Current.Resources["ThreeColor"] = new SolidColorBrush(Color.FromRgb(244, 213, 187));
            Application.Current.Resources["HeadingForGroupBox"] = new SolidColorBrush(Color.FromRgb(244, 213, 187));
            Application.Current.Resources["Coordinate"] = new SolidColorBrush(Colors.Black);
            Application.Current.Resources["Figure"] = new SolidColorBrush(Colors.Black);
            Application.Current.Resources["Tools"] = new SolidColorBrush(Colors.Black);
            Application.Current.Resources["Border"] = new SolidColorBrush(Colors.White);
            Application.Current.Resources["tgBtn_default"] = new ImageBrush(new BitmapImage(new System.Uri("Resource/Images/tgBtn_default_two.png", UriKind.RelativeOrAbsolute)));
            Application.Current.Resources["tb_mouse_over"] = new ImageBrush(new BitmapImage(new System.Uri("Resource/Images/tgBtn_MouseOver_two.png", UriKind.RelativeOrAbsolute)));
            Application.Current.Resources["EraserImage"] = new ImageBrush(new BitmapImage(new System.Uri("Resource/Images/Eraser.png", UriKind.RelativeOrAbsolute)));
            Application.Current.Resources["Undo"] = new ImageBrush(new BitmapImage(new System.Uri("Resource/Images/Return.png", UriKind.RelativeOrAbsolute)));
            Application.Current.Resources["Redo"] = new ImageBrush(new BitmapImage(new System.Uri("Resource/Images/Forward.png", UriKind.RelativeOrAbsolute)));
            Application.Current.Resources["Basket"] = new ImageBrush(new BitmapImage(new System.Uri("Resource/Images/Basket.png", UriKind.RelativeOrAbsolute)));
            Application.Current.Resources["Pencel"] = new ImageBrush(new BitmapImage(new System.Uri("Resource/Images/Pen.png", UriKind.RelativeOrAbsolute)));
            Application.Current.Resources["BackButtonImage"] = new ImageBrush(new BitmapImage(new System.Uri("Resource/Images/BackPageIcon.png", UriKind.RelativeOrAbsolute)));
            Application.Current.Resources["DoskaImage"] = new ImageBrush(new BitmapImage(new System.Uri("Resource/Images/OpenPaint.png", UriKind.RelativeOrAbsolute)));
            cv.Width = cv.ActualWidth+1;
        }

        public void BlackTheme(Grid cv)
        {
            Application.Current.Resources["TwoColor"] = new SolidColorBrush(Color.FromRgb(231,231,231));
            Application.Current.Resources["ThreeColor"] = new SolidColorBrush(Colors.Black);
            Application.Current.Resources["HeadingForGroupBox"] = new SolidColorBrush(Color.FromRgb(231, 231, 231));
            Application.Current.Resources["Coordinate"] = new SolidColorBrush(Color.FromRgb(231, 231, 231));
            Application.Current.Resources["Figure"] = new SolidColorBrush(Color.FromRgb(231, 231, 231));
            Application.Current.Resources["Tools"] = new SolidColorBrush(Color.FromRgb(231, 231, 231));
            Application.Current.Resources["Border"] = new SolidColorBrush(Color.FromRgb(231, 231, 231));
            Application.Current.Resources["tgBtn_default"] = new ImageBrush(new BitmapImage(new System.Uri("Resource/Images/tgBtn_default_two.png", UriKind.RelativeOrAbsolute)));
            Application.Current.Resources["tb_mouse_over"] = new ImageBrush(new BitmapImage(new System.Uri("Resource/Images/tgBtn_MouseOver_two.png", UriKind.RelativeOrAbsolute)));
            Application.Current.Resources["EraserImage"] = new ImageBrush(new BitmapImage(new System.Uri("Resource/Images/EraserWhite.png", UriKind.RelativeOrAbsolute)));
            Application.Current.Resources["Undo"] = new ImageBrush(new BitmapImage(new System.Uri("Resource/Images/ReturnWhite.png", UriKind.RelativeOrAbsolute)));
            Application.Current.Resources["Redo"] = new ImageBrush(new BitmapImage(new System.Uri("Resource/Images/ForwardWhite.png", UriKind.RelativeOrAbsolute)));
            Application.Current.Resources["Basket"] = new ImageBrush(new BitmapImage(new System.Uri("Resource/Images/BasketWhite.png", UriKind.RelativeOrAbsolute)));
            Application.Current.Resources["Pencel"] = new ImageBrush(new BitmapImage(new System.Uri("Resource/Images/PenWhite.png", UriKind.RelativeOrAbsolute)));
            Application.Current.Resources["BackButtonImage"] = new ImageBrush(new BitmapImage(new System.Uri("Resource/Images/BackPageIconWhite.png", UriKind.RelativeOrAbsolute)));
            Application.Current.Resources["DoskaImage"] = new ImageBrush(new BitmapImage(new System.Uri("Resource/Images/OpenPaintWhite.png", UriKind.RelativeOrAbsolute)));
            cv.Width = cv.ActualWidth + 1;
        }
        public Color ChangedColor(int numberColor)
        {
            Color color = Colors.Black;
            switch (numberColor)
            {
                case 1:
                    color = Colors.Black;
                    break;
                case 2:
                    color = Colors.Red;
                    break;
                case 3:
                    color = Color.FromRgb(5, 0, 255);
                    break;
                case 4:
                    color = Color.FromRgb(5, 255, 0);
                    break;
                case 5:
                    color = Color.FromRgb(255, 245, 0);
                    break;
                case 6:
                    color = Color.FromRgb(0, 255, 240);
                    break;
                default:
                    break;
            }
            return color;
        }
    }
}
