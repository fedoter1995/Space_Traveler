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
                    Debug.Log("landing");
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

            var hit1 = Physics2D.OverlapCircle(_leftObject.position, rayDistance, _layerMask);
            var hit2 = Physics2D.OverlapCircle(_rightObject.position, rayDistance, _layerMask);


            if (hit1)
            {
                if (currentGroundLayer != hit1.gameObject.layer)
                {
                    currentGroundLayer = hit1.gameObject.layer;
                    GroundTypeChangeEvent?.Invoke(hit1.GetComponent<GroundSettings>());
                }
            }
            else if (hit2)
            {
                if (currentGroundLayer != hit2.gameObject.layer)
                {
                    currentGroundLayer = hit2.gameObject.layer;
                    GroundTypeChangeEvent?.Invoke(hit2.GetComponent<GroundSettings>());
                }
            }


            if (hit1 != onGround && hit2 != onGround)
            {
                OnGround = hit1;
            }
        }
    }
}
