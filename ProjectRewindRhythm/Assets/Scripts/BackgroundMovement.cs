using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Diagnostics;

public class BackgroundMovement : MonoBehaviour
{
    private float speed;
    public float speedMod; 
    public bool allowedToMove; 

    void Update()
    {
        speed = (GameObject.Find("Player").GetComponent<PlayerMovement>().curSpeed / 35) * speedMod; 

        if (GameObject.Find("Player").GetComponent<PlayerMovement>().gameStarted && !GameObject.Find("Player").GetComponent<PlayerMovement>().gameWon && !GameObject.Find("Player").GetComponent<PlayerMovement>().tripped)
        {
            allowedToMove = true;
        }
        else
        {
            allowedToMove = false; 
        }

        if (allowedToMove)
        {
            transform.Translate(-Vector3.left * Time.deltaTime * speed);
        }
        else 
        {
            transform.Translate(-Vector3.left * Time.deltaTime * 0.05f);
        }
    }
}
