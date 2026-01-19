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
    public class Level3 : Level
    {
        public Level3(LevelConfig config)
            : base(config) { }

        public override void BuildLevel()
        {
            
        int groundY = _screenHeight - 15;

            //Floors
            Floor floor1 = new Floor(_floorTexture, 0, groundY, 200, 30);
            Collidables.Add(floor1);

            Floor floor2 = new Floor(_floorTexture, 320, groundY, 180, 30);
            Collidables.Add(floor2);

            Floor floor3 = new Floor(_floorTexture, 640, groundY, 220, 30);
            Collidables.Add(floor3);

            Floor floor4 = new Floor(_floorTexture, 990, groundY, 200, 30);
            Collidables.Add(floor4);

            Floor floor5 = new Floor(_floorTexture, 1340, groundY, 180, 30);
            Collidables.Add(floor5);

            Floor floor6 = new Floor(_floorTexture, 1680, groundY, 200, 30);
            Collidables.Add(floor6);

            Floor floor7 = new Floor(_floorTexture, 2020, groundY, 190, 30);
            Collidables.Add(floor7);

            Floor floor8 = new Floor(_floorTexture, 2340, groundY, 500, 30);
            Collidables.Add(floor8);

            //walls
            Floor leftWall = new Floor(_wallTexture, -200, 0, 200, _screenHeight);
            Collidables.Add(leftWall);

            Floor rightWall = new Floor(_wallTexture, 2840, 0, 200, _screenHeight);
            Collidables.Add(rightWall);

            Floor obstacle1 = new Floor(_wallTexture, 250, groundY - 60, 20, 60);
            Collidables.Add(obstacle1);

            Floor obstacle2 = new Floor(_wallTexture, 400, groundY - 80, 20, 80);
            Collidables.Add(obstacle2);
            Floor obstacle3 = new Floor(_wallTexture, 480, groundY - 80, 20, 80);
            Collidables.Add(obstacle3);

            Floor obstacle4 = new Floor(_wallTexture, 720, groundY - 40, 30, 40);
            Collidables.Add(obstacle4);

            Floor obstacle5 = new Floor(_wallTexture, 900, groundY - 100, 25, 100);
            Collidables.Add(obstacle5);

            Floor obstacle6 = new Floor(_wallTexture, 1050, groundY - 70, 20, 70);
            Collidables.Add(obstacle6);
            Floor obstacle7 = new Floor(_wallTexture, 1130, groundY - 70, 20, 70);
            Collidables.Add(obstacle7);

            Floor obstacle8 = new Floor(_wallTexture, 1420, groundY - 50, 25, 50);
            Collidables.Add(obstacle8);

            Floor obstacle9 = new Floor(_wallTexture, 1800, groundY - 120, 30, 120);
            Collidables.Add(obstacle9);

            Floor obstacle10 = new Floor(_wallTexture, 1900, groundY - 80, 20, 80);
            Collidables.Add(obstacle10);
            Floor obstacle11 = new Floor(_wallTexture, 1980, groundY - 80, 20, 80);
            Collidables.Add(obstacle11);

            Floor obstacle12 = new Floor(_wallTexture, 2200, groundY - 90, 25, 90);
            Collidables.Add(obstacle12);

            //Platforms
            Platform platform1 = new Platform(_platformTexture, 100, groundY - 50, 45, 15);
            Collidables.Add(platform1);

            Platform platform2 = new Platform(_platformTexture, 180, groundY - 100, 40, 15);
            Collidables.Add(platform2);

            Platform platform3 = new Platform(_platformTexture, 240, groundY - 160, 50, 15);
            Collidables.Add(platform3);

            Platform platform4 = new Platform(_platformTexture, 300, groundY - 100, 40, 15);
            Collidables.Add(platform4);

            Platform platform5 = new Platform(_platformTexture, 370, groundY - 120, 35, 15);
            Collidables.Add(platform5);

            Platform platform6 = new Platform(_platformTexture, 440, groundY - 180, 50, 15);
            Collidables.Add(platform6);

            Platform platform7 = new Platform(_platformTexture, 520, groundY - 140, 40, 15);
            Collidables.Add(platform7);

            Platform platform8 = new Platform(_platformTexture, 580, groundY - 80, 45, 15);
            Collidables.Add(platform8);

            Platform platform9 = new Platform(_platformTexture, 680, groundY - 60, 40, 15);
            Collidables.Add(platform9);

            Platform platform10 = new Platform(_platformTexture, 760, groundY - 140, 45, 15);
            Collidables.Add(platform10);

            Platform platform11 = new Platform(_platformTexture, 840, groundY - 220, 40, 15);
            Collidables.Add(platform11);

            Platform platform12 = new Platform(_platformTexture, 750, groundY - 300, 50, 15);
            Collidables.Add(platform12);

            Platform platform13 = new Platform(_platformTexture, 850, groundY - 260, 40, 15);
            Collidables.Add(platform13);

            Platform platform14 = new Platform(_platformTexture, 930, groundY - 180, 45, 15);
            Collidables.Add(platform14);

            Platform platform15 = new Platform(_platformTexture, 1020, groundY - 100, 40, 15);
            Collidables.Add(platform15);

            Platform platform16 = new Platform(_platformTexture, 1080, groundY - 200, 50, 15);
            Collidables.Add(platform16);

            Platform platform17 = new Platform(_platformTexture, 1160, groundY - 300, 40, 15);
            Collidables.Add(platform17);

            Platform platform18 = new Platform(_platformTexture, 1230, groundY - 400, 45, 15);
            Collidables.Add(platform18);

            Platform platform19 = new Platform(_platformTexture, 1310, groundY - 320, 40, 15);
            Collidables.Add(platform19);

            Platform platform20 = new Platform(_platformTexture, 1220, groundY - 220, 50, 15);
            Collidables.Add(platform20);

            Platform platform21 = new Platform(_platformTexture, 1460, groundY - 350, 45, 15);
            Collidables.Add(platform21);

            Platform platform22 = new Platform(_platformTexture, 1540, groundY - 280, 40, 15);
            Collidables.Add(platform22);

            Platform platform23 = new Platform(_platformTexture, 1620, groundY - 200, 45, 15);
            Collidables.Add(platform23);

            Platform platform24 = new Platform(_platformTexture, 1760, groundY - 80, 40, 15);
            Collidables.Add(platform24);

            Platform platform25 = new Platform(_platformTexture, 1840, groundY - 180, 50, 15);
            Collidables.Add(platform25);

            Platform platform26 = new Platform(_platformTexture, 1940, groundY - 300, 40, 15);
            Collidables.Add(platform26);

            Platform platform27 = new Platform(_platformTexture, 2040, groundY - 400, 45, 15);
            Collidables.Add(platform27);

            Platform platform28 = new Platform(_platformTexture, 2140, groundY - 480, 50, 15);
            Collidables.Add(platform28);

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
                new Vector2(740, groundY - 50),
                1
            );
            Enemies.Add(walker3);

            WalkingEnemy walker4 = new WalkingEnemy(
                _walkingEnemyWalkTexture,
                _walkingEnemyDieTexture,
                new Vector2(1090, groundY - 50),
                1
            );
            Enemies.Add(walker4);

            WalkingEnemy walker5 = new WalkingEnemy(
                _walkingEnemyWalkTexture,
                _walkingEnemyDieTexture,
                new Vector2(1580, groundY - 50),
                1
            );
            Enemies.Add(walker5);

            WalkingEnemy walker6 = new WalkingEnemy(
                _walkingEnemyWalkTexture,
                _walkingEnemyDieTexture,
                new Vector2(2100, groundY - 50),
                1
            );
            Enemies.Add(walker6);

            JumpingEnemy jumper1 = new JumpingEnemy(
                _jumpingEnemyJumpTexture,
                _jumpingEnemyDieTexture,
                _jumpingEnemyIdleTexture,
                new Vector2(340, groundY - 50),
                1
            );
            Enemies.Add(jumper1);

            JumpingEnemy jumper2 = new JumpingEnemy(
                _jumpingEnemyJumpTexture,
                _jumpingEnemyDieTexture,
                _jumpingEnemyIdleTexture,
                new Vector2(820, groundY - 50),
                1
            );
            Enemies.Add(jumper2);

            JumpingEnemy jumper3 = new JumpingEnemy(
                _jumpingEnemyJumpTexture,
                _jumpingEnemyDieTexture,
                _jumpingEnemyIdleTexture,
                new Vector2(1390, groundY - 50),
                1
            );
            Enemies.Add(jumper3);

            JumpingEnemy jumper4 = new JumpingEnemy(
                _jumpingEnemyJumpTexture,
                _jumpingEnemyDieTexture,
                _jumpingEnemyIdleTexture,
                new Vector2(1860, groundY - 50),
                1
            );
            Enemies.Add(jumper4);

            JumpingEnemy jumper5 = new JumpingEnemy(
                _jumpingEnemyJumpTexture,
                _jumpingEnemyDieTexture,
                _jumpingEnemyIdleTexture,
                new Vector2(2480, groundY - 50),
                1
            );
            Enemies.Add(jumper5);


            ShooterEnemy shooter1 = new ShooterEnemy(
                _shooterEnemyIdleTexture,
                _shooterEnemyAttackTexture,
                _shooterEnemyDieTexture,
                new Vector2(440, groundY - 220),
                Hero,
                3
            );
            Enemies.Add(shooter1);

            ShooterEnemy shooter2 = new ShooterEnemy(
                _shooterEnemyIdleTexture,
                _shooterEnemyAttackTexture,
                _shooterEnemyDieTexture,
                new Vector2(760, groundY - 320),
                Hero,
                3
            );
            Enemies.Add(shooter2);

            ShooterEnemy shooter3 = new ShooterEnemy(
                _shooterEnemyIdleTexture,
                _shooterEnemyAttackTexture,
                _shooterEnemyDieTexture,
                new Vector2(1230, groundY - 440),
                Hero,
                3
            );
            Enemies.Add(shooter3);

            ShooterEnemy shooter4 = new ShooterEnemy(
                _shooterEnemyIdleTexture,
                _shooterEnemyAttackTexture,
                _shooterEnemyDieTexture,
                new Vector2(1200, groundY - 50),
                Hero,
                3
            );
            Enemies.Add(shooter4);

            ShooterEnemy shooter5 = new ShooterEnemy(
                _shooterEnemyIdleTexture,
                _shooterEnemyAttackTexture,
                _shooterEnemyDieTexture,
                new Vector2(1540, groundY - 320),
                Hero,
                3
            );
            Enemies.Add(shooter5);

            ShooterEnemy shooter6 = new ShooterEnemy(
                _shooterEnemyIdleTexture,
                _shooterEnemyAttackTexture,
                _shooterEnemyDieTexture,
                new Vector2(2140, groundY - 520),
                Hero,
                3
            );
            Enemies.Add(shooter6);

            ShooterEnemy shooter7 = new ShooterEnemy(
                _shooterEnemyIdleTexture,
                _shooterEnemyAttackTexture,
                _shooterEnemyDieTexture,
                new Vector2(2500, groundY - 50),
                Hero,
                3
            );
            Enemies.Add(shooter7);

            //End coin
            Coin endCoin = new Coin(_coinTexture, 2750, groundY - 50, 25, 25);
            Collectibles.Add(endCoin);
        }
    }
}
