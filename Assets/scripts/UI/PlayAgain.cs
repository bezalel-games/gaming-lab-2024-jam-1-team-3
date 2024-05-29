using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayAgain : MonoBehaviour
{
    public void OnPlayButtonClick()
    {
        Time.timeScale = 1f;
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name);
        Audio.AudioController.PlayCommand(Audio.AudioController._buttonPress);
    }

    public void OnPlayLastSceneButtonClick()
    {
        Time.timeScale = 1f;
        // Retrieve the last played scene name from PlayerPrefs
        string lastSceneName = PlayerPrefs.GetString("LastScene", "DefaultSceneName");
        SceneManager.LoadScene(lastSceneName);
        Audio.AudioController.PlayCommand(Audio.AudioController._buttonPress);
    }

}
