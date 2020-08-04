using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    /* Locations
     *   Obstacle   >     Jump 
     * (15, 01, 00) > (17, 01, 00) 
     * (00, 01, 00) > (02, 01, 00)
     * (##, ##, ##) > (##, ##, ##)
     * (##, ##, ##) > (##, ##, ##)
     * (##, ##, ##) > (##, ##, ##)
     * (##, ##, ##) > (##, ##, ##)
     * (##, ##, ##) > (##, ##, ##)
     */

    private Rigidbody playerRb;
    [Header("Jump Locations")]
    public Vector3[] jumpPoints;
    [Header("Jump Forces")]
    public int[] jumpForces; 
    private int jumpIndex;
    [Header("Speed")]
    public float speed;
    public bool allowedToCheck;
    public bool tripped;
    public bool pressedButton; 

    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        jumpIndex = 0;
        pressedButton = false; 
    }

    void Update()
    {
        if (!tripped)
        {
            transform.Translate(Vector3.left * Time.deltaTime * speed);
        }

        if (jumpIndex < jumpPoints.Length && transform.position.x < jumpPoints[jumpIndex].x + 0.1f)
        {
            Debug.Log("Player Jumped because player is at " + transform.position); 
            playerRb.AddForce(Vector3.up * jumpForces[jumpIndex]);
            jumpIndex++;
            Invoke("AllowToCheck", 0.1f); 
        }

        if (allowedToCheck)
        { 
            if (transform.position.y < 2f && transform.position.y > 1.5f && !pressedButton)
            {
                Debug.Log("Listening for spacebar... "); 
                
                if ( Input.GetKeyDown(KeyCode.Space) )
                {
                    Debug.Log("<color=green>Spacebar pressed! Sucessfully landed.</color>"); 
                    pressedButton = true; 
                }
            }

            if (transform.position.y == 1.5f && !pressedButton)
            {
                tripped = true; 
            }
        }
    }

    void AllowToCheck()
    {
        allowedToCheck = true; 
    }
}
