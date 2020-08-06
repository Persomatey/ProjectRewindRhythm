using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Buttons : MonoBehaviour
{
    public bool isThereMusic; 
    private AudioSource source;
    private AudioSource musicSource; 
    public AudioClip selectionSFX;
    public AudioClip exitSFX;
    public AudioClip glitchSFX; 
    public bool fade;
    private float fadeAmount;
    private float fadeAmountM;

    void Start()
    {
        source = GetComponent<AudioSource>();
        if (isThereMusic)
        { musicSource = GameObject.Find("MusicObj").GetComponent<AudioSource>(); }
        fadeAmount = 0;
        fadeAmountM = 1; 
        fade = false; 
    }

    void Update()
    {
        if (fade)
        {
            fadeAmount += 0.002f;
            fadeAmountM -= 0.002f;
            GameObject.Find("ImageToFade").GetComponent<Image>().color = new Color(0, 0, 0, fadeAmount);
            GameObject.Find("ExitButton").GetComponent<Image>().color = new Color(255, 255, 255, fadeAmountM);
            GameObject.Find("ExitButton").transform.GetChild(0).GetComponent<Text>().color = new Color(0, 0, 0, fadeAmountM);
            GameObject.Find("StartButton").GetComponent<Image>().color = new Color(255, 255, 255, fadeAmountM);
            GameObject.Find("StartButton").transform.GetChild(0).GetComponent<Text>().color = new Color(0, 0, 0, fadeAmountM);
        }
    }

    public void BeginButton()
    {
        source.PlayOneShot(selectionSFX, 1.0f);
        if (isThereMusic)
        { StartCoroutine(FadeOut(musicSource, 0.8f)); }
        fade = true; 
        Invoke("BeginButton2", 2.39f);
        Invoke("RecordScratch", 1f);
    }

    void BeginButton2()
    {
        Debug.Log("Loading Credits"); 
        SceneManager.LoadScene("Credits");
    }

    void RecordScratch()
    {
        source.PlayOneShot(glitchSFX, 3f);
    }

    public void RetryButton()
    {
        source.PlayOneShot(selectionSFX, 1.0f);
        if(isThereMusic)
        { StartCoroutine(FadeOut(musicSource, 0.8f)); }
        Invoke("RetryButton2", 1f);
    }

    void RetryButton2()
    {
        Debug.Log("Loading Level1");
        SceneManager.LoadScene("Level1");
    }

    public void ExitButton()
    {
        source.Stop();
        if (isThereMusic)
        { StartCoroutine(FadeOut(musicSource, 0.8f)); }
        Invoke("ExitButton2", 1f);
    }

    void ExitButton2()
    {
        Debug.Log("Exiting Application");
        Application.Quit();
    }

    public void MainMenuButton()
    {
        source.PlayOneShot(selectionSFX, 1.0f);
        if (isThereMusic)
        { StartCoroutine(FadeOut(musicSource, 0.8f)); }
        Invoke("MainMenuButton2", 1f);
    }

    void MainMenuButton2()
    {
        Debug.Log("Loading MainMenu");
        SceneManager.LoadScene("MainMenu");
    }

    public static IEnumerator FadeOut(AudioSource audioSource, float FadeTime)
    {
        float startVolume = audioSource.volume;

        while (audioSource.volume > 0)
        {
            audioSource.volume -= startVolume * Time.deltaTime / FadeTime;

            yield return null;
        }

        audioSource.Stop();
        audioSource.volume = startVolume;
    }
}
