//Programmer: Gerardo Bonnet
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClampEdges : MonoBehaviour
{
    void Start()
    {
        //Remove visible seam on plane edge which previously ruined background
        GetComponent<Renderer>().material.mainTexture.wrapMode = TextureWrapMode.Clamp;
    }
}
