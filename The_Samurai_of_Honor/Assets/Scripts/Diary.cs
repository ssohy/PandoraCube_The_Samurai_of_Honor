using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Diary : MonoBehaviour
{
    public GameObject diaryImage;

    void Awake()
    {
        diaryImage.SetActive(false);    
    }
    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            Debug.Log("다이어리 태그됨");
            diaryImage.SetActive(true);
            Time.timeScale = 0f;
        }
    }

    public void UIClose()
    {
        diaryImage.SetActive(false);
        Time.timeScale = 1f;
    }
}
