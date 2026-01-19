using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projectSoftwareEngineering.Animations
{
    /// <summary>
    /// Container klasse die alle mogelijke animaties voor een character bevat.
    /// Ondersteunt zowel hero animaties als verschillende enemy type animaties.
    /// 
    /// SOLID Principes:
    /// - Single Responsibility Principle (SRP): Verantwoordelijk voor het groeperen
    ///   van alle animaties die bij een character type horen.
    /// - Open/Closed Principle (OCP): Kan uitgebreid worden met nieuwe animatie types
    ///   zonder bestaande functionaliteit te breken.
    /// - Interface Segregation Principle (ISP): Niet alle animaties worden door elk
    ///   character type gebruikt - elk character krijgt alleen de animaties die het nodig heeft.
    /// </summary>
    public class AnimationSet
    {
        #region Hero Animations

        /// <summary>
        /// Idle (stilstaan) animatie voor de hero.
        /// </summary>
        public Animation Idle { get; set; }

        /// <summary>
        /// Run (rennen) animatie voor de hero.
        /// </summary>
        public Animation Run { get; set; }

        /// <summary>
        /// Jump (springen) animatie voor de hero.
        /// </summary>
        public Animation Jump { get; set; }

        /// <summary>
        /// Die (dood) animatie voor de hero.
        /// </summary>
        public Animation Die { get; set; }

        /// <summary>
        /// Attack (aanval) animatie voor de hero.
        /// </summary>
        public Animation Attack { get; set; }

        #endregion

        #region Walker Enemy Animations

        /// <summary>
        /// Walk animatie voor de lopende vijand (Walker Enemy).
        /// </summary>
        public Animation WalkerEnemyWalk { get; set; }

        /// <summary>
        /// Die animatie voor de lopende vijand.
        /// </summary>
        public Animation WalkerEnemyDie { get; set; }

        #endregion

        #region Jumper Enemy Animations

        /// <summary>
        /// Jump animatie voor de springende vijand (Jumper Enemy).
        /// </summary>
        public Animation JumperJump { get; set; }

        /// <summary>
        /// Idle animatie voor de springende vijand.
        /// </summary>
        public Animation JumperIdle { get; set; }

        /// <summary>
        /// Die animatie voor de springende vijand.
        /// </summary>
        public Animation JumperDie { get; set; }

        #endregion

        #region Shooter Enemy Animations

        /// <summary>
        /// Idle animatie voor de schietende vijand (Shooter Enemy).
        /// </summary>
        public Animation ShooterIdle { get; set; }

        /// <summary>
        /// Attack animatie voor de schietende vijand.
        /// </summary>
        public Animation ShooterAttack { get; set; }

        /// <summary>
        /// Die animatie voor de schietende vijand.
        /// </summary>
        public Animation ShooterDie { get; set; }

        #endregion

        /// <summary>
        /// Zoekt en retourneert de eerste niet-null animatie in de set.
        /// Gebruikt een prioriteitsvolgorde: eerst hero animaties, dan enemy animaties.
        /// Dit zorgt ervoor dat er altijd een fallback animatie beschikbaar is.
        /// </summary>
        /// <returns>De eerste beschikbare animatie, of null als er geen animaties zijn</returns>
        public Animation GetFirstAvailableAnimation()
        {
            // Null-coalescing operator (??) wordt gebruikt voor fallback logica
            // Retourneert de eerste niet-null waarde in de chain
            return Idle
                ?? Run
                ?? Jump
                ?? Attack
                ?? WalkerEnemyWalk
                ?? JumperJump
                ?? JumperIdle
                ?? ShooterIdle
                ?? WalkerEnemyDie
                ?? JumperDie
                ?? ShooterAttack
                ?? ShooterDie;
        }
    }
}