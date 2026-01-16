using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using projectSoftwareEngineering.Characters;
using projectSoftwareEngineering.Characters.Enemies;
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
        protected Texture2D _enemyTexture;
        protected Texture2D _spikeTexture;

        public Hero Hero { get; set; }
        public Camera Camera { get; set; }
        public List<ICollidable> Collidables { get; set; } = new List<ICollidable>();
        public List<Enemy> Enemies { get; set; } = new List<Enemy>();
        public List<Spike> Spikes { get; set; } = new List<Spike>();

        protected Level(LevelConfig config)
        {
            _graphicsDevice = config.GraphicsDevice;
            _screenWidth = config.VirtualWidth;
            _screenHeight = config.VirtualHeight;

            _heroTexture = config.HeroTexture;
            _floorTexture = config.FloorTexture;
            _platformTexture = config.PlatformTexture;
            _wallTexture = config.WallTexture;
            _enemyTexture = config.EnemyTexture;
            _spikeTexture = config.SpikeTexture;
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

        //Temp until sprites
        protected Texture2D CreateColoredTexture(Color color)
        {
            Texture2D texture = new Texture2D(_graphicsDevice, 1, 1);
            texture.SetData(new[] { color });
            return texture;
        }
    }
}
