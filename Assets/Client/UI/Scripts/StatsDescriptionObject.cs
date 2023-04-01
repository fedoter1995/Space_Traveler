using System;
using TMPro;
using UnityEngine;

public class StatsDescriptionObject : TextMeshProUGUI, IPoolsObject<StatsDescriptionObject>
{
    public event Action<StatsDescriptionObject> OnDisableEvent;
}
