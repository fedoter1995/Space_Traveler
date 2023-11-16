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
        protected int _remainingTime = 0;
        [SerializeField]
        protected bool _isPermanent = false;


        protected int duration = 0;

        protected CancellationToken sharedСancellationToken;
        protected CancellationToken personalСancellationToken;

        private CancellationTokenSource cts;


        public event Action<int> EverySecondsEvent;
        public event Action<LastingEffect> OnEffectEndEvent;
        protected async void DurationDecreaseAsync()
        {
            while (duration > 0)
            {
                if(sharedСancellationToken.IsCancellationRequested)
                    return;

                if (personalСancellationToken.IsCancellationRequested)
                    return;


                var task = Task.Delay(1000);
                await task;
                duration--;
                _remainingTime = duration;
                EverySecondsEvent?.Invoke(duration);
            }
            EndEffect();
        }
        public void EndEffect()
        {
            OnEffectEndEvent?.Invoke(this);
        }
        public void Remove()
        {
            cts.Cancel();
        }
        public virtual void Initialize(CancellationToken sharedToken)
        {
            sharedСancellationToken = sharedToken;

            cts = new CancellationTokenSource();
            personalСancellationToken = cts.Token;
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
