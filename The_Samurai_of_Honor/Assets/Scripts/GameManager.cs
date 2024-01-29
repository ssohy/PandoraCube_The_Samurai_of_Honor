using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;

public class GameManager : MonoBehaviour
{
    public GameObject player;

    public string[] enemyObjs;
    public ObjectManager objectManager;


    public int currentDay;
    public float dTime;
    public bool isDay;

    public Transform[] spawnPoints;
    public List<Spawn> spawnList;
    public int spawnIndex;
    public bool spawnEnd;
    public float nextSpawnDelay;
    public float curSpawnDelay;
    public List<GameObject> enemyPool;


    IEnumerator Start()
    {
        currentDay = 1;
        spawnList = new List<Spawn>();
        enemyObjs = new string[] { "Samurai", "Bowel", "Skull" };
        dTime = 0;
        isDay = true;

        // GameManager ����
        yield return StartCoroutine(InitializeGameManager());

        // ObjectManager �ʱ�ȭ
        StartCoroutine(InitializeObjectManager());
    }

    IEnumerator InitializeGameManager()
    {
        // GameManager �ʱ�ȭ �۾� ����
        Debug.Log("GameManager �ʱ�ȭ �۾� ���� ��...");
        yield return null; // ���÷�, ������ �ʱ�ȭ �۾��� ���ٸ� �� �������� ��ٸ��ϴ�.
        Debug.Log("GameManager �ʱ�ȭ �Ϸ�!");
    }

    IEnumerator InitializeObjectManager()
    {
        // GameManager�� �ʱ�ȭ�� �Ŀ� ����� �� �ֵ��� ���
        yield return new WaitForEndOfFrame();

        // ObjectManager �ʱ�ȭ �۾� ����
        Debug.Log("ObjectManager �ʱ�ȭ �۾� ���� ��...");

        // ���⼭ objectManager�� �ʱ�ȭ�ϰų� �ٸ� �ʱ�ȭ �۾��� �����մϴ�.
        objectManager.Init();

        Debug.Log("ObjectManager �ʱ�ȭ �Ϸ�!");
    }
   

    void Update()
    {
        IsDay();
    }
    public void DayStart()
    {

        //#.Enemy Spawn File �о����
        //ReadSpawnFile();
    }

    // Day�� ��, �� Ÿ�̸� �����ϱ�
    void IsDay()
    {
        if(currentDay == 3)
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
        

    }

    // �� ������Ʈ Ǯ�� ����
    void CreateEnemyPool()
    {
        
        foreach (string enemyName in enemyObjs)
        {
            Debug.Log("enemyName : " + enemyName);
            GameObject enemyPrefab = objectManager.MakeObj(enemyName);
            //enemyPrefab.SetActive(false);
            enemyPool.Add(enemyPrefab);
        }
    }

    // ������Ʈ Ǯ������ �� ������Ʈ ��������
    GameObject GetEnemyFromPool(int index)
    {
        // �ش� �ε����� �� ������Ʈ ��������
        GameObject enemy = enemyPool[index];
        return enemy;
    }




    /*
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
       Debug.Log("-1Ȱ��ȭ");
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
       Debug.Log("0Ȱ��ȭ");
       // ������Ʈ Ǯ������ �� ������Ʈ ��������
       GameObject enemy = GetEnemyFromPool(enemyIndex);
       enemy.transform.position = spawnPoints[enemyPoint].position;

       Debug.Log("1Ȱ��ȭ");
       // Ȱ��ȭ
       enemy.SetActive(true);
       Debug.Log("2Ȱ��ȭ�Ϸ�");
       Rigidbody rigid = enemy.GetComponent<Rigidbody>();

       Enemy enemyLogic = enemy.GetComponent<Enemy>();
       enemyLogic.player = player;
       enemyLogic.gameManager = this;
       enemyLogic.objectManager = objectManager;

       // �� �̵� ���� ����

       spawnIndex++;

       if (spawnIndex == spawnList.Count)
       {
           spawnEnd = true;
           return;
       }

       nextSpawnDelay = spawnList[spawnIndex].delay;
   }*/
}

