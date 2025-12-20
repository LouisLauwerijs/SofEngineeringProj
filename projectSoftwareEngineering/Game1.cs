using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.Net.Mime;
using static System.Formats.Asn1.AsnWriter;
using static System.Net.Mime.MediaTypeNames;

namespace projectSoftwareEngineering
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        //sprites
        private Texture2D _heroTexture;
        private Hero _hero;

        //floors/platforms
        private Texture2D _floorTexture;
        private Texture2D _platformTexture;

        //collisions
        private List<ICollidable> _collidables;
        private CollisionManager _collisionManager;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            _collidables = new List<ICollidable>();
            _collisionManager = new CollisionManager();
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            _heroTexture = Content.Load<Texture2D>("characterSpritesheet");

            var inputHandler = new KeyboardInputChecker();
            var heroConfig = new CharacterConfig { StartPosition = new Vector2(50, 50) };
            _hero = new Hero(_heroTexture, inputHandler, heroConfig, _collisionManager);

            _floorTexture = CreateColoredTexture(Color.DarkGreen);
            _platformTexture = CreateColoredTexture(Color.Brown);

            // Create floor (bottom of screen)
            var floor = new Floor(_floorTexture, 0, 150, 800, 30);
            _collidables.Add(floor);

            // Create some platforms
            var platform1 = new Platform(_platformTexture, 80, 120, 60, 10);
            _collidables.Add(platform1);

            var platform2 = new Platform(_platformTexture, 160, 100, 60, 10);
            _collidables.Add(platform2);

            var platform3 = new Platform(_platformTexture, 240, 80, 60, 10);
            _collidables.Add(platform3);

            // Create walls
            var leftWall = new Floor(_floorTexture, 0, 0, 10, 480);
            _collidables.Add(leftWall);

            var rightWall = new Floor(_floorTexture, 800 - 10, 0, 10, 480);
            _collidables.Add(rightWall);
        }
        private Texture2D CreateColoredTexture(Color color)
        {
            Texture2D texture = new Texture2D(GraphicsDevice, 1, 1);
            texture.SetData(new[] { color });
            return texture;
        }

        protected override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            _hero.Update(gameTime, _collidables);

            foreach (var collidable in _collidables)
            {
                if (collidable is IGameObject gameObject)
                {
                    gameObject.Update(gameTime);
                }
            }
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Gray);

            _spriteBatch.Begin();

            // Draw platforms and floors
            foreach (var collidable in _collidables)
            {
                if (collidable is IGameObject gameObject)
                {
                    gameObject.Draw(_spriteBatch);
                }
            }

            // Draw hero
            _hero.Draw(_spriteBatch);

            _spriteBatch.End();

            GraphicsDevice.SetRenderTarget(null);

            base.Draw(gameTime);
        }

    }
}
