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

    void Awake()
    {
        spawnList = new List<Spawn>();
        enemyObjs = new string[] { "Samurai", "Bowel", "Skull"};

        currentDay = 1;
        dTime = 0;
        isDay = true;
        IsDay();
        DayStart();
    }


    public void DayStart()
    {
        //#.Stage UI Load
        //stageAnim.SetTrigger("On");
        //stageAnim.GetComponent<Text>().text = "Stage " + stage + "\nStart";
        //clearAnim.GetComponent<Text>().text = "Stage " + stage + "\nClear";

        //#.Enemy Spawn File Read
        ReadSpawnFile();

        //#.Fade In
        //fadeAnim.SetTrigger("In");
    }

    // Day별 낮, 밤 타이머 설정하기
    void IsDay()
    {
        if(currentDay == 3)
        {
            // 낮 15분, 밤 15분
            if (isDay)
            {
                dTime += Time.deltaTime;
                if (dTime >= 900f)
                {
                    Debug.Log("15분 경과");
                    dTime = 0;
                    isDay = false;
                }
            }
            else
            {
                dTime += Time.deltaTime;
                if (dTime >= 900f)
                {
                    Debug.Log("15분 경과");
                    dTime = 0;
                    isDay = true;
                }
            }
        }
        else
        {
            // 낮 10분, 밤 15분
            if (isDay)
            {
                dTime += Time.deltaTime;
                Debug.Log("타이머 시작");
                if (dTime >= 600f)
                {
                    Debug.Log("10분 경과");
                    dTime = 0;
                    isDay = false;
                }
            }
            else
            {
                dTime += Time.deltaTime;
                Debug.Log("밤 시작");
                if (dTime >= 900f)
                {
                    Debug.Log("15분 경과");
                    dTime = 0;
                    isDay = true;
                }
            }
        }
        

    }

    // 적 스폰 파일
    void ReadSpawnFile()
    {
        //#1.변수 초기화
        spawnList.Clear();
        spawnIndex = 0;
        spawnEnd = false;

        //#2. 리스폰 파일 읽기
        TextAsset textFile = Resources.Load("Day" + currentDay) as TextAsset;
        StringReader stringReader = new StringReader(textFile.text);

        //#3. 한 줄씩 데이터 저장
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

        Rigidbody rigid = enemy.GetComponent<Rigidbody>();

        Enemy enemyLogic = enemy.GetComponent<Enemy>();
        enemyLogic.player = player;
        enemyLogic.gameManager = this;
        enemyLogic.objectManager = objectManager;

        if (enemyPoint == 5 || enemyPoint == 6)
        {
            enemy.transform.Rotate(Vector3.back * 90);
            rigid.velocity = new Vector3(enemyLogic.speed * (-1), 0, -1);
        }
        else if (enemyPoint == 7 || enemyPoint == 8)
        {
            enemy.transform.Rotate(Vector3.forward * 90);
            rigid.velocity = new Vector3(enemyLogic.speed, 0, -1);
        }
        else
        {
            rigid.velocity = new Vector3(0, 0, enemyLogic.speed * (-1));
        }

        spawnIndex++;

        if (spawnIndex == spawnList.Count)
        {
            spawnEnd = true;
            return;
        }

        nextSpawnDelay = spawnList[spawnIndex].delay;
    }

}
