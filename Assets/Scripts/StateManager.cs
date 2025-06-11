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
    
    void Start()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }

        DontDestroyOnLoad(this);

        InitRooms();
    }

    private void InitRooms()
    {
        maze = new Maze(Rows, Columns);
        maze.GenerateMaze();
        List<BaseCell> cells =  maze.Cells.Cast<BaseCell>().ToList();
    }
}
