using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinDefeatScreen : MonoBehaviour
{
    public static WinDefeatScreen Instance { get; set; }


    [SerializeField] GameObject screen;
    [SerializeField] GameObject menuScreen;
    [SerializeField] CinemachineBrain cinemachineBrain;
    bool onScreen = false;
    float timeScaleBeforeScreen = 1f;
    bool cursorWasVisible;
    CursorLockMode cursorWaslocked;

    [SerializeField] GameObject minimapCanvas;

    void Start()
    {
        startScreen();
        screen.SetActive(false);


    }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    public void startScreen()
    {
        onScreen = true;

        cursorWasVisible = Cursor.visible;
        cursorWaslocked = Cursor.lockState;

        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        screen.SetActive(onScreen);

        cinemachineBrain.enabled = !onScreen; //disable camera control with mouse movement

        timeScaleBeforeScreen = Time.timeScale; //store value

        Time.timeScale = 0f;
        minimapCanvas.SetActive(false);
    }

    public void closeScreen()
    {
        onScreen = false;

        Cursor.visible = cursorWasVisible;
        Cursor.lockState = cursorWaslocked;

        screen.SetActive(onScreen);

        cinemachineBrain.enabled = !onScreen; //enable camera control with mouse movement

        Time.timeScale = timeScaleBeforeScreen;
        minimapCanvas.SetActive(true);
    }

    public void PlayAgain(mainMenu mainMenu)
    {
        closeScreen();
        ReloadScene();
    }
    private void ReloadScene()
    {
        // Get the name of the current scene
        string currentSceneName = SceneManager.GetActiveScene().name;
        // Reload the current scene
        SceneManager.LoadScene(currentSceneName);

    }

}
