using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using projectSoftwareEngineering.Animations;
using projectSoftwareEngineering.Interfaces;
using projectSoftwareEngineering.Systems;
using System;
using System.Collections.Generic;

namespace projectSoftwareEngineering.Characters.Enemies.JumpingEnemy
{
    public class JumpingEnemy : Enemy
    {
        public override int Width => 30;
        public override int Height => 30;

        private float _groundWaitTimer = 0f;
        private float _groundWaitDuration = 0.8f;
        private bool _waiting = false;
        private bool _isDying = false;
        private float _deathTimer = 0f;
        private const float DEATH_DURATION = 1.0f;

        public JumpingEnemy(Dictionary<string, Texture2D> textures, Vector2 startPosition, int health) 
            : base(textures, new JumpingEnemyConfig(startPosition), health,
                   AnimationFactory.CreateJumpingEnemyAnimations())
        {
        }
        public JumpingEnemy(Texture2D jumpTexture, Texture2D dieTexture, Texture2D idleTexture, Vector2 startPosition, int health)
            : this(new Dictionary<string, Texture2D>
            {
                { "jump", jumpTexture },
                { "die", dieTexture },
                { "idle", idleTexture }
            }, startPosition, health)
        {
        }
        protected override string GetCurrentTextureKey()
        {
            if (_isDying || Health.CurrentHealth <= 0)
            {
                return "die";
            }

            if (_physics.IsGrounded)
            {
                return "idle";
            }

            return "jump";
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            _direction = _facingRight ? SpriteEffects.None : SpriteEffects.FlipHorizontally;
            Vector2 drawPosition = new Vector2(
                _physics.Position.X,
                _physics.Position.Y+8
            );
            spriteBatch.Draw(
                GetCurrentTexture(),
                drawPosition,
                _animationController.GetCurrentFrameRectangle(),
                Color.White,
                0f,
                Vector2.Zero,
                1f,
                _direction,
                0f
            );
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

            if (_physics.IsGrounded)
            {
                _animationController.IdleAnimation();
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
                _animationController.JumpAnimation();
                Vector2 oldPosition = _physics.Position;
                float direction = _facingRight ? 1 : -1;
                _physics.MoveHorizontal(direction);

                collisionManager.SolidCollisionCheck(_physics, collidables);

                if (_physics.Position.X == oldPosition.X && _physics.Velocity.X == 0)
                {
                    _facingRight = !_facingRight;
                }
            }

            _animationController.Update(gametime);
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
