using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class Videoplayer : MonoBehaviour
{
    public VideoPlayer video;
    public AudioSource backMusic;

    GameObject soundManager;
    void Awake()
    {
        soundManager = GameObject.Find("SoundManager");
        Time.timeScale = 0f;
        video.loopPointReached += EndReached;
    }

    void EndReached(UnityEngine.Video.VideoPlayer vp)
    {
        Time.timeScale = 1f;
        gameObject.SetActive(false);
        soundManager.GetComponent<SoundManager>().MusicPlay();
    }

    public void Skip()
    {
        video.Pause();
        Time.timeScale = 1f;
        gameObject.SetActive(false);
        soundManager.GetComponent<SoundManager>().MusicPlay();
    }
}
