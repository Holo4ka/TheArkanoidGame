using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace TheArkanoidGame1
{
    class Platform : Figure
    {
        public int width { get; set; }
        public int height { get; set; }
        public Color color { get; set; }

        public Platform(int x, int y, Brush brush, Color color, int width, int height) : base(x, y, brush, color)
        {
            this.width = width;
            this.height = height;
        }

        /*public override Rectangle GetObjectRectangle()
        {
            return new Rectangle(position.X, position.Y, width, height);
        }*/

        public bool CanMoveToPoint(int x, int y, int boundsStartX, int boundsEndX, int rightBoundsDelta)
        {
            
            return x - (width / 2) >= boundsStartX && x + (width / 2) + rightBoundsDelta < boundsEndX;
        }

        public void SetPositionCenteredHorizontally(int initialX)
        {
            this.x = initialX - (width / 2);
        }

    }
}
