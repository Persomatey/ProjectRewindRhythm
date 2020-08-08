using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CreditsScene : MonoBehaviour
{
    private AudioSource source;
    public AudioClip song;
    public AudioClip scratch;

    private float speed; 

    public Transform textObj1;
    public Transform textObj2;
    public Transform textObj3;
    public Transform textObj4;
    public Transform textObj5;
    public Transform textObj6;
    public Transform textObj7;

    void Start()
    {
        source = GetComponent<AudioSource>();
        source.PlayOneShot(song, 1);
        source.PlayOneShot(scratch, 0.25f);
        Invoke("StartLevel", 2f);
        speed = 13; 
    }

    void Update()
    {
        textObj1.Translate(-Vector3.up * Time.deltaTime * speed);
        textObj2.Translate(-Vector3.up * Time.deltaTime * speed);
        textObj3.Translate(-Vector3.up * Time.deltaTime * speed); 
        textObj4.Translate(-Vector3.up * Time.deltaTime * speed);
        textObj5.Translate(-Vector3.up * Time.deltaTime * speed);
        textObj6.Translate(-Vector3.up * Time.deltaTime * speed);
        textObj7.Translate(-Vector3.up * Time.deltaTime * speed);
    }

    void StartLevel()
    {
        SceneManager.LoadScene("Level1");
    }
}
