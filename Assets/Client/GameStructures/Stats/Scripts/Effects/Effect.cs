using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Stats
{
    [System.Serializable]
    public class Effect
    {
        [SerializeField]
        private string _name = "New Effect";
        [SerializeField]
        private List<StatModifier> _modifiers = new List<StatModifier>();
        [SerializeField]
        private EffectType _type = EffectType.Permanent;
        [SerializeField]
        private float _duration = 0;

        public string Name => _name;
        public List<StatModifier> Modifiers => _modifiers;
        public float Duration => _type == EffectType.Permanent ? 0 : _duration;
        public EffectType Type => _type;
    }
    public enum EffectType
    {
        Temporary,
        Permanent
    }
}

