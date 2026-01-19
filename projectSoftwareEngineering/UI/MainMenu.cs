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
        private string _title = "Samurai Jumper";

        private int _screenWidth;
        private int _screenHeight;

        public MainMenu(Texture2D buttonTexture, SpriteFont titleFont, SpriteFont buttonFont, int screenWidth, int screenHeight)
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
            _levelButtons = new List<MenuButton>();

            int buttonWidth = 300;
            int buttonHeight = 250;
            int startY = _screenHeight / 2;
            int spacing = 40;
            int numberOfLevels = 3;

            int totalWidth = (buttonWidth * numberOfLevels) + (spacing * (numberOfLevels - 1));

            int startX = (_screenWidth - totalWidth) / 2;
            int y = (_screenHeight / 2) + 50;

            for (int i = 1; i <= numberOfLevels; i++) 
            {
                int x = startX + ((i - 1) * (buttonWidth + spacing));

                MenuButton button = new MenuButton(
                    _buttonTexture,
                    _buttonFont,
                    $"Level {i}",
                    x, y,
                    buttonWidth, buttonHeight
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
                (_screenWidth - titleSize.X) / 2,
                _screenHeight / 4
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
