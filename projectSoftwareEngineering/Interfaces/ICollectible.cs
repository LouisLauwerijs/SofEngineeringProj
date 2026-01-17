using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projectSoftwareEngineering.Interfaces
{
    public interface ICollectible
    {
        Rectangle Bounds { get; }
        bool IsCollected { get; }
        void Collect();
    }
}
