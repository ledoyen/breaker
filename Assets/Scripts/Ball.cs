using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField] Paddle paddle;
    [SerializeField] AudioClip[] audioClips;
    [SerializeField] float screenWidthInUnits = 6F;

    Vector2 paddleToBallVector;
    Rigidbody2D rigidBody;
    AudioSource audioSource;
    bool launched = false;
    Vector2 lastMousePosition = Vector2.zero;
    private Vector2 mouseVelocity = Vector2.zero;

    private float launchVelocity;

    private bool autoplay;

    // Start is called before the first frame update
    void Start()
    {
        paddleToBallVector = transform.position - paddle.transform.position;
        rigidBody = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
        rigidBody.simulated = false;
        var gameSession = FindObjectOfType<GameSession>();
        launchVelocity = gameSession.LaunchVelocity();
        autoplay = gameSession.IsAutoPlay();
    }

    void FixedUpdate()
    {
        Vector2 mousePosition = Input.mousePosition;
        Vector2 mouseVelocity = mousePosition - lastMousePosition;
        lastMousePosition = mousePosition;
    }

    // Update is called once per frame
    void Update()
    {
        if (!launched)
        {
            LockToPaddle();
            LaunchOnMouseClick();
        }
    }

    private void OnCollisionEnter2D(Collision2D collider)
    {
        if (!"Breakable".Equals(collider.gameObject.tag))
        {
            //AudioSource.PlayClipAtPoint(audioClips[UnityEngine.Random.Range(0, audioClips.Length)], Camera.main.transform.position, 0.33f);
            audioSource.PlayOneShot(audioClips[UnityEngine.Random.Range(0, audioClips.Length)]);

            if (Mathf.Abs(rigidBody.velocity.x) < 0.2f)
            {
                float way = transform.position.x > screenWidthInUnits / 2 ? -1F : 1F;
                rigidBody.velocity += new Vector2(way * 1F, 0.0F);
            }

            if (Mathf.Abs(rigidBody.velocity.y) < 0.2f)
            {
                float way = transform.position.y > screenWidthInUnits / 2 * 3 / 4 ? -1F : 1F;
                rigidBody.velocity += new Vector2(0.0F, way * 1F);
            }
        }
    }

    private void LaunchOnMouseClick()
    {

        if (Input.GetMouseButtonDown(0) || autoplay)
        {
            launched = true;
            rigidBody.simulated = true;
            rigidBody.velocity = new Vector2(mouseVelocity.x, launchVelocity);
        }
    }

    private void LockToPaddle()
    {
        Vector2 paddlePos = paddle.transform.position;
        transform.position = paddlePos + paddleToBallVector;
    }
}
