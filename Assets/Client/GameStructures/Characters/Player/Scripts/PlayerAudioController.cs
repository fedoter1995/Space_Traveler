using SpaceTraveler.Audio;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceTraveler.Characters.Player
{
    public class PlayerAudioController : MonoBehaviour
    {
        [SerializeField] private AudioSource m_audioSourceToOther;
        [SerializeField] private AudioSource m_audioSourceToSteps;
        [SerializeField] private AudioSource m_audioSourceToAbillitiesAndSlashes;

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

            m_audioSourceToSteps.clip = clip;
            m_audioSourceToSteps.Play();

            stepClips.Enqueue(clip);

        }
        public void OnLanding()
        {
            m_audioSourceToSteps.clip = groundSettings.LandingSound;
            m_audioSourceToSteps.Play();
        }
        public void SlashAudio(AudioClip clip)
        {
            m_audioSourceToAbillitiesAndSlashes.clip = clip;
            m_audioSourceToAbillitiesAndSlashes.Play();
        }
    }
}