using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TransitionScene : MonoBehaviour
{
    public static TransitionScene Instance;
    public GameObject fadeInAnimator;
    public GameObject fadeOutAnimator;

    private void Start()
    {
        Debug.Log("START IS CALLED");
        LoadNextLevelFromTransition();
        fadeInAnimator.SetActive(true); 
        fadeOutAnimator.SetActive(false); 
        
    }
    public void LoadNextLevelFromTransition()
    {
        string lastSceneName = PlayerPrefs.GetString("LastScene", "DefaultSceneName");
        // Determine the next scene based on the last scene
        string nextSceneName = DetermineNextScene(lastSceneName);
        LoadNextScene(nextSceneName);
        
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
                return "WIN";
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
        yield return new WaitForSeconds(2.5f);
        fadeOutAnimator.SetActive(true); 
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene(sceneName); // Load the target scene
    }

    
}
