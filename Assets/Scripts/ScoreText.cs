using UnityEngine;
using TMPro; // Importing TextMeshPro for text rendering

public class ScoreText : MonoBehaviour
{
    #region Fields
    //variables
    int totalScore = 0; // Total score of the player
    // TextMeshPro component to display the score
    TMPro.TextMeshProUGUI scoreText;
    public static int LastScore = 0; // Static field to store the last score
    #endregion

    private void Start()
    {
        scoreText = GetComponent<TMPro.TextMeshProUGUI>(); // Get the TextMeshPro component attached to this GameObject
    }
    // Update is called once per frame
    void Update()
    {
        scoreText.text = "Score: " + totalScore.ToString(); // Update the score text every frame
    }

    /// <summary>
    /// adds the specified score value to the total score.
    /// </summary>
    /// <param name="scoreValue"></param>
    public void IncrementScore(int scoreValue)
    {
        totalScore += scoreValue; // Increment the score by the score value
    }

    public int GetScore() {
        return totalScore; // Returns the current total score
    }
    //adding listener to the OnPlayerDied event
    void PlayerDiedListener()
    {
        LastScore = totalScore;
        // No need to wait for scene load here
    }

    private void OnSceneLoaded(UnityEngine.SceneManagement.Scene scene, UnityEngine.SceneManagement.LoadSceneMode mode)
    {
        if (scene.name == "GameEndedScene")
        {
            var endScoreObj = FindObjectOfType<GameEndScore>();
            if (endScoreObj != null)
            {
                endScoreObj.SetFinalScore(GetScore());
            }
            // Unsubscribe to avoid duplicate calls
            UnityEngine.SceneManagement.SceneManager.sceneLoaded -= OnSceneLoaded;
        }
    }

    private void OnEnable()
    {
        GameOverEvent.OnPlayerDied += PlayerDiedListener; // Subscribe to the OnPlayerDied event
    }

    private void OnDisable()
    {
        GameOverEvent.OnPlayerDied -= PlayerDiedListener; // Unsubscribe from the OnPlayerDied event
    }
}
