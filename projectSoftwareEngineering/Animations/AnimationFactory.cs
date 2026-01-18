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
                Jump = BuildJumpAnimation(),
                Die = BuildDeathAnimation(),
                Attack = BuildAttackAnimation()
            };
        }

        public static AnimationSet CreateWalkingEnemyAnimations()
        {
            return new AnimationSet
            {
                WalkerEnemyWalk = BuildWalkerEnemyWalkAnimation(),
                WalkerEnemyDie = BuildsWalkerEnemyDieAnimation()
            };
        }
        public static AnimationSet CreateJumpingEnemyAnimations()
        {
            return new AnimationSet
            {
                JumperJump = BuildJumperEnemyJumpAnimation(),
                JumperDie = BuildJumperDieAnimation(),
                JumperIdle = BuildJumperEnemyIdleAnimation()
            };
        }
        public static AnimationSet CreateShooterEnemyAnimations()
        {
            return new AnimationSet
            {
                ShooterIdle = BuildShooterIdleAnimation(),
                ShooterAttack = BuildShooterAttackAnimation(),
                ShooterDie = BuildShooterDieAnimation()
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
        private static Animation BuildDeathAnimation()
        {
            Animation animation = new Animation();
            animation.AddFrame(new AnimationFrame(new Rectangle(0, 448, 64, 64)));
            animation.AddFrame(new AnimationFrame(new Rectangle(64, 448, 64, 64)));
            animation.AddFrame(new AnimationFrame(new Rectangle(128, 448, 64, 64)));
            animation.AddFrame(new AnimationFrame(new Rectangle(192, 448, 64, 64)));
            animation.AddFrame(new AnimationFrame(new Rectangle(256, 448, 64, 64)));
            animation.AddFrame(new AnimationFrame(new Rectangle(320, 448, 64, 64)));
            animation.AddFrame(new AnimationFrame(new Rectangle(384, 448, 64, 64)));
            animation.AddFrame(new AnimationFrame(new Rectangle(448, 448, 64, 64)));
            animation.AddFrame(new AnimationFrame(new Rectangle(512, 448, 64, 64)));
            animation.AddFrame(new AnimationFrame(new Rectangle(576, 448, 64, 64)));
            animation.AddFrame(new AnimationFrame(new Rectangle(640, 448, 64, 64)));
            animation.AddFrame(new AnimationFrame(new Rectangle(704, 448, 64, 64)));
            animation.AddFrame(new AnimationFrame(new Rectangle(768, 448, 64, 64)));
            animation.AddFrame(new AnimationFrame(new Rectangle(832, 448, 64, 64)));
            animation.Loop = false;
            return animation;
        }
        private static Animation BuildAttackAnimation()
        {
            Animation animation = new Animation();
            animation.AddFrame(new AnimationFrame(new Rectangle(0, 192, 64, 64)));
            animation.AddFrame(new AnimationFrame(new Rectangle(64, 192, 64, 64)));
            animation.AddFrame(new AnimationFrame(new Rectangle(128, 192, 64, 64)));
            animation.Loop = false;
            animation.FrameInterval = 60;
            return animation;
        }

        //Enemies
        private static Animation BuildShooterDieAnimation()
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
            animation.AddFrame(new AnimationFrame(new Rectangle(512, 0, 64, 64)));
            animation.AddFrame(new AnimationFrame(new Rectangle(576, 0, 64, 64)));
            animation.AddFrame(new AnimationFrame(new Rectangle(640, 0, 64, 64)));
            animation.Loop = false;
            return animation;
        }

        private static Animation BuildShooterAttackAnimation()
        {
            Animation animation = new Animation();
            //animation.AddFrame(new AnimationFrame(new Rectangle(0, 0, 64, 64)));
            animation.AddFrame(new AnimationFrame(new Rectangle(64, 0, 64, 64)));
            animation.AddFrame(new AnimationFrame(new Rectangle(128, 0, 64, 64)));
            animation.AddFrame(new AnimationFrame(new Rectangle(192, 0, 64, 64)));
            animation.AddFrame(new AnimationFrame(new Rectangle(256, 0, 64, 64)));
            animation.AddFrame(new AnimationFrame(new Rectangle(320, 0, 64, 64)));
            animation.AddFrame(new AnimationFrame(new Rectangle(384, 0, 64, 64)));
            animation.AddFrame(new AnimationFrame(new Rectangle(448, 0, 64, 64)));
            animation.AddFrame(new AnimationFrame(new Rectangle(512, 0, 64, 64)));
            animation.AddFrame(new AnimationFrame(new Rectangle(576, 0, 64, 64)));
            animation.AddFrame(new AnimationFrame(new Rectangle(640, 0, 64, 64)));
            animation.Loop = false;
            return animation;
        }

        private static Animation BuildShooterIdleAnimation()
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
            animation.AddFrame(new AnimationFrame(new Rectangle(512, 0, 64, 64)));
            return animation;
        }

        private static Animation BuildJumperDieAnimation()
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
            animation.AddFrame(new AnimationFrame(new Rectangle(512, 0, 64, 64)));
            animation.AddFrame(new AnimationFrame(new Rectangle(576, 0, 64, 64)));
            animation.AddFrame(new AnimationFrame(new Rectangle(640, 0, 64, 64)));
            animation.Loop = false;
            return animation;
        }

        private static Animation BuildJumperEnemyJumpAnimation()
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
            animation.AddFrame(new AnimationFrame(new Rectangle(512, 0, 64, 64)));
            animation.Loop = false;
            return animation;
        }
        private static Animation BuildJumperEnemyIdleAnimation()
        {
            Animation animation = new Animation();
            animation.AddFrame(new AnimationFrame(new Rectangle(0, 0, 64, 64)));
            animation.AddFrame(new AnimationFrame(new Rectangle(64, 0, 64, 64)));
            animation.AddFrame(new AnimationFrame(new Rectangle(128, 0, 64, 64)));
            animation.AddFrame(new AnimationFrame(new Rectangle(192, 0, 64, 64)));
            animation.AddFrame(new AnimationFrame(new Rectangle(256, 0, 64, 64)));
            animation.AddFrame(new AnimationFrame(new Rectangle(320, 0, 64, 64)));
            animation.Loop = true;
            return animation;
        }

        private static Animation BuildsWalkerEnemyDieAnimation()
        {
            Animation animation = new Animation();
            animation.AddFrame(new AnimationFrame(new Rectangle(0, 0, 96, 64)));
            animation.AddFrame(new AnimationFrame(new Rectangle(96, 0, 96, 64)));
            animation.AddFrame(new AnimationFrame(new Rectangle(192, 0, 96, 64)));
            animation.AddFrame(new AnimationFrame(new Rectangle(288, 0, 96, 64)));
            animation.AddFrame(new AnimationFrame(new Rectangle(384, 0, 96, 64)));
            animation.AddFrame(new AnimationFrame(new Rectangle(480, 0, 96, 64)));
            animation.AddFrame(new AnimationFrame(new Rectangle(576, 0, 96, 64)));
            animation.AddFrame(new AnimationFrame(new Rectangle(672, 0, 96, 64)));
            animation.AddFrame(new AnimationFrame(new Rectangle(768, 0, 96, 64)));
            animation.AddFrame(new AnimationFrame(new Rectangle(864, 0, 96, 64)));
            animation.AddFrame(new AnimationFrame(new Rectangle(960, 0, 96, 64)));
            animation.AddFrame(new AnimationFrame(new Rectangle(1056, 0, 96, 64)));
            animation.AddFrame(new AnimationFrame(new Rectangle(1152, 0, 96, 64)));
            animation.Loop = false;
            return animation;
        }

        private static Animation BuildWalkerEnemyWalkAnimation()
        {
            Animation animation = new Animation();
            animation.AddFrame(new AnimationFrame(new Rectangle(0, 0, 96, 64)));
            animation.AddFrame(new AnimationFrame(new Rectangle(96, 0, 96, 64)));
            animation.AddFrame(new AnimationFrame(new Rectangle(192, 0, 96, 64)));
            animation.AddFrame(new AnimationFrame(new Rectangle(288, 0, 96, 64)));
            animation.AddFrame(new AnimationFrame(new Rectangle(384, 0, 96, 64)));
            animation.AddFrame(new AnimationFrame(new Rectangle(480, 0, 96, 64)));
            animation.AddFrame(new AnimationFrame(new Rectangle(576, 0, 96, 64)));
            animation.AddFrame(new AnimationFrame(new Rectangle(672, 0, 96, 64)));
            animation.AddFrame(new AnimationFrame(new Rectangle(768, 0, 96, 64)));
            animation.AddFrame(new AnimationFrame(new Rectangle(864, 0, 96, 64)));
            animation.Loop = true;
            return animation;
        }
    }
}
