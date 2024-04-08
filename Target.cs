using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace TheArkanoidGame1
{
    public class Target : Figure /// Класс прямоугольников-мишеней
    {
        private int width, height;

        /*protected override Geometry DefiningGeometry
        {
            get { return this.RenderedGeometry; }
        }*/

        public Target(int x, int y, Brush brush, Color color, int width, int height) : base(x, y, brush, color)
        {
            this.width = width;
            this.height = height;
        }

        /*public override Rectangle GetObjectRectangle()
        {
            return new Rectangle(position.X, position.Y, width, height);
        }*/

        public void Destroy() 
        {
            //TODO
        }

    }
}
