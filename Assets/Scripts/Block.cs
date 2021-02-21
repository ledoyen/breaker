using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    [SerializeField] private AudioClip destructionSound;
    [SerializeField] private GameObject blockDestructionVFX;
    [SerializeField] private Sprite[] hitSprites = {};

    private Level level;
    private GameSession gameStatus;
    private SpriteRenderer spriteRenderer;

    private int timesHit = 0;
    private int maxHits;

    void Start()
    {
        level = GameObject.FindObjectOfType<Level>();
        if (tag.Equals("Breakable"))
        {
            level.NewBlockCreated();
        }
        gameStatus = FindObjectOfType<GameSession>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        maxHits = hitSprites.Length + 1;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (tag.Equals("Breakable"))
        {
            if(hitSprites.Length > timesHit) {
                spriteRenderer.sprite = hitSprites[timesHit];
            }
            timesHit++;
            if (timesHit >= maxHits)
            {
                DestroyBlock();
            }
        }
    }

    private void DestroyBlock()
    {
        AudioSource.PlayClipAtPoint(destructionSound, Camera.main.transform.position);
        level.BlockDestroyed();
        gameStatus.OnBlockDestruction();

        GameObject particles = Instantiate(blockDestructionVFX, transform.position, transform.rotation);
        Destroy(particles, 2f);
        Destroy(gameObject);
    }
}
