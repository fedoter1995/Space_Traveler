using SpaceTraveler.GameStructures.Stats.Presets;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceTraveler.GameStructures.Stats
{
    [CreateAssetMenu(menuName = "Stats/DataBase")]
    public class StatPresetDataBase : ScriptableObject
    {
        [SerializeField]
        private string _name;
        [SerializeField]
        private List<StatPreset> _presets;

        public string Name => _name;
        public StatPreset GetStatPreset(string id)
        {
            var stats = _presets.FindAll(stat => stat.Id == id);
            StatPreset stat = null;
            switch(stats.Count)
            {
                case 1 :
                    {
                        stat = stats[0];
                        break;
                    }
                case > 1 :
                    {
                        throw new System.Exception("There are matching ID");
                    }
                case 0 :
                    {
                        throw new System.Exception($"No item with id = {id} found");
                    }
            }
            return stat;
        }
    }
}
