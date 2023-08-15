using SpaceTraveler.GameStructures.Gear.Weapons;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceTraveler.GameStructures.Stats
{
    public class ShotStats
    {

        private float speed;
        
        private Vector2 shotDirrection;
        private List<Vector2> shotPositions;
        private List<Quaternion> rotation;
       
        public float ShotSpeed => speed;
        public Vector2 ShotDir => shotDirrection;
        public List<Vector2> ShotPos => shotPositions;
        public List<Quaternion> Rotation => rotation;
        public ShotStats(Vector2 shotDirrection, List<ShootPosition> parrents, float speed)
        {

            shotPositions = new List<Vector2>();
            rotation = new List<Quaternion>();
            foreach (ShootPosition point in parrents)
            {
                shotPositions.Add(point.transform.position);
                rotation.Add(point.transform.rotation);
            }

            this.shotDirrection = shotDirrection;
            this.speed = speed;
        }
    }
}
