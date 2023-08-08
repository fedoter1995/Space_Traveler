using GameStructures.Effects;
using GameStructures.Enemys;
using GameStructures.Hits;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(TakeDamageHandler),typeof(StatusEffectHandler))]
public class HumanoidEnemy : MonoBehaviour, IEnemy
{

    [SerializeField]
    private TakeDamageHandler _takeDamageHandler;
    [SerializeField]
    private StatusEffectHandler _statusEffectHandler;

    public TakeHitHandler TakeHitHandler => _takeDamageHandler;
    public TakeDamageHandler TakeDamageHandler => _takeDamageHandler;
    public StatusEffectHandler StatusHandler => _statusEffectHandler;

    // Update is called once per frame
    void Update()
    {
        
    }
}
