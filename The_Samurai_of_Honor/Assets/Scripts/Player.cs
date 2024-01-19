using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int moveSpeed = 20; // �̵� �ӵ�
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

    //#.�̵�(ui�߰��� �� ��ư���� ������ ����)
    void Move() // ����� ���Ǽ��� ���� WASD�� �̵���
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


        /*������ ����ȭ
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

    //#.����
    void Attack() // ����� ���Ǽ��� ���� ��Ŭ��, ��Ŭ������ ����
    {
        if (Input.GetMouseButtonDown(0)) // �⺻ ����
        {

        }

        if (Input.GetMouseButtonDown(1)) // ���� ����
        {

        }
    }

}
