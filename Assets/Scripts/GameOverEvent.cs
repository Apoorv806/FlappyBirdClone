using UnityEngine;

public class GameOverEvent : MonoBehaviour
{
    public delegate void PlayerDied();
    /// <summary>
    /// Event that is triggered when the player dies.
    /// </summary>
    public static event PlayerDied OnPlayerDied;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject); // Destroy the player object on collision
        GameOver(); // Call the GameOver method
    }

    /// <summary>
    /// Invokes the OnPlayerDied event to signal that the player has died.
    /// </summary>
    void GameOver()
    {
        // Trigger the player died event
        OnPlayerDied?.Invoke();
        // Optionally, you can add additional logic here, such as showing a game over screen or restarting the game
        Debug.Log("Game Over! Player has died.");
    }
}
