using Microsoft.Xna.Framework;
using projectSoftwareEngineering.Characters.Enemies.JumpingEnemy;
using projectSoftwareEngineering.Characters.Enemies.ShooterEnemy;
using projectSoftwareEngineering.Characters.Enemies.WalkingEnemy;
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
            int groundY = _screenHeight - 15;

            // Floors
            Floor floor1 = new Floor(_floorTexture, 0, groundY, 250, 30);
            Collidables.Add(floor1);

            Floor floor2 = new Floor(_floorTexture, 400, groundY, 200, 30);
            Collidables.Add(floor2);

            Floor floor3 = new Floor(_floorTexture, 730, groundY, 180, 30);
            Collidables.Add(floor3);

            Floor floor4 = new Floor(_floorTexture, 1070, groundY, 250, 30);
            Collidables.Add(floor4);

            Floor floor5 = new Floor(_floorTexture, 1460, groundY, 200, 30);
            Collidables.Add(floor5);

            Floor floor6 = new Floor(_floorTexture, 1830, groundY, 180, 30);
            Collidables.Add(floor6);

            Floor floor7 = new Floor(_floorTexture, 2130, groundY, 220, 30);
            Collidables.Add(floor7);

            Floor floor8 = new Floor(_floorTexture, 2500, groundY, 190, 30);
            Collidables.Add(floor8);

            Floor floor9 = new Floor(_floorTexture, 2820, groundY, 210, 30);
            Collidables.Add(floor9);

            Floor floor10 = new Floor(_floorTexture, 3170, groundY, 350, 30);
            Collidables.Add(floor10);

            //Walls
            Floor leftWall = new Floor(_wallTexture, -200, 0, 200, _screenHeight);
            Collidables.Add(leftWall);

            Floor rightWall = new Floor(_wallTexture, 3520, 0, 200, _screenHeight);
            Collidables.Add(rightWall);

            //Platforms
            Platform platform1 = new Platform(_platformTexture, 100, groundY - 80, 50, 15);
            Collidables.Add(platform1);

            Platform platform2 = new Platform(_platformTexture, 200, groundY - 140, 45, 15);
            Collidables.Add(platform2);

            Platform platform3 = new Platform(_platformTexture, 320, groundY - 100, 40, 15);
            Collidables.Add(platform3);

            Platform platform4 = new Platform(_platformTexture, 280, groundY - 200, 50, 15);
            Collidables.Add(platform4);

            Platform platform5 = new Platform(_platformTexture, 480, groundY - 60, 45, 15);
            Collidables.Add(platform5);

            Platform platform6 = new Platform(_platformTexture, 550, groundY - 130, 40, 15);
            Collidables.Add(platform6);

            Platform platform7 = new Platform(_platformTexture, 620, groundY - 200, 45, 15);
            Collidables.Add(platform7);

            Platform platform8 = new Platform(_platformTexture, 700, groundY - 280, 40, 15);
            Collidables.Add(platform8);

            Platform platform9 = new Platform(_platformTexture, 800, groundY - 180, 50, 15);
            Collidables.Add(platform9);

            Platform platform10 = new Platform(_platformTexture, 880, groundY - 100, 40, 15);
            Collidables.Add(platform10);

            Platform platform11 = new Platform(_platformTexture, 950, groundY - 220, 45, 15);
            Collidables.Add(platform11);

            Platform platform12 = new Platform(_platformTexture, 1040, groundY - 350, 40, 15);
            Collidables.Add(platform12);

            Platform platform13 = new Platform(_platformTexture, 1120, groundY - 280, 45, 15);
            Collidables.Add(platform13);

            Platform platform14 = new Platform(_platformTexture, 1200, groundY - 180, 40, 15);
            Collidables.Add(platform14);

            Platform platform15 = new Platform(_platformTexture, 1280, groundY - 100, 45, 15);
            Collidables.Add(platform15);

            Platform platform16 = new Platform(_platformTexture, 1370, groundY - 80, 50, 15);
            Collidables.Add(platform16);

            Platform platform17 = new Platform(_platformTexture, 1340, groundY - 160, 40, 15);
            Collidables.Add(platform17);

            Platform platform18 = new Platform(_platformTexture, 1420, groundY - 160, 40, 15);
            Collidables.Add(platform18);

            Platform platform19 = new Platform(_platformTexture, 1380, groundY - 250, 50, 15);
            Collidables.Add(platform19);

            Platform platform20 = new Platform(_platformTexture, 1310, groundY - 340, 40, 15);
            Collidables.Add(platform20);

            Platform platform21 = new Platform(_platformTexture, 1450, groundY - 340, 40, 15);
            Collidables.Add(platform21);

            Platform platform22 = new Platform(_platformTexture, 1380, groundY - 430, 50, 15);
            Collidables.Add(platform22);

            Platform platform23 = new Platform(_platformTexture, 1520, groundY - 360, 45, 15);
            Collidables.Add(platform23);

            Platform platform24 = new Platform(_platformTexture, 1600, groundY - 280, 40, 15);
            Collidables.Add(platform24);

            Platform platform25 = new Platform(_platformTexture, 1700, groundY - 300, 45, 15);
            Collidables.Add(platform25);

            Platform platform26 = new Platform(_platformTexture, 1780, groundY - 220, 40, 15);
            Collidables.Add(platform26);

            Platform platform27 = new Platform(_platformTexture, 1860, groundY - 140, 45, 15);
            Collidables.Add(platform27);

            Platform platform28 = new Platform(_platformTexture, 1940, groundY - 80, 40, 15);
            Collidables.Add(platform28);

            Platform platform29 = new Platform(_platformTexture, 2050, groundY - 150, 50, 15);
            Collidables.Add(platform29);

            Platform platform30 = new Platform(_platformTexture, 2150, groundY - 240, 40, 15);
            Collidables.Add(platform30);

            Platform platform31 = new Platform(_platformTexture, 2240, groundY - 320, 45, 15);
            Collidables.Add(platform31);

            Platform platform32 = new Platform(_platformTexture, 2340, groundY - 380, 40, 15);
            Collidables.Add(platform32);

            Platform platform33 = new Platform(_platformTexture, 2430, groundY - 300, 50, 15);
            Collidables.Add(platform33);

            Platform platform34 = new Platform(_platformTexture, 2620, groundY - 120, 45, 15);
            Collidables.Add(platform34);

            Platform platform35 = new Platform(_platformTexture, 2720, groundY - 200, 40, 15);
            Collidables.Add(platform35);

            Platform platform36 = new Platform(_platformTexture, 2810, groundY - 300, 50, 15);
            Collidables.Add(platform36);

            Platform platform37 = new Platform(_platformTexture, 2920, groundY - 400, 40, 15);
            Collidables.Add(platform37);

            Platform platform38 = new Platform(_platformTexture, 3010, groundY - 320, 45, 15);
            Collidables.Add(platform38);

            Platform platform39 = new Platform(_platformTexture, 3100, groundY - 220, 50, 15);
            Collidables.Add(platform39);

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
                new Vector2(500, groundY - 50),
                1
            );
            Enemies.Add(walker2);

            WalkingEnemy walker3 = new WalkingEnemy(
                _walkingEnemyWalkTexture,
                _walkingEnemyDieTexture,
                new Vector2(800, groundY - 50),
                1
            );
            Enemies.Add(walker3);

            WalkingEnemy walker4 = new WalkingEnemy(
                _walkingEnemyWalkTexture,
                _walkingEnemyDieTexture,
                new Vector2(1150, groundY - 50),
                1
            );
            Enemies.Add(walker4);

            WalkingEnemy walker5 = new WalkingEnemy(
                _walkingEnemyWalkTexture,
                _walkingEnemyDieTexture,
                new Vector2(1900, groundY - 50),
                1
            );
            Enemies.Add(walker5);

            WalkingEnemy walker6 = new WalkingEnemy(
                _walkingEnemyWalkTexture,
                _walkingEnemyDieTexture,
                new Vector2(2200, groundY - 50),
                1
            );
            Enemies.Add(walker6);

            WalkingEnemy walker7 = new WalkingEnemy(
                _walkingEnemyWalkTexture,
                _walkingEnemyDieTexture,
                new Vector2(2600, groundY - 50),
                1
            );
            Enemies.Add(walker7);

            WalkingEnemy walker8 = new WalkingEnemy(
                _walkingEnemyWalkTexture,
                _walkingEnemyDieTexture,
                new Vector2(2900, groundY - 50),
                1
            );
            Enemies.Add(walker8);

            WalkingEnemy walker9 = new WalkingEnemy(
                _walkingEnemyWalkTexture,
                _walkingEnemyDieTexture,
                new Vector2(3250, groundY - 50),
                1
            );
            Enemies.Add(walker9);

            JumpingEnemy jumper1 = new JumpingEnemy(
                _jumpingEnemyJumpTexture,
                _jumpingEnemyDieTexture,
                _jumpingEnemyIdleTexture,
                new Vector2(450, groundY - 50),
                1
            );
            Enemies.Add(jumper1);

            JumpingEnemy jumper2 = new JumpingEnemy(
                _jumpingEnemyJumpTexture,
                _jumpingEnemyDieTexture,
                _jumpingEnemyIdleTexture,
                new Vector2(950, groundY - 50),
                1
            );
            Enemies.Add(jumper2);

            JumpingEnemy jumper3 = new JumpingEnemy(
                _jumpingEnemyJumpTexture,
                _jumpingEnemyDieTexture,
                _jumpingEnemyIdleTexture,
                new Vector2(1400, groundY - 50),
                1
            );
            Enemies.Add(jumper3);

            JumpingEnemy jumper4 = new JumpingEnemy(
                _jumpingEnemyJumpTexture,
                _jumpingEnemyDieTexture,
                _jumpingEnemyIdleTexture,
                new Vector2(2330, groundY - 50),
                1
            );
            Enemies.Add(jumper4);

            JumpingEnemy jumper5 = new JumpingEnemy(
                _jumpingEnemyJumpTexture,
                _jumpingEnemyDieTexture,
                _jumpingEnemyIdleTexture,
                new Vector2(2850, groundY - 50),
                1
            );
            Enemies.Add(jumper5);

            JumpingEnemy jumper6 = new JumpingEnemy(
                _jumpingEnemyJumpTexture,
                _jumpingEnemyDieTexture,
                _jumpingEnemyIdleTexture,
                new Vector2(3080, groundY - 50),
                1
            );
            Enemies.Add(jumper6);

            JumpingEnemy jumper7 = new JumpingEnemy(
                _jumpingEnemyJumpTexture,
                _jumpingEnemyDieTexture,
                _jumpingEnemyIdleTexture,
                new Vector2(3380, groundY - 50),
                1
            );
            Enemies.Add(jumper7);

            //End coin
            Coin endCoin = new Coin(_coinTexture, 3450, groundY - 50, 25, 25);
            Collectibles.Add(endCoin);
        }
    }
}
