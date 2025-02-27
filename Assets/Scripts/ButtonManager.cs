using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{
    public GameObject tutorialPanel;
    public GameObject introPanel;
    public GameManager gameManager;

    public void HowToPlay()
    {
        tutorialPanel.SetActive(true);
    }

    public void ExitButton()
    {
        tutorialPanel.SetActive(false);
    }

    public void ClickToStart()
    {       
        gameManager.ClearData();
        SceneManager.LoadScene(1);
        Time.timeScale = 1;
    }

    public void ClickToRestart()
    {
        gameManager.ClearData();
        SceneManager.LoadScene(1);
        Time.timeScale = 1;
    }

    public void ClickToHome()
    {
        gameManager.ClearData();
        SceneManager.LoadScene(0);
    }
}
