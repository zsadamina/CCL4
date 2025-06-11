using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Classes;
using UnityEngine;

[RequireComponent(typeof(RoomGenerator))]
public class StateManager : MonoBehaviour
{
    public static StateManager Instance;

    [Range(2,5)]
    [SerializeField] public int Rows;
    
    [Range(2,5)]
    [SerializeField] public int Columns;
    
    private Maze maze;
    
    private RoomGenerator roomGenerator;
    
    void Start()
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

        roomGenerator = this.GetComponent<RoomGenerator>();
        InitRooms();
    }

    private void InitRooms()
    {
        maze = new Maze(Rows, Columns);
        maze.GenerateMaze();
        List<Room> cells =  maze.Cells.Cast<Room>().ToList();
        
        cells.ForEach(room => roomGenerator.InstanciateRoom(room));
        Debug.Log(maze.PrintMaze());
    }
}
