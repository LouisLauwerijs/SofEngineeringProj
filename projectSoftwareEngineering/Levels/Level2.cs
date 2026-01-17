using Microsoft.Xna.Framework;
using projectSoftwareEngineering.Characters.Enemies;
using projectSoftwareEngineering.Environment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projectSoftwareEngineering.Levels
{
    public class Level2 : Level
    {
        public Level2(LevelConfig config) : base(config)
        {
        }

        public override void BuildLevel()
        {
            // Floor
            Floor floor = new Floor(_floorTexture, 0, _screenHeight - 15, 1000, 30);
            Collidables.Add(floor);

            // Platforms
            Platform platform1 = new Platform(_platformTexture, 100, _screenHeight - 80, 60, 15);
            Collidables.Add(platform1);

            Platform platform2 = new Platform(_platformTexture, 250, _screenHeight - 150, 60, 15);
            Collidables.Add(platform2);

            Platform platform3 = new Platform(_platformTexture, 400, _screenHeight - 120, 60, 15);
            Collidables.Add(platform3);

            Platform platform4 = new Platform(_platformTexture, 550, _screenHeight - 180, 60, 15);
            Collidables.Add(platform4);

            // Walls
            Floor leftWall = new Floor(_wallTexture, -155, 0, 160, _screenHeight);
            Collidables.Add(leftWall);

            // Multiple enemies
            WalkingEnemy enemy1 = new WalkingEnemy(_enemyTexture, new Vector2(200, 100), 1);
            Enemies.Add(enemy1);

            WalkingEnemy enemy2 = new WalkingEnemy(_enemyTexture, new Vector2(450, 80), 1);
            Enemies.Add(enemy2);

            JumpingEnemy jumpingEnemy = new JumpingEnemy(_enemyTexture, new Vector2(350, 100), 1);
            Enemies.Add(jumpingEnemy);

            Spike spike1 = new Spike(_spikeTexture, 180, _screenHeight - 35, 20, 20);
            Spikes.Add(spike1);
            Collidables.Add(spike1);

            Spike spike2 = new Spike(_spikeTexture, 320, _screenHeight - 35, 20, 20);
            Spikes.Add(spike2);
            Collidables.Add(spike2);

            //Completion coin
            Coin coin = new Coin(_cointTexture, (_screenWidth - 500), (_screenHeight - 50), 25, 25);
            Collectibles.Add(coin);
        }
    }
}
