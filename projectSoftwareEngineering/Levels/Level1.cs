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
            Floor floor1 = new Floor(_floorTexture, 0, groundY, 300, 30);
            Collidables.Add(floor1);

            Floor floor2 = new Floor(_floorTexture, 380, groundY, 180, 30);
            Collidables.Add(floor2);

            Floor floor3 = new Floor(_floorTexture, 660, groundY, 220, 30);
            Collidables.Add(floor3);
        
            Floor floor4 = new Floor(_floorTexture, 970, groundY, 150, 30);
            Collidables.Add(floor4);
       
            Floor floor5 = new Floor(_floorTexture, 1230, groundY, 250, 30);
            Collidables.Add(floor5);
       
            Floor floor6 = new Floor(_floorTexture, 1565, groundY, 140, 30);
            Collidables.Add(floor6);
        
            Floor floor7 = new Floor(_floorTexture, 1825, groundY, 300, 30);
            Collidables.Add(floor7);
        
            Floor floor8 = new Floor(_floorTexture, 2220, groundY, 160, 30);
            Collidables.Add(floor8);
        
            Floor floor9 = new Floor(_floorTexture, 2480, groundY, 120, 30);
            Collidables.Add(floor9);
       
            Floor floor10 = new Floor(_floorTexture, 2730, groundY, 280, 30);
            Collidables.Add(floor10);
        
            Floor floor11 = new Floor(_floorTexture, 3090, groundY, 130, 30);
            Collidables.Add(floor11);
        
            Floor floor12 = new Floor(_floorTexture, 3310, groundY, 150, 30);
            Collidables.Add(floor12);
        
            Floor floor13 = new Floor(_floorTexture, 3560, groundY, 200, 30);
            Collidables.Add(floor13);
        
            Floor floor14 = new Floor(_floorTexture, 3845, groundY, 140, 30);
            Collidables.Add(floor14);
        
            Floor floor15 = new Floor(_floorTexture, 4100, groundY, 400, 30);
            Collidables.Add(floor15);

            //Walls
            Floor leftWall = new Floor(_wallTexture, -200, 0, 200, _screenHeight);
            Collidables.Add(leftWall);

            Floor rightWall = new Floor(_wallTexture, 4500, 0, 200, _screenHeight);
            Collidables.Add(rightWall);

            //Platfrms
            Platform platform1 = new Platform(_platformTexture, 180, groundY - 50, 50, 15);
            Collidables.Add(platform1);

            Platform platform2 = new Platform(_platformTexture, 280, groundY - 100, 40, 15);
            Collidables.Add(platform2);

            Platform platform3 = new Platform(_platformTexture, 480, groundY - 70, 45, 15);
            Collidables.Add(platform3);

            Platform platform4 = new Platform(_platformTexture, 600, groundY - 40, 35, 15);
            Collidables.Add(platform4);

       
            Platform platform5 = new Platform(_platformTexture, 780, groundY - 60, 50, 15);
            Collidables.Add(platform5);

            Platform platform6 = new Platform(_platformTexture, 880, groundY - 120, 45, 15);
            Collidables.Add(platform6);

            Platform platform7 = new Platform(_platformTexture, 1050, groundY - 80, 40, 15);
            Collidables.Add(platform7);

            Platform platform8 = new Platform(_platformTexture, 1150, groundY - 150, 35, 15);
            Collidables.Add(platform8);

            Platform platform9 = new Platform(_platformTexture, 1320, groundY - 55, 45, 15);
            Collidables.Add(platform9);

            Platform platform10 = new Platform(_platformTexture, 1430, groundY - 110, 40, 15);
            Collidables.Add(platform10);

            Platform platform11 = new Platform(_platformTexture, 1670, groundY - 90, 50, 15);
            Collidables.Add(platform11);

            Platform platform12 = new Platform(_platformTexture, 1760, groundY - 140, 35, 15);
            Collidables.Add(platform12);

            Platform platform13 = new Platform(_platformTexture, 1920, groundY - 50, 50, 15);
            Collidables.Add(platform13);

            Platform platform14 = new Platform(_platformTexture, 2050, groundY - 100, 45, 15);
            Collidables.Add(platform14);

            Platform platform15 = new Platform(_platformTexture, 2150, groundY - 70, 35, 15);
            Collidables.Add(platform15);

            Platform platform16 = new Platform(_platformTexture, 2340, groundY - 110, 40, 15);
            Collidables.Add(platform16);

            Platform platform17 = new Platform(_platformTexture, 2580, groundY - 80, 30, 15);
            Collidables.Add(platform17);

            Platform platform18 = new Platform(_platformTexture, 2650, groundY - 130, 35, 15);
            Collidables.Add(platform18);

            Platform platform19 = new Platform(_platformTexture, 2840, groundY - 60, 50, 15);
            Collidables.Add(platform19);

            Platform platform20 = new Platform(_platformTexture, 2950, groundY - 120, 40, 15);
            Collidables.Add(platform20);

            Platform platform21 = new Platform(_platformTexture, 3170, groundY - 50, 30, 15);
            Collidables.Add(platform21);

            Platform platform22 = new Platform(_platformTexture, 3430, groundY - 80, 40, 15);
            Collidables.Add(platform22);

            Platform platform23 = new Platform(_platformTexture, 3680, groundY - 60, 35, 15);
            Collidables.Add(platform23);

            Platform platform24 = new Platform(_platformTexture, 3920, groundY - 100, 45, 15);
            Collidables.Add(platform24);

            Platform platform25 = new Platform(_platformTexture, 4020, groundY - 160, 35, 15);
            Collidables.Add(platform25);

            //Enemies
            WalkingEnemy walker1 = new WalkingEnemy(
                _walkingEnemyWalkTexture,
                _walkingEnemyDieTexture,
                new Vector2(100, groundY - 50),
                1
            );
            Enemies.Add(walker1);

            WalkingEnemy walker2 = new WalkingEnemy(
                _walkingEnemyWalkTexture,
                _walkingEnemyDieTexture,
                new Vector2(420, groundY - 50),
                1
            );
            Enemies.Add(walker2);

            WalkingEnemy walker3 = new WalkingEnemy(
                _walkingEnemyWalkTexture,
                _walkingEnemyDieTexture,
                new Vector2(720, groundY - 50),
                1
            );
            Enemies.Add(walker3);

            WalkingEnemy walker4 = new WalkingEnemy(
                _walkingEnemyWalkTexture,
                _walkingEnemyDieTexture,
                new Vector2(1020, groundY - 50),
                1
            );
            Enemies.Add(walker4);

            WalkingEnemy walker5 = new WalkingEnemy(
                _walkingEnemyWalkTexture,
                _walkingEnemyDieTexture,
                new Vector2(1350, groundY - 50),
                1
            );
            Enemies.Add(walker5);

            WalkingEnemy walker6 = new WalkingEnemy(
                _walkingEnemyWalkTexture,
                _walkingEnemyDieTexture,
                new Vector2(1630, groundY - 50),
                1
            );
            Enemies.Add(walker6);

            WalkingEnemy walker7 = new WalkingEnemy(
                _walkingEnemyWalkTexture,
                _walkingEnemyDieTexture,
                new Vector2(1950, groundY - 50),
                1
            );
            Enemies.Add(walker7);

            WalkingEnemy walker8 = new WalkingEnemy(
                _walkingEnemyWalkTexture,
                _walkingEnemyDieTexture,
                new Vector2(2320, groundY - 50),
                1
            );
            Enemies.Add(walker8);

            WalkingEnemy walker9 = new WalkingEnemy(
                _walkingEnemyWalkTexture,
                _walkingEnemyDieTexture,
                new Vector2(2850, groundY - 50),
                1
            );
            Enemies.Add(walker9);

            WalkingEnemy walker10 = new WalkingEnemy(
                _walkingEnemyWalkTexture,
                _walkingEnemyDieTexture,
                new Vector2(3650, groundY - 50),
                1
            );
            Enemies.Add(walker10);

            //End coin
            Coin endCoin = new Coin(_coinTexture, 4400, groundY - 50, 25, 25);
            Collectibles.Add(endCoin);
        }
    }
}
