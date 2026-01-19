using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using projectSoftwareEngineering.Animations;
using projectSoftwareEngineering.Interfaces;
using projectSoftwareEngineering.Systems;
using System;
using System.Collections.Generic;

namespace projectSoftwareEngineering.Characters.Enemies.JumpingEnemy
{
    /// <summary>
    /// Concrete implementatie van een springende vijand (Jumping Enemy).
    /// Deze enemy beweegt door herhaaldelijk te springen en van richting te veranderen bij muur collision.
    /// Wacht een korte tijd op de grond voordat hij weer springt.
    /// 
    /// SOLID Principes:
    /// - Single Responsibility Principle (SRP): Verantwoordelijk voor jumping enemy specifiek gedrag
    ///   zoals jump timing, ground waiting en collision-based direction changes.
    /// - Liskov Substitution Principle (LSP): Kan overal gebruikt worden waar een Enemy verwacht wordt
    ///   zonder het programma te breken.
    /// - Open/Closed Principle (OCP): Extend de Enemy base class zonder deze te wijzigen.
    /// - Dependency Inversion Principle (DIP): Hangt af van interfaces (ICollidable) en abstracties
    ///   (CollisionManager) in plaats van concrete implementaties.
    /// </summary>
    public class JumpingEnemy : Enemy
    {
        /// <summary>
        /// Breedte van de jumping enemy sprite.
        /// </summary>
        public override int Width => 30;

        /// <summary>
        /// Hoogte van de jumping enemy sprite.
        /// </summary>
        public override int Height => 30;

        #region Ground Wait Logic

        /// <summary>
        /// Timer die bijhoudt hoe lang de enemy al op de grond staat.
        /// </summary>
        private float _groundWaitTimer = 0f;

        /// <summary>
        /// Duur die de enemy wacht op de grond voordat hij weer springt.
        /// 0.8 seconden geeft een goede balans tussen actie en voorspelbaarheid.
        /// </summary>
        private float _groundWaitDuration = 0.8f;

        /// <summary>
        /// Geeft aan of de enemy momenteel aan het wachten is op de grond.
        /// </summary>
        private bool _waiting = false;

        #endregion

        #region Death State

        /// <summary>
        /// Geeft aan of de enemy in de death state is.
        /// </summary>
        private bool _isDying = false;

        /// <summary>
        /// Timer voor het afspelen van de death animatie.
        /// </summary>
        private float _deathTimer = 0f;

        /// <summary>
        /// Totale duur van de death animatie voordat de enemy verwijderd wordt.
        /// </summary>
        private const float DEATH_DURATION = 1.0f;

        #endregion

        /// <summary>
        /// Constructor met dictionary van textures.
        /// Gebruikt door factory of test code.
        /// </summary>
        /// <param name="textures">Dictionary met textures: "jump", "die", "idle"</param>
        /// <param name="startPosition">Start positie in de game wereld</param>
        /// <param name="health">Initiële health waarde</param>
        public JumpingEnemy(Dictionary<string, Texture2D> textures, Vector2 startPosition, int health)
            : base(textures, new JumpingEnemyConfig(startPosition), health,
                   AnimationFactory.CreateJumpingEnemyAnimations())
        {
        }

        /// <summary>
        /// Constructor met individuele textures.
        /// Convenience constructor die intern een dictionary maakt.
        /// </summary>
        /// <param name="jumpTexture">Texture voor jump state</param>
        /// <param name="dieTexture">Texture voor death state</param>
        /// <param name="idleTexture">Texture voor idle/ground state</param>
        /// <param name="startPosition">Start positie in de game wereld</param>
        /// <param name="health">Initiële health waarde</param>
        public JumpingEnemy(Texture2D jumpTexture, Texture2D dieTexture, Texture2D idleTexture, Vector2 startPosition, int health)
            : this(new Dictionary<string, Texture2D>
            {
                { "jump", jumpTexture },
                { "die", dieTexture },
                { "idle", idleTexture }
            }, startPosition, health)
        {
        }

        /// <summary>
        /// Bepaalt welke texture key gebruikt moet worden op basis van de enemy state.
        /// Prioriteit: Die > Idle (grounded) > Jump (airborne).
        /// </summary>
        /// <returns>Key voor de _textures dictionary</returns>
        protected override string GetCurrentTextureKey()
        {
            // Death state heeft hoogste prioriteit
            if (_isDying || Health.CurrentHealth <= 0)
            {
                return "die";
            }

            // Idle texture wanneer op de grond
            if (_physics.IsGrounded)
            {
                return "idle";
            }

            // Jump texture tijdens airtime
            return "jump";
        }

        /// <summary>
        /// Tekent de jumping enemy op het scherm.
        /// Past sprite flipping toe op basis van facing direction.
        /// Voegt een Y offset toe voor visuele correctie.
        /// </summary>
        /// <param name="spriteBatch">SpriteBatch voor rendering</param>
        public override void Draw(SpriteBatch spriteBatch)
        {
            // Bepaal sprite flip direction
            _direction = _facingRight ? SpriteEffects.None : SpriteEffects.FlipHorizontally;

            // Pas Y offset toe voor betere visuele uitlijning
            Vector2 drawPosition = new Vector2(
                _physics.Position.X,
                _physics.Position.Y + 8  // 8 pixel offset omlaag
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

        /// <summary>
        /// Hoofd update methode die alle jumping enemy logica afhandelt.
        /// Implementeert death handling, physics, collision en jump behavior.
        /// </summary>
        /// <param name="gametime">GameTime voor timing</param>
        /// <param name="collidables">Lijst van collidable objecten voor collision detection</param>
        /// <param name="collisionManager">Manager voor collision detection logica</param>
        public override void Update(GameTime gametime, List<ICollidable> collidables, CollisionManager collisionManager)
        {
            #region Death Handling

            // Als dood, speel alleen death animatie af en wacht op verwijdering
            if (Health.CurrentHealth <= 0)
            {
                if (!_isDying)
                {
                    _isDying = true;
                    _animationController.DieAnimation();
                }

                // Update death timer
                _deathTimer += (float)gametime.ElapsedGameTime.TotalSeconds;

                // Markeer voor verwijdering na death animatie
                if (_deathTimer >= DEATH_DURATION)
                {
                    ReadyToRemove = true;
                }

                // Update alleen animatie tijdens death
                _animationController.Update(gametime);
                return;
            }

            #endregion

            #region Physics and Collision

            // Pas gravity toe en update verticale positie
            _physics.ApplyGravity();
            _physics.UpdateVerticalPosition();

            // Check collisions met solids en platforms
            collisionManager.SolidCollisionCheck(_physics, collidables);
            collisionManager.PlatformCollisionCheck(_physics, collidables);

            // Controleer of enemy op de grond staat (alleen tijdens val)
            if (_physics.Velocity.Y >= 0)
            {
                _physics.IsGrounded = collisionManager.IsStandingOnGroud(Bounds, collidables);
            }

            #endregion

            #region Ground Wait and Jump Logic

            if (_physics.IsGrounded)
            {
                // Speel idle animatie op de grond
                _animationController.IdleAnimation();

                if (!_waiting)
                {
                    // Start wait period wanneer landing op grond
                    _waiting = true;
                    _groundWaitTimer = _groundWaitDuration;
                    _physics.StopHorizontalMovement();
                }
                else
                {
                    // Tel wait timer af
                    _groundWaitTimer -= (float)gametime.ElapsedGameTime.TotalSeconds;

                    // Spring na wait period
                    if (_groundWaitTimer <= 0)
                    {
                        EnemyJump();
                        _waiting = false;
                    }
                }
            }

            #endregion

            #region Air Movement and Wall Detection

            else
            {
                // Speel jump animatie in de lucht
                _animationController.JumpAnimation();

                // Onthoud oude positie voor wall detection
                Vector2 oldPosition = _physics.Position;

                // Beweeg horizontaal in facing direction
                float direction = _facingRight ? 1 : -1;
                _physics.MoveHorizontal(direction);

                // Check voor wall collision
                collisionManager.SolidCollisionCheck(_physics, collidables);

                // Als positie niet veranderd is en velocity 0 is, zijn we tegen een muur gebotst
                if (_physics.Position.X == oldPosition.X && _physics.Velocity.X == 0)
                {
                    // Verander richting bij muur collision
                    _facingRight = !_facingRight;
                }
            }

            #endregion

            // Update animatie controller
            _animationController.Update(gametime);
        }

        /// <summary>
        /// Voert een sprong uit.
        /// Momenteel is de horizontale velocity gecanceld (waarde is Velocity.Y).
        /// Dit maakt dat de enemy alleen verticaal springt.
        /// </summary>
        public void EnemyJump()
        {
            _physics.Jump();

            // Bereken richting (momenteel niet gebruikt)
            float direction = _facingRight ? 1 : -1;

            // Velocity wordt gezet maar X component blijft Velocity.Y (mogelijk een bug?)
            // Dit betekent dat horizontale beweging tijdens sprong enkel via MoveHorizontal() gebeurt
            _physics.Velocity = new Vector2(
                _physics.Velocity.X,  // Behoudt huidige X velocity
                _physics.Velocity.Y   // Behoudt jump Y velocity (negatief)
            );
        }

        /// <summary>
        /// Triggert de death state en animatie.
        /// </summary>
        public override void Die()
        {
            _isDying = true;
            _animationController.DieAnimation();
        }

        /// <summary>
        /// Overload voor IGameObject interface compliance.
        /// Niet gebruikt omdat we collision info nodig hebben.
        /// </summary>
        public override void Update(GameTime gametime)
        {
        }
    }
}