using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlowManager : MonoBehaviour
{
    public GameObject glowBallUpper, glowBallLower, ring, player;
    [SerializeField]
    private PlayerMovement pScript;
    private GlowBallUpper glowUScript;
    private GlowBallLower glowLScript;
    public Animator rAnimator;
    //private Animation ringAnim;
    [SerializeField]
    private int pJumps;
    [SerializeField]
    private int jumpCheck;
    private Vector3 newPosition;

    void Start()
    {
        //Set up Player for retrieving values. Other GameObjects are preset within Unity.
        player = GameObject.Find("Player");
        pScript = player.GetComponent<PlayerMovement>();
        glowUScript = glowBallUpper.GetComponent<GlowBallUpper>();
        glowLScript = glowBallLower.GetComponent<GlowBallLower>();

        pJumps = pScript.totalJumpsMade;
        jumpCheck = pJumps;

        //ringAnim = ring.GetComponent<Animation>();
        //ringAnim["Ring Expand"].wrapMode = WrapMode.Once;
    }

    // Update is called once per frame
    void Update()
    {
        pJumps = pScript.totalJumpsMade;
        //Attempts to move Glow Effect to landing spots and fails miserably
        newPosition = new Vector3(pScript.jumpPoints[pScript.jumpIndex + 1].x, pScript.jumpPoints[pScript.jumpIndex + 1].y);
        transform.position = newPosition;
        glowUScript.spawnY = newPosition.y - 1;
        glowLScript.spawnY = newPosition.y - 1;

        //transform.position = new Vector3(player.transform.position.x, player.transform.position.y, 0);

        if (pJumps > jumpCheck)
        {
            ring.transform.position = new Vector3(pScript.jumpPoints[pScript.jumpIndex].x, pScript.jumpPoints[pScript.jumpIndex].y + 1);
            animPlay();
        }
    }

    private void animPlay()
    {
        jumpCheck++;
        Debug.Log("GLOWJUMP!!!");
        ring.GetComponent<MeshRenderer>().enabled = true;
        rAnimator.SetTrigger("Ring Trigger");
        //ring.GetComponent<MeshRenderer>().enabled = false;

    }
}
