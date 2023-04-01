

using System.Collections;
using UnityEngine;

namespace UI.Tooltip
{
    [RequireComponent(typeof(CanvasGroup))]
    public abstract class TooltipWindow : MonoBehaviour, ITooltipWindow
    {
        
        [SerializeField]
        private CanvasGroup _group;

        protected Coroutine actualEnumerator;

        public CanvasGroup Group => _group;
        public RectTransform Box => (RectTransform)transform;

        public abstract void Hide();
        public abstract void Show(ITooltipData data);

        protected IEnumerator ShowRoutine(float sensetivity)
        {
            while (Group.alpha < 1)
            {
                Group.alpha += sensetivity;
                yield return new WaitForFixedUpdate();
            }
        }
        protected IEnumerator HideRoutine(float sensetivity)
        {
            while (Group.alpha > 0)
            {
                Group.alpha -= sensetivity;
                yield return new WaitForFixedUpdate();
            }
        }
    }
}
