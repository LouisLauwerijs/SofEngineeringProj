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
        private SpriteEffects direction = SpriteEffects.None;

        private AnimationSet animations;
        private Animation currentAnimation;

        // Movement data
        private Vector2 positie = new Vector2(0, 50);
        private Vector2 snelheid = new Vector2(3, 0);

        public Hero(Texture2D texture)
        {
            this.texture = texture;

            animations = new AnimationSet()
            {
                Idle = BuildIdleAnimation(),
                Run = BuildRunAnimation(),
                Jump = BuildJumpAnimation()
            };

            currentAnimation = animations.Idle;
        }
        private Animation BuildRunAnimation()
        {
            Animation anim = new Animation();

            anim.AddFrame(new AnimationFrame(new Rectangle(0, 64, 64, 64)));
            anim.AddFrame(new AnimationFrame(new Rectangle(64, 64, 64, 64)));
            anim.AddFrame(new AnimationFrame(new Rectangle(128, 64, 64, 64)));
            anim.AddFrame(new AnimationFrame(new Rectangle(192, 64, 64, 64)));
            anim.AddFrame(new AnimationFrame(new Rectangle(256, 64, 64, 64)));
            anim.AddFrame(new AnimationFrame(new Rectangle(320, 64, 64, 64)));
            anim.AddFrame(new AnimationFrame(new Rectangle(384, 64, 64, 64)));
            anim.AddFrame(new AnimationFrame(new Rectangle(448, 64, 64, 64)));

            return anim;
        }
        private Animation BuildJumpAnimation()
        {
            Animation anim = new Animation();
            anim.AddFrame(new AnimationFrame(new Rectangle(0, 256, 64, 64)));
            anim.AddFrame(new AnimationFrame(new Rectangle(64, 256, 64, 64)));
            anim.AddFrame(new AnimationFrame(new Rectangle(128, 256, 64, 64)));
            anim.AddFrame(new AnimationFrame(new Rectangle(192, 256, 64, 64)));

            return anim;
        }
        private Animation BuildIdleAnimation()
        {
            Animation anim = new Animation();

            anim.AddFrame(new AnimationFrame(new Rectangle(0, 0, 64, 64)));
            anim.AddFrame(new AnimationFrame(new Rectangle(64, 0, 64, 64)));
            anim.AddFrame(new AnimationFrame(new Rectangle(128, 0, 64, 64)));
            anim.AddFrame(new AnimationFrame(new Rectangle(192, 0, 64, 64)));
            anim.AddFrame(new AnimationFrame(new Rectangle(256, 0, 64, 64)));
            anim.AddFrame(new AnimationFrame(new Rectangle(320, 0, 64, 64)));
            anim.AddFrame(new AnimationFrame(new Rectangle(384, 0, 64, 64)));
            anim.AddFrame(new AnimationFrame(new Rectangle(448, 0, 64, 64)));

            return anim;
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, positie, currentAnimation.GetCurrentFrameRectangle(), Color.White, 0f, Vector2.Zero, 1f, direction, 0f);
        }

        public void Update(GameTime gametime)
        {
            HandleInput();
            currentAnimation.Update(gametime);
        }

        private void HandleInput()
        {
            KeyboardState k = Keyboard.GetState();

            bool isRunning = false;

            // Move Right
            if (k.IsKeyDown(Keys.D))
            {
                snelheid.X = 3;
                direction = SpriteEffects.None;
                positie += snelheid;

                currentAnimation = animations.Run;
                isRunning = true;
            }

            // Move Left
            if (k.IsKeyDown(Keys.A))
            {
                snelheid.X = -3;
                direction = SpriteEffects.FlipHorizontally;
                positie += snelheid;

                currentAnimation = animations.Run;
                isRunning = true;
            }

            //Jump
            if (k.IsKeyDown(Keys.Space))
            {
                snelheid.X = 0;
                positie += snelheid;

                currentAnimation = animations.Jump;
                isRunning = true;
            }

            // No movement → Idle
            if (!isRunning)
            {
                snelheid.X = 0;
                currentAnimation = animations.Idle;
            }
        }
    }
}
