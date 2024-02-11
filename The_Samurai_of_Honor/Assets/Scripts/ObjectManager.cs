using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectManager : MonoBehaviour
{
    public GameManager gameManager;
    public GameObject bowelPrefab;
    public GameObject skullPrefab;
    public GameObject samuraiPrefab;


    GameObject[] bowel;
    GameObject[] samurai;
    GameObject[] skull;
    GameObject[] targetPool;

    public int playerHp;
    public bool isDay;
    public int enemyCnt;
    public int currentDay;
    int samuraiCnt;
    int bowelCnt;
    int skullCnt;

    private DataManager dataManager;
    int hp, cnt, day;
    void Awake()
    {
        dataManager = DataManager.GetInstance();
        currentDay = dataManager.GetCurrentDay();
        isDay = dataManager.GetIsDay();
        //currentDay = 1;
        //isDay = true;
        SpawnEnemyCount();
        bowel = new GameObject[bowelCnt]; // 몇개?
        samurai = new GameObject[samuraiCnt]; // 몇개?
        skull = new GameObject[skullCnt]; // 몇개?
        Generate();
        
    }

    void SpawnEnemyCount()
    {

        switch (currentDay)
        {
            case 1:
                samuraiCnt = 40;
                bowelCnt = 20;
                skullCnt = 40;
                break;
            case 2:
                samuraiCnt = 60;
                bowelCnt = 30;
                skullCnt = 50;
                break;
            case 3:
                samuraiCnt = 60;
                bowelCnt = 50;
                skullCnt = 100;
                break;
        }
    }
    void Generate()
    {
        if (samurai != null)
        {
            for (int index = 0; index < samurai.Length; index++)
            {
                //Debug.Log("사무라이 생성중 " + index);
                samurai[index] = Instantiate(samuraiPrefab);
                samurai[index].SetActive(false);
            }
        }
        else
        {
            Debug.LogError("samurai 배열이 null");
        }

        if (bowel != null)
        {
            for (int index = 0; index < bowel.Length; index++)
            {
                bowel[index] = Instantiate(bowelPrefab);
                bowel[index].SetActive(false);
            }
        }
        else
        {
            Debug.LogError("bowel 배열이 null");
        }

        if (skull != null)
        {
            for (int index = 0; index < skull.Length; index++)
            {
                skull[index] = Instantiate(skullPrefab);
                skull[index].SetActive(false);
            }
        }
        else
        {
            Debug.LogError("skull 배열이 null");
        }
    }

    public GameObject MakeObj(string type)
    {
        switch (type)
        {
            case "Samurai":
                targetPool = samurai;
                break;
            case "Bowel":
                targetPool = bowel;
                break;
            case "Skull":
                targetPool = skull;
                break;
        }

        for (int index = 0; index < targetPool.Length; index++)
        {
            if (!targetPool[index].activeSelf)
            {
                //targetPool[index].SetActive(true);
                return targetPool[index];
            }
        }
        return null;
    }

    
}
