using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameStructures.Zones
{
    public class AttackTriggerZone : TriggerZone
    {
        [SerializeField]
        private List<int> _attacksId;

        public List<int> AttacksId => _attacksId;
    }
}

