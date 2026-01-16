using Microsoft.Xna.Framework;
using projectSoftwareEngineering.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projectSoftwareEngineering.Characters
{
    internal class HeroConfig : ICharacterConfig
    {
        public Vector2 StartPosition => new Vector2(50, 50);
        public float Gravity => 0.4f;
        public float JumpStrength => -9f;
        public float MoveSpeed => 3f;
    }
}
