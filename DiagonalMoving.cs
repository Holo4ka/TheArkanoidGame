using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static TheArkanoidGame1.DiagonalMoving;

namespace TheArkanoidGame1
{
    internal class DiagonalMoving : IDiagonalMoving
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

        public DiagonalMoving(DiagonalMovingDirection currentDirection) : this()
        {
            this.currentDirection = currentDirection;
        }

        public DiagonalMoving()
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
