//Programmer: Gerardo Bonnet

using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BackgroundMovement : MonoBehaviour
{
    public float speed = .01f;
    private Vector3 startPos;
    private float repeatWidth;
    // Start is called before the first frame update
    void Start()
    {
        //retrieve position
        startPos = transform.position;
        //retrieve object width
        repeatWidth = GetComponent<BoxCollider>().size.x / 2;
        //Remove visible seam on plane edge
        GetComponent<Renderer>().material.mainTexture.wrapMode = TextureWrapMode.Clamp;
    }

    // Update is called once per frame
    void Update()
    {
        //Move gameobject slowly to left
        transform.Translate(Vector3.left * Time.deltaTime * speed);
        //Reset background position for continued scrolling
        if (transform.position.x < startPos.x - repeatWidth)
            transform.position = startPos;
    }
}
