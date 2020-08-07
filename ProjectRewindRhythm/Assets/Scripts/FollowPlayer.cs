using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    private Transform playerTrans;
    private Vector3 offset;
    public bool following; 

    void Start()
    {
        playerTrans = GameObject.Find("Player").transform;
        offset = new Vector3(0, 0f, -10); 
    }


    void Update()
    {
        // -226
        if (following && transform.position.x > -226)
        {
            transform.position = playerTrans.position + offset;
        }
    }
}
