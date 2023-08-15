using UnityEngine;
[CreateAssetMenu(menuName = "Ability/new_Ability_Tag")]
public class AbilityTag : ScriptableObject, IAbilityTag
{
    [SerializeField]
    private string _name;
    public string Name => _name;
}
