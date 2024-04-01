using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace TheArkanoidGame1
{
    public abstract class Figure
    {
        public int x, y;
        public Color color;
        public Brush brush;
        //public Graphics gr;


        public Figure(int x, int y, Brush brush, Color color)
        {
            this.x = x;
            this.y = y;
            this.brush = brush;
            this.color = color;
        }

        /*public void SetPosition(int x, int y)
        {
            position.X = x;
            position.Y = y;
        }*/

        //public abstract Rectangle GetObjectRectangle(); ///Метод, возвращающий прямоугольник вокруг необходимого объекта на поле
    }
}
