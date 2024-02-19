using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    private DataManager dataManager;
    public GameObject maskManager;
    private void Awake()
    {
        dataManager = DataManager.GetInstance();
    }
    public void GameStart()
    {
        dataManager.SetEnemyCount(0);
        SceneManager.LoadScene("Day1");
    }

    public void MaskPage()
    {
        SceneManager.LoadScene("Mask");
    }

    public void BackPage()
    {
        SceneManager.LoadScene("Start");
    }
}
