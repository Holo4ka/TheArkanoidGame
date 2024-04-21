using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArkanoidGame
{
    public interface IMovingGameObject<T> where T : IMovingDirection
    {
        /// <summary>
        /// Указывает игровому объекту двигаться (изменить своё положение) в текущем направлении движения
        /// </summary>
        void MoveAtCurrentDirection();

        /// <summary>
        /// Может ли игровой объект продолжить движение в текущем направлении движения?
        /// </summary>
        /// <param name="lowerBoundX">левая граница игрового поля, по оси X</param>
        /// <param name="upperBoundX">правая граница игрового поля, по оси X</param>
        /// <param name="lowerBoundY">нижняя (начальная) граница игрового поля, по оси Y. для формы - это верхний край формы</param>
        /// <param name="upperBoundY">верхняя (конечная) граница игрового поля, по оси Y. для формы - это нижний край формы</param>
        /// <param name="upperBoundXDelta">погрешность для границы по оси X, в пикселях</param>
        /// <param name="upperBoundYDelta">погрешность для границы по оси Y, в пикселях</param>
        /// <returns>true, если текущий движущийся игровой объект может продолжать движение в текущем направлении движения, иначе false</returns>
        bool CanMoveAtCurrentDirection(int lowerBoundX, int upperBoundX, int lowerBoundY, int upperBoundY, int upperBoundXDelta, int upperBoundYDelta);

        /// <summary>
        /// Предписывает игровому объекту инициализировать произвольное направление для дальнейшего движения
        /// </summary>
        void InitRandomMovingDirection();

        /// <summary>
        /// Получить текущее направление движения игрового объекта
        /// </summary>
        /// <returns></returns>
        T GetMovingDirection();

        /// <summary>
        /// Установить новое направление движения игрового объекта
        /// </summary>
        /// <param name="movingDirection">новое направление для движения текущего движущегося игрового объекта</param>
        void SetMovingDirection(T movingDirection);

        /// <summary>
        /// Установить граничный объект, или "стену" для игрового объекта, при достижении которой произойдет
        /// некоторое негативное действие в игре (например, проигрыш игрока и завершение игры).
        /// </summary>
        /// <param name="failureWallConstraint">значение граничного объекта-стены</param>
        void SetWallFailureConstraint(WallPosition failureWallConstraint);

        /// <summary>
        /// Достиг ли игровой объект граничного значения (т.е. нижней стены,
        /// которая является признаком поражения для игрока)
        /// </summary>
        /// <returns></returns>
        bool ReachedWallFailureConstraint();

        /// <summary>
        /// Сбросить признак достижения стены, являющейся признаком поражения в игре
        /// </summary>
        void ResetReachedWallFailureConstraint();

        /// <summary>
        /// Установить скорость движения игрового объекта
        /// </summary>
        /// <param name="speed">новая скорость движения</param>
        void SetMovingSpeed(int speed);

        void InitRandomSafeDiagonalMovingDirection();

        /// <summary>
        /// Получить текущую скорость движения игрового объекта
        /// </summary>
        /// <returns>значение текущей скорости игрового объекта</returns>
        int GetMovingSpeed();


    }
}
