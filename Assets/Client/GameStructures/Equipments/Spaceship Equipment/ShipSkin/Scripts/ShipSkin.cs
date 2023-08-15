using UnityEngine;

namespace SpaceTraveler.GameStructures.Gear.Spaceship
{
    [CreateAssetMenu(menuName = "Item/Equipment/Armor/new_Ship_Skin")]
    public class ShipSkin : SpaceshipEquipment
    {
        public override void InitEquipment()
        {
            Debug.Log("ShipSkin initialize");
        }
    }
}

