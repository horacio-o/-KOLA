using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace RGB
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Color current_color = Color.FromRgb(0, 0, 0);
        public MainWindow()
        {
            InitializeComponent();
            RECTANGLE.Fill = new SolidColorBrush(current_color);
        }

        private void Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            Slider slider = (Slider)sender;
            switch (slider.Name)
            {
                case "R":
                    current_color.R = (byte)e.NewValue;
                    break;
                case "G":
                    current_color.G = (byte)e.NewValue;
                    break;
                case "B":
                    current_color.B = (byte)e.NewValue;
                    break;

                default:
                    throw new Exception();
            }
            RECTANGLE.Fill = new SolidColorBrush(current_color);
        }
    }
}