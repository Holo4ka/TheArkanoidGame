using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheArkanoidGame
{
    internal interface IMovingDirection
    {
        bool IsNotMoving(); // Проверка на движение

        //void InitRandomDirection(); // Инициация движения в произвольном направлении

        void InitRandomSafeDirection(); // Инициация движения в произвольном направлении, кроме движения вниз
    }
}
