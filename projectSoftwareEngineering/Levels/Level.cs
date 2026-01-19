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
    public abstract class Level
    {
        protected GraphicsDevice _graphicsDevice;
        protected int _screenWidth;
        protected int _screenHeight;

        protected Texture2D _heroTexture;
        protected Texture2D _floorTexture;
        protected Texture2D _platformTexture;
        protected Texture2D _wallTexture;
        protected Texture2D _walkingEnemyWalkTexture;
        protected Texture2D _walkingEnemyDieTexture;
        protected Texture2D _jumpingEnemyJumpTexture;
        protected Texture2D _jumpingEnemyDieTexture;
        protected Texture2D _jumpingEnemyIdleTexture;
        protected Texture2D _shooterEnemyIdleTexture;
        protected Texture2D _shooterEnemyAttackTexture;
        protected Texture2D _shooterEnemyDieTexture;
        protected Texture2D _spikeTexture;
        protected Texture2D _coinTexture;

        public Hero Hero { get; set; }
        public Camera Camera { get; set; }
        public List<ICollidable> Collidables { get; set; } = new List<ICollidable>();
        public List<Enemy> Enemies { get; set; } = new List<Enemy>();
        public List<Spike> Spikes { get; set; } = new List<Spike>();
        public List<ICollectible> Collectibles { get; set; } = new List<ICollectible>();

        protected Level(LevelConfig config)
        {
            _graphicsDevice = config.GraphicsDevice;
            _screenWidth = config.RenderWidth;
            _screenHeight = config.RenderHeight;

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

        public void Load(CollisionManager collisionManager)
        {
            var inputHandler = new KeyboardInputChecker();
            var heroConfig = new HeroConfig();
            Hero = new Hero(_heroTexture, inputHandler, heroConfig, collisionManager);

            float zoomFactor = 3f;
            Camera = new Camera((int)(_screenWidth / zoomFactor));

            BuildLevel();
        }

        public abstract void BuildLevel();
    }
}
