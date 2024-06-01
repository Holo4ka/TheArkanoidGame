using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

namespace ArkanoidGame
{
    // Игровой движок, основная логика игры
    public class GameEngine
    {
        private const int platformWidth = 120;

        /// <summary>
        /// Высота движущейся платформы игрока, в пикселях
        /// </summary>
        private const int platformHeight = 15;

        /// <summary>
        /// Радиус движущегося шарика, в пикселях
        /// </summary>
        private const int ballRadius = 15;

        /// <summary>
        /// Отступ движущейся платформы игрока от нижней границы игрового поля (т.е. от нижнего края формы)
        /// </summary>
        private const int bottomMargin = 45;

        /// <summary>
        /// Сколько всего мы хотим уровней в игре?
        /// </summary>
        public const int TOTAL_GAME_LEVELS_TO_WIN = 7;

        /// <summary>
        /// Сколько шариков нужно отбить игроку для перехода на новый уровень?
        /// </summary>
        private const int BALLS_TO_PUSH_AWAY_FOR_NEXT_LEVEL = 5;

        /// <summary>
        /// Начальная скорость движения шарика
        /// </summary>
        private const int BALL_STARTING_SPEED = 4;

        private readonly GameObject platform = new Platform("Платформа игрока", platformWidth, platformHeight);
        private readonly GameObject ball = new Ball("Шарик", ballRadius);
        public GameStats gameStats = new GameStats();
        private readonly List<StaticBlock> blocks = new List<StaticBlock>();

        public static readonly string GAME_STATS_PUSHED_AWAY_BALLS_TOTAL = "Отбитых шариков за все игры";
        public static readonly string GAME_STATS_PUSHED_AWAY_BALLS_CURRENT = "Отбитых шариков в текущей игре";
        public static readonly string GAME_STATS_CURRENT_BALL_SPEED = "Текущая скорость шарика";
        public static readonly string GAME_STATS_LEVEL = "Текущий уровень";

        private bool isGamePaused = false;

        public bool IsGamePaused
        {
            get
            {
                return isGamePaused;
            }
        }

        /// <summary>
        /// Ширина игрового поля (ширина главной формы)
        /// </summary>
        public int GameFieldWidth { get; set; }

        /// <summary>
        /// Высота игрового поля (высота главной формы)
        /// </summary>
        public int GameFieldHeight { get; set; }

        public GameStats GameStatistics
        {
            get
            {
                return gameStats;
            }
        }

        /// <summary>
        /// Показывать ли игровую статистику прямо на игровом поле?
        /// Включить/выключить отображение можно нажатием клавиши S во время игры.
        /// По умолчанию не выводим статистику. Если нужно выводить, то поменять на true.
        /// </summary>
        public bool IsShowStats { get; set; } = false;


        public System.Windows.Forms.Timer timer;

        public GameObject PlayerPlatform
        {
            get
            {
                return platform;
            }
        }

        public GameObject BouncingBall
        {
            get
            {
                return ball;
            }
        }

        public List<StaticBlock> Blocks
        {
            get
            {
                return blocks;
            }
        }

        public GameEngine(System.Windows.Forms.Timer gameTimer, int gameFieldWidth, int gameFieldHeight)
        {
            timer = gameTimer;
            GameFieldWidth = gameFieldWidth;
            GameFieldHeight = gameFieldHeight;
        }

        public GameEngine(int gameFieldWidth, int gameFieldHeight) 
        {
            GameFieldWidth = gameFieldWidth;
            GameFieldHeight = gameFieldHeight;
        }

        public void setStats(GameStats stats) 
        {
            this.gameStats = stats;
        }

        public void setTimer(System.Windows.Forms.Timer gameTimer) 
        {
            timer = gameTimer;
        }

        public void StartGame()
        {
            InitGameObjectsPositionsAndState();
            timer.Start();
        }


        private void GenerateBlocksForCurrentLevel()
        {
            int blockWidth = 60;
            int blockHeight = 15;

            blocks.Clear();

            int currentGameLevel = gameStats.GetGameCounterValue(GAME_STATS_LEVEL);

            switch (currentGameLevel)
            {
                case 1:
                    // для 1-го уровня игры генерируем 3 одинаковых ряда сиреневых блоков, для разрушения каждого из блоков достаточно одного удара.
                    for (int blockLayer = 1; blockLayer <= 2; blockLayer++)
                    {
                        for (int n = 1; n <= 7; n++)
                        {
                            StaticBlock block = new StaticBlock("Блок #" + n + ", ряд #" + blockLayer, blockWidth, blockHeight);
                            block.Position.X = (n - 1) * blockWidth;
                            block.Position.Y = (blockLayer - 1) * blockHeight;
                            block.BorderColor = Color.Orange;
                            block.BodyColor = Color.Purple;
                            block.HitsToDestroy = 1;
                            blocks.Add(block);
                        }
                    }
                    break;
                case 2:
                    // для 2го уровня игры генерируем 3 ряда блоков:
                    // 1-й ряд - синие блоки (нужно 2 удара для уничтожения блока)
                    // 2-й и 3-й ряд - сиреневые блоки (нужен 1 удар для уничтожения блока)                    
                    for (int blockLayer = 1; blockLayer <= 3; blockLayer++)
                    {
                        for (int n = 1; n <= 7; n++)
                        {
                            StaticBlock block = new StaticBlock("Блок #" + n + ", ряд #" + blockLayer, blockWidth, blockHeight);
                            block.Position.X = (n - 1) * blockWidth;
                            block.Position.Y = (blockLayer - 1) * blockHeight;
                            block.BorderColor = Color.Orange;
                            if (blockLayer == 1)
                            {
                                block.BodyColor = Color.Blue;
                                block.HitsToDestroy = 2;
                            }
                            else
                            {
                                block.BodyColor = Color.Purple;
                                block.HitsToDestroy = 1;
                            }
                            blocks.Add(block);
                        }
                    }
                    break;
                case 3:
                    // для 3го уровня игры генерируем 4 ряда блоков:
                    // 1-й и 2-й ряд - синие блоки (нужно по 2 удара для уничтожения каждого блока)
                    // 3-й и 4-й ряд - сиреневые блоки (нужен 1 удар для уничтожения каждого блока)
                    for (int blockLayer = 1; blockLayer <= 4; blockLayer++)
                    {
                        for (int n = 1; n <= 7; n++)
                        {
                            StaticBlock block = new StaticBlock("Блок #" + n + ", ряд #" + blockLayer, blockWidth, blockHeight);
                            block.Position.X = (n - 1) * blockWidth;
                            block.Position.Y = (blockLayer - 1) * blockHeight;
                            block.BorderColor = Color.Orange;
                            if (blockLayer == 1 || blockLayer == 2)
                            {
                                block.BodyColor = Color.Blue;
                                block.HitsToDestroy = 2;
                            }
                            else
                            {
                                block.BodyColor = Color.Purple;
                                block.HitsToDestroy = 1;
                            }
                            blocks.Add(block);
                        }
                    }
                    break;
                case 4:
                    // для 4го уровня генерируем 4 ряда блоков:
                    // 1-й ряд - коричневые блоки (нужно по 3 удара для уничтожения каждого блока)
                    // 2-й ряд - синие блоки (нужно по 2 удара для уничтожения каждого блока)
                    // 3-й и 4-й ряд - сиреневые блоки (нужен 1 удар для уничтожения каждого блока)
                    for (int blockLayer = 1; blockLayer <= 4; blockLayer++)
                    {
                        for (int n = 1; n <= 7; n++)
                        {
                            StaticBlock block = new StaticBlock("Блок #" + n + ", ряд #" + blockLayer, blockWidth, blockHeight);
                            block.Position.X = (n - 1) * blockWidth;
                            block.Position.Y = (blockLayer - 1) * blockHeight;
                            block.BorderColor = Color.Orange;
                            if (blockLayer == 1)
                            {
                                block.BodyColor = Color.Brown;
                                block.HitsToDestroy = 3;
                            }
                            else if (blockLayer == 2)
                            {
                                block.BodyColor = Color.Blue;
                                block.HitsToDestroy = 2;
                            }
                            else
                            {
                                block.BodyColor = Color.Purple;
                                block.HitsToDestroy = 1;
                            }
                            blocks.Add(block);
                        }
                    }
                    break;
                case 5:
                    // для 5го уровня генерируем 4 ряда блоков:
                    // 1-й ряд - коричневые блоки (нужно по 3 удара для уничтожения каждого блока)
                    // 2-й, 3-й - синие блоки (нужно по 2 удара для уничтожения каждого блока)                    
                    // 4-й ряд - сиреневые блоки (нужен 1 удар для уничтожения каждого блока)
                    for (int blockLayer = 1; blockLayer <= 4; blockLayer++)
                    {
                        for (int n = 1; n <= 7; n++)
                        {
                            StaticBlock block = new StaticBlock("Блок #" + n + ", ряд #" + blockLayer, blockWidth, blockHeight);
                            block.Position.X = (n - 1) * blockWidth;
                            block.Position.Y = (blockLayer - 1) * blockHeight;
                            block.BorderColor = Color.Orange;
                            if (blockLayer == 1)
                            {
                                block.BodyColor = Color.Brown;
                                block.HitsToDestroy = 3;
                            }
                            else if (blockLayer == 2 || blockLayer == 3)
                            {
                                block.BodyColor = Color.Blue;
                                block.HitsToDestroy = 2;
                            }
                            else
                            {
                                block.BodyColor = Color.Purple;
                                block.HitsToDestroy = 1;
                            }
                            blocks.Add(block);
                        }
                    }
                    break;
                case 6:
                    // для 6го уровня генерируем 5 рядов блоков:
                    // 1-й и 2-й ряд - коричневые блоки (нужно по 3 удара для уничтожения каждого блока)
                    // 3-й, 4-й - синие блоки (нужно по 2 удара для уничтожения каждого блока)
                    // 5-й ряд - сиреневые блоки (нужно 1 удар для уничтожения каждого блока)
                    for (int blockLayer = 1; blockLayer <= 5; blockLayer++)
                    {
                        for (int n = 1; n <= 7; n++)
                        {
                            StaticBlock block = new StaticBlock("Блок #" + n + ", ряд #" + blockLayer, blockWidth, blockHeight);
                            block.Position.X = (n - 1) * blockWidth;
                            block.Position.Y = (blockLayer - 1) * blockHeight;
                            block.BorderColor = Color.Orange;
                            if (blockLayer == 1 || blockLayer == 2)
                            {
                                block.BodyColor = Color.Brown;
                                block.HitsToDestroy = 3;
                            }
                            else if (blockLayer == 3 || blockLayer == 4)
                            {
                                block.BodyColor = Color.Blue;
                                block.HitsToDestroy = 2;
                            }
                            else
                            {
                                block.BodyColor = Color.Purple;
                                block.HitsToDestroy = 1;
                            }
                            blocks.Add(block);
                        }
                    }
                    break;
            }
        }

        /// <summary>
        /// Инициализировать позиции и состояния игровых объектов
        /// </summary>
        public void InitGameObjectsPositionsAndState()
        {
            ResetObjectsPositions();

            gameStats.AddGameCounter(GAME_STATS_PUSHED_AWAY_BALLS_TOTAL, 0);
            gameStats.AddGameCounter(GAME_STATS_PUSHED_AWAY_BALLS_CURRENT, 0);
            gameStats.AddGameCounter(GAME_STATS_LEVEL, 1);
            gameStats.AddGameCounter(GAME_STATS_CURRENT_BALL_SPEED, BALL_STARTING_SPEED);

            ball.CollapsedWithOtherObjects += Ball_CollapsedWithOtherObjects;
            ball.InitIncrementNumberOfFailures += Ball_InitIncrementNumberOfFailures;
            ball.InitPositiveGameAction += Ball_InitPositiveGameAction;

            IBouncingDiagonalMovingGameObject bouncingBall = ball as IBouncingDiagonalMovingGameObject;

            // указываем шарику, что объект, от которого он будет отталкиваться - это платформа игрока
            bouncingBall.SetBounceFromObject(platform);

            GenerateBlocksAndSetThemAsDestroyingObjects(bouncingBall);

            InitializeBallMovement(bouncingBall);

            // достижение стены снизу - признак окончания игры
            bouncingBall.SetWallFailureConstraint(WallPosition.WallFromTheBottom);
        }

        private void Ball_InitPositiveGameAction(object sender, EventArgs e)
        {
            gameStats.IncrementGameCounter(GAME_STATS_PUSHED_AWAY_BALLS_TOTAL);
            gameStats.IncrementGameCounter(GAME_STATS_PUSHED_AWAY_BALLS_CURRENT);
        }

        private void Ball_InitIncrementNumberOfFailures(object sender, EventArgs e)
        {
            gameStats.IncrementNumberOfFailures();
        }

        private void Ball_CollapsedWithOtherObjects(object sender, ICollection<GameObject> destroyedBlocks)
        {
            Blocks.RemoveAll(block => destroyedBlocks.Contains(block));
        }

        private void GenerateBlocksAndSetThemAsDestroyingObjects(IBouncingDiagonalMovingGameObject bouncingBall)
        {
            GenerateBlocksForCurrentLevel();
            List<GameObject> destroyingBlocks = new List<GameObject>(blocks);
            bouncingBall.SetBounceFromDestroyingObjects(destroyingBlocks);
        }


        /// <summary>
        /// Сбросить положения игровых объектов в начальные
        /// </summary>
        public void ResetObjectsPositions()
        {
            platform.SetPosition(GameFieldWidth / 2 - platformWidth / 2, GameFieldHeight - platformHeight - bottomMargin);
            ball.SetPosition(GameFieldWidth / 2 - ballRadius, GameFieldHeight / 2 - ballRadius);
        }

        /// <summary>
        /// Инициализировать движение игрового шарика
        /// </summary>
        /// <param name="bouncingBall">объект игрового шарика</param>
        public void InitializeBallMovement(IBouncingDiagonalMovingGameObject bouncingBall)
        {
            // инициируем произвольное, но "безопасное для игрока" движение шарика в диагональном направлении.
            // безопасное - когда шарик полетит либо вверх-вправо, либо вверх-влево, давая игроку время на то, 
            // чтобы включиться в игру и отреагировать
            bouncingBall.InitRandomSafeDiagonalMovingDirection();

            // Если нужно позволить шарику двигаться на старте игры в любом направлении -
            // закомментировать предыдущую строку кода и раскомментировать ту, что ниже:
            //bouncingBall.InitRandomDiagonalMovingDirection();

            // задаём начальную скорость движения шарика
            bouncingBall.SetMovingSpeed(BALL_STARTING_SPEED);
        }

        /// <summary>
        /// Сброс игры и показателей, относящихся к выигранной/проигранной текущей игре.
        /// </summary>
        public void ResetGame()
        {
            gameStats.ResetGameCounter(GAME_STATS_PUSHED_AWAY_BALLS_CURRENT);
            gameStats.SetGameCounterValue(GAME_STATS_LEVEL, 1);
            gameStats.SetGameCounterValue(GAME_STATS_CURRENT_BALL_SPEED, BALL_STARTING_SPEED);
            ResetObjectsPositions();
            GenerateBlocksAndSetThemAsDestroyingObjects(ball as IBouncingDiagonalMovingGameObject);
            InitializeBallMovement(ball as IBouncingDiagonalMovingGameObject);


            // возвращаем размер платформы игрока в исходный
            (platform as Platform).Width = platformWidth;
        }

        public void HandleMouseMove(Point mouseCursorLocation)
        {
            Platform rectangularPlatform = platform as Platform;
            if (rectangularPlatform.CanMoveToPointWhenCentered(mouseCursorLocation, 0, GameFieldWidth, 15))
            {
                rectangularPlatform.SetPositionCenteredHorizontally(mouseCursorLocation.X);
            }
        }

        public void CheckForGameLevelIncrease()
        {
            if (blocks.Count > 0)
            {
                // ещё остались блоки, не повышать уровень игры
                return;
            }

            int currentPushedAwayBalls = gameStats.GetGameCounterValue(GAME_STATS_PUSHED_AWAY_BALLS_CURRENT);
            int currentGameLevel = gameStats.GetGameCounterValue(GAME_STATS_LEVEL);

            IBouncingDiagonalMovingGameObject bouncingBall = ball as IBouncingDiagonalMovingGameObject;
            Platform rectPlayerPlatform = platform as Platform;

            int currentBallSpeed = bouncingBall.GetMovingSpeed();

            for (int level = 1; level <= TOTAL_GAME_LEVELS_TO_WIN; level++)
            {
                if (currentGameLevel == level /*&& currentPushedAwayBalls >= BALLS_TO_PUSH_AWAY_FOR_NEXT_LEVEL * currentGameLevel*/)
                {
                    gameStats.IncrementGameCounter(GAME_STATS_LEVEL);

                    GenerateBlocksAndSetThemAsDestroyingObjects(bouncingBall);

                    int newBallSpeed = currentBallSpeed + 1;
                    // увеличиваем скорость движения шарика при достижении очередного уровня
                    bouncingBall.SetMovingSpeed(newBallSpeed);
                    gameStats.SetGameCounterValue(GAME_STATS_CURRENT_BALL_SPEED, newBallSpeed);

                    // уменьшаем ширину движущейся платформы игрока на 10 пикселей (можно закомментировать, если это не нужно):
                    rectPlayerPlatform.Width -= 10;
                    break;
                }
            }
        }

        public void PauseGame(Action actionWhenPaused)
        {
            timer.Stop();
            isGamePaused = true;
            actionWhenPaused.Invoke();
        }

        public void UnpauseGame()
        {
            timer.Start();
            isGamePaused = false;
        }

        public void ToggleGamePauseMode(Action actionWhenPaused)
        {
            if (isGamePaused)
            {
                UnpauseGame();
            }
            else
            {
                PauseGame(actionWhenPaused);
            }
        }

        public void RestartGame()
        {
            if (!timer.Enabled)
            {
                IBouncingDiagonalMovingGameObject bouncingMovingBall = ball as IBouncingDiagonalMovingGameObject;
                bouncingMovingBall.ResetReachedWallFailureConstraint();
                ResetGame();
                timer.Start();
            }
        }

        public void HandleGameCycle(Action actionIfWinHappened, Func<bool> funcIsContinueWhenGameIsOver, Func<int, bool> funcIsRepeatAfterWin)
        {
            IBouncingDiagonalMovingGameObject bouncingMovingBall = ball as IBouncingDiagonalMovingGameObject;

            if (bouncingMovingBall.CanMoveAtCurrentDirection(0, GameFieldWidth, 0, GameFieldHeight, 15, 37))
            {
                bouncingMovingBall.MoveAtCurrentDirection();
            }
            else
            {
                if (bouncingMovingBall.ReachedWallFailureConstraint())
                {
                    timer.Stop();
                    if (funcIsContinueWhenGameIsOver.Invoke())
                    {
                        // перезапускаем игру
                        bouncingMovingBall.ResetReachedWallFailureConstraint();
                        ResetGame();
                        timer.Start();
                    }
                }
                else
                {
                    bouncingMovingBall.Bounce();
                    CheckForGameLevelIncrease();
                    CheckForWin(() => {
                        timer.Stop();
                        actionIfWinHappened.Invoke();
                    },
                    funcIsRepeatAfterWin);
                }
            }
        }


        public void CheckForWin(Action actionIfWinHappened, Func<int, bool> funcIsRepeatAfterWin)
        {
            int currentGameLevel = gameStats.GetGameCounterValue(GAME_STATS_LEVEL);
            if (currentGameLevel == TOTAL_GAME_LEVELS_TO_WIN)
            {
                gameStats.IncrementNumberOfWins();
                actionIfWinHappened.Invoke();
                if (funcIsRepeatAfterWin.Invoke(currentGameLevel))
                {
                    // выбрали начать игру заново
                    ResetGame();
                    timer.Start();
                }
            }
        }
    }
}
