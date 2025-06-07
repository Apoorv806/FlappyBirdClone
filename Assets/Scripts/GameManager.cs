using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Manages the overall state and flow of the game, including initialization, updates, and transitions between game
/// states.
/// </summary>
/// <remarks>This class serves as the central controller for game logic and coordination. It is typically
/// responsible for managing game state transitions, handling global events, and interacting with other core systems in
/// the game.</remarks>
public class GameManager : MonoBehaviour
{
    #region Fields

    //Adding prefab of player
    [SerializeField]
    GameObject playerPrefab;

    //bools to track game state
    /// <summary>
    /// Indicates whether the game has started.
    /// </summary>
    private bool isGameStarted = false;
    /// <summary>
    /// Indicates whether the game has ended.
    /// </summary>
    bool isGameOver = false;

    bool isTitleScreenLoaded = false; // Track if the title screen is loaded
    #endregion

    private void OnEnable()
    {
        GameOverEvent.OnPlayerDied += PlayerDiedListener; // Subscribe to the player died event

    }
    private void OnDisable()
    {
        GameOverEvent.OnPlayerDied -= PlayerDiedListener; // Unsubscribe from the player died event
    }

    private void Awake()
    {
        //instantiates the game manager and ensures there is only one instance in the scene and it persists across scenes
        if (FindObjectsOfType<GameManager>().Length > 1)
        {
            Destroy(gameObject); // Destroy duplicate GameManager instances
        }
        else
        {
            DontDestroyOnLoad(gameObject); // Keep this instance across scene loads
        }
        //reset game state flags
        isGameStarted = false; // Reset the game started state
        isGameOver = false; // Reset the game over state
    }

    /// <summary>
    /// Loads the title screen scene.
    /// </summary>
    /// <remarks>This method transitions the application to the title screen by loading the scene named
    /// "TitleScene". Ensure that the scene name matches the one configured in the project.</remarks>
    private void LoadTitleScreen()
    {
        if(!isTitleScreenLoaded)
        {
            // Load the title screen scene
            SceneManager.LoadScene("TitleScene"); // Ensure "TitleScreen" is the name of your title screen scene
            //reset game state flags
            isTitleScreenLoaded = true; // Set the title screen loaded flag to true
            isGameStarted = false; // Reset the game started state
            isGameOver = false; // Reset the game over state
        }
    }

    private void LoadEndGameScreen()
    {
        if(isGameOver)
        {
            // Load the end game screen scene
            Destroy(gameObject); // Destroy the GameManager instance to clean up
            SceneManager.LoadScene("GameEndedScene"); // Ensure "EndGameScene" is the name of your end game scene
        }
    }
    /// <summary>
    /// starts the game by initializing necessary components and setting the initial game state.
    /// </summary>
    public void StartGame()
    {
        if(!isGameStarted)
        {
            isTitleScreenLoaded = false; // Reset the title screen loaded flag
            isGameStarted = true; // Set the game as started
            //loads the gameplay scene
            SceneManager.LoadScene("GamePlay"); // Ensure "GameplayScene" is the name of your gameplay scene
        }

    }
    /// <summary>
    /// Ends the current game session and marks the game as over.
    /// </summary>
    /// <remarks>This method sets the game state to indicate that the game is over.  Subsequent calls to this
    /// method will have no effect if the game is already marked as over.</remarks>
    public void EndGame()
    {         
        if(!isGameOver)
        {
            isGameOver = true; // Set the game as over
            isGameStarted = false; // Reset the game started state
            LoadEndGameScreen(); // Load the end game screen
        }

    }

    public void QuitGame()
    {
               // Quit the application
        Application.Quit();
        Debug.Log("Game is quitting..."); // Log message for debugging purposes
    }

    /// <summary>
    /// Triggers when the player dies, invoking the end game logic.
    /// </summary>
    void PlayerDiedListener()
    {
               EndGame(); // Call the EndGame method when the player dies
    }
}
