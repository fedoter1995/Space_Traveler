using UnityEngine;
using Architecture;
using GameStructures.Meteors;

public class BreakableAsteroid : Asteroid
{
    
    [SerializeField, Header("Break Settings")]
    private int _numberOfFragments = 2;
    [SerializeField]
    private AsteroidType _fragmentsType;
    
    protected override void DestroyAsteroid(object sender)
    {
        var interactor = Game.GetInteractor<AsteroidsInteractor>();
        base.DestroyAsteroid(sender);
    }

}
