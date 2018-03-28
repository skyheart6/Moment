using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour {
    public GameObject pauseMenuUI;
    public GameObject controlMenuUI;

    public static bool gameIsPaused = false;
    public static bool inControlMenu = false;

    void Update () {
		if (Input.GetKeyDown(KeyCode.Escape) && !inControlMenu)
        {
            if (gameIsPaused)
                Resume();
            else
                Pause();
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && inControlMenu)
        {
            ExitFromOptions();
        }

    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        gameIsPaused = false;
    }

    void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        gameIsPaused = true;
    }

    public void Options()
    {
        pauseMenuUI.SetActive(false);
        controlMenuUI.SetActive(true);
        inControlMenu = true;
    }

    public void ExitFromOptions()
    {
        pauseMenuUI.SetActive(true);
        controlMenuUI.SetActive(false);
        inControlMenu = false;
    }

    public void QuitGame()
    {
        Debug.Log("Quitting Game.");
        Application.Quit();
    }
}
