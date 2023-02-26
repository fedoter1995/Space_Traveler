using System.Collections;
using System.Collections.Generic;
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
        if (Input.GetKeyDown(KeyCode.X))
        {
            var interactor = Game.GetInteractor<GameStatisticsInteractor>();
            Debug.Log(interactor.statistics.Points);
            Debug.Log(interactor.statistics.DestroyedAsteroidsNumb[AsteroidType.Large]);
            Debug.Log(interactor.statistics.DestroyedAsteroidsNumb[AsteroidType.Medium]);
            Debug.Log(interactor.statistics.DestroyedAsteroidsNumb[AsteroidType.Small]);
        }
    }
}
