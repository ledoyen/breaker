using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level : MonoBehaviour
{
    private int blockCount;

    private SceneLoader sceneLoader;

    void Start()
    {
        sceneLoader = FindObjectOfType<SceneLoader>();
    }

    public void NewBlockCreated()
    {
        blockCount++;
    }

    public void BlockDestroyed()
    {
        blockCount--;
        if (blockCount < 1)
        {
            sceneLoader.LoadNextScene();
        }
    }
}
