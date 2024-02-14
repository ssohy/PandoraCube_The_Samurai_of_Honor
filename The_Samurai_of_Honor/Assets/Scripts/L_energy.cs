using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class L_energy : MonoBehaviour
{
    private DataManager dataManager;

    public int energyCnt;
    public float energyTime;
    public bool isDay;

    public GameObject player;
    public GameObject playerLight;
    public GameObject gameLight;
    private bool isSpotlightActive = false;

    public Material daySkybox;
    public Material nightSkybox;
    private bool isNight = false;

    public Slider energySlider;
    void Awake()
    {
        dataManager = DataManager.GetInstance();
        energyCnt = 0;
        isDay = dataManager.GetIsDay();
        RenderSettings.skybox = daySkybox;
        playerLight.SetActive(false);
        gameLight.SetActive(true);
        InitializeEnergyBar();

    }

    void Update()
    {
        isDay = dataManager.GetIsDay();
        if (!isDay)
        {
            NightStart();
            energyTime -= Time.deltaTime;
            UpdateEnergyBar(energyTime);
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
            energyTime = energyCnt * 10;
            UpdateEnergyBar(energyTime);
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

        if (energyTime <= 0)
        {
            playerLight.SetActive(false);
            isSpotlightActive = false;
        }
    }

    void InitializeEnergyBar()
    {
        energySlider.maxValue = 100;
        energySlider.value = 0;
    }

    public void UpdateEnergyBar(float newEnergy)
    {
        energySlider.value = newEnergy;
    }

}
