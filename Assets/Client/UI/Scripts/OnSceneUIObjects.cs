using GameStructures.InterractiveObjects;
using UI.InterractiveObjectsUI;
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
            
            _interactiveObjectInfoPanel.HideContent();

            var interractiveObjects = FindObjectsOfType<Interractive2DObject>();

            foreach(Interractive2DObject obj in interractiveObjects)
            {
                obj.OnTriggerExitEvent += _interactiveObjectInfoPanel.HideContent;
                obj.OnTriggerEnterEvent += _interactiveObjectInfoPanel.ShowContent;
            }

        }
    }
}
