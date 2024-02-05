using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class L_energy : MonoBehaviour
{
    public GameObject objectManager;   

    public int energyCnt;
    public float energyTime;
    public bool isDay;

    void Awake()
    {
        objectManager = GameObject.Find("ObjectManager");
        energyCnt = 0;
        isDay = objectManager.GetComponent<ObjectManager>().isDay;
    }

    void Update()
    {

        if (!isDay)
        {
            NightStart();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Energy")
        {
            Destroy(gameObject);
            energyCnt++;
        }
    }

    void NightStart()
    {
        energyTime = energyCnt * 60;
        // 밝기 애니메이션 UI설정
    }


}
