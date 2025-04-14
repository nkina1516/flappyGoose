using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    
    private SpriteRenderer spriteRenderer;
    public Sprite[] sprites;

    private int spriteIndex;
    public float strength = 5f;
    public float gravity = -9.81f;
    private Vector3 direction;
    

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        ResetDirection();
        InvokeRepeating(nameof(AnimateSprite), 0.15f, 0.15f);
    }

    public void ResetDirection()
    {
        direction = Vector3.zero;
    }

    private void Update()
    {
        // Only accept input when the game is actually running
        if (Time.timeScale > 0)
        {
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0)) {
                direction = Vector3.up * strength;
            }

            // Apply gravity and update the position
            direction.y += gravity * Time.deltaTime;
            transform.position += direction * Time.deltaTime;
        }
    }

    private void AnimateSprite()
    {
        spriteIndex++;

        if (spriteIndex >= sprites.Length) {
            spriteIndex = 0;
        }

        if (spriteIndex < sprites.Length && spriteIndex >= 0) {
            spriteRenderer.sprite = sprites[spriteIndex];
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        Debug.Log("Collision with: " + other.gameObject.name + " - Tag: " + other.gameObject.tag);
        
        GameManager gameManager = FindObjectOfType<GameManager>();
        if (gameManager == null) {
            Debug.LogError("GameManager not found in the scene!");
            return;
        }
        
        if (other.gameObject.CompareTag("Obstacle")) {
            gameManager.GameOver();
        } else if (other.gameObject.name == "Scoring") {
            gameManager.IncreaseScore();
        }
    }

    // Add this to also detect regular collisions (not just triggers)
    private void OnCollisionEnter2D(Collision2D collision) {
        Debug.Log("Collision with: " + collision.gameObject.name + " - Tag: " + collision.gameObject.tag);
        
        if (collision.gameObject.CompareTag("Obstacle")) {
            GameManager gameManager = FindObjectOfType<GameManager>();
            if (gameManager != null) {
                gameManager.GameOver();
            }
        }
    }
}