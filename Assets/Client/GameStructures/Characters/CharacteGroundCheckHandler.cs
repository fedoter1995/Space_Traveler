using SpaceTraveler.Audio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.Burst.CompilerServices;
using UnityEngine;

namespace SpaceTraveler.GameStructures.Characters
{
    public class CharacteGroundCheckHandler : MonoBehaviour
    {

        [SerializeField]
        private LayerMask _layerMask;

        [SerializeField]
        private Transform _leftObject;
        [SerializeField]
        private Transform _rightObject;

        private float rayDistance = 0.07f;
        private int currentGroundLayer;
        private bool onGround = false;



        public event Action<GroundSettings> GroundTypeChangeEvent;
        public event Action<bool> OnGroundStateChangeEvent;
        public event Action LandingEvent;

        public bool OnGround
        {
            get { return onGround; }
            set
            {
                if (onGround != value && value)
                {
                    LandingEvent?.Invoke();
                }

                onGround = value;
                OnGroundStateChangeEvent?.Invoke(onGround);
            }
        }


        private void Update()
        {
            CheckGround();
        }
        private void CheckGround()
        {

            var hitLeft = Physics2D.OverlapCircle(_leftObject.position, rayDistance, _layerMask);
            var hitRight = Physics2D.OverlapCircle(_rightObject.position, rayDistance, _layerMask);


            if (hitRight)
            {
                if (currentGroundLayer != hitLeft.gameObject.layer)
                {
                    currentGroundLayer = hitLeft.gameObject.layer;
                    GroundTypeChangeEvent?.Invoke(hitLeft.GetComponent<GroundSettings>());
                }
            }


            if (hitLeft != onGround && hitRight != onGround)
            {
                OnGround = hitRight;
            }
        }
    }
}
