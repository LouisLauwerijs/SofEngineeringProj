using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projectSoftwareEngineering
{
    public class Spike : ICollidable, IGameObject
    {
        private Texture2D _texture;
        public Rectangle Bounds { get; set; }
        public bool IsSolid => false;

        public Spike(Texture2D texture, int x, int y, int width, int height)
        {
            _texture = texture;
            Bounds = new Rectangle(x, y, width, height);
        }

        public void Update(GameTime gameTime) { }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture, Bounds, Color.DarkRed);
        }
    }
}
