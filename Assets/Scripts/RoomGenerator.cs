using System.Collections;
using System.Collections.Generic;
using Classes;
using UnityEngine;

public class RoomGenerator : MonoBehaviour
{
    public static RoomGenerator Instance;

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

    public void InitializeRoom(Room room)
    {
        
    }
}