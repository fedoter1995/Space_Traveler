using CustomTools.Observable;
using System;
using Stats;


public interface ITakeDamage
{
    Observable<int> HealthPoints { get; }

    //arg == received damage
    event Action<float> OnTakeDamageEvent;
    void TakeDamage(ShotDamage damage);
}

