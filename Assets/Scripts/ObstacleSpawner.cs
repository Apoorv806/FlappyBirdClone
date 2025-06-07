using UnityEngine;


/// <summary>
/// spawns obstacles in the game at regular intervals.
/// </summary>
public class ObstacleSpawner : MonoBehaviour
{
    #region Fields

    //timer to control the spawning of obstacles
    float spawnDelayDuration = 2.5f; // Time in seconds between spawns
    Timer timer;


    //obstacle prefab to spawn
    [SerializeField] 
    GameObject obstacle1Prefab;
    [SerializeField]
    GameObject obstacle2Prefab;

    //initial positions of prefab obstacles and the offset for spawning
    Vector3 spawnOffset1 = new Vector3(10,3.5f,0);
    Vector3 spawnOffset2 = new Vector3(10,-3.2f,0);
    int randomObstacleIndex; // Randomly choose between four obstacle types
    Quaternion spawnRotation = Quaternion.Euler(0, 0, -180); // to reverse rotations

    //inverting the y position for spawning obstacles
    float newY1Inverted;
    float newY2Inverted;

    #endregion
    // Start is called before the first frame update
    void Start()
    {
        timer = gameObject.AddComponent<Timer>();
        timer.Duration = spawnDelayDuration; // Set the timer duration

        Instantiate(obstacle1Prefab, spawnOffset1, spawnRotation);
        Instantiate(obstacle2Prefab, spawnOffset2, Quaternion.identity);
        timer.Run(); // Start the timer

    }

    // Update is called once per frame
    void Update()
    {
        if(timer.Finished)
        {
            // Spawn a new obstacle or obstacle type when the timer finishes
            randomObstacleIndex = Random.Range(0, 6); // Randomly choose between four obstacle types
            switch(randomObstacleIndex)
            { 
                case 0:     
                    Instantiate(obstacle1Prefab, spawnOffset1 , spawnRotation);

                    break;      //spawns obstacle 1
                case 1:     
                    Instantiate(obstacle2Prefab,spawnOffset2, Quaternion.identity);
                    break;      //spawns obstacle 2

                case 2:
                    Instantiate(obstacle1Prefab,spawnOffset1, spawnRotation);
                    Instantiate(obstacle2Prefab,spawnOffset2, Quaternion.identity);

                    break;      //spawns both obstacles at the same time

                case 3:     
                    newY1Inverted = spawnOffset1.y * -1; // Adjust the y position for the first obstacle
                    newY2Inverted = spawnOffset2.y * -1; // Adjust the y position for the second obstacle
                    Instantiate(obstacle1Prefab,new Vector3(spawnOffset1.x,newY1Inverted),
                        Quaternion.identity);
                    Instantiate(obstacle2Prefab,new Vector3(spawnOffset2.x,newY2Inverted), 
                        spawnRotation);

                    break;      //spawns both obstacles at inverted y positions
                case 4:     
                    newY1Inverted = spawnOffset1.y * -1; // Adjust the y position for the first obstacle
                    Instantiate(obstacle1Prefab,new Vector3(spawnOffset1.x, newY1Inverted), 
                        Quaternion.identity);
                    break;      //spawns obstacle 1 at inverted y position
                case 5:     
                    newY2Inverted = spawnOffset2.y * -1; // Adjust the y position for the second obstacle
                    Instantiate(obstacle2Prefab,new Vector3(spawnOffset2.x, newY2Inverted), 
                        spawnRotation);
                    break;      //spawns obstacle 2 at inverted y position

                default:
                    Debug.LogWarning("No obstacle type defined for index: " + randomObstacleIndex);
                    break;
            }
            timer.Run(); // Restart the timer
        }

    }


}
