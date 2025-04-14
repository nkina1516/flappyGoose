using UnityEngine;

public class ScoringZone : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Something entered scoring zone: " + other.gameObject.name);
        
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player entered scoring zone!");
            
            GameManager gameManager = FindObjectOfType<GameManager>();
            if (gameManager != null)
            {
                gameManager.IncreaseScore();
            }
            else
            {
                Debug.LogError("GameManager not found!");
            }
        }
    }
}