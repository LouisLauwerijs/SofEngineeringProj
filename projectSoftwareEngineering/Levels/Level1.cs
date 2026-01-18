using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using projectSoftwareEngineering.Characters.Enemies.WalkingEnemy;
using projectSoftwareEngineering.Environment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projectSoftwareEngineering.Levels
{
    public class Level1 : Level
    {
        public Level1(LevelConfig config) 
            : base(config){}

        public override void BuildLevel()
        {
            // Floors
            Floor floor = new Floor(_floorTexture, 0, _screenHeight - 15, 250, 30);
            Collidables.Add(floor);
            Floor floor2 = new Floor(_floorTexture, 300, _screenHeight - 15, 500, 30);
            Collidables.Add(floor2);
            Floor floor3 = new Floor(_floorTexture, 550, _screenHeight - 15, 800, 30);
            Collidables.Add(floor3);
            Floor floor4 = new Floor(_floorTexture, 850, _screenHeight - 15, 1600, 30);
            Collidables.Add(floor4);
            Floor floor5 = new Floor(_floorTexture, 1800, _screenHeight - 15, 2000, 30);
            Collidables.Add(floor5);


            // Platforms
            Platform platform1 = new Platform(_platformTexture, 80, _screenHeight - 80, 50, 15);
            Collidables.Add(platform1);

            Platform platform2 = new Platform(_platformTexture, 220, _screenHeight - 130, 50, 15);
            Collidables.Add(platform2);

            Platform platform3 = new Platform(_platformTexture, 360, _screenHeight - 180, 50, 15);
            Collidables.Add(platform3);

            // Walls
            Floor leftWall = new Floor(_wallTexture, -155, 0, 160, _screenHeight);
            Collidables.Add(leftWall);

            // Enemy with platform
            WalkingEnemy enemy1 = new WalkingEnemy(
                _walkingEnemyWalkTexture,  
                _walkingEnemyDieTexture,  
                new Vector2(300, 100),     
                1
            );
            Enemies.Add(enemy1);
            Platform enemyPlatform = new Platform(_platformTexture, 300, 200, 80, 15);
            Collidables.Add(enemyPlatform);

            // Spike hazard
            Spike spike = new Spike(_spikeTexture, 180, _screenHeight - 35, 20, 20);
            Spikes.Add(spike);
            Collidables.Add(spike);



            //Completion coin
            Coin coin = new Coin(_cointTexture, 1800, _screenHeight - 50, 25, 25);
            Collectibles.Add(coin);
        }
    }
}
