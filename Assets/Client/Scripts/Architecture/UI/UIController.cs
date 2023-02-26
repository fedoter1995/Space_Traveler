using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Architecture
{
    public abstract class UIController : ArchitectureComponent, IUIController
    {
        protected UIContainer uiContainer;

        protected void FindCanvas()
        {
            uiContainer = Object.FindObjectOfType<UIContainer>();

            if (uiContainer == null)
                CreateCanvas();
        }
        private void CreateCanvas()
        {
            uiContainer = new GameObject().AddComponent<UIContainer>();
        }

    }
}

