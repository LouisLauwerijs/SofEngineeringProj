using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projectSoftwareEngineering.Animations
{
    /// <summary>
    /// Factory klasse voor het creëren van complete AnimationSets voor verschillende character types.
    /// Centraliseert alle animatie configuraties en frame definitie op één plek.
    /// 
    /// SOLID Principes:
    /// - Single Responsibility Principle (SRP): Enige verantwoordelijkheid is het construeren
    ///   van animatie objecten met hun correcte frame data.
    /// - Open/Closed Principle (OCP): Open voor uitbreiding door nieuwe Create methodes toe te voegen
    ///   voor nieuwe character types zonder bestaande code te wijzigen.
    /// - Factory Pattern: Encapsuleert de complexe creatie logica van AnimationSets.
    ///   Client code hoeft niet te weten hoe animaties geconstrueerd worden.
    /// </summary>
    public static class AnimationFactory
    {
        #region Public Factory Methods

        /// <summary>
        /// Creëert de complete set van animaties voor de speler (Hero).
        /// Bevat: Idle, Run, Jump, Die en Attack animaties.
        /// </summary>
        /// <returns>AnimationSet met alle hero animaties</returns>
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

        /// <summary>
        /// Creëert de complete set van animaties voor de lopende vijand (Walking Enemy).
        /// Bevat: Walk en Die animaties.
        /// </summary>
        /// <returns>AnimationSet met alle walking enemy animaties</returns>
        public static AnimationSet CreateWalkingEnemyAnimations()
        {
            return new AnimationSet
            {
                WalkerEnemyWalk = BuildWalkerEnemyWalkAnimation(),
                WalkerEnemyDie = BuildsWalkerEnemyDieAnimation()
            };
        }

        /// <summary>
        /// Creëert de complete set van animaties voor de springende vijand (Jumping Enemy).
        /// Bevat: Jump, Die en Idle animaties.
        /// </summary>
        /// <returns>AnimationSet met alle jumping enemy animaties</returns>
        public static AnimationSet CreateJumpingEnemyAnimations()
        {
            return new AnimationSet
            {
                JumperJump = BuildJumperEnemyJumpAnimation(),
                JumperDie = BuildJumperDieAnimation(),
                JumperIdle = BuildJumperEnemyIdleAnimation()
            };
        }

        /// <summary>
        /// Creëert de complete set van animaties voor de schietende vijand (Shooter Enemy).
        /// Bevat: Idle, Attack en Die animaties.
        /// </summary>
        /// <returns>AnimationSet met alle shooter enemy animaties</returns>
        public static AnimationSet CreateShooterEnemyAnimations()
        {
            return new AnimationSet
            {
                ShooterIdle = BuildShooterIdleAnimation(),
                ShooterAttack = BuildShooterAttackAnimation(),
                ShooterDie = BuildShooterDieAnimation()
            };
        }

        #endregion

        #region Hero Animation Builders

        /// <summary>
        /// Bouwt de idle animatie voor de hero.
        /// 8 frames van 64x64 pixels, horizontaal gerangschikt in de sprite sheet (rij 0).
        /// </summary>
        private static Animation BuildIdleAnimation()
        {
            Animation animation = new Animation();
            // 8 frames op rij Y=0, elk 64x64 pixels breed
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

        /// <summary>
        /// Bouwt de run animatie voor de hero.
        /// 8 frames van 64x64 pixels op rij Y=64 in de sprite sheet.
        /// </summary>
        private static Animation BuildRunAnimation()
        {
            Animation animation = new Animation();
            // 8 frames op rij Y=64
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

        /// <summary>
        /// Bouwt de jump animatie voor de hero.
        /// 4 frames van 64x64 pixels op rij Y=256 in de sprite sheet.
        /// </summary>
        private static Animation BuildJumpAnimation()
        {
            Animation animation = new Animation();
            // 4 frames op rij Y=256
            animation.AddFrame(new AnimationFrame(new Rectangle(0, 256, 64, 64)));
            animation.AddFrame(new AnimationFrame(new Rectangle(64, 256, 64, 64)));
            animation.AddFrame(new AnimationFrame(new Rectangle(128, 256, 64, 64)));
            animation.AddFrame(new AnimationFrame(new Rectangle(192, 256, 64, 64)));
            return animation;
        }

        /// <summary>
        /// Bouwt de death animatie voor de hero.
        /// 14 frames van 64x64 pixels op rij Y=448 in de sprite sheet.
        /// Loop is false zodat de animatie stopt op het laatste frame.
        /// </summary>
        private static Animation BuildDeathAnimation()
        {
            Animation animation = new Animation();
            // 14 frames op rij Y=448 voor de dood animatie
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
            animation.Loop = false; // Speelt slechts één keer af
            return animation;
        }

        /// <summary>
        /// Bouwt de attack animatie voor de hero.
        /// 3 frames van 64x64 pixels op rij Y=192 in de sprite sheet.
        /// Snellere frame interval (60ms) voor een korte, snappy attack.
        /// Loop is false zodat de attack slechts één keer afspeelt.
        /// </summary>
        private static Animation BuildAttackAnimation()
        {
            Animation animation = new Animation();
            // 3 frames op rij Y=192
            animation.AddFrame(new AnimationFrame(new Rectangle(0, 192, 64, 64)));
            animation.AddFrame(new AnimationFrame(new Rectangle(64, 192, 64, 64)));
            animation.AddFrame(new AnimationFrame(new Rectangle(128, 192, 64, 64)));
            animation.Loop = false; // Attack speelt slechts één keer
            animation.FrameInterval = 60; // Snellere animatie voor responsive gevoel
            return animation;
        }

        #endregion

        #region Shooter Enemy Animation Builders

        /// <summary>
        /// Bouwt de die animatie voor de shooter enemy.
        /// 11 frames van 64x64 pixels op rij Y=0.
        /// </summary>
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

        /// <summary>
        /// Bouwt de attack animatie voor de shooter enemy.
        /// 10 frames van 64x64 pixels (frame 0 is uitgecommentarieerd, start bij frame 1).
        /// </summary>
        private static Animation BuildShooterAttackAnimation()
        {
            Animation animation = new Animation();
            // Eerste frame is uitgecommentarieerd - mogelijk een windup frame
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

        /// <summary>
        /// Bouwt de idle animatie voor de shooter enemy.
        /// 9 frames van 64x64 pixels, loopt continu.
        /// </summary>
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

        #endregion

        #region Jumper Enemy Animation Builders

        /// <summary>
        /// Bouwt de die animatie voor de jumper enemy.
        /// 11 frames van 64x64 pixels.
        /// </summary>
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

        /// <summary>
        /// Bouwt de jump animatie voor de jumper enemy.
        /// 9 frames van 64x64 pixels, speelt één keer af per sprong.
        /// </summary>
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
            animation.Loop = false; // Spring animatie speelt één keer
            return animation;
        }

        /// <summary>
        /// Bouwt de idle animatie voor de jumper enemy.
        /// 6 frames van 64x64 pixels, loopt continu terwijl op de grond.
        /// </summary>
        private static Animation BuildJumperEnemyIdleAnimation()
        {
            Animation animation = new Animation();
            animation.AddFrame(new AnimationFrame(new Rectangle(0, 0, 64, 64)));
            animation.AddFrame(new AnimationFrame(new Rectangle(64, 0, 64, 64)));
            animation.AddFrame(new AnimationFrame(new Rectangle(128, 0, 64, 64)));
            animation.AddFrame(new AnimationFrame(new Rectangle(192, 0, 64, 64)));
            animation.AddFrame(new AnimationFrame(new Rectangle(256, 0, 64, 64)));
            animation.AddFrame(new AnimationFrame(new Rectangle(320, 0, 64, 64)));
            animation.Loop = true; // Blijft loopen tijdens idle
            return animation;
        }

        #endregion

        #region Walker Enemy Animation Builders

        /// <summary>
        /// Bouwt de die animatie voor de walker enemy.
        /// 13 frames van 96x64 pixels (bredere frames dan andere enemies).
        /// </summary>
        private static Animation BuildsWalkerEnemyDieAnimation()
        {
            Animation animation = new Animation();
            // Walker enemy heeft bredere frames (96 pixels breed vs 64)
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

        /// <summary>
        /// Bouwt de walk animatie voor de walker enemy.
        /// 10 frames van 96x64 pixels, loopt continu tijdens beweging.
        /// </summary>
        private static Animation BuildWalkerEnemyWalkAnimation()
        {
            Animation animation = new Animation();
            // 10 frames voor loop cycle
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
            animation.Loop = true; // Blijft loopen tijdens beweging
            return animation;
        }

        #endregion
    }
}