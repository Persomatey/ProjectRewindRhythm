using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody playerRb;
    public int jumpIndex;
    [Header("Lose & Win Stuff")]
    public Sprite[] idleAnim;
    public Sprite[] runAnim;
    public Sprite[] jumpAnim;
    public Sprite[] spinAnim;
    public Sprite[] tripAnim;
    [Header("Lose & Win Stuff")]
    public GameObject gameOver1;
    public GameObject gameOver2;
    public GameObject gameOver3;
    public GameObject gameOver4;
    public GameObject win1;
    public GameObject win2;
    public GameObject win3;
    public GameObject win4;
    public GameObject win5;
    public GameObject win6;
    public GameObject win7;
    public GameObject win8;
    public GameObject win9;
    public GameObject win10;
    public GameObject comboTitle; 
    [Header("Strikes")]
    public GameObject strike1;
    public GameObject strike2;
    public GameObject strike3;
    [Header("Jump Locations")]
    public Vector3[] jumpPoints;
    [Header("Jump Forces")]
    public int[] jumpForces; 
    [Header("Speed Changes")]
    public float[] speedChanges;
    public float[] speedLocs; 
    //[Header("Bool CAtch")]
    //public bool[] boolCatch;
    [Header("Other Variables")]
    public Text comboText;
    private AudioSource source;
    private GameObject otherSource; 
    public AudioClip landSFX;
    public AudioClip strikeSFX;
    public AudioClip loseSFX;
    public AudioClip comboSFX;
    public AudioClip glitchSFX;
    private GameObject cam; 
    public float curSpeed;
    public bool tripped;
    private bool pressedButton;
    public float generosity;
    public int strikes;
    public int strikesAllowed;
    private bool allowedToJump;
    public bool gameStarted;
    public float startDelay;
    //public float veloc;
    public float landingTime;
    public bool allowedToCheck;
    public bool allowedToChangeCheck;
    public bool justJumped;
    private bool onlyLogOnce;
    public int combo;
    public int comboHeal;
    private bool playedComboSFX;
    public int highestCombo; 
    public int score;
    public bool gameWon;
    public int totalJumpsMade;
    public int amountToIncreaseRegularly;
    [Header("Animation Stuff")]
    private SpriteRenderer playerSprite; 
    private Coroutine curAnim;
    private bool isSpinning;
    private bool called;
    public bool isIdling;
    public bool isRunning;
    private bool isJumping;
    private bool isTripping;
    public bool doIdling;
    public bool doRunning;
    private bool doJumping;
    private bool doTripping;
    public bool[] allowToRun;
    public bool isFalling;
    private bool startedIdle;
    private bool onlyDieOnce;

    void Start()
    {
        onlyLogOnce = false; 
        gameStarted = false;
        strike1.SetActive(false);
        strike2.SetActive(false);
        strike3.SetActive(false);
        gameOver1.SetActive(false);
        gameOver2.SetActive(false);
        gameOver3.SetActive(false);
        gameOver4.SetActive(false);
        win1.SetActive(false);
        win2.SetActive(false);
        win3.SetActive(false);
        win4.SetActive(false);
        win5.SetActive(false);
        win6.SetActive(false);
        win7.SetActive(false);
        win8.SetActive(false);
        win9.SetActive(false);
        win10.SetActive(false);
        playedComboSFX = false; 
        source = GetComponent<AudioSource>();
        otherSource = GameObject.Find("AudioController"); 
        playerRb = GetComponent<Rigidbody>();
        jumpIndex = 0;
        amountToIncreaseRegularly = 10; 
        pressedButton = false;
        allowedToJump = true;
        curSpeed = speedChanges[0];
        Invoke("StartLevel", startDelay);
        InvokeRepeating("RaiseScoreGradually", startDelay, 0.25f);
        playerSprite = transform.GetChild(0).GetComponent<SpriteRenderer>();
        curAnim = StartCoroutine(SpinAnim());
        called = false;
        isIdling = false;
        isRunning = false;
        isJumping = false;
        isTripping = false;
        doIdling = false;
        doRunning = false;
        doJumping = false;
        doTripping = false;
        startedIdle = false;
        onlyDieOnce = false; 
    }

    void Update()
    {
        Animationer(); 

        comboText.text = "" + combo;

        if (combo > highestCombo)
        {
            highestCombo = combo; 
        }

        if (combo < 10)
        {
            comboText.color = Color.white; 
            comboText.fontSize = 85; 
        }
        else if (combo >= 10 && combo < 20)
        {
            comboText.color = new Color(0.50f, 1, 0.50f, 1);
            comboText.fontSize = 100;
        }
        else if (combo >= 20 && combo < 30)
        {
            comboText.color = new Color(0.25f, 1, 0.25f, 1);
            comboText.fontSize = 115;
        }
        else if (combo >= 30 && combo < 35)
        {
            comboText.color = new Color(0, 1, 0, 1);
            comboText.fontSize = 130;
        }
        else if (combo == 34)
        {
            comboText.color = new Color(1, 1, 0, 1);
            comboText.fontSize = 130;
        }

        if (!playedComboSFX && (combo == 10 || combo == 20 || combo == 30)) 
        {
            source.PlayOneShot(comboSFX, 0.5f);
            score += 5000;

            playedComboSFX = true; 
        }

        if (combo == 11 || combo == 21 || combo == 31)
        {
            playedComboSFX = false; 
        }

        if ( strikes > 0 && comboHeal == 10)
        {
            strikes--;
            comboHeal = 0;
        }

        if (playerRb.velocity.y < -2 && allowedToChangeCheck && strikes < 3)
        {
            allowedToCheck = true;
            allowedToChangeCheck = false; 
        }
        
        if (transform.position.x <= speedLocs[jumpIndex])
        {
            curSpeed = speedChanges[jumpIndex];
        }

        if (strikes == 0)
        {
            strike1.SetActive(false);
            strike2.SetActive(false);
            strike3.SetActive(false);
        }

        if (strikes == 1)
        {
            strike1.SetActive(true);
            strike2.SetActive(false);
            strike3.SetActive(false);
        }

        if (strikes == 2)
        {
            strike1.SetActive(true);
            strike2.SetActive(true);
            strike3.SetActive(false);
        }

        if (strikes == 3 && !onlyDieOnce)
        {
            onlyDieOnce = true; 
            Destroy(otherSource);
            source.PlayOneShot(loseSFX, 0.75f);
            strike1.SetActive(true);
            strike2.SetActive(true);
            strike3.SetActive(true);
            Invoke("ActivateDeathScreens", 1f); 
        }

        if (tripped && cam.GetComponent<ShaderEffect_CorruptedVram>().enabled)
        {
            cam.GetComponent<ShaderEffect_CorruptedVram>().shift += 0.001f;
        }

        if (gameStarted)
        {
            if (strikes >= strikesAllowed && !onlyLogOnce)
            {
                tripped = true;
                Debug.Log("<color=red>3 strikes, you're out!</color>");
                Debug.Log("Stopping all coroutines");
                StopCoroutine(curAnim);
                Debug.Log("<color=red>playing trip animation</color>");
                curAnim = StartCoroutine(TripAnim()); 
                onlyLogOnce = true; 
            }

            if (!tripped)
            {
                transform.Translate(Vector3.left * Time.deltaTime * curSpeed);
                if (isWithinGround(jumpPoints[jumpIndex]) && allowToRun[jumpIndex])
                {
                    doRunning = true;
                }

                if (!isWithinGround(jumpPoints[jumpIndex]))
                {
                    doRunning = false;
                    isRunning = false; 

                    if (playerRb.velocity.y < 0)
                    {
                        // falling
                        Debug.Log("falling"); 
                        StopCoroutine(curAnim);
                        playerSprite.sprite = jumpAnim[0];
                    }
                    if (playerRb.velocity.y > 0)
                    {
                        // rising 
                        //Debug.Log("Rising"); 
                        //StopCoroutine(curAnim);
                        //playerSprite.sprite = jumpAnim[1];
                    }
                }
            }

            if (jumpIndex < jumpPoints.Length && transform.position.x <= jumpPoints[jumpIndex].x + 0.1f  && strikes < 3 && !justJumped && isWithinGround(jumpPoints[jumpIndex]))
            {
                Debug.Log("<color=purple>Doing jump </color>" + jumpIndex + "<color=purple> because player is at </color>" + transform.position + "<color=purple> with </color>" + jumpForces[jumpIndex] + "<color=purple> force, running at </color>" + speedChanges[jumpIndex + 1] + "<color=purple> speed </color>");
                GetComponent<Rigidbody>().velocity = Vector3.zero;
                GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
                playerRb.AddForce(Vector3.up * jumpForces[jumpIndex]);

                Debug.Log("Stopping all coroutines");
                StopCoroutine(curAnim);
                Debug.Log("Rising");
                StopCoroutine(curAnim);
                playerSprite.sprite = jumpAnim[1]; 

                allowedToJump = false;
                justJumped = true; 
            }

            if (allowedToCheck)
            {
                if (jumpIndex <= jumpPoints.Length && transform.position.y <= jumpPoints[jumpIndex + 1].y + generosity && transform.position.y > jumpPoints[jumpIndex + 1].y  && justJumped && Input.GetKeyDown(KeyCode.Space) && !pressedButton)
                {
                    Debug.Log("<color=green>Spacebar pressed! Can sucessfully land.</color>");
                    source.PlayOneShot(landSFX, 0.75f); 
                    //if (jumpIndex == 2 || jumpIndex == 4 || jumpIndex == 8 || jumpIndex == 12 || jumpIndex == 13 || jumpIndex == 14 || jumpIndex == 19 || jumpIndex == 20 || jumpIndex == 23 || jumpIndex == 24 || jumpIndex == 25 || jumpIndex == 26 || jumpIndex == 27 || jumpIndex == 28 || jumpIndex == 29 || jumpIndex == 30)
                    //{
                    //    Debug.Log("At " + jumpIndex + " so making it true");
                    //    allowToRun = true;
                    //}
                    //else
                    //{
                    //    Debug.Log("At " + jumpIndex + " so making it false");
                    //    allowToRun = false;
                    //    isRunning = false;
                    //    doRunning = false;
                    //}
                    //Debug.Log("<color=magenta> incrimenting jumpIndex from " + jumpIndex + " to " + (jumpIndex + 1) + "</color>");
                    //jumpIndex++;
                    //allowedToJump = true;
                    //allowedToCheck = false;
                    //allowedToChangeCheck = false;
                    //justJumped = false;
                    combo++;
                    comboHeal++;
                    totalJumpsMade++; 
                    score += 1000;
                    pressedButton = true; 
                    Invoke("DelayedAllowToChangeCheck", landingTime);
                }
                else if (justJumped & isWithinGround(jumpPoints[jumpIndex + 1]) && !pressedButton)
                {
                    Debug.Log("<color=orange>Spacebar not pressed. Player gets a strike.</color>");
                    source.PlayOneShot(strikeSFX, 0.75f);
                    strikes++;
                    //if (jumpIndex == 2 || jumpIndex == 4 || jumpIndex == 8 || jumpIndex == 12 || jumpIndex == 13 || jumpIndex == 14 || jumpIndex == 19 || jumpIndex == 20 || jumpIndex == 23 || jumpIndex == 24 || jumpIndex == 25 || jumpIndex == 26 || jumpIndex == 27 || jumpIndex == 28 || jumpIndex == 29 || jumpIndex == 30)
                    //{
                    //    Debug.Log("At " + jumpIndex + " so making it true");
                    //    allowToRun = true;
                    //}
                    //else
                    //{
                    //    Debug.Log("At " + jumpIndex + " so making it false");
                    //    allowToRun = false; 
                    //    isRunning = false; 
                    //    doRunning = false; 
                    //}
                    Debug.Log("<color=magenta>incrimenting jumpIndex from " + jumpIndex + " to " + (jumpIndex + 1) + "</color>");
                    jumpIndex++;
                    allowedToJump = true;
                    allowedToCheck = false;
                    allowedToChangeCheck = false;
                    justJumped = false;
                    combo = 0;
                    comboHeal = 0;
                    score -= 1000;
                    Invoke("DelayedAllowToChangeCheck", landingTime); 
                }

                if (isWithinGround(jumpPoints[jumpIndex + 1]) && justJumped && pressedButton)
                {
                    Debug.Log("<color=green>Fully landed, reseting bools</color>");
                    Debug.Log("<color=magenta> incrimenting jumpIndex from " + jumpIndex + " to " + (jumpIndex + 1) + "</color>");
                    jumpIndex++;
                    allowedToJump = true;
                    allowedToCheck = false;
                    allowedToChangeCheck = false;
                    justJumped = false;
                    pressedButton = false;
                    Debug.Log("<color=green> allowedToJump = " + allowedToJump + " | allowedToCheck = " + allowedToCheck + " | allowedToChangeCheck = " + allowedToChangeCheck + "</color>");
                }

                //if (isWithinGround() && justJumped && pressedButton)
                //{
                //    justJumped = false;
                //    allowedToJump = true;
                //    allowedToCheck = false;
                //    allowedToChangeCheck = false;
                //    pressedButton = false;
                //    jumpIndex++; 
                //}
            }
        }

        if (transform.position.x < -230)
        {
            gameWon = true; 
        }

        if (gameWon)
        {
            gameStarted = false; 
            amountToIncreaseRegularly = 0;
            if (transform.position.x > -233)
            {
                transform.Translate(Vector3.left * Time.deltaTime * 8);
            }
            else
            {
                win1.SetActive(true);
                win2.SetActive(true);
                win3.SetActive(true);
                win4.SetActive(true);
                win5.SetActive(true);
                win6.SetActive(true);
                win6.GetComponent<Text>().text = "" + highestCombo;
                win7.SetActive(true);
                win8.SetActive(true);
                win8.GetComponent<Text>().text = "" + totalJumpsMade + "/34";
                win9.SetActive(true);
                win10.SetActive(true);
                win10.GetComponent<Text>().text = "" + score;

                strike1.SetActive(false);
                strike2.SetActive(false);
                strike3.SetActive(false);
                comboTitle.SetActive(false);
                comboText.gameObject.SetActive(false);
                
                if (!startedIdle)
                {
                    StopCoroutine(curAnim);
                    Debug.Log("<color=green>Starting idle coroutine</color>");
                    curAnim = StartCoroutine(IdleAnim());
                    startedIdle = true; 
                }
            }

            GameObject.Find("Main Camera").GetComponent<FollowPlayer>().following = false; 
        }
    }  

    bool isWithinGround(Vector3 passed)
    {
        if (playerRb.velocity.y < -1)
        {
            return false; 
        }
        //     transform.position.y <= jumpPoints[jumpIndex].y
        return transform.position.y <= passed.y; 
    }

    void RaiseScoreGradually()
    {
        score += amountToIncreaseRegularly; 
    }

    void DelayedChangeVals()
    {
        curSpeed = speedChanges[jumpIndex];
    }

    void StartLevel()
    {
        gameStarted = true;
    }

    void DelayedAllowToChangeCheck()
    {
        allowedToChangeCheck = true; 
    }

    void IsStillZero()
    {
        if (isWithinGround(jumpPoints[jumpIndex]) && !isRunning) 
        {
            Debug.Log("<color=darkblue>looks good</color>");
            doRunning = true; 
        }
        else
        {
            Debug.Log("<color=darkblue>not good</color>");
        }
    }

    void ResetAnimBools()
    {
        isIdling = false;
        isRunning = false;
        doIdling = false;
        doRunning = false;
    }

    void Animationer()
    {
        if (doRunning && !isRunning)
        {
            Debug.Log("Starting Running"); 
            curAnim = StartCoroutine(RunAnim()); 
        }
        
        if (doIdling && !isIdling)
        {
            curAnim = StartCoroutine(IdleAnim());
        }
    }

    void ActivateDeathScreens()
    {
        gameOver1.SetActive(true);
        gameOver2.SetActive(true);
        gameOver3.SetActive(true);
        gameOver4.SetActive(true);
        cam = GameObject.Find("Main Camera");
        cam.GetComponent<ShaderEffect_CorruptedVram>().enabled = true;
        source.PlayOneShot(glitchSFX, 1f);
    }

    IEnumerator IdleAnim()
    {
        Debug.Log("<color=green>idle animation is playing</color>");
        Debug.Log("idleAnim[3]"); 
        playerSprite.sprite = idleAnim[3];
        yield return new WaitForSeconds(0.1f);
        Debug.Log("idleAnim[2]");
        playerSprite.sprite = idleAnim[2];
        yield return new WaitForSeconds(0.1f);
        Debug.Log("idleAnim[1]");
        playerSprite.sprite = idleAnim[1];
        yield return new WaitForSeconds(0.1f);
        Debug.Log("idleAnim[0]");
        playerSprite.sprite = idleAnim[0];
        yield return new WaitForSeconds(0.1f);
        curAnim = StartCoroutine(IdleAnim());
    }

    IEnumerator RunAnim()
    {
        isRunning = true; 
        Debug.Log("Run Sprite 0"); 
        playerSprite.sprite = runAnim[0];
        yield return new WaitForSeconds(0.1f);
        Debug.Log("Run Sprite 1");
        playerSprite.sprite = runAnim[3];
        yield return new WaitForSeconds(0.1f);
        Debug.Log("Run Sprite 2");
        playerSprite.sprite = runAnim[2];
        yield return new WaitForSeconds(0.1f);
        Debug.Log("Run Sprite 3");
        playerSprite.sprite = runAnim[1];
        yield return new WaitForSeconds(0.1f);
        isRunning = false; 
    }

    IEnumerator JumpAnim()
    {
        playerSprite.sprite = jumpAnim[2];
        yield return new WaitForSeconds(0.1f);
        playerSprite.sprite = jumpAnim[1];
        yield return new WaitForSeconds(0.1f);
        playerSprite.sprite = jumpAnim[0];
        yield return new WaitForSeconds(0.1f);
    }

    IEnumerator SpinAnim()
    {
        isSpinning = true; 
        playerSprite.sprite = spinAnim[9];
        yield return new WaitForSeconds(1f);
        playerSprite.sprite = spinAnim[8];
        yield return new WaitForSeconds(0.1f);
        playerSprite.sprite = spinAnim[7];
        yield return new WaitForSeconds(0.1f);
        playerSprite.sprite = spinAnim[6];
        yield return new WaitForSeconds(0.1f);
        playerSprite.sprite = spinAnim[5];
        yield return new WaitForSeconds(0.1f);
        playerSprite.sprite = spinAnim[4];
        yield return new WaitForSeconds(0.1f);
        playerSprite.sprite = spinAnim[3];
        yield return new WaitForSeconds(0.1f);
        playerSprite.sprite = spinAnim[2];
        yield return new WaitForSeconds(0.1f);
        playerSprite.sprite = spinAnim[1];
        yield return new WaitForSeconds(0.1f);
        playerSprite.sprite = spinAnim[0];
        yield return new WaitForSeconds(0.1f);
        playerSprite.sprite = jumpAnim[2];
        isSpinning = false; 
    }

    IEnumerator TripAnim()
    {
        playerSprite.sprite = tripAnim[0];
        yield return new WaitForSeconds(0.1f);
        playerSprite.sprite = tripAnim[1];
        yield return new WaitForSeconds(0.1f);
        playerSprite.sprite = tripAnim[0];
        yield return new WaitForSeconds(0.1f);
        playerSprite.sprite = tripAnim[1];
        yield return new WaitForSeconds(0.1f);
        playerSprite.sprite = tripAnim[0];
        yield return new WaitForSeconds(0.1f);
        playerSprite.sprite = tripAnim[1];
        yield return new WaitForSeconds(0.1f);
        playerSprite.sprite = tripAnim[2];
        yield return new WaitForSeconds(0.1f);
        playerSprite.sprite = tripAnim[3];
        yield return new WaitForSeconds(0.1f);
    }
}

