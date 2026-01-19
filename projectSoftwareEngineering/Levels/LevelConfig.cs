using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projectSoftwareEngineering.Levels
{
    /// <summary>
    /// Configuratie klasse die alle resources en settings bevat die nodig zijn voor level creatie.
    /// Centraliseert alle dependencies die een Level nodig heeft op één plek.
    /// Gebruikt als parameter voor Level constructors om dependency injection te vergemakkelijken.
    /// 
    /// SOLID Principes:
    /// - Single Responsibility Principle (SRP): Enige verantwoordelijkheid is het groeperen
    ///   en doorgeven van level configuratie data.
    /// - Dependency Injection Pattern: Alle dependencies worden via constructor doorgegeven,
    ///   wat testing en flexibility verbetert.
    /// - Immutability: Alle properties zijn readonly (get only), configuratie kan niet gewijzigd
    ///   worden na constructie, wat bugs voorkomt.
    /// 
    /// Design Pattern: Parameter Object Pattern
    /// - Vermijdt lange parameter lijsten door gerelateerde parameters te groeperen
    /// - Maakt het toevoegen van nieuwe configuratie opties makkelijker
    /// </summary>
    public class LevelConfig
    {
        #region Graphics Configuration

        /// <summary>
        /// Graphics device voor rendering operaties.
        /// </summary>
        public GraphicsDevice GraphicsDevice { get; }

        /// <summary>
        /// Breedte van de render target in pixels.
        /// </summary>
        public int RenderWidth { get; }

        /// <summary>
        /// Hoogte van de render target in pixels.
        /// </summary>
        public int RenderHeight { get; }

        #endregion

        #region Character Textures

        /// <summary>
        /// Sprite sheet texture voor de Hero.
        /// </summary>
        public Texture2D HeroTexture { get; }

        #endregion

        #region Environment Textures

        /// <summary>
        /// Texture voor floor (grond) objecten.
        /// </summary>
        public Texture2D FloorTexture { get; }

        /// <summary>
        /// Texture voor platform objecten.
        /// </summary>
        public Texture2D PlatformTexture { get; }

        /// <summary>
        /// Texture voor wall (muur) objecten.
        /// </summary>
        public Texture2D WallTexture { get; }

        #endregion

        #region Walking Enemy Textures

        /// <summary>
        /// Texture voor walking enemy walk animatie.
        /// </summary>
        public Texture2D WalkingEnemyWalkTexture { get; }

        /// <summary>
        /// Texture voor walking enemy death animatie.
        /// </summary>
        public Texture2D WalkingEnemyDieTexture { get; }

        #endregion

        #region Jumping Enemy Textures

        /// <summary>
        /// Texture voor jumping enemy jump animatie.
        /// </summary>
        public Texture2D JumpingEnemyJumpTexture { get; }

        /// <summary>
        /// Texture voor jumping enemy death animatie.
        /// </summary>
        public Texture2D JumpingEnemyDieTexture { get; }

        /// <summary>
        /// Texture voor jumping enemy idle animatie.
        /// </summary>
        public Texture2D JumpingEnemyIdleTexture { get; }

        #endregion

        #region Shooter Enemy Textures

        /// <summary>
        /// Texture voor shooter enemy idle animatie.
        /// </summary>
        public Texture2D ShooterEnemyIdleTexture { get; }

        /// <summary>
        /// Texture voor shooter enemy attack animatie.
        /// </summary>
        public Texture2D ShooterEnemyAttackTexture { get; }

        /// <summary>
        /// Texture voor shooter enemy death animatie.
        /// </summary>
        public Texture2D ShooterEnemyDieTexture { get; }

        #endregion

        #region Hazard and Collectible Textures

        /// <summary>
        /// Texture voor spike hazard objecten.
        /// </summary>
        public Texture2D SpikeTexture { get; }

        /// <summary>
        /// Texture voor coin collectible objecten.
        /// </summary>
        public Texture2D CoinTexture { get; }

        #endregion

        /// <summary>
        /// Constructor die alle configuratie waarden initialiseert.
        /// Alle parameters zijn verplicht, er zijn geen defaults.
        /// Dit zorgt ervoor dat Levels altijd volledig geconfigureerd zijn.
        /// </summary>
        /// <param name="graphicsDevice">Graphics device voor rendering</param>
        /// <param name="renderWidth">Breedte van de render target</param>
        /// <param name="renderHeight">Hoogte van de render target</param>
        /// <param name="heroTexture">Hero sprite sheet</param>
        /// <param name="floorTexture">Floor texture</param>
        /// <param name="platformTexture">Platform texture</param>
        /// <param name="wallTexture">Wall texture</param>
        /// <param name="walkingEnemyWalkTexture">Walking enemy walk animation</param>
        /// <param name="walkingEnemyDieTexture">Walking enemy death animation</param>
        /// <param name="jumpingEnemyJumpTexture">Jumping enemy jump animation</param>
        /// <param name="jumpingEnemyDieTexture">Jumping enemy death animation</param>
        /// <param name="jumpingEnemyIdleTexture">Jumping enemy idle animation</param>
        /// <param name="shooterEnemyIdleTexture">Shooter enemy idle animation</param>
        /// <param name="shooterEnemyAttackTexture">Shooter enemy attack animation</param>
        /// <param name="shooterEnemyDieTexture">Shooter enemy death animation</param>
        /// <param name="spikeTexture">Spike hazard texture</param>
        /// <param name="coinTexture">Coin collectible texture</param>
        public LevelConfig(
            GraphicsDevice graphicsDevice,
            int renderWidth,
            int renderHeight,
            Texture2D heroTexture,
            Texture2D floorTexture,
            Texture2D platformTexture,
            Texture2D wallTexture,
            Texture2D walkingEnemyWalkTexture,
            Texture2D walkingEnemyDieTexture,
            Texture2D jumpingEnemyJumpTexture,
            Texture2D jumpingEnemyDieTexture,
            Texture2D jumpingEnemyIdleTexture,
            Texture2D shooterEnemyIdleTexture,
            Texture2D shooterEnemyAttackTexture,
            Texture2D shooterEnemyDieTexture,
            Texture2D spikeTexture,
            Texture2D coinTexture)
        {
            // Initialiseer alle properties met constructor parameters
            GraphicsDevice = graphicsDevice;
            RenderWidth = renderWidth;
            RenderHeight = renderHeight;
            HeroTexture = heroTexture;
            FloorTexture = floorTexture;
            PlatformTexture = platformTexture;
            WallTexture = wallTexture;
            WalkingEnemyWalkTexture = walkingEnemyWalkTexture;
            WalkingEnemyDieTexture = walkingEnemyDieTexture;
            JumpingEnemyJumpTexture = jumpingEnemyJumpTexture;
            JumpingEnemyDieTexture = jumpingEnemyDieTexture;
            JumpingEnemyIdleTexture = jumpingEnemyIdleTexture;
            ShooterEnemyIdleTexture = shooterEnemyIdleTexture;
            ShooterEnemyAttackTexture = shooterEnemyAttackTexture;
            ShooterEnemyDieTexture = shooterEnemyDieTexture;
            SpikeTexture = spikeTexture;
            CoinTexture = coinTexture;
        }
    }
}