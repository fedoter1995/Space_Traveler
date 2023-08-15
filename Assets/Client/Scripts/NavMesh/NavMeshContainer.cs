using NavMesh;
using NavMeshPlus.Components;
using NavMeshPlus.Extensions;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(NavMeshSurface), typeof(CollectSources2d))]
public class NavMeshContainer : MonoBehaviour
{

    private NavMeshSurface surface;
    private List<IHaveNavMeshModifier> influencingObjects;


    private void Awake()
    {
        surface = GetComponent<NavMeshSurface>();
        influencingObjects = GetComponentsInChildren<IHaveNavMeshModifier>().ToList();

        foreach (IHaveNavMeshModifier obj in influencingObjects)
        {
            obj.OnDisableEvent += UpdateSurface;
            obj.OnEnableEvent += UpdateSurface;
        }
    }

    private void UpdateSurface()
    {
        surface.UpdateNavMesh(surface.navMeshData);
    }

}
