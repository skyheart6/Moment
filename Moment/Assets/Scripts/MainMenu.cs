using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MainMenu : MonoBehaviour {
    public GameObject startMenuUI;
    public GameObject levelSelectMenuUI;
    public GameObject optionMenuUI;
    public GameObject creditsMenuUI;

    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void LevelMenuSelect()
    {
        startMenuUI.SetActive(false);
        levelSelectMenuUI.SetActive(true);
        
    }

    public void OptionMenuSelect()
    {
        startMenuUI.SetActive(false);
        optionMenuUI.SetActive(true);
    }

    public void CreditsMenuSelect()
    {
        startMenuUI.SetActive(false);
        creditsMenuUI.SetActive(true);

    }


    public void returnToMenu()
    {
        levelSelectMenuUI.SetActive(false);
        optionMenuUI.SetActive(false);
        creditsMenuUI.SetActive(false);
        startMenuUI.SetActive(true);
    }

    public void Level2()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 2);
    }

    public void Level3()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 3);
    }

    public void BossLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 4);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
