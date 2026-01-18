using Microsoft.Xna.Framework;
using projectSoftwareEngineering.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projectSoftwareEngineering.Characters.Enemies.ShooterEnemy
{
    public class ShooterEnemyConfig : ICharacterConfig
    {
        private Vector2 _spawnPoint;

        public ShooterEnemyConfig(Vector2 spawnPoint)
        {
            _spawnPoint = spawnPoint;
        }
        public Vector2 StartPosition => _spawnPoint;

        public float Gravity => 0.4f;

        public float JumpStrength => 0;

        public float MoveSpeed => 0;
    }

}
