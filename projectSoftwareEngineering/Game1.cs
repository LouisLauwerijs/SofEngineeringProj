using System.Net.Mime;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace projectSoftwareEngineering
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        //sprites
        private Texture2D _heroTexture;
        private Hero hero;

        RenderTarget2D renderTarget;
        int virtualWidth = 320;   // smaller render size
        int virtualHeight = 180;  // smaller render size


        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            base.Initialize();
            hero = new Hero(_heroTexture);
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
        }

        protected override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            hero.Update(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            // 1. Draw to the low-res render target
            GraphicsDevice.SetRenderTarget(renderTarget);
            GraphicsDevice.Clear(Color.Gray);

            _spriteBatch.Begin();
            hero.Draw(_spriteBatch);
            _spriteBatch.End();

            // 2. Switch back to the window
            GraphicsDevice.SetRenderTarget(null);

            // 3. Draw the low-res result stretched to the window
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
