using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheArkanoidGame
{
    public class Target : Figure /// Класс прямоугольников-мишеней
    {
        private int width, height;

        public Target(int x, int y, Brush brush, Color color, Graphics gr, int width, int height) : base(x, y, brush, color, gr)
        {
            this.width = width;
            this.height = height;
        }

        public override Rectangle GetObjectRectangle()
        {
            return new Rectangle(position.X, position.Y, width, height);
        }

    }
}
