using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlowBallUpper : MonoBehaviour
{
    private GameObject player;
    public float spawnY;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        spawnY = this.transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(transform.position.x, player.transform.position.y - 1, transform.position.z);

        if (transform.position.y < spawnY)
            transform.position = new Vector3(transform.position.x, spawnY, transform.position.z);

    }
}
