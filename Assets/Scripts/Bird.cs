using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// A simple script to represent a bird in the game.
/// </summary>
public class Bird : MonoBehaviour
{
    #region Fields;
    const float rotationTime = 0.2f; //time in seconds for the bird to rotate back to normal position
    //constants
    const int rotationAngle = 45; //degrees to rotate the bird when space is pressed
    /// <summary>
    /// The upward force applied when space is pressed.
    /// </summary>
    const float upForce = 8f;

    //variables
    Rigidbody2D rb;
    /// <summary>
    /// indicates whether the input key (space) is pressed.
    /// </summary>
    bool inputKeyPressed = false;
    #endregion
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        inputKeyPressed = Input.GetKeyDown(KeyCode.Space);
        if (inputKeyPressed)
        {
            rb.AddForce(Vector2.up * upForce, ForceMode2D.Impulse);
            //rotates the bird temporarily by 45 degrees up when space is pressed
            AddRotation(rotationAngle);
            Invoke("ResetRotation", rotationTime); // Reset rotation after a delay
        }
    }

    void AddRotation(float rotationAngle)
    {
        // Adds a rotation to the bird
        transform.rotation = Quaternion.Euler(0, 0, rotationAngle);
    }

    void ResetRotation()
    {
        // Resets the bird rotation to normal position
        transform.rotation = Quaternion.Euler(0, 0, 0);
    }

}
