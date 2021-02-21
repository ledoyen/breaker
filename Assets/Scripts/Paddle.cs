using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour
{

    // Screen width in units
    // Ex: given a camera of size 6 (half height), with a ratio of 4:3, width is 16 units
    [SerializeField] float screenWidthInUnits = 16F;
    private float paddleDemiLength;
    private GameSession gameSession;

    private PaddlePositionSupplier paddlePositionSupplier;

    // Start is called before the first frame update
    void Start()
    {
        paddleDemiLength = GetComponent<Renderer>().bounds.size.x / 2; // in units
        gameSession = FindObjectOfType<GameSession>();
        if (gameSession.IsAutoPlay())
        {
            paddlePositionSupplier = PaddlePositionSupplierFactory.fromBall();
        }
        else
        {
            paddlePositionSupplier = PaddlePositionSupplierFactory.fromMouse(screenWidthInUnits);
        }
    }

    // Update is called once per frame
    void Update()
    {
        float newX = paddlePositionSupplier.X;
        Vector3 position = transform.position;
        transform.position = new Vector2(newX, position.y);
        //Debug.Log(newX);
    }

    private void OnCollisionEnter2D(Collision2D collider)
    {
        gameSession.OnPaddleCollision();
    }
}
