using UnityEngine;

namespace UI.Tooltip
{
    public interface ITooltipWindow
    {
        RectTransform Box { get; }
        void Show(ITooltipData data);
        void Hide();
    }
}
