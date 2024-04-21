using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArkanoidGame
{
    public class Ball : GameObject, IBouncingDiagonalMovingGameObject
    {
        protected int radius;

        /// <summary>
        /// Экземпляр класса Random для генерации случайных чисел
        /// </summary>
        protected Random random;

        /// <summary>
        /// Позиция стены, с которой столкнулся шарик
        /// </summary>
        protected WallPosition wallPosition;

        /// <summary>
        /// Позиция той "стены", которая является признаком проигрыша в игре.
        /// В нашем примере это будет нижняя "стена", т.е. нижний край формы
        /// </summary>
        protected WallPosition failureWallConstraint;

        /// <summary>
        /// Достигнуто ли ограничение для того, чтобы игрок проиграл?
        /// (т.е. достигнута ли нижняя "стена", или нижний край формы?)
        /// </summary>
        protected bool reachedFailureConstraint;

        /// <summary>
        /// Скорость движения шарика
        /// </summary>
        protected int movingSpeed;

        /// <summary>
        /// Игровой объект, от которого шарик будет отталкиваться (помимо "стен").
        /// В игре этот объект - экземпляр платформы игрока
        /// </summary>
        protected GameObject movingPlatform;

        /// <summary>
        /// Список уничтожающихся статичных блоков, в которые должен попадать шарик
        /// </summary>
        protected List<GameObject> destroyingStaticBlocks;

        /// <summary>
        /// Признак того, что произошло столкновение с платформой игрока.
        /// </summary>
        protected bool happenedCollisionWithBounceFromObject;

        /// <summary>
        /// Поле описывает диагональное движение в одном из допустимых направлений.
        /// </summary>
        protected IMovingDirection diagonalMovingDirection;

        /// <summary>
        /// Радуис шарика
        /// </summary>
        public int Radius
        {
            get
            {
                return radius;
            }
        }

        /// <summary>
        /// Конструктор шарика
        /// </summary>
        /// <param name="title">название игрового объекта (шарика)</param>
        /// <param name="radius">радиус шарика</param>
        public Ball(string title, int radius) : base(title)
        {
            this.radius = radius;
            this.movingSpeed = 1;
            this.reachedFailureConstraint = false;
            this.happenedCollisionWithBounceFromObject = false;
            this.wallPosition = WallPosition.NoWall;
            this.failureWallConstraint = WallPosition.NoWall;
            this.random = new Random();
        }

        /// <summary>
        /// Получить объект класса Rectangle, который описывает местоположение и размеры шарика 
        /// (т.е. фактически это описанный вокруг шарика квадрат)
        /// </summary>
        /// <returns>инициализированный объект класса Rectangle, описывающий текущее местоположение и размеры шарика</returns>
        public override Rectangle GetObjectRectangle()
        {
            return new Rectangle(position.X, position.Y, 2 * Radius, 2 * Radius);
        }

        /// <summary>
        /// Предписывает шарику двигаться "вверх-вправо" с его текущей скоростью
        /// </summary>
        public void MoveUpRight()
        {
            position.X += movingSpeed;
            position.Y -= movingSpeed;
        }

        /// <summary>
        /// Предписывает шарику двигаться "вверх-влево" с его текущей скоростью
        /// </summary>
        public void MoveUpLeft()
        {
            position.X -= movingSpeed;
            position.Y -= movingSpeed;
        }

        /// <summary>
        /// Предписывает шарику двигаться "вниз-влево" с его текущей скоростью
        /// </summary>
        public void MoveDownLeft()
        {
            position.X -= movingSpeed;
            position.Y += movingSpeed;
        }

        /// <summary>
        /// Предписывает шарику двигаться "вниз-вправо" с его текущей скоростью
        /// </summary>
        public void MoveDownRight()
        {
            position.X += movingSpeed;
            position.Y += movingSpeed;
        }

        /// <summary>
        /// Инициализировать произвольное направление движения по какой-то из диагоналей и установить его 
        /// в качестве текущего направления движения шарика
        /// </summary>
        public void InitRandomDiagonalMovingDirection()
        {
            IMovingDirection direction = new SimpleDiagonalMoving();
            direction.InitRandomDirection();
            SetMovingDirection(direction);
        }

        /// <summary>
        /// Инициализировать произвольное и безопасное для игрока направление движения по какой-то 
        /// из диагоналей и установить его в качестве текущего направления движения шарика
        /// </summary>
        public void InitRandomSafeDiagonalMovingDirection()
        {
            IMovingDirection safeDirection = new SimpleDiagonalMoving();
            safeDirection.InitRandomSafeDirection();
            SetMovingDirection(safeDirection);
        }

        /// <summary>
        /// Указывает шарику продолжить движение в текущем направлении его движения
        /// </summary>
        public void MoveAtCurrentDirection()
        {
            if (diagonalMovingDirection == null || diagonalMovingDirection.IsNotMoving())
            {
                return;
            }

            if (diagonalMovingDirection.IsMovingUpLeft())
            {
                MoveUpLeft();
            }
            else if (diagonalMovingDirection.IsMovingUpRight())
            {
                MoveUpRight();
            }
            else if (diagonalMovingDirection.IsMovingDownLeft())
            {
                MoveDownLeft();
            }
            else if (diagonalMovingDirection.IsMovingDownRight())
            {
                MoveDownRight();
            }
        }

        /// <summary>
        /// Произошло ли столкновение с другим игровым объектом (платформа игрока) в указанной позиции шарика?
        /// </summary>
        /// <param name="newX">координата X шарика, в которой потенциально производится столкновение с платформой игрока</param>
        /// <param name="newY">координата Y шарика, в которой потенциально производится столкновение с платформой игрока</param>
        /// <returns>true, если произошло столкновение, иначе false</returns>
        public bool IsCollisionWithBounceFromObject(int newX, int newY)
        {

            if (movingPlatform.Position.Y > newY + GetObjectRectangle().Height)
            {
                // шарик ещё не долетел до низа, поэтому у нас точно нет столкновения с платформой
                return false;
            }

            if (movingPlatform.Position.X > newX + GetObjectRectangle().Width ||
                movingPlatform.Position.X + movingPlatform.GetObjectRectangle().Width < newX)
            {
                return false;
            }

            happenedCollisionWithBounceFromObject = true;

            // мы отбили шарик, вызываем позитивное действие в игре, которое увеличит нужный счётчик в статистике
            OnInitPositiveGameAction();

            return true;
        }

        /// <summary>
        /// Произошло ли столкновение с одним или несколькими статичными блоками?
        /// </summary>
        /// <param name="newX">координата X шарика, в которой потенциально производится столкновение со статичным блоком</param>
        /// <param name="newY">координата Y шарика, в которой потенциально производится столкновение со статичным блоком</param>
        /// <returns>true, если произошло столкновение, иначе false</returns>
        public bool IsCollisionWithDestroyingObjects(int newX, int newY)
        {
            if (destroyingStaticBlocks == null || destroyingStaticBlocks.Count == 0)
            {
                return false;
            }

            List<StaticBlock> destroyedBlocks = new List<StaticBlock>();
            bool isCollisionWithOneOfTheBlocks = false;

            foreach (GameObject blockGameObject in destroyingStaticBlocks)
            {
                if (blockGameObject.Position.Y + blockGameObject.GetObjectRectangle().Height < newY)
                {
                    continue;
                }

                if (blockGameObject.Position.X > newX + GetObjectRectangle().Width ||
                    blockGameObject.Position.X + blockGameObject.GetObjectRectangle().Width < newX)
                {
                    continue;
                }

                if (blockGameObject is StaticBlock staticBlock)
                {
                    isCollisionWithOneOfTheBlocks = true;
                    if (staticBlock.CurrentHits + 1 >= staticBlock.HitsToDestroy)
                    {
                        destroyedBlocks.Add(staticBlock);
                    }
                    else
                    {
                        staticBlock.CurrentHits++;
                    }
                }
            }

            if (isCollisionWithOneOfTheBlocks)
            {
                List<GameObject> destroyedBlockObjects = new List<GameObject>(destroyedBlocks);
                OnCollapsedWithOtherObjects(destroyedBlockObjects);
                destroyingStaticBlocks.RemoveAll(block => block is StaticBlock staticBlock && destroyedBlocks.Contains(staticBlock));

                wallPosition = WallPosition.WallFromTheTop;
                return true;
            }

            return false;
        }

        /// <summary>
        /// Может ли шарик двигаться дальше в текущем направлении его движения?
        /// </summary>
        /// <param name="lowerBoundX">левая граница игрового поля, по оси X (левый край формы)</param>
        /// <param name="upperBoundX">правая граница игрового поля, по оси X (правый край формы)</param>
        /// <param name="lowerBoundY">нижняя граница игрового поля, по оси Y (верхний край формы)</param>
        /// <param name="upperBoundY">верхняя граница игрового поля, по оси Y (нижний край формы)</param>
        /// <param name="upperBoundXDelta">допустимая погрешность (дельта) для правой границы игрового поля (для правого края формы)</param>
        /// <param name="upperBoundYDelta">допустимая погрешность (дельта) для верхней границы игрового поля (для нижнего края формы)</param>
        /// <returns>true - если шарик может продолжать движение в текущем направлении, иначе false</returns>
        public bool CanMoveAtCurrentDirection(int lowerBoundX, int upperBoundX, int lowerBoundY, int upperBoundY, int upperBoundXDelta, int upperBoundYDelta)
        {
            if (diagonalMovingDirection == null || diagonalMovingDirection.IsNotMoving())
            {
                return false;
            }

            int newX = 0;
            int newY = 0;

            if (diagonalMovingDirection.IsMovingUpLeft())
            {
                newX = position.X - movingSpeed;
                newY = position.Y - movingSpeed;

                if (IsCollisionWithDestroyingObjects(newX, newY))
                {
                    return false;
                }

                if (position.X - movingSpeed > lowerBoundX)
                {
                    if (position.Y - movingSpeed > lowerBoundY)
                    {
                        return true;
                    }
                    else
                    {
                        wallPosition = WallPosition.WallFromTheTop;
                    }
                }
                else
                {
                    wallPosition = WallPosition.WallFromTheLeft;
                }
            }
            else if (diagonalMovingDirection.IsMovingUpRight())
            {
                newX = position.X + movingSpeed;
                newY = position.Y - movingSpeed;

                if (IsCollisionWithDestroyingObjects(newX, newY))
                {
                    return false;
                }

                if (position.X + movingSpeed + GetObjectRectangle().Width + upperBoundXDelta < upperBoundX)
                {
                    if (position.Y - movingSpeed > lowerBoundY)
                    {
                        return true;
                    }
                    else
                    {
                        wallPosition = WallPosition.WallFromTheTop;
                    }
                }
                else
                {
                    wallPosition = WallPosition.WallFromTheRight;
                }
            }
            else if (diagonalMovingDirection.IsMovingDownLeft())
            {
                newX = position.X - movingSpeed;
                newY = position.Y + movingSpeed + GetObjectRectangle().Height + upperBoundYDelta;

                if (IsCollisionWithBounceFromObject(position.X, position.Y))
                {
                    return false;
                }

                if (newX > lowerBoundX)
                {
                    if (newY < upperBoundY)
                    {
                        return true;
                    }
                    else
                    {
                        wallPosition = WallPosition.WallFromTheBottom;
                        if (wallPosition == failureWallConstraint)
                        {
                            OnInitIncrementNumberOfFailures();
                            reachedFailureConstraint = true;
                        }
                    }
                }
                else
                {
                    wallPosition = WallPosition.WallFromTheLeft;
                }
            }
            else if (diagonalMovingDirection.IsMovingDownRight())
            {
                newX = position.X + movingSpeed + GetObjectRectangle().Width + upperBoundXDelta;
                newY = position.Y + movingSpeed + GetObjectRectangle().Height + upperBoundYDelta;

                if (IsCollisionWithBounceFromObject(position.X, position.Y))
                {
                    return false;
                }

                if (newX < upperBoundX)
                {
                    if (newY < upperBoundY)
                    {
                        return true;
                    }
                    else
                    {
                        wallPosition = WallPosition.WallFromTheBottom;
                        if (wallPosition == failureWallConstraint)
                        {
                            OnInitIncrementNumberOfFailures();
                            reachedFailureConstraint = true;
                        }
                    }
                }
                else
                {
                    wallPosition = WallPosition.WallFromTheRight;
                }
            }

            return false;
        }

        /// <summary>
        /// Предписывает шарику оттолкнуться от стены при движении "вверх-влево"
        /// </summary>
        private void BounceWhenMovingUpLeft()
        {
            if (wallPosition == WallPosition.WallFromTheLeft)
            {
                diagonalMovingDirection.ChangeDirectionToUpRight();
            }
            else if (wallPosition == WallPosition.WallFromTheTop)
            {
                diagonalMovingDirection.ChangeDirectionToDownLeft();
            }
        }

        /// <summary>
        /// Предписывает шарику оттолкнуться от стены при движении "вверх-вправо"
        /// </summary>
        private void BounceWhenMovingUpRight()
        {
            if (wallPosition == WallPosition.WallFromTheRight)
            {
                diagonalMovingDirection.ChangeDirectionToUpLeft();
            }
            else if (wallPosition == WallPosition.WallFromTheTop)
            {
                diagonalMovingDirection.ChangeDirectionToDownRight();
            }
        }

        /// <summary>
        /// Проверить столкновение с платформой игрока, и если оно случилось, то выполнить заданное
        /// действие <paramref name="actionIfCollisionHappened"/>
        /// </summary>
        /// <param name="actionIfCollisionHappened">действие, которое нужно выполнить при столкновении с платформой игрока</param>
        private void CheckForCollisionWithPlatform(Action actionIfCollisionHappened)
        {
            if (happenedCollisionWithBounceFromObject)
            {
                actionIfCollisionHappened.Invoke();
                happenedCollisionWithBounceFromObject = false;
            }
        }

        /// <summary>
        /// Проверить столкновение с платформой игрока и оттолкнуться при исходном движении "вниз-влево"
        /// </summary>
        private void CheckForCollisionWithPlatformAndBounceWhenMovingDownLeft()
        {
            CheckForCollisionWithPlatform(() => diagonalMovingDirection.ChangeDirectionToUpLeft());

            if (wallPosition == WallPosition.WallFromTheLeft)
            {
                diagonalMovingDirection.ChangeDirectionToDownRight();
            }
            else if (wallPosition == WallPosition.WallFromTheBottom)
            {
                diagonalMovingDirection.ChangeDirectionToUpLeft();
            }
        }

        /// <summary>
        /// Проверить столкновение с платформой игрока и оттолкнуться при исходном движении "вниз-вправо"
        /// </summary>
        private void CheckForCollisionWithPlatformAndBounceWhenMovingDownRight()
        {
            CheckForCollisionWithPlatform(() => diagonalMovingDirection.ChangeDirectionToUpRight());

            if (wallPosition == WallPosition.WallFromTheRight)
            {
                diagonalMovingDirection.ChangeDirectionToDownLeft();
            }
            else if (wallPosition == WallPosition.WallFromTheBottom)
            {
                diagonalMovingDirection.ChangeDirectionToUpRight();
            }
        }

        /// <summary>
        /// Выполняет действие шарика "оттолкнуться" в зависимости от текущего направления
        /// движения шарика
        /// </summary>
        public void Bounce()
        {
            if (diagonalMovingDirection == null || diagonalMovingDirection.IsNotMoving())
            {
                return;
            }

            if (diagonalMovingDirection.IsMovingUpLeft())
            {
                BounceWhenMovingUpLeft();
            }
            else if (diagonalMovingDirection.IsMovingUpRight())
            {
                BounceWhenMovingUpRight();
            }
            else if (diagonalMovingDirection.IsMovingDownLeft())
            {
                CheckForCollisionWithPlatformAndBounceWhenMovingDownLeft();
            }
            else if (diagonalMovingDirection.IsMovingDownRight())
            {
                CheckForCollisionWithPlatformAndBounceWhenMovingDownRight();
            }
        }

        /// <summary>
        /// Установить значение "стены", достижение которой является проигрышем в игре.
        /// В игре такой стеной является нижняя "стена".
        /// </summary>
        /// <param name="failureWallConstraint">значение "стены", являющейся условием проигрыша</param>
        public void SetWallFailureConstraint(WallPosition failureWallConstraint)
        {
            this.failureWallConstraint = failureWallConstraint;
        }

        /// <summary>
        /// Достигнута ли нижняя "стена"? (т.е. нижний край формы)
        /// </summary>
        /// <returns>true - нижняя "стена" достигнута, false - не достигнута</returns>
        public bool ReachedWallFailureConstraint()
        {
            return reachedFailureConstraint;
        }

        /// <summary>
        /// Изменить скорость движения шарика на новую
        /// </summary>
        /// <param name="speed">новая скорость движения шарика</param>
        public void SetMovingSpeed(int speed)
        {
            movingSpeed = speed;
        }

        /// <summary>
        /// Получить текущую скорость движения шарика
        /// </summary>
        /// <returns></returns>
        public int GetMovingSpeed()
        {
            return movingSpeed;
        }

        /// <summary>
        /// Установить другой игровой объект в качестве объекта, от которого
        /// будет отталкиваться шарик. В игре этим объектом выступает движущаяся платформа игрока.
        /// </summary>
        /// <param name="gameObject">другой игровой объект, от которого будет отталкиваться шарик</param>
        public void SetBounceFromObject(GameObject gameObject)
        {
            movingPlatform = gameObject;
        }

        /// <summary>
        /// Сбросить признак достижения нижней "стены" (которая является
        /// условием проигрыша в игре). Нижняя "стена" - это нижний край формы
        /// </summary>
        public void ResetReachedWallFailureConstraint()
        {
            reachedFailureConstraint = false;
        }

        /// <summary>
        /// Инициализировать произвольное движение игрового объекта (шарика)
        /// </summary>
        public void InitRandomMovingDirection()
        {
            InitRandomDiagonalMovingDirection();
        }

        /// <summary>
        /// Получить текущее направление движения шарика
        /// </summary>
        /// <returns></returns>
        public IMovingDirection GetMovingDirection()
        {
            return diagonalMovingDirection;
        }

        /// <summary>
        /// Установить новое направление движения шарика
        /// </summary>
        /// <param name="movingDirection">новое направление движения шарика</param>
        public void SetMovingDirection(IMovingDirection movingDirection)
        {
            this.diagonalMovingDirection = movingDirection;
        }

        /// <summary>
        /// Установить шарику список уничтожаемых статичных блоков для дальнейшей проверки на столкновение
        /// с ними
        /// </summary>
        /// <param name="destroyingGameObjects">список статичных блоков</param>
        public void SetBounceFromDestroyingObjects(List<GameObject> destroyingGameObjects)
        {
            this.destroyingStaticBlocks = destroyingGameObjects;
        }
    }
}
