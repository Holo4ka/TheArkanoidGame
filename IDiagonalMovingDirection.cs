using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheArkanoidGame
{
    internal interface IDiagonalMovingDirection : IMovingDirection
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
