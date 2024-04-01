using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheArkanoidGame1
{
    internal interface IHorizontalMoving : IMoving
    {
        bool IsMovingLeft();
        bool IsMovingRight();
    }
}
