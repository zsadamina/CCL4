using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{  
    public void StartGame(string Warehouse)
    {
        SceneManager.LoadScene(Warehouse);
    }

    public void StartLevel(string Level)
    {
        SceneManager.LoadScene(Level);
    }

    public void LoadMenu(string MainMenu)
    {
        SceneManager.LoadScene(MainMenu);
    }

    public void QuitGame() 
    {
        Debug.Log("Quitting the Game...");
        Application.Quit();
    }
}
