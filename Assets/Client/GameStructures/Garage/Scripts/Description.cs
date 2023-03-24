using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;
using UI.Tooltip;

public abstract class Description : TooltipWindow
{
    [SerializeField]
    protected Image _icon;
    [SerializeField]
    protected TextMeshProUGUI _header;
    [SerializeField]
    protected TextMeshProUGUI _footer;
    [SerializeField]
    protected TextMeshProUGUI _description;
}
public struct DescriptionData : ITooltipData
{
    public string Description { get; }
    public string Header { get; }
    public List<string> Footer { get; }
    public Sprite Icon { get; }

    public DescriptionData(string description, string header, List<string> footer, Sprite icon = null)
    {
        Description = description;
        Header = header;
        Footer = footer;
        Icon = icon;
    }
}