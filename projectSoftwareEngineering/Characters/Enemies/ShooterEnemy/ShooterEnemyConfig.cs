using Microsoft.Xna.Framework;
using projectSoftwareEngineering.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projectSoftwareEngineering.Characters.Enemies.ShooterEnemy
{
    /// <summary>
    /// Configuratie klasse die alle physics parameters voor de Shooter Enemy definieert.
    /// Shooter enemies zijn stationary (beweegen niet), dus MoveSpeed en JumpStrength zijn 0.
    /// Ondersteunt dynamic spawn positions via constructor parameter.
    /// 
    /// SOLID Principes:
    /// - Single Responsibility Principle (SRP): Enige verantwoordelijkheid is het 
    ///   definiëren van shooter enemy configuratie waarden.
    /// - Dependency Inversion Principle (DIP): Implementeert ICharacterConfig interface,
    ///   waardoor ShooterEnemy niet afhankelijk is van deze concrete klasse.
    /// - Open/Closed Principle (OCP): Nieuwe configuratie waarden kunnen toegevoegd worden
    ///   door de interface uit te breiden.
    /// - Configuration Pattern: Scheidt configuratie data van business logica.
    /// </summary>
    public class ShooterEnemyConfig : ICharacterConfig
    {
        /// <summary>
        /// Privé veld dat de spawn positie opslaat.
        /// Wordt gezet via constructor voor flexibele placement.
        /// </summary>
        private Vector2 _spawnPoint;

        /// <summary>
        /// Constructor die een configuratie object maakt met een specifieke spawn point.
        /// Staat toe dat elke shooter enemy op een andere locatie kan spawnen.
        /// </summary>
        /// <param name="spawnPoint">De positie waar deze enemy moet spawnen in de game wereld</param>
        public ShooterEnemyConfig(Vector2 spawnPoint)
        {
            _spawnPoint = spawnPoint;
        }

        /// <summary>
        /// Start positie van de shooter enemy (dynamisch via constructor).
        /// Staat flexible enemy placement toe in levels (vaak op platforms of verhogingen).
        /// </summary>
        public Vector2 StartPosition => _spawnPoint;

        /// <summary>
        /// Gravity acceleratie voor de shooter enemy.
        /// 0.4f is hetzelfde als Hero voor consistente physics.
        /// Zorgt dat de enemy naar beneden valt tot hij grounded is.
        /// </summary>
        public float Gravity => 0.4f;

        /// <summary>
        /// Jump kracht van de shooter enemy.
        /// 0 omdat shooter enemies niet kunnen springen - ze blijven stationary.
        /// </summary>
        public float JumpStrength => 0;

        /// <summary>
        /// Horizontale bewegingssnelheid van de shooter enemy.
        /// 0 omdat shooter enemies niet horizontaal bewegen - ze zijn turret-achtig.
        /// Dit maakt ze voorspelbaar maar gevaarlijk door hun projectielen.
        /// </summary>
        public float MoveSpeed => 0;
    }
}