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
    public class DeathScreen
    {
        private SpriteFont _titleFont;
        private SpriteFont _buttonFont;
        private Texture2D _buttonTexture;
        private List<MenuButton> _buttons;
        private string _deathText = "YOU DIED";

        private int _screenWidth;
        private int _screenHeight;

        // Button indices for clarity
        private const int RETRY_BUTTON_INDEX = 0;
        private const int MENU_BUTTON_INDEX = 1;

        public DeathScreen(Texture2D buttonTexture, SpriteFont titleFont, SpriteFont buttonFont, int screenWidth, int screenHeight)
        {
            _buttonTexture = buttonTexture;
            _titleFont = titleFont;
            _buttonFont = buttonFont;
            _screenWidth = screenWidth;
            _screenHeight = screenHeight;

            CreateButtons();
        }

        private void CreateButtons()
        {
            _buttons = new List<MenuButton>();

            int buttonWidth = 250;
            int buttonHeight = 80;
            int spacing = 30;
            int startY = (_screenHeight / 2) + 100;

            //Retry 
            MenuButton retryButton = new MenuButton(
                _buttonTexture,
                _buttonFont,
                "Retry",
                (_screenWidth - buttonWidth) / 2,
                startY,
                buttonWidth, buttonHeight
            );
            _buttons.Add(retryButton);

            //Menu
            MenuButton menuButton = new MenuButton(
                _buttonTexture,
                _buttonFont,
                "Main Menu",
                (_screenWidth - buttonWidth) / 2,
                startY + buttonHeight + spacing,
                buttonWidth, buttonHeight
            );
            _buttons.Add(menuButton);
        }

        public string Update(MouseState currentMouse, MouseState previousMouse)
        {
            foreach (var button in _buttons)
            {
                button.Update(currentMouse);
            }

            if (_buttons[RETRY_BUTTON_INDEX].Clicked(currentMouse, previousMouse))
            {
                return "retry";
            }

            if (_buttons[MENU_BUTTON_INDEX].Clicked(currentMouse, previousMouse))
            {
                return "menu";
            }

            return null;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            Vector2 textSize = _titleFont.MeasureString(_deathText);
            Vector2 textPosition = new Vector2(
                (_screenWidth - textSize.X) / 2,
                (_screenHeight / 2) - 100
            );
            spriteBatch.DrawString(_titleFont, _deathText, textPosition, Color.Red);

            foreach (var button in _buttons)
            {
                button.Draw(spriteBatch);
            }
        }
    }
}