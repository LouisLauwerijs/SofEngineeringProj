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
    /// <summary>
    /// Representeert de speler character in de game.
    /// Beheert player input, movement, animations, combat en collision detection.
    /// 
    /// SOLID Principes:
    /// - Single Responsibility Principle (SRP): Hoewel deze klasse veel doet, is de primaire
    ///   verantwoordelijkheid het coördineren van hero-specifieke gameplay elementen.
    ///   Delegates naar gespecialiseerde componenten (Physics, AnimationController, MeleeAttack).
    /// - Dependency Inversion Principle (DIP): Hangt af van interfaces (IInputChecker, 
    ///   ICharacterConfig) in plaats van concrete implementaties, wat testing en flexibility verbetert.
    /// - Open/Closed Principle (OCP): Nieuwe abilities kunnen toegevoegd worden door nieuwe
    ///   attack types toe te voegen zonder de kern hero logica te wijzigen.
    /// - Composition over Inheritance: Gebruikt compositie (Physics, AnimationController, MeleeAttack)
    ///   in plaats van diepe inheritance hiërarchieën.
    /// </summary>
    public class Hero : IGameObject, ICollidable, IDamageable, ITargetable
    {
        /// <summary>
        /// Texture voor de hero sprite sheet.
        /// </summary>
        private Texture2D _texture;

        /// <summary>
        /// Input handler die player input detecteert (keyboard, gamepad, etc.).
        /// Gebruikt interface voor flexibility en testability.
        /// </summary>
        private IInputChecker _inputHandler;

        /// <summary>
        /// Physics component die alle movement en gravity beheert.
        /// </summary>
        public Physics _physics;

        /// <summary>
        /// Animation controller die tussen verschillende animaties schakelt.
        /// </summary>
        private AnimationController _animationController;

        /// <summary>
        /// Collision manager voor het detecteren van botsingen met obstacles.
        /// </summary>
        private CollisionManager _collisionManager;

        /// <summary>
        /// Melee attack component die de hero's aanval beheert.
        /// </summary>
        private MeleeAttack _meleeAttack;

        /// <summary>
        /// Sprite effect voor het flippen van de hero sprite (links/rechts kijken).
        /// </summary>
        private SpriteEffects _direction = SpriteEffects.None;

        /// <summary>
        /// Timer voor knockback effect na damage.
        /// Tijdens knockback kan de speler niet bewegen.
        /// </summary>
        private float _knockbackTimer = 0;

        /// <summary>
        /// Geeft aan of de hero dood is.
        /// Blokkeert alle input en speelt death animatie af.
        /// </summary>
        public bool isDead { get; set; } = false;

        /// <summary>
        /// Health component die damage, healing en invulnerability beheert.
        /// </summary>
        public Health Health { get; set; }

        /// <summary>
        /// Collision bounds van de hero voor hitbox detectie.
        /// Gebruikt een offset van 18 pixels en kleinere bounds (28x32) dan de volledige sprite.
        /// </summary>
        public Rectangle Bounds => new Rectangle(
            (int)_physics.Position.X + 18,  // X offset voor nauwkeurigere hitbox
            (int)_physics.Position.Y + 18,  // Y offset
            28,  // Breedte van hitbox
            32   // Hoogte van hitbox
        );

        /// <summary>
        /// Geeft aan of de hero een solid object is voor collision.
        /// False betekent dat enemies er doorheen kunnen bewegen.
        /// </summary>
        public bool IsSolid => false;

        /// <summary>
        /// Constructor die alle hero componenten initialiseert.
        /// Gebruikt dependency injection voor texture, input handler en configuratie.
        /// </summary>
        /// <param name="texture">Sprite sheet texture voor de hero</param>
        /// <param name="inputHandler">Interface voor input detection</param>
        /// <param name="config">Configuratie met physics parameters</param>
        /// <param name="collisionManager">Manager voor collision detection</param>
        public Hero(Texture2D texture, IInputChecker inputHandler, ICharacterConfig config, CollisionManager collisionManager)
        {
            _texture = texture;
            _inputHandler = inputHandler;
            _collisionManager = collisionManager;

            // Initialiseer physics met configuratie waarden
            _physics = new Physics(
                config.StartPosition,
                config.Gravity,
                config.JumpStrength,
                config.MoveSpeed
            );

            // Maak hero animaties met factory
            _animationController = new AnimationController(AnimationFactory.CreateHeroAnimations());

            // Initialiseer melee attack met parameters
            _meleeAttack = new MeleeAttack(
                heroWidth: 28,           // Breedte van hero hitbox
                cooldownDuration: 0.5f,  // Half seconde tussen attacks
                attackDuration: 0.18f    // Attack hitbox actief voor 180ms
            );

            Health = new Health(3); // Hero start met 3 health
        }

        /// <summary>
        /// Hoofd update methode die alle hero logica en collision detection uitvoert.
        /// Moet elke frame aangeroepen worden.
        /// </summary>
        /// <param name="gameTime">GameTime voor delta timing</param>
        /// <param name="collidables">Lijst van objecten waarmee collision gecontroleerd wordt</param>
        public void Update(GameTime gameTime, List<ICollidable> collidables)
        {
            // Update knockback timer
            if (_knockbackTimer > 0)
            {
                _knockbackTimer -= (float)gameTime.ElapsedGameTime.TotalSeconds;
            }

            // Verwerk player input voor movement en attack
            HandleMovement();
            HandleAttack();

            // Pas gravity toe
            _physics.ApplyGravity();

            // Update verticale positie en check collisions
            _physics.UpdateVerticalPosition();
            _collisionManager.SolidCollisionCheck(_physics, collidables);
            _collisionManager.PlatformCollisionCheck(_physics, collidables);
            _collisionManager.SolidCollisionCheck(_physics, collidables);

            // Controleer of hero op de grond staat (alleen tijdens val)
            if (_physics.Velocity.Y >= 0)
            {
                _physics.IsGrounded = _collisionManager.IsStandingOnGroud(Bounds, collidables);
            }

            // Update animaties en componenten
            UpdateAnimation();
            _animationController.Update(gameTime);
            _meleeAttack.Update(gameTime);
            Health.VulnerableUpdate(gameTime);
        }

        /// <summary>
        /// Overload voor IGameObject interface compliance.
        /// Niet gebruikt omdat we collision info nodig hebben.
        /// </summary>
        public void Update(GameTime gameTime)
        { }

        /// <summary>
        /// Tekent de hero op het scherm met de huidige animatie frame.
        /// </summary>
        /// <param name="spriteBatch">SpriteBatch voor rendering</param>
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(
                _texture,
                _physics.Position,
                _animationController.GetCurrentFrameRectangle(),  // Huidige frame uit animatie
                Color.White,
                0f,                // Geen rotatie
                Vector2.Zero,      // Geen origin offset
                1f,                // Geen scaling
                _direction,        // Sprite flipping (links/rechts)
                0f                 // Layer depth
            );
        }

        /// <summary>
        /// Verwerkt alle movement input van de speler.
        /// Blokkeert input tijdens death of knockback.
        /// Update animaties op basis van movement state.
        /// </summary>
        private void HandleMovement()
        {
            // Geen input tijdens death of knockback
            if (isDead || _knockbackTimer > 0)
                return;

            bool isMoving = false;

            // Horizontale movement
            if (_inputHandler.IsMovingRight())
            {
                _physics.MoveHorizontal(1);           // Beweeg naar rechts
                _direction = SpriteEffects.None;      // Kijk naar rechts
                isMoving = true;
            }
            else if (_inputHandler.IsMovingLeft())
            {
                _physics.MoveHorizontal(-1);                    // Beweeg naar links
                _direction = SpriteEffects.FlipHorizontally;   // Kijk naar links (flip sprite)
                isMoving = true;
            }
            else
            {
                _physics.StopHorizontalMovement();  // Stop bij geen input
            }

            // Jump input
            if (_inputHandler.IsJumping())
            {
                _physics.Jump();  // Spring alleen als op de grond
            }

            // Update idle/run animatie op basis van movement
            if (!isMoving && _physics.IsGrounded)
            {
                _animationController.IdleAnimation();
            }
            else if (isMoving && _physics.IsGrounded)
            {
                _animationController.RunAnimation();
            }
        }

        /// <summary>
        /// Verwerkt attack input van de speler.
        /// Blokkeert attack tijdens death of knockback.
        /// </summary>
        private void HandleAttack()
        {
            // Geen attack tijdens death of knockback
            if (isDead || _knockbackTimer > 0)
                return;

            // Check attack input en cooldown
            if (_inputHandler.IsAttacking() && _meleeAttack.CanAttack)
            {
                // Bepaal richting op basis van sprite flip
                bool facingRight = _direction == SpriteEffects.None;

                // Voer attack uit op huidige positie
                _meleeAttack.Execute(new Vector2(Bounds.X, Bounds.Y), facingRight);
            }
        }

        /// <summary>
        /// Controleert of de hero's attack enemies raakt en past damage toe.
        /// Moet elke frame aangeroepen worden door de game manager.
        /// </summary>
        /// <param name="targets">Lijst van damageable targets (enemies)</param>
        public void CheckAttackCollisions(List<IDamageable> targets)
        {
            // Haal alle geraken targets op
            var hitTargets = _meleeAttack.CheckCollisions(targets);

            // Pas damage toe aan elk geraakt target
            foreach (var target in hitTargets)
            {
                target.Health.TakeDamage();
            }
        }

        /// <summary>
        /// Update de huidige animatie op basis van hero state.
        /// Prioriteit: Attack > Jump > Movement (run/idle).
        /// </summary>
        private void UpdateAnimation()
        {
            if (isDead)
                return;

            // Attack animatie heeft hoogste prioriteit
            if (_meleeAttack.IsActive)
            {
                _animationController.AttackAnimation();
                return;
            }

            // Jump animatie tijdens airtime
            if (!_physics.IsGrounded)
            {
                _animationController.JumpAnimation();
            }
            // Idle/Run animaties worden in HandleMovement() gezet
        }

        /// <summary>
        /// Triggert de death state en animatie.
        /// Blokkeert alle movement en speelt death animatie af.
        /// </summary>
        public void Die()
        {
            isDead = true;
            _animationController.DieAnimation();
            _physics.StopHorizontalMovement();
        }

        /// <summary>
        /// Past een knockback effect toe wanneer de hero damage neemt.
        /// Verplaatst de hero en blokkeert input voor een korte periode.
        /// </summary>
        /// <param name="direction">Richting van knockback (-1 voor links, 1 voor rechts)</param>
        public void ApplyKnockback(float direction)
        {
            _knockbackTimer = 0.5f;  // Half seconde knockback
            Console.WriteLine($"Knockback applied! Timer: {_knockbackTimer}");

            // Verplaats hero in de knockback richting
            _physics.Position += new Vector2(direction * 15, 0);
        }
    }
}