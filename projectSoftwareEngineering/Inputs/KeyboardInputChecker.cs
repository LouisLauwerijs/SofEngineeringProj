using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projectSoftwareEngineering.Inputs
{
    public class KeyboardInputChecker: IInputChecker
    {
        public bool IsMovingRight()
        {
            return Keyboard.GetState().IsKeyDown(Keys.D);
        }

        public bool IsMovingLeft()
        {
            return Keyboard.GetState().IsKeyDown(Keys.A);
        }

        public bool IsJumping()
        {
            return Keyboard.GetState().IsKeyDown(Keys.Space);
        }

        public bool IsAttacking()
        {
            return Keyboard.GetState().IsKeyDown(Keys.J);
        }
    }
}
