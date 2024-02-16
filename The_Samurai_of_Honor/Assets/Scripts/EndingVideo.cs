using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class EndingVideo : MonoBehaviour
{
    private DataManager dataManager;
    int butterflyCnt;
    public GameObject realEnding;
    public GameObject normalEnding;
    public VideoPlayer realEnding1;
    public VideoPlayer normalEnding1;
    GameObject playEnding;
    VideoPlayer playEnding1;
    void Awake()
    {
        dataManager = DataManager.GetInstance();
    }

    private void PlayEndingVideo()
    {
        StartEndingVideo();
        if (playEnding != null)
        {
            playEnding1.Play();
            playEnding.SetActive(true);
        }
    }
    void StartEndingVideo()
    {
        butterflyCnt = dataManager.GetButterfly();
        if (butterflyCnt >= 6)
        {
            playEnding = realEnding;
            playEnding1 = realEnding1;

        }
        else
        {
            playEnding = normalEnding;
            playEnding1 = normalEnding1;
        }
        playEnding1.gameObject.SetActive(true);
        playEnding1.loopPointReached += EndReached;
    }

    private void OnTriggerEnter(Collider other)
    {
        //플레이어가 end다이어리를 먹으면 영상재생
        if(other.tag == "Player")
        {
            PlayEndingVideo();
        }
    }
    void EndReached(UnityEngine.Video.VideoPlayer vp)
    {
        SceneManager.LoadScene("Start"); // start 씬으로 전환
    }
}
