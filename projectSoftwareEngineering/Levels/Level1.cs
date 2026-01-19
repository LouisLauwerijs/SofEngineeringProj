using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using projectSoftwareEngineering.Characters.Enemies.JumpingEnemy;
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
            int groundY = _screenHeight - 15;

            //Floors
            Floor floor1 = new Floor(_floorTexture, 0, groundY, 400, 30);
            Collidables.Add(floor1);
            Floor floor2 = new Floor(_floorTexture, 450, groundY, 350, 30);
            Collidables.Add(floor2);
            Floor floor3 = new Floor(_floorTexture, 850, groundY, 500, 30);
            Collidables.Add(floor3);
            Floor floor4 = new Floor(_floorTexture, 1400, groundY, 600, 30);
            Collidables.Add(floor4);

           //Walls
            Floor leftWall = new Floor(_wallTexture, -200, 0, 200, _screenHeight);
            Collidables.Add(leftWall);

            Floor rightWall = new Floor(_wallTexture, 2000, 0, 200, _screenHeight);
            Collidables.Add(rightWall);


            //Platforms
            Platform platform1 = new Platform(_platformTexture, 410, groundY - 60, 60, 15);
            Collidables.Add(platform1);

            Platform platform2 = new Platform(_platformTexture, 950, groundY - 80, 80, 15);
            Collidables.Add(platform2);

            Platform platform3 = new Platform(_platformTexture, 1320, groundY - 50, 60, 15);
            Collidables.Add(platform3);

            //Enemies
            WalkingEnemy walker1 = new WalkingEnemy(
                _walkingEnemyWalkTexture,
                _walkingEnemyDieTexture,
                new Vector2(300, groundY - 50)
            );
            Enemies.Add(walker1);

            WalkingEnemy walker2 = new WalkingEnemy(
                _walkingEnemyWalkTexture,
                _walkingEnemyDieTexture,
                new Vector2(1000, groundY - 50)
            );
            Enemies.Add(walker2);

            WalkingEnemy walker3 = new WalkingEnemy(
                _walkingEnemyWalkTexture,
                _walkingEnemyDieTexture,
                new Vector2(1600, groundY - 50)
            );
            Enemies.Add(walker3);

            JumpingEnemy jumper1 = new JumpingEnemy(
                _jumpingEnemyJumpTexture,
                _jumpingEnemyDieTexture,
                _jumpingEnemyIdleTexture,
                new Vector2(1200, groundY - 50),
                1
            );
            Enemies.Add(jumper1);
            JumpingEnemy jumper2 = new JumpingEnemy(
                _jumpingEnemyJumpTexture,
                _jumpingEnemyDieTexture,
                _jumpingEnemyIdleTexture,
                new Vector2(1800, groundY - 50),
                1
            );
            Enemies.Add(jumper2);


            //End coin
            Coin coin = new Coin(_coinTexture, 1950, groundY - 50, 25, 25);
            Collectibles.Add(coin);
        }
    }
}
