using System;
using UnityEngine;


public abstract class GarageTab : MonoBehaviour
{
    public abstract void Initialize();

    public void SetActive(bool activity)
    {
        gameObject.SetActive(activity);
    }
}

