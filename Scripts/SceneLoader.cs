using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using System.ComponentModel;
using static System.Net.Mime.MediaTypeNames;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class SceneLoader : MonoBehaviour
{
    public GameObject ingameUI;
    public Slider loadingBar;
    public Canvas loadingCanvas;

    private UnityEngine.AsyncOperation asyncOperation;

    private void Start()
    {
        loadingCanvas.gameObject.SetActive(false);
    }

    public void Quit()
    {
        #if UNITY_EDITOR
            EditorApplication.isPlaying = false;
        #else
            UnityEngine.Application.Quit();
        #endif
    }

    public void LoadNextScene(string sceneName)
    {
        UnityEngine.Debug.Log("Loading the sceneName: " +  sceneName);
        ingameUI.SetActive(false);
        StartCoroutine(LoadSceneAsync(sceneName));
    }

    IEnumerator LoadSceneAsync(string sceneName)
    {
        loadingCanvas.gameObject.SetActive(true);

        // Begin loading the next scene asynchronously.
        asyncOperation = SceneManager.LoadSceneAsync(sceneName);

        // Allow the scene to load in the background.
        asyncOperation.allowSceneActivation = false;

        while (!asyncOperation.isDone)
        {
            // Update the loading bar with the progress.
            float progress = Mathf.Clamp01(asyncOperation.progress / 0.9f);
            loadingBar.value = progress;

            // Check if the loading is almost complete (0.9 is considered fully loaded).
            if (asyncOperation.progress >= 0.9f)
            {
                // Allow the scene to activate when fully loaded.
                asyncOperation.allowSceneActivation = true;
            }

            yield return null;
        }
    }

    public void RestartLevel()
    {
        loadingCanvas.gameObject.SetActive(true);
        ingameUI.SetActive(false);

        StartCoroutine(RestartLevelAsync());
    }

    IEnumerator RestartLevelAsync()
    {
        // Get the current scene name
        string currentSceneName = SceneManager.GetActiveScene().name;

        // Start loading the scene asynchronously and store the returned AsyncOperation
        UnityEngine.AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(currentSceneName);

        // While the scene is still loading...
        while (!asyncLoad.isDone)
        {
            // Update the slider's value to the current load progress
            loadingBar.value = asyncLoad.progress;

            // Yield the loop for this frame
            yield return null;
        }

        // Once the scene is loaded, you can set the slider's value to the max if you want
        loadingBar.value = 1f;
    }
}
