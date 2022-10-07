using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenusController : MonoBehaviour
{
    public void Play()
    {
        SceneManager.LoadScene("InGame", LoadSceneMode.Single);
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
    }

    public void ShowWin()
    {
        SceneManager.LoadScene("WinScreen", LoadSceneMode.Single);
    }

    public void ShowLose()
    {
        SceneManager.LoadScene("LoseScreen", LoadSceneMode.Single);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
