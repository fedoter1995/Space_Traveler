using UnityEngine;

public sealed class PlayerSpawn : MonoBehaviour
{
    [SerializeField]
    private bool isInitialSpawn = false;

    public bool IsInitial => isInitialSpawn;
    public Vector3 Position => transform.position;
}
