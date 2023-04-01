using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;
using UI.Tooltip;

public abstract class Description : MonoBehaviour
{
    protected const string END_LINE = "\n";
    protected const string SPACE = " ";


    public abstract void Show(DescriptionData data);
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