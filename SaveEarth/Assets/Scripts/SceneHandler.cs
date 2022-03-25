using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneHandler : MonoBehaviour
{
    private void Update()
    {
        if(GameManager.instance)
        {
            if (GameManager.instance.health <= 0)
            {
                SwitchToExitScreen();
            }
        }
        if(SceneManager.GetActiveScene().name == "MainMenu")
        {
            if (Input.anyKeyDown)
                SwitchToGame();
        }
        
    }

    public void SwitchToGame()
    {
        SceneManager.LoadScene("3DGame");
        //CSVImportTool.TestMethod();
    }

    public void SwitchToExitScreen()
    {
        SceneManager.LoadScene("GameOver");
    }

    public void SwitchToMain()
    {
        SceneManager.LoadScene("MainMenu");
    }
    public void ExitApp()
    {
        Application.Quit(1);
    }
}
