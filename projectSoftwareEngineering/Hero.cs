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
    public class Hero : IGameObject
    {
        private readonly Texture2D _texture;
        private readonly IInputChecker _inputHandler;
        private readonly Physics _physics;
        private readonly AnimationController _animationController;

        private SpriteEffects _direction = SpriteEffects.None;

        // Dependency Injection (Dependency Inversion Principle)
        public Hero(Texture2D texture, IInputChecker inputHandler, HeroConfig config)
        {
            _texture = texture;
            _inputHandler = inputHandler;

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

        public void Update(GameTime gameTime)
        {
            HandleMovement();
            UpdatePhysics();
            UpdateAnimation();
            _animationController.Update(gameTime);
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

            //Change animation based on state
            if (!isMoving && _physics.IsGrounded)
            {
                _animationController.IdleAnimation();
            }
            else if (isMoving && _physics.IsGrounded)
            {
                _animationController.RunAnimation();
            }
        }

        private void UpdatePhysics()
        {
            _physics.ApplyGravity();
            _physics.UpdateVerticalPosition();
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
