using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Stats
{
    public interface IStatModifier
    {
        StatModType Type { get; }
        float Value { get; }
    }
}

