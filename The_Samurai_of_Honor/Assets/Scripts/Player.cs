using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    float h, v;
    Vector3 moveV;
    public float moveSpeed; // 이동 속도
    public GameObject player;
    public int hp;

    void Start()
    {
        player = GameObject.Find("Player");
        hp = 100;

    }

    void Update()
    {
        Move();
        Attack();
    }

    //#.이동(ui추가할 때 버튼으로 수정할 예정)
    void Move() // 현재는 편의성을 위해 WASD로 이동중
    {
        h = Input.GetAxisRaw("Horizontal");
        v = Input.GetAxisRaw("Vertical");

        moveV = new Vector3(h, 0, v).normalized;

        transform.position += moveV * moveSpeed * Time.deltaTime;
        
    }

    //#.공격
    void Attack() // 현재는 편의성을 위해 좌클릭, 우클릭으로 공격
    {
        if (Input.GetMouseButtonDown(0)) // 기본 공격
        {

        }

        if (Input.GetMouseButtonDown(1)) // 더블 공격
        {

        }
    }

}
