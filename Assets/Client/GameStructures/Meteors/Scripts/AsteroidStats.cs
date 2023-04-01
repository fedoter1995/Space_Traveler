using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Stats;
using GameStructures.Stats;

[System.Serializable]
public class AsteroidStats : StatsHandler
{    
    [SerializeField,Range(0f,10f)]
    private float _maxSpeed = 2f;
    [SerializeField, Range(0f, 10f)]
    private float _minSpeed = 1f;
    [SerializeField]
    private int _price = 1;
    [SerializeField]
    private int _maxHealthPoints = 20;
    [SerializeField]
    private List<Damage> _damages;
    
    public float MaxSpeed => _maxSpeed;
    public float MinSpeed => _minSpeed;
    public int HealthPoints => _maxHealthPoints;
    public List<Damage> Damage => _damages;
    public int PointPrice => _price;

    protected override void OnValuesCalculated()
    {
        throw new System.NotImplementedException();
    }
}
