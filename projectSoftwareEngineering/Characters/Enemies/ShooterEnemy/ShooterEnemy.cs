using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using projectSoftwareEngineering.Animations;
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
        private bool _isShooting = false;
        private ITargetable _target;

        public ShooterEnemy(Dictionary<string, Texture2D> textures, Vector2 startPosition, ITargetable target, int health = 3)
            : base(textures, new ShooterEnemyConfig(startPosition), health,
                   AnimationFactory.CreateShooterEnemyAnimations())
        {
            _projectiles = new List<Projectile>();
            _shootTimer = _shootInterval;
            _target = target;
        }

        public ShooterEnemy(Texture2D idleTexture, Texture2D attackTexture, Texture2D dieTexture,
                           Vector2 startPosition, ITargetable target, int health = 3)
            : this(new Dictionary<string, Texture2D>
            {
                { "idle", idleTexture },
                { "attack", attackTexture },
                { "die", dieTexture }
            }, startPosition, target, health)
        {
        }

        protected override string GetCurrentTextureKey()
        {
            if (_isDying || Health.CurrentHealth <= 0)
            {
                return "die";
            }
            if (_isShooting)
            {
                return "attack";
            }
            return "idle";
        }
        public List<Projectile> GetProjectiles()
        {
            return _projectiles;
        }

        public override void Update(GameTime gametime, List<ICollidable> collidables, CollisionManager collisionManager)
        {
            if (Health.CurrentHealth <= 0)
            {
                if (!_isDying)
                {
                    _isDying = true;
                    _animationController.DieAnimation();
                }

                _deathTimer += (float)gametime.ElapsedGameTime.TotalSeconds;

                if (_deathTimer >= DEATH_DURATION)
                {
                    ReadyToRemove = true;
                }

                _animationController.Update(gametime);
                return;
            }

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
                _isShooting = true;
                _animationController.AttackAnimation();
                _shootTimer = _shootInterval;
            }
            else if (_shootTimer < _shootInterval - 0.5f)
            {
                _isShooting = false;
                _animationController.IdleAnimation();
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

            _animationController.Update(gametime);
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
                _textures.Values.First(),
                spawnPosition,
                direction,
                _projectileSpeed,
                _projectileMaxDistance
            );

            _projectiles.Add(newProjectile);
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            _direction = _facingRight ? SpriteEffects.FlipHorizontally : SpriteEffects.None;

            spriteBatch.Draw(
                GetCurrentTexture(),
                _physics.Position,
                _animationController.GetCurrentFrameRectangle(),
                Color.White,
                0f,
                Vector2.Zero,
                1f,
                _direction,
                0f
            );

            // Only draw projectiles if alive
            if (Health.CurrentHealth > 0)
            {
                foreach (var projectile in _projectiles)
                {
                    projectile.Draw(spriteBatch);
                }
            }
        }

        public override void Die()
        {
            _isDying = true;
            _animationController.DieAnimation();
        }
        public override void Update(GameTime gametime)
        {
        }
    }
}
