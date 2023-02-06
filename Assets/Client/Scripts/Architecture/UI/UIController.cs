using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Architecture
{
    public abstract class UIController : ArchitectureComponent, IUIController
    {
        protected UIContainer uiContainer;
    }
}

