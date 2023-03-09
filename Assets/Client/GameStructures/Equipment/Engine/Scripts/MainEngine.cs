using UnityEngine;

namespace GameStructures.Equipment.Engine
{
    [CreateAssetMenu(menuName = "Item/Equipment/MainEngine/new_Main_Engine")]
    public class MainEngine : Equipment
    {
        public override void InitEquipment()
        {
            Debug.Log("MainEngine initialize");
        }
    }
}
