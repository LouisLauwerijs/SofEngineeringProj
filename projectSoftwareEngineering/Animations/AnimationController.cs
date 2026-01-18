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
            currentAnimation = animations.GetFirstAvailableAnimation();
        }

        public void IdleAnimation()
        {
            Animation targetAnimation = animations.Idle ?? animations.ShooterIdle ?? animations.JumperIdle;

            if (targetAnimation != null && currentAnimation != targetAnimation)
            {
                currentAnimation = targetAnimation;
                currentAnimation.Reset();
            }
        }

        public void RunAnimation()
        {
            Animation targetAnimation = animations.Run ?? animations.WalkerEnemyWalk;

            if (targetAnimation != null && currentAnimation != targetAnimation)
            {
                currentAnimation = targetAnimation;
                currentAnimation.Reset();
            }
        }

        public void JumpAnimation()
        {
            Animation targetAnimation = animations.Jump ?? animations.JumperJump;

            if (targetAnimation != null && currentAnimation != targetAnimation)
            {
                currentAnimation = targetAnimation;
                currentAnimation.Reset();
            }
        }

        public void DieAnimation()
        {
            Animation targetAnimation = animations.Die
                ?? animations.WalkerEnemyDie
                ?? animations.JumperDie
                ?? animations.ShooterDie;

            if (targetAnimation != null && currentAnimation != targetAnimation)
            {
                currentAnimation = targetAnimation;
                currentAnimation.Reset();
            }
        }
        public void AttackAnimation()
        {
            Animation targetAnimation = animations.Attack ?? animations.ShooterAttack;

            if (targetAnimation != null && currentAnimation != targetAnimation)
            {
                currentAnimation = targetAnimation;
                currentAnimation.Reset();
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
