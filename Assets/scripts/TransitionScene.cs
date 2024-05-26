using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TransitionScene : MonoBehaviour
{
   private void Start()
    {
        // Start the coroutine to wait for 3 seconds and then load the Programmers scene
        StartCoroutine(LoadProgrammersSceneAfterDelay(3f));
    }

    private IEnumerator LoadProgrammersSceneAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene("Programmers");
    }
}
