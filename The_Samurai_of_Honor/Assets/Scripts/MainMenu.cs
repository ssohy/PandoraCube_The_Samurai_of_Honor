using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void GameStart()
    {
        SceneManager.LoadScene("Day1");
    }

    public void MaskPage()
    {
        SceneManager.LoadScene("Mask");
    }
}
