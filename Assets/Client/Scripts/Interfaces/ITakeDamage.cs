using CustomTools.Observable;
using System;
using GameStructures.Stats;
using System.Collections.Generic;

public interface ITakeDamage : ITakeHit
{
    event Action<object, DamageTypeValue> OnTakeDamageEvent;
    void TakeDamage(object sender, HitDamage damage);
}
public struct TakeDamageMessage
{
    private object sender;
    private DamageTypeValue dmg;

    public TakeDamageMessage(object sender, DamageTypeValue dmg)
    {
        this.sender = sender;
        this.dmg = dmg;
    }

    public override string ToString()
    {
        return $"{sender} take {dmg.Type} {dmg.Value} damage";
    }
}
