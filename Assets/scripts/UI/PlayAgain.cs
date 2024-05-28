using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayAgain : MonoBehaviour
{
    public void OnPlayButtonClick()
    {
        Audio.AudioController.PlayCommand(Audio.AudioController._buttonPress);
        SceneManager.LoadScene("Transition");
    }
    
}
