using GameStructures.Gear;
using System;
using System.Collections.Generic;
using UnityEngine;
using Utils.Attributes;

namespace Garage.UI
{
    public class EquipmentBookmark : Bookmark
    {       
        private EquipmentTree three;
        public Equipment equipActual { get; private set; }

        public event Action<EquipmentBookmark> OnClickEvent;
        public EquipmentTree Three => three;
        public void SetEquipment(Equipment equipment)
        {
            equipActual = equipment;

            _image.sprite = equipment.Icon;
        }
        public void SetThree(EquipmentTree tree)
        {
            this.three = tree;
            _textMesh.text = tree.Name;
            tree.OnChangeEqipmentEvent += SetEquipment;
            SetEquipment(tree.EquipActual);
            SetActive(false);
        }

        public override void SetActive(bool activity)
        {
            three.SetActive(activity);
        }
        public override void OnClick()
        {
            OnClickEvent?.Invoke(this);
        }
    }
}
