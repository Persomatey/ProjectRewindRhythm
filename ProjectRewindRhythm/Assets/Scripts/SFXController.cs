using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXController : MonoBehaviour
{
    private AudioSource source; 
    public AudioClip sfxClip;
    public float[] sfxTimings; 
    public int sfxIndex; 

    void Start()
    {
        source = GetComponent<AudioSource>(); 
        sfxIndex = 0; 
        Invoke("DelayedPlaySFX", sfxTimings[sfxIndex]); 
    }

    void DelayedPlaySFX()
    {
        Debug.Log("SFX Invokation " + sfxIndex); 
        source.PlayOneShot(sfxClip);
        if (sfxIndex + 1 < sfxTimings.Length)
        {
            sfxIndex++;
            Invoke("DelayedPlaySFX", sfxTimings[sfxIndex]);
        }
    }
}
