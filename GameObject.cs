using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArkanoidGame
{
    public abstract class GameObject
    {
        public Point position;

        public delegate void MultipleGameObjectsInteractionDelegate(object sender, ICollection<GameObject> otherObjects);

        public event MultipleGameObjectsInteractionDelegate CollapsedWithOtherObjects;
        public event EventHandler InitIncrementNumberOfFailures;
        public event EventHandler InitPositiveGameAction;

        protected virtual void OnCollapsedWithOtherObjects(ICollection<GameObject> otherObjects)
        {
            CollapsedWithOtherObjects?.Invoke(this, otherObjects);
        }
        protected virtual void OnInitIncrementNumberOfFailures()
        {
            InitIncrementNumberOfFailures?.Invoke(this, new EventArgs());
        }
        protected virtual void OnInitPositiveGameAction()
        {
            InitPositiveGameAction?.Invoke(this, new EventArgs());
        }

        public string Title { get; set; }

        public Point Position
        {
            get
            {
                return position;
            }
        }

        protected GameObject(string title)
        {
            position = new Point();
            Title = title;
        }

        protected GameObject(string title, int width, int height)
        {
            position = new Point();
            Title = title;
        }

        public void SetPosition(int x, int y)
        {
            position.X = x;
            position.Y = y;
        }

        public abstract Rectangle GetObjectRectangle();
    }
}
