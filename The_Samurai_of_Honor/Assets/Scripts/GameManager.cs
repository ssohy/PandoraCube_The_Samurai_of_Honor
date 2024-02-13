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
    public bool isDay;

    public Transform[] spawnPoints;
    public List<Spawn> spawnList;
    public int spawnIndex;
    public bool spawnEnd;
    public float nextSpawnDelay;
    public float curSpawnDelay;
    public int playerHp;

    private DataManager dataManager;
    int hp, cnt, day;

    public GameObject pauseBack;
    public GameObject restart;
    void Awake()
    {
        dataManager = DataManager.GetInstance();
        playerHp =  dataManager.GetPlayerHp();
        currentDay = dataManager.GetCurrentDay();
        isDay = dataManager.GetIsDay();

        player.transform.position = spawnMidwayEndPoint[0].position;

        spawnList = new List<Spawn>();
        enemyObjs = new string[] { "Samurai", "Bowel", "Skull" };
        //isDay = true;
        //enemyCnt = 0;
        DayStart();
        pauseBack.SetActive(false);
        restart.SetActive(false);
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
    public void ReadSpawnFile()
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
        //Debug.Log("��ġ : " + enemy.transform.position);

        if (enemy != null)
        {
            enemy.SetActive(true);
            enemy.GetComponent<Enemy>().OnEnable();
            spawnIndex++;
        }
        if (spawnIndex == spawnList.Count)
        {
            spawnEnd = true;
            return;
        }

        nextSpawnDelay = spawnList[spawnIndex].delay;
    }
    public void PauseGame()
    {
        //Debug.Log("��ư ����");
        pauseBack.SetActive(!pauseBack.activeSelf);
        restart.SetActive(!restart.activeSelf);

        if (Time.timeScale == 0f)
        {
            Time.timeScale = 1f; // ���� �簳
        }
        else
        {
            Time.timeScale = 0f; // ���� �Ͻ�����
        }
    }

    public void RestartGame()
    {
        PauseGame();
    }
    /*
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
    }*/


}

