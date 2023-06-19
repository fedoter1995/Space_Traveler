using GameStructures.Stats;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;

namespace GameStructures.Effects
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
