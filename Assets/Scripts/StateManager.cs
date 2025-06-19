using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Classes;
using UnityEngine;

public class StateManager : MonoBehaviour
{
    public static StateManager Instance;
    
    
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
    }
}
