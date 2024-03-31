using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace TheArkanoidGame
{
    public partial class Form1 : Form
    {
        Graphics gr;
        Ball ball;
        Pen pen = new Pen(Color.Black, 2);
        SolidBrush brush = new SolidBrush(Color.Red);
        public Form1()
        {
            InitializeComponent();
            DoubleBuffered = true;
            gr = pictureBox1.CreateGraphics();
            ball = new Ball(pictureBox1.Width / 2 - 5, 400, new SolidBrush(Color.Red), Color.Red, gr);
            timer1.Interval = 10;
            //timer1.Tick += Timer_Tick;
            timer1.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            
            pictureBox1.Invalidate();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            button1.Hide();
            ball.Draw();
            ball.InitRandomSafeDiagonalMoving();
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            ball.MoveAtCurrentDirection();
        }
    }
}