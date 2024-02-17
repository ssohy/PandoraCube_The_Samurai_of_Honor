using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    private static DataManager instance;

    // ������ ����
    public int playerHp;
    public int enemyCnt;
    public int currentDay;
    public bool isDay;
    public int butterflyCnt;
    // �̱��� �ν��Ͻ� ��������
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

    //#1. �÷��̾� HP
    public int GetPlayerHp()
    {
        return playerHp;
    }

    public void SetPlayerHp(int health)
    {
        playerHp = health;
    }

    //#2. ���� Enemy ��
    public int GetEnemyCount()
    {
        return enemyCnt;
    }

    public void SetEnemyCount(int count)
    {
        enemyCnt = count;
    }

    //#3. ���� Day
    public int GetCurrentDay()
    {
        return currentDay;
    }

    public void SetCurrentDay(int day)
    {
        currentDay = day;
    }

    //#4. ���ΰ� ���ΰ�
    public bool GetIsDay()
    {
        return isDay;
    }

    public void SetIsDay(bool IsD)
    {
        isDay = IsD;
    }

    //#5. ����
    public int GetButterfly()
    {
        return butterflyCnt;
    }

    public void SetButterfly(int btf)
    {
        butterflyCnt = btf;
    }
}
