using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PauseGame : MonoBehaviour
{
    [SerializeField] private GameObject _unpauseTextGO;
    private bool _isPaused;
    
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
    }
    
    public void Resume()
    {
        _isPaused = false;
        Time.timeScale = 1f; // Resume time to unpause the game
        _unpauseTextGO.SetActive(false);
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
