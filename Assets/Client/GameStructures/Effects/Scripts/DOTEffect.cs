using SpaceTraveler.GameStructures.Stats;
using SpaceTraveler.GameStructures.Stats.PackedStats;
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

        private object sender;



        public event Action<TriggeredDotStats> OnDotTriggeredEvent;

        public DamageType DamageType => stats.Damage.Type;
        public DOTEffect(object sender, DotEffectStats stats)
        {
            this.stats = stats;
            this.sender = sender;
        }

        private async void EffectDurationAsync()
        {
            while (_isPermanent || duration > 0)
            {
                if (sharedСancellationToken.IsCancellationRequested)
                    return;

                if (personalСancellationToken.IsCancellationRequested)
                    return;

                var task = Task.Delay(1000 / stats.Frequency);
                await task;
                OnEffectAplly();
            }
        }
        public override void OnEffectAplly()
        {
            OnDotTriggeredEvent?.Invoke(new TriggeredDotStats(sender, stats.Damage));
        }
        public override void Initialize(CancellationToken sharedToken)
        {
            base.Initialize(sharedToken);
            duration = stats.Duration;

            _remainingTime = duration;

            if (!_isPermanent)
            {
                DurationDecreaseAsync();
            }

            EffectDurationAsync();
        }
    }
    [Serializable]
    public class DotEffectStats
    {
        [SerializeField]
        private DamageAttributes _damage;
        [SerializeField]
        DurationParameters _durationParameters;



        public int Duration => _durationParameters.Duration;
        public int Frequency => _durationParameters.Frequency;
        public bool IsPermanent => _durationParameters.IsPermanent;
        public DamageAttributes Damage => _damage;


        public DotEffectStats(DurationParameters durationParameters, DamageAttributes damage)
        {         
            _durationParameters = durationParameters;
            _damage = damage;
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
