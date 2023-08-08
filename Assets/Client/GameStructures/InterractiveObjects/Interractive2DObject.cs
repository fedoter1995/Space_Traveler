using GameStructures.Player;
using GameStructures.Struct;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace GameStructures.InterractiveObjects
{
    [RequireComponent(typeof(Collider2D), typeof(Rigidbody2D))]
    public abstract class Interractive2DObject : MonoBehaviour
    {
        [SerializeField]
        private InterractiveObjectInfo _info;

        public event Action<Interractive2DObject> OnTriggerEnterEvent;
        public event Action OnTriggerExitEvent;

        public InterractiveObjectInfo Info => _info;
        public abstract void Interract();
        
        private void OnTriggerEnter2D(Collider2D collision)
        {
            var actor = collision.GetComponent<Actor>();
            if (actor != null)
            {
                OnTriggerEnterEvent?.Invoke(this);
            }
        }
        private void OnTriggerExit2D(Collider2D collision)
        {
            var actor = collision.GetComponent<Actor>();
            if (actor != null)
            {
                OnTriggerExitEvent?.Invoke();
            }
        }
    }
}
