using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using projectSoftwareEngineering.Animations;
using projectSoftwareEngineering.Interfaces;
using projectSoftwareEngineering.Systems;
using System.Collections.Generic;
using System.Linq;

namespace projectSoftwareEngineering.Characters.Enemies
{
    /// <summary>
    /// Abstracte basis klasse voor alle enemy types in de game.
    /// Definieert gemeenschappelijke functionaliteit en verplichte implementaties voor enemies.
    /// 
    /// SOLID Principes:
    /// - Single Responsibility Principle (SRP): Beheert gemeenschappelijke enemy state
    ///   zoals textures, death state, physics en animations.
    /// - Open/Closed Principle (OCP): Open voor uitbreiding via inheritance, gesloten
    ///   voor modificatie. Nieuwe enemy types kunnen deze klasse extenden zonder wijzigingen.
    /// - Liskov Substitution Principle (LSP): Alle concrete enemy types kunnen gebruikt
    ///   worden waar een Enemy verwacht wordt zonder het gedrag te breken.
    /// - Dependency Inversion Principle (DIP): Hangt af van interfaces (IGameObject, 
    ///   ICollidable, IDamageable) in plaats van concrete implementaties.
    /// - Template Method Pattern: Definieert de structuur voor enemies, subclasses vullen
    ///   specifieke details in via abstracte methodes.
    /// </summary>
    public abstract class Enemy : IGameObject, ICollidable, IDamageable
    {
        /// <summary>
        /// Dictionary van textures voor verschillende enemy states (idle, attack, die, etc.).
        /// Key = state naam, Value = Texture2D voor die state.
        /// </summary>
        protected Dictionary<string, Texture2D> _textures;

        /// <summary>
        /// Geeft aan of de enemy bezig is met de death animatie.
        /// </summary>
        protected bool _isDying = false;

        /// <summary>
        /// Timer die bijhoudt hoe lang de death animatie al loopt.
        /// </summary>
        protected float _deathTimer = 0f;

        /// <summary>
        /// Totale duur van de death animatie voordat de enemy verwijderd kan worden.
        /// </summary>
        protected const float DEATH_DURATION = 1.0f;

        /// <summary>
        /// Geeft aan of de enemy klaar is om uit de game verwijderd te worden.
        /// Wordt true nadat de death animatie compleet is.
        /// </summary>
        public bool ReadyToRemove { get; protected set; } = false;

        /// <summary>
        /// Physics component die alle beweging en gravity beheert.
        /// </summary>
        public Physics _physics;

        /// <summary>
        /// Geeft aan of de enemy naar rechts kijkt (true) of links (false).
        /// Gebruikt voor sprite flipping.
        /// </summary>
        public bool _facingRight;

        /// <summary>
        /// Controller die animatie transities beheert.
        /// </summary>
        protected AnimationController _animationController;

        /// <summary>
        /// Sprite effect voor het flippen van de texture (horizontaal spiegelen).
        /// </summary>
        protected SpriteEffects _direction = SpriteEffects.None;

        /// <summary>
        /// Breedte van de enemy sprite. Moet gedefinieerd worden door subclasses.
        /// </summary>
        public abstract int Width { get; }

        /// <summary>
        /// Hoogte van de enemy sprite. Moet gedefinieerd worden door subclasses.
        /// </summary>
        public abstract int Height { get; }

        /// <summary>
        /// Collision bounds van de enemy voor hitbox detectie.
        /// Gebruikt een offset van 18 pixels en kleinere bounds (28x30) dan de volledige sprite
        /// voor nauwkeurigere collision detection.
        /// Virtual zodat subclasses dit kunnen overschrijven indien nodig.
        /// </summary>
        public virtual Rectangle Bounds => new Rectangle(
            (int)_physics.Position.X + 18,  // X offset
            (int)_physics.Position.Y + 18,  // Y offset
            28,  // Breedte van hitbox
            30   // Hoogte van hitbox
        );

        /// <summary>
        /// Health component die damage en healing beheert.
        /// </summary>
        public Health Health { get; set; }

        /// <summary>
        /// Geeft aan of de enemy een solid object is voor collision.
        /// False betekent dat andere entities er doorheen kunnen bewegen.
        /// </summary>
        public bool IsSolid => false;

        /// <summary>
        /// Constructor voor alle enemy types.
        /// Initialiseert gemeenschappelijke componenten zoals textures, physics, health en animations.
        /// </summary>
        /// <param name="textures">Dictionary met textures voor verschillende states</param>
        /// <param name="config">Configuratie object met physics parameters</param>
        /// <param name="health">Initiële health waarde</param>
        /// <param name="animationSet">Set van animaties voor deze enemy</param>
        protected Enemy(Dictionary<string, Texture2D> textures, ICharacterConfig config, int health, AnimationSet animationSet)
        {
            _textures = textures;
            _facingRight = true;

            // Initialiseer physics met config waarden
            _physics = new Physics(
                config.StartPosition,
                config.Gravity,
                config.JumpStrength,
                config.MoveSpeed
            );

            Health = new Health(health);
            _animationController = new AnimationController(animationSet);
        }

        /// <summary>
        /// Abstracte methode die de huidige texture key moet retourneren.
        /// Subclasses bepalen welke texture gebruikt wordt op basis van hun state.
        /// </summary>
        /// <returns>Key van de texture in de _textures dictionary</returns>
        protected abstract string GetCurrentTextureKey();

        /// <summary>
        /// Haalt de huidige texture op basis van de texture key.
        /// Gebruikt fallback naar de eerste beschikbare texture als de key niet bestaat.
        /// </summary>
        /// <returns>Texture2D voor de huidige enemy state</returns>
        protected Texture2D GetCurrentTexture()
        {
            string key = GetCurrentTextureKey();

            if (_textures.ContainsKey(key))
            {
                return _textures[key];
            }

            // Fallback naar eerste beschikbare texture
            return _textures.Values.First();
        }

        /// <summary>
        /// Virtual methode voor death handling.
        /// Kan overschreven worden door subclasses voor custom death behavior.
        /// </summary>
        public virtual void Die()
        {
        }

        /// <summary>
        /// Abstracte methode voor het tekenen van de enemy.
        /// Elke enemy type implementeert zijn eigen draw logica.
        /// </summary>
        /// <param name="spriteBatch">SpriteBatch voor rendering</param>
        public abstract void Draw(SpriteBatch spriteBatch);

        /// <summary>
        /// Abstracte methode voor basis update zonder collision.
        /// </summary>
        /// <param name="gametime">GameTime voor timing</param>
        public abstract void Update(GameTime gametime);

        /// <summary>
        /// Abstracte methode voor update met collision detection.
        /// Elke enemy type implementeert zijn eigen movement en collision behavior.
        /// </summary>
        /// <param name="gametime">GameTime voor timing</param>
        /// <param name="collidables">Lijst van objecten waarmee collision gecontroleerd moet worden</param>
        /// <param name="collisionManager">Manager voor collision detection logica</param>
        public abstract void Update(GameTime gametime, List<ICollidable> collidables, CollisionManager collisionManager);
    }
}