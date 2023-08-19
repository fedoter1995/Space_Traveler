using SpaceTraveler.GameStructures.Stats.Presets;
using System;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

namespace SpaceTraveler.GameStructures.Effects
{
    [Serializable]
    public abstract class LastingEffect : Effect
    {
        [SerializeField]
        protected bool _isPermanent = false;


        protected int duration = 0;
        protected CancellationToken cancellationToken;

        public event Action<int> EverySecondsEvent;
        public event Action<LastingEffect> OnEffectEndEvent;
        protected async void DurationDecreaseAsync()
        {
            while (duration > 0)
            {
                if(cancellationToken.IsCancellationRequested)
                    return;

                var task = Task.Delay(1000);
                await task;
                duration--;
                EverySecondsEvent?.Invoke(duration);
            }
            EndEffect();
        }
        public void EndEffect()
        {
            OnEffectEndEvent?.Invoke(this);
        }

        public virtual void Initialize(CancellationToken token)
        {
            this.cancellationToken = token;
        }      
    }
    [Serializable]
    public class DurationParameters
    {
        [SerializeField]
        private bool _isPermanent = false;
        [SerializeField]
        private int _duration;
        [SerializeField]
        private int _frequency;

        public int Duration => _duration;
        public int Frequency => _frequency;
        public bool IsPermanent => _isPermanent;
        public DurationParameters(int duration, int frequency)
        {
            _duration = duration;
            _frequency = frequency;
        }
    }
}
