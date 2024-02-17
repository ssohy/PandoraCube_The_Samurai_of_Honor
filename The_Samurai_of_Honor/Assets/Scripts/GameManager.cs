using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;
using TMPro;

public class GameManager : MonoBehaviour
{
    public GameObject player;
    public Transform[] spawnMidwayEndPoint;
    public string[] enemyObjs;
    public ObjectManager objectManager;
    public GameObject diary;

    public Transform[] spawnPoints;
    public List<Spawn> spawnList;
    public int spawnIndex;
    public bool spawnEnd;
    public float nextSpawnDelay;
    public float curSpawnDelay;
    public int playerHp;

    public GameObject pauseBack;
    public GameObject restart;
    public Slider hpSlider;
    public TMP_Text dayText;
    public TMP_Text killText;
    public GameObject maskManager;

    private DataManager dataManager;

    private int currentDay;
    private bool isDay;
    private int enemyKill;

    private void Awake()
    {
        Application.targetFrameRate = 60;
        dataManager = DataManager.GetInstance();
        playerHp = dataManager.GetPlayerHp();
        currentDay = dataManager.GetCurrentDay();
        isDay = dataManager.GetIsDay();

        player.transform.position = spawnMidwayEndPoint[0].position;

        spawnList = new List<Spawn>();
        enemyObjs = new string[] { "Samurai", "Bowel", "Skull" };

        DayStart();
        pauseBack.SetActive(false);
        restart.SetActive(false);
        InitializeHPBar();
    }

    void Update()
    {
        curSpawnDelay += Time.deltaTime;
        if (curSpawnDelay > nextSpawnDelay && !spawnEnd)
        {
            SpawnEnemy();
            curSpawnDelay = 0;
        }
        UpdateUI();
    }

    public void DayStart()
    {
        ReadSpawnFile();
        diary = GameObject.Find("Diary");
    }

    public void gameOver()
    {
        #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
        #else
                Application.Quit();
        #endif
    }
    public void EndGame()
    {
        //GetComponent<ItemInit>().InitializeItem();
        dataManager.SetPlayerHp(100);
        dataManager.SetButterfly(0);
        dataManager.SetCurrentDay(1);
        dataManager.SetEnemyCount(0);
        dataManager.SetIsDay(true);
        UpdateHPBar(100);
    }
    public void ReadSpawnFile()
    {
        spawnList.Clear();
        spawnIndex = 0;
        spawnEnd = false;

        TextAsset textFile = Resources.Load("Day" + currentDay) as TextAsset;
        StringReader stringReader = new StringReader(textFile.text);

        while (stringReader != null)
        {
            string line = stringReader.ReadLine();
            if (line == null)
                break;

            string[] data = line.Split(',');
            Spawn spawnData = new Spawn
            {
                delay = float.Parse(data[0]),
                type = data[1],
                point = int.Parse(data[2])
            };
            spawnList.Add(spawnData);
        }

        stringReader.Close();
        nextSpawnDelay = spawnList[0].delay;
    }

    private void SpawnEnemy()
    {
        if (spawnIndex >= spawnList.Count)
        {
            spawnEnd = true;
            return;
        }

        int enemyIndex = System.Array.IndexOf(enemyObjs, spawnList[spawnIndex].type);
        int enemyPoint = spawnList[spawnIndex].point;

        GameObject enemy = objectManager.MakeObj(enemyObjs[enemyIndex]);
        if (enemy != null)
        {
            enemy.transform.position = spawnPoints[enemyPoint].position;
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
        pauseBack.SetActive(!pauseBack.activeSelf);
        restart.SetActive(!restart.activeSelf);

        Time.timeScale = Time.timeScale == 0f ? 1f : 0f;
    }

    public void RestartGame()
    {
        PauseGame();
    }

    public void InitializeHPBar()
    {
        hpSlider.maxValue = 100;
        hpSlider.value = dataManager.GetPlayerHp();
    }

    public void UpdateHPBar(int newHP)
    {
        hpSlider.value = newHP;
    }

    private void UpdateUI()
    {
        dayText.text = "Day " + dataManager.GetCurrentDay().ToString();
        killText.text = dataManager.GetEnemyCount().ToString();
    }

    /*
    public void MaskClear()
    {
        enemyKill = dataManager.GetEnemyCount();
        if (enemyKill >= 0)
            maskManager.GetComponent<MaskManager>().UpdateMaskStatus(0);
        if (enemyKill >= 450)
            maskManager.GetComponent<MaskManager>().UpdateMaskStatus(1);
        if (enemyKill >= 550)
            maskManager.GetComponent<MaskManager>().UpdateMaskStatus(2);
    }*/

}

