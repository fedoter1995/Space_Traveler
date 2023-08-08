using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameStructures.Gear.Armors
{
    [CreateAssetMenu(menuName = "Item/Equipment/Armor/new_Ship_Skin")]
    public class ShipSkin : Equipment
    {
        public override void InitEquipment()
        {
            Debug.Log("ShipSkin initialize");
        }
    }
}

