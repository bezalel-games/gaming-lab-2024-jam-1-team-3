using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PauseGame : MonoBehaviour
{
    [SerializeField] private GameObject _unpauseTextGO;
    public static bool _isPaused;
    [SerializeField] private GameObject MenuPanel;
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }
    }


    public void Pause()
        //TODO Move pause game to different script
    {
        _isPaused = true;
        Time.timeScale = 0f; // Stop time to pause the game
        _unpauseTextGO.SetActive(true);
        MenuPanel.SetActive(true);

    }
    
    public void Resume()
    {
        _isPaused = false;
        Time.timeScale = 1f; // Resume time to unpause the game
        _unpauseTextGO.SetActive(false);
        MenuPanel.SetActive(false); // Ensure the panel is disabled at the start
    }
    
    public void TogglePause()
    {
        if (_isPaused)
        {
            Resume();
        }
        else
        {
            Pause();
        }
    }
   

    
}
