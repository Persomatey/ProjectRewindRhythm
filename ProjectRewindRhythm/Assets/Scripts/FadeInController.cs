using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeInController : MonoBehaviour
{
    private float fadeAmountM; 

    void Start()
    {
        fadeAmountM = 1;
    }

    void Update()
    {
        fadeAmountM -= 0.003f;
        GetComponent<SpriteRenderer>().color = new Color(0, 0, 0, fadeAmountM);
    } 
}
