using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    private static SoundManager instance;
    public AudioSource backgroundMusic;


    public static SoundManager GetInstance()
    {
        return instance;
    }
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void MusicPause()
    {
        backgroundMusic.Pause();
    }

    public void MusicPlay()
    {
        backgroundMusic.Play();
    }
}
