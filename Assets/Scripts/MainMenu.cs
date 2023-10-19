using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Main Menu/Screen displayed on game start.
/// </summary>
/// 
/// <remarks>
/// Instantiated By: Main_Menu Scene
/// Attached To: Main Menu Canvas
/// 
/// </remarks>
/// 

public class MainMenu : MonoBehaviour
{
    /// <summary>
    /// Sit and spin waiting for the Escape key press to exit the application.
    /// </summary>
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
            Debug.Log("Quiting Game");
        }
    }

    /// <summary>
    /// Load the main game when "Play Game" button us pressed.
    /// </summary>
    public void LoadScene()
    {
        SceneManager.LoadScene("Game"); 
    }
}
