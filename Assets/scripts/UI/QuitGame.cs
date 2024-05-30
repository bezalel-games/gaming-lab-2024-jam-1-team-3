using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitGame : MonoBehaviour
{
   public void Quit()
    {
        //Audio.AudioController.PlayCommand(Audio.AudioController._buttonPress);
        Debug.Log("Game is quitting...");
        Application.Quit();

        // If running in the editor
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
