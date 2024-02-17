using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    private static DataManager instance;

    // 데이터 변수
    public int playerHp;
    public int enemyCnt;
    public int currentDay;
    public bool isDay;
    public int butterflyCnt;
    // 싱글톤 인스턴스 가져오기
    public static DataManager Instance
    {
        get
        {
            if (instance == null)
            {
                GameObject singletonObject = new GameObject("DataManager");
                instance = singletonObject.AddComponent<DataManager>();
                DontDestroyOnLoad(singletonObject);
            }
            return instance;
        }
    }

    public static DataManager GetInstance()
    {
        return Instance;
    }


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            InitializeData();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void InitializeData()
    {
        playerHp = 100;
        enemyCnt = 0;
        currentDay = 1;
        isDay = true;
        butterflyCnt = 0;
}

    //#1. 플레이어 HP
    public int GetPlayerHp()
    {
        return playerHp;
    }

    public void SetPlayerHp(int health)
    {
        playerHp = health;
    }

    //#2. 죽인 Enemy 수
    public int GetEnemyCount()
    {
        return enemyCnt;
    }

    public void SetEnemyCount(int count)
    {
        enemyCnt = count;
    }

    //#3. 현재 Day
    public int GetCurrentDay()
    {
        return currentDay;
    }

    public void SetCurrentDay(int day)
    {
        currentDay = day;
    }

    //#4. 낮인가 밤인가
    public bool GetIsDay()
    {
        return isDay;
    }

    public void SetIsDay(bool IsD)
    {
        isDay = IsD;
    }

    //#5. 나비
    public int GetButterfly()
    {
        return butterflyCnt;
    }

    public void SetButterfly(int btf)
    {
        butterflyCnt = btf;
    }
}
