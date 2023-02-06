using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Level
{
    [SerializeField]
    private int baseValue = 1;

    public int BaseValue => baseValue;

    private int value;

    public int Value => value;

    public void Initialize()
    {
        value = baseValue;
    }

    public void LevelUP()
    {
        value++;
    }
}
