using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    float h, v;
    Vector3 moveV;
    public float moveSpeed; // �̵� �ӵ�
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

    //#.�̵�(ui�߰��� �� ��ư���� ������ ����)
    void Move() // ����� ���Ǽ��� ���� WASD�� �̵���
    {
        h = Input.GetAxisRaw("Horizontal");
        v = Input.GetAxisRaw("Vertical");

        moveV = new Vector3(h, 0, v).normalized;

        transform.position += moveV * moveSpeed * Time.deltaTime;
        
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
