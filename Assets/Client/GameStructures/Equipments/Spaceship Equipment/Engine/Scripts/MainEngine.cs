using UnityEngine;

namespace SpaceTraveler.GameStructures.Gear.Spaceship
{
    [CreateAssetMenu(menuName = "Item/Equipment/MainEngine/new_Main_Engine")]
    public class MainEngine : SpaceshipEquipment
    {
        public override void InitEquipment()
        {
            Debug.Log("MainEngine initialize");
        }

    }

}
