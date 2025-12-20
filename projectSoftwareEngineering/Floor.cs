using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projectSoftwareEngineering
{
    public class Floor: IGameObject
    {
        private Texture2D texture;
        public Rectangle Bounds { get; private set; }
        public Floor(Texture2D tex, int x, int y, int w, int h)
        {
            texture = tex;
            Bounds = new Rectangle(x, y, w, h);
        }
        public void Update(GameTime gameTime) { }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, Bounds, Color.White);
        }
    }
}
