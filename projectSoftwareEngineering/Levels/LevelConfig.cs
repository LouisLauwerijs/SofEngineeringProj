using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projectSoftwareEngineering.Levels
{
    public class LevelConfig
    {
        public GraphicsDevice GraphicsDevice { get; }
        public int RenderWidth { get; }
        public int RenderHeight { get; }
        public Texture2D HeroTexture { get; }
        public Texture2D FloorTexture { get; }
        public Texture2D PlatformTexture { get; }
        public Texture2D WallTexture { get; }

        public Texture2D WalkingEnemyWalkTexture { get; }
        public Texture2D WalkingEnemyDieTexture { get; }
        public Texture2D JumpingEnemyJumpTexture { get; }
        public Texture2D JumpingEnemyDieTexture { get; }
        public Texture2D JumpingEnemyIdleTexture { get; }
        public Texture2D ShooterEnemyIdleTexture { get; }
        public Texture2D ShooterEnemyAttackTexture { get; }
        public Texture2D ShooterEnemyDieTexture { get; }

        public Texture2D SpikeTexture { get; }
        public Texture2D CoinTexture { get; }

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
