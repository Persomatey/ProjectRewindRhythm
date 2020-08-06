using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CreditsScene : MonoBehaviour
{
    private AudioSource source;
    public AudioClip song;
    public AudioClip scratch; 

    public RectTransform textObj1;
    Vector3 pos1;
    public RectTransform textObj2;
    Vector3 pos2;
    public RectTransform textObj3;
    Vector3 pos3;
    public RectTransform textObj4;
    Vector3 pos4;

    void Start()
    {
        source = GetComponent<AudioSource>();
        source.PlayOneShot(song, 2);
        source.PlayOneShot(scratch, 0.5f);
        Invoke("StartLevel", 1.8f);
        pos1 = textObj1.position;
        pos2 = textObj2.position;
        pos3 = textObj3.position;
        pos4 = textObj4.position; 
    }

    void Update()
    {
        textObj1.Translate(-Vector3.up * Time.deltaTime * 1000);
        textObj2.Translate(-Vector3.up * Time.deltaTime * 1000);
        textObj3.Translate(-Vector3.up * Time.deltaTime * 1000); 
        textObj4.Translate(-Vector3.up * Time.deltaTime * 1000); 
    }

    void StartLevel()
    {
        SceneManager.LoadScene("Level1");
    }
}
