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
    // 싱글톤 인스턴스 가져오기
    public static DataManager Instance
    {
        get
        {
            // 인스턴스가 없는 경우 새로 생성
            if (instance == null)
            {
                // 새 GameObject 생성
                GameObject singletonObject = new GameObject("DataManager");
                instance = singletonObject.AddComponent<DataManager>();
                DontDestroyOnLoad(singletonObject); // 씬 전환 시 파괴되지 않도록 설정
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
        // 싱글톤 패턴 구현
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // 씬 전환 시에도 유지되도록 설정
            InitializeData(); // 데이터 초기화 함수 호출
        }
        else
        {
            Destroy(gameObject); // 이미 다른 DataManager 인스턴스가 있다면 파괴
        }
    }

    // 데이터 초기화
    private void InitializeData()
    {
        playerHp = 100;
        enemyCnt = 0;
        currentDay = 1;
        isDay = true;
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
}
