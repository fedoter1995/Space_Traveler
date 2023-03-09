using GameStructures.Stats;
using System.Collections.Generic;
using UnityEngine;

namespace GameStructures.Abilities
{
    public abstract class Ability
    {
        [SerializeField]
        protected List<StatModifier> _statModifiers;
    }
}
