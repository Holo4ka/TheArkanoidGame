using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheArkanoidGame
{
    internal interface IMovingGameObject<T> where T : IMovingDirection
    {
        void MoveAtCurrentDirection();
        
        //bool CanMoveAtCurrentDirection(int lowerBoundX, int upperBoundX, int lowerBoundY, int upperBoundY, int upperBoundXDelta, int upperBoundYDelta);

        //void InitRandomMovingDirection(); // Инициализация движения в рандомном направлении

        T GetMovingDirection(); // Получить направление движения

        void SetMovingDirection(T movingDirection); // Установить направление движения

        void SetMovingSpeed(int speed); // Установить скорость

        int GetMovingSpeed(); // Получить скорость
    }
}
