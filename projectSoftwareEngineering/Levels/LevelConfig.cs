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
        public Texture2D RunningEnemyTexture { get; }
        public Texture2D JumpingEnemyTexture { get; }
        public Texture2D ShooterEnemyTexture { get; }
        public Texture2D SpikeTexture { get; }
        public Texture2D CoinTexture { get; }

        public LevelConfig(
            GraphicsDevice graphicsDevice,
            int renderWidht,
            int renderHeight,
            Texture2D heroTexture,
            Texture2D floorTexture,
            Texture2D platformTexture,
            Texture2D wallTexture,
            Texture2D walkingEnemyTexture,
            Texture2D jumpingEnemyTexture,
            Texture2D shooterEnemyTexture,
            Texture2D spikeTexture,
            Texture2D coinTexture)
        {
            GraphicsDevice = graphicsDevice;
            RenderWidth = renderWidht;
            RenderHeight = renderHeight;
            HeroTexture = heroTexture;
            FloorTexture = floorTexture;
            PlatformTexture = platformTexture;
            WallTexture = wallTexture;
            RunningEnemyTexture = walkingEnemyTexture;
            JumpingEnemyTexture = jumpingEnemyTexture;
            ShooterEnemyTexture = shooterEnemyTexture;
            SpikeTexture = spikeTexture;
            CoinTexture = coinTexture;
        }
    }
}
