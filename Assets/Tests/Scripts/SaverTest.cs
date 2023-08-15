using UnityEngine;
using Architecture;
public class SaverTest : MonoBehaviour
{
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.V))
        {
            var interactor = Game.saveController;
            interactor.Save();
        }
    }
}
