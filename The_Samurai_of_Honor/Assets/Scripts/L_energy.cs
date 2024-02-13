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

    private float elapsedTime = 0f;
    private bool isSpotlightActive = false;
    void Awake()
    {
        dataManager = DataManager.GetInstance();
        energyCnt = 0;
        isDay = dataManager.GetIsDay();
        playerLight.SetActive(false);
    }

    void Update()
    {
        isDay = dataManager.GetIsDay();
        if (!isDay)
        {
            NightStart();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Energy")
        {
            Debug.Log("������ �±׵�");
            Destroy(other.gameObject);
            energyCnt++;
        }
    }

    void NightStart()
    {
        energyTime = energyCnt * 60;

        // ��ü ���� ���� �ڵ� �߰�

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
