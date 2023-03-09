using GameStructures.Stats;
using System.Collections.Generic;

namespace GameStructures.Abilities
{
    public class PassiveSkill : Ability
    {
        public List<StatModifier> Modifiers => _statModifiers;
    }
}
