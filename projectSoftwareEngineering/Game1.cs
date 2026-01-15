using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using projectSoftwareEngineering.Characters;
using projectSoftwareEngineering.Characters.Enemies;
using projectSoftwareEngineering.Inputs;
using projectSoftwareEngineering.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace projectSoftwareEngineering
{
    public class Game1 : Game
    {
        private Texture2D _debugTexture; 

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

        //-------------------------------- debug 

        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        //sprites
        private Texture2D _heroTexture;
        private Hero _hero;

        //floors/platforms
        private Texture2D _floorTexture;
        private Texture2D _platformTexture;
        private Texture2D _wallTexture;

        //collisions
        private List<ICollidable> _collidables;
        private CollisionManager _collisionManager;

        //camera
        private Camera _camera;

        //enemies and spikes
        private List<Enemy> _enemies;
        private List<Spike> _spikes;
        private Texture2D _enemyTexture;
        private Texture2D _spikeTexture;

        //bigger sprite
        RenderTarget2D _renderTarget;
        int virtualWidth = 600;
        int virtualHeight = 360;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            _graphics.IsFullScreen = true;
            _graphics.PreferredBackBufferWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
            _graphics.PreferredBackBufferHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
            _graphics.ApplyChanges();
        }

        protected override void Initialize()
        {
            _collidables = new List<ICollidable>();
            _collisionManager = new CollisionManager();

            _enemies = new List<Enemy>();
            _spikes = new List<Spike>();
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _debugTexture = CreateColoredTexture(Color.White); //-------------- debug

            _spriteBatch = new SpriteBatch(GraphicsDevice);
            _heroTexture = Content.Load<Texture2D>("characterSpritesheet");
            _floorTexture = Content.Load<Texture2D>("floorSprite");
            //TODO: Add all textures for platforms, enemies, floors, spikes

            var inputHandler = new KeyboardInputChecker();
            var heroConfig = new HeroConfig();
            _hero = new Hero(_heroTexture, inputHandler, heroConfig, _collisionManager);

            _renderTarget = new RenderTarget2D(
                GraphicsDevice,
                virtualWidth,
                virtualHeight
            );

            // TEMPORARY PLATFORM CREATION

            _platformTexture = CreateColoredTexture(Color.Brown);
            _wallTexture = CreateColoredTexture(Color.Orange);

            _enemyTexture = CreateColoredTexture(Color.Red);
            _spikeTexture = CreateColoredTexture(Color.DarkRed);

            _camera = new Camera(virtualWidth);
            
            // Create floor 
            Floor floor = new Floor(_floorTexture, 0, virtualHeight-15, 1000, 30);
            _collidables.Add(floor);

            // platforms
            Platform platform1 = new Platform(_platformTexture, 80, virtualHeight - 80, 50, 7);
            _collidables.Add(platform1);

            Platform platform2 = new Platform(_platformTexture, 220, virtualHeight - 130, 50, 7);
            _collidables.Add(platform2);

            Platform platform3 = new Platform(_platformTexture, 360, virtualHeight - 180, 50, 7);
            _collidables.Add(platform3);

            // Left wall -> floors have the same logic as walls
            Floor leftWall = new Floor(_wallTexture, -155, 0, 160, virtualHeight);
            _collidables.Add(leftWall);

            //Right wall
            Floor RightWall = new Floor(_wallTexture, 1000, 0, 500, virtualHeight);
            _collidables.Add(RightWall);

            //enemy 1
            WalkingEnemy enemy1 = new WalkingEnemy(_enemyTexture, new Vector2(300, 100), 1); 
            _enemies.Add(enemy1);

            //enemyplatform test
            Platform testFloor = new Platform(_platformTexture, 300, 200, 80, 7);
            _collidables.Add(testFloor);

            //spike
            Spike spike = new Spike(_spikeTexture, 180, virtualHeight - 35, 20, 20);
            _spikes.Add(spike);
            _collidables.Add(spike);
        }
        

        protected override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            _hero.Update(gameTime, _collidables);
            _camera.Follow(_hero.Bounds.Center.ToVector2());

            foreach (var collidable in _collidables)
            {
                if (collidable is IGameObject gameObject)
                {
                    gameObject.Update(gameTime);
                }
            }

            foreach (Enemy enemy in _enemies.ToList())
            {
                enemy.Update(gameTime, _collidables, _collisionManager);

                if (enemy.Health.CurrentHealth > 0 && enemy.Bounds.Intersects(_hero.Bounds))
                {
                    _hero.Health.TakeDamage();

                    if (_hero.Health.CurrentHealth > 0) 
                    {
                        if (_hero.Bounds.Center.X > enemy.Bounds.Center.X)
                        {
                            _hero.ApplyKnockback(3); 
                        }
                        else 
                        {
                            _hero.ApplyKnockback(-3); 
                        }
                    }
                }

                // Remove dead enemies
                if (enemy.Health.CurrentHealth <= 0)
                {
                    enemy.Die();
                    _enemies.Remove(enemy);
                }
            }

            foreach (Spike spike in _spikes)
            {
                if (spike.Bounds.Intersects(_hero.Bounds))
                {
                    _hero.Health.TakeDamage();

                    if (_hero.Health.CurrentHealth > -1)
                    {
                        if (_hero.Bounds.Center.X > spike.Bounds.Center.X)
                        {
                            _hero.ApplyKnockback(3);
                        }
                        else
                        {
                            _hero.ApplyKnockback(-3);
                        }
                    }
                }
            }

            if (_hero.Health.CurrentHealth <= 0)
            {
                _hero.Die();
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.SetRenderTarget(_renderTarget);
            GraphicsDevice.Clear(Color.Gray);

            _spriteBatch.Begin(transformMatrix: _camera.Transform);

            //Draw platforms and floors
            foreach (var collidable in _collidables)
            {
                if (collidable is IGameObject gameObject)
                {
                    gameObject.Draw(_spriteBatch);
                }

                DrawRectangleOutline(_spriteBatch, collidable.Bounds, Color.Red, 1); //--------- debug
            }

            //Draw enemies
            foreach (var enemy in _enemies)
            {
                enemy.Draw(_spriteBatch);
                DrawRectangleOutline(_spriteBatch, enemy.Bounds, Color.Yellow, 1); // debug
            }

            //Draw hero
            _hero.Draw(_spriteBatch);

            DrawRectangleOutline(_spriteBatch, _hero.Bounds, Color.Red, 1); //------------------- debug

            _spriteBatch.End();

            GraphicsDevice.SetRenderTarget(null);

            _spriteBatch.Begin(samplerState: SamplerState.PointClamp);
            _spriteBatch.Draw(
                _renderTarget,
                new Rectangle(0, 0, Window.ClientBounds.Width, Window.ClientBounds.Height),
                Color.White
            );

            DrawHealth();
            _spriteBatch.End();

            base.Draw(gameTime);
        }
        private Texture2D CreateColoredTexture(Color color)
        {
            Texture2D texture = new Texture2D(GraphicsDevice, 1, 1);
            texture.SetData(new[] { color });
            return texture;
        }
        public void DrawHealth()
        {
            for (int i = 0; i < _hero.Health.MaxHealth; i++)
            {
                Color heartColor = i < _hero.Health.CurrentHealth ? Color.Red : Color.DarkGray;

                Rectangle heartRectangle = new Rectangle(
                    20 + (i * 40),
                    20,
                    30,
                    30
                );

                _spriteBatch.Draw(_debugTexture, heartRectangle, heartColor);
            }
        }
    }
}
