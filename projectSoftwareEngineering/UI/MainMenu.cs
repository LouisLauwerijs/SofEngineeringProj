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
    public class MainMenu
    {
        private SpriteFont _titleFont;
        private SpriteFont _buttonFont;
        private Texture2D _buttonTexture;
        private List<MenuButton> _levelButtons;
        private string _title = "TEMP NAME";

        private int _virtualWidth;
        private int _virtualHeight;
        private int _screenWidth;
        private int _screenHeight;

        public MainMenu(Texture2D buttonTexture, SpriteFont titleFont, SpriteFont buttonFont, int virtualWidth, int virtualHeight, int screenWidth, int screenHeight)
        {
            _buttonTexture = buttonTexture;
            _titleFont = titleFont;
            _buttonFont = buttonFont;
            _virtualWidth = virtualWidth;
            _virtualHeight = virtualHeight;
            _screenWidth = screenWidth;
            _screenHeight = screenHeight;

            CreateButtons();
        }

        private void CreateButtons()
        {
            _levelButtons = new List<MenuButton>();

            int buttonWidth = 200;
            int buttonHeight = 80;
            int startY = _virtualHeight / 2;
            int spacing = 70;

            for (int i = 1; i <= 3; i++) // i <=3 means 3 levels
            {
                int x = (_virtualWidth - buttonWidth) / 2;
                int y = startY + ((i - 1) * spacing);

                MenuButton button = new MenuButton(
                    _buttonTexture,
                    _buttonFont,
                    $"Level {i}",
                    x, y,
                    buttonWidth, buttonHeight,
                    _virtualWidth, _virtualHeight,
                    _screenWidth, _screenHeight
                );

                _levelButtons.Add(button);
            }
        }

        public int? Update(MouseState currentMouse, MouseState previousMouse)
        {
            foreach (var button in _levelButtons)
            {
                button.Update(currentMouse);
            }

            // Check for clicks
            for (int i = 0; i < _levelButtons.Count; i++)
            {
                if (_levelButtons[i].Clicked(currentMouse, previousMouse))
                {
                    return i + 1;
                }
            }

            return null;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            //Title
            Vector2 titleSize = _titleFont.MeasureString(_title);
            Vector2 titlePosition = new Vector2(
                (_virtualWidth - titleSize.X) / 2,
                _virtualHeight / 4
            );
            spriteBatch.DrawString(_titleFont, _title, titlePosition, Color.White);

            //Buttons
            foreach (var button in _levelButtons)
            {
                button.Draw(spriteBatch);
            }
        }
    }
}
