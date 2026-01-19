using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projectSoftwareEngineering.Animations
{
    /// <summary>
    /// Beheert welke animatie actief is en schakelt tussen verschillende animaties.
    /// Fungeert als een state manager voor character animaties.
    /// 
    /// SOLID Principes:
    /// - Single Responsibility Principle (SRP): De enige verantwoordelijkheid is het beheren 
    ///   van animatie transities en het bijhouden van de huidige actieve animatie.
    /// - Dependency Inversion Principle (DIP): Hangt af van de abstractie AnimationSet 
    ///   in plaats van concrete animatie implementaties.
    /// - Open/Closed Principle (OCP): Je kan nieuwe animatie types toevoegen aan AnimationSet
    ///   zonder deze controller te wijzigen.
    /// </summary>
    public class AnimationController
    {
        /// <summary>
        /// Set van alle beschikbare animaties voor dit character.
        /// </summary>
        private readonly AnimationSet animations;

        /// <summary>
        /// De huidige actieve animatie die wordt afgespeeld.
        /// </summary>
        private Animation currentAnimation;

        /// <summary>
        /// Constructor die een AnimationSet ontvangt en de eerste beschikbare animatie selecteert.
        /// </summary>
        /// <param name="Animations">Set van alle animaties voor dit character</param>
        public AnimationController(AnimationSet Animations)
        {
            animations = Animations;
            currentAnimation = animations.GetFirstAvailableAnimation();
        }

        /// <summary>
        /// Schakelt naar de idle (stilstaan) animatie.
        /// Controleert welke idle animatie beschikbaar is (Hero, Shooter, of Jumper variant).
        /// Reset de animatie alleen als we naar een nieuwe animatie overschakelen.
        /// </summary>
        public void IdleAnimation()
        {
            // Zoek de juiste idle animatie met fallback logica
            Animation targetAnimation = animations.Idle ?? animations.ShooterIdle ?? animations.JumperIdle;

            // Wissel alleen als de target animatie anders is dan de huidige
            if (targetAnimation != null && currentAnimation != targetAnimation)
            {
                currentAnimation = targetAnimation;
                currentAnimation.Reset();
            }
        }

        /// <summary>
        /// Schakelt naar de run (rennen) animatie.
        /// Gebruikt fallback naar WalkerEnemyWalk als Run niet beschikbaar is.
        /// </summary>
        public void RunAnimation()
        {
            Animation targetAnimation = animations.Run ?? animations.WalkerEnemyWalk;

            if (targetAnimation != null && currentAnimation != targetAnimation)
            {
                currentAnimation = targetAnimation;
                currentAnimation.Reset();
            }
        }

        /// <summary>
        /// Schakelt naar de jump (springen) animatie.
        /// Gebruikt fallback naar JumperJump voor enemies.
        /// </summary>
        public void JumpAnimation()
        {
            Animation targetAnimation = animations.Jump ?? animations.JumperJump;

            if (targetAnimation != null && currentAnimation != targetAnimation)
            {
                currentAnimation = targetAnimation;
                currentAnimation.Reset();
            }
        }

        /// <summary>
        /// Schakelt naar de die (dood) animatie.
        /// Heeft meerdere fallbacks voor verschillende enemy types.
        /// </summary>
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

        /// <summary>
        /// Schakelt naar de attack (aanval) animatie.
        /// Gebruikt fallback naar ShooterAttack als Attack niet beschikbaar is.
        /// </summary>
        public void AttackAnimation()
        {
            Animation targetAnimation = animations.Attack ?? animations.ShooterAttack;

            if (targetAnimation != null && currentAnimation != targetAnimation)
            {
                currentAnimation = targetAnimation;
                currentAnimation.Reset();
            }
        }

        /// <summary>
        /// Haalt de source rectangle op van het huidige frame van de actieve animatie.
        /// </summary>
        /// <returns>Rectangle voor rendering van de huidige sprite frame</returns>
        public Rectangle GetCurrentFrameRectangle()
        {
            return currentAnimation.GetCurrentFrameRectangle();
        }

        /// <summary>
        /// Update de huidige actieve animatie.
        /// </summary>
        /// <param name="gameTime">GameTime object voor timing</param>
        public void Update(GameTime gameTime)
        {
            currentAnimation.Update(gameTime);
        }
    }
}