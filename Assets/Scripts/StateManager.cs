using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Classes;
using UnityEngine;

public class StateManager : MonoBehaviour
{
    public static StateManager Instance;

    [Range(2,5)]
    [SerializeField] public int Rows;
    
    [Range(2,5)]
    [SerializeField] public int Columns;
    
    private Maze maze;
    private RoomGenerator roomGenerator;

    
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

        DontDestroyOnLoad(this);

        roomGenerator = RoomGenerator.Instance;
    }

    private void Start()
    {
        InitRooms();
        NavMeshManager.Instance?.BuildNavMesh();
    }

    private void InitRooms()
    {
        maze = new Maze(Rows, Columns);
        maze.GenerateMaze();
        roomGenerator.setMaze(maze);
        roomGenerator.GenerateRooms();

        //Debug.Log(maze.PrintMaze());
    }
}
