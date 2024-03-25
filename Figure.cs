using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheArkanoidGame
{
    public abstract class Figure
    {
        public int x, y;
        public Color color;
        public Brush brush;
        public Graphics gr;

        protected Point position;

        public Figure(int x, int y, Brush brush, Color color,  Graphics gr)
        {
            this.x = x;
            this.y = y;
            this.brush = brush;
            this.color = color;
            this.gr = gr;
        }

        public void SetPosition(int x, int y)
        {
            position.X = x;
            position.Y = y;
        }

        public abstract Rectangle GetObjectRectangle(); ///Метод, возвращающий прямоугольник вокруг необходимого объекта на поле
    }
}
