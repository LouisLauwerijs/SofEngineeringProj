using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using projectSoftwareEngineering.Background;
using projectSoftwareEngineering.Characters.Enemies;
using projectSoftwareEngineering.Characters.Enemies.JumpingEnemy;
using projectSoftwareEngineering.Characters.Enemies.ShooterEnemy;
using projectSoftwareEngineering.Characters.Enemies.WalkingEnemy;
using projectSoftwareEngineering.Characters.Hero;
using projectSoftwareEngineering.Environment;
using projectSoftwareEngineering.Inputs;
using projectSoftwareEngineering.Interfaces;
using projectSoftwareEngineering.Levels;
using projectSoftwareEngineering.Systems;
using projectSoftwareEngineering.UI;
using System.Collections.Generic;
using System.Linq;

namespace projectSoftwareEngineering
{
    public enum GameState
    {
        MainMenu,
        Playing,
        Paused,
        Dead,
        LevelComplete
    }
    

    public class Game1 : Game
    {
        private Texture2D _debugTexture; 
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        //Zoom
        RenderTarget2D _renderTarget;
        const int RENDER_WIDTH = 800;
        const int RENDER_HEIGHT = 450;

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
        private Texture2D _spikeTexture;
        private ProjectileManager _projectileManager;

        //menu
        private GameState _currentState = GameState.MainMenu;
        private MainMenu _mainMenu;
        private DeathScreen _deathScreen;
        private LevelCompleteScreen _levelCompleteScreen;
        private SpriteFont _titleFont;
        private SpriteFont _buttonFont;
        private MouseState _previousMouseState;

        //death
        private float _deathTimer = 0f;
        private const float DEATH_ANIMATION_DURATION = 1.4f;

        //collectibles
        private List<ICollectible> _collectibles;
        private Texture2D _coinTexture;

        //levels
        private LevelFactory _levelFactory;
        private Level _currentLevel;
        private int _currentLevelNr;
        private const int MAX_LEVEL = 2;

        //Background
        private BackgroundManager _backgroundManager;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            _graphics.IsFullScreen = true;
            _graphics.PreferredBackBufferWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
            _graphics.PreferredBackBufferHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;

            Window.IsBorderless = true;
            Window.Position = new Point(0, 0);

            _graphics.ApplyChanges();
        }

        protected override void Initialize()
        {
            _collidables = new List<ICollidable>();
            _collisionManager = new CollisionManager();

            _enemies = new List<Enemy>();
            _spikes = new List<Spike>();
            _collectibles = new List<ICollectible>();

            _projectileManager = new ProjectileManager();
            base.Initialize();
        }

        protected override void LoadContent()
        {
            //Backgrounds
            _backgroundManager = new BackgroundManager(RENDER_WIDTH, RENDER_HEIGHT);

            Texture2D bg1 = Content.Load<Texture2D>("bg1");
            Texture2D bg2 = Content.Load<Texture2D>("bg2");
            Texture2D bg3 = Content.Load<Texture2D>("bg3");
            Texture2D bg4 = Content.Load<Texture2D>("bg4");
            Texture2D bg5 = Content.Load<Texture2D>("bg5");
            Texture2D bg6 = Content.Load<Texture2D>("bg6");

            _backgroundManager.AddLayer(bg1, 0.1f);  
            _backgroundManager.AddLayer(bg2, 0.2f);
            _backgroundManager.AddLayer(bg3, 0.3f);
            _backgroundManager.AddLayer(bg4, 0.4f);
            _backgroundManager.AddLayer(bg5, 0.5f);
            _backgroundManager.AddLayer(bg6, 0.6f);



            _renderTarget = new RenderTarget2D(GraphicsDevice, RENDER_WIDTH, RENDER_HEIGHT);

            //Hero
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            _heroTexture = Content.Load<Texture2D>("characterSpritesheet");

            //Environment
            _floorTexture = Content.Load<Texture2D>("floorSprite");
            _coinTexture = Content.Load<Texture2D>("MonedaP");
            _platformTexture = Content.Load<Texture2D>("platform");
            _wallTexture = Content.Load<Texture2D>("wall");
            _spikeTexture = Content.Load<Texture2D>("Spike");

            //Fonts
            _titleFont = Content.Load<SpriteFont>("TitleFont");
            _buttonFont = Content.Load<SpriteFont>("ButtonFont");

            //Enemies
            Texture2D _walkingEnemyWalkTexture = Content.Load<Texture2D>("walkerWalk");
            Texture2D _walkingEnemyDieTexture = Content.Load<Texture2D>("WalkerDie");

            Texture2D _jumpingEnemyJumpTexture = Content.Load<Texture2D>("Jumper");
            Texture2D _jumpingEnemyDieTexture = Content.Load<Texture2D>("jumperdie");
            Texture2D _jumpingEnemyIdleTexture = Content.Load<Texture2D>("JumperIdle");

            Texture2D _shooterEnemyIdleTexture = Content.Load<Texture2D>("shooterIdle");
            Texture2D _shooterEnemyAttackTexture = Content.Load<Texture2D>("shooterAtt");
            Texture2D _shooterEnemyDieTexture = Content.Load<Texture2D>("shooterDie");


            Texture2D buttonTexture = CreateColoredTexture(Color.DarkGray);

            int screenWidth = GraphicsDevice.Viewport.Width;
            int screenHeight = GraphicsDevice.Viewport.Height;

            _mainMenu = new MainMenu(buttonTexture, _titleFont, _buttonFont, screenWidth, screenHeight);
            _deathScreen = new DeathScreen(buttonTexture, _titleFont, _buttonFont, screenWidth, screenHeight);
            _levelCompleteScreen = new LevelCompleteScreen(buttonTexture, _titleFont, _buttonFont, screenWidth, screenHeight);


            LevelConfig levelConfig = new LevelConfig(
                    GraphicsDevice,
                    RENDER_WIDTH,
                    RENDER_HEIGHT,
                    _heroTexture,
                    _floorTexture,
                    _platformTexture,
                    _wallTexture,
                    _walkingEnemyWalkTexture,
                    _walkingEnemyDieTexture,
                    _jumpingEnemyJumpTexture,
                    _jumpingEnemyDieTexture,
                    _jumpingEnemyIdleTexture,
                    _shooterEnemyIdleTexture,
                    _shooterEnemyAttackTexture,
                    _shooterEnemyDieTexture,
                    _spikeTexture,
                    _coinTexture
                );
            _levelFactory = new LevelFactory(levelConfig);
        }
        

        protected override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            MouseState currentMouseState = Mouse.GetState();
            if (_currentState == GameState.MainMenu)
            {
                int? selectedLevel = _mainMenu.Update(
                currentMouseState,
                _previousMouseState
            );

                if (selectedLevel.HasValue)
                {
                    LoadLevel(selectedLevel.Value);
                    _currentState = GameState.Playing;
                }
            }
            else if (_currentState == GameState.Playing)
            {
                _hero.Update(gameTime, _collidables);
                _camera.Follow(_hero.Bounds.Center.ToVector2());

                var damageableEnemies = _enemies.Cast<IDamageable>().ToList();
                _hero.CheckAttackCollisions(damageableEnemies);

                foreach (var collidable in _collidables)
                {
                    if (collidable is IGameObject gameObject)
                    {
                        gameObject.Update(gameTime);
                    }
                }

                foreach (var collectible in _collectibles)
                {
                    if (collectible is IGameObject gameObject)
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

                    if (enemy is ShooterEnemy shooterEnemy)
                    {
                        _projectileManager.HandleProjectileCollisions(
                            shooterEnemy.GetProjectiles(),
                            _hero,
                            _collidables
                        );
                    }

                    if (enemy.ReadyToRemove)
                    {
                        _enemies.Remove(enemy);
                    }
                }



                foreach (Spike spike in _spikes)
                {
                    if (spike.Bounds.Intersects(_hero.Bounds))
                    {
                        _hero.Health.TakeDamage();

                        if (_hero.Health.CurrentHealth > 0)
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

                foreach (ICollectible collectible in _collectibles.ToList())
                {
                    if (!collectible.IsCollected && collectible.Bounds.Intersects(_hero.Bounds))
                    {
                        collectible.Collect();
                        _currentState = GameState.LevelComplete;
                    }
                }

                if (_hero.Health.CurrentHealth <= 0 && !_hero.isDead)
                {
                    _hero.Die();
                    _deathTimer = 0f;
                }

                if (_hero.isDead)
                {
                    _deathTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;

                    if (_deathTimer >= DEATH_ANIMATION_DURATION)
                    {
                        _currentState = GameState.Dead;
                    }
                }
            }
            else if (_currentState == GameState.Dead)
            {
                string action = _deathScreen.Update(currentMouseState, _previousMouseState);

                if (action == "retry")
                {
                    LoadLevel(_currentLevelNr);
                    _currentState = GameState.Playing;
                }
                else if (action == "menu")
                {
                    _currentState = GameState.MainMenu;
                }
            }

            else if (_currentState == GameState.LevelComplete)
            {
                bool hasNextLevel = _currentLevelNr < MAX_LEVEL;
                string action = _levelCompleteScreen.Update(currentMouseState, _previousMouseState, hasNextLevel);

                if (action == "next" && hasNextLevel)
                {
                    LoadLevel(_currentLevelNr + 1);
                    _currentState = GameState.Playing;
                }
                else if (action == "menu")
                {
                    _currentState = GameState.MainMenu;
                }
            }

            _previousMouseState = currentMouseState;
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            if (_currentState == GameState.MainMenu)
            {
                GraphicsDevice.SetRenderTarget(null);
                GraphicsDevice.Clear(Color.Black);
                _spriteBatch.Begin(samplerState: SamplerState.PointClamp);
                _mainMenu.Draw(_spriteBatch);
                _spriteBatch.End();
            }

            else if (_currentState!=GameState.MainMenu)
            {
                GraphicsDevice.SetRenderTarget(_renderTarget);
                GraphicsDevice.Clear(Color.Gray);

                //Draw Background
                _spriteBatch.Begin(samplerState: SamplerState.PointClamp);
                _backgroundManager.Draw(_spriteBatch, _camera.Position);
                _spriteBatch.End();

                _spriteBatch.Begin(
                    samplerState: SamplerState.PointClamp,
                    transformMatrix: _camera.Transform
                );

                

                //Draw platforms and floors
                foreach (var collidable in _collidables)
                {
                    if (collidable is IGameObject gameObject)
                    {
                        gameObject.Draw(_spriteBatch);
                    }
                }

                //Draw enemies
                foreach (var enemy in _enemies)
                {
                    enemy.Draw(_spriteBatch);
                    
                }

                //Draw collectibles
                foreach (var collectible in _collectibles)
                {
                    if (collectible is IGameObject gameObject)
                    {
                        gameObject.Draw(_spriteBatch);
                    }
                }
                //Draw hero
                _hero.Draw(_spriteBatch);

                _spriteBatch.End();

                GraphicsDevice.SetRenderTarget(null);
                _spriteBatch.Begin(samplerState: SamplerState.PointClamp);
                _spriteBatch.Draw(
                    _renderTarget,
                    new Rectangle(0, 0, Window.ClientBounds.Width, Window.ClientBounds.Height),Color.White
                );

                DrawHealth();

                if (_currentState == GameState.Dead)
                {
                    _spriteBatch.Draw(
                        _debugTexture,
                        new Rectangle(0, 0, Window.ClientBounds.Width, Window.ClientBounds.Height), Color.Black * 0.7f
                    );
                    _deathScreen.Draw(_spriteBatch);
                }

                if (_currentState == GameState.LevelComplete)
                {
                    _spriteBatch.Draw(
                        _debugTexture,
                        new Rectangle(0, 0, Window.ClientBounds.Width, Window.ClientBounds.Height), Color.Black * 0.7f
                    );
                    _levelCompleteScreen.Draw(_spriteBatch);
                }
                _spriteBatch.End();
            }
            
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
        public void LoadLevel(int level)
        {
            _currentLevelNr = level;
            _currentLevel = _levelFactory.CreateLevel(level);
            _currentLevel.Load(_collisionManager);

            _hero = _currentLevel.Hero;
            _camera = _currentLevel.Camera;
            _collidables = _currentLevel.Collidables;
            _enemies = _currentLevel.Enemies;
            _spikes = _currentLevel.Spikes;
            _collectibles = _currentLevel.Collectibles;
        }
    }
}
