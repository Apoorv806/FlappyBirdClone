using UnityEngine;

public class Obstacle : MonoBehaviour
{
    #region Fields
    const float velocity = 2f; // Speed of the obstacle
    const float leftBound = -10f; // Left boundary of the screen
    int scoreValue = 1;
    ScoreText scoreText; // Reference to the ScoreText script
    #endregion

    private void Start()
    {
        scoreText = FindObjectOfType<ScoreText>(); // Find the ScoreText script in the scene
    }
    private void Update()
    {
        // Move the obstacle to the left at a constant speed regardless of rotation
        // This ensures that the obstacle moves left even if it is rotated

        transform.Translate(Vector3.left * velocity * Time.deltaTime, Space.World);

        //destroy the obstacle if it goes off-screen
        if (transform.position.x < leftBound)
        {
            // Increment the score when the obstacle is destroyed
            if (scoreText != null)
            {
                scoreText.IncrementScore(scoreValue); // Increment the score in the ScoreText script
            }
            Destroy(gameObject);
        }
    }

    public void UpdatedScoreValue(int scoreValue)
    {
               this.scoreValue = scoreValue; // Update the score value for the obstacle
    }
}
