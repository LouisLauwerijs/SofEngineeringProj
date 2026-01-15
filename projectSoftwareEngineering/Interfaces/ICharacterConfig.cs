using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projectSoftwareEngineering.Interfaces
{
    public interface ICharacterConfig
    {
        Vector2 StartPosition { get; }
        float Gravity { get; }
        float JumpStrength { get;} 
        float MoveSpeed { get; }
    }
}
