using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Stats;
[CreateAssetMenu(menuName ="Environment/Settings")]
public class EnvironmentSettings : ScriptableObject
{
    [SerializeField]
    private List<StatModifier> _modifiers;

    public List<StatModifier> Modifiers => _modifiers;
}
