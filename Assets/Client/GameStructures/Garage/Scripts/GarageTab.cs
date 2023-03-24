using Garage.UI;
using System;
using UnityEngine;


public abstract class GarageTab : MonoBehaviour
{
    [SerializeField]
    private string _name = "GarageTab";
    public abstract void Initialize();
    protected abstract void OnOpen();

    public string Name => _name;

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

