using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArkanoidGame
{
    public class SimpleDiagonalMoving : IMovingDirection
    {
        private DiagonalMovingDirection currentDirection;
        private Random random;

        public enum DiagonalMovingDirection
        {
            IsNotMoving,
            MovingUpLeft,
            MovingUpRight,
            MovingDownLeft,
            MovingDownRight
        }

        public SimpleDiagonalMoving(DiagonalMovingDirection currentDirection) : this()
        {
            this.currentDirection = currentDirection;
        }

        public SimpleDiagonalMoving()
        {
            currentDirection = DiagonalMovingDirection.IsNotMoving;
            random = new Random();
        }


        public bool IsMovingDownLeft()
        {
            return currentDirection == DiagonalMovingDirection.MovingDownLeft;
        }

        public bool IsMovingDownRight()
        {
            return currentDirection == DiagonalMovingDirection.MovingDownRight;
        }

        public bool IsMovingUpLeft()
        {
            return currentDirection == DiagonalMovingDirection.MovingUpLeft;
        }

        public bool IsMovingUpRight()
        {
            return currentDirection == DiagonalMovingDirection.MovingUpRight;
        }

        public bool IsNotMoving()
        {
            return currentDirection == DiagonalMovingDirection.IsNotMoving;
        }

        public void InitRandomDirection()
        {
            int randomDirection = random.Next(0, 4);
            switch (randomDirection)
            {
                case 0:
                    currentDirection = DiagonalMovingDirection.MovingUpLeft;
                    break;
                case 1:
                    currentDirection = DiagonalMovingDirection.MovingUpRight;
                    break;
                case 2:
                    currentDirection = DiagonalMovingDirection.MovingDownLeft;
                    break;
                case 3:
                    currentDirection = DiagonalMovingDirection.MovingDownRight;
                    break;
            }
        }

        public void InitRandomSafeDirection()
        {
            int randomSafeDirection = random.Next(0, 2);
            switch (randomSafeDirection)
            {
                case 0:
                    currentDirection = DiagonalMovingDirection.MovingUpLeft;
                    break;
                case 1:
                    currentDirection = DiagonalMovingDirection.MovingUpRight;
                    break;
            }
        }

        public void ChangeDirectionToUpLeft()
        {
            currentDirection = DiagonalMovingDirection.MovingUpLeft;
        }

        public void ChangeDirectionToUpRight()
        {
            currentDirection = DiagonalMovingDirection.MovingUpRight;
        }

        public void ChangeDirectionToDownLeft()
        {
            currentDirection = DiagonalMovingDirection.MovingDownLeft;
        }

        public void ChangeDirectionToDownRight()
        {
            currentDirection = DiagonalMovingDirection.MovingDownRight;
        }
    }
}
