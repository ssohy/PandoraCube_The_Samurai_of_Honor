using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectManager : MonoBehaviour
{
    public GameObject bowelPrefab;
    public GameObject skullPrefab;
    public GameObject samuraiPrefab;


    GameObject[] bowel;
    GameObject[] samurai;
    GameObject[] skull;


    GameObject[] targetPool;

    void Awake()
    {
        bowel = new GameObject[10]; // 몇개?
        samurai = new GameObject[10]; // 몇개?
        skull = new GameObject[20]; // 몇개?

        Generate();
    }

    void Generate()
    {
        //#1.Enemy
        for (int index = 0; index < samurai.Length; index++)
        {
            samurai[index] = Instantiate(samuraiPrefab);
            samurai[index].SetActive(false);
        }

        for (int index = 0; index < bowel.Length; index++)
        {
            bowel[index] = Instantiate(bowelPrefab);
            bowel[index].SetActive(false);
        }
        for (int index = 0; index < skull.Length; index++)
        {
            skull[index] = Instantiate(skullPrefab);
            skull[index].SetActive(false);

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
                targetPool[index].SetActive(true);
                return targetPool[index];
            }
        }

        return null;
    }

    public GameObject[] GetPool(string type)
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

        return targetPool;
    }
}
