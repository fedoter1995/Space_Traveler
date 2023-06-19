using System;
using TMPro;
using UnityEngine;

public class StatsDescriptionObject : TextMeshProUGUI, IPoolsObject<StatsDescriptionObject>
{
    public Action<StatsDescriptionObject> OnDisableObject { get; set; }
}
