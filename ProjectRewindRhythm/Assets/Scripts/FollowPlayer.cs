using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    private Transform playerTrans;
    private Vector3 offset; 

    void Start()
    {
        playerTrans = GameObject.Find("Player").transform;
        offset = new Vector3(0, 1.5f, -10); 
    }


    void Update()
    {
        transform.position = playerTrans.position + offset; 
    }
}
