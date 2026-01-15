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
    public class Floor: IGameObject, ICollidable
    {
        private Texture2D texture;
        public Rectangle Bounds { get; private set; }

        public bool IsSolid => true;

        public Floor(Texture2D tex, int x, int y, int width, int height)
        {
            texture = tex;
            Bounds = new Rectangle(x, y, width, height);
        }
        public void Update(GameTime gameTime) { }
        public void Draw(SpriteBatch spriteBatch)
        {
            int tilesX = (int)Math.Ceiling((float)Bounds.Width / texture.Width);
            int tilesY = (int)Math.Ceiling((float)Bounds.Height / texture.Height);

            for (int x = 0; x < tilesX; x++)
            {
                for (int y = 0; y < tilesY; y++)
                {
                    Rectangle rect = new Rectangle(
                        Bounds.X + x * texture.Width,
                        Bounds.Y + y * texture.Height,
                        texture.Width,
                        texture.Height
                    );

                    Rectangle sourceRect = new Rectangle(
                        0, 0,
                        Math.Min(texture.Width, Bounds.Width - x * texture.Width),
                        Math.Min(texture.Height, Bounds.Height - y * texture.Height)
                    );

                    spriteBatch.Draw(texture, rect, sourceRect, Color.White);
                }
            }
        }
    }
}
