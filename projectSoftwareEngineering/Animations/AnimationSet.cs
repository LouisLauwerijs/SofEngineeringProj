using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projectSoftwareEngineering.Animations
{
    public class AnimationSet
    {
        public Animation Idle { get; set; }
        public Animation Run { get; set; }
        public Animation Jump { get; set; }
        public Animation Die { get; set; }
        public Animation Attack { get; set; }

        //enemies
        public Animation WalkerEnemyWalk { get; set; }
        public Animation WalkerEnemyDie { get; set; }
        public Animation JumperJump { get; set; }
        public Animation JumperIdle { get; set; }
        public Animation JumperDie { get; set; }
        public Animation ShooterIdle { get; set; }
        public Animation ShooterAttack { get; set; }
        public Animation ShooterDie { get; set; }

        public Animation GetFirstAvailableAnimation()
        {
            return Idle
                ?? Run
                ?? Jump
                ?? Attack
                ?? WalkerEnemyWalk
                ?? JumperJump
                ?? JumperIdle
                ?? ShooterIdle
                ?? WalkerEnemyDie
                ?? JumperDie
                ?? ShooterAttack
                ?? ShooterDie;
        }
    }
}
