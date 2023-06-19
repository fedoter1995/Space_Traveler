using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace GameStructures.Stats
{
    [CreateAssetMenu(menuName = "Buffs/New_BuffPreset")]
    public class BuffPreset : ScriptableObject
    {
        [SerializeField]
        private List<StatModifier> _modifiers = new List<StatModifier>();

        public List<StatModifier> Modifiers => _modifiers;
    }
}
