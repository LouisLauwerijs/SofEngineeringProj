using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projectSoftwareEngineering
{
    public class HeroConfig
    {
        public Vector2 StartPosition { get; set; } = new Vector2(0, 85);
        public float Gravity { get; set; } = 0.4f;
        public float JumpStrength { get; set; } = -7.5f;
        public float MoveSpeed { get; set; } = 3f;
        public float GroundLevel { get; set; } = 85f;
    }
}
