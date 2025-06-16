using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;

[RequireComponent(typeof(NavMeshSurface))]
public class NavMeshManager : MonoBehaviour
{
    public static NavMeshManager Instance;
    
    private NavMeshSurface _navMeshSurface;
    // Start is called before the first frame update
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
