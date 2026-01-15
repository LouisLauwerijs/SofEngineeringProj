using projectSoftwareEngineering.Characters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projectSoftwareEngineering.Interfaces
{
    public interface IDamageable
    {
        Health Health { get; }
        void Die();
    }
}
