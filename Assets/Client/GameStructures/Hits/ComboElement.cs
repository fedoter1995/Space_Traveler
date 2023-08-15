using System;
using UnityEngine;

namespace SpaceTraveler.GameStructures.Hits
{
    [Serializable]
    public class ComboElement
    {
        [SerializeField]
        private int _animationId;
        [SerializeField]
        private AddedModifiers _addedModifiers;


        public int AnimationId => _animationId;
        public AddedModifiers AddedModifiers => _addedModifiers;
    }
}
