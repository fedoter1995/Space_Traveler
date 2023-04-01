using GameStructures.Hits;
using GameStructures.Stats;
using Stats;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public interface ITakeHit
{
    event Action<HitStats> OnTakeHitEvent;
    void TakeHit(object sender, Hit hit);
}
struct Message
{
    public object sender;
    public DamageType type;
    public DamageValue dmg;

    public Message(object sender, DamageType type, DamageValue dmg)
    {
        this.sender = sender;
        this.type = type;
        this.dmg = dmg;
    }
    public override string ToString()
    {
        return $"{sender} take {dmg} {type} damage";
    }
}