using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projectSoftwareEngineering
{
    public interface ICollidable
    {
        Rectangle Bounds { get; }
        bool IsSolid { get; }
    }
}
