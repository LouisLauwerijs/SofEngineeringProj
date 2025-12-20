using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
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

        RenderTarget2D renderTarget;
        int virtualWidth = 300;  
        int virtualHeight = 160;   

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            _heroTexture = Content.Load<Texture2D>("characterSpritesheet");
            renderTarget = new RenderTarget2D(
                GraphicsDevice,
                virtualWidth,
                virtualHeight
            );

            var inputHandler = new KeyboardInputChecker();
            var heroConfig = new HeroConfig(); 
            _hero = new Hero(_heroTexture, inputHandler, heroConfig);
        }

        protected override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            _hero.Update(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.SetRenderTarget(renderTarget);
            GraphicsDevice.Clear(Color.Gray);

            _spriteBatch.Begin();
            _hero.Draw(_spriteBatch);
            _spriteBatch.End();

            GraphicsDevice.SetRenderTarget(null);

            _spriteBatch.Begin(samplerState: SamplerState.PointClamp);
            _spriteBatch.Draw(
                renderTarget,
                new Rectangle(0, 0, Window.ClientBounds.Width, Window.ClientBounds.Height),
                Color.White
            );
            _spriteBatch.End();

            base.Draw(gameTime);
        }

    }
}
