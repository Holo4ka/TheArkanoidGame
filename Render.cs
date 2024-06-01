using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArkanoidGame
{
    public class Render
    {
        private readonly GameEngine gameEngine;

        public Render(GameEngine gameEngine)
        {
            this.gameEngine = gameEngine;
        }

        /// <summary>
        /// Отрисовать игровую статистику
        /// </summary>
        /// <param name="g"></param>
        private void RenderGameStats(Graphics g)
        {
            if (gameEngine.IsShowStats)
            {
                GameStats gameStats = gameEngine.GameStatistics;
                Font statsFont = new Font("Ubuntu Mono", 12);
                string pushedAwayBallsStats = string.Format("{0}: {1}\r\n{2}: {3}\r\n{4}: {5} / {6}\r\n{7}: {8}\r\n{9}: {10}\r\n{11}: {12}\r\n\r\n[S] или средняя кнопка мыши\r\n - скрыть/показать статистику",
                    GameEngine.GAME_STATS_PUSHED_AWAY_BALLS_TOTAL, // {0}
                    gameStats.GetGameCounterValue(GameEngine.GAME_STATS_PUSHED_AWAY_BALLS_TOTAL), // {1}
                    GameEngine.GAME_STATS_PUSHED_AWAY_BALLS_CURRENT, // {2}
                    gameStats.GetGameCounterValue(GameEngine.GAME_STATS_PUSHED_AWAY_BALLS_CURRENT), // {3}
                    GameEngine.GAME_STATS_LEVEL, // {4}
                    gameStats.GetGameCounterValue(GameEngine.GAME_STATS_LEVEL), // {5}
                    GameEngine.TOTAL_GAME_LEVELS_TO_WIN, // {6}
                    GameEngine.GAME_STATS_CURRENT_BALL_SPEED, // {7}
                    gameStats.GetGameCounterValue(GameEngine.GAME_STATS_CURRENT_BALL_SPEED), // {8}
                    "Проиграно игр", // {9}
                    gameStats.NumberOfFailures, // {10}
                    "Выиграно игр", // {11}
                    gameStats.NumberOfWins); // {12}

                g.DrawString(pushedAwayBallsStats, statsFont, Brushes.Lime, new PointF(0, 150));

                statsFont.Dispose();
            }
        }

        /// <summary>
        /// Отрисовать платформу игрока
        /// </summary>
        /// <param name="g"></param>
        private void RenderPlayerPlatform(Graphics g)
        {
            GameObject platform = gameEngine.PlayerPlatform;
            Rectangle platformRect = platform.GetObjectRectangle();
            g.FillRectangle(Brushes.Crimson, platformRect);
        }

        /// <summary>
        /// Отрисовать двигающийся шарик
        /// </summary>
        /// <param name="g"></param>
        private void RenderBall(Graphics g)
        {
            GameObject ball = gameEngine.BouncingBall;
            Rectangle ballRect = ball.GetObjectRectangle();
            Rectangle ballOuterRect = new Rectangle(ballRect.X + 4, ballRect.Y + 4, ballRect.Width - 8, ballRect.Height - 8);
            g.FillEllipse(Brushes.Goldenrod, ballRect);
            g.FillEllipse(Brushes.DarkGoldenrod, ballOuterRect);
        }

        /// <summary>
        /// Отрисовать статичные блоки
        /// </summary>
        /// <param name="g"></param>
        private void RenderStaticBlocks(Graphics g)
        {
            foreach (StaticBlock block in gameEngine.Blocks)
            {
                Rectangle blockBorderRect = block.GetObjectRectangle();
                Pen blockBorderPen = new Pen(block.BorderColor, 2);
                Brush blockBodyBrush = new SolidBrush(block.BodyColor);

                g.DrawRectangle(blockBorderPen, blockBorderRect);

                Rectangle blockBodyRect = new Rectangle(blockBorderRect.X + 1, blockBorderRect.Y + 1, blockBorderRect.Width - 1, blockBorderRect.Height - 1);
                g.FillRectangle(blockBodyBrush, blockBodyRect);

                Font fontHits = new Font("Arial", 8, FontStyle.Bold);
                g.DrawString(block.CurrentHits + " / " + block.HitsToDestroy, fontHits, Brushes.White, new PointF(blockBodyRect.X + 2, blockBodyRect.Y));

                blockBodyBrush.Dispose();
                blockBorderPen.Dispose();
                fontHits.Dispose();
            }
        }

        /// <summary>
        /// Отрисовать сообщение о том, что игра на паузе, если в игровом движке выставлен признак паузы
        /// </summary>
        /// <param name="g"></param>
        private void RenderGamePausedMessage(Graphics g)
        {
            if (gameEngine.IsGamePaused)
            {
                
            }
        }

        /// <summary>
        /// Главный открытый метод класса по отрисовке всех игровых объектов на игровом поле
        /// </summary>
        /// <param name="g"></param>
        public void RenderGameObjects(Graphics g)
        {
            //g.SmoothingMode = SmoothingMode.AntiAlias;
            g.SmoothingMode = SmoothingMode.HighQuality;

            RenderGameStats(g);
            RenderPlayerPlatform(g);
            RenderBall(g);
            RenderStaticBlocks(g);
            RenderGamePausedMessage(g);
        }
    }
}
