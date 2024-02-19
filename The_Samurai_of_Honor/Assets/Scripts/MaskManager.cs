using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Newtonsoft.Json;

public class MaskManager : MonoBehaviour
{
    public List<Mask> mask = new List<Mask>();
    public GameObject mA;
    public GameObject mB;
    public GameObject mC;

    public GameObject LA;
    public GameObject LB;
    public GameObject LC;

    int enemyKill;
    DataManager dataManager;
    void Awake()
    {
        dataManager = DataManager.GetInstance();
        LoadMask();
        MaskClear();
    }
    public void MaskClear()
    {
        enemyKill = dataManager.GetEnemyCount();
        if (enemyKill >= 350)
            UpdateMaskStatus(0);
        if (enemyKill >= 450)
            UpdateMaskStatus(1);
        if (enemyKill >= 550)
            UpdateMaskStatus(2);
    }
    void LoadMask()
    {
        TextAsset jsonFile = Resources.Load<TextAsset>("mask");

        if (jsonFile != null)
        {
            string json = jsonFile.text;
            mask = JsonConvert.DeserializeObject<MaskData>(json).mask;
            Debug.Log("����ũ ������ �ε� �Ϸ�: " + mask.Count + "���� ����ũ�� �ε�Ǿ����ϴ�.");
        }
        else
        {
            Debug.LogError("���� ��ã�ڴ� �Ҳ���");
        }
    }

    public void UpdateMaskStatus(int index)
    {

        if (index >= 0 && index < mask.Count)
        {
            Debug.Log("�ε����� �յ� : " + index);
            mask[index].unlocked = true;
            SaveMask();
           Debug.Log("��������Ϸ�");
            MaskUIUpdate(index);
        }
        else
        {
            Debug.Log("�����������");
        }
        
    }

    private void SaveMask()
    {
        string path = Path.Combine(Application.streamingAssetsPath, "mask.json");
        MaskData data = new MaskData();
        data.mask = mask;
        string json = JsonConvert.SerializeObject(data, Formatting.Indented);
        File.WriteAllText(path, json);
    }

    void MaskUIUpdate(int idx)
    {
        //Debug.Log("��ݿ��� : " + mask[idx].unlocked);
        switch (idx)
        {
            case 0:
                mA.SetActive(true);
                LA.SetActive(false);
                break;
            case 1:
                mB.SetActive(true);
                LB.SetActive(false);
                break;
            case 2:
                mC.SetActive(true);
                LC.SetActive(false);
                break;
        }
        Debug.Log("���UI�����Ϸ� : " + mask[idx].unlocked);

    }
}

[System.Serializable]
public class MaskData
{
    public List<Mask> mask;
}
