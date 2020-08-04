using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    /* Locations
     *   Obstacle   >     Jump 
     * (15, 1.5, 0) > (17, 1.5, 0) 
     * (0, 1.5, 0) > (2, 1.5, 0)
     * (-10, 1.5, 0) > (-8, 1.5, 0)
     * (#, #, #) > (#, #, #)
     * (#, #, #) > (#, #, #)
     */

    private Rigidbody playerRb;
    [Header("Jump Locations")]
    public Vector3[] jumpPoints;
    [Header("Jump Forces")]
    public int[] jumpForces; 
    public int jumpIndex;
    [Header("Other Variables")]
    public float speed;
    private bool allowedToCheck;
    private bool tripped;
    private bool pressedButton;
    public float generosity;
    public int strikes;
    public int strikesAllowed;
    private bool allowedToJump; 

    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        jumpIndex = 0;
        pressedButton = false;
        allowedToJump = true; 
    }

    void Update()
    {
        if (strikes >= strikesAllowed)
        {
            tripped = true;
            Debug.Log("<color=red>3 strikes, you're out!</color>"); 
        }

        if (!tripped)
        {
            transform.Translate(Vector3.left * Time.deltaTime * speed);
        }

        if (jumpIndex < jumpPoints.Length && transform.position.x < jumpPoints[jumpIndex].x + 0.1f && allowedToJump)
        {
            Debug.Log("Player Jumped because player is at " + transform.position); 
            playerRb.AddForce(Vector3.up * jumpForces[jumpIndex]);
            allowedToJump = false; 
            Invoke("AllowToCheck", 0.1f);
            Debug.Log("AllowedToCheck called in 0.1f"); 
        }

        if (allowedToCheck)
        { 
            if (jumpIndex <= jumpPoints.Length && transform.position.y <= jumpPoints[jumpIndex].y + generosity && transform.position.y > jumpPoints[jumpIndex].y && !pressedButton)
            {
                if ( Input.GetKeyDown(KeyCode.Space) )
                {
                    Debug.Log("<color=green>Spacebar pressed! Can sucessfully land.</color>"); 
                    pressedButton = true;
                    jumpIndex++;
                    allowedToJump = true;
                    allowedToCheck = false; 
                }
            }
            else if (transform.position.y == jumpPoints[jumpIndex].y && !pressedButton)
            {
                Debug.Log("<color=orange>Spacebar not pressed. Player gets a strike.</color>");
                strikes++;
                allowedToCheck = false;
                jumpIndex++;
                allowedToJump = true;
            }
        }

        if (pressedButton && transform.position.y == jumpPoints[jumpIndex-1].y)
        {
            pressedButton = false; 
        }
    }

    void AllowToCheck()
    {
        Debug.Log("AllowedToCheck is true");
        allowedToCheck = true; 
    }
}
