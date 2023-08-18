using SpaceTraveler.GameStructures.Stats;
using System;
using System.Collections.Generic;
using System.Threading;
using UnityEditor;
using UnityEngine;

namespace SpaceTraveler.GameStructures.Effects
{
    [Serializable]
    public class LastingEffectsHandler
    {
        private const string DOT = "DOTEffect";

        [SerializeField]
        private List<DOTEffect> _dots = new List<DOTEffect>();
        [SerializeField]
        private List<BuffEffect> _buffs = new List<BuffEffect>();
        public List<DOTEffect> DotEffects => _dots;
        public List<BuffEffect> Buffs => _buffs;

        private CancellationTokenSource cts;
        private CancellationToken cancellationToken;


        public event Action<object, DamageAttributes> OnDotTriggeredEvent;

        public void Initialize()
        {
            EditorApplication.playModeStateChanged += ModeChanged;
            cts = new CancellationTokenSource();
            cancellationToken = cts.Token;

            foreach (var dot in _dots)
            {
                dot.OnDotTriggeredEvent += OnDotEffectTriggered;
                dot.Initialize(cancellationToken);
            }
        }
        public void AddNewStatusEffect(LastingEffect effect)
        {
            var effectTypeName = effect.GetType().Name;
            Debug.Log(effectTypeName);
            switch (effectTypeName)
            {
                case DOT:
                    {
                        var dot = effect as DOTEffect;
                        dot.Initialize(cancellationToken);
                        _dots.Add(dot);
                        break;
                    }
            }
        }
        private void OnDotEffectTriggered(TriggeredDotStats dotStats)
        {
            OnDotTriggeredEvent?.Invoke(dotStats.Sender, dotStats.Damage);
        }
        private void ModeChanged(PlayModeStateChange playModeState)
        {
            if (playModeState == PlayModeStateChange.EnteredEditMode && cts != null)
            {
                cts.Cancel();
            }
        }
    }
}
