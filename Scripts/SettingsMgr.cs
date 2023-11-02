using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;
using static System.Net.Mime.MediaTypeNames;
using System.ComponentModel;

public class SettingsMgr : MonoBehaviour
{
    public SceneLoader SceneLoader;

    public string state = "mainMenu";
    [SerializeField]
    private string sceneNameToLoad = "MainMenuHub";

    public GameObject MainMenuCam;
    public GameObject CreateCharCam;
    public GameObject LoadCharCam;

    public GameObject playerGUICanvas;
    public GameObject skillsCanvas;
    public GameObject inventoryCanvas;
    public GameObject questMapCanvas;
    public GameObject settingsCanvas;
    public GameObject createCharCanvas;
    //public GameObject LoadGameCanvas;
    public GameObject mainMenuCanvas;

    public bool settingsOn = false;
    // Start is called before the first frame update
    void Start()
    {
        if(MainMenuCam == null)
        {
            return;
        }

        MainMenuCam.SetActive(true);
        CreateCharCam.SetActive(false);
        LoadCharCam.SetActive(false);
        settingsCanvas.SetActive(false);
        //LoadGameCanvas.SetActive(false);
        mainMenuCanvas.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        // Check if the Escape key is pressed down this frame
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            UnityEngine.Debug.Log("Escape key was pressed!");
            if (settingsOn)
            {
                UnPauseGame();
                settingsOn = false;
            }
            else
            {
                PauseGame();
                settingsOn = true;
            }

            // You can add any logic here. For example, pausing the game:
            // PauseGame();
        }

        if (settingsCanvas.activeInHierarchy)
        {
            createCharCanvas.SetActive(false);
            //LoadGameCanvas.SetActive(false);
        }
        else
        {
            if (state == "mainMenu")
            {
                mainMenuCanvas.SetActive(true);
                UnityEngine.Debug.Log("pass");
            }
            else if (state == "createGame")
            {
                createCharCanvas.SetActive(true);
                UnityEngine.Debug.Log("create game");
            }
            else if (state == "loadMenu")
            {
                //LoadGameCanvas.SetActive(true);
                UnityEngine.Debug.Log("load game");
            }
            else if (state == "start")
            {
                if(mainMenuCanvas == null)
                {
                    return;
                }

                mainMenuCanvas.SetActive(false);
                createCharCanvas.SetActive(false);
                //LoadGameCanvas.SetActive(false);
                UnityEngine.Debug.Log("start game");
            }
        }
    }

    public void ClickedSteamForums()
    {
        UnityEngine.Application.OpenURL("https://steamcommunity.com/app/1972360/discussions/");
    }

    public void PauseGame()
    {
        Time.timeScale = 0f;  // This stops the flow of time in the game
        settingsCanvas.SetActive(true);
    }

    public void UnPauseGame()
    {
        Time.timeScale = 1f;  // This resumes the normal flow of time
        settingsCanvas.SetActive(false);
        skillsCanvas.SetActive(false);
        inventoryCanvas.SetActive(false);
        questMapCanvas.SetActive(false);
        settingsCanvas.SetActive(false);
        settingsOn = false;
    }

    public void Quit()
    {
        UnityEngine.Debug.Log("quitting");
        // Close the application
        UnityEngine.Application.Quit();

#if UNITY_EDITOR
        // Close the editor
        EditorApplication.ExitPlaymode();
#endif
    }

    public void GoToMainMenu()
    {
        
        UnPauseGame();
        if (!string.IsNullOrEmpty(sceneNameToLoad))
        {
            //LoadSceneAsync(sceneNameToLoad);
            UnityEngine.Debug.Log("loading next scene: " + sceneNameToLoad);
            //state = "mainMenu";
            // Load the next scene using the SceneLoader script.
            SceneLoader.LoadNextScene(sceneNameToLoad);
        }
    }

    public void LoadSceneAsync(string sceneName)
    {
        UnPauseGame();
        StartCoroutine(LoadAsyncRoutine(sceneName));
    }

    private IEnumerator LoadAsyncRoutine(string sceneName)
    {
        UnityEngine.AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);

        while (!asyncLoad.isDone)
        {
            // Here you can update a UI element to display loading progress, e.g. a slider or text
            // float progress = Mathf.Clamp01(asyncLoad.progress / 0.9f);
            // Debug.Log("Loading progress: " + (progress * 100) + "%");

            yield return null;
        }

        // Scene has finished loading, you can perform any additional setup or initialization here if needed
    }

    public void ClickedInventoryButtonFromSettings()
    {
        settingsCanvas.SetActive(false);
        inventoryCanvas.SetActive(true);
    }

    public void ClickedSKillsButtonFromSettings()
    {
        settingsCanvas.SetActive(false);
        skillsCanvas.SetActive(true);
    }

    public void ClickedQuestMapButtonFromSettings()
    {
        settingsCanvas.SetActive(false);
        questMapCanvas.SetActive(true);
    }

    public void ClickedCreateGame() 
    {
        state = "createGame";
        mainMenuCanvas.SetActive(false);

        MainMenuCam.SetActive(false);
        CreateCharCam.SetActive(true);
        LoadCharCam.SetActive(false);


    }

    public void FinishLoadGame()
    {
        // Check if we can make a profile etc

        MainMenuCam.SetActive(false);
        CreateCharCam.SetActive(false);
        LoadCharCam.SetActive(false);
        state = "start";
    }

    public void FinishCreateGame()
    {

        // Check if we can make a profile etc

        MainMenuCam.SetActive(false);
        CreateCharCam.SetActive(false);

        state = "start";
    }

    public void ClickedReturnFromCreateGame()
    {
        state = "mainMenu";
        MainMenuCam.SetActive(true);
        CreateCharCam.SetActive(false);
        LoadCharCam.SetActive(false);
    }


    public void ClickedLoadGame()
    {
        mainMenuCanvas.SetActive(false);
        state = "loadMenu";
        MainMenuCam.SetActive(false);
        CreateCharCam.SetActive(false);
        LoadCharCam.SetActive(true);

        //LoadGameCanvas.SetActive(true);
        mainMenuCanvas.SetActive(false);
    }

    public void ClickedReturnFromLoadGame()
    {
        state = "mainMenu";
        MainMenuCam.SetActive(true);
        CreateCharCam.SetActive(false);
        LoadCharCam.SetActive(false);

        //LoadGameCanvas.SetActive(false);
        mainMenuCanvas.SetActive(true);
    }

    public void ClickedSettings()
    {
        PauseGame();
    }

    public void ClickedReturnFromSettings()
    {
        UnPauseGame();
    }
}
