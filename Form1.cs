using System.CodeDom;

namespace ArkanoidGame
{
    public partial class Form1 : Form
    {
        private GameEngine gameEngine;
        private Render gameObjectsRenderer;

        public Form1(GameStats stats)
        {
            StartPosition = FormStartPosition.CenterScreen;
            gameEngine = new GameEngine(GameIterationTimer, 800, 500);
            gameEngine.setStats(stats);
            /*StartForm sf = new StartForm(gameEngine);
            sf.ShowDialog();*/
            InitializeComponent();
        }

        private void FrmArkanoidMain_Load(object sender, EventArgs e)
        {
            DoubleBuffered = true;
            if (gameEngine.timer == null) { gameEngine.setTimer(GameIterationTimer); }
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
                // ������ - ���������/����� ���� �� �����. ����� ���� �������� �� �����, 
                // ����� ��������� �������������� �������� - ������� ����� Invalidate() ����� ���
                // �������������� ����������� ���� ������� ��������, ��������� ������ � ������ ����� ���������������
                // � gameObjectsRenderer �� ������ ���������� ��� ����� � ���, ��� ���� ����� �� �����
                gameEngine.ToggleGamePauseMode(() => Invalidate());
            }
            else if (e.KeyCode == Keys.Escape)
            {
                // ������� Escape - ��� ����� �� ����. ������ ��� ����� �� ����, �� �������� ���� �� �����:
                gameEngine.PauseGame(() => Invalidate());

                // � ����� ������� ���������� ���� ������������. ���� �� ������� ����� �� ���� - �������, ����� ������� ���� � ����� � ����������
                using (CloseGameForm cgf = new CloseGameForm()) 
                {
                    cgf.ShowDialog();
                    string res = cgf.pressed;
                    if (res == "Button 1") { gameEngine.UnpauseGame(); }
                    else if (res == "Button 2") 
                    {
                        this.Close();

                        StartForm sf = new StartForm(gameEngine);
                        sf.ShowDialog();
                    }
                }
                /*DialogResult dlgResult = MessageBox.Show("����� ����� �� ����?", "�����", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                if (dlgResult == DialogResult.Yes)
                {
                    Application.Exit();
                }
                else
                {
                    gameEngine.UnpauseGame();
                }*/
            }
            else if (e.KeyCode == Keys.Enter)
            {
                // ������� �� Enter - ���������� ����
                gameEngine.RestartGame();
            }
            else if (e.KeyCode == Keys.S)
            {
                gameEngine.IsShowStats = !gameEngine.IsShowStats;
            }
        }

        private void FrmArkanoidMain_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                // ������� �� ������ ������ ����� ��������/��������� ����� ����� � ����, ���
                // � ������� �������
                gameEngine.ToggleGamePauseMode(() => Invalidate());
            }
            /*else if (e.Button == MouseButtons.Middle)
            {
                gameEngine.IsShowStats = !gameEngine.IsShowStats;
            }*/
        }

        private void GameIterationTimer_Tick(object sender, EventArgs e)
        {
            Func<bool> funcIsNeedToContinueWhenGameIsOver = () =>
            {
                DialogResult dlgResult = MessageBox.Show(
                    "�� ���������... ������ ������?\r\n�� ������ ���������� ������ � ������ ���� �����, ����� Enter",
                    "�������� :(",
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
                    "�����������, �� ��������, ����� �� ���������� ������ " + currentGameLevel + "!\r\n������� ��� ���?\r\n",
                    "������!",
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