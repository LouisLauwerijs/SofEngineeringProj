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
        public int VirtualWidth { get; }
        public int VirtualHeight { get; }
        public Texture2D HeroTexture { get; }
        public Texture2D FloorTexture { get; }
        public Texture2D PlatformTexture { get; }
        public Texture2D WallTexture { get; }
        public Texture2D EnemyTexture { get; }
        public Texture2D SpikeTexture { get; }
        public Texture2D CoinTexture { get; }

        public LevelConfig(
            GraphicsDevice graphicsDevice,
            int virtualWidth,
            int virtualHeight,
            Texture2D heroTexture,
            Texture2D floorTexture,
            Texture2D platformTexture,
            Texture2D wallTexture,
            Texture2D enemyTexture,
            Texture2D spikeTexture,
            Texture2D coinTexture)
        {
            GraphicsDevice = graphicsDevice;
            VirtualWidth = virtualWidth;
            VirtualHeight = virtualHeight;
            HeroTexture = heroTexture;
            FloorTexture = floorTexture;
            PlatformTexture = platformTexture;
            WallTexture = wallTexture;
            EnemyTexture = enemyTexture;
            SpikeTexture = spikeTexture;
            CoinTexture = coinTexture;
        }
    }
}
