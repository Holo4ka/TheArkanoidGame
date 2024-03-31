using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace TheArkanoidGame
{
    internal class Ball : Figure, IBouncingDiagonalMoving
    {
        private int radius = 16;
        private int speed;
        private Random random;
        protected IDiagonalMovingDirection diagonalMovingDirection;
        protected Platform platform;
        private int topWallY = 12;
        private int bottomWallY = 12 + 487;
        private int leftWallX = 12;
        private int rightWallX = 12 + 980;

        public Ball(int x, int y, Brush brush, Color color, Graphics gr) : base(x, y, brush, color, gr) 
        {
            this.speed = 1;
            this.random = new Random();
            SetPosition(x, y);
        }

        public void Draw() 
        {
            gr.FillEllipse(this.brush, GetObjectRectangle());
            gr.DrawEllipse(new Pen(this.brush, 2), GetObjectRectangle());
        }

        public override Rectangle GetObjectRectangle()
        {
            return new Rectangle(position.X, position.Y, radius * 2, radius * 2);
        }

        public void SetMovingSpeed(int newSpeed) 
        {
            this.speed = newSpeed;
        }

        public int GetMovingSpeed() 
        {
            return speed;
        }

        public IDiagonalMovingDirection GetMovingDirection() 
        {
            return diagonalMovingDirection;
        }

        public void MoveUpRight()
        {
            position.X += speed;
            position.Y -= speed;
        }

        public void MoveUpLeft()
        {
            position.X -= speed;
            position.Y -= speed;
        }

        public void MoveDownLeft()
        {
            position.X -= speed;
            position.Y += speed;
        }

        public void MoveDownRight()
        {
            position.X += speed;
            position.Y += speed;
        }

        /*public void InitRandomDiagonalMovingDirection()
        {
            IDiagonalMovingDirection direction = new DiagonalMoving();
            direction.InitRandomDirection();
            SetMovingDirection(direction);
        }*/

        public void InitRandomSafeDiagonalMoving() 
        {
            IDiagonalMovingDirection direction = new DiagonalMoving();
            direction.InitRandomSafeDirection();
            SetMovingDirection(direction);
        }

        public void SetMovingDirection(IDiagonalMovingDirection movingDirection)
        {
            this.diagonalMovingDirection = movingDirection;
        }

        public void MoveAtCurrentDirection() //Команда продолжить движение
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

            Draw();
        }

        //TODO
        public bool IsCollisionWithDestroyingObjects() { return true; }

    }
}
