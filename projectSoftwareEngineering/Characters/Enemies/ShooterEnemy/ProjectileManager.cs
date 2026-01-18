using projectSoftwareEngineering.Interfaces;
using HeroClass = projectSoftwareEngineering.Characters.Hero.Hero;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projectSoftwareEngineering.Characters.Enemies.ShooterEnemy
{
    public class ProjectileManager
    {
        public void HandleProjectileCollisions(List<Projectile> projectiles, IDamageable target, List<ICollidable> solidObjects)
        {
            foreach (var projectile in projectiles.ToList())
            {
                if (!projectile.IsActive) continue;

                if (target is ICollidable collidableTarget)
                {
                    if (projectile.Bounds.Intersects(collidableTarget.Bounds))
                    {
                        target.Health.TakeDamage();

                        if (target is HeroClass hero)
                        {
                            float knockbackDirection = projectile.Bounds.Center.X < hero.Bounds.Center.X ? 1 : -1;
                            hero.ApplyKnockback(knockbackDirection);
                        }

                        projectile.Deactivate();
                        continue;
                    }
                }

                foreach (var solid in solidObjects)
                {
                    if (solid.IsSolid && projectile.Bounds.Intersects(solid.Bounds))
                    {
                        projectile.Deactivate();
                        break;
                    }
                }
            }
        }
    }
}
