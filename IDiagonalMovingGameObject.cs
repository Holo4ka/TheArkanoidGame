using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ArkanoidGame.SimpleDiagonalMoving;

namespace ArkanoidGame
{
    public interface IDiagonalMovingGameObject : IMovingGameObject<IMovingDirection>
    {
        void MoveUpRight();
        void MoveUpLeft();
        void MoveDownLeft();
        void MoveDownRight();
        void InitRandomDiagonalMovingDirection();
        void InitRandomSafeDiagonalMovingDirection();
    }
}
