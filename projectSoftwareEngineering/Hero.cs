using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using projectSoftwareEngineering.Animations;

namespace projectSoftwareEngineering
{
    public class Hero : IGameObject, ICollidable
    {
        private Texture2D _texture;
        private IInputChecker _inputHandler;
        private Physics _physics;
        private AnimationController _animationController;
        private CollisionManager _collisionManager;

        private SpriteEffects _direction = SpriteEffects.None;

        public Rectangle Bounds => new Rectangle(
            (int)_physics.Position.X+18,
            (int)_physics.Position.Y+18,
            28, 32
        );

        public bool IsSolid => false;

        public Hero(Texture2D texture, IInputChecker inputHandler, CharacterConfig config, CollisionManager collisionManager)
        {
            _texture = texture;
            _inputHandler = inputHandler;
            _collisionManager = collisionManager;

            // Create components with injected configuration
            _physics = new Physics(
                config.StartPosition,
                config.Gravity,
                config.JumpStrength,
                config.MoveSpeed,
                config.GroundLevel
            );

            _animationController = new AnimationController(AnimationFactory.CreateHeroAnimations());
        }

        public void Update(GameTime gameTime, List<ICollidable> platforms)
        {
            HandleMovement();

            _physics.ApplyGravity();

            _physics.UpdateVerticalPosition();
            _collisionManager.FloorCollisionCheck(_physics, platforms);

            _collisionManager.WallCollisionCheck(_physics, platforms);

            if (_physics.Velocity.Y >= 0) 
            {
                _physics.IsGrounded = _collisionManager.IsStandingOnGroud(Bounds, platforms);
            }

            UpdateAnimation();
            _animationController.Update(gameTime);
        }
        public void Update(GameTime gameTime)
        {
            //just here for the IGameObject property.
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            

            spriteBatch.Draw(
                _texture,
                _physics.Position,
                _animationController.GetCurrentFrameRectangle(),
                Color.White,
                0f,
                Vector2.Zero,
                1f,
                _direction,
                0f
            );
        }

        private void HandleMovement()
        {
            bool isMoving = false;

            if (_inputHandler.IsMovingRight())
            {
                _physics.MoveHorizontal(1);
                _direction = SpriteEffects.None;
                isMoving = true;
            }
            else if (_inputHandler.IsMovingLeft())
            {
                _physics.MoveHorizontal(-1);
                _direction = SpriteEffects.FlipHorizontally;
                isMoving = true;
            }
            else
            {
                _physics.StopHorizontalMovement();
            }

            if (_inputHandler.IsJumping())
            {
                _physics.Jump();
            }

            if (!isMoving && _physics.IsGrounded)
            {
                _animationController.IdleAnimation();
            }
            else if (isMoving && _physics.IsGrounded)
            {
                _animationController.RunAnimation();
            }
        }

        private void UpdateAnimation()
        {
            if (!_physics.IsGrounded)
            {
                _animationController.JumpAnimation();
            }
        }
    }
}
