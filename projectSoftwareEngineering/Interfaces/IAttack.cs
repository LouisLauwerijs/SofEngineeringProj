using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projectSoftwareEngineering.Interfaces
{
    public interface IAttack
    {
        bool IsActive { get; }
        bool CanAttack { get; }
        Rectangle Hitbox { get; }

        void Execute(Vector2 position, bool facingRight);
        void Update(GameTime gameTime);
        List<IDamageable> CheckCollisions(List<IDamageable> targets);
    }
}
