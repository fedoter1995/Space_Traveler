using GameStructures.Spaceship;
using SpaceTraveler.GameStructures.Spaceship;
using System;

using UnityEngine;

public class StarshipController : MonoBehaviour
{
    private bool isInitialize = false;
    private IStarshipInputManager inputManager;
    private float currentMovementSpeed = 0;
    private float swing = 0;
    private Starship ship;
    private StarshipStatsHandler stats => ship.StatsHandler;

    

    private void Update()
    {
        if(isInitialize)
        {
            MoveSpeedChange();
            SwingSpeedChange();
        }    

    }
    private void FixedUpdate()
    {
        if(isInitialize)
            SpaceShipMovement();
    }
    public void Initialize(IStarshipInputManager manager, Starship ship)
    {
        this.ship = ship;
        inputManager = manager;
        isInitialize = true;
    }
    private void SpaceShipMovement()
    {
        if(currentMovementSpeed > 0)
            transform.Translate(transform.up * Time.fixedDeltaTime * currentMovementSpeed, Space.World);
        SpaceShipRotation();
    }
    private void SpaceShipRotation()
    {
        transform.Rotate(0, 0, swing);
    }
    private void MoveSpeedChange()
    {
        if (inputManager.Move && currentMovementSpeed < stats.MoveSpeed)
        {
            currentMovementSpeed += stats.Acceleration;
        }    
        else if(!inputManager.Move && currentMovementSpeed > 0)
            currentMovementSpeed -= stats.Deceleration;

        if (currentMovementSpeed > stats.MoveSpeed)
            currentMovementSpeed = stats.MoveSpeed;
    }
    private void SwingSpeedChange()
    {
       if(inputManager.Rotation == 0 && swing != 0)
        {
            switch(swing)
            {
                case 0:
                    break;
                case > 0:
                    swing -= stats.SwingSlowdown;
                    swing = swing < 0 ? 0 : swing;
                    break;
                case < 0:
                    swing += stats.SwingSlowdown;
                    swing = swing > 0 ? 0 : swing;
                    break;
            }
        }
       else if(Math.Abs(swing) < stats.SwingSpeed)
        {
            swing += stats.SwingSpeedup * inputManager.Rotation;
            swing = Math.Abs(swing) > stats.SwingSpeed ? stats.SwingSpeed * inputManager.Rotation : swing;
        }
    }
}
