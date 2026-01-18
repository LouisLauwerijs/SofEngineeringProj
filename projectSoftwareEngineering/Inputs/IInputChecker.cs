using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projectSoftwareEngineering.Inputs
{
    public interface IInputChecker
    {
        bool IsMovingRight();
        bool IsMovingLeft();
        bool IsJumping();
        bool IsAttacking();
    }
}
