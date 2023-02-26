using Stats;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStructures.Abilities
{
    public class PassiveSkill : Ability
    {
        public List<StatModifier> Modifiers => _statModifiers;
    }
}
