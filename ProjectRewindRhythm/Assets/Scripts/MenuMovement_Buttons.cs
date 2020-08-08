using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class MenuMovement_Buttons : MonoBehaviour
{
    private Vector3 startingPos;
    public bool moveUp;
    public float amount; 

    void Start()
    {
        startingPos = GetComponent<RectTransform>().position;
        amount = 2; 
    }

    void Update()
    {
        if (GetComponent<RectTransform>().position.y >= startingPos.y + 50)
        {
            moveUp = false;
        }
        else if (GetComponent<RectTransform>().position.y <= startingPos.y - 50)
        {
            moveUp = true;
        }

        if (GetComponent<RectTransform>().position.y >= startingPos.y + 40)
        {
            if (amount > 1)
            {
                amount -= 0.2f;
            }
        }
        else if (GetComponent<RectTransform>().position.y <= startingPos.y - 40)
        {
            if (amount > 1)
            {
                amount -= 0.2f; 
            }
        }
        else if (GetComponent<RectTransform>().position.y <= startingPos.y + 39 || GetComponent<RectTransform>().position.y >= startingPos.y - 39)
        {
            if (amount < 2)
            {
                amount += 0.2f;
            }
        }

        if (moveUp)
        {
            GetComponent<RectTransform>().position = new Vector3(GetComponent<RectTransform>().position.x, GetComponent<RectTransform>().position.y + amount, GetComponent<RectTransform>().position.z);
        }
        else if (!moveUp)
        {
            GetComponent<RectTransform>().position = new Vector3(GetComponent<RectTransform>().position.x, GetComponent<RectTransform>().position.y - amount, GetComponent<RectTransform>().position.z);
        }
    }
}
