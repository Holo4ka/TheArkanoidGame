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
    public partial class CloseGameForm : Form
    {
        public string pressed;
        public CloseGameForm()
        {
            StartPosition = FormStartPosition.CenterParent;
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e) // продолжить
        {
            pressed = "Button 1";
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e) // Главное меню
        {
            pressed = "Button 2";
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e) // Из игры
        {
            Application.Exit();
        }
    }
}
