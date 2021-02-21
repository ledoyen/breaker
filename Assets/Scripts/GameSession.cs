using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameSession : MonoBehaviour
{
    [SerializeField] [Range(0.1F, 1F)] private float gameSpeed = 0.67F;
    [SerializeField] bool autoplay = false;

    private float comboMultiplier = 1F;
    private int score = 0;
    private Text scoreText;
    private Text comboText;

    private Ball ball;

    void Awake()
    {
        if (FindObjectsOfType<GameSession>().Length > 1)
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    void Start()
    {
        UpdateDisplay();
        ball = FindObjectOfType<Ball>();
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        ball = FindObjectOfType<Ball>();
        var sceneCount = SceneManager.sceneCountInBuildSettings;
        var currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        var gameOver = (sceneCount - 1) == currentSceneIndex;
        if (gameOver)
        {
            Destroy(gameObject);
        }
    }

    private void UpdateDisplay()
    {
        if (scoreText == null)
        {
            InjectTexts();
        }
        scoreText.text = score.ToString();
        comboText.text = comboMultiplier == 1F ? "" : "Combo x" + comboMultiplier;
    }

    private void InjectTexts()
    {
        Text[] texts = FindObjectsOfType<Text>();
        foreach (var text in texts)
        {
            if ("ScoreText".Equals(text.gameObject.name))
            {
                scoreText = text;
            }
            else if ("ComboText".Equals(text.gameObject.name))
            {
                comboText = text;
            }
        }
    }

    public float LaunchVelocity()
    {
        return gameSpeed * 15;
    }

    public Boolean IsAutoPlay()
    {
        return autoplay;
    }

    public Ball GetBall()
    {
        return ball;
    }

    public void OnWallCollision()
    {
        //comboMultiplier = 1;
        UpdateDisplay();
    }

    public void OnPaddleCollision()
    {
        comboMultiplier = 1;
        UpdateDisplay();
    }

    public void OnBlockDestruction()
    {
        score += (int)Math.Round(comboMultiplier * 10);
        comboMultiplier += 0.5F;
        UpdateDisplay();
    }

    void OnEnable()
    {
        //Tell our 'OnLevelFinishedLoading' function to start listening for a scene change as soon as this script is enabled.
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        //Tell our 'OnLevelFinishedLoading' function to stop listening for a scene change as soon as this script is disabled. Remember to always have an unsubscription for every delegate you subscribe to!
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
