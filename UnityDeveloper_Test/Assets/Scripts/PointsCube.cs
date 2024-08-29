using UnityEngine;

public class PointsCube : MonoBehaviour
{
    public int points = 10;  // Points to award when collected

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Triggered by: " + other.gameObject.name);  // Log what is triggering

        if (other.CompareTag("Player"))
        {
            Debug.Log("Player collected the cube");

            if (ScoreManager.Instance != null)
            {
                Debug.Log("Adding points: " + points);
                ScoreManager.Instance.AddScore(points);
                Destroy(gameObject);  // Remove the cube after collection
            }
            else
            {
                Debug.LogError("ScoreManager instance is not found.");
            }
        }
    }
}
