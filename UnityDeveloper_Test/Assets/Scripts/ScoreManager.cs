using UnityEngine;
using UnityEngine.UI;  // Make sure to include this if you're using UI components

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;  // Singleton instance
    public Text scoreText;  // Reference to the UI Text component
    private int currentScore = 0;  // Initial score

    void Awake()
    {
        // Singleton pattern to ensure only one instance of ScoreManager exists
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        UpdateScoreText();  // Initialize score display
    }

    public void AddScore(int points)
    {
        currentScore += points;
        UpdateScoreText();
        Debug.Log("Score updated to: " + currentScore);
    }
    public int GetScore()
    {
        return currentScore;
    }

    private void UpdateScoreText()
    {
        scoreText.text = "Score: " + currentScore.ToString();
    }
    
}
