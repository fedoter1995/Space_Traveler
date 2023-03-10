using Garage.UI;
using System;
using UnityEngine;


public abstract class GarageTab : MonoBehaviour
{
    public abstract void Initialize();
    protected abstract void OnOpen();

    public void Open()
    {
        SetActive(true);
        OnOpen();
    }
    public void Close()
    {
        SetActive(false);
    }

    private void SetActive(bool activity)
    {
        gameObject.SetActive(activity);
    }
}

