using Architecture;
using SpaceTraveler.GameStructures.Effects;
using SpaceTraveler.GameStructures.Enemy.HumanoidEnemyes;
using SpaceTraveler.GameStructures.Enemys;
using SpaceTraveler.GameStructures.Hits;
using SpaceTraveler.GameStructures.Player;
using SpaceTraveler.GameStructures.Stats;
using UnityEngine;
namespace SpaceTraveler.GameStructures.Enemy.HumanoidEnemyes
{
    [RequireComponent(typeof(TakeDamageHandler),typeof(StatusEffectHandler))]
    public class HumanoidEnemy : MonoBehaviour, IEnemy
    {

        [SerializeField]
        private TakeDamageHandler _takeDamageHandler;
        [SerializeField]
        private StatusEffectHandler _statusEffectHandler;
        [SerializeField]
        private MeleHumanoidEnemyStatsHandler _statsHandler;

        public TakeHitHandler TakeHitHandler => _takeDamageHandler;
        public TakeDamageHandler TakeDamageHandler => _takeDamageHandler;
        public StatusEffectHandler StatusHandler => _statusEffectHandler;

        private void Awake()
        {                     
            _statsHandler.Initialize();
            _takeDamageHandler.Initialize(this, _statsHandler);

        }
    }
}

