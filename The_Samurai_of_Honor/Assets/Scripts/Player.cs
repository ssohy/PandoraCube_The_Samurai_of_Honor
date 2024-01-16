using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int moveSpeed = 2; // 이동 속도
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
        if (Input.GetKey(KeyCode.A))
        {
            player.transform.Translate(new Vector3(-moveSpeed * Time.deltaTime, 0, 0));
        }

        if (Input.GetKey(KeyCode.D))
        {
            player.transform.Translate(new Vector3(moveSpeed * Time.deltaTime, 0, 0) );
        }

        if (Input.GetKey(KeyCode.W))
        {
            player.transform.Translate(new Vector3(0, 0, moveSpeed * Time.deltaTime));
        }
        if (Input.GetKey(KeyCode.S))
        {
            player.transform.Translate(new Vector3(0, 0, -moveSpeed * Time.deltaTime));
        }
    }

    //#.공격
    void Attack()
    {
        if (Input.GetMouseButtonDown(0))
        {

        }
    }

}
