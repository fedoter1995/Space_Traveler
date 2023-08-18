using UnityEngine;

namespace SpaceTraveler.GameStructures.Actions
{
    public class Action : ScriptableObject
    {
        [SerializeField]
        private string _name;
        [SerializeField]
        private string _id;
        
        public string Name => _name;

        public string Id => _id;
    }

    public enum ActionType
    {
        EvadeHit
    }
}
