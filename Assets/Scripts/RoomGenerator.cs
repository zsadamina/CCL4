using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Classes;
using UnityEngine;
using UnityEngine.UIElements;

[Serializable]
public class RoomGenerator : MonoBehaviour
{
    public static RoomGenerator Instance;

    [SerializeField] private float roomSquare = 5f;

    [SerializeField] private GameObject hallwayPrefab;
    [SerializeField] private GameObject roomPrefab;

    private Maze maze;
    
    Dictionary<String,GameObject> InitializedRooms = new Dictionary<String, GameObject>();
    
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
    }

    public void setMaze(Maze maze) => this.maze = maze;

    public void GenerateRoomWithFiller(BaseCell room)
    {
        float x = room.Column * roomSquare * 3;
        float y = room.Row * roomSquare * -3;
        Instantiate(hallwayPrefab, new Vector3(x, 0, y), Quaternion.identity);

        GameObject topPrefab = !room.TopWall ? hallwayPrefab : roomPrefab;
        GameObject bottomPrefab = !room.BottomWall ? hallwayPrefab : roomPrefab;
        GameObject rightPrefab = !room.RightWall ? hallwayPrefab : roomPrefab;
        GameObject leftPrefab = !room.LeftWall ? hallwayPrefab : roomPrefab;

        Instantiate(topPrefab, new Vector3(x, 0, y + roomSquare), Quaternion.identity);
        Instantiate(bottomPrefab, new Vector3(x, 0, y - roomSquare), Quaternion.identity);
        Instantiate(rightPrefab, new Vector3(x + roomSquare, 0, y), Quaternion.identity);
        Instantiate(leftPrefab, new Vector3(x - roomSquare, 0, y), Quaternion.identity);
    }
    /*
    public void GenerateRooms(BaseCell[,] baseCell)
    {
        List<BaseCell> cells =  baseCell.Cast<BaseCell>().ToList();
        cells.ForEach(GenerateRoomWithoutFiller);
    }
    */

    public void GenerateRooms()
    {
        GenerateRoomWithoutFiller(maze.Cells[0, 0], 0, 0);
    }

    public void GenerateRoomWithoutFiller(BaseCell room, int column, int row)
    {
        float x = column * roomSquare;
        float y = row * -roomSquare;

        Debug.Log("Column: " + column + ", Row: " + row);

        GameObject cell = Instantiate(hallwayPrefab, new Vector3(x, 0, y), Quaternion.identity);
        InitializedRooms.Add($"{column}{row}", cell);
        if (!room.TopWall && !InitializedRooms.ContainsKey($"{column}{row - 1}"))
        {
            GenerateRoomWithoutFiller(maze.Cells[ row - 1,column], row - 1,column );
        }

        if (!room.BottomWall && !InitializedRooms.ContainsKey($"{column}{row + 1}"))
        {
            GenerateRoomWithoutFiller(maze.Cells[row + 1,column ], row + 1,column );
        }

        if (!room.RightWall && !InitializedRooms.ContainsKey($"{column + 1}{row}"))
        {
            GenerateRoomWithoutFiller(maze.Cells[row,column + 1], row,column + 1 );
        }

        if (!room.LeftWall && !InitializedRooms.ContainsKey($"{column - 1}{row}"))
        {
            GenerateRoomWithoutFiller(maze.Cells[row,column - 1], row,column - 1);
        }
    }
}