using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace UI
{
    public abstract class UIWidget : MonoBehaviour
    {
        protected void HideWidget()
        {
            gameObject.SetActive(false);
        }

        protected void ShowWidget()
        {
            gameObject.SetActive(true);
        }
    }
}
