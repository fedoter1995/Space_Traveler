using CustomTools.Observable;
using SpaceTraveler.GameStructures.Enemys.SpaceEnemys;
using SpaceTraveler.GameStructures.Hits;
using SpaceTraveler.GameStructures.Stats;
using System;
using UnityEngine;
using UnityEngine.AI;

namespace SpaceTraveler.GameStructures.NPC
{
    public class Npc : MonoBehaviour
    {
        [SerializeField]
        private string _name;
        [SerializeField]
        private TakeDamageHandler _takeDamageHandler;
        [SerializeField]
        private AnimatorController _enemyAnimatorController;

        [SerializeField]
        private NpcStats _stats;


        private BaseSpaceAgentController controller;

        public event Action<object, DamageAttributes> OnTakeDamageEvent;
        public event Action<HitStats> OnTakeHitEvent;

        public string Name => _name;
        public NavMeshAgent Agent { get; private set; }
        public Observable<bool> IsDestroyed { get; private set; } = new Observable<bool>(false);
        public Observable<int> HealthPoints { get; private set; }
        public Vector3 Position => transform.position;
        public NpcStats Stats => _stats;
    }
}
