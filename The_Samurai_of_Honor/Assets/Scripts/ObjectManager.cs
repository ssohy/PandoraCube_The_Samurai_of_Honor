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

    bool IsDay;
    int currentDay;
    int samuraiCnt;
    int bowelCnt;
    int skullCnt;

    IEnumerator Start()
    {
        // GameManager가 초기화될 때까지 대기
        yield return new WaitForEndOfFrame();

        // ObjectManager 초기화
        Init();
    }

    public void Init()
    {
        SpawnEnemyCount();
        bowel = new GameObject[bowelCnt]; // 몇개?
        samurai = new GameObject[samuraiCnt]; // 몇개?
        skull = new GameObject[skullCnt]; // 몇개?
        Generate();
    }

    void SpawnEnemyCount()
    {
        IsDay = gameManager.isDay;
        currentDay = gameManager.currentDay;

        if (IsDay)
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




    void Generate()
    {
        if (samurai != null)
        {
            //#1.Enemy
            for (int index = 0; index < samurai.Length; index++)
            {
                Debug.Log("사무라이 생성중" + index);
                samurai[index] = Instantiate(samuraiPrefab);
                samurai[index].SetActive(false);
            }
        }
        else
        {
            Debug.LogError("samurai 배열이 null입니다.");
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
            Debug.LogError("bowel 배열이 null입니다.");
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
            Debug.LogError("skull 배열이 null입니다.");
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
        Debug.Log("type.Length : " + enemies.Length);
        for (int i = 0; i < enemies.Length; i++)
        {
            Debug.Log("여기니?");
            if (!enemies[i].activeSelf)
            {
                Debug.Log("아님 여기?");
                enemies[i].SetActive(true);
                Debug.Log("SetActive(true)");
                return enemies[i];
            }
        }
        return null;
    }
}
