using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using projectSoftwareEngineering.Animations;
using projectSoftwareEngineering.Interfaces;
using projectSoftwareEngineering.Systems;
using System;
using System.Collections.Generic;

namespace projectSoftwareEngineering.Characters.Enemies.WalkingEnemy
{
    public class WalkingEnemy: Enemy
    {
        public override int Width => 30;
        public override int Height => 30;

        private int _edgeDistance= 4;
        private bool _isDying = false;

        public WalkingEnemy(Dictionary<string, Texture2D> textures, Vector2 startPosition, int health = 1)
            : base(textures, new WalkingEnemyConfig(startPosition), health, AnimationFactory.CreateWalkingEnemyAnimations())
        {
        }
        public WalkingEnemy(Texture2D walkTexture, Texture2D dieTexture, Vector2 startPosition, int health = 1)
            : this(new Dictionary<string, Texture2D>
            {
                { "walk", walkTexture },
                { "die", dieTexture }
            }, startPosition, health)
        { 
        }
        protected override string GetCurrentTextureKey()
        {
            if (_isDying || Health.CurrentHealth <= 0)
            {
                return "die";
            }
            return "walk";
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            _direction = _facingRight ? SpriteEffects.None : SpriteEffects.FlipHorizontally;
            Vector2 drawPosition = new Vector2(
                _physics.Position.X,  
                _physics.Position.Y - 16
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
                Patrol(collidables, collisionManager);
                if (_animationController != null)
                {
                    _animationController.RunAnimation();
                }
            }

            if(_animationController != null)
            {
                _animationController.Update(gametime);
            }
            Health.VulnerableUpdate(gametime);
        }
        private void Patrol(List<ICollidable> collidables, CollisionManager collisionManager)
        {
            Vector2 oldPosition = _physics.Position;

            // Move in current direction
            float direction;
            if (_facingRight)
            {
                direction = 1;
            }
            else
            {
                direction = -1;
            }
            _physics.MoveHorizontal(direction);

            collisionManager.SolidCollisionCheck(_physics, collidables);

            if (_physics.Position.X == oldPosition.X && _physics.Velocity.X == 0)
            {
                _facingRight = !_facingRight;
                return;
            }

            //Check for edge
            if (EdgeCheck(collidables))
            {
                //Turn before dropping off
                _facingRight = !_facingRight;
                _physics.Position = oldPosition;
            }
        }
        private bool EdgeCheck(List<ICollidable> collidables)
        {
            //Determine direction
            int checkX;
            if (_facingRight)
            {
                checkX = Bounds.Right + _edgeDistance;
            }
            else
            {
                checkX = Bounds.Left - _edgeDistance;
            }

            //ground checking rectangle
            Rectangle edgeChecker = new Rectangle(
                checkX,
                Bounds.Bottom - 2,
                2,
                4
            );

            //actual ground checking
            foreach (var collidable in collidables)
            {
                if (collidable.IsSolid && edgeChecker.Intersects(collidable.Bounds))
                {
                    return false;
                }
            }

            return true;
        }
        public override void Die()
        {
            _isDying = true;
            _animationController.DieAnimation();
        }
        public override Rectangle Bounds => new Rectangle(
            (int)_physics.Position.X + 34,
            (int)_physics.Position.Y,
            32, 50
        );

        public override void Update(GameTime gametime)
        {
        }
    }
}
