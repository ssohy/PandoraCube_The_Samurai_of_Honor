using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class Videoplayer : MonoBehaviour
{
    public VideoPlayer video;

    void Awake()
    {
        Time.timeScale = 0f;
        video.loopPointReached += EndReached;
    }

    void EndReached(UnityEngine.Video.VideoPlayer vp)
    {
        Time.timeScale = 1f;
        gameObject.SetActive(false); // ���ѷα� ������ ��Ȱ��ȭ
    }
}
