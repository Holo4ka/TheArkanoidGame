namespace ArkanoidGame
{
    public partial class Form1 : Form
    {
        private GameEngine gameEngine;
        private Render gameObjectsRenderer;

        public Form1()
        {
            InitializeComponent();
        }

        private void FrmArkanoidMain_Load(object sender, EventArgs e)
        {
            DoubleBuffered = true;
            gameEngine = new GameEngine(GameIterationTimer, Width, Height);
            gameObjectsRenderer = new Render(gameEngine);
            gameEngine.StartGame();
            Invalidate();
        }

        private void FrmArkanoidMain_Paint(object sender, PaintEventArgs e)
        {
            gameObjectsRenderer.RenderGameObjects(e.Graphics);
        }

        private void FrmArkanoidMain_MouseMove(object sender, MouseEventArgs e)
        {
            gameEngine.HandleMouseMove(e.Location);
        }

        private void FrmArkanoidMain_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space)
            {
                // пробел - поставить/снять игру на паузу. когда игра ставится на паузу, 
                // нужно выполнить дополнительное действие - вызвать метод Invalidate() формы для
                // принудительной перерисовки всех игровых объектов, поскольку таймер в момент паузы останавливается
                // и gameObjectsRenderer не успеет отрисовать сам текст о том, что игра стоит на паузе
                gameEngine.ToggleGamePauseMode(() => Invalidate());
            }
            else if (e.KeyCode == Keys.Escape)
            {
                // клавиша Escape - это выход из игры. прежде чем выйти из игры, мы поставим игру на паузу:
                gameEngine.PauseGame(() => Invalidate());

                // а затем покажем диалоговое окно пользователю. Если он выберет выход из игры - выходим, иначе снимаем игру с паузы и продолжаем
                DialogResult dlgResult = MessageBox.Show("Точно выйти из игры?", "Выход", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                if (dlgResult == DialogResult.Yes)
                {
                    Application.Exit();
                }
                else
                {
                    gameEngine.UnpauseGame();
                }
            }
            else if (e.KeyCode == Keys.Enter)
            {
                // нажатие на Enter - перезапуск игры
                gameEngine.RestartGame();
            }
            else if (e.KeyCode == Keys.S)
            {
                gameEngine.ShowStats = !gameEngine.ShowStats;
            }
        }

        private void FrmArkanoidMain_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                // нажатие на правую кнопку также включает/выключает режим паузы в игре, как
                // и клавиша пробела
                gameEngine.ToggleGamePauseMode(() => Invalidate());
            }
            else if (e.Button == MouseButtons.Middle)
            {
                gameEngine.ShowStats = !gameEngine.ShowStats;
            }
        }

        private void GameIterationTimer_Tick(object sender, EventArgs e)
        {
            Func<bool> funcIsNeedToContinueWhenGameIsOver = () =>
            {
                DialogResult dlgResult = MessageBox.Show(
                    "Вы проиграли... Начать заново?\r\nВы можете отказаться сейчас и начать игру позже, нажав Enter",
                    "Проигрыш :(",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning
                );

                if (dlgResult == DialogResult.Yes)
                {
                    return true;
                }
                return false;
            };

            Func<int, bool> funcIsNeedToRepeatAfterWin = (currentGameLevel) =>
            {
                DialogResult dlgResult = MessageBox.Show(
                    "Поздравляем, вы победили, дойдя до последнего уровня " + currentGameLevel + "!\r\nСыграем ещё раз?\r\n",
                    "Победа!",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question
                );

                if (dlgResult == DialogResult.Yes)
                {
                    return true;
                }
                return false;
            };

            gameEngine.HandleGameCycle(
                () => Invalidate(),
                funcIsNeedToContinueWhenGameIsOver,
                funcIsNeedToRepeatAfterWin
            );

            Invalidate();
        }
    }
}