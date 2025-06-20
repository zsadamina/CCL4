using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndTutorialScript : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {

    }
    
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && StateManager.allItemsCollected)
        {
            StateManager.allItemsCollected = false;
            SceneManager.LoadScene("Level");
        }
    }
}
