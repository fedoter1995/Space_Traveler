using Architecture;
using GameStructures.Stats;
using UnityEngine;

public class Environment : MonoBehaviour
{
    [SerializeField]
    private EnvironmentSettings _settings;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        var obj = collision.GetComponent<IHaveStatsHandler>();
        if (obj != null)
            obj.Handler.SetEnvironment(_settings);
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        var obj = collision.GetComponent<IHaveStatsHandler>();
        if (obj != null)
            obj.Handler.SetEnvironment(Game.DefaultEnvironment);
    }
}
