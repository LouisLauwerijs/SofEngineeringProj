using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace projectSoftwareEngineering
{
    public class WalkingEnemy: Enemy
    {
        public override int Width => 30;

        public override int Height => 30;

        private int _edgeDistance= 4;

        public WalkingEnemy(Texture2D texture, Vector2 startPosition, int health = 1)
            :base(texture, startPosition, health) 
        { 
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (Health.CurrentHealth <= 0) return;

            spriteBatch.Draw(_texture, Bounds, Color.Red);
        }

        

        public override void Update(GameTime gametime, List<ICollidable> collidables, CollisionManager collisionManager)
        {
            if (Health.CurrentHealth <= 0)
            {
                return;
            }

            _physics.ApplyGravity();

            _physics.UpdateVerticalPosition();

            collisionManager.SolidCollisionCheck(_physics, collidables);
            collisionManager.PlatformCollisionCheck(_physics, collidables);

            //CheckGround
            if (_physics.Velocity.Y >= 0)
            {
                _physics.IsGrounded = collisionManager.IsStandingOnGroud(Bounds, collidables);
            }

            if (_physics.IsGrounded)
            {
                Patrol(collidables, collisionManager);
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
        public override void Update(GameTime gametime)
        {
        }
    }
}
