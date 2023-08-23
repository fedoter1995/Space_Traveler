using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace SpaceTraveler.UI
{
    public class DisplayTooltipInfo
    {
        [SerializeField]
        protected string _title;
        [SerializeField]
        protected string _instruction = "Interact";

        public string Title => _title;
        public string Instruction => _instruction;


        public DisplayTooltipInfo(string title, string instruction)
        {
            _title = title;
            _instruction = instruction;
        }
    }
}
