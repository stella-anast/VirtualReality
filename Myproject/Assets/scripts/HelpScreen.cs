using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.SceneManagement;

//Manager class for pausing the app
public class HelpScreen : MonoBehaviour
{
    [SerializeField] GameObject helpScreen; 

    [SerializeField] CinemachineBrain cinemachineBrain;
    [SerializeField] KeyCode helpKey;
    bool onHelp = false;
    float timeScaleBeforeHelp = 1f;
    bool cursorWasVisible;
    CursorLockMode cursorWaslocked;

    [SerializeField] GameObject minimapCanvas;
    [SerializeField] GameObject healthBar;

    // Start is called before the first frame update
    void Start()
    {
        onHelp = false;

        timeScaleBeforeHelp = Time.timeScale; 
        cursorWasVisible = Cursor.visible;

        helpScreen.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        // When Escape button pressed, toggle between Pause and Unpause (Resume)
        if (Input.GetKeyUp(helpKey))
        {
            if (!onHelp)
            {
                //healthBar.SetActive(false);
                Help();
            }
            else
            {
                UnHelp();
            }
        }
    }

    public void Help()
    {
        minimapCanvas.SetActive(false);
        healthBar.SetActive(false);
        onHelp = true;

        // Cursor
        cursorWasVisible = Cursor.visible;
        cursorWaslocked = Cursor.lockState;

        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        helpScreen.SetActive(onHelp);

        //minimapCanvas.SetActive(false);

        healthBar.SetActive(false);


        cinemachineBrain.enabled = !onHelp; //disable camera control with mouse movement

        timeScaleBeforeHelp = Time.timeScale; //store value

        Time.timeScale = 0f;
    }

    public void UnHelp()
    {
        minimapCanvas.SetActive(true);
        onHelp = false;

        // Cursor
        Cursor.visible = cursorWasVisible;
        Cursor.lockState = cursorWaslocked;

        // Hide pause screen
        helpScreen.SetActive(onHelp);

        //Show minimap
        //minimapCanvas.SetActive(true);

        //Show health bar
        healthBar.SetActive(true);


        cinemachineBrain.enabled = !onHelp; 
        Time.timeScale = timeScaleBeforeHelp;
    }

    private void ReloadScene()
    {
        string currentSceneName = SceneManager.GetActiveScene().name;
        // Reload the current scene
        SceneManager.LoadScene(currentSceneName);

    }

    public void ExitToMainMenu()
    {
        UnHelp();
        ReloadScene();

    }
}