using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlowBallLower : MonoBehaviour
{
    private GameObject player;

    [SerializeField]
    public float spawnY;
    void Start()
    {
        player = GameObject.Find("Player");
        spawnY = this.transform.position.y;
    }

    void Update()
    {
        //spawnY = player.transform.position.y;
        transform.position = new Vector3(transform.position.x, ((player.transform.position.y - spawnY) * .33f) + spawnY - 1, transform.position.z);

        if (transform.position.y < spawnY)
            transform.position = new Vector3(transform.position.x, spawnY, transform.position.z);

    }

}
