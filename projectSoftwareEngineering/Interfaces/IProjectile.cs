using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projectSoftwareEngineering.Interfaces
{
    public interface IProjectile : ICollidable
    {
        bool IsActive { get; }
        void Update(GameTime gameTime);
        void Draw(SpriteBatch spriteBatch);
        void Deactivate();
    }
}
