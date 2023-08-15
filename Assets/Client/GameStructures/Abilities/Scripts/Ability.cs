using SpaceTraveler.GameStructures.Stats.StatModifiers;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceTraveler.GameStructures.Abilities
{
    public abstract class Ability
    {
        [SerializeField]
        protected List<StatModifier> _statModifiers;
    }
}
