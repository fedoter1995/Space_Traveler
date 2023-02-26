using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Architecture;
public class BreakableAsteroid : Asteroid
{
    
    [SerializeField, Header("Break Settings")]
    private int _numberOfFragments = 2;
    [SerializeField]
    private AsteroidType _fragmentsType;
    
    public override void DestroyAsteroid()
    {
        var interactor = Game.GetInteractor<AsteroidsInteractor>();
        for(int i = 0; i < _numberOfFragments; i++)
        {
            interactor.asteroids.CreateAsteroid(_fragmentsType, transform.position, _direction);
        }
        base.DestroyAsteroid();
    }

}
