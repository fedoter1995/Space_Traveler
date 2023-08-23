using SpaceTraveler.GameStructures.InterractiveObjects;
using SpaceTraveler.UI.InterractiveObjectsUI;
using UnityEngine;
namespace UI
{
    public class OnSceneUIObjects : MonoBehaviour
    {
        [SerializeField]
        private InterractiveObjectInfoUI _interactiveObjectInfoPanel;


        public InterractiveObjectInfoUI UIInfoPanel => _interactiveObjectInfoPanel;

        private void Awake()
        {
            


        }
    }
}
