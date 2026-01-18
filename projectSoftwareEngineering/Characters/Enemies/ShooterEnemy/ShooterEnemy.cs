using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using projectSoftwareEngineering.Interfaces;
using projectSoftwareEngineering.Systems;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projectSoftwareEngineering.Characters.Enemies.ShooterEnemy
{
    public class ShooterEnemy : Enemy
    {
        public override int Width => 30;
        public override int Height => 30;

        private List<Projectile> _projectiles;
        private float _shootTimer;
        private float _shootInterval = 2.0f;
        private float _projectileSpeed = 3.0f;
        private float _projectileMaxDistance = 200f;

        private ITargetable _target;

        public ShooterEnemy(Texture2D texture, Vector2 startPosition, ITargetable target, int health = 3)
            : base(texture, new ShooterEnemyConfig(startPosition), health)
        {
            _projectiles = new List<Projectile>();
            _shootTimer = _shootInterval;
            _target = target;
        }
        public List<Projectile> GetProjectiles()
        {
            return _projectiles;
        }

        public override void Update(GameTime gametime, List<ICollidable> collidables, CollisionManager collisionManager)
        {
            if (Health.CurrentHealth <= 0)
                return;

            _physics.ApplyGravity();
            _physics.UpdateVerticalPosition();

            collisionManager.SolidCollisionCheck(_physics, collidables);
            collisionManager.PlatformCollisionCheck(_physics, collidables);

            if (_physics.Velocity.Y >= 0)
            {
                _physics.IsGrounded = collisionManager.IsStandingOnGroud(Bounds, collidables);
            }

            _shootTimer -= (float)gametime.ElapsedGameTime.TotalSeconds;
            if (_shootTimer <= 0)
            {
                Shoot();
                _shootTimer = _shootInterval;
            }

            foreach (var projectile in _projectiles.ToList())
            {
                projectile.Update(gametime);

                foreach (var collidable in collidables)
                {
                    if (collidable.IsSolid && projectile.IsActive && projectile.Bounds.Intersects(collidable.Bounds))
                    {
                        projectile.Deactivate();
                        break;
                    }
                }
            }

            _projectiles.RemoveAll(p => !p.IsActive);

            Health.VulnerableUpdate(gametime);
        }
        private void Shoot()
        {
            float direction = _target.Bounds.Center.X < Bounds.Center.X ? -1 : 1;

            _facingRight = direction > 0;

            Vector2 spawnPosition = new Vector2(
                Bounds.Center.X - 4,
                Bounds.Center.Y - 4
            );

            Projectile newProjectile = new Projectile(
                _texture, 
                spawnPosition,
                direction,
                _projectileSpeed,
                _projectileMaxDistance
            );

            _projectiles.Add(newProjectile);
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            if (Health.CurrentHealth <= 0)
                return;

            spriteBatch.Draw(_texture, Bounds, Color.Blue);

            foreach (var projectile in _projectiles)
            {
                projectile.Draw(spriteBatch);
            }
        }
        public override void Update(GameTime gametime)
        {
        }
    }
}
