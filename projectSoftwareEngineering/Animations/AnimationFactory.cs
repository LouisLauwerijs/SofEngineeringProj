using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projectSoftwareEngineering.Animations
{
    public static class AnimationFactory
    {
        public static AnimationSet CreateHeroAnimations()
        {
            return new AnimationSet
            {
                Idle = BuildIdleAnimation(),
                Run = BuildRunAnimation(),
                Jump = BuildJumpAnimation()
            };
        }

        private static Animation BuildIdleAnimation()
        {
            Animation animation = new Animation();
            animation.AddFrame(new AnimationFrame(new Rectangle(0, 0, 64, 64)));
            animation.AddFrame(new AnimationFrame(new Rectangle(64, 0, 64, 64)));
            animation.AddFrame(new AnimationFrame(new Rectangle(128, 0, 64, 64)));
            animation.AddFrame(new AnimationFrame(new Rectangle(192, 0, 64, 64)));
            animation.AddFrame(new AnimationFrame(new Rectangle(256, 0, 64, 64)));
            animation.AddFrame(new AnimationFrame(new Rectangle(320, 0, 64, 64)));
            animation.AddFrame(new AnimationFrame(new Rectangle(384, 0, 64, 64)));
            animation.AddFrame(new AnimationFrame(new Rectangle(448, 0, 64, 64)));
            return animation;
        }

        private static Animation BuildRunAnimation()
        {
            Animation animation = new Animation();
            animation.AddFrame(new AnimationFrame(new Rectangle(0, 64, 64, 64)));
            animation.AddFrame(new AnimationFrame(new Rectangle(64, 64, 64, 64)));
            animation.AddFrame(new AnimationFrame(new Rectangle(128, 64, 64, 64)));
            animation.AddFrame(new AnimationFrame(new Rectangle(192, 64, 64, 64)));
            animation.AddFrame(new AnimationFrame(new Rectangle(256, 64, 64, 64)));
            animation.AddFrame(new AnimationFrame(new Rectangle(320, 64, 64, 64)));
            animation.AddFrame(new AnimationFrame(new Rectangle(384, 64, 64, 64)));
            animation.AddFrame(new AnimationFrame(new Rectangle(448, 64, 64, 64)));
            return animation;
        }

        private static Animation BuildJumpAnimation()
        {
            Animation animation = new Animation();
            animation.AddFrame(new AnimationFrame(new Rectangle(0, 256, 64, 64)));
            animation.AddFrame(new AnimationFrame(new Rectangle(64, 256, 64, 64)));
            animation.AddFrame(new AnimationFrame(new Rectangle(128, 256, 64, 64)));
            animation.AddFrame(new AnimationFrame(new Rectangle(192, 256, 64, 64)));
            return animation;
        }
    }
}
