//Programmer: Gerardo Bonnet

using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BackgroundMovement : MonoBehaviour
{
    public float speed = .01f;
    //private Vector3 startPos;
    //private float repeatWidth;

    void Start()
    {
        //Remove visible seam on plane edge
        GetComponent<Renderer>().material.mainTexture.wrapMode = TextureWrapMode.Clamp;
        //retrieve position
        //startPos = transform.position;
        //retrieve object width
        //repeatWidth = GetComponent<BoxCollider>().size.x;
    }


    void Update()
    {
        //Move gameobject slowly to left
        transform.Translate(Vector3.left * Time.deltaTime * speed);
        //Reset background position for continued scrolling
        /*if (transform.position.x < startPos.x - repeatWidth)
            transform.position = startPos;*/
    }
}
