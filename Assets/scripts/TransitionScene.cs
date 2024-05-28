using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TransitionScene : MonoBehaviour
{
    public static TransitionScene Instance;

    private void Start()
    {
        Debug.Log("START IS CALLED");
        LoadNextLevelFromTransition();
    }
    public void LoadNextLevelFromTransition()
    {
        string lastSceneName = PlayerPrefs.GetString("LastScene", "DefaultSceneName");
        // Determine the next scene based on the last scene
        string nextSceneName = DetermineNextScene(lastSceneName);
        SceneManager.LoadScene(nextSceneName);
        
    }

    private string DetermineNextScene(string lastSceneName)
    {
        switch (lastSceneName)
        {
            case "Tutorial":
                return "Level 1";
            case "Level 1":
                return "Level 2";
            case "Level 2":
                return "Level 3";
            case "Level 3":
                return "WinScene";
            default:
                return "MainMenu"; // Go back to main menu if there's an error
        }
    }
    public void LoadNextScene(string sceneName)
    {
        StartCoroutine(WaitAndLoadScene(sceneName));
    }

    private IEnumerator WaitAndLoadScene(string sceneName)
    {
        yield return new WaitForSeconds(3); 
        SceneManager.LoadScene(sceneName); // Load the target scene
    }

    
}
