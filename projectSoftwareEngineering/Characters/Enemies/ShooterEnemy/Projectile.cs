using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using projectSoftwareEngineering.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projectSoftwareEngineering.Characters.Enemies.ShooterEnemy
{
    /// <summary>
    /// Representeert een projectiel (kogel) dat afgevuurd wordt door een ShooterEnemy.
    /// Beweegt in een rechte lijn en deactiveert na een maximale afstand of bij collision.
    /// 
    /// SOLID Principes:
    /// - Single Responsibility Principle (SRP): Enige verantwoordelijkheid is het beheren
    ///   van een projectiel's beweging, levensduur en collision bounds.
    /// - Interface Segregation Principle (ISP): Implementeert IProjectile en ICollidable,
    ///   alleen de interfaces die relevant zijn voor een projectiel.
    /// - Open/Closed Principle (OCP): Kan uitgebreid worden voor verschillende projectiel
    ///   types (exploderende, tracking, etc.) zonder de core logica te wijzigen.
    /// </summary>
    public class Projectile : IProjectile
    {
        /// <summary>
        /// Huidige positie van het projectiel in de game wereld.
        /// </summary>
        private Vector2 _position;

        /// <summary>
        /// Velocity (snelheid en richting) van het projectiel.
        /// X component bepaalt horizontale beweging, Y is 0 (beweegt alleen horizontaal).
        /// </summary>
        private Vector2 _velocity;

        /// <summary>
        /// Maximale afstand die het projectiel kan afleggen voordat het deactiveert.
        /// Voorkomt dat projectielen oneindig blijven bestaan.
        /// </summary>
        private float _maxDistance;

        /// <summary>
        /// Totale afstand die het projectiel al heeft afgelegd sinds creatie.
        /// </summary>
        private float _distanceTraveled;

        /// <summary>
        /// Texture voor het renderen van het projectiel.
        /// </summary>
        private Texture2D _texture;

        /// <summary>
        /// Geeft aan of het projectiel actief is en moet worden geupdate/getekend.
        /// False betekent dat het projectiel verwijderd kan worden.
        /// </summary>
        public bool IsActive { get; private set; }

        /// <summary>
        /// Collision bounds van het projectiel voor hitbox detectie.
        /// Klein vierkant van 8x8 pixels op de huidige positie.
        /// </summary>
        public Rectangle Bounds => new Rectangle(
            (int)_position.X,
            (int)_position.Y,
            8,  // Breedte van projectiel hitbox
            8   // Hoogte van projectiel hitbox
        );

        /// <summary>
        /// Geeft aan of het projectiel een solid object is voor collision.
        /// False betekent dat objecten er doorheen kunnen bewegen (behalve targets).
        /// </summary>
        public bool IsSolid => false;

        /// <summary>
        /// Constructor die een nieuw projectiel initialiseert met alle benodigde parameters.
        /// </summary>
        /// <param name="texture">Texture voor het renderen van het projectiel</param>
        /// <param name="startPosition">Start positie waar het projectiel gespawnd wordt</param>
        /// <param name="direction">Richting van beweging (-1 voor links, 1 voor rechts)</param>
        /// <param name="speed">Snelheid van het projectiel in pixels per frame</param>
        /// <param name="maxDistance">Maximale afstand voordat deactivatie</param>
        public Projectile(Texture2D texture, Vector2 startPosition, float direction, float speed, float maxDistance)
        {
            _texture = texture;
            _position = startPosition;

            // Bereken velocity: alleen horizontale beweging (Y = 0)
            _velocity = new Vector2(direction * speed, 0);

            _maxDistance = maxDistance;
            _distanceTraveled = 0;
            IsActive = true;  // Start altijd actief
        }

        /// <summary>
        /// Update de positie van het projectiel en controleer of het zijn maximale afstand bereikt heeft.
        /// Deactiveert automatisch wanneer de max distance overschreden wordt.
        /// Moet elke frame aangeroepen worden.
        /// </summary>
        /// <param name="gameTime">GameTime voor timing (momenteel niet gebruikt)</param>
        public void Update(GameTime gameTime)
        {
            // Geen update als al gedeactiveerd
            if (!IsActive) return;

            // Verplaats het projectiel
            _position += _velocity;

            // Tel de afgelegde afstand bij (absolute waarde voor beide richtingen)
            _distanceTraveled += System.Math.Abs(_velocity.X);

            // Deactiveer wanneer maximale afstand bereikt is
            if (_distanceTraveled >= _maxDistance)
            {
                Deactivate();
            }
        }

        /// <summary>
        /// Tekent het projectiel op het scherm.
        /// Gebruikt een gele kleur overlay voor zichtbaarheid.
        /// Tekent alleen als het projectiel actief is.
        /// </summary>
        /// <param name="spriteBatch">SpriteBatch voor rendering</param>
        public void Draw(SpriteBatch spriteBatch)
        {
            // Teken alleen actieve projectielen
            if (!IsActive) return;

            // Teken met gele tint voor visuele feedback
            spriteBatch.Draw(_texture, Bounds, Color.Yellow);
        }

        /// <summary>
        /// Deactiveert het projectiel.
        /// Aangeroepen na collision met target of solid object, of na max distance.
        /// Markeert het projectiel voor verwijdering uit de game.
        /// </summary>
        public void Deactivate()
        {
            IsActive = false;
        }
    }
}