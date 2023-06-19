using System;
using System.Collections.Generic;
using UnityEngine;

namespace GameStructures.Hits
{
    [Serializable]
    public class ComboElement
    {
        [SerializeField]
        private float _animatorRef;
        [SerializeField]
        private AddedModifiers _addedModifiers;


        public float AnimatorVarRef => _animatorRef;
        public AddedModifiers AddedModifiers => _addedModifiers;
    }
}
