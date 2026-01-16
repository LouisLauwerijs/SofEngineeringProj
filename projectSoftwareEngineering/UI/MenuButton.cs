using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projectSoftwareEngineering.UI
{
    public class MenuButton
    {
        public Rectangle Bounds { get; private set; }
        public string Text { get; private set; }
        private Texture2D _texture;
        private SpriteFont _font;
        private Color _Color = Color.Gray;
        private Color _hoverColor = Color.LimeGreen;
        private bool _isHovered;
        private int _virtualWidth; 
        private int _virtualHeight;
        private int _screenWidth;
        private int _screenHeight;

        public MenuButton(Texture2D texture, SpriteFont font, string text, int x, int y, int width, int height, int virtualWidth, int virtualHeight, int screenWidth, int screenHeight)
        {
            _texture = texture;
            _font = font;
            Text = text;
            Bounds = new Rectangle(x, y, width, height);
            _virtualWidth = virtualWidth;
            _virtualHeight = virtualHeight;
            _screenWidth = screenWidth;
            _screenHeight = screenHeight;
        }

        public void Update(MouseState mouseState)
        {
            float scaleX = (float)_virtualWidth / _screenWidth;
            float scaleY = (float)_virtualHeight / _screenHeight;

            Point mousePos = new Point(
                (int)(mouseState.X * scaleX),
                (int)(mouseState.Y * scaleY)
            );
            _isHovered = Bounds.Contains(mousePos);
        }

        public bool Clicked(MouseState currentMouse, MouseState previousMouse)
        {
            return _isHovered &&
                   currentMouse.LeftButton == ButtonState.Released &&
                   previousMouse.LeftButton == ButtonState.Pressed;
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            Color buttonColor = _isHovered ? _hoverColor : _Color;

            spriteBatch.Draw(_texture, Bounds, buttonColor);

            Vector2 textSize = _font.MeasureString(Text);
            Vector2 textPosition = new Vector2(
                Bounds.X + (Bounds.Width - textSize.X) / 2,
                Bounds.Y + (Bounds.Height - textSize.Y) / 2
            );

            spriteBatch.DrawString(_font, Text, textPosition, Color.Black);
        }
    }
}
