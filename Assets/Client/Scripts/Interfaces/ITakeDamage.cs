using CustomTools.Observable;
using System;
using Stats;


public interface ITakeDamage : ITakeHit
{
    Observable<int> HealthPoints { get; }

    //arg == received damage
    event Action<int> OnTakeDamageEvent;
    void TakeDamage(int damage);
}

