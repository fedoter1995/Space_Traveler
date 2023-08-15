using System;
using TMPro;

public class StatsDescriptionObject : TextMeshProUGUI, IPoolsObject<StatsDescriptionObject>
{
    public Action<StatsDescriptionObject> OnDisableObject { get; set; }
}
