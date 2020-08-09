using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Linq;
using UnityEngine.UI;

public class HardEasyToggle : MonoBehaviour
{
    public AudioClip SFX;
    private AudioSource source;

    public GameObject easy;
    public GameObject hard; 

    private List<string> readList = new List<string>();
    private string readPath = "Assets/Scripts/ModeToggle.txt";
    string reed; 
    public int mode; // 0 = Easy | 1 = Hard 

    void Start()
    {
        source = GetComponent<AudioSource>();
        StatReader();

        if (mode == 0)
        {
            easy.SetActive(true);
            hard.SetActive(false);
        }
        if (mode == 1)
        {
            hard.SetActive(true);
            easy.SetActive(false);
        }
    }

    void Update()
    {
        if (mode == 0)
        {
            easy.SetActive(true);
            hard.SetActive(false);
        }
        if (mode == 1)
        {
            hard.SetActive(true);
            easy.SetActive(false);
        }
    }

    public void ChangeMode()
    {
        Debug.Log("Changing mode"); 
        if (mode == 0)
        {
            source.PlayOneShot(SFX, 1); 
            mode = 1;
            reed = "" + mode; 
            StatWriter();
        }
        else if (mode == 1)
        {
            source.PlayOneShot(SFX, 1);
            mode = 0;
            reed = "" + mode;
            StatWriter();
        }
    }

    public void StatReader()
    {
        Debug.Log("Reading sheet...");
        StreamReader reader = new StreamReader(readPath);

        string line = reader.ReadLine();
        reed = line; 

        reader.Close();

        mode = Int32.Parse(line);

        Debug.Log("...sheet read.");
    }

    public void StatWriter()
    {
        Debug.Log("Writing to sheet...");

        StreamWriter writer = new StreamWriter(readPath);

        writer.WriteLine(reed);
        
        writer.Close();

        Debug.Log("...sheet saved.");
    }
}
