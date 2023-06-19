using System;
using System.Threading;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;

namespace GameStructures.Effects
{
    [Serializable]
    public abstract class LastingEffect : Effect
    {
        protected int duration = 0;
        protected bool isPermanent = false;
        protected CancellationToken cancellationToken;

        public event Action<int> EverySecondsEvent;
        public event Action OnEffectEndEvent;
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
            OnEffectEndEvent?.Invoke();
        }

        public virtual void Initialize(CancellationToken token)
        {
            this.cancellationToken = token;
        }      
    }
    [Serializable]
    public struct DurationParameters
    {
        [SerializeField]
        private bool isPermanent;
        [SerializeField]
        private int duration;

        public bool IsPermanent => isPermanent;
        public int Duration => duration;

        public DurationParameters(int duration, bool isPermanent = false)
        {
            this.isPermanent = isPermanent;
            this.duration = duration;
        }
    }
}
