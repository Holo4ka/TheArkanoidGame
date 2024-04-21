using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArkanoidGame
{
    public class Platform : GameObject
    {
        public int Width { get; set; }

        /// <summary>
        /// Высота прямоугольного игрового объекта
        /// </summary>
        public int Height { get; set; }

        /// <summary>
        /// Цвет игрового объекта
        /// </summary>
        public Color Color { get; set; }

        public Platform(string title, int width, int height) : base(title)
        {
            Width = width;
            Height = height;
        }

        /// <summary>
        /// Получить объект прямоугольника, который описывает габариты/размеры текущего прямоугольного игрового объекта
        /// </summary>
        /// <returns></returns>
        public override Rectangle GetObjectRectangle()
        {
            return new Rectangle(position.X, position.Y, Width, Height);
        }

        /// <summary>
        /// Получить половину ширины текущего прямоугольного объекта
        /// </summary>
        /// <returns></returns>
        public int GetHalfWidth()
        {
            return Width / 2;
        }

        public bool CanMoveToPointWhenCentered(Point point, int boundsStartX, int boundsEndX, int rightBoundsDelta)
        {
            if (point == Point.Empty)
            {
                return false;
            }
            return point.X - GetHalfWidth() >= boundsStartX && point.X + GetHalfWidth() + rightBoundsDelta < boundsEndX;
        }

        public void SetPositionCenteredHorizontally(int initialX)
        {
            position.X = initialX - GetHalfWidth();
        }
    }
}
