using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public void LoadNextScene() {
         int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
         int sceneCount = SceneManager.sceneCountInBuildSettings;
         int nextSceneIndex = (currentSceneIndex + 1) % sceneCount;

         SceneManager.LoadScene(nextSceneIndex);
    }

    public void ExitGame() {
        Application.Quit();
    }
}
