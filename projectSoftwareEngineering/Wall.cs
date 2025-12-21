using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projectSoftwareEngineering
{
    public class Wall : IGameObject, ICollidable
    {
        private Texture2D _texture;
        public Rectangle Bounds { get; set; }

        public bool IsSolid => true;
        public bool IsOneWay => false;

        public Wall(Texture2D texture, int x, int y, int height, int width)
        {
            _texture = texture;
            Bounds = new Rectangle(x, y, width, height);
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture, Bounds, Color.White);
        }

        public void Update(GameTime gametime)
        {
        }
    }
}
