using Architecture;
using SpaceTraveler.GameStructures.Stats;
using UnityEngine;

namespace GameStructures.Environment
{
    public class Environment : MonoBehaviour
    {
        [SerializeField]
        private EnvironmentSettings _settings;


        private void OnTriggerEnter2D(Collider2D collision)
        {
            var obj = collision.GetComponentInParent<IHaveStatsHandler<StatsHandler>>();

            if (obj != null)
                obj.StatsHandler.SetEnvironment(_settings);
        }
        private void OnTriggerExit2D(Collider2D collision)
        {
            var obj = collision.GetComponentInParent<IHaveStatsHandler<StatsHandler>>();
            if (obj != null)
                obj.StatsHandler.SetEnvironment(Game.DefaultEnvironment);
        }
    }

}
