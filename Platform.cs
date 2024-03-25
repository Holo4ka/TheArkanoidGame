using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheArkanoidGame
{
    class Platform : Figure
    {
        public int width { get; set; }
        public int height { get; set; }
        public Color color { get; set; }

        public Platform(int x, int y, Brush brush, Color color, Graphics g, int width, int height) : base(x, y, brush, color, g)
        {
            this.width = width;
            this.height = height;
        }

        public override Rectangle GetObjectRectangle()
        {
            return new Rectangle(position.X, position.Y, width, height);
        }

        public bool CanMoveToPoint(Point point, int boundsStartX, int boundsEndX, int rightBoundsDelta)
        {
            if (point == Point.Empty)
            {
                return false;
            }
            return point.X - (width / 2) >= boundsStartX && point.X + (width / 2) + rightBoundsDelta < boundsEndX;
        }

        public void SetPositionCenteredHorizontally(int initialX)
        {
            position.X = initialX - (width / 2);
        }

    }
}
