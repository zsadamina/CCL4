using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;

[RequireComponent(typeof(NavMeshSurface))]
// Script to manage the NavMeshSurface in the scene
public class NavMeshManager : MonoBehaviour
{
    public static NavMeshManager Instance;
    
    private NavMeshSurface _navMeshSurface;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            _navMeshSurface = GetComponent<NavMeshSurface>();
        }
        else
        {
            Destroy(this.gameObject);
            
            return;
        }

        DontDestroyOnLoad(this);
    }

    public void BuildNavMesh()
    {
        _navMeshSurface.BuildNavMesh();
    }
}
