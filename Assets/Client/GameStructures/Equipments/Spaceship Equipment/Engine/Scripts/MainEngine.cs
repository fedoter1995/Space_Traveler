using GameStructures.Stats;
using System.Collections.Generic;
using UnityEngine;

namespace GameStructures.Gear.Engine
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
