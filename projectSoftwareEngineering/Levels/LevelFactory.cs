using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projectSoftwareEngineering.Levels
{
    /// <summary>
    /// Factory klasse voor het creëren van Level objecten.
    /// Encapsuleert de creatie logica en kiest het juiste Level type op basis van level nummer.
    /// 
    /// SOLID Principes:
    /// - Single Responsibility Principle (SRP): Enige verantwoordelijkheid is het construeren
    ///   van Level objecten op basis van een level identifier.
    /// - Open/Closed Principle (OCP): Open voor uitbreiding (nieuwe levels toevoegen aan switch)
    ///   maar vereist wel modificatie van de switch statement. Dit kan verbeterd worden met
    ///   een dictionary-based approach of strategy pattern.
    /// - Dependency Inversion Principle (DIP): Retourneert Level abstractie in plaats van
    ///   concrete level implementaties.
    /// - Factory Pattern: Encapsuleert object creatie logica, client code hoeft niet te weten
    ///   welke concrete Level class geïnstantieerd wordt.
    /// 
    /// Design Considerations:
    /// - Switch statement is simpel maar vereist modificatie voor nieuwe levels
    /// - Alternatief: Dictionary<int, Func<LevelConfig, Level>> voor true OCP compliance
    /// - Default fallback naar Level1 zorgt voor graceful handling van ongeldige level nummers
    /// </summary>
    public class LevelFactory
    {
        /// <summary>
        /// LevelConfig die gebruikt wordt voor alle gecreëerde levels.
        /// Wordt gezet via constructor en hergebruikt voor elk level.
        /// </summary>
        private LevelConfig _config;

        /// <summary>
        /// Constructor die de factory initialiseert met een LevelConfig.
        /// Deze configuratie wordt gebruikt voor alle levels die deze factory creëert.
        /// </summary>
        /// <param name="config">Configuratie object met alle benodigde resources voor level creatie</param>
        public LevelFactory(LevelConfig config)
        {
            _config = config;
        }

        /// <summary>
        /// Creëert een Level object op basis van het opgegeven level nummer.
        /// Gebruikt een switch statement om de juiste Level subclass te instantiëren.
        /// Fallback naar Level1 als het level nummer niet herkend wordt.
        /// </summary>
        /// <param name="level">Level nummer (1, 2, 3, etc.)</param>
        /// <returns>
        /// Nieuwe instantie van de gevraagde Level subclass (Level1, Level2, Level3)
        /// of Level1 als default voor onbekende level nummers
        /// </returns>
        public Level CreateLevel(int level)
        {
            // Switch op level nummer om juiste Level type te selecteren
            switch (level)
            {
                case 1:
                    // Level 1: Introductie level met alleen walking enemies
                    return new Level1(_config);

                case 2:
                    // Level 2: Intermediate level met walking en jumping enemies
                    return new Level2(_config);

                case 3:
                    // Level 3: Advanced level met alle enemy types (walking, jumping, shooter)
                    return new Level3(_config);

                default:
                    // Fallback: gebruik Level1 voor ongeldige level nummers
                    // Alternatief: throw exception of return null
                    return new Level1(_config);
            }
        }
    }
}