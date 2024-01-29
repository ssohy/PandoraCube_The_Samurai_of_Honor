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
    //GameObject[] targetPool;

    public bool isDay;
    public int currentDay;
    int samuraiCnt;
    int bowelCnt;
    int skullCnt;



    void SpawnEnemyCount()
    {
        if (isDay)
        {
            switch (currentDay)
            {
                case 1:
                    samuraiCnt = 40;
                    bowelCnt = 0;
                    skullCnt = 0;
                    break;
                case 2:
                    samuraiCnt = 60;
                    bowelCnt = 0;
                    skullCnt = 0;
                    break;
                case 3:
                    samuraiCnt = 30;
                    bowelCnt = 20;
                    skullCnt = 40;
                    break;
            }
        }
        else
        {
            switch (currentDay)
            {
                case 1:
                    samuraiCnt = 0;
                    bowelCnt = 20;
                    skullCnt = 40;
                    break;
                case 2:
                    samuraiCnt = 0;
                    bowelCnt = 30;
                    skullCnt = 50;
                    break;
                case 3:
                    samuraiCnt = 30;
                    bowelCnt = 30;
                    skullCnt = 60;
                    break;
            }
        }
    }


    void Awake()
    {
        currentDay = 1;
        isDay = true;
        SpawnEnemyCount();
        bowel = new GameObject[bowelCnt]; // 몇개?
        samurai = new GameObject[samuraiCnt]; // 몇개?
        skull = new GameObject[skullCnt]; // 몇개?
        Generate();
    }



    void Generate()
    {
        if (samurai != null)
        {
            //#1.Enemy
            for (int index = 0; index < samurai.Length; index++)
            {
                //Debug.Log("사무라이 생성중" + index);
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
                return ActiveObj(samurai);
            case "Bowel":
                return ActiveObj(bowel);
            case "Skull":
                return ActiveObj(skull);
        }

        return null;
    }

    public GameObject ActiveObj(GameObject[] enemies)
    {
        for (int index = 0; index < enemies.Length; index++)
        {
            enemies[index].SetActive(true);
            return enemies[index];
        }
        return null;
    }
}
