using SpaceTraveler.Audio;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace SpaceTraveler.GameStructures.Characters
{
    [Serializable]
    public class CharactersAudioController : MonoBehaviour
    {
        [SerializeField] 
        private AudioSource _footStepsAudioSource;

        
        private GroundAudioSettings groundSettings;

        private Queue<AudioClip> stepClips;

        public void ChangeGroundSettings(GroundAudioSettings groundSettings)
        {
            this.groundSettings = groundSettings;
            stepClips = new Queue<AudioClip>(groundSettings.FootStepsClips);
        }

        public void OnStep()
        {
            var clip = stepClips.Dequeue();

            _footStepsAudioSource.clip = clip;
            _footStepsAudioSource.Play();

            stepClips.Enqueue(clip);

        }
        public void OnLanding()
        {
            Debug.Log(groundSettings.LandingSound);
            _footStepsAudioSource.clip = groundSettings.LandingSound;
            _footStepsAudioSource.Play();
        }

    }
}
