using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projectSoftwareEngineering.Levels
{
    public class LevelFactory
    {
        private LevelConfig _config;

        public LevelFactory(LevelConfig config)
        {
            _config = config;
        }

        public Level CreateLevel(int level)
        {
            switch (level)
            {
                case 1:
                    return new Level1(_config);
                case 2:
                    return new Level2(_config);
                case 3:
                    return new Level3(_config);
                default:
                    return new Level1(_config);
            }
        }

    }
}
