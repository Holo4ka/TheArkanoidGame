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
using System.Windows.Media.Animation;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Threading;
//using System.Timers;

namespace TheArkanoidGame1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            //ball = new Ball(20, 20, new SolidColorBrush(Color.FromArgb(255, 255, 0, 0)), Color.FromRgb(255, 0, 0));

            TranslateTransform translateX = new TranslateTransform();
            TranslateTransform translateY = new TranslateTransform();
            ball.RenderTransform = new TransformGroup { Children = { translateX, translateY } };

            // Анимация движения вниз и вправо
            DoubleAnimation animationX = new DoubleAnimation
            {
                From = 51,
                To = 800 - ball.Width,
                Duration = TimeSpan.FromSeconds(3),
                AutoReverse = true,
                RepeatBehavior = RepeatBehavior.Forever
            };
            translateX.BeginAnimation(TranslateTransform.XProperty, animationX);

            DoubleAnimation animationY = new DoubleAnimation
            {
                From = 51,
                To = 480 - ball.Height,
                Duration = TimeSpan.FromSeconds(3),
                AutoReverse = true,
                RepeatBehavior = RepeatBehavior.Forever
            };
            translateY.BeginAnimation(TranslateTransform.YProperty, animationY);
        }

        /*private void button1_Click(object sender, RoutedEventArgs e)
        {
            ball.RenderTransform.BeginAnimation(TranslateTransform.XProperty, null);
            ball.RenderTransform.BeginAnimation(TranslateTransform.YProperty, null);
        }*/

    }
}
