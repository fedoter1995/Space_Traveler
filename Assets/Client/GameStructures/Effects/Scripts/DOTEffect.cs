using SpaceTraveler.GameStructures.Stats;
using System;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

namespace SpaceTraveler.GameStructures.Effects
{
    [Serializable]
    public class DOTEffect : LastingEffect
    {
        [SerializeField]
        private DotEffectStats stats;

        public event Action<TriggeredDotStats> OnDotTriggeredEvent;
        public DOTEffect(DotEffectStats stats)
        {
            this.stats = stats;
        }

        private async void EffectDurationAsync()
        {
            while (isPermanent || duration > 0)
            {
                if (cancellationToken.IsCancellationRequested)
                    return;

                var task = Task.Delay(1000 / stats.Frequency);
                await task;
                OnEffectAplly();
            }
        }
        public override void OnEffectAplly()
        {         
            OnDotTriggeredEvent?.Invoke(new TriggeredDotStats(stats.Sender, stats.Damage));
        }
        public override void Initialize(CancellationToken token)
        {
            base.Initialize(token);
            duration = stats.Duration;
            isPermanent = stats.IsPermanent;

            if(!isPermanent)
            {
                DurationDecreaseAsync();
            }

            EffectDurationAsync();          
        }
    }
    [Serializable]
    public struct DotEffectStats
    {
        [SerializeField]
        private DamageAttributes _damage;
        [SerializeField]
        DurationParameters _durationParameters;
        [SerializeField]
        private int _frequency;


        private object sender;


        public bool IsPermanent => _durationParameters.IsPermanent;
        public int Duration => _durationParameters.Duration;
        public int Frequency => _frequency;
        public DamageAttributes Damage => _damage;
        public object Sender => sender;
        public DotEffectStats(DurationParameters durationParameters, int frequency, DamageAttributes damage, object sender)
        {         
            _durationParameters = durationParameters;
            _damage = damage;
            _frequency = frequency;
            this.sender = sender;
        }
    }
    public class TriggeredDotStats : TriggeredEffectStats
    {
        private DamageAttributes damage;
        public DamageAttributes Damage => damage;

        public TriggeredDotStats(object sender, DamageAttributes damage) : base(sender)
        {
            this.damage = damage;
            this.sender = sender;
        }

    }
}
