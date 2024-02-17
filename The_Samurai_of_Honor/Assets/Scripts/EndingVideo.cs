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

    
    public GameObject soundManager;

    public GameObject gameManager;
    int currentDay;
    bool isDay;

    void Awake()
    {
        dataManager = DataManager.GetInstance();
        soundManager = GameObject.Find("SoundManager");
        gameManager = GameObject.Find("GameManager");
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
        soundManager.GetComponent<SoundManager>().MusicPause();
        playEnding1.gameObject.SetActive(true);
        playEnding1.loopPointReached += EndReached;
    }

    private void OnTriggerEnter(Collider other)
    {
        //�÷��̾ end���̾�� ������ �������
        if(other.tag == "Player")
        {
            //gameManager.GetComponent<GameManager>().MaskClear();
            currentDay = 1;
            isDay = true;
            dataManager.SetCurrentDay(currentDay);
            dataManager.SetIsDay(isDay);
            dataManager.SetButterfly(0);
            dataManager.SetEnemyCount(0);
            gameManager.GetComponent<GameManager>().EndGame();
           //SceneManager.LoadScene("Strat");
            Debug.Log("������ �ʱ�ȭ");
            PlayEndingVideo();
        }
    }
    void EndReached(UnityEngine.Video.VideoPlayer vp)
    {
        gameObject.SetActive(false); // ���� ���� ��Ȱ��ȭ
        SceneManager.LoadScene("Start"); // start ������ ��ȯ
    }

    public void Skip()
    {
        playEnding1.Pause();
        Time.timeScale = 1f;
        gameObject.SetActive(false);
        SceneManager.LoadScene("Start"); // start ������ ��ȯ
    }

}
