using Microsoft.Xna.Framework;

namespace projectSoftwareEngineering.Characters
{
    /// <summary>
    /// Beheert de health (gezondheid) status van een character inclusief damage en healing.
    /// Implementeert een invulnerability (onkwetsbaarheid) mechanisme om spam damage te voorkomen.
    /// 
    /// SOLID Principes:
    /// - Single Responsibility Principle (SRP): Enige verantwoordelijkheid is het beheren
    ///   van health state, damage, healing en invulnerability timing.
    /// - Open/Closed Principle (OCP): Kan uitgebreid worden met nieuwe health mechanismen
    ///   (bijv. shield, armor) zonder de bestaande code te wijzigen.
    /// </summary>
    public class Health
    {
        /// <summary>
        /// Huidige health waarde van het character.
        /// Wanneer dit 0 bereikt, is het character dood.
        /// </summary>
        public int CurrentHealth { get; set; }

        /// <summary>
        /// Maximale health waarde die het character kan hebben.
        /// Gebruikt bij healing om te voorkomen dat health boven het maximum gaat.
        /// </summary>
        public int MaxHealth { get; set; }

        /// <summary>
        /// Geeft aan of het character momenteel onkwetsbaar is.
        /// Tijdens invulnerability kan het character geen damage nemen.
        /// Voorkomt dat een character meerdere keren snel achter elkaar geraakt wordt.
        /// </summary>
        public bool IsInvulnerable { get; set; }

        /// <summary>
        /// Timer die de resterende invulnerability tijd bijhoudt in seconden.
        /// Telt af tijdens VulnerableUpdate().
        /// </summary>
        private float _invulnerabilityTimer;

        /// <summary>
        /// Constructor die een Health object initialiseert met een maximale health waarde.
        /// </summary>
        /// <param name="maxHealth">De maximale en initiële health waarde</param>
        public Health(int maxHealth)
        {
            MaxHealth = maxHealth;
            CurrentHealth = maxHealth; // Start op vol health
            IsInvulnerable = false;
        }

        /// <summary>
        /// Vermindert de current health met 1 als het character niet onkwetsbaar is.
        /// Activeert invulnerability voor 1 seconde na damage (behalve bij dood).
        /// </summary>
        public void TakeDamage()
        {
            // Geen damage als al onkwetsbaar of al dood
            if (IsInvulnerable || CurrentHealth <= 0)
                return;

            CurrentHealth -= 1;

            // Activeer invulnerability alleen als het character nog leeft
            if (CurrentHealth > 0)
            {
                IsInvulnerable = true;
                _invulnerabilityTimer = 1; // 1 seconde onkwetsbaarheid
            }
        }

        /// <summary>
        /// Verhoogt de current health met 1, tot maximaal MaxHealth.
        /// Voorkomt overhealing boven het maximum.
        /// </summary>
        public void Heal()
        {
            CurrentHealth += 1;

            // Cap de health op het maximum
            if (CurrentHealth >= MaxHealth)
            {
                CurrentHealth = MaxHealth;
            }
        }

        /// <summary>
        /// Reset de health naar de initiële staat.
        /// Herstelt health naar maximum en verwijdert invulnerability.
        /// Nuttig bij respawn of level reset.
        /// </summary>
        public void Reset()
        {
            CurrentHealth = MaxHealth;
            IsInvulnerable = false;
            _invulnerabilityTimer = 0;
        }

        /// <summary>
        /// Update de invulnerability timer en verwijdert invulnerability wanneer de tijd verstreken is.
        /// Moet elke frame aangeroepen worden in de game loop.
        /// </summary>
        /// <param name="gameTime">GameTime object voor delta time berekening</param>
        public void VulnerableUpdate(GameTime gameTime)
        {
            if (IsInvulnerable)
            {
                // Tel af met de verstreken tijd sinds het laatste frame
                _invulnerabilityTimer -= (float)gameTime.ElapsedGameTime.TotalSeconds;

                // Verwijder invulnerability wanneer de timer afloopt
                if (_invulnerabilityTimer <= 0)
                    IsInvulnerable = false;
            }
        }
    }
}