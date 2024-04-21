using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArkanoidGame
{
    public interface IBouncingDiagonalMovingGameObject : IDiagonalMovingGameObject
    {
        /// <summary>
        /// Выполнить действия для осуществления "отскока"
        /// </summary>
        void Bounce();

        /// <summary>
        /// Установить для данного игрового объекта другой объект <paramref name="gameObject"/>, который
        /// будет для него препятствием и вызовет "отскок" текущего объекта
        /// </summary>
        /// <param name="gameObject">другой игровой объект-препятствие, который вызовет эффект "отскока" для текущего объекта</param>
        void SetBounceFromObject(GameObject gameObject);

        /// <summary>
        /// Проверить - есть ли коллизия (пересечение или столкновение) текущего игрового объекта и другого объекта, который
        /// был установлен в качестве препятствия для текущего объекта.
        /// </summary>
        /// <param name="newX">позиция по оси X текущего объекта, для которой нужно выполнить проверку</param>
        /// <param name="newY">позиция по оси Y текущего объекта, для которой нужно выполнить проверку</param>
        /// <returns>true, если произошло столкновение/пересечение текущего объекта с объектом-препятствием</returns>
        bool IsCollisionWithBounceFromObject(int newX, int newY);


        /// <summary>
        /// Установить для данного игрового объекта список других объектов <paramref name="destroyingGameObjects"/>,
        /// которые будут уничтожаться при попадании по ним текущего объекта
        /// </summary>
        /// <param name="destroyingGameObjects"></param>
        void SetBounceFromDestroyingObjects(List<GameObject> destroyingGameObjects);

        /// <summary>
        /// Проверить - есть ли коллизия (пересечение или столкновение) текущего игрового объекта и других уничтожающихся объектов (блоки)
        /// </summary>
        /// <param name="newX">позиция по оси X текущего объекта, для которой нужно выполнить проверку</param>
        /// <param name="newY">позиция по оси Y текущего объекта, для которой нужно выполнить проверку</param>
        /// <returns></returns>
        bool IsCollisionWithDestroyingObjects(int newX, int newY);
    }
}
