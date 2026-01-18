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
    }
}
