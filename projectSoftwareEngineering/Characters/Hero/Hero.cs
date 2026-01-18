using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using projectSoftwareEngineering.Animations;
using projectSoftwareEngineering.Inputs;
using projectSoftwareEngineering.Interfaces;
using projectSoftwareEngineering.Systems;

namespace projectSoftwareEngineering.Characters.Hero
{
    public class Hero : IGameObject, ICollidable, IDamageable, ITargetable
    {
        private Texture2D _texture;
        private IInputChecker _inputHandler;
        public Physics _physics;
        private AnimationController _animationController;
        private CollisionManager _collisionManager;
        private MeleeAttack _meleeAttack;

        private SpriteEffects _direction = SpriteEffects.None;

        private float _knockbackTimer = 0;

        public bool isDead { get; set; } = false;
        public Health Health { get; set; }
        public Rectangle Bounds => new Rectangle(
            (int)_physics.Position.X+18,
            (int)_physics.Position.Y+18,
            28, 32
        );

        public bool IsSolid => false;

        public Hero(Texture2D texture, IInputChecker inputHandler, ICharacterConfig config, CollisionManager collisionManager)
        {
            _texture = texture;
            _inputHandler = inputHandler;
            _collisionManager = collisionManager;

            _physics = new Physics(
                config.StartPosition,
                config.Gravity,
                config.JumpStrength,
                config.MoveSpeed
            );

            _animationController = new AnimationController(AnimationFactory.CreateHeroAnimations());
            _meleeAttack = new MeleeAttack(heroWidth: 28, cooldownDuration: 0.5f, attackDuration: 0.18f);
            Health = new Health(3);
        }

        public void Update(GameTime gameTime, List<ICollidable> collidables)
        {
            if (_knockbackTimer > 0)
            {
                _knockbackTimer -= (float)gameTime.ElapsedGameTime.TotalSeconds;
            }
            HandleMovement();
            HandleAttack();
            _physics.ApplyGravity();

            _physics.UpdateVerticalPosition();
            _collisionManager.SolidCollisionCheck(_physics, collidables);
            _collisionManager.PlatformCollisionCheck(_physics, collidables);
            _collisionManager.SolidCollisionCheck(_physics, collidables);

            if (_physics.Velocity.Y >= 0) 
            {
                _physics.IsGrounded = _collisionManager.IsStandingOnGroud(Bounds, collidables);
            }

            UpdateAnimation();
            _animationController.Update(gameTime);
            _meleeAttack.Update(gameTime);
            Health.VulnerableUpdate(gameTime);
        }
        public void Update(GameTime gameTime)
        {}

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
            if (isDead || _knockbackTimer > 0)
                return;

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

        private void HandleAttack()
        {
            if (isDead || _knockbackTimer > 0)
                return;

            if(_inputHandler.IsAttacking() && _meleeAttack.CanAttack)
            {
                bool facingRight = _direction == SpriteEffects.None;
                _meleeAttack.Execute(new Vector2(Bounds.X, Bounds.Y), facingRight);
            }
        }
        public void CheckAttackCollisions(List<IDamageable> targets)
        {
            var hitTargets = _meleeAttack.CheckCollisions(targets);
            foreach (var target in hitTargets)
            {
                target.Health.TakeDamage();
            }
        }

        private void UpdateAnimation()
        {
            if (isDead)
                return;

            if (_meleeAttack.IsActive)
            {
                _animationController.AttackAnimation();
                return;
            }

            if (!_physics.IsGrounded)
            {
                _animationController.JumpAnimation();
            }
        }

        public void Die()
        {
            isDead = true;
            _animationController.DieAnimation();
            _physics.StopHorizontalMovement();
        }
        public void ApplyKnockback(float direction)
        {
            _knockbackTimer = 0.5f;
            Console.WriteLine($"Knockback applied! Timer: {_knockbackTimer}");
            _physics.Position += new Vector2(direction * 15, 0);
        }
    }
}
