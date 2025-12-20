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
        private Texture2D _debugTexture; // Add this field at the top with other textures

        // Add this method
        private void DrawRectangleOutline(SpriteBatch spriteBatch, Rectangle rect, Color color, int thickness = 2)
        {
            // Top
            spriteBatch.Draw(_debugTexture, new Rectangle(rect.X, rect.Y, rect.Width, thickness), color);
            // Bottom
            spriteBatch.Draw(_debugTexture, new Rectangle(rect.X, rect.Bottom - thickness, rect.Width, thickness), color);
            // Left
            spriteBatch.Draw(_debugTexture, new Rectangle(rect.X, rect.Y, thickness, rect.Height), color);
            // Right
            spriteBatch.Draw(_debugTexture, new Rectangle(rect.Right - thickness, rect.Y, thickness, rect.Height), color);
        }

        //--------------------------------

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

        RenderTarget2D _renderTarget;
        int virtualWidth = 500;
        int virtualHeight = 300;

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

            _debugTexture = CreateColoredTexture(Color.White);
            //--------------------------------------------

            _spriteBatch = new SpriteBatch(GraphicsDevice);
            _heroTexture = Content.Load<Texture2D>("characterSpritesheet");

            var inputHandler = new KeyboardInputChecker();
            var heroConfig = new CharacterConfig { StartPosition = new Vector2(100, 50) };
            _hero = new Hero(_heroTexture, inputHandler, heroConfig, _collisionManager);

            _renderTarget = new RenderTarget2D(
                GraphicsDevice,
                virtualWidth,
                virtualHeight
            );

            _floorTexture = CreateColoredTexture(Color.DarkGreen);
            _platformTexture = CreateColoredTexture(Color.Brown);

            int screenWidth = GraphicsDevice.Viewport.Width;
            int screenHeight = GraphicsDevice.Viewport.Height;

            // Create floor (bottom of screen)
            var floor = new Floor(_floorTexture, 0, virtualHeight-10, virtualWidth, 30);
            _collidables.Add(floor);

            // Create some platforms
            var platform1 = new Platform(_platformTexture, 80, virtualHeight - 80, 50, 15);
            _collidables.Add(platform1);

            var platform2 = new Platform(_platformTexture, 220, virtualHeight - 130, 50, 15);
            _collidables.Add(platform2);

            var platform3 = new Platform(_platformTexture, 360, virtualHeight - 180, 50, 15);
            _collidables.Add(platform3);

            // Create THIN walls on the sides
            var leftWall = new Floor(_floorTexture, 0, 0, 5, virtualHeight);
            _collidables.Add(leftWall);

            //var rightWall = new Floor(_floorTexture, screenWidth - 5, 0, 5, virtualHeight);
            //_collidables.Add(rightWall);
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
            GraphicsDevice.SetRenderTarget(_renderTarget);
            GraphicsDevice.Clear(Color.Gray);

            _spriteBatch.Begin();

            // Draw platforms and floors
            foreach (var collidable in _collidables)
            {
                if (collidable is IGameObject gameObject)
                {
                    gameObject.Draw(_spriteBatch);
                }

                DrawRectangleOutline(_spriteBatch, collidable.Bounds, Color.Red, 2); //---------
            }

            // Draw hero
            _hero.Draw(_spriteBatch);

            DrawRectangleOutline(_spriteBatch, _hero.Bounds, Color.Red, 2); //-------------------

            _spriteBatch.End();

            GraphicsDevice.SetRenderTarget(null);

            _spriteBatch.Begin(samplerState: SamplerState.PointClamp);
            _spriteBatch.Draw(
                _renderTarget,
                new Rectangle(0, 0, Window.ClientBounds.Width, Window.ClientBounds.Height),
                Color.White
            );
            _spriteBatch.End();

            base.Draw(gameTime);
        }

    }
}
