using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody playerRb;
    public int jumpIndex;
    [Header("Jump Locations")]
    public Vector3[] jumpPoints;
    [Header("Jump Forces")]
    public int[] jumpForces; 
    [Header("Speed Changes")]
    public float[] speedChanges;
    //[Header("Bool CAtch")]
    //public bool[] boolCatch;
    [Header("Other Variables")]
    public float curSpeed;
    public bool allowedToCheck;
    private bool tripped;
    private bool pressedButton;
    public float generosity;
    public int strikes;
    public int strikesAllowed;
    public bool allowedToJump; 

    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        jumpIndex = 0;
        pressedButton = false;
        allowedToJump = true;
        curSpeed = speedChanges[0];
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
            transform.Translate(Vector3.left * Time.deltaTime * curSpeed);
        }

        if (jumpIndex < jumpPoints.Length && transform.position.x < jumpPoints[jumpIndex].x + 0.2f && allowedToJump)
        {
            Debug.Log("<color=purple>Doing jump </color>" + jumpIndex + "<color=purple> because player is at </color>" + transform.position + "<color=purple> with </color>" + jumpForces[jumpIndex] + "<color=purple> force, running at </color>" + speedChanges[jumpIndex + 1] + "<color=purple> speed </color>");
            GetComponent<Rigidbody>().velocity = Vector3.zero;
            GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
            playerRb.AddForce(Vector3.up * jumpForces[jumpIndex]);
            allowedToJump = false; 
            Invoke("AllowToCheck", 0.1f);
            Debug.Log("<color=lightblue>AllowedToCheck called in 0.1f</color>"); 
        }

        if (allowedToCheck)
        { 
            if (jumpIndex <= jumpPoints.Length && transform.position.y <= jumpPoints[jumpIndex + 1].y + generosity && transform.position.y > jumpPoints[jumpIndex + 1].y /*&& !pressedButton*/ && Input.GetKeyDown(KeyCode.Space))
            {
                Debug.Log("<color=green>Spacebar pressed! Can sucessfully land.</color>"); 
                //pressedButton = true;
                Debug.Log("<color=magenta> incrimenting jumpIndex from " + jumpIndex + " to " + (jumpIndex + 1) + "</color>"); 
                jumpIndex++;
                curSpeed = speedChanges[jumpIndex];
                allowedToJump = true;
                allowedToCheck = false;
                Debug.Log("<color=darkblue>No longer allowed to check</color>");
            }
            else if (transform.position.y <= jumpPoints[jumpIndex + 1].y /*&& !pressedButton*/)
            {
                Debug.Log("<color=orange>Spacebar not pressed. Player gets a strike.</color>");
                strikes++;
                Debug.Log("<color=magenta> incrimenting jumpIndex from " + jumpIndex + " to " + (jumpIndex + 1) + "</color>");
                jumpIndex++;
                curSpeed = speedChanges[jumpIndex];
                allowedToJump = true;
                allowedToCheck = false;
                Debug.Log("<color=darkblue>No longer allowed to check</color>");
            }
        }

        if (pressedButton && transform.position.y == jumpPoints[jumpIndex].y)
        {
            Debug.Log("Making presedButton false again"); 
            pressedButton = false; 
        }
    }

    void AllowToCheck()
    {
        Debug.Log("<color=blue>AllowedToCheck is true</color>");
        allowedToCheck = true; 
    }
}
