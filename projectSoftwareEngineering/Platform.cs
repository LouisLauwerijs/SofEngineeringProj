using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projectSoftwareEngineering
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
            spriteBatch.Draw(texture, Bounds, Color.Black);
        }

        public void Update(GameTime gametime)
        {
        }
    }
}
