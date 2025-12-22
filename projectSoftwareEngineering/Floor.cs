using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projectSoftwareEngineering
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
            spriteBatch.Draw(texture, Bounds, Color.White);
        }
    }
}
