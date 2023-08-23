using SpaceTraveler.GameStructures.Characters.Player;
using System;
using UnityEngine;

namespace SpaceTraveler.GameStructures.InterractiveObjects
{
    [RequireComponent(typeof(Collider2D), typeof(Rigidbody2D))]
    public abstract class Interractive2DObject : MonoBehaviour
    {
        [SerializeField]
        private InterractiveObjectInfo _info;

        public event Action<Actor> OnTriggerEnterEvent;
        public event Action OnTriggerExitEvent;

        public InterractiveObjectInfo Info => _info;
        public abstract void Interract(Actor actor);

    }
}
