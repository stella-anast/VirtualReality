using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class mainMenu : MonoBehaviour
{

    public void PlayStartGame()
    {
        SceneManager.UnloadSceneAsync("MainMenu");
    }

    public void PlayEasyGame()
    {
        SceneManager.LoadScene("MainScene");
        SceneManager.UnloadSceneAsync("MainMenu");
    }
    public void PlayMediumGame()
    {
        SceneManager.LoadScene("Medium");
        SceneManager.UnloadSceneAsync("MainMenu");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
   
}
