using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndTutorialScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    
    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Collision Detected with: " + other.name);
        if (other.CompareTag("Player") && StateManager.itemBuild)
        {
            Debug.Log("End of Tutorial Triggered");
            SceneManager.LoadScene("SampleScene");
        }
    }
}
