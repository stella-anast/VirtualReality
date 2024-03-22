using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HomeScreen : MonoBehaviour
{
    public static HomeScreen Instance { get; set; }

    [SerializeField] GameObject homeScreen;
    [SerializeField] GameObject menuScreen;
    [SerializeField] CinemachineBrain cinemachineBrain;
    bool onHomeScreen = false;
    float timeScaleBeforeHomeScreen = 1f;
    bool cursorWasVisible;
    CursorLockMode cursorWaslocked;

    [SerializeField] GameObject minimapCanvas;
    [SerializeField] GameObject healthBar;
    void Start()
    {
        startHomeScreen();
        homeScreen.SetActive(true);


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

    public void startHomeScreen()
    {
        onHomeScreen = true;

        cursorWasVisible = Cursor.visible;
        cursorWaslocked = Cursor.lockState;

        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        homeScreen.SetActive(onHomeScreen);

        cinemachineBrain.enabled = !onHomeScreen; //disable camera control with mouse movement

        timeScaleBeforeHomeScreen = Time.timeScale; //store value

        Time.timeScale = 0f;
        minimapCanvas.SetActive(false);
    }

    public void closeHomeScreen()
    {
        onHomeScreen = false;

        Cursor.visible = cursorWasVisible;
        Cursor.lockState = cursorWaslocked;

        homeScreen.SetActive(onHomeScreen);

        cinemachineBrain.enabled = !onHomeScreen; //enable camera control with mouse movement

        Time.timeScale = timeScaleBeforeHomeScreen;
        minimapCanvas.SetActive(true);
    }

    public void PlayStartGame(mainMenu mainMenu)
    {
        closeHomeScreen();
        menuScreen.SetActive(true);
        //mainMenu.Menu();
    }

}
