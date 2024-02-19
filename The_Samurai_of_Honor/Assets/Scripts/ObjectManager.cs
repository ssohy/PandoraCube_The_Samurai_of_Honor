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

    private DataManager dataManager;
    int samuraiCnt, bowelCnt, skullCnt;

    void Awake()
    {
        dataManager = DataManager.GetInstance();
        int currentDay = dataManager.GetCurrentDay();
        bool isDay = dataManager.GetIsDay();

        // 스폰할 적의 수 설정
        SetEnemyCounts(currentDay);

        // 배열 초기화
        bowel = new GameObject[bowelCnt];
        samurai = new GameObject[samuraiCnt];
        skull = new GameObject[skullCnt];

        // 적 생성
        GenerateEnemies();
    }

    void SetEnemyCounts(int currentDay)
    {
        switch (currentDay)
        {
            case 1:
                samuraiCnt = 50;
                bowelCnt = 30;
                skullCnt = 60;
                break;
            case 2:
                samuraiCnt = 70;
                bowelCnt = 50;
                skullCnt = 80;
                break;
            case 3:
                samuraiCnt = 60;
                bowelCnt = 80;
                skullCnt = 140;
                break;
        }
    }

    void GenerateEnemies()
    {
        GenerateObjects(samurai, samuraiPrefab);
        GenerateObjects(bowel, bowelPrefab);
        GenerateObjects(skull, skullPrefab);
    }

    void GenerateObjects(GameObject[] array, GameObject prefab)
    {
        if (array != null)
        {
            for (int i = 0; i < array.Length; i++)
            {
                array[i] = Instantiate(prefab);
                array[i].SetActive(false);
            }
        }
        else
        {
            Debug.LogError($"{prefab.name} 배열null");
        }
    }

    public GameObject MakeObj(string type)
    {
        GameObject[] targetPool = null;

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
        if (targetPool != null)
        {
            foreach (var obj in targetPool)
            {
                if (!obj.activeSelf)
                {
                    return obj;
                }
            }
        }
        return null;
    }
}