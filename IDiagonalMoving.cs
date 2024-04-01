using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheArkanoidGame1
{
    internal interface IDiagonalMoving : IMoving
    {
        bool IsMovingUpRight();
        bool IsMovingUpLeft();
        bool IsMovingDownRight();
        bool IsMovingDownLeft();

        void ChangeDirectionToUpLeft();
        void ChangeDirectionToUpRight();
        void ChangeDirectionToDownLeft();
        void ChangeDirectionToDownRight();
    }
}
