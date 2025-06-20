using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Classes;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UIElements;

[Serializable]
public class RoomGenerator : MonoBehaviour
{
    public static RoomGenerator Instance;

    [SerializeField] private float roomSquare = 5f;
    
    [Range(2,5)]
    [SerializeField] public int Rows;
    
    [Range(2,5)]
    [SerializeField] public int Columns;
    

    [SerializeField] private GameObject hallwayPrefab;
    [SerializeField] private GameObject[] roomPrefabs;
    [SerializeField] private float[] propabilities;
    
    private Maze maze;
    
    public List<GameObject> paths = new List<GameObject>();
    
    Dictionary<String, GameObject> InitializedRooms = new Dictionary<String, GameObject>();

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
        this.maze = new Maze(Rows, Columns);
        maze.GenerateMaze();
        GenerateRooms();
        NavMeshManager.Instance?.BuildNavMesh();
    }
    
    public void GenerateRooms()
    {
        List<BaseCell> cells = maze.Cells.Cast<BaseCell>().ToList();
        cells.ForEach(GenerateRoomWithFiller);
    }

    /*
    public void GenerateRooms()
    {
        GenerateRoomWithoutFiller(maze.Cells[0, 0], 0, 0);
    }
    */

    public void GenerateRoomWithFiller(BaseCell room)
    {
        float x = room.Column * roomSquare * 3;
        float y = room.Row * roomSquare * -3;
        var midHallway = Instantiate(hallwayPrefab, new Vector3(x, 0, y), Quaternion.identity, this.gameObject.transform);
        paths.Add(midHallway);
        
        GameObject topPrefab = !room.TopWall ? hallwayPrefab : GetRandomRoomWithSpawnChance();
        GameObject bottomPrefab = !room.BottomWall ? hallwayPrefab : GetRandomRoomWithSpawnChance();
        GameObject rightPrefab = !room.RightWall ? hallwayPrefab : GetRandomRoomWithSpawnChance();
        GameObject leftPrefab = !room.LeftWall ? hallwayPrefab : GetRandomRoomWithSpawnChance();

        //Cross tiles
        Instantiate(topPrefab, new Vector3(x, 0, y + roomSquare), Quaternion.Euler(0, 90, 0),
            this.gameObject.transform); // T
        Instantiate(bottomPrefab, new Vector3(x, 0, y - roomSquare), Quaternion.Euler(0, -90, 0),
            this.gameObject.transform); // B
        Instantiate(rightPrefab, new Vector3(x + roomSquare, 0, y), Quaternion.Euler(0, 180, 0),
            this.gameObject.transform); // R
        Instantiate(leftPrefab, new Vector3(x - roomSquare, 0, y), Quaternion.Euler(0, 0, 0),
            this.gameObject.transform); // L

        //Corner Tiles
        if (!(room.TopWall && room.RightWall))
            Instantiate(GetRandomRoomWithSpawnChance(), new Vector3(x + roomSquare, 0, y + roomSquare),
                !room.TopWall ? Quaternion.Euler(0, 180, 0) : Quaternion.Euler(0, 90, 0),
                this.gameObject.transform); // TR
        if (!(room.BottomWall && room.LeftWall))
            Instantiate(GetRandomRoomWithSpawnChance(), new Vector3(x - roomSquare, 0, y - roomSquare),
                !room.BottomWall ? Quaternion.Euler(0, 0, 0) : Quaternion.Euler(0, -90, 0),
                this.gameObject.transform); // BL
        if (!(room.BottomWall && room.RightWall))
            Instantiate(GetRandomRoomWithSpawnChance(), new Vector3(x + roomSquare, 0, y - roomSquare),
                !room.BottomWall ? Quaternion.Euler(0, 180, 0) : Quaternion.Euler(0, -90, 0),
                this.gameObject.transform); // BR
        if (!(room.TopWall && room.LeftWall))
            Instantiate(GetRandomRoomWithSpawnChance(), new Vector3(x - roomSquare, 0, y + roomSquare),
                !room.TopWall ? Quaternion.Euler(0, 0, 0) : Quaternion.Euler(0, 90, 0),
                this.gameObject.transform); // TL
    }

    private GameObject GetRandomRoomWithSpawnChance()
    {
        int randomIndex = GetRandomRoomIndex();
        if (randomIndex == -1)
        {
            return null;
        }

        return roomPrefabs[randomIndex];
    }

    private int GetRandomRoomIndex()
    {
        if (propabilities == null || propabilities.Length == 0)
        {
            Debug.LogWarning("WeightedRandomSelector: Weights array is null or empty.");
            return -1;
        }

        if (roomPrefabs.Length != propabilities.Length)
        {
            Debug.LogWarning("WeightedRandomSelector: The Probality count does not match with the Room Prefab count.");
            return -1;
        }

        float totalWeight = propabilities.Sum();

        if (totalWeight <= 0f)
        {
            Debug.LogWarning("WeightedRandomSelector: Total weight is zero or negative.");
            return -1;
        }

        float randomNumber = UnityEngine.Random.Range(0f, totalWeight);

        float cumulativeWeight = 0f;
        for (int i = 0; i < propabilities.Length; i++)
        {
            if (propabilities[i] < 0f) continue; // Skip negative weights

            cumulativeWeight += propabilities[i];
            if (randomNumber <= cumulativeWeight)
            {
                return i;
            }
        }

        // Fallback for edge cases (e.g., float precision, or if randomNumber exactly equals totalWeight)
        return propabilities.Length - 1;
    }

    public void GenerateRoomWithoutFiller(BaseCell room, int row, int column)
    {
        float x = column * roomSquare;
        float y = row * -roomSquare;

        Debug.Log("Column: " + column + ", Row: " + row);

        GameObject cell = Instantiate(hallwayPrefab, new Vector3(x, 0, y), Quaternion.identity);
        InitializedRooms.Add($"{column}{row}", cell);
        if (!room.TopWall && !InitializedRooms.ContainsKey($"{column}{row - 1}"))
        {
            GenerateRoomWithoutFiller(maze.Cells[column, row - 1], row - 1, column);
            return;
        }

        if (!room.BottomWall && !InitializedRooms.ContainsKey($"{column}{row + 1}"))
        {
            GenerateRoomWithoutFiller(maze.Cells[column, row + 1], row + 1, column);
            return;
        }

        if (!room.RightWall && !InitializedRooms.ContainsKey($"{column + 1}{row}"))
        {
            GenerateRoomWithoutFiller(maze.Cells[column + 1, row], row, column + 1);
            return;
        }

        if (!room.LeftWall && !InitializedRooms.ContainsKey($"{column - 1}{row}"))
        {
            GenerateRoomWithoutFiller(maze.Cells[column - 1, row], row, column - 1);
        }
    }
}