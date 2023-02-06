using System;
using System.Collections;
using UnityEngine;

namespace Architecture
{
    public class UI<T> where T : IUIController
    {
        public ComponentsBase<IUIController> controllers { get; private set; }

        public UI(string[] references)
        {
            controllers = new ComponentsBase<IUIController>(references);
        }
    }
}