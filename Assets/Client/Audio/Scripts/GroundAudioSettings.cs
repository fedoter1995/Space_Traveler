using System;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceTraveler.Audio
{
    [Serializable]
    public class GroundAudioSettings
    {
        [SerializeField]
        private List<AudioClip> _footStepsSounds = new List<AudioClip>();
        [SerializeField]
        private AudioClip _landingSound;


        public List<AudioClip> FootStepsClips => _footStepsSounds;

        public AudioClip LandingSound => _landingSound;

        
    }
}

