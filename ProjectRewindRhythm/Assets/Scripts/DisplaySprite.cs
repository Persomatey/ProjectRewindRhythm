using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplaySprite : MonoBehaviour
{
    private int spriteIndex; 
    public Sprite[] displaySprite;
    public GameObject spacebar; 

    void Start()
    {
        spriteIndex = 0;
        InvokeRepeating("ChangeSprite", 0.5f, 0.25f); 
    }

    void Update()
    {
        if (spriteIndex == 11 || spriteIndex == 10)
        {
            Debug.Log("Changing color to yellow");
            spacebar.GetComponent<SpriteRenderer>().color = Color.yellow;
        }
        else
        {
            spacebar.GetComponent<SpriteRenderer>().color = Color.white;
        }
    }

    void ChangeSprite()
    {
        if (spriteIndex + 1 > displaySprite.Length)
        {
            spriteIndex = 0; 
        }
        else
        {
            spriteIndex++; 
        }

        GetComponent<SpriteRenderer>().sprite = displaySprite[spriteIndex];
    }
}
