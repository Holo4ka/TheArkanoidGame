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
//using System.Drawing;
using System.Windows.Threading;
//using System.Timers;

namespace TheArkanoidGame1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Point ballVelocity = new Point(5, 5); // Скорость шарика
        private double ballRadius = 50;
        private double bounceSpeed = 1;

        public MainWindow()
        {
            InitializeComponent();

            // Запускаем таймер для перемещения шарика
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromMilliseconds(10);
            timer.Tick += Timer_Tick;
            timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            Canvas.SetLeft(ball, Canvas.GetLeft(ball) + ballVelocity.X);
            Canvas.SetTop(ball, Canvas.GetTop(ball) + ballVelocity.Y);

            // Проверка столкновения со стенами окна
            if (Canvas.GetLeft(ball) < 0 || Canvas.GetLeft(ball) > canvas.ActualWidth - 2 * ballRadius)
            {
                ballVelocity.X *= -1;
            }

            if (Canvas.GetTop(ball) < 0 || Canvas.GetTop(ball) > canvas.ActualHeight - 2 * ballRadius)
            {
                ballVelocity.Y *= -1;
            }

            // Проверка столкновения с прямоугольником
            if (Canvas.GetLeft(ball) + 2 * ballRadius > Canvas.GetLeft(rectangle) &&
            Canvas.GetLeft(ball) < Canvas.GetLeft(rectangle) + rectangle.Width &&
                Canvas.GetTop(ball) + 2 * ballRadius > Canvas.GetTop(rectangle) &&
                Canvas.GetTop(ball) < Canvas.GetTop(rectangle) + rectangle.Height)
            {
                // Обработка отскока
                if (Canvas.GetLeft(ball) + ballRadius < Canvas.GetLeft(rectangle) ||
                    Canvas.GetLeft(ball) + ballRadius > Canvas.GetLeft(rectangle) + rectangle.Width)
                {
                    ballVelocity.X *= -1;
                }

                if (Canvas.GetTop(ball) + ballRadius < Canvas.GetTop(rectangle) ||
                    Canvas.GetTop(ball) + ballRadius > Canvas.GetTop(rectangle) + rectangle.Height)
                {
                    ballVelocity.Y *= -1;
                }
            }
        }
    }
}
