using System.Collections.Generic;
using UnityEngine;
using SpaceTraveler.GameStructures.Stats.StatModifiers;

[CreateAssetMenu(menuName ="Environment/Settings")]
public class EnvironmentSettings : ScriptableObject
{
    [SerializeField]
    private List<StatModifier> _modifiers;

    public List<StatModifier> Modifiers => _modifiers;
}
