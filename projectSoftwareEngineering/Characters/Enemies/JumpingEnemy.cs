using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using projectSoftwareEngineering.Interfaces;
using projectSoftwareEngineering.Systems;
using System;
using System.Collections.Generic;

namespace projectSoftwareEngineering.Characters.Enemies
{
    public class JumpingEnemy : Enemy
    {
        public override int Width => 30;
        public override int Height => 30;

        private float _groundWaitTimer = 0f;
        private float _groundWaitDuration = 0.8f;
        private bool _waiting = false;

        public JumpingEnemy(Texture2D texture, Vector2 startPosition, int health) 
            : base(texture, new JumpingEnemyConfig(startPosition), health)
        {
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (Health.CurrentHealth <= 0)
                return;

            spriteBatch.Draw(_texture, Bounds, Color.Purple);
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

            if (_physics.IsGrounded)
            {
                if (!_waiting)
                {
                    _waiting = true;
                    _groundWaitTimer = _groundWaitDuration;
                    _physics.StopHorizontalMovement();
                }
                else
                { 
                    _groundWaitTimer -= (float)gametime.ElapsedGameTime.TotalSeconds; 
                     if(_groundWaitTimer <= 0)
                     {
                        EnemyJump();
                        _waiting = false;
                     }
                }
            }
            else
            {
                Vector2 oldPosition = _physics.Position;
                float direction = _facingRight ? 1 : -1;
                _physics.MoveHorizontal(direction);

                collisionManager.SolidCollisionCheck(_physics, collidables);

                if (_physics.Position.X == oldPosition.X && _physics.Velocity.X == 0)
                {
                    _facingRight = !_facingRight;
                }
            }
        }

        public void EnemyJump()
        {
            _physics.Jump();

            float direction = _facingRight ? 1 : -1;
            _physics.Velocity = new Vector2(
                _physics.Velocity.X,
                _physics.Velocity.Y 
            );
        }

        public override void Update(GameTime gametime)
        {
        }
    }
}
