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

        // GameManager 실행
        yield return StartCoroutine(InitializeGameManager());

        // ObjectManager 초기화
        StartCoroutine(InitializeObjectManager());
    }

    IEnumerator InitializeGameManager()
    {
        // GameManager 초기화 작업 수행
        Debug.Log("GameManager 초기화 작업 수행 중...");
        yield return null; // 예시로, 별도의 초기화 작업이 없다면 한 프레임을 기다립니다.
        Debug.Log("GameManager 초기화 완료!");
    }

    IEnumerator InitializeObjectManager()
    {
        // GameManager가 초기화된 후에 실행될 수 있도록 대기
        yield return new WaitForEndOfFrame();

        // ObjectManager 초기화 작업 수행
        Debug.Log("ObjectManager 초기화 작업 수행 중...");

        // 여기서 objectManager를 초기화하거나 다른 초기화 작업을 수행합니다.
        objectManager.Init();

        Debug.Log("ObjectManager 초기화 완료!");
    }
   

    void Update()
    {
        IsDay();
    }
    public void DayStart()
    {

        //#.Enemy Spawn File 읽어오기
        //ReadSpawnFile();
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
                    //Debug.Log("15분 경과");
                    dTime = 0;
                    isDay = false;
                }
            }
            else
            {
                dTime += Time.deltaTime;
                if (dTime >= 900f)
                {
                    //Debug.Log("15분 경과");
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
            // 낮 10분, 밤 15분
            if (isDay)
            {
                dTime += Time.deltaTime;
                if (dTime >= 600f)
                {
                    //Debug.Log("10분 경과");
                    dTime = 0;
                    isDay = false;
                }
            }
            else
            {
                dTime += Time.deltaTime;
                if (dTime >= 900f)
                {
                    //Debug.Log("15분 경과");
                    dTime = 0;
                    isDay = true;
                    currentDay++;
                }
            }
        }
        

    }

    // 적 오브젝트 풀링 생성
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

    // 오브젝트 풀링에서 적 오브젝트 가져오기
    GameObject GetEnemyFromPool(int index)
    {
        // 해당 인덱스의 적 오브젝트 가져오기
        GameObject enemy = enemyPool[index];
        return enemy;
    }




    /*
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
       Debug.Log("-1활성화");
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
       Debug.Log("0활성화");
       // 오브젝트 풀링에서 적 오브젝트 가져오기
       GameObject enemy = GetEnemyFromPool(enemyIndex);
       enemy.transform.position = spawnPoints[enemyPoint].position;

       Debug.Log("1활성화");
       // 활성화
       enemy.SetActive(true);
       Debug.Log("2활성화완료");
       Rigidbody rigid = enemy.GetComponent<Rigidbody>();

       Enemy enemyLogic = enemy.GetComponent<Enemy>();
       enemyLogic.player = player;
       enemyLogic.gameManager = this;
       enemyLogic.objectManager = objectManager;

       // 적 이동 로직 유지

       spawnIndex++;

       if (spawnIndex == spawnList.Count)
       {
           spawnEnd = true;
           return;
       }

       nextSpawnDelay = spawnList[spawnIndex].delay;
   }*/
}

