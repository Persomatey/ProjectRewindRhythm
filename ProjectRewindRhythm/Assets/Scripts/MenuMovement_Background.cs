using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuMovement_Background : MonoBehaviour
{
    Vector3 startingPos;
    public float speed;
    public bool isBlock;
    public bool isOther; 

    void Start()
    {
        startingPos = transform.position; 
    }

    void Update()
    {
        if (isBlock && !isOther)
        {
            transform.Translate(-Vector3.up * Time.deltaTime * speed);
        }
        else if (isBlock && isOther)
        {
            transform.Translate(Vector3.up * Time.deltaTime * speed);
        }    
        else
        {
            transform.Translate(-Vector3.left * Time.deltaTime * speed);
        }

        if (transform.position.x >= -32.2)
        {
            Debug.Log("Reset position"); 
            transform.position = startingPos; 
        }
    }
}
