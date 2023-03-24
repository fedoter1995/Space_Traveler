using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceshipCameraController : MonoBehaviour
{
    [SerializeField]
    private CinemachineVirtualCamera cameraSettingsPrefab;

    private CinemachineVirtualCamera cameraSettings;

    private Spaceship spaceship;

    private void OnEnable()
    {
        CreateCameraSettings();

        spaceship = GetComponent<Spaceship>();


        Follow(spaceship.transform);
    }

    private void CreateCameraSettings()
    {
        cameraSettings = Instantiate(cameraSettingsPrefab);
    }

    public void LookAt(Transform target)
    {
        cameraSettings.LookAt = target;
    }
    public void Follow(Transform target)
    {
        cameraSettings.Follow = target;
    }
}
