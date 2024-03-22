using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DefeatScreen : MonoBehaviour
{
    public static DefeatScreen Instance { get; set; }

    [SerializeField] GameObject defeatScreen;
    [SerializeField] GameObject menuScreen;
    [SerializeField] CinemachineBrain cinemachineBrain;
    bool onDefeatScreen = false;
    float timeScaleBeforeDefeatScreen = 1f;
    bool cursorWasVisible;
    CursorLockMode cursorWaslocked;

    [SerializeField] GameObject minimapCanvas;
    void Start()
    {
        startDefeatScreen();
        defeatScreen.SetActive(false);


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

    public void startDefeatScreen()
    {
        onDefeatScreen = true;

        cursorWasVisible = Cursor.visible;
        cursorWaslocked = Cursor.lockState;

        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        defeatScreen.SetActive(onDefeatScreen);

        cinemachineBrain.enabled = !onDefeatScreen; //disable camera control with mouse movement

        timeScaleBeforeDefeatScreen = Time.timeScale; //store value

        Time.timeScale = 0f;
        minimapCanvas.SetActive(false);
    }

    public void closeDefeatScreen()
    {
        onDefeatScreen = false;

        Cursor.visible = cursorWasVisible;
        Cursor.lockState = cursorWaslocked;

        defeatScreen.SetActive(onDefeatScreen);

        cinemachineBrain.enabled = !onDefeatScreen; //enable camera control with mouse movement

        Time.timeScale = timeScaleBeforeDefeatScreen;
        minimapCanvas.SetActive(true);
    }

    public void PlayAgain(mainMenu mainMenu)
    {
        closeDefeatScreen();
        mainMenu.Menu();
        menuScreen.SetActive(true);
    }

}
