using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Player player;
    public Text scoreText;
    public GameObject playButton;
    public GameObject gameOverPanel;
    public Vector3 playerStartPosition = Vector3.zero; // Add this for custom start position

    private int score;
    private bool gameOver = true;

    public void Awake()
    {
        Application.targetFrameRate = 60;
        Pause();
    }

    private void Update()
    {
        // If game is over and player clicks or presses space, start a new game
        if (gameOver && (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space)))
        {
            Play();
        }
    }

    public void Play()
    {
        Debug.Log("Play method called!");
        gameOver = false;
        
        score = 0;
        if (scoreText != null) scoreText.text = score.ToString();
        if (playButton != null) playButton.SetActive(false);
        if (gameOverPanel != null) gameOverPanel.SetActive(false);

        // Reset player position and direction
        if (player != null)
        {
            player.transform.position = playerStartPosition;
            player.ResetDirection();
        }

        Time.timeScale = 1f;
        if (player != null) player.enabled = true;

        Pipes[] pipes = FindObjectsOfType<Pipes>();
        for (int i = 0; i < pipes.Length; i++)
        {
            Destroy(pipes[i].gameObject);
        }
    }

    public void Pause()
    {
        Time.timeScale = 0f;
        if (player != null) player.enabled = false;
    }

    public void GameOver()
    {
        gameOver = true;
        if (gameOverPanel != null) gameOverPanel.SetActive(true);
        if (playButton != null) playButton.SetActive(true);
        Pause();
    }

    public void IncreaseScore()
    {
        Debug.Log("Score increased!"); // Add this to debug
        score++;
        if (scoreText != null) scoreText.text = score.ToString();
    }
}