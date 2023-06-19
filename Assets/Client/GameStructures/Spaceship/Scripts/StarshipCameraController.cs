using Cinemachine;
using GameStructures.Player;
using GameStructures.Spaceship;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarshipCameraController : MonoBehaviour
{
    [SerializeField]
    private CinemachineVirtualCamera cameraSettingsPrefab;

    private CinemachineVirtualCamera cameraSettings;

    private Starship spaceship;

    private void OnEnable()
    {
        CreateCameraSettings();

        spaceship = GetComponent<Starship>();


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
