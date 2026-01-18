using Microsoft.Xna.Framework;
using projectSoftwareEngineering.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projectSoftwareEngineering.Characters.Enemies.JumpingEnemy
{
    public class JumpingEnemyConfig: ICharacterConfig
    {
        private Vector2 _spawnPoint;

        public JumpingEnemyConfig(Vector2 spawnPoint)
        {
            _spawnPoint = spawnPoint;
        }

        public Vector2 StartPosition => _spawnPoint;

        public float Gravity => 0.4f;

        public float JumpStrength => -8.5f;

        public float MoveSpeed => 1.5f;
    }
}
