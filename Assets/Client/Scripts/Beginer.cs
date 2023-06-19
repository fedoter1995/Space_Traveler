using System.Collections;
using System.Threading.Tasks;
using Architecture;
using GameStructures.Effects;
using GameStructures.Stats;
using UnityEngine;

public class Beginer : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        Game.Run();
    }

}
