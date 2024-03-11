using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class mainMenu : MonoBehaviour
{
    public string sceneName;
    public void PlayEasyGame()
    {
        SceneManager.LoadScene(sceneName);
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
