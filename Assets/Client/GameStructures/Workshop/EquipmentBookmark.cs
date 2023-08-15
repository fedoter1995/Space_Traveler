using SpaceTraveler.GameStructures.Gear;
using SpaceTraveler.UI;
using System;


namespace SpaceTraveler.GameStructures.Workshop.UI
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
            SetEquipment(tree.SlotActual.Equip);
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

