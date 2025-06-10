using System;
using System.Collections;
using System.Collections.Generic;
using Classes;
using UnityEngine;

public class StateManager : MonoBehaviour
{
    public static StateManager Instance;

    public Room[,] Rooms;

    [Range(2,5)]
    [SerializeField] public int Rows;
    
    [Range(2,5)]
    [SerializeField] public int Columns;
    
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
        Rooms = new Room[Rows, Columns];
        int[] middle = {Rows/2, Columns/2};
        
        Rooms[middle[0],middle[1]] = new Room(RoomType.LivingRoom); 
    }
}
