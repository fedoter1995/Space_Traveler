using System;
using System.Threading;
using UnityEngine;

namespace SpaceTraveler.GameStructures.Effects
{
    public class BuffEffect : LastingEffect
    {
        [SerializeField]
        private BuffPreset preset;
        public override void Initialize(CancellationToken token)
        {
            base.Initialize(token);
        }

        public override void OnEffectAplly()
        {
            throw new NotImplementedException();
        }
    }
}
