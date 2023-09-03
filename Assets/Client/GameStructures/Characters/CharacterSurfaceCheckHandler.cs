using SpaceTraveler.Audio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.Burst.CompilerServices;
using UnityEngine;
using UnityEngine.InputSystem.HID;

namespace SpaceTraveler.GameStructures.Characters
{
    public class CharacterSurfaceCheckHandler : MonoBehaviour
    {

        [SerializeField]
        private LayerMask _groundLayers;
        [SerializeField]
        private LayerMask _wallLayers;

        [SerializeField]
        private Transform _leftFootObject;
        [SerializeField]
        private Transform _rightFootObject;
        [SerializeField]
        private Transform _ledgeCheckObj;
        [SerializeField]
        private Transform _wallCheckObj;

        private float rayDistance = 0.5f;
        private float circleRadius = 0.07f;
        private int currentGroundLayer;
        private bool onGround = false;
        private float wallLedgeDelta => _ledgeCheckObj.position.y - _wallCheckObj.position.y;


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

            var hitLeft = Physics2D.OverlapCircle(_leftFootObject.position, circleRadius, _groundLayers);
            var hitRight = Physics2D.OverlapCircle(_rightFootObject.position, circleRadius, _groundLayers);


            if (hitRight != null)
            {
                if (currentGroundLayer != hitRight.gameObject.layer)
                {
                    currentGroundLayer = hitRight.gameObject.layer;
                    GroundTypeChangeEvent?.Invoke(hitRight.GetComponent<GroundSettings>());
                }
            }


            if (hitLeft != onGround && hitRight != onGround)
            {
                OnGround = hitRight;
            }
        }
        public Vector2 DetermineCornerPosition(int dirrection)
        {
            Vector2 space = Vector2.zero;
            var xHit = CheckWall(dirrection);

            if(xHit)
            {
                var xHitDis = xHit.distance;
                space.Set(xHitDis * dirrection, 0f);

                RaycastHit2D yHit =  Physics2D.Raycast(_ledgeCheckObj.position + (Vector3)space, Vector2.down, wallLedgeDelta, _wallLayers);
                var yHitDis = yHit.distance;
  
                space.Set(_wallCheckObj.position.x + (xHitDis * dirrection), _ledgeCheckObj.position.y - yHitDis);

            }

            return space;
        }
        public bool CheckLedge(int dirrection)
        {
            var hit= Physics2D.Raycast(_ledgeCheckObj.position, new Vector2(dirrection, 0), rayDistance, _wallLayers);

            if (!hit)
                if(CheckWall(dirrection))
                    return true;

            return false;
        }
        private RaycastHit2D CheckWall(int dirrection)
        {
            return Physics2D.Raycast(_wallCheckObj.position, new Vector2(dirrection, 0), rayDistance, _wallLayers);             
        }
    }
}
