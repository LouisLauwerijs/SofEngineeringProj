using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using projectSoftwareEngineering.Background;
using projectSoftwareEngineering.Characters;
using projectSoftwareEngineering.Characters.Enemies;
using projectSoftwareEngineering.Characters.Hero;
using projectSoftwareEngineering.Environment;
using projectSoftwareEngineering.Inputs;
using projectSoftwareEngineering.Interfaces;
using projectSoftwareEngineering.Systems;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projectSoftwareEngineering.Levels
{
    /// <summary>
    /// Abstracte basis klasse voor alle levels in de game.
    /// Definieert gemeenschappelijke functionaliteit en verplichte implementaties voor levels.
    /// Beheert alle textures, game objects, en level initialization.
    /// 
    /// SOLID Principes:
    /// - Single Responsibility Principle (SRP): Verantwoordelijk voor het beheren van
    ///   level-wide resources (textures) en het coördineren van level loading.
    /// - Open/Closed Principle (OCP): Open voor uitbreiding via inheritance, gesloten
    ///   voor modificatie. Nieuwe levels kunnen deze klasse extenden zonder wijzigingen.
    /// - Template Method Pattern: Definieert de structuur voor level loading via Load(),
    ///   subclasses implementeren specifieke level layouts via BuildLevel().
    /// - Dependency Inversion Principle (DIP): Werkt met lijsten van interfaces
    ///   (ICollidable, ICollectible) in plaats van concrete types.
    /// </summary>
    public abstract class Level
    {
        #region Graphics and Screen Configuration

        /// <summary>
        /// Graphics device voor rendering operaties.
        /// </summary>
        protected GraphicsDevice _graphicsDevice;

        /// <summary>
        /// Breedte van het scherm in pixels.
        /// </summary>
        protected int _screenWidth;

        /// <summary>
        /// Hoogte van het scherm in pixels.
        /// </summary>
        protected int _screenHeight;

        #endregion

        #region Texture Assets

        // Hero
        protected Texture2D _heroTexture;

        // Environment
        protected Texture2D _floorTexture;
        protected Texture2D _platformTexture;
        protected Texture2D _wallTexture;

        // Walking Enemy
        protected Texture2D _walkingEnemyWalkTexture;
        protected Texture2D _walkingEnemyDieTexture;

        // Jumping Enemy
        protected Texture2D _jumpingEnemyJumpTexture;
        protected Texture2D _jumpingEnemyDieTexture;
        protected Texture2D _jumpingEnemyIdleTexture;

        // Shooter Enemy
        protected Texture2D _shooterEnemyIdleTexture;
        protected Texture2D _shooterEnemyAttackTexture;
        protected Texture2D _shooterEnemyDieTexture;

        // Environment Objects
        protected Texture2D _spikeTexture;
        protected Texture2D _coinTexture;

        #endregion

        #region Game Objects

        /// <summary>
        /// De speler character (Hero).
        /// Public voor toegang vanuit game manager.
        /// </summary>
        public Hero Hero { get; set; }

        /// <summary>
        /// Camera voor viewport management en scrolling.
        /// </summary>
        public Camera Camera { get; set; }

        /// <summary>
        /// Lijst van alle collidable objecten in het level (floors, platforms, walls).
        /// Gebruikt voor collision detection.
        /// </summary>
        public List<ICollidable> Collidables { get; set; } = new List<ICollidable>();

        /// <summary>
        /// Lijst van alle enemies in het level.
        /// </summary>
        public List<Enemy> Enemies { get; set; } = new List<Enemy>();

        /// <summary>
        /// Lijst van alle spikes (hazards) in het level.
        /// </summary>
        public List<Spike> Spikes { get; set; } = new List<Spike>();

        /// <summary>
        /// Lijst van alle collectibles (coins, powerups) in het level.
        /// </summary>
        public List<ICollectible> Collectibles { get; set; } = new List<ICollectible>();

        #endregion

        /// <summary>
        /// Protected constructor die alle textures initialiseert vanuit LevelConfig.
        /// Kan alleen aangeroepen worden door subclasses.
        /// </summary>
        /// <param name="config">Configuratie object met alle benodigde resources</param>
        protected Level(LevelConfig config)
        {
            // Graphics configuratie
            _graphicsDevice = config.GraphicsDevice;
            _screenWidth = config.RenderWidth;
            _screenHeight = config.RenderHeight;

            // Initialiseer alle texture references
            _heroTexture = config.HeroTexture;
            _floorTexture = config.FloorTexture;
            _platformTexture = config.PlatformTexture;
            _wallTexture = config.WallTexture;
            _walkingEnemyWalkTexture = config.WalkingEnemyWalkTexture;
            _walkingEnemyDieTexture = config.WalkingEnemyDieTexture;
            _jumpingEnemyJumpTexture = config.JumpingEnemyJumpTexture;
            _jumpingEnemyDieTexture = config.JumpingEnemyDieTexture;
            _jumpingEnemyIdleTexture = config.JumpingEnemyIdleTexture;
            _shooterEnemyIdleTexture = config.ShooterEnemyIdleTexture;
            _shooterEnemyAttackTexture = config.ShooterEnemyAttackTexture;
            _shooterEnemyDieTexture = config.ShooterEnemyDieTexture;
            _spikeTexture = config.SpikeTexture;
            _coinTexture = config.CoinTexture;
        }

        /// <summary>
        /// Template method die het level loading proces coördineert.
        /// Creëert de Hero, Camera en roept de abstracte BuildLevel() methode aan.
        /// Dit is de standaard flow die elk level volgt.
        /// </summary>
        /// <param name="collisionManager">Manager voor collision detection systeem</param>
        public void Load(CollisionManager collisionManager)
        {
            // Creëer input handler voor Hero
            var inputHandler = new KeyboardInputChecker();

            // Creëer Hero configuratie
            var heroConfig = new HeroConfig();

            // Initialiseer de Hero met alle dependencies
            Hero = new Hero(_heroTexture, inputHandler, heroConfig, collisionManager);

            // Creëer camera met zoom factor
            // Zoom factor van 3 betekent dat de wereld 3x vergroot wordt weergegeven
            float zoomFactor = 3f;
            Camera = new Camera((int)(_screenWidth / zoomFactor));

            // Roep de specifieke level layout implementatie aan
            // Elke subclass implementeert zijn eigen BuildLevel()
            BuildLevel();
        }

        /// <summary>
        /// Abstracte methode die door elke concrete level class geïmplementeerd moet worden.
        /// Verantwoordelijk voor het plaatsen van alle game objects in het level:
        /// - Floors en walls (terrain)
        /// - Platforms (voor verticale movement)
        /// - Enemies (verschillende types)
        /// - Collectibles (coins, powerups)
        /// - Hazards (spikes)
        /// </summary>
        public abstract void BuildLevel();
    }
}