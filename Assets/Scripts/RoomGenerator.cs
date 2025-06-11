using System;
using System.Collections;
using System.Collections.Generic;
using Classes;
using UnityEngine;

[Serializable]
public class RoomGenerator : MonoBehaviour
{
    public static RoomGenerator Instance;

    [SerializeField] private float roomSquare = 5f;
    
    [SerializeField] private GameObject hallwayPrefab;
    [SerializeField] private GameObject roomPrefab;
    [SerializeField] private GameObject fillerPathPrefab;
    
    public List<Room> InitializedRooms { get; set; }
    
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

    public void InstanciateRoom(Room room)
    {

        float x = room.Column * roomSquare * -3;
        float y = room.Row * roomSquare * +3;
        
        var roomGO = Instantiate(hallwayPrefab, new Vector3(x, 0,y), Quaternion.identity);
        if (!room.TopWall)
        {
            Instantiate(fillerPathPrefab, new Vector3(x, 0,y + 50), Quaternion.identity);
        }
        else
        {
            Instantiate(roomPrefab, new Vector3(x, 0,y + 50), Quaternion.identity);

        }
        if (!room.BottomWall)
        {
            Instantiate(fillerPathPrefab, new Vector3(x, 0,y + 50), Quaternion.identity);
        }
        else
        {
            Instantiate(roomPrefab, new Vector3(x, 0,y + 50), Quaternion.identity);
        }
        
        if (!room.RightWall)
        {
            Instantiate(fillerPathPrefab, new Vector3(x - 50, 0,y), Quaternion.identity);
        }
        else
        {
            Instantiate(roomPrefab, new Vector3(x - 50, 0,y + 50), Quaternion.identity);
        }
        
        if (!room.LeftWall)
        {
            Instantiate(fillerPathPrefab, new Vector3(x, 0,y + 50), Quaternion.identity);
        }
        else
        {
            Instantiate(roomPrefab, new Vector3(x, 0,y + 50), Quaternion.identity);
        }
    }
}