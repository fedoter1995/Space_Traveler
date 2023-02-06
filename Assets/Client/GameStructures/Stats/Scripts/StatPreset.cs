using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Stats
{
    [CreateAssetMenu(menuName = "Stats/New_Stat_Preset")]
    public class StatPreset : ScriptableObject
    {
        [SerializeField]
        private string _statID = "";
        [SerializeField]
        private string _statName = "New stat preset";


        public string Id => _statID;
        public string Name => _statName;
    }
}

