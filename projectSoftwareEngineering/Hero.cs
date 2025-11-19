using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace projectSoftwareEngineering
{
    internal class Hero : IGameObject
    {
        private Texture2D texture;
        private Rectangle deelRectangle;
        private SpriteEffects direction = SpriteEffects.None;

        //moving the sprite
        private Vector2 positie;
        private Vector2 snelheid;
            

        private int schuifOp_X = 0;
        private int schuifOp_Y = 0;
        

        private double timer = 0;
        private double frameInterval = 110;

        public Hero(Texture2D texture)
        {
            //De hero moet een texture krijgen wanneer hij aangemaakt word.
            this.texture = texture;
            deelRectangle = new Rectangle(schuifOp_X, schuifOp_Y, 64, 64);
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, positie, deelRectangle, Color.White, 0f, Vector2.Zero, 1f, direction, 0f);

            snelheid = new Vector2(3, 0);

        }

        public void Update(GameTime gametime)
        {

            Idle(gametime);
            Run(gametime);
        }

        public void Idle(GameTime gametime)
        {
            if (Keyboard.GetState().GetPressedKeys().Length == 0)
            {
                timer += gametime.ElapsedGameTime.TotalMilliseconds;
                if (timer >= frameInterval)
                {
                    schuifOp_Y = 0;
                    deelRectangle.Y = schuifOp_Y;
                    schuifOp_X += 64;
                    if (schuifOp_X >= 512)
                    {
                        schuifOp_X = 0;
                    }
                    deelRectangle.X = schuifOp_X;
                    timer = 0;
                }
            }
        }

        public void Run(GameTime gametime)
        {
            
            if (Keyboard.GetState().IsKeyDown(Keys.D))
            {
                direction = SpriteEffects.None;
                timer += gametime.ElapsedGameTime.TotalMilliseconds;
                Move();

                if (timer >= frameInterval)
                {
                    schuifOp_Y = 64;
                    deelRectangle.Y = schuifOp_Y;
                    schuifOp_X += 64;
                    if (schuifOp_X >= 512)
                    {
                        schuifOp_X = 0;
                    }
                    deelRectangle.X = schuifOp_X;
                    timer = 0;
                }
            }
            if (Keyboard.GetState().IsKeyDown(Keys.A))
            {
                direction = SpriteEffects.FlipHorizontally;
                timer += gametime.ElapsedGameTime.TotalMilliseconds;
                Move();

                if (timer >= frameInterval)
                {
                    schuifOp_Y = 64;
                    deelRectangle.Y = schuifOp_Y;
                    schuifOp_X += 64;
                    if (schuifOp_X >= 512)
                    {
                        schuifOp_X = 0;
                    }
                    deelRectangle.X = schuifOp_X;
                    timer = 0;
                }
            }
        }
        public void Move()
        {
            KeyboardState k = Keyboard.GetState();
            if (k.IsKeyDown(Keys.D))
                snelheid.X = 3;
            if (k.IsKeyDown(Keys.A))
                snelheid.X = -3;

            positie += snelheid;
        }

        public void Jump()
        {

        }

    }
}
