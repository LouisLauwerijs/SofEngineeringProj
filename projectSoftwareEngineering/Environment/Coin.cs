using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using projectSoftwareEngineering.Animations;
using projectSoftwareEngineering.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projectSoftwareEngineering.Environment
{
    public class Coin: ICollidable, ICollectible, IGameObject
    {
        private Texture2D _texture;
        private Rectangle _bounds;
        private Animation _spinAnimation;

        public Rectangle Bounds => _bounds;
        public bool IsCollected { get; private set; }
        public bool IsSolid => false;

        public Coin(Texture2D texture, int x, int y, int width, int height)
        {
            _texture = texture;
            _bounds = new Rectangle(x, y, width, height);
            IsCollected = false;

            
            _spinAnimation = CreateCoinAnimation();
        }

        private Animation CreateCoinAnimation()
        {
            Animation animation = new Animation();
            animation.FrameInterval = 100;
            animation.Loop = true;

            for (int i = 0; i < 4; i++)
            {
                animation.AddFrame(new AnimationFrame(new Rectangle(i * 16, 0, 16, 16)));
            }

            return animation;
        }
        public void Collect()
        {
            IsCollected = true;
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            if (!IsCollected)
            {
                spriteBatch.Draw(_texture, _bounds, _spinAnimation.GetCurrentFrameRectangle(), Color.White);
            }
        }

        public void Update(GameTime gameTime)
        {
            if (!IsCollected)
            {
                _spinAnimation.Update(gameTime);
            }
        }
    }
}
