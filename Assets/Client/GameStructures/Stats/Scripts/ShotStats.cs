using GameStructures.Effects;
using GameStructures.Equipment.Weapons;
using Stats;
using System;
using System.Collections.Generic;

using UnityEngine;

namespace GameStructures.Stats
{
    public class ShotStats
    {

        private float speed;
        

        private List<Vector2> shotPositions;
        private List<Vector2> shotDirrections;
        private List<Quaternion> rotation;
        public float ShotSpeed => speed;
        public List<Vector2> ShotPos => shotPositions;
        public List<Vector2> ShotDir => shotDirrections;
        public List<Quaternion> Rotation => rotation;
        public ShotStats(List<ShootPosition> parrents, float speed)
        {
            shotPositions = new List<Vector2>();
            shotDirrections = new List<Vector2>();
            rotation = new List<Quaternion>();
            foreach (ShootPosition point in parrents)
            {
                shotPositions.Add(point.transform.position);
                shotDirrections.Add(point.transform.up);
                rotation.Add(point.transform.rotation);
            }

            this.speed = speed;
        }
    }
}
