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
    /// <summary>
    /// Concrete implementatie van een schietende vijand (Shooter Enemy).
    /// Deze enemy blijft stationary en vuurt periodiek projectielen af richting het target (Hero).
    /// Beheert zijn eigen lijst van actieve projectielen.
    /// 
    /// SOLID Principes:
    /// - Single Responsibility Principle (SRP): Verantwoordelijk voor shooter enemy specifiek gedrag
    ///   zoals shoot timing, projectile management en target tracking.
    /// - Liskov Substitution Principle (LSP): Kan overal gebruikt worden waar een Enemy verwacht wordt
    ///   zonder het programma te breken.
    /// - Open/Closed Principle (OCP): Extend de Enemy base class zonder deze te wijzigen.
    /// - Dependency Inversion Principle (DIP): Hangt af van ITargetable interface voor target
    ///   in plaats van concrete Hero klasse.
    /// </summary>
    public class ShooterEnemy : Enemy
    {
        /// <summary>
        /// Breedte van de shooter enemy sprite.
        /// </summary>
        public override int Width => 30;

        /// <summary>
        /// Hoogte van de shooter enemy sprite.
        /// </summary>
        public override int Height => 30;

        #region Projectile Management

        /// <summary>
        /// Lijst van alle actieve projectielen die deze enemy heeft afgevuurd.
        /// </summary>
        private List<Projectile> _projectiles;

        /// <summary>
        /// Timer die aftelt tot de volgende shoot actie.
        /// </summary>
        private float _shootTimer;

        /// <summary>
        /// Interval tussen shots in seconden.
        /// 2.0f betekent dat de enemy elke 2 seconden schiet.
        /// </summary>
        private float _shootInterval = 2.0f;

        /// <summary>
        /// Snelheid van projectielen in pixels per frame.
        /// 3.0f is snel genoeg om gevaarlijk te zijn maar niet onontwijkbaar.
        /// </summary>
        private float _projectileSpeed = 3.0f;

        /// <summary>
        /// Maximale afstand die een projectiel kan afleggen voordat het deactiveert.
        /// 200f pixels bereik geeft een goede balance tussen threat en fairness.
        /// </summary>
        private float _projectileMaxDistance = 200f;

        #endregion

        #region State Variables

        /// <summary>
        /// Geeft aan of de enemy momenteel bezig is met schieten.
        /// Gebruikt voor animatie selectie.
        /// </summary>
        private bool _isShooting = false;

        /// <summary>
        /// Referentie naar het target (meestal de Hero).
        /// Gebruikt voor aim berekening.
        /// </summary>
        private ITargetable _target;

        #endregion

        /// <summary>
        /// Constructor met dictionary van textures.
        /// Gebruikt door factory of test code.
        /// </summary>
        /// <param name="textures">Dictionary met textures: "idle", "attack", "die"</param>
        /// <param name="startPosition">Start positie in de game wereld</param>
        /// <param name="target">Het target om naar te schieten (meestal Hero)</param>
        /// <param name="health">Initiële health waarde (default: 3)</param>
        public ShooterEnemy(Dictionary<string, Texture2D> textures, Vector2 startPosition, ITargetable target, int health = 3)
            : base(textures, new ShooterEnemyConfig(startPosition), health,
                   AnimationFactory.CreateShooterEnemyAnimations())
        {
            _projectiles = new List<Projectile>();
            _shootTimer = _shootInterval;  // Start met volle timer
            _target = target;
        }

        /// <summary>
        /// Constructor met individuele textures.
        /// Convenience constructor die intern een dictionary maakt.
        /// </summary>
        /// <param name="idleTexture">Texture voor idle state</param>
        /// <param name="attackTexture">Texture voor attack/shooting state</param>
        /// <param name="dieTexture">Texture voor death state</param>
        /// <param name="startPosition">Start positie in de game wereld</param>
        /// <param name="target">Het target om naar te schieten (meestal Hero)</param>
        /// <param name="health">Initiële health waarde (default: 3)</param>
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

        /// <summary>
        /// Bepaalt welke texture key gebruikt moet worden op basis van de enemy state.
        /// Prioriteit: Die > Attack (shooting) > Idle.
        /// </summary>
        /// <returns>Key voor de _textures dictionary</returns>
        protected override string GetCurrentTextureKey()
        {
            // Death state heeft hoogste prioriteit
            if (_isDying || Health.CurrentHealth <= 0)
            {
                return "die";
            }

            // Attack texture tijdens schieten
            if (_isShooting)
            {
                return "attack";
            }

            // Default naar idle
            return "idle";
        }

        /// <summary>
        /// Publieke accessor voor de projectiles lijst.
        /// Gebruikt door game manager voor collision detection.
        /// </summary>
        /// <returns>Lijst van alle actieve projectielen van deze enemy</returns>
        public List<Projectile> GetProjectiles()
        {
            return _projectiles;
        }

        /// <summary>
        /// Hoofd update methode die alle shooter enemy logica afhandelt.
        /// Implementeert death handling, physics, shooting behavior en projectile management.
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

            #region Physics (Gravity only - no horizontal movement)

            // Shooter enemy beweegt niet horizontaal, alleen gravity voor grounding
            _physics.ApplyGravity();
            _physics.UpdateVerticalPosition();

            // Check collisions voor grounding
            collisionManager.SolidCollisionCheck(_physics, collidables);
            collisionManager.PlatformCollisionCheck(_physics, collidables);

            // Update grounded state (alleen tijdens val)
            if (_physics.Velocity.Y >= 0)
            {
                _physics.IsGrounded = collisionManager.IsStandingOnGroud(Bounds, collidables);
            }

            #endregion

            #region Shooting Logic

            // Tel shoot timer af
            _shootTimer -= (float)gametime.ElapsedGameTime.TotalSeconds;

            // Tijd om te schieten
            if (_shootTimer <= 0)
            {
                Shoot();
                _isShooting = true;
                _animationController.AttackAnimation();
                _shootTimer = _shootInterval;  // Reset timer voor volgende shot
            }
            // Return naar idle na 0.5 seconden van shooting
            else if (_shootTimer < _shootInterval - 0.5f)
            {
                _isShooting = false;
                _animationController.IdleAnimation();
            }

            #endregion

            #region Projectile Management

            // Update alle actieve projectielen
            foreach (var projectile in _projectiles.ToList())
            {
                projectile.Update(gametime);

                // Check collision van projectiles met solid objecten
                foreach (var collidable in collidables)
                {
                    if (collidable.IsSolid && projectile.IsActive && projectile.Bounds.Intersects(collidable.Bounds))
                    {
                        projectile.Deactivate();
                        break;
                    }
                }
            }

            // Verwijder inactieve projectielen uit de lijst
            _projectiles.RemoveAll(p => !p.IsActive);

            #endregion

            // Update animatie en health systemen
            _animationController.Update(gametime);
            Health.VulnerableUpdate(gametime);
        }

        /// <summary>
        /// Vuurt een nieuw projectiel af richting het target.
        /// Berekent richting op basis van relatieve positie van target.
        /// Spawnt projectiel in het midden van de enemy.
        /// </summary>
        private void Shoot()
        {
            // Bereken richting naar target
            // Als target links van enemy is, schiet naar links (-1)
            // Als target rechts van enemy is, schiet naar rechts (1)
            float direction = _target.Bounds.Center.X < Bounds.Center.X ? -1 : 1;

            // Update facing direction voor sprite flipping
            _facingRight = direction > 0;

            // Bereken spawn positie: center van enemy bounds
            // Trekt 4 pixels af om projectiel te centreren (8x8 projectiel)
            Vector2 spawnPosition = new Vector2(
                Bounds.Center.X - 4,
                Bounds.Center.Y - 4
            );

            // Creëer nieuw projectiel
            Projectile newProjectile = new Projectile(
                _textures.Values.First(),  // Gebruik eerste texture (kan verbeterd worden met dedicated projectile texture)
                spawnPosition,
                direction,
                _projectileSpeed,
                _projectileMaxDistance
            );

            // Voeg toe aan lijst van actieve projectielen
            _projectiles.Add(newProjectile);
        }

        /// <summary>
        /// Tekent de shooter enemy en al zijn actieve projectielen op het scherm.
        /// Past sprite flipping toe op basis van facing direction.
        /// </summary>
        /// <param name="spriteBatch">SpriteBatch voor rendering</param>
        public override void Draw(SpriteBatch spriteBatch)
        {
            // Flip sprite horizontaal op basis van facing direction
            _direction = _facingRight ? SpriteEffects.FlipHorizontally : SpriteEffects.None;

            // Teken de enemy
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

            // Teken alleen projectielen als de enemy nog leeft
            // Voorkomt rare visuals van projectielen die blijven tijdens death animatie
            if (Health.CurrentHealth > 0)
            {
                foreach (var projectile in _projectiles)
                {
                    projectile.Draw(spriteBatch);
                }
            }
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