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
    public partial class Form2 : Form
    {
        Image bckground = Image.FromFile("background.png");
        GameEngine engine;
        bool haveStarted = false;

        public Form2(GameEngine eg)
        {
            StartPosition = FormStartPosition.CenterScreen;
            BackgroundImage = bckground;
            engine = eg;
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            GameStats gameStats = engine.GameStatistics;
            Font statsFont = new Font("Ubuntu Mono", 12);
            string pushedAwayBallsStats = string.Format("{0}: {1}\r\n{2}: {3} / {4}\r\n{5}: {6}\r\n{7}: {8}\r\n{9}: {10}\r\n\r\n",
                GameEngine.GAME_STATS_PUSHED_AWAY_BALLS_TOTAL, // {0}
                gameStats.GetGameCounterValue(GameEngine.GAME_STATS_PUSHED_AWAY_BALLS_TOTAL), // {1}
                GameEngine.GAME_STATS_PUSHED_AWAY_BALLS_CURRENT, // {2}
                gameStats.GetGameCounterValue(GameEngine.GAME_STATS_LEVEL), // {5}
                GameEngine.TOTAL_GAME_LEVELS_TO_WIN, // {6}
                GameEngine.GAME_STATS_CURRENT_BALL_SPEED, // {7}
                gameStats.GetGameCounterValue(GameEngine.GAME_STATS_CURRENT_BALL_SPEED), // {8}
                "Проиграно игр", // {9}
                gameStats.NumberOfFailures, // {10}
                "Выиграно игр", // {11}
                gameStats.NumberOfWins); // {12}
            label1.Text = pushedAwayBallsStats;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
