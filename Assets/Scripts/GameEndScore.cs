using UnityEngine;
using TMPro;
/// <summary>
/// Gets the total score at the end of the game and displays it on the screen.
/// </summary>
public class GameEndScore : MonoBehaviour
{
    TMPro.TextMeshProUGUI scoreText; // Reference to the TextMeshProUGUI component for displaying the score

    private void Start()
    {
        scoreText = GetComponent<TMPro.TextMeshProUGUI>(); // Get the TextMeshProUGUI component attached to this GameObject
        scoreText.text = "Final Score: " + ScoreText.LastScore.ToString();
    }

    //create listener for on score updated event
    void ScoreUpdateListener(int score)
    {
        scoreText.text += score.ToString(); // Update the score text with the final score
    }

    public void SetFinalScore(int score)
    {
        if (scoreText == null)
            scoreText = GetComponent<TMPro.TextMeshProUGUI>();
        scoreText.text = "Final Score: " + score.ToString();
    }
}
