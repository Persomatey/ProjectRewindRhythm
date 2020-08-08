using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiagonalMovementForFloatingBlocks : MonoBehaviour
{
    public float speed;
    public bool isOpp; 

    void Update()
    {
        if (isOpp)
        {
            transform.Translate(-Vector3.up * Time.deltaTime * speed);
        }
        else
        {
            transform.Translate(Vector3.up * Time.deltaTime * speed);
        }

    }
}
