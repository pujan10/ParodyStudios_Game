using UnityEngine;
using UnityEngine.UI; // Import for UI elements
using TMPro; // Import for TextMeshPro UI elements
using UnityEngine.SceneManagement;

public class GameTimer : MonoBehaviour
{
    public Text timerText; // Use TextMeshProUGUI if you're using TextMeshPro, or Text if you're using default Unity Text
    public float startTime = 120f; // Start time in seconds
    private float currentTime;
    private bool timerRunning = true;

    public GameObject gameOverPanel; // Panel to display on game over
    public GameObject victoryPanel;  // Panel to display on victory
    public int pointsRequiredForVictory = 50; // Points needed to win

    void Start()
    {
        currentTime = startTime;
        UpdateTimerText();
        gameOverPanel.SetActive(false); // Ensure the panel is initially hidden
        victoryPanel.SetActive(false); // Ensure the panel is initially hidden
    }

    void Update()
    {
        if (timerRunning)
        {
            currentTime -= Time.deltaTime;
            UpdateTimerText();

            if (currentTime <= 0)
            {
                currentTime = 0;
                timerRunning = false;
                CheckEndConditions();
            }
        }
    }

    void UpdateTimerText()
    {
        int minutes = Mathf.FloorToInt(currentTime / 60);
        int seconds = Mathf.FloorToInt(currentTime % 60);
        timerText.text = string.Format("Time: {0:00}:{1:00}", minutes, seconds);
    }

    void CheckEndConditions()
    {
        if (ScoreManager.Instance.GetScore() < pointsRequiredForVictory)
        {
            // Trigger game over if score is less than required points
            GameOver();
        }
        else if (ScoreManager.Instance.GetScore() >= pointsRequiredForVictory)
        {
            // Trigger victory if score is equal to or more than required points
            Victory();
        }
    }

    public void GameOver()
    {
        Debug.Log("Game Over");
        timerRunning = false;
        gameOverPanel.SetActive(true);
        // Optional: Add logic to disable player controls or other game objects
    }

    public void Victory()
    {
        Debug.Log("Victory!");
        timerRunning = false;
        victoryPanel.SetActive(true);
        // Optional: Add logic to disable player controls or other game objects
    }

    public void StopTimer()
    {
        timerRunning = false;
    }

    public void StartTimer()
    {
        timerRunning = true;
    }
}
