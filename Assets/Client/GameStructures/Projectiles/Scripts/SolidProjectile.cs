using SpaceTraveler.GameStructures.Stats;
using System.Collections;
using UnityEngine;

namespace SpaceTraveler.GameStructures.Projectiles
{
    public class SolidProjectile : Projectile
    {

        private Coroutine moveEnumerator = null;

        public override void Initialize(object sender, ProjSettings settings, HitStats hitStats)
        {
            this.sender = sender;
            this.settings = settings;
            this.hitStats = hitStats;
        }
        public override void Move()
        {
            if (moveEnumerator == null)
                moveEnumerator = StartCoroutine(MoveRoutine());

        }
        private IEnumerator MoveRoutine()
        {
            float deltaTime = 0f;
            while(deltaTime < _lifetime)
            {
                yield return new WaitForFixedUpdate();
                transform.Translate(settings.Dirrection * Time.fixedDeltaTime * settings.Speed, Space.World);
                deltaTime += Time.fixedDeltaTime;
            }
            moveEnumerator = null;
            SetActive(false);
        }
        protected override void SetActive(bool activity)
        {
            moveEnumerator = null;
            base.SetActive(activity);
        }
    }
}


