using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{

    private GameSession gameStatus;

    void Start()
    {
        gameStatus = FindObjectOfType<GameSession>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        gameStatus.OnWallCollision();
    }
}
