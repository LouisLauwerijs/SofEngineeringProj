using Microsoft.Xna.Framework;
using projectSoftwareEngineering.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projectSoftwareEngineering.Characters.Enemies.JumpingEnemy
{
    /// <summary>
    /// Configuratie klasse die alle physics parameters voor de Jumping Enemy definieert.
    /// Ondersteunt dynamic spawn positions via constructor parameter.
    /// 
    /// SOLID Principes:
    /// - Single Responsibility Principle (SRP): Enige verantwoordelijkheid is het 
    ///   definiëren van jumping enemy configuratie waarden.
    /// - Dependency Inversion Principle (DIP): Implementeert ICharacterConfig interface,
    ///   waardoor JumpingEnemy niet afhankelijk is van deze concrete klasse.
    /// - Open/Closed Principle (OCP): Nieuwe configuratie waarden kunnen toegevoegd worden
    ///   door de interface uit te breiden.
    /// - Configuration Pattern: Scheidt configuratie data van business logica.
    /// </summary>
    public class JumpingEnemyConfig : ICharacterConfig
    {
        /// <summary>
        /// Privé veld dat de spawn positie opslaat.
        /// Wordt gezet via constructor voor flexibele placement.
        /// </summary>
        private Vector2 _spawnPoint;

        /// <summary>
        /// Constructor die een configuratie object maakt met een specifieke spawn point.
        /// Staat toe dat elke jumping enemy op een andere locatie kan spawnen.
        /// </summary>
        /// <param name="spawnPoint">De positie waar deze enemy moet spawnen in de game wereld</param>
        public JumpingEnemyConfig(Vector2 spawnPoint)
        {
            _spawnPoint = spawnPoint;
        }

        /// <summary>
        /// Start positie van de jumping enemy (dynamisch via constructor).
        /// Staat flexible enemy placement toe in levels.
        /// </summary>
        public Vector2 StartPosition => _spawnPoint;

        /// <summary>
        /// Gravity acceleratie voor de jumping enemy.
        /// 0.4f is hetzelfde als Hero voor consistente physics.
        /// </summary>
        public float Gravity => 0.4f;

        /// <summary>
        /// Jump kracht van de jumping enemy.
        /// -8.5f geeft dezelfde jump hoogte als de Hero.
        /// Maakt het voorspelbaar voor spelers om jumping enemies te ontwijken.
        /// </summary>
        public float JumpStrength => -8.5f;

        /// <summary>
        /// Horizontale bewegingssnelheid van de jumping enemy.
        /// 1.5f is langzamer dan Hero (4f), maakt ze makkelijker te ontwijken.
        /// Lagere snelheid past bij hun jump-based movement patroon.
        /// </summary>
        public float MoveSpeed => 1.5f;
    }
}