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
        // 수직 레이아웃 그룹 시작
        GUILayout.BeginVertical();

        // 여기에 일부 GUI 요소 추가
        //GUILayout.Label("안녕, 세계!");
        //GUILayout.Button("클릭해주세요!");

        // 수직 레이아웃 그룹 종료
        GUILayout.EndVertical();
    }
}
