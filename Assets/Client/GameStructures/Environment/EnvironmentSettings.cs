using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameStructures.Stats;

[CreateAssetMenu(menuName ="Environment/Settings")]
public class EnvironmentSettings : ScriptableObject
{
    [SerializeField]
    private List<StatModifier> _modifiers;

    public List<StatModifier> Modifiers => _modifiers;
}
