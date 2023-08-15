using SpaceTraveler.GameStructures.Abilities;
using SpaceTraveler.GameStructures.Stats.StatModifiers;
using System.Collections.Generic;

namespace SpaceTraveler.GameStructures.Abilities
{
    public class PassiveSkill : Ability
    {
        public List<StatModifier> Modifiers => _statModifiers;



        public List<StatModifier> GetAllModifiers()
        {
            return _statModifiers;
        }
    }
}
