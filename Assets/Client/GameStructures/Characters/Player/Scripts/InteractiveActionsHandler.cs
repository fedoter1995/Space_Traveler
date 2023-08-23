using SpaceTraveler.GameStructures.InterractiveObjects;
using SpaceTraveler.GameStructures.Zones;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace SpaceTraveler.GameStructures.Characters.Player
{
    public class InteractiveActionsHandler : MonoBehaviour
    {
        private Actor actor;


        private List<Interractive2DObject> interractive2DObjects = new List<Interractive2DObject>();

        public event Action<List<Interractive2DObject>> ChangeObjectListEvent;


        public void Initialize(Actor actor)
        {
            this.actor = actor;
        }    

        public void Interact(Interractive2DObject obj)
        {
            if(interractive2DObjects.Contains(obj))
            {
                obj.Interract(actor);
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            var obj = collision.GetComponent<Interractive2DObject>();

            if (obj != null )
            {
                if (!interractive2DObjects.Contains(obj))
                {
                    interractive2DObjects.Add(obj);
                    ChangeObjectListEvent?.Invoke(interractive2DObjects);
                }

            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            var obj = collision.GetComponent<Interractive2DObject>();

            if (obj != null)
            {   
                if(interractive2DObjects.Contains(obj))
                {
                    interractive2DObjects.Remove(obj);
                    ChangeObjectListEvent?.Invoke(interractive2DObjects);
                }

            }

        }
    }
}
