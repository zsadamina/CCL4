using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// Functions handling UI Interactions
public class MainMenu : MonoBehaviour
{  
    public void StartGame()
    {
        SceneManager.LoadScene("Warehouse");
    }

    public void StartLevel()
    {
        SceneManager.LoadScene("Level");
    }

    public void LoadMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void QuitGame() 
    {
        Debug.Log("Quitting the Game...");
        Application.Quit();
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
