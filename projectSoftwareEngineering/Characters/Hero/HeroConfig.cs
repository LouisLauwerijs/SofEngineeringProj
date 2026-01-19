using Microsoft.Xna.Framework;
using projectSoftwareEngineering.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projectSoftwareEngineering.Characters.Hero
{
    /// <summary>
    /// Configuratie klasse die alle physics parameters voor de Hero definieert.
    /// Centraliseert alle hero-specifieke constanten op één plek voor makkelijk tweaken.
    /// 
    /// SOLID Principes:
    /// - Single Responsibility Principle (SRP): Enige verantwoordelijkheid is het 
    ///   definiëren van hero configuratie waarden.
    /// - Dependency Inversion Principle (DIP): Implementeert ICharacterConfig interface,
    ///   waardoor Hero niet afhankelijk is van deze concrete klasse.
    /// - Open/Closed Principle (OCP): Nieuwe configuratie waarden kunnen toegevoegd worden
    ///   door de interface uit te breiden zonder bestaande code te breken.
    /// - Configuration Pattern: Scheidt configuratie data van business logica.
    /// </summary>
    public class HeroConfig : ICharacterConfig
    {
        /// <summary>
        /// Start positie van de hero wanneer een level begint.
        /// (50, 50) plaatst de hero linksboven in het level.
        /// </summary>
        public Vector2 StartPosition => new Vector2(50, 50);

        /// <summary>
        /// Gravity acceleratie die elke frame aan de Y velocity toegevoegd wordt.
        /// 0.4f zorgt voor een redelijk realistische val snelheid.
        /// Hogere waarde = snellere val, lagere waarde = floatier gevoel.
        /// </summary>
        public float Gravity => 0.4f;

        /// <summary>
        /// Jump kracht - de negatieve Y velocity die toegepast wordt bij een sprong.
        /// -8.5f geeft een goede spring hoogte voor platformer gameplay.
        /// Meer negatieve waarde = hogere sprong, minder negatief = lagere sprong.
        /// </summary>
        public float JumpStrength => -8.5f;

        /// <summary>
        /// Horizontale bewegingssnelheid in pixels per frame.
        /// 4f is een goede balans tussen responsive controls en controllability.
        /// Hogere waarde = snellere beweging maar moeilijker te controleren.
        /// </summary>
        public float MoveSpeed => 4f;
    }
}