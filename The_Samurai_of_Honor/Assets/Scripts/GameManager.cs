using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;

public class GameManager : MonoBehaviour
{
    public GameObject player;

    public Transform[] spawnMidwayEndPoint;
    public string[] enemyObjs;
    public ObjectManager objectManager;
    public GameObject diary;

    public int currentDay;
    public float dTime;
    public bool isDay;

    public Transform[] spawnPoints;
    public List<Spawn> spawnList;
    public int spawnIndex;
    public bool spawnEnd;
    public float nextSpawnDelay;
    public float curSpawnDelay;
    public int enemyCnt;
    public int playerHp;
    void Awake()
    {
        //Debug.Log("���� ��ġ : " + spawnMidwayEndPoint[0].position);
        player.transform.position = spawnMidwayEndPoint[0].position;

        currentDay = objectManager.GetComponent<ObjectManager>().currentDay;
        isDay = objectManager.GetComponent<ObjectManager>().isDay;
        spawnList = new List<Spawn>();
        enemyObjs = new string[] { "Samurai", "Bowel", "Skull" };
        dTime = 0;
        isDay = true;
        enemyCnt = 0;
        DayStart();
    }

    void Update()
    {
        curSpawnDelay += Time.deltaTime;
        if (curSpawnDelay > nextSpawnDelay && !spawnEnd)
        {
            SpawnEnemy();
            curSpawnDelay = 0;
        }
        //IsDay();
    }
    public void DayStart()
    {
        //#.Enemy Spawn File �о����
        ReadSpawnFile();
        diary = GameObject.Find("Diary");

    }

    // �� ���� ����
    void ReadSpawnFile()
    {
        //#1.���� �ʱ�ȭ
        spawnList.Clear();
        spawnIndex = 0;
        spawnEnd = false;

        //#2. ������ ���� �б�
        TextAsset textFile = Resources.Load("Day" + currentDay) as TextAsset;
        StringReader stringReader = new StringReader(textFile.text);

        //#3. �� �پ� ������ ����
        while (stringReader != null)
        {
            string line = stringReader.ReadLine();
            Debug.Log(line);

            if (line == null)
                break;

            Spawn spawnData = new Spawn();
            spawnData.delay = float.Parse(line.Split(',')[0]);
            spawnData.type = line.Split(',')[1];
            spawnData.point = int.Parse(line.Split(',')[2]);
            spawnList.Add(spawnData);
        }

        stringReader.Close();

        nextSpawnDelay = spawnList[0].delay;
    }
    


    void SpawnEnemy()
    {
        if (spawnIndex >= spawnList.Count)
        {
            spawnEnd = true;
            return;
        }

        int enemyIndex = 0;
        switch (spawnList[spawnIndex].type)
        {
            case "Samurai":
                enemyIndex = 0;
                break;
            case "Bowel":
                enemyIndex = 1;
                break;
            case "Skull":
                enemyIndex = 2;
                break;
        }
        int enemyPoint = spawnList[spawnIndex].point;
        GameObject enemy = objectManager.MakeObj(enemyObjs[enemyIndex]);
        enemy.transform.position = spawnPoints[enemyPoint].position;
        // Debug.Log("enemyIndex : " + enemyIndex);
        
        if (enemy != null)
        {
            enemy.transform.position = spawnPoints[enemyPoint].position;
            enemy.SetActive(true);

            Rigidbody rigid = enemy.GetComponent<Rigidbody>();
            Enemy enemyLogic = enemy.GetComponent<Enemy>();
            enemyLogic.player = player;
            enemyLogic.gameManager = this;
            enemyLogic.objectManager = objectManager;

            rigid.velocity = new Vector3(0, enemyLogic.speed * (-1));

            spawnIndex++;
        }
        if (spawnIndex == spawnList.Count)
        {
            spawnEnd = true;
            return;
        }

        nextSpawnDelay = spawnList[spawnIndex].delay;
    }

    void MaskClear()
    {
        if(enemyCnt >= 550)
        {

        }
        else if(enemyCnt >= 450)
        {
            
        }
        else if(enemyCnt >= 350)
        {

        }
    }

    /*
    // Day�� ��, �� Ÿ�̸� �����ϱ�
    void IsDay()
    {
        if (currentDay == 3)
        {
            // �� 15��, �� 15��
            if (isDay)
            {
                dTime += Time.deltaTime;

                if (dTime >= 900f)
                {
                    //Debug.Log("15�� ���");
                    dTime = 0;
                    isDay = false;
                }
            }
            else
            {
                dTime += Time.deltaTime;
                if (dTime >= 900f)
                {
                    //Debug.Log("15�� ���");
                    dTime = 0;
                    #if UNITY_EDITOR
                          UnityEditor.EditorApplication.isPlaying = false;
                    #else
                         Application.Quit();
                    #endif
                }
            }
        }
        else
        {
            // �� 10��, �� 15��
            if (isDay)
            {
                dTime += Time.deltaTime;
                if (dTime >= 600f)
                {
                    //Debug.Log("10�� ���");
                    dTime = 0;
                    isDay = false;
                }
            }
            else
            {
                dTime += Time.deltaTime;
                if (dTime >= 900f)
                {
                    //Debug.Log("15�� ���");
                    dTime = 0;
                    isDay = true;
                    currentDay++;
                }
            }
        }
    }*/
    
}

