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
    public class LevelCompleteScreen
    {
        private SpriteFont _titleFont;
        private SpriteFont _buttonFont;
        private Texture2D _buttonTexture;
        private List<MenuButton> _buttons;
        private string _completeText = "LEVEL COMPLETE!";
        private int _screenWidth;
        private int _screenHeight;
        private bool _hasNextLevel;

        private const int NEXT_LEVEL_BUTTON_INDEX = 0;
        private const int MENU_BUTTON_INDEX = 1;

        public LevelCompleteScreen(Texture2D buttonTexture, SpriteFont titleFont, SpriteFont buttonFont, int screenWidth, int screenHeight)
        {
            _buttonTexture = buttonTexture;
            _titleFont = titleFont;
            _buttonFont = buttonFont;
            _screenWidth = screenWidth;
            _screenHeight = screenHeight;
            _hasNextLevel = true;

            CreateButtons();
        }

        private void CreateButtons()
        {
            _buttons = new List<MenuButton>();
            int buttonWidth = 250;
            int buttonHeight = 80;
            int spacing = 30;
            int startY = (_screenHeight / 2) + 100;

            //Next level
            MenuButton nextLevelButton = new MenuButton(
                _buttonTexture,
                _buttonFont,
                "Next Level",
                (_screenWidth - buttonWidth) / 2,
                startY,
                buttonWidth, buttonHeight
            );
            _buttons.Add(nextLevelButton);

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

        public string Update(MouseState currentMouse, MouseState previousMouse, bool hasNextLevel)
        {
            _hasNextLevel = hasNextLevel;

            if (_hasNextLevel)
            {
                _buttons[NEXT_LEVEL_BUTTON_INDEX].Update(currentMouse);
                if (_buttons[NEXT_LEVEL_BUTTON_INDEX].Clicked(currentMouse, previousMouse))
                {
                    return "next";
                }
            }

            _buttons[MENU_BUTTON_INDEX].Update(currentMouse);
            if (_buttons[MENU_BUTTON_INDEX].Clicked(currentMouse, previousMouse))
            {
                return "menu";
            }

            return null;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            Vector2 textSize = _titleFont.MeasureString(_completeText);
            Vector2 textPosition = new Vector2(
                (_screenWidth - textSize.X) / 2,
                (_screenHeight / 2) - 100
            );
            spriteBatch.DrawString(_titleFont, _completeText, textPosition, Color.Gold);

            if (_hasNextLevel)
            {
                _buttons[NEXT_LEVEL_BUTTON_INDEX].Draw(spriteBatch);
            }

            _buttons[MENU_BUTTON_INDEX].Draw(spriteBatch);
        }
    }
}

