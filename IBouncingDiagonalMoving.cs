using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheArkanoidGame
{
    internal interface IBouncingDiagonalMoving : IMovingGameObject<IDiagonalMovingDirection>
    {
        void MoveUpRight();
        void MoveUpLeft();
        void MoveDownLeft();
        void MoveDownRight();
        //void InitRandomDiagonalMovingDirection();
        void InitRandomSafeDiagonalMoving();
    }
}
