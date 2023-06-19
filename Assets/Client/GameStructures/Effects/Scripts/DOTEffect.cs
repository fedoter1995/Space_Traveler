using GameStructures.Stats;
using System;
using System.Security.Cryptography;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

namespace GameStructures.Effects
{
    [Serializable]
    public class DOTEffect : LastingEffect
    {
        [SerializeField]
        private DotEffectStats stats;
        public DamageType DamageType => stats.DamageType;
        public event Action<DamageType, int> OnDotTriggeredEvent;
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
            Debug.Log(stats.Damage.ToString());
            OnDotTriggeredEvent?.Invoke(stats.DamageType, stats.DamageValue);
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
        private DamageTypeValue damage;
        [SerializeField]
        DurationParameters durationParameters;
        [SerializeField]
        private int frequency;


        public bool IsPermanent => durationParameters.IsPermanent;
        public int Duration => durationParameters.Duration;
        public int Frequency => frequency;
        public int DamageValue => damage.Value;
        public DamageType DamageType => damage.Type;
        public DamageTypeValue Damage => damage;
        public DotEffectStats(DurationParameters durationParameters, int frequency, DamageTypeValue damage)
        {         
            this.durationParameters = durationParameters;
            this.damage = damage;
            this.frequency = frequency;
        }
    }
}
