using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheArkanoidGame
{
    internal interface IVerticalMovingDirection : IMovingDirection
    {
        bool IsMovingUp();
        bool IsMovingDown();
    }
}
