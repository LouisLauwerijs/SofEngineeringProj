using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projectSoftwareEngineering.Animations
{
    public class AnimationController
    {
        private readonly AnimationSet animations;
        private Animation currentAnimation;

        public AnimationController(AnimationSet Animations)
        {
            animations = Animations;
            currentAnimation = animations.Idle;
        }

        public void IdleAnimation()
        {
            if (currentAnimation != animations.Idle)
            {
                currentAnimation = animations.Idle;
            }
        }

        public void RunAnimation()
        {
            if (currentAnimation != animations.Run)
            {
                currentAnimation = animations.Run;
            }
        }

        public void JumpAnimation()
        {
            if (currentAnimation != animations.Jump)
            {
                currentAnimation = animations.Jump;
            }
        }

        public void DieAnimation()
        {
            if (currentAnimation != animations.Die)
            {
                currentAnimation = animations.Die;
            }
        }

        public Rectangle GetCurrentFrameRectangle()
        {
            return currentAnimation.GetCurrentFrameRectangle();
        }

        public void Update(GameTime gameTime)
        {
            currentAnimation.Update(gameTime);
        }
    }
}
