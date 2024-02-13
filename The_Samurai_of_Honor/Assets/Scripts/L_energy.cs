using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class L_energy : MonoBehaviour
{
    private DataManager dataManager;

    public int energyCnt;
    public float energyTime;
    public bool isDay;

    public GameObject player;
    public GameObject playerLight;
    public GameObject gameLight;
    private float elapsedTime = 0f;
    private bool isSpotlightActive = false;

    public Material daySkybox;
    public Material nightSkybox;
    private bool isNight = false;
    void Awake()
    {
        dataManager = DataManager.GetInstance();
        energyCnt = 0;
        isDay = dataManager.GetIsDay();
        RenderSettings.skybox = daySkybox;
        playerLight.SetActive(false);
        gameLight.SetActive(true);
    }

    void Update()
    {
        isDay = dataManager.GetIsDay();
        if (!isDay)
        {
            NightStart();
        }
        else
        {
            DayStart();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Energy")
        {
            Debug.Log("에너지 태그됨");
            Destroy(other.gameObject);
            energyCnt++;
        }
    }

    void DayStart()
    {
        gameLight.SetActive(true);
        if (isNight)
        {
            RenderSettings.skybox = daySkybox;
            isNight = false;
        }
    }


    void NightStart()
    {
        energyTime = energyCnt * 10;
        gameLight.SetActive(false);
        if (!isNight)
        {
            RenderSettings.skybox = nightSkybox;
            isNight = true;
        }

        if (!isSpotlightActive)
        {
            playerLight.SetActive(true);
            isSpotlightActive = true;
        }

        elapsedTime += Time.deltaTime;

        if (elapsedTime >= energyTime)
        {
            playerLight.SetActive(false);
            isSpotlightActive = false;
        }
    }
}
