using GameStructures.Stats;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace GameStructures.Hits
{
    [Serializable]
    public  class AddedModifiers
    {
        [SerializeField]
        private List<Damage> _addedDamages;
        [SerializeField]
        private List<Multiplier> _addedMultipliers;
        [SerializeField]
        private List<Chance> _addedChances;

        [SerializeField]
        public List<Damage> AddedDamages => _addedDamages;
        [SerializeField]
        public List<Multiplier> AddedMultipliers => _addedMultipliers;
        [SerializeField]
        public List<Chance> AddedChances => _addedChances;
    }
}
