using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
//using static TheArkanoidGame1.DiagonalMoving;

namespace TheArkanoidGame1
{
    internal class Ball : Figure, IDiagonalMoving
    {
        private int radius = 16;
        private int speed;
        private Random random;
        protected DiagonalMovingDirection diagonalMovingDirection;
        protected Platform platform;
        private int topWallY = 12;
        private int bottomWallY = 12 + 487;
        private int leftWallX = 12;
        private int rightWallX = 12 + 980;

        public enum DiagonalMovingDirection
        {
            IsNotMoving,
            MovingUpLeft,
            MovingUpRight,
            MovingDownLeft,
            MovingDownRight
        }

        public Ball(int x, int y, Brush brush, Color color) : base(x, y, brush, color)
        {
            this.speed = 1;
            this.random = new Random();
            //SetPosition(x, y);
        }

        public void Draw()
        {
            //gr.FillEllipse(this.brush, this.x, this.y, this.radius * 2, this.radius * 2);
            //gr.DrawEllipse(new Pen(this.brush, 2), this.x, this.y, this.radius * 2, this.radius * 2);
        }

        /*public override Rectangle GetObjectRectangle()
        {
            return new Rectangle(position.X, position.Y, radius * 2, radius * 2);
        }*/

        public void SetMovingSpeed(int newSpeed)
        {
            this.speed = newSpeed;
        }

        public int GetMovingSpeed()
        {
            return speed;
        }

        public DiagonalMovingDirection GetMovingDirection()
        {
            return diagonalMovingDirection;
        }

        public void MoveUpRight()
        {
            this.x += speed;
            this.y -= speed;
        }

        public void MoveUpLeft()
        {
            this.x -= speed;
            this.y -= speed;
        }

        public void MoveDownLeft()
        {
            this.x -= speed;
            this.y += speed;
        }

        public void MoveDownRight()
        {
            this.x += speed;
            this.y += speed;
        }

        /*public void InitRandomDiagonalMovingDirection()
        {
            IDiagonalMovingDirection direction = new DiagonalMoving();
            direction.InitRandomDirection();
            SetMovingDirection(direction);
        }*/

        public void InitRandomSafeDiagonalMoving()
        {
            DiagonalMovingDirection direction = new DiagonalMovingDirection();
            int randomSafeDirection = random.Next(0, 2);
            switch (randomSafeDirection)
            {
                case 0:
                    direction = DiagonalMovingDirection.MovingUpLeft;
                    break;
                case 1:
                    direction = DiagonalMovingDirection.MovingUpRight;
                    break;
            }
            SetMovingDirection(direction);
        }

        public void SetMovingDirection(DiagonalMovingDirection movingDirection)
        {
            this.diagonalMovingDirection = movingDirection;
        }

        public void MoveAtCurrentDirection() //Команда продолжить движение
        {
            if (diagonalMovingDirection == DiagonalMovingDirection.IsNotMoving)
            {
                return;
            }

            if (diagonalMovingDirection == DiagonalMovingDirection.MovingUpLeft)
            {
                MoveUpLeft();
            }
            else if (diagonalMovingDirection == DiagonalMovingDirection.MovingUpRight)
            {
                MoveUpRight();
            }
            else if (diagonalMovingDirection == DiagonalMovingDirection.MovingDownLeft)
            {
                MoveDownLeft();
            }
            else if (diagonalMovingDirection == DiagonalMovingDirection.MovingUpLeft)
            {
                MoveDownRight();
            }

            //Draw();
        }

        public bool IsNotMoving() { return diagonalMovingDirection == DiagonalMovingDirection.IsNotMoving; }

        public bool IsMovingUpRight() { return diagonalMovingDirection == DiagonalMovingDirection.MovingUpRight; }

        public bool IsMovingDownLeft() { return diagonalMovingDirection == DiagonalMovingDirection.MovingDownLeft; }

        public bool IsMovingDownRight() { return diagonalMovingDirection == DiagonalMovingDirection.MovingDownRight; }

        public bool IsMovingUpLeft() { return diagonalMovingDirection == DiagonalMovingDirection.MovingUpLeft; }

        public void ChangeDirectionToUpLeft() 
        { 
            DiagonalMovingDirection direction = new DiagonalMovingDirection();
            direction = DiagonalMovingDirection.MovingUpLeft;
            SetMovingDirection(direction);
        }

        public void ChangeDirectionToDownLeft() 
        {
            DiagonalMovingDirection direction = new DiagonalMovingDirection();
            direction = DiagonalMovingDirection.MovingDownLeft;
            SetMovingDirection(direction);
        }

        public void ChangeDirectionToDownRight() 
        {
            DiagonalMovingDirection direction = new DiagonalMovingDirection();
            direction = DiagonalMovingDirection.MovingDownRight;
            SetMovingDirection(direction);
        }

        public void ChangeDirectionToUpRight() 
        {
            DiagonalMovingDirection direction = new DiagonalMovingDirection();
            direction = DiagonalMovingDirection.MovingUpRight;
            SetMovingDirection(direction);
        }

        //TODO
        public bool IsCollisionWithDestroyingObjects() { return true; }

    }
}
