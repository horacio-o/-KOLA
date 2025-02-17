using System.Diagnostics;
using System.IO;
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
using System.Timers;

namespace minesweeper
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        private void EasyClick(object sender, RoutedEventArgs e)
        {
            Expander.Visibility = Visibility.Collapsed;
            MainWindowViewModel vm = new MainWindowViewModel();
            DataContext = vm;
            vm.GenerateButtons(grid, 8, 10);
        }

        private void MediumClick(object sender, RoutedEventArgs e)
        {
            Expander.Visibility = Visibility.Collapsed;
            MainWindowViewModel vm = new MainWindowViewModel();
            DataContext = vm;
            vm.GenerateButtons(grid, 14, 40);
        }

        private void HardcoreClick(object sender, RoutedEventArgs e)
        {
            Expander.Visibility = Visibility.Collapsed;
            MainWindowViewModel vm = new MainWindowViewModel();
            DataContext = vm;
            vm.GenerateButtons(grid, 20,99);
        }
    }
}