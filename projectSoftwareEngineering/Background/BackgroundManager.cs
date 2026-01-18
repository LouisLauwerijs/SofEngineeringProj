using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projectSoftwareEngineering.Background
{
    public class BackgroundManager
    {
        private List<BackgroundLayer> _layers;
        private int _screenWidth;
        private int _screenHeight;

        public BackgroundManager(int screenWidth, int screenHeight)
        {
            _layers = new List<BackgroundLayer>();
            _screenWidth = screenWidth;
            _screenHeight = screenHeight;
        }

        public void AddLayer(Texture2D texture, float parallaxFactor)
        {
            _layers.Add(new BackgroundLayer(texture, parallaxFactor));
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 cameraPosition)
        {
            foreach (var layer in _layers)
            {
                layer.Draw(spriteBatch, cameraPosition, _screenWidth, _screenHeight);
            }
        }
    }
}
