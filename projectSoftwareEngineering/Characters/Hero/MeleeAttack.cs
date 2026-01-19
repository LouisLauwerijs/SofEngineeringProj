using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using projectSoftwareEngineering.Animations;
using projectSoftwareEngineering.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projectSoftwareEngineering.Characters.Hero
{
    /// <summary>
    /// Beheert de melee (close-range) attack mechanisme voor de hero.
    /// Implementeert cooldown systeem, attack duration en collision detection met hitbox.
    /// 
    /// SOLID Principes:
    /// - Single Responsibility Principle (SRP): Enige verantwoordelijkheid is het beheren
    ///   van melee attack state, timing en collision detection.
    /// - Interface Segregation Principle (ISP): Implementeert IAttack interface die alleen
    ///   attack-gerelateerde methodes definieert.
    /// - Open/Closed Principle (OCP): Kan uitgebreid worden voor verschillende attack types
    ///   zonder de core logica te wijzigen.
    /// </summary>
    public class MeleeAttack : IAttack
    {
        #region Timing Variables

        /// <summary>
        /// Huidige tijd tot volgende attack mogelijk is (in seconden).
        /// Telt af naar 0, wanneer 0 bereikt is kan er weer aangevallen worden.
        /// </summary>
        private float _cooldownTimer;

        /// <summary>
        /// Totale cooldown duur tussen attacks (in seconden).
        /// Voorkomt spam van attacks.
        /// </summary>
        private float _cooldownDuration;

        /// <summary>
        /// Huidige tijd dat de attack actief is (in seconden).
        /// Telt af tijdens een actieve attack.
        /// </summary>
        private float _attackTimer;

        /// <summary>
        /// Totale duur dat een attack actief blijft (in seconden).
        /// Bepaalt hoe lang de hitbox actief is.
        /// </summary>
        private float _attackDuration;

        #endregion

        #region Hitbox Configuration

        /// <summary>
        /// Breedte van de hero character (voor hitbox positionering).
        /// </summary>
        private int _heroWidth;

        /// <summary>
        /// Positie waar de attack uitgevoerd wordt (hero's positie).
        /// </summary>
        private Vector2 _attackPosition;

        /// <summary>
        /// Richting waarin de attack uitgevoerd wordt.
        /// True = rechts, False = links.
        /// </summary>
        private bool _facingRight;

        /// <summary>
        /// Geeft aan of er momenteel een attack actief is.
        /// </summary>
        private bool _isAttacking;

        /// <summary>
        /// Breedte van de attack hitbox in pixels.
        /// 40 pixels geeft een redelijke melee range.
        /// </summary>
        private int _attackWidth = 40;

        /// <summary>
        /// Hoogte van de attack hitbox in pixels.
        /// 55 pixels dekt de hoogte van de meeste enemies.
        /// </summary>
        private int _attackHeight = 55;

        /// <summary>
        /// Horizontale offset van de hitbox ten opzichte van de hero.
        /// Negatieve waarde plaatst de hitbox gedeeltelijk over de hero heen.
        /// </summary>
        private int _attackOffsetX = -20;

        /// <summary>
        /// Verticale offset van de hitbox ten opzichte van de hero.
        /// Negatieve waarde verschuift de hitbox omhoog.
        /// </summary>
        private int _attackOffsetY = -15;

        #endregion

        /// <summary>
        /// HashSet die bijhoudt welke targets al geraakt zijn tijdens deze attack.
        /// Voorkomt dat dezelfde enemy meerdere keren geraakt wordt door één attack.
        /// </summary>
        private HashSet<IDamageable> _hitThisAttack;

        #region Public Properties

        /// <summary>
        /// Geeft aan of de attack momenteel actief is (hitbox is actief).
        /// </summary>
        public bool IsActive => _isAttacking;

        /// <summary>
        /// Geeft aan of een nieuwe attack gestart kan worden (cooldown is afgelopen).
        /// </summary>
        public bool CanAttack => _cooldownTimer <= 0;

        /// <summary>
        /// Berekent en retourneert de huidige attack hitbox rectangle.
        /// Positioneert de hitbox voor of achter de hero op basis van facing direction.
        /// Retourneert Rectangle.Empty als er geen actieve attack is.
        /// </summary>
        public Rectangle Hitbox
        {
            get
            {
                // Geen hitbox als attack niet actief is
                if (!_isAttacking) return Rectangle.Empty;

                // Bereken X positie op basis van richting
                int xPos = _facingRight
                    ? (int)_attackPosition.X + _heroWidth + _attackOffsetX  // Voor de hero (rechts)
                    : (int)_attackPosition.X - _attackWidth - _attackOffsetX; // Achter de hero (links)

                return new Rectangle(
                    xPos,
                    (int)_attackPosition.Y + _attackOffsetY,
                    _attackWidth,
                    _attackHeight
                );
            }
        }

        #endregion

        /// <summary>
        /// Constructor die een MeleeAttack object initialiseert met timing parameters.
        /// </summary>
        /// <param name="heroWidth">Breedte van de hero character voor hitbox positionering</param>
        /// <param name="cooldownDuration">Tijd tussen attacks in seconden (default: 0.5s)</param>
        /// <param name="attackDuration">Duur van een actieve attack in seconden (default: 0.18s)</param>
        public MeleeAttack(int heroWidth, float cooldownDuration = 0.5f, float attackDuration = 0.18f)
        {
            _heroWidth = heroWidth;
            _cooldownDuration = cooldownDuration;
            _attackDuration = attackDuration;
            _cooldownTimer = 0;
            _attackTimer = 0;
            _isAttacking = false;
            _hitThisAttack = new HashSet<IDamageable>();
        }

        /// <summary>
        /// Voert een melee attack uit op de gegeven positie en richting.
        /// Start de attack alleen als de cooldown afgelopen is.
        /// Reset de lijst van geraken targets.
        /// </summary>
        /// <param name="position">Positie van de hero waar de attack uitgevoerd wordt</param>
        /// <param name="facingRight">Richting waarin de hero kijkt (true = rechts, false = links)</param>
        public void Execute(Vector2 position, bool facingRight)
        {
            // Kan niet attacken tijdens cooldown
            if (!CanAttack)
                return;

            // Start de attack
            _isAttacking = true;
            _attackPosition = position;
            _facingRight = facingRight;

            // Start beide timers
            _cooldownTimer = _cooldownDuration;  // Start cooldown voor volgende attack
            _attackTimer = _attackDuration;       // Start attack duration timer

            // Reset de lijst van geraken targets voor deze nieuwe attack
            _hitThisAttack.Clear();
        }

        /// <summary>
        /// Update de attack timers. Moet elke frame aangeroepen worden.
        /// Telt cooldown en attack duration timers af.
        /// </summary>
        /// <param name="gameTime">GameTime voor delta timing</param>
        public void Update(GameTime gameTime)
        {
            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

            // Update cooldown timer (altijd actief tot 0)
            if (_cooldownTimer > 0)
            {
                _cooldownTimer -= deltaTime;
            }

            // Update attack duration timer (alleen tijdens actieve attack)
            if (_isAttacking)
            {
                _attackTimer -= deltaTime;

                // Beëindig attack wanneer de duration verstreken is
                if (_attackTimer <= 0)
                {
                    _isAttacking = false;
                }
            }
        }

        /// <summary>
        /// Controleert collision tussen de attack hitbox en een lijst van targets.
        /// Retourneert alle targets die geraakt zijn maar nog niet eerder geraakt waren.
        /// Voorkomt dat dezelfde target meerdere keren geraakt wordt tijdens één attack.
        /// </summary>
        /// <param name="targets">Lijst van mogelijke targets om te controleren</param>
        /// <returns>Lijst van nieuw geraken targets tijdens deze attack</returns>
        public List<IDamageable> CheckCollisions(List<IDamageable> targets)
        {
            // Geen collisions als attack niet actief is
            if (!_isAttacking) return new List<IDamageable>();

            var hitTargets = new List<IDamageable>();
            Rectangle attackHitbox = Hitbox;

            // Check elk target voor collision
            foreach (var target in targets)
            {
                // Skip als deze target al geraakt is tijdens deze attack
                if (_hitThisAttack.Contains(target)) continue;

                // Check of het target ook collidable is
                if (target is ICollidable collidable)
                {
                    // Check rectangle intersection
                    if (attackHitbox.Intersects(collidable.Bounds))
                    {
                        hitTargets.Add(target);
                        _hitThisAttack.Add(target);  // Markeer als geraakt
                    }
                }
            }

            return hitTargets;
        }
    }
}