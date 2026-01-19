using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projectSoftwareEngineering.Background
{
    public class BackgroundLayer
    {
        private Texture2D _texture;
        private float _parallaxFactor;

        public BackgroundLayer(Texture2D texture, float parallaxFactor)
        {
            _texture = texture;
            _parallaxFactor = parallaxFactor;
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 cameraPosition, int screenWidth, int screenHeight)
        {
            // Safety checks
            if (_texture == null || _texture.Width == 0 || _texture.Height == 0)
                return;

            // Calculate parallax offset
            float offsetX = cameraPosition.X * _parallaxFactor;

            // Wrap offset to prevent issues with large values
            offsetX = offsetX % _texture.Width;
            if (offsetX > 0)
                offsetX -= _texture.Width;

            // Calculate how many tiles needed to cover screen
            int tilesNeeded = (int)Math.Ceiling((float)screenWidth / _texture.Width) + 1;

            // Draw tiles
            for (int i = 0; i <= tilesNeeded; i++)
            {
                float xPos = offsetX + (i * _texture.Width);

                // Scale to fit screen height
                float scale = (float)screenHeight / _texture.Height;

                spriteBatch.Draw(
                    _texture,
                    new Vector2(xPos, 0),
                    null,
                    Color.White,
                    0f,
                    Vector2.Zero,
                    scale,
                    SpriteEffects.None,
                    0f
                );
            }
        }
    }
}
