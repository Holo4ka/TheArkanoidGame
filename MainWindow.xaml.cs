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

namespace TheArkanoidGame1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Ball ball;
        public MainWindow()
        {
            InitializeComponent();
            ball = new Ball(20, 20, new SolidColorBrush(Color.FromArgb(255, 255, 0, 0)), Color.FromRgb(255, 0, 0));
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            
        }
    }
}
