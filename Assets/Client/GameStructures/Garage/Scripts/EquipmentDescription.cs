using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EquipmentDescription : Description
{
    [SerializeField]
    protected Image _icon;
    [SerializeField]
    protected TextMeshProUGUI _header;
    [SerializeField]
    protected StatsDescriptionUI _statsDescription;
    [SerializeField]
    protected TextMeshProUGUI _description;

    public override void Show(DescriptionData data)
    {
        var descriptionData = data;
        _header.text = descriptionData.Header;
        _description.text = descriptionData.Description;
        _icon.sprite = data.Icon;

        _statsDescription.SetObjects(descriptionData.Footer);

    }

}

