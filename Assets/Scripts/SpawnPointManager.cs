using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPointManager : MonoBehaviour
{
    // Singleton instance of SpawnPointManager
    public static SpawnPointManager Instance;
    // List to hold all spawn points in the scene
    public List<TodoItem> Todos { get; set; } = new List<TodoItem>();

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
            return;
        }
    }
}
