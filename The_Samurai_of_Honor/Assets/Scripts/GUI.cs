using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GUI : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnGUI()
    {
        // ���� ���̾ƿ� �׷� ����
        GUILayout.BeginVertical();

        // ���⿡ �Ϻ� GUI ��� �߰�
        //GUILayout.Label("�ȳ�, ����!");
        //GUILayout.Button("Ŭ�����ּ���!");

        // ���� ���̾ƿ� �׷� ����
        GUILayout.EndVertical();
    }
}
