using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArkanoidGame
{
    public interface IMovingDirection
    {
        bool IsMovingUpRight();
        bool IsMovingUpLeft();
        bool IsMovingDownRight();
        bool IsMovingDownLeft();
        bool IsNotMoving();

        void InitRandomDirection();
        void InitRandomSafeDirection();
        void ChangeDirectionToUpLeft();
        void ChangeDirectionToUpRight();
        void ChangeDirectionToDownLeft();
        void ChangeDirectionToDownRight();
    }
}
