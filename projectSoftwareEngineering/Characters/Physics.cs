using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projectSoftwareEngineering.Characters
{
    /// <summary>
    /// Beheert de physics (natuurkunde) voor een character inclusief positie, velocity, 
    /// gravity en movement. Implementeert basis platformer physics.
    /// 
    /// SOLID Principes:
    /// - Single Responsibility Principle (SRP): Verantwoordelijk voor alle physics-gerelateerde
    ///   berekeningen en state management van een character.
    /// - Open/Closed Principle (OCP): Kan uitgebreid worden met extra physics features
    ///   (bijv. acceleration, friction) zonder bestaande code te wijzigen.
    /// - Dependency Inversion Principle (DIP): Gebruikt geen concrete implementaties,
    ///   werkt alleen met XNA/MonoGame primitives (Vector2).
    /// </summary>
    public class Physics
    {
        /// <summary>
        /// Huidige positie van het character in de game wereld (in pixels).
        /// </summary>
        public Vector2 Position { get; set; }

        /// <summary>
        /// Huidige velocity (snelheid en richting) van het character.
        /// X component = horizontale beweging, Y component = verticale beweging.
        /// </summary>
        public Vector2 Velocity { get; set; }

        /// <summary>
        /// Geeft aan of het character op de grond staat.
        /// Belangrijk voor jump mechanisme - je kan alleen springen als je op de grond staat.
        /// </summary>
        public bool IsGrounded { get; set; }

        /// <summary>
        /// Gravity waarde die constant aan de Y velocity toegevoegd wordt.
        /// Positieve waarde zorgt voor neerwaartse acceleratie.
        /// </summary>
        private float _gravity;

        /// <summary>
        /// Jump strength - de negatieve Y velocity die toegepast wordt bij een sprong.
        /// Negatieve waarde zorgt voor opwaartse beweging.
        /// </summary>
        private float _jumpStrength;

        /// <summary>
        /// Horizontale bewegingssnelheid van het character.
        /// </summary>
        private float _moveSpeed;

        /// <summary>
        /// Constructor die een Physics object initialiseert met alle benodigde parameters.
        /// </summary>
        /// <param name="startPosition">Initiële positie van het character</param>
        /// <param name="gravity">Gravity acceleratie (positief = naar beneden)</param>
        /// <param name="jumpStrength">Jump kracht (negatief = naar boven)</param>
        /// <param name="moveSpeed">Horizontale bewegingssnelheid</param>
        public Physics(Vector2 startPosition, float gravity, float jumpStrength, float moveSpeed)
        {
            Position = startPosition;
            _gravity = gravity;
            _jumpStrength = jumpStrength;
            _moveSpeed = moveSpeed;
            Velocity = Vector2.Zero; // Start zonder beweging
            IsGrounded = false;
        }

        /// <summary>
        /// Past gravity toe op de velocity als het character niet op de grond staat.
        /// Verhoogt de neerwaartse (Y) velocity met de gravity waarde.
        /// Moet elke frame aangeroepen worden voor realistische val-physics.
        /// </summary>
        public void ApplyGravity()
        {
            if (!IsGrounded)
            {
                // Voeg gravity toe aan de Y component (neerwaarts)
                Velocity = new Vector2(Velocity.X, Velocity.Y + _gravity);
            }
        }

        /// <summary>
        /// Beweegt het character horizontaal in de opgegeven richting.
        /// Past zowel velocity als positie aan.
        /// </summary>
        /// <param name="direction">Richting van beweging: -1 voor links, 1 voor rechts</param>
        public void MoveHorizontal(float direction)
        {
            // Bereken nieuwe velocity gebaseerd op richting en snelheid
            Velocity = new Vector2(direction * _moveSpeed, Velocity.Y);

            // Pas de positie direct aan met de horizontale velocity
            Position += new Vector2(Velocity.X, 0);
        }

        /// <summary>
        /// Voert een sprong uit als het character op de grond staat.
        /// Past een negatieve (opwaartse) Y velocity toe en zet IsGrounded op false.
        /// </summary>
        public void Jump()
        {
            // Kan alleen springen als op de grond
            if (IsGrounded)
            {
                // Pas jump strength toe (negatieve waarde = omhoog)
                Velocity = new Vector2(Velocity.X, _jumpStrength);
                IsGrounded = false;
            }
        }

        /// <summary>
        /// Update de verticale positie gebaseerd op de huidige Y velocity.
        /// Moet na ApplyGravity() aangeroepen worden.
        /// </summary>
        public void UpdateVerticalPosition()
        {
            // Voeg de verticale velocity toe aan de positie
            Position += new Vector2(0, Velocity.Y);
        }

        /// <summary>
        /// Stopt alle horizontale beweging door de X velocity op 0 te zetten.
        /// Behoudt de Y velocity voor gravity/jumping.
        /// Nuttig wanneer de speler geen input geeft of tegen een muur botst.
        /// </summary>
        public void StopHorizontalMovement()
        {
            Velocity = new Vector2(0, Velocity.Y);
        }
    }
}