using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int moveSpeed = 20; // 이동 속도
    public GameObject player;
    public int hp;
    void Start()
    {
        player = GameObject.Find("Player");
        hp = 100;
    }

    void FixedUpdate()
    {
        Move();
        Attack();
    }

    //#.이동(ui추가할 때 버튼으로 수정할 예정)
    void Move() // 현재는 편의성을 위해 WASD로 이동중
    {
        if (Input.GetKey(KeyCode.W))
        {
            transform.position += Vector3.forward * moveSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.position += Vector3.back * moveSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.position += Vector3.left * moveSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.position += Vector3.right * moveSpeed * Time.deltaTime;
        }


        /*벡터의 정규화
        float h, v;
        h = Input.GetAxis("Horizontal");
        v = Input.GetAxis("Vertical");

        Vector3 moveDir = Vector3.forward * v + Vector3.right * h;

        Rigidbody rb = player.GetComponent<Rigidbody>();
        rb.MovePosition(rb.position + moveDir.normalized * moveSpeed * Time.deltaTime);*/


        /*if (Input.GetKey(KeyCode.A))
        {
            player.transform.Translate(new Vector3(-1 , 0, 0).normalized * Time.deltaTime * moveSpeed);
            Debug.Log(new Vector3(-1, 0, 0).normalized * Time.deltaTime * moveSpeed);
        }

        if (Input.GetKey(KeyCode.D))
        {
            player.transform.Translate(new Vector3(1 , 0, 0).normalized * Time.deltaTime * moveSpeed);
        }

        if (Input.GetKey(KeyCode.W))
        {
            player.transform.Translate(new Vector3(0, 0, 1 ).normalized * Time.deltaTime * moveSpeed);
        }
        if (Input.GetKey(KeyCode.S))
        {
            player.transform.Translate(new Vector3(0, 0, -1 ).normalized * Time.deltaTime * moveSpeed);
        }*/


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
