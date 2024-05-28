using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NewBehaviourScript : MonoBehaviour
{
    public void OnPlayButtonClick()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
        
    }
}
