using GameStructures.Equipment;
using System;
using System.Collections.Generic;
using UnityEngine;
using Utils.Attributes;

namespace Garage.UI
{
    public class EquipmentBookmark : Bookmark
    {
        [SerializeField]
        private EquipmentThree _three;

        [SerializeField, ClassReference(typeof(Equipment))]
        private string _type;


        public string TypeReference => _type;
        public Equipment equipActual { get; private set; }

        public event Action<EquipmentBookmark> OnClickEvent;
        public event Action<Equipment> OnChangeEquipmentEvent;
        public void SetEquipment(Equipment equipment)
        {
            equipActual = equipment;

            Debug.Log(equipment);
            image.sprite = equipment.Icon;
        }
        public override void Initialize()
        {
            base.Initialize();
            _three.Initialize(equipActual);
            _three.OnChangeEquipEvent += OnChangeEquipment;
            SetActive(false);
        }
        private void OnChangeEquipment(Equipment equipment)
        {
            SetEquipment(equipment);
            OnChangeEquipmentEvent?.Invoke(equipActual);
        }
        public override void SetActive(bool activity)
        {
            _three.SetActive(activity);
        }
        public override void OnClick()
        {
            OnClickEvent?.Invoke(this);
        }
    }
}
