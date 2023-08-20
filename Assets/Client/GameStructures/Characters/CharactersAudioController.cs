using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Client.GameStructures.Characters
{
    [Serializable]
    public class CharactersAudioController : MonoBehaviour
    {
        [SerializeField]
        private AudioSource footStepsAudioSource;
        [SerializeField]
        private List<AudioClip> clipList;


        private Queue<AudioClip> stepClips;

        private void Awake()
        {
            stepClips = new Queue<AudioClip>(clipList);
        }

        public void OnStep()
        {
            var clip = stepClips.Dequeue();

            footStepsAudioSource.clip = clip;
            footStepsAudioSource.Play();

            stepClips.Enqueue(clip);

        }

    }
}
