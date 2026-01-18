using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using projectSoftwareEngineering.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projectSoftwareEngineering.Environment
{
    public class Platform : IGameObject, ICollidable
    {
        private Texture2D texture;
        public Rectangle Bounds { get; set; }

        public bool IsSolid => true;

        public Platform(Texture2D _texture, int x, int y, int width, int height)
        {
            texture = _texture;
            Bounds = new Rectangle(x, y, width, height);
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            if (texture == null) return;

            int tileWidth = texture.Width;
            int tileHeight = texture.Height;

            for (int x = 0; x < Bounds.Width; x += tileWidth)
            {
                for (int y = 0; y < Bounds.Height; y += tileHeight)
                {
                    Rectangle sourceRect = new Rectangle(0, 0,
                        Math.Min(tileWidth, Bounds.Width - x),
                        Math.Min(tileHeight, Bounds.Height - y));

                    Rectangle destRect = new Rectangle(
                        Bounds.X + x,
                        Bounds.Y + y,
                        sourceRect.Width,
                        sourceRect.Height);

                    spriteBatch.Draw(texture, destRect, sourceRect, Color.White);
                }
            }
        }

        public void Update(GameTime gametime)
        {
        }
    }
}
