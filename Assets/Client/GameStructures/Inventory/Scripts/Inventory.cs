using System;
using System.Linq;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UnityEngine;

[Serializable]
public class Inventory : ItemCollection
{
    public override string ToString()
    {
        return "Spaceship Inventory";
    }
}
