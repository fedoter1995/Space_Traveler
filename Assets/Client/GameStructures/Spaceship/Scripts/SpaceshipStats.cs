using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Ship/Stats")]
public class SpaceshipStats : ScriptableObject
{
    [SerializeField]
    private float _moveSpeed = 1f;
    [SerializeField]
    private float _swingSpeed = 1f;
    [SerializeField]
    private float _projectileSpeed = 5f;
    [SerializeField]
    private float _rateOfFire = 0.3f;
    
    public float MoveSpeed => _moveSpeed;

    public float SwingSpeed => _swingSpeed;

    public float ProjectileSpeed => _projectileSpeed;

    public float RateOfFire => _rateOfFire;
    
}
