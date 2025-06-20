using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//Handle collision with the Archway at the end of the tutorial
public class EndTutorialScript : MonoBehaviour
{
    // if the player collides with the archway and all items are collected, load the Level scene
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && StateManager.allItemsCollected)
        {
            StateManager.allItemsCollected = false; // Reset the state for the next level
            SceneManager.LoadScene("Level");
        }
    }
}
