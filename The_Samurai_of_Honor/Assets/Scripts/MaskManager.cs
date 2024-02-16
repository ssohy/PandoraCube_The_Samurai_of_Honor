using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class MaskManager : MonoBehaviour
{
    public List<Mask> mask = new List<Mask>();

    void Awake()
    {
        LoadMask();
    }

    void LoadMask()
    {
        TextAsset jsonFile = Resources.Load<TextAsset>("mask");

        if (jsonFile != null)
        {
            string json = jsonFile.text;
            mask = JsonUtility.FromJson<List<Mask>>(json);
        }
        else
        {
            Debug.LogError("颇老 给茫摆促 惨部府");
        }
    }

    public void UpdateMaskStatus(int index)
    {
        if (index >= 0 && index < mask.Count)
        {
            mask[index].unlocked = true;
            SaveMask();
        }
    }

    private void SaveMask()
    {
        string path = Path.Combine(Application.streamingAssetsPath, "mask.json");
        string json = JsonUtility.ToJson(mask, true);
        File.WriteAllText(path, json);
    }

}
