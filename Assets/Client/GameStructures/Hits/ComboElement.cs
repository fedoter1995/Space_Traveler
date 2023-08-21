using SpaceTraveler.GameStructures.Hits;
using System;
using UnityEngine;

namespace SpaceTraveler.GameStructures.Characters
{
    [CreateAssetMenu(menuName = "ComboElement")]
    public class ComboElement : ScriptableObject
    {
        [SerializeField]
        private int _animationId;
        [SerializeField]
        private AudioClip _slashAudio;
        [SerializeField]
        private AddedModifiers _addedModifiers;

        
        public int AnimationId => _animationId;
        public AddedModifiers AddedModifiers => _addedModifiers;
        public AudioClip SlashAudio => _slashAudio;
    }
}
