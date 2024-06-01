using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ArkanoidGame
{
    public partial class StartForm : Form
    {
        Image bckground = Image.FromFile("background.png");
        GameEngine engine;
        bool haveStarted = false;

        public StartForm(GameEngine ge)
        {
            StartPosition = FormStartPosition.CenterScreen;
            BackgroundImage = bckground;
            engine = ge;
            InitializeComponent();
        }

        public StartForm() 
        {
            StartPosition = FormStartPosition.CenterScreen;
            BackgroundImage = bckground;
            InitializeComponent();
        }

        private void button3_Click(object sender, EventArgs e) // Выход
        {
            Application.Exit();
        }

        private void button1_Click(object sender, EventArgs e) // Начать
        {
            this.Close();
            Form1 form = new Form1(engine.GameStatistics);
            form.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)  // Статистика
        {
            engine.IsShowStats = !engine.IsShowStats;
            Form2 f2 = new Form2(engine);
            f2.ShowDialog();
        }

    }
}
